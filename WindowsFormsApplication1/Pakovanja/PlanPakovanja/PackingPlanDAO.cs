using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApplication1.Pakovanja.PlanPakovanja
{
    public class PackingPlanDAO
    {
        //private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private string connectionString = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public int Insert(PackingPlanModel obj)
        {
            int success = -1;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("PlanoviPakovanja_Insert", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Naziv", obj.Naziv);
                command.Parameters.AddWithValue("@Napomena", obj.Napomena);
                command.Parameters.AddWithValue("@DatumOd", obj.DatumOd);
                command.Parameters.AddWithValue("@DatumDo", obj.DatumDo);
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

        public int Update(PackingPlanModel obj)
        {
            int success = -1;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("PlanoviPakovanja_Update", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", obj.Id);
                command.Parameters.AddWithValue("@Naziv", obj.Naziv);
                command.Parameters.AddWithValue("@Napomena", obj.Napomena);
                command.Parameters.AddWithValue("@DatumOd", obj.DatumOd);
                command.Parameters.AddWithValue("@DatumDo", obj.DatumDo);
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

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("PlanoviPakovanja_Delete", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", Id);

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

        public List<PackingPlanModel> GetAll()
        {
            List<PackingPlanModel> returnThese = new List<PackingPlanModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("PlanoviPakovanja_GetAll", con);
                command.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.Add(new SqlParameter("@CustomerID", custId));
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnThese.Add(new PackingPlanModel { Id = (int)reader[0], Naziv = (string)reader[1], DatumOd = (DateTime)reader[2], DatumDo = (DateTime)reader[3], Napomena = (string)reader[4] });
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

        public List<PackingPlanModel> Search(string term)
        {
            List<PackingPlanModel> returnThese = new List<PackingPlanModel>();
            string termUPD = "%" + term + "%";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("PlanoviPakovanja_Search", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@term", termUPD);
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnThese.Add(new PackingPlanModel { Id = (int)reader[0], Naziv = (string)reader[1], DatumOd = (DateTime)reader[2], DatumDo = (DateTime)reader[3], Napomena = (string)reader[4] });
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

    }
}
