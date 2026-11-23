/* =====================================================================
   ZAH-008 Izracun cena - 07: nove forme u sistemu prava
   Upisuje forme u dbo.Forme (procedura InsertForme, kao frmForme).
   Grupe im se dodeljuju u aplikaciji: Administracija -> Dodeli pravo.
     administrator:   frmUvozExcel, frmOpstiCenovnik
     drugi korisnik:  frmPripremaCenovnika, frmCenovnici
     sifarnici:       frmICProgrami, frmICKolekcije, frmICPripadnost (administrator)
     proizvodi:       frmICProizvodi (jela_products)
     modul:           frmMainKalkulacije (ZAH-012, ulaz u Kalkulacije sa prijave)
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

-- sifarnici izracuna cena (30.10.): Programi, Kolekcije, Pripadnost kolekcijama
IF NOT EXISTS (SELECT 1 FROM dbo.Forme WHERE RTRIM(Code) = 'frmICProgrami')
    EXEC dbo.InsertForme @Naziv = N'Izračun cena - Programi', @Code = 'frmICProgrami';
IF NOT EXISTS (SELECT 1 FROM dbo.Forme WHERE RTRIM(Code) = 'frmICKolekcije')
    EXEC dbo.InsertForme @Naziv = N'Izračun cena - Kolekcije', @Code = 'frmICKolekcije';
IF NOT EXISTS (SELECT 1 FROM dbo.Forme WHERE RTRIM(Code) = 'frmICPripadnost')
    EXEC dbo.InsertForme @Naziv = N'Izračun cena - Pripadnost kolekcijama', @Code = 'frmICPripadnost';

SELECT IdForme, Naziv, Code FROM dbo.Forme
WHERE RTRIM(Code) IN ('frmICProgrami', 'frmICKolekcije', 'frmICPripadnost');

-- proizvodi (31.10.): jela_products - unos, izmena, slika, uvoz iz CSV
IF NOT EXISTS (SELECT 1 FROM dbo.Forme WHERE RTRIM(Code) = 'frmICProizvodi')
    EXEC dbo.InsertForme @Naziv = N'Izračun cena - Proizvodi (jela_products)', @Code = 'frmICProizvodi';

SELECT IdForme, Naziv, Code FROM dbo.Forme WHERE RTRIM(Code) = 'frmICProizvodi';

-- ZAH-012 (21.11.): ulaz u modul Kalkulacije sa prijavnog ekrana ide po grupama, ne po imenu korisnika.
-- Posle skripte: Dodeli pravo -> frmMainKalkulacije grupama koje rade u kalkulacijama (i administratoru).
IF NOT EXISTS (SELECT 1 FROM dbo.Forme WHERE RTRIM(Code) = 'frmMainKalkulacije')
    EXEC dbo.InsertForme @Naziv = N'Kalkulacije - glavni meni', @Code = 'frmMainKalkulacije';

SELECT IdForme, Naziv, Code FROM dbo.Forme WHERE RTRIM(Code) = 'frmMainKalkulacije';
