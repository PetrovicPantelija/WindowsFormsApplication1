/* =====================================================================
   ZAH-010 Servis - 02: tabele
   Sve tabele imaju prefiks SRV_. Nema stranih kljuceva prema postojecim
   tabelama Jele (Partnerji, Vozila, Korisnici): veza je samo po broju.
   Nema fizickog brisanja: kolona Aktivno.
   Skripta se sme pokretati vise puta (IF OBJECT_ID ... IS NULL).
   ===================================================================== */
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET NOCOUNT ON;
GO

/* =====================================================================
   SERVIS - sifarnici i sredstva
   ===================================================================== */

IF OBJECT_ID('dbo.SRV_VrstaSredstva', 'U') IS NULL
CREATE TABLE dbo.SRV_VrstaSredstva
(
    ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_VrstaSredstva PRIMARY KEY,
    Naziv           nvarchar(100) NOT NULL,
    Napomena        nvarchar(400) NULL,
    Aktivno         bit NOT NULL CONSTRAINT DF_SRV_VrstaSredstva_Aktivno DEFAULT (1),
    Kreirao         nvarchar(50) NULL,
    DatumKreiranja  datetime2(0) NOT NULL CONSTRAINT DF_SRV_VrstaSredstva_DatumKreiranja DEFAULT (SYSDATETIME()),
    Izmenio         nvarchar(50) NULL,
    DatumIzmene     datetime2(0) NULL,
    CONSTRAINT UQ_SRV_VrstaSredstva_Naziv UNIQUE (Naziv),
    CONSTRAINT CK_SRV_VrstaSredstva_Naziv CHECK (LEN(Naziv) > 0)
);
GO

IF OBJECT_ID('dbo.SRV_VrstaSredstvaBrojilo', 'U') IS NULL
CREATE TABLE dbo.SRV_VrstaSredstvaBrojilo
(
    ID               int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_VrstaSredstvaBrojilo PRIMARY KEY,
    VrstaSredstvaID  int NOT NULL CONSTRAINT FK_SRV_VrstaSredstvaBrojilo_Vrsta REFERENCES dbo.SRV_VrstaSredstva (ID),
    Tip              nvarchar(20) NOT NULL,          -- KM, MH, ...
    Redosled         int NOT NULL CONSTRAINT DF_SRV_VrstaSredstvaBrojilo_Redosled DEFAULT (1),
    Aktivno          bit NOT NULL CONSTRAINT DF_SRV_VrstaSredstvaBrojilo_Aktivno DEFAULT (1),
    CONSTRAINT UQ_SRV_VrstaSredstvaBrojilo_Tip UNIQUE (VrstaSredstvaID, Tip)
);
GO

IF OBJECT_ID('dbo.SRV_VrstaSredstvaPolje', 'U') IS NULL
CREATE TABLE dbo.SRV_VrstaSredstvaPolje
(
    ID               int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_VrstaSredstvaPolje PRIMARY KEY,
    VrstaSredstvaID  int NOT NULL CONSTRAINT FK_SRV_VrstaSredstvaPolje_Vrsta REFERENCES dbo.SRV_VrstaSredstva (ID),
    Naziv            nvarchar(100) NOT NULL,
    Tip              nvarchar(10)  NOT NULL,
    JM               nvarchar(20)  NULL,
    Opcije           nvarchar(400) NULL,              -- za Listu: vrednosti odvojene sa ;
    Obavezno         bit NOT NULL CONSTRAINT DF_SRV_VrstaSredstvaPolje_Obavezno DEFAULT (0),
    Redosled         int NOT NULL CONSTRAINT DF_SRV_VrstaSredstvaPolje_Redosled DEFAULT (1),
    Aktivno          bit NOT NULL CONSTRAINT DF_SRV_VrstaSredstvaPolje_Aktivno DEFAULT (1),
    Kreirao          nvarchar(50) NULL,
    DatumKreiranja   datetime2(0) NOT NULL CONSTRAINT DF_SRV_VrstaSredstvaPolje_DatumKreiranja DEFAULT (SYSDATETIME()),
    Izmenio          nvarchar(50) NULL,
    DatumIzmene      datetime2(0) NULL,
    CONSTRAINT UQ_SRV_VrstaSredstvaPolje_Naziv UNIQUE (VrstaSredstvaID, Naziv),
    CONSTRAINT CK_SRV_VrstaSredstvaPolje_Tip CHECK (Tip IN (N'Tekst', N'Broj', N'Lista', N'Datum')),
    CONSTRAINT CK_SRV_VrstaSredstvaPolje_Opcije CHECK (Tip <> N'Lista' OR LEN(ISNULL(Opcije, N'')) > 0)
);
GO

IF OBJECT_ID('dbo.SRV_VrstaSredstvaDokument', 'U') IS NULL
CREATE TABLE dbo.SRV_VrstaSredstvaDokument
(
    ID               int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_VrstaSredstvaDokument PRIMARY KEY,
    VrstaSredstvaID  int NOT NULL CONSTRAINT FK_SRV_VrstaSredstvaDokument_Vrsta REFERENCES dbo.SRV_VrstaSredstva (ID),
    Naziv            nvarchar(100) NOT NULL,
    Obavezno         bit NOT NULL CONSTRAINT DF_SRV_VrstaSredstvaDokument_Obavezno DEFAULT (0),
    UpozorenjeDana   int NOT NULL CONSTRAINT DF_SRV_VrstaSredstvaDokument_UpozorenjeDana DEFAULT (30),
    Aktivno          bit NOT NULL CONSTRAINT DF_SRV_VrstaSredstvaDokument_Aktivno DEFAULT (1),
    Kreirao          nvarchar(50) NULL,
    DatumKreiranja   datetime2(0) NOT NULL CONSTRAINT DF_SRV_VrstaSredstvaDokument_DatumKreiranja DEFAULT (SYSDATETIME()),
    Izmenio          nvarchar(50) NULL,
    DatumIzmene      datetime2(0) NULL,
    CONSTRAINT UQ_SRV_VrstaSredstvaDokument_Naziv UNIQUE (VrstaSredstvaID, Naziv),
    CONSTRAINT CK_SRV_VrstaSredstvaDokument_Upozorenje CHECK (UpozorenjeDana >= 0)
);
GO

IF OBJECT_ID('dbo.SRV_Sredstvo', 'U') IS NULL
CREATE TABLE dbo.SRV_Sredstvo
(
    ID               int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_Sredstvo PRIMARY KEY,
    Oznaka           nvarchar(30)  NOT NULL,          -- inventarski broj
    Naziv            nvarchar(150) NOT NULL,
    VrstaSredstvaID  int NOT NULL CONSTRAINT FK_SRV_Sredstvo_Vrsta REFERENCES dbo.SRV_VrstaSredstva (ID),
    Proizvodjac      nvarchar(100) NULL,
    Model            nvarchar(100) NULL,
    Godina           int NULL,
    SerijskiBroj     nvarchar(100) NULL,
    Lokacija         nvarchar(100) NULL,               -- maticna lokacija
    Status           nvarchar(20)  NOT NULL CONSTRAINT DF_SRV_Sredstvo_Status DEFAULT (N'U radu'),
    VoziloID         int NULL,                          -- opciona veza sa tabelom Vozila (isporuke), bez FK (OP-J-01)
    Napomena         nvarchar(400) NULL,
    Aktivno          bit NOT NULL CONSTRAINT DF_SRV_Sredstvo_Aktivno DEFAULT (1),
    Kreirao          nvarchar(50) NULL,
    DatumKreiranja   datetime2(0) NOT NULL CONSTRAINT DF_SRV_Sredstvo_DatumKreiranja DEFAULT (SYSDATETIME()),
    Izmenio          nvarchar(50) NULL,
    DatumIzmene      datetime2(0) NULL,
    CONSTRAINT UQ_SRV_Sredstvo_Oznaka UNIQUE (Oznaka),
    CONSTRAINT CK_SRV_Sredstvo_Status CHECK (Status IN (N'U radu', N'U kvaru', N'Van upotrebe', N'Arhivirano')),
    CONSTRAINT CK_SRV_Sredstvo_Godina CHECK (Godina IS NULL OR Godina BETWEEN 1900 AND 2100)
);
GO

IF OBJECT_ID('dbo.SRV_SredstvoPolje', 'U') IS NULL
CREATE TABLE dbo.SRV_SredstvoPolje
(
    ID          int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_SredstvoPolje PRIMARY KEY,
    SredstvoID  int NOT NULL CONSTRAINT FK_SRV_SredstvoPolje_Sredstvo REFERENCES dbo.SRV_Sredstvo (ID),
    PoljeID     int NOT NULL CONSTRAINT FK_SRV_SredstvoPolje_Polje REFERENCES dbo.SRV_VrstaSredstvaPolje (ID),
    Vrednost    nvarchar(400) NULL,
    CONSTRAINT UQ_SRV_SredstvoPolje_Polje UNIQUE (SredstvoID, PoljeID)
);
GO

IF OBJECT_ID('dbo.SRV_SredstvoBrojilo', 'U') IS NULL
CREATE TABLE dbo.SRV_SredstvoBrojilo
(
    ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_SredstvoBrojilo PRIMARY KEY,
    SredstvoID      int NOT NULL CONSTRAINT FK_SRV_SredstvoBrojilo_Sredstvo REFERENCES dbo.SRV_Sredstvo (ID),
    Tip             nvarchar(20) NOT NULL,
    Vrednost        decimal(18,2) NOT NULL,
    Datum           date NOT NULL,
    Izvor           nvarchar(30) NOT NULL CONSTRAINT DF_SRV_SredstvoBrojilo_Izvor DEFAULT (N'Očitavanje'),
    Razlog          nvarchar(200) NULL,                -- obavezan kada brojilo ide unazad
    Kreirao         nvarchar(50) NULL,
    DatumKreiranja  datetime2(0) NOT NULL CONSTRAINT DF_SRV_SredstvoBrojilo_DatumKreiranja DEFAULT (SYSDATETIME()),
    CONSTRAINT CK_SRV_SredstvoBrojilo_Vrednost CHECK (Vrednost >= 0)
);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_SRV_SredstvoBrojilo_Sredstvo' AND object_id = OBJECT_ID('dbo.SRV_SredstvoBrojilo'))
    CREATE INDEX IX_SRV_SredstvoBrojilo_Sredstvo ON dbo.SRV_SredstvoBrojilo (SredstvoID, Tip, Datum, ID);
GO

IF OBJECT_ID('dbo.SRV_SredstvoDokument', 'U') IS NULL
CREATE TABLE dbo.SRV_SredstvoDokument
(
    ID                int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_SredstvoDokument PRIMARY KEY,
    SredstvoID        int NOT NULL CONSTRAINT FK_SRV_SredstvoDokument_Sredstvo REFERENCES dbo.SRV_Sredstvo (ID),
    VrstaDokumentaID  int NOT NULL CONSTRAINT FK_SRV_SredstvoDokument_Vrsta REFERENCES dbo.SRV_VrstaSredstvaDokument (ID),
    Broj              nvarchar(50)  NULL,
    DatumIzdavanja    date NOT NULL,
    DatumIsteka       date NOT NULL,
    Prilog            nvarchar(400) NULL,
    Napomena          nvarchar(400) NULL,
    ObavestenoPrag    nvarchar(20)  NULL,               -- poslednji prag za koji je napravljeno obavestenje (SR-17)
    Aktivno           bit NOT NULL CONSTRAINT DF_SRV_SredstvoDokument_Aktivno DEFAULT (1),
    Kreirao           nvarchar(50) NULL,
    DatumKreiranja    datetime2(0) NOT NULL CONSTRAINT DF_SRV_SredstvoDokument_DatumKreiranja DEFAULT (SYSDATETIME()),
    Izmenio           nvarchar(50) NULL,
    DatumIzmene       datetime2(0) NULL,
    CONSTRAINT CK_SRV_SredstvoDokument_Datumi CHECK (DatumIsteka >= DatumIzdavanja)
);
GO

/* =====================================================================
   MAGACIN DELOVA - sifarnik (potreban za normativ plana)
   ===================================================================== */

IF OBJECT_ID('dbo.SRV_DeoGrupa', 'U') IS NULL
CREATE TABLE dbo.SRV_DeoGrupa
(
    ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_DeoGrupa PRIMARY KEY,
    Naziv           nvarchar(100) NOT NULL,
    Aktivno         bit NOT NULL CONSTRAINT DF_SRV_DeoGrupa_Aktivno DEFAULT (1),
    Kreirao         nvarchar(50) NULL,
    DatumKreiranja  datetime2(0) NOT NULL CONSTRAINT DF_SRV_DeoGrupa_DatumKreiranja DEFAULT (SYSDATETIME()),
    Izmenio         nvarchar(50) NULL,
    DatumIzmene     datetime2(0) NULL,
    CONSTRAINT UQ_SRV_DeoGrupa_Naziv UNIQUE (Naziv),
    CONSTRAINT CK_SRV_DeoGrupa_Naziv CHECK (LEN(Naziv) > 0)
);
GO

IF OBJECT_ID('dbo.SRV_Deo', 'U') IS NULL
CREATE TABLE dbo.SRV_Deo
(
    ID                int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_Deo PRIMARY KEY,
    Sifra             nvarchar(30)  NOT NULL,
    Naziv             nvarchar(150) NOT NULL,
    DeoGrupaID        int NOT NULL CONSTRAINT FK_SRV_Deo_Grupa REFERENCES dbo.SRV_DeoGrupa (ID),
    JM                nvarchar(10)  NOT NULL,
    MinZaliha         decimal(18,3) NOT NULL CONSTRAINT DF_SRV_Deo_MinZaliha DEFAULT (0),
    MaxZaliha         decimal(18,3) NOT NULL CONSTRAINT DF_SRV_Deo_MaxZaliha DEFAULT (0),
    ProsecnaCena      decimal(18,4) NOT NULL CONSTRAINT DF_SRV_Deo_ProsecnaCena DEFAULT (0),
    Lokacija          nvarchar(50)  NULL,
    Kritican          bit NOT NULL CONSTRAINT DF_SRV_Deo_Kritican DEFAULT (0),
    DobavljacPaSifra  int NULL,                          -- Partnerji.PaSifra, bez FK (OP-J-05)
    RokIsporukeDana   int NULL,
    Napomena          nvarchar(400) NULL,
    IspodMinimuma     bit NOT NULL CONSTRAINT DF_SRV_Deo_IspodMinimuma DEFAULT (0),  -- prag za obavestenje (MD-R08)
    Aktivno           bit NOT NULL CONSTRAINT DF_SRV_Deo_Aktivno DEFAULT (1),
    Kreirao           nvarchar(50) NULL,
    DatumKreiranja    datetime2(0) NOT NULL CONSTRAINT DF_SRV_Deo_DatumKreiranja DEFAULT (SYSDATETIME()),
    Izmenio           nvarchar(50) NULL,
    DatumIzmene       datetime2(0) NULL,
    CONSTRAINT UQ_SRV_Deo_Sifra UNIQUE (Sifra),
    CONSTRAINT CK_SRV_Deo_Min CHECK (MinZaliha >= 0),
    CONSTRAINT CK_SRV_Deo_Max CHECK (MaxZaliha >= MinZaliha),
    CONSTRAINT CK_SRV_Deo_Cena CHECK (ProsecnaCena >= 0),
    CONSTRAINT CK_SRV_Deo_Rok CHECK (RokIsporukeDana IS NULL OR RokIsporukeDana >= 0)
);
GO

IF OBJECT_ID('dbo.SRV_DeoPrimenljivost', 'U') IS NULL
CREATE TABLE dbo.SRV_DeoPrimenljivost
(
    ID               int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_DeoPrimenljivost PRIMARY KEY,
    DeoID            int NOT NULL CONSTRAINT FK_SRV_DeoPrimenljivost_Deo REFERENCES dbo.SRV_Deo (ID),
    VrstaSredstvaID  int NOT NULL CONSTRAINT FK_SRV_DeoPrimenljivost_Vrsta REFERENCES dbo.SRV_VrstaSredstva (ID),
    SredstvoID       int NULL CONSTRAINT FK_SRV_DeoPrimenljivost_Sredstvo REFERENCES dbo.SRV_Sredstvo (ID),
    Kreirao          nvarchar(50) NULL,
    DatumKreiranja   datetime2(0) NOT NULL CONSTRAINT DF_SRV_DeoPrimenljivost_DatumKreiranja DEFAULT (SYSDATETIME()),
    CONSTRAINT UQ_SRV_DeoPrimenljivost_Vrsta UNIQUE (DeoID, VrstaSredstvaID, SredstvoID)
);
GO

/* =====================================================================
   SERVIS - plan odrzavanja i normativ
   ===================================================================== */

IF OBJECT_ID('dbo.SRV_PlanOdrzavanja', 'U') IS NULL
CREATE TABLE dbo.SRV_PlanOdrzavanja
(
    ID               int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_PlanOdrzavanja PRIMARY KEY,
    VrstaSredstvaID  int NOT NULL CONSTRAINT FK_SRV_PlanOdrzavanja_Vrsta REFERENCES dbo.SRV_VrstaSredstva (ID),
    Naziv            nvarchar(100) NOT NULL,
    IntervalKm       decimal(18,2) NOT NULL CONSTRAINT DF_SRV_PlanOdrzavanja_IntervalKm DEFAULT (0),
    IntervalMH       decimal(18,2) NOT NULL CONSTRAINT DF_SRV_PlanOdrzavanja_IntervalMH DEFAULT (0),
    IntervalDana     int NOT NULL CONSTRAINT DF_SRV_PlanOdrzavanja_IntervalDana DEFAULT (0),
    IntervalMeseci   int NOT NULL CONSTRAINT DF_SRV_PlanOdrzavanja_IntervalMeseci DEFAULT (0),
    UpozorenjeKm     decimal(18,2) NOT NULL CONSTRAINT DF_SRV_PlanOdrzavanja_UpozorenjeKm DEFAULT (0),
    UpozorenjeMH     decimal(18,2) NOT NULL CONSTRAINT DF_SRV_PlanOdrzavanja_UpozorenjeMH DEFAULT (0),
    UpozorenjeDana   int NOT NULL CONSTRAINT DF_SRV_PlanOdrzavanja_UpozorenjeDana DEFAULT (0),  -- za intervale u danima i mesecima
    Napomena         nvarchar(400) NULL,
    Aktivno          bit NOT NULL CONSTRAINT DF_SRV_PlanOdrzavanja_Aktivno DEFAULT (1),
    Kreirao          nvarchar(50) NULL,
    DatumKreiranja   datetime2(0) NOT NULL CONSTRAINT DF_SRV_PlanOdrzavanja_DatumKreiranja DEFAULT (SYSDATETIME()),
    Izmenio          nvarchar(50) NULL,
    DatumIzmene      datetime2(0) NULL,
    CONSTRAINT UQ_SRV_PlanOdrzavanja_Naziv UNIQUE (VrstaSredstvaID, Naziv),
    CONSTRAINT CK_SRV_PlanOdrzavanja_Interval CHECK (IntervalKm > 0 OR IntervalMH > 0 OR IntervalDana > 0 OR IntervalMeseci > 0),
    CONSTRAINT CK_SRV_PlanOdrzavanja_Negativno CHECK (IntervalKm >= 0 AND IntervalMH >= 0 AND IntervalDana >= 0 AND IntervalMeseci >= 0
                                                      AND UpozorenjeKm >= 0 AND UpozorenjeMH >= 0 AND UpozorenjeDana >= 0),
    CONSTRAINT CK_SRV_PlanOdrzavanja_Upozorenje CHECK (UpozorenjeKm <= IntervalKm AND UpozorenjeMH <= IntervalMH
                                                       AND (UpozorenjeDana = 0 OR UpozorenjeDana <= IntervalDana OR UpozorenjeDana <= IntervalMeseci * 30))
);
GO

IF OBJECT_ID('dbo.SRV_PlanNormativ', 'U') IS NULL
CREATE TABLE dbo.SRV_PlanNormativ
(
    ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_PlanNormativ PRIMARY KEY,
    PlanID          int NOT NULL CONSTRAINT FK_SRV_PlanNormativ_Plan REFERENCES dbo.SRV_PlanOdrzavanja (ID),
    DeoID           int NOT NULL CONSTRAINT FK_SRV_PlanNormativ_Deo REFERENCES dbo.SRV_Deo (ID),
    Kolicina        decimal(18,3) NOT NULL,
    Kreirao         nvarchar(50) NULL,
    DatumKreiranja  datetime2(0) NOT NULL CONSTRAINT DF_SRV_PlanNormativ_DatumKreiranja DEFAULT (SYSDATETIME()),
    CONSTRAINT UQ_SRV_PlanNormativ_Deo UNIQUE (PlanID, DeoID),
    CONSTRAINT CK_SRV_PlanNormativ_Kolicina CHECK (Kolicina > 0)
);
GO

/* =====================================================================
   SERVIS - kvarovi, nalozi, spoljni servis
   ===================================================================== */

IF OBJECT_ID('dbo.SRV_Kvar', 'U') IS NULL
CREATE TABLE dbo.SRV_Kvar
(
    ID                 int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_Kvar PRIMARY KEY,
    Broj               nvarchar(30)  NOT NULL,
    SredstvoID         int NOT NULL CONSTRAINT FK_SRV_Kvar_Sredstvo REFERENCES dbo.SRV_Sredstvo (ID),
    DatumPrijave       datetime2(0)  NOT NULL CONSTRAINT DF_SRV_Kvar_DatumPrijave DEFAULT (SYSDATETIME()),
    Prijavio           nvarchar(100) NOT NULL,
    Kanal              nvarchar(30)  NULL,
    BrojiloTip         nvarchar(20)  NULL,
    Brojilo            decimal(18,2) NULL,
    Kategorija         nvarchar(50)  NULL,
    Hitnost            nvarchar(20)  NOT NULL CONSTRAINT DF_SRV_Kvar_Hitnost DEFAULT (N'Srednja'),
    MozeDaRadi         bit NOT NULL CONSTRAINT DF_SRV_Kvar_MozeDaRadi DEFAULT (1),
    Opis               nvarchar(1000) NOT NULL,
    Status             nvarchar(20)  NOT NULL CONSTRAINT DF_SRV_Kvar_Status DEFAULT (N'Nova'),
    RazlogOdbacivanja  nvarchar(400) NULL,
    Kreirao            nvarchar(50) NULL,
    DatumKreiranja     datetime2(0) NOT NULL CONSTRAINT DF_SRV_Kvar_DatumKreiranja DEFAULT (SYSDATETIME()),
    Izmenio            nvarchar(50) NULL,
    DatumIzmene        datetime2(0) NULL,
    CONSTRAINT UQ_SRV_Kvar_Broj UNIQUE (Broj),
    CONSTRAINT CK_SRV_Kvar_Status CHECK (Status IN (N'Nova', N'U obradi', N'Zatvoreno', N'Odbačeno')),
    CONSTRAINT CK_SRV_Kvar_Hitnost CHECK (Hitnost IN (N'Niska', N'Srednja', N'Visoka', N'Hitno')),
    CONSTRAINT CK_SRV_Kvar_Odbacen CHECK (Status <> N'Odbačeno' OR LEN(ISNULL(RazlogOdbacivanja, N'')) > 0),
    CONSTRAINT CK_SRV_Kvar_Opis CHECK (LEN(Opis) > 0)
);
GO

IF OBJECT_ID('dbo.SRV_KvarPrilog', 'U') IS NULL
CREATE TABLE dbo.SRV_KvarPrilog
(
    ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_KvarPrilog PRIMARY KEY,
    KvarID          int NOT NULL CONSTRAINT FK_SRV_KvarPrilog_Kvar REFERENCES dbo.SRV_Kvar (ID),
    Putanja         nvarchar(400) NOT NULL,           -- relativno prema Servis.KorenPriloga
    Naziv           nvarchar(200) NULL,
    Aktivno         bit NOT NULL CONSTRAINT DF_SRV_KvarPrilog_Aktivno DEFAULT (1),
    Kreirao         nvarchar(50) NULL,
    DatumKreiranja  datetime2(0) NOT NULL CONSTRAINT DF_SRV_KvarPrilog_DatumKreiranja DEFAULT (SYSDATETIME())
);
GO

IF OBJECT_ID('dbo.SRV_Nalog', 'U') IS NULL
CREATE TABLE dbo.SRV_Nalog
(
    ID                 int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_Nalog PRIMARY KEY,
    Broj               nvarchar(30)  NOT NULL,
    SredstvoID         int NOT NULL CONSTRAINT FK_SRV_Nalog_Sredstvo REFERENCES dbo.SRV_Sredstvo (ID),
    Vrsta              nvarchar(20)  NOT NULL,
    KvarID             int NULL CONSTRAINT FK_SRV_Nalog_Kvar REFERENCES dbo.SRV_Kvar (ID),
    PlanID             int NULL CONSTRAINT FK_SRV_Nalog_Plan REFERENCES dbo.SRV_PlanOdrzavanja (ID),
    Izvrsilac          nvarchar(20)  NOT NULL CONSTRAINT DF_SRV_Nalog_Izvrsilac DEFAULT (N'Interni'),
    Status             nvarchar(20)  NOT NULL CONSTRAINT DF_SRV_Nalog_Status DEFAULT (N'Otvoren'),
    DatumOtvaranja     datetime2(0)  NOT NULL CONSTRAINT DF_SRV_Nalog_DatumOtvaranja DEFAULT (SYSDATETIME()),
    DatumZavrsetka     datetime2(0)  NULL,
    BrojiloTip         nvarchar(20)  NULL,
    BrojiloOtvaranje   decimal(18,2) NULL,
    BrojiloZatvaranje  decimal(18,2) NULL,
    Opis               nvarchar(1000) NULL,
    RazlogOtkazivanja  nvarchar(400) NULL,
    Kreirao            nvarchar(50) NULL,
    DatumKreiranja     datetime2(0) NOT NULL CONSTRAINT DF_SRV_Nalog_DatumKreiranja DEFAULT (SYSDATETIME()),
    Izmenio            nvarchar(50) NULL,
    DatumIzmene        datetime2(0) NULL,
    CONSTRAINT UQ_SRV_Nalog_Broj UNIQUE (Broj),
    CONSTRAINT CK_SRV_Nalog_Vrsta CHECK (Vrsta IN (N'Vanredni', N'Redovni')),
    CONSTRAINT CK_SRV_Nalog_Izvrsilac CHECK (Izvrsilac IN (N'Interni', N'Spoljni')),
    CONSTRAINT CK_SRV_Nalog_Status CHECK (Status IN (N'Otvoren', N'U radu', N'Čeka delove', N'Čeka odobrenje', N'Završen', N'Otkazan')),
    CONSTRAINT CK_SRV_Nalog_Brojilo CHECK (BrojiloZatvaranje IS NULL OR BrojiloOtvaranje IS NULL OR BrojiloZatvaranje >= BrojiloOtvaranje),
    CONSTRAINT CK_SRV_Nalog_Otkazan CHECK (Status <> N'Otkazan' OR LEN(ISNULL(RazlogOtkazivanja, N'')) > 0),
    CONSTRAINT CK_SRV_Nalog_Redovni CHECK (Vrsta <> N'Redovni' OR PlanID IS NOT NULL)
);
GO

IF OBJECT_ID('dbo.SRV_NalogRad', 'U') IS NULL
CREATE TABLE dbo.SRV_NalogRad
(
    ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_NalogRad PRIMARY KEY,
    NalogID         int NOT NULL CONSTRAINT FK_SRV_NalogRad_Nalog REFERENCES dbo.SRV_Nalog (ID),
    Osoba           nvarchar(100) NOT NULL,
    Zanimanje       nvarchar(50)  NULL,
    Datum           date NOT NULL,
    Sati            decimal(9,2)  NOT NULL,
    CenaSata        decimal(18,2) NOT NULL CONSTRAINT DF_SRV_NalogRad_CenaSata DEFAULT (0),
    Opis            nvarchar(400) NULL,
    Aktivno         bit NOT NULL CONSTRAINT DF_SRV_NalogRad_Aktivno DEFAULT (1),
    Kreirao         nvarchar(50) NULL,
    DatumKreiranja  datetime2(0) NOT NULL CONSTRAINT DF_SRV_NalogRad_DatumKreiranja DEFAULT (SYSDATETIME()),
    CONSTRAINT CK_SRV_NalogRad_Sati CHECK (Sati > 0),
    CONSTRAINT CK_SRV_NalogRad_Cena CHECK (CenaSata >= 0)
);
GO

-- delovi na nalogu = rezervacija dela (SRV_DeoRezervacija iz izvornog fajla je ista stvar, vidi dnevnik ZAH-010)
IF OBJECT_ID('dbo.SRV_NalogDeo', 'U') IS NULL
CREATE TABLE dbo.SRV_NalogDeo
(
    ID               int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_NalogDeo PRIMARY KEY,
    NalogID          int NOT NULL CONSTRAINT FK_SRV_NalogDeo_Nalog REFERENCES dbo.SRV_Nalog (ID),
    DeoID            int NOT NULL CONSTRAINT FK_SRV_NalogDeo_Deo REFERENCES dbo.SRV_Deo (ID),
    Kolicina         decimal(18,3) NOT NULL,
    Status           nvarchar(20)  NOT NULL CONSTRAINT DF_SRV_NalogDeo_Status DEFAULT (N'Rezervisano'),
    CenaIzdavanja    decimal(18,4) NULL,
    IzdatnicaBroj    nvarchar(30)  NULL,
    VracenoKolicina  decimal(18,3) NOT NULL CONSTRAINT DF_SRV_NalogDeo_VracenoKolicina DEFAULT (0),
    IzNormativa      bit NOT NULL CONSTRAINT DF_SRV_NalogDeo_IzNormativa DEFAULT (0),
    Kreirao          nvarchar(50) NULL,
    DatumKreiranja   datetime2(0) NOT NULL CONSTRAINT DF_SRV_NalogDeo_DatumKreiranja DEFAULT (SYSDATETIME()),
    Izmenio          nvarchar(50) NULL,
    DatumIzmene      datetime2(0) NULL,
    CONSTRAINT CK_SRV_NalogDeo_Kolicina CHECK (Kolicina > 0),
    CONSTRAINT CK_SRV_NalogDeo_Status CHECK (Status IN (N'Rezervisano', N'Izdato', N'Nema na stanju', N'Storno')),
    CONSTRAINT CK_SRV_NalogDeo_Vraceno CHECK (VracenoKolicina >= 0 AND VracenoKolicina <= Kolicina)
);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_SRV_NalogDeo_Deo' AND object_id = OBJECT_ID('dbo.SRV_NalogDeo'))
    CREATE INDEX IX_SRV_NalogDeo_Deo ON dbo.SRV_NalogDeo (DeoID, Status) INCLUDE (Kolicina, NalogID);
GO

IF OBJECT_ID('dbo.SRV_SpoljniServisZahtev', 'U') IS NULL
CREATE TABLE dbo.SRV_SpoljniServisZahtev
(
    ID               int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_SpoljniServisZahtev PRIMARY KEY,
    Broj             nvarchar(30)  NOT NULL,
    NalogID          int NOT NULL CONSTRAINT FK_SRV_SpoljniServisZahtev_Nalog REFERENCES dbo.SRV_Nalog (ID),
    PaSifra          int NOT NULL,                       -- Partnerji.PaSifra, bez FK
    BrojPonude       nvarchar(50)  NULL,
    Razlog           nvarchar(1000) NOT NULL,
    Iznos            decimal(18,2) NOT NULL,
    Nivo             tinyint NOT NULL,
    Status           nvarchar(30)  NOT NULL CONSTRAINT DF_SRV_SpoljniServisZahtev_Status DEFAULT (N'Čeka odobrenje'),
    Podneo           nvarchar(50)  NOT NULL,
    Odobrio          nvarchar(50)  NULL,
    DatumOdobrenja   datetime2(0)  NULL,
    Faktura          nvarchar(50)  NULL,
    GarancijaMeseci  int NULL,
    Kreirao          nvarchar(50) NULL,
    DatumKreiranja   datetime2(0) NOT NULL CONSTRAINT DF_SRV_SpoljniServisZahtev_DatumKreiranja DEFAULT (SYSDATETIME()),
    Izmenio          nvarchar(50) NULL,
    DatumIzmene      datetime2(0) NULL,
    CONSTRAINT UQ_SRV_SpoljniServisZahtev_Broj UNIQUE (Broj),
    CONSTRAINT CK_SRV_SpoljniServisZahtev_Iznos CHECK (Iznos > 0),
    CONSTRAINT CK_SRV_SpoljniServisZahtev_Nivo CHECK (Nivo BETWEEN 1 AND 3),
    CONSTRAINT CK_SRV_SpoljniServisZahtev_Status CHECK (Status IN (N'Čeka odobrenje', N'Odobreno', N'Odbijeno', N'Vraćeno na doradu', N'Zatvoreno')),
    CONSTRAINT CK_SRV_SpoljniServisZahtev_Garancija CHECK (GarancijaMeseci IS NULL OR GarancijaMeseci >= 0)
);
GO

IF OBJECT_ID('dbo.SRV_SpoljniServisIstorija', 'U') IS NULL
CREATE TABLE dbo.SRV_SpoljniServisIstorija
(
    ID         int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_SpoljniServisIstorija PRIMARY KEY,
    ZahtevID   int NOT NULL CONSTRAINT FK_SRV_SpoljniServisIstorija_Zahtev REFERENCES dbo.SRV_SpoljniServisZahtev (ID),
    Vreme      datetime2(0) NOT NULL CONSTRAINT DF_SRV_SpoljniServisIstorija_Vreme DEFAULT (SYSDATETIME()),
    Korisnik   nvarchar(50) NOT NULL,
    Radnja     nvarchar(50) NOT NULL,
    Komentar   nvarchar(1000) NULL
);
GO

/* =====================================================================
   MAGACIN DELOVA - kretanja i dokumenti
   ===================================================================== */

IF OBJECT_ID('dbo.SRV_Narudzbenica', 'U') IS NULL
CREATE TABLE dbo.SRV_Narudzbenica
(
    ID               int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_Narudzbenica PRIMARY KEY,
    Broj             nvarchar(30)  NOT NULL,
    Datum            date NOT NULL,
    PaSifra          int NOT NULL,                       -- Partnerji.PaSifra, bez FK
    OcekivaniDatum   date NULL,
    Status           nvarchar(30)  NOT NULL CONSTRAINT DF_SRV_Narudzbenica_Status DEFAULT (N'Nacrt'),
    Nivo             tinyint NULL,
    Napomena         nvarchar(400) NULL,
    Podneo           nvarchar(50)  NULL,
    DatumSlanja      datetime2(0)  NULL,
    Odobrio          nvarchar(50)  NULL,
    DatumOdobrenja   datetime2(0)  NULL,
    Komentar         nvarchar(400) NULL,
    Kreirao          nvarchar(50) NULL,
    DatumKreiranja   datetime2(0) NOT NULL CONSTRAINT DF_SRV_Narudzbenica_DatumKreiranja DEFAULT (SYSDATETIME()),
    Izmenio          nvarchar(50) NULL,
    DatumIzmene      datetime2(0) NULL,
    CONSTRAINT UQ_SRV_Narudzbenica_Broj UNIQUE (Broj),
    CONSTRAINT CK_SRV_Narudzbenica_Status CHECK (Status IN (N'Nacrt', N'Čeka odobrenje', N'Poručeno', N'Delimično isporučeno', N'Isporučeno', N'Otkazano'))
);
GO

IF OBJECT_ID('dbo.SRV_NarudzbenicaStavka', 'U') IS NULL
CREATE TABLE dbo.SRV_NarudzbenicaStavka
(
    ID                 int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_NarudzbenicaStavka PRIMARY KEY,
    NarudzbenicaID     int NOT NULL CONSTRAINT FK_SRV_NarudzbenicaStavka_Narudzbenica REFERENCES dbo.SRV_Narudzbenica (ID),
    DeoID              int NOT NULL CONSTRAINT FK_SRV_NarudzbenicaStavka_Deo REFERENCES dbo.SRV_Deo (ID),
    Kolicina           decimal(18,3) NOT NULL,
    Cena               decimal(18,4) NOT NULL CONSTRAINT DF_SRV_NarudzbenicaStavka_Cena DEFAULT (0),
    PrimljenoKolicina  decimal(18,3) NOT NULL CONSTRAINT DF_SRV_NarudzbenicaStavka_PrimljenoKolicina DEFAULT (0),
    CONSTRAINT UQ_SRV_NarudzbenicaStavka_Deo UNIQUE (NarudzbenicaID, DeoID),
    CONSTRAINT CK_SRV_NarudzbenicaStavka_Kolicina CHECK (Kolicina > 0),
    CONSTRAINT CK_SRV_NarudzbenicaStavka_Cena CHECK (Cena >= 0),
    CONSTRAINT CK_SRV_NarudzbenicaStavka_Primljeno CHECK (PrimljenoKolicina >= 0)
);
GO

IF OBJECT_ID('dbo.SRV_Popis', 'U') IS NULL
CREATE TABLE dbo.SRV_Popis
(
    ID               int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_Popis PRIMARY KEY,
    Broj             nvarchar(30)  NOT NULL,
    Datum            date NOT NULL,
    DeoGrupaID       int NULL CONSTRAINT FK_SRV_Popis_Grupa REFERENCES dbo.SRV_DeoGrupa (ID),   -- NULL = svi delovi
    Status           nvarchar(20)  NOT NULL CONSTRAINT DF_SRV_Popis_Status DEFAULT (N'U toku'),
    Napomena         nvarchar(400) NULL,
    Podneo           nvarchar(50)  NULL,
    Odobrio          nvarchar(50)  NULL,
    DatumOdobrenja   datetime2(0)  NULL,
    Kreirao          nvarchar(50) NULL,
    DatumKreiranja   datetime2(0) NOT NULL CONSTRAINT DF_SRV_Popis_DatumKreiranja DEFAULT (SYSDATETIME()),
    Izmenio          nvarchar(50) NULL,
    DatumIzmene      datetime2(0) NULL,
    CONSTRAINT UQ_SRV_Popis_Broj UNIQUE (Broj),
    CONSTRAINT CK_SRV_Popis_Status CHECK (Status IN (N'U toku', N'Čeka odobrenje', N'Odobren', N'Storniran'))
);
GO

IF OBJECT_ID('dbo.SRV_PopisStavka', 'U') IS NULL
CREATE TABLE dbo.SRV_PopisStavka
(
    ID                        int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_PopisStavka PRIMARY KEY,
    PopisID                   int NOT NULL CONSTRAINT FK_SRV_PopisStavka_Popis REFERENCES dbo.SRV_Popis (ID),
    DeoID                     int NOT NULL CONSTRAINT FK_SRV_PopisStavka_Deo REFERENCES dbo.SRV_Deo (ID),
    KnjigovodstvenaKolicina   decimal(18,3) NOT NULL,
    PrebrojanaKolicina        decimal(18,3) NULL,        -- NULL = nije prebrojano
    Cena                      decimal(18,4) NOT NULL CONSTRAINT DF_SRV_PopisStavka_Cena DEFAULT (0),
    Napomena                  nvarchar(400) NULL,
    Izmenio                   nvarchar(50) NULL,
    DatumIzmene               datetime2(0) NULL,
    CONSTRAINT UQ_SRV_PopisStavka_Deo UNIQUE (PopisID, DeoID),
    CONSTRAINT CK_SRV_PopisStavka_Prebrojano CHECK (PrebrojanaKolicina IS NULL OR PrebrojanaKolicina BETWEEN 0 AND 999999999)
);
GO

IF OBJECT_ID('dbo.SRV_Korekcija', 'U') IS NULL
CREATE TABLE dbo.SRV_Korekcija
(
    ID               int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_Korekcija PRIMARY KEY,
    Broj             nvarchar(30)  NOT NULL,
    DeoID            int NOT NULL CONSTRAINT FK_SRV_Korekcija_Deo REFERENCES dbo.SRV_Deo (ID),
    Kolicina         decimal(18,3) NOT NULL,             -- + visak, - manjak
    Razlog           nvarchar(400) NOT NULL,
    Status           nvarchar(20)  NOT NULL CONSTRAINT DF_SRV_Korekcija_Status DEFAULT (N'Čeka odobrenje'),
    Podneo           nvarchar(50)  NOT NULL,
    Odlucio          nvarchar(50)  NULL,
    DatumOdluke      datetime2(0)  NULL,
    Kreirao          nvarchar(50) NULL,
    DatumKreiranja   datetime2(0) NOT NULL CONSTRAINT DF_SRV_Korekcija_DatumKreiranja DEFAULT (SYSDATETIME()),
    CONSTRAINT UQ_SRV_Korekcija_Broj UNIQUE (Broj),
    CONSTRAINT CK_SRV_Korekcija_Kolicina CHECK (Kolicina <> 0),
    CONSTRAINT CK_SRV_Korekcija_Razlog CHECK (LEN(Razlog) > 0),
    CONSTRAINT CK_SRV_Korekcija_Status CHECK (Status IN (N'Čeka odobrenje', N'Odobrena', N'Odbijena'))
);
GO

-- kartica dela: stanje = zbir kolicina (sa znakom), nikad rucno upisano
IF OBJECT_ID('dbo.SRV_DeoKretanje', 'U') IS NULL
CREATE TABLE dbo.SRV_DeoKretanje
(
    ID              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_DeoKretanje PRIMARY KEY,
    DeoID           int NOT NULL CONSTRAINT FK_SRV_DeoKretanje_Deo REFERENCES dbo.SRV_Deo (ID),
    Datum           datetime2(0) NOT NULL CONSTRAINT DF_SRV_DeoKretanje_Datum DEFAULT (SYSDATETIME()),
    Tip             nvarchar(20) NOT NULL,
    DokumentBroj    nvarchar(30) NOT NULL,
    Kolicina        decimal(18,3) NOT NULL,
    Cena            decimal(18,4) NOT NULL CONSTRAINT DF_SRV_DeoKretanje_Cena DEFAULT (0),
    NalogID         int NULL CONSTRAINT FK_SRV_DeoKretanje_Nalog REFERENCES dbo.SRV_Nalog (ID),
    NalogDeoID      int NULL CONSTRAINT FK_SRV_DeoKretanje_NalogDeo REFERENCES dbo.SRV_NalogDeo (ID),
    NarudzbenicaID  int NULL CONSTRAINT FK_SRV_DeoKretanje_Narudzbenica REFERENCES dbo.SRV_Narudzbenica (ID),
    PaSifra         int NULL,
    PopisID         int NULL CONSTRAINT FK_SRV_DeoKretanje_Popis REFERENCES dbo.SRV_Popis (ID),
    KorekcijaID     int NULL CONSTRAINT FK_SRV_DeoKretanje_Korekcija REFERENCES dbo.SRV_Korekcija (ID),
    Napomena        nvarchar(400) NULL,
    Korisnik        nvarchar(50) NULL,
    DatumKreiranja  datetime2(0) NOT NULL CONSTRAINT DF_SRV_DeoKretanje_DatumKreiranja DEFAULT (SYSDATETIME()),
    CONSTRAINT CK_SRV_DeoKretanje_Tip CHECK (Tip IN (N'Prijem', N'Izdavanje', N'Povrat', N'Popis', N'Početno stanje', N'Korekcija')),
    CONSTRAINT CK_SRV_DeoKretanje_Kolicina CHECK (Kolicina <> 0),
    CONSTRAINT CK_SRV_DeoKretanje_Cena CHECK (Cena >= 0)
);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_SRV_DeoKretanje_Deo' AND object_id = OBJECT_ID('dbo.SRV_DeoKretanje'))
    CREATE INDEX IX_SRV_DeoKretanje_Deo ON dbo.SRV_DeoKretanje (DeoID, Datum, ID) INCLUDE (Kolicina, Cena);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_SRV_DeoKretanje_Dokument' AND object_id = OBJECT_ID('dbo.SRV_DeoKretanje'))
    CREATE INDEX IX_SRV_DeoKretanje_Dokument ON dbo.SRV_DeoKretanje (DokumentBroj);
GO

-- obavestenja: delovi ispod minimuma, rokovi dokumenata, dospeli servisi
IF OBJECT_ID('dbo.SRV_DeoObavestenje', 'U') IS NULL
CREATE TABLE dbo.SRV_DeoObavestenje
(
    ID                  int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_DeoObavestenje PRIMARY KEY,
    Vreme               datetime2(0) NOT NULL CONSTRAINT DF_SRV_DeoObavestenje_Vreme DEFAULT (SYSDATETIME()),
    Vrsta               nvarchar(20) NOT NULL CONSTRAINT DF_SRV_DeoObavestenje_Vrsta DEFAULT (N'Deo'),
    DeoID               int NULL CONSTRAINT FK_SRV_DeoObavestenje_Deo REFERENCES dbo.SRV_Deo (ID),
    SredstvoID          int NULL CONSTRAINT FK_SRV_DeoObavestenje_Sredstvo REFERENCES dbo.SRV_Sredstvo (ID),
    SredstvoDokumentID  int NULL CONSTRAINT FK_SRV_DeoObavestenje_Dokument REFERENCES dbo.SRV_SredstvoDokument (ID),
    PlanID              int NULL CONSTRAINT FK_SRV_DeoObavestenje_Plan REFERENCES dbo.SRV_PlanOdrzavanja (ID),
    Tekst               nvarchar(400) NOT NULL,
    RaspolozivoUTrenutku decimal(18,3) NULL,
    Procitano           bit NOT NULL CONSTRAINT DF_SRV_DeoObavestenje_Procitano DEFAULT (0),
    ProcitaoKorisnik    nvarchar(50) NULL,
    VremeCitanja        datetime2(0) NULL,
    CONSTRAINT CK_SRV_DeoObavestenje_Vrsta CHECK (Vrsta IN (N'Deo', N'Dokument', N'Plan'))
);
GO

-- prag plana po sredstvu: obavestenje o dospelom servisu samo jednom po pragu (SR-17)
IF OBJECT_ID('dbo.SRV_PlanSredstvoPrag', 'U') IS NULL
CREATE TABLE dbo.SRV_PlanSredstvoPrag
(
    PlanID          int NOT NULL CONSTRAINT FK_SRV_PlanSredstvoPrag_Plan REFERENCES dbo.SRV_PlanOdrzavanja (ID),
    SredstvoID      int NOT NULL CONSTRAINT FK_SRV_PlanSredstvoPrag_Sredstvo REFERENCES dbo.SRV_Sredstvo (ID),
    ObavestenoPrag  nvarchar(20) NULL,
    CONSTRAINT PK_SRV_PlanSredstvoPrag PRIMARY KEY (PlanID, SredstvoID)
);
GO
