/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50714
Source Host           : localhost:3306
Source Database       : portal

Target Server Type    : MYSQL
Target Server Version : 50714
File Encoding         : 65001

Date: 2019-10-14 17:24:04
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `cart`
-- ----------------------------
DROP TABLE IF EXISTS `cart`;
CREATE TABLE `cart` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `UserID` varchar(50) NOT NULL COMMENT '用户ID',
  `ProductID` varchar(50) NOT NULL COMMENT '商品ID',
  `UnitID` varchar(50) NOT NULL COMMENT '商品单位ID',
  `Amount` int(10) NOT NULL DEFAULT '0' COMMENT '数量',
  `UserName` varchar(50) NOT NULL,
  `ProductName` varchar(50) NOT NULL,
  `UnitName` varchar(50) NOT NULL,
  `CreatDate` datetime NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `favorite_index` (`ID`) USING HASH,
  KEY `c_u_pk` (`UserID`),
  KEY `c_p_pk` (`ProductID`),
  KEY `c_p_u_pk` (`UnitID`),
  CONSTRAINT `c_p_pk` FOREIGN KEY (`ProductID`) REFERENCES `product` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `c_p_u_pk` FOREIGN KEY (`UnitID`) REFERENCES `productunit` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `c_u_pk` FOREIGN KEY (`UserID`) REFERENCES `userinfo` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='购物车';

-- ----------------------------
-- Records of cart
-- ----------------------------
INSERT INTO `cart` VALUES ('b1611a15-ec7e-470a-bc1c-5d835d99a5b5', '9466019e-d7a0-41e4-9301-93bf16faea8e', '5714397e-4b5a-4407-bfc0-5b223e7571c0', '1', '3', '用户_538351', '土豆', '斤', '2019-10-14 17:20:49');

-- ----------------------------
-- Table structure for `favorite`
-- ----------------------------
DROP TABLE IF EXISTS `favorite`;
CREATE TABLE `favorite` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `UserID` varchar(50) NOT NULL COMMENT '用户ID',
  `ProductID` varchar(50) NOT NULL COMMENT '商品ID',
  `UnitID` varchar(50) NOT NULL COMMENT '商品单位ID',
  `Amount` int(10) NOT NULL DEFAULT '0' COMMENT '数量',
  `UserName` varchar(50) NOT NULL,
  `ProductName` varchar(50) NOT NULL,
  `UnitName` varchar(50) NOT NULL,
  `CreatDate` datetime NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `favorite_index` (`ID`) USING HASH,
  KEY `f_p_pk` (`ProductID`),
  KEY `f_p_u_pk` (`UnitID`),
  KEY `f_u_pk` (`UserID`),
  CONSTRAINT `f_p_pk` FOREIGN KEY (`ProductID`) REFERENCES `product` (`ID`),
  CONSTRAINT `f_p_u_pk` FOREIGN KEY (`UnitID`) REFERENCES `productunit` (`ID`),
  CONSTRAINT `f_u_pk` FOREIGN KEY (`UserID`) REFERENCES `userinfo` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='�ղ�';

-- ----------------------------
-- Records of favorite
-- ----------------------------
INSERT INTO `favorite` VALUES ('1b7e6c13-172d-4f43-80f8-dcfff420ef9d', '9466019e-d7a0-41e4-9301-93bf16faea8e', '5714397e-4b5a-4407-bfc0-5b223e7571c0', '1', '1', '用户_538351', '土豆', '斤', '2019-10-14 16:57:52');

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
  `State` int(2) NOT NULL COMMENT '1待支付,2已支付,3已取货,',
  `UserID` varchar(50) NOT NULL COMMENT '下单人',
  `UserName` varchar(50) NOT NULL COMMENT '下单人名称',
  `PMoney` decimal(8,2) NOT NULL COMMENT '支付金额',
  `Money` decimal(8,2) NOT NULL COMMENT '订单金额',
  `Amount` decimal(8,2) NOT NULL COMMENT '订单数量',
  `CreateDate` datetime NOT NULL COMMENT '创建时间',
  `Remark` varchar(255) DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `order_index` (`OrderNo`) USING HASH,
  KEY `o_u_pk` (`UserID`),
  CONSTRAINT `o_u_pk` FOREIGN KEY (`UserID`) REFERENCES `userinfo` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='订单';

-- ----------------------------
-- Records of order
-- ----------------------------

-- ----------------------------
-- Table structure for `orderdetail`
-- ----------------------------
DROP TABLE IF EXISTS `orderdetail`;
CREATE TABLE `orderdetail` (
  `ID` varchar(50) NOT NULL,
  `OrderNo` varchar(50) NOT NULL,
  `ProductID` varchar(50) NOT NULL,
  `ProductName` varchar(50) NOT NULL,
  `UnitID` varchar(50) NOT NULL,
  `UnitName` varchar(50) NOT NULL,
  `Amount` decimal(8,2) NOT NULL,
  `Money` decimal(8,2) NOT NULL,
  `PMoney` decimal(8,2) NOT NULL,
  `Price` decimal(8,2) NOT NULL,
  `PPrice` decimal(8,2) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `o_d_pk` (`OrderNo`),
  CONSTRAINT `o_d_pk` FOREIGN KEY (`OrderNo`) REFERENCES `order` (`OrderNo`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of orderdetail
-- ----------------------------

-- ----------------------------
-- Table structure for `price`
-- ----------------------------
DROP TABLE IF EXISTS `price`;
CREATE TABLE `price` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `ProductID` varchar(50) NOT NULL COMMENT '商品ID',
  `Price` decimal(10,2) NOT NULL COMMENT '价格',
  `MemberPrice` decimal(10,2) NOT NULL DEFAULT '0.00' COMMENT '会员价',
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
INSERT INTO `price` VALUES ('403da68b-f424-4646-b21b-9fe8deb504ed', '5714397e-4b5a-4407-bfc0-5b223e7571c0', '1.50', '1.00', '500g', '斤', 'admin', '东风小区', '0001-01-01 00:00:00');
INSERT INTO `price` VALUES ('5227fab2-155a-4b4e-80af-ac6a6a07d200', '9fc3f390-a005-4a35-ac95-51d957c11e3e', '6.00', '0.00', 'kg', '千克', 'admin', '东风小区', '2019-08-15 13:56:50');
INSERT INTO `price` VALUES ('60972cf7-04b2-454d-bfe8-79e0038aa755', '9fc3f390-a005-4a35-ac95-51d957c11e3e', '3.00', '0.00', '500g', '斤', 'admin', '东风小区', '2019-08-15 13:56:50');
INSERT INTO `price` VALUES ('99038a4d-35bf-4d92-bb63-207b0e4f35ee', '5714397e-4b5a-4407-bfc0-5b223e7571c0', '3.00', '0.00', 'kg', '千克', 'admin', '东风小区', '0001-01-01 00:00:00');

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
  `CreateDate` datetime NOT NULL COMMENT '创建时间',
  `IsActive` int(2) NOT NULL COMMENT '是否有效',
  `ImgUrl` varchar(255) NOT NULL COMMENT '图片',
  `ImgUrl2` varchar(255) NOT NULL COMMENT '图片',
  `ImgUrl3` varchar(255) NOT NULL COMMENT '图片',
  `Detail` varchar(1000) DEFAULT '销售数量' COMMENT '显示明细',
  `Sales` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='商品';

-- ----------------------------
-- Records of product
-- ----------------------------
INSERT INTO `product` VALUES ('2', 'P1245689452', '西红柿', 'T201909280408181429', '777', 'kg', '千克', '2019-10-02 21:37:47', '1', '/images/nopic.png', '', '', '销售数量', '0');
INSERT INTO `product` VALUES ('5714397e-4b5a-4407-bfc0-5b223e7571c0', 'P201907120451229056', '土豆', 'T201909280408181429', '777', 'kg', '千克', '2019-10-10 14:44:37', '1', '/upload/20191010/2f71d094-947f-4ba2-aae3-3fb41c34c996.JPG', '/upload/20191010/879c0a47-6bb2-4d76-bade-15f279edcbd6.JPG', '/upload/20191010/9bcac5f9-a762-4c34-bb43-9cdc944de897.JPG', '销售数量', '22');
INSERT INTO `product` VALUES ('784561ef-511a-4d47-86d5-4898f10bed99', 'P201910020104403729', '55', 'T201909280408181429', '777', '500g', '斤', '2019-10-02 13:04:41', '1', '/upload/20191002/84f24739-6499-4a49-8d85-56d91c45e23d.JPG', '', '', '销售数量', '0');
INSERT INTO `product` VALUES ('9fc3f390-a005-4a35-ac95-51d957c11e3e', 'P201907041037034385', '黄瓜', 'T201907020152143118', '蔬菜', '500g', '斤', '2019-07-12 17:01:41', '1', '/images/nopic.png', '', '', '销售数量', '0');

-- ----------------------------
-- Table structure for `producttype`
-- ----------------------------
DROP TABLE IF EXISTS `producttype`;
CREATE TABLE `producttype` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `Code` varchar(50) NOT NULL COMMENT '编码',
  `Name` varchar(50) NOT NULL COMMENT '类别名称',
  `IsActive` int(2) NOT NULL COMMENT '是否有效',
  `CreateDate` datetime NOT NULL COMMENT '创建时间',
  `StaffID` varchar(50) NOT NULL COMMENT '创建人名称',
  `StaffName` varchar(50) NOT NULL COMMENT '创建人名称',
  `ImgUrl` varchar(200) NOT NULL COMMENT '类型图片',
  `TopCategoryID` varchar(50) NOT NULL COMMENT '一级大类型',
  `TopCategoryName` varchar(50) NOT NULL COMMENT '一级大类型名称',
  PRIMARY KEY (`ID`),
  KEY `Staff_p_pk` (`StaffID`),
  CONSTRAINT `Staff_p_pk` FOREIGN KEY (`StaffID`) REFERENCES `staff` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='商品类别';

-- ----------------------------
-- Records of producttype
-- ----------------------------
INSERT INTO `producttype` VALUES ('0d3907a5-fd6f-46b6-8aa5-dab5c2fb19db', 'T201909280408181429', '粉芡', '1', '2019-10-10 14:44:24', '2', '东风小区', '/upload/20191010/41807b08-c60e-49b1-9667-158f754d84da.JPG', '3', '调料');

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
  `ProductName` varchar(50) NOT NULL COMMENT '商品或者套餐名称',
  `ProductID` varchar(50) NOT NULL COMMENT '商品或者套餐ID',
  `ImgUrl` varchar(255) NOT NULL COMMENT '轮播图片',
  `Index` int(2) NOT NULL DEFAULT '1' COMMENT '排序',
  `IsActive` int(2) NOT NULL DEFAULT '1' COMMENT '是否有效',
  `CreateDate` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`ID`)
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
  `ImgUrl` varchar(200) NOT NULL COMMENT '头像',
  `MemberDate` datetime DEFAULT NULL COMMENT '会员到期时间',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='用户表';

-- ----------------------------
-- Records of userinfo
-- ----------------------------
INSERT INTO `userinfo` VALUES ('0d860f4e-8048-48a8-a582-a703f6c85ff7', '用户_558316', '13191885668', 'C3-33-67-70-15-11-B4-F6-02-0E-C6-1D-ED-35-20-59', '1', '1', '0001-01-01 00:00:00', '13191885668', '', '2019-10-03 14:23:39');
INSERT INTO `userinfo` VALUES ('93e59c14-4e11-49ea-aa0f-42224c45542d', '用户_452588', '15614385666', 'E1-0A-DC-39-49-BA-59-AB-BE-56-E0-57-F2-0F-88-3E', '0', '1', '2019-07-29 11:02:09', '15614385666', '', '2019-10-03 14:23:36');
INSERT INTO `userinfo` VALUES ('9466019e-d7a0-41e4-9301-93bf16faea8e', '用户_538351', '15614385667', 'E1-0A-DC-39-49-BA-59-AB-BE-56-E0-57-F2-0F-88-3E', '0', '1', '2019-07-29 11:00:23', '15614385667', '', '2019-10-03 14:23:33');
INSERT INTO `userinfo` VALUES ('97f6c8c6-2516-4523-aed7-292850bd8404', '用户_251672', '15614385662', 'C3-33-67-70-15-11-B4-F6-02-0E-C6-1D-ED-35-20-59', '0', '1', '2019-07-12 16:05:36', '15614385662', '', '2019-10-03 14:23:28');
