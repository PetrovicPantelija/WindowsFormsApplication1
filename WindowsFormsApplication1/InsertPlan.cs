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
    class InsertPlan
    {
        public void InsertPlanStavke(string PlanNaziv, int MPSifra, string MPStaraSif, string LokSkSifra, string LokOpis, double Kolicina, int termin, DateTime Datum1, int Termin1, DateTime Datum2, string RN)
        {

         //   MessageBox.Show(PlanNaziv.ToString() + ' ' + MPSifra.ToString() + ' ' + MPStaraSif.ToString() + ' ' + LokSkSifra.ToString() + ' ' + LokOpis.ToString() + ' ' + Kolicina.ToString() + ' ' + termin.ToString() + ' ' + Datum1.ToString() + ' ' + Termin1.ToString() + ' ' + Datum2.ToString());
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertPlan";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@PlanNaziv";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 50;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = PlanNaziv;
            myCommand.Parameters.Add(parameter);
            // PrepareCommand(myCommand);
           
            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@MpSifra";
            parameter2.SqlDbType = SqlDbType.Int;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = MPSifra;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@MPStaraSif";
            parameter3.SqlDbType = SqlDbType.NVarChar;
            parameter3.Size = 50;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = MPStaraSif;
            myCommand.Parameters.Add(parameter3);
          
              SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@LokSkSifra";
            parameter4.SqlDbType = SqlDbType.NVarChar;
            parameter4.Size = 50;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = LokSkSifra;
            myCommand.Parameters.Add(parameter4);
          
            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@LokOpis";
            parameter5.SqlDbType = SqlDbType.NVarChar;
            parameter5.Size = 100;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = LokOpis;
            myCommand.Parameters.Add(parameter5);
           
            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@Kolicina";
            parameter6.SqlDbType = SqlDbType.Decimal;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = Kolicina;
            myCommand.Parameters.Add(parameter6);
          
           SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Termin";
            parameter7.SqlDbType = SqlDbType.Int;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Termin1;
            myCommand.Parameters.Add(parameter7);

             SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@Datum1";
            parameter8.SqlDbType = SqlDbType.DateTime;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = Datum1;
            myCommand.Parameters.Add(parameter8);

           SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@Termin2";
            parameter9.SqlDbType = SqlDbType.Int;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = Termin1;
            myCommand.Parameters.Add(parameter9);

             SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@Datum2";
            parameter10.SqlDbType = SqlDbType.DateTime;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = Datum2;
            myCommand.Parameters.Add(parameter10);

            SqlParameter parameter11 = new SqlParameter();
            parameter11.ParameterName = "@RN";
            parameter11.SqlDbType = SqlDbType.NVarChar;
            parameter11.Size = 10;
            parameter11.Direction = ParameterDirection.Input;
            parameter11.Value = RN;
            myCommand.Parameters.Add(parameter11);
          
         

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
                throw new Exception("Neuspešan upis zaglavlja isporuke");
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

        public void UpdatePlanStavke(int ID,  int termin, DateTime Datum1)
        {

            //   MessageBox.Show(PlanNaziv.ToString() + ' ' + MPSifra.ToString() + ' ' + MPStaraSif.ToString() + ' ' + LokSkSifra.ToString() + ' ' + LokOpis.ToString() + ' ' + Kolicina.ToString() + ' ' + termin.ToString() + ' ' + Datum1.ToString() + ' ' + Termin1.ToString() + ' ' + Datum2.ToString());
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdatePlan";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
        
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);
            // PrepareCommand(myCommand);

           

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Termin";
            parameter7.SqlDbType = SqlDbType.Int;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = termin;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@Datum1";
            parameter8.SqlDbType = SqlDbType.DateTime;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = Datum1;
            myCommand.Parameters.Add(parameter8);


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
                throw new Exception("Neuspešan upis zaglavlja isporuke");
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

        public void UpdatePlanStavkeNeto(int ID, int termin,  string PlanNaziv)
        {

            //   MessageBox.Show(PlanNaziv.ToString() + ' ' + MPSifra.ToString() + ' ' + MPStaraSif.ToString() + ' ' + LokSkSifra.ToString() + ' ' + LokOpis.ToString() + ' ' + Kolicina.ToString() + ' ' + termin.ToString() + ' ' + Datum1.ToString() + ' ' + Termin1.ToString() + ' ' + Datum2.ToString());
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdatePlanStavkeNeto";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;

            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);
            // PrepareCommand(myCommand);



            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Termin";
            parameter7.SqlDbType = SqlDbType.Int;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = termin;
            myCommand.Parameters.Add(parameter7);

         

            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@PlanNaziv";
            parameter9.SqlDbType = SqlDbType.NVarChar;
            parameter9.Size = 50;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = PlanNaziv;
            myCommand.Parameters.Add(parameter9);


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
                throw new Exception("Neuspešan upis zaglavlja isporuke");
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

        public void InsertPlanStavkeNeto( string PlanNaziv, int MPSifra , string  MPStaraSif , string JM  ,  double Kolicina  ,int RedniBroj  ,  string Grupa  , string Napomena  , string LokSkSifra  ,string LokOpis ,int Termin  , int MAXTermin )
        {

            //   MessageBox.Show(PlanNaziv.ToString() + ' ' + MPSifra.ToString() + ' ' + MPStaraSif.ToString() + ' ' + LokSkSifra.ToString() + ' ' + LokOpis.ToString() + ' ' + Kolicina.ToString() + ' ' + termin.ToString() + ' ' + Datum1.ToString() + ' ' + Termin1.ToString() + ' ' + Datum2.ToString());
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertPlanNeto";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;


            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@PlanNaziv";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 50;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = PlanNaziv;
            myCommand.Parameters.Add(parameter);
            // PrepareCommand(myCommand);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@MpSifra";
            parameter2.SqlDbType = SqlDbType.Int;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = MPSifra;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@MPStaraSif";
            parameter3.SqlDbType = SqlDbType.NVarChar;
            parameter3.Size = 50;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = MPStaraSif;
            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@JM";
            parameter4.SqlDbType = SqlDbType.NVarChar;
            parameter4.Size = 50;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = JM;
            myCommand.Parameters.Add(parameter4);
             
             SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@Kolicina";
            parameter5.SqlDbType = SqlDbType.Decimal;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = Kolicina;
            myCommand.Parameters.Add(parameter5);
          
        SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@RedniBroj";
            parameter6.SqlDbType = SqlDbType.Int;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = RedniBroj;
            myCommand.Parameters.Add(parameter6);

         
            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Grupa";
            parameter7.SqlDbType = SqlDbType.NVarChar;
            parameter7.Size = 50;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Grupa;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@Napomena";
            parameter8.SqlDbType = SqlDbType.NVarChar;
            parameter8.Size = 50;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = Napomena;
            myCommand.Parameters.Add(parameter8);

          SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@LokSkSifra";
            parameter9.SqlDbType = SqlDbType.NVarChar;
            parameter9.Size = 50;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = LokSkSifra;
            myCommand.Parameters.Add(parameter9);

            SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@LokOpis";
            parameter10.SqlDbType = SqlDbType.NVarChar;
            parameter10.Size = 100;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = LokOpis;
            myCommand.Parameters.Add(parameter10);
         
           SqlParameter parameter11 = new SqlParameter();
            parameter11.ParameterName = "@Termin";
            parameter11.SqlDbType = SqlDbType.Int;
            parameter11.Direction = ParameterDirection.Input;
            parameter11.Value = Termin;
            myCommand.Parameters.Add(parameter11);

            SqlParameter parameter12 = new SqlParameter();
            parameter12.ParameterName = "@MAXTermin";
            parameter12.SqlDbType = SqlDbType.Int;
            parameter12.Direction = ParameterDirection.Input;
            parameter12.Value = MAXTermin;
            myCommand.Parameters.Add(parameter12);

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
                throw new Exception("Neuspešan upis zaglavlja isporuke");
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

        public void UpdateTermin(string PlanNaziv, int MPSifra)
        {

            //   MessageBox.Show(PlanNaziv.ToString() + ' ' + MPSifra.ToString() + ' ' + MPStaraSif.ToString() + ' ' + LokSkSifra.ToString() + ' ' + LokOpis.ToString() + ' ' + Kolicina.ToString() + ' ' + termin.ToString() + ' ' + Datum1.ToString() + ' ' + Termin1.ToString() + ' ' + Datum2.ToString());
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateTermin";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@PlanNaziv";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 50;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = PlanNaziv;
            myCommand.Parameters.Add(parameter);
          

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@MPSifra";
            parameter1.SqlDbType = SqlDbType.Int;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = MPSifra;
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
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
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

        public void UpdateTermin2(string PlanNaziv)
        {

            //   MessageBox.Show(PlanNaziv.ToString() + ' ' + MPSifra.ToString() + ' ' + MPStaraSif.ToString() + ' ' + LokSkSifra.ToString() + ' ' + LokOpis.ToString() + ' ' + Kolicina.ToString() + ' ' + termin.ToString() + ' ' + Datum1.ToString() + ' ' + Termin1.ToString() + ' ' + Datum2.ToString());
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateTermin2";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@PlanNaziv";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 50;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = PlanNaziv;
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
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
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

        public void UpdateMAXTermin(string PlanNaziv)
        {

            //   MessageBox.Show(PlanNaziv.ToString() + ' ' + MPSifra.ToString() + ' ' + MPStaraSif.ToString() + ' ' + LokSkSifra.ToString() + ' ' + LokOpis.ToString() + ' ' + Kolicina.ToString() + ' ' + termin.ToString() + ' ' + Datum1.ToString() + ' ' + Termin1.ToString() + ' ' + Datum2.ToString());
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateMAXTermin";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@PlanNaziv";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 50;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = PlanNaziv;
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
                throw new Exception("Neuspešan upis zaglavlja isporuke");
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

        public void UpdateMAXTermin2(string PlanNaziv)
        {

            //   MessageBox.Show(PlanNaziv.ToString() + ' ' + MPSifra.ToString() + ' ' + MPStaraSif.ToString() + ' ' + LokSkSifra.ToString() + ' ' + LokOpis.ToString() + ' ' + Kolicina.ToString() + ' ' + termin.ToString() + ' ' + Datum1.ToString() + ' ' + Termin1.ToString() + ' ' + Datum2.ToString());
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateMAXTermin2";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@PlanNaziv";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 50;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = PlanNaziv;
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
                throw new Exception("Neuspešan upis zaglavlja isporuke");
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

        public void UpdatePlanStavkeNetoRN(string PlanNaziv)
        {

            //   MessageBox.Show(PlanNaziv.ToString() + ' ' + MPSifra.ToString() + ' ' + MPStaraSif.ToString() + ' ' + LokSkSifra.ToString() + ' ' + LokOpis.ToString() + ' ' + Kolicina.ToString() + ' ' + termin.ToString() + ' ' + Datum1.ToString() + ' ' + Termin1.ToString() + ' ' + Datum2.ToString());
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdatePlanStavkeNetoRN";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@PlanNaziv";
            parameter9.SqlDbType = SqlDbType.NVarChar;
            parameter9.Size = 50;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = PlanNaziv;
            myCommand.Parameters.Add(parameter9);


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
                throw new Exception("Neuspešan upis ");
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

        public void UpdatePlanStavkeNetoRNStatus(string PlanNaziv)
        {

            //   MessageBox.Show(PlanNaziv.ToString() + ' ' + MPSifra.ToString() + ' ' + MPStaraSif.ToString() + ' ' + LokSkSifra.ToString() + ' ' + LokOpis.ToString() + ' ' + Kolicina.ToString() + ' ' + termin.ToString() + ' ' + Datum1.ToString() + ' ' + Termin1.ToString() + ' ' + Datum2.ToString());
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "TerminiranjeSrediStatuseRN";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@PlanNaziv";
            parameter9.SqlDbType = SqlDbType.NVarChar;
            parameter9.Size = 50;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = PlanNaziv;
            myCommand.Parameters.Add(parameter9);


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
                throw new Exception("Neuspešan upis ");
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
