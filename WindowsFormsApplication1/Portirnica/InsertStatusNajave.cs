using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WindowsFormsApplication1.Portirnica
{
    class InsertStatusNajave
    {
        public void InsSatusNajave(string RegistarskiBroj, string PrevoznikKlijent, string Partner, DateTime DatumVremeDolaska, string Napomena, string UtovarBroj, string KorisnikOtvorio, DateTime DatumVremeOtvaranja, string Gate, DateTime DatumVremePrijema, string KorinsikPrimio, string NapomenaPrijema, int PaletaSpremne1, DateTime DatumSpremne1, int PaletaSpremne2, DateTime DatumSpremne2, string KontrolorSpremne1, string KontrolorSpremne2, int DokumentacijaSpremne, string ProdajaKorisnik, DateTime DatumDokumentSpremne, DateTime DatumKamionIzasao, string Korisnik, string DatumNajave, string Vozilo, string NapomenaKontrola, string NapomenaIzlaz)
        {

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertStatusNajave";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@RegistarskiBroj";
            parameter1.SqlDbType = SqlDbType.NVarChar;
            parameter1.Size = 50;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = RegistarskiBroj;
            myCommand.Parameters.Add(parameter1);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@PrevoznikKlijent";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 50;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = PrevoznikKlijent;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Partner";
            parameter3.SqlDbType = SqlDbType.NVarChar;
            parameter3.Size = 50;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Partner;
            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@DatumVremeDolaska";
            parameter4.SqlDbType = SqlDbType.DateTime;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = DatumVremeDolaska;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@Napomena";
            parameter5.SqlDbType = SqlDbType.NVarChar;
            parameter5.Size = 50;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = Napomena;
            myCommand.Parameters.Add(parameter5);

            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@UtovarBroj";
            parameter6.SqlDbType = SqlDbType.NVarChar;
            parameter6.Size = 50;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = UtovarBroj;
            myCommand.Parameters.Add(parameter6);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@KorisnikOtvorio";
            parameter7.SqlDbType = SqlDbType.NVarChar;
            parameter7.Size = 50;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = KorisnikOtvorio;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@DatumVremeOtvaranja";
            parameter8.SqlDbType = SqlDbType.DateTime;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = DatumVremeOtvaranja;
            myCommand.Parameters.Add(parameter8);

            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@Gate";
            parameter9.SqlDbType = SqlDbType.NVarChar;
            parameter9.Size = 50;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = Gate;
            myCommand.Parameters.Add(parameter9);

            SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@DatumVremePrijema";
            parameter10.SqlDbType = SqlDbType.DateTime;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = DatumVremePrijema;
            myCommand.Parameters.Add(parameter10);

            SqlParameter parameter11 = new SqlParameter();
            parameter11.ParameterName = "@KorinsikPrimio";
            parameter11.SqlDbType = SqlDbType.NVarChar;
            parameter11.Size = 50;
            parameter11.Direction = ParameterDirection.Input;
            parameter11.Value = KorinsikPrimio;
            myCommand.Parameters.Add(parameter11);

            SqlParameter parameter12 = new SqlParameter();
            parameter12.ParameterName = "@NapomenaPrijema";
            parameter12.SqlDbType = SqlDbType.NVarChar;
            parameter12.Size = 100;
            parameter12.Direction = ParameterDirection.Input;
            parameter12.Value = NapomenaPrijema;
            myCommand.Parameters.Add(parameter12);

            SqlParameter parameter13 = new SqlParameter();
            parameter13.ParameterName = "@PaletaSpremne1";
            parameter13.SqlDbType = SqlDbType.TinyInt;
            parameter13.Direction = ParameterDirection.Input;
            parameter13.Value = PaletaSpremne1;
            myCommand.Parameters.Add(parameter13);

            SqlParameter parameter14 = new SqlParameter();
            parameter14.ParameterName = "@DatumSpremne1";
            parameter14.SqlDbType = SqlDbType.DateTime;
            parameter14.Direction = ParameterDirection.Input;
            parameter14.Value = DatumSpremne1;
            myCommand.Parameters.Add(parameter14);

            SqlParameter parameter15 = new SqlParameter();
            parameter15.ParameterName = "@PaletaSpremne2";
            parameter15.SqlDbType = SqlDbType.TinyInt;
            parameter15.Direction = ParameterDirection.Input;
            parameter15.Value = PaletaSpremne2;
            myCommand.Parameters.Add(parameter15);

            SqlParameter parameter16 = new SqlParameter();
            parameter16.ParameterName = "@DatumSpremne2";
            parameter16.SqlDbType = SqlDbType.DateTime;
            parameter16.Direction = ParameterDirection.Input;
            parameter16.Value = DatumSpremne2;
            myCommand.Parameters.Add(parameter16);

            SqlParameter parameter17 = new SqlParameter();
            parameter17.ParameterName = "@KontrolorSpremne1";
            parameter17.SqlDbType = SqlDbType.NVarChar;
            parameter17.Size = 20;
            parameter17.Direction = ParameterDirection.Input;
            parameter17.Value = KontrolorSpremne1;
            myCommand.Parameters.Add(parameter17);

            SqlParameter parameter18 = new SqlParameter();
            parameter18.ParameterName = "@KontrolorSpremne2";
            parameter18.SqlDbType = SqlDbType.NVarChar;
            parameter18.Size = 20;
            parameter18.Direction = ParameterDirection.Input;
            parameter18.Value = KontrolorSpremne2;
            myCommand.Parameters.Add(parameter18);

            SqlParameter parameter19 = new SqlParameter();
            parameter19.ParameterName = "@DokumentacijaSpremne";
            parameter19.SqlDbType = SqlDbType.TinyInt;
            parameter19.Direction = ParameterDirection.Input;
            parameter19.Value = DokumentacijaSpremne;
            myCommand.Parameters.Add(parameter19);

            SqlParameter parameter20 = new SqlParameter();
            parameter20.ParameterName = "@ProdajaKorisnik";
            parameter20.SqlDbType = SqlDbType.NVarChar;
            parameter20.Size = 20;
            parameter20.Direction = ParameterDirection.Input;
            parameter20.Value = ProdajaKorisnik;
            myCommand.Parameters.Add(parameter20);

            SqlParameter parameter21 = new SqlParameter();
            parameter21.ParameterName = "@DatumDokumentSpremne";
            parameter21.SqlDbType = SqlDbType.DateTime;
            parameter21.Direction = ParameterDirection.Input;
            parameter21.Value = DatumDokumentSpremne;
            myCommand.Parameters.Add(parameter21);

            SqlParameter parameter22 = new SqlParameter();
            parameter22.ParameterName = "@DatumKamionIzasao";
            parameter22.SqlDbType = SqlDbType.DateTime;
            parameter22.Direction = ParameterDirection.Input;
            parameter22.Value = DatumKamionIzasao;
            myCommand.Parameters.Add(parameter22);

            SqlParameter parameter23 = new SqlParameter();
            parameter23.ParameterName = "@Korisnik";
            parameter23.SqlDbType = SqlDbType.NVarChar;
            parameter23.Size = 20;
            parameter23.Direction = ParameterDirection.Input;
            parameter23.Value = Korisnik;
            myCommand.Parameters.Add(parameter23);

            SqlParameter parameter24 = new SqlParameter();
            parameter24.ParameterName = "@DatumNajave";
            parameter24.SqlDbType = SqlDbType.NVarChar;
            parameter24.Size = 20;
            parameter24.Direction = ParameterDirection.Input;
            parameter24.Value = DatumNajave;
            myCommand.Parameters.Add(parameter24);

            SqlParameter parameter25 = new SqlParameter();
            parameter25.ParameterName = "@Vozilo";
            parameter25.SqlDbType = SqlDbType.NVarChar;
            parameter25.Size = 20;
            parameter25.Direction = ParameterDirection.Input;
            parameter25.Value = Vozilo;
            myCommand.Parameters.Add(parameter25);

            SqlParameter parameter26 = new SqlParameter();
            parameter26.ParameterName = "@NapomenaKontrola";
            parameter26.SqlDbType = SqlDbType.NVarChar;
            parameter26.Size = 100;
            parameter26.Direction = ParameterDirection.Input;
            parameter26.Value = NapomenaKontrola;
            myCommand.Parameters.Add(parameter26);

            SqlParameter parameter27 = new SqlParameter();
            parameter27.ParameterName = "@NapomenaIzlaz";
            parameter27.SqlDbType = SqlDbType.NVarChar;
            parameter27.Size = 100;
            parameter27.Direction = ParameterDirection.Input;
            parameter27.Value = NapomenaIzlaz;
            myCommand.Parameters.Add(parameter27);

            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            bool error = true;
            try
            {
                //Panta commit
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = myConnection.BeginTransaction();
                myCommand.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                //throw new Exception("Neuspešan upis cena u bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis cena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }


            }
        }

        public void UpdStatusNajave(int ID, string RegistarskiBroj, string PrevoznikKlijent, string Partner, DateTime DatumVremeDolaska, string Napomena, string UtovarBroj, string KorisnikOtvorio, DateTime DatumVremeOtvaranja, string Gate, DateTime DatumVremePrijema, string KorinsikPrimio, string NapomenaPrijema, int PaletaSpremne1, DateTime DatumSpremne1, int PaletaSpremne2, DateTime DatumSpremne2, string KontrolorSpremne1, string KontrolorSpremne2, int DokumentacijaSpremne, string ProdajaKorisnik, DateTime DatumDokumentSpremne, DateTime DatumKamionIzasao, string Korisnik, string DatumNajave, string Vozilo, string NapomenaKontrola, string NapomenaIzlaz)
        {

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateStatusNajave";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@RegistarskiBroj";
            parameter1.SqlDbType = SqlDbType.NVarChar;
            parameter1.Size = 50;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = RegistarskiBroj;
            myCommand.Parameters.Add(parameter1);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@PrevoznikKlijent";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 50;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = PrevoznikKlijent;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Partner";
            parameter3.SqlDbType = SqlDbType.NVarChar;
            parameter3.Size = 50;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Partner;
            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@DatumVremeDolaska";
            parameter4.SqlDbType = SqlDbType.DateTime;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = DatumVremeDolaska;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@Napomena";
            parameter5.SqlDbType = SqlDbType.NVarChar;
            parameter5.Size = 50;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = Napomena;
            myCommand.Parameters.Add(parameter5);

            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@UtovarBroj";
            parameter6.SqlDbType = SqlDbType.NVarChar;
            parameter6.Size = 50;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = UtovarBroj;
            myCommand.Parameters.Add(parameter6);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@KorisnikOtvorio";
            parameter7.SqlDbType = SqlDbType.NVarChar;
            parameter7.Size = 50;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = KorisnikOtvorio;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@DatumVremeOtvaranja";
            parameter8.SqlDbType = SqlDbType.DateTime;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = DatumVremeOtvaranja;
            myCommand.Parameters.Add(parameter8);

            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@Gate";
            parameter9.SqlDbType = SqlDbType.NVarChar;
            parameter9.Size = 50;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = Gate;
            myCommand.Parameters.Add(parameter9);

            SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@DatumVremePrijema";
            parameter10.SqlDbType = SqlDbType.DateTime;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = DatumVremePrijema;
            myCommand.Parameters.Add(parameter10);

            SqlParameter parameter11 = new SqlParameter();
            parameter11.ParameterName = "@KorinsikPrimio";
            parameter11.SqlDbType = SqlDbType.NVarChar;
            parameter11.Size = 50;
            parameter11.Direction = ParameterDirection.Input;
            parameter11.Value = KorinsikPrimio;
            myCommand.Parameters.Add(parameter11);

            SqlParameter parameter12 = new SqlParameter();
            parameter12.ParameterName = "@NapomenaPrijema";
            parameter12.SqlDbType = SqlDbType.NVarChar;
            parameter12.Size = 100;
            parameter12.Direction = ParameterDirection.Input;
            parameter12.Value = NapomenaPrijema;
            myCommand.Parameters.Add(parameter12);

            SqlParameter parameter13 = new SqlParameter();
            parameter13.ParameterName = "@PaletaSpremne1";
            parameter13.SqlDbType = SqlDbType.TinyInt;
            parameter13.Direction = ParameterDirection.Input;
            parameter13.Value = PaletaSpremne1;
            myCommand.Parameters.Add(parameter13);

            SqlParameter parameter14 = new SqlParameter();
            parameter14.ParameterName = "@DatumSpremne1";
            parameter14.SqlDbType = SqlDbType.DateTime;
            parameter14.Direction = ParameterDirection.Input;
            parameter14.Value = DatumSpremne1;
            myCommand.Parameters.Add(parameter14);

            SqlParameter parameter15 = new SqlParameter();
            parameter15.ParameterName = "@PaletaSpremne2";
            parameter15.SqlDbType = SqlDbType.TinyInt;
            parameter15.Direction = ParameterDirection.Input;
            parameter15.Value = PaletaSpremne2;
            myCommand.Parameters.Add(parameter15);

            SqlParameter parameter16 = new SqlParameter();
            parameter16.ParameterName = "@DatumSpremne2";
            parameter16.SqlDbType = SqlDbType.DateTime;
            parameter16.Direction = ParameterDirection.Input;
            parameter16.Value = DatumSpremne2;
            myCommand.Parameters.Add(parameter16);

            SqlParameter parameter17 = new SqlParameter();
            parameter17.ParameterName = "@KontrolorSpremne1";
            parameter17.SqlDbType = SqlDbType.NVarChar;
            parameter17.Size = 20;
            parameter17.Direction = ParameterDirection.Input;
            parameter17.Value = KontrolorSpremne1;
            myCommand.Parameters.Add(parameter17);

            SqlParameter parameter18 = new SqlParameter();
            parameter18.ParameterName = "@KontrolorSpremne2";
            parameter18.SqlDbType = SqlDbType.NVarChar;
            parameter18.Size = 20;
            parameter18.Direction = ParameterDirection.Input;
            parameter18.Value = KontrolorSpremne2;
            myCommand.Parameters.Add(parameter18);

            SqlParameter parameter19 = new SqlParameter();
            parameter19.ParameterName = "@DokumentacijaSpremne";
            parameter19.SqlDbType = SqlDbType.TinyInt;
            parameter19.Direction = ParameterDirection.Input;
            parameter19.Value = DokumentacijaSpremne;
            myCommand.Parameters.Add(parameter19);

            SqlParameter parameter20 = new SqlParameter();
            parameter20.ParameterName = "@ProdajaKorisnik";
            parameter20.SqlDbType = SqlDbType.NVarChar;
            parameter20.Size = 20;
            parameter20.Direction = ParameterDirection.Input;
            parameter20.Value = ProdajaKorisnik;
            myCommand.Parameters.Add(parameter20);

            SqlParameter parameter21 = new SqlParameter();
            parameter21.ParameterName = "@DatumDokumentSpremne";
            parameter21.SqlDbType = SqlDbType.DateTime;
            parameter21.Direction = ParameterDirection.Input;
            parameter21.Value = DatumDokumentSpremne;
            myCommand.Parameters.Add(parameter21);

            SqlParameter parameter22 = new SqlParameter();
            parameter22.ParameterName = "@DatumKamionIzasao";
            parameter22.SqlDbType = SqlDbType.DateTime;
            parameter22.Direction = ParameterDirection.Input;
            parameter22.Value = DatumKamionIzasao;
            myCommand.Parameters.Add(parameter22);

            SqlParameter parameter23 = new SqlParameter();
            parameter23.ParameterName = "@Korisnik";
            parameter23.SqlDbType = SqlDbType.NVarChar;
            parameter23.Size = 20;
            parameter23.Direction = ParameterDirection.Input;
            parameter23.Value = Korisnik;
            myCommand.Parameters.Add(parameter23);

            SqlParameter parameter24 = new SqlParameter();
            parameter24.ParameterName = "@DatumNajave";
            parameter24.SqlDbType = SqlDbType.NVarChar;
            parameter24.Size = 20;
            parameter24.Direction = ParameterDirection.Input;
            parameter24.Value = DatumNajave;
            myCommand.Parameters.Add(parameter24);

            SqlParameter parameter25 = new SqlParameter();
            parameter25.ParameterName = "@Vozilo";
            parameter25.SqlDbType = SqlDbType.NVarChar;
            parameter25.Size = 20;
            parameter25.Direction = ParameterDirection.Input;
            parameter25.Value = Vozilo;
            myCommand.Parameters.Add(parameter25);

            SqlParameter parameter26 = new SqlParameter();
            parameter26.ParameterName = "@NapomenaKontrola";
            parameter26.SqlDbType = SqlDbType.NVarChar;
            parameter26.Size = 100;
            parameter26.Direction = ParameterDirection.Input;
            parameter26.Value = NapomenaKontrola;
            myCommand.Parameters.Add(parameter26);

            SqlParameter parameter27 = new SqlParameter();
            parameter27.ParameterName = "@NapomenaIzlaz";
            parameter27.SqlDbType = SqlDbType.NVarChar;
            parameter27.Size = 100;
            parameter27.Direction = ParameterDirection.Input;
            parameter27.Value = NapomenaIzlaz;
            myCommand.Parameters.Add(parameter27);



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
                throw new Exception("Neuspešan upis aktivnosti u Bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis aktivnosti", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }


            }
        }

        public void DeleteStatusNjave(int ID)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteStatusNajave";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
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
                    MessageBox.Show("Brisanje Cena uspešno završeno", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void UpdRkShippingZavrseno(string DatumUtovara, string Vozilo)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateRkShippingZavrseno";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter paramDatum = new SqlParameter();
            paramDatum.ParameterName = "@DatumUtovara";
            paramDatum.SqlDbType = SqlDbType.NVarChar;
            paramDatum.Size = 20;
            paramDatum.Direction = ParameterDirection.Input;
            paramDatum.Value = DatumUtovara;
            myCommand.Parameters.Add(paramDatum);

            SqlParameter paramVozilo = new SqlParameter();
            paramVozilo.ParameterName = "@Vozilo";
            paramVozilo.SqlDbType = SqlDbType.NChar;
            paramVozilo.Size = 50;
            paramVozilo.Direction = ParameterDirection.Input;
            paramVozilo.Value = Vozilo;
            myCommand.Parameters.Add(paramVozilo);

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
                throw new Exception("Neuspešan upis aktivnosti u Bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis aktivnosti", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }

            }
        }
        public void UpdRkShippingPocetna(string DatumUtovara, string Vozilo)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateRkShippingPocetna";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter paramDatum = new SqlParameter();
            paramDatum.ParameterName = "@DatumUtovara";
            paramDatum.SqlDbType = SqlDbType.NVarChar;
            paramDatum.Size = 20;
            paramDatum.Direction = ParameterDirection.Input;
            paramDatum.Value = DatumUtovara;
            myCommand.Parameters.Add(paramDatum);

            SqlParameter paramVozilo = new SqlParameter();
            paramVozilo.ParameterName = "@Vozilo";
            paramVozilo.SqlDbType = SqlDbType.NChar;
            paramVozilo.Size = 50;
            paramVozilo.Direction = ParameterDirection.Input;
            paramVozilo.Value = Vozilo;
            myCommand.Parameters.Add(paramVozilo);

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
                throw new Exception("Neuspešan upis aktivnosti u Bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis aktivnosti", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }

            }
        }
        public void UpdRkShipping(string DatumUtovara, string Vozilo)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateRkShipping";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter paramDatum = new SqlParameter();
            paramDatum.ParameterName = "@DatumUtovara";
            paramDatum.SqlDbType = SqlDbType.NVarChar;
            paramDatum.Size = 20;
            paramDatum.Direction = ParameterDirection.Input;
            paramDatum.Value = DatumUtovara;
            myCommand.Parameters.Add(paramDatum);

            SqlParameter paramVozilo = new SqlParameter();
            paramVozilo.ParameterName = "@Vozilo";
            paramVozilo.SqlDbType = SqlDbType.NChar;
            paramVozilo.Size = 50;
            paramVozilo.Direction = ParameterDirection.Input;
            paramVozilo.Value = Vozilo;
            myCommand.Parameters.Add(paramVozilo);

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
                throw new Exception("Neuspešan upis aktivnosti u Bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis aktivnosti", "",
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
