/* =====================================================================
   ZAH-008 Izracun cena - 99: uklanjanje svih IC_ objekata
   SAMO ZA TEST BAZU. Brise sve cenovnike, cene i veze slika!
   Brise i sifarnike izracuna cena (IC_Program, IC_Kolekcija, IC_KolekcijaPripadnost).
   Stare tabele Program, Kolekcije i KolekcijePripadnost se NE diraju.
   Da se ne bi pokrenulo slucajno, promeniti @Potvrda u 'DA'.
   ===================================================================== */
SET NOCOUNT ON;
DECLARE @Potvrda varchar(2) = 'NE';
IF @Potvrda <> 'DA'
BEGIN
    RAISERROR('Uklanjanje nije potvrdjeno (@Potvrda = ''DA'').', 16, 1);
    SET NOEXEC ON;
END
GO

IF OBJECT_ID('dbo.IC_UpisiPresek', 'P')              IS NOT NULL DROP PROCEDURE dbo.IC_UpisiPresek;
IF OBJECT_ID('dbo.IC_SacuvajPripadnost', 'P')        IS NOT NULL DROP PROCEDURE dbo.IC_SacuvajPripadnost;
IF OBJECT_ID('dbo.IC_IzvestajSlika', 'P')            IS NOT NULL DROP PROCEDURE dbo.IC_IzvestajSlika;
IF OBJECT_ID('dbo.IC_DodeliSlikuRucno', 'P')         IS NOT NULL DROP PROCEDURE dbo.IC_DodeliSlikuRucno;
IF OBJECT_ID('dbo.IC_PoveziSlikeAutomatski', 'P')    IS NOT NULL DROP PROCEDURE dbo.IC_PoveziSlikeAutomatski;
IF OBJECT_ID('dbo.IC_UpisiUvoz', 'P')                IS NOT NULL DROP PROCEDURE dbo.IC_UpisiUvoz;
IF OBJECT_ID('dbo.IC_ObrisiRadniCenovnik', 'P')      IS NOT NULL DROP PROCEDURE dbo.IC_ObrisiRadniCenovnik;
IF OBJECT_ID('dbo.IC_ProglasiVazecim', 'P')          IS NOT NULL DROP PROCEDURE dbo.IC_ProglasiVazecim;
IF OBJECT_ID('dbo.IC_PreuzmiIzmeneOpsteg', 'P')      IS NOT NULL DROP PROCEDURE dbo.IC_PreuzmiIzmeneOpsteg;
IF OBJECT_ID('dbo.IC_NapraviRadniCenovnik', 'P')     IS NOT NULL DROP PROCEDURE dbo.IC_NapraviRadniCenovnik;
IF OBJECT_ID('dbo.IC_KopirajSadrzaj', 'P')           IS NOT NULL DROP PROCEDURE dbo.IC_KopirajSadrzaj;
IF OBJECT_ID('dbo.IC_SacuvajParametre', 'P')         IS NOT NULL DROP PROCEDURE dbo.IC_SacuvajParametre;
IF OBJECT_ID('dbo.IC_SacuvajCene', 'P')              IS NOT NULL DROP PROCEDURE dbo.IC_SacuvajCene;
IF OBJECT_ID('dbo.IC_SacuvajZaglavlje', 'P')         IS NOT NULL DROP PROCEDURE dbo.IC_SacuvajZaglavlje;
IF OBJECT_ID('dbo.IC_ProglasiTroskoveNovim', 'P')    IS NOT NULL DROP PROCEDURE dbo.IC_ProglasiTroskoveNovim;
IF OBJECT_ID('dbo.IC_AzurirajTroskove', 'P')         IS NOT NULL DROP PROCEDURE dbo.IC_AzurirajTroskove;
IF OBJECT_ID('dbo.IC_OsveziProgrameIKolekcije', 'P') IS NOT NULL DROP PROCEDURE dbo.IC_OsveziProgrameIKolekcije;
GO

IF OBJECT_ID('dbo.vw_IC_Cenovnik', 'V')                 IS NOT NULL DROP VIEW dbo.vw_IC_Cenovnik;
IF OBJECT_ID('dbo.vw_IC_CenovnikAkcijePoredjenje', 'V') IS NOT NULL DROP VIEW dbo.vw_IC_CenovnikAkcijePoredjenje;
IF OBJECT_ID('dbo.vw_IC_CenovnikObracun', 'V')          IS NOT NULL DROP VIEW dbo.vw_IC_CenovnikObracun;
IF OBJECT_ID('dbo.vw_IC_MaticniPodatak', 'V')           IS NOT NULL DROP VIEW dbo.vw_IC_MaticniPodatak;
IF OBJECT_ID('dbo.IC_UDecimal', 'FN')                   IS NOT NULL DROP FUNCTION dbo.IC_UDecimal;
GO

IF OBJECT_ID('dbo.IC_CenovnikPresek', 'U')     IS NOT NULL DROP TABLE dbo.IC_CenovnikPresek;
IF OBJECT_ID('dbo.IC_SlikaProizvoda', 'U')     IS NOT NULL DROP TABLE dbo.IC_SlikaProizvoda;
IF OBJECT_ID('dbo.IC_CenovnikVazecaCena', 'U') IS NOT NULL DROP TABLE dbo.IC_CenovnikVazecaCena;
IF OBJECT_ID('dbo.IC_CenovnikStavka', 'U')     IS NOT NULL DROP TABLE dbo.IC_CenovnikStavka;
IF OBJECT_ID('dbo.IC_CenovnikCena', 'U')       IS NOT NULL DROP TABLE dbo.IC_CenovnikCena;
IF OBJECT_ID('dbo.IC_CenovnikAkcija', 'U')     IS NOT NULL DROP TABLE dbo.IC_CenovnikAkcija;
IF OBJECT_ID('dbo.IC_CenovnikParametar', 'U')  IS NOT NULL DROP TABLE dbo.IC_CenovnikParametar;
IF OBJECT_ID('dbo.IC_Cenovnik', 'U')           IS NOT NULL DROP TABLE dbo.IC_Cenovnik;
IF OBJECT_ID('dbo.IC_KolekcijaPripadnost', 'U') IS NOT NULL DROP TABLE dbo.IC_KolekcijaPripadnost;
IF OBJECT_ID('dbo.IC_Kolekcija', 'U')          IS NOT NULL DROP TABLE dbo.IC_Kolekcija;
IF OBJECT_ID('dbo.IC_Program', 'U')            IS NOT NULL DROP TABLE dbo.IC_Program;
IF OBJECT_ID('dbo.IC_Migracija', 'U')          IS NOT NULL DROP TABLE dbo.IC_Migracija;
GO

IF TYPE_ID('dbo.IC_SlikaTip')       IS NOT NULL DROP TYPE dbo.IC_SlikaTip;
IF TYPE_ID('dbo.IC_IdTip')          IS NOT NULL DROP TYPE dbo.IC_IdTip;
IF TYPE_ID('dbo.IC_UvozStavkaTip')  IS NOT NULL DROP TYPE dbo.IC_UvozStavkaTip;
IF TYPE_ID('dbo.IC_UvozCenaTip')    IS NOT NULL DROP TYPE dbo.IC_UvozCenaTip;
IF TYPE_ID('dbo.IC_AkcijaTip')      IS NOT NULL DROP TYPE dbo.IC_AkcijaTip;
IF TYPE_ID('dbo.IC_ParametarTip')   IS NOT NULL DROP TYPE dbo.IC_ParametarTip;
IF TYPE_ID('dbo.IC_CenaTip')        IS NOT NULL DROP TYPE dbo.IC_CenaTip;
GO

SET NOEXEC OFF;
GO
