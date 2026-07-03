using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Linq.Expressions;

namespace WindowsFormsApplication1.Neto_potrebe
{
     public class InsertStampaNP
    {
       
        public void InsertNP(int MP, string KOD, double Kolicina, double Potroseno, string JM, string Plan, string PodPlan)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertStampajNP";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@MP";
            parameter4.SqlDbType = SqlDbType.Int;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = MP;
            myCommand.Parameters.Add(parameter4);



            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@Kod";
            parameter5.SqlDbType = SqlDbType.NVarChar;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = KOD;
            myCommand.Parameters.Add(parameter5);


            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Kolicina";
            parameter7.SqlDbType = SqlDbType.Decimal;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Kolicina;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter71 = new SqlParameter();
            parameter71.ParameterName = "@Potroseno";
            parameter71.SqlDbType = SqlDbType.Decimal;
            parameter71.Direction = ParameterDirection.Input;
            parameter71.Value = Potroseno;
            myCommand.Parameters.Add(parameter71);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@JM";
            parameter8.SqlDbType = SqlDbType.NVarChar;
            parameter8.Size = 30;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = JM;
            myCommand.Parameters.Add(parameter8);

            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@Plan";
            parameter6.SqlDbType = SqlDbType.NVarChar;
            parameter6.Size = 50;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = Plan;
            myCommand.Parameters.Add(parameter6);

            SqlParameter parameter61 = new SqlParameter();
            parameter61.ParameterName = "@PodPlan";
            parameter61.SqlDbType = SqlDbType.NVarChar;
            parameter61.Size = 30;
            parameter61.Direction = ParameterDirection.Input;
            parameter61.Value = PodPlan;
            myCommand.Parameters.Add(parameter61);


            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            bool error = true;
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = myConnection.BeginTransaction();
                myCommand.Transaction = myTransaction;
            }
            catch (SqlException)
            {
                throw new Exception("Neuspešan upis zaglavlja medjuskladisnog");
            }
            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos kombinacije je uspesno zavrsen", "Rezultat generisanja",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void DeleteAll()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteStampajNP";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;



            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            bool error = true;
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = myConnection.BeginTransaction();
                myCommand.Transaction = myTransaction;
            }
            catch (SqlException)
            {
                throw new Exception("Neuspešan upis zaglavlja medjuskladisnog");
            }
            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos kombinacije je uspesno zavrsen", "Rezultat generisanja",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
    }
}
