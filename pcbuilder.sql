-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 20, 2021 at 11:05 PM
-- Server version: 10.4.21-MariaDB
-- PHP Version: 8.0.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `pcbuilder`
--

-- --------------------------------------------------------

--
-- Table structure for table `advertphotos`
--

CREATE TABLE `advertphotos` (
  `Id` int(11) NOT NULL,
  `Path` text DEFAULT NULL,
  `AdvertId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `advertphotos`
--

INSERT INTO `advertphotos` (`Id`, `Path`, `AdvertId`) VALUES
(10, 'https://localhost:5001/adverts/4/1.jpg', 4),
(11, 'https://localhost:5001/adverts/4/2.jpg', 4),
(12, 'https://localhost:5001/adverts/4/3.jpg', 4),
(22, 'https://localhost:5001/adverts/5/1.jpg', 5),
(23, 'https://localhost:5001/adverts/5/2.jpg', 5),
(24, 'https://localhost:5001/adverts/5/3.jpg', 5);

-- --------------------------------------------------------

--
-- Table structure for table `adverts`
--

CREATE TABLE `adverts` (
  `Id` int(11) NOT NULL,
  `Price` double NOT NULL,
  `Description` text DEFAULT NULL,
  `Used` bit(1) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `LastEditedOn` datetime NOT NULL,
  `Confirmed` bit(1) NOT NULL,
  `ProductId` int(11) DEFAULT NULL,
  `SellerId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `adverts`
--

INSERT INTO `adverts` (`Id`, `Price`, `Description`, `Used`, `CreatedOn`, `LastEditedOn`, `Confirmed`, `ProductId`, `SellerId`) VALUES
(1, 20, 'atveztas is anglijos 2019-08', b'1', '2020-11-10 22:38:20', '2020-11-10 22:38:20', b'1', 1, 1),
(4, 220, 'apynaujis, tuscias', b'1', '2020-11-11 00:24:50', '2020-11-11 02:22:40', b'1', 1, 2),
(5, 150, 'nenaudota', b'0', '2020-11-11 00:28:30', '2020-11-11 00:28:30', b'1', 12, 1),
(10, 320, 'Mazai naudotas, nusipirkau kita', b'1', '2021-01-06 04:23:27', '2021-01-06 04:23:27', b'1', 2, 4),
(11, 70, 'Skytech garantija 3m.', b'0', '2021-01-06 04:24:49', '2021-01-06 04:24:49', b'1', 26, 4),
(12, 170, 'Nauja, garantija iki 2022 m.', b'0', '2021-01-06 04:25:32', '2021-01-06 04:25:32', b'1', 7, 4),
(13, 70, 'Su visa komplektacija', b'1', '2021-01-06 04:26:43', '2021-01-06 04:26:43', b'1', 32, 4),
(14, 90, 'Neseniai nupirktas', b'0', '2021-01-06 04:27:10', '2021-01-06 04:27:10', b'1', 33, 4),
(15, 160, 'Naudotas mano sistemoje', b'1', '2021-01-06 04:41:15', '2021-01-06 04:41:15', b'1', 4, 2),
(16, 60, 'Naudotas 1 metus', b'1', '2021-01-06 04:42:09', '2021-01-06 04:42:09', b'1', 6, 2),
(17, 80, 'Nera keliu sata laidu', b'1', '2021-01-06 04:42:28', '2021-01-06 04:42:28', b'1', 23, 2),
(18, 60, 'Yra garantija 6 men', b'1', '2021-01-06 04:42:48', '2021-01-06 04:42:48', b'1', 8, 2),
(19, 120, 'Naujas, neatidarytas', b'0', '2021-01-06 04:43:44', '2021-01-06 04:43:44', b'1', 9, 2),
(20, 90, 'naujos pc dalys pigiau', b'0', '2021-01-06 04:45:54', '2021-01-06 04:45:54', b'1', 25, 5),
(21, 150, 'naujos pc dalys pigiau', b'0', '2021-01-06 04:46:06', '2021-01-06 04:46:06', b'1', 27, 5),
(22, 150, 'naujos pc dalys pigiau', b'0', '2021-01-06 04:46:28', '2021-01-06 04:46:28', b'1', 22, 5),
(23, 80, 'naujos pc dalys pigiau', b'0', '2021-01-06 04:46:40', '2021-01-06 04:46:40', b'1', 29, 5),
(24, 80, 'naujos pc dalys pigiau', b'0', '2021-01-06 04:46:56', '2021-01-06 04:46:56', b'1', 31, 5),
(25, 90, 'naujos pc dalys pigiau', b'0', '2021-01-06 04:47:12', '2021-01-06 04:47:12', b'1', 13, 5),
(26, 100, 'geras naujas', b'1', '2021-01-06 13:39:19', '2021-01-06 13:39:19', b'1', 2, 1);

-- --------------------------------------------------------

--
-- Table structure for table `familychipsets`
--

CREATE TABLE `familychipsets` (
  `Id` int(11) NOT NULL,
  `CoreFamily` text DEFAULT NULL,
  `Chipset` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `familychipsets`
--

INSERT INTO `familychipsets` (`Id`, `CoreFamily`, `Chipset`) VALUES
(1, 'Comet Lake', 'Intel B460'),
(2, 'Comet Lake', 'Intel H410'),
(3, 'Comet Lake', 'Intel Z490'),
(4, 'Comet Lake', 'Intel H470'),
(5, 'Matisse', 'AMD B450'),
(6, 'Matisse', 'AMD B550'),
(7, 'Coffee Lake Refresh', 'Intel Z390');

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

CREATE TABLE `products` (
  `Id` int(11) NOT NULL,
  `Name` text DEFAULT NULL,
  `ProductType` int(11) NOT NULL,
  `ImagePath` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`Id`, `Name`, `ProductType`, `ImagePath`) VALUES
(1, 'AMD Ryzen 5 3600', 1, 'https://localhost:5001/images/products/1.jpg'),
(2, 'Intel Core i7-10700K', 1, 'https://localhost:5001/images/products/2.jpg'),
(4, 'Intel Core i5 9700', 1, NULL),
(6, 'NZXT Kraken X62', 2, 'https://localhost:5001/images/products/6.jpg'),
(7, 'Gigabyte Z490 VISION G', 3, 'https://localhost:5001/images/products/7.jpg'),
(8, 'G.Skill Ripjaws V 32 GB', 4, 'https://localhost:5001/images/products/8.png'),
(9, 'Samsung 970 Evo', 5, 'https://localhost:5001/images/products/9.jpg'),
(10, 'Gigabyte RTX 3080', 6, 'https://localhost:5001/images/products/10.png'),
(11, 'NZXT H700', 7, 'https://localhost:5001/images/products/11.jpg'),
(12, 'Seagate Baraccuda 1TB 7200RPM', 5, NULL),
(13, 'Corsair RM750x', 8, 'https://localhost:5001/images/products/13.jpg'),
(22, 'Crucial Ballistix', 4, 'https://localhost:5001/images/products/22.jpg'),
(23, 'ASRock B450M', 3, 'https://localhost:5001/images/products/23.png'),
(24, 'MSI GeForce GTX 1050 Ti', 6, 'https://localhost:5001/images/products/24.jpg'),
(25, 'Noctua NH-D15', 2, 'https://localhost:5001/images/products/25.jpg'),
(26, 'be quiet! Dark Rock Pro 4', 2, 'https://localhost:5001/images/products/26.jpg'),
(27, 'Gigabyte AORUS MASTER Z390', 3, 'https://localhost:5001/images/products/27.jpg'),
(28, 'Kingston HyperX Fury RGB 8 GB', 4, 'https://localhost:5001/images/products/28.jpg'),
(29, 'Western Digital Blue 500 GB 2.5\"', 5, 'https://localhost:5001/images/products/29.jpg'),
(30, 'MSI Radeon RX 5700 XT 8 GB GAMING X', 6, 'https://localhost:5001/images/products/30.jpg'),
(31, 'Fractal Design Define Mini C', 7, 'https://localhost:5001/images/products/31.jpg'),
(32, 'NZXT H200i', 7, 'https://localhost:5001/images/products/32.jpg'),
(33, 'SeaSonic FOCUS Plus Gold 1000 W 80+ Gold', 8, 'https://localhost:5001/images/products/33.jpg'),
(34, 'EVGA BQ 750 W 80+ Bronze', 8, 'https://localhost:5001/images/products/34.jpg');

-- --------------------------------------------------------

--
-- Table structure for table `productspecifications`
--

CREATE TABLE `productspecifications` (
  `Id` int(11) NOT NULL,
  `ProductId` int(11) NOT NULL,
  `SpecificationId` int(11) NOT NULL,
  `Value` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `productspecifications`
--

INSERT INTO `productspecifications` (`Id`, `ProductId`, `SpecificationId`, `Value`) VALUES
(1, 2, 2, 'LGA1200'),
(2, 6, 9, 'LGA1150'),
(3, 6, 9, 'LGA1155'),
(4, 6, 9, 'LGA1200'),
(5, 2, 27, 'no'),
(6, 2, 3, 'Comet Lake'),
(7, 7, 12, 'LGA1200'),
(8, 7, 14, 'Intel Z490'),
(9, 8, 32, '2'),
(10, 8, 33, '8'),
(11, 8, 36, 'no'),
(12, 2, 37, '128'),
(13, 2, 38, 'yes'),
(14, 8, 30, 'DDR4'),
(15, 2, 39, 'DDR4'),
(16, 8, 31, '3600'),
(17, 7, 16, 'DDR4'),
(18, 7, 15, '128'),
(19, 7, 17, '4'),
(20, 7, 40, '3200'),
(21, 7, 41, 'yes'),
(23, 9, 43, '1000'),
(24, 9, 44, 'M.2-2280'),
(25, 9, 45, 'M.2'),
(26, 9, 46, 'Yes'),
(27, 7, 24, '2'),
(28, 7, 47, '2'),
(29, 10, 48, 'PCI-e x16'),
(30, 10, 49, '350'),
(31, 10, 50, '8'),
(32, 10, 50, '8'),
(33, 7, 18, '1'),
(34, 7, 19, '1'),
(35, 11, 51, 'ATX'),
(36, 11, 51, 'mATX'),
(37, 11, 51, 'Mini ITX'),
(38, 11, 52, '400'),
(39, 7, 13, 'ATX'),
(40, 12, 44, '3.5\"'),
(41, 11, 53, '2'),
(42, 11, 54, '2'),
(43, 7, 23, '4'),
(44, 12, 45, 'SATA 6GB/s'),
(45, 13, 58, 'Corsair'),
(46, 13, 55, 'ATX'),
(47, 13, 56, '1'),
(48, 13, 57, '10'),
(49, 11, 59, 'mATX'),
(50, 11, 59, 'Mini ITX'),
(51, 11, 59, 'atx'),
(52, 4, 2, 'LGA1151'),
(53, 4, 27, 'no'),
(54, 4, 3, 'Coffee Lake Refresh'),
(55, 4, 37, '128'),
(56, 4, 38, 'yes'),
(57, 4, 39, 'ddr4'),
(58, 1, 2, 'AM4'),
(59, 1, 3, 'Matisse'),
(60, 1, 27, 'yes'),
(61, 7, 40, '3600'),
(62, 1, 60, '4.2 Ghz'),
(63, 1, 5, '6'),
(64, 1, 6, '65W'),
(65, 2, 60, '5.1 Ghz'),
(66, 2, 5, '8'),
(67, 2, 6, '125W'),
(68, 4, 60, '4.9 Ghz'),
(69, 4, 5, '8'),
(70, 4, 6, '95W'),
(71, 2, 1, 'Intel'),
(72, 1, 1, 'AMD'),
(73, 6, 7, 'NZXT'),
(74, 6, 10, 'Water'),
(75, 6, 61, '500-2000 RPM'),
(76, 6, 62, '21 - 38 dB'),
(85, 12, 43, '1000'),
(86, 10, 63, '10'),
(87, 10, 64, '1800'),
(88, 11, 65, 'ATX'),
(89, 13, 66, '750'),
(90, 13, 67, 'Yes'),
(111, 22, 29, '288-pin DIMM'),
(112, 22, 28, 'Crucial'),
(114, 22, 36, 'No'),
(115, 22, 31, '4000'),
(116, 22, 30, 'DDR4'),
(117, 22, 33, '16'),
(118, 22, 35, '19-19-19-42'),
(119, 22, 34, '19'),
(120, 22, 32, '2'),
(121, 23, 11, 'ASRock'),
(122, 23, 12, 'AM4'),
(123, 23, 13, 'mATX'),
(124, 23, 24, '2'),
(125, 23, 22, '0'),
(126, 23, 20, '0'),
(127, 23, 21, '1'),
(128, 23, 23, '6'),
(129, 23, 41, 'No'),
(130, 23, 25, 'Yes'),
(132, 23, 26, 'No'),
(133, 23, 15, '64'),
(134, 23, 14, 'AMD B450'),
(135, 23, 17, '4'),
(136, 23, 47, '2'),
(137, 23, 16, 'DDR4'),
(138, 23, 18, '2'),
(139, 23, 19, '0'),
(140, 24, 63, '4'),
(141, 24, 49, '215 '),
(142, 24, 50, '8'),
(143, 24, 48, 'PCI-e x16'),
(144, 24, 64, '1455'),
(145, 25, 62, '27'),
(146, 25, 9, 'LGA1200'),
(147, 25, 7, 'Noctua'),
(148, 25, 9, 'LGA1150'),
(149, 25, 9, 'AM4'),
(150, 25, 10, 'Air'),
(151, 25, 61, '500-1000'),
(152, 26, 62, '13-33'),
(153, 26, 7, 'be quiet!'),
(154, 26, 9, 'LGA1151'),
(155, 26, 61, '1500'),
(156, 26, 10, 'Air'),
(157, 26, 9, 'LGA1200'),
(158, 27, 17, '4'),
(159, 27, 13, 'ATX'),
(160, 27, 15, '128'),
(161, 27, 11, 'Gigabyte'),
(162, 27, 14, 'Intel Z390'),
(163, 27, 12, 'LGA1151'),
(164, 27, 16, 'DDR4'),
(165, 27, 18, '3'),
(166, 27, 23, '6'),
(167, 27, 19, '0'),
(168, 27, 24, '3'),
(169, 27, 20, '0'),
(170, 27, 21, '3'),
(171, 27, 22, ' '),
(172, 27, 25, 'Yes'),
(173, 27, 26, 'No'),
(174, 27, 40, '4133'),
(175, 27, 40, '4000'),
(176, 27, 40, '3600'),
(177, 27, 41, 'No'),
(178, 27, 47, '3'),
(179, 28, 36, 'No'),
(180, 28, 29, '288-pin DIMM'),
(181, 28, 30, 'DDR4'),
(182, 28, 33, '8'),
(183, 28, 31, '3200'),
(184, 28, 28, 'Kingston'),
(185, 28, 32, '1'),
(186, 28, 34, '16'),
(187, 28, 35, '16-18-18-36'),
(188, 29, 44, '2.5\"'),
(189, 29, 46, ' '),
(190, 29, 42, 'Western Digital'),
(191, 29, 45, 'SATA 6GB/s'),
(192, 29, 43, '500'),
(193, 30, 49, '297'),
(194, 30, 50, '8'),
(195, 30, 64, '1980'),
(196, 30, 63, '8'),
(197, 30, 50, '8'),
(198, 30, 48, 'PCI-E x16'),
(199, 31, 65, 'MicroATX Mid Tower'),
(200, 31, 53, '3'),
(201, 31, 59, 'mATX'),
(202, 31, 51, 'Mini ITX'),
(203, 31, 51, 'mATX'),
(204, 31, 54, '2'),
(205, 31, 52, '315'),
(206, 32, 52, '325'),
(207, 32, 54, '1'),
(208, 32, 51, 'Mini ITX'),
(209, 32, 59, 'Mini ITX'),
(210, 32, 53, '3'),
(211, 32, 65, 'Mini ITX Tower'),
(212, 33, 57, '10'),
(213, 33, 56, '6'),
(214, 33, 58, 'SeaSonic'),
(215, 33, 55, 'ATX'),
(216, 33, 67, 'Yes'),
(217, 33, 66, '1000'),
(218, 34, 66, '750'),
(219, 34, 55, 'ATX'),
(220, 34, 67, 'Semi'),
(221, 34, 58, 'EVGA'),
(222, 34, 56, '3'),
(223, 34, 57, '9');

-- --------------------------------------------------------

--
-- Table structure for table `specifications`
--

CREATE TABLE `specifications` (
  `Id` int(11) NOT NULL,
  `Name` text DEFAULT NULL,
  `ProductType` int(11) NOT NULL,
  `Multiple` bit(1) DEFAULT b'0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `specifications`
--

INSERT INTO `specifications` (`Id`, `Name`, `ProductType`, `Multiple`) VALUES
(1, 'Manufacturer', 1, b'0'),
(2, 'Socket', 1, b'0'),
(3, 'Family', 1, b'0'),
(4, 'Integrated Graphics', 1, b'0'),
(5, 'Core Count', 1, b'0'),
(6, 'TDP', 1, b'0'),
(7, 'Manufacturer', 2, b'0'),
(9, 'Socket', 2, b'1'),
(10, 'Type', 2, b'0'),
(11, 'Manufacturer', 3, b'0'),
(12, 'Socket', 3, b'0'),
(13, 'Form factor', 3, b'0'),
(14, 'Chipset', 3, b'0'),
(15, 'Max Memory', 3, b'0'),
(16, 'Memory Type', 3, b'0'),
(17, 'Memory Slots', 3, b'0'),
(18, 'PCI-E X16 Slots', 3, b'0'),
(19, 'PCI-E X8 Slots', 3, b'0'),
(20, 'PCI-E X4 Slots', 3, b'0'),
(21, 'PCI-E X1 Slots', 3, b'0'),
(22, 'SATA 3GB/s Ports', 3, b'0'),
(23, 'SATA 6GB/s Ports', 3, b'0'),
(24, 'M.2 Slots', 3, b'0'),
(25, 'Onboard Video', 3, b'0'),
(26, 'Wireless', 3, b'0'),
(27, 'Cooler', 1, b'0'),
(28, 'Manufacturer', 4, b'0'),
(29, 'Form Factor', 4, b'0'),
(30, 'Type', 4, b'0'),
(31, 'Speed', 4, b'0'),
(32, 'Module Count', 4, b'0'),
(33, 'Module Size', 4, b'0'),
(34, 'CAS Latency', 4, b'0'),
(35, 'Timings', 4, b'0'),
(36, 'ECC', 4, b'0'),
(37, 'Maximum Supported Memory', 1, b'0'),
(38, 'ECC', 1, b'0'),
(39, 'Memory Type', 1, b'0'),
(40, 'Memory Speed', 3, b'1'),
(41, 'ECC', 3, b'0'),
(42, 'Manufacturer', 5, b'0'),
(43, 'Capacity', 5, b'0'),
(44, 'Form Factor', 5, b'0'),
(45, 'Interface', 5, b'0'),
(46, 'NVME', 5, b'0'),
(47, 'NVME Slots', 3, b'0'),
(48, 'Interface', 6, b'0'),
(49, 'Length', 6, b'0'),
(50, 'External Power', 6, b'1'),
(51, 'MOTHERBOARD FORM FACTOR', 7, b'1'),
(52, 'GPU Length', 7, b'0'),
(53, 'INTERNAL 3.5 BAYS', 7, b'0'),
(54, 'INTERNAL 2.5 BAYS', 7, b'0'),
(55, 'Form Factor', 8, b'0'),
(56, 'PCIe 6+2-Pin Connectors', 8, b'0'),
(57, 'SATA Connectors', 8, b'0'),
(58, 'Manufacturer', 8, b'0'),
(59, 'PSU Form factor', 7, b'1'),
(60, 'Boost Clock', 1, b'0'),
(61, 'Fan RPM', 2, b'0'),
(62, 'Noise Level', 2, b'0'),
(63, 'Memory', 6, b'0'),
(64, 'Boost Clock', 6, b'0'),
(65, 'Type', 7, b'0'),
(66, 'Wattage', 8, b'0'),
(67, 'Modular', 8, b'0');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `Id` int(11) NOT NULL,
  `Name` text DEFAULT NULL,
  `Phone` text DEFAULT NULL,
  `Email` text DEFAULT NULL,
  `Location` text DEFAULT NULL,
  `PasswordHash` varbinary(4000) DEFAULT NULL,
  `PasswordSalt` varbinary(4000) DEFAULT NULL,
  `Role` text NOT NULL DEFAULT 'Seller'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`Id`, `Name`, `Phone`, `Email`, `Location`, `PasswordHash`, `PasswordSalt`, `Role`) VALUES
(1, 'edvinas', '+37061121463', 'edvinas@gmail.com', 'Kaunas', 0x1e50cbe07c8ea1825e5a21d165c9a471a906136f500d029aaa11ba73fac4ee98b3be08a608591fb997e03b2314cafec9e6e4757f33338c043dde06fcdbb0d982, 0x568a3ef0fe4aadc1e5683720c074cbbde1ba59441e4f3a838fe52161bf3befed83d1193775e63322f0a8e6cae31e414e1cf51ce031281258adb5fb5d4ad5a172a3cd876cf6498a1ee4e6b21918698ed735e0163b021b99d148b40e17b1e01fc6d29a2364f9fc69ab318810f963531002b31c9a4b509b4b071a463c767f316468, 'Seller'),
(2, 'dominykas', '+3706214358', 'dominykas@gmail.com', 'Vilnius', 0x7dbbc153b597161ed9386b21dc9eb1d209a988398318384bbc68a808506527a5877ee04ff9ee34576d2104100eedfdf95a1b2ede0c5a4609fe5e80f406a4b0d3, 0x5cd35bfa6850501071d9be4f79b7075f720da497637b54235c4be7c1068052c180325019f4e8540ae4b115e617602eb9bc5a80db87d12988c1295f3620a0edac8cff7b5d7a449b6777e6e675271188de4af2bdaeb5f7eeff4c34f51c1d9f55d148fe8ec7bb33387dcbc55ffb310ff4fd2dc37fed6d0aca4533557cbca631bddd, 'Seller'),
(3, 'Admin', NULL, 'admin@admin.com', NULL, 0x6675573135c2d9074f27d8f20cd4315437931ec48dc18e8511dc17f3d08c128fd01fd3dde601e99d40904559c7c28cc1bf8e4d92351550e4bf9fe4266da7eda2, 0x40543e691127a40c0a43bc04d1fc1705005dea04b570193e2bbed6b9b8ce4c8a9b2dc5a517cf730fed71858164a87adf70d83b8d58542ec56b8cb6287dfcaae65f4981e9f018cb8141e1ece37d5f1ae229649f0fd0e7549c699009f027138f8ca96a169d6c7d76b2597f1c5916de11652f212ed5285c1469249afd198ab08497, 'Admin'),
(4, 'Mantas', '+37061258365', 'mantas@gmail.com', 'Kėdainiai', 0x28cc0696c24618bd0c0277a88d03ebb84ffb080ed64d6e96956496d858d89daea79eee109704be8cdc2e63f73f921d2ea6d5ecdd6741e053770cdf92706cf1fe, 0xe38bdc54e934d5330476986d21de32c608cfe606c2bb32fcf47a7d8016840d00122f4d17a8f7da2356165fc528cb5a0ba7e269218e941f4fe94967e98fdd78313647d2c6c8bb13e7596799e69ae095408388f55e8b7effa39a8f6429a06c1829d125846b6b537298b49e88655070ff6d65cf8dd1c225ff21bace975bd8e3c32b, 'Seller'),
(5, 'PC dalys', '+3706124444', 'help@pcdalys.lt', 'Kaunas', 0xe5bee3e4a994c1ac95d96408f525b34e144ff19416dbc6f59c3bf1ec72def960e7abba9dd7a4146b6bf5bb854f709076f93cb29fb594048a50bd37c5bcc49f18, 0x3c3388ad42070c2b86ffca4162e0d7fee4120ecdc9fc5fd64023514179c6f2c33ed297eda1e9f4553b7ff7ceb0878125ca5e02688495dc3d15b58eb8d76096b6beb4d8c62fa9c29e25f3e45d52be6eec41ef648c9501d73f4fe70d6a117ce91a8ccf432190d6b39348a52ee8b348cb4c4e725fd4369cae98738eee5eb6e7f33a, 'Seller'),
(6, 'Kajus', '+370546132', 'k.ame@gmail.com', 'Šiauliai', 0x4a0decb9f21ee4c7b9d55ae786eb1d685e0a0b6c2ee91c94272c482eadf2cd15bb4875e7e386f40b045d61584234a38210b1c1a082af84a18f19509f0a33ad25, 0x504b96646b19c7cdaf8a21c619c41a7d95b52b06ac4d51f4aa125a838be721b6f4046ae1c79bf699e65e0dcddb0f4653cb05070615a06411588d8265b2c0c5e23f74c8e7978b46365a2bf182c9d88f3b86a43d2f39e70e35cf13e19ec5ccac65d298d398ecdf5ed04a20cdf56b9d1508b26583a0bd993a163ad79bfd4610ca15, 'Seller'),
(7, 'testinis', '+386112121', 'test@test.com', 'Kaunas', 0x8accd886e2617ed268a5717546f4542890efef92ec160249cb78b2cd2be84ee5817a5764b45c5c750aa08688379721403b1de979b28822d2f24c651491599f2f, 0x47e864877faf7fd31cf3b7e72948a67c3c75c4aa01bf2ba2feb0943f618bc317b373dc10ac199bd27a65f712df6cd00bf13f67ba29f4a43b09e06b517945c0e3690905e7493d7d11ff1ee9ad53b52652df1717c726666ff05c8719f5555e9bad95884b9bcfabc5c1f19f8e2fbb461d52cb0f19ca79ed7f4b058c666efd75fbd6, 'Seller');

-- --------------------------------------------------------

--
-- Table structure for table `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20201020223147_Initial', '3.1.9'),
('20201026215005_ChipsetTable', '3.1.9'),
('20201110184620_SellerEdited', '3.1.9'),
('20201110212158_ChangedSellerToUsers', '3.1.9'),
('20201110213427_Role', '3.1.9'),
('20201114224856_AdvertPhotos', '3.1.9'),
('20201115001141_ProductPhoto', '3.1.9'),
('20201218175948_MultipleSpecsAttribute', '3.1.9');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `advertphotos`
--
ALTER TABLE `advertphotos`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_AdvertPhotos_AdvertId` (`AdvertId`);

--
-- Indexes for table `adverts`
--
ALTER TABLE `adverts`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Adverts_ProductId` (`ProductId`),
  ADD KEY `IX_Adverts_SellerId` (`SellerId`);

--
-- Indexes for table `familychipsets`
--
ALTER TABLE `familychipsets`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `productspecifications`
--
ALTER TABLE `productspecifications`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_ProductSpecifications_ProductId` (`ProductId`),
  ADD KEY `IX_ProductSpecifications_SpecificationId` (`SpecificationId`);

--
-- Indexes for table `specifications`
--
ALTER TABLE `specifications`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `advertphotos`
--
ALTER TABLE `advertphotos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT for table `adverts`
--
ALTER TABLE `adverts`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=27;

--
-- AUTO_INCREMENT for table `familychipsets`
--
ALTER TABLE `familychipsets`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `products`
--
ALTER TABLE `products`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=35;

--
-- AUTO_INCREMENT for table `productspecifications`
--
ALTER TABLE `productspecifications`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=224;

--
-- AUTO_INCREMENT for table `specifications`
--
ALTER TABLE `specifications`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=68;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `advertphotos`
--
ALTER TABLE `advertphotos`
  ADD CONSTRAINT `FK_AdvertPhotos_Adverts_AdvertId` FOREIGN KEY (`AdvertId`) REFERENCES `adverts` (`Id`);

--
-- Constraints for table `adverts`
--
ALTER TABLE `adverts`
  ADD CONSTRAINT `FK_Adverts_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`),
  ADD CONSTRAINT `FK_Adverts_Sellers_SellerId` FOREIGN KEY (`SellerId`) REFERENCES `users` (`Id`);

--
-- Constraints for table `productspecifications`
--
ALTER TABLE `productspecifications`
  ADD CONSTRAINT `FK_ProductSpecifications_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_ProductSpecifications_Specifications_SpecificationId` FOREIGN KEY (`SpecificationId`) REFERENCES `specifications` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
