/* =====================================================================
   ZAH-010 Servis - 07: forme modula u sistemu prava
   Upisuje forme u dbo.Forme (procedura InsertForme, kao frmForme).
   Forme.Code = ime klase (frmMainServis za meni MainServis).
   Grupe im dodeljuje administrator: Administracija -> Dodeli pravo.
   Predlog grupa (potvrdjuje Jela, OP-J-03):
     Prijavilac kvara     frmSrvKvarovi (Upis)
     Magacioner delova    frmSrvZalihe, frmSrvDokumenta (Upis, Izmena), frmSrvPopis (Upis), frmSrvObavestenja
     Nabavka              frmSrvNabavka (Upis, Izmena)
     Šef tehničke službe  sve forme; odobrava popis, korekcije i nivo 1
     Tehnički direktor    pregled svih; odobrava nivo 2
     Direktor             pregled svih; odobrava nivo 3
     Administrator        frmSrvVrste, frmSrvSredstva, frmSrvPlanovi, frmSrvGrupeDelova
   Svi korisnici modula moraju imati i frmMainServis.
   Skripta se sme pokretati vise puta.
   ===================================================================== */
SET NOCOUNT ON;

DECLARE @f TABLE (Code nvarchar(100), Naziv nvarchar(100), Redosled int);
INSERT INTO @f VALUES
 (N'frmMainServis',        N'MainServis-frmMainServis (meni Servis)', 1),
 (N'frmSrvZalihe',         N'MainServis-frmSrvZalihe (Zalihe i šifarnik delova)', 2),
 (N'frmSrvDokumenta',      N'MainServis-frmSrvDokumenta (Dokumenta delova)', 3),
 (N'frmSrvNabavka',        N'MainServis-frmSrvNabavka (Potrebe za nabavkom)', 4),
 (N'frmSrvPopis',          N'MainServis-frmSrvPopis (Popis zaliha)', 5),
 (N'frmSrvKorekcije',      N'MainServis-frmSrvKorekcije (Korekcije zaliha)', 6),
 (N'frmSrvObavestenja',    N'MainServis-frmSrvObavestenja (Obaveštenja)', 7),
 (N'frmSrvNalozi',         N'MainServis-frmSrvNalozi (Servisni nalozi)', 8),
 (N'frmSrvKvarovi',        N'MainServis-frmSrvKvarovi (Prijava kvarova)', 9),
 (N'frmSrvSpoljniServis',  N'MainServis-frmSrvSpoljniServis (Spoljni servis)', 10),
 (N'frmSrvVrste',          N'MainServis-frmSrvVrste (Vrste sredstava)', 11),
 (N'frmSrvSredstva',       N'MainServis-frmSrvSredstva (Sredstva)', 12),
 (N'frmSrvPlanovi',        N'MainServis-frmSrvPlanovi (Plan održavanja)', 13),
 (N'frmSrvGrupeDelova',    N'MainServis-frmSrvGrupeDelova (Grupe delova)', 14),
 (N'frmSrvStanje',         N'MainServis-frmSrvStanje (Stanje sredstava)', 15);

DECLARE @Code nvarchar(100), @Naziv nvarchar(100);
DECLARE c CURSOR LOCAL FAST_FORWARD FOR SELECT Code, Naziv FROM @f ORDER BY Redosled;
OPEN c;
FETCH NEXT FROM c INTO @Code, @Naziv;
WHILE @@FETCH_STATUS = 0
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.Forme WHERE RTRIM(Code) = @Code)
        EXEC dbo.InsertForme @Naziv = @Naziv, @Code = @Code;
    FETCH NEXT FROM c INTO @Code, @Naziv;
END
CLOSE c; DEALLOCATE c;

SELECT fo.IdForme, fo.Naziv, fo.Code
FROM dbo.Forme fo JOIN @f x ON x.Code = RTRIM(fo.Code)
ORDER BY x.Redosled;
