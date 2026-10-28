using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using DataRow = System.Data.DataRow;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    // ZAH-008: izvoz cenovnika u .xlsx u rasporedu kao danas:
    // sheet po programu, sekcija po kolekciji sa redom proseka marze, red po maticnom podatku.
    // Kolona A = LargoProizvodID, B = Code; kurs u D1. Trosak i VP su vrednosti, a marza, cena posle
    // rabata, akcije i Jela marza su formule koje citaju kurs i procente iz zaglavlja (red 2),
    // pa fajl radi i van aplikacije. Rabat/akcija reda se upisuje samo kad se razlikuje od zaglavlja.
    public static class IzvozExcel
    {
        const int RedProcenata = 2, RedNaslova = 3, PrviRed = 4;

        class Kolona
        {
            public string Naslov;
            public string Format;
            public Func<int, string> Formula;            // za red r
            public Func<StavkaObracuna, object> Vrednost;
            public decimal? Zaglavlje;                   // procenat u redu 2
            public double Sirina = 11;
        }

        public static string ImeFajla(string naziv, DateTime vreme)
        {
            string ime = naziv + " " + vreme.ToString("yyyy-MM-dd HHmm") + ".xlsx";
            foreach (var c in Path.GetInvalidFileNameChars()) ime = ime.Replace(c, '_');
            return ime;
        }

        static string Slovo(int kolona)
        {
            return XLHelper.GetColumnLetterFromNumber(kolona);
        }

        static decimal Najcesci(IEnumerable<decimal> vrednosti)
        {
            return vrednosti.GroupBy(v => v).OrderByDescending(g => g.Count()).Select(g => g.Key).DefaultIfEmpty(0).First();
        }

        public static void Izvezi(string fajl, DataRow zaglavlje, IList<StavkaObracuna> stavke, bool uEvrima)
        {
            string naziv = zaglavlje["Naziv"].ToString();
            string valuta = zaglavlje["Valuta"].ToString();
            decimal kurs = Convert.ToDecimal(zaglavlje["Kurs"]);
            decimal pdv = Convert.ToDecimal(zaglavlje["PDV"]);
            bool rsd = valuta == ObracunCene.RSD;

            using (var wb = new XLWorkbook())
            {
                foreach (var program in stavke.GroupBy(s => s.Program).OrderBy(g => g.Key))
                {
                    string imeSheeta = program.Key ?? "Program";
                    foreach (var c in new[] { ':', '\\', '/', '?', '*', '[', ']' }) imeSheeta = imeSheeta.Replace(c, '_');
                    if (imeSheeta.Length > 31) imeSheeta = imeSheeta.Substring(0, 31);
                    var ws = wb.Worksheets.Add(imeSheeta);

                    var redovi = program.ToList();
                    decimal rabat = Najcesci(redovi.Select(r => r.Rabat));
                    decimal akcija = Najcesci(redovi.Select(r => r.Akcija ?? 0));
                    var poredjenje = redovi.Select(r => r.AkcijePoredjenje).Where(a => a.Count > 0)
                        .GroupBy(a => string.Join("|", a)).OrderByDescending(g => g.Count())
                        .Select(g => g.First()).FirstOrDefault() ?? new List<decimal>();

                    var kolone = NapraviKolone(rsd, uEvrima, rabat, akcija, poredjenje);
                    Zaglavlje(ws, naziv, kurs, pdv, valuta, kolone);
                    Podaci(ws, redovi, kolone);
                    ws.SheetView.FreezeRows(RedNaslova);
                    ws.SheetView.FreezeColumns(3);
                }
                if (wb.Worksheets.Count == 0) wb.Worksheets.Add("Prazno");
                wb.SaveAs(fajl);
            }
        }

        static List<Kolona> NapraviKolone(bool rsd, bool uEvrima, decimal rabat, decimal akcija, List<decimal> poredjenje)
        {
            var k = new List<Kolona>();
            // indeksi kolona su fiksni za A-E, ostali se racunaju posle dodavanja
            k.Add(new Kolona { Naslov = "ID", Format = "0", Vrednost = s => s.LargoProizvodID, Sirina = 9 });
            k.Add(new Kolona { Naslov = "Code", Vrednost = s => s.Code, Sirina = 14 });
            k.Add(new Kolona { Naslov = "Naziv", Vrednost = s => s.NazivProizvoda, Sirina = 32 });
            k.Add(new Kolona { Naslov = "UKUPNO TP", Format = "#,##0.00", Vrednost = s => s.TP });
            k.Add(new Kolona { Naslov = "VP cena", Format = "#,##0", Vrednost = s => s.VP });

            // pomocni izrazi; kolone Rabat (R) i Akcija (A) se popunjavaju dole
            string tpu = rsd ? "D{0}" : "(D{0}/$D$1)";
            Func<int, string> TPu = r => string.Format(tpu, r);
            int kolRabat = 0, kolAkcija = 0;
            Func<int, string> R = r => "IF(" + Slovo(kolRabat) + r + "=\"\"," + Slovo(kolRabat) + "$" + RedProcenata + "," + Slovo(kolRabat) + r + ")";
            Func<int, string> A = r => "IF(" + Slovo(kolAkcija) + r + "=\"\"," + Slovo(kolAkcija) + "$" + RedProcenata + "," + Slovo(kolAkcija) + r + ")";
            Func<int, string> Rabatna = r => "E" + r + "*(1-" + R(r) + ")";

            k.Add(new Kolona { Naslov = "Marža", Format = "0.00%", Formula = r => "IFERROR(E" + r + "/" + TPu(r) + "-1,\"\")" });
            if (rsd) k.Add(new Kolona { Naslov = "VP sa PDV", Format = "#,##0", Formula = r => "ROUND(E" + r + "*(1+$F$1),0)" });

            k.Add(new Kolona
            {
                Naslov = "Rabat " + (rabat * 100).ToString("0.##", CultureInfo.InvariantCulture) + "%",
                Format = "0.00%", Zaglavlje = rabat, Sirina = 10,
                Vrednost = s => s.Rabat == rabat ? null : (object)s.Rabat
            });
            kolRabat = k.Count;
            k.Add(new Kolona { Naslov = "Posle rabata", Format = "#,##0", Formula = r => "ROUND(" + Rabatna(r) + ",0)" });
            k.Add(new Kolona { Naslov = "Jela marža", Format = "0.00%", Formula = r => "IFERROR(" + Rabatna(r) + "/" + TPu(r) + "-1,\"\")" });
            if (rsd) k.Add(new Kolona { Naslov = "Salon marža", Format = "0.00%", Formula = r => "IFERROR((E" + r + "-" + Rabatna(r) + ")/(" + Rabatna(r) + "),\"\")" });

            k.Add(new Kolona
            {
                Naslov = "Akcija " + (akcija * 100).ToString("0.##", CultureInfo.InvariantCulture) + "%",
                Format = "0.00%", Zaglavlje = akcija, Sirina = 10,
                Vrednost = s => (s.Akcija ?? 0) == akcija ? null : (object)(s.Akcija ?? 0)
            });
            kolAkcija = k.Count;
            k.Add(new Kolona { Naslov = "Cena u akciji", Format = "#,##0", Formula = r => "ROUND(" + Rabatna(r) + "*(1-" + A(r) + "),0)" });
            k.Add(new Kolona { Naslov = "Jela marža akcija", Format = "0.00%", Formula = r => "IFERROR(" + Rabatna(r) + "*(1-" + A(r) + ")/" + TPu(r) + "-1,\"\")" });

            foreach (var p in poredjenje)
            {
                int kolP = k.Count + 1;
                string pct = (p * 100).ToString("0.##", CultureInfo.InvariantCulture) + "%";
                k.Add(new Kolona { Naslov = "Akcija " + pct, Format = "#,##0", Zaglavlje = p, Formula = r => "ROUND(" + Rabatna(r) + "*(1-" + Slovo(kolP) + "$" + RedProcenata + "),0)" });
                k.Add(new Kolona { Naslov = "Jela " + pct, Format = "0.00%", Formula = r => "IFERROR(" + Rabatna(r) + "*(1-" + Slovo(kolP) + "$" + RedProcenata + ")/" + TPu(r) + "-1,\"\")" });
            }

            if (rsd && uEvrima)
            {
                k.Add(new Kolona { Naslov = "VP €", Format = "#,##0", Formula = r => "ROUND(E" + r + "/$D$1,0)" });
                k.Add(new Kolona { Naslov = "Posle rabata €", Format = "#,##0", Formula = r => "ROUND(" + Rabatna(r) + "/$D$1,0)" });
            }
            return k;
        }

        static void Zaglavlje(IXLWorksheet ws, string naziv, decimal kurs, decimal pdv, string valuta, List<Kolona> kolone)
        {
            ws.Cell(1, 1).Value = naziv;
            ws.Cell(1, 1).Style.Font.Bold = true;
            ws.Cell(1, 3).Value = "Kurs";
            ws.Cell(1, 4).Value = kurs;
            ws.Cell(1, 4).Style.NumberFormat.Format = "0.00";
            ws.Cell(1, 5).Value = "PDV";
            ws.Cell(1, 6).Value = pdv;
            ws.Cell(1, 6).Style.NumberFormat.Format = "0%";
            ws.Cell(1, 7).Value = valuta;
            ws.Cell(1, 8).Value = DateTime.Now;
            ws.Cell(1, 8).Style.NumberFormat.Format = "dd.MM.yyyy HH:mm";

            for (int i = 0; i < kolone.Count; i++)
            {
                var k = kolone[i];
                if (k.Zaglavlje.HasValue)
                {
                    var c = ws.Cell(RedProcenata, i + 1);
                    c.Value = k.Zaglavlje.Value;
                    c.Style.NumberFormat.Format = "0.00%";
                    c.Style.Fill.BackgroundColor = XLColor.FromArgb(255, 226, 190);
                }
                var n = ws.Cell(RedNaslova, i + 1);
                n.Value = k.Naslov;
                n.Style.Font.Bold = true;
                n.Style.Font.FontColor = XLColor.White;
                n.Style.Fill.BackgroundColor = XLColor.FromArgb(58, 77, 57);
                n.Style.Alignment.WrapText = true;
                ws.Column(i + 1).Width = k.Sirina;
            }
        }

        static void Podaci(IXLWorksheet ws, List<StavkaObracuna> redovi, List<Kolona> kolone)
        {
            int r = PrviRed;
            int kolMarza = kolone.FindIndex(k => k.Naslov == "Marža") + 1;
            int kolJela = kolone.FindIndex(k => k.Naslov == "Jela marža") + 1;

            foreach (var kol in redovi.GroupBy(s => s.Kolekcija).OrderBy(g => g.Key))
            {
                var naslov = ws.Range(r, 1, r, kolone.Count);
                ws.Cell(r, 1).Value = kol.Key;
                naslov.Style.Font.Bold = true;
                naslov.Style.Fill.BackgroundColor = XLColor.FromArgb(232, 228, 218);
                r++;

                int prvi = r;
                foreach (var s in kol.OrderBy(x => x.LargoProizvodID))
                {
                    for (int i = 0; i < kolone.Count; i++)
                    {
                        var k = kolone[i];
                        var c = ws.Cell(r, i + 1);
                        if (k.Formula != null)
                            c.FormulaA1 = k.Formula(r);
                        else if (k.Vrednost != null)
                        {
                            var v = k.Vrednost(s);
                            if (v is decimal) c.Value = (decimal)v;
                            else if (v is int) c.Value = (int)v;
                            else if (v != null) c.Value = v.ToString();
                        }
                        if (k.Format != null) c.Style.NumberFormat.Format = k.Format;
                    }
                    if (s.BezTroska) ws.Range(r, 1, r, kolone.Count).Style.Fill.BackgroundColor = XLColor.FromArgb(225, 225, 225);
                    r++;
                }

                // prosek marze kolekcije
                ws.Cell(r, 3).Value = "Prosek " + kol.Key;
                ws.Cell(r, 3).Style.Font.Italic = true;
                foreach (int kp in new[] { kolMarza, kolJela })
                {
                    if (kp <= 0) continue;
                    string sl = Slovo(kp);
                    var c = ws.Cell(r, kp);
                    c.FormulaA1 = "IFERROR(AVERAGE(" + sl + prvi + ":" + sl + (r - 1) + "),\"\")";
                    c.Style.NumberFormat.Format = "0.00%";
                    c.Style.Font.Bold = true;
                }
                r += 2;
            }
        }
    }
}
