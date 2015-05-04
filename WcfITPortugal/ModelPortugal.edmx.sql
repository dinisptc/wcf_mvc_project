
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 07/09/2012 15:17:06
-- Generated from EDMX file: G:\PROJECTOS\www.jobs-it.com\MvcTIC-IT_14_presente\MvcTIC-IT\MvcTIC-IT\WcfITPortugal\ModelPortugal.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Portugal];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_anuncio_empresa]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[anuncio] DROP CONSTRAINT [FK_anuncio_empresa];
GO
IF OBJECT_ID(N'[dbo].[FK_anunciodesc_anuncio]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[anunciodesc] DROP CONSTRAINT [FK_anunciodesc_anuncio];
GO
IF OBJECT_ID(N'[dbo].[FK_anunciodesc_idiomas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[anunciodesc] DROP CONSTRAINT [FK_anunciodesc_idiomas];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[anuncio]', 'U') IS NOT NULL
    DROP TABLE [dbo].[anuncio];
GO
IF OBJECT_ID(N'[dbo].[anunciodesc]', 'U') IS NOT NULL
    DROP TABLE [dbo].[anunciodesc];
GO
IF OBJECT_ID(N'[dbo].[empresa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[empresa];
GO
IF OBJECT_ID(N'[dbo].[idiomas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[idiomas];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'anuncio'
CREATE TABLE [dbo].[anuncio] (
    [id] int IDENTITY(1,1) NOT NULL,
    [dataanuncio] datetime  NOT NULL,
    [empresa_id] int  NOT NULL,
    [local_de_trabalho] nvarchar(200)  NOT NULL,
    [salario] nvarchar(200)  NOT NULL,
    [email] nvarchar(200)  NOT NULL,
    [identidade] nvarchar(200)  NOT NULL,
    [numvisitas] int  NOT NULL
);
GO

-- Creating table 'anunciodesc'
CREATE TABLE [dbo].[anunciodesc] (
    [id] int IDENTITY(1,1) NOT NULL,
    [titulo] nvarchar(200)  NOT NULL,
    [descricao] nvarchar(2000)  NOT NULL,
    [idioma_id] int  NOT NULL,
    [anuncio_id] int  NOT NULL
);
GO

-- Creating table 'empresa'
CREATE TABLE [dbo].[empresa] (
    [id] int IDENTITY(1,1) NOT NULL,
    [nome_da_empresa] nvarchar(50)  NULL,
    [url] nvarchar(500)  NULL,
    [logo] nvarchar(50)  NULL,
    [contacto] nvarchar(500)  NULL,
    [username] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'idiomas'
CREATE TABLE [dbo].[idiomas] (
    [id] int IDENTITY(1,1) NOT NULL,
    [lang_code] varchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'anuncio'
ALTER TABLE [dbo].[anuncio]
ADD CONSTRAINT [PK_anuncio]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'anunciodesc'
ALTER TABLE [dbo].[anunciodesc]
ADD CONSTRAINT [PK_anunciodesc]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'empresa'
ALTER TABLE [dbo].[empresa]
ADD CONSTRAINT [PK_empresa]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [id] in table 'idiomas'
ALTER TABLE [dbo].[idiomas]
ADD CONSTRAINT [PK_idiomas]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [empresa_id] in table 'anuncio'
ALTER TABLE [dbo].[anuncio]
ADD CONSTRAINT [FK_anuncio_empresa]
    FOREIGN KEY ([empresa_id])
    REFERENCES [dbo].[empresa]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_anuncio_empresa'
CREATE INDEX [IX_FK_anuncio_empresa]
ON [dbo].[anuncio]
    ([empresa_id]);
GO

-- Creating foreign key on [anuncio_id] in table 'anunciodesc'
ALTER TABLE [dbo].[anunciodesc]
ADD CONSTRAINT [FK_anunciodesc_anuncio]
    FOREIGN KEY ([anuncio_id])
    REFERENCES [dbo].[anuncio]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_anunciodesc_anuncio'
CREATE INDEX [IX_FK_anunciodesc_anuncio]
ON [dbo].[anunciodesc]
    ([anuncio_id]);
GO

-- Creating foreign key on [idioma_id] in table 'anunciodesc'
ALTER TABLE [dbo].[anunciodesc]
ADD CONSTRAINT [FK_anunciodesc_idiomas]
    FOREIGN KEY ([idioma_id])
    REFERENCES [dbo].[idiomas]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_anunciodesc_idiomas'
CREATE INDEX [IX_FK_anunciodesc_idiomas]
ON [dbo].[anunciodesc]
    ([idioma_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------