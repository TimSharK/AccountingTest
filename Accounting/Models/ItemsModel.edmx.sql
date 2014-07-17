
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 07/17/2014 02:07:54
-- Generated from EDMX file: G:\WorkBook\Accounting\Accounting\Models\ItemsModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Accounting_ItemsDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ItemPropertyValue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ParameterValues] DROP CONSTRAINT [FK_ItemPropertyValue];
GO
IF OBJECT_ID(N'[dbo].[FK_ParameterParameterValue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ParameterValues] DROP CONSTRAINT [FK_ParameterParameterValue];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Items]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Items];
GO
IF OBJECT_ID(N'[dbo].[Parameters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Parameters];
GO
IF OBJECT_ID(N'[dbo].[ParameterValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ParameterValues];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Items'
CREATE TABLE [dbo].[Items] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Number] bigint  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'Parameters'
CREATE TABLE [dbo].[Parameters] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Type] int  NOT NULL,
    [IsInSchema] bit  NOT NULL
);
GO

-- Creating table 'ParameterValues'
CREATE TABLE [dbo].[ParameterValues] (
    [Id] uniqueidentifier  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [ItemId] bigint  NOT NULL,
    [ParameterId] bigint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Items'
ALTER TABLE [dbo].[Items]
ADD CONSTRAINT [PK_Items]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Parameters'
ALTER TABLE [dbo].[Parameters]
ADD CONSTRAINT [PK_Parameters]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ParameterValues'
ALTER TABLE [dbo].[ParameterValues]
ADD CONSTRAINT [PK_ParameterValues]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ItemId] in table 'ParameterValues'
ALTER TABLE [dbo].[ParameterValues]
ADD CONSTRAINT [FK_ItemPropertyValue]
    FOREIGN KEY ([ItemId])
    REFERENCES [dbo].[Items]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemPropertyValue'
CREATE INDEX [IX_FK_ItemPropertyValue]
ON [dbo].[ParameterValues]
    ([ItemId]);
GO

-- Creating foreign key on [ParameterId] in table 'ParameterValues'
ALTER TABLE [dbo].[ParameterValues]
ADD CONSTRAINT [FK_ParameterParameterValue]
    FOREIGN KEY ([ParameterId])
    REFERENCES [dbo].[Parameters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ParameterParameterValue'
CREATE INDEX [IX_FK_ParameterParameterValue]
ON [dbo].[ParameterValues]
    ([ParameterId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------