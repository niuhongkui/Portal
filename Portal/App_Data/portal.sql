/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50714
Source Host           : localhost:3306
Source Database       : portal

Target Server Type    : MYSQL
Target Server Version : 50714
File Encoding         : 65001

Date: 2019-06-24 15:32:24
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `meal`
-- ----------------------------
DROP TABLE IF EXISTS `meal`;
CREATE TABLE `meal` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `Code` varchar(50) NOT NULL COMMENT '编码',
  `Name` varchar(50) NOT NULL COMMENT '套餐名称',
  `StationID` varchar(50) NOT NULL COMMENT '所属服务站',
  `IsActive` int(2) NOT NULL COMMENT '有效',
  `CreateDate` datetime NOT NULL COMMENT '创建时间',
  `ImgUrl` varchar(50) NOT NULL COMMENT '图片路径',
  `StationName` varchar(50) NOT NULL COMMENT '站点名称',
  `StaffCode` varchar(50) NOT NULL COMMENT '创建人',
  `StaffName` varchar(50) NOT NULL COMMENT '创建人',
  `Price` decimal(10,2) NOT NULL COMMENT '价格',
  `OriginalPrice` decimal(10,2) NOT NULL COMMENT '原价',
  PRIMARY KEY (`ID`),
  KEY `station_m_PK` (`StationID`),
  CONSTRAINT `station_m_PK` FOREIGN KEY (`StationID`) REFERENCES `station` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='优惠套餐';

-- ----------------------------
-- Records of meal
-- ----------------------------

-- ----------------------------
-- Table structure for `mealdetail`
-- ----------------------------
DROP TABLE IF EXISTS `mealdetail`;
CREATE TABLE `mealdetail` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `PID` varchar(50) NOT NULL COMMENT '外键',
  `ProductID` varchar(50) NOT NULL COMMENT '商品Id',
  `ProductName` varchar(50) NOT NULL COMMENT '商品名称',
  `Price` decimal(10,2) NOT NULL COMMENT '价格',
  `ImgUrl` varchar(255) NOT NULL COMMENT '图片',
  `Amount` decimal(10,2) NOT NULL COMMENT '数量',
  `OriginalPrice` decimal(10,2) NOT NULL COMMENT '原价',
  `ProductCode` varchar(50) NOT NULL COMMENT '商品ocde',
  PRIMARY KEY (`ID`),
  KEY `m_d_Pk` (`PID`),
  CONSTRAINT `m_d_Pk` FOREIGN KEY (`PID`) REFERENCES `meal` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='套餐明细';

-- ----------------------------
-- Records of mealdetail
-- ----------------------------

-- ----------------------------
-- Table structure for `oldprice`
-- ----------------------------
DROP TABLE IF EXISTS `oldprice`;
CREATE TABLE `oldprice` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `ProductID` varchar(50) NOT NULL COMMENT '商品ID',
  `Price` decimal(10,2) NOT NULL COMMENT '价格',
  `StaffCode` varchar(50) NOT NULL COMMENT '创建人编码',
  `StaffName` varchar(50) NOT NULL COMMENT '创建人',
  `CreateDate` datetime NOT NULL COMMENT '创建时间'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC COMMENT='历史价格';

-- ----------------------------
-- Records of oldprice
-- ----------------------------

-- ----------------------------
-- Table structure for `order`
-- ----------------------------
DROP TABLE IF EXISTS `order`;
CREATE TABLE `order` (
  `Id` varchar(45) NOT NULL,
  `OrderNo` varchar(45) DEFAULT NULL,
  `AtmId` varchar(45) DEFAULT NULL,
  `EndTime` datetime DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `State` int(11) DEFAULT NULL,
  `IsDelete` bit(1) DEFAULT NULL,
  `CreateUserId` varchar(45) DEFAULT NULL,
  `CreateBankId` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of order
-- ----------------------------

-- ----------------------------
-- Table structure for `price`
-- ----------------------------
DROP TABLE IF EXISTS `price`;
CREATE TABLE `price` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `ProductID` varchar(50) NOT NULL COMMENT '商品ID',
  `Price` decimal(10,2) NOT NULL COMMENT '价格',
  `StaffCode` varchar(50) NOT NULL COMMENT '创建人编码',
  `StaffName` varchar(50) NOT NULL COMMENT '创建人',
  `CreateDate` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`ID`),
  KEY `p_price_pk` (`ProductID`),
  CONSTRAINT `p_price_pk` FOREIGN KEY (`ProductID`) REFERENCES `product` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='商品价格';

-- ----------------------------
-- Records of price
-- ----------------------------

-- ----------------------------
-- Table structure for `product`
-- ----------------------------
DROP TABLE IF EXISTS `product`;
CREATE TABLE `product` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `Code` varchar(50) NOT NULL COMMENT '编码',
  `Name` varchar(50) NOT NULL COMMENT '名称',
  `TypeCode` varchar(50) NOT NULL COMMENT '类别编码',
  `TypeName` varchar(50) NOT NULL COMMENT '类别名称',
  `UnitCode` varchar(50) NOT NULL COMMENT '单位编码',
  `UnitName` varchar(50) NOT NULL COMMENT '单位名称',
  `StationID` varchar(50) NOT NULL COMMENT '服务站ID',
  `StationCode` varchar(50) NOT NULL COMMENT '服务站code',
  `StationName` varchar(50) NOT NULL COMMENT '服务站名称',
  `CreateDate` datetime NOT NULL COMMENT '创建时间',
  `IsActive` int(2) NOT NULL COMMENT '是否有效',
  `ImgUrl` varchar(50) NOT NULL COMMENT '图片',
  PRIMARY KEY (`ID`),
  KEY `station_pro_pk` (`StationID`),
  CONSTRAINT `station_pro_pk` FOREIGN KEY (`StationID`) REFERENCES `station` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='商品';

-- ----------------------------
-- Records of product
-- ----------------------------

-- ----------------------------
-- Table structure for `producttype`
-- ----------------------------
DROP TABLE IF EXISTS `producttype`;
CREATE TABLE `producttype` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `Code` varchar(50) NOT NULL COMMENT '编码',
  `Name` varchar(50) NOT NULL COMMENT '类别名称',
  `StationID` varchar(50) NOT NULL COMMENT '所属服务站',
  `IsActive` int(2) NOT NULL COMMENT '是否有效',
  `CreateDate` datetime NOT NULL COMMENT '创建时间',
  `StaffID` varchar(50) NOT NULL COMMENT '创建人名称',
  `StaffName` varchar(50) NOT NULL COMMENT '创建人名称',
  PRIMARY KEY (`ID`),
  KEY `Station_p_pk` (`StationID`),
  KEY `Staff_p_pk` (`StaffID`),
  CONSTRAINT `Staff_p_pk` FOREIGN KEY (`StaffID`) REFERENCES `staff` (`StationID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `Station_p_pk` FOREIGN KEY (`StationID`) REFERENCES `station` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='商品类别';

-- ----------------------------
-- Records of producttype
-- ----------------------------

-- ----------------------------
-- Table structure for `productunit`
-- ----------------------------
DROP TABLE IF EXISTS `productunit`;
CREATE TABLE `productunit` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `Name` varchar(50) NOT NULL COMMENT '名称名称',
  `Code` varchar(50) NOT NULL COMMENT '单位编码',
  `IsActive` int(2) NOT NULL COMMENT '有效',
  `CreateDate` datetime NOT NULL COMMENT '创建时间',
  `StaffID` varchar(50) NOT NULL COMMENT '创建staffid',
  `StaffName` varchar(50) NOT NULL COMMENT '创建人名称',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='商品单位';

-- ----------------------------
-- Records of productunit
-- ----------------------------

-- ----------------------------
-- Table structure for `staff`
-- ----------------------------
DROP TABLE IF EXISTS `staff`;
CREATE TABLE `staff` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `UserCode` varchar(50) NOT NULL COMMENT '编码',
  `UserName` varchar(50) NOT NULL COMMENT '名称',
  `PassWord` varchar(50) NOT NULL COMMENT '密码',
  `StationID` varchar(50) NOT NULL COMMENT '所属服务站',
  `IsActive` int(2) NOT NULL COMMENT '是否有效',
  `Phone` varchar(11) DEFAULT NULL COMMENT '手机号',
  `CreateDate` datetime NOT NULL COMMENT '创建时间',
  `StationName` varchar(50) NOT NULL COMMENT '服务站名称',
  `StationCode` varchar(50) NOT NULL COMMENT '服务站编码',
  PRIMARY KEY (`ID`),
  KEY `station_pk` (`StationID`),
  CONSTRAINT `station_pk` FOREIGN KEY (`StationID`) REFERENCES `station` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='员工';

-- ----------------------------
-- Records of staff
-- ----------------------------
INSERT INTO `staff` VALUES ('2', '2', '3', '2', '1', '1', '1', '2019-06-24 13:34:10', '2', '2');

-- ----------------------------
-- Table structure for `station`
-- ----------------------------
DROP TABLE IF EXISTS `station`;
CREATE TABLE `station` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `Code` varchar(255) NOT NULL COMMENT '编码',
  `Name` varchar(50) NOT NULL COMMENT '服务站名称',
  `Address` varchar(255) NOT NULL COMMENT '地址',
  `Latitude` decimal(10,6) NOT NULL DEFAULT '0.000000' COMMENT '纬度',
  `Longitude` decimal(10,6) NOT NULL DEFAULT '0.000000' COMMENT '经度',
  `IsActive` int(2) NOT NULL COMMENT '是否有效',
  `CreateDate` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`ID`),
  KEY `ID` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='服务站点';

-- ----------------------------
-- Records of station
-- ----------------------------
INSERT INTO `station` VALUES ('1', '1', '1', '1', '0.000000', '0.000000', '1', '2019-06-24 13:32:53');

-- ----------------------------
-- Table structure for `userinfo`
-- ----------------------------
DROP TABLE IF EXISTS `userinfo`;
CREATE TABLE `userinfo` (
  `ID` varchar(50) NOT NULL DEFAULT '' COMMENT '主键',
  `UserName` varchar(50) NOT NULL DEFAULT '' COMMENT '用户名',
  `UserCode` varchar(50) NOT NULL DEFAULT '' COMMENT '登录账号',
  `PassWord` varchar(50) NOT NULL DEFAULT '' COMMENT '密码',
  `IsMember` int(2) NOT NULL DEFAULT '0' COMMENT '是否会员',
  `IsActive` int(2) NOT NULL DEFAULT '1' COMMENT '是否可用用户',
  `CreateDate` datetime NOT NULL COMMENT '创建时间',
  `Phone` varchar(11) NOT NULL DEFAULT '' COMMENT '手机号',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='用户表';

-- ----------------------------
-- Records of userinfo
-- ----------------------------
