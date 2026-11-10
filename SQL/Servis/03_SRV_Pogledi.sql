/* =====================================================================
   ZAH-010 Servis - 03: pogledi
   Stanje dela se uvek racuna iz SRV_DeoKretanje (nikad rucno upisano).
   Kompatibilno sa SQL Server 2008 R2 (bez SUM() OVER (ORDER BY), LAG/LEAD).
   Pogledi se brisu i prave ponovo (sme vise puta).
   ===================================================================== */
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET NOCOUNT ON;
GO

/* ---------------------------------------------------------------------
   Stanje dela
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_vw_DeoStanje', 'V') IS NOT NULL DROP VIEW dbo.SRV_vw_DeoStanje;
GO
CREATE VIEW dbo.SRV_vw_DeoStanje
AS
SELECT d.ID AS DeoID,
       ISNULL((SELECT SUM(k.Kolicina) FROM dbo.SRV_DeoKretanje k WHERE k.DeoID = d.ID), 0) AS Stanje
FROM dbo.SRV_Deo d;
GO

/* ---------------------------------------------------------------------
   Zalihe: stanje, rezervisano, raspolozivo, naruceno, ceka na nalozima.
   Rezervisano / ceka se racunaju samo za otvorene naloge.
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_vw_Zalihe', 'V') IS NOT NULL DROP VIEW dbo.SRV_vw_Zalihe;
GO
CREATE VIEW dbo.SRV_vw_Zalihe
AS
SELECT x.*,
       x.Stanje - x.Rezervisano AS Raspolozivo,
       CAST(x.Stanje * x.Cena AS decimal(18,2)) AS Vrednost,
       CASE WHEN x.Stanje - x.Rezervisano < x.MinZaliha AND x.Kritican = 1 THEN N'Kritičan'
            WHEN x.Stanje - x.Rezervisano < x.MinZaliha THEN N'Ispod minimuma'
            ELSE N'Na stanju' END AS Status
FROM
(
    SELECT d.ID AS DeoID, d.Sifra, d.Naziv, d.DeoGrupaID, g.Naziv AS Grupa, d.JM,
           s.Stanje,
           ISNULL(r.Rezervisano, 0) AS Rezervisano,
           ISNULL(r.Ceka, 0) AS CekaNaNalozima,
           ISNULL(n.Naruceno, 0) AS Naruceno,
           d.MinZaliha, d.MaxZaliha, d.ProsecnaCena AS Cena, d.Lokacija, d.Kritican,
           d.DobavljacPaSifra, RTRIM(p.PaNaziv) AS Dobavljac, d.RokIsporukeDana, d.Napomena, d.Aktivno
    FROM dbo.SRV_Deo d
    JOIN dbo.SRV_DeoGrupa g ON g.ID = d.DeoGrupaID
    JOIN dbo.SRV_vw_DeoStanje s ON s.DeoID = d.ID
    LEFT JOIN dbo.Partnerji p ON p.PaSifra = d.DobavljacPaSifra
    OUTER APPLY (SELECT SUM(CASE WHEN nd.Status = N'Rezervisano' THEN nd.Kolicina ELSE 0 END) AS Rezervisano,
                        SUM(CASE WHEN nd.Status = N'Nema na stanju' THEN nd.Kolicina ELSE 0 END) AS Ceka
                 FROM dbo.SRV_NalogDeo nd
                 JOIN dbo.SRV_Nalog nl ON nl.ID = nd.NalogID
                 WHERE nd.DeoID = d.ID AND nl.Status NOT IN (N'Završen', N'Otkazan')) r
    OUTER APPLY (SELECT SUM(CASE WHEN st.Kolicina > st.PrimljenoKolicina THEN st.Kolicina - st.PrimljenoKolicina ELSE 0 END) AS Naruceno
                 FROM dbo.SRV_NarudzbenicaStavka st
                 JOIN dbo.SRV_Narudzbenica nr ON nr.ID = st.NarudzbenicaID
                 WHERE st.DeoID = d.ID AND nr.Status IN (N'Poručeno', N'Delimično isporučeno')) n
) x;
GO

/* ---------------------------------------------------------------------
   Predlog nabavke (MD-R06)
   Na spisku: raspolozivo + naruceno < minimum ili postoji kolicina koja ceka.
   Predlog = maksimum - raspolozivo + ceka na nalozima - naruceno (ne manje od 0).
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_vw_PredlogNabavke', 'V') IS NOT NULL DROP VIEW dbo.SRV_vw_PredlogNabavke;
GO
CREATE VIEW dbo.SRV_vw_PredlogNabavke
AS
SELECT y.*,
       CAST(y.Predlog * y.Cena AS decimal(18,2)) AS ProcenaIznosa,
       dbo.SRV_NivoOdobrenja(CAST(y.Predlog * y.Cena AS decimal(18,2))) AS Nivo
FROM
(
    SELECT z.DeoID, z.Sifra, z.Naziv, z.Grupa, z.JM, z.Stanje, z.Rezervisano, z.Raspolozivo, z.MinZaliha, z.MaxZaliha,
           z.CekaNaNalozima, z.Naruceno, z.Cena, z.Kritican, z.DobavljacPaSifra, z.Dobavljac, z.RokIsporukeDana,
           CASE WHEN z.MaxZaliha - z.Raspolozivo + z.CekaNaNalozima - z.Naruceno > 0
                THEN z.MaxZaliha - z.Raspolozivo + z.CekaNaNalozima - z.Naruceno ELSE 0 END AS Predlog
    FROM dbo.SRV_vw_Zalihe z
    WHERE z.Aktivno = 1
      AND (z.Raspolozivo + z.Naruceno < z.MinZaliha OR z.CekaNaNalozima > 0)
) y;
GO

/* ---------------------------------------------------------------------
   Dokumenta delova (izdatnice, prijemi, povrati, pocetno stanje, popis, korekcije)
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_vw_DeoDokument', 'V') IS NOT NULL DROP VIEW dbo.SRV_vw_DeoDokument;
GO
CREATE VIEW dbo.SRV_vw_DeoDokument
AS
SELECT k.DokumentBroj AS Dokument,
       MIN(k.Datum) AS Datum,
       k.Tip AS Vrsta,
       MAX(nl.Broj) AS Nalog,
       MAX(nr.Broj) AS Narudzbenica,
       MAX(RTRIM(p.PaNaziv)) AS Dobavljac,
       COUNT(*) AS Stavki,
       CAST(SUM(CASE WHEN k.Kolicina > 0 THEN k.Kolicina * k.Cena ELSE 0 END) AS decimal(18,2)) AS UlazRSD,
       CAST(SUM(CASE WHEN k.Kolicina < 0 THEN -k.Kolicina * k.Cena ELSE 0 END) AS decimal(18,2)) AS IzlazRSD,
       MAX(k.Napomena) AS Napomena,
       MAX(k.Korisnik) AS Korisnik
FROM dbo.SRV_DeoKretanje k
LEFT JOIN dbo.SRV_Nalog nl ON nl.ID = k.NalogID
LEFT JOIN dbo.SRV_Narudzbenica nr ON nr.ID = k.NarudzbenicaID
LEFT JOIN dbo.Partnerji p ON p.PaSifra = ISNULL(k.PaSifra, nr.PaSifra)
GROUP BY k.DokumentBroj, k.Tip;
GO

/* ---------------------------------------------------------------------
   Kartica dela (stanje posle svakog kretanja racuna forma u C#)
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_vw_DeoKartica', 'V') IS NOT NULL DROP VIEW dbo.SRV_vw_DeoKartica;
GO
CREATE VIEW dbo.SRV_vw_DeoKartica
AS
SELECT k.ID, k.DeoID, k.Datum, k.Tip, k.DokumentBroj, k.Kolicina, k.Cena,
       CAST(k.Kolicina * k.Cena AS decimal(18,2)) AS Vrednost,
       nl.Broj AS Nalog, nr.Broj AS Narudzbenica, k.Napomena, k.Korisnik
FROM dbo.SRV_DeoKretanje k
LEFT JOIN dbo.SRV_Nalog nl ON nl.ID = k.NalogID
LEFT JOIN dbo.SRV_Narudzbenica nr ON nr.ID = k.NarudzbenicaID;
GO

/* ---------------------------------------------------------------------
   Brojila: poslednje ocitavanje po sredstvu i tipu
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_vw_BrojiloTrenutno', 'V') IS NOT NULL DROP VIEW dbo.SRV_vw_BrojiloTrenutno;
GO
CREATE VIEW dbo.SRV_vw_BrojiloTrenutno
AS
SELECT SredstvoID, Tip, Vrednost, Datum
FROM (SELECT b.SredstvoID, b.Tip, b.Vrednost, b.Datum,
             ROW_NUMBER() OVER (PARTITION BY b.SredstvoID, b.Tip ORDER BY b.Datum DESC, b.ID DESC) AS rb
      FROM dbo.SRV_SredstvoBrojilo b) x
WHERE rb = 1;
GO

/* ---------------------------------------------------------------------
   Trosak naloga (SR-14): rad + izdati delovi (umanjeno za povrat) + odobreni spoljni servis
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_vw_NalogTrosak', 'V') IS NOT NULL DROP VIEW dbo.SRV_vw_NalogTrosak;
GO
CREATE VIEW dbo.SRV_vw_NalogTrosak
AS
SELECT n.ID AS NalogID,
       ISNULL(r.Rad, 0) AS Rad,
       ISNULL(d.Delovi, 0) AS Delovi,
       ISNULL(s.Spoljni, 0) AS Spoljni,
       ISNULL(r.Rad, 0) + ISNULL(d.Delovi, 0) + ISNULL(s.Spoljni, 0) AS Ukupno
FROM dbo.SRV_Nalog n
OUTER APPLY (SELECT CAST(SUM(Sati * CenaSata) AS decimal(18,2)) AS Rad
             FROM dbo.SRV_NalogRad WHERE NalogID = n.ID AND Aktivno = 1) r
OUTER APPLY (SELECT CAST(SUM(-k.Kolicina * k.Cena) AS decimal(18,2)) AS Delovi
             FROM dbo.SRV_DeoKretanje k WHERE k.NalogID = n.ID AND k.Tip IN (N'Izdavanje', N'Povrat')) d
OUTER APPLY (SELECT SUM(Iznos) AS Spoljni
             FROM dbo.SRV_SpoljniServisZahtev WHERE NalogID = n.ID AND Status IN (N'Odobreno', N'Zatvoreno')) s;
GO

/* ---------------------------------------------------------------------
   Servisni nalozi (lista)
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_vw_Nalog', 'V') IS NOT NULL DROP VIEW dbo.SRV_vw_Nalog;
GO
CREATE VIEW dbo.SRV_vw_Nalog
AS
SELECT n.ID, n.Broj, n.DatumOtvaranja, n.SredstvoID, s.Oznaka AS Sredstvo, s.Naziv AS NazivSredstva,
       s.VrstaSredstvaID, v.Naziv AS VrstaSredstva,
       n.Vrsta, kv.Broj AS Kvar, n.KvarID, pl.Naziv AS PlanNaziv, n.PlanID,
       n.Izvrsilac, n.Status, n.Opis, n.BrojiloTip, n.BrojiloOtvaranje, n.BrojiloZatvaranje,
       n.DatumZavrsetka, n.RazlogOtkazivanja,
       t.Rad, t.Delovi, t.Spoljni, t.Ukupno,
       CASE WHEN n.Status IN (N'Završen', N'Otkazan') THEN 1 ELSE 0 END AS Zakljucan,
       n.Kreirao
FROM dbo.SRV_Nalog n
JOIN dbo.SRV_Sredstvo s ON s.ID = n.SredstvoID
JOIN dbo.SRV_VrstaSredstva v ON v.ID = s.VrstaSredstvaID
JOIN dbo.SRV_vw_NalogTrosak t ON t.NalogID = n.ID
LEFT JOIN dbo.SRV_Kvar kv ON kv.ID = n.KvarID
LEFT JOIN dbo.SRV_PlanOdrzavanja pl ON pl.ID = n.PlanID;
GO

/* ---------------------------------------------------------------------
   Delovi na nalogu
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_vw_NalogDeo', 'V') IS NOT NULL DROP VIEW dbo.SRV_vw_NalogDeo;
GO
CREATE VIEW dbo.SRV_vw_NalogDeo
AS
SELECT nd.ID, nd.NalogID, nd.DeoID, d.Sifra, d.Naziv, d.JM, nd.Kolicina, nd.Status,
       z.Raspolozivo,
       ISNULL(nd.CenaIzdavanja, d.ProsecnaCena) AS Cena,
       CAST(CASE WHEN nd.Status = N'Izdato' THEN (nd.Kolicina - nd.VracenoKolicina) * nd.CenaIzdavanja ELSE 0 END AS decimal(18,2)) AS Trosak,
       nd.VracenoKolicina AS Vraceno, nd.IzdatnicaBroj, nd.IzNormativa
FROM dbo.SRV_NalogDeo nd
JOIN dbo.SRV_Deo d ON d.ID = nd.DeoID
JOIN dbo.SRV_vw_Zalihe z ON z.DeoID = nd.DeoID;
GO

/* ---------------------------------------------------------------------
   Sredstva (lista) sa glavnim brojilom
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_vw_Sredstvo', 'V') IS NOT NULL DROP VIEW dbo.SRV_vw_Sredstvo;
GO
CREATE VIEW dbo.SRV_vw_Sredstvo
AS
SELECT s.ID, s.Oznaka, s.Naziv, s.VrstaSredstvaID, v.Naziv AS Vrsta, s.Proizvodjac, s.Model, s.Godina,
       s.SerijskiBroj, s.Lokacija, s.Status, gb.Tip AS BrojiloTip, bt.Vrednost AS Brojilo, bt.Datum AS DatumBrojila,
       (SELECT COUNT(*) FROM dbo.SRV_Nalog n WHERE n.SredstvoID = s.ID AND n.Status NOT IN (N'Završen', N'Otkazan')) AS OtvorenihNaloga,
       s.VoziloID, s.Napomena, s.Aktivno
FROM dbo.SRV_Sredstvo s
JOIN dbo.SRV_VrstaSredstva v ON v.ID = s.VrstaSredstvaID
OUTER APPLY (SELECT TOP 1 b.Tip FROM dbo.SRV_VrstaSredstvaBrojilo b
             WHERE b.VrstaSredstvaID = s.VrstaSredstvaID AND b.Aktivno = 1 ORDER BY b.Redosled, b.ID) gb
LEFT JOIN dbo.SRV_vw_BrojiloTrenutno bt ON bt.SredstvoID = s.ID AND bt.Tip = gb.Tip;
GO

/* ---------------------------------------------------------------------
   Status plana odrzavanja po sredstvu (SR-15)
   Osnova: poslednji zavrsen redovni nalog po tom planu, inace pocetak evidencije.
   Status po svakom intervalu, vazi najgori (blizi): U redu < Upozorenje < Dospelo < Kasni.
   Dospelo: ostatak <= 0 a prekoracenje manje od 10% intervala; Kasni: 10% i vise.
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_vw_PlanStatus', 'V') IS NOT NULL DROP VIEW dbo.SRV_vw_PlanStatus;
GO
CREATE VIEW dbo.SRV_vw_PlanStatus
AS
SELECT q.*,
       CASE q.Rang WHEN 3 THEN N'Kasni' WHEN 2 THEN N'Dospelo' WHEN 1 THEN N'Upozorenje' ELSE N'U redu' END AS StatusPlana
FROM
(
    SELECT b.*,
           (SELECT MAX(r) FROM (VALUES (b.RangKm), (b.RangMH), (b.RangDana)) AS t(r)) AS Rang
    FROM
    (
        SELECT a.*,
               CASE WHEN a.IntervalKm <= 0 OR a.TrenutnoKm IS NULL THEN 0
                    WHEN a.SledeciKm - a.TrenutnoKm > a.UpozorenjeKm THEN 0
                    WHEN a.SledeciKm - a.TrenutnoKm > 0 THEN 1
                    WHEN a.TrenutnoKm - a.SledeciKm < a.IntervalKm * 0.1 THEN 2 ELSE 3 END AS RangKm,
               CASE WHEN a.IntervalMH <= 0 OR a.TrenutnoMH IS NULL THEN 0
                    WHEN a.SledeciMH - a.TrenutnoMH > a.UpozorenjeMH THEN 0
                    WHEN a.SledeciMH - a.TrenutnoMH > 0 THEN 1
                    WHEN a.TrenutnoMH - a.SledeciMH < a.IntervalMH * 0.1 THEN 2 ELSE 3 END AS RangMH,
               CASE WHEN a.SledeciDatum IS NULL THEN 0
                    WHEN a.OstaloDana > a.UpozorenjeDana THEN 0
                    WHEN a.OstaloDana > 0 THEN 1
                    WHEN -a.OstaloDana < a.IntervalUDanima * 0.1 THEN 2 ELSE 3 END AS RangDana
        FROM
        (
            SELECT s.ID AS SredstvoID, s.Oznaka, s.Naziv AS NazivSredstva, s.VrstaSredstvaID,
                   p.ID AS PlanID, p.Naziv AS PlanNaziv,
                   p.IntervalKm, p.IntervalMH, p.IntervalDana, p.IntervalMeseci,
                   p.UpozorenjeKm, p.UpozorenjeMH, p.UpozorenjeDana,
                   z.DatumZavrsetka AS PoslednjiServis, z.Broj AS PoslednjiNalog,
                   km.Vrednost AS TrenutnoKm, mh.Vrednost AS TrenutnoMH,
                   CASE WHEN p.IntervalKm > 0 THEN ISNULL(bkm.Vrednost, 0) + p.IntervalKm END AS SledeciKm,
                   CASE WHEN p.IntervalMH > 0 THEN ISNULL(bmh.Vrednost, 0) + p.IntervalMH END AS SledeciMH,
                   sd.SledeciDatum,
                   DATEDIFF(day, CAST(GETDATE() AS date), sd.SledeciDatum) AS OstaloDana,
                   CASE WHEN p.IntervalDana > 0 THEN p.IntervalDana ELSE p.IntervalMeseci * 30 END AS IntervalUDanima,
                   (SELECT TOP 1 o.Broj FROM dbo.SRV_Nalog o
                    WHERE o.SredstvoID = s.ID AND o.PlanID = p.ID AND o.Status NOT IN (N'Završen', N'Otkazan')
                    ORDER BY o.ID DESC) AS OtvorenNalog
            FROM dbo.SRV_Sredstvo s
            JOIN dbo.SRV_PlanOdrzavanja p ON p.VrstaSredstvaID = s.VrstaSredstvaID AND p.Aktivno = 1
            OUTER APPLY (SELECT TOP 1 n.DatumZavrsetka, n.Broj FROM dbo.SRV_Nalog n
                         WHERE n.SredstvoID = s.ID AND n.PlanID = p.ID AND n.Status = N'Završen'
                         ORDER BY n.DatumZavrsetka DESC, n.ID DESC) z
            OUTER APPLY (SELECT CAST(ISNULL(z.DatumZavrsetka, s.DatumKreiranja) AS date) AS Osnova) o
            -- brojilo u trenutku poslednjeg servisa (ili prvo ocitavanje)
            OUTER APPLY (SELECT TOP 1 b.Vrednost FROM dbo.SRV_SredstvoBrojilo b
                         WHERE b.SredstvoID = s.ID AND b.Tip = N'KM'
                           AND (z.DatumZavrsetka IS NULL OR b.Datum <= CAST(z.DatumZavrsetka AS date))
                         ORDER BY CASE WHEN z.DatumZavrsetka IS NULL THEN 0 ELSE 1 END,
                                  CASE WHEN z.DatumZavrsetka IS NULL THEN b.Datum END ASC,
                                  b.Datum DESC, b.ID DESC) bkm
            OUTER APPLY (SELECT TOP 1 b.Vrednost FROM dbo.SRV_SredstvoBrojilo b
                         WHERE b.SredstvoID = s.ID AND b.Tip = N'MH'
                           AND (z.DatumZavrsetka IS NULL OR b.Datum <= CAST(z.DatumZavrsetka AS date))
                         ORDER BY CASE WHEN z.DatumZavrsetka IS NULL THEN 0 ELSE 1 END,
                                  CASE WHEN z.DatumZavrsetka IS NULL THEN b.Datum END ASC,
                                  b.Datum DESC, b.ID DESC) bmh
            LEFT JOIN dbo.SRV_vw_BrojiloTrenutno km ON km.SredstvoID = s.ID AND km.Tip = N'KM'
            LEFT JOIN dbo.SRV_vw_BrojiloTrenutno mh ON mh.SredstvoID = s.ID AND mh.Tip = N'MH'
            OUTER APPLY (SELECT MIN(d) AS SledeciDatum
                         FROM (VALUES (CASE WHEN p.IntervalDana > 0 THEN DATEADD(day, p.IntervalDana, o.Osnova) END),
                                      (CASE WHEN p.IntervalMeseci > 0 THEN DATEADD(month, p.IntervalMeseci, o.Osnova) END)) AS t(d)) sd
            WHERE s.Aktivno = 1 AND s.Status IN (N'U radu', N'U kvaru')
        ) a
    ) b
) q;
GO

/* ---------------------------------------------------------------------
   Status dokumenata sredstava (SR-16) + obavezna dokumenta koja nedostaju
   Vazeci > Upozorenje (do praga iz vrste) > Istice (do 7 dana, ne vise od praga) > Isteklo
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_vw_DokumentStatus', 'V') IS NOT NULL DROP VIEW dbo.SRV_vw_DokumentStatus;
GO
CREATE VIEW dbo.SRV_vw_DokumentStatus
AS
SELECT x.ID, x.SredstvoID, s.Oznaka, s.Naziv AS NazivSredstva, x.VrstaDokumentaID, vd.Naziv AS Dokument,
       x.Broj, x.DatumIzdavanja, x.DatumIsteka, x.Prilog, x.Napomena,
       DATEDIFF(day, CAST(GETDATE() AS date), x.DatumIsteka) AS DanaDoIsteka,
       vd.UpozorenjeDana,
       CASE WHEN DATEDIFF(day, CAST(GETDATE() AS date), x.DatumIsteka) < 0 THEN N'Isteklo'
            WHEN DATEDIFF(day, CAST(GETDATE() AS date), x.DatumIsteka) <= CASE WHEN vd.UpozorenjeDana < 7 THEN vd.UpozorenjeDana ELSE 7 END THEN N'Ističe'
            WHEN DATEDIFF(day, CAST(GETDATE() AS date), x.DatumIsteka) <= vd.UpozorenjeDana THEN N'Upozorenje'
            ELSE N'Važeći' END AS Status,
       x.ObavestenoPrag
FROM (SELECT d.*, ROW_NUMBER() OVER (PARTITION BY d.SredstvoID, d.VrstaDokumentaID ORDER BY d.DatumIsteka DESC, d.ID DESC) AS rb
      FROM dbo.SRV_SredstvoDokument d WHERE d.Aktivno = 1) x
JOIN dbo.SRV_Sredstvo s ON s.ID = x.SredstvoID
JOIN dbo.SRV_VrstaSredstvaDokument vd ON vd.ID = x.VrstaDokumentaID
WHERE x.rb = 1 AND s.Aktivno = 1
UNION ALL
SELECT NULL, s.ID, s.Oznaka, s.Naziv, vd.ID, vd.Naziv, NULL, NULL, NULL, NULL, NULL, NULL, vd.UpozorenjeDana, N'Nedostaje', NULL
FROM dbo.SRV_Sredstvo s
JOIN dbo.SRV_VrstaSredstvaDokument vd ON vd.VrstaSredstvaID = s.VrstaSredstvaID AND vd.Obavezno = 1 AND vd.Aktivno = 1
WHERE s.Aktivno = 1
  AND NOT EXISTS (SELECT 1 FROM dbo.SRV_SredstvoDokument d WHERE d.SredstvoID = s.ID AND d.VrstaDokumentaID = vd.ID AND d.Aktivno = 1);
GO

/* ---------------------------------------------------------------------
   Prijave kvarova
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_vw_Kvar', 'V') IS NOT NULL DROP VIEW dbo.SRV_vw_Kvar;
GO
CREATE VIEW dbo.SRV_vw_Kvar
AS
SELECT k.ID, k.Broj, k.DatumPrijave, k.SredstvoID, s.Oznaka AS Sredstvo, s.Naziv AS NazivSredstva,
       k.Prijavio, k.Kanal, k.Kategorija, k.Hitnost,
       CASE WHEN k.MozeDaRadi = 1 THEN N'Da' ELSE N'Ne' END AS MozeDaRadi,
       k.BrojiloTip, k.Brojilo, k.Opis, k.Status, k.RazlogOdbacivanja,
       (SELECT TOP 1 n.Broj FROM dbo.SRV_Nalog n WHERE n.KvarID = k.ID ORDER BY n.ID DESC) AS Nalog,
       (SELECT COUNT(*) FROM dbo.SRV_KvarPrilog p WHERE p.KvarID = k.ID AND p.Aktivno = 1) AS Priloga
FROM dbo.SRV_Kvar k
JOIN dbo.SRV_Sredstvo s ON s.ID = k.SredstvoID;
GO

/* ---------------------------------------------------------------------
   Zahtevi za spoljni servis
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_vw_SpoljniServis', 'V') IS NOT NULL DROP VIEW dbo.SRV_vw_SpoljniServis;
GO
CREATE VIEW dbo.SRV_vw_SpoljniServis
AS
SELECT z.ID, z.Broj, z.DatumKreiranja AS Datum, z.NalogID, n.Broj AS Nalog, s.Oznaka AS Sredstvo,
       z.PaSifra, RTRIM(p.PaNaziv) AS Partner, z.BrojPonude, z.Razlog, z.Iznos, z.Nivo, z.Status,
       z.Podneo, z.Odobrio, z.DatumOdobrenja, z.Faktura, z.GarancijaMeseci
FROM dbo.SRV_SpoljniServisZahtev z
JOIN dbo.SRV_Nalog n ON n.ID = z.NalogID
JOIN dbo.SRV_Sredstvo s ON s.ID = n.SredstvoID
LEFT JOIN dbo.Partnerji p ON p.PaSifra = z.PaSifra;
GO

/* ---------------------------------------------------------------------
   Narudzbenice
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_vw_Narudzbenica', 'V') IS NOT NULL DROP VIEW dbo.SRV_vw_Narudzbenica;
GO
CREATE VIEW dbo.SRV_vw_Narudzbenica
AS
SELECT n.ID, n.Broj, n.Datum, n.PaSifra, RTRIM(p.PaNaziv) AS Dobavljac, n.OcekivaniDatum, n.Status,
       (SELECT COUNT(*) FROM dbo.SRV_NarudzbenicaStavka s WHERE s.NarudzbenicaID = n.ID) AS Stavki,
       ISNULL((SELECT CAST(SUM(s.Kolicina * s.Cena) AS decimal(18,2)) FROM dbo.SRV_NarudzbenicaStavka s WHERE s.NarudzbenicaID = n.ID), 0) AS Iznos,
       n.Nivo, n.Podneo, n.Odobrio, n.DatumOdobrenja, n.Napomena, n.Komentar
FROM dbo.SRV_Narudzbenica n
LEFT JOIN dbo.Partnerji p ON p.PaSifra = n.PaSifra;
GO

/* ---------------------------------------------------------------------
   Popisi
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_vw_Popis', 'V') IS NOT NULL DROP VIEW dbo.SRV_vw_Popis;
GO
CREATE VIEW dbo.SRV_vw_Popis
AS
SELECT p.ID, p.Broj, p.Datum, p.DeoGrupaID, ISNULL(g.Naziv, N'Svi delovi') AS Grupa, p.Status,
       ISNULL(a.Stavki, 0) AS Stavki, ISNULL(a.Prebrojano, 0) AS Prebrojano, ISNULL(a.SaRazlikom, 0) AS SaRazlikom,
       ISNULL(a.Manjak, 0) AS ManjakRSD, ISNULL(a.Visak, 0) AS VisakRSD,
       p.Podneo, p.Odobrio, p.DatumOdobrenja, p.Napomena
FROM dbo.SRV_Popis p
LEFT JOIN dbo.SRV_DeoGrupa g ON g.ID = p.DeoGrupaID
OUTER APPLY (SELECT COUNT(*) AS Stavki,
                    SUM(CASE WHEN s.PrebrojanaKolicina IS NOT NULL THEN 1 ELSE 0 END) AS Prebrojano,
                    SUM(CASE WHEN s.PrebrojanaKolicina IS NOT NULL AND s.PrebrojanaKolicina <> s.KnjigovodstvenaKolicina THEN 1 ELSE 0 END) AS SaRazlikom,
                    CAST(SUM(CASE WHEN s.PrebrojanaKolicina < s.KnjigovodstvenaKolicina THEN (s.KnjigovodstvenaKolicina - s.PrebrojanaKolicina) * s.Cena ELSE 0 END) AS decimal(18,2)) AS Manjak,
                    CAST(SUM(CASE WHEN s.PrebrojanaKolicina > s.KnjigovodstvenaKolicina THEN (s.PrebrojanaKolicina - s.KnjigovodstvenaKolicina) * s.Cena ELSE 0 END) AS decimal(18,2)) AS Visak
             FROM dbo.SRV_PopisStavka s WHERE s.PopisID = p.ID) a;
GO

IF OBJECT_ID('dbo.SRV_vw_PopisStavka', 'V') IS NOT NULL DROP VIEW dbo.SRV_vw_PopisStavka;
GO
CREATE VIEW dbo.SRV_vw_PopisStavka
AS
SELECT s.ID, s.PopisID, d.Sifra, d.Naziv AS Deo, d.Lokacija, d.JM,
       s.KnjigovodstvenaKolicina AS Knjigovodstveno, s.PrebrojanaKolicina AS Prebrojano,
       CASE WHEN s.PrebrojanaKolicina IS NULL THEN NULL ELSE s.PrebrojanaKolicina - s.KnjigovodstvenaKolicina END AS Razlika,
       s.Cena,
       CASE WHEN s.PrebrojanaKolicina IS NULL THEN NULL
            ELSE CAST((s.PrebrojanaKolicina - s.KnjigovodstvenaKolicina) * s.Cena AS decimal(18,2)) END AS VrednostRazlike,
       s.Napomena, s.DeoID
FROM dbo.SRV_PopisStavka s
JOIN dbo.SRV_Deo d ON d.ID = s.DeoID;
GO

/* ---------------------------------------------------------------------
   Korekcije
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_vw_Korekcija', 'V') IS NOT NULL DROP VIEW dbo.SRV_vw_Korekcija;
GO
CREATE VIEW dbo.SRV_vw_Korekcija
AS
SELECT k.ID, k.Broj AS Zahtev, k.DatumKreiranja AS Datum, d.Sifra, d.Naziv AS Deo, k.Kolicina AS Korekcija, d.JM,
       st.Stanje AS StanjeSada, k.Razlog, k.Status, k.Podneo AS Trazio, k.Odlucio, k.DatumOdluke,
       CASE WHEN k.Status = N'Odobrena' THEN k.Broj END AS Dokument, k.DeoID
FROM dbo.SRV_Korekcija k
JOIN dbo.SRV_Deo d ON d.ID = k.DeoID
JOIN dbo.SRV_vw_DeoStanje st ON st.DeoID = k.DeoID;
GO

/* ---------------------------------------------------------------------
   Obavestenja
   --------------------------------------------------------------------- */
IF OBJECT_ID('dbo.SRV_vw_Obavestenje', 'V') IS NOT NULL DROP VIEW dbo.SRV_vw_Obavestenje;
GO
CREATE VIEW dbo.SRV_vw_Obavestenje
AS
SELECT o.ID, o.Vreme, o.Vrsta, o.Tekst AS Obavestenje, z.Raspolozivo AS RaspolozivoSada,
       CASE WHEN o.Procitano = 1 THEN N'Pročitano' ELSE N'Nepročitano' END AS Status,
       o.ProcitaoKorisnik AS Procitao, o.VremeCitanja, o.DeoID, o.SredstvoID, o.Procitano
FROM dbo.SRV_DeoObavestenje o
LEFT JOIN dbo.SRV_vw_Zalihe z ON z.DeoID = o.DeoID;
GO
