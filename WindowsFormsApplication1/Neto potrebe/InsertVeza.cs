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
        public void InsProgram(string MpSifra, int Grupa, int Podgrupa, string Materijal1, string Materijal2, string Debljina, string Tehnologija1, String Tehnologija2)
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


            SqlParameter materijal1 = new SqlParameter();
            materijal1.ParameterName = "@Materijal1";
            materijal1.SqlDbType = SqlDbType.NVarChar;
            materijal1.Size = 30;
            materijal1.Direction = ParameterDirection.Input;
            materijal1.Value = Materijal1;
            cmd.Parameters.Add(materijal1);

            SqlParameter materijal2 = new SqlParameter();
            materijal2.ParameterName = "@Materijal2";
            materijal2.SqlDbType = SqlDbType.NVarChar;
            materijal2.Size = 30;
            materijal2.Direction = ParameterDirection.Input;
            materijal2.Value = Materijal2;
            cmd.Parameters.Add(materijal2);


            SqlParameter debljina = new SqlParameter();
            debljina.ParameterName = "@Debljina";
            debljina.SqlDbType = SqlDbType.NVarChar;
            debljina.Size = 30;
            debljina.Direction = ParameterDirection.Input;
            debljina.Value = Debljina;
            cmd.Parameters.Add(debljina);

            SqlParameter tehnologija1 = new SqlParameter();
            tehnologija1.ParameterName = "@Tehnologija1";
            tehnologija1.SqlDbType = SqlDbType.NVarChar;
            tehnologija1.Size = 30;
            tehnologija1.Direction = ParameterDirection.Input;
            tehnologija1.Value = Tehnologija1;
            cmd.Parameters.Add(tehnologija1);

            SqlParameter tehnologija2 = new SqlParameter();
            tehnologija2.ParameterName = "@Tehnologija2";
            tehnologija2.SqlDbType = SqlDbType.NVarChar;
            tehnologija2.Size = 30;
            tehnologija2.Direction = ParameterDirection.Input;
            tehnologija2.Value = Tehnologija2;
            cmd.Parameters.Add(tehnologija2);



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

        public void DelProgram(int MpSifra, int Grupa, int Podgrupa)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteNetoVeza";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter naziv = new SqlParameter();
            naziv.ParameterName = "@MpSifra";
            naziv.SqlDbType = SqlDbType.Int;
            naziv.Direction = ParameterDirection.Input;
            naziv.Value = MpSifra;
            cmd.Parameters.Add(naziv);


            SqlParameter grupa = new SqlParameter();
            grupa.ParameterName = "@MdfGrupa";
            grupa.SqlDbType = SqlDbType.Int;
            grupa.Direction = ParameterDirection.Input;
            grupa.Value = Grupa;
            cmd.Parameters.Add(grupa);


            SqlParameter podgrupa = new SqlParameter();
            podgrupa.ParameterName = "@MdfPodgrupa";
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

        public void PromeniKolicinu(int ZapisID, double Kolicina)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "NetoPotrebeIgorPromeniKolicinu";
            cmd.CommandType = CommandType.StoredProcedure;


            SqlParameter grupa = new SqlParameter();
            grupa.ParameterName = "@ZapisID";
            grupa.SqlDbType = SqlDbType.Int;
            grupa.Direction = ParameterDirection.Input;
            grupa.Value = ZapisID;
            cmd.Parameters.Add(grupa);


            SqlParameter grupa2 = new SqlParameter();
            grupa2.ParameterName = "@Kolicina";
            grupa2.SqlDbType = SqlDbType.Decimal;
            grupa2.Direction = ParameterDirection.Input;
            grupa2.Value = Kolicina;
            cmd.Parameters.Add(grupa2);


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


        public void DefinisiPodplan(int ZapisID, string Podplan)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "NetoPotrebeIgorDefinisiPodplan";
            cmd.CommandType = CommandType.StoredProcedure;


            SqlParameter grupa = new SqlParameter();
            grupa.ParameterName = "@ZapisID";
            grupa.SqlDbType = SqlDbType.Int;
            grupa.Direction = ParameterDirection.Input;
            grupa.Value = ZapisID;
            cmd.Parameters.Add(grupa);


            SqlParameter grupa2 = new SqlParameter();
            grupa2.ParameterName = "@Podplan";
            grupa2.SqlDbType = SqlDbType.NVarChar;
            grupa2.Size = 100;
            grupa2.Direction = ParameterDirection.Input;
            grupa2.Value = Podplan;
            cmd.Parameters.Add(grupa2);


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
