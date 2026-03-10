-- Run this script in your SQL Server to create the database and Users table expected by the app.
-- Adjust database name or connection string in Web.config if needed.

CREATE DATABASE EmployeeDB;
GO

USE EmployeeDB;
GO

CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    -- legacy plaintext password (kept nullable for migration). New code stores salted hashes instead.
    Password NVARCHAR(200) NULL,
    PasswordHash VARBINARY(MAX) NULL,
    PasswordSalt VARBINARY(MAX) NULL,
    Name NVARCHAR(200) NULL,
    Address NVARCHAR(500) NULL,
    Contact NVARCHAR(50) NULL
);
GO

-- Insert a sample user (password meets complexity: Abc@123)
INSERT INTO Users (Username, Password, Name, Address, Contact) VALUES ('admin','Abc@123','Administrator','Head Office','1234567890');
GO
