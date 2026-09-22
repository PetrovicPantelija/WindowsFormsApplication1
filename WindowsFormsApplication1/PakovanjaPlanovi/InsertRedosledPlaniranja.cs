using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    internal class InsertRedosledPlaniranja
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public void InsertRedosledPlan(string Redosled, int MpSifra, string MpCode, decimal MpKol, int MpNadredjeni)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "InsertRedosledPlaniranja";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@Redosled", SqlDbType.NVarChar, 30) { Value = Redosled });
                            cmd.Parameters.Add(new SqlParameter("@MpSifra", SqlDbType.Int) { Value = MpSifra });
                            cmd.Parameters.Add(new SqlParameter("@MpCode", SqlDbType.NVarChar, 60) { Value = MpCode });
                            cmd.Parameters.Add(new SqlParameter("@MpKol", SqlDbType.Decimal) { Value = MpKol });
                            cmd.Parameters.Add(new SqlParameter("@MpNadredjeni", SqlDbType.Int) { Value = MpNadredjeni });

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspešan upis u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show(ex.ToString());
                    }
                }
                conn.Close();
            }
        }

        public void UpdateRedosledPlaniranja(int MpSifra, string Redosled, string MpCode, decimal MpKol, int MpNadredjeni)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "UpdateRedosledPlaniranja";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@MpSifra", SqlDbType.Int) { Value = MpSifra });
                            cmd.Parameters.Add(new SqlParameter("@Redosled", SqlDbType.NVarChar, 30) { Value = Redosled });
                            cmd.Parameters.Add(new SqlParameter("@MpCode", SqlDbType.NVarChar, 60) { Value = MpCode });
                            cmd.Parameters.Add(new SqlParameter("@MpKol", SqlDbType.Decimal) { Value = MpKol });
                            cmd.Parameters.Add(new SqlParameter("@MpNadredjeni", SqlDbType.Int) { Value = MpNadredjeni });

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspešan upis u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show(ex.ToString());
                    }
                }
                conn.Close();
            }
        }

        public void DeleteRedosledPlaniranja(int MpSifra, int MpNadredjeni)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "DeleteRedosledPlaniranja";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@MpSifra", SqlDbType.Int) { Value = MpSifra });
                            cmd.Parameters.Add(new SqlParameter("@MpNadredjeni", SqlDbType.Int) { Value = MpNadredjeni });

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspešno brisanje iz baze", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show(ex.ToString());
                    }
                }
                conn.Close();
            }
        }
    }
}