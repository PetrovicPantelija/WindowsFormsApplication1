using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.ProizvodnjaUKadrovskoj
{
    class InsertEvidencijaRada
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public void InsEvidencijaRada(int LargoBroj, int TipRadaID ,float BrojSati, DateTime Datum,string Napomena)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertEvidencijaRada";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter largoBroj = new SqlParameter();
            largoBroj.ParameterName = "@LargoBroj";
            largoBroj.SqlDbType = SqlDbType.Int;
            largoBroj.Direction = ParameterDirection.Input;
            largoBroj.Value = LargoBroj;
            cmd.Parameters.Add(largoBroj);

            SqlParameter tipRadaID = new SqlParameter();
            tipRadaID.ParameterName = "@TipRadaID";
            tipRadaID.SqlDbType = SqlDbType.Int;
            tipRadaID.Direction = ParameterDirection.Input;
            tipRadaID.Value = TipRadaID;
            cmd.Parameters.Add(tipRadaID);

            SqlParameter brojSati = new SqlParameter();
            brojSati.ParameterName = "@BrojSati";
            brojSati.SqlDbType = SqlDbType.Float;
            brojSati.Direction = ParameterDirection.Input;
            brojSati.Value = BrojSati;
            cmd.Parameters.Add(brojSati);

            SqlParameter datum = new SqlParameter();
            datum.ParameterName = "@Datum";
            datum.SqlDbType = SqlDbType.DateTime;
            datum.Direction = ParameterDirection.Input;
            datum.Value = Datum;
            cmd.Parameters.Add(datum);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = -1;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

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
                throw new Exception("Neuspesan upis evidencija rada");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Unos evidencija rada je uspesno zavrsen", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
                if (error)
                {

                }
            }
        }

        public void UpdEvidencijaRada(int ID,int TipRadaID, float BrojSati, string Napomena)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateEvidencijaRada";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter tipRadaID = new SqlParameter();
            tipRadaID.ParameterName = "@TipRadaID";
            tipRadaID.SqlDbType = SqlDbType.Int;
            tipRadaID.Direction = ParameterDirection.Input;
            tipRadaID.Value = TipRadaID;
            cmd.Parameters.Add(tipRadaID);


            SqlParameter brojSati = new SqlParameter();
            brojSati.ParameterName = "@BrojSati";
            brojSati.SqlDbType = SqlDbType.Float;
            brojSati.Size = 100;
            brojSati.Direction = ParameterDirection.Input;
            brojSati.Value = BrojSati;
            cmd.Parameters.Add(brojSati);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = -1;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

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
                throw new Exception("Neuspesan upis Broja sati");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Unos Broja sati je uspesno zavrsen", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
                if (error) { }
            }
        }

        public void DelEvidencijaRada(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteEvidencijaRada";
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
                throw new Exception("Neuspesano brisanje sektora brojeva");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Brisanje uspesno", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
                if (error) { }
            }
        }
    }
}
