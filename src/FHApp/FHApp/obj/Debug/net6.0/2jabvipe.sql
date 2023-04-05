IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Developers] (
    [Id] uniqueidentifier NOT NULL,
    [Name] varchar(200) NOT NULL,
    [Document] varchar(14) NOT NULL,
    [DeveloperType] int NOT NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_Developers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Addresses] (
    [Id] uniqueidentifier NOT NULL,
    [DeveloperId] uniqueidentifier NOT NULL,
    [Street] varchar(200) NOT NULL,
    [Number] varchar(50) NOT NULL,
    [Complement] varchar(250) NOT NULL,
    [ZipCode] varchar(8) NOT NULL,
    [Neighborhood] varchar(100) NOT NULL,
    [City] varchar(100) NOT NULL,
    [State] varchar(50) NOT NULL,
    [Country] nvarchar(max) NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Addresses_Developers_DeveloperId] FOREIGN KEY ([DeveloperId]) REFERENCES [Developers] ([Id])
);
GO

CREATE TABLE [Games] (
    [Id] uniqueidentifier NOT NULL,
    [DeveloperId] uniqueidentifier NOT NULL,
    [Name] varchar(200) NOT NULL,
    [Description] varchar(1000) NOT NULL,
    [Image] varchar(100) NOT NULL,
    [Value] decimal(18,2) NOT NULL,
    [RegisterDate] datetime2 NOT NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_Games] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Games_Developers_DeveloperId] FOREIGN KEY ([DeveloperId]) REFERENCES [Developers] ([Id])
);
GO

CREATE UNIQUE INDEX [IX_Addresses_DeveloperId] ON [Addresses] ([DeveloperId]);
GO

CREATE INDEX [IX_Games_DeveloperId] ON [Games] ([DeveloperId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230311182911_Initial', N'6.0.14');
GO

COMMIT;
GO

