/* =====================================================================
   ZAH-010 Servis - 99: uklanjanje svih SRV_ objekata
   SAMO ZA TEST BAZU. Brise sve podatke modula Servis!
   Postojece tabele Jele se ne diraju; stavke formi u dbo.Forme ostaju
   (brisu se rucno u Administracija -> Forme ako treba).
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

-- procedure i funkcije
DECLARE @sql nvarchar(max) = N'';
SELECT @sql = @sql + N'DROP PROCEDURE dbo.' + QUOTENAME(name) + N';' + CHAR(10)
FROM sys.procedures WHERE name LIKE N'SRV[_]%';
SELECT @sql = @sql + N'DROP VIEW dbo.' + QUOTENAME(name) + N';' + CHAR(10)
FROM sys.views WHERE name LIKE N'SRV[_]%';
EXEC sp_executesql @sql;
GO
IF OBJECT_ID('dbo.SRV_NivoOdobrenja', 'FN') IS NOT NULL DROP FUNCTION dbo.SRV_NivoOdobrenja;
IF OBJECT_ID('dbo.SRV_MozeDaOdobri', 'FN') IS NOT NULL DROP FUNCTION dbo.SRV_MozeDaOdobri;
IF OBJECT_ID('dbo.SRV_DatumDokumenta', 'FN') IS NOT NULL DROP FUNCTION dbo.SRV_DatumDokumenta;
IF OBJECT_ID('dbo.SRV_Broj', 'FN') IS NOT NULL DROP FUNCTION dbo.SRV_Broj;
IF OBJECT_ID('dbo.SRV_Pod', 'FN') IS NOT NULL DROP FUNCTION dbo.SRV_Pod;
GO

-- tabele (redosled zbog stranih kljuceva)
IF OBJECT_ID('dbo.SRV_DeoObavestenje', 'U')        IS NOT NULL DROP TABLE dbo.SRV_DeoObavestenje;
IF OBJECT_ID('dbo.SRV_PlanSredstvoPrag', 'U')      IS NOT NULL DROP TABLE dbo.SRV_PlanSredstvoPrag;
IF OBJECT_ID('dbo.SRV_DeoKretanje', 'U')           IS NOT NULL DROP TABLE dbo.SRV_DeoKretanje;
IF OBJECT_ID('dbo.SRV_Korekcija', 'U')             IS NOT NULL DROP TABLE dbo.SRV_Korekcija;
IF OBJECT_ID('dbo.SRV_PopisStavka', 'U')           IS NOT NULL DROP TABLE dbo.SRV_PopisStavka;
IF OBJECT_ID('dbo.SRV_Popis', 'U')                 IS NOT NULL DROP TABLE dbo.SRV_Popis;
IF OBJECT_ID('dbo.SRV_NarudzbenicaStavka', 'U')    IS NOT NULL DROP TABLE dbo.SRV_NarudzbenicaStavka;
IF OBJECT_ID('dbo.SRV_Narudzbenica', 'U')          IS NOT NULL DROP TABLE dbo.SRV_Narudzbenica;
IF OBJECT_ID('dbo.SRV_SpoljniServisIstorija', 'U') IS NOT NULL DROP TABLE dbo.SRV_SpoljniServisIstorija;
IF OBJECT_ID('dbo.SRV_SpoljniServisZahtev', 'U')   IS NOT NULL DROP TABLE dbo.SRV_SpoljniServisZahtev;
IF OBJECT_ID('dbo.SRV_NalogDeo', 'U')              IS NOT NULL DROP TABLE dbo.SRV_NalogDeo;
IF OBJECT_ID('dbo.SRV_NalogRad', 'U')              IS NOT NULL DROP TABLE dbo.SRV_NalogRad;
IF OBJECT_ID('dbo.SRV_Nalog', 'U')                 IS NOT NULL DROP TABLE dbo.SRV_Nalog;
IF OBJECT_ID('dbo.SRV_KvarPrilog', 'U')            IS NOT NULL DROP TABLE dbo.SRV_KvarPrilog;
IF OBJECT_ID('dbo.SRV_Kvar', 'U')                  IS NOT NULL DROP TABLE dbo.SRV_Kvar;
IF OBJECT_ID('dbo.SRV_PlanNormativ', 'U')          IS NOT NULL DROP TABLE dbo.SRV_PlanNormativ;
IF OBJECT_ID('dbo.SRV_PlanOdrzavanja', 'U')        IS NOT NULL DROP TABLE dbo.SRV_PlanOdrzavanja;
IF OBJECT_ID('dbo.SRV_DeoPrimenljivost', 'U')      IS NOT NULL DROP TABLE dbo.SRV_DeoPrimenljivost;
IF OBJECT_ID('dbo.SRV_Deo', 'U')                   IS NOT NULL DROP TABLE dbo.SRV_Deo;
IF OBJECT_ID('dbo.SRV_DeoGrupa', 'U')              IS NOT NULL DROP TABLE dbo.SRV_DeoGrupa;
IF OBJECT_ID('dbo.SRV_SredstvoDokument', 'U')      IS NOT NULL DROP TABLE dbo.SRV_SredstvoDokument;
IF OBJECT_ID('dbo.SRV_SredstvoBrojilo', 'U')       IS NOT NULL DROP TABLE dbo.SRV_SredstvoBrojilo;
IF OBJECT_ID('dbo.SRV_SredstvoPolje', 'U')         IS NOT NULL DROP TABLE dbo.SRV_SredstvoPolje;
IF OBJECT_ID('dbo.SRV_Sredstvo', 'U')              IS NOT NULL DROP TABLE dbo.SRV_Sredstvo;
IF OBJECT_ID('dbo.SRV_VrstaSredstvaDokument', 'U') IS NOT NULL DROP TABLE dbo.SRV_VrstaSredstvaDokument;
IF OBJECT_ID('dbo.SRV_VrstaSredstvaPolje', 'U')    IS NOT NULL DROP TABLE dbo.SRV_VrstaSredstvaPolje;
IF OBJECT_ID('dbo.SRV_VrstaSredstvaBrojilo', 'U')  IS NOT NULL DROP TABLE dbo.SRV_VrstaSredstvaBrojilo;
IF OBJECT_ID('dbo.SRV_VrstaSredstva', 'U')         IS NOT NULL DROP TABLE dbo.SRV_VrstaSredstva;
IF OBJECT_ID('dbo.SRV_Log', 'U')                   IS NOT NULL DROP TABLE dbo.SRV_Log;
IF OBJECT_ID('dbo.SRV_Brojac', 'U')                IS NOT NULL DROP TABLE dbo.SRV_Brojac;
IF OBJECT_ID('dbo.SRV_Podesavanje', 'U')           IS NOT NULL DROP TABLE dbo.SRV_Podesavanje;
GO

IF TYPE_ID('dbo.SRV_StavkaTip')        IS NOT NULL DROP TYPE dbo.SRV_StavkaTip;
IF TYPE_ID('dbo.SRV_IdTip')            IS NOT NULL DROP TYPE dbo.SRV_IdTip;
IF TYPE_ID('dbo.SRV_TekstTip')         IS NOT NULL DROP TYPE dbo.SRV_TekstTip;
IF TYPE_ID('dbo.SRV_PoljeVrednostTip') IS NOT NULL DROP TYPE dbo.SRV_PoljeVrednostTip;
GO
SET NOEXEC OFF;
GO
