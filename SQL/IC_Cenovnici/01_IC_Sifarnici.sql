/* =====================================================================
   ZAH-008 Izracun cena - 01: sifarnici izracuna cena
   Baza: Jela_Test. Kompatibilno sa SQL Server 2008 R2.

   Izracun cena ima SVOJE sifarnike i ne koristi postojece tabele
   Program, Kolekcije i KolekcijePripadnost (njih koriste planiranje i
   pakovanje i ova skripta ih ne dira):
     IC_Program               - programi (NazivRobneGrupe iz jela_products)
     IC_Kolekcija             - kolekcije programa (NazivGrupe iz jela_products)
     IC_KolekcijaPripadnost   - rucna dodela maticnog podatka kolekciji;
                                ima prednost nad dodelom po nazivu iz jela_products
   Na bazi gde su IC_ tabele vec pravljene sa starim sifarnicima, posle ove
   skripte pokrenuti 08_Prelazak_na_IC_sifarnike.sql.
   Skripta se sme pokretati vise puta.
   ===================================================================== */
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET NOCOUNT ON;
GO

IF OBJECT_ID('dbo.IC_Program', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.IC_Program (
        ID    int IDENTITY(1,1) NOT NULL CONSTRAINT PK_IC_Program PRIMARY KEY,
        Naziv nvarchar(100) NOT NULL CONSTRAINT UQ_IC_Program_Naziv UNIQUE
    );
    PRINT 'Napravljena tabela dbo.IC_Program.';
END
GO

IF OBJECT_ID('dbo.IC_Kolekcija', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.IC_Kolekcija (
        ID        int IDENTITY(1,1) NOT NULL CONSTRAINT PK_IC_Kolekcija PRIMARY KEY,
        Naziv     nvarchar(100) NOT NULL,
        ProgramID int NOT NULL CONSTRAINT FK_IC_Kolekcija_Program REFERENCES dbo.IC_Program(ID),
        CONSTRAINT UQ_IC_Kolekcija UNIQUE (ProgramID, Naziv)
    );
    PRINT 'Napravljena tabela dbo.IC_Kolekcija.';
END
GO

IF OBJECT_ID('dbo.IC_KolekcijaPripadnost', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.IC_KolekcijaPripadnost (
        LargoProizvodID int NOT NULL CONSTRAINT PK_IC_KolekcijaPripadnost PRIMARY KEY,   -- maticni podatak
        KolekcijaID     int NOT NULL CONSTRAINT FK_IC_Pripadnost_Kolekcija REFERENCES dbo.IC_Kolekcija(ID),
        Korisnik        nvarchar(50) NULL,
        Datum           datetime2(0) NOT NULL CONSTRAINT DF_IC_Pripadnost_Datum DEFAULT (SYSDATETIME())
    );
    CREATE INDEX IX_IC_Pripadnost_Kolekcija ON dbo.IC_KolekcijaPripadnost (KolekcijaID);
    PRINT 'Napravljena tabela dbo.IC_KolekcijaPripadnost.';
END
GO

-- evidencija jednokratnih prelaza (08)
IF OBJECT_ID('dbo.IC_Migracija', 'U') IS NULL
CREATE TABLE dbo.IC_Migracija (
    Naziv varchar(50) NOT NULL CONSTRAINT PK_IC_Migracija PRIMARY KEY,
    Datum datetime2(0) NOT NULL CONSTRAINT DF_IC_Migracija_Datum DEFAULT (SYSDATETIME())
);
GO
