using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace WindowsFormsApplication1.ProizvodnjaUKadrovskoj
{
    class InsertKadroviSektora
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public void InsKadroviUProizvodnji(int LargoBroj, int IDSektora)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertKadroviUProizvodnji";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter largoBroj = new SqlParameter();
            largoBroj.ParameterName = "@LargoBroj";
            largoBroj.SqlDbType = SqlDbType.Int;
            largoBroj.Direction = ParameterDirection.Input;
            largoBroj.Value = LargoBroj;
            cmd.Parameters.Add(largoBroj);

            SqlParameter iDSektora = new SqlParameter();
            iDSektora.ParameterName = "@IDSektora";
            iDSektora.SqlDbType = SqlDbType.Int;
            iDSektora.Direction = ParameterDirection.Input;
            iDSektora.Value = IDSektora;
            cmd.Parameters.Add(iDSektora);

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
                throw new Exception("Neuspesan upis kadrova u proizvodnji");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Unos kadrova u proizvodnji je uspesno zavrsen", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
                if (error)
                {

                }
            }
        }
        public void DelKadroviUProizvodnji(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteKadroviUProizvodnji";
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
                throw new Exception("Neuspesano brisanje kadrova u proizvodnji");
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
