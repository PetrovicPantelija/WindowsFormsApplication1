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
    public partial class WebIsporukaVoziloPocetna : Form
    {
        public bool PravoP;
        Boolean prviulaz = true;
        int barkodrb = 1;
        public static string code = "WebIsporukaVoziloPocetna";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public WebIsporukaVoziloPocetna()
        {
            InitializeComponent();
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
            if (chkUcitan.Checked == true)
                return;
            string connString =
          ConfigurationManager.AppSettings["webs"];

            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                //Panta E fakture
               // OleDbCommand cmd = new OleDbCommand("SELECT * FROM RkShipping where ExportLargo = false and RkShipping.VoziloIDz  = (Select Min(VoziloID) from TrVozila where TrVozila.RegistarskiBroj = '" + cboVozila.Text + "') and DatumIstovara =  CDate('" + cboDatumUtovara.Text + "')", conn);

                OleDbCommand cmd = new OleDbCommand("SELECT * FROM RkShipping where ExportLargo = false and RkShipping.VoziloIDz  = (Select Min(VoziloID) from TrVozila where TrVozila.RegistarskiBroj = '" + cboVozila.Text + "') and DatumIstovara=  CDate('" + cboDatumUtovara.Text + "')", conn);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                adapter.Fill(dataTableRes);
                dataGridView1.DataSource = dataTableRes;
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
                
                conn.Close();
            }
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                //PantaE

                //  OleDbCommand cmd2 = new OleDbCommand("SELECT * FROM RkShippingItemPro inner join RkShipping on  RkShipping.ShippingID = RkShippingItemPro.ShipingIdz where ExportLargo = false and RkShipping.VoziloIDz  = (Select Min(VoziloID) from TrVozila where TrVozila.RegistarskiBroj = '" + cboVozila.Text + "') and DatumIstovara =  CDate('" + cboDatumUtovara.Text + "')", conn);
                OleDbCommand cmd2 = new OleDbCommand("SELECT * FROM RkShippingItemPro inner join RkShipping on  RkShipping.ShippingID = RkShippingItemPro.ShipingIdz where ExportLargo = false and RkShipping.VoziloIDz  = (Select Min(VoziloID) from TrVozila where TrVozila.RegistarskiBroj = '" + cboVozila.Text + "') and DatumIstovara =  CDate('" + cboDatumUtovara.Text + "')", conn);

                conn.Open();
                DataTable dataTableResProizvodi = new DataTable();
                OleDbDataAdapter adapter2 = new OleDbDataAdapter(cmd2);

                adapter2.Fill(dataTableResProizvodi);
                dataGridView2.DataSource = dataTableResProizvodi;
                conn.Close();
            }
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                DataTable dataTableResPaketi = new DataTable();
               
                //PantaE
              //  OleDbCommand cmd3 = new OleDbCommand("SELECT * FROM RkShippingItemPak inner join RkShipping on  RkShipping.ShippingID = RkShippingItemPak.ShipingIdz where ExportLargo = false and RkShipping.VoziloIDz  = (Select Min(VoziloID) from TrVozila where TrVozila.RegistarskiBroj = '" + cboVozila.Text + "') and DatumIstovara =  CDate('" + cboDatumUtovara.Text + "') and StornoIsporuke <> true", conn);
                OleDbCommand cmd3 = new OleDbCommand("SELECT * FROM RkShippingItemPak inner join RkShipping on  RkShipping.ShippingID = RkShippingItemPak.ShipingIdz where ExportLargo = false and RkShipping.VoziloIDz  = (Select Min(VoziloID) from TrVozila where TrVozila.RegistarskiBroj = '" + cboVozila.Text + "') and DatumIstovara =  CDate('" + cboDatumUtovara.Text + "') and StornoIsporuke <> true", conn);

                conn.Open();

                OleDbDataAdapter adapter3 = new OleDbDataAdapter(cmd3);

                adapter3.Fill(dataTableResPaketi);
                dataGridView3.DataSource = dataTableResPaketi;
                chkUcitan.Checked = true;
                conn.Close();
            }

        

        }

        private void WebIsporukaVoziloPocetna_Load(object sender, EventArgs e)
        {
            string connString =
              ConfigurationManager.AppSettings["webs"];

            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
               // OleDbCommand cmd = new OleDbCommand("SELECT Distinct(Format(DatumIstovara, " + "dd.MM.yyyy" + ")) as DatumIstovara FROM RkShipping where ExportLargo = false", conn);
               
                //PantaE
              // OleDbCommand cmd = new OleDbCommand("SELECT Distinct(DatumIstovara) as DatumIstovara FROM RkShipping where ExportLargo = false", conn);
                OleDbCommand cmd = new OleDbCommand("SELECT Distinct(DatumIstovara) as DatumUtovara FROM RkShipping where ExportLargo = false", conn);


                //FORMAT(@d, 'dd/MM/yyyy', 'en-US' )
                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                adapter.Fill(dataTableRes);
                cboDatumUtovara.DataSource = dataTableRes;
                cboDatumUtovara.DisplayMember = "DatumUtovara";
                cboDatumUtovara.ValueMember = "DatumUtovara";

                conn.Close();
            }

            prviulaz = false;
            
        }

        private void cboVozila_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // if (prviulaz == false)
           // {
                string connString =
          ConfigurationManager.AppSettings["webs"];

                DataTable dataTableRes = new DataTable();

                using (OleDbConnection conn = new OleDbConnection(connString))
                {
                   //PantaE
                  //  OleDbCommand cmd = new OleDbCommand("SELECT Distinct(TrVozila.RegistarskiBroj) as RegistarskiBroj FROM RkShipping INNER JOIN TrVozila ON RkShipping.VoziloIDz = TrVozila.VoziloID where ExportLargo = false and DatumIstovara = CDate('" + cboDatumUtovara.Text + "')", conn);
                OleDbCommand cmd = new OleDbCommand("SELECT Distinct(TrVozila.RegistarskiBroj) as RegistarskiBroj FROM RkShipping INNER JOIN TrVozila ON RkShipping.VoziloIDz = TrVozila.VoziloID where ExportLargo = false and DatumIstovara = CDate('" + cboDatumUtovara.Text + "')", conn);

                conn.Open();

                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                    adapter.Fill(dataTableRes);
                    cboVozila.DataSource = dataTableRes;
                    cboVozila.DisplayMember = "RegistarskiBroj";
                    cboVozila.ValueMember = "RegistarskiBroj";

                    conn.Close();
                }

               // prviulaz = false;

            
        }

        private void btnInsertRefTabela_Click(object sender, EventArgs e)
        {
           // pantaE
            if (chkPrenesen.Checked == true)
                return;
            progressBar1.Maximum = dataGridView3.Rows.Count;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row != null && row.Cells[0].Value != null)
                {
                   InsertIsporuka isporuka = new InsertIsporuka();
                  // isporuka.InsertIsporPocetna(Convert.ToInt32(row.Cells[0].Value.ToString()), row.Cells[1].Value.ToString(), row.Cells[3].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[5].Value.ToString(), row.Cells[4].Value.ToString(), row.Cells[6].Value.ToString(), Convert.ToInt32(row.Cells[7].Value.ToString()), Convert.ToInt32(row.Cells[8].Value.ToString()), Convert.ToInt32(row.Cells[11].Value.ToString()), Convert.ToInt32((row.Cells[12].Value = true ? 1 : 0)), Convert.ToInt32((row.Cells[13].Value = true ? 1 : 0)), Convert.ToDateTime(DateTime.Now), Convert.ToInt32(row.Cells[10].Value.ToString()), Convert.ToInt32(row.Cells[9].Value.ToString()));
                    isporuka.InsertIsporPocetna(Convert.ToInt32(row.Cells[0].Value.ToString()), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(),row.Cells[3].Value.ToString(),  row.Cells[5].Value.ToString(), row.Cells[4].Value.ToString(), row.Cells[6].Value.ToString(), Convert.ToInt32(row.Cells[7].Value.ToString()), Convert.ToInt32(row.Cells[8].Value.ToString()), Convert.ToInt32(row.Cells[11].Value.ToString()), Convert.ToInt32((row.Cells[12].Value = true ? 1 : 0)), Convert.ToInt32((row.Cells[13].Value = true ? 1 : 0)), Convert.ToDateTime(DateTime.Now), Convert.ToInt32(row.Cells[10].Value.ToString()), Convert.ToInt32(row.Cells[9].Value.ToString()));

                    isporuka.SrediIstovar1Pocetna(Convert.ToInt32(row.Cells[0].Value.ToString()));
                   isporuka.SrediIstovar2Pocetna(Convert.ToInt32(row.Cells[0].Value.ToString()));
                   isporuka.SrediUtovar1Pocetna(Convert.ToInt32(row.Cells[0].Value.ToString()));
                   isporuka.SrediUtovar2Pocetna(Convert.ToInt32(row.Cells[0].Value.ToString()));
                    string connString =
        ConfigurationManager.AppSettings["webs"];
                    // string BarkodBezZ = textBox1.Text.TrimEnd(new char[] {'(', ')'});
                   // string BarkodBezZ = textBox1.Text.Replace("(", "");
                   // BarkodBezZ = BarkodBezZ.Replace(")", "");
                   // BarkodBezZ = BarkodBezZ.Replace(" ", "");
                    DataTable dataTableRes = new DataTable();
                    int brojisporuke = 0;


                }
            }
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row != null && row.Cells[0].Value != null)
                {

                    InsertIsporuka isporuka = new InsertIsporuka();
                    isporuka.InsertProizvodiPocetna(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), Convert.ToInt32(row.Cells[2].Value.ToString()), Convert.ToInt32(row.Cells[3].Value.ToString()), Convert.ToInt32(row.Cells[4].Value.ToString()), row.Cells[5].Value.ToString(), Convert.ToDouble(row.Cells[6].Value.ToString()));
                }
            }
            int barkodprovera = 0;
            int tekucibarkod = 0;
            int rb = 0;
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {   
                if (row != null && row.Cells[0].Value != null)
                {
                    tekucibarkod = Convert.ToInt32(row.Cells[1].Value.ToString());
                    if (tekucibarkod == barkodprovera)
                    { rb = rb + 1; }
                    else
                    {
                    rb = 1;
                    barkodprovera = tekucibarkod;
                    }
                    InsertIsporuka isporuka = new InsertIsporuka();
                    isporuka.InsertPaketiPocetna(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), Convert.ToInt32(row.Cells[2].Value.ToString()), row.Cells[3].Value.ToString(), row.Cells[4].Value.ToString(), row.Cells[5].Value.ToString(), row.Cells[7].Value.ToString(), row.Cells[8].Value.ToString(), Convert.ToDecimal(row.Cells[9].Value.ToString()), row.Cells[10].Value.ToString(), row.Cells[11].Value.ToString(), Convert.ToInt32(row.Cells[6].Value.ToString()), Convert.ToInt32(row.Cells[9].Value.ToString()), rb);
                    progressBar1.Value = progressBar1.Value + 1;
                }
            }
            string connString2 = ConfigurationManager.AppSettings["webs"];
            using (OleDbConnection conn = new OleDbConnection(connString2))
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row != null && row.Cells[0].Value != null)
                    {

                        //Promeniti da bi setovao po Barkodu isporuke// OleDbCommand cmd = new OleDbCommand("Update RkShipping Set ExportLargo = true where ShippingNo = " + Convert.ToInt32(textBox1.Text) + "", conn);
                        OleDbCommand cmd = new OleDbCommand("Update RkShipping Set ExportLargo = true where ShippingId = " + row.Cells[0].Value + "", conn);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            chkPrenesen.Checked = true;
            MessageBox.Show("Isporuke za Vozilo uspešno prenete ");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertIsporuka isporuka = new InsertIsporuka();
           // isporuka.InsertDobavnica(cboDatumUtovara.Text);
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row != null && row.Cells[0].Value != null)
                {
                    //isporuka.InsertDobavnicaPostav(Convert.ToInt32(row.Cells[11].Value.ToString()), Convert.ToDecimal(row.Cells[9].Value.ToString()));
                }
            }
            MessageBox.Show("Otpremnica  uspešno kreirana");
        }

        private void cboDatumUtovara_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            WebReklamacijeVoziloPocetna wrv = new WebReklamacijeVoziloPocetna();
            PravoP = wrv.Pravo;
            if (PravoP == true) { wrv.Show(); } else { return; }
        }
    }
}

