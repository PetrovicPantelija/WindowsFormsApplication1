using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;

namespace WindowsFormsApplication1
{
    public partial class Reklamacije : Form
    {
        public static string code = "Reklamacije";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";

        public Reklamacije()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            IdGrupe();
            IdForme();
            PravoPristupa();
        }
        public string IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            //Sifarnici.frmLogovanje frm = new Sifarnici.frmLogovanje();         
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
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
            conn.Close();
            return niz;
        }
        private int IdForme()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select IdForme from Forme where Rtrim(Code)=" + "'" + code + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idForme = Convert.ToInt32(dr["IdForme"].ToString());
            }
            conn.Close();
            return idForme;
        }

        private void PravoPristupa()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select * From GrupaForma Where IdGrupe in (" + niz + ") and IdForme=" + idForme;
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
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
                        //tsNew.Enabled = false;
                    }
                    update = Convert.ToBoolean(reader["Izmena"]);
                    if (update == false)
                    {
                        //tsSave.Enabled = false;
                    }
                    delete = Convert.ToBoolean(reader["Brisanje"]);
                    if (delete == false)
                    {
                        //tsDelete.Enabled = false;
                    }
                }
            }

            conn.Close();
        }
        private void Reklamacije_Load(object sender, EventArgs e)
        {
            string connString =
          ConfigurationManager.AppSettings["webs"];
            
            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM NalogZaUtovarReklamacije", conn);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                adapter.Fill(dataTableRes);
                dataGridView1.DataSource = dataTableRes;
                conn.Close();
            }
           
        }

        private void btnPoDatumuIVozilu_Click(object sender, EventArgs e)
        {

            string connString =
            ConfigurationManager.AppSettings["webs"];
            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM NalogZaUtovarReklamacije where Vozilo = " + "'" + txtVozilo.Text + "' and DatumUtovara =" + "DateValue('" + txtDatumIsporuke.Text + "')", conn);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                adapter.Fill(dataTableRes);
            }

            dataGridView1.DataSource = dataTableRes;

        }

        private void btnInsertNakNalog_Click(object sender, EventArgs e)
        {
            string constring = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            int BrojNakNalog = 0;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("SelectNakNAlog", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MaxBroj", SqlDbType.Int);
                    cmd.Parameters["@MaxBroj"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    BrojNakNalog = Convert.ToInt32(cmd.Parameters["@MaxBroj"].Value.ToString());
                }
            }

            // Unosstavki
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // currQty += row.Cells["qty"].Value;
                //More code here
                if (row != null && row.Cells[0].Value != null)
                {

                    InsertNakNalogR innp = new InsertNakNalogR();
                    //string kod = row.Cells[7].Value.ToString() + "-" + row.Cells[15].Value.ToString() + "-" + row.Cells[17].Value.ToString();
                    innp.InsertNakNalogRV(BrojNakNalog, row.Cells[4].Value.ToString(), "KOM", Convert.ToDouble(row.Cells[2].Value.ToString()),row.Cells[5].Value.ToString(), row.Cells[10].Value.ToString(), Convert.ToInt32(row.Cells[16].Value.ToString()), row.Cells[17].Value.ToString(), Convert.ToInt32(row.Cells[18].Value.ToString()), row.Cells[19].Value.ToString(), row.Cells[11].Value.ToString(), row.Cells[7].Value.ToString());

                }
            }
   
        }
    }
}
