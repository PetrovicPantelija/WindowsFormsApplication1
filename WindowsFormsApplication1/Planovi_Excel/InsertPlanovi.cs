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

        public void DeletePlanSaStavkama(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeletePlanAktivSaStavkama";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            //  naziv.Size = 100;
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
                throw new Exception("Neuspešan brisanje Planova");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Brisanje Plana nije ispesno", "",
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
        public void ProglasiIzlaznost(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "AzurirajIzlaznost";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@IDPlana";
            id.SqlDbType = SqlDbType.Int;
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
        public void PoveziIzlaznost(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PoveziIzlaznost";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
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
        public void PoveziPlanove(int ID,int Plan1,int Plan2,int Plan3,int Plan4, int Plan5)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PoveziPlanove";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@IDPlana";
            id.SqlDbType = SqlDbType.Int;
            id.Value = ID;
            cmd.Parameters.Add(id);

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
        public void PoveziPredloge(int ID,int Predlog1Count,int Predlog2Count,
            int Predlog1m1, int Predlog1m2, int Predlog1m3, int Predlog1m4, int Predlog1m5, int Predlog1m6, int Predlog1m7, int Predlog1m8, int Predlog1m9, int Predlog1m10, int Predlog1m11, int Predlog1m12,
            int Predlog2m1, int Predlog2m2, int Predlog2m3, int Predlog2m4, int Predlog2m5, int Predlog2m6, int Predlog2m7, int Predlog2m8, int Predlog2m9, int Predlog2m10, int Predlog2m11, int Predlog2m12)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PoveziPredloge";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@IDPlana";
            id.SqlDbType = SqlDbType.Int;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter p1Count = new SqlParameter();
            p1Count.ParameterName = "@Predlog1Count";
            p1Count.SqlDbType = SqlDbType.Int;
            p1Count.Value = Predlog1Count;
            cmd.Parameters.Add(p1Count);

            SqlParameter p2Count = new SqlParameter();
            p2Count.ParameterName = "@Predlog2Count";
            p2Count.SqlDbType = SqlDbType.Int;
            p2Count.Value = Predlog2Count;
            cmd.Parameters.Add(p2Count);


            SqlParameter p1m1 = new SqlParameter();
            p1m1.ParameterName = "@Predlog1m1";
            p1m1.SqlDbType = SqlDbType.Int;
            p1m1.Value = Predlog1m1;
            cmd.Parameters.Add(p1m1);

            SqlParameter p1m2 = new SqlParameter();
            p1m2.ParameterName = "@Predlog1m2";
            p1m2.SqlDbType = SqlDbType.Int;
            p1m2.Value = Predlog1m2;
            cmd.Parameters.Add(p1m2);

            SqlParameter p1m3 = new SqlParameter();
            p1m3.ParameterName = "@Predlog1m3";
            p1m3.SqlDbType = SqlDbType.Int;
            p1m3.Value = Predlog1m3;
            cmd.Parameters.Add(p1m3);

            SqlParameter p1m4 = new SqlParameter();
            p1m4.ParameterName = "@Predlog1m4";
            p1m4.SqlDbType = SqlDbType.Int;
            p1m4.Value = Predlog1m4;
            cmd.Parameters.Add(p1m4);

            SqlParameter p1m5 = new SqlParameter();
            p1m5.ParameterName = "@Predlog1m5";
            p1m5.SqlDbType = SqlDbType.Int;
            p1m5.Value = Predlog1m5;
            cmd.Parameters.Add(p1m5);

            SqlParameter p1m6 = new SqlParameter();
            p1m6.ParameterName = "@Predlog1m6";
            p1m6.SqlDbType = SqlDbType.Int;
            p1m6.Value = Predlog1m6;
            cmd.Parameters.Add(p1m6);

            SqlParameter p1m7 = new SqlParameter();
            p1m7.ParameterName = "@Predlog1m7";
            p1m7.SqlDbType = SqlDbType.Int;
            p1m7.Value = Predlog1m7;
            cmd.Parameters.Add(p1m7);

            SqlParameter p1m8 = new SqlParameter();
            p1m8.ParameterName = "@Predlog1m8";
            p1m8.SqlDbType = SqlDbType.Int;
            p1m8.Value = Predlog1m8;
            cmd.Parameters.Add(p1m8);

            SqlParameter p1m9 = new SqlParameter();
            p1m9.ParameterName = "@Predlog1m9";
            p1m9.SqlDbType = SqlDbType.Int;
            p1m9.Value = Predlog1m9;
            cmd.Parameters.Add(p1m9);

            SqlParameter p1m10 = new SqlParameter();
            p1m10.ParameterName = "@Predlog1m10";
            p1m10.SqlDbType = SqlDbType.Int;
            p1m10.Value = Predlog1m10;
            cmd.Parameters.Add(p1m10);

            SqlParameter p1m11 = new SqlParameter();
            p1m11.ParameterName = "@Predlog1m11";
            p1m11.SqlDbType = SqlDbType.Int;
            p1m11.Value = Predlog1m11;
            cmd.Parameters.Add(p1m11);

            SqlParameter p1m12 = new SqlParameter();
            p1m12.ParameterName = "@Predlog1m12";
            p1m12.SqlDbType = SqlDbType.Int;
            p1m12.Value = Predlog1m12;
            cmd.Parameters.Add(p1m12);

            SqlParameter p2m1 = new SqlParameter();
            p2m1.ParameterName = "@Predlog2m1";
            p2m1.SqlDbType = SqlDbType.Int;
            p2m1.Value = Predlog2m1;
            cmd.Parameters.Add(p2m1);

            SqlParameter p2m2 = new SqlParameter();
            p2m2.ParameterName = "@Predlog2m2";
            p2m2.SqlDbType = SqlDbType.Int;
            p2m2.Value = Predlog2m2;
            cmd.Parameters.Add(p2m2);

            SqlParameter p2m3 = new SqlParameter();
            p2m3.ParameterName = "@Predlog2m3";
            p2m3.SqlDbType = SqlDbType.Int;
            p2m3.Value = Predlog2m3;
            cmd.Parameters.Add(p2m3);

            SqlParameter p2m4 = new SqlParameter();
            p2m4.ParameterName = "@Predlog2m4";
            p2m4.SqlDbType = SqlDbType.Int;
            p2m4.Value = Predlog2m4;
            cmd.Parameters.Add(p2m4);

            SqlParameter p2m5 = new SqlParameter();
            p2m5.ParameterName = "@Predlog2m5";
            p2m5.SqlDbType = SqlDbType.Int;
            p2m5.Value = Predlog2m5;
            cmd.Parameters.Add(p2m5);

            SqlParameter p2m6 = new SqlParameter();
            p2m6.ParameterName = "@Predlog2m6";
            p2m6.SqlDbType = SqlDbType.Int;
            p2m6.Value = Predlog2m6;
            cmd.Parameters.Add(p2m6);

            SqlParameter p2m7 = new SqlParameter();
            p2m7.ParameterName = "@Predlog2m7";
            p2m7.SqlDbType = SqlDbType.Int;
            p2m7.Value = Predlog2m7;
            cmd.Parameters.Add(p2m7);

            SqlParameter p2m8 = new SqlParameter();
            p2m8.ParameterName = "@Predlog2m8";
            p2m8.SqlDbType = SqlDbType.Int;
            p2m8.Value = Predlog2m8;
            cmd.Parameters.Add(p2m8);

            SqlParameter p2m9 = new SqlParameter();
            p2m9.ParameterName = "@Predlog2m9";
            p2m9.SqlDbType = SqlDbType.Int;
            p2m9.Value = Predlog2m9;
            cmd.Parameters.Add(p2m9);

            SqlParameter p2m10 = new SqlParameter();
            p2m10.ParameterName = "@Predlog2m10";
            p2m10.SqlDbType = SqlDbType.Int;
            p2m10.Value = Predlog2m10;
            cmd.Parameters.Add(p2m10);

            SqlParameter p2m11 = new SqlParameter();
            p2m11.ParameterName = "@Predlog2m11";
            p2m11.SqlDbType = SqlDbType.Int;
            p2m11.Value = Predlog2m11;
            cmd.Parameters.Add(p2m11);

            SqlParameter p2m12 = new SqlParameter();
            p2m12.ParameterName = "@Predlog2m12";
            p2m12.SqlDbType = SqlDbType.Int;
            p2m12.Value = Predlog2m12;
            cmd.Parameters.Add(p2m12);


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
        public void PoveziProseke(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PoveziProseke";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@IDPlana";
            id.SqlDbType = SqlDbType.Int;
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
        public void PoveziStanja(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PoveziStanja";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@IDPlana";
            id.SqlDbType = SqlDbType.Int;
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
        public void PoveziTrend(int ID)
        {
            //Komentar 

            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PoveziTrend";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@IDPlana";
            id.SqlDbType = SqlDbType.Int;
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
        public void UpdatePodesavanje(int ID,int Visible)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdatePodesavanjePlan";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter visible = new SqlParameter();
            visible.ParameterName = "@Visible";
            visible.SqlDbType = SqlDbType.Int;
            visible.Value = Visible;
            cmd.Parameters.Add(visible);

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
        public void DodeliGrupi(int ID, int MpSifra)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertPlanMpVezniPostojeci";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@IdGrupe";
            id.SqlDbType = SqlDbType.Int;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter mp = new SqlParameter();
            mp.ParameterName = "@MpSifra";
            mp.SqlDbType = SqlDbType.Int;
            mp.Value = MpSifra;
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
        public void KreirajNovuGrupu()
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PlanMpVezniGrupa";
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
        public void DodeliNovuGrupu(int MpSifra)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertPlanMpVezniNovi";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter mp = new SqlParameter();
            mp.ParameterName = "@MpSifra";
            mp.SqlDbType = SqlDbType.Int;
            mp.Value = MpSifra;
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
        public void ObrisiIzGrupe(int MpSifra,int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeletePlanMpVezni";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter mp = new SqlParameter();
            mp.ParameterName = "@MpSifra";
            mp.SqlDbType = SqlDbType.Int;
            mp.Value = MpSifra;
            cmd.Parameters.Add(mp);

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@IDGrupe";
            id.SqlDbType = SqlDbType.Int;
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
        public void PoveziUsvojenuOdluku(int IDPlana,int MpSifra,decimal Odluka)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PoveziUsvojenuOdluku";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@IDPlana";
            id.SqlDbType = SqlDbType.Int;
            id.Value = IDPlana;
            cmd.Parameters.Add(id);

            SqlParameter mp = new SqlParameter();
            mp.ParameterName = "@MpSifra";
            mp.SqlDbType = SqlDbType.Int;
            mp.Value = MpSifra;
            cmd.Parameters.Add(mp);

            SqlParameter odluka = new SqlParameter();
            odluka.ParameterName = "@Odluka";
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
        public void DodeliPlan(int IDPlana,int RN)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PoveziRN";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@IDPlana";
            id.SqlDbType = SqlDbType.Int;
            id.Value = IDPlana;
            cmd.Parameters.Add(id);

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

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

        public void IskljuciPlan(int RN)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "IskljuciPlan";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter rn = new SqlParameter();
            rn.ParameterName = "@RN";
            rn.SqlDbType = SqlDbType.Int;
            rn.Value = RN;
            cmd.Parameters.Add(rn);

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

        public void UpdateKoeficijent(int IDKolekcije,int IDPrograma,int MpSifra,decimal Koeficijent)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateKoeficijent";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idk = new SqlParameter();
            idk.ParameterName = "@IDKolekcije";
            idk.SqlDbType = SqlDbType.Int;
            idk.Value = IDKolekcije;
            cmd.Parameters.Add(idk);


            SqlParameter idp = new SqlParameter();
            idp.ParameterName = "@IDPrograma";
            idp.SqlDbType = SqlDbType.Int;
            idp.Value = IDPrograma;
            cmd.Parameters.Add(idp);


            SqlParameter mp = new SqlParameter();
            mp.ParameterName = "@MpSifra";
            mp.SqlDbType = SqlDbType.Int;
            mp.Value = MpSifra;
            cmd.Parameters.Add(mp);


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
    }
}
