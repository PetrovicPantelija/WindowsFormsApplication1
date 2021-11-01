using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WindowsFormsApplication1.Magacin
{
    class InsertMSPNalepnica
    {
        public void InsMspNalepnica( int BrojMSP,  int BrojRN,string MpKod,string MagacinIzdajni, string MagacinPrijemni, int MpSifra,string Izdao,  double Kolicina,  string Napomena, string Barkod, string BarkodMSP)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertMSPNalepnica";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@BrojMSP";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = BrojMSP;
            myCommand.Parameters.Add(parameter);

             SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@BrojRN";
            parameter1.SqlDbType = SqlDbType.Int;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = BrojRN;
            myCommand.Parameters.Add(parameter1);
           
            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@MpKod";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 70;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = MpKod;
            myCommand.Parameters.Add(parameter2);

 SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@MagacinIzdajni";
            parameter3.SqlDbType = SqlDbType.NVarChar;
            parameter3.Size = 70;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = MagacinIzdajni;
            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@MagacinPrijemni";
            parameter4.SqlDbType = SqlDbType.NVarChar;
            parameter4.Size = 70;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = MagacinPrijemni;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@MpSifra";
            parameter5.SqlDbType = SqlDbType.Int;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = MpSifra;
            myCommand.Parameters.Add(parameter5);

             SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@Izdao";
            parameter6.SqlDbType = SqlDbType.NVarChar;
            parameter6.Size = 70;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = Izdao;
            myCommand.Parameters.Add(parameter6);

              SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Kolicina";
            parameter7.SqlDbType = SqlDbType.Decimal;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Kolicina;
            myCommand.Parameters.Add(parameter7);

             SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@Napomena";
            parameter8.SqlDbType = SqlDbType.NVarChar;
            parameter8.Size = 100;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = Napomena;
            myCommand.Parameters.Add(parameter8);

              SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@Barkod";
            parameter9.SqlDbType = SqlDbType.NVarChar;
            parameter9.Size = 15;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = Barkod;
            myCommand.Parameters.Add(parameter9);

            SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@BarkodMSP";
            parameter10.SqlDbType = SqlDbType.NVarChar;
            parameter10.Size = 15;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = BarkodMSP;
            myCommand.Parameters.Add(parameter10);
    
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
                throw new Exception("Neuspešan upis Nalepnica");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos NHM broja je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }


        }


        public void UpdMspNalepnica(int MpSifra, int BN)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateMSPNalepnica";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@MpSifra";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = MpSifra;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@BN";
            parameter1.SqlDbType = SqlDbType.Int;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = BN;
            myCommand.Parameters.Add(parameter1);

          
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
                throw new Exception("Neuspešan upis Nalepnica");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos NHM broja je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }


        }
        
        
        public void DeleteNalepnice()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteMSPNalepnica";
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
                throw new Exception("Brisanje neuspešno");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Brisanje nije uspešno", "",
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
