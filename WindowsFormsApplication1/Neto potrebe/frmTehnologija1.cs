using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Neto_potrebe
{
    public partial class frmTehnologija1 : Form
    {
        bool status = false;
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public bool PravoP;

        public static string code = "frmTehnologija1";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string niz = "";
        string Kor = frmLogovanje.user.ToString().TrimEnd();

        public frmTehnologija1()
        {
            InitializeComponent();
            IdGrupe(); IdForme(); PravoPristupa();
        }

        private void frmTehnologija1_Load(object sender, EventArgs e)
        {
            FillGV();
        }

        private void FillGV()
        {
            var query = "Select * from MdfTehnologija1 order by Tehnologija1 asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    txtNaziv.Text = row.Cells["Tehnologija1"].Value.ToString().TrimEnd();
                }
            }
        }

        int VratiPostoji()
        {
            int Najveci = 0;
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select count(*) as Broj from mdfTehnologija1 where Upper(Tehnologija1) = UPPER('" + txtNaziv.Text + "')", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Najveci = Convert.ToInt32(dr["Broj"].ToString());

                return Najveci;
            }
            return 0;
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            if (VratiPostoji() > 0) 
            {
                MessageBox.Show("Postoji materijal sa tim nazivom!");
                return;
            }


            if (!string.IsNullOrWhiteSpace(txtNaziv.Text))
            {
                InsertTehnologija1(txtNaziv.Text.ToString().TrimEnd());
                FillGV();
            }
            else
            {
                MessageBox.Show("Nema naziva Materijala, morate uneti!");
                return;
            }
        }

        private void InsertTehnologija1(string Naziv)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertMdfTehnologija1";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter naziv = new SqlParameter();
            naziv.ParameterName = "@Naziv";
            naziv.SqlDbType = SqlDbType.NVarChar;
            naziv.Size = 100;
            naziv.Direction = ParameterDirection.Input;
            naziv.Value = Naziv;
            cmd.Parameters.Add(naziv);

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
                throw new Exception("Neuspešan upis tehnologija 1");
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
   
        private void DeleteTehnologija1(string Naziv)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteMdfTehnologija1";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter naziv = new SqlParameter();
            naziv.ParameterName = "@Naziv";
            naziv.SqlDbType = SqlDbType.NVarChar;
            naziv.Size = 100;
            naziv.Direction = ParameterDirection.Input;
            naziv.Value = Naziv;
            cmd.Parameters.Add(naziv);

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
                throw new Exception("Neuspešno brisanje Tehnologije");
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

        private void btnObrisi_Click(object sender, EventArgs e)
        {

                DeleteTehnologija1(txtNaziv.Text);
                FillGV();
           
        }

        public string IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            using (SqlConnection conn = new SqlConnection(s_connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    int count = 0;
                    while (dr.Read())
                    {
                        if (count == 0)
                        {
                            niz = dr["IdGrupe"].ToString();
                            count++;
                        }
                        else
                        {
                            niz = niz + "," + dr["IdGrupe"].ToString();
                            count++;
                        }
                    }
                }
            }
            return niz;
        }

        private int IdForme()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select IdForme from Forme where Rtrim(Code)=" + "'" + code + "'";
            using (SqlConnection conn = new SqlConnection(s_connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        idForme = Convert.ToInt32(dr["IdForme"].ToString());
                    }
                }
            }
            return idForme;
        }

        private void PravoPristupa()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select * From GrupaForma Where IdGrupe in (" + niz + ") and IdForme=" + idForme;
            using (SqlConnection conn = new SqlConnection(s_connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows == false)
                    {
                        MessageBox.Show("Nemate prava za pristup ovoj formi", code);
                        Pravo = false;
                    }
                    else
                    {
                        Pravo = true;
                        while (reader.Read())
                        {
                            insert = Convert.ToBoolean(reader["Upis"]);
                            if (insert == false)
                            {
                                // disable new control if present e.g. tsNew.Enabled = false;
                            }
                            update = Convert.ToBoolean(reader["Izmena"]);
                            if (update == false)
                            {
                                // disable save control if present e.g. tsSave.Enabled = false;
                            }
                            delete = Convert.ToBoolean(reader["Brisanje"]);
                            if (delete == false)
                            {
                                // disable delete control if present e.g. tsDelete.Enabled = false;
                            }
                        }
                    }
                }
            }
        }
    }
}