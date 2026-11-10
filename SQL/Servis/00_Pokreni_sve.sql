/* =====================================================================
   ZAH-010 Servis - pokretanje svih skripti redom
   U SSMS: Query -> SQLCMD Mode, pa Execute. Ili iz komandne linije:
     sqlcmd -S <server> -d Jela_Test -E -f 65001 -i 00_Pokreni_sve.sql
   (pokrenuti iz ovog foldera, zbog :r putanja; -f 65001 zbog slova š, č, ć, ž, đ)

   Sve skripte smeju da se pokrenu vise puta zaredom.
   Postojece tabele Jele se ne menjaju; Servis ih samo cita
   (Partnerji, KorisnikGrupa, Grupe) i dodaje stavke u Forme (07).
   08 (test podaci) se pokrece samo na Jela_Test.
   ===================================================================== */
:on error exit
USE Jela_Test;
GO
:r .\01_SRV_Osnova.sql
:r .\02_SRV_Tabele.sql
:r .\03_SRV_Pogledi.sql
:r .\04_SRV_Procedure_Sifarnici.sql
:r .\05_SRV_Procedure_Magacin.sql
:r .\06_SRV_Procedure_Servis.sql
:r .\07_Forme_prava.sql
GO
:r .\08_Test_podaci.sql
