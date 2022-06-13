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
    class InsertKalkulator
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public void InsKalkulator(DateTime Datum,string Naziv,decimal Kurs,int VpCenovnik,int Zavrsen)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertKalkulator";
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

            SqlParameter kurs = new SqlParameter();
            kurs.ParameterName = "@Kurs";
            kurs.SqlDbType = SqlDbType.Decimal;
            kurs.Direction = ParameterDirection.Input;
            kurs.Value = Kurs;
            cmd.Parameters.Add(kurs);

            SqlParameter vpCena = new SqlParameter();
            vpCena.ParameterName = "@VpCenovnik";
            vpCena.SqlDbType = SqlDbType.Int;
            vpCena.Direction = ParameterDirection.Input;
            vpCena.Value = VpCenovnik;
            cmd.Parameters.Add(vpCena);

            SqlParameter zavrsen = new SqlParameter();
            zavrsen.ParameterName = "@Zavrsen";
            zavrsen.SqlDbType = SqlDbType.Int;
            zavrsen.Direction = ParameterDirection.Input;
            zavrsen.Value = Zavrsen;
            cmd.Parameters.Add(zavrsen);

            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = conn.BeginTransaction();
                cmd.Transaction = myTransaction;
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
                throw new Exception("Neuspešan upis NHM brojeva");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos NHM broja je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void UpdKalkulator(int ID, DateTime Datum, string Naziv, decimal Kurs, int VpCenovnik, int Zavrsen)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateKalkulator";
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

            SqlParameter kurs = new SqlParameter();
            kurs.ParameterName = "@Kurs";
            kurs.SqlDbType = SqlDbType.Decimal;
            kurs.Direction = ParameterDirection.Input;
            kurs.Value = Kurs;
            cmd.Parameters.Add(kurs);

            SqlParameter vpCena = new SqlParameter();
            vpCena.ParameterName = "@VpCenovnik";
            vpCena.SqlDbType = SqlDbType.Int;
            vpCena.Direction = ParameterDirection.Input;
            vpCena.Value = VpCenovnik;
            cmd.Parameters.Add(vpCena);

            SqlParameter zavrsen = new SqlParameter();
            zavrsen.ParameterName = "@Zavrsen";
            zavrsen.SqlDbType = SqlDbType.Int;
            zavrsen.Direction = ParameterDirection.Input;
            zavrsen.Value = Zavrsen;
            cmd.Parameters.Add(zavrsen);

            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = conn.BeginTransaction();
                cmd.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis NHM brojeva");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos NHM broja je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void DelKalkulator(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteKalkulator";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = conn.BeginTransaction();
                cmd.Transaction = myTransaction;
            }
            catch (SqlException)
            {
                throw new Exception("Neuspešan upis NHM brojeva");
            }
            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Brisanje uspešno", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void PrekopirajStavke(int Kalkulator, int KalkulatorStari)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PrekopirajStavkeKalkulator";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@Kalkulator";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = Kalkulator;
            cmd.Parameters.Add(id);

            SqlParameter id2 = new SqlParameter();
            id2.ParameterName = "@KalkulatorStari";
            id2.SqlDbType = SqlDbType.Int;
            id2.Direction = ParameterDirection.Input;
            id2.Value = KalkulatorStari;
            cmd.Parameters.Add(id2);

            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = conn.BeginTransaction();
                cmd.Transaction = myTransaction;
            }
            catch (SqlException)
            {
                throw new Exception("Neuspešan upis stavki");
            }
            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Brisanje uspešno", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
    }
}
