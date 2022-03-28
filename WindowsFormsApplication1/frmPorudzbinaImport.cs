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
using System.Globalization;

namespace WindowsFormsApplication1
{
    public partial class frmPorudzbinaImport : Form
    {
        public bool PravoP;
        public static string code = "frmPorudzbinaImport";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public frmPorudzbinaImport()
        {
            InitializeComponent();
            dtpVremeOd.Value = DateTime.Now;
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
        private void button1_Click(object sender, EventArgs e)
        {
            string connString =
          ConfigurationManager.AppSettings["webs"];

            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM RkShippingExportPorudzbina ", conn);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                adapter.Fill(dataTableRes);
                dataGridView1.DataSource = dataTableRes;
               
                /*
                DataGridViewColumn column = dataGridView1.Columns[0];
                dataGridView1.Columns[0].HeaderText = "Isporuka ID";
                dataGridView1.Columns[0].Width = 50;

                DataGridViewColumn column2 = dataGridView1.Columns[1];
                dataGridView1.Columns[1].HeaderText = "Barkod isporuke";
                dataGridView1.Columns[1].Width = 150;

                DataGridViewColumn column3 = dataGridView1.Columns[2];
                dataGridView1.Columns[2].HeaderText = "Datum utovara";
                dataGridView1.Columns[2].Width = 100;

                DataGridViewColumn column4 = dataGridView1.Columns[3];
                dataGridView1.Columns[3].HeaderText = "Datum istovara";
                dataGridView1.Columns[3].Width = 100;

                DataGridViewColumn column5 = dataGridView1.Columns[4];
                dataGridView1.Columns[4].HeaderText = "Broj utovara";
                dataGridView1.Columns[4].Width = 70;

                DataGridViewColumn column6 = dataGridView1.Columns[5];
                dataGridView1.Columns[5].HeaderText = "Broj istovara";
                dataGridView1.Columns[5].Width = 70;

                DataGridViewColumn column7 = dataGridView1.Columns[6];
                dataGridView1.Columns[6].HeaderText = "Broj palete";
                dataGridView1.Columns[6].Width = 70;

                DataGridViewColumn column8 = dataGridView1.Columns[7];
                dataGridView1.Columns[7].HeaderText = "Kupac";
                dataGridView1.Columns[7].Width = 70;

                DataGridViewColumn column9 = dataGridView1.Columns[8];
                dataGridView1.Columns[8].HeaderText = "Salon";
                dataGridView1.Columns[8].Width = 70;

                DataGridViewColumn column10 = dataGridView1.Columns[9];
                dataGridView1.Columns[9].HeaderText = "Vozilo";
                dataGridView1.Columns[9].Width = 70;

                DataGridViewColumn column11 = dataGridView1.Columns[10];
                dataGridView1.Columns[10].HeaderText = "Priključno vozilo";
                dataGridView1.Columns[10].Width = 70;

                DataGridViewColumn column12 = dataGridView1.Columns[11];
                dataGridView1.Columns[11].HeaderText = "Broj paketa";
                dataGridView1.Columns[11].Width = 70;

                DataGridViewColumn column13 = dataGridView1.Columns[12];
                dataGridView1.Columns[12].HeaderText = "Štampa";
                dataGridView1.Columns[12].Width = 70;

                DataGridViewColumn column14 = dataGridView1.Columns[13];
                dataGridView1.Columns[13].HeaderText = "Export Largo";
                dataGridView1.Columns[13].Width = 70;

                DataGridViewColumn column15 = dataGridView1.Columns[14];
                dataGridView1.Columns[14].HeaderText = "Export Largo - Može";
                dataGridView1.Columns[14].Width = 70;
                */
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // Convert.ToInt32(row.Cells[15].Value.ToString()), row.Cells[1].Value.ToString())
            progressBar1.Maximum = dataGridView1.Rows.Count;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            
            InsertPorudzbina porudzbina = new InsertPorudzbina();

            porudzbina.InsertPor(dtpVremeOd.Text);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
               // porudzbina.InsertPorudzbinaPostav(Convert.ToInt32(LargoPaketID), Convert.ToDecimal(Kolicina));
                porudzbina.InsertPorudzbinaPostav(Convert.ToInt32(row.Cells[3].Value.ToString()), Convert.ToInt32(row.Cells[5].Value.ToString()));
                progressBar1.Value = progressBar1.Value + 1;
            }
            MessageBox.Show("Zavrsen uvoz porudybine");

            progressBar2.Maximum = dataGridView1.Rows.Count;
            progressBar2.Step = 1;
            progressBar2.Value = 0;
            string NazivBeleske = DateTime.Now.ToString() + '-' + "Porudzbina";
            MessageBox.Show(NazivBeleske);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // porudzbina.InsertPorudzbinaPostav(Convert.ToInt32(LargoPaketID), Convert.ToDecimal(Kolicina));
                porudzbina.InsertBeleskaPostav(NazivBeleske, Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[2].Value.ToString(), Convert.ToInt32(row.Cells[3].Value.ToString()), row.Cells[4].Value.ToString(), Convert.ToDouble(row.Cells[5].Value.ToString()));
                progressBar2.Value = progressBar2.Value + 1;
            }
            MessageBox.Show("Zavrsen uvoz beleske");
        }

        private void frmPorudzbinaImport_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtSifra.Text == "p3777p")
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button4.Enabled = true;
                dataGridView1.Enabled = true;
                dtpVremeOd.Enabled = true;
            }
            else
            {
                MessageBox.Show("Nemate pravo da uvozite neisporučene");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = dataGridView1.Rows.Count;
            progressBar1.Step = 1;
            progressBar1.Value = 0;

            InsertPorudzbina porudzbina = new InsertPorudzbina();

           
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // porudzbina.InsertPorudzbinaPostav(Convert.ToInt32(LargoPaketID), Convert.ToDecimal(Kolicina));
                porudzbina.InsertBeleskaPostav(txtBeleska.Text,  Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[2].Value.ToString(), Convert.ToInt32(row.Cells[3].Value.ToString()), row.Cells[4].Value.ToString(), Convert.ToDouble(row.Cells[5].Value.ToString()));
                progressBar1.Value = progressBar1.Value + 1;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmPorudzbinaImportPregled porimp = new frmPorudzbinaImportPregled();
            PravoP = porimp.Pravo;
            if (PravoP == true) { porimp.Show(); } else { return; }
        }
    }
}
