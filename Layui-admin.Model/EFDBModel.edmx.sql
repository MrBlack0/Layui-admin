
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/03/2019 22:08:54
-- Generated from EDMX file: C:\Users\ThinkPad\documents\visual studio 2017\Projects\Layui-admin\Layui-admin.Model\EFDBModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [WebSiteTemplate1];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Ad]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ad];
GO
IF OBJECT_ID(N'[dbo].[Admin_User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Admin_User];
GO
IF OBJECT_ID(N'[dbo].[News]', 'U') IS NOT NULL
    DROP TABLE [dbo].[News];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Ad'
CREATE TABLE [dbo].[Ad] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ParentId] int  NULL,
    [Sorts] int  NULL,
    [Name] nvarchar(100)  NULL,
    [EnName] nvarchar(100)  NULL,
    [Width] int  NULL,
    [Height] int  NULL,
    [Url] nvarchar(200)  NULL,
    [Pic] nvarchar(200)  NULL,
    [Description] nvarchar(max)  NULL,
    [Contents] nvarchar(max)  NULL,
    [AddTime] datetime  NULL
);
GO

-- Creating table 'Admin_User'
CREATE TABLE [dbo].[Admin_User] (
    [Id] nvarchar(50)  NOT NULL,
    [NickName] nvarchar(150)  NULL,
    [UserName] nvarchar(50)  NULL,
    [RealName] nvarchar(150)  NULL,
    [PassWord] nvarchar(50)  NULL,
    [Email] nvarchar(25)  NULL,
    [Phone] nvarchar(20)  NULL,
    [Sex] int  NOT NULL,
    [Photo] nvarchar(100)  NULL,
    [Age] int  NOT NULL,
    [Brithday] datetime  NULL,
    [Introduce] nvarchar(200)  NULL,
    [Country] nvarchar(50)  NULL,
    [Province] int  NULL,
    [City] int  NULL,
    [Address] nvarchar(250)  NULL,
    [LoginIp] nvarchar(50)  NULL,
    [RoleID] nvarchar(50)  NULL,
    [CreateUserID] nvarchar(50)  NOT NULL,
    [CreateUserName] nvarchar(50)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [ModifyUserID] nvarchar(50)  NULL,
    [ModifyUserName] nvarchar(50)  NULL,
    [UpdateDate] datetime  NULL,
    [DeleteMark] bit  NOT NULL
);
GO

-- Creating table 'News'
CREATE TABLE [dbo].[News] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Sorts] int  NULL,
    [ClassId] int  NULL,
    [Hot] int  NULL,
    [Tops] int  NULL,
    [ClickCount] bigint  NULL,
    [CommentCount] bigint  NULL,
    [Name] nvarchar(100)  NULL,
    [SubName] nvarchar(100)  NULL,
    [OutLink] nvarchar(100)  NULL,
    [Author] nvarchar(50)  NULL,
    [Froms] nvarchar(50)  NULL,
    [Pic] nvarchar(100)  NULL,
    [Tags] nvarchar(200)  NULL,
    [DownloadFiles] nvarchar(50)  NULL,
    [Description] nvarchar(max)  NULL,
    [Contents] nvarchar(max)  NULL,
    [MobContents] nvarchar(max)  NULL,
    [IsHidden] int  NULL,
    [AddTime] datetime  NULL
);
GO

-- Creating table 'ProductSet'
CREATE TABLE [dbo].[ProductSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Ad'
ALTER TABLE [dbo].[Ad]
ADD CONSTRAINT [PK_Ad]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Admin_User'
ALTER TABLE [dbo].[Admin_User]
ADD CONSTRAINT [PK_Admin_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'News'
ALTER TABLE [dbo].[News]
ADD CONSTRAINT [PK_News]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductSet'
ALTER TABLE [dbo].[ProductSet]
ADD CONSTRAINT [PK_ProductSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------