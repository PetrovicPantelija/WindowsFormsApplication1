/* =====================================================================
   ZAH-010 Servis - 09: provera pravila iz poglavlja 13 (prihvatanje)
   SAMO ZA Jela_Test, posle 08_Test_podaci.sql.
   Pravi svoje test podatke sa oznakom TST (grupa „TST grupa“, delovi TST-...),
   pa sme da se pokrene vise puta (svaki put novi delovi TST-<broj>).
   Korisnici se samo simuliraju parametrom @Korisnik; za proveru odobravanja
   u bazi moraju postojati dva clana grupe nivoa 1 (vidi @Sef1, @Sef2).
   Rezultat: lista provera sa OK / GRESKA.
   ===================================================================== */
SET NOCOUNT ON;

-- dva razlicita clana grupe Servis.Odobravanje.GrupaNivo1 (promeniti po bazi)
DECLARE @Sef1 nvarchar(50), @Sef2 nvarchar(50), @Dir nvarchar(50);
SELECT TOP 1 @Sef1 = RTRIM(kg.Korisnik) FROM dbo.KorisnikGrupa kg JOIN dbo.Grupe g ON g.IdGrupe = kg.IdGrupe
WHERE RTRIM(g.Naziv) = dbo.SRV_Pod(N'Servis.Odobravanje.GrupaNivo1') ORDER BY kg.Korisnik;
SELECT TOP 1 @Sef2 = RTRIM(kg.Korisnik) FROM dbo.KorisnikGrupa kg JOIN dbo.Grupe g ON g.IdGrupe = kg.IdGrupe
WHERE RTRIM(g.Naziv) = dbo.SRV_Pod(N'Servis.Odobravanje.GrupaNivo1') AND RTRIM(kg.Korisnik) <> @Sef1 ORDER BY kg.Korisnik;
SELECT TOP 1 @Dir = RTRIM(kg.Korisnik) FROM dbo.KorisnikGrupa kg JOIN dbo.Grupe g ON g.IdGrupe = kg.IdGrupe
WHERE RTRIM(g.Naziv) IN (dbo.SRV_Pod(N'Servis.Odobravanje.GrupaNivo2'), dbo.SRV_Pod(N'Servis.Odobravanje.GrupaNivo3')) ORDER BY kg.Korisnik;

DECLARE @Rez TABLE (RB int IDENTITY, Provera nvarchar(200), Ocekivano nvarchar(200), Dobijeno nvarchar(400), Ok bit);
DECLARE @K nvarchar(50) = N'test-prihvatanje', @e nvarchar(2048), @x nvarchar(400), @n int = (SELECT COUNT(*) FROM dbo.SRV_Deo WHERE Sifra LIKE N'TST-%');
DECLARE @r1 TABLE (ID int); DECLARE @r2 TABLE (ID int, Broj nvarchar(30)); DECLARE @r3 TABLE (Broj nvarchar(30)); DECLARE @r4 TABLE (Status nvarchar(20));
DECLARE @r5 TABLE (ID int, Broj nvarchar(30), Nivo tinyint); DECLARE @r6 TABLE (Izdatnica nvarchar(30));
DECLARE @s dbo.SRV_StavkaTip, @ids dbo.SRV_IdTip;
DECLARE @Grupa int, @Kamion int, @KamT1 int, @Nalog int, @Nd int, @d1 int, @d2 int, @d3 int, @d4 int, @d5 int, @Pop int, @Kor int, @Nar int, @Zah int;
DECLARE @suf nvarchar(10) = CAST(@n + 1 AS nvarchar(10));

IF @Sef1 IS NULL OR @Sef2 IS NULL
BEGIN
    RAISERROR(N'Za test su potrebna dva člana grupe nivoa 1 (Servis.Odobravanje.GrupaNivo1).', 16, 1);
    RETURN;
END

SELECT @Grupa = ID FROM dbo.SRV_DeoGrupa WHERE Naziv = N'TST grupa';
IF @Grupa IS NULL BEGIN INSERT INTO @r1 EXEC dbo.SRV_SacuvajGrupuDelova NULL, N'TST grupa', 1, @K; SELECT @Grupa = ID FROM @r1; DELETE FROM @r1; END
SELECT @Kamion = ID FROM dbo.SRV_VrstaSredstva WHERE Naziv = N'Kamion';
SELECT @KamT1 = ID FROM dbo.SRV_Sredstvo WHERE Oznaka = N'KAM-T1';

-- test delovi: d1 izdavanje (10 kom po 800), d2 nema dela (3 kom), d3 prosecna cena, d4 predlog, d5 popis/korekcija
DECLARE @sx nvarchar(30);
SET @sx = N'TST-' + @suf + N'-1'; INSERT INTO @r1 EXEC dbo.SRV_SacuvajDeo NULL, @sx, N'Test izdavanje', @Grupa, N'kom', 0, 20, NULL, 0, 101, NULL, NULL, 1, @K; SELECT @d1 = ID FROM @r1; DELETE FROM @r1;
SET @sx = N'TST-' + @suf + N'-2'; INSERT INTO @r1 EXEC dbo.SRV_SacuvajDeo NULL, @sx, N'Test nema dela', @Grupa, N'kom', 2, 10, NULL, 1, 102, NULL, NULL, 1, @K; SELECT @d2 = ID FROM @r1; DELETE FROM @r1;
SET @sx = N'TST-' + @suf + N'-3'; INSERT INTO @r1 EXEC dbo.SRV_SacuvajDeo NULL, @sx, N'Test prosecna cena', @Grupa, N'kom', 0, 50, NULL, 0, NULL, NULL, NULL, 1, @K; SELECT @d3 = ID FROM @r1; DELETE FROM @r1;
SET @sx = N'TST-' + @suf + N'-4'; INSERT INTO @r1 EXEC dbo.SRV_SacuvajDeo NULL, @sx, N'Test predlog', @Grupa, N'kom', 10, 30, NULL, 0, 101, NULL, NULL, 1, @K; SELECT @d4 = ID FROM @r1; DELETE FROM @r1;
SET @sx = N'TST-' + @suf + N'-5'; INSERT INTO @r1 EXEC dbo.SRV_SacuvajDeo NULL, @sx, N'Test popis', @Grupa, N'kom', 0, 100, NULL, 0, NULL, NULL, NULL, 1, @K; SELECT @d5 = ID FROM @r1; DELETE FROM @r1;

INSERT INTO @s VALUES (@d1, 10, 800, NULL), (@d2, 3, 1200, NULL), (@d3, 10, 0, NULL), (@d4, 12, 500, NULL), (@d5, 41, 100, NULL);
INSERT INTO @r3 EXEC dbo.SRV_PocetnoStanje @s, NULL, N'TST', @K; DELETE FROM @r3; DELETE FROM @s;

/* ---- 1. Izdavanje ---- */
INSERT INTO @r2 EXEC dbo.SRV_NalogNovi @KamT1, N'Vanredni', NULL, NULL, N'Interni', N'TST nalog', NULL, 0, @K;
SELECT @Nalog = ID FROM @r2; DELETE FROM @r2;
INSERT INTO @r4 EXEC dbo.SRV_NalogDodajDeo @Nalog, @d1, 3, @K; DELETE FROM @r4;
INSERT INTO @Rez SELECT N'Rezervacija 3 od 10', N'stanje 10, rezervisano 3, raspoloživo 7',
       dbo.SRV_Broj(Stanje) + N' / ' + dbo.SRV_Broj(Rezervisano) + N' / ' + dbo.SRV_Broj(Raspolozivo),
       CASE WHEN Stanje = 10 AND Rezervisano = 3 AND Raspolozivo = 7 THEN 1 ELSE 0 END FROM dbo.SRV_vw_Zalihe WHERE DeoID = @d1;
SELECT @Nd = ID FROM dbo.SRV_NalogDeo WHERE NalogID = @Nalog AND DeoID = @d1;
INSERT INTO @ids VALUES (@Nd);
INSERT INTO @r3 EXEC dbo.SRV_NalogIzdaj @Nalog, @ids, 0, @K; SELECT @x = Broj FROM @r3; DELETE FROM @r3; DELETE FROM @ids;
INSERT INTO @Rez SELECT N'Izdavanje 3', N'stanje 7, rezervisano 0, raspoloživo 7',
       dbo.SRV_Broj(Stanje) + N' / ' + dbo.SRV_Broj(Rezervisano) + N' / ' + dbo.SRV_Broj(Raspolozivo),
       CASE WHEN Stanje = 7 AND Rezervisano = 0 AND Raspolozivo = 7 THEN 1 ELSE 0 END FROM dbo.SRV_vw_Zalihe WHERE DeoID = @d1;
INSERT INTO @Rez SELECT N'Trošak naloga posle izdavanja', N'delovi 2.400,00', dbo.SRV_Broj(Delovi), CASE WHEN Delovi = 2400 THEN 1 ELSE 0 END
FROM dbo.SRV_vw_NalogTrosak WHERE NalogID = @Nalog;
INSERT INTO @Rez SELECT N'Broj izdatnice ima 4 cifre', N'SIZ-####', @x, CASE WHEN @x LIKE N'SIZ-[0-9][0-9][0-9][0-9]' THEN 1 ELSE 0 END;

/* ---- povrat: najvise izdato - vraceno ---- */
BEGIN TRY EXEC dbo.SRV_NalogPovrat @Nd, 4, NULL, @K; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
DELETE FROM @r3;
INSERT INTO @Rez VALUES (N'Povrat 4 od izdatih 3', N'Količina mora biti veća od 0 i najviše 3,00.', @e, CASE WHEN @e = N'Količina mora biti veća od 0 i najviše 3,00.' THEN 1 ELSE 0 END);
INSERT INTO @r3 EXEC dbo.SRV_NalogPovrat @Nd, 1, NULL, @K; DELETE FROM @r3;
INSERT INTO @Rez SELECT N'Povrat 1: trošak naloga', N'delovi 1.600,00', dbo.SRV_Broj(Delovi), CASE WHEN Delovi = 1600 THEN 1 ELSE 0 END
FROM dbo.SRV_vw_NalogTrosak WHERE NalogID = @Nalog;

/* ---- 3. Nema dela -> Ceka delove -> po izdavanju U radu ---- */
INSERT INTO @r4 EXEC dbo.SRV_NalogDodajDeo @Nalog, @d2, 5, @K; SELECT @x = Status FROM @r4; DELETE FROM @r4;
INSERT INTO @Rez SELECT N'Stavka bez zalihe', N'Nema na stanju / nalog Čeka delove', @x + N' / ' + Status,
       CASE WHEN @x = N'Nema na stanju' AND Status = N'Čeka delove' THEN 1 ELSE 0 END FROM dbo.SRV_Nalog WHERE ID = @Nalog;
INSERT INTO @s VALUES (@d2, 5, 1200, NULL);
INSERT INTO @r3 EXEC dbo.SRV_Prijem NULL, NULL, NULL, N'TST hitna nabavka', NULL, NULL, @s, @K; SELECT @x = Broj FROM @r3; DELETE FROM @r3; DELETE FROM @s;
INSERT INTO @Rez VALUES (N'Broj prijema', N'SPR-####', @x, CASE WHEN @x LIKE N'SPR-[0-9][0-9][0-9][0-9]' THEN 1 ELSE 0 END);
SELECT @Nd = ID FROM dbo.SRV_NalogDeo WHERE NalogID = @Nalog AND DeoID = @d2;
INSERT INTO @ids VALUES (@Nd);
INSERT INTO @r3 EXEC dbo.SRV_NalogIzdaj @Nalog, @ids, 0, @K; DELETE FROM @r3; DELETE FROM @ids;
INSERT INTO @Rez SELECT N'Po izdavanju poslednjeg dela', N'U radu', Status, CASE WHEN Status = N'U radu' THEN 1 ELSE 0 END FROM dbo.SRV_Nalog WHERE ID = @Nalog;

/* ---- MD-R08: kritican deo ispod minimuma (d2: min 2, sada 3) ---- */
DECLARE @ob1 int = (SELECT COUNT(*) FROM dbo.SRV_DeoObavestenje WHERE DeoID = @d2);
INSERT INTO @r4 EXEC dbo.SRV_NalogDodajDeo @Nalog, @d2, 2, @K; DELETE FROM @r4;   -- raspolozivo 1 < 2
INSERT INTO @r4 EXEC dbo.SRV_NalogDodajDeo @Nalog, @d2, 1, @K; DELETE FROM @r4;   -- raspolozivo 0, ne sme novo obavestenje
INSERT INTO @Rez SELECT N'Obaveštenje kritičan deo (jedno)', N'1 novo obaveštenje', CAST(COUNT(*) - @ob1 AS nvarchar(10)) + N': ' + MAX(Tekst),
       CASE WHEN COUNT(*) - @ob1 = 1 THEN 1 ELSE 0 END FROM dbo.SRV_DeoObavestenje WHERE DeoID = @d2;

/* ---- SR-10: zavrsen nalog je zakljucan; SR-08 izbor za neizdate ---- */
BEGIN TRY EXEC dbo.SRV_NalogZavrsi @Nalog, 250100, NULL, 0, @K; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
DELETE FROM @r6;
INSERT INTO @Rez VALUES (N'Završetak sa neizdatim delovima bez izbora', N'traži izbor', @e, CASE WHEN @e LIKE N'Nalog ima rezervisane a neizdate delove%' THEN 1 ELSE 0 END);
BEGIN TRY EXEC dbo.SRV_NalogZavrsi @Nalog, 249000, N'Storniraj', 0, @K; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
DELETE FROM @r6;
INSERT INTO @Rez VALUES (N'Brojilo pri zatvaranju manje od otvaranja', N'odbijeno', @e, CASE WHEN @e LIKE N'Brojilo pri zatvaranju ne sme biti manje%' THEN 1 ELSE 0 END);
INSERT INTO @r6 EXEC dbo.SRV_NalogZavrsi @Nalog, 250100, N'Storniraj', 0, @K; DELETE FROM @r6;
BEGIN TRY EXEC dbo.SRV_NalogDodajDeo @Nalog, @d1, 1, @K; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
DELETE FROM @r4;
INSERT INTO @Rez VALUES (N'Dodavanje dela na završen nalog', N'zaključan', @e, CASE WHEN @e LIKE N'%zaključan%' THEN 1 ELSE 0 END);
INSERT INTO @Rez SELECT N'Rezervacije posle storna', N'rezervisano 0 (d2)', dbo.SRV_Broj(Rezervisano), CASE WHEN Rezervisano = 0 THEN 1 ELSE 0 END
FROM dbo.SRV_vw_Zalihe WHERE DeoID = @d2;

/* ---- 5. Prosecna cena: 10 po 0 + prijem 5 po 800 ---- */
INSERT INTO @s VALUES (@d3, 5, 800, NULL);
INSERT INTO @r3 EXEC dbo.SRV_Prijem NULL, NULL, NULL, N'TST', NULL, NULL, @s, @K; DELETE FROM @r3; DELETE FROM @s;
INSERT INTO @Rez SELECT N'Prosečna cena', N'15 kom, 266,67, vrednost 4.000,00',
       dbo.SRV_Broj(Stanje) + N' kom, ' + dbo.SRV_Broj(Cena) + N', vrednost ' + dbo.SRV_Broj(Vrednost),
       CASE WHEN Stanje = 15 AND ROUND(Cena, 2) = 266.67 AND Vrednost = 4000 THEN 1 ELSE 0 END FROM dbo.SRV_vw_Zalihe WHERE DeoID = @d3;

/* ---- 4. Predlog nabavke: max 30, raspolozivo 12, ceka 20, naruceno 0 -> 38 ---- */
INSERT INTO @r2 EXEC dbo.SRV_NalogNovi @KamT1, N'Vanredni', NULL, NULL, N'Interni', N'TST predlog', NULL, 0, @K;
DECLARE @Nalog2 int = (SELECT ID FROM @r2); DELETE FROM @r2;
INSERT INTO @r4 EXEC dbo.SRV_NalogDodajDeo @Nalog2, @d4, 20, @K; DELETE FROM @r4;
INSERT INTO @Rez SELECT N'Predlog nabavke', N'38', ISNULL(dbo.SRV_Broj(MAX(Predlog)), N'(nije na spisku)'), CASE WHEN MAX(Predlog) = 38 THEN 1 ELSE 0 END
FROM dbo.SRV_vw_PredlogNabavke WHERE DeoID = @d4;
INSERT INTO @Rez SELECT N'Deo iznad minimuma bez čekanja nije u predlogu', N'0 redova', CAST(COUNT(*) AS nvarchar(10)), CASE WHEN COUNT(*) = 0 THEN 1 ELSE 0 END
FROM dbo.SRV_vw_PredlogNabavke WHERE DeoID = @d1;

/* ---- narudzbenica: isti korisnik ne odobrava ---- */
INSERT INTO @s VALUES (@d4, 38, 500, NULL);
INSERT INTO @r2 EXEC dbo.SRV_SacuvajNarudzbenicu NULL, 101, NULL, NULL, N'TST', @s, @Sef1; SELECT @Nar = ID, @x = Broj FROM @r2; DELETE FROM @r2; DELETE FROM @s;
INSERT INTO @Rez VALUES (N'Broj narudžbenice', N'SNA-GGGG-####', @x, CASE WHEN @x LIKE N'SNA-' + CAST(YEAR(GETDATE()) AS nvarchar(4)) + N'-[0-9][0-9][0-9][0-9]' THEN 1 ELSE 0 END);
EXEC dbo.SRV_NarudzbenicaAkcija @Nar, N'Posalji', NULL, @Sef1;
BEGIN TRY EXEC dbo.SRV_NarudzbenicaAkcija @Nar, N'Odobri', NULL, @Sef1; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
INSERT INTO @Rez VALUES (N'Narudžbenicu odobrava podnosilac', N'odbijeno', @e, CASE WHEN @e LIKE N'Zahtev ne sme da odobri isti korisnik%' THEN 1 ELSE 0 END);
EXEC dbo.SRV_NarudzbenicaAkcija @Nar, N'Odobri', NULL, @Sef2;
INSERT INTO @Rez SELECT N'Naručeno posle odobrenja', N'naručeno 38, predlog 0', dbo.SRV_Broj(Naruceno) + N', predlog ' + ISNULL((SELECT dbo.SRV_Broj(Predlog) FROM dbo.SRV_vw_PredlogNabavke WHERE DeoID = @d4), N'-'),
       CASE WHEN Naruceno = 38 THEN 1 ELSE 0 END FROM dbo.SRV_vw_Zalihe WHERE DeoID = @d4;
-- prijem preko narucenog bez komentara
INSERT INTO @s VALUES (@d4, 40, NULL, NULL);
BEGIN TRY EXEC dbo.SRV_Prijem @Nar, NULL, NULL, NULL, NULL, NULL, @s, @K; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
DELETE FROM @r3; DELETE FROM @s;
INSERT INTO @Rez VALUES (N'Prijem preko naručenog bez komentara', N'Količina je veća od preostale po narudžbenici (38,00). Unesite komentar.', @e,
                         CASE WHEN @e = N'Količina je veća od preostale po narudžbenici (38,00). Unesite komentar.' THEN 1 ELSE 0 END);
INSERT INTO @s VALUES (@d4, 20, NULL, NULL);
INSERT INTO @r3 EXEC dbo.SRV_Prijem @Nar, NULL, NULL, NULL, NULL, NULL, @s, @K; DELETE FROM @r3; DELETE FROM @s;
INSERT INTO @Rez SELECT N'Delimičan prijem', N'Delimično isporučeno', Status, CASE WHEN Status = N'Delimično isporučeno' THEN 1 ELSE 0 END FROM dbo.SRV_Narudzbenica WHERE ID = @Nar;

/* ---- 7. Popis: razlika -1, zaliha se ne menja do odobrenja; dva popisa ---- */
UPDATE dbo.SRV_Popis SET Status = N'Storniran' WHERE DeoGrupaID = @Grupa AND Status IN (N'U toku', N'Čeka odobrenje');  -- ostaci prethodnog pokretanja
INSERT INTO @r2 EXEC dbo.SRV_PopisNovi NULL, @Grupa, N'TST', @Sef1; SELECT @Pop = ID FROM @r2; DELETE FROM @r2;
DECLARE @ps int = (SELECT ID FROM dbo.SRV_PopisStavka WHERE PopisID = @Pop AND DeoID = @d5);
EXEC dbo.SRV_PopisUnesi @ps, 40, NULL, @Sef1;
INSERT INTO @Rez SELECT N'Popis razlika', N'knjig. 41, prebrojano 40, razlika -1, vrednost -100', dbo.SRV_Broj(Knjigovodstveno) + N' / ' + dbo.SRV_Broj(Prebrojano) + N' / ' + dbo.SRV_Broj(Razlika) + N' / ' + dbo.SRV_Broj(VrednostRazlike),
       CASE WHEN Razlika = -1 AND VrednostRazlike = -100 THEN 1 ELSE 0 END FROM dbo.SRV_vw_PopisStavka WHERE ID = @ps;
INSERT INTO @Rez SELECT N'Zaliha pre odobrenja popisa', N'41', dbo.SRV_Broj(Stanje), CASE WHEN Stanje = 41 THEN 1 ELSE 0 END FROM dbo.SRV_vw_DeoStanje WHERE DeoID = @d5;
BEGIN TRY EXEC dbo.SRV_PopisNovi NULL, @Grupa, N'TST 2', @Sef1; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
DELETE FROM @r2;
INSERT INTO @Rez VALUES (N'Drugi popis iste grupe', N'Za iste delove već postoji popis koji nije zaključen.', @e, CASE WHEN @e = N'Za iste delove već postoji popis koji nije zaključen.' THEN 1 ELSE 0 END);
BEGIN TRY EXEC dbo.SRV_PopisUnesi @ps, 1000000000, NULL, @Sef1; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
INSERT INTO @Rez VALUES (N'Prebrojano van opsega', N'Polje „Prebrojano“ mora biti između 0 i 999.999.999.', @e, CASE WHEN @e = N'Polje „Prebrojano“ mora biti između 0 i 999.999.999.' THEN 1 ELSE 0 END);
EXEC dbo.SRV_PopisAkcija @Pop, N'Posalji', @Sef1;
BEGIN TRY EXEC dbo.SRV_PopisAkcija @Pop, N'Odobri', @Sef1; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
INSERT INTO @Rez VALUES (N'Popis odobrava podnosilac', N'odbijeno', @e, CASE WHEN @e LIKE N'Zahtev ne sme da odobri isti korisnik%' THEN 1 ELSE 0 END);
EXEC dbo.SRV_PopisAkcija @Pop, N'Odobri', @Sef2;
INSERT INTO @Rez SELECT N'Zaliha posle odobrenja popisa', N'40', dbo.SRV_Broj(Stanje), CASE WHEN Stanje = 40 THEN 1 ELSE 0 END FROM dbo.SRV_vw_DeoStanje WHERE DeoID = @d5;

/* ---- Korekcija: u minus odbijena pri zahtevu (uracunate neodobrene) ---- */
BEGIN TRY EXEC dbo.SRV_KorekcijaNova @d5, -41, N'TST', @Sef1; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
DELETE FROM @r2;
INSERT INTO @Rez VALUES (N'Korekcija u minus', N'Posle korekcije stanje dela bi bilo negativno.', @e, CASE WHEN @e = N'Posle korekcije stanje dela bi bilo negativno.' THEN 1 ELSE 0 END);
INSERT INTO @r2 EXEC dbo.SRV_KorekcijaNova @d5, -30, N'TST lom', @Sef1; SELECT @Kor = ID FROM @r2; DELETE FROM @r2;
BEGIN TRY EXEC dbo.SRV_KorekcijaNova @d5, -15, N'TST', @Sef1; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
DELETE FROM @r2;
INSERT INTO @Rez VALUES (N'Korekcija sa neodobrenom (-30, pa -15 od 40)', N'odbijeno', @e, CASE WHEN @e = N'Posle korekcije stanje dela bi bilo negativno.' THEN 1 ELSE 0 END);
BEGIN TRY EXEC dbo.SRV_KorekcijaNova @d5, 0, N'TST', @Sef1; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
DELETE FROM @r2;
INSERT INTO @Rez VALUES (N'Korekcija 0', N'Polje „Korekcija (+ višak, − manjak)“ ne sme biti 0.', @e, CASE WHEN @e = N'Polje „Korekcija (+ višak, − manjak)“ ne sme biti 0.' THEN 1 ELSE 0 END);
BEGIN TRY EXEC dbo.SRV_KorekcijaOdluka @Kor, 1, @Sef1; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
INSERT INTO @Rez VALUES (N'Korekciju odobrava podnosilac', N'odbijeno', @e, CASE WHEN @e LIKE N'Zahtev ne sme da odobri isti korisnik%' THEN 1 ELSE 0 END);
EXEC dbo.SRV_KorekcijaOdluka @Kor, 1, @Sef2;
INSERT INTO @Rez SELECT N'Stanje posle korekcije', N'10', dbo.SRV_Broj(Stanje), CASE WHEN Stanje = 10 THEN 1 ELSE 0 END FROM dbo.SRV_vw_DeoStanje WHERE DeoID = @d5;

/* ---- Spoljni servis: 168.000 -> nivo 2; podnosilac ne odobrava ---- */
INSERT INTO @r2 EXEC dbo.SRV_NalogNovi @KamT1, N'Vanredni', NULL, NULL, N'Spoljni', N'TST spoljni', NULL, 0, @K;
DECLARE @Nalog3 int = (SELECT ID FROM @r2); DELETE FROM @r2;
BEGIN TRY EXEC dbo.SRV_ZahtevSacuvaj NULL, @Nalog3, 104, N'P-1', N'TST', 0, @Sef1; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
DELETE FROM @r5;
INSERT INTO @Rez VALUES (N'Zahtev sa iznosom 0', N'odbijeno', @e, CASE WHEN @e = N'Polje „Iznos“ mora biti veće od 0.' THEN 1 ELSE 0 END);
INSERT INTO @r5 EXEC dbo.SRV_ZahtevSacuvaj NULL, @Nalog3, 104, N'P-1', N'TST menjac', 168000, @Sef1; SELECT @Zah = ID, @x = CAST(Nivo AS nvarchar(3)) FROM @r5; DELETE FROM @r5;
INSERT INTO @Rez SELECT N'Zahtev 168.000: nivo i status naloga', N'nivo 2, nalog Čeka odobrenje', N'nivo ' + @x + N', nalog ' + Status,
       CASE WHEN @x = N'2' AND Status = N'Čeka odobrenje' THEN 1 ELSE 0 END FROM dbo.SRV_Nalog WHERE ID = @Nalog3;
BEGIN TRY EXEC dbo.SRV_ZahtevAkcija @Zah, N'Odobri', NULL, NULL, NULL, @Sef2; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
INSERT INTO @Rez VALUES (N'Nivo 2 odobrava član grupe nivoa 1', N'odbijeno', @e, CASE WHEN @e LIKE N'Za odobrenje nivoa 2%' THEN 1 ELSE 0 END);
IF @Dir IS NOT NULL
BEGIN
    EXEC dbo.SRV_ZahtevAkcija @Zah, N'Odobri', NULL, NULL, NULL, @Dir;
    INSERT INTO @Rez SELECT N'Odobren spoljni servis u trošku', N'spoljni 168.000,00, nalog U radu', dbo.SRV_Broj(t.Spoljni) + N', nalog ' + n.Status,
           CASE WHEN t.Spoljni = 168000 AND n.Status = N'U radu' THEN 1 ELSE 0 END
    FROM dbo.SRV_vw_NalogTrosak t JOIN dbo.SRV_Nalog n ON n.ID = t.NalogID WHERE t.NalogID = @Nalog3;
END

/* ---- Dupli unosi: srpska poruka po nazivu ogranicenja ---- */
DECLARE @b dbo.SRV_TekstTip; INSERT INTO @b VALUES (N'KM');
BEGIN TRY EXEC dbo.SRV_SacuvajVrstu NULL, N'Kamion', NULL, 1, @b, @K; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
DELETE FROM @r1;
INSERT INTO @Rez VALUES (N'Dupla vrsta sredstva', N'#SQL2627# ... UQ_SRV_VrstaSredstva_Naziv', LEFT(@e, 120), CASE WHEN @e LIKE N'#SQL2627#%UQ_SRV_VrstaSredstva_Naziv%' THEN 1 ELSE 0 END);
SET @sx = N'TST-' + @suf + N'-1';
BEGIN TRY EXEC dbo.SRV_SacuvajDeo NULL, @sx, N'dup', @Grupa, N'kom', 0, 0, NULL, 0, NULL, NULL, NULL, 1, @K; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
DELETE FROM @r1;
INSERT INTO @Rez VALUES (N'Dupla šifra dela', N'UQ_SRV_Deo_Sifra', LEFT(@e, 120), CASE WHEN @e LIKE N'#SQL2627#%UQ_SRV_Deo_Sifra%' THEN 1 ELSE 0 END);
BEGIN TRY EXEC dbo.SRV_SacuvajDeo @d5, @sx, N'x', @Grupa, N'kom', 5, 1, NULL, 0, NULL, NULL, NULL, 1, @K; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
DELETE FROM @r1;
INSERT INTO @Rez VALUES (N'Min > Max', N'Maksimalna zaliha mora biti veća ili jednaka minimalnoj.', @e, CASE WHEN @e = N'Maksimalna zaliha mora biti veća ili jednaka minimalnoj.' THEN 1 ELSE 0 END);
SET @sx = (SELECT Sifra FROM dbo.SRV_Deo WHERE ID = @d5);
BEGIN TRY EXEC dbo.SRV_SacuvajDeo @d5, @sx, N'Test popis', @Grupa, N'kom', 0, 100, NULL, 0, NULL, NULL, NULL, 0, @K; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
DELETE FROM @r1;
INSERT INTO @Rez VALUES (N'Deaktivacija dela sa stanjem', N'Deo ima stanje 10,00 i ne može se deaktivirati.', @e, CASE WHEN @e = N'Deo ima stanje 10,00 i ne može se deaktivirati.' THEN 1 ELSE 0 END);
BEGIN TRY EXEC dbo.SRV_SacuvajPlan NULL, @Kamion, N'TST plan', 0, 0, 0, 0, 0, 0, 0, NULL, 1, @K; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
DELETE FROM @r1;
INSERT INTO @Rez VALUES (N'Plan bez intervala', N'Unesite bar jedan interval veći od 0.', @e, CASE WHEN @e = N'Unesite bar jedan interval veći od 0.' THEN 1 ELSE 0 END);
DECLARE @PlanK int = (SELECT ID FROM dbo.SRV_PlanOdrzavanja WHERE VrstaSredstvaID = @Kamion AND Naziv = N'Servis');
DECLARE @D1001 int = (SELECT ID FROM dbo.SRV_Deo WHERE Sifra = N'D-1001');
BEGIN TRY EXEC dbo.SRV_DodajNormativ @PlanK, @D1001, 1, @K; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
DELETE FROM @r1;
INSERT INTO @Rez VALUES (N'Dupli deo u normativu', N'UQ_SRV_PlanNormativ_Deo', LEFT(@e, 120), CASE WHEN @e LIKE N'#SQL2627#%UQ_SRV_PlanNormativ_Deo%' THEN 1 ELSE 0 END);
DECLARE @Vilj int = (SELECT ID FROM dbo.SRV_VrstaSredstva WHERE Naziv = N'Viljuškar');
DECLARE @PlanV int = (SELECT ID FROM dbo.SRV_PlanOdrzavanja WHERE VrstaSredstvaID = @Vilj AND Naziv = N'Mali servis');
BEGIN TRY EXEC dbo.SRV_NalogNovi @KamT1, N'Redovni', NULL, @PlanV, N'Interni', NULL, NULL, 0, @K; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
DELETE FROM @r2;
INSERT INTO @Rez VALUES (N'Nalog sa planom druge vrste', N'Plan ne pripada vrsti izabranog sredstva.', @e, CASE WHEN @e = N'Plan ne pripada vrsti izabranog sredstva.' THEN 1 ELSE 0 END);

/* ---- Redovni nalog rezervise normativ (MD-R09) ---- */
INSERT INTO @r2 EXEC dbo.SRV_NalogNovi @KamT1, N'Redovni', NULL, @PlanK, N'Interni', N'TST redovni', NULL, 0, @K;
DECLARE @Nalog4 int = (SELECT ID FROM @r2); DELETE FROM @r2;
INSERT INTO @Rez SELECT N'Redovni nalog: stavke iz normativa', N'2 stavke (D-1001 x3, D-1003 x120)', CAST(COUNT(*) AS nvarchar(10)) + N' stavke',
       CASE WHEN COUNT(*) = 2 AND SUM(CASE WHEN IzNormativa = 1 THEN 1 ELSE 0 END) = 2 THEN 1 ELSE 0 END FROM dbo.SRV_NalogDeo WHERE NalogID = @Nalog4;
EXEC dbo.SRV_NalogOtkazi @Nalog4, N'TST otkazan', @K;

/* ---- Kvar: ne moze da radi -> U kvaru; zavrsetak naloga -> U radu ---- */
DECLARE @VljT1 int = (SELECT ID FROM dbo.SRV_Sredstvo WHERE Oznaka = N'VLJ-T1'), @Kv int;
INSERT INTO @r2 EXEC dbo.SRV_KvarSacuvaj NULL, @VljT1, N'Vozač test', N'Telefon', N'Hidraulika', N'Visoka', 0, N'TST curi ulje', NULL, @K;
SELECT @Kv = ID, @x = Broj FROM @r2; DELETE FROM @r2;
INSERT INTO @Rez SELECT N'Prijava „ne može da radi“', N'sredstvo U kvaru, broj KV-GGGG-####', Status + N', ' + @x,
       CASE WHEN Status = N'U kvaru' AND @x LIKE N'KV-____-[0-9][0-9][0-9][0-9]' THEN 1 ELSE 0 END FROM dbo.SRV_Sredstvo WHERE ID = @VljT1;
BEGIN TRY EXEC dbo.SRV_KvarAkcija @Kv, N'Odbaci', NULL, @K; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
INSERT INTO @Rez VALUES (N'Odbacivanje bez razloga', N'Unesite polje „Razlog odbacivanja“.', @e, CASE WHEN @e = N'Unesite polje „Razlog odbacivanja“.' THEN 1 ELSE 0 END);
INSERT INTO @r2 EXEC dbo.SRV_NalogNovi @VljT1, N'Vanredni', @Kv, NULL, N'Interni', N'TST', NULL, 0, @K;
DECLARE @Nalog5 int = (SELECT ID FROM @r2); DELETE FROM @r2;
BEGIN TRY EXEC dbo.SRV_NalogNovi @VljT1, N'Vanredni', @Kv, NULL, N'Interni', N'TST', NULL, 0, @K; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
DELETE FROM @r2;
INSERT INTO @Rez VALUES (N'Drugi nalog iz iste prijave', N'upozorenje', @e, CASE WHEN @e LIKE N'Za ovu prijavu kvara već postoji servisni nalog%' THEN 1 ELSE 0 END);
DECLARE @mh decimal(18,2) = (SELECT Vrednost FROM dbo.SRV_vw_BrojiloTrenutno WHERE SredstvoID = @VljT1 AND Tip = N'MH') + 5;
BEGIN TRY EXEC dbo.SRV_NalogDodajRad @Nalog5, N'Mehaničar test', N'Mehaničar', NULL, 0, 1500, NULL, @K; SET @e = N'(nije odbijeno)'; END TRY BEGIN CATCH SET @e = ERROR_MESSAGE(); END CATCH
INSERT INTO @Rez VALUES (N'Rad sa 0 sati', N'Polje „Sati“ mora biti veće od 0.', @e, CASE WHEN @e = N'Polje „Sati“ mora biti veće od 0.' THEN 1 ELSE 0 END);
DECLARE @danas date = CAST(GETDATE() AS date);
EXEC dbo.SRV_NalogDodajRad @Nalog5, N'Mehaničar test', N'Mehaničar', @danas, 2, 1500, NULL, @K;
INSERT INTO @r6 EXEC dbo.SRV_NalogZavrsi @Nalog5, @mh, NULL, 0, @K; DELETE FROM @r6;
INSERT INTO @Rez SELECT N'Završetak naloga po kvaru', N'sredstvo U radu, kvar Zatvoreno, rad 3.000,00',
       s.Status + N', ' + k.Status + N', rad ' + dbo.SRV_Broj(t.Rad),
       CASE WHEN s.Status = N'U radu' AND k.Status = N'Zatvoreno' AND t.Rad = 3000 THEN 1 ELSE 0 END
FROM dbo.SRV_Sredstvo s, dbo.SRV_Kvar k, dbo.SRV_vw_NalogTrosak t WHERE s.ID = @VljT1 AND k.ID = @Kv AND t.NalogID = @Nalog5;

/* ---- SR-17: obavestenje o roku dokumenta jednom ---- */
DECLARE @VrDok int = (SELECT ID FROM dbo.SRV_VrstaSredstvaDokument WHERE VrstaSredstvaID = @Kamion AND Naziv = N'Registracija');
DECLARE @do date = DATEADD(day, 10, CAST(GETDATE() AS date)), @od date = DATEADD(year, -1, CAST(GETDATE() AS date)), @Dok int;
INSERT INTO @r1 EXEC dbo.SRV_SacuvajDokumentSredstva NULL, @KamT1, @VrDok, N'TST-REG', @od, @do, NULL, NULL, 1, @K; SELECT @Dok = ID FROM @r1; DELETE FROM @r1;
EXEC dbo.SRV_ProveriRokove @K; EXEC dbo.SRV_ProveriRokove @K;
INSERT INTO @Rez SELECT N'Obaveštenje o roku (dva pokretanja)', N'1', CAST(COUNT(*) AS nvarchar(10)), CASE WHEN COUNT(*) = 1 THEN 1 ELSE 0 END
FROM dbo.SRV_DeoObavestenje WHERE SredstvoDokumentID = @Dok;
SET @do = DATEADD(year, 1, @do);
INSERT INTO @r1 EXEC dbo.SRV_SacuvajDokumentSredstva @Dok, @KamT1, @VrDok, N'TST-REG', @od, @do, NULL, NULL, 1, @K; DELETE FROM @r1;
INSERT INTO @Rez SELECT N'Produženje dokumenta', N'Važeći', Status, CASE WHEN Status = N'Važeći' THEN 1 ELSE 0 END FROM dbo.SRV_vw_DokumentStatus WHERE ID = @Dok;
UPDATE dbo.SRV_SredstvoDokument SET Aktivno = 0 WHERE ID = @Dok;

/* ---- rezultat ---- */
SELECT RB, CASE WHEN Ok = 1 THEN N'OK' ELSE N'GRESKA' END AS Rezultat, Provera, Ocekivano, Dobijeno FROM @Rez ORDER BY RB;
SELECT SUM(CASE WHEN Ok = 1 THEN 1 ELSE 0 END) AS OK, SUM(CASE WHEN Ok = 0 THEN 1 ELSE 0 END) AS Greske FROM @Rez;
