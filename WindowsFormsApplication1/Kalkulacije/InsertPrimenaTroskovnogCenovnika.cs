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
    class InsertPrimenaTroskovnogCenovnika
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public void InsTroskovniCenovnik(int MpSifra, int MpSifraVeznog)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertTroskovniCenovik";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter mpSifra = new SqlParameter();
            mpSifra.ParameterName = "@MpSifra";
            mpSifra.SqlDbType = SqlDbType.Int;
            mpSifra.Direction = ParameterDirection.Input;
            mpSifra.Value = MpSifra;
            cmd.Parameters.Add(mpSifra);

            SqlParameter mpSifraVeznog = new SqlParameter();
            mpSifraVeznog.ParameterName = "@MpSifraVeznog";
            mpSifraVeznog.SqlDbType = SqlDbType.Int;
            mpSifraVeznog.Direction = ParameterDirection.Input;
            mpSifraVeznog.Value = MpSifraVeznog;
            cmd.Parameters.Add(mpSifraVeznog);

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
        public void UpdTroskovniCenovnik(int ID,int MpSifra,int MpSifraVeznog)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateTroskovniCenovnik";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

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

            SqlParameter mpSifraVeznog = new SqlParameter();
            mpSifraVeznog.ParameterName = "@MpSifraVeznog";
            mpSifraVeznog.SqlDbType = SqlDbType.Int;
            mpSifraVeznog.Direction = ParameterDirection.Input;
            mpSifraVeznog.Value = MpSifraVeznog;
            cmd.Parameters.Add(mpSifraVeznog);

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
        public void DelTroskovniCenovnik(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteTroskovnogCenovnika";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

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
    }
}
