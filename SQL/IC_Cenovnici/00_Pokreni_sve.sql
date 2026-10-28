/* =====================================================================
   ZAH-008 Izracun cena - pokretanje svih skripti redom
   U SSMS: Query -> SQLCMD Mode, pa Execute. Ili iz komandne linije:
     sqlcmd -S <server> -d Jela_Test -E -i 00_Pokreni_sve.sql
   (pokrenuti iz ovog foldera, zbog :r putanja)
   ===================================================================== */
:on error exit
USE Jela_Test;
GO
:r .\01_Program_Kolekcije.sql
:r .\02_IC_Tabele.sql
:r .\03_IC_Funkcije_Pogledi.sql
:r .\04_IC_Procedure.sql
GO
EXEC dbo.IC_OsveziProgrameIKolekcije;
GO
:r .\06_Test_obracuna.sql
