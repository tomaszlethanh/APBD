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

CREATE TABLE [Doctors] (
    [IdDoctor] int NOT NULL IDENTITY,
    [FirstName] nvarchar(100) NOT NULL,
    [LastName] nvarchar(100) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Doctors] PRIMARY KEY ([IdDoctor])
);
GO

CREATE TABLE [Medicaments] (
    [IdMedicament] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(100) NOT NULL,
    [Type] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Medicaments] PRIMARY KEY ([IdMedicament])
);
GO

CREATE TABLE [Patients] (
    [IdPatient] int NOT NULL IDENTITY,
    [FirstName] nvarchar(100) NOT NULL,
    [LastName] nvarchar(100) NOT NULL,
    [BirthDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Patients] PRIMARY KEY ([IdPatient])
);
GO

CREATE TABLE [Prescriptions] (
    [IdPrescription] int NOT NULL IDENTITY,
    [Date] datetime2 NOT NULL,
    [DueDate] datetime2 NOT NULL,
    [IdPatient] int NOT NULL,
    [IdDoctor] int NOT NULL,
    CONSTRAINT [PK_Prescriptions] PRIMARY KEY ([IdPrescription]),
    CONSTRAINT [FK_Prescriptions_Doctors_IdDoctor] FOREIGN KEY ([IdDoctor]) REFERENCES [Doctors] ([IdDoctor]) ON DELETE CASCADE,
    CONSTRAINT [FK_Prescriptions_Patients_IdPatient] FOREIGN KEY ([IdPatient]) REFERENCES [Patients] ([IdPatient]) ON DELETE CASCADE
);
GO

CREATE TABLE [Prescription_Medicaments] (
    [IdMedicament] int NOT NULL,
    [IdPrescription] int NOT NULL,
    [Dose] int NULL,
    [Details] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Prescription_Medicaments] PRIMARY KEY ([IdMedicament], [IdPrescription]),
    CONSTRAINT [FK_Prescription_Medicaments_Medicaments_IdMedicament] FOREIGN KEY ([IdMedicament]) REFERENCES [Medicaments] ([IdMedicament]) ON DELETE CASCADE,
    CONSTRAINT [FK_Prescription_Medicaments_Prescriptions_IdPrescription] FOREIGN KEY ([IdPrescription]) REFERENCES [Prescriptions] ([IdPrescription]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IdDoctor', N'Email', N'FirstName', N'LastName') AND [object_id] = OBJECT_ID(N'[Doctors]'))
    SET IDENTITY_INSERT [Doctors] ON;
INSERT INTO [Doctors] ([IdDoctor], [Email], [FirstName], [LastName])
VALUES (1, N'hansolo@gmail.com', N'Han', N'Solo'),
(2, N'recovery@gmail.com', N'Rec', N'Covery');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IdDoctor', N'Email', N'FirstName', N'LastName') AND [object_id] = OBJECT_ID(N'[Doctors]'))
    SET IDENTITY_INSERT [Doctors] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IdMedicament', N'Description', N'Name', N'Type') AND [object_id] = OBJECT_ID(N'[Medicaments]'))
    SET IDENTITY_INSERT [Medicaments] ON;
INSERT INTO [Medicaments] ([IdMedicament], [Description], [Name], [Type])
VALUES (1, N'abdde', N'Ibuprom', N'Painkiller'),
(2, N'qwerty', N'Macromax', N'Antibiotic');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IdMedicament', N'Description', N'Name', N'Type') AND [object_id] = OBJECT_ID(N'[Medicaments]'))
    SET IDENTITY_INSERT [Medicaments] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IdPatient', N'BirthDate', N'FirstName', N'LastName') AND [object_id] = OBJECT_ID(N'[Patients]'))
    SET IDENTITY_INSERT [Patients] ON;
INSERT INTO [Patients] ([IdPatient], [BirthDate], [FirstName], [LastName])
VALUES (1, '1999-02-14T00:00:00.0000000', N'Gizmo', N'Big'),
(2, '2000-08-18T00:00:00.0000000', N'Jinx', N'Ryoma');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IdPatient', N'BirthDate', N'FirstName', N'LastName') AND [object_id] = OBJECT_ID(N'[Patients]'))
    SET IDENTITY_INSERT [Patients] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IdPrescription', N'Date', N'DueDate', N'IdDoctor', N'IdPatient') AND [object_id] = OBJECT_ID(N'[Prescriptions]'))
    SET IDENTITY_INSERT [Prescriptions] ON;
INSERT INTO [Prescriptions] ([IdPrescription], [Date], [DueDate], [IdDoctor], [IdPatient])
VALUES (1, '2018-06-28T00:00:00.0000000', '2018-12-28T00:00:00.0000000', 1, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IdPrescription', N'Date', N'DueDate', N'IdDoctor', N'IdPatient') AND [object_id] = OBJECT_ID(N'[Prescriptions]'))
    SET IDENTITY_INSERT [Prescriptions] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IdPrescription', N'Date', N'DueDate', N'IdDoctor', N'IdPatient') AND [object_id] = OBJECT_ID(N'[Prescriptions]'))
    SET IDENTITY_INSERT [Prescriptions] ON;
INSERT INTO [Prescriptions] ([IdPrescription], [Date], [DueDate], [IdDoctor], [IdPatient])
VALUES (2, '2020-03-26T00:00:00.0000000', '2020-10-16T00:00:00.0000000', 1, 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IdPrescription', N'Date', N'DueDate', N'IdDoctor', N'IdPatient') AND [object_id] = OBJECT_ID(N'[Prescriptions]'))
    SET IDENTITY_INSERT [Prescriptions] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IdMedicament', N'IdPrescription', N'Details', N'Dose') AND [object_id] = OBJECT_ID(N'[Prescription_Medicaments]'))
    SET IDENTITY_INSERT [Prescription_Medicaments] ON;
INSERT INTO [Prescription_Medicaments] ([IdMedicament], [IdPrescription], [Details], [Dose])
VALUES (1, 1, N'ABC', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IdMedicament', N'IdPrescription', N'Details', N'Dose') AND [object_id] = OBJECT_ID(N'[Prescription_Medicaments]'))
    SET IDENTITY_INSERT [Prescription_Medicaments] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IdMedicament', N'IdPrescription', N'Details', N'Dose') AND [object_id] = OBJECT_ID(N'[Prescription_Medicaments]'))
    SET IDENTITY_INSERT [Prescription_Medicaments] ON;
INSERT INTO [Prescription_Medicaments] ([IdMedicament], [IdPrescription], [Details], [Dose])
VALUES (1, 2, N'DEF', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IdMedicament', N'IdPrescription', N'Details', N'Dose') AND [object_id] = OBJECT_ID(N'[Prescription_Medicaments]'))
    SET IDENTITY_INSERT [Prescription_Medicaments] OFF;
GO

CREATE INDEX [IX_Prescription_Medicaments_IdPrescription] ON [Prescription_Medicaments] ([IdPrescription]);
GO

CREATE INDEX [IX_Prescriptions_IdDoctor] ON [Prescriptions] ([IdDoctor]);
GO

CREATE INDEX [IX_Prescriptions_IdPatient] ON [Prescriptions] ([IdPatient]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210506185923_AddedAllTables', N'5.0.5');
GO

COMMIT;
GO

