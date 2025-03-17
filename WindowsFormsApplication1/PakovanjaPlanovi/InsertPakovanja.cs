using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;
using WindowsFormsApplication1.Magacin;
using static WindowsFormsApplication1.PakovanjaPlanovi.StavkePlanaPakovanjaNovi;

namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    internal class InsertPakovanja
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public void InsertPlanPakovanja(string Naziv,DateTime DatumOd,DateTime DatumDo,string Napomena,string Tip)
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
                            cmd.CommandText = "PlanoviPakovanja_Insert";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@Naziv", SqlDbType.NVarChar,200) { Value = Naziv });
                            cmd.Parameters.Add(new SqlParameter("@DatumOd", SqlDbType.DateTime) { Value = DatumOd });
                            cmd.Parameters.Add(new SqlParameter("@DatumDo",SqlDbType.DateTime) { Value = DatumDo });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 350) { Value = Napomena });
                            cmd.Parameters.Add(new SqlParameter("@Tip", SqlDbType.Char, 2) { Value= Tip });

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
        public void UpdatePlanPakovanja(int ID,string Naziv, DateTime DatumOd, DateTime DatumDo, string Napomena,int Konacan)
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
                            cmd.CommandText = "PlanoviPakovanja_Update";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value= ID });
                            cmd.Parameters.Add(new SqlParameter("@Naziv", SqlDbType.NVarChar, 200) { Value = Naziv });
                            cmd.Parameters.Add(new SqlParameter("@DatumOd", SqlDbType.DateTime) { Value = DatumOd });
                            cmd.Parameters.Add(new SqlParameter("@DatumDo", SqlDbType.DateTime) { Value = DatumDo });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 350) { Value = Napomena });
                            cmd.Parameters.Add(new SqlParameter("@Konacan",SqlDbType.Int) { Value = Konacan });

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
        public void DeletePlanPakovanja(int ID)
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
                            cmd.CommandText = "PlanoviPakovanja_Delete";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });


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
        public void UpisiRaspored(int ID,int Pozicija)
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
                            cmd.CommandText = "StavkePlanaPakovanja_Raspored";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@Pozicija",SqlDbType.Int) { Value = Pozicija});


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
        public void ProgalsiPlanPakovanja(int ID)
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
                            cmd.CommandText = "PlanoviPakovanja_Konacan";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });


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
        public void DodeliPlanuPakovanja(int IDPakovanja,int RNStavka,int Termin,string Korisnik)
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
                            cmd.CommandText = "StavkePlanaPakovanja_InsertFromTable";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@id_sup", SqlDbType.Int) { Value = IDPakovanja });
                            cmd.Parameters.Add(new SqlParameter("@sif_zapisa", SqlDbType.Int) { Value=RNStavka });
                            cmd.Parameters.Add(new SqlParameter("@termin", SqlDbType.Int) { Value=Termin });
                            cmd.Parameters.Add(new SqlParameter("@korisnik", SqlDbType.NVarChar, 50) { Value = Korisnik });


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
        public void UpdateKolicinePlanaPakovanja(int ID, decimal Kolicina, int Artikal, int RN, decimal RnKolicina)
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
                            cmd.CommandText = "UpdatePregledStavkiPlanaPakovanja_Kolicine";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@id_sup", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@kolicina_sup", SqlDbType.Decimal) { Value = Kolicina });
                            cmd.Parameters.Add(new SqlParameter("@ArtikalPaket", SqlDbType.Int) { Value = Artikal });
                            cmd.Parameters.Add(new SqlParameter("@RadniNalog",SqlDbType.Int) { Value = RN });
                            cmd.Parameters.Add(new SqlParameter("@RnKolicina", SqlDbType.Decimal) { Value=RnKolicina });

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
        public void UpdateStavkuPlanaPakovanja(int ID,int IDPlana,int RNBroj,string Status,int Vrsta,int Artikal,string Kod,string Naziv,decimal Kolicina,DateTime Datum,int Termin)
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
                            cmd.CommandText = "StavkePlanaPakovanja_Update";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@Id",SqlDbType.Int) { Value=ID });
                            cmd.Parameters.Add(new SqlParameter("@IDPlanPakovanja", SqlDbType.Int) { Value = IDPlana });
                            cmd.Parameters.Add(new SqlParameter("@RNBroj", SqlDbType.Int) { Value=RNBroj });
                            cmd.Parameters.Add(new SqlParameter("@StatusRN",SqlDbType.NVarChar,2) { Value=Status });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRN", SqlDbType.Int) { Value=Vrsta });
                            cmd.Parameters.Add(new SqlParameter("@Artikal", SqlDbType.Int) { Value = Artikal });
                            cmd.Parameters.Add(new SqlParameter("@Kod", SqlDbType.NVarChar, 24) { Value = Kod });
                            cmd.Parameters.Add(new SqlParameter("@Naziv", SqlDbType.NVarChar, 60) { Value=Naziv });
                            cmd.Parameters.Add(new SqlParameter("@KolicinaZaProizvodnju", SqlDbType.Decimal) { Value = Kolicina });
                            cmd.Parameters.Add(new SqlParameter("@PlaniraniKrajnjiDatumZavrsetka", SqlDbType.DateTime) { Value = Datum });
                            cmd.Parameters.Add(new SqlParameter("@Termin",SqlDbType.Int) { Value = Termin });


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
        public void DeleteStavkuPlanaPakovanja(int ID)
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
                            cmd.CommandText = "StavkePlanaPakovanja_Delete";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = ID });

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
        public void InsertPregledStavkiPakovanja(int ID,decimal Kolicina,int Artikal)
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
                            cmd.CommandText = "InsertPregledStavkiPlanaPakovanja";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@id_sup", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@kolicina_sup", SqlDbType.Decimal) { Value = Kolicina });
                            cmd.Parameters.Add(new SqlParameter("@ArtikalPaket",SqlDbType.Int) { Value = Artikal });

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
        public void UpdatePregledStavkiPakovanja(int ID,int Mp,decimal Stanje,decimal KolZaRezervaciju)
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
                            cmd.CommandText = "UpdatePregledStavkiPakovanja";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@MP", SqlDbType.Int) { Value=Mp });
                            cmd.Parameters.Add(new SqlParameter("@Stanje", SqlDbType.Decimal) { Value = Stanje });
                            cmd.Parameters.Add(new SqlParameter("@KolZaRezervaciju", SqlDbType.Decimal) { Value=KolZaRezervaciju });

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
        public void DeletePregledStavkiItem(int ID)
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
                            cmd.CommandText = "PregledStavkiPlanaPakovanja_Delete";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = ID });

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
        public void UpdateStanjePregledStavkiPakovanja(int ID, int IDStavke, int ArtikalPrviNivo)
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
                            cmd.CommandText = "UpdateStanjaPregledStavkiPlanaPakovanja";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@IDStavke", SqlDbType.Int) { Value = IDStavke });
                            cmd.Parameters.Add(new SqlParameter("@ArtikalPrviNivo", SqlDbType.Decimal) { Value = ArtikalPrviNivo });

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
        public void RezervacijaPoRasporedu(int ID, int IDStavke, int ArtikalPrviNivo)
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
                            cmd.CommandText = "RezervisiStanjaPoRasporedu";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@IDStavke", SqlDbType.Int) { Value = IDStavke });
                            cmd.Parameters.Add(new SqlParameter("@ArtikalPrviNivo", SqlDbType.Decimal) { Value = ArtikalPrviNivo });

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
        public void RezervacijaPreostalo(int ID,int ArtikalPrviNivo)
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
                            cmd.CommandText = "RezervisiPreostaloStanje";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@ArtikalPrviNivo", SqlDbType.Decimal) { Value = ArtikalPrviNivo });

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Rezervacija preostalo error", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show(ex.ToString());
                    }
                }
                conn.Close();
            }
        }
        public void InsertStavkePakovanjaRec(int IDPlana,int ShippingItemRecId,int ShippingID,string Element,string MestoIsporuke,string SifraReklamacije,string NazivReklamacije,decimal Kolicina)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using(SqlTransaction transaction= conn.BeginTransaction())
                {
                    try
                    {
                        using(SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "InsertStavkePlanaPakovanjaRec";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@IDPlanaPakovanja", SqlDbType.Int) { Value = IDPlana });
                            cmd.Parameters.Add(new SqlParameter("@ShippingItemRecID", SqlDbType.Int) { Value = ShippingItemRecId });
                            cmd.Parameters.Add(new SqlParameter("@ShippingID", SqlDbType.Int) { Value = ShippingID });
                            cmd.Parameters.Add(new SqlParameter("@Element", SqlDbType.NVarChar, 200) { Value = Element });
                            cmd.Parameters.Add(new SqlParameter("@MestoIsporuke", SqlDbType.NVarChar) { Value = MestoIsporuke });
                            cmd.Parameters.Add(new SqlParameter("@SifraReklamacije", SqlDbType.NVarChar, 500) { Value = SifraReklamacije });
                            cmd.Parameters.Add(new SqlParameter("@NazivReklamacije", SqlDbType.NVarChar, 500) { Value = NazivReklamacije });
                            cmd.Parameters.Add(new SqlParameter("@Kolicina", SqlDbType.Decimal) { Value = Kolicina });

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch(SqlException ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }
        public void InsertStavkePakovanjaNamensko(int IDPlana,int RNBroj,string StatusRN,int VrstaRN, int MP, string Sifra, string Naziv, decimal Kolicina, DateTime Datum, string Planer)
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
                            cmd.CommandText = "InsertStavkePlanaPakovanjaNamensko";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = IDPlana });
                            cmd.Parameters.Add(new SqlParameter("@RNBroj", SqlDbType.Int) { Value = RNBroj });
                            cmd.Parameters.Add(new SqlParameter("@StatusRN", SqlDbType.NVarChar, 10) { Value = StatusRN });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRN", SqlDbType.Int) { Value= VrstaRN });
                            cmd.Parameters.Add(new SqlParameter("@Artikal", SqlDbType.Int) { Value = MP });
                            cmd.Parameters.Add(new SqlParameter("@Sifra", SqlDbType.NVarChar, 500) { Value= Sifra });
                            cmd.Parameters.Add(new SqlParameter("@Naziv",SqlDbType.NVarChar, 500) { Value= Naziv });
                            cmd.Parameters.Add(new SqlParameter("@Kolicina",SqlDbType.Decimal) { Value = Kolicina });
                            cmd.Parameters.Add(new SqlParameter("@Datum", SqlDbType.DateTime) { Value = Datum });
                            cmd.Parameters.Add(new SqlParameter("@Planer", SqlDbType.NVarChar, 50) { Value= Planer });

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }
        public void UpdateNamenskoKolicina(int ID,decimal Kolicina)
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
                            cmd.CommandText = "NamenskoUpdateKolicinu";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@Kolicina", SqlDbType.Decimal) { Value = Kolicina });

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
        public void DeleteStavkePakovanja(int ID)
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
                            cmd.CommandText = "DeleteStavkePlanaPakovanja";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });

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
    }
}

