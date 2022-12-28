using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Pakovanja.PregledStavkiPlanaPakovanja
{
    internal class PackingPlanItemOverviewDAO
    {
        //private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private string connectionString = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public int Update(PackingPlanItemOverviewModel obj)
        {
            int success = -1;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("PregledStavkiPlanaPakovanja_Update", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", obj.Id);
                command.Parameters.AddWithValue("@IDStavkePlanaPakovanja", obj.IdStavkePlanaPakovanja);
                command.Parameters.AddWithValue("@RN", obj.RN);
                command.Parameters.AddWithValue("@StatusStavke", obj.StatusStavke);
                command.Parameters.AddWithValue("@ArtikalPaket", obj.ArtikalPaket);
                command.Parameters.AddWithValue("@KolicinaZaProizvodnju", obj.KolicinaZaProizvodnju);
                command.Parameters.AddWithValue("@ArtikalIzPrvogNivoaSastavnice", obj.ArtikalIzPrvogNivoaSastavnice);
                command.Parameters.AddWithValue("@PotrebnaKolicina", obj.PotrebnaKolicina);
                command.Parameters.AddWithValue("@TrenutnaKolicinaNaZalihama", obj.TrenutnaKolicinaNaZalihama);
                command.Parameters.AddWithValue("@RezervisanaKolicinaPoPlanu", obj.RezervisanaKolicinaPoPlanu);
                command.Parameters.AddWithValue("@UkucatiKolicinuZaRezervaciju", obj.UkucatiKolicinuZaRezervaciju);

                try
                {
                    success = command.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    throw new Exception("ERROR!");
                }
            }
            return success;
        }

        public int Delete(int Id)
        {
            int success = -1;

            if (Id < 1)
            {
                return -1;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("PregledStavkiPlanaPakovanja_Delete", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", Id);

                try
                {
                    success = command.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    throw new Exception("ERROR!");
                }
            }
            return success;
        }

        // Search by Id and Artikal where id_supp=id
        public List<PackingPlanItemOverviewModel> Search(string term, int id)
        {
            List<PackingPlanItemOverviewModel> returnThese = new List<PackingPlanItemOverviewModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("PregledStavkiPlanaPakovanja_Search", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@term", term);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnThese.Add(new PackingPlanItemOverviewModel
                            {
                                Id = (int)reader[0],
                                IdStavkePlanaPakovanja = (int)reader[1],
                                RN = (int)reader[2],
                                StatusStavke = (string)reader[3],
                                ArtikalPaket = (int)reader[4],
                                KolicinaZaProizvodnju = (decimal)reader[5],
                                ArtikalIzPrvogNivoaSastavnice = (int)reader[6],
                                PotrebnaKolicina = (decimal)reader[7],
                                TrenutnaKolicinaNaZalihama = (decimal)reader[8],
                                RezervisanaKolicinaPoPlanu = (decimal)reader[9],
                                UkucatiKolicinuZaRezervaciju = (decimal)reader[10]
                            });
                        }
                    }
                }
                catch (SqlException)
                {
                    throw new Exception("ERROR!");
                }
            }
            return returnThese;
        }

        public List<PackingPlanItemOverviewModel> GetAllByIdSuperior(int id)
        {
            List<PackingPlanItemOverviewModel> returnThese = new List<PackingPlanItemOverviewModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("PregledStavkiPlanaPakovanja_GetAllByIdSuperior", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnThese.Add(new PackingPlanItemOverviewModel
                            {
                                Id = (int)reader[0],
                                IdStavkePlanaPakovanja = (int)reader[1],
                                RN = (int)reader[2],
                                StatusStavke = (string)reader[3],
                                ArtikalPaket = (int)reader[4],
                                KolicinaZaProizvodnju = (decimal)reader[5],
                                ArtikalIzPrvogNivoaSastavnice = (int)reader[6],
                                PotrebnaKolicina = (decimal)reader[7],
                                TrenutnaKolicinaNaZalihama = (decimal)reader[8],
                                RezervisanaKolicinaPoPlanu = (decimal)reader[9],
                                UkucatiKolicinuZaRezervaciju = (decimal)reader[10]
                            });
                        }
                    }
                }
                catch (SqlException)
                {
                    throw new Exception("ERROR!");
                }
            }
            return returnThese;
        }

        public int InsertFromTable(int id_sup, decimal kolicina_sup, int ArtikalPaket)
        {
            int success = -1;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("PregledStavkiPlanaPakovanja_InsertFromTable", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id_sup", id_sup);
                command.Parameters.AddWithValue("@kolicina_sup", kolicina_sup);
                command.Parameters.AddWithValue("@ArtikalPaket", ArtikalPaket);
                try
                {
                    success = command.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    throw new Exception("ERROR!");
                }
            }
            return success;
        }
    }
}
