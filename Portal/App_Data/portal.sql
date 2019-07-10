/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50714
Source Host           : localhost:3306
Source Database       : portal

Target Server Type    : MYSQL
Target Server Version : 50714
File Encoding         : 65001

Date: 2019-07-10 17:43:16
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `cart`
-- ----------------------------
DROP TABLE IF EXISTS `cart`;
CREATE TABLE `cart` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `ProductID` varchar(50) NOT NULL COMMENT '商品ID',
  `UserInfoID` varchar(50) NOT NULL COMMENT '登录人ID',
  `CreateDate` datetime NOT NULL COMMENT '创建日期',
  `CartType` int(2) NOT NULL COMMENT '1正常商品，2促销，3套餐',
  PRIMARY KEY (`ID`),
  KEY `u_c_PK` (`UserInfoID`),
  CONSTRAINT `u_c_PK` FOREIGN KEY (`UserInfoID`) REFERENCES `userinfo` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='购物车';

-- ----------------------------
-- Records of cart
-- ----------------------------

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
  `IsMember` int(2) NOT NULL DEFAULT '0' COMMENT '会员专享',
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
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `OrderNo` varchar(50) NOT NULL COMMENT '单号',
  `StationID` varchar(50) NOT NULL COMMENT '所属服务站',
  `CreateDate` datetime NOT NULL COMMENT '创建时间',
  `State` int(2) NOT NULL COMMENT '1待支付,2已支付,3已取货,',
  `IsActive` int(2) NOT NULL COMMENT '是否有效',
  `UserID` varchar(50) NOT NULL COMMENT '下单人',
  `UserName` varchar(50) NOT NULL COMMENT '下单人名称',
  `Remark` varchar(255) DEFAULT NULL COMMENT '备注',
  `Amount` decimal(8,2) NOT NULL COMMENT '订单数量',
  `Money` varchar(255) NOT NULL COMMENT '订单金额',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='订单';

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
  `UnitCode` varchar(50) NOT NULL COMMENT '商品单位Code',
  `UnitName` varchar(50) NOT NULL COMMENT '商品单位Name',
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
INSERT INTO `product` VALUES ('2', 'P1245689452', '西红柿', 'T201907020152143118', '蔬菜', 'kg', '千克', '1', 'ST001', '东风小区', '2019-07-04 13:28:07', '1', '/images/nopic.png');
INSERT INTO `product` VALUES ('9fc3f390-a005-4a35-ac95-51d957c11e3e', 'P201907041037034385', '黄瓜', 'T201907020152143118', '蔬菜', 'kg', '千克', '1', 'ST001', '东风小区', '2019-07-04 13:28:24', '1', '/images/nopic.png');

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
  CONSTRAINT `Staff_p_pk` FOREIGN KEY (`StaffID`) REFERENCES `staff` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `Station_p_pk` FOREIGN KEY (`StationID`) REFERENCES `station` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='商品类别';

-- ----------------------------
-- Records of producttype
-- ----------------------------
INSERT INTO `producttype` VALUES ('d08eb24f-a4e4-43ab-8599-f1bdc96462e4', 'T201907020152143118', '蔬菜', '1', '1', '2019-07-02 13:53:27', '2', '东风小区');
INSERT INTO `producttype` VALUES ('e942c436-79d0-4631-b527-f448155b31cd', 'T201907020153077066', '水果', '1', '1', '2019-07-02 13:53:07', '2', '东风小区');

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
INSERT INTO `productunit` VALUES ('1', '斤', '500g', '1', '2019-07-02 15:42:55', '1', '222');
INSERT INTO `productunit` VALUES ('2', '千克', 'kg', '1', '2019-07-04 10:28:34', '1', '22');

-- ----------------------------
-- Table structure for `promot`
-- ----------------------------
DROP TABLE IF EXISTS `promot`;
CREATE TABLE `promot` (
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
  `PromotType` varchar(50) NOT NULL COMMENT '打折，直降，满减',
  `IsMember` int(2) NOT NULL DEFAULT '0' COMMENT '会员专享',
  PRIMARY KEY (`ID`),
  KEY `station_promot_PK` (`StationID`),
  CONSTRAINT `station_promot_PK` FOREIGN KEY (`StationID`) REFERENCES `station` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC COMMENT='促销主表';

-- ----------------------------
-- Records of promot
-- ----------------------------

-- ----------------------------
-- Table structure for `promotdetail`
-- ----------------------------
DROP TABLE IF EXISTS `promotdetail`;
CREATE TABLE `promotdetail` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `PID` varchar(50) NOT NULL COMMENT '外键',
  `ProductID` varchar(50) NOT NULL COMMENT '商品Id',
  `OriginalPrice` decimal(10,2) NOT NULL COMMENT '原价',
  PRIMARY KEY (`ID`),
  KEY `promot_d_Pk` (`PID`),
  CONSTRAINT `promot_d_Pk` FOREIGN KEY (`PID`) REFERENCES `promot` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC COMMENT='促销明显';

-- ----------------------------
-- Records of promotdetail
-- ----------------------------

-- ----------------------------
-- Table structure for `promotrule`
-- ----------------------------
DROP TABLE IF EXISTS `promotrule`;
CREATE TABLE `promotrule` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `PID` varchar(50) NOT NULL COMMENT '外键',
  `Text` varchar(50) NOT NULL COMMENT '文本显示',
  `ExePrice` decimal(10,4) NOT NULL DEFAULT '0.0000' COMMENT '用于计算价格，直降金额，打折比例，满减扣减',
  `Condition` decimal(10,4) NOT NULL COMMENT '满减条件',
  PRIMARY KEY (`ID`),
  KEY `promot_r_PK` (`PID`),
  CONSTRAINT `promot_r_PK` FOREIGN KEY (`PID`) REFERENCES `promot` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='促销规则';

-- ----------------------------
-- Records of promotrule
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
INSERT INTO `staff` VALUES ('2', 'admin', '东风小区', 'E1-0A-DC-39-49-BA-59-AB-BE-56-E0-57-F2-0F-88-3E', '1', '1', '15614385668', '2019-06-24 13:34:10', '东风小区', 'ST001');

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
INSERT INTO `station` VALUES ('1', 'ST001', '东风小区店', '新华路东风小学', '0.000000', '0.000000', '1', '2019-06-24 13:32:53');

-- ----------------------------
-- Table structure for `swiper`
-- ----------------------------
DROP TABLE IF EXISTS `swiper`;
CREATE TABLE `swiper` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `ProductName` varchar(50) DEFAULT NULL COMMENT '商品或者套餐名称',
  `ProductID` int(11) DEFAULT NULL COMMENT '商品或者套餐ID',
  `ImgUrl` varchar(255) NOT NULL COMMENT '轮播图片',
  `Index` int(2) NOT NULL DEFAULT '1' COMMENT '排序',
  `IsActive` int(2) NOT NULL DEFAULT '1' COMMENT '是否有效',
  `StationID` int(11) NOT NULL COMMENT '所属服务站',
  `CreateDate` datetime NOT NULL COMMENT '创建时间',
  `ProductType` int(2) NOT NULL DEFAULT '1' COMMENT '1商品 2 促销 3 套餐'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='轮播图';

-- ----------------------------
-- Records of swiper
-- ----------------------------

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
  `ImgUrl` varchar(50) NOT NULL COMMENT '头像',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='用户表';

-- ----------------------------
-- Records of userinfo
-- ----------------------------
INSERT INTO `userinfo` VALUES ('0d860f4e-8048-48a8-a582-a703f6c85ff7', '用户_558316', '13191885668', 'C3-33-67-70-15-11-B4-F6-02-0E-C6-1D-ED-35-20-59', '0', '1', '2019-07-10 11:07:46', '13191885668', '');
INSERT INTO `userinfo` VALUES ('1', 'niuhk', '15614385668', 'E1-0A-DC-39-49-BA-59-AB-BE-56-E0-57-F2-0F-88-3E', '1', '1', '2019-06-24 18:08:29', '15614385668', '\'\'');
