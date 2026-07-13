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
    public class InsertKrojnaLista
    {
        public void InsertKrojna(int MP, int IDZapisa, int PlanID, double Kolicina, string NazivKrojneListe, int Prioritet)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertKrojnaLista";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@MP";
            parameter4.SqlDbType = SqlDbType.Int;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = MP;
            myCommand.Parameters.Add(parameter4);



            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@IDZapisa";
            parameter5.SqlDbType = SqlDbType.Int;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = IDZapisa;
            myCommand.Parameters.Add(parameter5);


            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Kolicina";
            parameter7.SqlDbType = SqlDbType.Decimal;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Kolicina;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@KrojnaLista";
            parameter8.SqlDbType = SqlDbType.NVarChar;
            parameter8.Size = 50;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = NazivKrojneListe;
            myCommand.Parameters.Add(parameter8);

            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@PlanID";
            parameter6.SqlDbType = SqlDbType.Int;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = PlanID;
            myCommand.Parameters.Add(parameter6);

            SqlParameter parameter61 = new SqlParameter();
            parameter61.ParameterName = "@Prioritet";
            parameter61.SqlDbType = SqlDbType.Int;
            parameter61.Direction = ParameterDirection.Input;
            parameter61.Value = Prioritet;
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
                throw new Exception("Neuspešan upis ");
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

        public void UpdateKrojna(int MP, int IDZapisa, int PlanID, double Kolicina, string NazivKrojneListe, int Prioritet)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateKrojnaLista";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@MP";
            parameter4.SqlDbType = SqlDbType.Int;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = MP;
            myCommand.Parameters.Add(parameter4);



            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@IDZapisa";
            parameter5.SqlDbType = SqlDbType.Int;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = IDZapisa;
            myCommand.Parameters.Add(parameter5);


            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Kolicina";
            parameter7.SqlDbType = SqlDbType.Decimal;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Kolicina;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@KrojnaLista";
            parameter8.SqlDbType = SqlDbType.NVarChar;
            parameter8.Size = 50;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = NazivKrojneListe;
            myCommand.Parameters.Add(parameter8);

            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@PlanID";
            parameter6.SqlDbType = SqlDbType.Int;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = PlanID;
            myCommand.Parameters.Add(parameter6);

            SqlParameter parameter61 = new SqlParameter();
            parameter61.ParameterName = "@Prioritet";
            parameter61.SqlDbType = SqlDbType.Int;
            parameter61.Direction = ParameterDirection.Input;
            parameter61.Value = Prioritet;
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
                throw new Exception("Neuspešan upis ");
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
