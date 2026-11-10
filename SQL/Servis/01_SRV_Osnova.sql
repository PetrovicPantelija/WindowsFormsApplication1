/* =====================================================================
   ZAH-010 Servis - 01: osnova modula
   SRV_Podesavanje, SRV_Brojac, SRV_Log, tipovi za TVP i pomocne funkcije.
   Baza: Jela_Test. Kompatibilno sa SQL Server 2008 R2.
   Postojece tabele se ne menjaju. Skripta se sme pokretati vise puta.
   ===================================================================== */
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET NOCOUNT ON;
GO

/* ---------------------------------------------------------------------
   Podesavanja (kljuc -> vrednost). Iznosi i grupe se ne pisu u kod.
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_Podesavanje', 'U') IS NULL
CREATE TABLE dbo.SRV_Podesavanje
(
    Kljuc        nvarchar(100) NOT NULL CONSTRAINT PK_SRV_Podesavanje PRIMARY KEY,
    Vrednost     nvarchar(400) NOT NULL,
    Opis         nvarchar(400) NULL,
    Izmenio      nvarchar(50)  NULL,
    DatumIzmene  datetime2(0)  NULL
);
GO

-- pocetne vrednosti; postojece vrednosti se ne prepisuju
DECLARE @p TABLE (Kljuc nvarchar(100), Vrednost nvarchar(400), Opis nvarchar(400));
INSERT INTO @p VALUES
 (N'Servis.Odobravanje.Nivo1',               N'100000',  N'Gornja granica nivoa 1 (RSD). Potvrđuje Jela (OP-J-02).'),
 (N'Servis.Odobravanje.Nivo2',               N'500000',  N'Gornja granica nivoa 2 (RSD); iznad je nivo 3. Potvrđuje Jela (OP-J-02).'),
 (N'Servis.Odobravanje.GrupaNivo1',          N'Šef tehničke službe', N'Naziv grupe korisnika (Grupe.Naziv) koja odobrava nivo 1, popis i korekcije.'),
 (N'Servis.Odobravanje.GrupaNivo2',          N'Tehnički direktor',   N'Naziv grupe korisnika koja odobrava nivo 2.'),
 (N'Servis.Odobravanje.GrupaNivo3',          N'Direktor',            N'Naziv grupe korisnika koja odobrava nivo 3.'),
 (N'Servis.Odobravanje.DozvoliIstogKorisnika', N'Ne',    N'Da = podnosilac sme da odobri svoj zahtev (samo uz pisanu odluku vlasnika); zapisuje se u log.'),
 (N'Servis.Delovi.FormatIzdatnice',          N'SIZ-{BROJ:0000}', NULL),
 (N'Servis.Delovi.FormatPrijema',            N'SPR-{BROJ:0000}', NULL),
 (N'Servis.Delovi.FormatPovrata',            N'SPV-{BROJ:0000}', NULL),
 (N'Servis.Delovi.FormatPocetnogStanja',     N'SPS-{BROJ:0000}', NULL),
 (N'Servis.Delovi.FormatNarudzbenice',       N'SNA-{GGGG}-{BROJ:0000}', NULL),
 (N'Servis.Delovi.FormatPopisa',             N'SPO-{GGGG}-{BROJ:0000}', NULL),
 (N'Servis.Delovi.FormatKorekcije',          N'SKO-{GGGG}-{BROJ:0000}', NULL),
 (N'Servis.FormatNaloga',                    N'SN-{GGGG}-{BROJ:0000}',  NULL),
 (N'Servis.FormatKvara',                     N'KV-{GGGG}-{BROJ:0000}',  NULL),
 (N'Servis.FormatZahteva',                   N'SSZ-{GGGG}-{BROJ:0000}', NULL),
 (N'Servis.Delovi.TolerancijaPrijema',       N'0',      N'Dozvoljeni prijem preko naručenog, u %, bez komentara.'),
 (N'Servis.Delovi.CenaNulaDozvoljena',       N'Ne',     N'Ne = cena 0 traži potvrdu korisnika.'),
 (N'Servis.KorenPriloga',                    N'',       N'Mrežna putanja za priloge kvarova (kao IC_KorenSlika).');

INSERT INTO dbo.SRV_Podesavanje (Kljuc, Vrednost, Opis)
SELECT p.Kljuc, p.Vrednost, p.Opis FROM @p p
WHERE NOT EXISTS (SELECT 1 FROM dbo.SRV_Podesavanje s WHERE s.Kljuc = p.Kljuc);
GO

/* ---------------------------------------------------------------------
   Brojac dokumenata. Godina = 0 kada format nema {GGGG} (broj tece stalno).
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_Brojac', 'U') IS NULL
CREATE TABLE dbo.SRV_Brojac
(
    Vrsta          varchar(30) NOT NULL,
    Godina         int         NOT NULL,
    PoslednjiBroj  int         NOT NULL CONSTRAINT DF_SRV_Brojac_PoslednjiBroj DEFAULT (0),
    CONSTRAINT PK_SRV_Brojac PRIMARY KEY (Vrsta, Godina)
);
GO

/* ---------------------------------------------------------------------
   Log izmena (sredstvo, deo, dokument, status, odobravanja, greske).
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_Log', 'U') IS NULL
CREATE TABLE dbo.SRV_Log
(
    ID        int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SRV_Log PRIMARY KEY,
    Tabela    varchar(50)    NOT NULL,
    ZapisID   int            NULL,
    Opis      nvarchar(200)  NOT NULL,
    Staro     nvarchar(max)  NULL,
    Novo      nvarchar(max)  NULL,
    Korisnik  nvarchar(50)   NULL,
    Vreme     datetime2(0)   NOT NULL CONSTRAINT DF_SRV_Log_Vreme DEFAULT (SYSDATETIME())
);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_SRV_Log_Tabela' AND object_id = OBJECT_ID('dbo.SRV_Log'))
    CREATE INDEX IX_SRV_Log_Tabela ON dbo.SRV_Log (Tabela, ZapisID);
GO

/* ---------------------------------------------------------------------
   Tipovi za TVP (stavke dokumenata, liste ID-jeva, tekstova, polja)
   --------------------------------------------------------------------- */
IF TYPE_ID('dbo.SRV_StavkaTip') IS NULL
    CREATE TYPE dbo.SRV_StavkaTip AS TABLE
    (
        DeoID     int            NOT NULL,
        Kolicina  decimal(18,3)  NOT NULL,
        Cena      decimal(18,4)  NULL,
        Napomena  nvarchar(400)  NULL
    );
GO
IF TYPE_ID('dbo.SRV_IdTip') IS NULL
    CREATE TYPE dbo.SRV_IdTip AS TABLE (ID int NOT NULL PRIMARY KEY);
GO
IF TYPE_ID('dbo.SRV_TekstTip') IS NULL
    CREATE TYPE dbo.SRV_TekstTip AS TABLE (Tekst nvarchar(100) NOT NULL);
GO
IF TYPE_ID('dbo.SRV_PoljeVrednostTip') IS NULL
    CREATE TYPE dbo.SRV_PoljeVrednostTip AS TABLE (PoljeID int NOT NULL PRIMARY KEY, Vrednost nvarchar(400) NULL);
GO

/* ---------------------------------------------------------------------
   Pomocne funkcije
   --------------------------------------------------------------------- */

-- vrednost podesavanja (NULL ako kljuc ne postoji)
IF OBJECT_ID('dbo.SRV_Pod', 'FN') IS NOT NULL DROP FUNCTION dbo.SRV_Pod;
GO
CREATE FUNCTION dbo.SRV_Pod (@Kljuc nvarchar(100))
RETURNS nvarchar(400)
AS
BEGIN
    RETURN (SELECT Vrednost FROM dbo.SRV_Podesavanje WHERE Kljuc = @Kljuc);
END
GO

-- broj u lokalnom formatu za poruke: 1234.5 -> 1.234,50
IF OBJECT_ID('dbo.SRV_Broj', 'FN') IS NOT NULL DROP FUNCTION dbo.SRV_Broj;
GO
CREATE FUNCTION dbo.SRV_Broj (@x decimal(18,4))
RETURNS nvarchar(40)
AS
BEGIN
    DECLARE @s varchar(40) = CONVERT(varchar(40), CAST(ISNULL(@x, 0) AS money), 1);   -- 1,234.50
    RETURN REPLACE(REPLACE(REPLACE(@s, ',', '#'), '.', ','), '#', '.');
END
GO

-- nivo odobrenja po iznosu: 0 = odbija se, 1, 2, 3
IF OBJECT_ID('dbo.SRV_NivoOdobrenja', 'FN') IS NOT NULL DROP FUNCTION dbo.SRV_NivoOdobrenja;
GO
CREATE FUNCTION dbo.SRV_NivoOdobrenja (@Iznos decimal(18,2))
RETURNS tinyint
AS
BEGIN
    DECLARE @n1 decimal(18,2) = CAST(dbo.SRV_Pod(N'Servis.Odobravanje.Nivo1') AS decimal(18,2)),
            @n2 decimal(18,2) = CAST(dbo.SRV_Pod(N'Servis.Odobravanje.Nivo2') AS decimal(18,2));
    RETURN CASE WHEN ISNULL(@Iznos, 0) <= 0 THEN 0
                WHEN @Iznos <= @n1 THEN 1
                WHEN @Iznos <= @n2 THEN 2
                ELSE 3 END;
END
GO

-- da li je korisnik clan grupe koja odobrava dati nivo (ili visi nivo)
IF OBJECT_ID('dbo.SRV_MozeDaOdobri', 'FN') IS NOT NULL DROP FUNCTION dbo.SRV_MozeDaOdobri;
GO
CREATE FUNCTION dbo.SRV_MozeDaOdobri (@Korisnik nvarchar(50), @Nivo tinyint)
RETURNS bit
AS
BEGIN
    IF EXISTS (SELECT 1
               FROM dbo.KorisnikGrupa kg
               JOIN dbo.Grupe g ON g.IdGrupe = kg.IdGrupe
               WHERE RTRIM(kg.Korisnik) = RTRIM(@Korisnik)
                 AND (   (@Nivo <= 1 AND RTRIM(g.Naziv) = dbo.SRV_Pod(N'Servis.Odobravanje.GrupaNivo1'))
                      OR (@Nivo <= 2 AND RTRIM(g.Naziv) = dbo.SRV_Pod(N'Servis.Odobravanje.GrupaNivo2'))
                      OR (               RTRIM(g.Naziv) = dbo.SRV_Pod(N'Servis.Odobravanje.GrupaNivo3'))))
        RETURN 1;
    RETURN 0;
END
GO

/* ---------------------------------------------------------------------
   SRV_SledeciBroj: sledeci broj dokumenta po formatu iz podesavanja.
   Poziva se UNUTAR transakcije upisa (zakljucava red brojaca do COMMIT),
   pa nema rupa ni duplikata kod paralelnog rada.
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_SledeciBroj', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_SledeciBroj;
GO
CREATE PROCEDURE dbo.SRV_SledeciBroj
    @Vrsta varchar(30),            -- Izdatnica, Prijem, Povrat, PocetnoStanje, Narudzbenica, Popis, Korekcija, Nalog, Kvar, Zahtev
    @Broj  nvarchar(30) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Kljuc nvarchar(100) = CASE @Vrsta
            WHEN 'Izdatnica'     THEN N'Servis.Delovi.FormatIzdatnice'
            WHEN 'Prijem'        THEN N'Servis.Delovi.FormatPrijema'
            WHEN 'Povrat'        THEN N'Servis.Delovi.FormatPovrata'
            WHEN 'PocetnoStanje' THEN N'Servis.Delovi.FormatPocetnogStanja'
            WHEN 'Narudzbenica'  THEN N'Servis.Delovi.FormatNarudzbenice'
            WHEN 'Popis'         THEN N'Servis.Delovi.FormatPopisa'
            WHEN 'Korekcija'     THEN N'Servis.Delovi.FormatKorekcije'
            WHEN 'Nalog'         THEN N'Servis.FormatNaloga'
            WHEN 'Kvar'          THEN N'Servis.FormatKvara'
            WHEN 'Zahtev'        THEN N'Servis.FormatZahteva'
        END;
    DECLARE @Format nvarchar(400) = dbo.SRV_Pod(@Kljuc);
    IF @Format IS NULL
    BEGIN
        RAISERROR(N'Nije podešen format broja dokumenta (%s).', 16, 1, @Vrsta);
        RETURN 1;
    END

    DECLARE @God int = YEAR(GETDATE());
    DECLARE @GodBrojaca int = CASE WHEN CHARINDEX(N'{GGGG}', @Format) > 0 THEN @God ELSE 0 END;
    DECLARE @n int;

    BEGIN TRAN;
    IF NOT EXISTS (SELECT 1 FROM dbo.SRV_Brojac WITH (UPDLOCK, HOLDLOCK) WHERE Vrsta = @Vrsta AND Godina = @GodBrojaca)
        INSERT INTO dbo.SRV_Brojac (Vrsta, Godina, PoslednjiBroj) VALUES (@Vrsta, @GodBrojaca, 0);

    UPDATE dbo.SRV_Brojac WITH (UPDLOCK, HOLDLOCK)
    SET @n = PoslednjiBroj = PoslednjiBroj + 1
    WHERE Vrsta = @Vrsta AND Godina = @GodBrojaca;
    COMMIT;

    -- uvek najmanje 4 cifre
    SET @Broj = REPLACE(REPLACE(@Format, N'{GGGG}', CAST(@God AS nvarchar(4))),
                        N'{BROJ:0000}',
                        CASE WHEN @n < 10000 THEN RIGHT(N'0000' + CAST(@n AS nvarchar(10)), 4) ELSE CAST(@n AS nvarchar(10)) END);
    RETURN 0;
END
GO

/* ---------------------------------------------------------------------
   SRV_PonoviGresku: iz CATCH bloka ponovo javlja gresku (2008 R2 nema THROW).
   Nase poruke (50000) idu kakve jesu; sistemske dobijaju oznaku #SQL<broj>#
   da bi aplikacija po broju i nazivu ogranicenja (UQ_/CK_/FK_) dala srpsku poruku.
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_PonoviGresku', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_PonoviGresku;
GO
CREATE PROCEDURE dbo.SRV_PonoviGresku
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @n int = ERROR_NUMBER(), @m nvarchar(2048) = ERROR_MESSAGE();
    IF @n IS NULL RETURN;
    IF @n <> 50000 SET @m = N'#SQL' + CAST(@n AS nvarchar(10)) + N'# ' + @m;
    RAISERROR(N'%s', 16, 1, @m);
END
GO

/* ---------------------------------------------------------------------
   SRV_ProveriOdobravaoca: razdvajanje duznosti (SR-13) i grupa nivoa.
   Poziva se PRE BEGIN TRAN; vraca 1 i javlja gresku ako odobrenje nije dozvoljeno.
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_ProveriOdobravaoca', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_ProveriOdobravaoca;
GO
CREATE PROCEDURE dbo.SRV_ProveriOdobravaoca
    @Korisnik nvarchar(50),
    @Podneo   nvarchar(50),
    @Nivo     tinyint,
    @Tabela   varchar(50),
    @ZapisID  int
AS
BEGIN
    SET NOCOUNT ON;

    IF RTRIM(ISNULL(@Podneo, N'')) = RTRIM(@Korisnik)
    BEGIN
        IF ISNULL(dbo.SRV_Pod(N'Servis.Odobravanje.DozvoliIstogKorisnika'), N'Ne') <> N'Da'
        BEGIN
            DECLARE @k nvarchar(50) = RTRIM(@Korisnik);
            RAISERROR(N'Zahtev ne sme da odobri isti korisnik koji ga je podneo (%s). Odobrenje treba da da drugi ovlašćeni korisnik.', 16, 1, @k);
            RETURN 1;
        END
        INSERT INTO dbo.SRV_Log (Tabela, ZapisID, Opis, Korisnik)
        VALUES (@Tabela, @ZapisID, N'Odobrio isti korisnik koji je podneo (dozvoljeno podešavanjem)', @Korisnik);
    END

    IF dbo.SRV_MozeDaOdobri(@Korisnik, @Nivo) = 0
    BEGIN
        DECLARE @Grupa nvarchar(400) = dbo.SRV_Pod(N'Servis.Odobravanje.GrupaNivo' + CAST(CASE WHEN @Nivo < 1 THEN 1 ELSE @Nivo END AS nvarchar(1)));
        DECLARE @n int = @Nivo;
        RAISERROR(N'Za odobrenje nivoa %d potrebno je članstvo u grupi „%s“ (ili višoj).', 16, 1, @n, @Grupa);
        RETURN 1;
    END
    RETURN 0;
END
GO
