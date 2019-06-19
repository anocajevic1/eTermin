IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607214410_MojaMigracija')
BEGIN
    CREATE TABLE [Administrator] (
        [AdministratorID] int NOT NULL IDENTITY,
        [Username] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [AccessLevel] int NOT NULL,
        CONSTRAINT [PK_Administrator] PRIMARY KEY ([AdministratorID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607214410_MojaMigracija')
BEGIN
    CREATE TABLE [SportCentre] (
        [SportCentreID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NOT NULL,
        [Photo] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_SportCentre] PRIMARY KEY ([SportCentreID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607214410_MojaMigracija')
BEGIN
    CREATE TABLE [Hall] (
        [HallID] int NOT NULL IDENTITY,
        [StartTime] datetime2 NOT NULL,
        [EndTime] datetime2 NOT NULL,
        [Duration] int NOT NULL,
        [Price] int NOT NULL,
        [Sport] int NOT NULL,
        [SportCentreID] int NOT NULL,
        CONSTRAINT [PK_Hall] PRIMARY KEY ([HallID]),
        CONSTRAINT [FK_Hall_SportCentre_SportCentreID] FOREIGN KEY ([SportCentreID]) REFERENCES [SportCentre] ([SportCentreID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607214410_MojaMigracija')
BEGIN
    CREATE TABLE [Person] (
        [PersonID] int NOT NULL IDENTITY,
        [Username] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [Discriminator] nvarchar(max) NOT NULL,
        [SportCentreID] int NULL,
        [Balance] float NULL,
        [Photo] nvarchar(max) NULL,
        CONSTRAINT [PK_Person] PRIMARY KEY ([PersonID]),
        CONSTRAINT [FK_Person_SportCentre_SportCentreID] FOREIGN KEY ([SportCentreID]) REFERENCES [SportCentre] ([SportCentreID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607214410_MojaMigracija')
BEGIN
    CREATE TABLE [Log] (
        [LogID] int NOT NULL IDENTITY,
        [DateTime] datetime2 NOT NULL,
        [Note] nvarchar(max) NULL,
        [PersonID] int NOT NULL,
        CONSTRAINT [PK_Log] PRIMARY KEY ([LogID]),
        CONSTRAINT [FK_Log_Person_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Person] ([PersonID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607214410_MojaMigracija')
BEGIN
    CREATE TABLE [Reservation] (
        [ReservationID] int NOT NULL IDENTITY,
        [DateTimeCreated] datetime2 NOT NULL,
        [DateTime] datetime2 NOT NULL,
        [HallID] int NOT NULL,
        [PersonID] int NOT NULL,
        CONSTRAINT [PK_Reservation] PRIMARY KEY ([ReservationID]),
        CONSTRAINT [FK_Reservation_Hall_HallID] FOREIGN KEY ([HallID]) REFERENCES [Hall] ([HallID]),
        CONSTRAINT [FK_Reservation_Person_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Person] ([PersonID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607214410_MojaMigracija')
BEGIN
    CREATE TABLE [Subscription] (
        [SubscriptionID] int NOT NULL IDENTITY,
        [DayOfWeek] int NOT NULL,
        [Time] datetime2 NOT NULL,
        [Note] nvarchar(max) NULL,
        [HallID] int NOT NULL,
        [UserID] int NOT NULL,
        CONSTRAINT [PK_Subscription] PRIMARY KEY ([SubscriptionID]),
        CONSTRAINT [FK_Subscription_Hall_HallID] FOREIGN KEY ([HallID]) REFERENCES [Hall] ([HallID]),
        CONSTRAINT [FK_Subscription_Person_UserID] FOREIGN KEY ([UserID]) REFERENCES [Person] ([PersonID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607214410_MojaMigracija')
BEGIN
    CREATE TABLE [Transaction] (
        [TransactionID] int NOT NULL IDENTITY,
        [Time] datetime2 NOT NULL,
        [Amount] int NOT NULL,
        [Note] nvarchar(max) NULL,
        [SportCentreID] int NOT NULL,
        [UserID] int NOT NULL,
        [EmployeeID] int NOT NULL,
        CONSTRAINT [PK_Transaction] PRIMARY KEY ([TransactionID]),
        CONSTRAINT [FK_Transaction_Person_EmployeeID] FOREIGN KEY ([EmployeeID]) REFERENCES [Person] ([PersonID]),
        CONSTRAINT [FK_Transaction_SportCentre_SportCentreID] FOREIGN KEY ([SportCentreID]) REFERENCES [SportCentre] ([SportCentreID]),
        CONSTRAINT [FK_Transaction_Person_UserID] FOREIGN KEY ([UserID]) REFERENCES [Person] ([PersonID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607214410_MojaMigracija')
BEGIN
    CREATE INDEX [IX_Hall_SportCentreID] ON [Hall] ([SportCentreID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607214410_MojaMigracija')
BEGIN
    CREATE INDEX [IX_Log_PersonID] ON [Log] ([PersonID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607214410_MojaMigracija')
BEGIN
    CREATE INDEX [IX_Person_SportCentreID] ON [Person] ([SportCentreID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607214410_MojaMigracija')
BEGIN
    CREATE INDEX [IX_Reservation_HallID] ON [Reservation] ([HallID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607214410_MojaMigracija')
BEGIN
    CREATE INDEX [IX_Reservation_PersonID] ON [Reservation] ([PersonID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607214410_MojaMigracija')
BEGIN
    CREATE INDEX [IX_Subscription_HallID] ON [Subscription] ([HallID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607214410_MojaMigracija')
BEGIN
    CREATE INDEX [IX_Subscription_UserID] ON [Subscription] ([UserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607214410_MojaMigracija')
BEGIN
    CREATE INDEX [IX_Transaction_EmployeeID] ON [Transaction] ([EmployeeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607214410_MojaMigracija')
BEGIN
    CREATE INDEX [IX_Transaction_SportCentreID] ON [Transaction] ([SportCentreID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607214410_MojaMigracija')
BEGIN
    CREATE INDEX [IX_Transaction_UserID] ON [Transaction] ([UserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190607214410_MojaMigracija')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190607214410_MojaMigracija', N'2.2.4-servicing-10062');
END;

GO

