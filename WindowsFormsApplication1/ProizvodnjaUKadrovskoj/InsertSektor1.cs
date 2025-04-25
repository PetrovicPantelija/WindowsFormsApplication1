using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.PodaciOZaposlenima
{
    class InsertSektor
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public void InsSektora(string Naziv)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertSektora";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter naziv = new SqlParameter();
            naziv.ParameterName = "@Naziv";
            naziv.SqlDbType = SqlDbType.NVarChar;
            naziv.Size = 100;
            naziv.Direction = ParameterDirection.Input;
            naziv.Value = Naziv;
            cmd.Parameters.Add(naziv);

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
                throw new Exception("Neuspesan upis Sektora");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Unos Sektora je uspesno zavrsen", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
                if (error)
                {

                }
            }
        }

        public void UpdSektora(int ID, string Naziv)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateSektora";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter naziv = new SqlParameter();
            naziv.ParameterName = "@Naziv";
            naziv.SqlDbType = SqlDbType.NVarChar;
            naziv.Size = 100;
            naziv.Direction = ParameterDirection.Input;
            naziv.Value = Naziv;
            cmd.Parameters.Add(naziv);

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
                throw new Exception("Neuspesan upis sektora");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Unos sektora je uspesno zavrsena", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
                if (error) { }
            }
        }

        public void DelSektora(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteSektora";
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
