/* =====================================================================
   ZAH-008 Izracun cena - pokretanje svih skripti redom
   U SSMS: Query -> SQLCMD Mode, pa Execute. Ili iz komandne linije:
     sqlcmd -S <server> -d Jela_Test -E -i 00_Pokreni_sve.sql
   (pokrenuti iz ovog foldera, zbog :r putanja)

   08 je jednokratni prelazak sa starih tabela Program/Kolekcije na
   sifarnike izracuna cena; na novoj bazi ne radi nista, a na postojecoj
   se izvrsava samo jednom.
   ===================================================================== */
:on error exit
USE Jela_Test;
GO
:r .\01_IC_Sifarnici.sql
:r .\08_Prelazak_na_IC_sifarnike.sql
:r .\02_IC_Tabele.sql
:r .\03_IC_Funkcije_Pogledi.sql
:r .\04_IC_Procedure.sql
:r .\07_Forme_prava.sql
GO
EXEC dbo.IC_OsveziProgrameIKolekcije;
GO
:r .\06_Test_obracuna.sql
