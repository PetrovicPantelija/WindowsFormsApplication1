/* =====================================================================
   ZAH-010 Servis - 08: primer podataka za razvoj i test (poglavlje 10)
   SAMO ZA Jela_Test. Izmisljen primer; zameniti stvarnim podacima Jele.
   Upisuje se samo ono cega nema (sme vise puta). Ide preko procedura,
   pa vaze ista pravila kao u aplikaciji.
   Dobavljaci: prva tri partnera iz postojece tabele Partnerji (samo citanje).
   ===================================================================== */
SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @K nvarchar(50) = N'test-skripta';
DECLARE @b dbo.SRV_TekstTip, @pv dbo.SRV_PoljeVrednostTip, @s dbo.SRV_StavkaTip;
DECLARE @r TABLE (ID int);
DECLARE @Kamion int, @Viljuskar int, @Masina int, @Kompresor int, @id int;

/* ---- vrste sredstava ---- */
IF NOT EXISTS (SELECT 1 FROM dbo.SRV_VrstaSredstva WHERE Naziv = N'Kamion')
BEGIN DELETE FROM @b; INSERT INTO @b VALUES (N'KM'); INSERT INTO @r EXEC dbo.SRV_SacuvajVrstu NULL, N'Kamion', NULL, 1, @b, @K; END
IF NOT EXISTS (SELECT 1 FROM dbo.SRV_VrstaSredstva WHERE Naziv = N'Viljuškar')
BEGIN DELETE FROM @b; INSERT INTO @b VALUES (N'MH'); INSERT INTO @r EXEC dbo.SRV_SacuvajVrstu NULL, N'Viljuškar', NULL, 1, @b, @K; END
IF NOT EXISTS (SELECT 1 FROM dbo.SRV_VrstaSredstva WHERE Naziv = N'Mašina / radni centar')
BEGIN DELETE FROM @b; INSERT INTO @b VALUES (N'MH'); INSERT INTO @r EXEC dbo.SRV_SacuvajVrstu NULL, N'Mašina / radni centar', NULL, 1, @b, @K; END
IF NOT EXISTS (SELECT 1 FROM dbo.SRV_VrstaSredstva WHERE Naziv = N'Kompresor')
BEGIN DELETE FROM @b; INSERT INTO @b VALUES (N'MH'); INSERT INTO @r EXEC dbo.SRV_SacuvajVrstu NULL, N'Kompresor', NULL, 1, @b, @K; END

SELECT @Kamion = ID FROM dbo.SRV_VrstaSredstva WHERE Naziv = N'Kamion';
SELECT @Viljuskar = ID FROM dbo.SRV_VrstaSredstva WHERE Naziv = N'Viljuškar';
SELECT @Masina = ID FROM dbo.SRV_VrstaSredstva WHERE Naziv = N'Mašina / radni centar';

/* ---- dodatna polja i dokumenta ---- */
IF NOT EXISTS (SELECT 1 FROM dbo.SRV_VrstaSredstvaPolje WHERE VrstaSredstvaID = @Kamion AND Naziv = N'Registarska oznaka')
    INSERT INTO @r EXEC dbo.SRV_SacuvajPoljeVrste NULL, @Kamion, N'Registarska oznaka', N'Tekst', NULL, NULL, 1, 1, 1, @K;
IF NOT EXISTS (SELECT 1 FROM dbo.SRV_VrstaSredstvaPolje WHERE VrstaSredstvaID = @Kamion AND Naziv = N'Nosivost')
    INSERT INTO @r EXEC dbo.SRV_SacuvajPoljeVrste NULL, @Kamion, N'Nosivost', N'Broj', N't', NULL, 0, 2, 1, @K;
IF NOT EXISTS (SELECT 1 FROM dbo.SRV_VrstaSredstvaPolje WHERE VrstaSredstvaID = @Viljuskar AND Naziv = N'Nosivost')
    INSERT INTO @r EXEC dbo.SRV_SacuvajPoljeVrste NULL, @Viljuskar, N'Nosivost', N'Broj', N'kg', NULL, 1, 1, 1, @K;
IF NOT EXISTS (SELECT 1 FROM dbo.SRV_VrstaSredstvaPolje WHERE VrstaSredstvaID = @Viljuskar AND Naziv = N'Pogon')
    INSERT INTO @r EXEC dbo.SRV_SacuvajPoljeVrste NULL, @Viljuskar, N'Pogon', N'Lista', NULL, N'Dizel;Elektro;Gas', 0, 2, 1, @K;

IF NOT EXISTS (SELECT 1 FROM dbo.SRV_VrstaSredstvaDokument WHERE VrstaSredstvaID = @Kamion AND Naziv = N'Registracija')
    INSERT INTO @r EXEC dbo.SRV_SacuvajDokumentVrste NULL, @Kamion, N'Registracija', 1, 30, 1, @K;
IF NOT EXISTS (SELECT 1 FROM dbo.SRV_VrstaSredstvaDokument WHERE VrstaSredstvaID = @Kamion AND Naziv = N'Tehnički pregled')
    INSERT INTO @r EXEC dbo.SRV_SacuvajDokumentVrste NULL, @Kamion, N'Tehnički pregled', 1, 30, 1, @K;
IF NOT EXISTS (SELECT 1 FROM dbo.SRV_VrstaSredstvaDokument WHERE VrstaSredstvaID = @Viljuskar AND Naziv = N'Atest')
    INSERT INTO @r EXEC dbo.SRV_SacuvajDokumentVrste NULL, @Viljuskar, N'Atest', 1, 30, 1, @K;

/* ---- sredstva ---- */
IF NOT EXISTS (SELECT 1 FROM dbo.SRV_Sredstvo WHERE Oznaka = N'KAM-T1')
BEGIN
    DELETE FROM @pv;
    INSERT INTO @pv SELECT ID, N'JA-000-TT' FROM dbo.SRV_VrstaSredstvaPolje WHERE VrstaSredstvaID = @Kamion AND Naziv = N'Registarska oznaka';
    INSERT INTO @r EXEC dbo.SRV_SacuvajSredstvo NULL, N'KAM-T1', N'Kamion test', @Kamion, NULL, NULL, 2018, NULL, N'Jagodina', N'U radu', NULL, NULL, @pv, 250000, @K;
END
IF NOT EXISTS (SELECT 1 FROM dbo.SRV_Sredstvo WHERE Oznaka = N'VLJ-T1')
BEGIN
    DELETE FROM @pv;
    INSERT INTO @pv SELECT ID, N'2500' FROM dbo.SRV_VrstaSredstvaPolje WHERE VrstaSredstvaID = @Viljuskar AND Naziv = N'Nosivost';
    INSERT INTO @pv SELECT ID, N'Dizel' FROM dbo.SRV_VrstaSredstvaPolje WHERE VrstaSredstvaID = @Viljuskar AND Naziv = N'Pogon';
    INSERT INTO @r EXEC dbo.SRV_SacuvajSredstvo NULL, N'VLJ-T1', N'Viljuškar test', @Viljuskar, NULL, NULL, 2020, NULL, N'Magacin', N'U radu', NULL, NULL, @pv, 5000, @K;
END
IF NOT EXISTS (SELECT 1 FROM dbo.SRV_Sredstvo WHERE Oznaka = N'MAS-T1')
BEGIN
    DELETE FROM @pv;
    INSERT INTO @r EXEC dbo.SRV_SacuvajSredstvo NULL, N'MAS-T1', N'Mašina test', @Masina, NULL, NULL, 2015, NULL, N'Proizvodnja', N'U radu', NULL, NULL, @pv, 3000, @K;
END

/* ---- grupe delova ---- */
DECLARE @g TABLE (Naziv nvarchar(100));
INSERT INTO @g VALUES (N'Elektro'), (N'Filteri'), (N'Gume'), (N'Hidraulika'), (N'Kočnice'), (N'Maziva i gorivo'), (N'Ostalo'), (N'Zaptivke');
DECLARE @gn nvarchar(100);
DECLARE cg CURSOR LOCAL FAST_FORWARD FOR SELECT Naziv FROM @g WHERE Naziv NOT IN (SELECT Naziv FROM dbo.SRV_DeoGrupa);
OPEN cg; FETCH NEXT FROM cg INTO @gn;
WHILE @@FETCH_STATUS = 0
BEGIN
    INSERT INTO @r EXEC dbo.SRV_SacuvajGrupuDelova NULL, @gn, 1, @K;
    FETCH NEXT FROM cg INTO @gn;
END
CLOSE cg; DEALLOCATE cg;

/* ---- delovi (dobavljaci: prva tri partnera) ---- */
DECLARE @P1 int, @P2 int, @P3 int;
SELECT @P1 = MIN(PaSifra) FROM dbo.Partnerji;
SELECT @P2 = MIN(PaSifra) FROM dbo.Partnerji WHERE PaSifra > @P1;
SELECT @P3 = MIN(PaSifra) FROM dbo.Partnerji WHERE PaSifra > @P2;

DECLARE @Filteri int = (SELECT ID FROM dbo.SRV_DeoGrupa WHERE Naziv = N'Filteri'),
        @Kocnice int = (SELECT ID FROM dbo.SRV_DeoGrupa WHERE Naziv = N'Kočnice'),
        @Maziva  int = (SELECT ID FROM dbo.SRV_DeoGrupa WHERE Naziv = N'Maziva i gorivo');

IF NOT EXISTS (SELECT 1 FROM dbo.SRV_Deo WHERE Sifra = N'D-1001')
    INSERT INTO @r EXEC dbo.SRV_SacuvajDeo NULL, N'D-1001', N'Filter ulja', @Filteri, N'kom', 4, 20, N'A-01', 0, @P1, 7, NULL, 1, @K;
IF NOT EXISTS (SELECT 1 FROM dbo.SRV_Deo WHERE Sifra = N'D-1002')
    INSERT INTO @r EXEC dbo.SRV_SacuvajDeo NULL, N'D-1002', N'Kočioni uložak', @Kocnice, N'kom', 8, 40, N'A-02', 1, @P2, 10, NULL, 1, @K;
IF NOT EXISTS (SELECT 1 FROM dbo.SRV_Deo WHERE Sifra = N'D-1003')
    INSERT INTO @r EXEC dbo.SRV_SacuvajDeo NULL, N'D-1003', N'Ulje 15W40', @Maziva, N'l', 100, 400, N'B-01', 0, @P3, 5, NULL, 1, @K;
IF NOT EXISTS (SELECT 1 FROM dbo.SRV_Deo WHERE Sifra = N'D-1004')
    INSERT INTO @r EXEC dbo.SRV_SacuvajDeo NULL, N'D-1004', N'Zaptivka cilindra', @Kocnice, N'kom', 2, 10, N'A-03', 0, @P2, 10, NULL, 1, @K;

/* ---- primenljivost ---- */
INSERT INTO dbo.SRV_DeoPrimenljivost (DeoID, VrstaSredstvaID, Kreirao)
SELECT d.ID, v.ID, @K FROM dbo.SRV_Deo d CROSS JOIN dbo.SRV_VrstaSredstva v
WHERE ((d.Sifra IN (N'D-1001', N'D-1003') AND v.ID IN (@Kamion, @Viljuskar)) OR (d.Sifra IN (N'D-1002', N'D-1004') AND v.ID = @Kamion))
  AND NOT EXISTS (SELECT 1 FROM dbo.SRV_DeoPrimenljivost p WHERE p.DeoID = d.ID AND p.VrstaSredstvaID = v.ID AND p.SredstvoID IS NULL);

/* ---- planovi i normativ ---- */
IF NOT EXISTS (SELECT 1 FROM dbo.SRV_PlanOdrzavanja WHERE VrstaSredstvaID = @Kamion AND Naziv = N'Servis')
    INSERT INTO @r EXEC dbo.SRV_SacuvajPlan NULL, @Kamion, N'Servis', 15000, 0, 0, 0, 1000, 0, 0, NULL, 1, @K;
IF NOT EXISTS (SELECT 1 FROM dbo.SRV_PlanOdrzavanja WHERE VrstaSredstvaID = @Viljuskar AND Naziv = N'Mali servis')
    INSERT INTO @r EXEC dbo.SRV_SacuvajPlan NULL, @Viljuskar, N'Mali servis', 0, 500, 180, 0, 0, 50, 14, NULL, 1, @K;

INSERT INTO dbo.SRV_PlanNormativ (PlanID, DeoID, Kolicina, Kreirao)
SELECT p.ID, d.ID, x.Kol, @K
FROM (VALUES (N'Kamion', N'Servis', N'D-1001', 3), (N'Kamion', N'Servis', N'D-1003', 120),
             (N'Viljuškar', N'Mali servis', N'D-1001', 2), (N'Viljuškar', N'Mali servis', N'D-1003', 40)) AS x(Vrsta, PlanNaziv, Sifra, Kol)
JOIN dbo.SRV_VrstaSredstva v ON v.Naziv = x.Vrsta
JOIN dbo.SRV_PlanOdrzavanja p ON p.VrstaSredstvaID = v.ID AND p.Naziv = x.PlanNaziv
JOIN dbo.SRV_Deo d ON d.Sifra = x.Sifra
WHERE NOT EXISTS (SELECT 1 FROM dbo.SRV_PlanNormativ n WHERE n.PlanID = p.ID AND n.DeoID = d.ID);

/* ---- pocetno stanje (samo jednom) ---- */
IF NOT EXISTS (SELECT 1 FROM dbo.SRV_DeoKretanje k JOIN dbo.SRV_Deo d ON d.ID = k.DeoID WHERE d.Sifra LIKE N'D-100[1-4]')
BEGIN
    INSERT INTO @s (DeoID, Kolicina, Cena)
    SELECT ID, CASE Sifra WHEN N'D-1001' THEN 10 WHEN N'D-1002' THEN 12 WHEN N'D-1003' THEN 200 ELSE 3 END,
           CASE Sifra WHEN N'D-1001' THEN 800 WHEN N'D-1002' THEN 3500 WHEN N'D-1003' THEN 310 ELSE 1200 END
    FROM dbo.SRV_Deo WHERE Sifra IN (N'D-1001', N'D-1002', N'D-1003', N'D-1004');
    DECLARE @br TABLE (Broj nvarchar(30));
    INSERT INTO @br EXEC dbo.SRV_PocetnoStanje @s, NULL, N'Početno stanje (test)', @K;
END

SELECT z.Sifra, z.Naziv, z.Stanje, z.Raspolozivo, z.MinZaliha, z.Cena, z.Vrednost, z.Status FROM dbo.SRV_vw_Zalihe z ORDER BY z.Sifra;
SELECT Oznaka, Naziv, Vrsta, Status, BrojiloTip, Brojilo FROM dbo.SRV_vw_Sredstvo ORDER BY Oznaka;
