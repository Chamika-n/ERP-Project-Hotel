-- phpMyAdmin SQL Dump
-- version 5.2.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Jul 10, 2026 at 02:16 PM
-- Server version: 8.4.7
-- PHP Version: 8.3.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `grand_hotel_db.sql`
--

-- --------------------------------------------------------

--
-- Table structure for table `rooms`
--

DROP TABLE IF EXISTS `rooms`;
CREATE TABLE IF NOT EXISTS `rooms` (
  `id` int NOT NULL AUTO_INCREMENT,
  `room_number` varchar(10) COLLATE utf8mb4_unicode_ci NOT NULL,
  `room_type` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `floor` varchar(10) COLLATE utf8mb4_unicode_ci NOT NULL,
  `capacity` int NOT NULL DEFAULT '2',
  `rate_per_night` decimal(10,2) NOT NULL DEFAULT '0.00',
  `status` enum('Available','Occupied','Reserved','Cleaning','Maintenance') COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT 'Available',
  `condition` enum('Excellent','Good','Fair','Needs Repair') COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT 'Excellent',
  `amenities` text COLLATE utf8mb4_unicode_ci,
  `notes` text COLLATE utf8mb4_unicode_ci,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `room_number` (`room_number`)
) ENGINE=MyISAM AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `rooms`
--

INSERT INTO `rooms` (`id`, `room_number`, `room_type`, `floor`, `capacity`, `rate_per_night`, `status`, `condition`, `amenities`, `notes`, `created_at`, `updated_at`) VALUES
(1, '101', 'Standard', '1', 2, 14500.00, 'Available', 'Excellent', 'Wi-Fi, Air Conditioning, TV', 'Near reception', '2026-07-10 14:14:17', '2026-07-10 14:14:17'),
(2, '102', 'Standard', '1', 2, 14500.00, 'Occupied', 'Good', 'Wi-Fi, Air Conditioning, TV', '', '2026-07-10 14:14:17', '2026-07-10 14:14:17'),
(3, '103', 'Deluxe', '1', 2, 19500.00, 'Reserved', 'Excellent', 'Wi-Fi, Air Conditioning, Smart TV, Mini Bar', 'Late check-in expected', '2026-07-10 14:14:17', '2026-07-10 14:14:17'),
(4, '104', 'Deluxe', '1', 2, 19500.00, 'Available', 'Excellent', 'Wi-Fi, Air Conditioning, Smart TV', '', '2026-07-10 14:14:17', '2026-07-10 14:14:17'),
(5, '105', 'Family', '1', 4, 25500.00, 'Cleaning', 'Good', 'Wi-Fi, Air Conditioning, TV, Mini Fridge', '', '2026-07-10 14:14:17', '2026-07-10 14:14:17'),
(6, '201', 'Deluxe', '2', 2, 20500.00, 'Cleaning', 'Good', 'Wi-Fi, Air Conditioning, Smart TV, Balcony', 'Priority cleaning', '2026-07-10 14:14:17', '2026-07-10 14:14:17'),
(7, '202', 'Suite', '2', 3, 36500.00, 'Available', 'Excellent', 'Wi-Fi, Smart TV, Mini Bar, Jacuzzi', '', '2026-07-10 14:14:17', '2026-07-10 14:14:17'),
(8, '203', 'Suite', '2', 3, 36500.00, 'Occupied', 'Excellent', 'Wi-Fi, Smart TV, Mini Bar, Jacuzzi', '', '2026-07-10 14:14:17', '2026-07-10 14:14:17'),
(9, '204', 'Executive', '2', 2, 28500.00, 'Available', 'Excellent', 'Wi-Fi, Smart TV, Coffee Machine', '', '2026-07-10 14:14:17', '2026-07-10 14:14:17'),
(10, '205', 'Executive', '2', 2, 28500.00, 'Maintenance', 'Needs Repair', 'Wi-Fi, Smart TV', 'Air conditioner repair', '2026-07-10 14:14:17', '2026-07-10 14:14:17'),
(11, '301', 'Presidential Suite', '3', 5, 65000.00, 'Available', 'Excellent', 'Wi-Fi, Smart TV, Jacuzzi, Kitchen, Living Room', '', '2026-07-10 14:14:17', '2026-07-10 14:14:17'),
(12, '302', 'Presidential Suite', '3', 5, 65000.00, 'Reserved', 'Excellent', 'Wi-Fi, Smart TV, Jacuzzi, Kitchen, Living Room', 'VIP Guest', '2026-07-10 14:14:17', '2026-07-10 14:14:17'),
(13, '303', 'Family', '3', 4, 25500.00, 'Available', 'Good', 'Wi-Fi, Air Conditioning, TV', '', '2026-07-10 14:14:17', '2026-07-10 14:14:17'),
(14, '304', 'Standard', '3', 2, 14500.00, 'Occupied', 'Fair', 'Wi-Fi, TV', '', '2026-07-10 14:14:17', '2026-07-10 14:14:17'),
(15, '305', 'Deluxe', '3', 2, 20500.00, 'Available', 'Excellent', 'Wi-Fi, Smart TV, Balcony', '', '2026-07-10 14:14:17', '2026-07-10 14:14:17');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
