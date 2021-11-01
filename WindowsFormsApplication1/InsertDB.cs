using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Nedra
{
    class InsertDB
    {
       

        public void InsertPartnerji(int PaSifra, string NazivKomintenta, string PIBKomintenta, string MaticniBrojKomintenta, string PostanskiBrojIDz, string Adresa, string Telefon, string Fax, string WWWAdresa, string TekuciRacun)
        {

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertParnerji";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;
            
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@PaSifra";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = PaSifra;

            myCommand.Parameters.Add(parameter);
           // PrepareCommand(myCommand);
            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@NazivKomintenta";
            parameter2.SqlDbType = SqlDbType.Char;
            parameter2.Size = 30;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = NazivKomintenta;
            myCommand.Parameters.Add(parameter2);
            
            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@PIBKomintenta";
            parameter3.SqlDbType = SqlDbType.Char;
            parameter3.Size = 35;
            parameter3.Direction = ParameterDirection.Input;
           // parameter3.Value = PIBKomintenta;
            parameter3.Value = PIBKomintenta;
            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@MaticniBrojKomintenta";
            parameter4.SqlDbType = SqlDbType.Char;
            parameter4.Size = 35;
            parameter4.Direction = ParameterDirection.Input;
           // parameter4.Value = MaticniBrojKomintenta;
            parameter4.Value = MaticniBrojKomintenta;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@Adresa";
            parameter5.SqlDbType = SqlDbType.Char;
            parameter5.Size = 35;
            parameter5.Direction = ParameterDirection.Input;
           // parameter5.Value = Adresa;
            parameter5.Value = Adresa;
            myCommand.Parameters.Add(parameter5);

            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@PostanskiBrojIDz";
            parameter6.SqlDbType = SqlDbType.Char;
            parameter6.Size = 9;
            parameter6.Direction = ParameterDirection.Input;
           // parameter6.Value = PostanskiBrojIDz;
            parameter6.Value = PostanskiBrojIDz;
            myCommand.Parameters.Add(parameter6);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Telefon";
            parameter7.SqlDbType = SqlDbType.Char;
            parameter7.Size = 17;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Telefon;
          //  parameter7.Value = " mm ";
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@Fax";
            parameter8.SqlDbType = SqlDbType.Char;
            parameter8.Size = 17;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = Fax;
            // parameter8.Value = " mm ";
            myCommand.Parameters.Add(parameter8);

            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@WWWAdresa";
            parameter9.SqlDbType = SqlDbType.Char;
            parameter9.Size = 90;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = WWWAdresa;
            // parameter9.Value = " mm ";
            myCommand.Parameters.Add(parameter9);

            SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@TekuciRacun";
            parameter10.SqlDbType = SqlDbType.Char;
            parameter10.Size = 45;
            parameter10.Direction = ParameterDirection.Input;
          //  parameter10.Value = TekuciRacun;
            parameter10.Value = TekuciRacun;
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

        protected void PrepareCommand(SqlCommand command)
        {
            /*
            SqlParameter BPG = new SqlParameter("@PaSifra", SqlDbType.Char, 12);
            command.Parameters.Add(PaSifra);

            SqlParameter Naziv = new SqlParameter("@NazivKomintenta", SqlDbType.Char, 150);
            command.Parameters.Add(NazivKomintenta);

            SqlParameter JMBG = new SqlParameter("@PIBKomintenta", SqlDbType.Char, 20);
            command.Parameters.Add(PIBKomintenta);

            SqlParameter PIB = new SqlParameter("@MaticniBrojKomintenta", SqlDbType.Char, 20);
            command.Parameters.Add(MaticniBrojKomintenta);

            SqlParameter Posta = new SqlParameter("@Adresa", SqlDbType.Char, 20);
            command.Parameters.Add(Adresa);

            SqlParameter Mesto = new SqlParameter("@PostanskiBrojIDz", SqlDbType.Char, 50);
            command.Parameters.Add(PostanskiBrojIDz);

            SqlParameter Adresa = new SqlParameter("@Adresa", SqlDbType.Char, 250);
            command.Parameters.Add(Adresa);

            SqlParameter Telefon = new SqlParameter("@Telefon", SqlDbType.Char, 20);
            command.Parameters.Add(Telefon);

            SqlParameter Aktivnost = new SqlParameter("@Fax", SqlDbType.Char, 50);
            command.Parameters.Add(Fax);

            SqlParameter Datum = new SqlParameter("@WWWAdresa", SqlDbType.Char, 50);
            command.Parameters.Add(WWWAdresa);

            SqlParameter Opstina = new SqlParameter("@TekuciRacun", SqlDbType.Char, 50);
            command.Parameters.Add(TekuciRacun);
            */
        }
    }
}

