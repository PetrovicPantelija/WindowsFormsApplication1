

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
    class InsertTanjaPregled
    {
        public void InsertTP(string Program, string Sifra,string BarKodProizvoda, string NazivProizvoda)
        {

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertTanjaPregled";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

          
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Program";
            parameter.SqlDbType = SqlDbType.Char;
            parameter.Size = 40;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Program;

            myCommand.Parameters.Add(parameter);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Sifra";
            parameter2.SqlDbType = SqlDbType.Char;
            parameter2.Size = 40;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Sifra;

            myCommand.Parameters.Add(parameter2);

             
            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@BarKodProizvoda";
            parameter3.SqlDbType = SqlDbType.Char;
            parameter3.Size = 40;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = BarKodProizvoda;

            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@NazivProizvoda";
            parameter4.SqlDbType = SqlDbType.Char;
            parameter4.Size = 40;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = NazivProizvoda;

            myCommand.Parameters.Add(parameter4);

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
                throw new Exception("Neuspešan upis računa u bazu");
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



