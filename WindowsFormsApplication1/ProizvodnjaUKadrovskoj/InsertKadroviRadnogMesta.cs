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
    class InsertKadroviRadnogMesta
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public void InsKadroviRadnihMesta(int IDRadnoMesto, int LargoBroj, string Napomena)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertKadroviRadnihMesta";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter iDRadnoMesto = new SqlParameter();
            iDRadnoMesto.ParameterName = "@IDRadnoMesto";
            iDRadnoMesto.SqlDbType = SqlDbType.Int;
            iDRadnoMesto.Direction = ParameterDirection.Input;
            iDRadnoMesto.Value = IDRadnoMesto;
            cmd.Parameters.Add(iDRadnoMesto);

            SqlParameter largoBroj = new SqlParameter();
            largoBroj.ParameterName = "@LargoBroj";
            largoBroj.SqlDbType = SqlDbType.Int;
            largoBroj.Direction = ParameterDirection.Input;
            largoBroj.Value = LargoBroj;
            cmd.Parameters.Add(largoBroj);

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

        public void UpdKadroviRM(int ID, int IDRadnoMesto)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateKadroviRadnihMesta";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter iDRadnoMesto = new SqlParameter();
            iDRadnoMesto.ParameterName = "@IDRadnoMesto";
            iDRadnoMesto.SqlDbType = SqlDbType.Int;
            iDRadnoMesto.Direction = ParameterDirection.Input;
            iDRadnoMesto.Value = IDRadnoMesto;
            cmd.Parameters.Add(iDRadnoMesto);

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
                throw new Exception("Neuspesan upis Kadrovi radnih mesta");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Unos Kadrovi radnih mesta je uspesno zavrsena", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
                if (error) { }
            }
        }

        public void DelKadroviRadnogMesta(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeaktivacijaKadroviRadnihMesta";
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
