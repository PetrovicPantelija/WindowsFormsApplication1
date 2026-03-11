using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.Pakovanja.PackingPlanItem;

namespace WindowsFormsApplication1.Neto_potrebe
{
    internal class InsertVeza
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public void InsProgram(string MpSifra, int Grupa, int Podgrupa)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertNetoVeza";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter naziv = new SqlParameter();
            naziv.ParameterName = "@MpSifra";
            naziv.SqlDbType = SqlDbType.NVarChar;
            naziv.Size = 30;
            naziv.Direction = ParameterDirection.Input;
            naziv.Value = MpSifra;
            cmd.Parameters.Add(naziv);


            SqlParameter grupa = new SqlParameter();
            grupa.ParameterName = "@Grupa";
            grupa.SqlDbType = SqlDbType.Int;
            grupa.Direction = ParameterDirection.Input;
            grupa.Value = Grupa;
            cmd.Parameters.Add(grupa);


            SqlParameter podgrupa = new SqlParameter();
            podgrupa.ParameterName = "@Podgrupa";
            podgrupa.SqlDbType = SqlDbType.Int;
            podgrupa.Direction = ParameterDirection.Input;
            podgrupa.Value = Podgrupa;
            cmd.Parameters.Add(podgrupa);

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
        public void ObrisiNetoPotrebe(int PlanID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "ObrisiNetoPotrebe";
            cmd.CommandType = CommandType.StoredProcedure;

    
            SqlParameter grupa = new SqlParameter();
            grupa.ParameterName = "@PlanID";
            grupa.SqlDbType = SqlDbType.Int;
            grupa.Direction = ParameterDirection.Input;
            grupa.Value = PlanID;
            cmd.Parameters.Add(grupa);


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
                throw new Exception("Neuspesano brisanje");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show(" Brisanje nije uspelo ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
                if (error)
                {

                }
            }
        }
    }
}
