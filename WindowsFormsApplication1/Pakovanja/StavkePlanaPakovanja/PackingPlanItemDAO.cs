using WindowsFormsApplication1.Pakovanja.PackingPlanItem;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Pakovanja.PackingPlanItem
{
    internal class PackingPlanItemDAO
    {
        //private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private string connectionString = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public int Update(PackingPlanItemModel obj)
        {
            int success = -1;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("StavkePlanaPakovanja_Update", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", obj.Id);
                command.Parameters.AddWithValue("@IDPlanPakovanja", obj.IdPlanaPakovanja);
                command.Parameters.AddWithValue("@RNBroj", obj.RnBroj);
                command.Parameters.AddWithValue("@StatusRN", obj.RnStatus);
                command.Parameters.AddWithValue("@VrstaRN", obj.RnVrsta);
                command.Parameters.AddWithValue("@Artikal", obj.Artikal);
                command.Parameters.AddWithValue("@Kod", obj.Kod);
                command.Parameters.AddWithValue("@Naziv", obj.Naziv);
                command.Parameters.AddWithValue("@KolicinaZaProizvodnju", obj.KolicinaZaProizvodnju);
                command.Parameters.AddWithValue("@PlaniraniKrajnjiDatumZavrsetka", obj.KrajnjiDatum);
                command.Parameters.AddWithValue("@Termin", obj.Termin);

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
                SqlCommand command = new SqlCommand("StavkePlanaPakovanja_Delete", con);
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
        public List<PackingPlanItemModel> Search(string term, int id)
        {
            List<PackingPlanItemModel> returnThese = new List<PackingPlanItemModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("StavkePlanaPakovanja_Search", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@term", term);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnThese.Add(new PackingPlanItemModel
                            {
                                Id = (int)reader[0],
                                IdPlanaPakovanja = (int)reader[1],
                                RnBroj = (int)reader[2],
                                RnStatus = (string)reader[3],
                                RnVrsta = (int)reader[4],
                                Artikal = (int)reader[5],
                                Kod = (string)reader[6],
                                Naziv = (string)reader[7],
                                KolicinaZaProizvodnju = (decimal)reader[8],
                                KrajnjiDatum = (DateTime)reader[9],
                                Termin = (int)reader[10]
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

        public List<PackingPlanItemModel> SearchByIdSuperior(int id)
        {
            List<PackingPlanItemModel> returnThese = new List<PackingPlanItemModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("StavkePlanaPakovanja_GetAllByIdSuperior", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnThese.Add(new PackingPlanItemModel
                            {
                                Id = (int)reader[0],
                                IdPlanaPakovanja = (int)reader[1],
                                RnBroj = (int)reader[2],
                                RnStatus = (string)reader[3],
                                RnVrsta = (int)reader[4],
                                Artikal = (int)reader[5],
                                Kod = (string)reader[6],
                                Naziv = (string)reader[7],
                                KolicinaZaProizvodnju = (decimal)reader[8],
                                KrajnjiDatum = (DateTime)reader[9],
                                Termin = (int)reader[10]
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

        public List<string> GetAllVrsteDN()
        {
            List<string> returnThese = new List<string>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("VrsteDN_GetAll", con);
                command.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.Add(new SqlParameter("@CustomerID", custId));
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnThese.Add((string)reader[0] + " - " + (string)reader[1]);
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
        public List<JObject> GetAllByPlanProizvodnje(int id,string vrsta)
        {
            List<JObject> returnThese = new List<JObject>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("PlanProizvodnje_GetAllByID", con);
                command.CommandType= CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@vrsta", vrsta);
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            JObject newDNP = new JObject();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                newDNP.Add(reader.GetName(i).ToString(), reader.GetValue(i).ToString());
                            }
                            returnThese.Add(newDNP);
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
        
        public List<JObject> GetAllByDateDNP(string typeDN, DateTime fromDate, DateTime toDate)
        {
            List<JObject> returnThese = new List<JObject>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("DelNalogProizvodni_GetAllByDate", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@vrsta", typeDN);
                command.Parameters.AddWithValue("@datumOd", fromDate);
                command.Parameters.AddWithValue("@datumDo", toDate);

                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            JObject newDNP = new JObject();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                newDNP.Add(reader.GetName(i).ToString(), reader.GetValue(i).ToString());
                            }
                            returnThese.Add(newDNP);
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

        public int InsertFromTable(int id_sup, int sif_zapisa, int termin)
        {
            int success = -1;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("StavkePlanaPakovanja_InsertFromTable", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id_sup", id_sup);
                command.Parameters.AddWithValue("@sif_zapisa", sif_zapisa);
                command.Parameters.AddWithValue("@termin", termin);
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
