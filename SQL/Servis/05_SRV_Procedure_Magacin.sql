/* =====================================================================
   ZAH-010 Servis - 05: procedure magacina delova
   Stanje = zbir SRV_DeoKretanje. Prosecna ponderisana cena (OP-MD-02):
     (stanje x stara + ulaz x nova) / (stanje + ulaz)
   Pravila: vidi zaglavlje 04_SRV_Procedure_Sifarnici.sql.
   ===================================================================== */
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

/* ---------------------------------------------------------------------
   SRV_Ulaz_Int: ulaz dela sa cenom (prijem, pocetno stanje, povrat) i
   preracun prosecne cene. Zakljucava red dela do kraja transakcije.
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_Ulaz_Int', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_Ulaz_Int;
GO
CREATE PROCEDURE dbo.SRV_Ulaz_Int
    @DeoID           int,
    @Kolicina        decimal(18,3),
    @Cena            decimal(18,4),
    @Tip             nvarchar(20),
    @Broj            nvarchar(30),
    @Datum           datetime2(0),
    @NarudzbenicaID  int = NULL,
    @PaSifra         int = NULL,
    @NalogID         int = NULL,
    @NalogDeoID      int = NULL,
    @Napomena        nvarchar(400) = NULL,
    @Korisnik        nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Stara decimal(18,4), @Stanje decimal(18,3);
    SELECT @Stara = ProsecnaCena FROM dbo.SRV_Deo WITH (UPDLOCK, HOLDLOCK) WHERE ID = @DeoID;
    SELECT @Stanje = ISNULL(SUM(Kolicina), 0) FROM dbo.SRV_DeoKretanje WHERE DeoID = @DeoID;

    INSERT INTO dbo.SRV_DeoKretanje (DeoID, Datum, Tip, DokumentBroj, Kolicina, Cena, NalogID, NalogDeoID, NarudzbenicaID, PaSifra, Napomena, Korisnik)
    VALUES (@DeoID, @Datum, @Tip, @Broj, @Kolicina, @Cena, @NalogID, @NalogDeoID, @NarudzbenicaID, @PaSifra, @Napomena, @Korisnik);

    UPDATE dbo.SRV_Deo
    SET ProsecnaCena = CASE WHEN @Stanje > 0 THEN ROUND((@Stanje * @Stara + @Kolicina * @Cena) / (@Stanje + @Kolicina), 4)
                            ELSE @Cena END
    WHERE ID = @DeoID;

    EXEC dbo.SRV_ProveriMinimum_Int @DeoID;
END
GO

-- datum dokumenta: danas = sada, raniji dan = pocetak tog dana; buducnost se ne prihvata
IF OBJECT_ID('dbo.SRV_DatumDokumenta', 'FN') IS NOT NULL DROP FUNCTION dbo.SRV_DatumDokumenta;
GO
CREATE FUNCTION dbo.SRV_DatumDokumenta (@Datum date)
RETURNS datetime2(0)
AS
BEGIN
    RETURN CASE WHEN @Datum IS NULL OR @Datum >= CAST(SYSDATETIME() AS date) THEN SYSDATETIME() ELSE CAST(@Datum AS datetime2(0)) END;
END
GO

/* ---------------------------------------------------------------------
   Zajednicka provera stavki ulaznog dokumenta
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_ProveriStavke_Int', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_ProveriStavke_Int;
GO
CREATE PROCEDURE dbo.SRV_ProveriStavke_Int
    @Stavke dbo.SRV_StavkaTip READONLY
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @s nvarchar(30);
    IF NOT EXISTS (SELECT 1 FROM @Stavke)
    BEGIN
        RAISERROR(N'Dodajte bar jednu stavku.', 16, 1);
        RETURN 1;
    END
    SELECT TOP 1 @s = d.Sifra FROM @Stavke t JOIN dbo.SRV_Deo d ON d.ID = t.DeoID WHERE t.Kolicina <= 0;
    IF @s IS NOT NULL BEGIN RAISERROR(N'Polje „Količina“ mora biti veće od 0 (deo %s).', 16, 1, @s); RETURN 1; END
    SELECT TOP 1 @s = d.Sifra FROM @Stavke t JOIN dbo.SRV_Deo d ON d.ID = t.DeoID WHERE t.Cena < 0;
    IF @s IS NOT NULL BEGIN RAISERROR(N'Polje „Cena“ ne sme biti negativno (deo %s).', 16, 1, @s); RETURN 1; END
    SELECT TOP 1 @s = d.Sifra FROM @Stavke t JOIN dbo.SRV_Deo d ON d.ID = t.DeoID GROUP BY d.Sifra HAVING COUNT(*) > 1;
    IF @s IS NOT NULL BEGIN RAISERROR(N'Deo %s je naveden više puta.', 16, 1, @s); RETURN 1; END
    SELECT TOP 1 @s = d.Sifra FROM @Stavke t JOIN dbo.SRV_Deo d ON d.ID = t.DeoID WHERE d.Aktivno = 0;
    IF @s IS NOT NULL BEGIN RAISERROR(N'Deo %s nije aktivan.', 16, 1, @s); RETURN 1; END
    RETURN 0;
END
GO

/* =====================================================================
   POCETNO STANJE (MD-R13: cena 0 potvrdjuje korisnik u formi)
   ===================================================================== */
IF OBJECT_ID('dbo.SRV_PocetnoStanje', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_PocetnoStanje;
GO
CREATE PROCEDURE dbo.SRV_PocetnoStanje
    @Stavke    dbo.SRV_StavkaTip READONLY,
    @Datum     date = NULL,
    @Napomena  nvarchar(400) = NULL,
    @Korisnik  nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @Broj nvarchar(30), @Vreme datetime2(0) = dbo.SRV_DatumDokumenta(@Datum),
            @DeoID int, @Kol decimal(18,3), @Cena decimal(18,4), @Nap nvarchar(400);
    BEGIN TRY
        IF @Datum > CAST(GETDATE() AS date) RAISERROR(N'Datum dokumenta ne sme biti u budućnosti.', 16, 1);
        EXEC dbo.SRV_ProveriStavke_Int @Stavke;

        BEGIN TRAN;
        EXEC dbo.SRV_SledeciBroj 'PocetnoStanje', @Broj OUTPUT;
        DECLARE c CURSOR LOCAL FAST_FORWARD FOR SELECT DeoID, Kolicina, ISNULL(Cena, 0), Napomena FROM @Stavke;
        OPEN c;
        FETCH NEXT FROM c INTO @DeoID, @Kol, @Cena, @Nap;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            EXEC dbo.SRV_Ulaz_Int @DeoID, @Kol, @Cena, N'Početno stanje', @Broj, @Vreme, NULL, NULL, NULL, NULL,
                                  @Napomena, @Korisnik;
            FETCH NEXT FROM c INTO @DeoID, @Kol, @Cena, @Nap;
        END
        CLOSE c; DEALLOCATE c;
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

/* =====================================================================
   PRIJEM (MD-R05, MD-R05a)
   ===================================================================== */
IF OBJECT_ID('dbo.SRV_Prijem', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_Prijem;
GO
CREATE PROCEDURE dbo.SRV_Prijem
    @NarudzbenicaID  int = NULL,
    @PaSifra         int = NULL,                 -- dobavljac kod prijema bez narudzbenice (nije obavezan)
    @Datum           date = NULL,
    @Razlog          nvarchar(400) = NULL,       -- obavezan bez narudzbenice
    @Komentar        nvarchar(400) = NULL,       -- obavezan kada se prima preko narucenog
    @Napomena        nvarchar(400) = NULL,
    @Stavke          dbo.SRV_StavkaTip READONLY,
    @Korisnik        nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @Broj nvarchar(30), @Vreme datetime2(0) = dbo.SRV_DatumDokumenta(@Datum), @Status nvarchar(30),
            @Tol decimal(9,4), @s nvarchar(30), @t nvarchar(40), @Tekst nvarchar(400),
            @DeoID int, @Kol decimal(18,3), @Cena decimal(18,4);
    SET @Tol = CAST(ISNULL(NULLIF(dbo.SRV_Pod(N'Servis.Delovi.TolerancijaPrijema'), N''), N'0') AS decimal(9,4));
    BEGIN TRY
        IF @Datum > CAST(GETDATE() AS date) RAISERROR(N'Datum prijema ne sme biti u budućnosti.', 16, 1);
        EXEC dbo.SRV_ProveriStavke_Int @Stavke;
        IF @NarudzbenicaID IS NULL AND LTRIM(RTRIM(ISNULL(@Razlog, N''))) = N''
            RAISERROR(N'Unesite polje „Razlog prijema bez narudžbenice“.', 16, 1);

        BEGIN TRAN;
        IF @NarudzbenicaID IS NOT NULL
        BEGIN
            SELECT @Status = Status, @PaSifra = PaSifra FROM dbo.SRV_Narudzbenica WITH (UPDLOCK, HOLDLOCK) WHERE ID = @NarudzbenicaID;
            IF @Status NOT IN (N'Poručeno', N'Delimično isporučeno')
                RAISERROR(N'Prijem je moguć samo po narudžbenici u statusu „Poručeno“ ili „Delimično isporučeno“.', 16, 1);

            SELECT TOP 1 @s = d.Sifra FROM @Stavke t JOIN dbo.SRV_Deo d ON d.ID = t.DeoID
            WHERE NOT EXISTS (SELECT 1 FROM dbo.SRV_NarudzbenicaStavka ns WHERE ns.NarudzbenicaID = @NarudzbenicaID AND ns.DeoID = t.DeoID);
            IF @s IS NOT NULL RAISERROR(N'Deo %s nije na izabranoj narudžbenici.', 16, 1, @s);

            -- MD-R05a: preko preostale kolicine samo uz komentar
            SELECT TOP 1 @s = d.Sifra, @t = dbo.SRV_Broj(CASE WHEN ns.Kolicina > ns.PrimljenoKolicina THEN ns.Kolicina - ns.PrimljenoKolicina ELSE 0 END)
            FROM @Stavke t
            JOIN dbo.SRV_NarudzbenicaStavka ns ON ns.NarudzbenicaID = @NarudzbenicaID AND ns.DeoID = t.DeoID
            JOIN dbo.SRV_Deo d ON d.ID = t.DeoID
            WHERE t.Kolicina > (ns.Kolicina - ns.PrimljenoKolicina) + ns.Kolicina * @Tol / 100;
            IF @s IS NOT NULL AND LTRIM(RTRIM(ISNULL(@Komentar, N''))) = N''
                RAISERROR(N'Količina je veća od preostale po narudžbenici (%s). Unesite komentar.', 16, 1, @t);
        END

        EXEC dbo.SRV_SledeciBroj 'Prijem', @Broj OUTPUT;
        SET @Tekst = NULLIF(LTRIM(RTRIM(ISNULL(@Razlog, N'') + CASE WHEN ISNULL(@Komentar, N'') <> N'' THEN N' ' + @Komentar ELSE N'' END
                                       + CASE WHEN ISNULL(@Napomena, N'') <> N'' THEN N' ' + @Napomena ELSE N'' END)), N'');

        DECLARE c CURSOR LOCAL FAST_FORWARD FOR
            SELECT t.DeoID, t.Kolicina, COALESCE(t.Cena, ns.Cena, d.ProsecnaCena)
            FROM @Stavke t
            JOIN dbo.SRV_Deo d ON d.ID = t.DeoID
            LEFT JOIN dbo.SRV_NarudzbenicaStavka ns ON ns.NarudzbenicaID = @NarudzbenicaID AND ns.DeoID = t.DeoID;
        OPEN c;
        FETCH NEXT FROM c INTO @DeoID, @Kol, @Cena;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            EXEC dbo.SRV_Ulaz_Int @DeoID, @Kol, @Cena, N'Prijem', @Broj, @Vreme, @NarudzbenicaID, @PaSifra, NULL, NULL, @Tekst, @Korisnik;
            FETCH NEXT FROM c INTO @DeoID, @Kol, @Cena;
        END
        CLOSE c; DEALLOCATE c;

        IF @NarudzbenicaID IS NOT NULL
        BEGIN
            UPDATE ns SET PrimljenoKolicina = ns.PrimljenoKolicina + t.Kolicina
            FROM dbo.SRV_NarudzbenicaStavka ns JOIN @Stavke t ON t.DeoID = ns.DeoID
            WHERE ns.NarudzbenicaID = @NarudzbenicaID;

            UPDATE dbo.SRV_Narudzbenica
            SET Status = CASE WHEN EXISTS (SELECT 1 FROM dbo.SRV_NarudzbenicaStavka WHERE NarudzbenicaID = @NarudzbenicaID AND PrimljenoKolicina < Kolicina)
                              THEN N'Delimično isporučeno' ELSE N'Isporučeno' END,
                Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE ID = @NarudzbenicaID;
        END
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

/* =====================================================================
   NARUDZBENICE (MD-R07)
   ===================================================================== */
IF OBJECT_ID('dbo.SRV_SacuvajNarudzbenicu', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_SacuvajNarudzbenicu;
GO
CREATE PROCEDURE dbo.SRV_SacuvajNarudzbenicu
    @ID              int = NULL,
    @PaSifra         int,
    @Datum           date = NULL,
    @OcekivaniDatum  date = NULL,
    @Napomena        nvarchar(400) = NULL,
    @Stavke          dbo.SRV_StavkaTip READONLY,
    @Korisnik        nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @Broj nvarchar(30), @Status nvarchar(30);
    BEGIN TRY
        IF @PaSifra IS NULL RAISERROR(N'Unesite polje „Dobavljač“.', 16, 1);
        IF EXISTS (SELECT 1 FROM @Stavke) EXEC dbo.SRV_ProveriStavke_Int @Stavke;

        BEGIN TRAN;
        IF @ID IS NULL
        BEGIN
            EXEC dbo.SRV_SledeciBroj 'Narudzbenica', @Broj OUTPUT;
            INSERT INTO dbo.SRV_Narudzbenica (Broj, Datum, PaSifra, OcekivaniDatum, Napomena, Kreirao)
            VALUES (@Broj, ISNULL(@Datum, CAST(GETDATE() AS date)), @PaSifra, @OcekivaniDatum, @Napomena, @Korisnik);
            SET @ID = SCOPE_IDENTITY();
        END
        ELSE
        BEGIN
            SELECT @Status = Status, @Broj = Broj FROM dbo.SRV_Narudzbenica WITH (UPDLOCK, HOLDLOCK) WHERE ID = @ID;
            IF @Status <> N'Nacrt'
                RAISERROR(N'Narudžbenica se menja samo u statusu „Nacrt“ (sada: %s).', 16, 1, @Status);
            UPDATE dbo.SRV_Narudzbenica
            SET PaSifra = @PaSifra, Datum = ISNULL(@Datum, Datum), OcekivaniDatum = @OcekivaniDatum, Napomena = @Napomena,
                Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE ID = @ID;
            DELETE FROM dbo.SRV_NarudzbenicaStavka WHERE NarudzbenicaID = @ID;
        END
        INSERT INTO dbo.SRV_NarudzbenicaStavka (NarudzbenicaID, DeoID, Kolicina, Cena)
        SELECT @ID, t.DeoID, t.Kolicina, COALESCE(t.Cena, d.ProsecnaCena)
        FROM @Stavke t JOIN dbo.SRV_Deo d ON d.ID = t.DeoID;
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

-- iz predloga nabavke: jedna narudzbenica (Nacrt) po dobavljacu
IF OBJECT_ID('dbo.SRV_KreirajNarudzbenice', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_KreirajNarudzbenice;
GO
CREATE PROCEDURE dbo.SRV_KreirajNarudzbenice
    @Stavke    dbo.SRV_StavkaTip READONLY,
    @Korisnik  nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @s nvarchar(30), @PaSifra int, @Broj nvarchar(30), @ID int;
    DECLARE @Nove TABLE (ID int, Broj nvarchar(30), PaSifra int);
    BEGIN TRY
        EXEC dbo.SRV_ProveriStavke_Int @Stavke;
        SELECT TOP 1 @s = d.Sifra FROM @Stavke t JOIN dbo.SRV_Deo d ON d.ID = t.DeoID WHERE d.DobavljacPaSifra IS NULL;
        IF @s IS NOT NULL RAISERROR(N'Deo %s nema dobavljača. Unesite dobavljača u kartici dela.', 16, 1, @s);

        BEGIN TRAN;
        DECLARE c CURSOR LOCAL FAST_FORWARD FOR
            SELECT DISTINCT d.DobavljacPaSifra FROM @Stavke t JOIN dbo.SRV_Deo d ON d.ID = t.DeoID;
        OPEN c;
        FETCH NEXT FROM c INTO @PaSifra;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            EXEC dbo.SRV_SledeciBroj 'Narudzbenica', @Broj OUTPUT;
            INSERT INTO dbo.SRV_Narudzbenica (Broj, Datum, PaSifra, OcekivaniDatum, Napomena, Kreirao)
            SELECT @Broj, CAST(GETDATE() AS date), @PaSifra,
                   DATEADD(day, MAX(ISNULL(d.RokIsporukeDana, 0)), CAST(GETDATE() AS date)), N'Iz predloga nabavke', @Korisnik
            FROM @Stavke t JOIN dbo.SRV_Deo d ON d.ID = t.DeoID WHERE d.DobavljacPaSifra = @PaSifra;
            SET @ID = SCOPE_IDENTITY();
            INSERT INTO dbo.SRV_NarudzbenicaStavka (NarudzbenicaID, DeoID, Kolicina, Cena)
            SELECT @ID, t.DeoID, t.Kolicina, COALESCE(t.Cena, d.ProsecnaCena)
            FROM @Stavke t JOIN dbo.SRV_Deo d ON d.ID = t.DeoID WHERE d.DobavljacPaSifra = @PaSifra;
            INSERT INTO @Nove VALUES (@ID, @Broj, @PaSifra);
            FETCH NEXT FROM c INTO @PaSifra;
        END
        CLOSE c; DEALLOCATE c;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
    SELECT ID, Broj, PaSifra FROM @Nove ORDER BY Broj;
END
GO

-- Posalji (Nacrt -> Ceka odobrenje), Odobri (-> Poruceno), Vrati (-> Nacrt), Otkazi
IF OBJECT_ID('dbo.SRV_NarudzbenicaAkcija', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_NarudzbenicaAkcija;
GO
CREATE PROCEDURE dbo.SRV_NarudzbenicaAkcija
    @ID        int,
    @Akcija    nvarchar(20),          -- Posalji, Odobri, Vrati, Otkazi
    @Komentar  nvarchar(400) = NULL,
    @Korisnik  nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @Status nvarchar(30), @Podneo nvarchar(50), @Nivo tinyint, @Iznos decimal(18,2);
    BEGIN TRY
        IF @Akcija IN (N'Vrati', N'Otkazi') AND LTRIM(RTRIM(ISNULL(@Komentar, N''))) = N''
            RAISERROR(N'Unesite polje „Komentar“.', 16, 1);

        BEGIN TRAN;
        SELECT @Status = Status, @Podneo = Podneo, @Nivo = Nivo FROM dbo.SRV_Narudzbenica WITH (UPDLOCK, HOLDLOCK) WHERE ID = @ID;
        SELECT @Iznos = ISNULL(SUM(Kolicina * Cena), 0) FROM dbo.SRV_NarudzbenicaStavka WHERE NarudzbenicaID = @ID;

        IF @Akcija = N'Posalji'
        BEGIN
            IF @Status <> N'Nacrt' RAISERROR(N'Na odobrenje se šalje samo narudžbenica u statusu „Nacrt“.', 16, 1);
            IF NOT EXISTS (SELECT 1 FROM dbo.SRV_NarudzbenicaStavka WHERE NarudzbenicaID = @ID)
                RAISERROR(N'Dodajte bar jednu stavku pre slanja na odobrenje.', 16, 1);
            SET @Nivo = dbo.SRV_NivoOdobrenja(@Iznos);
            IF @Nivo = 0 RAISERROR(N'Iznos narudžbenice je 0. Unesite cene stavki pre slanja na odobrenje.', 16, 1);
            UPDATE dbo.SRV_Narudzbenica
            SET Status = N'Čeka odobrenje', Nivo = @Nivo, Podneo = @Korisnik, DatumSlanja = SYSDATETIME(), Komentar = NULL,
                Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE ID = @ID;
        END
        ELSE IF @Akcija = N'Odobri'
        BEGIN
            IF @Status <> N'Čeka odobrenje' RAISERROR(N'Odobrava se samo narudžbenica koja čeka odobrenje.', 16, 1);
            EXEC dbo.SRV_ProveriOdobravaoca @Korisnik, @Podneo, @Nivo, 'SRV_Narudzbenica', @ID;
            UPDATE dbo.SRV_Narudzbenica
            SET Status = N'Poručeno', Odobrio = @Korisnik, DatumOdobrenja = SYSDATETIME(), Komentar = NULLIF(@Komentar, N''),
                Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE ID = @ID;
        END
        ELSE IF @Akcija = N'Vrati'
        BEGIN
            IF @Status <> N'Čeka odobrenje' RAISERROR(N'Vraća se samo narudžbenica koja čeka odobrenje.', 16, 1);
            IF RTRIM(@Podneo) <> RTRIM(@Korisnik) AND dbo.SRV_MozeDaOdobri(@Korisnik, @Nivo) = 0
                RAISERROR(N'Narudžbenicu vraća podnosilac ili korisnik koji je može odobriti.', 16, 1);
            UPDATE dbo.SRV_Narudzbenica SET Status = N'Nacrt', Komentar = @Komentar, Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE ID = @ID;
        END
        ELSE IF @Akcija = N'Otkazi'
        BEGIN
            IF @Status NOT IN (N'Nacrt', N'Čeka odobrenje', N'Poručeno')
                RAISERROR(N'Narudžbenica u statusu „%s“ se ne može otkazati.', 16, 1, @Status);
            IF EXISTS (SELECT 1 FROM dbo.SRV_NarudzbenicaStavka WHERE NarudzbenicaID = @ID AND PrimljenoKolicina > 0)
                RAISERROR(N'Po narudžbenici je već bilo prijema; ne može se otkazati.', 16, 1);
            UPDATE dbo.SRV_Narudzbenica SET Status = N'Otkazano', Komentar = @Komentar, Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE ID = @ID;
        END
        ELSE RAISERROR(N'Nepoznata radnja.', 16, 1);

        INSERT INTO dbo.SRV_Log (Tabela, ZapisID, Opis, Staro, Novo, Korisnik)
        SELECT 'SRV_Narudzbenica', @ID, N'Narudžbenica: ' + @Akcija, @Status, Status + ISNULL(N' | ' + @Komentar, N''), @Korisnik
        FROM dbo.SRV_Narudzbenica WHERE ID = @ID;
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
   POPIS (MD-R10)
   ===================================================================== */
IF OBJECT_ID('dbo.SRV_PopisNovi', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_PopisNovi;
GO
CREATE PROCEDURE dbo.SRV_PopisNovi
    @Datum     date = NULL,
    @GrupaID   int = NULL,               -- NULL = svi delovi
    @Napomena  nvarchar(400) = NULL,
    @Korisnik  nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @Broj nvarchar(30), @ID int;
    BEGIN TRY
        BEGIN TRAN;
        IF EXISTS (SELECT 1 FROM dbo.SRV_Popis WITH (UPDLOCK, HOLDLOCK)
                   WHERE Status IN (N'U toku', N'Čeka odobrenje')
                     AND (DeoGrupaID IS NULL OR @GrupaID IS NULL OR DeoGrupaID = @GrupaID))
            RAISERROR(N'Za iste delove već postoji popis koji nije zaključen.', 16, 1);
        IF NOT EXISTS (SELECT 1 FROM dbo.SRV_Deo WHERE Aktivno = 1 AND (@GrupaID IS NULL OR DeoGrupaID = @GrupaID))
            RAISERROR(N'U izabranoj grupi nema aktivnih delova.', 16, 1);

        EXEC dbo.SRV_SledeciBroj 'Popis', @Broj OUTPUT;
        INSERT INTO dbo.SRV_Popis (Broj, Datum, DeoGrupaID, Napomena, Kreirao)
        VALUES (@Broj, ISNULL(@Datum, CAST(GETDATE() AS date)), @GrupaID, @Napomena, @Korisnik);
        SET @ID = SCOPE_IDENTITY();

        INSERT INTO dbo.SRV_PopisStavka (PopisID, DeoID, KnjigovodstvenaKolicina, Cena)
        SELECT @ID, d.ID, s.Stanje, d.ProsecnaCena
        FROM dbo.SRV_Deo d JOIN dbo.SRV_vw_DeoStanje s ON s.DeoID = d.ID
        WHERE d.Aktivno = 1 AND (@GrupaID IS NULL OR d.DeoGrupaID = @GrupaID);
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

IF OBJECT_ID('dbo.SRV_PopisUnesi', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_PopisUnesi;
GO
CREATE PROCEDURE dbo.SRV_PopisUnesi
    @StavkaID    int,
    @Prebrojano  decimal(18,3) = NULL,     -- NULL = brise unos
    @Napomena    nvarchar(400) = NULL,
    @Korisnik    nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        IF @Prebrojano IS NOT NULL AND (@Prebrojano < 0 OR @Prebrojano > 999999999)
            RAISERROR(N'Polje „Prebrojano“ mora biti između 0 i 999.999.999.', 16, 1);
        BEGIN TRAN;
        IF NOT EXISTS (SELECT 1 FROM dbo.SRV_PopisStavka s JOIN dbo.SRV_Popis p WITH (UPDLOCK, HOLDLOCK) ON p.ID = s.PopisID
                       WHERE s.ID = @StavkaID AND p.Status = N'U toku')
            RAISERROR(N'Popis nije u toku; unos prebrojane količine nije moguć.', 16, 1);
        UPDATE dbo.SRV_PopisStavka
        SET PrebrojanaKolicina = @Prebrojano, Napomena = @Napomena, Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
        WHERE ID = @StavkaID;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
END
GO

-- Posalji (U toku -> Ceka odobrenje), Odobri (-> Odobren, knjizi razlike), Vrati (-> U toku), Storniraj
IF OBJECT_ID('dbo.SRV_PopisAkcija', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_PopisAkcija;
GO
CREATE PROCEDURE dbo.SRV_PopisAkcija
    @ID        int,
    @Akcija    nvarchar(20),
    @Korisnik  nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @Status nvarchar(20), @Podneo nvarchar(50), @Broj nvarchar(30), @s nvarchar(30), @DeoID int;
    BEGIN TRY
        BEGIN TRAN;
        SELECT @Status = Status, @Podneo = Podneo, @Broj = Broj FROM dbo.SRV_Popis WITH (UPDLOCK, HOLDLOCK) WHERE ID = @ID;

        IF @Akcija = N'Posalji'
        BEGIN
            IF @Status <> N'U toku' RAISERROR(N'Na odobrenje se šalje samo popis u toku.', 16, 1);
            IF NOT EXISTS (SELECT 1 FROM dbo.SRV_PopisStavka WHERE PopisID = @ID AND PrebrojanaKolicina IS NOT NULL)
                RAISERROR(N'Unesite bar jednu prebrojanu količinu pre slanja na odobrenje.', 16, 1);
            UPDATE dbo.SRV_Popis SET Status = N'Čeka odobrenje', Podneo = @Korisnik, Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE ID = @ID;
        END
        ELSE IF @Akcija = N'Odobri'
        BEGIN
            IF @Status <> N'Čeka odobrenje' RAISERROR(N'Odobrava se samo popis koji čeka odobrenje.', 16, 1);
            EXEC dbo.SRV_ProveriOdobravaoca @Korisnik, @Podneo, 1, 'SRV_Popis', @ID;

            -- stanje posle razlike ne sme biti negativno (ako je u medjuvremenu bilo izdavanja)
            SELECT TOP 1 @s = d.Sifra
            FROM dbo.SRV_PopisStavka ps
            JOIN dbo.SRV_Deo d WITH (UPDLOCK, HOLDLOCK) ON d.ID = ps.DeoID
            JOIN dbo.SRV_vw_DeoStanje st ON st.DeoID = ps.DeoID
            WHERE ps.PopisID = @ID AND ps.PrebrojanaKolicina IS NOT NULL
              AND st.Stanje + (ps.PrebrojanaKolicina - ps.KnjigovodstvenaKolicina) < 0;
            IF @s IS NOT NULL RAISERROR(N'Stanje dela %s bi posle popisa bilo negativno (bilo je izdavanja posle početka popisa).', 16, 1, @s);

            -- razlika se vrednuje po prosecnoj ceni; neprebrojane stavke ne menjaju zalihu
            INSERT INTO dbo.SRV_DeoKretanje (DeoID, Datum, Tip, DokumentBroj, Kolicina, Cena, PopisID, Napomena, Korisnik)
            SELECT ps.DeoID, SYSDATETIME(), N'Popis', @Broj, ps.PrebrojanaKolicina - ps.KnjigovodstvenaKolicina, d.ProsecnaCena, @ID,
                   ps.Napomena, @Korisnik
            FROM dbo.SRV_PopisStavka ps JOIN dbo.SRV_Deo d ON d.ID = ps.DeoID
            WHERE ps.PopisID = @ID AND ps.PrebrojanaKolicina IS NOT NULL AND ps.PrebrojanaKolicina <> ps.KnjigovodstvenaKolicina;

            UPDATE dbo.SRV_Popis SET Status = N'Odobren', Odobrio = @Korisnik, DatumOdobrenja = SYSDATETIME(),
                   Izmenio = @Korisnik, DatumIzmene = SYSDATETIME()
            WHERE ID = @ID;

            DECLARE c CURSOR LOCAL FAST_FORWARD FOR
                SELECT DeoID FROM dbo.SRV_PopisStavka WHERE PopisID = @ID AND PrebrojanaKolicina IS NOT NULL AND PrebrojanaKolicina <> KnjigovodstvenaKolicina;
            OPEN c;
            FETCH NEXT FROM c INTO @DeoID;
            WHILE @@FETCH_STATUS = 0
            BEGIN
                EXEC dbo.SRV_ProveriMinimum_Int @DeoID;
                FETCH NEXT FROM c INTO @DeoID;
            END
            CLOSE c; DEALLOCATE c;
        END
        ELSE IF @Akcija = N'Vrati'
        BEGIN
            IF @Status <> N'Čeka odobrenje' RAISERROR(N'Na brojanje se vraća samo popis koji čeka odobrenje.', 16, 1);
            IF RTRIM(@Podneo) <> RTRIM(@Korisnik) AND dbo.SRV_MozeDaOdobri(@Korisnik, 1) = 0
                RAISERROR(N'Popis vraća podnosilac ili korisnik koji ga može odobriti.', 16, 1);
            UPDATE dbo.SRV_Popis SET Status = N'U toku', Izmenio = @Korisnik, DatumIzmene = SYSDATETIME() WHERE ID = @ID;
        END
        ELSE IF @Akcija = N'Storniraj'
        BEGIN
            IF @Status NOT IN (N'U toku', N'Čeka odobrenje') RAISERROR(N'Popis u statusu „%s“ se ne može stornirati.', 16, 1, @Status);
            UPDATE dbo.SRV_Popis SET Status = N'Storniran', Izmenio = @Korisnik, DatumIzmene = SYSDATETIME() WHERE ID = @ID;
        END
        ELSE RAISERROR(N'Nepoznata radnja.', 16, 1);

        INSERT INTO dbo.SRV_Log (Tabela, ZapisID, Opis, Staro, Novo, Korisnik)
        SELECT 'SRV_Popis', @ID, N'Popis ' + @Broj + N': ' + @Akcija, @Status, Status, @Korisnik FROM dbo.SRV_Popis WHERE ID = @ID;
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
   KOREKCIJE (MD-R11)
   ===================================================================== */
IF OBJECT_ID('dbo.SRV_KorekcijaNova', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_KorekcijaNova;
GO
CREATE PROCEDURE dbo.SRV_KorekcijaNova
    @DeoID     int,
    @Kolicina  decimal(18,3),
    @Razlog    nvarchar(400),
    @Korisnik  nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @Broj nvarchar(30), @ID int, @Stanje decimal(18,3), @Ceka decimal(18,3);
    BEGIN TRY
        IF @DeoID IS NULL RAISERROR(N'Unesite polje „Deo“.', 16, 1);
        IF ISNULL(@Kolicina, 0) = 0 RAISERROR(N'Polje „Korekcija (+ višak, − manjak)“ ne sme biti 0.', 16, 1);
        IF LTRIM(RTRIM(ISNULL(@Razlog, N''))) = N'' RAISERROR(N'Unesite polje „Razlog“.', 16, 1);

        BEGIN TRAN;
        SELECT @Stanje = s.Stanje FROM dbo.SRV_Deo d WITH (UPDLOCK, HOLDLOCK) JOIN dbo.SRV_vw_DeoStanje s ON s.DeoID = d.ID WHERE d.ID = @DeoID;
        -- uracunavaju se i neodobrene korekcije istog dela koje smanjuju stanje
        SELECT @Ceka = ISNULL(SUM(Kolicina), 0) FROM dbo.SRV_Korekcija WHERE DeoID = @DeoID AND Status = N'Čeka odobrenje' AND Kolicina < 0;
        IF @Stanje + @Ceka + @Kolicina < 0 RAISERROR(N'Posle korekcije stanje dela bi bilo negativno.', 16, 1);

        EXEC dbo.SRV_SledeciBroj 'Korekcija', @Broj OUTPUT;
        INSERT INTO dbo.SRV_Korekcija (Broj, DeoID, Kolicina, Razlog, Podneo, Kreirao)
        VALUES (@Broj, @DeoID, @Kolicina, LTRIM(RTRIM(@Razlog)), @Korisnik, @Korisnik);
        SET @ID = SCOPE_IDENTITY();
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

IF OBJECT_ID('dbo.SRV_KorekcijaOdluka', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_KorekcijaOdluka;
GO
CREATE PROCEDURE dbo.SRV_KorekcijaOdluka
    @ID        int,
    @Odobri    bit,
    @Korisnik  nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @Status nvarchar(20), @Podneo nvarchar(50), @DeoID int, @Kol decimal(18,3), @Broj nvarchar(30), @Razlog nvarchar(400),
            @Stanje decimal(18,3), @Cena decimal(18,4);
    BEGIN TRY
        BEGIN TRAN;
        SELECT @Status = Status, @Podneo = Podneo, @DeoID = DeoID, @Kol = Kolicina, @Broj = Broj, @Razlog = Razlog
        FROM dbo.SRV_Korekcija WITH (UPDLOCK, HOLDLOCK) WHERE ID = @ID;
        IF @Status <> N'Čeka odobrenje' RAISERROR(N'Korekcija je već rešena (%s).', 16, 1, @Status);

        IF @Odobri = 1
        BEGIN
            EXEC dbo.SRV_ProveriOdobravaoca @Korisnik, @Podneo, 1, 'SRV_Korekcija', @ID;
            SELECT @Stanje = s.Stanje, @Cena = d.ProsecnaCena
            FROM dbo.SRV_Deo d WITH (UPDLOCK, HOLDLOCK) JOIN dbo.SRV_vw_DeoStanje s ON s.DeoID = d.ID WHERE d.ID = @DeoID;
            IF @Stanje + @Kol < 0 RAISERROR(N'Posle korekcije stanje dela bi bilo negativno.', 16, 1);

            INSERT INTO dbo.SRV_DeoKretanje (DeoID, Datum, Tip, DokumentBroj, Kolicina, Cena, KorekcijaID, Napomena, Korisnik)
            VALUES (@DeoID, SYSDATETIME(), N'Korekcija', @Broj, @Kol, @Cena, @ID, @Razlog, @Korisnik);
            UPDATE dbo.SRV_Korekcija SET Status = N'Odobrena', Odlucio = @Korisnik, DatumOdluke = SYSDATETIME() WHERE ID = @ID;
            EXEC dbo.SRV_ProveriMinimum_Int @DeoID;
        END
        ELSE
        BEGIN
            IF RTRIM(@Podneo) <> RTRIM(@Korisnik) AND dbo.SRV_MozeDaOdobri(@Korisnik, 1) = 0
                RAISERROR(N'Korekciju odbija podnosilac ili korisnik koji je može odobriti.', 16, 1);
            UPDATE dbo.SRV_Korekcija SET Status = N'Odbijena', Odlucio = @Korisnik, DatumOdluke = SYSDATETIME() WHERE ID = @ID;
        END

        INSERT INTO dbo.SRV_Log (Tabela, ZapisID, Opis, Novo, Korisnik)
        VALUES ('SRV_Korekcija', @ID, N'Korekcija ' + @Broj + CASE WHEN @Odobri = 1 THEN N' odobrena' ELSE N' odbijena' END,
                dbo.SRV_Broj(@Kol) + N' | ' + @Razlog, @Korisnik);
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
   OBAVESTENJA
   ===================================================================== */
IF OBJECT_ID('dbo.SRV_ObavestenjaProcitana', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_ObavestenjaProcitana;
GO
CREATE PROCEDURE dbo.SRV_ObavestenjaProcitana
    @IDs       dbo.SRV_IdTip READONLY,
    @Korisnik  nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @DeoID int;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM @IDs) RAISERROR(N'Izaberite obaveštenje.', 16, 1);
        BEGIN TRAN;
        UPDATE o SET Procitano = 1, ProcitaoKorisnik = @Korisnik, VremeCitanja = SYSDATETIME()
        FROM dbo.SRV_DeoObavestenje o JOIN @IDs i ON i.ID = o.ID
        WHERE o.Procitano = 0;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
END
GO

/* ---------------------------------------------------------------------
   SRV_ProveriRokove: obavestenja za dokumenta i planove (SR-17).
   Pravi se jedno obavestenje po prelasku praga (ulazak u zonu upozorenja,
   pa istek / dospece), ne pri svakom pokretanju. Sme da se zove cesto.
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_ProveriRokove', 'P') IS NOT NULL DROP PROCEDURE dbo.SRV_ProveriRokove;
GO
CREATE PROCEDURE dbo.SRV_ProveriRokove
    @Korisnik nvarchar(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRAN;

        -- dokumenta: prag 1 = upozorenje/istice, prag 2 = isteklo
        DECLARE @Dok TABLE (ID int, SredstvoID int, Prag nvarchar(20), Tekst nvarchar(400));
        INSERT INTO @Dok
        SELECT d.ID, d.SredstvoID,
               CASE WHEN d.Status = N'Isteklo' THEN N'Isteklo' ELSE N'Upozorenje' END,
               N'Dokument „' + d.Dokument + N'“ za ' + d.Oznaka + N' ' + d.NazivSredstva
               + CASE WHEN d.Status = N'Isteklo' THEN N' je istekao ' ELSE N' ističe ' END
               + CONVERT(nvarchar(10), d.DatumIsteka, 104) + N'.'
               + CASE WHEN d.DanaDoIsteka >= 0 THEN N' (za ' + CAST(d.DanaDoIsteka AS nvarchar(10)) + N' dana)' ELSE N'' END
        FROM dbo.SRV_vw_DokumentStatus d
        WHERE d.ID IS NOT NULL AND d.Status IN (N'Upozorenje', N'Ističe', N'Isteklo')
          AND ISNULL(d.ObavestenoPrag, N'') <> CASE WHEN d.Status = N'Isteklo' THEN N'Isteklo' ELSE N'Upozorenje' END
          AND ISNULL(d.ObavestenoPrag, N'') <> N'Isteklo';

        INSERT INTO dbo.SRV_DeoObavestenje (Vrsta, SredstvoID, SredstvoDokumentID, Tekst)
        SELECT N'Dokument', SredstvoID, ID, Tekst FROM @Dok;
        UPDATE sd SET ObavestenoPrag = x.Prag FROM dbo.SRV_SredstvoDokument sd JOIN @Dok x ON x.ID = sd.ID;

        -- dokument ponovo vazeci (produzen): prag se brise
        UPDATE sd SET ObavestenoPrag = NULL
        FROM dbo.SRV_SredstvoDokument sd JOIN dbo.SRV_vw_DokumentStatus v ON v.ID = sd.ID
        WHERE v.Status = N'Važeći' AND sd.ObavestenoPrag IS NOT NULL;

        -- planovi: prag 1 = upozorenje, prag 2 = dospelo/kasni (samo kada nema otvorenog naloga za plan)
        DECLARE @Pl TABLE (PlanID int, SredstvoID int, Prag nvarchar(20), Tekst nvarchar(400));
        INSERT INTO @Pl
        SELECT p.PlanID, p.SredstvoID,
               CASE WHEN p.Rang >= 2 THEN N'Dospelo' ELSE N'Upozorenje' END,
               N'Servis „' + p.PlanNaziv + N'“ za ' + p.Oznaka + N' ' + p.NazivSredstva + N': ' + p.StatusPlana
               + CASE WHEN p.SledeciKm IS NOT NULL THEN N', sledeći na ' + dbo.SRV_Broj(p.SledeciKm) + N' km' ELSE N'' END
               + CASE WHEN p.SledeciMH IS NOT NULL THEN N', sledeći na ' + dbo.SRV_Broj(p.SledeciMH) + N' mh' ELSE N'' END
               + CASE WHEN p.SledeciDatum IS NOT NULL THEN N', rok ' + CONVERT(nvarchar(10), p.SledeciDatum, 104) + N'.' ELSE N'' END
        FROM dbo.SRV_vw_PlanStatus p
        LEFT JOIN dbo.SRV_PlanSredstvoPrag g ON g.PlanID = p.PlanID AND g.SredstvoID = p.SredstvoID
        WHERE p.Rang >= 1 AND p.OtvorenNalog IS NULL
          AND ISNULL(g.ObavestenoPrag, N'') <> CASE WHEN p.Rang >= 2 THEN N'Dospelo' ELSE N'Upozorenje' END
          AND ISNULL(g.ObavestenoPrag, N'') <> N'Dospelo';

        INSERT INTO dbo.SRV_DeoObavestenje (Vrsta, SredstvoID, PlanID, Tekst)
        SELECT N'Plan', SredstvoID, PlanID, Tekst FROM @Pl;
        UPDATE g SET ObavestenoPrag = x.Prag FROM dbo.SRV_PlanSredstvoPrag g JOIN @Pl x ON x.PlanID = g.PlanID AND x.SredstvoID = g.SredstvoID;
        INSERT INTO dbo.SRV_PlanSredstvoPrag (PlanID, SredstvoID, ObavestenoPrag)
        SELECT x.PlanID, x.SredstvoID, x.Prag FROM @Pl x
        WHERE NOT EXISTS (SELECT 1 FROM dbo.SRV_PlanSredstvoPrag g WHERE g.PlanID = x.PlanID AND g.SredstvoID = x.SredstvoID);

        -- plan ponovo u redu (posle servisa): prag se brise
        UPDATE g SET ObavestenoPrag = NULL
        FROM dbo.SRV_PlanSredstvoPrag g JOIN dbo.SRV_vw_PlanStatus p ON p.PlanID = g.PlanID AND p.SredstvoID = g.SredstvoID
        WHERE p.Rang = 0 AND g.ObavestenoPrag IS NOT NULL;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        EXEC dbo.SRV_PonoviGresku;
        RETURN 1;
    END CATCH
END
GO
