using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace WindowsFormsApplication1.Magacin
{
    internal enum JelaType { Text, Int, Decimal }

    /// <summary>Opis jedne kolone tabele dbo.jela_products.</summary>
    internal sealed class JelaField
    {
        public string Name;
        public JelaType Type;
        public SqlDbType Db;
        public int Size;        // maksimalna duzina za tekst
        public byte Precision;  // za decimal
        public byte Scale;      // za decimal
        public bool Required;   // NOT NULL
    }

    /// <summary>Pristup tabeli dbo.jela_products (svi upiti su parametrizovani).</summary>
    internal static class JelaProductRepo
    {
        private static JelaField T(string n, SqlDbType db, int size, bool req)
        {
            return new JelaField { Name = n, Type = JelaType.Text, Db = db, Size = size, Required = req };
        }
        private static JelaField I(string n, bool req)
        {
            return new JelaField { Name = n, Type = JelaType.Int, Db = SqlDbType.Int, Required = req };
        }
        private static JelaField D(string n, byte precision, byte scale)
        {
            return new JelaField { Name = n, Type = JelaType.Decimal, Db = SqlDbType.Decimal, Precision = precision, Scale = scale };
        }

        /// <summary>Sva polja osim Active, redosledom iz tabele.</summary>
        public static readonly JelaField[] Fields =
        {
            I("ProizvodID", true),
            T("Code", SqlDbType.NVarChar, 50, true),
            T("BarCode", SqlDbType.VarChar, 20, true),                 // primarni kljuc
            T("SifraProizvodaLargo", SqlDbType.NVarChar, 100, true),
            T("NazivRobneGrupe", SqlDbType.NVarChar, 100, true),
            T("NazivGrupe", SqlDbType.NVarChar, 200, true),
            T("NazivProizvoda", SqlDbType.NVarChar, 200, true),
            I("KorpusID", false),
            I("FronID", false),
            T("KorpusOpis", SqlDbType.NVarChar, 50, false),
            T("FrontOpis", SqlDbType.NVarChar, 50, false),
            I("BrojPaketa", false),
            D("Bruto", 10, 3),
            D("Neto", 10, 3),
            T("DeklaracijaDimenzija", SqlDbType.NVarChar, 100, false),
            D("price_rs_WP", 18, 2),
            D("price_WP", 18, 2),
            I("Kompleta", false),
            T("Jci", SqlDbType.VarChar, 20, false)
        };

        public const string Key = "BarCode";

        private static string Cols
        {
            get { return string.Join(", ", Fields.Select(f => "[" + f.Name + "]")); }
        }

        // ---------------------------------------------------------------- citanje

        /// <summary>Za prikaz u gridu: Active je prva kolona da se vidi bez skrolovanja.</summary>
        public static DataTable Load(string cs, bool includeInactive)
        {
            string sql = "SELECT [Active], " + Cols + " FROM dbo.jela_products " +
                         (includeInactive ? "" : "WHERE [Active] = 1 ") +
                         "ORDER BY [ProizvodID], [SifraProizvodaLargo];";
            var table = new DataTable();
            using (var cn = new SqlConnection(cs))
            using (var da = new SqlDataAdapter(sql, cn))
                da.Fill(table);
            return table;
        }

        /// <summary>Svi zapisi (i neaktivni) po BarCode-u; vrednost null = SQL NULL; kljuc "Active" je bool.</summary>
        public static Dictionary<string, Dictionary<string, object>> LoadAll(string cs)
        {
            var result = new Dictionary<string, Dictionary<string, object>>(StringComparer.Ordinal);
            using (var cn = new SqlConnection(cs))
            using (var cmd = new SqlCommand("SELECT " + Cols + ", [Active] FROM dbo.jela_products;", cn))
            {
                cn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var d = new Dictionary<string, object>();
                        foreach (var f in Fields)
                        {
                            object v = r[f.Name];
                            d[f.Name] = v == DBNull.Value ? null : v;
                        }
                        d["Active"] = (bool)r["Active"];
                        result[(string)d[Key]] = d;
                    }
                }
            }
            return result;
        }

        // ---------------------------------------------------------------- upis

        private static SqlParameter P(JelaField f, object value, string name)
        {
            var p = new SqlParameter(name, f.Db);
            if (f.Type == JelaType.Text) p.Size = f.Size;
            if (f.Type == JelaType.Decimal) { p.Precision = f.Precision; p.Scale = f.Scale; }
            p.Value = value ?? DBNull.Value;
            return p;
        }

        public static JelaField Field(string name)
        {
            return Fields.First(f => f.Name == name);
        }

        /// <summary>Unos novog zapisa; u values idu samo polja koja treba upisati (ostala ostaju NULL).</summary>
        public static void Insert(SqlConnection cn, SqlTransaction tx, Dictionary<string, object> values, bool active)
        {
            var used = Fields.Where(f => values.ContainsKey(f.Name)).ToList();
            string sql = "INSERT INTO dbo.jela_products (" +
                         string.Join(", ", used.Select(f => "[" + f.Name + "]")) + ", [Active]) VALUES (" +
                         string.Join(", ", used.Select(f => "@" + f.Name)) + ", @Active);";
            using (var cmd = new SqlCommand(sql, cn, tx))
            {
                foreach (var f in used) cmd.Parameters.Add(P(f, values[f.Name], "@" + f.Name));
                cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = active;
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>Izmena po BarCode-u; menjaju se samo polja iz values (BarCode se ne menja) i Active ako je zadat.</summary>
        public static int Update(SqlConnection cn, SqlTransaction tx, string barCode, Dictionary<string, object> values, bool? active)
        {
            var used = Fields.Where(f => f.Name != Key && values.ContainsKey(f.Name)).ToList();
            var sets = used.Select(f => "[" + f.Name + "] = @" + f.Name).ToList();
            if (active.HasValue) sets.Add("[Active] = @Active");
            if (sets.Count == 0) return 0;

            string sql = "UPDATE dbo.jela_products SET " + string.Join(", ", sets) + " WHERE [BarCode] = @BarCodeKey;";
            using (var cmd = new SqlCommand(sql, cn, tx))
            {
                foreach (var f in used) cmd.Parameters.Add(P(f, values[f.Name], "@" + f.Name));
                if (active.HasValue) cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = active.Value;
                cmd.Parameters.Add(P(Field(Key), barCode, "@BarCodeKey"));
                return cmd.ExecuteNonQuery();
            }
        }

        public static void Insert(string cs, Dictionary<string, object> values, bool active)
        {
            using (var cn = new SqlConnection(cs))
            {
                cn.Open();
                Insert(cn, null, values, active);
            }
        }

        public static void Update(string cs, string barCode, Dictionary<string, object> values, bool active)
        {
            using (var cn = new SqlConnection(cs))
            {
                cn.Open();
                Update(cn, null, barCode, values, active);
            }
        }

        public static void SetActive(string cs, string barCode, bool active)
        {
            using (var cn = new SqlConnection(cs))
            using (var cmd = new SqlCommand("UPDATE dbo.jela_products SET [Active] = @Active WHERE [BarCode] = @BarCode;", cn))
            {
                cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = active;
                cmd.Parameters.Add(P(Field(Key), barCode, "@BarCode"));
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static bool IsDuplicateKey(SqlException ex)
        {
            return ex.Number == 2627 || ex.Number == 2601;
        }

        // ---------------------------------------------------------------- pretvaranje teksta u vrednost

        /// <summary>
        /// Pretvara tekst u vrednost polja. Prazno ili "NULL" daje null (greska ako je polje obavezno).
        /// Decimalni zarez i tacka su oba dozvoljeni; decimale se zaokruzuju na skalu kolone.
        /// </summary>
        public static bool TryParse(JelaField f, string raw, out object value, out string error)
        {
            value = null;
            error = null;
            string s = (raw ?? "").Trim();

            if (s.Length == 0 || s.Equals("NULL", StringComparison.OrdinalIgnoreCase))
            {
                if (f.Required) { error = "obavezno polje"; return false; }
                return true;
            }

            switch (f.Type)
            {
                case JelaType.Text:
                    if (s.Length > f.Size) { error = "predugacak tekst (" + s.Length + " > " + f.Size + ")"; return false; }
                    value = s;
                    return true;

                case JelaType.Int:
                    decimal whole;
                    if (!TryDecimal(s, out whole) || whole != Math.Truncate(whole) ||
                        whole > int.MaxValue || whole < int.MinValue)
                    { error = "nije ceo broj: " + s; return false; }
                    value = (int)whole;
                    return true;

                default:
                    decimal d;
                    if (!TryDecimal(s, out d)) { error = "nije broj: " + s; return false; }
                    d = Math.Round(d, f.Scale, MidpointRounding.AwayFromZero);
                    if (Math.Abs(d) >= Pow10(f.Precision - f.Scale))
                    { error = "broj je prevelik: " + s; return false; }
                    value = d;
                    return true;
            }
        }

        private static decimal Pow10(int n)
        {
            decimal r = 1;
            for (int i = 0; i < n; i++) r *= 10;
            return r;
        }

        private static bool TryDecimal(string s, out decimal d)
        {
            s = s.Replace(" ", "");
            if (s.Contains(",") && !s.Contains(".")) s = s.Replace(',', '.');   // 48,6 -> 48.6
            else if (s.Contains(",")) s = s.Replace(",", "");                    // 1,234.50 -> 1234.50
            return decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out d);
        }

        /// <summary>Isti sadrzaj? (null = null, tekst poredjen ordinalno, brojevi po vrednosti)</summary>
        public static bool Same(object a, object b)
        {
            bool an = a == null || a == DBNull.Value;
            bool bn = b == null || b == DBNull.Value;
            if (an || bn) return an && bn;
            if (a is string) return string.Equals((string)a, b as string, StringComparison.Ordinal);
            if (a is decimal) return (decimal)a == Convert.ToDecimal(b, CultureInfo.InvariantCulture);
            return Convert.ToInt32(a, CultureInfo.InvariantCulture) == Convert.ToInt32(b, CultureInfo.InvariantCulture);
        }

        public static string ToText(object v)
        {
            if (v == null || v == DBNull.Value) return "";
            if (v is decimal) return ((decimal)v).ToString("0.###", CultureInfo.InvariantCulture);
            return Convert.ToString(v, CultureInfo.InvariantCulture);
        }
    }
}
