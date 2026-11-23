using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WindowsFormsApplication1.Kalkulacije.IzracunCena;

namespace WindowsFormsApplication1.Servis
{
    // ZAH-010: pristup bazi modula Servis (SRV_ tabele, pogledi i procedure iz SQL/Servis).
    // Citanje: parametrizovani upiti nad SRV_vw_ pogledima. Upis: samo procedure, uvek sa @Korisnik.
    // Parametri se zadaju u parovima: "@Ime", vrednost, ... (ili gotov SqlParameter, npr. TVP).
    public class ServisBaza
    {
        readonly string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        const int Timeout = 120;

        public static string Korisnik
        {
            get { return PravaForme.Korisnik; }
        }

        static void Dodaj(SqlCommand cmd, object[] parametri)
        {
            if (parametri == null) return;
            for (int i = 0; i < parametri.Length; i++)
            {
                var gotov = parametri[i] as SqlParameter;
                if (gotov != null) { cmd.Parameters.Add(gotov); continue; }
                string ime = (string)parametri[i];
                object vrednost = i + 1 < parametri.Length ? parametri[i + 1] : null;
                cmd.Parameters.AddWithValue(ime, vrednost ?? DBNull.Value);
                i++;
            }
        }

        public DataTable Upit(string sql, params object[] parametri)
        {
            using (var conn = new SqlConnection(connect))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.CommandTimeout = Timeout;
                Dodaj(cmd, parametri);
                var dt = new DataTable();
                new SqlDataAdapter(cmd).Fill(dt);
                return dt;
            }
        }

        public object Vrednost(string sql, params object[] parametri)
        {
            using (var conn = new SqlConnection(connect))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.CommandTimeout = Timeout;
                Dodaj(cmd, parametri);
                conn.Open();
                object o = cmd.ExecuteScalar();
                return o == DBNull.Value ? null : o;
            }
        }

        // procedura; @Korisnik se dodaje uvek
        public DataSet Proc(string naziv, params object[] parametri)
        {
            using (var conn = new SqlConnection(connect))
            using (var cmd = new SqlCommand(naziv, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = Timeout;
                Dodaj(cmd, parametri);
                cmd.Parameters.AddWithValue("@Korisnik", Korisnik);
                var ds = new DataSet();
                new SqlDataAdapter(cmd).Fill(ds);
                return ds;
            }
        }

        // prvi red prvog rezultata procedure (ID, Broj...) ili null
        public DataRow ProcRed(string naziv, params object[] parametri)
        {
            var ds = Proc(naziv, parametri);
            for (int i = ds.Tables.Count - 1; i >= 0; i--)
                if (ds.Tables[i].Rows.Count > 0) return ds.Tables[i].Rows[0];
            return null;
        }

        public string Podesavanje(string kljuc)
        {
            object o = Vrednost("SELECT Vrednost FROM dbo.SRV_Podesavanje WHERE Kljuc = @K", "@K", kljuc);
            return o == null ? "" : o.ToString();
        }

        // tehnicki tekst greske ide u log, ne korisniku
        public void LogGreske(string tekst)
        {
            try
            {
                Vrednost("INSERT INTO dbo.SRV_Log (Tabela, Opis, Novo, Korisnik) VALUES ('Greska', N'Greška u aplikaciji', @T, @K)",
                         "@T", tekst, "@K", Korisnik);
            }
            catch (Exception)
            {
                System.Diagnostics.Trace.WriteLine(tekst);
            }
        }

        /* ---------------- TVP ---------------- */

        public static SqlParameter Tvp(string naziv, string tip, DataTable vrednost)
        {
            var p = new SqlParameter(naziv, SqlDbType.Structured);
            p.TypeName = tip;
            p.Value = vrednost;
            return p;
        }

        // dbo.SRV_StavkaTip
        public static DataTable Stavke()
        {
            var t = new DataTable();
            t.Columns.Add("DeoID", typeof(int));
            t.Columns.Add("Kolicina", typeof(decimal));
            t.Columns.Add("Cena", typeof(decimal));
            t.Columns.Add("Napomena", typeof(string));
            return t;
        }

        // dbo.SRV_IdTip
        public static DataTable Idovi(IEnumerable<int> ids)
        {
            var t = new DataTable();
            t.Columns.Add("ID", typeof(int));
            var bilo = new HashSet<int>();
            foreach (int id in ids) if (bilo.Add(id)) t.Rows.Add(id);
            return t;
        }

        // dbo.SRV_TekstTip
        public static DataTable Tekstovi(IEnumerable<string> tekstovi)
        {
            var t = new DataTable();
            t.Columns.Add("Tekst", typeof(string));
            foreach (string s in tekstovi) if (!string.IsNullOrWhiteSpace(s)) t.Rows.Add(s.Trim());
            return t;
        }

        // dbo.SRV_PoljeVrednostTip
        public static DataTable PoljaVrednosti()
        {
            var t = new DataTable();
            t.Columns.Add("PoljeID", typeof(int));
            t.Columns.Add("Vrednost", typeof(string));
            return t;
        }

        /* ---------------- sifarnici za liste ---------------- */

        public DataTable Partneri()
        {
            return Upit("SELECT PaSifra AS ID, RTRIM(PaNaziv) + N' (' + CAST(PaSifra AS nvarchar(10)) + N')' AS Naziv FROM dbo.Partnerji ORDER BY PaNaziv");
        }

        public DataTable Delovi()
        {
            return Upit(@"SELECT d.ID, d.Sifra + N' – ' + d.Naziv AS Naziv, d.JM, d.ProsecnaCena, d.Sifra, d.Naziv AS NazivDela
                          FROM dbo.SRV_Deo d WHERE d.Aktivno = 1 ORDER BY d.Sifra");
        }

        public DataTable GrupeDelova()
        {
            return Upit("SELECT ID, Naziv FROM dbo.SRV_DeoGrupa WHERE Aktivno = 1 ORDER BY Naziv");
        }

        public DataTable VrsteSredstava()
        {
            return Upit("SELECT ID, Naziv FROM dbo.SRV_VrstaSredstva WHERE Aktivno = 1 ORDER BY Naziv");
        }

        public DataTable Sredstva()
        {
            return Upit(@"SELECT ID, Oznaka + N' – ' + Naziv AS Naziv, VrstaSredstvaID FROM dbo.SRV_Sredstvo
                          WHERE Aktivno = 1 ORDER BY Oznaka");
        }

        public int NeprocitanaObavestenja()
        {
            object o = Vrednost("SELECT COUNT(*) FROM dbo.SRV_DeoObavestenje WHERE Procitano = 0");
            return o == null ? 0 : Convert.ToInt32(o);
        }
    }
}
