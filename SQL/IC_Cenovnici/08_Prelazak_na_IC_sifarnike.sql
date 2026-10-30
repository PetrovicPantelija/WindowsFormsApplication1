/* =====================================================================
   ZAH-008 Izracun cena - 08: prelazak sa starih tabela Program/Kolekcije
   na sifarnike izracuna cena IC_Program / IC_Kolekcija.
   Baza: Jela_Test. Kompatibilno sa SQL Server 2008 R2.
   Pokrenuti posle 01_IC_Sifarnici.sql, a PRE 02, 03 i 04.

   Do 30.10. su IC_ cenovnici koristili tabele Program i Kolekcije (iste
   koje koriste planiranje i pakovanje). Ova skripta:
     1. puni IC_Program i IC_Kolekcija iz jela_products;
     2. stari ProgramID / KolekcijaID u IC_CenovnikStavka i
        IC_CenovnikParametar prevodi u nove ID-jeve:
          a) po nazivima iz jela_products za maticne podatke tog ID-a
             (koji novi ID ima vecina redova),
          b) ako to ne moze - po nazivu iz stare tabele,
          c) ako ni to ne moze - pravi "Nepoznat program / Nepoznata
             kolekcija (stari ID n)", da se nijedan red ne izgubi;
     3. uklanja FK sa IC_ tabela prema starim tabelama (02 ih pravi prema
        novim);
     4. sa tabela Program i Kolekcije uklanja ogranicenja koja je dodala
        ranija verzija skripte 01 (UQ_Program_Naziv, UQ_Kolekcije,
        UQ_Program_ID, UQ_Kolekcije_ID, FK_Kolekcije_Program). PK se ne dira.
   Podaci u tabelama Program, Kolekcije i KolekcijePripadnost se NE menjaju.
   Prevodjenje se radi samo jednom (zapis u IC_Migracija); ponovno
   pokretanje ne radi nista.
   ===================================================================== */
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

IF OBJECT_ID('dbo.IC_Program', 'U') IS NULL OR OBJECT_ID('dbo.IC_Kolekcija', 'U') IS NULL OR OBJECT_ID('dbo.IC_Migracija', 'U') IS NULL
BEGIN
    RAISERROR('Prvo pokrenuti 01_IC_Sifarnici.sql.', 16, 1);
    RETURN;
END

IF EXISTS (SELECT 1 FROM dbo.IC_Migracija WHERE Naziv = '08_IC_sifarnici')
BEGIN
    PRINT '08: prelazak je vec uradjen - preskace se.';
    RETURN;
END

DECLARE @sql nvarchar(max), @fk sysname, @tab sysname;

BEGIN TRY
    BEGIN TRAN;

    /* ---------- 1. sifarnici iz jela_products ---------- */
    INSERT INTO dbo.IC_Program (Naziv)
    SELECT DISTINCT LTRIM(RTRIM(p.NazivRobneGrupe))
    FROM dbo.jela_products p
    WHERE LTRIM(RTRIM(ISNULL(p.NazivRobneGrupe, ''))) <> ''
      AND NOT EXISTS (SELECT 1 FROM dbo.IC_Program g WHERE g.Naziv = LTRIM(RTRIM(p.NazivRobneGrupe)));

    INSERT INTO dbo.IC_Kolekcija (Naziv, ProgramID)
    SELECT DISTINCT LTRIM(RTRIM(p.NazivGrupe)), g.ID
    FROM dbo.jela_products p
    JOIN dbo.IC_Program g ON g.Naziv = LTRIM(RTRIM(p.NazivRobneGrupe))
    WHERE LTRIM(RTRIM(ISNULL(p.NazivGrupe, ''))) <> ''
      AND NOT EXISTS (SELECT 1 FROM dbo.IC_Kolekcija k WHERE k.Naziv = LTRIM(RTRIM(p.NazivGrupe)) AND k.ProgramID = g.ID);
    DECLARE @bp int, @bk int;
    SELECT @bp = COUNT(*) FROM dbo.IC_Program;
    SELECT @bk = COUNT(*) FROM dbo.IC_Kolekcija;
    PRINT '08: IC_Program = ' + CAST(@bp AS varchar(10)) + ', IC_Kolekcija = ' + CAST(@bk AS varchar(10));

    /* ---------- 2. prevodjenje starih ID-jeva (samo ako postoje IC_ cenovnici) ---------- */
    IF OBJECT_ID('dbo.IC_CenovnikStavka', 'U') IS NOT NULL
    BEGIN
        -- novi program / kolekcija po maticnom podatku (nazivi iz jela_products)
        SELECT m.LargoProizvodID, g.ID AS NoviProgramID, k.ID AS NovaKolekcijaID
        INTO #N
        FROM (SELECT l.LargoProizvodID,
                     MIN(LTRIM(RTRIM(p.NazivRobneGrupe))) AS Program,
                     MIN(LTRIM(RTRIM(p.NazivGrupe)))      AS Kolekcija
              FROM dbo.RkShippingLinkWebsLargo l
              JOIN dbo.jela_products p ON p.BarCode = RTRIM(l.WebsGTIN)
              GROUP BY l.LargoProizvodID) m
        LEFT JOIN dbo.IC_Program g   ON g.Naziv = m.Program
        LEFT JOIN dbo.IC_Kolekcija k ON k.Naziv = m.Kolekcija AND k.ProgramID = g.ID;

        CREATE TABLE #MapP (StariID int PRIMARY KEY, NoviID int NULL, Nacin varchar(30) NULL);
        CREATE TABLE #MapK (StariID int PRIMARY KEY, NoviID int NULL, Nacin varchar(30) NULL);

        INSERT INTO #MapP (StariID) SELECT DISTINCT ProgramID FROM dbo.IC_CenovnikStavka WHERE ProgramID IS NOT NULL
        UNION SELECT DISTINCT ProgramID FROM dbo.IC_CenovnikParametar WHERE ProgramID IS NOT NULL;
        INSERT INTO #MapK (StariID) SELECT DISTINCT KolekcijaID FROM dbo.IC_CenovnikStavka WHERE KolekcijaID IS NOT NULL
        UNION SELECT DISTINCT KolekcijaID FROM dbo.IC_CenovnikParametar WHERE KolekcijaID IS NOT NULL;

        -- a) vecina redova sa tim starim ID-jem
        UPDATE m SET NoviID = x.NoviID, Nacin = 'po proizvodima'
        FROM #MapP m
        CROSS APPLY (SELECT TOP (1) n.NoviProgramID AS NoviID
                     FROM dbo.IC_CenovnikStavka s JOIN #N n ON n.LargoProizvodID = s.LargoProizvodID
                     WHERE s.ProgramID = m.StariID AND n.NoviProgramID IS NOT NULL
                     GROUP BY n.NoviProgramID ORDER BY COUNT(*) DESC) x;
        UPDATE m SET NoviID = x.NoviID, Nacin = 'po proizvodima'
        FROM #MapK m
        CROSS APPLY (SELECT TOP (1) n.NovaKolekcijaID AS NoviID
                     FROM dbo.IC_CenovnikStavka s JOIN #N n ON n.LargoProizvodID = s.LargoProizvodID
                     WHERE s.KolekcijaID = m.StariID AND n.NovaKolekcijaID IS NOT NULL
                     GROUP BY n.NovaKolekcijaID ORDER BY COUNT(*) DESC) x;

        -- b) po nazivu iz starih tabela (ako jos postoje redovi sa tim ID-jem)
        IF OBJECT_ID('dbo.Program', 'U') IS NOT NULL
        BEGIN
            SET @sql = N'
                INSERT INTO dbo.IC_Program (Naziv)
                SELECT DISTINCT LTRIM(RTRIM(o.Naziv)) FROM #MapP m JOIN dbo.Program o ON o.ID = m.StariID
                WHERE m.NoviID IS NULL AND LTRIM(RTRIM(ISNULL(o.Naziv, ''''))) <> ''''
                  AND NOT EXISTS (SELECT 1 FROM dbo.IC_Program g WHERE g.Naziv = LTRIM(RTRIM(o.Naziv)));
                UPDATE m SET NoviID = g.ID, Nacin = ''po nazivu stare tabele''
                FROM #MapP m JOIN dbo.Program o ON o.ID = m.StariID JOIN dbo.IC_Program g ON g.Naziv = LTRIM(RTRIM(o.Naziv))
                WHERE m.NoviID IS NULL;';
            EXEC (@sql);
        END
        IF OBJECT_ID('dbo.Kolekcije', 'U') IS NOT NULL
        BEGIN
            -- program stare kolekcije: preveden stari ProgramID, inace naziv starog programa
            SET @sql = N'
                SELECT m.StariID, LTRIM(RTRIM(o.Naziv)) AS Naziv,
                       COALESCE(mp.NoviID, (SELECT TOP (1) g.ID FROM dbo.Program op JOIN dbo.IC_Program g ON g.Naziv = LTRIM(RTRIM(op.Naziv)) WHERE op.ID = o.ProgramID)) AS NoviProgramID
                INTO #KS
                FROM #MapK m JOIN dbo.Kolekcije o ON o.ID = m.StariID
                LEFT JOIN #MapP mp ON mp.StariID = o.ProgramID
                WHERE m.NoviID IS NULL AND LTRIM(RTRIM(ISNULL(o.Naziv, ''''))) <> '''';
                INSERT INTO dbo.IC_Kolekcija (Naziv, ProgramID)
                SELECT DISTINCT Naziv, NoviProgramID FROM #KS
                WHERE NoviProgramID IS NOT NULL
                  AND NOT EXISTS (SELECT 1 FROM dbo.IC_Kolekcija k WHERE k.Naziv = #KS.Naziv AND k.ProgramID = #KS.NoviProgramID);
                UPDATE m SET NoviID = k.ID, Nacin = ''po nazivu stare tabele''
                FROM #MapK m JOIN #KS ON #KS.StariID = m.StariID
                JOIN dbo.IC_Kolekcija k ON k.Naziv = #KS.Naziv AND k.ProgramID = #KS.NoviProgramID
                WHERE m.NoviID IS NULL;';
            EXEC (@sql);
        END

        -- c) nepoznato: poseban red da se nista ne izgubi
        IF EXISTS (SELECT 1 FROM #MapP WHERE NoviID IS NULL)
        BEGIN
            INSERT INTO dbo.IC_Program (Naziv)
            SELECT N'Nepoznat program (stari ID ' + CAST(StariID AS nvarchar(10)) + N')' FROM #MapP m
            WHERE NoviID IS NULL
              AND NOT EXISTS (SELECT 1 FROM dbo.IC_Program g WHERE g.Naziv = N'Nepoznat program (stari ID ' + CAST(m.StariID AS nvarchar(10)) + N')');
            UPDATE m SET NoviID = g.ID, Nacin = 'nepoznat'
            FROM #MapP m JOIN dbo.IC_Program g ON g.Naziv = N'Nepoznat program (stari ID ' + CAST(m.StariID AS nvarchar(10)) + N')'
            WHERE m.NoviID IS NULL;
        END
        IF EXISTS (SELECT 1 FROM #MapK WHERE NoviID IS NULL)
        BEGIN
            -- program nepoznate kolekcije = preveden program njenih redova u cenovniku
            DECLARE @NepoznatP int;
            IF NOT EXISTS (SELECT 1 FROM dbo.IC_Program WHERE Naziv = N'Nepoznat program')
                INSERT INTO dbo.IC_Program (Naziv) VALUES (N'Nepoznat program');
            SELECT @NepoznatP = ID FROM dbo.IC_Program WHERE Naziv = N'Nepoznat program';

            SELECT m.StariID,
                   N'Nepoznata kolekcija (stari ID ' + CAST(m.StariID AS nvarchar(10)) + N')' AS Naziv,
                   ISNULL((SELECT TOP (1) mp.NoviID FROM dbo.IC_CenovnikStavka s JOIN #MapP mp ON mp.StariID = s.ProgramID
                           WHERE s.KolekcijaID = m.StariID GROUP BY mp.NoviID ORDER BY COUNT(*) DESC), @NepoznatP) AS ProgramID
            INTO #KN
            FROM #MapK m WHERE m.NoviID IS NULL;
            INSERT INTO dbo.IC_Kolekcija (Naziv, ProgramID)
            SELECT Naziv, ProgramID FROM #KN
            WHERE NOT EXISTS (SELECT 1 FROM dbo.IC_Kolekcija k WHERE k.Naziv = #KN.Naziv AND k.ProgramID = #KN.ProgramID);
            UPDATE m SET NoviID = k.ID, Nacin = 'nepoznata'
            FROM #MapK m JOIN #KN ON #KN.StariID = m.StariID
            JOIN dbo.IC_Kolekcija k ON k.Naziv = #KN.Naziv AND k.ProgramID = #KN.ProgramID;
        END

        /* ---------- 3. FK prema starim tabelama se uklanjaju, pa se ID-jevi prevode ---------- */
        DECLARE fk CURSOR LOCAL FAST_FORWARD FOR
            SELECT f.name, OBJECT_NAME(f.parent_object_id)
            FROM sys.foreign_keys f
            WHERE f.parent_object_id IN (OBJECT_ID('dbo.IC_CenovnikStavka'), OBJECT_ID('dbo.IC_CenovnikParametar'))
              AND f.referenced_object_id IN (OBJECT_ID('dbo.Program'), OBJECT_ID('dbo.Kolekcije'));
        OPEN fk; FETCH NEXT FROM fk INTO @fk, @tab;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            SET @sql = N'ALTER TABLE dbo.' + QUOTENAME(@tab) + N' DROP CONSTRAINT ' + QUOTENAME(@fk);
            EXEC (@sql);
            PRINT '08: uklonjen ' + @fk + ' (pokazivao na staru tabelu).';
            FETCH NEXT FROM fk INTO @fk, @tab;
        END
        CLOSE fk; DEALLOCATE fk;

        UPDATE s SET ProgramID = mp.NoviID, KolekcijaID = mk.NoviID
        FROM dbo.IC_CenovnikStavka s
        JOIN #MapP mp ON mp.StariID = s.ProgramID
        JOIN #MapK mk ON mk.StariID = s.KolekcijaID;
        PRINT '08: prevedeno redova u IC_CenovnikStavka: ' + CAST(@@ROWCOUNT AS varchar(10));

        UPDATE p SET ProgramID = mp.NoviID
        FROM dbo.IC_CenovnikParametar p JOIN #MapP mp ON mp.StariID = p.ProgramID;
        UPDATE p SET KolekcijaID = mk.NoviID
        FROM dbo.IC_CenovnikParametar p JOIN #MapK mk ON mk.StariID = p.KolekcijaID;

        -- izvestaj prevodjenja
        SELECT 'Program' AS Sifarnik, m.StariID, m.NoviID, g.Naziv AS NoviNaziv, m.Nacin
        FROM #MapP m LEFT JOIN dbo.IC_Program g ON g.ID = m.NoviID
        UNION ALL
        SELECT 'Kolekcija', m.StariID, m.NoviID, k.Naziv + N' (' + g.Naziv + N')', m.Nacin
        FROM #MapK m LEFT JOIN dbo.IC_Kolekcija k ON k.ID = m.NoviID LEFT JOIN dbo.IC_Program g ON g.ID = k.ProgramID
        ORDER BY 1, 2;
    END
    ELSE
        PRINT '08: IC_ cenovnici jos ne postoje - nema sta da se prevodi.';

    /* ---------- 4. ogranicenja koja je ranija skripta 01 dodala starim tabelama ---------- */
    DECLARE @o TABLE (Rb int IDENTITY, Tabela sysname, Ime sysname);
    INSERT INTO @o (Tabela, Ime) VALUES ('Kolekcije', 'FK_Kolekcije_Program'), ('Program', 'UQ_Program_Naziv'),
                                        ('Kolekcije', 'UQ_Kolekcije'), ('Program', 'UQ_Program_ID'), ('Kolekcije', 'UQ_Kolekcije_ID');
    DECLARE @i int = 1, @n int;
    SELECT @n = COUNT(*) FROM @o;
    WHILE @i <= @n
    BEGIN
        SELECT @tab = Tabela, @fk = Ime FROM @o WHERE Rb = @i;
        IF EXISTS (SELECT 1 FROM sys.objects WHERE name = @fk AND parent_object_id = OBJECT_ID('dbo.' + @tab))
        BEGIN
            SET @sql = N'ALTER TABLE dbo.' + QUOTENAME(@tab) + N' DROP CONSTRAINT ' + QUOTENAME(@fk);
            EXEC (@sql);
            PRINT '08: sa dbo.' + @tab + ' uklonjen ' + @fk + '.';
        END
        SET @i = @i + 1;
    END

    INSERT INTO dbo.IC_Migracija (Naziv) VALUES ('08_IC_sifarnici');
    COMMIT;
    PRINT '08: prelazak zavrsen. Sada pokrenuti 02, 03 i 04.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK;
    DECLARE @g nvarchar(2048) = ERROR_MESSAGE();
    RAISERROR('08: greska, nista nije promenjeno: %s', 16, 1, @g);
END CATCH
GO
