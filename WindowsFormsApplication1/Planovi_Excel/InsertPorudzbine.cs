using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Planovi_Excel
{
    class InsertPorudzbine
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public void InsertRKStavkePOrudzbine(int ID, int LargoProizvodID, string LargoProizvodNaziv, int LargoPaketID, string LargoPaketNaziv, int Kolicina, string Relacija, string RelacijaNaziv, string RelacijaDatum)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRkShippingExportPorudzbina";
            cmd.CommandType = CommandType.StoredProcedure;

            /* @ID[int] ,
                     @LargoProizvodID[int] ,
                     @LargoProizvodNaziv[nvarchar](100) ,
                     @LargoPaketID[int] ,
                     @LargoPaketNaziv[nvarchar](50),
                     @Kolicina[int] ,
                     @Relacija[nvarchar](250) ,
                     @RelacijaNaziv[nvarchar](250) ,
                     @RelacijaDatum[nvarchar](250)
            */

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);


            SqlParameter largoProizvodID = new SqlParameter();
            largoProizvodID.ParameterName = "@LargoProizvodID";
            largoProizvodID.SqlDbType = SqlDbType.NVarChar;
            largoProizvodID.Size = 100;
            largoProizvodID.Direction = ParameterDirection.Input;
            largoProizvodID.Value = ID;
            cmd.Parameters.Add(largoProizvodID);

            SqlParameter largoProizvodNaziv = new SqlParameter();
            largoProizvodNaziv.ParameterName = "@LargoProizvodNaziv";
            largoProizvodNaziv.SqlDbType = SqlDbType.NVarChar;
            largoProizvodNaziv.Size = 100;
            largoProizvodNaziv.Direction = ParameterDirection.Input;
            largoProizvodNaziv.Value = LargoProizvodNaziv;
            cmd.Parameters.Add(largoProizvodNaziv);


            SqlParameter largoPaketID = new SqlParameter();
            largoPaketID.ParameterName = "@LargoPaketID";
            largoPaketID.SqlDbType = SqlDbType.Int;
            largoPaketID.Direction = ParameterDirection.Input;
            largoPaketID.Value = LargoPaketID;
            cmd.Parameters.Add(largoPaketID);

            SqlParameter largoPaketNaziv = new SqlParameter();
            largoPaketNaziv.ParameterName = "@LargoPaketNaziv";
            largoPaketNaziv.SqlDbType = SqlDbType.NVarChar;
            largoPaketNaziv.Size = 50;
            largoPaketNaziv.Direction = ParameterDirection.Input;
            largoPaketNaziv.Value = LargoPaketNaziv;
            cmd.Parameters.Add(largoPaketNaziv);


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


            SqlParameter relacijaNaziv = new SqlParameter();
            relacijaNaziv.ParameterName = "@RelacijaNaziv";
            relacijaNaziv.SqlDbType = SqlDbType.NVarChar;
            relacijaNaziv.Size = 250;
            relacijaNaziv.Direction = ParameterDirection.Input;
            relacijaNaziv.Value = RelacijaNaziv;
            cmd.Parameters.Add(relacijaNaziv);


            SqlParameter relacijaDatum = new SqlParameter();
            relacijaDatum.ParameterName = "@RelacijaDatum";
            relacijaDatum.SqlDbType = SqlDbType.NVarChar;
            relacijaDatum.Size = 250;
            relacijaDatum.Direction = ParameterDirection.Input;
            relacijaDatum.Value = RelacijaDatum;
            cmd.Parameters.Add(relacijaDatum);

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
                throw new Exception("Neuspešan upis NHM brojeva");
            }
            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos NHM broja je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void DeleteRkStavkePorudzbine()
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteRkShippingExportPorudzbine";
            cmd.CommandType = CommandType.StoredProcedure;

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
                throw new Exception("Neuspešan upis NHM brojeva");
            }
            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos NHM broja je uspešno završena", "",
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

