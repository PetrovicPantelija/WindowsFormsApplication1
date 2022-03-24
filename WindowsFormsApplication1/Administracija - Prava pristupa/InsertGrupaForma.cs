using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Administracija___Prava_pristupa
{
    class InsertGrupaForma
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public void InsGrupaForma(int IdGrupe,int IdForme,bool Upis,bool Izmena,bool Brisanje)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertGrupaForma";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter IdG = new SqlParameter();
            IdG.ParameterName = "@IdGrupe";
            IdG.SqlDbType = SqlDbType.Int;
            IdG.Direction = ParameterDirection.Input;
            IdG.Value = IdGrupe;
            cmd.Parameters.Add(IdG);

            SqlParameter IdF = new SqlParameter();
            IdF.ParameterName = "@IdForme";
            IdF.SqlDbType = SqlDbType.Int;
            IdF.Direction = ParameterDirection.Input;
            IdF.Value = IdForme;
            cmd.Parameters.Add(IdF);

            SqlParameter upis = new SqlParameter();
            upis.ParameterName = "@Upis";
            upis.SqlDbType = SqlDbType.Bit;
            upis.Direction = ParameterDirection.Input;
            upis.Value = Upis;
            cmd.Parameters.Add(upis);

            SqlParameter izmena = new SqlParameter();
            izmena.ParameterName = "@Izmena";
            izmena.SqlDbType = SqlDbType.Bit;
            izmena.Direction = ParameterDirection.Input;
            izmena.Value = Izmena;
            cmd.Parameters.Add(izmena);

            SqlParameter brisanje = new SqlParameter();
            brisanje.ParameterName = "@Brisanje";
            brisanje.SqlDbType = SqlDbType.Bit;
            brisanje.Direction = ParameterDirection.Input;
            brisanje.Value = Brisanje;
            cmd.Parameters.Add(brisanje);

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
        public void UpdGrupaForma(int ID,int IdGrupe, int IdForme, bool Upis, bool Izmena, bool Brisanje)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateGrupaForma";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter IdG = new SqlParameter();
            IdG.ParameterName = "@IdGrupe";
            IdG.SqlDbType = SqlDbType.Int;
            IdG.Direction = ParameterDirection.Input;
            IdG.Value = IdGrupe;
            cmd.Parameters.Add(IdG);

            SqlParameter IdF = new SqlParameter();
            IdF.ParameterName = "@IdForme";
            IdF.SqlDbType = SqlDbType.Int;
            IdF.Direction = ParameterDirection.Input;
            IdF.Value = IdForme;
            cmd.Parameters.Add(IdF);

            SqlParameter upis = new SqlParameter();
            upis.ParameterName = "@Upis";
            upis.SqlDbType = SqlDbType.Bit;
            upis.Direction = ParameterDirection.Input;
            upis.Value = Upis;
            cmd.Parameters.Add(upis);

            SqlParameter izmena = new SqlParameter();
            izmena.ParameterName = "@Izmena";
            izmena.SqlDbType = SqlDbType.Bit;
            izmena.Direction = ParameterDirection.Input;
            izmena.Value = Izmena;
            cmd.Parameters.Add(izmena);

            SqlParameter brisanje = new SqlParameter();
            brisanje.ParameterName = "@Brisanje";
            brisanje.SqlDbType = SqlDbType.Bit;
            brisanje.Direction = ParameterDirection.Input;
            brisanje.Value = Brisanje;
            cmd.Parameters.Add(brisanje);

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
        public void DelGrupaForma(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteGrupaForma";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = @"ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

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
