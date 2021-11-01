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
    class InsertReklamacijaKalkulator
    {
        public void InsertCenaSonja(int CnSifMp, string CnRazTipCn, string CnValutaCene, decimal CnCena, string CnMpStaraSif)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertSonjaCena";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

         
            
            
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@CnSifMp";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = CnSifMp;
            myCommand.Parameters.Add(parameter);


            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@CnRazTipCn";
            parameter4.SqlDbType = SqlDbType.Char;
            parameter4.Size = 4;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = CnRazTipCn;
            myCommand.Parameters.Add(parameter4);

            /* 

@CnMpStaraSif char(24) */

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@CnValutaCene";
            parameter5.SqlDbType = SqlDbType.Char;
            parameter5.Size = 3;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = CnValutaCene;
            myCommand.Parameters.Add(parameter5);

           

            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@CnCena";
            parameter9.SqlDbType = SqlDbType.Decimal;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = CnCena;
            myCommand.Parameters.Add(parameter9);

            SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@CnMpStaraSif";
            parameter10.SqlDbType = SqlDbType.Char;
            parameter10.Size = 24;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = CnMpStaraSif;
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
                throw new Exception("Neuspešan upis zaglavlja ");
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
