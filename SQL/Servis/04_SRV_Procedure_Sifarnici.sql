/* =====================================================================
   ZAH-010 Servis - 04: procedure sifarnika, sredstava i delova
   Pravila za sve procedure:
     - SET XACT_ABORT ON, upis u TRY/CATCH; greska ponistava celu transakciju
       i ponovo se javlja preko SRV_PonoviGresku;
     - poruke korisniku su na srpskom (RAISERROR, broj 50000), bez SQL teksta;
     - svaka procedura prima @Korisnik (audit i provere);
     - procedure sa sufiksom _Int nemaju svoju transakciju i pozivaju se
       samo iz drugih procedura (unutar njihove transakcije).
   ===================================================================== */
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

/* =====================================================================
   VRSTE SREDSTAVA
   ===================================================================== */
IF OBJECT_ID('dbo.SRV_SacuvajVrstu', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_SacuvajVrstu;
GO
CREATE PROCEDURE dbo.SRV_SacuvajVrstu
    @ID        int = NULL,
    @Naziv     nvarchar(100),
    @Napomena  nvarchar(400) = NULL,
    @Aktivno   bit = 1,
    @Brojila   dbo.SRV_TekstTip READONLY,     -- tipovi brojila (KM, MH, ...)
    @Korisnik  nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET @Naziv = LTRIM(RTRIM(ISNULL(@Naziv, N'')));
    BEGIN TRY
        IF @Naziv = N'' RAISERROR(N'Unesite polje „Naziv“.', 16, 1);
        IF NOT EXISTS (SELECT 1 FROM @Brojila WHERE LTRIM(RTRIM(Tekst)) <> N'')
            RAISERROR(N'Vrsta sredstva mora imati bar jedno brojilo (npr. KM ili MH).', 16, 1);
        IF @ID IS NOT NULL AND @Aktivno = 0
           AND EXISTS (SELECT 1 FROM dbo.SRV_Sredstvo WHERE VrstaSredstvaID = @ID AND Aktivno = 1)
            RAISERROR(N'Vrsta ima aktivna sredstva i ne može se deaktivirati.', 16, 1);

        BEGIN TRAN;
        IF @ID IS NULL
        BEGIN
            INSERT INTO dbo.SRV_VrstaSredstva (Naziv, Napomena, Aktivno, Kreirao) VALUES (@Naziv, @Napomena, @Aktivno, @Korisnik);
            SET @ID = SCOPE_IDENTITY();
        END
        ELSE
            UPDATE dbo.SRV_VrstaSredstva
            SET Naziv = @Naziv, Napomena = @Napomena, Aktivno = @Aktivno, Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE ID = @ID;

        -- brojila: nova se dodaju, uklonjena se deaktiviraju (zbog ocitavanja koja ih koriste)
        UPDATE b SET Aktivno = CASE WHEN EXISTS (SELECT 1 FROM @Brojila t WHERE UPPER(LTRIM(RTRIM(t.Tekst))) = b.Tip) THEN 1 ELSE 0 END
        FROM dbo.SRV_VrstaSredstvaBrojilo b WHERE b.VrstaSredstvaID = @ID;

        INSERT INTO dbo.SRV_VrstaSredstvaBrojilo (VrstaSredstvaID, Tip, Redosled)
        SELECT @ID, x.Tip, ROW_NUMBER() OVER (ORDER BY x.Tip)
        FROM (SELECT DISTINCT UPPER(LTRIM(RTRIM(Tekst))) AS Tip FROM @Brojila WHERE LTRIM(RTRIM(Tekst)) <> N'') x
        WHERE NOT EXISTS (SELECT 1 FROM dbo.SRV_VrstaSredstvaBrojilo b WHERE b.VrstaSredstvaID = @ID AND b.Tip = x.Tip);
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
    SELECT @ID AS ID;
END
GO

IF OBJECT_ID('dbo.SRV_SacuvajPoljeVrste', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_SacuvajPoljeVrste;
GO
CREATE PROCEDURE dbo.SRV_SacuvajPoljeVrste
    @ID        int = NULL,
    @VrstaID   int,
    @Naziv     nvarchar(100),
    @Tip       nvarchar(10),
    @JM        nvarchar(20) = NULL,
    @Opcije    nvarchar(400) = NULL,
    @Obavezno  bit = 0,
    @Redosled  int = 1,
    @Aktivno   bit = 1,
    @Korisnik  nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET @Naziv = LTRIM(RTRIM(ISNULL(@Naziv, N'')));
    BEGIN TRY
        IF @Naziv = N'' RAISERROR(N'Unesite polje „Naziv“.', 16, 1);
        IF @Tip = N'Lista' AND LTRIM(RTRIM(ISNULL(@Opcije, N''))) = N''
            RAISERROR(N'Za tip „Lista“ unesite polje „Opcije“ (vrednosti odvojene sa ;).', 16, 1);
        -- SR-02: tip polja koje vec ima vrednosti se ne menja
        IF @ID IS NOT NULL
           AND EXISTS (SELECT 1 FROM dbo.SRV_VrstaSredstvaPolje WHERE ID = @ID AND Tip <> @Tip)
           AND EXISTS (SELECT 1 FROM dbo.SRV_SredstvoPolje WHERE PoljeID = @ID AND LTRIM(RTRIM(ISNULL(Vrednost, N''))) <> N'')
            RAISERROR(N'Polje već ima unete vrednosti kod sredstava; tip polja se ne može menjati.', 16, 1);

        BEGIN TRAN;
        IF @ID IS NULL
        BEGIN
            INSERT INTO dbo.SRV_VrstaSredstvaPolje (VrstaSredstvaID, Naziv, Tip, JM, Opcije, Obavezno, Redosled, Aktivno, Kreirao)
            VALUES (@VrstaID, @Naziv, @Tip, @JM, @Opcije, @Obavezno, @Redosled, @Aktivno, @Korisnik);
            SET @ID = SCOPE_IDENTITY();
        END
        ELSE
            UPDATE dbo.SRV_VrstaSredstvaPolje
            SET Naziv = @Naziv, Tip = @Tip, JM = @JM, Opcije = @Opcije, Obavezno = @Obavezno, Redosled = @Redosled,
                Aktivno = @Aktivno, Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE ID = @ID;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
    SELECT @ID AS ID;
END
GO

IF OBJECT_ID('dbo.SRV_SacuvajDokumentVrste', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_SacuvajDokumentVrste;
GO
CREATE PROCEDURE dbo.SRV_SacuvajDokumentVrste
    @ID              int = NULL,
    @VrstaID         int,
    @Naziv           nvarchar(100),
    @Obavezno        bit = 0,
    @UpozorenjeDana  int = 30,
    @Aktivno         bit = 1,
    @Korisnik        nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET @Naziv = LTRIM(RTRIM(ISNULL(@Naziv, N'')));
    BEGIN TRY
        IF @Naziv = N'' RAISERROR(N'Unesite polje „Naziv“.', 16, 1);
        IF ISNULL(@UpozorenjeDana, -1) < 0 RAISERROR(N'Polje „Upozorenje (dana)“ mora biti 0 ili veće.', 16, 1);
        BEGIN TRAN;
        IF @ID IS NULL
        BEGIN
            INSERT INTO dbo.SRV_VrstaSredstvaDokument (VrstaSredstvaID, Naziv, Obavezno, UpozorenjeDana, Aktivno, Kreirao)
            VALUES (@VrstaID, @Naziv, @Obavezno, @UpozorenjeDana, @Aktivno, @Korisnik);
            SET @ID = SCOPE_IDENTITY();
        END
        ELSE
            UPDATE dbo.SRV_VrstaSredstvaDokument
            SET Naziv = @Naziv, Obavezno = @Obavezno, UpozorenjeDana = @UpozorenjeDana, Aktivno = @Aktivno,
                Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE ID = @ID;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
    SELECT @ID AS ID;
END
GO

/* =====================================================================
   GRUPE DELOVA
   ===================================================================== */
IF OBJECT_ID('dbo.SRV_SacuvajGrupuDelova', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_SacuvajGrupuDelova;
GO
CREATE PROCEDURE dbo.SRV_SacuvajGrupuDelova
    @ID        int = NULL,
    @Naziv     nvarchar(100),
    @Aktivno   bit = 1,
    @Korisnik  nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET @Naziv = LTRIM(RTRIM(ISNULL(@Naziv, N'')));
    BEGIN TRY
        IF @Naziv = N'' RAISERROR(N'Unesite polje „Naziv“.', 16, 1);
        IF @ID IS NOT NULL AND @Aktivno = 0 AND EXISTS (SELECT 1 FROM dbo.SRV_Deo WHERE DeoGrupaID = @ID AND Aktivno = 1)
            RAISERROR(N'Grupa ima aktivne delove i ne može se deaktivirati.', 16, 1);
        BEGIN TRAN;
        IF @ID IS NULL
        BEGIN
            INSERT INTO dbo.SRV_DeoGrupa (Naziv, Aktivno, Kreirao) VALUES (@Naziv, @Aktivno, @Korisnik);
            SET @ID = SCOPE_IDENTITY();
        END
        ELSE
            UPDATE dbo.SRV_DeoGrupa SET Naziv = @Naziv, Aktivno = @Aktivno, Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE ID = @ID;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
    SELECT @ID AS ID;
END
GO

/* =====================================================================
   BROJILA (SR-03)
   ===================================================================== */
IF OBJECT_ID('dbo.SRV_UpisiBrojilo_Int', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_UpisiBrojilo_Int;
GO
CREATE PROCEDURE dbo.SRV_UpisiBrojilo_Int
    @SredstvoID int,
    @Tip        nvarchar(20),
    @Vrednost   decimal(18,2),
    @Datum      date,
    @Razlog     nvarchar(200) = NULL,
    @Izvor      nvarchar(30),
    @Korisnik   nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @PosVrednost decimal(18,2), @PosDatum date, @t nvarchar(40), @d nvarchar(12);

    IF NOT EXISTS (SELECT 1 FROM dbo.SRV_Sredstvo s
                   JOIN dbo.SRV_VrstaSredstvaBrojilo b ON b.VrstaSredstvaID = s.VrstaSredstvaID AND b.Aktivno = 1
                   WHERE s.ID = @SredstvoID AND b.Tip = @Tip)
    BEGIN
        RAISERROR(N'Brojilo „%s“ ne postoji za vrstu ovog sredstva.', 16, 1, @Tip);
        RETURN 1;
    END
    IF @Vrednost IS NULL OR @Vrednost < 0
    BEGIN
        RAISERROR(N'Polje „Brojilo“ mora biti 0 ili veće.', 16, 1);
        RETURN 1;
    END
    IF @Datum > CAST(GETDATE() AS date)
    BEGIN
        RAISERROR(N'Datum očitavanja ne sme biti u budućnosti.', 16, 1);
        RETURN 1;
    END

    SELECT TOP 1 @PosVrednost = Vrednost, @PosDatum = Datum
    FROM dbo.SRV_SredstvoBrojilo WITH (UPDLOCK, HOLDLOCK)
    WHERE SredstvoID = @SredstvoID AND Tip = @Tip
    ORDER BY Datum DESC, ID DESC;

    IF @PosDatum IS NOT NULL AND @Datum < @PosDatum
    BEGIN
        SET @d = CONVERT(nvarchar(10), @PosDatum, 104) + N'.';
        RAISERROR(N'Datum očitavanja ne sme biti pre poslednjeg očitavanja (%s).', 16, 1, @d);
        RETURN 1;
    END
    IF @PosVrednost IS NOT NULL AND @Vrednost < @PosVrednost AND LTRIM(RTRIM(ISNULL(@Razlog, N''))) = N''
    BEGIN
        SET @t = dbo.SRV_Broj(@PosVrednost);
        RAISERROR(N'Brojilo ne može da ide unazad (poslednje očitavanje: %s). Unesite razlog.', 16, 1, @t);
        RETURN 1;
    END

    -- isto ocitavanje istog dana se ne duplira
    IF @PosVrednost = @Vrednost AND @PosDatum = @Datum RETURN 0;

    INSERT INTO dbo.SRV_SredstvoBrojilo (SredstvoID, Tip, Vrednost, Datum, Izvor, Razlog, Kreirao)
    VALUES (@SredstvoID, @Tip, @Vrednost, @Datum, @Izvor, NULLIF(LTRIM(RTRIM(@Razlog)), N''), @Korisnik);
    RETURN 0;
END
GO

IF OBJECT_ID('dbo.SRV_UpisiBrojilo', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_UpisiBrojilo;
GO
CREATE PROCEDURE dbo.SRV_UpisiBrojilo
    @SredstvoID int,
    @Tip        nvarchar(20),
    @Vrednost   decimal(18,2),
    @Datum      date,
    @Razlog     nvarchar(200) = NULL,
    @Korisnik   nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRAN;
        EXEC dbo.SRV_UpisiBrojilo_Int @SredstvoID, @Tip, @Vrednost, @Datum, @Razlog, N'Očitavanje', @Korisnik;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
END
GO

/* =====================================================================
   SREDSTVA (SR-02, SR-18)
   ===================================================================== */
IF OBJECT_ID('dbo.SRV_SacuvajSredstvo', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_SacuvajSredstvo;
GO
CREATE PROCEDURE dbo.SRV_SacuvajSredstvo
    @ID              int = NULL,
    @Oznaka          nvarchar(30),
    @Naziv           nvarchar(150),
    @VrstaID         int,
    @Proizvodjac     nvarchar(100) = NULL,
    @Model           nvarchar(100) = NULL,
    @Godina          int = NULL,
    @SerijskiBroj    nvarchar(100) = NULL,
    @Lokacija        nvarchar(100) = NULL,
    @Status          nvarchar(20) = N'U radu',
    @VoziloID        int = NULL,
    @Napomena        nvarchar(400) = NULL,
    @Polja           dbo.SRV_PoljeVrednostTip READONLY,
    @PocetnoBrojilo  decimal(18,2) = NULL,          -- samo za novo sredstvo: prvo ocitavanje glavnog brojila
    @Korisnik        nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @Polje nvarchar(100), @StaroStanje nvarchar(400), @Novo bit = CASE WHEN @ID IS NULL THEN 1 ELSE 0 END,
            @Tip nvarchar(20), @StariStatus nvarchar(20);
    SET @Oznaka = LTRIM(RTRIM(ISNULL(@Oznaka, N'')));
    SET @Naziv = LTRIM(RTRIM(ISNULL(@Naziv, N'')));
    BEGIN TRY
        IF @Oznaka = N'' RAISERROR(N'Unesite polje „Oznaka (inventarski broj)“.', 16, 1);
        IF @Naziv = N'' RAISERROR(N'Unesite polje „Naziv“.', 16, 1);
        IF @VrstaID IS NULL RAISERROR(N'Unesite polje „Vrsta sredstva“.', 16, 1);

        SELECT TOP 1 @Polje = p.Naziv
        FROM dbo.SRV_VrstaSredstvaPolje p
        LEFT JOIN @Polja v ON v.PoljeID = p.ID
        WHERE p.VrstaSredstvaID = @VrstaID AND p.Aktivno = 1 AND p.Obavezno = 1
          AND LTRIM(RTRIM(ISNULL(v.Vrednost, N''))) = N''
        ORDER BY p.Redosled, p.ID;
        IF @Polje IS NOT NULL RAISERROR(N'Unesite polje „%s“.', 16, 1, @Polje);

        IF @Status = N'Arhivirano' AND @ID IS NOT NULL
           AND EXISTS (SELECT 1 FROM dbo.SRV_Nalog WHERE SredstvoID = @ID AND Status NOT IN (N'Završen', N'Otkazan'))
            RAISERROR(N'Sredstvo ima otvoren servisni nalog i ne može se arhivirati.', 16, 1);

        BEGIN TRAN;
        IF @Novo = 1
        BEGIN
            INSERT INTO dbo.SRV_Sredstvo (Oznaka, Naziv, VrstaSredstvaID, Proizvodjac, Model, Godina, SerijskiBroj, Lokacija,
                                          Status, VoziloID, Napomena, Aktivno, Kreirao)
            VALUES (@Oznaka, @Naziv, @VrstaID, @Proizvodjac, @Model, @Godina, @SerijskiBroj, @Lokacija,
                    @Status, @VoziloID, @Napomena, CASE WHEN @Status = N'Arhivirano' THEN 0 ELSE 1 END, @Korisnik);
            SET @ID = SCOPE_IDENTITY();
            INSERT INTO dbo.SRV_Log (Tabela, ZapisID, Opis, Novo, Korisnik)
            VALUES ('SRV_Sredstvo', @ID, N'Novo sredstvo', @Oznaka + N' ' + @Naziv, @Korisnik);
        END
        ELSE
        BEGIN
            SELECT @StaroStanje = Oznaka + N' | ' + Naziv + N' | ' + Status + N' | ' + ISNULL(Lokacija, N''), @StariStatus = Status
            FROM dbo.SRV_Sredstvo WHERE ID = @ID;
            UPDATE dbo.SRV_Sredstvo
            SET Oznaka = @Oznaka, Naziv = @Naziv, VrstaSredstvaID = @VrstaID, Proizvodjac = @Proizvodjac, Model = @Model,
                Godina = @Godina, SerijskiBroj = @SerijskiBroj, Lokacija = @Lokacija, Status = @Status, VoziloID = @VoziloID,
                Napomena = @Napomena, Aktivno = CASE WHEN @Status = N'Arhivirano' THEN 0 ELSE 1 END,
                Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE ID = @ID;
            INSERT INTO dbo.SRV_Log (Tabela, ZapisID, Opis, Staro, Novo, Korisnik)
            VALUES ('SRV_Sredstvo', @ID, CASE WHEN @StariStatus <> @Status THEN N'Promena statusa sredstva' ELSE N'Izmena sredstva' END,
                    @StaroStanje, @Oznaka + N' | ' + @Naziv + N' | ' + @Status + N' | ' + ISNULL(@Lokacija, N''), @Korisnik);
        END

        -- vrednosti dodatnih polja (samo polja izabrane vrste)
        UPDATE sp SET Vrednost = NULLIF(LTRIM(RTRIM(v.Vrednost)), N'')
        FROM dbo.SRV_SredstvoPolje sp JOIN @Polja v ON v.PoljeID = sp.PoljeID
        WHERE sp.SredstvoID = @ID;
        INSERT INTO dbo.SRV_SredstvoPolje (SredstvoID, PoljeID, Vrednost)
        SELECT @ID, v.PoljeID, NULLIF(LTRIM(RTRIM(v.Vrednost)), N'')
        FROM @Polja v
        JOIN dbo.SRV_VrstaSredstvaPolje p ON p.ID = v.PoljeID AND p.VrstaSredstvaID = @VrstaID
        WHERE NOT EXISTS (SELECT 1 FROM dbo.SRV_SredstvoPolje sp WHERE sp.SredstvoID = @ID AND sp.PoljeID = v.PoljeID);

        IF @Novo = 1 AND @PocetnoBrojilo IS NOT NULL
        BEGIN
            SELECT TOP 1 @Tip = Tip FROM dbo.SRV_VrstaSredstvaBrojilo
            WHERE VrstaSredstvaID = @VrstaID AND Aktivno = 1 ORDER BY Redosled, ID;
            DECLARE @Danas date = CAST(GETDATE() AS date);
            EXEC dbo.SRV_UpisiBrojilo_Int @ID, @Tip, @PocetnoBrojilo, @Danas, NULL, N'Početno', @Korisnik;
        END
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
    SELECT @ID AS ID;
END
GO

/* =====================================================================
   DOKUMENTA SREDSTAVA (SR-16)
   ===================================================================== */
IF OBJECT_ID('dbo.SRV_SacuvajDokumentSredstva', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_SacuvajDokumentSredstva;
GO
CREATE PROCEDURE dbo.SRV_SacuvajDokumentSredstva
    @ID                int = NULL,
    @SredstvoID        int,
    @VrstaDokumentaID  int,
    @Broj              nvarchar(50) = NULL,
    @DatumIzdavanja    date,
    @DatumIsteka       date,
    @Prilog            nvarchar(400) = NULL,
    @Napomena          nvarchar(400) = NULL,
    @Aktivno           bit = 1,
    @Korisnik          nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        IF @VrstaDokumentaID IS NULL RAISERROR(N'Unesite polje „Dokument“.', 16, 1);
        IF @DatumIzdavanja IS NULL RAISERROR(N'Unesite polje „Datum izdavanja“.', 16, 1);
        IF @DatumIsteka IS NULL RAISERROR(N'Unesite polje „Datum isteka“.', 16, 1);
        IF @DatumIsteka < @DatumIzdavanja RAISERROR(N'Datum isteka ne sme biti pre datuma izdavanja.', 16, 1);
        IF NOT EXISTS (SELECT 1 FROM dbo.SRV_Sredstvo s JOIN dbo.SRV_VrstaSredstvaDokument d ON d.VrstaSredstvaID = s.VrstaSredstvaID
                       WHERE s.ID = @SredstvoID AND d.ID = @VrstaDokumentaID)
            RAISERROR(N'Dokument ne pripada vrsti izabranog sredstva.', 16, 1);

        BEGIN TRAN;
        IF @ID IS NULL
        BEGIN
            INSERT INTO dbo.SRV_SredstvoDokument (SredstvoID, VrstaDokumentaID, Broj, DatumIzdavanja, DatumIsteka, Prilog, Napomena, Aktivno, Kreirao)
            VALUES (@SredstvoID, @VrstaDokumentaID, @Broj, @DatumIzdavanja, @DatumIsteka, @Prilog, @Napomena, @Aktivno, @Korisnik);
            SET @ID = SCOPE_IDENTITY();
            INSERT INTO dbo.SRV_Log (Tabela, ZapisID, Opis, Novo, Korisnik)
            VALUES ('SRV_SredstvoDokument', @ID, N'Novi dokument sredstva', ISNULL(@Broj, N'') + N' do ' + CONVERT(nvarchar(10), @DatumIsteka, 104), @Korisnik);
        END
        ELSE
        BEGIN
            INSERT INTO dbo.SRV_Log (Tabela, ZapisID, Opis, Staro, Novo, Korisnik)
            SELECT 'SRV_SredstvoDokument', @ID, N'Izmena dokumenta sredstva',
                   ISNULL(Broj, N'') + N' do ' + CONVERT(nvarchar(10), DatumIsteka, 104),
                   ISNULL(@Broj, N'') + N' do ' + CONVERT(nvarchar(10), @DatumIsteka, 104), @Korisnik
            FROM dbo.SRV_SredstvoDokument WHERE ID = @ID;
            UPDATE dbo.SRV_SredstvoDokument
            SET VrstaDokumentaID = @VrstaDokumentaID, Broj = @Broj, DatumIzdavanja = @DatumIzdavanja,
                ObavestenoPrag = CASE WHEN DatumIsteka <> @DatumIsteka THEN NULL ELSE ObavestenoPrag END,   -- produzenje: novi ciklus obavestenja
                DatumIsteka = @DatumIsteka, Prilog = @Prilog, Napomena = @Napomena, Aktivno = @Aktivno,
                Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE ID = @ID;
        END
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
    SELECT @ID AS ID;
END
GO

/* =====================================================================
   PLAN ODRZAVANJA I NORMATIV (SR-15)
   ===================================================================== */
IF OBJECT_ID('dbo.SRV_SacuvajPlan', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_SacuvajPlan;
GO
CREATE PROCEDURE dbo.SRV_SacuvajPlan
    @ID              int = NULL,
    @VrstaID         int,
    @Naziv           nvarchar(100),
    @IntervalKm      decimal(18,2) = 0,
    @IntervalMH      decimal(18,2) = 0,
    @IntervalDana    int = 0,
    @IntervalMeseci  int = 0,
    @UpozorenjeKm    decimal(18,2) = 0,
    @UpozorenjeMH    decimal(18,2) = 0,
    @UpozorenjeDana  int = 0,
    @Napomena        nvarchar(400) = NULL,
    @Aktivno         bit = 1,
    @Korisnik        nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET @Naziv = LTRIM(RTRIM(ISNULL(@Naziv, N'')));
    SELECT @IntervalKm = ISNULL(@IntervalKm, 0), @IntervalMH = ISNULL(@IntervalMH, 0), @IntervalDana = ISNULL(@IntervalDana, 0),
           @IntervalMeseci = ISNULL(@IntervalMeseci, 0), @UpozorenjeKm = ISNULL(@UpozorenjeKm, 0),
           @UpozorenjeMH = ISNULL(@UpozorenjeMH, 0), @UpozorenjeDana = ISNULL(@UpozorenjeDana, 0);
    BEGIN TRY
        IF @VrstaID IS NULL RAISERROR(N'Unesite polje „Vrsta sredstva“.', 16, 1);
        IF @Naziv = N'' RAISERROR(N'Unesite polje „Naziv“.', 16, 1);
        IF @IntervalKm < 0 OR @IntervalMH < 0 OR @IntervalDana < 0 OR @IntervalMeseci < 0
           OR @UpozorenjeKm < 0 OR @UpozorenjeMH < 0 OR @UpozorenjeDana < 0
            RAISERROR(N'Intervali i upozorenja ne smeju biti negativni.', 16, 1);
        IF NOT (@IntervalKm > 0 OR @IntervalMH > 0 OR @IntervalDana > 0 OR @IntervalMeseci > 0)
            RAISERROR(N'Unesite bar jedan interval veći od 0.', 16, 1);
        IF @UpozorenjeKm > @IntervalKm OR @UpozorenjeMH > @IntervalMH
           OR (@UpozorenjeDana > 0 AND @UpozorenjeDana > @IntervalDana AND @UpozorenjeDana > @IntervalMeseci * 30)
            RAISERROR(N'Upozorenje ne sme biti veće od intervala.', 16, 1);

        BEGIN TRAN;
        IF @ID IS NULL
        BEGIN
            INSERT INTO dbo.SRV_PlanOdrzavanja (VrstaSredstvaID, Naziv, IntervalKm, IntervalMH, IntervalDana, IntervalMeseci,
                                                UpozorenjeKm, UpozorenjeMH, UpozorenjeDana, Napomena, Aktivno, Kreirao)
            VALUES (@VrstaID, @Naziv, @IntervalKm, @IntervalMH, @IntervalDana, @IntervalMeseci,
                    @UpozorenjeKm, @UpozorenjeMH, @UpozorenjeDana, @Napomena, @Aktivno, @Korisnik);
            SET @ID = SCOPE_IDENTITY();
        END
        ELSE
            UPDATE dbo.SRV_PlanOdrzavanja
            SET VrstaSredstvaID = @VrstaID, Naziv = @Naziv, IntervalKm = @IntervalKm, IntervalMH = @IntervalMH,
                IntervalDana = @IntervalDana, IntervalMeseci = @IntervalMeseci, UpozorenjeKm = @UpozorenjeKm,
                UpozorenjeMH = @UpozorenjeMH, UpozorenjeDana = @UpozorenjeDana, Napomena = @Napomena, Aktivno = @Aktivno,
                Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE ID = @ID;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
    SELECT @ID AS ID;
END
GO

IF OBJECT_ID('dbo.SRV_DodajNormativ', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_DodajNormativ;
GO
CREATE PROCEDURE dbo.SRV_DodajNormativ
    @PlanID    int,
    @DeoID     int,
    @Kolicina  decimal(18,3),
    @Korisnik  nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        IF @DeoID IS NULL RAISERROR(N'Unesite polje „Deo“.', 16, 1);
        IF ISNULL(@Kolicina, 0) <= 0 RAISERROR(N'Polje „Količina“ mora biti veće od 0.', 16, 1);
        BEGIN TRAN;
        INSERT INTO dbo.SRV_PlanNormativ (PlanID, DeoID, Kolicina, Kreirao) VALUES (@PlanID, @DeoID, @Kolicina, @Korisnik);
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
    SELECT CAST(SCOPE_IDENTITY() AS int) AS ID;
END
GO

IF OBJECT_ID('dbo.SRV_UkloniNormativ', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_UkloniNormativ;
GO
CREATE PROCEDURE dbo.SRV_UkloniNormativ
    @ID        int,
    @Korisnik  nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    -- normativ nije dokument (nema kretanja), pa se stavka sme ukloniti
    DELETE FROM dbo.SRV_PlanNormativ WHERE ID = @ID;
    INSERT INTO dbo.SRV_Log (Tabela, ZapisID, Opis, Korisnik) VALUES ('SRV_PlanNormativ', @ID, N'Uklonjena stavka normativa', @Korisnik);
END
GO

/* ---------------------------------------------------------------------
   SRV_ProveriMinimum_Int: obavestenje za kritican deo ispod minimuma (MD-R08).
   Jedno obavestenje po prelasku praga; novo tek kada je prethodno procitano
   i deo se vratio iznad minimuma pa ponovo pao ispod.
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_ProveriMinimum_Int', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_ProveriMinimum_Int;
GO
CREATE PROCEDURE dbo.SRV_ProveriMinimum_Int
    @DeoID int
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Rasp decimal(18,3), @Min decimal(18,3), @Krit bit, @Akt bit, @Ispod bit, @Sifra nvarchar(30), @Naziv nvarchar(150), @JM nvarchar(10);

    SELECT @Rasp = z.Raspolozivo, @Min = z.MinZaliha, @Krit = z.Kritican, @Akt = z.Aktivno,
           @Sifra = z.Sifra, @Naziv = z.Naziv, @JM = z.JM, @Ispod = d.IspodMinimuma
    FROM dbo.SRV_vw_Zalihe z JOIN dbo.SRV_Deo d ON d.ID = z.DeoID
    WHERE z.DeoID = @DeoID;

    IF @Krit = 1 AND @Akt = 1 AND @Rasp < @Min
    BEGIN
        IF @Ispod = 0
        BEGIN
            IF NOT EXISTS (SELECT 1 FROM dbo.SRV_DeoObavestenje WHERE DeoID = @DeoID AND Vrsta = N'Deo' AND Procitano = 0)
                INSERT INTO dbo.SRV_DeoObavestenje (Vrsta, DeoID, Tekst, RaspolozivoUTrenutku)
                VALUES (N'Deo', @DeoID,
                        N'Kritičan deo ' + @Sifra + N' ' + @Naziv + N' je ispod minimuma: raspoloživo ' + dbo.SRV_Broj(@Rasp) + N' ' + @JM
                        + N', minimum ' + dbo.SRV_Broj(@Min) + N' ' + @JM, @Rasp);
            UPDATE dbo.SRV_Deo SET IspodMinimuma = 1 WHERE ID = @DeoID;
        END
    END
    ELSE IF @Ispod = 1
        UPDATE dbo.SRV_Deo SET IspodMinimuma = 0 WHERE ID = @DeoID;
END
GO

/* =====================================================================
   DELOVI (MD-R12) I PRIMENLJIVOST
   ===================================================================== */
IF OBJECT_ID('dbo.SRV_SacuvajDeo', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_SacuvajDeo;
GO
CREATE PROCEDURE dbo.SRV_SacuvajDeo
    @ID                int = NULL,
    @Sifra             nvarchar(30),
    @Naziv             nvarchar(150),
    @GrupaID           int,
    @JM                nvarchar(10),
    @MinZaliha         decimal(18,3) = 0,
    @MaxZaliha         decimal(18,3) = 0,
    @Lokacija          nvarchar(50) = NULL,
    @Kritican          bit = 0,
    @DobavljacPaSifra  int = NULL,
    @RokIsporukeDana   int = NULL,
    @Napomena          nvarchar(400) = NULL,
    @Aktivno           bit = 1,
    @Korisnik          nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @Stanje decimal(18,3), @t nvarchar(40), @Staro nvarchar(400);
    SET @Sifra = LTRIM(RTRIM(ISNULL(@Sifra, N'')));
    SET @Naziv = LTRIM(RTRIM(ISNULL(@Naziv, N'')));
    BEGIN TRY
        IF @Sifra = N'' RAISERROR(N'Unesite polje „Šifra“.', 16, 1);
        IF @Naziv = N'' RAISERROR(N'Unesite polje „Naziv“.', 16, 1);
        IF @GrupaID IS NULL RAISERROR(N'Unesite polje „Grupa“.', 16, 1);
        IF LTRIM(RTRIM(ISNULL(@JM, N''))) = N'' RAISERROR(N'Unesite polje „Jedinica mere“.', 16, 1);
        IF ISNULL(@MinZaliha, 0) < 0 RAISERROR(N'Polje „Minimalna zaliha“ ne sme biti negativno.', 16, 1);
        IF ISNULL(@MaxZaliha, 0) < ISNULL(@MinZaliha, 0) RAISERROR(N'Maksimalna zaliha mora biti veća ili jednaka minimalnoj.', 16, 1);

        BEGIN TRAN;
        IF @ID IS NOT NULL AND @Aktivno = 0
        BEGIN
            SELECT @Stanje = Stanje FROM dbo.SRV_vw_DeoStanje WHERE DeoID = @ID;
            IF @Stanje <> 0
            BEGIN
                SET @t = dbo.SRV_Broj(@Stanje);
                RAISERROR(N'Deo ima stanje %s i ne može se deaktivirati.', 16, 1, @t);
            END
            IF EXISTS (SELECT 1 FROM dbo.SRV_NalogDeo nd JOIN dbo.SRV_Nalog n ON n.ID = nd.NalogID
                       WHERE nd.DeoID = @ID AND nd.Status IN (N'Rezervisano', N'Nema na stanju') AND n.Status NOT IN (N'Završen', N'Otkazan'))
                RAISERROR(N'Deo je rezervisan na otvorenom servisnom nalogu i ne može se deaktivirati.', 16, 1);
        END

        IF @ID IS NULL
        BEGIN
            INSERT INTO dbo.SRV_Deo (Sifra, Naziv, DeoGrupaID, JM, MinZaliha, MaxZaliha, Lokacija, Kritican, DobavljacPaSifra,
                                     RokIsporukeDana, Napomena, Aktivno, Kreirao)
            VALUES (@Sifra, @Naziv, @GrupaID, @JM, ISNULL(@MinZaliha, 0), ISNULL(@MaxZaliha, 0), @Lokacija, @Kritican, @DobavljacPaSifra,
                    @RokIsporukeDana, @Napomena, @Aktivno, @Korisnik);
            SET @ID = SCOPE_IDENTITY();
            INSERT INTO dbo.SRV_Log (Tabela, ZapisID, Opis, Novo, Korisnik) VALUES ('SRV_Deo', @ID, N'Novi deo', @Sifra + N' ' + @Naziv, @Korisnik);
        END
        ELSE
        BEGIN
            SELECT @Staro = Sifra + N' | ' + Naziv + N' | min ' + dbo.SRV_Broj(MinZaliha) + N' | max ' + dbo.SRV_Broj(MaxZaliha)
                            + N' | ' + CASE WHEN Kritican = 1 THEN N'kritičan' ELSE N'-' END + N' | ' + CASE WHEN Aktivno = 1 THEN N'aktivan' ELSE N'neaktivan' END
            FROM dbo.SRV_Deo WHERE ID = @ID;
            UPDATE dbo.SRV_Deo
            SET Sifra = @Sifra, Naziv = @Naziv, DeoGrupaID = @GrupaID, JM = @JM, MinZaliha = ISNULL(@MinZaliha, 0),
                MaxZaliha = ISNULL(@MaxZaliha, 0), Lokacija = @Lokacija, Kritican = @Kritican, DobavljacPaSifra = @DobavljacPaSifra,
                RokIsporukeDana = @RokIsporukeDana, Napomena = @Napomena, Aktivno = @Aktivno,
                Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE ID = @ID;
            INSERT INTO dbo.SRV_Log (Tabela, ZapisID, Opis, Staro, Novo, Korisnik)
            VALUES ('SRV_Deo', @ID, N'Izmena dela', @Staro,
                    @Sifra + N' | ' + @Naziv + N' | min ' + dbo.SRV_Broj(@MinZaliha) + N' | max ' + dbo.SRV_Broj(@MaxZaliha)
                    + N' | ' + CASE WHEN @Kritican = 1 THEN N'kritičan' ELSE N'-' END + N' | ' + CASE WHEN @Aktivno = 1 THEN N'aktivan' ELSE N'neaktivan' END,
                    @Korisnik);
            -- promena minimuma ili kriticnosti moze da otvori/zatvori prag obavestenja
            EXEC dbo.SRV_ProveriMinimum_Int @ID;
        END
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
    SELECT @ID AS ID;
END
GO

IF OBJECT_ID('dbo.SRV_DodajPrimenljivost', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_DodajPrimenljivost;
GO
CREATE PROCEDURE dbo.SRV_DodajPrimenljivost
    @DeoID       int,
    @VrstaID     int,
    @SredstvoID  int = NULL,
    @Korisnik    nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        IF @VrstaID IS NULL RAISERROR(N'Unesite polje „Vrsta sredstva“.', 16, 1);
        IF @SredstvoID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.SRV_Sredstvo WHERE ID = @SredstvoID AND VrstaSredstvaID = @VrstaID)
            RAISERROR(N'Izabrano sredstvo nije te vrste.', 16, 1);
        BEGIN TRAN;
        INSERT INTO dbo.SRV_DeoPrimenljivost (DeoID, VrstaSredstvaID, SredstvoID, Kreirao) VALUES (@DeoID, @VrstaID, @SredstvoID, @Korisnik);
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
END
GO

IF OBJECT_ID('dbo.SRV_UkloniPrimenljivost', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_UkloniPrimenljivost;
GO
CREATE PROCEDURE dbo.SRV_UkloniPrimenljivost
    @ID        int,
    @Korisnik  nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM dbo.SRV_DeoPrimenljivost WHERE ID = @ID;
    INSERT INTO dbo.SRV_Log (Tabela, ZapisID, Opis, Korisnik) VALUES ('SRV_DeoPrimenljivost', @ID, N'Uklonjena primenljivost', @Korisnik);
END
GO
