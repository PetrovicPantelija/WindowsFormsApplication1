using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    // Prava po formi, isto kao u ostalim formama: KorisnikGrupa -> GrupaForma za Forme.Code.
    // Forma se upisuje u Forme skriptom SQL/IC_Cenovnici/07_Forme_prava.sql,
    // a grupe joj dodeljuje administrator u frmDodeliPravo.
    public class PravaForme
    {
        public bool Pravo { get; private set; }
        public bool Upis { get; private set; }
        public bool Izmena { get; private set; }
        public bool Brisanje { get; private set; }

        public static string Korisnik
        {
            get { return frmLogovanje.user.ToString().TrimEnd(); }
        }

        public PravaForme(string code)
        {
            string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            using (var conn = new SqlConnection(connect))
            using (var cmd = new SqlCommand(
                @"SELECT gf.Upis, gf.Izmena, gf.Brisanje
                  FROM GrupaForma gf
                  JOIN Forme f ON f.IdForme = gf.IdForme
                  WHERE RTRIM(f.Code) = @Code
                    AND gf.IdGrupe IN (SELECT IdGrupe FROM KorisnikGrupa WHERE RTRIM(Korisnik) = @Korisnik)", conn))
            {
                cmd.Parameters.AddWithValue("@Code", code);
                cmd.Parameters.AddWithValue("@Korisnik", Korisnik);
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Pravo = true;
                        Upis |= Convert.ToBoolean(dr["Upis"]);
                        Izmena |= Convert.ToBoolean(dr["Izmena"]);
                        Brisanje |= Convert.ToBoolean(dr["Brisanje"]);
                    }
                }
            }
            if (!Pravo)
                MessageBox.Show("Nemate prava za pristup ovoj formi", code);
        }
    }
}
