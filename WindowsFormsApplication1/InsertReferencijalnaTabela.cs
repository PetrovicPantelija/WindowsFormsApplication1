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
    class InsertReferencijalnaTabela
    {
        public void DeleteRefTab()
        {

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteReferencijalnaTabela";
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
                throw new Exception("Neuspešno brisanje računa u Bazi");
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



        public void InsertRefTab(int ID, int WebsProizvodID, int WebsPaketID, int WebsBojaKorpusIDz, int WebsBojaFrontIDz, string WebsGTIN, string WebsProizvodSifra, string WebsProizvodNazivGTIN, string WebsProizvodKorpusBoja, string WebsProizvodFrontBoja, string WebsPaketNaziv, string WebsPaketOznakaEAN, int LargoProizvodID, string LargoProizvodNaziv, int LargoPaketID, string LargoPaketNaziv)
        {

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertReferencijalnaTabela";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);
            // PrepareCommand(myCommand);
            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@WebsProizvodID";
            parameter2.SqlDbType = SqlDbType.Int;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = WebsProizvodID;
            myCommand.Parameters.Add(parameter2);

            
            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@WebsPaketID";
            parameter3.SqlDbType = SqlDbType.Int;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = WebsPaketID;
            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@WebsBojaKorpusIDz";
            parameter4.SqlDbType = SqlDbType.Int;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = WebsBojaKorpusIDz;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@WebsBojaFrontIDz";
            parameter5.SqlDbType = SqlDbType.Int;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = WebsBojaFrontIDz;
            myCommand.Parameters.Add(parameter5);


            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@WebsGTIN";
            parameter6.SqlDbType = SqlDbType.Char;
            parameter6.Size = 40;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = WebsGTIN;
            myCommand.Parameters.Add(parameter6);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@WebsProizvodSifra";
            parameter7.SqlDbType = SqlDbType.Char;
            parameter7.Size = 40;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = WebsProizvodSifra;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@WebsProizvodNazivGTIN";
            parameter8.SqlDbType = SqlDbType.Char;
            parameter8.Size = 40;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = WebsProizvodNazivGTIN;
            myCommand.Parameters.Add(parameter8);

            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@WebsProizvodKorpusBoja";
            parameter9.SqlDbType = SqlDbType.Char;
            parameter9.Size = 6;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = WebsProizvodKorpusBoja;
            myCommand.Parameters.Add(parameter9);

            SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@WebsProizvodFrontBoja";
            parameter10.SqlDbType = SqlDbType.Char;
            parameter10.Size = 6;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = WebsProizvodFrontBoja;
            myCommand.Parameters.Add(parameter10);


            SqlParameter parameter11 = new SqlParameter();
            parameter11.ParameterName = "@WebsPaketNaziv";
            parameter11.SqlDbType = SqlDbType.Char;
            parameter11.Size = 50;
            parameter11.Direction = ParameterDirection.Input;
            parameter11.Value = WebsPaketNaziv;
            myCommand.Parameters.Add(parameter11);

            SqlParameter parameter12 = new SqlParameter();
            parameter12.ParameterName = "@WebsPaketOznakaEAN";
            parameter12.SqlDbType = SqlDbType.Char;
            parameter12.Size = 5;
            parameter12.Direction = ParameterDirection.Input;
            parameter12.Value = WebsPaketOznakaEAN;
            myCommand.Parameters.Add(parameter12);

            SqlParameter parameter13 = new SqlParameter();
            parameter13.ParameterName = "@LargoProizvodID";
            parameter13.SqlDbType = SqlDbType.Int;
            parameter13.Direction = ParameterDirection.Input;
            parameter13.Value = LargoProizvodID;
            myCommand.Parameters.Add(parameter13);

            SqlParameter parameter14 = new SqlParameter();
            parameter14.ParameterName = "@LargoProizvodNaziv";
            parameter14.SqlDbType = SqlDbType.Char;
            parameter14.Size = 50;
            parameter14.Direction = ParameterDirection.Input;
            parameter14.Value = LargoProizvodNaziv;
            myCommand.Parameters.Add(parameter14);

            SqlParameter parameter15 = new SqlParameter();
            parameter15.ParameterName = "@LargoPaketID";
            parameter15.SqlDbType = SqlDbType.Int;
            parameter15.Direction = ParameterDirection.Input;
            parameter15.Value = LargoPaketID;
            myCommand.Parameters.Add(parameter15);

            SqlParameter parameter16 = new SqlParameter();
            parameter16.ParameterName = "@LargoPaketNaziv";
            parameter16.SqlDbType = SqlDbType.Char;
            parameter16.Size = 50;
            parameter16.Direction = ParameterDirection.Input;
            parameter16.Value = LargoPaketNaziv;
            myCommand.Parameters.Add(parameter16);

        
            
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