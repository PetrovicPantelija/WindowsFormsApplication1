using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using ClosedXML.Excel;
using ExcelDataReader;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    // ZAH-011: ulazni fajl troskovnog obracuna (ista funkcija kao stara forma frmTroskoviIzracun).
    // Excel (svi vidljivi listovi) ili CSV; prvi red je zaglavlje. Kolona 1 = ID paketa (LargoPaketID, ceo broj),
    // kolona 2 = kolicina (prazno ili nema kolone = "1"). Kolicina ide u parametar @Kod procedure InsertMpCenaTroskovi.
    public class UlazTroskova
    {
        public class Red
        {
            public string List;      // naziv sheeta; prazno za CSV
            public int BrojReda;     // red u fajlu, zaglavlje je red 1
            public int PaketID;
            public string Kolicina;
        }

        public class Preskocen
        {
            public string List;
            public int BrojReda;
            public string Razlog;
            public string Vrednost;
        }

        public string Fajl { get; private set; }
        public List<Red> Redovi { get; } = new List<Red>();
        public List<Preskocen> Preskoceni { get; } = new List<Preskocen>();
        public int Praznih { get; private set; }
        public int PreskocenoRedova { get { return Preskoceni.Count(p => p.BrojReda > 0); } }
        public int SakrivenihListova { get { return Preskoceni.Count(p => p.BrojReda == 0); } }
        public int Procitano { get { return Redovi.Count + PreskocenoRedova; } }

        public static bool JeExcel(string fajl)
        {
            string ext = Path.GetExtension(fajl).ToLowerInvariant();
            return ext == ".xlsx" || ext == ".xls" || ext == ".xlsm";
        }

        public static bool JeCsv(string fajl)
        {
            string ext = Path.GetExtension(fajl).ToLowerInvariant();
            return ext == ".csv" || ext == ".txt";
        }

        public static UlazTroskova Procitaj(string fajl)
        {
            var u = new UlazTroskova { Fajl = fajl };
            if (JeExcel(fajl)) u.CitajExcel(fajl);
            else if (JeCsv(fajl)) u.CitajCsv(fajl);
            else throw new InvalidOperationException("Nepoznat tip fajla (" + Path.GetExtension(fajl) + "). Izaberite Excel (.xlsx, .xls) ili CSV (.csv).");
            return u;
        }

        void CitajExcel(string fajl)
        {
            using (var stream = File.Open(fajl, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                do
                {
                    if (reader.VisibleState != "visible")
                    {
                        Preskoceni.Add(new Preskocen { List = reader.Name, Razlog = "Sakriven list, nije čitan" });
                        continue;
                    }
                    int red = 0;
                    while (reader.Read())
                    {
                        red++;
                        if (red == 1) continue;   // zaglavlje
                        Dodaj(reader.Name, red,
                              reader.FieldCount > 0 ? reader.GetValue(0) : null,
                              reader.FieldCount > 1 ? reader.GetValue(1) : null);
                    }
                }
                while (reader.NextResult());
            }
        }

        // isti parser kao uvoz proizvoda (ZAH-009): razdvajac ; , ili tab, UTF-8 ili Windows-1250
        void CitajCsv(string fajl)
        {
            string[] zaglavlje;
            var redovi = Magacin.JelaProductImport.ReadCsv(fajl, out zaglavlje);
            for (int i = 0; i < redovi.Count; i++)
            {
                var c = redovi[i];
                Dodaj("", i + 2, c.Length > 0 ? c[0] : null, c.Length > 1 ? c[1] : null);
            }
        }

        void Dodaj(string list, int red, object id, object kol)
        {
            string idTekst = Tekst(id), kolTekst = Tekst(kol);
            if (idTekst == "" && kolTekst == "") { Praznih++; return; }

            int paket;
            if (!CeoBroj(id, out paket))
            {
                Preskoceni.Add(new Preskocen { List = list, BrojReda = red, Razlog = "ID paketa nije ceo broj", Vrednost = idTekst });
                return;
            }
            string kolicina;
            if (!Kolicina(kol, out kolicina))
            {
                Preskoceni.Add(new Preskocen { List = list, BrojReda = red, Razlog = "Količina nije broj", Vrednost = kolTekst });
                return;
            }
            if (kolicina.Length > 20)
            {
                Preskoceni.Add(new Preskocen { List = list, BrojReda = red, Razlog = "Količina duža od 20 znakova", Vrednost = kolTekst });
                return;
            }
            Redovi.Add(new Red { List = list, BrojReda = red, PaketID = paket, Kolicina = kolicina });
        }

        static string Tekst(object v)
        {
            if (v == null || v == DBNull.Value) return "";
            if (v is double) return ((double)v).ToString(CultureInfo.InvariantCulture);
            return v.ToString().Trim();
        }

        // vrednost celije, ne prikazani tekst: 12345 i 12345,0 su isti broj
        static bool CeoBroj(object v, out int id)
        {
            id = 0;
            if (v is int) { id = (int)v; return true; }
            if (v is double || v is decimal)
            {
                double d = Convert.ToDouble(v);
                if (d != Math.Floor(d) || d < int.MinValue || d > int.MaxValue) return false;
                id = (int)d;
                return true;
            }
            string s = v as string;
            return s != null && int.TryParse(s.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out id);
        }

        // prazno = "1" (kao stari CSV uvoz); broj se salje bez razdvajaca hiljada, sa tackom za decimale (2, 2.5)
        static bool Kolicina(object v, out string kolicina)
        {
            kolicina = null;
            decimal d;
            if (v == null || v == DBNull.Value) { kolicina = "1"; return true; }
            if (v is double)
            {
                double x = (double)v;
                if (double.IsNaN(x) || Math.Abs(x) > 1e15) return false;
                d = (decimal)x;
            }
            else if (v is int || v is decimal) d = Convert.ToDecimal(v);
            else
            {
                string s = v as string;
                if (s == null) return false;   // datum, logicka vrednost
                s = s.Trim().Replace(" ", "");
                if (s == "") { kolicina = "1"; return true; }
                if (s.Contains(",")) s = s.Replace(".", "").Replace(',', '.');   // 2,5 ili 1.234,5
                if (!decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out d)) return false;
            }
            kolicina = d.ToString("0.##########", CultureInfo.InvariantCulture);
            return true;
        }

        public DataTable TabelaPreskocenih()
        {
            var dt = new DataTable("Preskočeni");
            dt.Columns.Add("List");
            dt.Columns.Add("Red", typeof(int));
            dt.Columns.Add("Razlog");
            dt.Columns.Add("Vrednost");
            foreach (var p in Preskoceni)
                dt.Rows.Add(p.List, p.BrojReda > 0 ? (object)p.BrojReda : DBNull.Value, p.Razlog, p.Vrednost);
            return dt;
        }
    }

    // ZAH-011: procedure troskovnog obracuna. Imena i parametri kao u InsertPorudzbina (InsertMPCene,
    // DeleteMPCene, ObracunajMPCene), ali sve u jednoj konekciji i transakciji: greska u bilo kom redu
    // vraca i brisanje, pa stari spisak ostaje. Logika obracuna je u procedurama i ne menja se.
    public class TroskovniObracunBaza
    {
        readonly string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        const int Timeout = 300;

        public DataTable Rezultat()
        {
            using (var conn = new SqlConnection(connect))
            using (var cmd = new SqlCommand("SELECT * FROM [0Kalk]", conn))
            {
                cmd.CommandTimeout = Timeout;
                var dt = new DataTable();
                new SqlDataAdapter(cmd).Fill(dt);
                return dt;
            }
        }

        public int BrojRedova()
        {
            using (var conn = new SqlConnection(connect))
            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM [0Kalk]", conn))
            {
                cmd.CommandTimeout = Timeout;
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        static SqlCommand Procedura(SqlConnection conn, SqlTransaction tr, string naziv)
        {
            var cmd = new SqlCommand(naziv, conn, tr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = Timeout;
            return cmd;
        }

        // napredak: broj upisanih redova; kada dostigne redovi.Count, sledi obracun
        public void Obracunaj(IList<UlazTroskova.Red> redovi, IProgress<int> napredak)
        {
            using (var conn = new SqlConnection(connect))
            {
                conn.Open();
                using (var tr = conn.BeginTransaction())
                {
                    string korak = "Brisanje postojećeg spiska (DeleteMpCeneTroskovi)";
                    try
                    {
                        using (var cmd = Procedura(conn, tr, "DeleteMpCeneTroskovi"))
                            cmd.ExecuteNonQuery();

                        using (var cmd = Procedura(conn, tr, "InsertMpCenaTroskovi"))
                        {
                            var mp = cmd.Parameters.Add("@MP", SqlDbType.Int);
                            var kod = cmd.Parameters.Add("@Kod", SqlDbType.NVarChar, 20);
                            for (int i = 0; i < redovi.Count; i++)
                            {
                                var r = redovi[i];
                                korak = "Red " + r.BrojReda + (r.List != "" ? " (list " + r.List + ")" : "") +
                                        ", ID paketa " + r.PaketID + ", količina " + r.Kolicina;
                                mp.Value = r.PaketID;
                                kod.Value = r.Kolicina;
                                cmd.ExecuteNonQuery();
                                if ((i + 1) % 25 == 0 || i + 1 == redovi.Count) napredak.Report(i + 1);
                            }
                        }

                        korak = "Obračun (MPKalkulatorCenaTroskovi)";
                        using (var cmd = Procedura(conn, tr, "MPKalkulatorCenaTroskovi"))
                            cmd.ExecuteNonQuery();
                        tr.Commit();
                    }
                    catch (Exception ex)
                    {
                        try { if (tr.Connection != null) tr.Rollback(); }
                        catch (InvalidOperationException) { /* veza je prekinuta; server sam vraca transakciju */ }
                        throw new Exception("Upis je vraćen, postojeći spisak nije menjan.\n\n" + korak + ":\n" + ex.Message, ex);
                    }
                }
            }
        }
    }

    // ZAH-011: izvoz tekuceg prikaza grida (vidljive kolone, redovi posle filtera i sortiranja).
    public static class IzvozTroskova
    {
        static readonly CultureInfo Srpski = CultureInfo.GetCultureInfo("sr-Latn-RS");

        public static string ImeFajla(string nastavak, DateTime vreme)
        {
            return "Troskovni_obracun_" + vreme.ToString("yyyyMMdd_HHmm") + nastavak;
        }

        static bool Decimalni(object v)
        {
            return v is decimal || v is double || v is float;
        }

        // ; kao razdvajac, UTF-8 sa BOM (Excel tako ispravno prikazuje š č ć ž đ), brojevi 1.234,50
        public static void Csv(string fajl, IList<KeyValuePair<string, string>> kolone, IList<DataRow> redovi)
        {
            using (var w = new StreamWriter(fajl, false, new UTF8Encoding(true)))
            {
                w.WriteLine(string.Join(";", kolone.Select(k => Polje(k.Value))));
                foreach (var r in redovi)
                    w.WriteLine(string.Join(";", kolone.Select(k => Polje(Vrednost(r[k.Key])))));
            }
        }

        static string Vrednost(object v)
        {
            if (v == null || v == DBNull.Value) return "";
            if (Decimalni(v)) return ((IFormattable)v).ToString("#,##0.00##", Srpski);
            if (v is DateTime)
            {
                var d = (DateTime)v;
                return d.TimeOfDay == TimeSpan.Zero ? d.ToString("dd.MM.yyyy") : d.ToString("dd.MM.yyyy HH:mm");
            }
            if (v is bool) return (bool)v ? "Da" : "Ne";
            return Convert.ToString(v, Srpski);   // celi brojevi bez razdvajaca hiljada (ID, sifre)
        }

        static string Polje(string s)
        {
            if (s.IndexOfAny(new[] { ';', '"', '\r', '\n' }) < 0) return s;
            return "\"" + s.Replace("\"", "\"\"") + "\"";
        }

        public static void Excel(string fajl, IList<KeyValuePair<string, string>> kolone, IList<DataRow> redovi)
        {
            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Troskovni obracun");
                for (int k = 0; k < kolone.Count; k++)
                {
                    var n = ws.Cell(1, k + 1);
                    n.Value = kolone[k].Value;
                    n.Style.Font.Bold = true;
                    n.Style.Font.FontColor = XLColor.White;
                    n.Style.Fill.BackgroundColor = XLColor.FromArgb(58, 77, 57);
                }
                for (int r = 0; r < redovi.Count; r++)
                {
                    for (int k = 0; k < kolone.Count; k++)
                    {
                        object v = redovi[r][kolone[k].Key];
                        if (v == null || v == DBNull.Value) continue;
                        var c = ws.Cell(r + 2, k + 1);
                        if (Decimalni(v)) { c.Value = Convert.ToDecimal(v); c.Style.NumberFormat.Format = "#,##0.00"; }
                        else if (v is int || v is long || v is short || v is byte) c.Value = Convert.ToDecimal(v);
                        else if (v is DateTime) { c.Value = (DateTime)v; c.Style.NumberFormat.Format = "dd.MM.yyyy HH:mm"; }
                        else if (v is bool) c.Value = (bool)v;
                        else c.Value = v.ToString();
                    }
                }
                ws.SheetView.FreezeRows(1);
                ws.Range(1, 1, Math.Max(1, redovi.Count + 1), Math.Max(1, kolone.Count)).SetAutoFilter();
                ws.Columns().AdjustToContents();
                wb.SaveAs(fajl);
            }
        }
    }
}
