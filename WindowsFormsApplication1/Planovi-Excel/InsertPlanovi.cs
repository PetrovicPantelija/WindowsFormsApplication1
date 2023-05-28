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
    class InsertPlanovi
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public void InsertPlan(string Naziv, DateTime DatumOd, DateTime DatumDo, string Napomena, string Status)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertPlanAktiv";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter naziv = new SqlParameter();
            naziv.ParameterName = "@Naziv";
            naziv.SqlDbType = SqlDbType.NVarChar;
            naziv.Size = 100;
            naziv.Direction = ParameterDirection.Input;
            naziv.Value = Naziv;
            cmd.Parameters.Add(naziv);

            SqlParameter datOD = new SqlParameter();
            datOD.ParameterName = "@DatumOd";
            datOD.SqlDbType = SqlDbType.Date;
            datOD.Direction = ParameterDirection.Input;
            datOD.Value = DatumOd;
            cmd.Parameters.Add(datOD);

            SqlParameter datDO = new SqlParameter();
            datDO.ParameterName = "@DatumDo";
            datDO.SqlDbType = SqlDbType.Date;
            datDO.Direction = ParameterDirection.Input;
            datDO.Value = DatumDo;
            cmd.Parameters.Add(datDO);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter status = new SqlParameter();
            status.ParameterName = "@Status";
            status.SqlDbType = SqlDbType.NVarChar;
            status.Size = 10;
            status.Value = Status;
            cmd.Parameters.Add(status);


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
        public void InsertPlanStavke(int IDPlana,int IDPrograma,int IDKolekcije,int MpSifra)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertPlanAktivStavke";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idP = new SqlParameter();
            idP.ParameterName = "@IDPlana";
            idP.SqlDbType = SqlDbType.Int;
            idP.Value = IDPlana;
            cmd.Parameters.Add(idP);

            SqlParameter program = new SqlParameter();
            program.ParameterName = "@IDPrograma";
            program.SqlDbType = SqlDbType.Int;
            program.Value = IDPrograma;
            cmd.Parameters.Add(program);

            SqlParameter kol = new SqlParameter();
            kol.ParameterName = "@IDKolekcije";
            kol.SqlDbType = SqlDbType.Int;
            kol.Value = IDKolekcije;
            cmd.Parameters.Add(kol);

            SqlParameter mpSifra = new SqlParameter();
            mpSifra.ParameterName = "@MpSifra";
            mpSifra.SqlDbType = SqlDbType.Int;
            mpSifra.Value = MpSifra;
            cmd.Parameters.Add(mpSifra);

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
        public void DeletePlanStavke(int Id)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeletePlanAktivStavke";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Value = Id;
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
        public void InsertPripadnostKP(int IdKol,int IdProgram,int MpSifra,int Tip,decimal Koeficijent)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertPripadnostKP";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idkol = new SqlParameter();
            idkol.ParameterName = "@IDKolekcije";
            idkol.SqlDbType = SqlDbType.Int;
            idkol.Value = IdKol;
            cmd.Parameters.Add(idkol);

            SqlParameter idProgram = new SqlParameter();
            idProgram.ParameterName = "@IDPrograma";
            idProgram.SqlDbType = SqlDbType.Int;
            idProgram.Value = IdProgram;
            cmd.Parameters.Add(idProgram);

            SqlParameter mp = new SqlParameter();
            mp.ParameterName = "@MpSifra";
            mp.SqlDbType = SqlDbType.Int;
            mp.Value = MpSifra;
            cmd.Parameters.Add(mp);

            SqlParameter tip = new SqlParameter();
            tip.ParameterName = "@TipPaketa";
            tip.SqlDbType = SqlDbType.Int;
            tip.Value = Tip;
            cmd.Parameters.Add(tip);

            SqlParameter koef = new SqlParameter();
            koef.ParameterName = "@Koeficijent";
            koef.SqlDbType = SqlDbType.Decimal;
            koef.Value = Koeficijent;
            cmd.Parameters.Add(koef);

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
        public void UpdatePripadnostKP(int Id,int IdKol, int IdProgram, int MpSifra, int Tip,decimal Koeficijent)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdatePripadnostKP";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Value = Id;
            cmd.Parameters.Add(id);

            SqlParameter idkol = new SqlParameter();
            idkol.ParameterName = "@IDKolekcije";
            idkol.SqlDbType = SqlDbType.Int;
            idkol.Value = IdKol;
            cmd.Parameters.Add(idkol);

            SqlParameter idProgram = new SqlParameter();
            idProgram.ParameterName = "@IDPrograma";
            idProgram.SqlDbType = SqlDbType.Int;
            idProgram.Value = IdProgram;
            cmd.Parameters.Add(idProgram);

            SqlParameter mp = new SqlParameter();
            mp.ParameterName = "@MpSifra";
            mp.SqlDbType = SqlDbType.Int;
            mp.Value = MpSifra;
            cmd.Parameters.Add(mp);

            SqlParameter tip = new SqlParameter();
            tip.ParameterName = "@TipPaketa";
            tip.SqlDbType = SqlDbType.Int;
            tip.Value = Tip;
            cmd.Parameters.Add(tip);

            SqlParameter koef = new SqlParameter();
            koef.ParameterName = "@Koeficijent";
            koef.SqlDbType = SqlDbType.Decimal;
            koef.Value = Koeficijent;
            cmd.Parameters.Add(koef);

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
        public void DeletePripadnostKP(int Id)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeletePripadnostKP";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Value = Id;
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
        public void ProglasiIzlaznost(int Id)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "AzurirajIzlaznost";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@IDPlana";
            id.SqlDbType = SqlDbType.Int;
            id.Value = Id;
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
        public void PoveziIzlaznost()
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PoveziIzlaznost";
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
        public void PoveziPlanove(int Plan1,int Plan2,int Plan3,int Plan4, int Plan5)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PoveziPlanove";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter plan1 = new SqlParameter();
            plan1.ParameterName = "@Plan1";
            plan1.SqlDbType = SqlDbType.Int;
            plan1.Value = Plan1;
            cmd.Parameters.Add(plan1);

            SqlParameter plan2 = new SqlParameter();
            plan2.ParameterName = "@Plan2";
            plan2.SqlDbType = SqlDbType.Int;
            plan2.Value = Plan2;
            cmd.Parameters.Add(plan2);


            SqlParameter plan3 = new SqlParameter();
            plan3.ParameterName = "@Plan3";
            plan3.SqlDbType = SqlDbType.Int;
            plan3.Value = Plan3;
            cmd.Parameters.Add(plan3);


            SqlParameter plan4 = new SqlParameter();
            plan4.ParameterName = "@Plan4";
            plan4.SqlDbType = SqlDbType.Int;
            plan4.Value = Plan4;
            cmd.Parameters.Add(plan4);


            SqlParameter plan5 = new SqlParameter();
            plan5.ParameterName = "@Plan5";
            plan5.SqlDbType = SqlDbType.Int;
            plan5.Value = Plan5;
            cmd.Parameters.Add(plan5);


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
        public void PoveziPredloge(int Mesec)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PoveziPredloge";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter mesec = new SqlParameter();
            mesec.ParameterName = "@Meseci1";
            mesec.SqlDbType = SqlDbType.Int;
            mesec.Value = Mesec;
            cmd.Parameters.Add(mesec);

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
        public void PoveziProseke()
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PoveziProseke";
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
        public void PoveziStanja()
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PoveziStanja";
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
        public void PoveziTrend()
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PoveziTrend";
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
        public void ArhivirajPlan(int PlanID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "ArhivirajPlan";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@IDPlana";
            id.SqlDbType = SqlDbType.Int;
            id.Value = PlanID;
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
        public void UpdatePlanAktivStavke(int Id,int IdPlana,int IdKolekcije,int IdPrograma,int MpSifra,decimal? Odluka)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdatePlanAktivStavke";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Value = Id;
            cmd.Parameters.Add(id);

            SqlParameter idP = new SqlParameter();
            idP.ParameterName = "@IDPlana";
            idP.SqlDbType = SqlDbType.Int;
            idP.Value = IdPlana;
            cmd.Parameters.Add(idP);

            SqlParameter idK = new SqlParameter();
            idK.ParameterName = "@IDKolekcije";
            idK.SqlDbType = SqlDbType.Int;
            idK.Value = IdKolekcije;
            cmd.Parameters.Add(idK);

            SqlParameter idProg = new SqlParameter();
            idProg.ParameterName = "@IDPrograma";
            idProg.SqlDbType = SqlDbType.Int;
            idProg.Value = IdPrograma;
            cmd.Parameters.Add(idProg);

            SqlParameter mp = new SqlParameter();
            mp.ParameterName = "@MpSifra";
            mp.SqlDbType = SqlDbType.Int;
            mp.Value = MpSifra;
            cmd.Parameters.Add(mp);

            SqlParameter odluka = new SqlParameter();
            odluka.ParameterName = "@KonacnaOdluka";
            odluka.SqlDbType = SqlDbType.Decimal;
            odluka.Value = Odluka;
            cmd.Parameters.Add(odluka);

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
