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
    class InsertMaticni
    {
        public void InsMaticni(int Sifra, int Paleta, double MaxKolicina, double BrutoPaketa, double TezinaPalete, string RegalniRed)
        {

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertMaticniMagacin";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;


            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@MpSifra";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Sifra;
            myCommand.Parameters.Add(parameter);


            SqlParameter parameter01 = new SqlParameter();
            parameter01.ParameterName = "@Paleta";
            parameter01.SqlDbType = SqlDbType.Int;
            parameter01.Direction = ParameterDirection.Input;
            parameter01.Value = Paleta;
            myCommand.Parameters.Add(parameter01);

            SqlParameter parameter02 = new SqlParameter();
            parameter02.ParameterName = "@MaxKolicina";
            parameter02.SqlDbType = SqlDbType.Decimal;
            parameter02.Direction = ParameterDirection.Input;
            parameter02.Value = MaxKolicina;
            myCommand.Parameters.Add(parameter02);

            SqlParameter parameter03 = new SqlParameter();
            parameter03.ParameterName = "@BrutoPaketa";
            parameter03.SqlDbType = SqlDbType.Decimal;
            parameter03.Direction = ParameterDirection.Input;
            parameter03.Value = BrutoPaketa;
            myCommand.Parameters.Add(parameter03);


            SqlParameter parameter04 = new SqlParameter();
            parameter04.ParameterName = "@TezinaPalete";
            parameter04.SqlDbType = SqlDbType.Decimal;
            parameter04.Direction = ParameterDirection.Input;
            parameter04.Value = TezinaPalete;
            myCommand.Parameters.Add(parameter04);

            SqlParameter parameter05 = new SqlParameter();
            parameter05.ParameterName = "@RegalniRed";
            parameter05.SqlDbType = SqlDbType.NVarChar;
            parameter05.Size = 30;
            parameter05.Direction = ParameterDirection.Input;
            parameter05.Value = RegalniRed;
            myCommand.Parameters.Add(parameter05);



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
                throw new Exception("Neuspešna promena Maticnog podatka");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Promena Artikla broja je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }


        }
        public void UpdMaticni(int Sifra, int Paleta, double MaxKolicina, double BrutoPaketa, double TezinaPalete,string RegalniRed)
        {
            
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateMaticniMagacin";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;


            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Sifra";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Sifra;
            myCommand.Parameters.Add(parameter);


            SqlParameter parameter01 = new SqlParameter();
            parameter01.ParameterName = "@Paleta";
            parameter01.SqlDbType = SqlDbType.Int;
            parameter01.Direction = ParameterDirection.Input;
            parameter01.Value = Paleta;
            myCommand.Parameters.Add(parameter01);

            SqlParameter parameter02 = new SqlParameter();
            parameter02.ParameterName = "@MaxKolicina";
            parameter02.SqlDbType = SqlDbType.Decimal;
            parameter02.Direction = ParameterDirection.Input;
            parameter02.Value = MaxKolicina;
            myCommand.Parameters.Add(parameter02);

            SqlParameter parameter03 = new SqlParameter();
            parameter03.ParameterName = "@BrutoPaketa";
            parameter03.SqlDbType = SqlDbType.Decimal;
            parameter03.Direction = ParameterDirection.Input;
            parameter03.Value = BrutoPaketa;
            myCommand.Parameters.Add(parameter03);


            SqlParameter parameter04 = new SqlParameter();
            parameter04.ParameterName = "@TezinaPalete";
            parameter04.SqlDbType = SqlDbType.Decimal;
            parameter04.Direction = ParameterDirection.Input;
            parameter04.Value = TezinaPalete;
            myCommand.Parameters.Add(parameter04);

            SqlParameter parameter05 = new SqlParameter();
            parameter05.ParameterName = "@RegalniRed";
            parameter05.SqlDbType = SqlDbType.NVarChar;
            parameter05.Size = 30;
            parameter05.Direction = ParameterDirection.Input;
            parameter05.Value = RegalniRed;
            myCommand.Parameters.Add(parameter05);

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
                throw new Exception("Neuspešna promena Maticnog podatka");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Promena Artikla broja je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }


        }
        public void DeleteMaticni(int Sifra)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteMaticniPodatkiMagacin";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Sifra";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Sifra;
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
