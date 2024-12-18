USE [master]
GO

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'cubicfoxd')
BEGIN
    CREATE DATABASE [cubicfoxd]
END
GO 

USE [cubicfoxdb]
GO
