/* =====================================================================
   ZAH-008 Izracun cena - 02: nove IC_ tabele i tipovi (TVP)
   Baza: Jela_Test. Kompatibilno sa SQL Server 2008 R2.
   Pokrenuti posle 01_IC_Sifarnici.sql.
   Skripta se sme pokretati vise puta (pravi samo ono sto ne postoji).

   Dodato u odnosu na specifikaciju (oznaceno sa "-- dodato"):
     IC_Cenovnik.DatumPreuzimanjaIzvora  - za obavestenje "Opsti se promenio"
     IC_CenovnikCena.VPIzBaze            - Code bez cene u Excelu (cena iz jela_products)
     IC_CenovnikStavka.BezTroska         - red bez vazeceg troska pri poslednjem azuriranju
     CHECK ogranicenja (nivo parametra, period, kurs), indeksi,
     jedinstvena glavna slika po Code, tipovi tabela za procedure.
   ===================================================================== */
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

/* ---------- IC_Cenovnik: zaglavlje ---------- */
IF OBJECT_ID('dbo.IC_Cenovnik', 'U') IS NULL
CREATE TABLE dbo.IC_Cenovnik (
    CenovnikID             int IDENTITY(1,1) CONSTRAINT PK_IC_Cenovnik PRIMARY KEY,
    Naziv                  nvarchar(100) NOT NULL CONSTRAINT UQ_IC_Cenovnik_Naziv UNIQUE,
    Vrsta                  varchar(10)  NOT NULL CONSTRAINT CK_IC_Cenovnik_Vrsta CHECK (Vrsta IN ('Opsti', 'Radni', 'Vazeci')),
    Valuta                 char(3)      NOT NULL CONSTRAINT CK_IC_Cenovnik_Valuta CHECK (Valuta IN ('RSD', 'EUR')),
    Kurs                   decimal(9,2) NOT NULL CONSTRAINT CK_IC_Cenovnik_Kurs CHECK (Kurs > 0),          -- dodato CHECK
    PDV                    decimal(5,4) NOT NULL CONSTRAINT DF_IC_Cenovnik_PDV DEFAULT (0.20),
    IzvorniCenovnikID      int NULL CONSTRAINT FK_IC_Cenovnik_Izvor REFERENCES dbo.IC_Cenovnik(CenovnikID),
    PaSifra                int NULL,          -- Partnerji.PaSifra; NULL = opsti
    VaziOd                 date NULL,
    VaziDo                 date NULL,         -- NULL = do daljnjeg
    DatumAzuriranjaTroska  datetime2(0) NULL,
    DatumProglasenjaTroska datetime2(0) NULL,
    DatumPreuzimanjaIzvora datetime2(0) NULL, -- dodato: kada je radna kopija poslednji put uskladjena sa izvorom
    Kreirao                nvarchar(50) NOT NULL,
    DatumKreiranja         datetime2(0) NOT NULL CONSTRAINT DF_IC_Cenovnik_DK DEFAULT (SYSDATETIME()),
    Izmenio                nvarchar(50) NULL,
    DatumIzmene            datetime2(0) NULL,
    CONSTRAINT CK_IC_Cenovnik_Period CHECK (VaziDo IS NULL OR VaziOd IS NULL OR VaziDo >= VaziOd),     -- dodato
    CONSTRAINT CK_IC_Cenovnik_VazeciOd CHECK (Vrsta <> 'Vazeci' OR VaziOd IS NOT NULL)                -- dodato
);
GO

/* ---------- IC_CenovnikParametar: rabat i vazeca akcija po nivou ---------- */
IF OBJECT_ID('dbo.IC_CenovnikParametar', 'U') IS NULL
CREATE TABLE dbo.IC_CenovnikParametar (
    ParametarID int IDENTITY(1,1) CONSTRAINT PK_IC_CenovnikParametar PRIMARY KEY,
    CenovnikID  int NOT NULL CONSTRAINT FK_IC_Param_Cenovnik REFERENCES dbo.IC_Cenovnik(CenovnikID) ON DELETE CASCADE,
    Nivo        varchar(10) NOT NULL CONSTRAINT CK_IC_Param_Nivo CHECK (Nivo IN ('Cenovnik', 'Program', 'Kolekcija', 'Code')),
    ProgramID   int NULL,                     -- FK_IC_Param_Program na kraju skripte
    KolekcijaID int NULL,                     -- FK_IC_Param_Kolekcija na kraju skripte
    Code        nvarchar(30) NULL,
    Rabat       decimal(9,4) NULL,            -- NULL = nasledjuje visi nivo
    Akcija      decimal(9,4) NULL,            -- vazeca akcija; NULL = nasledjuje
    CONSTRAINT UQ_IC_Param UNIQUE (CenovnikID, Nivo, ProgramID, KolekcijaID, Code),
    -- dodato: popunjen je tacno onaj kljuc koji odgovara nivou
    CONSTRAINT CK_IC_Param_Kljuc CHECK (
           (Nivo = 'Cenovnik'  AND ProgramID IS NULL     AND KolekcijaID IS NULL     AND Code IS NULL)
        OR (Nivo = 'Program'   AND ProgramID IS NOT NULL AND KolekcijaID IS NULL     AND Code IS NULL)
        OR (Nivo = 'Kolekcija' AND ProgramID IS NULL     AND KolekcijaID IS NOT NULL AND Code IS NULL)
        OR (Nivo = 'Code'      AND ProgramID IS NULL     AND KolekcijaID IS NULL     AND Code IS NOT NULL)),
    CONSTRAINT CK_IC_Param_Opseg CHECK ((Rabat IS NULL OR Rabat BETWEEN 0 AND 1)
                                    AND (Akcija IS NULL OR Akcija BETWEEN 0 AND 1))                 -- dodato
);
GO

/* ---------- IC_CenovnikAkcija: akcije za poredjenje ---------- */
IF OBJECT_ID('dbo.IC_CenovnikAkcija', 'U') IS NULL
CREATE TABLE dbo.IC_CenovnikAkcija (          -- prazna lista = nasledjuje
    ParametarID int NOT NULL CONSTRAINT FK_IC_Akcija_Param REFERENCES dbo.IC_CenovnikParametar(ParametarID) ON DELETE CASCADE,
    Redosled    tinyint NOT NULL,
    Procenat    decimal(9,4) NOT NULL CONSTRAINT CK_IC_Akcija_Procenat CHECK (Procenat BETWEEN 0 AND 1), -- dodato CHECK
    CONSTRAINT PK_IC_CenovnikAkcija PRIMARY KEY (ParametarID, Redosled)
);
GO

/* ---------- IC_CenovnikCena: VP po Code ---------- */
IF OBJECT_ID('dbo.IC_CenovnikCena', 'U') IS NULL
CREATE TABLE dbo.IC_CenovnikCena (
    CenovnikID  int NOT NULL CONSTRAINT FK_IC_Cena_Cenovnik REFERENCES dbo.IC_Cenovnik(CenovnikID) ON DELETE CASCADE,
    Code        nvarchar(30) NOT NULL,
    VPAktuelna  decimal(18,2) NULL,
    PovecanjeVP decimal(9,4) NOT NULL CONSTRAINT DF_IC_Cena_Pov DEFAULT (0),
    VP          decimal(18,2) NOT NULL,
    VPIzBaze    bit NOT NULL CONSTRAINT DF_IC_Cena_VPIzBaze DEFAULT (0),   -- dodato: 1 = cena nije bila u Excelu, uzeta iz jela_products
    Izmenio     nvarchar(50) NULL,
    DatumIzmene datetime2(0) NULL,
    CONSTRAINT PK_IC_CenovnikCena PRIMARY KEY (CenovnikID, Code)
);
GO

/* ---------- IC_CenovnikStavka: red = maticni podatak ---------- */
IF OBJECT_ID('dbo.IC_CenovnikStavka', 'U') IS NULL
CREATE TABLE dbo.IC_CenovnikStavka (
    StavkaID            int IDENTITY(1,1) CONSTRAINT PK_IC_CenovnikStavka PRIMARY KEY,
    CenovnikID          int NOT NULL,
    LargoProizvodID     int NOT NULL,
    SifraProizvodaLargo nvarchar(30) NOT NULL,
    Code                nvarchar(30) NOT NULL,
    ProgramID           int NOT NULL,         -- FK_IC_Stavka_Program na kraju skripte
    KolekcijaID         int NOT NULL,         -- FK_IC_Stavka_Kolekcija na kraju skripte
    NazivProizvoda      nvarchar(50) NULL,
    TrosakStari         decimal(18,2) NULL,
    TrosakNovi          decimal(18,2) NULL,
    DatumTroskaStari    datetime NULL,
    DatumTroskaNovi     datetime NULL,
    MarzaStara          decimal(9,4) NULL,
    BezTroska           bit NOT NULL CONSTRAINT DF_IC_Stavka_BezTroska DEFAULT (0), -- dodato: pri poslednjem azuriranju nije nadjen vazeci trosak (red sivo)
    CONSTRAINT FK_IC_Stavka_Cena FOREIGN KEY (CenovnikID, Code)
        REFERENCES dbo.IC_CenovnikCena(CenovnikID, Code) ON DELETE CASCADE,
    CONSTRAINT UQ_IC_Stavka UNIQUE (CenovnikID, LargoProizvodID)
);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_IC_Stavka_Code' AND object_id = OBJECT_ID('dbo.IC_CenovnikStavka'))
    CREATE INDEX IX_IC_Stavka_Code ON dbo.IC_CenovnikStavka (CenovnikID, Code);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_IC_Stavka_Kolekcija' AND object_id = OBJECT_ID('dbo.IC_CenovnikStavka'))
    CREATE INDEX IX_IC_Stavka_Kolekcija ON dbo.IC_CenovnikStavka (CenovnikID, ProgramID, KolekcijaID);
GO

/* ---------- IC_CenovnikVazecaCena: zamrznuto pri proglasenju ---------- */
IF OBJECT_ID('dbo.IC_CenovnikVazecaCena', 'U') IS NULL
CREATE TABLE dbo.IC_CenovnikVazecaCena (
    CenovnikID      int NOT NULL CONSTRAINT FK_IC_Vazeca_Cenovnik REFERENCES dbo.IC_Cenovnik(CenovnikID),
    LargoProizvodID int NOT NULL,
    Code            nvarchar(30) NOT NULL,
    TP              decimal(18,2) NULL,
    VP              decimal(18,2) NOT NULL,
    VPsaPDV         decimal(18,2) NULL,
    Marza           decimal(9,4) NULL,
    Rabat           decimal(9,4) NOT NULL,
    CenaPosleRabata decimal(18,2) NOT NULL,
    JelaMarza       decimal(9,4) NULL,
    Akcija          decimal(9,4) NULL,
    CenaAkcija      decimal(18,2) NULL,
    JelaMarzaAkcija decimal(9,4) NULL,
    -- samo vazeca akcija; akcije za poredjenje se ne zamrzavaju
    CONSTRAINT PK_IC_CenovnikVazecaCena PRIMARY KEY (CenovnikID, LargoProizvodID)
);
GO

/* ---------- IC_SlikaProizvoda: putanje slika po Code ---------- */
IF OBJECT_ID('dbo.IC_SlikaProizvoda', 'U') IS NULL
CREATE TABLE dbo.IC_SlikaProizvoda (
    SlikaID          int IDENTITY(1,1) CONSTRAINT PK_IC_SlikaProizvoda PRIMARY KEY,
    Code             nvarchar(30)  NOT NULL,
    Putanja          nvarchar(400) NOT NULL,     -- relativno prema korenskom folderu
    Glavna           bit NOT NULL CONSTRAINT DF_IC_Slika_Glavna DEFAULT (0),
    Izvor            varchar(10) NOT NULL CONSTRAINT CK_IC_Slika_Izvor CHECK (Izvor IN ('Automatski', 'Rucno')),
    DatumPovezivanja datetime2(0) NOT NULL CONSTRAINT DF_IC_Slika_Datum DEFAULT (SYSDATETIME()),
    CONSTRAINT UQ_IC_Slika UNIQUE (Code, Putanja)
);
GO

-- dodato: najvise jedna glavna slika po Code (procedure to ionako cuvaju)
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_IC_Slika_Glavna' AND object_id = OBJECT_ID('dbo.IC_SlikaProizvoda'))
BEGIN TRY
    EXEC (N'CREATE UNIQUE INDEX UX_IC_Slika_Glavna ON dbo.IC_SlikaProizvoda (Code) WHERE Glavna = CONVERT(bit, 1)');
END TRY
BEGIN CATCH
    PRINT 'PAZNJA: UX_IC_Slika_Glavna nije napravljen (' + ERROR_MESSAGE() + '). Nije obavezan.';
END CATCH
GO

/* ---------- IC_CenovnikPresek: evidencija izvoza u Excel ---------- */
IF OBJECT_ID('dbo.IC_CenovnikPresek', 'U') IS NULL
CREATE TABLE dbo.IC_CenovnikPresek (
    PresekID   int IDENTITY(1,1) CONSTRAINT PK_IC_CenovnikPresek PRIMARY KEY,
    CenovnikID int NOT NULL CONSTRAINT FK_IC_Presek_Cenovnik REFERENCES dbo.IC_Cenovnik(CenovnikID),
    Fajl       nvarchar(400) NOT NULL,
    Datum      datetime2(0) NOT NULL CONSTRAINT DF_IC_Presek_Datum DEFAULT (SYSDATETIME()),
    Korisnik   nvarchar(50) NOT NULL
);
GO

/* =====================================================================
   FK prema sifarnicima izracuna cena IC_Program i IC_Kolekcija (skripta 01).
   Ako FK vec postoji, ali pokazuje na staru tabelu Program/Kolekcije
   (baza pravljena pre 30.10.), ne dira se: pokrenuti
   08_Prelazak_na_IC_sifarnike.sql, koja prevezuje podatke i FK.
   ===================================================================== */
DECLARE @Fk TABLE (Rb int, Ime sysname, Tabela sysname, Kolona sysname, Cilj sysname);
INSERT INTO @Fk VALUES
    (1, 'FK_IC_Param_Program',    'IC_CenovnikParametar', 'ProgramID',   'IC_Program'),
    (2, 'FK_IC_Param_Kolekcija',  'IC_CenovnikParametar', 'KolekcijaID', 'IC_Kolekcija'),
    (3, 'FK_IC_Stavka_Program',   'IC_CenovnikStavka',    'ProgramID',   'IC_Program'),
    (4, 'FK_IC_Stavka_Kolekcija', 'IC_CenovnikStavka',    'KolekcijaID', 'IC_Kolekcija');

DECLARE @Rb int = 1, @Ime sysname, @Tabela sysname, @Kolona sysname, @Cilj sysname, @Sql nvarchar(max);

WHILE @Rb <= 4
BEGIN
    SELECT @Ime = Ime, @Tabela = Tabela, @Kolona = Kolona, @Cilj = Cilj FROM @Fk WHERE Rb = @Rb;

    IF OBJECT_ID('dbo.' + @Ime, 'F') IS NULL
    BEGIN
        IF OBJECT_ID('dbo.' + @Cilj, 'U') IS NOT NULL
        BEGIN
            SET @Sql = N'ALTER TABLE dbo.' + QUOTENAME(@Tabela) + N' ADD CONSTRAINT ' + QUOTENAME(@Ime)
                     + N' FOREIGN KEY (' + QUOTENAME(@Kolona) + N') REFERENCES dbo.' + QUOTENAME(@Cilj) + N'(ID)';
            EXEC (@Sql);
            PRINT 'Dodat ' + @Ime + '.';
        END
        ELSE
            PRINT 'PAZNJA: ' + @Ime + ' nije dodat - nema tabele dbo.' + @Cilj + ' (pokrenuti 01_IC_Sifarnici.sql).';
    END
    ELSE IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys
                        WHERE name = @Ime AND referenced_object_id = OBJECT_ID('dbo.' + @Cilj))
        PRINT 'PAZNJA: ' + @Ime + ' pokazuje na staru tabelu - pokrenuti 08_Prelazak_na_IC_sifarnike.sql.';

    SET @Rb = @Rb + 1;
END
GO

/* =====================================================================
   Tipovi tabela (table-valued parametri) za procedure iz C#
   ===================================================================== */

-- VP po Code iz grida (IC_SacuvajCene)
IF TYPE_ID('dbo.IC_CenaTip') IS NULL
CREATE TYPE dbo.IC_CenaTip AS TABLE (
    Code        nvarchar(30)  NOT NULL PRIMARY KEY,
    VPAktuelna  decimal(18,2) NULL,
    PovecanjeVP decimal(9,4)  NOT NULL,
    VP          decimal(18,2) NOT NULL
);
GO

-- rabat i vazeca akcija po nivou (IC_UpisiUvoz, IC_SacuvajParametre)
-- RedBr je lokalni kljuc reda, da se uz njega vezu akcije za poredjenje.
-- PostaviRabat / PostaviAkciju: 1 = upisi vrednost (NULL = nasledjuje), 0 = ne diraj.
IF TYPE_ID('dbo.IC_ParametarTip') IS NULL
CREATE TYPE dbo.IC_ParametarTip AS TABLE (
    RedBr         int          NOT NULL PRIMARY KEY,
    Nivo          varchar(10)  NOT NULL,
    ProgramID     int          NULL,
    KolekcijaID   int          NULL,
    Code          nvarchar(30) NULL,
    Rabat         decimal(9,4) NULL,
    Akcija        decimal(9,4) NULL,
    PostaviRabat  bit          NOT NULL,
    PostaviAkciju bit          NOT NULL
);
GO

-- akcije za poredjenje, vezane za IC_ParametarTip.RedBr (IC_UpisiUvoz)
IF TYPE_ID('dbo.IC_AkcijaTip') IS NULL
CREATE TYPE dbo.IC_AkcijaTip AS TABLE (
    RedBr    int          NOT NULL,
    Redosled tinyint      NOT NULL,
    Procenat decimal(9,4) NOT NULL,
    PRIMARY KEY (RedBr, Redosled)
);
GO

-- cene iz Excela po Code (IC_UpisiUvoz)
IF TYPE_ID('dbo.IC_UvozCenaTip') IS NULL
CREATE TYPE dbo.IC_UvozCenaTip AS TABLE (
    Code nvarchar(30)  NOT NULL PRIMARY KEY,
    VP   decimal(18,2) NOT NULL
);
GO

-- redovi iz Excela po maticnom podatku (IC_UpisiUvoz)
IF TYPE_ID('dbo.IC_UvozStavkaTip') IS NULL
CREATE TYPE dbo.IC_UvozStavkaTip AS TABLE (
    LargoProizvodID int           NOT NULL PRIMARY KEY,
    Code            nvarchar(30)  NOT NULL,
    TrosakStari     decimal(18,2) NULL,     -- "TP - maj"
    MarzaStara      decimal(9,4)  NULL      -- "marza maj"
);
GO

-- spisak ID-jeva, npr. oznaceni redovi u gridu (IC_ProglasiTroskoveNovim)
IF TYPE_ID('dbo.IC_IdTip') IS NULL
CREATE TYPE dbo.IC_IdTip AS TABLE (
    ID int NOT NULL PRIMARY KEY
);
GO

-- slike nadjene u folderu (IC_PoveziSlikeAutomatski)
IF TYPE_ID('dbo.IC_SlikaTip') IS NULL
CREATE TYPE dbo.IC_SlikaTip AS TABLE (
    Code     nvarchar(30)  NOT NULL,
    Putanja  nvarchar(400) NOT NULL,
    Dodatna  bit           NOT NULL,        -- 1 = fajl sa dodatkom "(2)"
    PRIMARY KEY (Code, Putanja)
);
GO
