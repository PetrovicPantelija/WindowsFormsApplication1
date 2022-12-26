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
    class InsertReklamacijaNalepnica
    {
         public void InsertReklNal(int ShippingItemRecID, int ShipingIDz,string ShippingNo, string MestoIsporuke,  string SifraReklamacije,string NazivReklamacije, double Kolicina,string Zapisnik,  string Opis,int KolicinaOrg, string Barkod)
        {
  
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertReklamcijaNalepnica";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ShippingItemRecID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = @ShippingItemRecID;
            myCommand.Parameters.Add(parameter);
            // PrepareCommand(myCommand);
              SqlParameter parameter01 = new SqlParameter();
            parameter01.ParameterName = "@ShipingIDz";
            parameter01.SqlDbType = SqlDbType.Int;
            parameter01.Direction = ParameterDirection.Input;
            parameter01.Value = ShipingIDz;
            myCommand.Parameters.Add(parameter01);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@ShippingNo";
            parameter2.SqlDbType = SqlDbType.Char;
            parameter2.Size = 25;  
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = ShippingNo;
            myCommand.Parameters.Add(parameter2);

   
            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@MestoIsporuke";
            parameter7.SqlDbType = SqlDbType.NVarChar;
            parameter7.Size = 50;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = MestoIsporuke;
            myCommand.Parameters.Add(parameter7);


            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@SifraReklamacije";
             parameter8.SqlDbType = SqlDbType.NVarChar;
            parameter8.Size = 30;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = SifraReklamacije;
            myCommand.Parameters.Add(parameter8);

             SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@NazivReklamacije";
            parameter9.SqlDbType = SqlDbType.NVarChar;
            parameter9.Size = 30;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = NazivReklamacije;
            myCommand.Parameters.Add(parameter9);


            SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@Kolicina";
            parameter10.SqlDbType = SqlDbType.Decimal;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = Kolicina;
            myCommand.Parameters.Add(parameter10);
           
           
             SqlParameter parameter11 = new SqlParameter();
            parameter11.ParameterName = "@Zapisnik";
            parameter11.SqlDbType = SqlDbType.NVarChar;
            parameter11.Size = 35;
            parameter11.Direction = ParameterDirection.Input;
            parameter11.Value = Zapisnik;
            myCommand.Parameters.Add(parameter11);

               SqlParameter parameter12 = new SqlParameter();
            parameter12.ParameterName = "@Opis";
            parameter12.SqlDbType = SqlDbType.NVarChar;
            parameter12.Size = 80;
            parameter12.Direction = ParameterDirection.Input;
            parameter12.Value = Opis;
            myCommand.Parameters.Add(parameter12);

            SqlParameter parameter13 = new SqlParameter();
            parameter13.ParameterName = "@KolicinaOrg";
            parameter13.SqlDbType = SqlDbType.Int;
            parameter13.Direction = ParameterDirection.Input;
            parameter13.Value = KolicinaOrg;
            myCommand.Parameters.Add(parameter13);

            SqlParameter parameter14 = new SqlParameter();
            parameter14.ParameterName = "@Barkod";
            parameter14.SqlDbType = SqlDbType.NVarChar;
            parameter14.Size = 15;
            parameter14.Direction = ParameterDirection.Input;
            parameter14.Value = Barkod;
            myCommand.Parameters.Add(parameter14);

         

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
                throw new Exception("Neuspešan upis nalepnice reklamacije");
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

         public void InsertReklROTStampaj(int Nadredjeni)
         {

             var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
             SqlConnection myConnection = new SqlConnection(s_connection);
             SqlCommand myCommand = myConnection.CreateCommand();
             myCommand.CommandText = "StampajROT";
             myCommand.CommandType = System.Data.CommandType.StoredProcedure;

             SqlParameter parameter = new SqlParameter();
             parameter.ParameterName = "@Nadredjeni";
             parameter.SqlDbType = SqlDbType.Int;
             parameter.Direction = ParameterDirection.Input;
             parameter.Value = Nadredjeni;
             myCommand.Parameters.Add(parameter);

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
                 throw new Exception("Neuspešan upis nalepnice reklamacije");
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
        
        
        
        
        
        public void DeleteReklNal()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteReklamacijaNalepnica";
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
                throw new Exception("Neuspešan ");
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
