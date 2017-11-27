
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/23/2014 15:25:50
-- Generated from EDMX file: D:\Dropbox\DailySoft\Projet\LeaveManagement\LeaveManagement_Entities\LeaveManagementModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LeaveManagement];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CompanyLeave]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Leave] DROP CONSTRAINT [FK_CompanyLeave];
GO
IF OBJECT_ID(N'[dbo].[FK_NationalHoliday_inherits_PublicHoliday]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PublicHoliday_NationalHoliday] DROP CONSTRAINT [FK_NationalHoliday_inherits_PublicHoliday];
GO
IF OBJECT_ID(N'[dbo].[FK_HijriHoliday_inherits_PublicHoliday]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PublicHoliday_HijriHoliday] DROP CONSTRAINT [FK_HijriHoliday_inherits_PublicHoliday];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PublicHoliday]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PublicHoliday];
GO
IF OBJECT_ID(N'[dbo].[Leave]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Leave];
GO
IF OBJECT_ID(N'[dbo].[Company]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Company];
GO
IF OBJECT_ID(N'[dbo].[PublicHoliday_NationalHoliday]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PublicHoliday_NationalHoliday];
GO
IF OBJECT_ID(N'[dbo].[PublicHoliday_HijriHoliday]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PublicHoliday_HijriHoliday];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PublicHoliday'
CREATE TABLE [dbo].[PublicHoliday] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Day] smallint  NOT NULL,
    [Month] smallint  NOT NULL
);
GO

-- Creating table 'Leave'
CREATE TABLE [dbo].[Leave] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [StartDate] datetimeoffset  NOT NULL,
    [EndDate] datetimeoffset  NULL,
    [Description] nvarchar(max)  NULL,
    [Company_ID] int  NOT NULL
);
GO

-- Creating table 'Company'
CREATE TABLE [dbo].[Company] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [RecruitementDate] datetimeoffset  NOT NULL,
    [EndOfMissionDate] datetimeoffset  NULL
);
GO

-- Creating table 'PublicHoliday_NationalHoliday'
CREATE TABLE [dbo].[PublicHoliday_NationalHoliday] (
    [ID] int  NOT NULL
);
GO

-- Creating table 'PublicHoliday_HijriHoliday'
CREATE TABLE [dbo].[PublicHoliday_HijriHoliday] (
    [Year] smallint  NOT NULL,
    [ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'PublicHoliday'
ALTER TABLE [dbo].[PublicHoliday]
ADD CONSTRAINT [PK_PublicHoliday]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Leave'
ALTER TABLE [dbo].[Leave]
ADD CONSTRAINT [PK_Leave]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Company'
ALTER TABLE [dbo].[Company]
ADD CONSTRAINT [PK_Company]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'PublicHoliday_NationalHoliday'
ALTER TABLE [dbo].[PublicHoliday_NationalHoliday]
ADD CONSTRAINT [PK_PublicHoliday_NationalHoliday]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'PublicHoliday_HijriHoliday'
ALTER TABLE [dbo].[PublicHoliday_HijriHoliday]
ADD CONSTRAINT [PK_PublicHoliday_HijriHoliday]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Company_ID] in table 'Leave'
ALTER TABLE [dbo].[Leave]
ADD CONSTRAINT [FK_CompanyLeave]
    FOREIGN KEY ([Company_ID])
    REFERENCES [dbo].[Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyLeave'
CREATE INDEX [IX_FK_CompanyLeave]
ON [dbo].[Leave]
    ([Company_ID]);
GO

-- Creating foreign key on [ID] in table 'PublicHoliday_NationalHoliday'
ALTER TABLE [dbo].[PublicHoliday_NationalHoliday]
ADD CONSTRAINT [FK_NationalHoliday_inherits_PublicHoliday]
    FOREIGN KEY ([ID])
    REFERENCES [dbo].[PublicHoliday]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ID] in table 'PublicHoliday_HijriHoliday'
ALTER TABLE [dbo].[PublicHoliday_HijriHoliday]
ADD CONSTRAINT [FK_HijriHoliday_inherits_PublicHoliday]
    FOREIGN KEY ([ID])
    REFERENCES [dbo].[PublicHoliday]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------