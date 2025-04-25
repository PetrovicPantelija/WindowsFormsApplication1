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
    class InsertKoeficijentRada
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public void InsKoeficijentRada(int LargoBroj, int Koeficijent, DateTime MesecKoeficijenta)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertKoeficijentRada";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter largoBroj = new SqlParameter();
            largoBroj.ParameterName = "@LargoBroj";
            largoBroj.SqlDbType = SqlDbType.Int;
            largoBroj.Direction = ParameterDirection.Input;
            largoBroj.Value = LargoBroj;
            cmd.Parameters.Add(largoBroj);

            SqlParameter koeficijent = new SqlParameter();
            koeficijent.ParameterName = "@Koeficijent";
            koeficijent.SqlDbType = SqlDbType.Int;
            koeficijent.Direction = ParameterDirection.Input;
            koeficijent.Value = Koeficijent;
            cmd.Parameters.Add(koeficijent);

            SqlParameter mesecKoeficijenta = new SqlParameter();
            mesecKoeficijenta.ParameterName = "@MesecKoeficijenta";
            mesecKoeficijenta.SqlDbType = SqlDbType.DateTime;
            mesecKoeficijenta.Direction = ParameterDirection.Input;
            mesecKoeficijenta.Value = MesecKoeficijenta;
            cmd.Parameters.Add(mesecKoeficijenta);

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
                throw new Exception("Neuspesan upis koeficijent rada");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Unos koeficijent rada je uspesno zavrsen", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
                if (error)
                {

                }
            }
        }


        public void UpdKoeficijentRada(int ID, int Koeficijent, DateTime MesecKoeficijenta)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateKoeficijentRada";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter koeficijent = new SqlParameter();
            koeficijent.ParameterName = "@Koeficijent";
            koeficijent.SqlDbType = SqlDbType.Int;
            koeficijent.Direction = ParameterDirection.Input;
            koeficijent.Value = Koeficijent;
            cmd.Parameters.Add(koeficijent);

            SqlParameter mesecKoeficijenta = new SqlParameter();
            mesecKoeficijenta.ParameterName = "@MesecKoeficijenta";
            mesecKoeficijenta.SqlDbType = SqlDbType.DateTime;
            mesecKoeficijenta.Direction = ParameterDirection.Input;
            mesecKoeficijenta.Value = MesecKoeficijenta;
            cmd.Parameters.Add(mesecKoeficijenta);

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
                throw new Exception("Neuspesan upis koeficejnta rada");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Unos koeficejnta je uspesno zavrsen", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
                if (error) { }
            }
        }

        public void DelKoeficijentRada(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteKoeficijentRada";
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
                throw new Exception("Neuspesano brisanje koeficijenta");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Brisanje je uspesno", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
                if (error) { }
            }
        }
    }
}
