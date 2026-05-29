CREATE DATABASE  IF NOT EXISTS `pruebatecnicadb` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `pruebatecnicadb`;
-- MySQL dump 10.13  Distrib 8.0.40, for Win64 (x86_64)
--
-- Host: localhost    Database: pruebatecnicadb
-- ------------------------------------------------------
-- Server version	8.0.40

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `bitacoraerrores`
--

DROP TABLE IF EXISTS `bitacoraerrores`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `bitacoraerrores` (
  `IdBitacoraErrores` int NOT NULL AUTO_INCREMENT,
  `Fecha` datetime DEFAULT CURRENT_TIMESTAMP,
  `TipoEvento` varchar(50) NOT NULL,
  `Mensaje` text NOT NULL,
  `DetallesExcepcion` text,
  PRIMARY KEY (`IdBitacoraErrores`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bitacoraerrores`
--

LOCK TABLES `bitacoraerrores` WRITE;
/*!40000 ALTER TABLE `bitacoraerrores` DISABLE KEYS */;
INSERT INTO `bitacoraerrores` VALUES (1,'2026-05-29 05:05:46','Intento Fallido','Intento de inicio de sesión fallido para el correo: calomar.1992@gmail.com',NULL),(2,'2026-05-29 06:02:48','Intento Fallido','Intento de inicio de sesión fallido para el correo: calomar.1992@gmail.com',NULL);
/*!40000 ALTER TABLE `bitacoraerrores` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `clientes`
--

DROP TABLE IF EXISTS `clientes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clientes` (
  `IdClientes` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(100) NOT NULL,
  `IdentidadRTN` varchar(15) NOT NULL,
  `Telefono` varchar(15) DEFAULT NULL,
  `Correo` varchar(100) DEFAULT NULL,
  `FechaRegistro` datetime DEFAULT CURRENT_TIMESTAMP,
  `Estado` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`IdClientes`),
  UNIQUE KEY `IdentidadRTN` (`IdentidadRTN`),
  KEY `IDX_Clientes_IdentidadRTN` (`IdentidadRTN`),
  KEY `IDX_Clientes_Nombre` (`Nombre`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clientes`
--

LOCK TABLES `clientes` WRITE;
/*!40000 ALTER TABLE `clientes` DISABLE KEYS */;
INSERT INTO `clientes` VALUES (1,'Roberto Lagos','0801-1988-04549','9875-4512','roberto87lagos@gmail.com','2026-05-27 14:55:04',1),(2,'Juan López','0801-1986-04578','9876-2545','juan25@gmail.com','2026-05-29 06:37:23',0),(3,'Luis Rodriguez','0801-1996-05478','9456-5878','luigi@gmail.com','2026-05-29 07:32:09',1),(4,'María Rodríguez','0809-1967-00085','9654-2575','maria2r@gmail.com','2026-05-29 11:53:53',1);
/*!40000 ALTER TABLE `clientes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `facturadetalles`
--

DROP TABLE IF EXISTS `facturadetalles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `facturadetalles` (
  `IdFacturaDetalles` int NOT NULL AUTO_INCREMENT,
  `IdFacturas` int NOT NULL,
  `IdProductos` int NOT NULL,
  `Cantidad` int NOT NULL,
  `PrecioUnitario` decimal(10,2) NOT NULL,
  `Subtotal` decimal(10,2) NOT NULL,
  PRIMARY KEY (`IdFacturaDetalles`),
  KEY `IdFacturas` (`IdFacturas`),
  KEY `IdProductos` (`IdProductos`),
  CONSTRAINT `facturadetalles_ibfk_1` FOREIGN KEY (`IdFacturas`) REFERENCES `facturas` (`IdFacturas`),
  CONSTRAINT `facturadetalles_ibfk_2` FOREIGN KEY (`IdProductos`) REFERENCES `productos` (`IdProductos`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `facturadetalles`
--

LOCK TABLES `facturadetalles` WRITE;
/*!40000 ALTER TABLE `facturadetalles` DISABLE KEYS */;
INSERT INTO `facturadetalles` VALUES (1,1,3,2,28500.00,57000.00),(2,2,1,3,5000.00,15000.00),(3,2,27,1,45000.00,45000.00),(4,3,1,6,5000.00,30000.00),(5,4,15,2,16500.00,33000.00);
/*!40000 ALTER TABLE `facturadetalles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `facturas`
--

DROP TABLE IF EXISTS `facturas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `facturas` (
  `IdFacturas` int NOT NULL AUTO_INCREMENT,
  `IdUsuarios` int NOT NULL,
  `IdClientes` int NOT NULL,
  `Fecha` datetime DEFAULT CURRENT_TIMESTAMP,
  `Subtotal` decimal(10,2) NOT NULL,
  `ISV` decimal(10,2) NOT NULL,
  `Total` decimal(10,2) NOT NULL,
  PRIMARY KEY (`IdFacturas`),
  KEY `IdUsuarios` (`IdUsuarios`),
  KEY `IdClientes` (`IdClientes`),
  CONSTRAINT `facturas_ibfk_1` FOREIGN KEY (`IdUsuarios`) REFERENCES `usuarios` (`IdUsuarios`),
  CONSTRAINT `facturas_ibfk_2` FOREIGN KEY (`IdClientes`) REFERENCES `clientes` (`IdClientes`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `facturas`
--

LOCK TABLES `facturas` WRITE;
/*!40000 ALTER TABLE `facturas` DISABLE KEYS */;
INSERT INTO `facturas` VALUES (1,1,1,'2026-05-29 14:10:17',57000.00,8550.00,65550.00),(2,1,3,'2026-05-29 14:12:43',60000.00,9000.00,69000.00),(3,1,4,'2026-05-29 14:16:57',30000.00,4500.00,34500.00),(4,1,1,'2026-05-29 14:18:01',33000.00,4950.00,37950.00);
/*!40000 ALTER TABLE `facturas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `productos`
--

DROP TABLE IF EXISTS `productos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `productos` (
  `IdProductos` int NOT NULL AUTO_INCREMENT,
  `Codigo` varchar(50) NOT NULL,
  `Nombre` varchar(150) NOT NULL,
  `Precio` decimal(10,2) NOT NULL,
  `Stock` int NOT NULL DEFAULT '0',
  `Estado` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`IdProductos`),
  UNIQUE KEY `Codigo` (`Codigo`),
  KEY `IDX_Productos_Codigo` (`Codigo`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productos`
--

LOCK TABLES `productos` WRITE;
/*!40000 ALTER TABLE `productos` DISABLE KEYS */;
INSERT INTO `productos` VALUES (1,'MOVIL-0001','SAMSUNG A25',5000.00,2,1),(2,'MOVIL-0002','Samsung A21s',3600.00,15,1),(3,'MOVIL-0003','Samsung Galaxy S24 Ultra',28500.00,6,1),(4,'MOVIL-0004','Xiaomi Redmi Note 13 Pro',6500.00,20,1),(5,'MOVIL-0005','iPhone 15 Pro Max',32000.00,5,1),(6,'MOVIL-0006','Motorola Edge 50 Ultra',14500.00,12,1),(7,'MOVIL-0007','Samsung Galaxy A55',8500.00,18,1),(8,'MOVIL-0008','Huawei Nova 12 SE',7200.00,0,0),(9,'MOVIL-0009','Sony Xperia 1 VI',24000.00,3,1),(10,'MOVIL-0010','Google Pixel 8 Pro',19500.00,7,1),(11,'MOVIL-0011','Xiaomi Poco X6 Pro',5800.00,25,1),(12,'MOVIL-0012','iPhone 14 Plus',21000.00,6,1),(13,'MOVIL-0013','Motorola Moto G84',4500.00,15,1),(14,'MOVIL-0014','Samsung Galaxy Z Fold 6',38000.00,2,1),(15,'LAPTOP-001','HP Pavilion 15 Ryzen 7',16500.00,8,1),(16,'LAPTOP-002','Dell Inspiron 14 Intel i5',14200.00,14,1),(17,'LAPTOP-003','Lenovo IdeaPad Slim 3',11500.00,20,1),(18,'LAPTOP-004','ASUS ROG Strix G16 Gaming',34000.00,4,1),(19,'LAPTOP-005','Apple MacBook Air M3',27500.00,8,1),(20,'LAPTOP-006','Acer Nitro V15 Gaming',18900.00,11,1),(21,'LAPTOP-007','MSI Thin 15 Intel i7',22500.00,6,1),(22,'LAPTOP-008','Huawei MateBook D16',15800.00,9,1),(23,'LAPTOP-009','Lenovo ThinkPad E14',19200.00,12,1),(24,'LAPTOP-010','Dell Vostro 3400',12000.00,0,0),(25,'LAPTOP-011','HP Victus 16 Rtx 4050',21500.00,7,1),(26,'LAPTOP-012','ASUS Zenbook 14 OLED',25000.00,5,1),(27,'LAPTOP-013','Apple MacBook Pro M3 Pro',45000.00,2,1);
/*!40000 ALTER TABLE `productos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuarios` (
  `IdUsuarios` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(100) NOT NULL,
  `Correo` varchar(100) NOT NULL,
  `Contrasena` varchar(255) NOT NULL,
  `Estado` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`IdUsuarios`),
  UNIQUE KEY `Correo` (`Correo`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES (1,'Carlos Martinez','calomar.1992@gmail.com','Carlos123',1);
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-05-29 17:11:37
