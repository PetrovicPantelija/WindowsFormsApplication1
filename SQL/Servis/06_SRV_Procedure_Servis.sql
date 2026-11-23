/* =====================================================================
   ZAH-010 Servis - 06: procedure servisa
   Servisni nalozi, delovi na nalogu (rezervacija, izdavanje, povrat, storno),
   rad, prijave kvarova i spoljni servis sa odobravanjem.
   Pravila: vidi zaglavlje 04_SRV_Procedure_Sifarnici.sql.
   ===================================================================== */
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

/* ---------------------------------------------------------------------
   Pomocne (_Int): pozivaju se samo unutar transakcije druge procedure
   --------------------------------------------------------------------- */

-- SR-10: zavrsen ili otkazan nalog je zakljucan
IF OBJECT_ID('dbo.SRV_NalogOtvoren_Int', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_NalogOtvoren_Int;
GO
CREATE PROCEDURE dbo.SRV_NalogOtvoren_Int
    @NalogID int
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Status nvarchar(20), @Broj nvarchar(30);
    SELECT @Status = Status, @Broj = Broj FROM dbo.SRV_Nalog WITH (UPDLOCK, HOLDLOCK) WHERE ID = @NalogID;
    IF @Status IS NULL
    BEGIN
        RAISERROR(N'Servisni nalog ne postoji.', 16, 1);
        RETURN 1;
    END
    IF @Status IN (N'Završen', N'Otkazan')
    BEGIN
        RAISERROR(N'Servisni nalog %s je zaključan (%s); izmene nisu moguće.', 16, 1, @Broj, @Status);
        RETURN 1;
    END
    RETURN 0;
END
GO

-- status naloga iz stanja: Ceka odobrenje > Ceka delove > U radu / Otvoren (MD-R03)
IF OBJECT_ID('dbo.SRV_NalogOsveziStatus_Int', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_NalogOsveziStatus_Int;
GO
CREATE PROCEDURE dbo.SRV_NalogOsveziStatus_Int
    @NalogID   int,
    @Korisnik  nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Staro nvarchar(20), @Novo nvarchar(20);
    SELECT @Staro = Status FROM dbo.SRV_Nalog WHERE ID = @NalogID;
    IF @Staro IN (N'Završen', N'Otkazan') RETURN 0;

    SET @Novo = CASE
        WHEN EXISTS (SELECT 1 FROM dbo.SRV_SpoljniServisZahtev WHERE NalogID = @NalogID AND Status = N'Čeka odobrenje') THEN N'Čeka odobrenje'
        WHEN EXISTS (SELECT 1 FROM dbo.SRV_NalogDeo WHERE NalogID = @NalogID AND Status = N'Nema na stanju') THEN N'Čeka delove'
        WHEN @Staro = N'Otvoren'
             AND NOT EXISTS (SELECT 1 FROM dbo.SRV_NalogRad WHERE NalogID = @NalogID AND Aktivno = 1)
             AND NOT EXISTS (SELECT 1 FROM dbo.SRV_NalogDeo WHERE NalogID = @NalogID AND Status = N'Izdato')
             AND NOT EXISTS (SELECT 1 FROM dbo.SRV_SpoljniServisZahtev WHERE NalogID = @NalogID AND Status IN (N'Odobreno', N'Zatvoreno'))
             THEN N'Otvoren'
        ELSE N'U radu' END;

    IF @Novo <> @Staro
    BEGIN
        UPDATE dbo.SRV_Nalog SET Status = @Novo, Izmenio = @Korisnik, DatumIzmene = SYSDATETIME() WHERE ID = @NalogID;
        INSERT INTO dbo.SRV_Log (Tabela, ZapisID, Opis, Staro, Novo, Korisnik)
        VALUES ('SRV_Nalog', @NalogID, N'Promena statusa naloga', @Staro, @Novo, @Korisnik);
    END
    RETURN 0;
END
GO

-- SR-04: sredstvo se vraca u rad kada nema otvorene prijave "ne moze da radi"
IF OBJECT_ID('dbo.SRV_VratiSredstvoURad_Int', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_VratiSredstvoURad_Int;
GO
CREATE PROCEDURE dbo.SRV_VratiSredstvoURad_Int
    @SredstvoID  int,
    @Korisnik    nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM dbo.SRV_Sredstvo WHERE ID = @SredstvoID AND Status = N'U kvaru')
       AND NOT EXISTS (SELECT 1 FROM dbo.SRV_Kvar WHERE SredstvoID = @SredstvoID AND MozeDaRadi = 0 AND Status IN (N'Nova', N'U obradi'))
    BEGIN
        UPDATE dbo.SRV_Sredstvo SET Status = N'U radu', Izmenio = @Korisnik, DatumIzmene = SYSDATETIME() WHERE ID = @SredstvoID;
        INSERT INTO dbo.SRV_Log (Tabela, ZapisID, Opis, Staro, Novo, Korisnik)
        VALUES ('SRV_Sredstvo', @SredstvoID, N'Promena statusa sredstva', N'U kvaru', N'U radu', @Korisnik);
    END
END
GO

-- MD-R01: rezervacija; vise od raspolozivog nije moguce, stavka je "Nema na stanju"
IF OBJECT_ID('dbo.SRV_Rezervisi_Int', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_Rezervisi_Int;
GO
CREATE PROCEDURE dbo.SRV_Rezervisi_Int
    @NalogID      int,
    @DeoID        int,
    @Kolicina     decimal(18,3),
    @IzNormativa  bit,
    @Korisnik     nvarchar(50),
    @Status       nvarchar(20) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Akt bit, @Rasp decimal(18,3), @Sifra nvarchar(30);
    SELECT @Akt = Aktivno, @Sifra = Sifra FROM dbo.SRV_Deo WITH (UPDLOCK, HOLDLOCK) WHERE ID = @DeoID;
    IF @Akt IS NULL OR @Akt = 0
    BEGIN
        RAISERROR(N'Deo %s nije aktivan.', 16, 1, @Sifra);
        RETURN 1;
    END
    SELECT @Rasp = Raspolozivo FROM dbo.SRV_vw_Zalihe WHERE DeoID = @DeoID;
    SET @Status = CASE WHEN @Rasp >= @Kolicina THEN N'Rezervisano' ELSE N'Nema na stanju' END;
    INSERT INTO dbo.SRV_NalogDeo (NalogID, DeoID, Kolicina, Status, IzNormativa, Kreirao)
    VALUES (@NalogID, @DeoID, @Kolicina, @Status, @IzNormativa, @Korisnik);
    EXEC dbo.SRV_ProveriMinimum_Int @DeoID;
    RETURN 0;
END
GO

-- MD-R02: izdavanje (izdatnica) izabranih stavki; prazna lista = sve rezervisane
IF OBJECT_ID('dbo.SRV_Izdaj_Int', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_Izdaj_Int;
GO
CREATE PROCEDURE dbo.SRV_Izdaj_Int
    @NalogID          int,
    @IDs              dbo.SRV_IdTip READONLY,
    @DozvoliCenuNula  bit,
    @Korisnik         nvarchar(50),
    @Broj             nvarchar(30) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Sve bit = CASE WHEN EXISTS (SELECT 1 FROM @IDs) THEN 0 ELSE 1 END,
            @NdID int, @DeoID int, @Kol decimal(18,3), @St nvarchar(20), @Cena decimal(18,4), @Stanje decimal(18,3),
            @Rasp decimal(18,3), @Sifra nvarchar(30), @JM nvarchar(10), @t nvarchar(40), @Vreme datetime2(0) = SYSDATETIME();
    DECLARE @Stavke TABLE (ID int, DeoID int, Kolicina decimal(18,3), Status nvarchar(20));

    INSERT INTO @Stavke
    SELECT nd.ID, nd.DeoID, nd.Kolicina, nd.Status
    FROM dbo.SRV_NalogDeo nd WITH (UPDLOCK, HOLDLOCK)
    WHERE nd.NalogID = @NalogID
      AND ((@Sve = 1 AND nd.Status = N'Rezervisano')
        OR (@Sve = 0 AND nd.Status IN (N'Rezervisano', N'Nema na stanju') AND nd.ID IN (SELECT ID FROM @IDs)));
    IF NOT EXISTS (SELECT 1 FROM @Stavke)
    BEGIN
        RAISERROR(N'Nema rezervisanih stavki za izdavanje.', 16, 1);
        RETURN 1;
    END

    EXEC dbo.SRV_SledeciBroj 'Izdatnica', @Broj OUTPUT;

    DECLARE c CURSOR LOCAL FAST_FORWARD FOR SELECT ID, DeoID, Kolicina, Status FROM @Stavke ORDER BY ID;
    OPEN c;
    FETCH NEXT FROM c INTO @NdID, @DeoID, @Kol, @St;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SELECT @Cena = ProsecnaCena, @Sifra = Sifra, @JM = JM FROM dbo.SRV_Deo WITH (UPDLOCK, HOLDLOCK) WHERE ID = @DeoID;
        SELECT @Stanje = Stanje, @Rasp = Raspolozivo FROM dbo.SRV_vw_Zalihe WHERE DeoID = @DeoID;

        IF @St = N'Nema na stanju' AND @Rasp < @Kol
        BEGIN
            SET @t = dbo.SRV_Broj(@Rasp) + N' ' + @JM;
            RAISERROR(N'Deo %s nema dovoljno na stanju (raspoloživo %s).', 16, 1, @Sifra, @t);
            RETURN 1;
        END
        IF @Stanje < @Kol
        BEGIN
            SET @t = dbo.SRV_Broj(@Stanje) + N' ' + @JM;
            RAISERROR(N'Deo %s nema dovoljno na stanju (stanje %s).', 16, 1, @Sifra, @t);
            RETURN 1;
        END
        -- 14.9: cena 0 ne prolazi tiho kroz izdavanje
        IF @Cena = 0 AND @DozvoliCenuNula = 0
        BEGIN
            RAISERROR(N'Cena dela %s je 0. Potvrdite izdavanje po ceni 0 ili prvo proknjižite prijem sa cenom.', 16, 1, @Sifra);
            RETURN 1;
        END

        INSERT INTO dbo.SRV_DeoKretanje (DeoID, Datum, Tip, DokumentBroj, Kolicina, Cena, NalogID, NalogDeoID, Korisnik)
        VALUES (@DeoID, @Vreme, N'Izdavanje', @Broj, -@Kol, @Cena, @NalogID, @NdID, @Korisnik);
        UPDATE dbo.SRV_NalogDeo
        SET Status = N'Izdato', CenaIzdavanja = @Cena, IzdatnicaBroj = @Broj, Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
        WHERE ID = @NdID;
        EXEC dbo.SRV_ProveriMinimum_Int @DeoID;

        FETCH NEXT FROM c INTO @NdID, @DeoID, @Kol, @St;
    END
    CLOSE c; DEALLOCATE c;
    RETURN 0;
END
GO

/* =====================================================================
   SERVISNI NALOG
   ===================================================================== */
IF OBJECT_ID('dbo.SRV_NalogNovi', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_NalogNovi;
GO
CREATE PROCEDURE dbo.SRV_NalogNovi
    @SredstvoID         int,
    @Vrsta              nvarchar(20),               -- Vanredni, Redovni
    @KvarID             int = NULL,
    @PlanID             int = NULL,
    @Izvrsilac          nvarchar(20) = N'Interni',
    @Opis               nvarchar(1000) = NULL,
    @BrojiloOtvaranje   decimal(18,2) = NULL,       -- NULL = poslednje ocitavanje
    @DozvoliDrugiNalog  bit = 0,                    -- SR-05: drugi nalog iz iste prijave samo uz potvrdu
    @Korisnik           nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @ID int, @Broj nvarchar(30), @VrstaSr int, @StSr nvarchar(20), @Tip nvarchar(20), @Trenutno decimal(18,2),
            @Postojeci nvarchar(30), @KvStatus nvarchar(20), @DeoID int, @Kol decimal(18,3), @St nvarchar(20),
            @Danas date = CAST(GETDATE() AS date);
    BEGIN TRY
        IF @SredstvoID IS NULL RAISERROR(N'Unesite polje „Sredstvo“.', 16, 1);
        IF @Vrsta NOT IN (N'Vanredni', N'Redovni') RAISERROR(N'Unesite polje „Vrsta naloga“.', 16, 1);
        IF @Vrsta = N'Redovni' AND @PlanID IS NULL RAISERROR(N'Unesite polje „Plan održavanja“.', 16, 1);

        SELECT @VrstaSr = VrstaSredstvaID, @StSr = Status FROM dbo.SRV_Sredstvo WHERE ID = @SredstvoID;
        IF @StSr = N'Arhivirano' RAISERROR(N'Sredstvo je arhivirano; nalog se ne može otvoriti.', 16, 1);
        -- SR-11
        IF @PlanID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.SRV_PlanOdrzavanja WHERE ID = @PlanID AND VrstaSredstvaID = @VrstaSr)
            RAISERROR(N'Plan ne pripada vrsti izabranog sredstva.', 16, 1);

        BEGIN TRAN;
        IF @KvarID IS NOT NULL
        BEGIN
            SELECT @KvStatus = Status FROM dbo.SRV_Kvar WITH (UPDLOCK, HOLDLOCK) WHERE ID = @KvarID AND SredstvoID = @SredstvoID;
            IF @KvStatus IS NULL RAISERROR(N'Prijava kvara ne pripada izabranom sredstvu.', 16, 1);
            IF @KvStatus IN (N'Zatvoreno', N'Odbačeno') RAISERROR(N'Prijava kvara je zatvorena ili odbačena.', 16, 1);
            SELECT TOP 1 @Postojeci = Broj FROM dbo.SRV_Nalog WHERE KvarID = @KvarID AND Status <> N'Otkazan' ORDER BY ID DESC;
            IF @Postojeci IS NOT NULL AND @DozvoliDrugiNalog = 0
                RAISERROR(N'Za ovu prijavu kvara već postoji servisni nalog %s.', 16, 1, @Postojeci);
        END

        SELECT TOP 1 @Tip = Tip FROM dbo.SRV_VrstaSredstvaBrojilo WHERE VrstaSredstvaID = @VrstaSr AND Aktivno = 1 ORDER BY Redosled, ID;
        SELECT @Trenutno = Vrednost FROM dbo.SRV_vw_BrojiloTrenutno WHERE SredstvoID = @SredstvoID AND Tip = @Tip;
        IF @BrojiloOtvaranje IS NOT NULL AND @Tip IS NOT NULL AND (@Trenutno IS NULL OR @BrojiloOtvaranje <> @Trenutno)
            EXEC dbo.SRV_UpisiBrojilo_Int @SredstvoID, @Tip, @BrojiloOtvaranje, @Danas, NULL, N'Nalog', @Korisnik;

        EXEC dbo.SRV_SledeciBroj 'Nalog', @Broj OUTPUT;
        INSERT INTO dbo.SRV_Nalog (Broj, SredstvoID, Vrsta, KvarID, PlanID, Izvrsilac, BrojiloTip, BrojiloOtvaranje, Opis, Kreirao)
        VALUES (@Broj, @SredstvoID, @Vrsta, @KvarID, @PlanID, ISNULL(@Izvrsilac, N'Interni'), @Tip, ISNULL(@BrojiloOtvaranje, @Trenutno),
                @Opis, @Korisnik);
        SET @ID = SCOPE_IDENTITY();

        IF @KvarID IS NOT NULL
            UPDATE dbo.SRV_Kvar SET Status = N'U obradi', Izmenio = @Korisnik, DatumIzmene = SYSDATETIME() WHERE ID = @KvarID AND Status = N'Nova';

        -- MD-R09: redovni nalog rezervise delove iz normativa; kojih nema idu u predlog nabavke
        IF @PlanID IS NOT NULL
        BEGIN
            DECLARE c CURSOR LOCAL FAST_FORWARD FOR
                SELECT n.DeoID, n.Kolicina FROM dbo.SRV_PlanNormativ n JOIN dbo.SRV_Deo d ON d.ID = n.DeoID
                WHERE n.PlanID = @PlanID AND d.Aktivno = 1 ORDER BY n.ID;
            OPEN c;
            FETCH NEXT FROM c INTO @DeoID, @Kol;
            WHILE @@FETCH_STATUS = 0
            BEGIN
                EXEC dbo.SRV_Rezervisi_Int @ID, @DeoID, @Kol, 1, @Korisnik, @St OUTPUT;
                FETCH NEXT FROM c INTO @DeoID, @Kol;
            END
            CLOSE c; DEALLOCATE c;
        END

        INSERT INTO dbo.SRV_Log (Tabela, ZapisID, Opis, Novo, Korisnik) VALUES ('SRV_Nalog', @ID, N'Novi servisni nalog', @Broj, @Korisnik);
        EXEC dbo.SRV_NalogOsveziStatus_Int @ID, @Korisnik;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
    SELECT @ID AS ID, @Broj AS Broj;
END
GO

IF OBJECT_ID('dbo.SRV_NalogSacuvaj', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_NalogSacuvaj;
GO
CREATE PROCEDURE dbo.SRV_NalogSacuvaj
    @ID         int,
    @Izvrsilac  nvarchar(20),
    @Opis       nvarchar(1000) = NULL,
    @Korisnik   nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRAN;
        EXEC dbo.SRV_NalogOtvoren_Int @ID;
        UPDATE dbo.SRV_Nalog SET Izvrsilac = @Izvrsilac, Opis = @Opis, Izmenio = @Korisnik, DatumIzmene = SYSDATETIME() WHERE ID = @ID;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
END
GO

IF OBJECT_ID('dbo.SRV_NalogDodajDeo', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_NalogDodajDeo;
GO
CREATE PROCEDURE dbo.SRV_NalogDodajDeo
    @NalogID   int,
    @DeoID     int,
    @Kolicina  decimal(18,3),
    @Korisnik  nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @Status nvarchar(20);
    BEGIN TRY
        IF @DeoID IS NULL RAISERROR(N'Unesite polje „Deo“.', 16, 1);
        IF ISNULL(@Kolicina, 0) <= 0 RAISERROR(N'Polje „Količina“ mora biti veće od 0.', 16, 1);
        BEGIN TRAN;
        EXEC dbo.SRV_NalogOtvoren_Int @NalogID;
        EXEC dbo.SRV_Rezervisi_Int @NalogID, @DeoID, @Kolicina, 0, @Korisnik, @Status OUTPUT;
        EXEC dbo.SRV_NalogOsveziStatus_Int @NalogID, @Korisnik;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
    SELECT @Status AS Status;
END
GO

IF OBJECT_ID('dbo.SRV_NalogIzdaj', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_NalogIzdaj;
GO
CREATE PROCEDURE dbo.SRV_NalogIzdaj
    @NalogID          int,
    @IDs              dbo.SRV_IdTip READONLY,     -- prazno = sve rezervisane stavke
    @DozvoliCenuNula  bit = 0,
    @Korisnik         nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @Broj nvarchar(30);
    BEGIN TRY
        BEGIN TRAN;
        EXEC dbo.SRV_NalogOtvoren_Int @NalogID;
        EXEC dbo.SRV_Izdaj_Int @NalogID, @IDs, @DozvoliCenuNula, @Korisnik, @Broj OUTPUT;
        EXEC dbo.SRV_NalogOsveziStatus_Int @NalogID, @Korisnik;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
    SELECT @Broj AS Broj;
END
GO

-- MD-R04: povrat po ceni sa izdatnice; najvise izdato - vec vraceno
IF OBJECT_ID('dbo.SRV_NalogPovrat', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_NalogPovrat;
GO
CREATE PROCEDURE dbo.SRV_NalogPovrat
    @NalogDeoID  int,
    @Kolicina    decimal(18,3),
    @Napomena    nvarchar(400) = NULL,
    @Korisnik    nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @NalogID int, @DeoID int, @St nvarchar(20), @Max decimal(18,3), @Cena decimal(18,4), @Broj nvarchar(30), @t nvarchar(40);
    BEGIN TRY
        BEGIN TRAN;
        SELECT @NalogID = NalogID, @DeoID = DeoID, @St = Status, @Max = Kolicina - VracenoKolicina, @Cena = CenaIzdavanja
        FROM dbo.SRV_NalogDeo WITH (UPDLOCK, HOLDLOCK) WHERE ID = @NalogDeoID;
        EXEC dbo.SRV_NalogOtvoren_Int @NalogID;
        IF @St <> N'Izdato' RAISERROR(N'Povrat je moguć samo za izdatu stavku.', 16, 1);
        IF ISNULL(@Kolicina, 0) <= 0 OR @Kolicina > @Max
        BEGIN
            SET @t = dbo.SRV_Broj(@Max);
            RAISERROR(N'Količina mora biti veća od 0 i najviše %s.', 16, 1, @t);
        END

        EXEC dbo.SRV_SledeciBroj 'Povrat', @Broj OUTPUT;
        DECLARE @Vreme datetime2(0) = SYSDATETIME();
        EXEC dbo.SRV_Ulaz_Int @DeoID, @Kolicina, @Cena, N'Povrat', @Broj, @Vreme, NULL, NULL, @NalogID, @NalogDeoID, @Napomena, @Korisnik;
        UPDATE dbo.SRV_NalogDeo SET VracenoKolicina = VracenoKolicina + @Kolicina, Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
        WHERE ID = @NalogDeoID;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
    SELECT @Broj AS Broj;
END
GO

IF OBJECT_ID('dbo.SRV_NalogStornirajStavku', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_NalogStornirajStavku;
GO
CREATE PROCEDURE dbo.SRV_NalogStornirajStavku
    @NalogDeoID  int,
    @Korisnik    nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @NalogID int, @DeoID int, @St nvarchar(20);
    BEGIN TRY
        BEGIN TRAN;
        SELECT @NalogID = NalogID, @DeoID = DeoID, @St = Status FROM dbo.SRV_NalogDeo WITH (UPDLOCK, HOLDLOCK) WHERE ID = @NalogDeoID;
        EXEC dbo.SRV_NalogOtvoren_Int @NalogID;
        IF @St = N'Izdato' RAISERROR(N'Izdata stavka se ne stornira; uradite povrat.', 16, 1);
        IF @St = N'Storno' RAISERROR(N'Stavka je već stornirana.', 16, 1);
        UPDATE dbo.SRV_NalogDeo SET Status = N'Storno', Izmenio = @Korisnik, DatumIzmene = SYSDATETIME() WHERE ID = @NalogDeoID;
        EXEC dbo.SRV_ProveriMinimum_Int @DeoID;
        EXEC dbo.SRV_NalogOsveziStatus_Int @NalogID, @Korisnik;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
END
GO

IF OBJECT_ID('dbo.SRV_NalogDodajRad', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_NalogDodajRad;
GO
CREATE PROCEDURE dbo.SRV_NalogDodajRad
    @NalogID    int,
    @Osoba      nvarchar(100),
    @Zanimanje  nvarchar(50) = NULL,
    @Datum      date,
    @Sati       decimal(9,2),
    @CenaSata   decimal(18,2) = 0,
    @Opis       nvarchar(400) = NULL,
    @Korisnik   nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        IF LTRIM(RTRIM(ISNULL(@Osoba, N''))) = N'' RAISERROR(N'Unesite polje „Izvršilac (osoba)“.', 16, 1);
        IF ISNULL(@Sati, 0) <= 0 RAISERROR(N'Polje „Sati“ mora biti veće od 0.', 16, 1);
        IF ISNULL(@CenaSata, 0) < 0 RAISERROR(N'Polje „Cena po satu“ ne sme biti negativno.', 16, 1);
        IF @Datum IS NULL OR @Datum > CAST(GETDATE() AS date) RAISERROR(N'Datum rada ne sme biti prazan ni u budućnosti.', 16, 1);
        BEGIN TRAN;
        EXEC dbo.SRV_NalogOtvoren_Int @NalogID;
        INSERT INTO dbo.SRV_NalogRad (NalogID, Osoba, Zanimanje, Datum, Sati, CenaSata, Opis, Kreirao)
        VALUES (@NalogID, LTRIM(RTRIM(@Osoba)), @Zanimanje, @Datum, @Sati, ISNULL(@CenaSata, 0), @Opis, @Korisnik);
        EXEC dbo.SRV_NalogOsveziStatus_Int @NalogID, @Korisnik;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
END
GO

IF OBJECT_ID('dbo.SRV_NalogUkloniRad', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_NalogUkloniRad;
GO
CREATE PROCEDURE dbo.SRV_NalogUkloniRad
    @ID        int,
    @Korisnik  nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @NalogID int;
    BEGIN TRY
        BEGIN TRAN;
        SELECT @NalogID = NalogID FROM dbo.SRV_NalogRad WHERE ID = @ID;
        EXEC dbo.SRV_NalogOtvoren_Int @NalogID;
        UPDATE dbo.SRV_NalogRad SET Aktivno = 0 WHERE ID = @ID;
        INSERT INTO dbo.SRV_Log (Tabela, ZapisID, Opis, Korisnik) VALUES ('SRV_NalogRad', @ID, N'Uklonjen rad sa naloga', @Korisnik);
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
END
GO

-- Zavrsetak naloga: SR-07, SR-08, SR-04
IF OBJECT_ID('dbo.SRV_NalogZavrsi', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_NalogZavrsi;
GO
CREATE PROCEDURE dbo.SRV_NalogZavrsi
    @ID                 int,
    @BrojiloZatvaranje  decimal(18,2) = NULL,
    @NeizdatoAkcija     nvarchar(20) = NULL,        -- Izdaj (rezervisane) ili Storniraj; obavezno kada ima neizdatih stavki
    @DozvoliCenuNula    bit = 0,
    @Korisnik           nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @SredstvoID int, @KvarID int, @Tip nvarchar(20), @Otv decimal(18,2), @Broj nvarchar(30), @Zahtev nvarchar(30),
            @t nvarchar(40), @Izd nvarchar(30), @Danas date = CAST(GETDATE() AS date);
    DECLARE @Rez dbo.SRV_IdTip;
    BEGIN TRY
        BEGIN TRAN;
        EXEC dbo.SRV_NalogOtvoren_Int @ID;
        SELECT @SredstvoID = SredstvoID, @KvarID = KvarID, @Tip = BrojiloTip, @Otv = BrojiloOtvaranje, @Broj = Broj
        FROM dbo.SRV_Nalog WHERE ID = @ID;

        SELECT TOP 1 @Zahtev = Broj FROM dbo.SRV_SpoljniServisZahtev WHERE NalogID = @ID AND Status IN (N'Čeka odobrenje', N'Vraćeno na doradu');
        IF @Zahtev IS NOT NULL RAISERROR(N'Zahtev za spoljni servis %s nije rešen; nalog se ne može završiti.', 16, 1, @Zahtev);

        -- SR-08: rezervisane a neizdate stavke traze izbor
        IF EXISTS (SELECT 1 FROM dbo.SRV_NalogDeo WHERE NalogID = @ID AND Status IN (N'Rezervisano', N'Nema na stanju'))
        BEGIN
            IF @NeizdatoAkcija IS NULL
                RAISERROR(N'Nalog ima rezervisane a neizdate delove. Izaberite: izdaj ili storniraj rezervacije.', 16, 1);
            IF @NeizdatoAkcija = N'Izdaj' AND EXISTS (SELECT 1 FROM dbo.SRV_NalogDeo WHERE NalogID = @ID AND Status = N'Rezervisano')
            BEGIN
                INSERT INTO @Rez SELECT ID FROM dbo.SRV_NalogDeo WHERE NalogID = @ID AND Status = N'Rezervisano';
                EXEC dbo.SRV_Izdaj_Int @ID, @Rez, @DozvoliCenuNula, @Korisnik, @Izd OUTPUT;
            END
            UPDATE dbo.SRV_NalogDeo SET Status = N'Storno', Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE NalogID = @ID AND Status IN (N'Rezervisano', N'Nema na stanju');
        END

        -- SR-07
        IF @Tip IS NOT NULL
        BEGIN
            IF @BrojiloZatvaranje IS NULL RAISERROR(N'Unesite polje „Brojilo pri zatvaranju“.', 16, 1);
            IF @Otv IS NOT NULL AND @BrojiloZatvaranje < @Otv
            BEGIN
                SET @t = dbo.SRV_Broj(@Otv);
                RAISERROR(N'Brojilo pri zatvaranju ne sme biti manje od brojila pri otvaranju (%s).', 16, 1, @t);
            END
            EXEC dbo.SRV_UpisiBrojilo_Int @SredstvoID, @Tip, @BrojiloZatvaranje, @Danas, NULL, N'Nalog', @Korisnik;
        END

        UPDATE dbo.SRV_Nalog
        SET Status = N'Završen', DatumZavrsetka = SYSDATETIME(), BrojiloZatvaranje = @BrojiloZatvaranje,
            Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
        WHERE ID = @ID;
        INSERT INTO dbo.SRV_Log (Tabela, ZapisID, Opis, Novo, Korisnik) VALUES ('SRV_Nalog', @ID, N'Nalog završen', @Broj, @Korisnik);

        IF @KvarID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.SRV_Nalog WHERE KvarID = @KvarID AND ID <> @ID AND Status NOT IN (N'Završen', N'Otkazan'))
            UPDATE dbo.SRV_Kvar SET Status = N'Zatvoreno', Izmenio = @Korisnik, DatumIzmene = SYSDATETIME() WHERE ID = @KvarID;
        EXEC dbo.SRV_VratiSredstvoURad_Int @SredstvoID, @Korisnik;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
    SELECT @Izd AS Izdatnica;
END
GO

-- Otkazivanje: SR-09
IF OBJECT_ID('dbo.SRV_NalogOtkazi', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_NalogOtkazi;
GO
CREATE PROCEDURE dbo.SRV_NalogOtkazi
    @ID        int,
    @Razlog    nvarchar(400),
    @Korisnik  nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @KvarID int, @s nvarchar(30), @Broj nvarchar(30);
    BEGIN TRY
        IF LTRIM(RTRIM(ISNULL(@Razlog, N''))) = N'' RAISERROR(N'Unesite polje „Razlog otkazivanja“.', 16, 1);
        BEGIN TRAN;
        EXEC dbo.SRV_NalogOtvoren_Int @ID;
        SELECT @KvarID = KvarID, @Broj = Broj FROM dbo.SRV_Nalog WHERE ID = @ID;

        SELECT TOP 1 @s = d.Sifra FROM dbo.SRV_NalogDeo nd JOIN dbo.SRV_Deo d ON d.ID = nd.DeoID
        WHERE nd.NalogID = @ID AND nd.Status = N'Izdato' AND nd.Kolicina > nd.VracenoKolicina;
        IF @s IS NOT NULL RAISERROR(N'Na nalogu ima izdatih delova (npr. %s). Pre otkazivanja uradite povrat.', 16, 1, @s);

        -- rezervacije se oslobadjaju
        UPDATE dbo.SRV_NalogDeo SET Status = N'Storno', Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
        WHERE NalogID = @ID AND Status IN (N'Rezervisano', N'Nema na stanju');

        -- zahtevi za spoljni servis koji nisu odobreni se odbijaju
        INSERT INTO dbo.SRV_SpoljniServisIstorija (ZahtevID, Korisnik, Radnja, Komentar)
        SELECT ID, @Korisnik, N'Odbijeno', N'Nalog otkazan: ' + @Razlog FROM dbo.SRV_SpoljniServisZahtev
        WHERE NalogID = @ID AND Status IN (N'Čeka odobrenje', N'Vraćeno na doradu');
        UPDATE dbo.SRV_SpoljniServisZahtev SET Status = N'Odbijeno', Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
        WHERE NalogID = @ID AND Status IN (N'Čeka odobrenje', N'Vraćeno na doradu');

        UPDATE dbo.SRV_Nalog
        SET Status = N'Otkazan', RazlogOtkazivanja = LTRIM(RTRIM(@Razlog)), DatumZavrsetka = SYSDATETIME(),
            Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
        WHERE ID = @ID;
        INSERT INTO dbo.SRV_Log (Tabela, ZapisID, Opis, Novo, Korisnik) VALUES ('SRV_Nalog', @ID, N'Nalog otkazan', @Broj + N' | ' + @Razlog, @Korisnik);

        -- prijava kvara ostaje otvorena (moze novi nalog)
        IF @KvarID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.SRV_Nalog WHERE KvarID = @KvarID AND Status NOT IN (N'Završen', N'Otkazan'))
            UPDATE dbo.SRV_Kvar SET Status = N'Nova', Izmenio = @Korisnik, DatumIzmene = SYSDATETIME() WHERE ID = @KvarID AND Status = N'U obradi';

        DECLARE @DeoID int;
        DECLARE c CURSOR LOCAL FAST_FORWARD FOR SELECT DISTINCT DeoID FROM dbo.SRV_NalogDeo WHERE NalogID = @ID;
        OPEN c;
        FETCH NEXT FROM c INTO @DeoID;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            EXEC dbo.SRV_ProveriMinimum_Int @DeoID;
            FETCH NEXT FROM c INTO @DeoID;
        END
        CLOSE c; DEALLOCATE c;
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
   PRIJAVA KVARA (SR-04, SR-05, SR-06)
   ===================================================================== */
IF OBJECT_ID('dbo.SRV_KvarSacuvaj', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_KvarSacuvaj;
GO
CREATE PROCEDURE dbo.SRV_KvarSacuvaj
    @ID          int = NULL,
    @SredstvoID  int,
    @Prijavio    nvarchar(100),
    @Kanal       nvarchar(30) = NULL,
    @Kategorija  nvarchar(50) = NULL,
    @Hitnost     nvarchar(20) = N'Srednja',
    @MozeDaRadi  bit = 1,
    @Opis        nvarchar(1000),
    @Brojilo     decimal(18,2) = NULL,
    @Korisnik    nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @Broj nvarchar(30), @Tip nvarchar(20), @Trenutno decimal(18,2), @Status nvarchar(20), @StSr nvarchar(20),
            @Danas date = CAST(GETDATE() AS date);
    BEGIN TRY
        IF @SredstvoID IS NULL RAISERROR(N'Unesite polje „Sredstvo“.', 16, 1);
        IF LTRIM(RTRIM(ISNULL(@Prijavio, N''))) = N'' RAISERROR(N'Unesite polje „Prijavio“.', 16, 1);
        IF LTRIM(RTRIM(ISNULL(@Opis, N''))) = N'' RAISERROR(N'Unesite polje „Opis kvara“.', 16, 1);

        BEGIN TRAN;
        SELECT @StSr = s.Status FROM dbo.SRV_Sredstvo s WITH (UPDLOCK, HOLDLOCK) WHERE s.ID = @SredstvoID;
        IF @StSr = N'Arhivirano' RAISERROR(N'Sredstvo je arhivirano.', 16, 1);
        SELECT TOP 1 @Tip = b.Tip FROM dbo.SRV_Sredstvo s JOIN dbo.SRV_VrstaSredstvaBrojilo b ON b.VrstaSredstvaID = s.VrstaSredstvaID AND b.Aktivno = 1
        WHERE s.ID = @SredstvoID ORDER BY b.Redosled, b.ID;

        IF @ID IS NULL
        BEGIN
            IF @Brojilo IS NOT NULL AND @Tip IS NOT NULL
            BEGIN
                SELECT @Trenutno = Vrednost FROM dbo.SRV_vw_BrojiloTrenutno WHERE SredstvoID = @SredstvoID AND Tip = @Tip;
                IF @Trenutno IS NULL OR @Brojilo <> @Trenutno
                    EXEC dbo.SRV_UpisiBrojilo_Int @SredstvoID, @Tip, @Brojilo, @Danas, NULL, N'Kvar', @Korisnik;
            END
            EXEC dbo.SRV_SledeciBroj 'Kvar', @Broj OUTPUT;
            INSERT INTO dbo.SRV_Kvar (Broj, SredstvoID, Prijavio, Kanal, BrojiloTip, Brojilo, Kategorija, Hitnost, MozeDaRadi, Opis, Kreirao)
            VALUES (@Broj, @SredstvoID, LTRIM(RTRIM(@Prijavio)), @Kanal, CASE WHEN @Brojilo IS NULL THEN NULL ELSE @Tip END, @Brojilo,
                    @Kategorija, ISNULL(@Hitnost, N'Srednja'), @MozeDaRadi, LTRIM(RTRIM(@Opis)), @Korisnik);
            SET @ID = SCOPE_IDENTITY();
        END
        ELSE
        BEGIN
            SELECT @Status = Status, @Broj = Broj FROM dbo.SRV_Kvar WITH (UPDLOCK, HOLDLOCK) WHERE ID = @ID;
            IF @Status NOT IN (N'Nova', N'U obradi') RAISERROR(N'Prijava u statusu „%s“ se ne menja.', 16, 1, @Status);
            UPDATE dbo.SRV_Kvar
            SET Prijavio = LTRIM(RTRIM(@Prijavio)), Kanal = @Kanal, Kategorija = @Kategorija, Hitnost = ISNULL(@Hitnost, N'Srednja'),
                MozeDaRadi = @MozeDaRadi, Opis = LTRIM(RTRIM(@Opis)), Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE ID = @ID;
        END

        -- SR-04: "ne moze da nastavi rad" stavlja sredstvo u kvar
        IF @MozeDaRadi = 0 AND @StSr = N'U radu'
        BEGIN
            UPDATE dbo.SRV_Sredstvo SET Status = N'U kvaru', Izmenio = @Korisnik, DatumIzmene = SYSDATETIME() WHERE ID = @SredstvoID;
            INSERT INTO dbo.SRV_Log (Tabela, ZapisID, Opis, Staro, Novo, Korisnik)
            VALUES ('SRV_Sredstvo', @SredstvoID, N'Promena statusa sredstva (prijava ' + @Broj + N')', N'U radu', N'U kvaru', @Korisnik);
        END
        ELSE IF @MozeDaRadi = 1
            EXEC dbo.SRV_VratiSredstvoURad_Int @SredstvoID, @Korisnik;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
    SELECT @ID AS ID, @Broj AS Broj;
END
GO

-- Odbaci (SR-06: razlog obavezan) ili Zatvori (SR-05: ne dok postoji otvoren nalog)
IF OBJECT_ID('dbo.SRV_KvarAkcija', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_KvarAkcija;
GO
CREATE PROCEDURE dbo.SRV_KvarAkcija
    @ID        int,
    @Akcija    nvarchar(20),
    @Razlog    nvarchar(400) = NULL,
    @Korisnik  nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @Status nvarchar(20), @SredstvoID int, @Nalog nvarchar(30);
    BEGIN TRY
        IF @Akcija = N'Odbaci' AND LTRIM(RTRIM(ISNULL(@Razlog, N''))) = N''
            RAISERROR(N'Unesite polje „Razlog odbacivanja“.', 16, 1);
        BEGIN TRAN;
        SELECT @Status = Status, @SredstvoID = SredstvoID FROM dbo.SRV_Kvar WITH (UPDLOCK, HOLDLOCK) WHERE ID = @ID;
        IF @Status NOT IN (N'Nova', N'U obradi') RAISERROR(N'Prijava je već zatvorena ili odbačena.', 16, 1);
        SELECT TOP 1 @Nalog = Broj FROM dbo.SRV_Nalog WHERE KvarID = @ID AND Status NOT IN (N'Završen', N'Otkazan');
        IF @Nalog IS NOT NULL
            RAISERROR(N'Prijava ima otvoren servisni nalog %s; zatvara se završetkom naloga.', 16, 1, @Nalog);

        IF @Akcija = N'Odbaci'
            UPDATE dbo.SRV_Kvar SET Status = N'Odbačeno', RazlogOdbacivanja = LTRIM(RTRIM(@Razlog)), Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE ID = @ID;
        ELSE IF @Akcija = N'Zatvori'
            UPDATE dbo.SRV_Kvar SET Status = N'Zatvoreno', Izmenio = @Korisnik, DatumIzmene = SYSDATETIME() WHERE ID = @ID;
        ELSE RAISERROR(N'Nepoznata radnja.', 16, 1);

        INSERT INTO dbo.SRV_Log (Tabela, ZapisID, Opis, Staro, Novo, Korisnik)
        VALUES ('SRV_Kvar', @ID, N'Prijava kvara: ' + @Akcija, @Status, @Razlog, @Korisnik);
        EXEC dbo.SRV_VratiSredstvoURad_Int @SredstvoID, @Korisnik;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
END
GO

IF OBJECT_ID('dbo.SRV_KvarPrilogDodaj', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_KvarPrilogDodaj;
GO
CREATE PROCEDURE dbo.SRV_KvarPrilogDodaj
    @KvarID    int,
    @Putanja   nvarchar(400),
    @Naziv     nvarchar(200) = NULL,
    @Korisnik  nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    IF LTRIM(RTRIM(ISNULL(@Putanja, N''))) = N''
    BEGIN
        RAISERROR(N'Izaberite fajl priloga.', 16, 1);
        RETURN 1;
    END
    INSERT INTO dbo.SRV_KvarPrilog (KvarID, Putanja, Naziv, Kreirao) VALUES (@KvarID, @Putanja, @Naziv, @Korisnik);
END
GO

IF OBJECT_ID('dbo.SRV_KvarPrilogUkloni', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_KvarPrilogUkloni;
GO
CREATE PROCEDURE dbo.SRV_KvarPrilogUkloni
    @ID        int,
    @Korisnik  nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.SRV_KvarPrilog SET Aktivno = 0 WHERE ID = @ID;
    INSERT INTO dbo.SRV_Log (Tabela, ZapisID, Opis, Korisnik) VALUES ('SRV_KvarPrilog', @ID, N'Uklonjen prilog kvara', @Korisnik);
END
GO

/* =====================================================================
   SPOLJNI SERVIS (SR-12, SR-13)
   ===================================================================== */
IF OBJECT_ID('dbo.SRV_ZahtevSacuvaj', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_ZahtevSacuvaj;
GO
CREATE PROCEDURE dbo.SRV_ZahtevSacuvaj
    @ID          int = NULL,
    @NalogID     int,
    @PaSifra     int,
    @BrojPonude  nvarchar(50) = NULL,
    @Razlog      nvarchar(1000),
    @Iznos       decimal(18,2),
    @Korisnik    nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @Broj nvarchar(30), @Nivo tinyint = dbo.SRV_NivoOdobrenja(@Iznos), @Status nvarchar(30);
    BEGIN TRY
        IF @NalogID IS NULL RAISERROR(N'Unesite polje „Servisni nalog“.', 16, 1);
        IF @PaSifra IS NULL RAISERROR(N'Unesite polje „Spoljni servis (partner)“.', 16, 1);
        IF LTRIM(RTRIM(ISNULL(@Razlog, N''))) = N'' RAISERROR(N'Unesite polje „Razlog“.', 16, 1);
        IF @Nivo = 0 RAISERROR(N'Polje „Iznos“ mora biti veće od 0.', 16, 1);

        BEGIN TRAN;
        EXEC dbo.SRV_NalogOtvoren_Int @NalogID;
        IF @ID IS NULL
        BEGIN
            EXEC dbo.SRV_SledeciBroj 'Zahtev', @Broj OUTPUT;
            INSERT INTO dbo.SRV_SpoljniServisZahtev (Broj, NalogID, PaSifra, BrojPonude, Razlog, Iznos, Nivo, Podneo, Kreirao)
            VALUES (@Broj, @NalogID, @PaSifra, @BrojPonude, LTRIM(RTRIM(@Razlog)), @Iznos, @Nivo, @Korisnik, @Korisnik);
            SET @ID = SCOPE_IDENTITY();
            INSERT INTO dbo.SRV_SpoljniServisIstorija (ZahtevID, Korisnik, Radnja, Komentar)
            VALUES (@ID, @Korisnik, N'Podnet', N'Iznos ' + dbo.SRV_Broj(@Iznos) + N' RSD, nivo ' + CAST(@Nivo AS nvarchar(1)));
        END
        ELSE
        BEGIN
            SELECT @Status = Status, @Broj = Broj FROM dbo.SRV_SpoljniServisZahtev WITH (UPDLOCK, HOLDLOCK) WHERE ID = @ID;
            IF @Status <> N'Vraćeno na doradu' RAISERROR(N'Zahtev se menja samo kada je vraćen na doradu.', 16, 1);
            -- posle dorade nivo se preracunava
            UPDATE dbo.SRV_SpoljniServisZahtev
            SET PaSifra = @PaSifra, BrojPonude = @BrojPonude, Razlog = LTRIM(RTRIM(@Razlog)), Iznos = @Iznos, Nivo = @Nivo,
                Status = N'Čeka odobrenje', Podneo = @Korisnik, Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE ID = @ID;
            INSERT INTO dbo.SRV_SpoljniServisIstorija (ZahtevID, Korisnik, Radnja, Komentar)
            VALUES (@ID, @Korisnik, N'Ponovo podnet', N'Iznos ' + dbo.SRV_Broj(@Iznos) + N' RSD, nivo ' + CAST(@Nivo AS nvarchar(1)));
        END
        EXEC dbo.SRV_NalogOsveziStatus_Int @NalogID, @Korisnik;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
    SELECT @ID AS ID, @Broj AS Broj, @Nivo AS Nivo;
END
GO

-- Odobri, Odbij, Vrati (na doradu), Zatvori (faktura, garancija)
IF OBJECT_ID('dbo.SRV_ZahtevAkcija', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_ZahtevAkcija;
GO
CREATE PROCEDURE dbo.SRV_ZahtevAkcija
    @ID               int,
    @Akcija           nvarchar(20),
    @Komentar         nvarchar(1000) = NULL,
    @Faktura          nvarchar(50) = NULL,
    @GarancijaMeseci  int = NULL,
    @Korisnik         nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @Status nvarchar(30), @Podneo nvarchar(50), @Nivo tinyint, @NalogID int, @Novi nvarchar(30);
    BEGIN TRY
        IF @Akcija IN (N'Odbij', N'Vrati') AND LTRIM(RTRIM(ISNULL(@Komentar, N''))) = N''
            RAISERROR(N'Unesite polje „Komentar“.', 16, 1);
        BEGIN TRAN;
        SELECT @Status = Status, @Podneo = Podneo, @Nivo = Nivo, @NalogID = NalogID
        FROM dbo.SRV_SpoljniServisZahtev WITH (UPDLOCK, HOLDLOCK) WHERE ID = @ID;

        IF @Akcija IN (N'Odobri', N'Odbij', N'Vrati')
        BEGIN
            IF @Status <> N'Čeka odobrenje' RAISERROR(N'Zahtev ne čeka odobrenje (status: %s).', 16, 1, @Status);
            IF @Akcija = N'Odobri'
                EXEC dbo.SRV_ProveriOdobravaoca @Korisnik, @Podneo, @Nivo, 'SRV_SpoljniServisZahtev', @ID;
            ELSE IF dbo.SRV_MozeDaOdobri(@Korisnik, @Nivo) = 0
            BEGIN
                DECLARE @n int = @Nivo;
                RAISERROR(N'Zahtev nivoa %d odbija ili vraća samo korisnik koji ga može odobriti.', 16, 1, @n);
            END
            SET @Novi = CASE @Akcija WHEN N'Odobri' THEN N'Odobreno' WHEN N'Odbij' THEN N'Odbijeno' ELSE N'Vraćeno na doradu' END;
            UPDATE dbo.SRV_SpoljniServisZahtev
            SET Status = @Novi,
                Odobrio = CASE WHEN @Akcija = N'Odobri' THEN @Korisnik ELSE Odobrio END,
                DatumOdobrenja = CASE WHEN @Akcija = N'Odobri' THEN SYSDATETIME() ELSE DatumOdobrenja END,
                Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE ID = @ID;
        END
        ELSE IF @Akcija = N'Zatvori'
        BEGIN
            IF @Status <> N'Odobreno' RAISERROR(N'Zatvara se samo odobren zahtev.', 16, 1);
            IF LTRIM(RTRIM(ISNULL(@Faktura, N''))) = N'' RAISERROR(N'Unesite polje „Broj fakture“.', 16, 1);
            IF @GarancijaMeseci < 0 RAISERROR(N'Polje „Garancija (meseci)“ ne sme biti negativno.', 16, 1);
            SET @Novi = N'Zatvoreno';
            UPDATE dbo.SRV_SpoljniServisZahtev
            SET Status = @Novi, Faktura = LTRIM(RTRIM(@Faktura)), GarancijaMeseci = @GarancijaMeseci,
                Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE ID = @ID;
        END
        ELSE RAISERROR(N'Nepoznata radnja.', 16, 1);

        INSERT INTO dbo.SRV_SpoljniServisIstorija (ZahtevID, Korisnik, Radnja, Komentar)
        VALUES (@ID, @Korisnik, @Novi,
                NULLIF(LTRIM(RTRIM(ISNULL(@Komentar, N'')
                    + CASE WHEN @Akcija = N'Zatvori' THEN N' Faktura ' + ISNULL(@Faktura, N'') + ISNULL(N', garancija ' + CAST(@GarancijaMeseci AS nvarchar(10)) + N' meseci', N'') ELSE N'' END)), N''));
        EXEC dbo.SRV_NalogOsveziStatus_Int @NalogID, @Korisnik;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
END
GO
