/* =====================================================================
   ZAH-008 Izracun cena - 03: funkcija i pogledi
   Baza: Jela_Test. Kompatibilno sa SQL Server 2008 R2.
   Pokrenuti posle 02_IC_Tabele.sql.

   SQL 2008 R2 nema TRY_CONVERT ni CREATE OR ALTER, zato:
     - dbo.IC_UDecimal cita tekst iz jela_products (nvarchar(30)) kao broj;
     - svaki objekat se brise pa pravi ponovo (IF OBJECT_ID ... DROP).
   ===================================================================== */
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

/* ---------- dbo.IC_UDecimal: tekst -> decimal(18,2), NULL ako nije broj ----------
   Prihvata 16412, 16412.00, 16412,00, 16.412,00 i 16,412.00.
   Ako ima i tacku i zarez, decimalni je onaj koji je poslednji.
   Ako ima samo zarez, on je decimalni.                                   */
IF OBJECT_ID('dbo.IC_UDecimal', 'FN') IS NOT NULL DROP FUNCTION dbo.IC_UDecimal;
GO
CREATE FUNCTION dbo.IC_UDecimal (@Tekst nvarchar(30))
RETURNS decimal(18,2)
WITH SCHEMABINDING
AS
BEGIN
    DECLARE @n nvarchar(30) = REPLACE(REPLACE(LTRIM(RTRIM(@Tekst)), N' ', N''), NCHAR(160), N'');
    IF @n IS NULL OR @n = N'' RETURN NULL;
    -- provera PRE prelaska u varchar (inace bi se ², ½, − preslikali u 2, 1, -):
    -- dozvoljene su samo ASCII cifre, tacka, zarez i minus
    IF @n COLLATE Latin1_General_BIN2 LIKE N'%[^0123456789.,-]%' RETURN NULL;
    DECLARE @t varchar(30) = @n;

    DECLARE @odKrajaTacka int = CHARINDEX('.', REVERSE(@t)),
            @odKrajaZarez int = CHARINDEX(',', REVERSE(@t));

    IF @odKrajaTacka > 0 AND @odKrajaZarez > 0
        SET @t = CASE WHEN @odKrajaZarez < @odKrajaTacka
                      THEN REPLACE(REPLACE(@t, '.', ''), ',', '.')   -- 16.412,00
                      ELSE REPLACE(@t, ',', '') END;                 -- 16,412.00
    ELSE IF @odKrajaZarez > 0
        SET @t = REPLACE(@t, ',', '.');

    -- binarna kolacija: [0-9] su samo ASCII cifre (pod Windows kolacijom prolaze i ², ½...)
    IF @t COLLATE Latin1_General_BIN LIKE '%[^0123456789.-]%'
       OR @t LIKE '%.%.%' OR @t LIKE '_%-%'
       OR @t COLLATE Latin1_General_BIN NOT LIKE '%[0123456789]%'
       OR LEN(CASE WHEN CHARINDEX('.', @t) > 0 THEN LEFT(@t, CHARINDEX('.', @t) - 1) ELSE @t END) > 16
        RETURN NULL;

    RETURN ROUND(CONVERT(decimal(38,6), @t), 2);
END
GO

/* ---------- vw_IC_MaticniPodatak ----------
   Maticni podatak (LargoProizvodID) sa Code, programom, kolekcijom i cenom
   iz jela_products. Program i kolekcija su iz sifarnika izracuna cena
   (IC_Program, IC_Kolekcija): rucna dodela iz IC_KolekcijaPripadnost ima
   prednost, inace po nazivu iz jela_products (NazivRobneGrupe, NazivGrupe).
   ProgramID/KolekcijaID su NULL dok se ne pokrene IC_OsveziProgrameIKolekcije.
   ProgramIzProizvoda/KolekcijaIzProizvoda = nazivi iz jela_products.        */
IF OBJECT_ID('dbo.vw_IC_MaticniPodatak', 'V') IS NOT NULL DROP VIEW dbo.vw_IC_MaticniPodatak;
GO
CREATE VIEW dbo.vw_IC_MaticniPodatak AS
SELECT m.LargoProizvodID, m.SifraProizvodaLargo, m.Code,
       CASE WHEN r.LargoProizvodID IS NOT NULL THEN gr.Naziv ELSE m.Program END   AS Program,
       CASE WHEN r.LargoProizvodID IS NOT NULL THEN gr.ID    ELSE g.ID END        AS ProgramID,
       CASE WHEN r.LargoProizvodID IS NOT NULL THEN kr.Naziv ELSE m.Kolekcija END AS Kolekcija,
       CASE WHEN r.LargoProizvodID IS NOT NULL THEN kr.ID    ELSE k.ID END        AS KolekcijaID,
       CAST(CASE WHEN r.LargoProizvodID IS NOT NULL THEN 1 ELSE 0 END AS bit)     AS RucnaPripadnost,
       m.Program AS ProgramIzProizvoda, m.Kolekcija AS KolekcijaIzProizvoda,
       m.NazivProizvoda, m.Dimenzija, m.CenaRSD, m.CenaEUR, m.BrojSifara
FROM (
    SELECT l.LargoProizvodID,
           MIN(p.SifraProizvodaLargo)                   AS SifraProizvodaLargo,
           MIN(p.Code)                                  AS Code,
           MIN(LTRIM(RTRIM(p.NazivRobneGrupe)))         AS Program,
           MIN(LTRIM(RTRIM(p.NazivGrupe)))              AS Kolekcija,
           MIN(p.NazivProizvoda)                        AS NazivProizvoda,
           MIN(p.DeklaracijaDimenzija)                  AS Dimenzija,
           MIN(dbo.IC_UDecimal(p.price_rs_WP))          AS CenaRSD,
           MIN(dbo.IC_UDecimal(p.price_WP))             AS CenaEUR,
           COUNT(DISTINCT p.SifraProizvodaLargo)        AS BrojSifara   -- ocekuje se 1
    FROM dbo.RkShippingLinkWebsLargo l
    JOIN dbo.jela_products p ON p.BarCode = RTRIM(l.WebsGTIN)
    GROUP BY l.LargoProizvodID
) m
LEFT JOIN dbo.IC_Program   g  ON g.Naziv = m.Program
LEFT JOIN dbo.IC_Kolekcija k  ON k.Naziv = m.Kolekcija AND k.ProgramID = g.ID
LEFT JOIN dbo.IC_KolekcijaPripadnost r ON r.LargoProizvodID = m.LargoProizvodID
LEFT JOIN dbo.IC_Kolekcija kr ON kr.ID = r.KolekcijaID
LEFT JOIN dbo.IC_Program   gr ON gr.ID = kr.ProgramID;
GO

/* ---------- vw_IC_CenovnikObracun ----------
   Sve izracunate kolone po redu cenovnika. Isti pogled koriste grid,
   izvoz i proglasenje vazecim. Procenti su razlomci (0,25 = 25%).

   Zaokruzivanje (kao u Excelu):
     VP, VP sa PDV, cena posle rabata, cena u akciji -> ceo dinar / evro;
     Jela marze se racunaju od NEzaokruzenih cena.
   TP reda = TP-Novi; ako ga nema, TP-Stari.
   Marza = marza nova (VP prema TP reda). VP sa PDV, rabat, akcije, Jela i
   Salon marza: u opstem cenovniku od VP aktuelne (VPOsnova) dok se trosak
   ne proglasi novim (31.10.), u radnom i vazecem od VP.                    */
IF OBJECT_ID('dbo.vw_IC_CenovnikObracun', 'V') IS NOT NULL DROP VIEW dbo.vw_IC_CenovnikObracun;
GO
CREATE VIEW dbo.vw_IC_CenovnikObracun AS
SELECT s.StavkaID, s.CenovnikID, h.Valuta, h.Kurs, h.PDV,
       s.LargoProizvodID, s.SifraProizvodaLargo, s.Code, s.NazivProizvoda,
       s.ProgramID,   g.Naziv AS Program,
       s.KolekcijaID, k.Naziv AS Kolekcija,
       s.TrosakStari, s.TrosakNovi, s.DatumTroskaStari, s.DatumTroskaNovi,
       CAST(CASE WHEN s.BezTroska = 1 OR ISNULL(x.TP, 0) <= 0
                 THEN 1 ELSE 0 END AS bit)                                  AS BezTroska,
       s.TrosakNovi / NULLIF(s.TrosakStari, 0) - 1                          AS PromenaTroska,
       c.VPAktuelna, c.PovecanjeVP, c.VP, c.VPIzBaze,
       c.Izmenio AS CenuIzmenio, c.DatumIzmene AS DatumIzmeneCene,
       x.TP,
       x.TPuValuti,
       c.VP / NULLIF(x.TPuValuti, 0) - 1                                    AS Marza,
       s.MarzaStara,
       b.MarzaStaraObracun,                                                 -- sacuvana, inace VP aktuelna / TP-Stari - 1
       b.VPOsnova,
       CASE WHEN h.Valuta = 'RSD' THEN ROUND(b.VPOsnova * (1 + h.PDV), 0) END AS VPsaPDV,
       r.Rabat,
       ROUND(n.CenaRabat, 0)                                                AS CenaPosleRabata,
       n.CenaRabat / NULLIF(x.TPuValuti, 0) - 1                             AS JelaMarza,
       CASE WHEN h.Valuta = 'RSD'
            THEN (b.VPOsnova - n.CenaRabat) / NULLIF(n.CenaRabat, 0) END    AS SalonMarza,
       a.Akcija,
       ROUND(n.CenaAkcija, 0)                                               AS CenaAkcija,
       n.CenaAkcija / NULLIF(x.TPuValuti, 0) - 1                            AS JelaMarzaAkcija,
       ap.ParametarID                                                       AS ParametarAkcijaID,
       -- predlozi za administratora
       ROUND(ISNULL(c.VPAktuelna, c.VP) * s.TrosakNovi / NULLIF(s.TrosakStari, 0), 0) AS PredlogVPPratiTrosak,
       ROUND(s.TrosakNovi * (1 + b.MarzaStaraObracun)
             / CASE WHEN h.Valuta = 'RSD' THEN 1 ELSE h.Kurs END, 0)        AS PredlogVPStaraMarza,
       -- iznosi u evrima (dinarski cenovnik)
       CASE WHEN h.Valuta = 'RSD' THEN ROUND(b.VPOsnova / h.Kurs, 0) END             AS VPuEUR,
       CASE WHEN h.Valuta = 'RSD' THEN ROUND(b.VPOsnova * (1 + h.PDV) / h.Kurs, 0) END AS VPsaPDVuEUR,
       CASE WHEN h.Valuta = 'RSD' THEN ROUND(n.CenaRabat / h.Kurs, 0) END           AS CenaPosleRabatauEUR,
       CASE WHEN h.Valuta = 'RSD' THEN ROUND(n.CenaAkcija / h.Kurs, 0) END          AS CenaAkcijauEUR
FROM dbo.IC_CenovnikStavka s
JOIN dbo.IC_Cenovnik h     ON h.CenovnikID = s.CenovnikID
JOIN dbo.IC_CenovnikCena c ON c.CenovnikID = s.CenovnikID AND c.Code = s.Code
LEFT JOIN dbo.IC_Program   g ON g.ID = s.ProgramID
LEFT JOIN dbo.IC_Kolekcija k ON k.ID = s.KolekcijaID
CROSS APPLY (SELECT ISNULL(s.TrosakNovi, s.TrosakStari) AS TP,
                    CASE h.Valuta WHEN 'RSD' THEN ISNULL(s.TrosakNovi, s.TrosakStari)
                                  ELSE ISNULL(s.TrosakNovi, s.TrosakStari) / h.Kurs END AS TPuValuti) x
-- osnova za VP sa PDV, rabat i akcije: u opstem cenovniku VP aktuelna dok se trosak ne proglasi novim
-- (tada VP postaje VP aktuelna), u radnom i vazecem VP
CROSS APPLY (SELECT CASE WHEN h.Vrsta = 'Opsti' THEN ISNULL(c.VPAktuelna, c.VP) ELSE c.VP END AS VPOsnova,
                    ISNULL(s.MarzaStara,
                           c.VPAktuelna / NULLIF(CASE h.Valuta WHEN 'RSD' THEN s.TrosakStari
                                                 ELSE s.TrosakStari / h.Kurs END, 0) - 1) AS MarzaStaraObracun) b
OUTER APPLY (SELECT TOP (1) p.Rabat               -- najnizi nivo koji ima rabat
             FROM dbo.IC_CenovnikParametar p
             WHERE p.CenovnikID = s.CenovnikID AND p.Rabat IS NOT NULL
               AND (p.Nivo = 'Cenovnik'
                    OR (p.Nivo = 'Program'   AND p.ProgramID   = s.ProgramID)
                    OR (p.Nivo = 'Kolekcija' AND p.KolekcijaID = s.KolekcijaID)
                    OR (p.Nivo = 'Code'      AND p.Code        = s.Code))
             ORDER BY CASE p.Nivo WHEN 'Code' THEN 1 WHEN 'Kolekcija' THEN 2
                                  WHEN 'Program' THEN 3 ELSE 4 END) r0
CROSS APPLY (SELECT ISNULL(r0.Rabat, 0) AS Rabat) r
OUTER APPLY (SELECT TOP (1) p.Akcija              -- najnizi nivo koji ima vazecu akciju
             FROM dbo.IC_CenovnikParametar p
             WHERE p.CenovnikID = s.CenovnikID AND p.Akcija IS NOT NULL
               AND (p.Nivo = 'Cenovnik'
                    OR (p.Nivo = 'Program'   AND p.ProgramID   = s.ProgramID)
                    OR (p.Nivo = 'Kolekcija' AND p.KolekcijaID = s.KolekcijaID)
                    OR (p.Nivo = 'Code'      AND p.Code        = s.Code))
             ORDER BY CASE p.Nivo WHEN 'Code' THEN 1 WHEN 'Kolekcija' THEN 2
                                  WHEN 'Program' THEN 3 ELSE 4 END) a
OUTER APPLY (SELECT TOP (1) p.ParametarID         -- najnizi nivo koji ima akcije za poredjenje
             FROM dbo.IC_CenovnikParametar p
             WHERE p.CenovnikID = s.CenovnikID
               AND EXISTS (SELECT 1 FROM dbo.IC_CenovnikAkcija ak WHERE ak.ParametarID = p.ParametarID)
               AND (p.Nivo = 'Cenovnik'
                    OR (p.Nivo = 'Program'   AND p.ProgramID   = s.ProgramID)
                    OR (p.Nivo = 'Kolekcija' AND p.KolekcijaID = s.KolekcijaID)
                    OR (p.Nivo = 'Code'      AND p.Code        = s.Code))
             ORDER BY CASE p.Nivo WHEN 'Code' THEN 1 WHEN 'Kolekcija' THEN 2
                                  WHEN 'Program' THEN 3 ELSE 4 END) ap
CROSS APPLY (SELECT b.VPOsnova * (1 - r.Rabat)                           AS CenaRabat,
                    b.VPOsnova * (1 - r.Rabat) * (1 - ISNULL(a.Akcija, 0)) AS CenaAkcija) n;
GO

/* ---------- vw_IC_CenovnikAkcijePoredjenje ----------
   Akcije za poredjenje po redu: svaka se racuna od cene posle rabata,
   ne jedna na drugu.                                                       */
IF OBJECT_ID('dbo.vw_IC_CenovnikAkcijePoredjenje', 'V') IS NOT NULL DROP VIEW dbo.vw_IC_CenovnikAkcijePoredjenje;
GO
CREATE VIEW dbo.vw_IC_CenovnikAkcijePoredjenje AS
SELECT o.StavkaID, o.CenovnikID, o.LargoProizvodID, o.Code,
       ak.Redosled, ak.Procenat,
       ROUND(o.VPOsnova * (1 - o.Rabat) * (1 - ak.Procenat), 0)                 AS Cena,
       o.VPOsnova * (1 - o.Rabat) * (1 - ak.Procenat) / NULLIF(o.TPuValuti, 0) - 1 AS JelaMarza
FROM dbo.vw_IC_CenovnikObracun o
JOIN dbo.IC_CenovnikAkcija ak ON ak.ParametarID = o.ParametarAkcijaID;
GO

/* ---------- vw_IC_Cenovnik: zaglavlja za listu cenovnika ----------
   Status vazeceg: Vazeci / Buduci / Istekao.
   IzvorPromenjen = 1 kad je opsti (izvor radne kopije) menjan posle
   kopiranja ili poslednjeg "Preuzmi izmene".                               */
IF OBJECT_ID('dbo.vw_IC_Cenovnik', 'V') IS NOT NULL DROP VIEW dbo.vw_IC_Cenovnik;
GO
CREATE VIEW dbo.vw_IC_Cenovnik AS
SELECT h.CenovnikID, h.Naziv, h.Vrsta, h.Valuta, h.Kurs, h.PDV,
       h.PaSifra, pa.PaNaziv,
       CASE WHEN h.PaSifra IS NULL THEN N'Opšti' ELSE pa.PaNaziv END AS OpstiIliPartner,
       h.VaziOd, h.VaziDo,
       CASE WHEN h.Vrsta <> 'Vazeci' THEN NULL
            WHEN h.VaziDo < CAST(GETDATE() AS date) THEN N'Istekao'
            WHEN h.VaziOd > CAST(GETDATE() AS date) THEN N'Budući'
            ELSE N'Važeći' END AS Status,
       h.IzvorniCenovnikID, iz.Naziv AS IzvorniNaziv,
       CAST(CASE WHEN h.Vrsta = 'Radni' AND iz.Vrsta = 'Opsti'
                  AND (iz.DatumIzmene           > ISNULL(h.DatumPreuzimanjaIzvora, h.DatumKreiranja)
                    OR iz.DatumAzuriranjaTroska > ISNULL(h.DatumPreuzimanjaIzvora, h.DatumKreiranja))
                 THEN 1 ELSE 0 END AS bit) AS IzvorPromenjen,
       h.DatumAzuriranjaTroska, h.DatumProglasenjaTroska, h.DatumPreuzimanjaIzvora,
       h.Kreirao, h.DatumKreiranja, h.Izmenio, h.DatumIzmene,
       (SELECT COUNT(*) FROM dbo.IC_CenovnikStavka s WHERE s.CenovnikID = h.CenovnikID) AS BrojStavki
FROM dbo.IC_Cenovnik h
LEFT JOIN dbo.IC_Cenovnik iz ON iz.CenovnikID = h.IzvorniCenovnikID
LEFT JOIN dbo.Partnerji   pa ON pa.PaSifra    = h.PaSifra;
GO
