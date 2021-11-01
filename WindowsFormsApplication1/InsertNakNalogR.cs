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
    class InsertNakNalogR
    {
        public void InsertNakNalogRV(int @Broj, string @Naziv, string @JM, double @Kolicina, string @kod, string @BrojUtovara, int @komitentID, string @KomitentNaziv, int MestoIsporukeId, string MestoIsporuke, string BrojIsporuke, string IsporukaId)
        {

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertNakNalogR";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Broj";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Broj;

            myCommand.Parameters.Add(parameter);





            // PrepareCommand(myCommand);
            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Naziv";
            parameter3.SqlDbType = SqlDbType.Char;
            parameter3.Size = 70;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Naziv;
            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@JM";
            parameter4.SqlDbType = SqlDbType.Char;
            parameter4.Size = 3;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = JM;
            //parameter4.Value = "KOM";
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@kolicina";
            parameter5.SqlDbType = SqlDbType.Decimal;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = Kolicina;
            myCommand.Parameters.Add(parameter5);

            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@kod";
            parameter6.SqlDbType = SqlDbType.Char;
            parameter6.Size = 24;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = kod;
            myCommand.Parameters.Add(parameter6);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@BrojUtovara";
            parameter7.SqlDbType = SqlDbType.Char;
            parameter7.Size = 24;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = BrojUtovara;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@komitentID";
            parameter8.SqlDbType = SqlDbType.Int;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = komitentID;
            myCommand.Parameters.Add(parameter8);

            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@komitentNaziv";
            parameter9.SqlDbType = SqlDbType.VarChar;
            parameter9.Size = 2048;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = KomitentNaziv;
            myCommand.Parameters.Add(parameter9);

            SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@MestoIsporukeID";
            parameter10.SqlDbType = SqlDbType.Int;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = MestoIsporukeId;
            myCommand.Parameters.Add(parameter10);

            SqlParameter parameter11 = new SqlParameter();
            parameter11.ParameterName = "@MestoIsporuke";
            parameter11.SqlDbType = SqlDbType.Char;
            parameter11.Size = 35;
            parameter11.Direction = ParameterDirection.Input;
            parameter11.Value = MestoIsporuke;
            myCommand.Parameters.Add(parameter11);

            SqlParameter parameter12 = new SqlParameter();
            parameter12.ParameterName = "@BrojIsporuke";
            parameter12.SqlDbType = SqlDbType.Char;
            parameter12.Size = 35;
            parameter12.Direction = ParameterDirection.Input;
            parameter12.Value = BrojIsporuke;
            myCommand.Parameters.Add(parameter12);

            SqlParameter parameter13 = new SqlParameter();
            parameter13.ParameterName = "@IsporukaID";
            parameter13.SqlDbType = SqlDbType.Int;
            parameter13.Direction = ParameterDirection.Input;
            parameter13.Value = IsporukaId;
            myCommand.Parameters.Add(parameter13);



            //@MestoIsporukeID, @MestoIsporuke, @BrojIsporuke, @IsporukaId, 0 , 0


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

