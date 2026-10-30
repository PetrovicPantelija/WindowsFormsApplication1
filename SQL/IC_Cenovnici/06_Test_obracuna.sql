/* =====================================================================
   ZAH-008 Izracun cena - 06: test obracuna (KRT90, sheet Decje sobe)
   Pravi probne podatke u transakciji, proverava vw_IC_CenovnikObracun
   i vw_IC_CenovnikAkcijePoredjenje, pa sve PONISTAVA (ROLLBACK).
   U bazi ne ostaje nista. Svaki red rezultata mora imati OK = 1.
   ===================================================================== */
SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRAN;

DECLARE @ProgramID int, @KolekcijaID int, @Dev int, @Din int, @ParDev int;

INSERT INTO dbo.IC_Program (Naziv) VALUES (N'ZZ_TEST_IC');
SET @ProgramID = SCOPE_IDENTITY();
INSERT INTO dbo.IC_Kolekcija (Naziv, ProgramID) VALUES (N'ZZ_TEST_IC', @ProgramID);
SET @KolekcijaID = SCOPE_IDENTITY();

/* ---------- Devizni: TP 6.449 (stari 6.487), VP 129, kurs 117,93, rabat 25%, akcija 5%, poredjenje 5% i 13% ---------- */
INSERT INTO dbo.IC_Cenovnik (Naziv, Vrsta, Valuta, Kurs, Kreirao) VALUES (N'ZZ_TEST_IC Devizni', 'Opsti', 'EUR', 117.93, N'test');
SET @Dev = SCOPE_IDENTITY();
INSERT INTO dbo.IC_CenovnikCena (CenovnikID, Code, VPAktuelna, VP) VALUES (@Dev, N'KRT90-TEST', 129, 129);
INSERT INTO dbo.IC_CenovnikStavka (CenovnikID, LargoProizvodID, SifraProizvodaLargo, Code, ProgramID, KolekcijaID,
                                   TrosakStari, TrosakNovi, MarzaStara)
VALUES (@Dev, -1, N'KRT90-BE/KI-00', N'KRT90-TEST', @ProgramID, @KolekcijaID, 6487, 6449, 1.3429);
INSERT INTO dbo.IC_CenovnikParametar (CenovnikID, Nivo, Rabat, Akcija) VALUES (@Dev, 'Cenovnik', 0.25, 0.05);
SET @ParDev = SCOPE_IDENTITY();
INSERT INTO dbo.IC_CenovnikAkcija (ParametarID, Redosled, Procenat) VALUES (@ParDev, 1, 0.05), (@ParDev, 2, 0.13);

/* ---------- Dinarski: TP 6.449, VP 16.412, rabat 34%, akcija 0% ---------- */
INSERT INTO dbo.IC_Cenovnik (Naziv, Vrsta, Valuta, Kurs, Kreirao) VALUES (N'ZZ_TEST_IC Dinarski', 'Opsti', 'RSD', 117.93, N'test');
SET @Din = SCOPE_IDENTITY();
INSERT INTO dbo.IC_CenovnikCena (CenovnikID, Code, VPAktuelna, VP) VALUES (@Din, N'KRT90-TEST', 16412, 16412);
INSERT INTO dbo.IC_CenovnikStavka (CenovnikID, LargoProizvodID, SifraProizvodaLargo, Code, ProgramID, KolekcijaID,
                                   TrosakStari, TrosakNovi)
VALUES (@Din, -1, N'KRT90-BE/KI-00', N'KRT90-TEST', @ProgramID, @KolekcijaID, 6449, 6449);
INSERT INTO dbo.IC_CenovnikParametar (CenovnikID, Nivo, Rabat, Akcija) VALUES (@Din, 'Cenovnik', 0.34, 0);

/* ---------- Provere ---------- */
DECLARE @R TABLE (Cenovnik varchar(10), Provera varchar(40), Ocekivano decimal(18,4), Dobijeno decimal(18,4));

INSERT INTO @R
SELECT 'Devizni', v.Provera, v.Ocekivano, v.Dobijeno
FROM dbo.vw_IC_CenovnikObracun o
CROSS APPLY (VALUES
    ('Marza 135,90%',            1.3590, ROUND(o.Marza, 4)),
    ('Posle rabata 97',          97,     o.CenaPosleRabata),
    ('Jela marza 77%',           0.77,   ROUND(o.JelaMarza, 2)),
    ('Akcija 5%: cena 92',       92,     o.CenaAkcija),
    ('Akcija 5%: Jela 68%',      0.68,   ROUND(o.JelaMarzaAkcija, 2)),
    ('Promena troska -0,59%',    -0.0059, ROUND(o.PromenaTroska, 4)),
    ('VP prati trosak 128',      128,    o.PredlogVPPratiTrosak),
    ('VP za staru marzu 128',    128,    o.PredlogVPStaraMarza)
) v (Provera, Ocekivano, Dobijeno)
WHERE o.CenovnikID = @Dev;

INSERT INTO @R
SELECT 'Devizni', 'Poredjenje ' + CAST(CAST(a.Procenat * 100 AS int) AS varchar(3)) + '%: cena', x.Ocekivano, a.Cena
FROM dbo.vw_IC_CenovnikAkcijePoredjenje a
CROSS APPLY (SELECT CASE a.Redosled WHEN 1 THEN 92 ELSE 84 END AS Ocekivano) x
WHERE a.CenovnikID = @Dev;

INSERT INTO @R
SELECT 'Dinarski', v.Provera, v.Ocekivano, v.Dobijeno
FROM dbo.vw_IC_CenovnikObracun o
CROSS APPLY (VALUES
    ('Marza 154,49%',         1.5449, ROUND(o.Marza, 4)),
    ('Posle rabata 10.832',   10832,  o.CenaPosleRabata),
    ('Jela marza 68%',        0.68,   ROUND(o.JelaMarza, 2)),
    ('Salon marza 52%',       0.52,   ROUND(o.SalonMarza, 2)),
    ('VP u evrima 139',       139,    o.VPuEUR),
    ('VP sa PDV 19.694',      19694,  o.VPsaPDV)
) v (Provera, Ocekivano, Dobijeno)
WHERE o.CenovnikID = @Din;

SELECT Cenovnik, Provera, Ocekivano, Dobijeno,
       CASE WHEN Dobijeno = Ocekivano THEN 1 ELSE 0 END AS OK
FROM @R;

SELECT CASE WHEN COUNT(*) = 16 AND MIN(CASE WHEN Dobijeno = Ocekivano THEN 1 ELSE 0 END) = 1
            THEN 'SVI TESTOVI PROSLI' ELSE 'IMA GRESAKA' END AS Rezultat,
       COUNT(*) AS BrojProvera
FROM @R;

ROLLBACK;   -- probni podaci se ne cuvaju
