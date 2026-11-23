using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Syncfusion.XlsIO;

namespace WindowsFormsApplication1.Magacin
{
    internal sealed class ImportInsert
    {
        public Dictionary<string, object> Values;
        public bool Active;
    }

    internal sealed class ImportUpdate
    {
        public string BarCode;
        public Dictionary<string, object> Values;   // samo polja koja su se promenila
        public bool? Active;                         // samo ako se razlikuje
    }

    internal sealed class ImportPlan
    {
        public string Fatal;                         // ako nije moguce uopste nastaviti
        public int TotalRows;
        public int Unchanged;
        public int Skipped;
        public List<ImportInsert> Inserts = new List<ImportInsert>();
        public List<ImportUpdate> Updates = new List<ImportUpdate>();
        public List<string> Errors = new List<string>();
    }

    /// <summary>
    /// Import iz Excela: prvi red su nazivi kolona (isti kao u tabeli, redosled nije bitan).
    /// Po BarCode-u: nema ga u tabeli -> novi zapis; ima ga -> poredi se ostala polja i menja samo ako ima razlike.
    /// Kolone koje nisu u fajlu se ne diraju. Kolona Active je opciona.
    /// </summary>
    internal static class JelaProductImport
    {
        // ------------------------------------------------------------ 1) priprema (nista se ne upisuje)

        public static ImportPlan Build(string cs, string path)
        {
            string[] header;
            List<string[]> rows = ReadSheet(path, out header);
            return Build(cs, header, rows);
        }

        /// <summary>Isto kao Excel, ali iz CSV fajla (prvi red su nazivi kolona; razdvajac i kodna strana se prepoznaju).</summary>
        public static ImportPlan BuildFromCsv(string cs, string path)
        {
            string[] header;
            List<string[]> rows = ReadCsv(path, out header);
            if (header == null)
                return new ImportPlan { Fatal = "CSV fajl je prazan." };
            return Build(cs, header, rows);
        }

        private static ImportPlan Build(string cs, string[] header, List<string[]> rows)
        {
            var plan = new ImportPlan();

            var col = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < header.Length; i++)
            {
                string h = (header[i] ?? "").Trim();
                if (h.Length > 0 && !col.ContainsKey(h)) col[h] = i;
            }

            var missing = JelaProductRepo.Fields
                .Where(f => f.Required && !col.ContainsKey(f.Name))
                .Select(f => f.Name).ToList();
            if (missing.Count > 0)
            {
                plan.Fatal = "U fajlu nedostaju obavezne kolone: " + string.Join(", ", missing) +
                             ".\nPrvi red mora da sadrzi nazive kolona iz tabele.";
                return plan;
            }

            int activeCol;
            if (!col.TryGetValue("Active", out activeCol)) activeCol = -1;

            var present = JelaProductRepo.Fields.Where(f => col.ContainsKey(f.Name)).ToList();
            var existing = JelaProductRepo.LoadAll(cs);
            var seen = new HashSet<string>(StringComparer.Ordinal);

            for (int r = 0; r < rows.Count; r++)
            {
                string[] cells = rows[r];
                int excelRow = r + 2;
                if (cells.All(c => string.IsNullOrWhiteSpace(c))) continue;   // prazan red
                plan.TotalRows++;

                var values = new Dictionary<string, object>();
                var errs = new List<string>();
                foreach (var f in present)
                {
                    string raw = col[f.Name] < cells.Length ? cells[col[f.Name]] : "";
                    object v;
                    string err;
                    if (JelaProductRepo.TryParse(f, raw, out v, out err)) values[f.Name] = v;
                    else errs.Add(f.Name + ": " + err);
                }

                bool? fileActive = null;
                if (activeCol >= 0)
                {
                    string a = (activeCol < cells.Length ? cells[activeCol] : "").Trim().ToLowerInvariant();
                    if (a.Length > 0)
                    {
                        if (a == "1" || a == "true" || a == "da" || a == "yes" || a == "x") fileActive = true;
                        else if (a == "0" || a == "false" || a == "ne" || a == "no") fileActive = false;
                        else errs.Add("Active: neocekivana vrednost '" + a + "'");
                    }
                }

                if (errs.Count > 0)
                {
                    plan.Skipped++;
                    plan.Errors.Add("Red " + excelRow + ": " + string.Join("; ", errs));
                    continue;
                }

                string barCode = (string)values[JelaProductRepo.Key];
                if (!seen.Add(barCode))
                {
                    plan.Skipped++;
                    plan.Errors.Add("Red " + excelRow + ": BarCode " + barCode + " se ponavlja u fajlu (preskoceno)");
                    continue;
                }

                Dictionary<string, object> cur;
                if (!existing.TryGetValue(barCode, out cur))
                {
                    plan.Inserts.Add(new ImportInsert { Values = values, Active = fileActive ?? true });
                    continue;
                }

                var changed = new Dictionary<string, object>();
                foreach (var f in present)
                {
                    if (f.Name == JelaProductRepo.Key) continue;
                    if (!JelaProductRepo.Same(values[f.Name], cur[f.Name])) changed[f.Name] = values[f.Name];
                }
                bool? act = null;
                if (fileActive.HasValue && fileActive.Value != (bool)cur["Active"]) act = fileActive;

                if (changed.Count > 0 || act.HasValue)
                    plan.Updates.Add(new ImportUpdate { BarCode = barCode, Values = changed, Active = act });
                else
                    plan.Unchanged++;
            }
            return plan;
        }

        // ------------------------------------------------------------ 2) upis (jedna transakcija)

        public static void Apply(string cs, ImportPlan plan)
        {
            using (var cn = new SqlConnection(cs))
            {
                cn.Open();
                using (SqlTransaction tx = cn.BeginTransaction())
                {
                    try
                    {
                        foreach (var ins in plan.Inserts)
                            JelaProductRepo.Insert(cn, tx, ins.Values, ins.Active);
                        foreach (var upd in plan.Updates)
                            JelaProductRepo.Update(cn, tx, upd.BarCode, upd.Values, upd.Active);
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        // ------------------------------------------------------------ citanje Excela (Syncfusion XlsIO)

        /// <summary>Prvi radni list. Vraca redove posle zaglavlja kao tekst; brojevi bez naucnog zapisa.</summary>
        private static List<string[]> ReadSheet(string path, out string[] header)
        {
            var rows = new List<string[]>();
            using (var engine = new ExcelEngine())
            {
                IApplication app = engine.Excel;
                app.DefaultVersion = ExcelVersion.Xlsx;

                // FileShare.ReadWrite: radi i kada je fajl istovremeno otvoren u Excelu
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    IWorkbook wb = app.Workbooks.Open(fs);
                    IWorksheet ws = wb.Worksheets[0];

                    int lastRow = ws.UsedRange.LastRow;
                    int lastCol = ws.UsedRange.LastColumn;

                    header = new string[lastCol];
                    for (int c = 1; c <= lastCol; c++) header[c - 1] = CellText(ws.Range[1, c]);

                    for (int r = 2; r <= lastRow; r++)
                    {
                        var line = new string[lastCol];
                        for (int c = 1; c <= lastCol; c++) line[c - 1] = CellText(ws.Range[r, c]);
                        rows.Add(line);
                    }
                    wb.Close();
                }
            }
            return rows;
        }

        // ------------------------------------------------------------ citanje CSV-a

        /// <summary>
        /// CSV: razdvajac je ; , ili tab (onaj kog u prvom redu ima najvise), polja pod navodnicima mogu sadrzati
        /// razdvajac, "" i prelom reda. Kodna strana: UTF-8 (sa ili bez BOM-a), a ako fajl nije ispravan UTF-8 - Windows-1250.
        /// </summary>
        private static List<string[]> ReadCsv(string path, out string[] header)
        {
            byte[] bytes;
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var ms = new MemoryStream())
            {
                fs.CopyTo(ms);
                bytes = ms.ToArray();
            }

            string text;
            try
            {
                text = new UTF8Encoding(false, true).GetString(bytes);
            }
            catch (DecoderFallbackException)
            {
                text = Encoding.GetEncoding(1250).GetString(bytes);
            }
            if (text.Length > 0 && text[0] == '﻿') text = text.Substring(1);

            List<string[]> all = ParseCsv(text, DetectDelimiter(text));
            header = all.Count > 0 ? all[0].Select(h => h.Trim()).ToArray() : null;
            if (all.Count > 0) all.RemoveAt(0);
            return all;
        }

        private static char DetectDelimiter(string text)
        {
            int[] count = new int[3];
            char[] cand = { ';', ',', '\t' };
            bool quoted = false;
            foreach (char ch in text)
            {
                if (ch == '"') quoted = !quoted;
                else if (!quoted && (ch == '\n' || ch == '\r')) break;
                else if (!quoted) for (int i = 0; i < cand.Length; i++) if (ch == cand[i]) count[i]++;
            }
            int best = 0;
            for (int i = 1; i < cand.Length; i++) if (count[i] > count[best]) best = i;
            return cand[best];
        }

        private static List<string[]> ParseCsv(string text, char delim)
        {
            var rows = new List<string[]>();
            var line = new List<string>();
            var sb = new StringBuilder();
            bool quoted = false;

            for (int i = 0; i < text.Length; i++)
            {
                char ch = text[i];
                if (quoted)
                {
                    if (ch == '"')
                    {
                        if (i + 1 < text.Length && text[i + 1] == '"') { sb.Append('"'); i++; }
                        else quoted = false;
                    }
                    else sb.Append(ch);
                }
                else if (ch == '"') quoted = true;
                else if (ch == delim) { line.Add(sb.ToString()); sb.Clear(); }
                else if (ch == '\r' || ch == '\n')
                {
                    if (ch == '\r' && i + 1 < text.Length && text[i + 1] == '\n') i++;
                    line.Add(sb.ToString());
                    sb.Clear();
                    rows.Add(line.ToArray());
                    line.Clear();
                }
                else sb.Append(ch);
            }
            if (sb.Length > 0 || line.Count > 0)
            {
                line.Add(sb.ToString());
                rows.Add(line.ToArray());
            }
            return rows;
        }

        private static string CellText(IRange cell)
        {
            if (cell.HasNumber)   // npr. BarCode upisan kao broj: 8600597100224, ne 8.6E+12
                return cell.Number.ToString("0.###############", CultureInfo.InvariantCulture);
            return (cell.DisplayText ?? "").Trim();
        }
    }
}
