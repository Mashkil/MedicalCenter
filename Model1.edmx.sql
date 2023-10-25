
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/25/2023 22:22:48
-- Generated from EDMX file: C:\Users\user\source\repos\MedicalCenter\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [medcentr];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Logins_an__Id_ad__6477ECF3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Logins_and_passwords] DROP CONSTRAINT [FK__Logins_an__Id_ad__6477ECF3];
GO
IF OBJECT_ID(N'[dbo].[FK__Logins_an__Id_do__6383C8BA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Logins_and_passwords] DROP CONSTRAINT [FK__Logins_an__Id_do__6383C8BA];
GO
IF OBJECT_ID(N'[dbo].[FK__Services__Id_doc__73BA3083]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Services] DROP CONSTRAINT [FK__Services__Id_doc__73BA3083];
GO
IF OBJECT_ID(N'[dbo].[FK__Time__Id_service__74AE54BC]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Time] DROP CONSTRAINT [FK__Time__Id_service__74AE54BC];
GO
IF OBJECT_ID(N'[dbo].[FK__Visits__Id_servi__75A278F5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Visits] DROP CONSTRAINT [FK__Visits__Id_servi__75A278F5];
GO
IF OBJECT_ID(N'[dbo].[FK_Date_Admins]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Date] DROP CONSTRAINT [FK_Date_Admins];
GO
IF OBJECT_ID(N'[dbo].[FK_Time_Admins]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Time] DROP CONSTRAINT [FK_Time_Admins];
GO
IF OBJECT_ID(N'[dbo].[FK_Time_Date]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Time] DROP CONSTRAINT [FK_Time_Date];
GO
IF OBJECT_ID(N'[dbo].[FK_Time_Doctors]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Time] DROP CONSTRAINT [FK_Time_Doctors];
GO
IF OBJECT_ID(N'[dbo].[FK_Time_Patients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Time] DROP CONSTRAINT [FK_Time_Patients];
GO
IF OBJECT_ID(N'[dbo].[FK_Time_Visits]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Time] DROP CONSTRAINT [FK_Time_Visits];
GO
IF OBJECT_ID(N'[dbo].[FK_Visits_Doctors]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Visits] DROP CONSTRAINT [FK_Visits_Doctors];
GO
IF OBJECT_ID(N'[dbo].[FK_Visits_Patients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Visits] DROP CONSTRAINT [FK_Visits_Patients];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Admins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Admins];
GO
IF OBJECT_ID(N'[dbo].[Date]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Date];
GO
IF OBJECT_ID(N'[dbo].[Doctors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Doctors];
GO
IF OBJECT_ID(N'[dbo].[Logins_and_passwords]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Logins_and_passwords];
GO
IF OBJECT_ID(N'[dbo].[Patients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Patients];
GO
IF OBJECT_ID(N'[dbo].[Services]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Services];
GO
IF OBJECT_ID(N'[dbo].[Time]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Time];
GO
IF OBJECT_ID(N'[dbo].[Visits]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Visits];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Admins'
CREATE TABLE [dbo].[Admins] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Firstname] nvarchar(20)  NOT NULL,
    [Lastname] nvarchar(20)  NOT NULL,
    [Patronimyc] nvarchar(20)  NOT NULL,
    [Age] int  NOT NULL,
    [Salary] decimal(19,4)  NOT NULL,
    [Education] nvarchar(2000)  NOT NULL
);
GO

-- Creating table 'Date'
CREATE TABLE [dbo].[Date] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date1] datetime  NOT NULL,
    [Type_of_day] nvarchar(11)  NULL,
    [adminId] int  NULL,
    [Date_in_text] nvarchar(15)  NULL
);
GO

-- Creating table 'Doctors'
CREATE TABLE [dbo].[Doctors] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Firstname] nvarchar(30)  NOT NULL,
    [Lastname] nvarchar(30)  NOT NULL,
    [Patronymic] nvarchar(30)  NOT NULL,
    [Age] int  NOT NULL,
    [Salary] decimal(19,4)  NOT NULL,
    [Experience] int  NOT NULL,
    [Education] nvarchar(255)  NOT NULL,
    [Position] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Logins_and_passwords'
CREATE TABLE [dbo].[Logins_and_passwords] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(20)  NOT NULL,
    [Password] nvarchar(20)  NOT NULL,
    [Admin_or_doctor] int  NULL,
    [Id_doc] int  NULL,
    [Id_admin] int  NULL
);
GO

-- Creating table 'Patients'
CREATE TABLE [dbo].[Patients] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Firstname] nvarchar(30)  NOT NULL,
    [Lastname] nvarchar(30)  NOT NULL,
    [Patronymic] nvarchar(30)  NULL,
    [Phone] varchar(20)  NOT NULL,
    [Chronic_deseases] nvarchar(200)  NULL,
    [Date_of_birth] datetime  NULL,
    [Series_and_number_of_pass] nvarchar(12)  NULL,
    [Date_of_birth_in_text] nvarchar(12)  NULL
);
GO

-- Creating table 'Services'
CREATE TABLE [dbo].[Services] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name_of_service] nvarchar(25)  NULL,
    [Id_doc] int  NULL,
    [Cost] int  NULL
);
GO

-- Creating table 'Time'
CREATE TABLE [dbo].[Time] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Time1] time  NOT NULL,
    [DoctorId] int  NOT NULL,
    [PatientId] int  NOT NULL,
    [DateId] int  NOT NULL,
    [VisitId] int  NULL,
    [Time_in_text] nvarchar(20)  NULL,
    [Id_admin] int  NULL,
    [Id_service] int  NULL
);
GO

-- Creating table 'Visits'
CREATE TABLE [dbo].[Visits] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DoctorId] int  NOT NULL,
    [PatientId] int  NOT NULL,
    [Appointment] nvarchar(max)  NULL,
    [Complaint] nvarchar(max)  NULL,
    [Therapy] nvarchar(max)  NULL,
    [Id_service] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Admins'
ALTER TABLE [dbo].[Admins]
ADD CONSTRAINT [PK_Admins]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Date'
ALTER TABLE [dbo].[Date]
ADD CONSTRAINT [PK_Date]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Doctors'
ALTER TABLE [dbo].[Doctors]
ADD CONSTRAINT [PK_Doctors]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Logins_and_passwords'
ALTER TABLE [dbo].[Logins_and_passwords]
ADD CONSTRAINT [PK_Logins_and_passwords]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Patients'
ALTER TABLE [dbo].[Patients]
ADD CONSTRAINT [PK_Patients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Services'
ALTER TABLE [dbo].[Services]
ADD CONSTRAINT [PK_Services]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Time'
ALTER TABLE [dbo].[Time]
ADD CONSTRAINT [PK_Time]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [PK_Visits]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Id_admin] in table 'Logins_and_passwords'
ALTER TABLE [dbo].[Logins_and_passwords]
ADD CONSTRAINT [FK__Logins_an__Id_ad__6477ECF3]
    FOREIGN KEY ([Id_admin])
    REFERENCES [dbo].[Admins]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Logins_an__Id_ad__6477ECF3'
CREATE INDEX [IX_FK__Logins_an__Id_ad__6477ECF3]
ON [dbo].[Logins_and_passwords]
    ([Id_admin]);
GO

-- Creating foreign key on [adminId] in table 'Date'
ALTER TABLE [dbo].[Date]
ADD CONSTRAINT [FK_Date_Admins]
    FOREIGN KEY ([adminId])
    REFERENCES [dbo].[Admins]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Date_Admins'
CREATE INDEX [IX_FK_Date_Admins]
ON [dbo].[Date]
    ([adminId]);
GO

-- Creating foreign key on [Id_admin] in table 'Time'
ALTER TABLE [dbo].[Time]
ADD CONSTRAINT [FK_Time_Admins]
    FOREIGN KEY ([Id_admin])
    REFERENCES [dbo].[Admins]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Time_Admins'
CREATE INDEX [IX_FK_Time_Admins]
ON [dbo].[Time]
    ([Id_admin]);
GO

-- Creating foreign key on [DateId] in table 'Time'
ALTER TABLE [dbo].[Time]
ADD CONSTRAINT [FK_Time_Date]
    FOREIGN KEY ([DateId])
    REFERENCES [dbo].[Date]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Time_Date'
CREATE INDEX [IX_FK_Time_Date]
ON [dbo].[Time]
    ([DateId]);
GO

-- Creating foreign key on [Id_doc] in table 'Logins_and_passwords'
ALTER TABLE [dbo].[Logins_and_passwords]
ADD CONSTRAINT [FK__Logins_an__Id_do__6383C8BA]
    FOREIGN KEY ([Id_doc])
    REFERENCES [dbo].[Doctors]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Logins_an__Id_do__6383C8BA'
CREATE INDEX [IX_FK__Logins_an__Id_do__6383C8BA]
ON [dbo].[Logins_and_passwords]
    ([Id_doc]);
GO

-- Creating foreign key on [Id_doc] in table 'Services'
ALTER TABLE [dbo].[Services]
ADD CONSTRAINT [FK__Services__Id_doc__73BA3083]
    FOREIGN KEY ([Id_doc])
    REFERENCES [dbo].[Doctors]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Services__Id_doc__73BA3083'
CREATE INDEX [IX_FK__Services__Id_doc__73BA3083]
ON [dbo].[Services]
    ([Id_doc]);
GO

-- Creating foreign key on [DoctorId] in table 'Time'
ALTER TABLE [dbo].[Time]
ADD CONSTRAINT [FK_Time_Doctors]
    FOREIGN KEY ([DoctorId])
    REFERENCES [dbo].[Doctors]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Time_Doctors'
CREATE INDEX [IX_FK_Time_Doctors]
ON [dbo].[Time]
    ([DoctorId]);
GO

-- Creating foreign key on [DoctorId] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_Visits_Doctors]
    FOREIGN KEY ([DoctorId])
    REFERENCES [dbo].[Doctors]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Visits_Doctors'
CREATE INDEX [IX_FK_Visits_Doctors]
ON [dbo].[Visits]
    ([DoctorId]);
GO

-- Creating foreign key on [PatientId] in table 'Time'
ALTER TABLE [dbo].[Time]
ADD CONSTRAINT [FK_Time_Patients]
    FOREIGN KEY ([PatientId])
    REFERENCES [dbo].[Patients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Time_Patients'
CREATE INDEX [IX_FK_Time_Patients]
ON [dbo].[Time]
    ([PatientId]);
GO

-- Creating foreign key on [PatientId] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_Visits_Patients]
    FOREIGN KEY ([PatientId])
    REFERENCES [dbo].[Patients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Visits_Patients'
CREATE INDEX [IX_FK_Visits_Patients]
ON [dbo].[Visits]
    ([PatientId]);
GO

-- Creating foreign key on [Id_service] in table 'Time'
ALTER TABLE [dbo].[Time]
ADD CONSTRAINT [FK__Time__Id_service__74AE54BC]
    FOREIGN KEY ([Id_service])
    REFERENCES [dbo].[Services]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Time__Id_service__74AE54BC'
CREATE INDEX [IX_FK__Time__Id_service__74AE54BC]
ON [dbo].[Time]
    ([Id_service]);
GO

-- Creating foreign key on [Id_service] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK__Visits__Id_servi__75A278F5]
    FOREIGN KEY ([Id_service])
    REFERENCES [dbo].[Services]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Visits__Id_servi__75A278F5'
CREATE INDEX [IX_FK__Visits__Id_servi__75A278F5]
ON [dbo].[Visits]
    ([Id_service]);
GO

-- Creating foreign key on [VisitId] in table 'Time'
ALTER TABLE [dbo].[Time]
ADD CONSTRAINT [FK_Time_Visits]
    FOREIGN KEY ([VisitId])
    REFERENCES [dbo].[Visits]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Time_Visits'
CREATE INDEX [IX_FK_Time_Visits]
ON [dbo].[Time]
    ([VisitId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------