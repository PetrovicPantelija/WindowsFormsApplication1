/* =====================================================================
   ZAH-008 Izracun cena - 05: provera veze maticnih podataka
   Samo citanje. Pokrenuti posle 03 i posle IC_OsveziProgrameIKolekcije.
   ===================================================================== */
SET NOCOUNT ON;

PRINT '1. Broj maticnih podataka (LargoProizvodID) i Code';
SELECT COUNT(*)                     AS MaticnihPodataka,
       COUNT(DISTINCT Code)         AS RazlicitihCode,
       SUM(CASE WHEN Code IS NULL THEN 1 ELSE 0 END)        AS BezCode,
       SUM(CASE WHEN ProgramID IS NULL THEN 1 ELSE 0 END)   AS BezPrograma,
       SUM(CASE WHEN KolekcijaID IS NULL THEN 1 ELSE 0 END) AS BezKolekcije,
       SUM(CASE WHEN CenaRSD IS NULL THEN 1 ELSE 0 END)     AS BezCeneRSD,
       SUM(CASE WHEN CenaEUR IS NULL THEN 1 ELSE 0 END)     AS BezCeneEUR
FROM dbo.vw_IC_MaticniPodatak;

PRINT '2. LargoProizvodID sa vise SifraProizvodaLargo (ocekuje se prazno)';
SELECT LargoProizvodID, BrojSifara, SifraProizvodaLargo, Code
FROM dbo.vw_IC_MaticniPodatak
WHERE BrojSifara > 1
ORDER BY LargoProizvodID;

PRINT '3. jela_products bez veze sa RkShippingLinkWebsLargo';
SELECT p.BarCode, p.Code, p.SifraProizvodaLargo, p.NazivProizvoda, p.NazivRobneGrupe, p.NazivGrupe
FROM dbo.jela_products p
WHERE NOT EXISTS (SELECT 1 FROM dbo.RkShippingLinkWebsLargo l WHERE RTRIM(l.WebsGTIN) = p.BarCode)
ORDER BY p.NazivRobneGrupe, p.NazivGrupe, p.Code;

PRINT '4. Cene koje nisu procitane kao broj (tekst nije prazan, a IC_UDecimal vraca NULL)';
SELECT p.Code, p.price_rs_WP, p.price_WP
FROM dbo.jela_products p
WHERE (LTRIM(RTRIM(ISNULL(p.price_rs_WP, ''))) <> '' AND dbo.IC_UDecimal(p.price_rs_WP) IS NULL)
   OR (LTRIM(RTRIM(ISNULL(p.price_WP, '')))    <> '' AND dbo.IC_UDecimal(p.price_WP)    IS NULL);

PRINT '5. Programi i kolekcije iz jela_products (ocekuje se 7 programa, 53 kolekcije)';
SELECT g.ID AS ProgramID, g.Naziv AS Program, COUNT(DISTINCT k.ID) AS Kolekcija
FROM dbo.Program g
LEFT JOIN dbo.Kolekcije k ON k.ProgramID = g.ID
WHERE EXISTS (SELECT 1 FROM dbo.jela_products p WHERE LTRIM(RTRIM(p.NazivRobneGrupe)) = g.Naziv)
GROUP BY g.ID, g.Naziv
ORDER BY g.Naziv;

PRINT '6. Isti Code u vise programa ili kolekcija (cena po Code ih spaja)';
SELECT Code, COUNT(DISTINCT ProgramID) AS Programa, COUNT(DISTINCT KolekcijaID) AS Kolekcija
FROM dbo.vw_IC_MaticniPodatak
WHERE Code IS NOT NULL
GROUP BY Code
HAVING COUNT(DISTINCT ProgramID) > 1 OR COUNT(DISTINCT KolekcijaID) > 1
ORDER BY Code;

PRINT '7. Maticni podaci bez vazeceg troska u KalkCenik';
SELECT m.LargoProizvodID, m.SifraProizvodaLargo, m.Code, m.Program, m.Kolekcija
FROM dbo.vw_IC_MaticniPodatak m
WHERE NOT EXISTS (SELECT 1 FROM dbo.KalkCenik c
                  WHERE c.KcSifMp = m.LargoProizvodID AND c.KcValid = 1 AND c.KcTipCn = 'G')
ORDER BY m.Program, m.Kolekcija, m.LargoProizvodID;
