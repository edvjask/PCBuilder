-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Oct 21, 2020 at 12:30 AM
-- Server version: 10.4.11-MariaDB
-- PHP Version: 7.2.29

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
-- Table structure for table `specifications`
--

CREATE TABLE `specifications` (
  `Id` int(11) NOT NULL,
  `Name` text DEFAULT NULL,
  `ProductType` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `specifications`
--

INSERT INTO `specifications` (`Id`, `Name`, `ProductType`) VALUES
(1, 'Manufacturer', 1),
(2, 'Socket', 1),
(3, 'Family', 1),
(4, 'Integrated Graphics', 1),
(5, 'Core Count', 1),
(6, 'TDP', 1),
(7, 'Manufacturer', 2),
(9, 'Socket', 2),
(10, 'Type', 2),
(11, 'Manufacturer', 3),
(12, 'Socket', 3),
(13, 'Form factor', 3),
(14, 'Chipset', 3),
(15, 'Max Memory', 3),
(16, 'Memory Type', 3),
(17, 'Memory Slots', 3),
(18, 'PCI-E X16 Slots', 3),
(19, 'PCI-E X8 Slots', 3),
(20, 'PCI-E X4 Slots', 3),
(21, 'PCI-E X1 Slots', 3),
(22, 'SATA 3GB/s Ports', 3),
(23, 'SATA 6GB/s Ports', 3),
(24, 'M.2 Slots', 3),
(25, 'Onboard Video', 3),
(26, 'Wireless', 3),
(27, 'Cooler', 1);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `specifications`
--
ALTER TABLE `specifications`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `specifications`
--
ALTER TABLE `specifications`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
