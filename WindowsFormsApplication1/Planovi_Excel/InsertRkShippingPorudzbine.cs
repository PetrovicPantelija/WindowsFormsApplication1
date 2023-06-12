using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Planovi_Excel
{
    class InsertRkShippingPorudzbine
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public void InsertRkShipping(int ID, int LargoProizvodID, string LargoProizvodNaziv, int LargoPaketID, string LargoPaketNaziv, int Kolicina, string Relacija, string RelacijaNaziv, string RelacijaDatum)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRkShippingExportPorudzbina";
            cmd.CommandType = CommandType.StoredProcedure;
            /*

            [ID] [int] NULL,
	[LargoProizvodID] [int] NULL,
	[LargoProizvodNaziv] [nvarchar](100) NULL,
	[LargoPaketID] [int] NULL,
	[LargoPaketNaziv] [nvarchar](50) NULL,
	[Kolicina] [int] NULL,
	[Relacija] [nvarchar](250) NULL,
	[RelacijaNaziv] [nvarchar](250) NULL,
	[RelacijaDatum] [nvarchar](250) NULL
            */



            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter largoproizvodid = new SqlParameter();
            largoproizvodid.ParameterName = "@LargoProizvodID";
            largoproizvodid.SqlDbType = SqlDbType.Int;
            largoproizvodid.Direction = ParameterDirection.Input;
            largoproizvodid.Value = LargoProizvodID;
            cmd.Parameters.Add(largoproizvodid);


            SqlParameter largoproizvodNaziv = new SqlParameter();
            largoproizvodNaziv.ParameterName = "@LargoProizvodNaziv";
            largoproizvodNaziv.SqlDbType = SqlDbType.NVarChar;
            largoproizvodNaziv.Size = 100;
            largoproizvodNaziv.Direction = ParameterDirection.Input;
            largoproizvodNaziv.Value = LargoProizvodNaziv;
            cmd.Parameters.Add(largoproizvodNaziv);


            SqlParameter largopaketid = new SqlParameter();
            largopaketid.ParameterName = "@LargoPaketID";
            largopaketid.SqlDbType = SqlDbType.Int;
            largopaketid.Direction = ParameterDirection.Input;
            largopaketid.Value = LargoPaketID;
            cmd.Parameters.Add(largopaketid);

            SqlParameter largopaketNaziv = new SqlParameter();
            largopaketNaziv.ParameterName = "@LargoPaketNaziv";
            largopaketNaziv.SqlDbType = SqlDbType.NVarChar;
            largopaketNaziv.Size = 50;
            largopaketNaziv.Direction = ParameterDirection.Input;
            largopaketNaziv.Value = LargoPaketNaziv;
            cmd.Parameters.Add(largopaketNaziv);


            SqlParameter kolicina = new SqlParameter();
            kolicina.ParameterName = "@Kolicina";
            kolicina.SqlDbType = SqlDbType.Int;
            kolicina.Direction = ParameterDirection.Input;
            kolicina.Value = Kolicina;
            cmd.Parameters.Add(kolicina);


            SqlParameter relacija = new SqlParameter();
            relacija.ParameterName = "@Relacija";
            relacija.SqlDbType = SqlDbType.NVarChar;
            relacija.Size = 250;
            relacija.Direction = ParameterDirection.Input;
            relacija.Value = Relacija;
            cmd.Parameters.Add(relacija);



            SqlParameter relacijanaziv = new SqlParameter();
            relacijanaziv.ParameterName = "@RelacijaNaziv";
            relacijanaziv.SqlDbType = SqlDbType.NVarChar;
            relacijanaziv.Size = 250;
            relacijanaziv.Direction = ParameterDirection.Input;
            relacijanaziv.Value = RelacijaNaziv;
            cmd.Parameters.Add(relacijanaziv);


            SqlParameter relacijadatum = new SqlParameter();
            relacijadatum.ParameterName = "@RelacijaDatum";
            relacijadatum.SqlDbType = SqlDbType.NVarChar;
            relacijadatum.Size = 250;
            relacijadatum.Direction = ParameterDirection.Input;
            relacijadatum.Value = RelacijaDatum;
            cmd.Parameters.Add(relacijadatum);



            // [Relacija] [nvarchar](250) NULL,
            // [RelacijaNaziv] [nvarchar](250) NULL,
            //	[RelacijaDatum] [nvarchar](250) NULL



            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = conn.BeginTransaction();
                cmd.Transaction = myTransaction;
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
                throw new Exception("Neuspešan upis porudzbina");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos   je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }


    }
}
