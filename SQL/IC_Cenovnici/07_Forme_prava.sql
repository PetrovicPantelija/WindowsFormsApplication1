/* =====================================================================
   ZAH-008 Izracun cena - 07: nove forme u sistemu prava
   Upisuje forme u dbo.Forme (procedura InsertForme, kao frmForme).
   Grupe im se dodeljuju u aplikaciji: Administracija -> Dodeli pravo.
     administrator:   frmUvozExcel, frmOpstiCenovnik
     drugi korisnik:  frmPripremaCenovnika, frmCenovnici
   Skripta se sme pokretati vise puta.
   ===================================================================== */
SET NOCOUNT ON;

IF NOT EXISTS (SELECT 1 FROM dbo.Forme WHERE RTRIM(Code) = 'frmUvozExcel')
    EXEC dbo.InsertForme @Naziv = N'Izračun cena - Uvoz iz Excela', @Code = 'frmUvozExcel';
IF NOT EXISTS (SELECT 1 FROM dbo.Forme WHERE RTRIM(Code) = 'frmOpstiCenovnik')
    EXEC dbo.InsertForme @Naziv = N'Izračun cena - Opšti cenovnik', @Code = 'frmOpstiCenovnik';
IF NOT EXISTS (SELECT 1 FROM dbo.Forme WHERE RTRIM(Code) = 'frmPripremaCenovnika')
    EXEC dbo.InsertForme @Naziv = N'Izračun cena - Priprema cenovnika', @Code = 'frmPripremaCenovnika';
IF NOT EXISTS (SELECT 1 FROM dbo.Forme WHERE RTRIM(Code) = 'frmCenovnici')
    EXEC dbo.InsertForme @Naziv = N'Izračun cena - Pregled cenovnika', @Code = 'frmCenovnici';

SELECT IdForme, Naziv, Code FROM dbo.Forme
WHERE RTRIM(Code) IN ('frmUvozExcel', 'frmOpstiCenovnik', 'frmPripremaCenovnika', 'frmCenovnici');
