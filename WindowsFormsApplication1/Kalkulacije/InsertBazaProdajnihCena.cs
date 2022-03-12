using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Kalkulacije
{
    class InsertBazaProdajnihCena
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public void InsBazaProdajnihCena(int MpSifra, string Slika, string Kod, string MpNaziv, decimal Sirina, decimal Visina,
            decimal Duzina, string OpisProizvodnje, string VrsteMaterijala, decimal KG, decimal Koleto, decimal M3, decimal Cena)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertBazaProdajnihCenovnika";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter mpSifra = new SqlParameter();
            mpSifra.ParameterName = "@MpSifra";
            mpSifra.SqlDbType = SqlDbType.Int;
            mpSifra.Direction = ParameterDirection.Input;
            mpSifra.Value = MpSifra;
            cmd.Parameters.Add(mpSifra);

            SqlParameter slika = new SqlParameter();
            slika.ParameterName = "@Slika";
            slika.SqlDbType = SqlDbType.NVarChar;
            slika.Size = 500;
            slika.Direction = ParameterDirection.Input;
            slika.Value = Slika;
            cmd.Parameters.Add(slika);

            SqlParameter kod = new SqlParameter();
            kod.ParameterName = "@Kod";
            kod.SqlDbType = SqlDbType.NVarChar;
            kod.Size = 60;
            kod.Direction = ParameterDirection.Input;
            kod.Value = Kod;
            cmd.Parameters.Add(kod);

            SqlParameter mpNaziv = new SqlParameter();
            mpNaziv.ParameterName = "@MpNaziv";
            mpNaziv.SqlDbType = SqlDbType.NVarChar;
            mpNaziv.Size = 100;
            mpNaziv.Direction = ParameterDirection.Input;
            mpNaziv.Value = MpNaziv;
            cmd.Parameters.Add(mpNaziv);

            SqlParameter sirina = new SqlParameter();
            sirina.ParameterName = "@Sirina";
            sirina.SqlDbType = SqlDbType.Decimal;
            sirina.Direction = ParameterDirection.Input;
            sirina.Value = Sirina;
            cmd.Parameters.Add(sirina);

            SqlParameter visina = new SqlParameter();
            visina.ParameterName = "@Visina";
            visina.SqlDbType = SqlDbType.Decimal;
            visina.Direction = ParameterDirection.Input;
            visina.Value = Visina;
            cmd.Parameters.Add(visina);

            SqlParameter duzina = new SqlParameter();
            duzina.ParameterName = "@Duzina";
            duzina.SqlDbType = SqlDbType.Decimal;
            duzina.Direction = ParameterDirection.Input;
            duzina.Value = Duzina;
            cmd.Parameters.Add(duzina);

            SqlParameter opis = new SqlParameter();
            opis.ParameterName = "@OpisProizvodnje";
            opis.SqlDbType = SqlDbType.NVarChar;
            opis.Size = 200;
            opis.Direction = ParameterDirection.Input;
            opis.Value = OpisProizvodnje;
            cmd.Parameters.Add(opis);

            SqlParameter vrste = new SqlParameter();
            vrste.ParameterName = "@VrsteMaterijala";
            vrste.SqlDbType = SqlDbType.NVarChar;
            vrste.Size = 200;
            vrste.Direction = ParameterDirection.Input;
            vrste.Value = VrsteMaterijala;
            cmd.Parameters.Add(vrste);

            SqlParameter kg = new SqlParameter();
            kg.ParameterName = "@KG";
            kg.SqlDbType = SqlDbType.Decimal;
            kg.Direction = ParameterDirection.Input;
            kg.Value = KG;
            cmd.Parameters.Add(kg);

            SqlParameter koleto = new SqlParameter();
            koleto.ParameterName = "@Koleto";
            koleto.SqlDbType = SqlDbType.Decimal;
            koleto.Direction = ParameterDirection.Input;
            koleto.Value = Koleto;
            cmd.Parameters.Add(koleto);

            SqlParameter m = new SqlParameter();
            m.ParameterName = "@M3";
            m.SqlDbType = SqlDbType.Decimal;
            m.Direction = ParameterDirection.Input;
            m.Value = M3;
            cmd.Parameters.Add(m);

            SqlParameter cena = new SqlParameter();
            cena.ParameterName = "@Cena";
            cena.SqlDbType = SqlDbType.Decimal;
            cena.Direction = ParameterDirection.Input;
            cena.Value = Cena;
            cmd.Parameters.Add(cena);

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
                throw new Exception("Neuspesan upis NHM brojeva");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Unos NHM broja je uspesno zavrsena", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
                if (error)
                {

                }
            }
        }
        public void UpdBazaProdajnihCena(int ID, int MpSifra, string Slika, string Kod, string MpNaziv, decimal Sirina, decimal Visina,
            decimal Duzina, string OpisProizvodnje, string VrsteMaterijala, decimal KG, decimal Koleto, decimal M3, decimal Cena)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateBazaProdajnihCenovnika";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter mpSifra = new SqlParameter();
            mpSifra.ParameterName = "@MpSifra";
            mpSifra.SqlDbType = SqlDbType.Int;
            mpSifra.Direction = ParameterDirection.Input;
            mpSifra.Value = MpSifra;
            cmd.Parameters.Add(mpSifra);

            SqlParameter slika = new SqlParameter();
            slika.ParameterName = "@Slika";
            slika.SqlDbType = SqlDbType.NVarChar;
            slika.Size = 500;
            slika.Direction = ParameterDirection.Input;
            slika.Value = Slika;
            cmd.Parameters.Add(slika);

            SqlParameter kod = new SqlParameter();
            kod.ParameterName = "@Kod";
            kod.SqlDbType = SqlDbType.NVarChar;
            kod.Size = 60;
            kod.Direction = ParameterDirection.Input;
            kod.Value = Kod;
            cmd.Parameters.Add(kod);

            SqlParameter mpNaziv = new SqlParameter();
            mpNaziv.ParameterName = "@MpNaziv";
            mpNaziv.SqlDbType = SqlDbType.NVarChar;
            mpNaziv.Size = 100;
            mpNaziv.Direction = ParameterDirection.Input;
            mpNaziv.Value = MpNaziv;
            cmd.Parameters.Add(mpNaziv);

            SqlParameter sirina = new SqlParameter();
            sirina.ParameterName = "@Sirina";
            sirina.SqlDbType = SqlDbType.Decimal;
            sirina.Direction = ParameterDirection.Input;
            sirina.Value = Sirina;
            cmd.Parameters.Add(sirina);

            SqlParameter visina = new SqlParameter();
            visina.ParameterName = "@Visina";
            visina.SqlDbType = SqlDbType.Decimal;
            visina.Direction = ParameterDirection.Input;
            visina.Value = Visina;
            cmd.Parameters.Add(visina);

            SqlParameter duzina = new SqlParameter();
            duzina.ParameterName = "@Duzina";
            duzina.SqlDbType = SqlDbType.Decimal;
            duzina.Direction = ParameterDirection.Input;
            duzina.Value = Duzina;
            cmd.Parameters.Add(duzina);

            SqlParameter opis = new SqlParameter();
            opis.ParameterName = "@OpisProizvodnje";
            opis.SqlDbType = SqlDbType.NVarChar;
            opis.Size = 200;
            opis.Direction = ParameterDirection.Input;
            opis.Value = OpisProizvodnje;
            cmd.Parameters.Add(opis);

            SqlParameter vrste = new SqlParameter();
            vrste.ParameterName = "@VrsteMaterijala";
            vrste.SqlDbType = SqlDbType.NVarChar;
            vrste.Size = 200;
            vrste.Direction = ParameterDirection.Input;
            vrste.Value = VrsteMaterijala;
            cmd.Parameters.Add(vrste);

            SqlParameter kg = new SqlParameter();
            kg.ParameterName = "@KG";
            kg.SqlDbType = SqlDbType.Decimal;
            kg.Direction = ParameterDirection.Input;
            kg.Value = KG;
            cmd.Parameters.Add(kg);

            SqlParameter koleto = new SqlParameter();
            koleto.ParameterName = "@Koleto";
            koleto.SqlDbType = SqlDbType.Decimal;
            koleto.Direction = ParameterDirection.Input;
            koleto.Value = Koleto;
            cmd.Parameters.Add(koleto);

            SqlParameter m = new SqlParameter();
            m.ParameterName = "@M3";
            m.SqlDbType = SqlDbType.Decimal;
            m.Direction = ParameterDirection.Input;
            m.Value = M3;
            cmd.Parameters.Add(m);

            SqlParameter cena = new SqlParameter();
            cena.ParameterName = "@Cena";
            cena.SqlDbType = SqlDbType.Decimal;
            cena.Direction = ParameterDirection.Input;
            cena.Value = Cena;
            cmd.Parameters.Add(cena);

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;
            }
            catch (SqlException)
            {
                throw new Exception("Neuspesan upis NHM brojeva");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Unos NHM broja je uspesno zavrsena", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
                if (error)
                {

                }
            }
        }
        public void DelBazaProdajnihCena(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteBazaProdajnihCenovnika";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;
            }
            catch (SqlException)
            {
                throw new Exception("Neuspesan upis NHM brojeva");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Unos NHM broja je uspesno zavrsena", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
                if (error)
                {

                }
            }
        }
    }
}
