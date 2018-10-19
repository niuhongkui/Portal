/*
Navicat MySQL Data Transfer

Source Server         : .
Source Server Version : 50621
Source Host           : localhost:3306
Source Database       : portal

Target Server Type    : MYSQL
Target Server Version : 50621
File Encoding         : 65001

Date: 2018-10-19 23:27:49
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `action`
-- ----------------------------
DROP TABLE IF EXISTS `action`;
CREATE TABLE `action` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `PkId` varchar(45) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `IsDelete` bit(1) DEFAULT NULL,
  `CreateUser` varchar(45) DEFAULT NULL,
  `CarId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of action
-- ----------------------------

-- ----------------------------
-- Table structure for `atm`
-- ----------------------------
DROP TABLE IF EXISTS `atm`;
CREATE TABLE `atm` (
  `Id` varchar(45) NOT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `Province` varchar(45) DEFAULT NULL,
  `City` varchar(45) DEFAULT NULL,
  `County` varchar(45) DEFAULT NULL,
  `Address` varchar(45) DEFAULT NULL,
  `BankId` varchar(45) DEFAULT NULL,
  `CreateDate` varchar(45) DEFAULT NULL,
  `UpdateDate` varchar(45) DEFAULT NULL,
  `IsDelate` bit(1) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of atm
-- ----------------------------

-- ----------------------------
-- Table structure for `bank`
-- ----------------------------
DROP TABLE IF EXISTS `bank`;
CREATE TABLE `bank` (
  `Id` varchar(45) NOT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `Code` varchar(45) NOT NULL DEFAULT '',
  `UpdateDate` datetime DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `IsDelete` bit(1) DEFAULT NULL,
  `Admin` varchar(45) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of bank
-- ----------------------------
INSERT INTO `bank` VALUES ('1', ' 平安银行', '12345', '2018-10-17 22:23:16', null, '', '2');
INSERT INTO `bank` VALUES ('29e51454-f683-40fc-8273-56cf99999583', '兴业银行', 'B201810172115253824', '2018-10-17 22:22:47', null, '', '42f40545-0f05-4c5b-ad2f-61965bc60f21');

-- ----------------------------
-- Table structure for `car`
-- ----------------------------
DROP TABLE IF EXISTS `car`;
CREATE TABLE `car` (
  `Id` varchar(45) NOT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `Code` varchar(45) DEFAULT NULL,
  `BankId` varchar(45) DEFAULT NULL,
  `BankName` varchar(45) DEFAULT NULL,
  `Admin` varchar(45) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `IsDelete` bit(1) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of car
-- ----------------------------
INSERT INTO `car` VALUES ('1', 'aseer', '5555', '29e51454-f683-40fc-8273-56cf99999583', '兴业银行', '3', '2018-10-18 00:00:00', '2018-10-18 00:00:00', '');

-- ----------------------------
-- Table structure for `operator`
-- ----------------------------
DROP TABLE IF EXISTS `operator`;
CREATE TABLE `operator` (
  `Id` varchar(50) NOT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `UserName` varchar(45) DEFAULT NULL,
  `PassWord` varchar(100) DEFAULT NULL,
  `UserType` varchar(45) DEFAULT NULL,
  `Phone` varchar(45) DEFAULT NULL,
  `IsDelete` bit(1) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `PKId` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of operator
-- ----------------------------
INSERT INTO `operator` VALUES ('1', '测试', 'admin', 'E1-0A-DC-39-49-BA-59-AB-BE-56-E0-57-F2-0F-88-3E', '1', '1', '', '2018-01-01 00:00:00', '2018-01-01 00:00:00', '0');
INSERT INTO `operator` VALUES ('2', '平安银行', 'payh', 'E1-0A-DC-39-49-BA-59-AB-BE-56-E0-57-F2-0F-88-3E', '1', '2', '', null, '2018-10-17 22:23:16', '1');
INSERT INTO `operator` VALUES ('3', 'car', 'car', 'E1-0A-DC-39-49-BA-59-AB-BE-56-E0-57-F2-0F-88-3E', '2', '111', '', '2018-10-17 22:22:47', '2018-10-17 22:22:47', '1');
INSERT INTO `operator` VALUES ('42f40545-0f05-4c5b-ad2f-61965bc60f21', '妞妞', 'niuhk', 'E1-0A-DC-39-49-BA-59-AB-BE-56-E0-57-F2-0F-88-3E', '1', '15688888888', '', null, '2018-10-17 22:22:47', '29e51454-f683-40fc-8273-56cf99999583');

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
