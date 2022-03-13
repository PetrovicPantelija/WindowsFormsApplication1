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
    class InsertKamion
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public void InsKamion(DateTime Datum, string Naziv, decimal VrednostKamiona)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertKamion";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter datum = new SqlParameter();
            datum.ParameterName = "@Datum";
            datum.SqlDbType = SqlDbType.Date;
            datum.Direction = ParameterDirection.Input;
            datum.Value = Datum;
            cmd.Parameters.Add(datum);

            SqlParameter naziv = new SqlParameter();
            naziv.ParameterName = "@Naziv";
            naziv.SqlDbType = SqlDbType.NVarChar;
            naziv.Size = 100;
            naziv.Direction = ParameterDirection.Input;
            naziv.Value = Naziv;
            cmd.Parameters.Add(naziv);

            SqlParameter vrednost = new SqlParameter();
            vrednost.ParameterName = "@VrednostKamiona";
            vrednost.SqlDbType = SqlDbType.Decimal;
            vrednost.Direction = ParameterDirection.Input;
            vrednost.Value = VrednostKamiona;
            cmd.Parameters.Add(vrednost);

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
        public void InsKamionStavke(int IDKamiona, int IDKalkulator, int MpSifra, decimal Kolicina, decimal MpCena, decimal VpCena, decimal Ukupno)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertKamionStavke";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idKam = new SqlParameter();
            idKam.ParameterName = "@IDKamiona";
            idKam.SqlDbType = SqlDbType.Int;
            idKam.Direction = ParameterDirection.Input;
            idKam.Value = IDKamiona;
            cmd.Parameters.Add(idKam);

            SqlParameter idKalk = new SqlParameter();
            idKalk.ParameterName = "@IDKalkulator";
            idKalk.SqlDbType = SqlDbType.Int;
            idKalk.Direction = ParameterDirection.Input;
            idKalk.Value = IDKalkulator;
            cmd.Parameters.Add(idKalk);

            SqlParameter mpSifra = new SqlParameter();
            mpSifra.ParameterName = "@MpSifra";
            mpSifra.SqlDbType = SqlDbType.Int;
            mpSifra.Direction = ParameterDirection.Input;
            mpSifra.Value = MpSifra;
            cmd.Parameters.Add(mpSifra);

            SqlParameter kolicina = new SqlParameter();
            kolicina.ParameterName = "@Kolicina";
            kolicina.SqlDbType = SqlDbType.Decimal;
            kolicina.Direction = ParameterDirection.Input;
            kolicina.Value = Kolicina;
            cmd.Parameters.Add(kolicina);

            SqlParameter mp = new SqlParameter();
            mp.ParameterName = "@MpCena";
            mp.SqlDbType = SqlDbType.Decimal;
            mp.Direction = ParameterDirection.Input;
            mp.Value = MpCena;
            cmd.Parameters.Add(mp);

            SqlParameter vp = new SqlParameter();
            vp.ParameterName = "@VpCena";
            vp.SqlDbType = SqlDbType.Decimal;
            vp.Direction = ParameterDirection.Input;
            vp.Value = VpCena;
            cmd.Parameters.Add(vp);

            SqlParameter ukupno = new SqlParameter();
            ukupno.ParameterName = "@Ukupno";
            ukupno.SqlDbType = SqlDbType.Decimal;
            ukupno.Direction = ParameterDirection.Input;
            ukupno.Value = Ukupno;
            cmd.Parameters.Add(ukupno);

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
        public void UpdKamion(int ID, DateTime Datum, string Naziv, decimal VrednostKamiona)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateKamion";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter datum = new SqlParameter();
            datum.ParameterName = "@Datum";
            datum.SqlDbType = SqlDbType.Date;
            datum.Direction = ParameterDirection.Input;
            datum.Value = Datum;
            cmd.Parameters.Add(datum);

            SqlParameter naziv = new SqlParameter();
            naziv.ParameterName = "@Naziv";
            naziv.SqlDbType = SqlDbType.NVarChar;
            naziv.Size = 100;
            naziv.Direction = ParameterDirection.Input;
            naziv.Value = Naziv;
            cmd.Parameters.Add(naziv);

            SqlParameter vrednost = new SqlParameter();
            vrednost.ParameterName = "@VrednostKamiona";
            vrednost.SqlDbType = SqlDbType.Decimal;
            vrednost.Direction = ParameterDirection.Input;
            vrednost.Value = VrednostKamiona;
            cmd.Parameters.Add(vrednost);

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
        public void UpdKamionStavke(int ID, int IDKamiona, int IDKalkulator, int MpSifra, decimal Kolicina, decimal MpCena, decimal VpCena, decimal Ukupno)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateKamionStavke";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter idKam = new SqlParameter();
            idKam.ParameterName = "@IDKamiona";
            idKam.SqlDbType = SqlDbType.Int;
            idKam.Direction = ParameterDirection.Input;
            idKam.Value = IDKamiona;
            cmd.Parameters.Add(idKam);

            SqlParameter idKalk = new SqlParameter();
            idKalk.ParameterName = "@IDKalkulator";
            idKalk.SqlDbType = SqlDbType.Int;
            idKalk.Direction = ParameterDirection.Input;
            idKalk.Value = IDKalkulator;
            cmd.Parameters.Add(idKalk);

            SqlParameter mpSifra = new SqlParameter();
            mpSifra.ParameterName = "@MpSifra";
            mpSifra.SqlDbType = SqlDbType.Int;
            mpSifra.Direction = ParameterDirection.Input;
            mpSifra.Value = MpSifra;
            cmd.Parameters.Add(mpSifra);

            SqlParameter kolicina = new SqlParameter();
            kolicina.ParameterName = "@Kolicina";
            kolicina.SqlDbType = SqlDbType.Decimal;
            kolicina.Direction = ParameterDirection.Input;
            kolicina.Value = Kolicina;
            cmd.Parameters.Add(kolicina);

            SqlParameter mp = new SqlParameter();
            mp.ParameterName = "@MpCena";
            mp.SqlDbType = SqlDbType.Decimal;
            mp.Direction = ParameterDirection.Input;
            mp.Value = MpCena;
            cmd.Parameters.Add(mp);

            SqlParameter vp = new SqlParameter();
            vp.ParameterName = "@VpCena";
            vp.SqlDbType = SqlDbType.Decimal;
            vp.Direction = ParameterDirection.Input;
            vp.Value = VpCena;
            cmd.Parameters.Add(vp);

            SqlParameter ukupno = new SqlParameter();
            ukupno.ParameterName = "@Ukupno";
            ukupno.SqlDbType = SqlDbType.Decimal;
            ukupno.Direction = ParameterDirection.Input;
            ukupno.Value = Ukupno;
            cmd.Parameters.Add(ukupno);

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
        public void DelKamion(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteKamion";
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
        public void DelKamionStavke(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteKamionStavke";
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
