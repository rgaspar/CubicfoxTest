USE [master]
GO

IF DB_ID('cubicfoxdb') IS NOT NULL
  set noexec on 

CREATE DATABASE [cubicfoxdb];
GO

USE [cubicfoxdb]
GO
