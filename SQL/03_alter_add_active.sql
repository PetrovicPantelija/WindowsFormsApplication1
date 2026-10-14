-- Dodavanje polja Active u dbo.jela_products (SQL Server / T-SQL)
-- Bezbedno za ponovno pokretanje: ako kolona vec postoji, ne radi nista.
-- Postojeci zapisi dobijaju Active = 1.

IF COL_LENGTH(N'dbo.jela_products', N'Active') IS NULL
BEGIN
    ALTER TABLE dbo.jela_products
        ADD [Active] BIT NOT NULL
            CONSTRAINT DF_jela_products_Active DEFAULT (1);
END
GO

-- provera
SELECT [Active], COUNT(*) AS broj_redova
FROM dbo.jela_products
GROUP BY [Active];
GO
