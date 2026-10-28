/* =====================================================================
   ZAH-008 Izracun cena - 01: Program i Kolekcije
   Baza: Jela_Test. Kompatibilno sa SQL Server 2008 R2.

   Program i Kolekcije VEC POSTOJE i koriste ih KalkulatorStavke,
   planovi i pakovanja (frmProgram, frmKolekcije, PripadnostKP...).
   Skripta zato NE brise i NE pravi ih ponovo:
     - ako tabela ne postoji, pravi je po specifikaciji;
     - ako postoji, obezbedjuje kljuc na koloni ID (PK, ili UNIQUE ako
       PK vec postoji na drugoj koloni), jer na njega pokazuju IC_ tabele;
     - dodaje UNIQUE na nazive i FK Kolekcije -> Program kad podaci dozvoljavaju.
   Sporni redovi se ispisuju; na kraju je tabela "Stanje kljuceva".
   Skripta se sme pokretati vise puta.
   ===================================================================== */
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET NOCOUNT ON;
GO

IF OBJECT_ID('dbo.Program', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Program (
        ID    int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Program PRIMARY KEY,
        Naziv nvarchar(100) NOT NULL CONSTRAINT UQ_Program_Naziv UNIQUE
    );
    PRINT 'Napravljena tabela dbo.Program.';
END
GO

IF OBJECT_ID('dbo.Kolekcije', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Kolekcije (
        ID        int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Kolekcije PRIMARY KEY,
        Naziv     nvarchar(100) NOT NULL,
        ProgramID int NOT NULL CONSTRAINT FK_Kolekcije_Program REFERENCES dbo.Program(ID),
        CONSTRAINT UQ_Kolekcije UNIQUE (ProgramID, Naziv)
    );
    PRINT 'Napravljena tabela dbo.Kolekcije.';
END
GO

/* ---------- Kljuc na ID za obe tabele ---------- */
DECLARE @Tabele TABLE (Rb int, Tabela sysname);
INSERT INTO @Tabele VALUES (1, 'Program'), (2, 'Kolekcije');

DECLARE @Rb int = 1, @T sysname, @Obj int, @Tip sysname, @Nullable bit, @Sql nvarchar(max), @Broj int;

WHILE @Rb <= 2
BEGIN
    SELECT @T = Tabela FROM @Tabele WHERE Rb = @Rb;
    SET @Obj = OBJECT_ID('dbo.' + @T);

    SELECT @Tip = TYPE_NAME(c.user_type_id), @Nullable = c.is_nullable
    FROM sys.columns c WHERE c.object_id = @Obj AND c.name = 'ID';

    IF EXISTS (SELECT 1 FROM sys.indexes i
               WHERE i.object_id = @Obj AND i.is_unique = 1
                 AND (SELECT COUNT(*) FROM sys.index_columns x
                      WHERE x.object_id = i.object_id AND x.index_id = i.index_id AND x.key_ordinal > 0) = 1
                 AND EXISTS (SELECT 1 FROM sys.index_columns x
                             JOIN sys.columns c ON c.object_id = x.object_id AND c.column_id = x.column_id
                             WHERE x.object_id = i.object_id AND x.index_id = i.index_id
                               AND x.key_ordinal > 0 AND c.name = 'ID'))
        PRINT 'dbo.' + @T + ': kljuc na ID postoji.';
    ELSE IF @Tip IS NULL
        PRINT 'PAZNJA: dbo.' + @T + ' nema kolonu ID.';
    ELSE IF @Tip <> 'int'
        PRINT 'PAZNJA: dbo.' + @T + '.ID je tipa ' + @Tip + ', a IC_ tabele ocekuju int. Kljuc nije dodat.';
    ELSE
    BEGIN
        SET @Sql = N'SELECT @n = COUNT(*) FROM (SELECT ID FROM dbo.' + QUOTENAME(@T)
                 + N' GROUP BY ID HAVING COUNT(*) > 1 OR ID IS NULL) d';
        EXEC sp_executesql @Sql, N'@n int OUTPUT', @n = @Broj OUTPUT;

        IF @Broj > 0
        BEGIN
            PRINT 'PAZNJA: dbo.' + @T + ' ima duple ili prazne ID - kljuc nije dodat. Sporni ID su u rezultatu.';
            SET @Sql = N'SELECT ''' + @T + N''' AS Tabela, ID, COUNT(*) AS Broj FROM dbo.' + QUOTENAME(@T)
                     + N' GROUP BY ID HAVING COUNT(*) > 1 OR ID IS NULL';
            EXEC (@Sql);
        END
        ELSE
        BEGIN
            IF @Nullable = 1
            BEGIN
                SET @Sql = N'ALTER TABLE dbo.' + QUOTENAME(@T) + N' ALTER COLUMN ID int NOT NULL';
                EXEC (@Sql);
                PRINT 'dbo.' + @T + '.ID je postavljen na NOT NULL.';
            END

            IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE parent_object_id = @Obj AND type = 'PK')
            BEGIN
                SET @Sql = N'ALTER TABLE dbo.' + QUOTENAME(@T) + N' ADD CONSTRAINT ' + QUOTENAME('PK_' + @T) + N' PRIMARY KEY (ID)';
                EXEC (@Sql);
                PRINT 'Dodat PK_' + @T + '.';
            END
            ELSE
            BEGIN
                -- PK vec postoji na drugoj koloni: UNIQUE na ID je dovoljan za FK
                SET @Sql = N'ALTER TABLE dbo.' + QUOTENAME(@T) + N' ADD CONSTRAINT ' + QUOTENAME('UQ_' + @T + '_ID') + N' UNIQUE (ID)';
                EXEC (@Sql);
                PRINT 'dbo.' + @T + ' ima PK na drugoj koloni - dodat UQ_' + @T + '_ID.';
            END
        END
    END

    SET @Rb = @Rb + 1;
END
GO

/* ---------- Program: UNIQUE (Naziv) ---------- */
IF NOT EXISTS (SELECT 1
               FROM sys.indexes i
               JOIN sys.index_columns ic ON ic.object_id = i.object_id AND ic.index_id = i.index_id
               JOIN sys.columns c        ON c.object_id = ic.object_id AND c.column_id = ic.column_id
               WHERE i.object_id = OBJECT_ID('dbo.Program') AND i.is_unique = 1 AND c.name = 'Naziv'
                 AND (SELECT COUNT(*) FROM sys.index_columns x
                      WHERE x.object_id = i.object_id AND x.index_id = i.index_id AND x.key_ordinal > 0) = 1)
BEGIN
    IF EXISTS (SELECT Naziv FROM dbo.Program GROUP BY Naziv HAVING COUNT(*) > 1)
    BEGIN
        PRINT 'PAZNJA: dbo.Program ima duple nazive - UQ_Program_Naziv nije dodat. Dupli su u rezultatu.';
        SELECT 'Program' AS Tabela, Naziv, COUNT(*) AS Broj FROM dbo.Program GROUP BY Naziv HAVING COUNT(*) > 1;
    END
    ELSE IF EXISTS (SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID('dbo.Program') AND name = 'Naziv' AND (max_length = -1 OR max_length > 900))
        PRINT 'PAZNJA: dbo.Program.Naziv je predugacak za UNIQUE - promeniti u nvarchar(100).';
    ELSE
    BEGIN
        ALTER TABLE dbo.Program ADD CONSTRAINT UQ_Program_Naziv UNIQUE (Naziv);
        PRINT 'Dodat UQ_Program_Naziv.';
    END
END
GO

/* ---------- Kolekcije: UNIQUE (ProgramID, Naziv) ---------- */
IF NOT EXISTS (SELECT 1
               FROM sys.indexes i
               WHERE i.object_id = OBJECT_ID('dbo.Kolekcije') AND i.is_unique = 1
                 AND (SELECT COUNT(*) FROM sys.index_columns x
                      WHERE x.object_id = i.object_id AND x.index_id = i.index_id AND x.key_ordinal > 0) = 2
                 AND (SELECT COUNT(*)
                      FROM sys.index_columns x
                      JOIN sys.columns c ON c.object_id = x.object_id AND c.column_id = x.column_id
                      WHERE x.object_id = i.object_id AND x.index_id = i.index_id AND x.key_ordinal > 0
                        AND c.name IN ('ProgramID', 'Naziv')) = 2)
BEGIN
    IF EXISTS (SELECT ProgramID, Naziv FROM dbo.Kolekcije GROUP BY ProgramID, Naziv HAVING COUNT(*) > 1)
    BEGIN
        PRINT 'PAZNJA: dbo.Kolekcije ima duple nazive u istom programu - UQ_Kolekcije nije dodat. Dupli su u rezultatu.';
        SELECT 'Kolekcije' AS Tabela, ProgramID, Naziv, COUNT(*) AS Broj
        FROM dbo.Kolekcije GROUP BY ProgramID, Naziv HAVING COUNT(*) > 1;
    END
    ELSE IF EXISTS (SELECT 1 FROM sys.columns
                    WHERE object_id = OBJECT_ID('dbo.Kolekcije') AND name = 'Naziv' AND (max_length = -1 OR max_length > 800))
        PRINT 'PAZNJA: dbo.Kolekcije.Naziv je predugacak za UNIQUE - promeniti u nvarchar(100).';
    ELSE
    BEGIN
        ALTER TABLE dbo.Kolekcije ADD CONSTRAINT UQ_Kolekcije UNIQUE (ProgramID, Naziv);
        PRINT 'Dodat UQ_Kolekcije.';
    END
END
GO

/* ---------- Kolekcije -> Program: FK ---------- */
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys
               WHERE parent_object_id = OBJECT_ID('dbo.Kolekcije')
                 AND referenced_object_id = OBJECT_ID('dbo.Program'))
BEGIN
    IF NOT EXISTS (SELECT 1 FROM sys.indexes i
                   JOIN sys.index_columns x ON x.object_id = i.object_id AND x.index_id = i.index_id AND x.key_ordinal > 0
                   JOIN sys.columns c ON c.object_id = x.object_id AND c.column_id = x.column_id
                   WHERE i.object_id = OBJECT_ID('dbo.Program') AND i.is_unique = 1 AND c.name = 'ID')
        PRINT 'PAZNJA: dbo.Program nema kljuc na ID - FK_Kolekcije_Program nije dodat.';
    ELSE IF EXISTS (SELECT 1 FROM dbo.Kolekcije k
                    WHERE k.ProgramID IS NOT NULL
                      AND NOT EXISTS (SELECT 1 FROM dbo.Program p WHERE p.ID = k.ProgramID))
    BEGIN
        PRINT 'PAZNJA: kolekcije sa nepostojecim programom - FK_Kolekcije_Program nije dodat. Sporne su u rezultatu.';
        SELECT 'Kolekcije bez programa' AS Tabela, k.ID, k.Naziv, k.ProgramID
        FROM dbo.Kolekcije k
        WHERE k.ProgramID IS NOT NULL
          AND NOT EXISTS (SELECT 1 FROM dbo.Program p WHERE p.ID = k.ProgramID);
    END
    ELSE
    BEGIN
        ALTER TABLE dbo.Kolekcije WITH CHECK
            ADD CONSTRAINT FK_Kolekcije_Program FOREIGN KEY (ProgramID) REFERENCES dbo.Program(ID);
        PRINT 'Dodat FK_Kolekcije_Program.';
    END
END
GO

/* ---------- Stanje kljuceva (poslati ovaj rezultat) ---------- */
SELECT OBJECT_NAME(i.object_id) AS Tabela, i.name AS Indeks,
       CASE WHEN i.is_primary_key = 1 THEN 'PK' WHEN i.is_unique_constraint = 1 THEN 'UQ'
            WHEN i.is_unique = 1 THEN 'UNIQUE indeks' ELSE 'indeks' END AS Vrsta,
       STUFF((SELECT ', ' + c.name
              FROM sys.index_columns x
              JOIN sys.columns c ON c.object_id = x.object_id AND c.column_id = x.column_id
              WHERE x.object_id = i.object_id AND x.index_id = i.index_id AND x.key_ordinal > 0
              ORDER BY x.key_ordinal FOR XML PATH('')), 1, 2, '') AS Kolone
FROM sys.indexes i
WHERE i.object_id IN (OBJECT_ID('dbo.Program'), OBJECT_ID('dbo.Kolekcije')) AND i.type > 0
ORDER BY Tabela, Vrsta;

SELECT 'Program' AS Tabela, c.name AS Kolona, TYPE_NAME(c.user_type_id) AS Tip, c.max_length, c.is_nullable, c.is_identity
FROM sys.columns c WHERE c.object_id = OBJECT_ID('dbo.Program')
UNION ALL
SELECT 'Kolekcije', c.name, TYPE_NAME(c.user_type_id), c.max_length, c.is_nullable, c.is_identity
FROM sys.columns c WHERE c.object_id = OBJECT_ID('dbo.Kolekcije');
GO
