/* =====================================================================
   ZAH-008 Izracun cena - 04: procedure
   Baza: Jela_Test. Kompatibilno sa SQL Server 2008 R2.
   Pokrenuti posle 03_IC_Funkcije_Pogledi.sql.

   Pravila za sve procedure:
     - SET XACT_ABORT ON: svaka SQL greska ponistava celu transakciju;
     - provere ulaza su PRE BEGIN TRAN, greska se javlja sa RAISERROR i RETURN;
     - procedure se mogu pozivati jedna iz druge (ugnjezdene transakcije).
   ===================================================================== */
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

/* =====================================================================
   SIFARNICI
   ===================================================================== */

/* IC_OsveziProgrameIKolekcije
   Upisuje nove nazive iz jela_products u Program i Kolekcije.
   Postojece redove ne brise i ne prenumerise.                             */
IF OBJECT_ID('dbo.IC_OsveziProgrameIKolekcije', 'P') IS NOT NULL DROP PROCEDURE dbo.IC_OsveziProgrameIKolekcije;
GO
CREATE PROCEDURE dbo.IC_OsveziProgrameIKolekcije
    @BezRezultata bit = 0      -- 1 kada se poziva iz druge procedure
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @NoviProgrami int, @NoveKolekcije int;

    BEGIN TRAN;

    -- novi programi
    SELECT DISTINCT LTRIM(RTRIM(p.NazivRobneGrupe)) AS Naziv
    INTO #NoviP
    FROM dbo.jela_products p
    WHERE LTRIM(RTRIM(ISNULL(p.NazivRobneGrupe, ''))) <> ''
      AND NOT EXISTS (SELECT 1 FROM dbo.Program g WITH (UPDLOCK, HOLDLOCK)
                      WHERE g.Naziv = LTRIM(RTRIM(p.NazivRobneGrupe)));

    -- ID je IDENTITY u novoj tabeli; postojeca tabela mozda nema IDENTITY, pa ID = MAX + 1
    IF COLUMNPROPERTY(OBJECT_ID('dbo.Program'), 'ID', 'IsIdentity') = 1
        INSERT INTO dbo.Program (Naziv) SELECT Naziv FROM #NoviP;
    ELSE
        INSERT INTO dbo.Program (ID, Naziv)
        SELECT (SELECT ISNULL(MAX(ID), 0) FROM dbo.Program WITH (UPDLOCK, HOLDLOCK))
                 + ROW_NUMBER() OVER (ORDER BY Naziv), Naziv
        FROM #NoviP;
    SET @NoviProgrami = @@ROWCOUNT;

    -- nove kolekcije
    SELECT DISTINCT LTRIM(RTRIM(p.NazivGrupe)) AS Naziv, g.ID AS ProgramID
    INTO #NoveK
    FROM dbo.jela_products p
    JOIN dbo.Program g ON g.Naziv = LTRIM(RTRIM(p.NazivRobneGrupe))
    WHERE LTRIM(RTRIM(ISNULL(p.NazivGrupe, ''))) <> ''
      AND NOT EXISTS (SELECT 1 FROM dbo.Kolekcije k WITH (UPDLOCK, HOLDLOCK)
                      WHERE k.Naziv = LTRIM(RTRIM(p.NazivGrupe)) AND k.ProgramID = g.ID);

    IF COLUMNPROPERTY(OBJECT_ID('dbo.Kolekcije'), 'ID', 'IsIdentity') = 1
        INSERT INTO dbo.Kolekcije (Naziv, ProgramID) SELECT Naziv, ProgramID FROM #NoveK;
    ELSE
        INSERT INTO dbo.Kolekcije (ID, Naziv, ProgramID)
        SELECT (SELECT ISNULL(MAX(ID), 0) FROM dbo.Kolekcije WITH (UPDLOCK, HOLDLOCK))
                 + ROW_NUMBER() OVER (ORDER BY ProgramID, Naziv), Naziv, ProgramID
        FROM #NoveK;
    SET @NoveKolekcije = @@ROWCOUNT;

    COMMIT;

    IF @BezRezultata = 0
        SELECT @NoviProgrami AS NoviProgrami, @NoveKolekcije AS NoveKolekcije;
END
GO

/* =====================================================================
   TROSKOVI
   ===================================================================== */

/* IC_AzurirajTroskove
   TP-Novi iz poslednjeg vazeceg reda KalkCenik (KcValid = 1, KcTipCn = 'G').
   TP-Stari ne dira. Red bez vazeceg troska zadrzava staru vrednost i
   dobija BezTroska = 1. Vraca spisak redova bez troska.                    */
IF OBJECT_ID('dbo.IC_AzurirajTroskove', 'P') IS NOT NULL DROP PROCEDURE dbo.IC_AzurirajTroskove;
GO
CREATE PROCEDURE dbo.IC_AzurirajTroskove
    @CenovnikID       int,
    @DodatakMaterijal decimal(6,4) = 1.03
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF EXISTS (SELECT 1 FROM dbo.IC_Cenovnik WHERE CenovnikID = @CenovnikID AND Vrsta = 'Vazeci')
    BEGIN
        RAISERROR(N'Važeći cenovnik je zamrznut; troškovi se ne ažuriraju.', 16, 1);
        RETURN;
    END

    CREATE TABLE #Novi (StavkaID int PRIMARY KEY, Ukupno decimal(18,2) NULL, KcDatSpremembe datetime NULL);

    INSERT INTO #Novi (StavkaID, Ukupno, KcDatSpremembe)
    SELECT s.StavkaID, k.Ukupno, k.KcDatSpremembe
    FROM dbo.IC_CenovnikStavka s
    OUTER APPLY (
        SELECT TOP (1)
               c.KcCena_D + c.KcCena_P + c.KcFStr + c.KcCena_DD
                 + c.KcCena_M * @DodatakMaterijal AS Ukupno,
               c.KcDatSpremembe
        FROM dbo.KalkCenik c
        WHERE c.KcSifMp = s.LargoProizvodID
          AND c.KcValid = 1
          AND c.KcTipCn = 'G'
        ORDER BY c.KcDatSpremembe DESC
    ) k
    WHERE s.CenovnikID = @CenovnikID;

    BEGIN TRAN;

    UPDATE s SET
        TrosakNovi      = CASE WHEN n.Ukupno > 0 THEN n.Ukupno         ELSE s.TrosakNovi      END,
        DatumTroskaNovi = CASE WHEN n.Ukupno > 0 THEN n.KcDatSpremembe ELSE s.DatumTroskaNovi END,
        BezTroska       = CASE WHEN n.Ukupno > 0 THEN 0 ELSE 1 END
    FROM dbo.IC_CenovnikStavka s
    JOIN #Novi n ON n.StavkaID = s.StavkaID;

    UPDATE dbo.IC_Cenovnik SET DatumAzuriranjaTroska = SYSDATETIME()
    WHERE CenovnikID = @CenovnikID;

    COMMIT;

    -- redovi bez vazeceg troska, za poruku u formi
    SELECT s.LargoProizvodID, s.SifraProizvodaLargo, s.Code, s.NazivProizvoda
    FROM #Novi n
    JOIN dbo.IC_CenovnikStavka s ON s.StavkaID = n.StavkaID
    WHERE n.Ukupno IS NULL OR n.Ukupno <= 0
    ORDER BY s.LargoProizvodID;
END
GO

/* IC_ProglasiTroskoveNovim
   TP-Novi -> TP-Stari, trenutna marza -> Marza stara.
   @SamoBroj = 1 samo vraca broj redova koji ce se promeniti (za potvrdu). */
IF OBJECT_ID('dbo.IC_ProglasiTroskoveNovim', 'P') IS NOT NULL DROP PROCEDURE dbo.IC_ProglasiTroskoveNovim;
GO
CREATE PROCEDURE dbo.IC_ProglasiTroskoveNovim
    @CenovnikID int,
    @Korisnik   nvarchar(50),
    @SamoBroj   bit = 0
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF @SamoBroj = 1
    BEGIN
        SELECT COUNT(*) AS BrojRedova
        FROM dbo.IC_CenovnikStavka s
        WHERE s.CenovnikID = @CenovnikID
          AND s.TrosakNovi > 0
          AND (s.TrosakStari IS NULL OR s.TrosakStari <> s.TrosakNovi);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM dbo.IC_Cenovnik WHERE CenovnikID = @CenovnikID AND Vrsta = 'Opsti')
    BEGIN
        RAISERROR(N'Troškovi se proglašavaju novim samo u opštem cenovniku.', 16, 1);
        RETURN;
    END

    DECLARE @Broj int;

    BEGIN TRAN;

    UPDATE s SET
        TrosakStari      = s.TrosakNovi,
        DatumTroskaStari = s.DatumTroskaNovi,
        MarzaStara       = ROUND(o.Marza, 4)
    FROM dbo.IC_CenovnikStavka s
    JOIN dbo.vw_IC_CenovnikObracun o ON o.StavkaID = s.StavkaID
    WHERE s.CenovnikID = @CenovnikID
      AND s.TrosakNovi > 0;
    SET @Broj = @@ROWCOUNT;

    UPDATE dbo.IC_Cenovnik
    SET DatumProglasenjaTroska = SYSDATETIME(), Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
    WHERE CenovnikID = @CenovnikID;

    COMMIT;

    SELECT @Broj AS BrojRedova;
END
GO

/* =====================================================================
   CUVANJE IZMENA IZ GRIDA
   ===================================================================== */

/* IC_SacuvajZaglavlje: kurs i PDV (administrator, opsti cenovnik) */
IF OBJECT_ID('dbo.IC_SacuvajZaglavlje', 'P') IS NOT NULL DROP PROCEDURE dbo.IC_SacuvajZaglavlje;
GO
CREATE PROCEDURE dbo.IC_SacuvajZaglavlje
    @CenovnikID int,
    @Kurs       decimal(9,2),
    @PDV        decimal(5,4),
    @Korisnik   nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF @Kurs IS NULL OR @Kurs < 50 OR @Kurs > 200
    BEGIN
        RAISERROR(N'Kurs mora biti između 50 i 200.', 16, 1);
        RETURN;
    END
    IF @PDV IS NULL OR @PDV < 0 OR @PDV >= 1
    BEGIN
        RAISERROR(N'PDV mora biti između 0 i 100%%.', 16, 1);
        RETURN;
    END
    IF EXISTS (SELECT 1 FROM dbo.IC_Cenovnik WHERE CenovnikID = @CenovnikID AND Vrsta = 'Vazeci')
    BEGIN
        RAISERROR(N'Važeći cenovnik je zamrznut.', 16, 1);
        RETURN;
    END

    UPDATE dbo.IC_Cenovnik
    SET Kurs = @Kurs, PDV = @PDV, Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
    WHERE CenovnikID = @CenovnikID
      AND (Kurs <> @Kurs OR PDV <> @PDV);
END
GO

/* IC_SacuvajCene: izmenjene VP po Code iz grida */
IF OBJECT_ID('dbo.IC_SacuvajCene', 'P') IS NOT NULL DROP PROCEDURE dbo.IC_SacuvajCene;
GO
CREATE PROCEDURE dbo.IC_SacuvajCene
    @CenovnikID int,
    @Cene       dbo.IC_CenaTip READONLY,
    @Korisnik   nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF EXISTS (SELECT 1 FROM dbo.IC_Cenovnik WHERE CenovnikID = @CenovnikID AND Vrsta = 'Vazeci')
    BEGIN
        RAISERROR(N'Važeći cenovnik je zamrznut.', 16, 1);
        RETURN;
    END
    IF EXISTS (SELECT 1 FROM @Cene WHERE VP <= 0)
    BEGIN
        RAISERROR(N'VP cena mora biti veća od nule.', 16, 1);
        RETURN;
    END

    DECLARE @Broj int;

    BEGIN TRAN;

    UPDATE c SET
        VPAktuelna  = u.VPAktuelna,
        PovecanjeVP = u.PovecanjeVP,
        VP          = ROUND(u.VP, 0),
        VPIzBaze    = 0,
        Izmenio     = @Korisnik,
        DatumIzmene = SYSDATETIME()
    FROM dbo.IC_CenovnikCena c
    JOIN @Cene u ON u.Code = c.Code
    WHERE c.CenovnikID = @CenovnikID;
    SET @Broj = @@ROWCOUNT;

    IF @Broj > 0
        UPDATE dbo.IC_Cenovnik SET Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
        WHERE CenovnikID = @CenovnikID;

    COMMIT;

    SELECT @Broj AS BrojSacuvanih;
END
GO

/* IC_SacuvajParametre
   Rabat i vazeca akcija po nivou (Cenovnik / Program / Kolekcija / Code).
   PostaviRabat/PostaviAkciju = 0 znaci "ne diraj tu vrednost".
   @PregaziNizeNivoe = 1 (Primeni na prikazane): vrednost postavljena na
   visem nivou brise istu vrednost na nizim nivoima unutar njega, da bi
   zaista vazila za sve prikazane redove.                                   */
IF OBJECT_ID('dbo.IC_SacuvajParametre', 'P') IS NOT NULL DROP PROCEDURE dbo.IC_SacuvajParametre;
GO
CREATE PROCEDURE dbo.IC_SacuvajParametre
    @CenovnikID       int,
    @Parametri        dbo.IC_ParametarTip READONLY,
    @Korisnik         nvarchar(50),
    @PregaziNizeNivoe bit = 0
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF EXISTS (SELECT 1 FROM dbo.IC_Cenovnik WHERE CenovnikID = @CenovnikID AND Vrsta = 'Vazeci')
    BEGIN
        RAISERROR(N'Važeći cenovnik je zamrznut.', 16, 1);
        RETURN;
    END
    IF EXISTS (SELECT 1 FROM @Parametri
               WHERE Rabat NOT BETWEEN 0 AND 1 OR Akcija NOT BETWEEN 0 AND 1)
    BEGIN
        RAISERROR(N'Rabat i akcija moraju biti između 0 i 100%%.', 16, 1);
        RETURN;
    END

    BEGIN TRAN;

    IF @PregaziNizeNivoe = 1
    BEGIN
        -- nizi parametri koje pokriva neki ulazni red (preko stavki cenovnika)
        SELECT p.ParametarID,
               MAX(CAST(u.PostaviRabat  AS int)) AS BrisiRabat,
               MAX(CAST(u.PostaviAkciju AS int)) AS BrisiAkciju
        INTO #Pokriveni
        FROM dbo.IC_CenovnikParametar p
        JOIN @Parametri u
          ON CASE p.Nivo WHEN 'Code' THEN 1 WHEN 'Kolekcija' THEN 2 WHEN 'Program' THEN 3 ELSE 4 END
           < CASE u.Nivo WHEN 'Code' THEN 1 WHEN 'Kolekcija' THEN 2 WHEN 'Program' THEN 3 ELSE 4 END
        WHERE p.CenovnikID = @CenovnikID
          AND EXISTS (SELECT 1 FROM dbo.IC_CenovnikStavka st
                      WHERE st.CenovnikID = @CenovnikID
                        AND (   (p.Nivo = 'Program'   AND st.ProgramID   = p.ProgramID)
                             OR (p.Nivo = 'Kolekcija' AND st.KolekcijaID = p.KolekcijaID)
                             OR (p.Nivo = 'Code'      AND st.Code        = p.Code))
                        AND (   u.Nivo = 'Cenovnik'
                             OR (u.Nivo = 'Program'   AND st.ProgramID   = u.ProgramID)
                             OR (u.Nivo = 'Kolekcija' AND st.KolekcijaID = u.KolekcijaID)))
        GROUP BY p.ParametarID;

        UPDATE p SET
            Rabat  = CASE WHEN x.BrisiRabat  = 1 THEN NULL ELSE p.Rabat  END,
            Akcija = CASE WHEN x.BrisiAkciju = 1 THEN NULL ELSE p.Akcija END
        FROM dbo.IC_CenovnikParametar p
        JOIN #Pokriveni x ON x.ParametarID = p.ParametarID;
    END

    -- postojeci parametri (bez MERGE, radi nivoa kompatibilnosti baze)
    UPDATE t SET
        Rabat  = CASE WHEN u.PostaviRabat  = 1 THEN u.Rabat  ELSE t.Rabat  END,
        Akcija = CASE WHEN u.PostaviAkciju = 1 THEN u.Akcija ELSE t.Akcija END
    FROM dbo.IC_CenovnikParametar t
    JOIN @Parametri u
      ON t.Nivo = u.Nivo
     AND ISNULL(t.ProgramID, -1)   = ISNULL(u.ProgramID, -1)
     AND ISNULL(t.KolekcijaID, -1) = ISNULL(u.KolekcijaID, -1)
     AND ISNULL(t.Code, N'')       = ISNULL(u.Code, N'')
    WHERE t.CenovnikID = @CenovnikID;

    -- novi parametri
    INSERT INTO dbo.IC_CenovnikParametar (CenovnikID, Nivo, ProgramID, KolekcijaID, Code, Rabat, Akcija)
    SELECT @CenovnikID, u.Nivo, u.ProgramID, u.KolekcijaID, u.Code,
           CASE WHEN u.PostaviRabat  = 1 THEN u.Rabat  END,
           CASE WHEN u.PostaviAkciju = 1 THEN u.Akcija END
    FROM @Parametri u
    WHERE NOT EXISTS (SELECT 1 FROM dbo.IC_CenovnikParametar t
                      WHERE t.CenovnikID = @CenovnikID
                        AND t.Nivo = u.Nivo
                        AND ISNULL(t.ProgramID, -1)   = ISNULL(u.ProgramID, -1)
                        AND ISNULL(t.KolekcijaID, -1) = ISNULL(u.KolekcijaID, -1)
                        AND ISNULL(t.Code, N'')       = ISNULL(u.Code, N''));

    -- prazni parametri (sve nasledjuje, nema akcija za poredjenje) se brisu
    DELETE p
    FROM dbo.IC_CenovnikParametar p
    WHERE p.CenovnikID = @CenovnikID
      AND p.Rabat IS NULL AND p.Akcija IS NULL
      AND NOT EXISTS (SELECT 1 FROM dbo.IC_CenovnikAkcija a WHERE a.ParametarID = p.ParametarID);

    UPDATE dbo.IC_Cenovnik SET Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
    WHERE CenovnikID = @CenovnikID;

    COMMIT;
END
GO

/* =====================================================================
   RADNA KOPIJA I VAZECI CENOVNIK
   ===================================================================== */

/* IC_KopirajSadrzaj (interna)
   Kopira cene, stavke, parametre i akcije iz jednog cenovnika u drugi,
   prazan cenovnik. Transakciju otvara pozivalac.
   @ZadrziIzmenio = 0: cene u kopiji nemaju oznaku izmene (radna kopija),
   pa se kasnije zna koje je Code menjao drugi korisnik.                    */
IF OBJECT_ID('dbo.IC_KopirajSadrzaj', 'P') IS NOT NULL DROP PROCEDURE dbo.IC_KopirajSadrzaj;
GO
CREATE PROCEDURE dbo.IC_KopirajSadrzaj
    @IzvorID       int,
    @CiljID        int,
    @ZadrziIzmenio bit
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    INSERT INTO dbo.IC_CenovnikCena (CenovnikID, Code, VPAktuelna, PovecanjeVP, VP, VPIzBaze, Izmenio, DatumIzmene)
    SELECT @CiljID, Code, VPAktuelna, PovecanjeVP, VP, VPIzBaze,
           CASE WHEN @ZadrziIzmenio = 1 THEN Izmenio END,
           CASE WHEN @ZadrziIzmenio = 1 THEN DatumIzmene END
    FROM dbo.IC_CenovnikCena
    WHERE CenovnikID = @IzvorID;

    INSERT INTO dbo.IC_CenovnikStavka (CenovnikID, LargoProizvodID, SifraProizvodaLargo, Code, ProgramID, KolekcijaID,
                                       NazivProizvoda, TrosakStari, TrosakNovi, DatumTroskaStari, DatumTroskaNovi,
                                       MarzaStara, BezTroska)
    SELECT @CiljID, LargoProizvodID, SifraProizvodaLargo, Code, ProgramID, KolekcijaID,
           NazivProizvoda, TrosakStari, TrosakNovi, DatumTroskaStari, DatumTroskaNovi,
           MarzaStara, BezTroska
    FROM dbo.IC_CenovnikStavka
    WHERE CenovnikID = @IzvorID;

    INSERT INTO dbo.IC_CenovnikParametar (CenovnikID, Nivo, ProgramID, KolekcijaID, Code, Rabat, Akcija)
    SELECT @CiljID, Nivo, ProgramID, KolekcijaID, Code, Rabat, Akcija
    FROM dbo.IC_CenovnikParametar
    WHERE CenovnikID = @IzvorID;

    -- akcije za poredjenje: stari -> novi parametar po prirodnom kljucu (UQ_IC_Param)
    INSERT INTO dbo.IC_CenovnikAkcija (ParametarID, Redosled, Procenat)
    SELECT n.ParametarID, a.Redosled, a.Procenat
    FROM dbo.IC_CenovnikAkcija a
    JOIN dbo.IC_CenovnikParametar s ON s.ParametarID = a.ParametarID AND s.CenovnikID = @IzvorID
    JOIN dbo.IC_CenovnikParametar n ON n.CenovnikID = @CiljID
                                   AND n.Nivo = s.Nivo
                                   AND ISNULL(n.ProgramID, -1)   = ISNULL(s.ProgramID, -1)
                                   AND ISNULL(n.KolekcijaID, -1) = ISNULL(s.KolekcijaID, -1)
                                   AND ISNULL(n.Code, N'')       = ISNULL(s.Code, N'');
END
GO

/* IC_NapraviRadniCenovnik
   Radna kopija za drugog korisnika, od opsteg (ili vazeceg, za
   "Kopiraj u novu radnu kopiju").                                          */
IF OBJECT_ID('dbo.IC_NapraviRadniCenovnik', 'P') IS NOT NULL DROP PROCEDURE dbo.IC_NapraviRadniCenovnik;
GO
CREATE PROCEDURE dbo.IC_NapraviRadniCenovnik
    @IzvorniCenovnikID int,
    @Naziv             nvarchar(100),
    @Korisnik          nvarchar(50),
    @CenovnikID        int OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    SET @CenovnikID = NULL;

    IF NOT EXISTS (SELECT 1 FROM dbo.IC_Cenovnik WHERE CenovnikID = @IzvorniCenovnikID)
    BEGIN
        RAISERROR(N'Izvorni cenovnik ne postoji.', 16, 1);
        RETURN;
    END
    IF LTRIM(RTRIM(ISNULL(@Naziv, N''))) = N''
    BEGIN
        RAISERROR(N'Naziv radne kopije je obavezan.', 16, 1);
        RETURN;
    END
    IF EXISTS (SELECT 1 FROM dbo.IC_Cenovnik WHERE Naziv = @Naziv)
    BEGIN
        RAISERROR(N'Cenovnik sa nazivom "%s" već postoji.', 16, 1, @Naziv);
        RETURN;
    END

    BEGIN TRAN;

    -- vazeci izvor: radna kopija se i dalje vezuje za njegov opsti cenovnik
    INSERT INTO dbo.IC_Cenovnik (Naziv, Vrsta, Valuta, Kurs, PDV, IzvorniCenovnikID, PaSifra,
                                 DatumAzuriranjaTroska, DatumProglasenjaTroska, DatumPreuzimanjaIzvora, Kreirao)
    SELECT @Naziv, 'Radni', i.Valuta, i.Kurs, i.PDV,
           CASE WHEN i.Vrsta = 'Opsti' THEN i.CenovnikID ELSE ISNULL(i.IzvorniCenovnikID, i.CenovnikID) END,
           i.PaSifra, i.DatumAzuriranjaTroska, i.DatumProglasenjaTroska, SYSDATETIME(), @Korisnik
    FROM dbo.IC_Cenovnik i
    WHERE i.CenovnikID = @IzvorniCenovnikID;

    SET @CenovnikID = SCOPE_IDENTITY();

    EXEC dbo.IC_KopirajSadrzaj @IzvorID = @IzvorniCenovnikID, @CiljID = @CenovnikID, @ZadrziIzmenio = 0;

    COMMIT;
END
GO

/* IC_PreuzmiIzmeneOpsteg
   Prenosi iz opsteg cenovnika u radnu kopiju:
     - kurs i PDV;
     - troskove i marzu staru za sve redove;
     - VP za Code koje drugi korisnik nije menjao (Izmenio IS NULL);
     - nove Code i redove koji su u opstem dodati posle kopiranja.          */
IF OBJECT_ID('dbo.IC_PreuzmiIzmeneOpsteg', 'P') IS NOT NULL DROP PROCEDURE dbo.IC_PreuzmiIzmeneOpsteg;
GO
CREATE PROCEDURE dbo.IC_PreuzmiIzmeneOpsteg
    @CenovnikID int,
    @Korisnik   nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @IzvorID int, @Cene int, @Troskovi int, @NoviRedovi int;

    SELECT @IzvorID = h.IzvorniCenovnikID
    FROM dbo.IC_Cenovnik h
    JOIN dbo.IC_Cenovnik i ON i.CenovnikID = h.IzvorniCenovnikID AND i.Vrsta = 'Opsti'
    WHERE h.CenovnikID = @CenovnikID AND h.Vrsta = 'Radni';

    IF @IzvorID IS NULL
    BEGIN
        RAISERROR(N'Izmene se preuzimaju samo u radnu kopiju opšteg cenovnika.', 16, 1);
        RETURN;
    END

    BEGIN TRAN;

    UPDATE h SET Kurs = i.Kurs, PDV = i.PDV,
                 DatumAzuriranjaTroska = i.DatumAzuriranjaTroska,
                 DatumProglasenjaTroska = i.DatumProglasenjaTroska
    FROM dbo.IC_Cenovnik h
    JOIN dbo.IC_Cenovnik i ON i.CenovnikID = @IzvorID
    WHERE h.CenovnikID = @CenovnikID;

    UPDATE c SET VPAktuelna = o.VPAktuelna, PovecanjeVP = o.PovecanjeVP, VP = o.VP, VPIzBaze = o.VPIzBaze
    FROM dbo.IC_CenovnikCena c
    JOIN dbo.IC_CenovnikCena o ON o.CenovnikID = @IzvorID AND o.Code = c.Code
    WHERE c.CenovnikID = @CenovnikID
      AND c.Izmenio IS NULL
      AND (c.VP <> o.VP OR c.PovecanjeVP <> o.PovecanjeVP
           OR ISNULL(c.VPAktuelna, -1) <> ISNULL(o.VPAktuelna, -1));
    SET @Cene = @@ROWCOUNT;

    INSERT INTO dbo.IC_CenovnikCena (CenovnikID, Code, VPAktuelna, PovecanjeVP, VP, VPIzBaze)
    SELECT @CenovnikID, o.Code, o.VPAktuelna, o.PovecanjeVP, o.VP, o.VPIzBaze
    FROM dbo.IC_CenovnikCena o
    WHERE o.CenovnikID = @IzvorID
      AND NOT EXISTS (SELECT 1 FROM dbo.IC_CenovnikCena c WHERE c.CenovnikID = @CenovnikID AND c.Code = o.Code);

    UPDATE s SET TrosakStari = o.TrosakStari, TrosakNovi = o.TrosakNovi,
                 DatumTroskaStari = o.DatumTroskaStari, DatumTroskaNovi = o.DatumTroskaNovi,
                 MarzaStara = o.MarzaStara, BezTroska = o.BezTroska
    FROM dbo.IC_CenovnikStavka s
    JOIN dbo.IC_CenovnikStavka o ON o.CenovnikID = @IzvorID AND o.LargoProizvodID = s.LargoProizvodID
    WHERE s.CenovnikID = @CenovnikID
      AND (ISNULL(s.TrosakNovi, -1) <> ISNULL(o.TrosakNovi, -1)
           OR ISNULL(s.TrosakStari, -1) <> ISNULL(o.TrosakStari, -1)
           OR ISNULL(s.MarzaStara, -1) <> ISNULL(o.MarzaStara, -1)
           OR s.BezTroska <> o.BezTroska);
    SET @Troskovi = @@ROWCOUNT;

    INSERT INTO dbo.IC_CenovnikStavka (CenovnikID, LargoProizvodID, SifraProizvodaLargo, Code, ProgramID, KolekcijaID,
                                       NazivProizvoda, TrosakStari, TrosakNovi, DatumTroskaStari, DatumTroskaNovi,
                                       MarzaStara, BezTroska)
    SELECT @CenovnikID, o.LargoProizvodID, o.SifraProizvodaLargo, o.Code, o.ProgramID, o.KolekcijaID,
           o.NazivProizvoda, o.TrosakStari, o.TrosakNovi, o.DatumTroskaStari, o.DatumTroskaNovi,
           o.MarzaStara, o.BezTroska
    FROM dbo.IC_CenovnikStavka o
    WHERE o.CenovnikID = @IzvorID
      AND NOT EXISTS (SELECT 1 FROM dbo.IC_CenovnikStavka s
                      WHERE s.CenovnikID = @CenovnikID AND s.LargoProizvodID = o.LargoProizvodID);
    SET @NoviRedovi = @@ROWCOUNT;

    UPDATE dbo.IC_Cenovnik
    SET DatumPreuzimanjaIzvora = SYSDATETIME(), Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
    WHERE CenovnikID = @CenovnikID;

    COMMIT;

    SELECT @Cene AS PreuzeteCene, @Troskovi AS PreuzetiTroskovi, @NoviRedovi AS NoviRedovi;
END
GO

/* IC_ProglasiVazecim
   Od radne kopije pravi zamrznut vazeci cenovnik.
   Povratna vrednost:
     0 = uradjeno, @VazeciCenovnikID popunjen;
     1 = treba potvrda; vraca spisak spornih cenovnika (Razlog = IstiNaziv
         ili Preklapanje). Forma pita korisnika i, ako prihvati, poziva
         ponovo sa @Prepisi = 1.
   Isti konacni cenovnik (isti naziv, partner i valuta) se uz @Prepisi = 1
   prepisuje: zadrzava ID, a cene i period se zamenjuju.                    */
IF OBJECT_ID('dbo.IC_ProglasiVazecim', 'P') IS NOT NULL DROP PROCEDURE dbo.IC_ProglasiVazecim;
GO
CREATE PROCEDURE dbo.IC_ProglasiVazecim
    @RadniCenovnikID  int,
    @Naziv            nvarchar(100),
    @VaziOd           date,
    @VaziDo           date = NULL,
    @PaSifra          int = NULL,
    @Korisnik         nvarchar(50),
    @Prepisi          bit = 0,
    @VazeciCenovnikID int OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    SET @VazeciCenovnikID = NULL;
    SET @Naziv = LTRIM(RTRIM(@Naziv));

    DECLARE @Valuta char(3), @PostojeciID int;

    SELECT @Valuta = Valuta FROM dbo.IC_Cenovnik WHERE CenovnikID = @RadniCenovnikID AND Vrsta = 'Radni';

    IF @Valuta IS NULL
    BEGIN
        RAISERROR(N'Važećim se proglašava samo radna kopija.', 16, 1);
        RETURN;
    END
    IF ISNULL(@Naziv, N'') = N''
    BEGIN
        RAISERROR(N'Naziv je obavezan.', 16, 1);
        RETURN;
    END
    IF @VaziOd IS NULL
    BEGIN
        RAISERROR(N'Datum "Važi od" je obavezan.', 16, 1);
        RETURN;
    END
    IF @VaziDo < @VaziOd
    BEGIN
        RAISERROR(N'Datum "Važi do" je pre datuma "Važi od".', 16, 1);
        RETURN;
    END

    -- isti naziv
    SELECT @PostojeciID = CenovnikID FROM dbo.IC_Cenovnik WHERE Naziv = @Naziv;

    IF @PostojeciID IS NOT NULL
       AND NOT EXISTS (SELECT 1 FROM dbo.IC_Cenovnik
                       WHERE CenovnikID = @PostojeciID AND Vrsta = 'Vazeci' AND Valuta = @Valuta
                         AND (PaSifra = @PaSifra OR (PaSifra IS NULL AND @PaSifra IS NULL)))
    BEGIN
        RAISERROR(N'Naziv "%s" već koristi drugi cenovnik (drugi partner, valuta ili vrsta).', 16, 1, @Naziv);
        RETURN;
    END

    -- potvrda: isti konacni cenovnik ili preklapanje perioda za istog partnera i valutu
    IF @Prepisi = 0
       AND (@PostojeciID IS NOT NULL
            OR EXISTS (SELECT 1 FROM dbo.IC_Cenovnik v
                       WHERE v.Vrsta = 'Vazeci' AND v.Valuta = @Valuta
                         AND (v.PaSifra = @PaSifra OR (v.PaSifra IS NULL AND @PaSifra IS NULL))
                         AND v.VaziOd <= ISNULL(@VaziDo, '99991231')
                         AND ISNULL(v.VaziDo, '99991231') >= @VaziOd))
    BEGIN
        SELECT v.CenovnikID, v.Naziv, v.Valuta, v.PaSifra, v.VaziOd, v.VaziDo,
               CASE WHEN v.CenovnikID = @PostojeciID THEN 'IstiNaziv' ELSE 'Preklapanje' END AS Razlog
        FROM dbo.IC_Cenovnik v
        WHERE v.CenovnikID = @PostojeciID
           OR (v.Vrsta = 'Vazeci' AND v.Valuta = @Valuta
               AND (v.PaSifra = @PaSifra OR (v.PaSifra IS NULL AND @PaSifra IS NULL))
               AND v.VaziOd <= ISNULL(@VaziDo, '99991231')
               AND ISNULL(v.VaziDo, '99991231') >= @VaziOd)
        ORDER BY v.VaziOd;
        RETURN 1;
    END

    BEGIN TRAN;

    IF @PostojeciID IS NOT NULL
    BEGIN
        DELETE FROM dbo.IC_CenovnikVazecaCena WHERE CenovnikID = @PostojeciID;
        DELETE FROM dbo.IC_CenovnikParametar  WHERE CenovnikID = @PostojeciID;   -- kaskadno i akcije
        DELETE FROM dbo.IC_CenovnikCena       WHERE CenovnikID = @PostojeciID;   -- kaskadno i stavke

        UPDATE v SET Kurs = r.Kurs, PDV = r.PDV, IzvorniCenovnikID = r.IzvorniCenovnikID,
                     VaziOd = @VaziOd, VaziDo = @VaziDo,
                     DatumAzuriranjaTroska = r.DatumAzuriranjaTroska,
                     DatumProglasenjaTroska = r.DatumProglasenjaTroska,
                     Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
        FROM dbo.IC_Cenovnik v
        JOIN dbo.IC_Cenovnik r ON r.CenovnikID = @RadniCenovnikID
        WHERE v.CenovnikID = @PostojeciID;

        SET @VazeciCenovnikID = @PostojeciID;
    END
    ELSE
    BEGIN
        INSERT INTO dbo.IC_Cenovnik (Naziv, Vrsta, Valuta, Kurs, PDV, IzvorniCenovnikID, PaSifra, VaziOd, VaziDo,
                                     DatumAzuriranjaTroska, DatumProglasenjaTroska, Kreirao)
        SELECT @Naziv, 'Vazeci', r.Valuta, r.Kurs, r.PDV, r.IzvorniCenovnikID, @PaSifra, @VaziOd, @VaziDo,
               r.DatumAzuriranjaTroska, r.DatumProglasenjaTroska, @Korisnik
        FROM dbo.IC_Cenovnik r
        WHERE r.CenovnikID = @RadniCenovnikID;

        SET @VazeciCenovnikID = SCOPE_IDENTITY();
    END

    -- stavke i parametri ostaju uz vazeci cenovnik (nazivi, program, kolekcija, akcije za poredjenje)
    EXEC dbo.IC_KopirajSadrzaj @IzvorID = @RadniCenovnikID, @CiljID = @VazeciCenovnikID, @ZadrziIzmenio = 1;

    -- zamrznute cene i marze, kao brojevi
    INSERT INTO dbo.IC_CenovnikVazecaCena (CenovnikID, LargoProizvodID, Code, TP, VP, VPsaPDV, Marza, Rabat,
                                           CenaPosleRabata, JelaMarza, Akcija, CenaAkcija, JelaMarzaAkcija)
    SELECT @VazeciCenovnikID, o.LargoProizvodID, o.Code, o.TP, o.VP, o.VPsaPDV, ROUND(o.Marza, 4), o.Rabat,
           o.CenaPosleRabata, ROUND(o.JelaMarza, 4), o.Akcija,
           CASE WHEN o.Akcija IS NOT NULL THEN o.CenaAkcija END,
           CASE WHEN o.Akcija IS NOT NULL THEN ROUND(o.JelaMarzaAkcija, 4) END
    FROM dbo.vw_IC_CenovnikObracun o
    WHERE o.CenovnikID = @RadniCenovnikID;

    COMMIT;

    RETURN 0;
END
GO

/* IC_ObrisiRadniCenovnik: brise napustenu radnu kopiju */
IF OBJECT_ID('dbo.IC_ObrisiRadniCenovnik', 'P') IS NOT NULL DROP PROCEDURE dbo.IC_ObrisiRadniCenovnik;
GO
CREATE PROCEDURE dbo.IC_ObrisiRadniCenovnik
    @CenovnikID int
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.IC_Cenovnik WHERE CenovnikID = @CenovnikID AND Vrsta = 'Radni')
    BEGIN
        RAISERROR(N'Briše se samo radna kopija.', 16, 1);
        RETURN;
    END

    BEGIN TRAN;
    UPDATE dbo.IC_Cenovnik SET IzvorniCenovnikID = NULL WHERE IzvorniCenovnikID = @CenovnikID;
    DELETE FROM dbo.IC_CenovnikPresek WHERE CenovnikID = @CenovnikID;
    DELETE FROM dbo.IC_Cenovnik       WHERE CenovnikID = @CenovnikID;   -- kaskadno cene, stavke, parametri
    COMMIT;
END
GO

/* =====================================================================
   UVOZ IZ EXCELA
   ===================================================================== */

/* IC_UpisiUvoz
   Pravi opsti cenovnik od pregledanog uvoza iz Excela.
   Cenovnik dobija SVE maticne podatke iz vw_IC_MaticniPodatak:
     - VP po Code iz Excela; Code bez cene u Excelu dobija cenu iz
       jela_products (price_rs_WP / price_WP) i VPIzBaze = 1; ako ni tamo
       nema cene, VP = 0 (forma ga istice);
     - TP - maj i marza maj: maticnom podatku iz kolone A; ako Code ima
       samo jedan red u fajlu (danasnji fajlovi), i svim varijantama tog Code;
     - parametri i akcije za poredjenje iz @Parametri / @Akcije.
   @SamoIzExcela = 1 (dugme "Uvezi samo povezane"): cenovnik dobija samo
   Code koji su povezani iz Excela (@Stavke), sa svim njihovim varijantama.
   Na kraju pokrece IC_AzurirajTroskove.
   Vraca dva skupa redova:
     1. maticni podaci preskoceni zbog nepotpunih podataka u bazi;
     2. redovi bez vazeceg troska (iz IC_AzurirajTroskove).                */
IF OBJECT_ID('dbo.IC_UpisiUvoz', 'P') IS NOT NULL DROP PROCEDURE dbo.IC_UpisiUvoz;
GO
CREATE PROCEDURE dbo.IC_UpisiUvoz
    @Naziv      nvarchar(100),
    @Valuta     char(3),
    @Kurs       decimal(9,2),
    @PDV        decimal(5,4) = 0.20,
    @Korisnik   nvarchar(50),
    @Cene       dbo.IC_UvozCenaTip READONLY,
    @Stavke     dbo.IC_UvozStavkaTip READONLY,
    @Parametri  dbo.IC_ParametarTip READONLY,
    @Akcije     dbo.IC_AkcijaTip READONLY,
    @CenovnikID int OUTPUT,
    @SamoIzExcela bit = 0
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    SET @CenovnikID = NULL;

    IF @Valuta NOT IN ('RSD', 'EUR')
    BEGIN
        RAISERROR(N'Valuta mora biti RSD ili EUR.', 16, 1);
        RETURN;
    END
    IF @Kurs IS NULL OR @Kurs < 50 OR @Kurs > 200
    BEGIN
        RAISERROR(N'Kurs mora biti između 50 i 200.', 16, 1);
        RETURN;
    END
    IF LTRIM(RTRIM(ISNULL(@Naziv, N''))) = N''
    BEGIN
        RAISERROR(N'Naziv cenovnika je obavezan.', 16, 1);
        RETURN;
    END
    IF EXISTS (SELECT 1 FROM dbo.IC_Cenovnik WHERE Naziv = @Naziv)
    BEGIN
        RAISERROR(N'Cenovnik sa nazivom "%s" već postoji.', 16, 1, @Naziv);
        RETURN;
    END
    IF @SamoIzExcela = 1 AND NOT EXISTS (SELECT 1 FROM @Stavke)
    BEGIN
        RAISERROR(N'Nema povezanih redova iz Excela.', 16, 1);
        RETURN;
    END
    IF EXISTS (SELECT 1 FROM @Akcije a WHERE NOT EXISTS (SELECT 1 FROM @Parametri p WHERE p.RedBr = a.RedBr))
    BEGIN
        RAISERROR(N'Akcija za poređenje je vezana za nepostojeći parametar (RedBr).', 16, 1);
        RETURN;
    END

    BEGIN TRAN;

    EXEC dbo.IC_OsveziProgrameIKolekcije @BezRezultata = 1;

    -- maticni podaci jednom, u privremenu tabelu (pogled je skup)
    SELECT LargoProizvodID, SifraProizvodaLargo, Code, ProgramID, KolekcijaID, NazivProizvoda,
           CASE @Valuta WHEN 'RSD' THEN CenaRSD ELSE CenaEUR END AS CenaBaza
    INTO #Mp
    FROM dbo.vw_IC_MaticniPodatak;

    IF @SamoIzExcela = 1
        DELETE m FROM #Mp m
        WHERE m.Code IS NULL OR NOT EXISTS (SELECT 1 FROM @Stavke s WHERE s.Code = m.Code);

    -- 1. preskoceni: nema Code, sifre, programa ili kolekcije
    SELECT LargoProizvodID, SifraProizvodaLargo, Code, NazivProizvoda,
           CASE WHEN Code IS NULL THEN 'Nema Code'
                WHEN SifraProizvodaLargo IS NULL THEN 'Nema sifre'
                WHEN ProgramID IS NULL THEN 'Nema programa'
                ELSE 'Nema kolekcije' END AS Razlog
    FROM #Mp
    WHERE Code IS NULL OR SifraProizvodaLargo IS NULL OR ProgramID IS NULL OR KolekcijaID IS NULL
    ORDER BY LargoProizvodID;

    DELETE FROM #Mp
    WHERE Code IS NULL OR SifraProizvodaLargo IS NULL OR ProgramID IS NULL OR KolekcijaID IS NULL;

    INSERT INTO dbo.IC_Cenovnik (Naziv, Vrsta, Valuta, Kurs, PDV, Kreirao)
    VALUES (@Naziv, 'Opsti', @Valuta, @Kurs, @PDV, @Korisnik);
    SET @CenovnikID = SCOPE_IDENTITY();

    -- cene po Code
    INSERT INTO dbo.IC_CenovnikCena (CenovnikID, Code, VPAktuelna, PovecanjeVP, VP, VPIzBaze)
    SELECT @CenovnikID, m.Code,
           ROUND(ISNULL(e.VP, ISNULL(m.CenaBaza, 0)), 0),
           0,
           ROUND(ISNULL(e.VP, ISNULL(m.CenaBaza, 0)), 0),
           CASE WHEN e.VP IS NULL THEN 1 ELSE 0 END
    FROM (SELECT Code, MIN(CenaBaza) AS CenaBaza FROM #Mp GROUP BY Code) m
    LEFT JOIN @Cene e ON e.Code = m.Code;

    -- stavke
    INSERT INTO dbo.IC_CenovnikStavka (CenovnikID, LargoProizvodID, SifraProizvodaLargo, Code, ProgramID, KolekcijaID,
                                       NazivProizvoda, TrosakStari, MarzaStara)
    SELECT @CenovnikID, m.LargoProizvodID, m.SifraProizvodaLargo, m.Code, m.ProgramID, m.KolekcijaID,
           LEFT(m.NazivProizvoda, 50),
           ISNULL(t.TrosakStari, tc.TrosakStari),
           ISNULL(t.MarzaStara,  tc.MarzaStara)
    FROM #Mp m
    LEFT JOIN @Stavke t ON t.LargoProizvodID = m.LargoProizvodID
    OUTER APPLY (SELECT TOP (1) x.TrosakStari, x.MarzaStara    -- samo ako Code ima jedan red u fajlu
                 FROM @Stavke x
                 WHERE x.Code = m.Code
                   AND (SELECT COUNT(*) FROM @Stavke y WHERE y.Code = m.Code) = 1) tc;

    -- parametri i akcije za poredjenje
    INSERT INTO dbo.IC_CenovnikParametar (CenovnikID, Nivo, ProgramID, KolekcijaID, Code, Rabat, Akcija)
    SELECT @CenovnikID, u.Nivo, u.ProgramID, u.KolekcijaID, u.Code, u.Rabat, u.Akcija
    FROM @Parametri u;

    -- RedBr -> ParametarID po prirodnom kljucu (UQ_IC_Param)
    INSERT INTO dbo.IC_CenovnikAkcija (ParametarID, Redosled, Procenat)
    SELECT n.ParametarID, a.Redosled, a.Procenat
    FROM @Akcije a
    JOIN @Parametri u ON u.RedBr = a.RedBr
    JOIN dbo.IC_CenovnikParametar n ON n.CenovnikID = @CenovnikID
                                   AND n.Nivo = u.Nivo
                                   AND ISNULL(n.ProgramID, -1)   = ISNULL(u.ProgramID, -1)
                                   AND ISNULL(n.KolekcijaID, -1) = ISNULL(u.KolekcijaID, -1)
                                   AND ISNULL(n.Code, N'')       = ISNULL(u.Code, N'');

    -- 2. TP-Novi iz KalkCenik (vraca redove bez troska)
    EXEC dbo.IC_AzurirajTroskove @CenovnikID = @CenovnikID;

    COMMIT;
END
GO

/* =====================================================================
   SLIKE
   ===================================================================== */

/* IC_PoveziSlikeAutomatski
   @Slike = sve .jpg/.png nadjene u korenskom folderu (Code vec izdvojen u C#).
   - dodaje nove parove Code + putanja kao Automatski;
   - rucno dodeljene slike ne menja;
   - @ObrisiNepostojece = 1 brise automatske veze ciji fajl vise ne postoji;
   - Code bez glavne slike dobija glavnu (rucna, pa slika bez "(2)", pa najkraca putanja).
   Vraca: slike ciji Code ne postoji u jela_products, pa broj dodatih.      */
IF OBJECT_ID('dbo.IC_PoveziSlikeAutomatski', 'P') IS NOT NULL DROP PROCEDURE dbo.IC_PoveziSlikeAutomatski;
GO
CREATE PROCEDURE dbo.IC_PoveziSlikeAutomatski
    @Slike              dbo.IC_SlikaTip READONLY,
    @ObrisiNepostojece  bit = 1
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Dodato int, @Obrisano int = 0;

    BEGIN TRAN;

    IF @ObrisiNepostojece = 1
    BEGIN
        DELETE s
        FROM dbo.IC_SlikaProizvoda s
        WHERE s.Izvor = 'Automatski'
          AND NOT EXISTS (SELECT 1 FROM @Slike u WHERE u.Code = s.Code AND u.Putanja = s.Putanja);
        SET @Obrisano = @@ROWCOUNT;
    END

    INSERT INTO dbo.IC_SlikaProizvoda (Code, Putanja, Glavna, Izvor)
    SELECT u.Code, u.Putanja, 0, 'Automatski'
    FROM @Slike u
    WHERE EXISTS (SELECT 1 FROM dbo.jela_products p WHERE p.Code = u.Code)
      AND NOT EXISTS (SELECT 1 FROM dbo.IC_SlikaProizvoda s WHERE s.Code = u.Code AND s.Putanja = u.Putanja);
    SET @Dodato = @@ROWCOUNT;

    ;WITH Kandidati AS (
        SELECT s.SlikaID,
               ROW_NUMBER() OVER (PARTITION BY s.Code
                                  ORDER BY CASE s.Izvor WHEN 'Rucno' THEN 0 ELSE 1 END,
                                           ISNULL(CAST(u.Dodatna AS int), 1),
                                           LEN(s.Putanja), s.Putanja) AS Rb
        FROM dbo.IC_SlikaProizvoda s
        LEFT JOIN @Slike u ON u.Code = s.Code AND u.Putanja = s.Putanja
        WHERE NOT EXISTS (SELECT 1 FROM dbo.IC_SlikaProizvoda g WHERE g.Code = s.Code AND g.Glavna = 1)
    )
    UPDATE s SET Glavna = 1
    FROM dbo.IC_SlikaProizvoda s
    JOIN Kandidati k ON k.SlikaID = s.SlikaID
    WHERE k.Rb = 1;

    COMMIT;

    -- slika bez Code u bazi
    SELECT u.Code, u.Putanja
    FROM @Slike u
    WHERE NOT EXISTS (SELECT 1 FROM dbo.jela_products p WHERE p.Code = u.Code)
    ORDER BY u.Putanja;

    SELECT @Dodato AS Dodato, @Obrisano AS Obrisano;
END
GO

/* IC_DodeliSlikuRucno: desni klik u gridu -> Dodeli sliku */
IF OBJECT_ID('dbo.IC_DodeliSlikuRucno', 'P') IS NOT NULL DROP PROCEDURE dbo.IC_DodeliSlikuRucno;
GO
CREATE PROCEDURE dbo.IC_DodeliSlikuRucno
    @Code    nvarchar(30),
    @Putanja nvarchar(400),
    @Glavna  bit = 1
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRAN;

    IF @Glavna = 1
        UPDATE dbo.IC_SlikaProizvoda SET Glavna = 0
        WHERE Code = @Code AND Glavna = 1 AND Putanja <> @Putanja;

    -- Code mora imati glavnu sliku: ako nijedna druga nije glavna, ova jeste
    IF NOT EXISTS (SELECT 1 FROM dbo.IC_SlikaProizvoda
                   WHERE Code = @Code AND Glavna = 1 AND Putanja <> @Putanja)
        SET @Glavna = 1;

    UPDATE dbo.IC_SlikaProizvoda
    SET Izvor = 'Rucno', Glavna = @Glavna, DatumPovezivanja = SYSDATETIME()
    WHERE Code = @Code AND Putanja = @Putanja;

    IF @@ROWCOUNT = 0
        INSERT INTO dbo.IC_SlikaProizvoda (Code, Putanja, Glavna, Izvor)
        VALUES (@Code, @Putanja, @Glavna, 'Rucno');

    COMMIT;
END
GO

/* IC_IzvestajSlika: Code bez slike, pa Code sa slikama bez glavne */
IF OBJECT_ID('dbo.IC_IzvestajSlika', 'P') IS NOT NULL DROP PROCEDURE dbo.IC_IzvestajSlika;
GO
CREATE PROCEDURE dbo.IC_IzvestajSlika
AS
BEGIN
    SET NOCOUNT ON;

    SELECT p.Code, MIN(p.NazivProizvoda) AS NazivProizvoda,
           MIN(p.NazivRobneGrupe) AS Program, MIN(p.NazivGrupe) AS Kolekcija
    FROM dbo.jela_products p
    WHERE p.Code IS NOT NULL
      AND NOT EXISTS (SELECT 1 FROM dbo.IC_SlikaProizvoda s WHERE s.Code = p.Code)
    GROUP BY p.Code
    ORDER BY p.Code;

    SELECT s.Code, COUNT(*) AS BrojSlika
    FROM dbo.IC_SlikaProizvoda s
    GROUP BY s.Code
    HAVING MAX(CAST(s.Glavna AS int)) = 0
    ORDER BY s.Code;
END
GO

/* =====================================================================
   IZVOZ
   ===================================================================== */

/* IC_UpisiPresek: evidencija izvoza u Excel */
IF OBJECT_ID('dbo.IC_UpisiPresek', 'P') IS NOT NULL DROP PROCEDURE dbo.IC_UpisiPresek;
GO
CREATE PROCEDURE dbo.IC_UpisiPresek
    @CenovnikID int,
    @Fajl       nvarchar(400),
    @Korisnik   nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.IC_CenovnikPresek (CenovnikID, Fajl, Korisnik)
    VALUES (@CenovnikID, @Fajl, @Korisnik);

    SELECT CAST(SCOPE_IDENTITY() AS int) AS PresekID;
END
GO
