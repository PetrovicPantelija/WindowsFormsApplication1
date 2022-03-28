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
using FontAwesome.Sharp;

namespace WindowsFormsApplication1
{
    public partial class Main : Form
    {
        public bool PravoP;
        public static string code = "Main";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        // polja
        private IconButton curentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;

        int trebaazurirati = 0;
        public Main()
        {
            InitializeComponent();
            IdGrupe();
            IdForme();
            PravoPristupa();

            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 50);
            PanelMenu.Controls.Add(leftBorderBtn);
            //form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
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
        //Struktura za boje
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(250, 166, 27);
            public static Color color2 = Color.FromArgb(250, 166, 27);
            public static Color color3 = Color.FromArgb(250, 166, 27);
            public static Color color4 = Color.FromArgb(250, 166, 27);
            public static Color color5 = Color.FromArgb(250, 166, 27);
            public static Color color6 = Color.FromArgb(250, 166, 27);
            public static Color color7 = Color.FromArgb(250, 166, 27);
            public static Color color8 = Color.FromArgb(250, 166, 27);
        }

        //metoda
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                //button
                curentBtn = (IconButton)senderBtn;
                curentBtn.BackColor = Color.FromArgb(82, 108, 120);
                curentBtn.ForeColor = color;
                curentBtn.TextAlign = ContentAlignment.MiddleCenter;
                curentBtn.IconColor = color;
                curentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                curentBtn.ImageAlign = ContentAlignment.MiddleCenter;
                //left border btn
                //leftBorderBtn.BackColor = color;
                // leftBorderBtn.Location = new Point(0, curentBtn.Location.Y);
                // leftBorderBtn.Visible = true;
                // leftBorderBtn.BringToFront();
                //Icon curent child form
                //iconCurentChildForm.IconChar = curentBtn.IconChar;
                //iconCurentChildForm.IconColor = color;
            }
        }

        private void DisableButton()
        {
            if (curentBtn != null)
            {
                curentBtn.BackColor = Color.FromArgb(69, 90, 100);
                curentBtn.ForeColor = Color.White;
                curentBtn.TextAlign = ContentAlignment.MiddleLeft;
                curentBtn.IconColor = Color.White;
                curentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                curentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                //otvaranje samo jedne forme
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            //lblTitlechildForm.Text = childForm.Text;
        }

        public Main(int azuriraj)
        {
            InitializeComponent();
            trebaazurirati = azuriraj;
            IdGrupe();
            IdForme();
            PravoPristupa();
        }

        private void partneriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Partneri part = new Partneri();
            PravoP = part.Pravo;
            if (PravoP == true) { part.Show(); } else { return; }
        }

        private void naloziZaUtovarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 nn = new Form1();
            PravoP = nn.Pravo;
            if (PravoP == true) { nn.Show(); } else { return; }
        }

        private void reklamacijeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reklamacije rek = new Reklamacije();
            PravoP = rek.Pravo;
            if (PravoP == true) { rek.Show(); } else { return; }

        }

        private void rkShippingToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void rkShippingWEBSLargoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReferencijalnaTabela rt = new ReferencijalnaTabela();
            PravoP = rt.Pravo;
            if (PravoP == true) { rt.Show(); } else { return; }
        }

        private void wEBSIsporukaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void pregledIsporukeToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void pregledIsporukaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PregledIsporuka pi = new PregledIsporuka();
            PravoP = pi.Pravo;
            if (PravoP == true) { pi.Show(); } else { return; }
        }

        private void paketiPakeristiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PregledPaketi pak = new PregledPaketi();
            PravoP = pak.Pravo;
            if (PravoP == true) { pak.Show(); } else { return; }
        }

        private void pregledPreostaliPaketiPoIsporuciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreostaliPaketi preos = new PreostaliPaketi();
            PravoP = preos.Pravo;
            if (PravoP == true) { preos.Show(); } else { return; }
        }

        private void pregledKontroloriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PregledKontrolor pk = new PregledKontrolor();
            PravoP = pk.Pravo;
            if (PravoP == true) { pk.Show(); } else { return; }
        }

        private void wEBSPodaciToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void trVozilaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vozila voz = new Vozila();
            PravoP = voz.Pravo;
            if (PravoP == true) { voz.Show(); } else { return; }
        }

        private void partneriToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Komitenti part = new Komitenti();
            PravoP = part.Pravo;
            if (PravoP == true) { part.Show(); } else { return; }
        }

        private void saloniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Saloni sal = new Saloni();
            PravoP = sal.Pravo;
            if (PravoP == true) { sal.Show(); } else { return; }
        }

        private void isporukaUvozToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebIsporuka wi = new WebIsporuka();
            PravoP = wi.Pravo;
            if (PravoP == true) { wi.Show(); } else { return; }
        }

        private void kontrolorObradaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObradaIsporuke oi = new ObradaIsporuke();
            PravoP = oi.Pravo;
            if (PravoP == true) { oi.Show(); } else { return; }
        }

        private void isporukaUvozPoVoziluToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebIsporukaVozilo wiv = new WebIsporukaVozilo();
            PravoP = wiv.Pravo;
            if (PravoP == true) { wiv.Show(); } else { return; }
        }

        private void istorijaRadaKontroloraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IstorijaRadaKontrolor irk = new IstorijaRadaKontrolor();
            PravoP = irk.Pravo;
            if (PravoP == true) { irk.Show(); } else { return; }
        }

        private void štampaZaPakeristuToolStripMenuItem_Click(object sender, EventArgs e)
        {
           StampaP sp = new StampaP();
            PravoP = sp.Pravo;
            if (PravoP == true) { sp.Show(); } else { return; }
        }

        private void poništiKontroloraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateKontrolor upk = new UpdateKontrolor();
            PravoP = upk.Pravo;
            if (PravoP == true) { upk.Show(); } else { return; }

        }

        private void poništiPakeristuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdatePakerista upp = new UpdatePakerista();
            PravoP = upp.Pravo;
            if (PravoP == true) { upp.Show(); } else { return; }
        }

        private void istorijaRadaPoKontroloruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IstorijaRadaKontrolor isk = new IstorijaRadaKontrolor();
            PravoP = isk.Pravo;
            if (PravoP == true) { isk.Show(); } else { return; }
        }

        private void istorijaRadaPoPaletaruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IstorijaRadaPakeriste isp = new IstorijaRadaPakeriste();
            PravoP = isp.Pravo;
            if (PravoP == true) { isp.Show(); } else { return; }
        }

        private void sumarnoIsporukaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProveraIsporukeSum pis = new ProveraIsporukeSum();
            PravoP = pis.Pravo;
            if (PravoP == true) { pis.Show(); } else { return; }
        }

        private void završetakIsporukeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZakljucavanjeIsporuke zak = new ZakljucavanjeIsporuke();
            PravoP = zak.Pravo;
            if (PravoP == true) { zak.Show(); } else { return; }
        }

        private void artikalProveraBarkodaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Artikal art = new Artikal();
            PravoP = art.Pravo;
            if (PravoP == true) { art.Show(); } else { return; }
        }

        private void štampaZaPaletuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StampajPal sp = new StampajPal();
            PravoP = sp.Pravo;
            if (PravoP == true) { sp.Show(); } else { return; }
        }

        private void vToolStripMenuItem_Click(object sender, EventArgs e)
        {
            v123v vs = new v123v();
            PravoP = vs.Pravo;
            if (PravoP == true) { vs.Show(); } else { return; }
        }

        private void RefreshReferencna()
        {
            //Ažuriram Referencijalnu tabelu
            string connString =
          ConfigurationManager.AppSettings["webs"];

            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM RkShippingLinkWebsLargo", conn);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                adapter.Fill(dataTableRes);
                dataGridView1.DataSource = dataTableRes;
                conn.Close();
            }


            InsertReferencijalnaTabela irt = new InsertReferencijalnaTabela();
            irt.DeleteRefTab();
            progressBar1.Maximum = dataGridView1.Rows.Count;
            progressBar1.Step = 1;
            progressBar1.Value = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // currQty += row.Cells["qty"].Value;
                //More code here
                if (row != null && row.Cells[0].Value != null)
                {
                    progressBar1.Value = progressBar1.Value + 1;
                    irt.InsertRefTab(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), Convert.ToInt32(row.Cells[2].Value.ToString()), Convert.ToInt32(row.Cells[3].Value.ToString()), Convert.ToInt32(row.Cells[4].Value.ToString()), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString(), row.Cells[7].Value.ToString(), row.Cells[8].Value.ToString(), row.Cells[9].Value.ToString(), row.Cells[10].Value.ToString(), row.Cells[11].Value.ToString(), Convert.ToInt32(row.Cells[12].Value.ToString()), row.Cells[13].Value.ToString(), Convert.ToInt32(row.Cells[14].Value.ToString()), row.Cells[15].Value.ToString());
                }
            }
        
        }

        private void RefreshKlijenti()
        {
            string connString =
             ConfigurationManager.AppSettings["webs"];

            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT KomintentID, NazivKomintenta, Adresa FROM SysKomintenti", conn);

                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(dataTableRes);
                dataGridView2.DataSource = dataTableRes;
                DataGridViewColumn column = dataGridView2.Columns[0];
                dataGridView2.Columns[0].HeaderText = "Komitent ID";
                dataGridView2.Columns[0].Width = 50;

                DataGridViewColumn column2 = dataGridView2.Columns[1];
                dataGridView2.Columns[1].HeaderText = "Naziv komitenta";
                dataGridView2.Columns[1].Width = 450;

                DataGridViewColumn column3 = dataGridView2.Columns[2];
                dataGridView2.Columns[2].HeaderText = "Adresa";
                dataGridView2.Columns[2].Width = 300;
                conn.Close();
            }
            progressBar2.Maximum = dataGridView2.Rows.Count;
            progressBar2.Step = 1;
            progressBar2.Value = 0;
            InsertKomitenti ins = new InsertKomitenti();
            ins.DeleteSysKomitenti();
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {

                if (row != null && row.Cells[0].Value != null)
                {
                    progressBar2.Value = progressBar2.Value + 1;
                    ins.InsertKom(Convert.ToInt32(row.Cells[0].Value.ToString()), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString());
                }
            }

        }

        private void RefreshSaloni()
        {
            string connString =
             ConfigurationManager.AppSettings["webs"];

            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT KomintentIDz, SifraKomintentaMestoIsporuke, MestoIsporuke, MestoIsporukeAdresa FROM SysKomintentiMestoIsporuke", conn);

                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(dataTableRes);
                dataGridView3.DataSource = dataTableRes;
                dataGridView3.DataSource = dataTableRes;
                DataGridViewColumn column = dataGridView3.Columns[0];
                dataGridView3.Columns[0].HeaderText = "Komitent ID";
                dataGridView3.Columns[0].Width = 50;

                DataGridViewColumn column2 = dataGridView3.Columns[1];
                dataGridView3.Columns[1].HeaderText = "Šifra Salona";
                dataGridView1.Columns[1].Width = 50;

                DataGridViewColumn column3 = dataGridView3.Columns[2];
                dataGridView3.Columns[2].HeaderText = "Salon";
                dataGridView3.Columns[2].Width = 300;

                DataGridViewColumn column4 = dataGridView3.Columns[3];
                dataGridView3.Columns[3].HeaderText = "Adresa Salona";
                dataGridView3.Columns[3].Width = 300;
                conn.Close();
            }
            progressBar3.Maximum = dataGridView3.Rows.Count;
            progressBar3.Step = 1;
            progressBar3.Value = 0;
            InsertSaloni ins = new InsertSaloni();
            ins.DeleteSaloni();
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row != null && row.Cells[0].Value != null)
                {
                    progressBar3.Value = progressBar3.Value+1;
                    ins.InsertSal(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString());
                }
            }
        }

        private void RefreshVozila()
        {
            string connString =
                   ConfigurationManager.AppSettings["webs"];

            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM TrVozila", conn);

                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(dataTableRes);
                dataGridView4.DataSource = dataTableRes;
                conn.Close();
            }
            progressBar4.Maximum = dataGridView4.Rows.Count;
            progressBar4.Step = 1;
            progressBar4.Value = 0;
            InsertVozilo ins = new InsertVozilo();
            ins.DeleteVozila();
            ins.DeleteVozilaLargo();

            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                progressBar4.Value = progressBar4.Value + 1;
                int transport = 0;
                int prikljvoz = 0;

                if (row != null && row.Cells[0].Value != null)
                {
                    if (row.Cells[8].Value.ToString() == "true")
                    { 
                        transport = 1; 
                    }
                    else
                    {
                        transport = 0;
                    }
                    if (row.Cells[4].Value.ToString() == "true")
                    { prikljvoz = 1; }
                    else
                    {
                        prikljvoz = 0;
                    }
                    ins.InsertVoz(Convert.ToInt32(row.Cells[0].Value.ToString()), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), prikljvoz, Convert.ToInt32(row.Cells[5].Value.ToString()), Convert.ToInt32(row.Cells[6].Value.ToString()), Convert.ToInt32(row.Cells[7].Value.ToString()), transport);
                    ins.InsertVozLargo(Convert.ToInt32(row.Cells[0].Value.ToString()), row.Cells[1].Value.ToString());
                }
            }
        }

        private void RefreshVozaci()
        {
            string connString =
                   ConfigurationManager.AppSettings["webs"];

            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM TrVozaci", conn);

                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(dataTableRes);
                dataGridView5.DataSource = dataTableRes;
                conn.Close();
            }
            progressBar5.Maximum = dataGridView5.Rows.Count;
            progressBar5.Step = 1;
            progressBar5.Value = 0;
            InsertVozaci ins = new InsertVozaci();
            //ins.DeleteVozaci();
            ins.DeleteVozaciLargo();

            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                progressBar5.Value = progressBar5.Value + 1;
               // int transport = 0;
               // int prikljvoz = 0;

                if (row != null && row.Cells[0].Value != null)
                {
                   // ins.InsertVoz(Convert.ToInt32(row.Cells[0].Value.ToString()), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), prikljvoz, Convert.ToInt32(row.Cells[5].Value.ToString()), Convert.ToInt32(row.Cells[6].Value.ToString()), Convert.ToInt32(row.Cells[7].Value.ToString()), transport);
                    ins.InsertVozLargo(Convert.ToInt32(row.Cells[0].Value.ToString()), row.Cells[2].Value.ToString());
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
           // MessageBox.Show("Treba azurirati" + trebaazurirati);


           
           
            // End of referencijalna tabela
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            //Panta

            if (trebaazurirati == 0)
            {
                RefreshReferencna();
                RefreshKlijenti();
                RefreshSaloni();
                RefreshVozila();
                ////RefreshVozaci();

                MessageBox.Show("Ažuriranje podataka je obavljeno možete nastaviti sa daljim radom");
            }
         
        }

        private void obradaPaletarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObradaPaletar op = new ObradaPaletar();
            PravoP = op.Pravo;
            if (PravoP == true) { op.Show(); } else { return; }
        }

        private void štampajNeisporučenePaketeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StampajNeisporucene sneis = new StampajNeisporucene();
            PravoP = sneis.Pravo;
            if (PravoP == true) { sneis.Show(); } else { return; }
        }

        private void promenaVozilaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PromenaVozila pvoz = new PromenaVozila();
            PravoP = pvoz.Pravo;
            if (PravoP == true) { pvoz.Show(); } else { return; }
        }

        private void štampajSvePaletePoVoziluToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StampajPalSve sps = new StampajPalSve();
            PravoP = sps.Pravo;
            if (PravoP == true) { sps.Show(); } else { return; }
        }

        private void štampajNedostajućePaketePoVoziluToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StampajPalSveP spsp = new StampajPalSveP();
            PravoP = spsp.Pravo;
            if (PravoP == true) { spsp.Show(); } else { return; }
        }

        private void uvozReklamacijaPoVoziluToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebReklamacijeVozilo wrv = new WebReklamacijeVozilo();
            PravoP = wrv.Pravo;
            if (PravoP == true) { wrv.Show(); } else { return; }
        }

        private void štampajPregledPoVremenuPakovanjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StampajPalVremePak spkk = new StampajPalVremePak();
            PravoP = spkk.Pravo;
            if (PravoP == true) { spkk.Show(); } else { return; }
        }

        private void pregledPaletarKontrolorIstorijaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PregledKontrolorIstorija pki = new PregledKontrolorIstorija();
            PravoP = pki.Pravo;
            if (PravoP == true) { pki.Show(); } else { return; }
        }

        private void štampajReklamacijeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ŠtampajReklamacijeP srp = new ŠtampajReklamacijeP();
            PravoP = srp.Pravo;
            if (PravoP == true) { srp.Show(); } else { return; }
        }

        private void izbrisiCeluIsporukuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IzbrisiIsporuku izbr = new IzbrisiIsporuku();
            PravoP = izbr.Pravo;
            if (PravoP == true) { izbr.Show(); } else { return; }
        }

        private void otpremnicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOtpremnicaUvoz frmOU = new frmOtpremnicaUvoz();
            PravoP = frmOU.Pravo;
            if (PravoP == true) { frmOU.Show(); } else { return; }
        }

        private void pregledKontrolorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pregled2018 prgl2018 = new Pregled2018();
            PravoP = prgl2018.Pravo;
            if (PravoP == true) { prgl2018.Show(); } else { return; }
        }

        private void porudžbinaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExcelImportPorudzbina frmExcelimp = new frmExcelImportPorudzbina();
            PravoP = frmExcelimp.Pravo;
            if (PravoP == true) { frmExcelimp.Show(); } else { return; }
        }

        private void formiranjeTimovaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTimovi nft = new frmTimovi();
            PravoP = nft.Pravo;
            if (PravoP == true) { nft.Show(); } else { return; }
        }

        private void reklamacijeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmReklamacijaUvoz frek = new frmReklamacijaUvoz();
            PravoP = frek.Pravo;
            if (PravoP == true) { frek.Show(); } else { return; }
        }

        private void porudzbinaDirektnoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPorudzbinaImport pimp = new frmPorudzbinaImport();
            PravoP = pimp.Pravo;
            if (PravoP == true) { pimp.Show(); } else { return; }
        }

        private void pregledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPrikazStanja prikstan = new frmPrikazStanja();
            PravoP = prikstan.Pravo;
            if (PravoP == true) { prikstan.Show(); } else { return; }
        }

        private void pregledPoOtpremniciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOtpremnicaPregled otp = new frmOtpremnicaPregled();
            PravoP = otp.Pravo;
            if (PravoP == true) { otp.Show(); } else { return; }
        }

        private void pregledPorudžbineLargoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPorudzbinaPregled pregled = new frmPorudzbinaPregled();
            PravoP = pregled.Pravo;
            if (PravoP == true) { pregled.Show(); } else { return; }
        }

        private void arhivirajDoBrojaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 pr = new Form2();
            PravoP = pr.Pravo;
            if (PravoP == true) { pr.Show(); } else { return; }
        }

        private void pregledPodatakaArhivToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PregledKontrolorIstorijaArhiv pkis = new PregledKontrolorIstorijaArhiv();
            PravoP = pkis.Pravo;
            if (PravoP == true) { pkis.Show(); } else { return; }
        }

        private void obradaReklamacijeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObradaReklamacije obr = new ObradaReklamacije();
            PravoP = obr.Pravo;
            if (PravoP == true) { obr.Show(); } else { return; }
        }

        private void pozicijeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPozicije poz = new Magacin.frmPozicije();
            PravoP = poz.Pravo;
            if (PravoP == true) { poz.Show(); } else { return; }
        }

        private void paleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPaleta paleta = new Magacin.frmPaleta();
            PravoP = paleta.Pravo;
            if (PravoP == true) { paleta.Show(); } else { return; }
        }

        private void matičniPodaaciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmMaticniPodatak maticni = new Magacin.frmMaticniPodatak();
            PravoP = maticni.Pravo;
            if (PravoP == true) { maticni.Show(); } else { return; }
        }

        private void međuskladišniPrenosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMedjuskaldisniPrenosi frmM = new frmMedjuskaldisniPrenosi();
            PravoP = frmM.Pravo;
            if (PravoP == true) { frmM.Show(); } else { return; }
        }

        private void formiranjeMedjuskladišnogPrenosaIzZalihaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPrebacivanjeMedjuskladisniPrenos mprp = new frmPrebacivanjeMedjuskladisniPrenos();
            PravoP = mprp.Pravo;
            if (PravoP == true) { mprp.Show(); } else { return; }
        }

        private void štampaEtiketeRegalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmStampaPozicije poz = new Magacin.frmStampaPozicije();
            PravoP = poz.Pravo;
            if (PravoP == true) { poz.Show(); } else { return; }
        }

        private void formiranjeMedjuskladišnogOperativniPešićToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPlanMedjuskladisni plan = new Magacin.frmPlanMedjuskladisni();
            PravoP = plan.Pravo;
            if (PravoP == true) { plan.Show(); } else { return; }
        }

        private void direktnaIzdavanjaTrebovanjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPregledIzdavanja izd = new Magacin.frmPregledIzdavanja();
            PravoP = izd.Pravo;
            if (PravoP == true) { izd.Show(); } else { return; }
        }

        private void direktniPrijemiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPregledPrijemi pre = new Magacin.frmPregledPrijemi();
            PravoP = pre.Pravo;
            if (PravoP == true) { pre.Show(); } else { return; }
        }

        private void pregledStanjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPrijavaStanja prijstan = new Magacin.frmPrijavaStanja();
            PravoP = prijstan.Pravo;
            if (PravoP == true) { prijstan.Show(); } else { return; }
        }

        private void pregledRadnihNalogaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPregledRadnihNaloga radninalozi = new Magacin.frmPregledRadnihNaloga();
            PravoP = radninalozi.Pravo;
            if (PravoP == true) { radninalozi.Show(); } else { return; }
        }

        private void pregledStanjaPoPozicijamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void pregledStanjaPoPozicijamaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Magacin.frmPregledStanjaPoPozicijama pesic = new Magacin.frmPregledStanjaPoPozicijama();
            PravoP = pesic.Pravo;
            if (PravoP == true) { pesic.Show(); } else { return; }
        }

        private void raspolozivoStanjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRaspolozivoStanje pesic2 = new frmRaspolozivoStanje();
            PravoP = pesic2.Pravo;
            if (PravoP == true) { pesic2.Show(); } else { return; }
        }

        private void maticniPodaciPrikazToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmMaticniPodaciPrikaz mpPrikaz = new Magacin.frmMaticniPodaciPrikaz();
            PravoP = mpPrikaz.Pravo;
            if (PravoP == true) { mpPrikaz.Show(); } else { return; }
        }

        private void pregledRezervacijaPoPorudžbinamaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void kalkulatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReklmacijaKalkulator rkl = new ReklmacijaKalkulator();
            PravoP = rkl.Pravo;
            if (PravoP == true) { rkl.Show(); } else { return; }
        }

        private void šifreReklamacijeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSifraReklamacije fSR = new frmSifraReklamacije();
            PravoP = fSR.Pravo;
            if (PravoP == true) { fSR.Show(); } else { return; }
        }

        private void pregledReklamacijaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPregledReklamacijaSonja prs = new frmPregledReklamacijaSonja();
            PravoP = prs.Pravo;
            if (PravoP == true) { prs.Show(); } else { return; }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ObradaIsporukePocetna oi = new ObradaIsporukePocetna();
            PravoP = oi.Pravo;
            if (PravoP == true) {oi.Show(); } else { return; }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            WebIsporukaVoziloPocetna wiv = new WebIsporukaVoziloPocetna();
            PravoP = wiv.Pravo;
            if (PravoP == true) { wiv.Show(); } else { return; }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            PregledKontrolorZavrseno pkz = new PregledKontrolorZavrseno();
            PravoP = pkz.Pravo;
            if (PravoP == true) { pkz.Show(); } else { return; }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Magacin.frmOtpremnicaUvozPocetna pocot = new Magacin.frmOtpremnicaUvozPocetna();
            PravoP = pocot.Pravo;
            if (PravoP == true) { pocot.Show(); } else { return; };
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            frmReklamacijeUvozPocetna reklpoc = new frmReklamacijeUvozPocetna();
            PravoP = reklpoc.Pravo;
            if (PravoP == true) { reklpoc.Show(); } else { return; }
        }

        private void BtnWEBSPodaci_Click(object sender, EventArgs e)
        {
            Meniji.frmMenijiWEBSPodaci web = new Meniji.frmMenijiWEBSPodaci();
            ActivateButton(sender, RGBColors.color1);
            PravoP = web.Pravo;
            if (PravoP == true) { OpenChildForm(web); } else { return; }    
            
            
        }

        private void btnIsporuka_Click(object sender, EventArgs e)
        {
            Meniji.frmIsporuke i = new Meniji.frmIsporuke();
            PravoP = i.Pravo;
            if (PravoP == true)
            {
                ActivateButton(sender, RGBColors.color1);
                OpenChildForm(new Meniji.frmIsporuke());
            }
            else { return; }
        }

        private void btnPregledi_Click(object sender, EventArgs e)
        {
            Meniji.frmPregledi p = new Meniji.frmPregledi();
            PravoP = p.Pravo;
            if (PravoP == true)
            {
                ActivateButton(sender, RGBColors.color1);
                OpenChildForm(new Meniji.frmPregledi());
            }
            else { return; }
        }

        private void btnPonisti_Click(object sender, EventArgs e)
        {
            Meniji.frmPonisti p = new Meniji.frmPonisti();
            PravoP = p.Pravo;
            if (PravoP == true)
            {
                ActivateButton(sender, RGBColors.color1);
                OpenChildForm(new Meniji.frmPonisti());
            }
            else { return; }
        }

        private void btnIstorija_Click(object sender, EventArgs e)
        {
            Meniji.frmIstorija i = new Meniji.frmIstorija();
            PravoP = i.Pravo;
            if (PravoP == true)
            {
                ActivateButton(sender, RGBColors.color1);
                OpenChildForm(new Meniji.frmIstorija());
            }
            else { return; }
        }

        private void btnPrenos_Click(object sender, EventArgs e)
        {
            Meniji.frmPrenos p = new Meniji.frmPrenos();
            PravoP = p.Pravo;
            if (PravoP == true)
            {
                ActivateButton(sender, RGBColors.color1);
                OpenChildForm(new Meniji.frmPrenos());
            }
            else { return; }
        }

        private void btn2018_Click(object sender, EventArgs e)
        {
            Meniji.frm2018 f = new Meniji.frm2018();
            PravoP = f.Pravo;
            if (PravoP == true)
            {
                ActivateButton(sender, RGBColors.color1);
                OpenChildForm(new Meniji.frm2018());
            }
            else { return; }
        }

        private void btnTimovi_Click(object sender, EventArgs e)
        {
            Meniji.frmTimovi t = new Meniji.frmTimovi();
            PravoP = t.Pravo;
            if (PravoP == true)
            {
                ActivateButton(sender, RGBColors.color1);
                OpenChildForm(new Meniji.frmTimovi());
            }
            else { return; }
        }

        private void btnArhiviranje_Click(object sender, EventArgs e)
        {
            Meniji.frmArhiviranje a = new Meniji.frmArhiviranje();
            PravoP = a.Pravo;
            if (PravoP == true)
            {
                ActivateButton(sender, RGBColors.color1);
                OpenChildForm(new Meniji.frmArhiviranje());
            }
            else { return; }
        }

        private void btnReklamacija_Click(object sender, EventArgs e)
        {
            Meniji.frmReklamacije r = new Meniji.frmReklamacije();
            PravoP = r.Pravo;
            if (PravoP == true)
            {
                ActivateButton(sender, RGBColors.color1);
                OpenChildForm(new Meniji.frmReklamacije());
            }
            else { return; }
        }

        private void btnMagacin_Click(object sender, EventArgs e)
        {
            Meniji.frmMagacin m = new Meniji.frmMagacin();
            PravoP = m.Pravo;
            if (PravoP == true)
            {
                ActivateButton(sender, RGBColors.color1);
                OpenChildForm(new Meniji.frmMagacin());
            }
            else { return; }
        }

        private void btnAnalaizeMagacin_Click(object sender, EventArgs e)
        {
            Meniji.frmAnalizeMagacin m = new Meniji.frmAnalizeMagacin();
            PravoP = m.Pravo;
            if (PravoP == true)
            {
                ActivateButton(sender, RGBColors.color1);
                OpenChildForm(new Meniji.frmAnalizeMagacin());
            }
            else { return; }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
