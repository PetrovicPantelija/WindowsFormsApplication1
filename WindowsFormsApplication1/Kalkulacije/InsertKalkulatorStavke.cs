using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Kalkulacije
{
    class InsertKalkulatorStavke
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public void InsKalkulatorStavke(int MpSifra,int IDKalkulator,decimal TrosakProizvodnje,decimal Avans,decimal Provizija,decimal Transport,
            decimal VpCena,decimal Rabat,decimal Marza,decimal Akcija,decimal MpCena)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertKalkulatorStavke";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter mpSifra = new SqlParameter();
            mpSifra.ParameterName = "@MpSifra";
            mpSifra.SqlDbType = SqlDbType.Int;
            mpSifra.Direction = ParameterDirection.Input;
            mpSifra.Value = MpSifra;
            cmd.Parameters.Add(mpSifra);

            SqlParameter idKalkulator = new SqlParameter();
            idKalkulator.ParameterName = "@IDKalkulator";
            idKalkulator.SqlDbType = SqlDbType.Int;
            idKalkulator.Direction = ParameterDirection.Input;
            idKalkulator.Value = IDKalkulator;
            cmd.Parameters.Add(idKalkulator);

            SqlParameter trosak = new SqlParameter();
            trosak.ParameterName = "@TrosakProizvodnje";
            trosak.SqlDbType = SqlDbType.Decimal;
            trosak.Direction = ParameterDirection.Input;
            trosak.Value = TrosakProizvodnje;
            cmd.Parameters.Add(trosak);

            SqlParameter provizija = new SqlParameter();
            provizija.ParameterName = "@Provizija";
            provizija.SqlDbType = SqlDbType.Decimal;
            provizija.Direction = ParameterDirection.Input;
            provizija.Value =Provizija;
            cmd.Parameters.Add(provizija);

            SqlParameter avans = new SqlParameter();
            avans.ParameterName = "@Avans";
            avans.SqlDbType = SqlDbType.Decimal;
            avans.Direction = ParameterDirection.Input;
            avans.Value = Avans;
            cmd.Parameters.Add(avans);

            SqlParameter transport = new SqlParameter();
            transport.ParameterName = "@Transport";
            transport.SqlDbType = SqlDbType.Decimal;
            transport.Direction = ParameterDirection.Input;
            transport.Value = Transport;
            cmd.Parameters.Add(transport);

            SqlParameter vp = new SqlParameter();
            vp.ParameterName = "@VPCena";
            vp.SqlDbType = SqlDbType.Decimal;
            vp.Direction = ParameterDirection.Input;
            vp.Value = VpCena;
            cmd.Parameters.Add(vp);

            SqlParameter rabat = new SqlParameter();
            rabat.ParameterName = "@Rabat";
            rabat.SqlDbType = SqlDbType.Decimal;
            rabat.Direction = ParameterDirection.Input;
            rabat.Value = Rabat;
            cmd.Parameters.Add(rabat);

            SqlParameter marza = new SqlParameter();
            marza.ParameterName = "@Marza";
            marza.SqlDbType = SqlDbType.Decimal;
            marza.Direction = ParameterDirection.Input;
            marza.Value = Marza;
            cmd.Parameters.Add(marza);

            SqlParameter akcija = new SqlParameter();
            akcija.ParameterName = "@Akcija";
            akcija.SqlDbType = SqlDbType.Decimal;
            akcija.Direction = ParameterDirection.Input;
            akcija.Value = Akcija;
            cmd.Parameters.Add(akcija);

            SqlParameter mp = new SqlParameter();
            mp.ParameterName = "@MPCena";
            mp.SqlDbType = SqlDbType.Decimal;
            mp.Direction = ParameterDirection.Input;
            mp.Value = MpCena;
            cmd.Parameters.Add(mp);

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

            catch (SqlException)
            {
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
        public void UpdKalkulatorStavke(int ID,int MpSifra, int IDKalkulator, decimal TrosakProizvodnje, decimal Avans, decimal Provizija, decimal Transport,
                    decimal VpCena, decimal Rabat, decimal Marza, decimal Akcija, decimal MpCena)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateKalkulatorStavke";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter mpSifra = new SqlParameter();
            mpSifra.ParameterName = "@MpSifra";
            mpSifra.SqlDbType = SqlDbType.Int;
            mpSifra.Direction = ParameterDirection.Input;
            mpSifra.Value = MpSifra;
            cmd.Parameters.Add(mpSifra);

            SqlParameter idKalkulator = new SqlParameter();
            idKalkulator.ParameterName = "@IDKalkulator";
            idKalkulator.SqlDbType = SqlDbType.Int;
            idKalkulator.Direction = ParameterDirection.Input;
            idKalkulator.Value = IDKalkulator;
            cmd.Parameters.Add(idKalkulator);

            SqlParameter trosak = new SqlParameter();
            trosak.ParameterName = "@TrosakProizvodnje";
            trosak.SqlDbType = SqlDbType.Decimal;
            trosak.Direction = ParameterDirection.Input;
            trosak.Value = TrosakProizvodnje;
            cmd.Parameters.Add(trosak);

            SqlParameter provizija = new SqlParameter();
            provizija.ParameterName = "@Provizija";
            provizija.SqlDbType = SqlDbType.Decimal;
            provizija.Direction = ParameterDirection.Input;
            provizija.Value = Provizija;
            cmd.Parameters.Add(provizija);

            SqlParameter avans = new SqlParameter();
            avans.ParameterName = "@Avans";
            avans.SqlDbType = SqlDbType.Decimal;
            avans.Direction = ParameterDirection.Input;
            avans.Value = Avans;
            cmd.Parameters.Add(avans);

            SqlParameter transport = new SqlParameter();
            transport.ParameterName = "@Transport";
            transport.SqlDbType = SqlDbType.Decimal;
            transport.Direction = ParameterDirection.Input;
            transport.Value = Transport;
            cmd.Parameters.Add(transport);

            SqlParameter vp = new SqlParameter();
            vp.ParameterName = "@VPCena";
            vp.SqlDbType = SqlDbType.Decimal;
            vp.Direction = ParameterDirection.Input;
            vp.Value = VpCena;
            cmd.Parameters.Add(vp);

            SqlParameter rabat = new SqlParameter();
            rabat.ParameterName = "@Rabat";
            rabat.SqlDbType = SqlDbType.Decimal;
            rabat.Direction = ParameterDirection.Input;
            rabat.Value = Rabat;
            cmd.Parameters.Add(rabat);

            SqlParameter marza = new SqlParameter();
            marza.ParameterName = "@Marza";
            marza.SqlDbType = SqlDbType.Decimal;
            marza.Direction = ParameterDirection.Input;
            marza.Value = Marza;
            cmd.Parameters.Add(marza);

            SqlParameter akcija = new SqlParameter();
            akcija.ParameterName = "@Akcija";
            akcija.SqlDbType = SqlDbType.Decimal;
            akcija.Direction = ParameterDirection.Input;
            akcija.Value = Akcija;
            cmd.Parameters.Add(akcija);

            SqlParameter mp = new SqlParameter();
            mp.ParameterName = "@MPCena";
            mp.SqlDbType = SqlDbType.Decimal;
            mp.Direction = ParameterDirection.Input;
            mp.Value = MpCena;
            cmd.Parameters.Add(mp);

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

            catch (SqlException)
            {
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
        public void DelKalkulatorStavke(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteKalkulatorStavke";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
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
            catch (SqlException)
            {
                throw new Exception("Neuspešan upis NHM brojeva");
            }
            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Brisanje uspešno", "",
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
