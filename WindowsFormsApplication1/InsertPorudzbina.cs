using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WindowsFormsApplication1
{
    class InsertPorudzbina
    {
        public void InsertPor(string Datum)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertPorudzbina";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Datum";
            parameter.SqlDbType = SqlDbType.Char;
            parameter.Size = 20;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Datum;
            myCommand.Parameters.Add(parameter);
            // PrepareCommand(myCommand);


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
                throw new Exception("Neuspešan upis zaglavlja otpremnice");
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

        public void InsertPorudzbinaPostav(int Artikal, decimal Kolicina)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertPorudzbinaPostav";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Artikal";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Artikal;
            myCommand.Parameters.Add(parameter);
            // PrepareCommand(myCommand);
            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Kolicina";
            parameter2.SqlDbType = SqlDbType.Decimal;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Kolicina;
            myCommand.Parameters.Add(parameter2);

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
                throw new Exception("Neuspešan upis zaglavlja otpremnice");
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

        public void InsertBeleskaPostav( string NazivZapisa,int ID, int LargoProizvodID, string LargoProizvodNaziv, int LargoPaketID, string LargoPaketNaziv, double Kolicina)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertBeleske";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

           

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@NazivZapisa";
            parameter1.SqlDbType = SqlDbType.NVarChar;
            parameter1.Size = 100;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = NazivZapisa;
            myCommand.Parameters.Add(parameter1);
         
     
          
            // PrepareCommand(myCommand);
            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@ID";
            parameter2.SqlDbType = SqlDbType.Int;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = ID;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@LargoProzvodID";
            parameter3.SqlDbType = SqlDbType.Int;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = LargoProizvodID;
            myCommand.Parameters.Add(parameter3);
            
   
          

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@LargoProzvodNaziv";
            parameter4.SqlDbType = SqlDbType.NVarChar;
            parameter4.Size = 100;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = LargoProizvodNaziv;
            myCommand.Parameters.Add(parameter4);

     


            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@LargoPaketID";
            parameter5.SqlDbType = SqlDbType.Int;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = LargoPaketID;
            myCommand.Parameters.Add(parameter5);

            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@LargoPaketNaziv";
            parameter6.SqlDbType = SqlDbType.NVarChar;
            parameter6.Size = 100;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = LargoPaketNaziv;
            myCommand.Parameters.Add(parameter6);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Kolicina";
            parameter7.SqlDbType = SqlDbType.Decimal;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Kolicina;
            myCommand.Parameters.Add(parameter7);

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
                throw new Exception("Neuspešan upis zaglavlja otpremnice");
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
