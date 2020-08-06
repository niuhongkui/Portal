/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50624
Source Host           : localhost:3306
Source Database       : portal

Target Server Type    : MYSQL
Target Server Version : 50624
File Encoding         : 65001

Date: 2020-06-25 23:56:46
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `address`
-- ----------------------------
DROP TABLE IF EXISTS `address`;
CREATE TABLE `address` (
  `ID` varchar(50) NOT NULL DEFAULT '',
  `UserID` varchar(50) NOT NULL,
  `Province` varchar(50) NOT NULL,
  `City` varchar(50) NOT NULL,
  `County` varchar(50) NOT NULL,
  `Mobile` varchar(11) NOT NULL,
  `Addressee` varchar(50) NOT NULL,
  `DetailAddr` varchar(300) NOT NULL,
  `IsDefault` int(2) NOT NULL DEFAULT '0',
  `CreateDate` datetime NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `u_adde_pk` (`UserID`),
  CONSTRAINT `u_adde_pk` FOREIGN KEY (`UserID`) REFERENCES `userinfo` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of address
-- ----------------------------
INSERT INTO `address` VALUES ('1', '93e59c14-4e11-49ea-aa0f-42224c45542d', '河北', '石家庄', '桥西', '15613888888', '牛先生', '华浦原2-2-1', '1', '2020-06-25 14:11:04');

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
  KEY `f_u_pk` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='�ղ�';

-- ----------------------------
-- Records of favorite
-- ----------------------------

-- ----------------------------
-- Table structure for `instore`
-- ----------------------------
DROP TABLE IF EXISTS `instore`;
CREATE TABLE `instore` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `ProductID` varchar(50) NOT NULL COMMENT '商品ID',
  `ProductName` varchar(50) NOT NULL COMMENT '商品名称',
  `UnitID` varchar(50) NOT NULL COMMENT '商品类型',
  `UnitName` varchar(50) NOT NULL COMMENT '商品类型',
  `Amount` decimal(10,2) NOT NULL COMMENT '数量',
  `StaffID` varchar(50) NOT NULL COMMENT '操作人',
  `StaffName` varchar(50) NOT NULL COMMENT '操作人',
  `CreateDate` datetime NOT NULL COMMENT '入库时间',
  `Remark` varchar(200) DEFAULT NULL COMMENT '注释',
  `TypeName` varchar(50) NOT NULL DEFAULT '正常入库' COMMENT '入库类型',
  PRIMARY KEY (`ID`),
  KEY `ins_pro_pk` (`ProductID`),
  KEY `ins_uni_pk` (`UnitID`),
  KEY `ins_sta_pk` (`StaffID`),
  CONSTRAINT `ins_pro_pk` FOREIGN KEY (`ProductID`) REFERENCES `product` (`ID`),
  CONSTRAINT `ins_sta_pk` FOREIGN KEY (`StaffID`) REFERENCES `staff` (`ID`),
  CONSTRAINT `ins_uni_pk` FOREIGN KEY (`UnitID`) REFERENCES `productunit` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of instore
-- ----------------------------
INSERT INTO `instore` VALUES ('1', '563833d8-e469-4515-ab96-f631214429a6', '2', '500g', '500g', '1.00', '2', '22', '2020-06-12 16:14:47', '222', '正常入库');
INSERT INTO `instore` VALUES ('51db230d-1b73-487d-85b1-459e86f8cf95', '8611ed05-b483-46bf-9d52-b25862384dd4', '333', '500g', '500g', '200.00', '2', '华普元', '2020-06-15 09:18:27', null, '正常入库');
INSERT INTO `instore` VALUES ('dfe76129-445f-4298-b1ee-35686e27b91c', '8611ed05-b483-46bf-9d52-b25862384dd4', '馒头', '500g', '500g', '50.00', '2', '华普元', '2020-06-24 13:45:56', null, '正常入库');

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
  `ID` varchar(50) NOT NULL DEFAULT '',
  `PriceID` varchar(50) DEFAULT NULL,
  `ProductID` varchar(50) DEFAULT NULL,
  `ProductName` varchar(50) DEFAULT NULL,
  `Price` decimal(6,2) DEFAULT NULL,
  `MemberPrice` decimal(6,2) DEFAULT NULL,
  `LimitNum` decimal(4,0) DEFAULT NULL,
  `CreatDate` datetime DEFAULT NULL,
  `StaffName` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of oldprice
-- ----------------------------
INSERT INTO `oldprice` VALUES ('496306c5-45f3-4f4e-bacf-b6ac4ecef1da', '1', '563833d8-e469-4515-ab96-f631214429a6', '香蕉', '0.00', '0.00', '0', '2020-06-11 14:09:28', '东风小区');
INSERT INTO `oldprice` VALUES ('6e4d6798-1717-437f-828d-25ea0a51ce4e', 'a533ee7e-14ca-4a72-8b05-83ef2f23be1d', '563833d8-e469-4515-ab96-f631214429a6', '', '4.00', null, '10', '2020-06-11 17:17:20', '东风小区');
INSERT INTO `oldprice` VALUES ('96e99d5c-9cac-4127-9520-978fb4620167', 'a533ee7e-14ca-4a72-8b05-83ef2f23be1d', '563833d8-e469-4515-ab96-f631214429a6', '', '7.00', null, '10', '2020-06-11 17:18:25', '东风小区');

-- ----------------------------
-- Table structure for `order`
-- ----------------------------
DROP TABLE IF EXISTS `order`;
CREATE TABLE `order` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `OrderNo` varchar(50) NOT NULL COMMENT '单号',
  `State` varchar(50) NOT NULL COMMENT '1待支�?2已支�?3已取�?',
  `UserID` varchar(50) NOT NULL COMMENT '下单人',
  `UserName` varchar(50) NOT NULL COMMENT '下单人名称',
  `PMoney` decimal(8,2) NOT NULL COMMENT '支付金额',
  `Money` decimal(8,2) NOT NULL COMMENT '订单金额',
  `Amount` decimal(8,2) NOT NULL COMMENT '订单数量',
  `CreateDate` datetime NOT NULL COMMENT '创建时间',
  `Remark` varchar(200) NOT NULL DEFAULT '' COMMENT '备注',
  `Address` varchar(200) NOT NULL DEFAULT '' COMMENT '收货地址',
  `SendTime` varchar(50) NOT NULL COMMENT '预计送达时间',
  `Phone` varchar(50) NOT NULL DEFAULT '' COMMENT '手机号',
  `SendType` varchar(50) NOT NULL DEFAULT '' COMMENT '送货or自取',
  `SendMoney` decimal(10,0) NOT NULL DEFAULT '0' COMMENT '送货费',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `order_index` (`OrderNo`) USING HASH,
  KEY `o_u_pk` (`UserID`),
  CONSTRAINT `o_u_pk` FOREIGN KEY (`UserID`) REFERENCES `userinfo` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='订单';

-- ----------------------------
-- Records of order
-- ----------------------------
INSERT INTO `order` VALUES ('13faae75-60a2-421d-b8b6-50a7d894dbb1', 'O202006251148466232', '待付款', '93e59c14-4e11-49ea-aa0f-42224c45542d', '用户_452588', '5.00', '11.00', '1.00', '2020-06-25 23:48:47', '', '河北石家庄桥西华浦原2-2-1牛先生', '今天-10点', '15613888888', '送货', '5');
INSERT INTO `order` VALUES ('bef0b9bb-be10-4944-be30-f0277ac449fa', 'O202006251028232574', '待付款', '93e59c14-4e11-49ea-aa0f-42224c45542d', '用户_452588', '5.00', '18.00', '2.00', '2020-06-25 22:28:24', '', '河北石家庄桥西华浦原2-2-1牛先生', '1999-02-02 00:00:00', '15613888888', '送货', '5');
INSERT INTO `order` VALUES ('c8434d19-6533-4b80-a561-59a84182a92b', 'O202006250401005014', '待付款', '93e59c14-4e11-49ea-aa0f-42224c45542d', '用户_452588', '0.00', '20.00', '3.00', '2020-06-25 16:01:00', '', '河北石家庄桥西华浦原2-2-1牛先生', '1999-02-02 00:00:00', '15613888888', '送货', '5');
INSERT INTO `order` VALUES ('df20661d-9a90-44b9-8262-b2aec95b3da5', 'O202006250420538158', '待付款', '93e59c14-4e11-49ea-aa0f-42224c45542d', '用户_452588', '5.00', '25.00', '3.00', '2020-06-25 16:20:53', '', '河北石家庄桥西华浦原2-2-1牛先生', '明天-10点', '15613888888', '送货', '5');

-- ----------------------------
-- Table structure for `orderdetail`
-- ----------------------------
DROP TABLE IF EXISTS `orderdetail`;
CREATE TABLE `orderdetail` (
  `ID` varchar(50) NOT NULL,
  `OrderNo` varchar(50) NOT NULL COMMENT '订单号',
  `ProductID` varchar(50) NOT NULL COMMENT '商品ID',
  `ProductName` varchar(50) NOT NULL COMMENT '商品名称',
  `UnitID` varchar(50) NOT NULL COMMENT '单位',
  `UnitName` varchar(50) NOT NULL COMMENT '单位',
  `Amount` decimal(8,2) NOT NULL COMMENT '数量',
  `Money` decimal(8,2) NOT NULL COMMENT '价格',
  `PMoney` decimal(8,2) NOT NULL COMMENT '优惠金额',
  `Price` decimal(8,2) NOT NULL COMMENT '单价',
  `PPrice` decimal(8,2) NOT NULL COMMENT '优惠价了',
  PRIMARY KEY (`ID`),
  KEY `o_d_pk` (`OrderNo`),
  CONSTRAINT `o_d_pk` FOREIGN KEY (`OrderNo`) REFERENCES `order` (`OrderNo`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of orderdetail
-- ----------------------------
INSERT INTO `orderdetail` VALUES ('0d6dbf87-f3e6-4424-90e2-a70c99e027a4', 'O202006250420538158', '8611ed05-b483-46bf-9d52-b25862384dd4', '馒头', '500g', '500g', '1.00', '6.00', '0.00', '6.00', '0.00');
INSERT INTO `orderdetail` VALUES ('38b1c141-e84e-47dd-bb96-a13458f7949e', 'O202006250420538158', '563833d8-e469-4515-ab96-f631214429a6', '香蕉', '500g', '500g', '2.00', '14.00', '0.00', '7.00', '0.00');
INSERT INTO `orderdetail` VALUES ('4186d7a2-cf20-4b3c-96d9-4e819e4b0da2', 'O202006250401005014', '563833d8-e469-4515-ab96-f631214429a6', '香蕉', '500g', '500g', '2.00', '14.00', '0.00', '7.00', '0.00');
INSERT INTO `orderdetail` VALUES ('49c43303-11e7-4457-944a-7370a6963faf', 'O202006250401005014', '8611ed05-b483-46bf-9d52-b25862384dd4', '馒头', '500g', '500g', '1.00', '6.00', '0.00', '6.00', '0.00');
INSERT INTO `orderdetail` VALUES ('4d8b327e-c1bd-4039-9894-6bb792ffda45', 'O202006251028232574', '8611ed05-b483-46bf-9d52-b25862384dd4', '馒头', '500g', '500g', '1.00', '6.00', '0.00', '6.00', '0.00');
INSERT INTO `orderdetail` VALUES ('b9e335aa-57c8-466c-8fd6-196bb816a97a', 'O202006251028232574', '563833d8-e469-4515-ab96-f631214429a6', '香蕉', '500g', '500g', '1.00', '7.00', '0.00', '7.00', '0.00');
INSERT INTO `orderdetail` VALUES ('c25e281b-d097-400c-92dd-3dbb36fd3e3e', 'O202006251148466232', '8611ed05-b483-46bf-9d52-b25862384dd4', '馒头', '500g', '500g', '1.00', '6.00', '0.00', '6.00', '0.00');

-- ----------------------------
-- Table structure for `packing`
-- ----------------------------
DROP TABLE IF EXISTS `packing`;
CREATE TABLE `packing` (
  `ID` varchar(50) NOT NULL,
  `OrderNo` varchar(50) NOT NULL COMMENT '订单号',
  `OrderID` varchar(50) NOT NULL COMMENT '订单ID',
  `State` varchar(50) NOT NULL COMMENT '拣货单状态',
  `UserName` varchar(50) NOT NULL COMMENT '用户名称',
  `UserPhone` varchar(50) NOT NULL COMMENT '用户手机号',
  `OrderRemark` varchar(200) NOT NULL COMMENT '用户订单备注',
  `Address` varchar(200) NOT NULL COMMENT '收货地址',
  `CreateDate` datetime NOT NULL COMMENT '支付时间',
  `SendTime` datetime NOT NULL COMMENT '要求送货时间',
  `UpdateDate` datetime NOT NULL COMMENT '更新时间',
  `StaffID` varchar(50) NOT NULL DEFAULT '''''' COMMENT '指定派送人',
  `StaffName` varchar(50) NOT NULL DEFAULT '''''' COMMENT '指定派送人',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of packing
-- ----------------------------

-- ----------------------------
-- Table structure for `packingdetail`
-- ----------------------------
DROP TABLE IF EXISTS `packingdetail`;
CREATE TABLE `packingdetail` (
  `ID` varchar(50) NOT NULL,
  `PID` varchar(50) DEFAULT NULL,
  `OrderDetailID` varchar(50) DEFAULT NULL,
  `ProductID` varchar(50) DEFAULT NULL,
  `ProductName` varchar(50) DEFAULT NULL,
  `UnitID` varchar(50) DEFAULT NULL,
  `UnitName` varchar(50) DEFAULT NULL,
  `Amount` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of packingdetail
-- ----------------------------

-- ----------------------------
-- Table structure for `product`
-- ----------------------------
DROP TABLE IF EXISTS `product`;
CREATE TABLE `product` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `Code` varchar(50) NOT NULL COMMENT '编码',
  `Name` varchar(50) NOT NULL COMMENT '名称',
  `TypeID` varchar(50) NOT NULL COMMENT '类别编码',
  `TypeName` varchar(50) NOT NULL COMMENT '类别名称',
  `CreateDate` datetime NOT NULL COMMENT '创建时间',
  `IsActive` int(2) NOT NULL COMMENT '是否有效',
  `StaffID` varchar(50) NOT NULL COMMENT '操作人',
  `StaffName` varchar(50) NOT NULL COMMENT '操作人',
  `Detail` varchar(500) NOT NULL DEFAULT '' COMMENT '说明',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='商品';

-- ----------------------------
-- Records of product
-- ----------------------------
INSERT INTO `product` VALUES ('563833d8-e469-4515-ab96-f631214429a6', 'P202004300955148701', '香蕉', '9e4001f8-7bd0-45fa-acc4-ef00d2889a6f', '水果', '2020-06-25 09:15:09', '1', '2', '华普元', '5555');
INSERT INTO `product` VALUES ('8611ed05-b483-46bf-9d52-b25862384dd4', 'P202004300953376402', '馒头', 'fe36d786-df60-4d09-87f0-50c69e5b0243', '主食', '2020-06-25 09:06:30', '1', '2', '华普元', '333');

-- ----------------------------
-- Table structure for `productimg`
-- ----------------------------
DROP TABLE IF EXISTS `productimg`;
CREATE TABLE `productimg` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `ProductID` varchar(50) NOT NULL,
  `Url` varchar(200) NOT NULL DEFAULT '' COMMENT '图片路径',
  `RowNO` int(11) DEFAULT NULL COMMENT '排序',
  PRIMARY KEY (`ID`),
  KEY `img_p_pk` (`ProductID`),
  CONSTRAINT `img_p_pk` FOREIGN KEY (`ProductID`) REFERENCES `product` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of productimg
-- ----------------------------
INSERT INTO `productimg` VALUES ('00d4693e-213f-42f2-b49a-7fc50dacfcea', '563833d8-e469-4515-ab96-f631214429a6', '/upload/20200625/ac0e6221-05f2-4752-af6e-1d511be92621.JPG', '0');
INSERT INTO `productimg` VALUES ('13b4d4e9-34c2-4291-8ecc-c45ae0188573', '8611ed05-b483-46bf-9d52-b25862384dd4', '/upload/20200625/50ab0c99-4fea-4d19-ba38-5e43f6bef3fe.JPG', '0');

-- ----------------------------
-- Table structure for `productprice`
-- ----------------------------
DROP TABLE IF EXISTS `productprice`;
CREATE TABLE `productprice` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `ProductID` varchar(50) NOT NULL COMMENT '商品ID',
  `Price` decimal(10,2) NOT NULL DEFAULT '0.00' COMMENT '价格',
  `MemberPrice` decimal(10,2) NOT NULL DEFAULT '0.00' COMMENT '会员价',
  `LimitNum` decimal(10,2) NOT NULL DEFAULT '0.00' COMMENT '会员限购数量',
  `UnitID` varchar(50) NOT NULL COMMENT '商品单位Code',
  `UnitName` varchar(50) NOT NULL COMMENT '商品单位Name',
  `StaffID` varchar(50) NOT NULL COMMENT '创建人编码',
  `StaffName` varchar(50) NOT NULL COMMENT '创建人',
  `CreateDate` datetime NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `only_pk` (`ProductID`,`UnitID`) USING BTREE,
  KEY `p_price_pk` (`ProductID`),
  CONSTRAINT `p_price_pk` FOREIGN KEY (`ProductID`) REFERENCES `product` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='商品价格';

-- ----------------------------
-- Records of productprice
-- ----------------------------
INSERT INTO `productprice` VALUES ('97cbcb6b-f4d7-4851-ba80-1b9a8c1ed878', '8611ed05-b483-46bf-9d52-b25862384dd4', '6.00', '4.00', '5.00', '500g', '500g', '2', '华普元', '2020-06-24 13:42:50');
INSERT INTO `productprice` VALUES ('a533ee7e-14ca-4a72-8b05-83ef2f23be1d', '563833d8-e469-4515-ab96-f631214429a6', '7.00', '2.00', '100.00', '500g', '500g', '2', '东风小区', '2020-06-11 17:18:25');

-- ----------------------------
-- Table structure for `producttype`
-- ----------------------------
DROP TABLE IF EXISTS `producttype`;
CREATE TABLE `producttype` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `Name` varchar(50) NOT NULL COMMENT '类别名称',
  `IsActive` int(2) NOT NULL COMMENT '是否有效',
  `CreateDate` datetime NOT NULL COMMENT '创建时间',
  `StaffID` varchar(50) NOT NULL COMMENT '创建人名称',
  `StaffName` varchar(50) NOT NULL COMMENT '创建人名称',
  `ImgUrl` varchar(200) NOT NULL COMMENT '类型图片',
  PRIMARY KEY (`ID`),
  KEY `Staff_p_pk` (`StaffID`),
  CONSTRAINT `Staff_p_pk` FOREIGN KEY (`StaffID`) REFERENCES `staff` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='商品类别';

-- ----------------------------
-- Records of producttype
-- ----------------------------
INSERT INTO `producttype` VALUES ('0dc1e51d-f168-4d4d-8d9b-e2a5a5c32dd7', '酒茶', '1', '2020-06-24 09:03:57', '2', '华普元', '/upload/20200624/b8a459f9-d420-46be-9cd6-bf2ba8b8f6c6.JPG');
INSERT INTO `producttype` VALUES ('15224eb1-fad2-4918-8f47-d89a066a6818', '日化', '1', '2020-06-24 09:04:26', '2', '华普元', '/upload/20200624/d8303f2d-1fbc-4693-a5d2-d0334100113f.JPG');
INSERT INTO `producttype` VALUES ('1c3818d2-71c1-43a6-afcb-16cf4e6926c9', '冷鲜', '1', '2020-06-24 09:02:20', '2', '华普元', '/upload/20200624/ed7eb425-a661-44d3-be00-a787c9703d9e.JPG');
INSERT INTO `producttype` VALUES ('28b522f5-7de5-461d-9cd7-a674f65002d0', '饮品', '1', '2020-06-24 09:03:25', '2', '华普元', '/upload/20200624/bee0fee8-dae0-4bb7-a13b-9cc096e30836.JPG');
INSERT INTO `producttype` VALUES ('28fd9b29-0ff6-4141-8fbe-121046472a46', '零食', '1', '2020-06-24 09:03:40', '2', '华普元', '/upload/20200624/b9ce8ec8-f4b4-4f0e-ad58-386c73f33c86.JPG');
INSERT INTO `producttype` VALUES ('2dccbe29-e46c-4d35-a2d3-60c238a49d3b', '米面', '1', '2020-06-24 09:02:34', '2', '华普元', '/upload/20200624/5f035600-4f44-464e-8e89-2e0ce83bc6a7.JPG');
INSERT INTO `producttype` VALUES ('8b6295bc-cbff-4a34-8d43-3d7f4fd3292f', '肉蛋奶', '1', '2020-06-24 09:01:49', '2', '华普元', '/upload/20200624/7b0e6425-88f3-4973-ab10-da5bdc30ceee.JPG');
INSERT INTO `producttype` VALUES ('8ce71f51-2801-4a0e-9b38-f72949f367b4', '文体', '1', '2020-06-24 09:04:12', '2', '华普元', '/upload/20200624/9e742260-a3d9-485c-83f6-643b7be14174.JPG');
INSERT INTO `producttype` VALUES ('9a55944e-9072-407e-b847-ec11f0d03f9a', '时蔬', '1', '2020-06-24 08:58:50', '2', '华普元', '/upload/20200527/4d2214ca-3f73-4ff2-8961-91acf41c18cb.JPG');
INSERT INTO `producttype` VALUES ('9e4001f8-7bd0-45fa-acc4-ef00d2889a6f', '瓜果', '1', '2020-06-24 08:59:04', '2', '华普元', '/upload/20200527/12013f2f-d4c1-4848-9e5e-353c0783ae46.JPG');
INSERT INTO `producttype` VALUES ('a01aaf5b-7e15-41ef-bbe7-b285868ced38', '油', '1', '2020-06-24 09:02:50', '2', '华普元', '/upload/20200624/f10a42a3-bce6-46d1-8571-1edb0b6836b5.JPG');
INSERT INTO `producttype` VALUES ('c171b8a5-afb6-4dc5-bc0d-083092a52540', '调料', '1', '2020-06-24 09:03:05', '2', '华普元', '/upload/20200624/6177dc49-413d-4c00-a19b-6c5f0e703e8d.JPG');
INSERT INTO `producttype` VALUES ('fe36d786-df60-4d09-87f0-50c69e5b0243', '主食', '1', '2020-06-24 09:01:30', '2', '华普元', '/upload/20200624/cc3f68b8-a185-4019-bf39-633373ce1071.JPG');

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
INSERT INTO `productunit` VALUES ('500g', '500g', '500g', '1', '2019-07-02 15:42:55', '1', '222');
INSERT INTO `productunit` VALUES ('个', '个', '个', '1', '2020-06-12 15:27:20', '1', '1');
INSERT INTO `productunit` VALUES ('盒', '盒', '盒', '1', '2020-06-12 15:27:42', '1', '1');
INSERT INTO `productunit` VALUES ('箱', '箱', '箱', '1', '2020-06-12 15:26:58', '1', '1');

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
INSERT INTO `staff` VALUES ('2', 'admin', '华普元', 'E1-0A-DC-39-49-BA-59-AB-BE-56-E0-57-F2-0F-88-3E', '1', '1', '13191885668', '0001-01-01 00:00:00', '东风小区', 'ST001');

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
-- Table structure for `store`
-- ----------------------------
DROP TABLE IF EXISTS `store`;
CREATE TABLE `store` (
  `ID` varchar(50) NOT NULL COMMENT '主键',
  `ProductID` varchar(50) NOT NULL COMMENT '商品ID',
  `ProductName` varchar(50) NOT NULL COMMENT '商品名称',
  `UnitID` varchar(50) NOT NULL COMMENT '单位id',
  `UnitName` varchar(50) NOT NULL COMMENT '单位名称',
  `Amount` decimal(10,2) NOT NULL COMMENT '库存数量',
  `OutAmount` decimal(10,2) NOT NULL COMMENT '出库数量',
  `LuckAmount` decimal(10,2) NOT NULL COMMENT '锁定数量',
  `CreateDate` datetime NOT NULL COMMENT '创建时间',
  `UpdateDate` datetime NOT NULL COMMENT '最后更新时间',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Only_store` (`ProductID`,`UnitID`) USING BTREE,
  KEY `store_unit_pk` (`UnitID`),
  CONSTRAINT `store_pro_PK` FOREIGN KEY (`ProductID`) REFERENCES `product` (`ID`),
  CONSTRAINT `store_unit_pk` FOREIGN KEY (`UnitID`) REFERENCES `productunit` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of store
-- ----------------------------
INSERT INTO `store` VALUES ('1', '563833d8-e469-4515-ab96-f631214429a6', '22', '500g', '500g', '21.00', '33.00', '12.00', '2020-06-12 15:28:57', '2020-06-25 22:28:24');
INSERT INTO `store` VALUES ('16e6b8d0-19e0-406d-8e52-dcc63e70b804', '8611ed05-b483-46bf-9d52-b25862384dd4', '333', '500g', '500g', '248.00', '0.00', '2.00', '2020-06-15 09:18:27', '2020-06-25 23:48:47');

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
INSERT INTO `swiper` VALUES ('1', '2', '3', '/upload/20191017/e2c00219-c481-42fe-91ac-f9b0309608ad.JPG', '1', '1', '2019-10-17 13:08:08');
INSERT INTO `swiper` VALUES ('1286f686-c88f-4254-a22e-ef64a783785e', '', '', '/upload/20191017/05b88e6f-33e9-4cae-87f2-e584514ae4f4.JPG', '2', '1', '2019-10-17 13:33:08');

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
INSERT INTO `userinfo` VALUES ('93e59c14-4e11-49ea-aa0f-42224c45542d', '用户_452588', '15614385666', 'E1-0A-DC-39-49-BA-59-AB-BE-56-E0-57-F2-0F-88-3E', '0', '1', '2019-07-29 11:02:09', '15614385666', '/images/15614385666/90486a99-56ef-4b0a-b985-01e9c943acc4.PNG', '2019-10-03 14:23:36');
