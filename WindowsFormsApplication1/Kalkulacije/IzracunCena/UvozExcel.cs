using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ExcelDataReader;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    // ZAH-008: citanje KALK RSD / KALK EUR (.xls i .xlsx) za pocetne vrednosti opsteg cenovnika.
    // Svaki sheet ima isto zaglavlje u redovima 1-3; kolone se nalaze po tekstu zaglavlja:
    //   UKUPNO TP, VP cena, Rabat xx%, Akcija xx% (vise), TP - <mesec>, marza <mesec>.
    // Red proizvoda je prvi red bloka: u koloni A ceo broj (LargoProizvodID), u koloni B tekst (Code)
    // koji nije "U EVRIMA" ni "MP-akcija", a u koloni C trosak veci od nule. Drugi red bloka takodje ima
    // broj u koloni A (redni broj), ali je B broj (VP u dinarskom) ili "U EVRIMA" (devizni), pa se preskace.
    // Kurs = vrednost celije D1 (117,93), ne prikazan tekst. Sakrivene kolone se ne citaju.
    public class UvozExcel
    {
        public class Sheet
        {
            public string Naziv;
            public bool Preskocen;
            public string Razlog;
            public int Nadjeno;
            public int Povezano;
            public decimal? Rabat;
            public List<decimal> Akcije = new List<decimal>();
            public decimal? Kurs;
            public string Nivo;           // Program ili Kolekcija (gde idu rabat i akcije)
        }

        public class Problem
        {
            public string List;
            public int Red;
            public string Vrsta;
            public string Opis;
        }

        public List<Sheet> Listovi { get; } = new List<Sheet>();
        public List<Problem> Problemi { get; } = new List<Problem>();
        public Dictionary<string, decimal> Cene { get; } = new Dictionary<string, decimal>();
        public List<IzracunCenaBaza.UvozRed> Redovi { get; } = new List<IzracunCenaBaza.UvozRed>();
        public ParametriCenovnika Parametri { get; } = new ParametriCenovnika();
        public decimal? Kurs { get; private set; }
        public decimal? PDV { get; private set; }            // A3 prvog sheeta (0,2)
        public int CodeBezCeneUExcelu { get; private set; }

        class Maticni
        {
            public string Code;
            public int? ProgramID;
            public int? KolekcijaID;
        }

        class Kolone
        {
            public int UkupnoTP = -1, VP = -1, Rabat = -1, TPStari = -1, MarzaStara = -1;
            public List<int> Akcije = new List<int>();
            public decimal? RabatProcenat;
            public List<decimal> AkcijeProcenti = new List<decimal>();
        }

        static string Normalizuj(string s)
        {
            if (s == null) return "";
            s = s.ToUpperInvariant().Replace('–', '-').Replace('—', '-').Replace('Ž', 'Z').Replace('ž', 'Z');
            return Regex.Replace(s, @"\s+", " ").Trim();
        }

        static decimal? Broj(object v)
        {
            if (v == null || v == DBNull.Value) return null;
            if (v is double) return (decimal)(double)v;
            if (v is decimal) return (decimal)v;
            if (v is int) return (int)v;
            decimal d;
            string t = v.ToString().Trim().Replace(" ", "");
            if (decimal.TryParse(t, NumberStyles.Any, CultureInfo.GetCultureInfo("sr-Latn-RS"), out d)) return d;
            if (decimal.TryParse(t, NumberStyles.Any, CultureInfo.InvariantCulture, out d)) return d;
            return null;
        }

        static decimal? Procenat(string zaglavlje, string rec)
        {
            var m = Regex.Match(zaglavlje, rec + @"\s*(\d+(?:[.,]\d+)?)\s*%");
            if (!m.Success) return null;
            return decimal.Parse(m.Groups[1].Value.Replace(',', '.'), CultureInfo.InvariantCulture) / 100;
        }

        // procenat iz naslova ("Rabat 34%") ili broj ispod naslova u zaglavlju (0,34 ili 34)
        static decimal? ProcenatKolone(string z, string rec, decimal? broj)
        {
            var p = Procenat(z, rec);
            if (p.HasValue) return p;
            if (!broj.HasValue || broj.Value < 0) return null;
            return broj.Value > 1 ? broj.Value / 100 : broj.Value;
        }

        static Kolone NadjiKolone(string[] zaglavlja, decimal?[] brojevi)
        {
            var k = new Kolone();
            for (int i = 0; i < zaglavlja.Length; i++)
            {
                string z = zaglavlja[i];
                if (z == "") continue;
                decimal? broj = i < brojevi.Length ? brojevi[i] : null;
                if (z.Contains("UKUPNO TP") && k.UkupnoTP < 0) k.UkupnoTP = i;
                else if (z.Contains("VP CENA") && !z.Contains("RABAT") && !z.Contains("AKCIJA") && k.VP < 0) k.VP = i;
                else if (z.Contains("RABAT") && k.Rabat < 0 && ProcenatKolone(z, "RABAT", broj).HasValue)
                {
                    k.Rabat = i;
                    k.RabatProcenat = ProcenatKolone(z, "RABAT", broj);
                }
                else if (z.Contains("AKCIJA") && !z.Contains("JELA") && ProcenatKolone(z, "AKCIJA", broj).HasValue)
                {
                    var p = ProcenatKolone(z, "AKCIJA", broj).Value;
                    if (!k.AkcijeProcenti.Contains(p)) { k.Akcije.Add(i); k.AkcijeProcenti.Add(p); }
                }
                else if (Regex.IsMatch(z, @"(^|\s)TP\s*-\s*\p{L}+") && k.TPStari < 0) k.TPStari = i;
                else if (Regex.IsMatch(z, @"^MARZA\s+\p{L}+") && !z.Contains("JELA") && !z.Contains("SALON") && k.MarzaStara < 0) k.MarzaStara = i;
            }
            return k;
        }

        // maticni: vw_IC_MaticniPodatak (LargoProizvodID, Code, ProgramID, KolekcijaID)
        public static UvozExcel Procitaj(string fajl, DataTable maticni)
        {
            var u = new UvozExcel();
            var mp = new Dictionary<int, Maticni>();
            foreach (DataRow r in maticni.Rows)
                mp[(int)r["LargoProizvodID"]] = new Maticni
                {
                    Code = r["Code"].ToString(),
                    ProgramID = r.IsNull("ProgramID") ? (int?)null : (int)r["ProgramID"],
                    KolekcijaID = r.IsNull("KolekcijaID") ? (int?)null : (int)r["KolekcijaID"]
                };

            var rabati = new List<decimal>();
            var akcijeListova = new List<List<decimal>>();

            using (var stream = File.Open(fajl, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                do
                {
                    var list = new Sheet { Naziv = reader.Name };
                    u.Listovi.Add(list);
                    if (reader.VisibleState != "visible")
                    {
                        list.Preskocen = true;
                        list.Razlog = "sakriven sheet";
                        continue;
                    }

                    var zaglavlja = new string[0];
                    var brojeviZaglavlja = new decimal?[0];   // prvi broj u koloni u redovima 1-3 (rabat 0,34; akcija 0)
                    Kolone k = null;
                    var redoviLista = new List<int>();   // LargoProizvodID u ovom sheetu
                    int red = 0;
                    while (reader.Read())
                    {
                        red++;
                        int n = reader.FieldCount;
                        if (red <= 3)
                        {
                            if (zaglavlja.Length < n) { Array.Resize(ref zaglavlja, n); Array.Resize(ref brojeviZaglavlja, n); }
                            for (int i = 0; i < n; i++)
                            {
                                if (Sakrivena(reader, i)) { zaglavlja[i] = (zaglavlja[i] ?? ""); continue; }
                                var v = reader.GetValue(i);
                                if (v is string || v == null)
                                    zaglavlja[i] = ((zaglavlja[i] ?? "") + " " + Normalizuj(v as string)).Trim();
                                else
                                {
                                    zaglavlja[i] = zaglavlja[i] ?? "";
                                    bool kurs = red == 1 && i == 3;
                                    if (!kurs && !brojeviZaglavlja[i].HasValue) brojeviZaglavlja[i] = Broj(v);
                                }
                            }
                            if (red == 1 && n > 3) list.Kurs = Broj(reader.GetValue(3));
                            if (red == 3 && n > 0 && !u.PDV.HasValue)
                            {
                                var pdv = Broj(reader.GetValue(0));   // A3 = PDV (0,2)
                                if (pdv.HasValue && pdv.Value > 0 && pdv.Value < 1) u.PDV = pdv;
                            }
                            if (red == 3)
                            {
                                k = NadjiKolone(zaglavlja, brojeviZaglavlja);
                                if (k.UkupnoTP < 0 || k.VP < 0)
                                {
                                    list.Preskocen = true;
                                    list.Razlog = "zaglavlje se ne poklapa (nema UKUPNO TP ili VP cena)";
                                    break;
                                }
                                list.Rabat = k.RabatProcenat;
                                list.Akcije.AddRange(k.AkcijeProcenti);
                            }
                            continue;
                        }

                        int id;
                        string code;
                        bool bezTroska;
                        if (!RedProizvoda(reader, out id, out code, out bezTroska))
                        {
                            if (bezTroska)
                                u.Problemi.Add(new Problem { List = list.Naziv, Red = red, Vrsta = "Bez troška u koloni C", Opis = "LargoProizvodID " + id + " (" + code + ") nije uvezen" });
                            continue;
                        }

                        list.Nadjeno++;
                        Maticni m;
                        if (!mp.TryGetValue(id, out m))
                        {
                            u.Problemi.Add(new Problem { List = list.Naziv, Red = red, Vrsta = "Nema u bazi", Opis = "LargoProizvodID " + id + " (" + code + ")" });
                            continue;
                        }
                        list.Povezano++;
                        redoviLista.Add(id);
                        // drugi Code u Excelu nego u bazi: VP tog reda pripada Excel Code-u, pa se ne upisuje
                        // na Code iz baze (inace bi pregazio njegovu pravu cenu); trosak reda se uzima
                        bool drugiCode = !string.Equals(m.Code, code, StringComparison.OrdinalIgnoreCase);
                        if (drugiCode)
                            u.Problemi.Add(new Problem { List = list.Naziv, Red = red, Vrsta = "Drugi Code", Opis = "ID " + id + ": u Excelu " + code + ", u bazi " + m.Code + " (VP iz ovog reda se ne uzima)" });

                        var vp = drugiCode ? null : Broj(reader.GetValue(k.VP));
                        if (vp.HasValue && vp.Value > 0)
                        {
                            decimal postojeca;
                            if (u.Cene.TryGetValue(m.Code, out postojeca))
                            {
                                if (Math.Round(postojeca, 0) != Math.Round(vp.Value, 0))
                                    u.Problemi.Add(new Problem { List = list.Naziv, Red = red, Vrsta = "Dve cene", Opis = m.Code + ": " + postojeca.ToString("N0") + " i " + vp.Value.ToString("N0") + " (ostaje prva)" });
                            }
                            else
                                u.Cene[m.Code] = Math.Round(vp.Value, 0, MidpointRounding.AwayFromZero);
                        }

                        if (u.Redovi.All(x => x.LargoProizvodID != id))
                            u.Redovi.Add(new IzracunCenaBaza.UvozRed
                            {
                                LargoProizvodID = id,
                                Code = m.Code,
                                TrosakStari = k.TPStari >= 0 ? Broj(reader.GetValue(k.TPStari)) : null,
                                MarzaStara = k.MarzaStara >= 0 ? Broj(reader.GetValue(k.MarzaStara)) : null
                            });
                    }

                    if (list.Preskocen || k == null) { if (!list.Preskocen) { list.Preskocen = true; list.Razlog = "prazan sheet"; } continue; }
                    if (list.Kurs.HasValue && !u.Kurs.HasValue) u.Kurs = list.Kurs;

                    // rabat i akcije sheeta: parametar programa ako je sheet jednog programa, inace svake kolekcije
                    var programi = redoviLista.Select(id => mp[id].ProgramID).Distinct().ToList();
                    if (list.Rabat.HasValue || list.Akcije.Count > 0)
                    {
                        if (programi.Count == 1 && programi[0].HasValue)
                        {
                            list.Nivo = "Program";
                            u.DodajParametar(NivoParametra.Program, programi[0], null, list);
                        }
                        else
                        {
                            list.Nivo = "Kolekcija";
                            foreach (var kol in redoviLista.Select(id => mp[id].KolekcijaID).Where(x => x.HasValue).Distinct())
                                u.DodajParametar(NivoParametra.Kolekcija, null, kol, list);
                        }
                        if (list.Rabat.HasValue) rabati.Add(list.Rabat.Value);
                        akcijeListova.Add(list.Akcije);
                    }
                }
                while (reader.NextResult());
            }

            // rabat i akcije koji se najvise ponavljaju vaze za ceo cenovnik
            var cen = u.Parametri.Nadji(NivoParametra.Cenovnik, null, null, null, true);
            if (rabati.Count > 0)
                cen.Rabat = rabati.GroupBy(r => r).OrderByDescending(g => g.Count()).First().Key;
            var najcesce = akcijeListova.Where(a => a.Count > 0)
                .GroupBy(a => string.Join("|", a)).OrderByDescending(g => g.Count()).FirstOrDefault();
            if (najcesce != null)
            {
                cen.Akcija = najcesce.First()[0];
                cen.AkcijePoredjenje.AddRange(najcesce.First().Where(a => a > 0));
            }

            u.CodeBezCeneUExcelu = mp.Values.Select(m => m.Code).Where(c => c != "").Distinct().Count(c => !u.Cene.ContainsKey(c));
            return u;
        }

        // vazeca akcija = prva akcija sheeta; sve akcije sheeta su i akcije za poredjenje
        void DodajParametar(NivoParametra nivo, int? programID, int? kolekcijaID, Sheet list)
        {
            var p = Parametri.Nadji(nivo, programID, kolekcijaID, null, false);
            if (p != null)
            {
                Problemi.Add(new Problem { List = list.Naziv, Vrsta = "Isti " + nivo.ToString().ToLower(), Opis = "rabat i akcije su već uzeti iz ranijeg sheeta; važe iz ovog" });
                p.AkcijePoredjenje.Clear();
            }
            else
                p = Parametri.Nadji(nivo, programID, kolekcijaID, null, true);
            p.Rabat = list.Rabat;
            p.Akcija = list.Akcije.Count > 0 ? list.Akcije[0] : (decimal?)null;
            p.AkcijePoredjenje.AddRange(list.Akcije.Where(a => a > 0));   // 0% nije akcija za poredjenje
        }

        // prvi red bloka: A = ceo broj, B = tekst (ne "U EVRIMA" / "MP-akcija"), C = trosak > 0.
        // bezTroska = true kada A i B jesu red proizvoda, ali u C nema troska (prijavljuje se).
        static bool RedProizvoda(IExcelDataReader reader, out int id, out string code, out bool bezTroska)
        {
            id = 0; code = ""; bezTroska = false;
            if (reader.FieldCount < 3) return false;

            var a = Broj(reader.GetValue(0));
            if (!a.HasValue || a.Value != Math.Floor(a.Value) || a.Value <= 0 || a.Value > int.MaxValue) return false;

            var b = reader.GetValue(1) as string;          // tekst; broj u B je drugi red bloka
            if (b == null) return false;
            code = b.Trim();
            string nb = Normalizuj(code);
            if (code == "" || nb.Contains("U EVRIMA") || nb.Replace(" ", "").StartsWith("MP-AKCIJA")) return false;
            decimal kaoBroj;   // broj upisan kao tekst je takodje drugi red bloka (VP)
            if (decimal.TryParse(code.Replace(" ", ""), NumberStyles.Number, CultureInfo.GetCultureInfo("sr-Latn-RS"), out kaoBroj)) return false;

            id = (int)a.Value;
            var c = Broj(reader.GetValue(2));
            if (!c.HasValue || c.Value <= 0) { bezTroska = true; return false; }
            return true;
        }

        static bool Sakrivena(IExcelDataReader reader, int kolona)
        {
            try { return reader.GetColumnWidth(kolona) == 0; }
            catch { return false; }
        }
    }
}
