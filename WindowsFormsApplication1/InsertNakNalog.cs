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
    class InsertNakNalog
    {
        public void InsertNakNalogV(string DatumUtovara, string Prevoz, string DatumIstovara, string VozacImePrezime, string PrikljucnoVozilo, string Suvozac)
        {

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertNakNalog";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@DatumUtovara";
            parameter.SqlDbType = SqlDbType.Char;
            parameter.Size = 10;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = DatumUtovara;

            myCommand.Parameters.Add(parameter);
            // PrepareCommand(myCommand);
            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Prevoz";
            parameter2.SqlDbType = SqlDbType.Char;
            parameter2.Size = 35;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Prevoz;
            myCommand.Parameters.Add(parameter2);


            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@DatumIstovara";
            parameter3.SqlDbType = SqlDbType.Char;
            parameter2.Size = 10;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = DatumIstovara;
            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@VozacImePrezime";
            parameter4.SqlDbType = SqlDbType.Char;
            parameter4.Size = 50;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = VozacImePrezime;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@PrikljucnoVozilo";
            parameter5.SqlDbType = SqlDbType.Char;
            parameter5.Size = 50;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = PrikljucnoVozilo;
            myCommand.Parameters.Add(parameter5);

            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@Suvozac";
            parameter5.SqlDbType = SqlDbType.Char;
            parameter5.Size = 50;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = Suvozac;
            myCommand.Parameters.Add(parameter6);


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
