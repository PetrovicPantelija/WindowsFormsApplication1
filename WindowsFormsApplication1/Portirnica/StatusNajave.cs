using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;


namespace WindowsFormsApplication1.Portirnica
{
    public partial class StatusNajave : Form
    {
        bool click = false;
        bool status = false;
        int PomPaleta1 = 0;
        int PomPaleta2 = 0;
        int PomDokumentacijaSpremna = 0;
        string UlogovaniKorisnik = "";
        MailMessage mailMessage;
        public StatusNajave()
        {
            InitializeComponent();


        }

        public StatusNajave(string Korisnik)
        {
            InitializeComponent();
            UlogovaniKorisnik = Korisnik;
        }
        private void RefreshDataGrid()
        {
            string select = "SELECT [ID] ,[UtovarBroj] , DatumNajave,  Vozilo,[RegistarskiBroj] ,[DatumVremeDolaska],[PrevoznikKlijent] ,[Partner] ,[Napomena]," +
                " [KorisnikOtvorio], [DatumVremeOtvaranja], [Gate], [KorinsikPrimio], [DatumVremePrijema],  [NapomenaPrijema] ," +
                " CASE WHEN [PaletaSpremne1] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [PaletaSpremne1] , [DatumSpremne1] , [KontrolorSpremne1], " +
                "CASE WHEN [PaletaSpremne2] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [PaletaSpremne2] , [DatumSpremne2] ,[KontrolorSpremne2], " +
                "CASE WHEN [DokumentacijaSpremne] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [DokumentacijaSpremne] , " +
                "[ProdajaKorisnik], [DatumDokumentSpremne],[NapomenaKontrola]," +
                "CASE WHEN korisnik <> '' THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Spremljeno,[DatumKamionIzasao],[Korisnik],[NapomenaIzlaz] " +
                "FROM [dbo].[StatusNajave] order by ID desc";


            string s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlConnection c = new SqlConnection(s_connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(select, c);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Otpremnica";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Datum isporuke";
            dataGridView1.Columns[2].Width = 120;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Vozilo";
            dataGridView1.Columns[3].Width = 120;


            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Registarski broj vozila";
            dataGridView1.Columns[4].Width = 120;


            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Datum i vreme dolaska";
            dataGridView1.Columns[5].Width = 120;


            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Prevozni klijent";
            dataGridView1.Columns[6].Width = 40;


            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Partner";
            dataGridView1.Columns[7].Width = 120;


            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Napomena najave";
            dataGridView1.Columns[8].Width = 40;


            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Korisnik najavio vozilo";
            dataGridView1.Columns[9].Width = 40;


            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Datum i vreme najave";
            dataGridView1.Columns[10].Width = 80;


            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Gate";
            dataGridView1.Columns[11].Width = 50;


            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Korisnik primio vozilo";
            dataGridView1.Columns[12].Width = 40;


            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Datum i vreme prijema";
            dataGridView1.Columns[13].Width = 120;


            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Napomena prijema";
            dataGridView1.Columns[14].Width = 40;


            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Isporuka paletirana 1";
            dataGridView1.Columns[15].Width = 40;


            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Datum i vreme zavr paletiranja 1";
            dataGridView1.Columns[16].Width = 40;


            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Paletirao kontrolor 1";
            dataGridView1.Columns[17].Width = 40;


            DataGridViewColumn column19 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "Isporuka utovarena 2";
            dataGridView1.Columns[18].Width = 40;


            DataGridViewColumn column20 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "Datum i vreme zavr utovara 2";
            dataGridView1.Columns[19].Width = 40;


            DataGridViewColumn column21 = dataGridView1.Columns[20];
            dataGridView1.Columns[20].HeaderText = "Utovario kontrolor 2";
            dataGridView1.Columns[20].Width = 40;


            DataGridViewColumn column22 = dataGridView1.Columns[21];
            dataGridView1.Columns[21].HeaderText = "Dokumentacija spremna";
            dataGridView1.Columns[21].Width = 40;



            DataGridViewColumn column23 = dataGridView1.Columns[22];
            dataGridView1.Columns[22].HeaderText = "Dokumentaciju spremio i izdao";
            dataGridView1.Columns[22].Width = 120;


            DataGridViewColumn column24 = dataGridView1.Columns[23];
            dataGridView1.Columns[23].HeaderText = "Datum i vreme izdavanja dokumentacije";
            dataGridView1.Columns[23].Width = 40;

            dataGridView1.Columns[24].HeaderText = "Napomena Kontrolor";
            dataGridView1.Columns[24].Width = 40;


            DataGridViewColumn column25 = dataGridView1.Columns[24];
            dataGridView1.Columns[25].HeaderText = "Vozilo izašlo";
            dataGridView1.Columns[25].Width = 40;


            DataGridViewColumn column26 = dataGridView1.Columns[25];
            dataGridView1.Columns[26].HeaderText = "Datum i vreme izlaska vozila";
            dataGridView1.Columns[26].Width = 120;


            DataGridViewColumn column27 = dataGridView1.Columns[26];
            dataGridView1.Columns[27].HeaderText = "Korisnik propustio vozilo";
            dataGridView1.Columns[27].Width = 120;

            dataGridView1.Columns[28].HeaderText = "Napomena Izlaz";
            dataGridView1.Columns[28].Width = 40;


            ObojDatagrid();
        }
        private void ObojDatagrid()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    if (dataGridView1.Rows[row.Index].Cells[9].Value.ToString() != "")
                    {
                        dataGridView1.Rows[row.Index].Cells[0].Style.BackColor = System.Drawing.Color.Red;
                        dataGridView1.Rows[row.Index].Cells[1].Style.BackColor = System.Drawing.Color.Red;
                        dataGridView1.Rows[row.Index].Cells[2].Style.BackColor = System.Drawing.Color.Red;
                        dataGridView1.Rows[row.Index].Cells[3].Style.BackColor = System.Drawing.Color.Red;
                        dataGridView1.Rows[row.Index].Cells[4].Style.BackColor = System.Drawing.Color.Red;
                        dataGridView1.Rows[row.Index].Cells[5].Style.BackColor = System.Drawing.Color.Red;
                        dataGridView1.Rows[row.Index].Cells[6].Style.BackColor = System.Drawing.Color.Red;
                        dataGridView1.Rows[row.Index].Cells[7].Style.BackColor = System.Drawing.Color.Red;
                        dataGridView1.Rows[row.Index].Cells[8].Style.BackColor = System.Drawing.Color.Red;
                        dataGridView1.Rows[row.Index].Cells[9].Style.BackColor = System.Drawing.Color.Red;
                        dataGridView1.Rows[row.Index].Cells[10].Style.BackColor = System.Drawing.Color.Red;
                        dataGridView1.Rows[row.Index].Cells[11].Style.BackColor = System.Drawing.Color.Red;
                    }
                    if (dataGridView1.Rows[row.Index].Cells[12].Value.ToString() != "")
                    {
                        dataGridView1.Rows[row.Index].Cells[12].Style.BackColor = System.Drawing.Color.Yellow;
                        dataGridView1.Rows[row.Index].Cells[13].Style.BackColor = System.Drawing.Color.Yellow;
                        dataGridView1.Rows[row.Index].Cells[14].Style.BackColor = System.Drawing.Color.Yellow;
                    }

                    if (dataGridView1.Rows[row.Index].Cells[22].Value.ToString() != "")
                    {
                        dataGridView1.Rows[row.Index].Cells[15].Style.BackColor = System.Drawing.Color.SteelBlue;
                        dataGridView1.Rows[row.Index].Cells[16].Style.BackColor = System.Drawing.Color.SteelBlue;
                        dataGridView1.Rows[row.Index].Cells[17].Style.BackColor = System.Drawing.Color.SteelBlue;
                        dataGridView1.Rows[row.Index].Cells[18].Style.BackColor = System.Drawing.Color.SteelBlue;
                        dataGridView1.Rows[row.Index].Cells[19].Style.BackColor = System.Drawing.Color.SteelBlue;
                        dataGridView1.Rows[row.Index].Cells[20].Style.BackColor = System.Drawing.Color.SteelBlue;
                        dataGridView1.Rows[row.Index].Cells[21].Style.BackColor = System.Drawing.Color.SteelBlue;
                        dataGridView1.Rows[row.Index].Cells[22].Style.BackColor = System.Drawing.Color.SteelBlue;
                        dataGridView1.Rows[row.Index].Cells[23].Style.BackColor = System.Drawing.Color.SteelBlue;
                        dataGridView1.Rows[row.Index].Cells[24].Style.BackColor = System.Drawing.Color.SteelBlue;

                        dataGridView1.Rows[row.Index].Cells[15].Style.ForeColor = System.Drawing.Color.White;
                        dataGridView1.Rows[row.Index].Cells[16].Style.ForeColor = System.Drawing.Color.White;
                        dataGridView1.Rows[row.Index].Cells[17].Style.ForeColor = System.Drawing.Color.White;
                        dataGridView1.Rows[row.Index].Cells[18].Style.ForeColor = System.Drawing.Color.White;
                        dataGridView1.Rows[row.Index].Cells[19].Style.ForeColor = System.Drawing.Color.White;
                        dataGridView1.Rows[row.Index].Cells[20].Style.ForeColor = System.Drawing.Color.White;
                        dataGridView1.Rows[row.Index].Cells[21].Style.ForeColor = System.Drawing.Color.White;
                        dataGridView1.Rows[row.Index].Cells[22].Style.ForeColor = System.Drawing.Color.White;
                        dataGridView1.Rows[row.Index].Cells[23].Style.ForeColor = System.Drawing.Color.White;
                        dataGridView1.Rows[row.Index].Cells[24].Style.ForeColor = System.Drawing.Color.White;
                    }

                    if (dataGridView1.Rows[row.Index].Cells[27].Value.ToString() != "")
                    {
                        dataGridView1.Rows[row.Index].Cells[25].Style.BackColor = System.Drawing.Color.Green;
                        dataGridView1.Rows[row.Index].Cells[26].Style.BackColor = System.Drawing.Color.Green;
                        dataGridView1.Rows[row.Index].Cells[27].Style.BackColor = System.Drawing.Color.Green;
                        dataGridView1.Rows[row.Index].Cells[28].Style.BackColor = System.Drawing.Color.Green;
                    }

                }
            }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txtSifra.Text = "";
            txtSifra.Enabled = false;
            status = true;
            txtRegistarskiBroj.Text = "";
            txtPrevozniKlijent.Text = "";
            txtPartner.Text = "";
            txtUtovarBroj.Text = "";
            txtNapomena.Text = "";
            txtKorisnikOtvorio.Text = "";
            txtGate.Text = "";
            txtkorisnikPrimio.Text = "";
            txtNapomenaPrijem.Text = "";
            txtKontrolor1.Text = "";
            txtKontrolor2.Text = "";
            txtProdajaKorisnik.Text = "";
            txtKorisnik.Text = "";
            chkPaleta1.Checked = false;
            chkPaleta2.Checked = false;
            chkDokumentacijaSpremna.Checked = false;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            noviUnosToolStripMenuItem.BackColor = default;
            if (chkPaleta1.Checked)
            {
                PomPaleta1 = 1;
            }
            else
            {
                PomPaleta1 = 0;
            }

            if (chkPaleta2.Checked)
            {
                PomPaleta2 = 1;
            }
            else
            {
                PomPaleta2 = 0;
            }

            if (chkDokumentacijaSpremna.Checked)
            {
                PomDokumentacijaSpremna = 1;
            }
            else
            {
                PomDokumentacijaSpremna = 0;
            }

            if (status == true)
            {//Ovde ispravi !!!
                //Unosi transport
                InsertStatusNajave ins = new InsertStatusNajave();
                ins.InsSatusNajave(txtRegistarskiBroj.Text, txtPrevozniKlijent.Text, txtPartner.Text, Convert.ToDateTime(dtpDatumVremeDolaska.Value), txtNapomena.Text, txtUtovarBroj.Text, UlogovaniKorisnik, DateTime.Now, txtGate.Text, Convert.ToDateTime(dtpVreme.Value), "", "", 0, Convert.ToDateTime(dtpVreme.Value), 0, Convert.ToDateTime(dtpVreme.Value), "", "", 0, "", Convert.ToDateTime(dtpVreme.Value), Convert.ToDateTime(Convert.ToDateTime(dtpVreme.Value)), "", cboDatumUtovara.Text, cboVozila.Text, txtNapomenaKontrola.Text, txtNapomenaIzlaz.Text);
                ins.UpdRkShippingZavrseno(cboDatumUtovara.Text, cboVozila.Text);
                ins.UpdRkShipping(cboDatumUtovara.Text, cboVozila.Text);
                ins.UpdRkShippingPocetna(cboDatumUtovara.Text, cboVozila.Text);
                /*
                txtRegistarskiBroj.Text, txtPrevozniKlijent.Text, txtPartner.Text, Convert.ToDateTime(dtpDatumVremeDolaska.Value), txtNapomena.Text,txtUtovarBroj.Text, UlogovaniKorisnik, DateTime.Now, txtGate.Text,
                      Convert.ToDateTime(dtpDatumVremePrijem.Value), txtkorisnikPrimio.Text, txtNapomenaPrijem.Text, 
                    
                    PomPaleta1, Convert.ToDateTime(dtpDatum1.Value), PomPaleta2, Convert.ToDateTime(dtpDatum2.Value), txtKontrolor1.Text, txtKontrolor2.Text, PomDokumentacijaSpremna, txtProdajaKorisnik.Text,  Convert.ToDateTime(dtpDokumentacijaSpremna.Value),
                    Convert.ToDateTime(dtpKamionIzasao.Value), txtKorisnik.Text)
                */
                status = false;
                PopuniDatumUtovara();
                cboVozila.Text = "";
                //RefreshDataGrid();
            }
            else
            {//Ovde ispravi !!!
                //int TipCenovnika ,int Komitent, double Cena , int VrstaManipulacije ,DateTime  Datum , string Korisnik
                if (txtKorisnikOtvorio != null || txtkorisnikPrimio != null || txtKontrolor1 != null || txtKontrolor2 != null || txtProdajaKorisnik != null || txtKorisnik != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da promenite zapis broj: " + txtSifra.Text + "? \nUkoliko izaberete DA može doći do brisanja svih unetih podataka", "Izbor", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        InsertStatusNajave upd = new InsertStatusNajave();
                        upd.UpdStatusNajave(Convert.ToInt32(txtSifra.Text), txtRegistarskiBroj.Text, txtPrevozniKlijent.Text, txtPartner.Text, Convert.ToDateTime(dtpDatumVremeDolaska.Value), txtNapomena.Text, txtUtovarBroj.Text, UlogovaniKorisnik, DateTime.Now, txtGate.Text, Convert.ToDateTime(dtpVreme.Value), "", "", 0, Convert.ToDateTime(dtpVreme.Value), 0, Convert.ToDateTime(dtpVreme.Value), "", "", 0, "", Convert.ToDateTime(dtpVreme.Value), Convert.ToDateTime(Convert.ToDateTime(dtpVreme.Value)), "", cboDatumUtovara.Text, cboVozila.Text, txtNapomenaKontrola.Text, txtNapomenaIzlaz.Text);
                        InsertLogNajave inslog = new InsertLogNajave();
                        inslog.InsLogNajave(Convert.ToInt32(txtSifra.Text), "Transport promena", UlogovaniKorisnik, DateTime.Now);
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                    }
                }
            }
            RefreshDataGrid();

        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da brišete?", "Izbor", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                InsertStatusNajave upd = new InsertStatusNajave();
                upd.DeleteStatusNjave(Convert.ToInt32(txtSifra.Text));
                InsertLogNajave inslog = new InsertLogNajave();
                inslog.InsLogNajave(Convert.ToInt32(txtSifra.Text), "Brisanje zapisa", UlogovaniKorisnik, DateTime.Now);
                RefreshDataGrid();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void VratiPodatke(string ID)
        {
            string s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [ID], [RegistarskiBroj], [PrevoznikKlijent], [Partner], [DatumVremeDolaska], [UtovarBroj], [Napomena] , " +
          " [KorisnikOtvorio], [DatumVremeOtvaranja], [Gate], [DatumVremePrijema], [KorinsikPrimio], [NapomenaPrijema], [DatumSpremne1], [DatumSpremne2], [KontrolorSpremne1], [KontrolorSpremne2] , PaletaSpremne1, PaletaSpremne2, DokumentacijaSpremne," +
          " [ProdajaKorisnik], [DatumDokumentSpremne],[NapomenaKontrola], [DatumKamionIzasao], [Korisnik],[NapomenaIzlaz], DatumNajave, Vozilo FROM [dbo].[StatusNajave] where ID=" + txtSifra.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtRegistarskiBroj.Text = dr["RegistarskiBroj"].ToString();
                txtPrevozniKlijent.Text = dr["PrevoznikKlijent"].ToString();
                txtPartner.Text = dr["Partner"].ToString();
                dtpDatumVremeDolaska.Value = Convert.ToDateTime(dr["DatumVremeDolaska"].ToString());
                txtUtovarBroj.Text = dr["UtovarBroj"].ToString();
                txtNapomena.Text = dr["Napomena"].ToString();
                txtKorisnikOtvorio.Text = dr["KorisnikOtvorio"].ToString();
                dtpVremeOtvaranja.Value = Convert.ToDateTime(dr["DatumVremeOtvaranja"].ToString());
                txtGate.Text = dr["Gate"].ToString();
                dtpDatumVremePrijem.Value = Convert.ToDateTime(dr["DatumVremePrijema"].ToString());
                txtkorisnikPrimio.Text = dr["KorinsikPrimio"].ToString();
                txtNapomenaPrijem.Text = dr["NapomenaPrijema"].ToString();
                dtpDatum1.Value = Convert.ToDateTime(dr["DatumSpremne1"].ToString());
                dtpDatum2.Value = Convert.ToDateTime(dr["DatumSpremne2"].ToString());
                txtKontrolor1.Text = dr["KontrolorSpremne1"].ToString();
                txtKontrolor2.Text = dr["KontrolorSpremne2"].ToString();
                txtProdajaKorisnik.Text = dr["ProdajaKorisnik"].ToString();
                dtpDokumentacijaSpremna.Value = Convert.ToDateTime(dr["DatumDokumentSpremne"].ToString());
                txtNapomenaKontrola.Text = dr["NapomenaKontrola"].ToString();
                dtpKamionIzasao.Value = Convert.ToDateTime(dr["DatumKamionIzasao"].ToString());
                txtKorisnik.Text = dr["Korisnik"].ToString();
                txtNapomenaIzlaz.Text = dr["NapomenaIzlaz"].ToString();

                if (dr["PaletaSpremne1"].ToString() == "1")
                {
                    chkPaleta1.Checked = true;
                }
                else
                {
                    chkPaleta1.Checked = false;

                }
                if (dr["PaletaSpremne2"].ToString() == "1")
                {
                    chkPaleta2.Checked = true;
                }
                else
                {
                    chkPaleta2.Checked = false;

                }
                if (dr["DokumentacijaSpremne"].ToString() == "1")
                {
                    chkDokumentacijaSpremna.Checked = true;
                }
                else
                {
                    chkDokumentacijaSpremna.Checked = false;

                }
                cboDatumUtovara.Text = dr["DatumNajave"].ToString();
                cboVozila.Text = dr["Vozilo"].ToString();

            }
            con.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifra.Text = row.Cells[0].Value.ToString();
                        VratiPodatke(txtSifra.Text);
                        // txtOpis.Text = row.Cells[1].Value.ToString();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void VratiPodatkeMax()
        {
            string s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from Komitenti", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }

            con.Close();
        }

        private void dtpDatumVremePrijema_Load(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }
        private void noviUnosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button5.Enabled = false;
            button9.Enabled = false;


            noviUnosToolStripMenuItem.BackColor = Color.Orange;
            logFajlToolStripMenuItem.BackColor = default;
            najavljeniToolStripMenuItem.BackColor = default;
            primljeniToolStripMenuItem.BackColor = default;
            spremniZaIzlazakToolStripMenuItem.BackColor = default;
            izašliToolStripMenuItem.BackColor = default;
            posaljiMailPesicuToolStripMenuItem.BackColor = default;


            txtSifra.Text = "";
            txtSifra.Enabled = false;
            status = true;
            txtRegistarskiBroj.Text = "";
            txtPrevozniKlijent.Text = "";
            txtPartner.Text = "";
            txtUtovarBroj.Text = "";
            txtNapomena.Text = "";
            txtKorisnikOtvorio.Text = "";
            txtGate.Text = "";
            txtkorisnikPrimio.Text = "";
            txtNapomenaPrijem.Text = "";
            txtKontrolor1.Text = "";
            txtKontrolor2.Text = "";
            txtProdajaKorisnik.Text = "";
            txtKorisnik.Text = "";
            chkPaleta1.Checked = false;
            chkPaleta2.Checked = false;
            chkDokumentacijaSpremna.Checked = false;
            PopuniDatumUtovara();

        }
        private void PopuniDatumUtovara()
        {
            string select = "Select distinct t1.DatumUtovara as DatumUtovara, t1.Mesec, t1.Dan" +
                " from (Select Distinct  DatumUtovara, Cast(SUBSTRING(DatumUtovara, 1, Charindex('.',DatumUtovara) -1) as int)  as  Dan," +
                " Cast(Replace(SUBSTRING(DatumUtovara, Charindex('.',DatumUtovara)+1, Len(DatumUtovara)-12 -1 -( (Charindex('.',DatumUtovara) ))),'.','') as int)" +
                " as  Mesec  From RkShippingPocetna where Najavljen <> 1" +
                " union " +
                "Select Distinct DatumUtovara, Cast(SUBSTRING(DatumUtovara, 1, Charindex('.', DatumUtovara) - 1) as int)  as Dan, " +
                "Cast(Replace(SUBSTRING(DatumUtovara, Charindex('.', DatumUtovara) + 1, Len(DatumUtovara) - 12 - 1 - ((Charindex('.', DatumUtovara)))), '.', '') as int)" +
                " as Mesec  From RkShipping where Najavljen <> 1 " +
                "union" +
                " Select Distinct DatumUtovara, Cast(SUBSTRING(DatumUtovara, 1, Charindex('.', DatumUtovara) - 1) as int)  as Dan," +
                " Cast(Replace(SUBSTRING(DatumUtovara, Charindex('.', DatumUtovara) + 1, Len(DatumUtovara) - 12 - 1 - ((Charindex('.', DatumUtovara)))), '.', '') as int) " +
                "as Mesec  From RkShippingZavrseno where Najavljen <> 1) as t1 order by t1.Mesec, t1.Dan";

            string s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlConnection c = new SqlConnection(s_connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(select, c);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            cboDatumUtovara.DataSource = ds.Tables[0];
            cboDatumUtovara.DisplayMember = "DatumUtovara";
            cboDatumUtovara.ValueMember = "DatumUtovara";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InsertStatusNajave upd = new InsertStatusNajave();

            upd.UpdStatusNajave(Convert.ToInt32(txtSifra.Text), txtRegistarskiBroj.Text, txtPrevozniKlijent.Text, txtPartner.Text, Convert.ToDateTime(dtpDatumVremeDolaska.Value), txtNapomena.Text, txtUtovarBroj.Text, txtKorisnikOtvorio.Text, Convert.ToDateTime(dtpVremeOtvaranja.Value), txtGate.Text, Convert.ToDateTime(DateTime.Now), UlogovaniKorisnik, txtNapomenaPrijem.Text, 0, Convert.ToDateTime(dtpVreme.Value), 0, Convert.ToDateTime(dtpVreme.Value), "", "", 0, "", Convert.ToDateTime(dtpVreme.Value), Convert.ToDateTime(Convert.ToDateTime(dtpVreme.Value)), "", cboDatumUtovara.Text, cboVozila.Text, txtNapomenaKontrola.Text, txtNapomenaIzlaz.Text);
            InsertLogNajave inslog = new InsertLogNajave();
            inslog.InsLogNajave(Convert.ToInt32(txtSifra.Text), "Portir ulaz promena", UlogovaniKorisnik, DateTime.Now);

            najavljeniToolStripMenuItem.BackColor = default;

            string select = "  SELECT [ID] ,[UtovarBroj] , DatumNajave,  Vozilo,[RegistarskiBroj] ,[DatumVremeDolaska],[PrevoznikKlijent] ,[Partner] ,[Napomena],  " +
                " [KorisnikOtvorio], [DatumVremeOtvaranja], [Gate], [KorinsikPrimio], [DatumVremePrijema],  [NapomenaPrijema] , " +
                " CASE WHEN [PaletaSpremne1] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [PaletaSpremne1] , [DatumSpremne1] , [KontrolorSpremne1],  " +
                " CASE WHEN [PaletaSpremne2] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [PaletaSpremne2] , [DatumSpremne2] , " +
                " [KontrolorSpremne2] , " +
                " CASE WHEN [DokumentacijaSpremne] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [DokumentacijaSpremne] , " +
                "  [ProdajaKorisnik], [DatumDokumentSpremne],[NapomenaKontrola]," +
                " CASE WHEN korisnik <> '' THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Spremljeno, " +
                " [DatumKamionIzasao],[Korisnik],[NapomenaIzlaz] FROM [dbo].[StatusNajave] " +
         " where KorinsikPrimio = '' order by ID desc";


            string s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlConnection c = new SqlConnection(s_connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(select, c);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];


            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Otpremnica";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Datum isporuke";
            dataGridView1.Columns[2].Width = 120;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Vozilo";
            dataGridView1.Columns[3].Width = 120;


            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Registarski broj vozila";
            dataGridView1.Columns[4].Width = 120;


            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Datum i vreme dolaska";
            dataGridView1.Columns[5].Width = 120;


            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Prevozni klijent";
            dataGridView1.Columns[6].Width = 40;


            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Partner";
            dataGridView1.Columns[7].Width = 120;


            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Napomena najave";
            dataGridView1.Columns[8].Width = 40;


            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Korisnik najavio vozilo";
            dataGridView1.Columns[9].Width = 40;


            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Datum i vreme najave";
            dataGridView1.Columns[10].Width = 80;


            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Gate";
            dataGridView1.Columns[11].Width = 50;


            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Korisnik primio vozilo";
            dataGridView1.Columns[12].Width = 40;


            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Datum i vreme prijema";
            dataGridView1.Columns[13].Width = 120;


            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Napomena prijema";
            dataGridView1.Columns[14].Width = 40;


            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Isporuka paletirana 1";
            dataGridView1.Columns[15].Width = 40;


            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Datum i vreme zavr paletiranja 1";
            dataGridView1.Columns[16].Width = 40;


            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Paletirao kontrolor 1";
            dataGridView1.Columns[17].Width = 40;


            DataGridViewColumn column19 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "Isporuka utovarena 2";
            dataGridView1.Columns[18].Width = 40;


            DataGridViewColumn column20 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "Datum i vreme zavr utovara 2";
            dataGridView1.Columns[19].Width = 40;


            DataGridViewColumn column21 = dataGridView1.Columns[20];
            dataGridView1.Columns[20].HeaderText = "Utovario kontrolor 2";
            dataGridView1.Columns[20].Width = 40;


            DataGridViewColumn column22 = dataGridView1.Columns[21];
            dataGridView1.Columns[21].HeaderText = "Dokumentacija spremna";
            dataGridView1.Columns[21].Width = 40;



            DataGridViewColumn column23 = dataGridView1.Columns[22];
            dataGridView1.Columns[22].HeaderText = "Dokumentaciju spremio i izdao";
            dataGridView1.Columns[22].Width = 120;


            DataGridViewColumn column24 = dataGridView1.Columns[23];
            dataGridView1.Columns[23].HeaderText = "Datum i vreme izdavanja dokumentacije";
            dataGridView1.Columns[23].Width = 40;

            dataGridView1.Columns[24].HeaderText = "Napomena Kontrolor";
            dataGridView1.Columns[24].Width = 40;


            DataGridViewColumn column25 = dataGridView1.Columns[24];
            dataGridView1.Columns[25].HeaderText = "Vozilo izašlo";
            dataGridView1.Columns[25].Width = 40;


            DataGridViewColumn column26 = dataGridView1.Columns[25];
            dataGridView1.Columns[26].HeaderText = "Datum i vreme izlaska vozila";
            dataGridView1.Columns[26].Width = 120;


            DataGridViewColumn column27 = dataGridView1.Columns[26];
            dataGridView1.Columns[27].HeaderText = "Korisnik propustio vozilo";
            dataGridView1.Columns[27].Width = 120;

            dataGridView1.Columns[28].HeaderText = "Napomena Izlaz";
            dataGridView1.Columns[28].Width = 40;

            ObojDatagrid();



            //RefreshDataGrid();

            /*
            txtRegistarskiBroj.Text, txtPrevozniKlijent.Text, txtPartner.Text, Convert.ToDateTime(dtpDatumVremeDolaska.Value), txtNapomena.Text,txtUtovarBroj.Text, UlogovaniKorisnik, DateTime.Now, txtGate.Text,
                  Convert.ToDateTime(dtpDatumVremePrijem.Value), txtkorisnikPrimio.Text, txtNapomenaPrijem.Text, 

                PomPaleta1, Convert.ToDateTime(dtpDatum1.Value), PomPaleta2, Convert.ToDateTime(dtpDatum2.Value), txtKontrolor1.Text, txtKontrolor2.Text, PomDokumentacijaSpremna, txtProdajaKorisnik.Text,  Convert.ToDateTime(dtpDokumentacijaSpremna.Value),
                Convert.ToDateTime(dtpKamionIzasao.Value), txtKorisnik.Text)
            */
            status = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            spremniZaIzlazakToolStripMenuItem.BackColor = default;
            InsertStatusNajave upd = new InsertStatusNajave();
            upd.UpdStatusNajave(Convert.ToInt32(txtSifra.Text), txtRegistarskiBroj.Text, txtPrevozniKlijent.Text, txtPartner.Text, Convert.ToDateTime(dtpDatumVremeDolaska.Value), txtNapomena.Text, txtUtovarBroj.Text, txtKorisnikOtvorio.Text, Convert.ToDateTime(dtpVremeOtvaranja.Value), txtGate.Text, Convert.ToDateTime(dtpDatumVremePrijem.Value), txtkorisnikPrimio.Text, txtNapomenaPrijem.Text, 1, Convert.ToDateTime(DateTime.Now), 0, Convert.ToDateTime(dtpVreme.Value), UlogovaniKorisnik, "", 0, "", Convert.ToDateTime(dtpVreme.Value), Convert.ToDateTime(Convert.ToDateTime(dtpVreme.Value)), "", cboDatumUtovara.Text, cboVozila.Text, txtNapomenaKontrola.Text, txtNapomenaIzlaz.Text);
            InsertLogNajave inslog = new InsertLogNajave();
            inslog.InsLogNajave(Convert.ToInt32(txtSifra.Text), "Kontrolor 1 promena", UlogovaniKorisnik, DateTime.Now);


            string select = "  SELECT [ID] ,[UtovarBroj] , DatumNajave,  Vozilo,[RegistarskiBroj] ,[DatumVremeDolaska],[PrevoznikKlijent] ,[Partner] ,[Napomena],  " +
              " [KorisnikOtvorio], [DatumVremeOtvaranja], [Gate], [KorinsikPrimio], [DatumVremePrijema],  [NapomenaPrijema] , " +
              " CASE WHEN [PaletaSpremne1] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [PaletaSpremne1] , [DatumSpremne1] , [KontrolorSpremne1],  " +
              " CASE WHEN [PaletaSpremne2] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [PaletaSpremne2] , [DatumSpremne2] , " +
              " [KontrolorSpremne2] , " +
              " CASE WHEN [DokumentacijaSpremne] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [DokumentacijaSpremne] , " +
              "  [ProdajaKorisnik], [DatumDokumentSpremne],[NapomenaKontrola]," +
              " CASE WHEN korisnik <> '' THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Spremljeno, " +
              " [DatumKamionIzasao],[Korisnik],[NapomenaIzlaz] FROM [dbo].[StatusNajave] " +
         " where KorinsikPrimio <> '' and ProdajaKorisnik = '' order by ID desc";


            string s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlConnection c = new SqlConnection(s_connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(select, c);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Otpremnica";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Datum isporuke";
            dataGridView1.Columns[2].Width = 120;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Vozilo";
            dataGridView1.Columns[3].Width = 120;


            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Registarski broj vozila";
            dataGridView1.Columns[4].Width = 120;


            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Datum i vreme dolaska";
            dataGridView1.Columns[5].Width = 120;


            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Prevozni klijent";
            dataGridView1.Columns[6].Width = 40;


            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Partner";
            dataGridView1.Columns[7].Width = 120;


            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Napomena najave";
            dataGridView1.Columns[8].Width = 40;


            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Korisnik najavio vozilo";
            dataGridView1.Columns[9].Width = 40;


            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Datum i vreme najave";
            dataGridView1.Columns[10].Width = 80;


            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Gate";
            dataGridView1.Columns[11].Width = 50;


            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Korisnik primio vozilo";
            dataGridView1.Columns[12].Width = 40;


            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Datum i vreme prijema";
            dataGridView1.Columns[13].Width = 120;


            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Napomena prijema";
            dataGridView1.Columns[14].Width = 40;


            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Isporuka paletirana 1";
            dataGridView1.Columns[15].Width = 40;


            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Datum i vreme zavr paletiranja 1";
            dataGridView1.Columns[16].Width = 40;


            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Paletirao kontrolor 1";
            dataGridView1.Columns[17].Width = 40;


            DataGridViewColumn column19 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "Isporuka utovarena 2";
            dataGridView1.Columns[18].Width = 40;


            DataGridViewColumn column20 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "Datum i vreme zavr utovara 2";
            dataGridView1.Columns[19].Width = 40;


            DataGridViewColumn column21 = dataGridView1.Columns[20];
            dataGridView1.Columns[20].HeaderText = "Utovario kontrolor 2";
            dataGridView1.Columns[20].Width = 40;


            DataGridViewColumn column22 = dataGridView1.Columns[21];
            dataGridView1.Columns[21].HeaderText = "Dokumentacija spremna";
            dataGridView1.Columns[21].Width = 40;



            DataGridViewColumn column23 = dataGridView1.Columns[22];
            dataGridView1.Columns[22].HeaderText = "Dokumentaciju spremio i izdao";
            dataGridView1.Columns[22].Width = 120;


            DataGridViewColumn column24 = dataGridView1.Columns[23];
            dataGridView1.Columns[23].HeaderText = "Datum i vreme izdavanja dokumentacije";
            dataGridView1.Columns[23].Width = 40;

            dataGridView1.Columns[24].HeaderText = "Napomena Kontrolor";
            dataGridView1.Columns[24].Width = 40;


            DataGridViewColumn column25 = dataGridView1.Columns[24];
            dataGridView1.Columns[25].HeaderText = "Vozilo izašlo";
            dataGridView1.Columns[25].Width = 40;


            DataGridViewColumn column26 = dataGridView1.Columns[25];
            dataGridView1.Columns[26].HeaderText = "Datum i vreme izlaska vozila";
            dataGridView1.Columns[26].Width = 120;


            DataGridViewColumn column27 = dataGridView1.Columns[26];
            dataGridView1.Columns[27].HeaderText = "Korisnik propustio vozilo";
            dataGridView1.Columns[27].Width = 120;

            dataGridView1.Columns[28].HeaderText = "Napomena Izlaz";
            dataGridView1.Columns[28].Width = 40;

            ObojDatagrid();

            //RefreshDataGrid();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int tmpKontrolor1 = 0;
            spremniZaIzlazakToolStripMenuItem.BackColor = default;


            if (chkPaleta1.Checked == true)
            {
                tmpKontrolor1 = 1;
            }
            else
            {
                tmpKontrolor1 = 0;
            }


            InsertStatusNajave upd = new InsertStatusNajave();
            upd.UpdStatusNajave(Convert.ToInt32(txtSifra.Text), txtRegistarskiBroj.Text, txtPrevozniKlijent.Text, txtPartner.Text, Convert.ToDateTime(dtpDatumVremeDolaska.Value), txtNapomena.Text, txtUtovarBroj.Text, txtKorisnikOtvorio.Text, Convert.ToDateTime(dtpVremeOtvaranja.Value), txtGate.Text, Convert.ToDateTime(dtpDatumVremePrijem.Value), txtkorisnikPrimio.Text, txtNapomenaPrijem.Text, tmpKontrolor1, Convert.ToDateTime(dtpDatum1.Value), 1, DateTime.Now, txtKontrolor1.Text, UlogovaniKorisnik, 0, "", Convert.ToDateTime(dtpVreme.Value), Convert.ToDateTime(Convert.ToDateTime(dtpVreme.Value)), "", cboDatumUtovara.Text, cboVozila.Text, txtNapomenaKontrola.Text, txtNapomenaIzlaz.Text);
            InsertLogNajave inslog = new InsertLogNajave();
            inslog.InsLogNajave(Convert.ToInt32(txtSifra.Text), "Kontrolor 2 promena", UlogovaniKorisnik, DateTime.Now);



            chkPaleta2.Checked = true;

            string select = "  SELECT [ID] ,[UtovarBroj] , DatumNajave,  Vozilo,[RegistarskiBroj] ,[DatumVremeDolaska],[PrevoznikKlijent] ,[Partner] ,[Napomena],  " +
              " [KorisnikOtvorio], [DatumVremeOtvaranja], [Gate], [KorinsikPrimio], [DatumVremePrijema],  [NapomenaPrijema] , " +
              " CASE WHEN [PaletaSpremne1] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [PaletaSpremne1] , [DatumSpremne1] , [KontrolorSpremne1],  " +
              " CASE WHEN [PaletaSpremne2] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [PaletaSpremne2] , [DatumSpremne2] , " +
              " [KontrolorSpremne2] , " +
              " CASE WHEN [DokumentacijaSpremne] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [DokumentacijaSpremne] , " +
              "  [ProdajaKorisnik], [DatumDokumentSpremne],[NapomenaKontrola]," +
              " CASE WHEN korisnik <> '' THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Spremljeno, " +
              " [DatumKamionIzasao],[Korisnik],[NapomenaIzlaz] FROM [dbo].[StatusNajave] " +
         " where KorinsikPrimio <> '' and ProdajaKorisnik = '' order by ID desc";


            string s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlConnection c = new SqlConnection(s_connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(select, c);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Otpremnica";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Datum isporuke";
            dataGridView1.Columns[2].Width = 120;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Vozilo";
            dataGridView1.Columns[3].Width = 120;


            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Registarski broj vozila";
            dataGridView1.Columns[4].Width = 120;


            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Datum i vreme dolaska";
            dataGridView1.Columns[5].Width = 120;


            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Prevozni klijent";
            dataGridView1.Columns[6].Width = 40;


            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Partner";
            dataGridView1.Columns[7].Width = 120;


            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Napomena najave";
            dataGridView1.Columns[8].Width = 40;


            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Korisnik najavio vozilo";
            dataGridView1.Columns[9].Width = 40;


            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Datum i vreme najave";
            dataGridView1.Columns[10].Width = 80;


            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Gate";
            dataGridView1.Columns[11].Width = 50;


            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Korisnik primio vozilo";
            dataGridView1.Columns[12].Width = 40;


            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Datum i vreme prijema";
            dataGridView1.Columns[13].Width = 120;


            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Napomena prijema";
            dataGridView1.Columns[14].Width = 40;


            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Isporuka paletirana 1";
            dataGridView1.Columns[15].Width = 40;


            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Datum i vreme zavr paletiranja 1";
            dataGridView1.Columns[16].Width = 40;


            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Paletirao kontrolor 1";
            dataGridView1.Columns[17].Width = 40;


            DataGridViewColumn column19 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "Isporuka utovarena 2";
            dataGridView1.Columns[18].Width = 40;


            DataGridViewColumn column20 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "Datum i vreme zavr utovara 2";
            dataGridView1.Columns[19].Width = 40;


            DataGridViewColumn column21 = dataGridView1.Columns[20];
            dataGridView1.Columns[20].HeaderText = "Utovario kontrolor 2";
            dataGridView1.Columns[20].Width = 40;


            DataGridViewColumn column22 = dataGridView1.Columns[21];
            dataGridView1.Columns[21].HeaderText = "Dokumentacija spremna";
            dataGridView1.Columns[21].Width = 40;



            DataGridViewColumn column23 = dataGridView1.Columns[22];
            dataGridView1.Columns[22].HeaderText = "Dokumentaciju spremio i izdao";
            dataGridView1.Columns[22].Width = 120;


            DataGridViewColumn column24 = dataGridView1.Columns[23];
            dataGridView1.Columns[23].HeaderText = "Datum i vreme izdavanja dokumentacije";
            dataGridView1.Columns[23].Width = 40;

            dataGridView1.Columns[24].HeaderText = "Napomena Kontrolor";
            dataGridView1.Columns[24].Width = 40;


            DataGridViewColumn column25 = dataGridView1.Columns[24];
            dataGridView1.Columns[25].HeaderText = "Vozilo izašlo";
            dataGridView1.Columns[25].Width = 40;


            DataGridViewColumn column26 = dataGridView1.Columns[25];
            dataGridView1.Columns[26].HeaderText = "Datum i vreme izlaska vozila";
            dataGridView1.Columns[26].Width = 120;


            DataGridViewColumn column27 = dataGridView1.Columns[26];
            dataGridView1.Columns[27].HeaderText = "Korisnik propustio vozilo";
            dataGridView1.Columns[27].Width = 120;

            dataGridView1.Columns[28].HeaderText = "Napomena Izlaz";
            dataGridView1.Columns[28].Width = 40;

            ObojDatagrid();

            //RefreshDataGrid();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (chkPaleta1.Checked && chkPaleta2.Checked && chkDokumentacijaSpremna.Checked && txtKontrolor1 != null && txtKontrolor2 != null)
            {
                int tmpKontrolor1 = 0;
                int tmpKontrolor2 = 0;
                spremniZaIzlazakToolStripMenuItem.BackColor = default;

                if (chkPaleta1.Checked == true)
                {
                    tmpKontrolor1 = 1;
                }
                else
                {
                    tmpKontrolor1 = 0;
                }
                if (chkPaleta2.Checked == true)
                {
                    tmpKontrolor2 = 1;
                }
                else
                {
                    tmpKontrolor2 = 0;
                }
                InsertStatusNajave upd = new InsertStatusNajave();
                upd.UpdStatusNajave(Convert.ToInt32(txtSifra.Text), txtRegistarskiBroj.Text, txtPrevozniKlijent.Text, txtPartner.Text, Convert.ToDateTime(dtpDatumVremeDolaska.Value), txtNapomena.Text, txtUtovarBroj.Text, txtKorisnikOtvorio.Text, Convert.ToDateTime(dtpVremeOtvaranja.Value), txtGate.Text, Convert.ToDateTime(dtpDatumVremePrijem.Value), txtkorisnikPrimio.Text, txtNapomenaPrijem.Text, tmpKontrolor1, Convert.ToDateTime(dtpDatum1.Value), tmpKontrolor2, Convert.ToDateTime(dtpDatum2.Value), txtKontrolor1.Text, txtKontrolor2.Text, 1, UlogovaniKorisnik, DateTime.Now, Convert.ToDateTime(Convert.ToDateTime(dtpVreme.Value)), "", cboDatumUtovara.Text, cboVozila.Text, txtNapomenaKontrola.Text, txtNapomenaIzlaz.Text);
                InsertLogNajave inslog = new InsertLogNajave();
                inslog.InsLogNajave(Convert.ToInt32(txtSifra.Text), "Dokumentacija spremna promena", UlogovaniKorisnik, DateTime.Now);


                //RefreshDataGrid();
                chkDokumentacijaSpremna.Checked = true;

                string select = "  SELECT [ID] ,[UtovarBroj] , DatumNajave,  Vozilo,[RegistarskiBroj] ,[DatumVremeDolaska],[PrevoznikKlijent] ,[Partner] ,[Napomena],  " +
                  " [KorisnikOtvorio], [DatumVremeOtvaranja], [Gate], [KorinsikPrimio], [DatumVremePrijema],  [NapomenaPrijema] , " +
                  " CASE WHEN [PaletaSpremne1] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [PaletaSpremne1] , [DatumSpremne1] , [KontrolorSpremne1],  " +
                  " CASE WHEN [PaletaSpremne2] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [PaletaSpremne2] , [DatumSpremne2] , " +
                  " [KontrolorSpremne2] , " +
                  " CASE WHEN [DokumentacijaSpremne] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [DokumentacijaSpremne] , " +
                  "  [ProdajaKorisnik], [DatumDokumentSpremne],[NapomenaKontrola]," +
                  " CASE WHEN korisnik <> '' THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Spremljeno, " +
                  " [DatumKamionIzasao],[Korisnik],[NapomenaIzlaz] FROM [dbo].[StatusNajave] " +
             " where KorinsikPrimio <> '' and ProdajaKorisnik = '' order by ID desc";


                string s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
                SqlConnection myConnection = new SqlConnection(s_connection);
                SqlConnection c = new SqlConnection(s_connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(select, c);

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = ds.Tables[0];

                DataGridViewColumn column = dataGridView1.Columns[0];
                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[0].Width = 40;

                DataGridViewColumn column2 = dataGridView1.Columns[1];
                dataGridView1.Columns[1].HeaderText = "Otpremnica";
                dataGridView1.Columns[1].Width = 50;

                DataGridViewColumn column3 = dataGridView1.Columns[2];
                dataGridView1.Columns[2].HeaderText = "Datum isporuke";
                dataGridView1.Columns[2].Width = 120;

                DataGridViewColumn column4 = dataGridView1.Columns[3];
                dataGridView1.Columns[3].HeaderText = "Vozilo";
                dataGridView1.Columns[3].Width = 120;


                DataGridViewColumn column5 = dataGridView1.Columns[4];
                dataGridView1.Columns[4].HeaderText = "Registarski broj vozila";
                dataGridView1.Columns[4].Width = 120;


                DataGridViewColumn column6 = dataGridView1.Columns[5];
                dataGridView1.Columns[5].HeaderText = "Datum i vreme dolaska";
                dataGridView1.Columns[5].Width = 120;


                DataGridViewColumn column7 = dataGridView1.Columns[6];
                dataGridView1.Columns[6].HeaderText = "Prevozni klijent";
                dataGridView1.Columns[6].Width = 40;


                DataGridViewColumn column8 = dataGridView1.Columns[7];
                dataGridView1.Columns[7].HeaderText = "Partner";
                dataGridView1.Columns[7].Width = 120;


                DataGridViewColumn column9 = dataGridView1.Columns[8];
                dataGridView1.Columns[8].HeaderText = "Napomena najave";
                dataGridView1.Columns[8].Width = 40;


                DataGridViewColumn column10 = dataGridView1.Columns[9];
                dataGridView1.Columns[9].HeaderText = "Korisnik najavio vozilo";
                dataGridView1.Columns[9].Width = 40;


                DataGridViewColumn column11 = dataGridView1.Columns[10];
                dataGridView1.Columns[10].HeaderText = "Datum i vreme najave";
                dataGridView1.Columns[10].Width = 80;


                DataGridViewColumn column12 = dataGridView1.Columns[11];
                dataGridView1.Columns[11].HeaderText = "Gate";
                dataGridView1.Columns[11].Width = 50;


                DataGridViewColumn column13 = dataGridView1.Columns[12];
                dataGridView1.Columns[12].HeaderText = "Korisnik primio vozilo";
                dataGridView1.Columns[12].Width = 40;


                DataGridViewColumn column14 = dataGridView1.Columns[13];
                dataGridView1.Columns[13].HeaderText = "Datum i vreme prijema";
                dataGridView1.Columns[13].Width = 120;


                DataGridViewColumn column15 = dataGridView1.Columns[14];
                dataGridView1.Columns[14].HeaderText = "Napomena prijema";
                dataGridView1.Columns[14].Width = 40;


                DataGridViewColumn column16 = dataGridView1.Columns[15];
                dataGridView1.Columns[15].HeaderText = "Isporuka paletirana 1";
                dataGridView1.Columns[15].Width = 40;


                DataGridViewColumn column17 = dataGridView1.Columns[16];
                dataGridView1.Columns[16].HeaderText = "Datum i vreme zavr paletiranja 1";
                dataGridView1.Columns[16].Width = 40;


                DataGridViewColumn column18 = dataGridView1.Columns[17];
                dataGridView1.Columns[17].HeaderText = "Paletirao kontrolor 1";
                dataGridView1.Columns[17].Width = 40;


                DataGridViewColumn column19 = dataGridView1.Columns[18];
                dataGridView1.Columns[18].HeaderText = "Isporuka utovarena 2";
                dataGridView1.Columns[18].Width = 40;


                DataGridViewColumn column20 = dataGridView1.Columns[19];
                dataGridView1.Columns[19].HeaderText = "Datum i vreme zavr utovara 2";
                dataGridView1.Columns[19].Width = 40;


                DataGridViewColumn column21 = dataGridView1.Columns[20];
                dataGridView1.Columns[20].HeaderText = "Utovario kontrolor 2";
                dataGridView1.Columns[20].Width = 40;


                DataGridViewColumn column22 = dataGridView1.Columns[21];
                dataGridView1.Columns[21].HeaderText = "Dokumentacija spremna";
                dataGridView1.Columns[21].Width = 40;



                DataGridViewColumn column23 = dataGridView1.Columns[22];
                dataGridView1.Columns[22].HeaderText = "Dokumentaciju spremio i izdao";
                dataGridView1.Columns[22].Width = 120;


                DataGridViewColumn column24 = dataGridView1.Columns[23];
                dataGridView1.Columns[23].HeaderText = "Datum i vreme izdavanja dokumentacije";
                dataGridView1.Columns[23].Width = 40;

                dataGridView1.Columns[24].HeaderText = "Napomena Kontrolor";
                dataGridView1.Columns[24].Width = 40;


                DataGridViewColumn column25 = dataGridView1.Columns[24];
                dataGridView1.Columns[25].HeaderText = "Vozilo izašlo";
                dataGridView1.Columns[25].Width = 40;


                DataGridViewColumn column26 = dataGridView1.Columns[25];
                dataGridView1.Columns[26].HeaderText = "Datum i vreme izlaska vozila";
                dataGridView1.Columns[26].Width = 120;


                DataGridViewColumn column27 = dataGridView1.Columns[26];
                dataGridView1.Columns[27].HeaderText = "Korisnik propustio vozilo";
                dataGridView1.Columns[27].Width = 120;

                dataGridView1.Columns[28].HeaderText = "Napomena Izlaz";
                dataGridView1.Columns[28].Width = 40;

                ObojDatagrid();
            }
            else
            {
                MessageBox.Show("Da bi vozilo bilo spremno kontrolori moraju čekirati palete i spremiti dokumentaciju");
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            InsertStatusNajave upd = new InsertStatusNajave();
            int tmpCekiranaDokumentacija = 0;
            int tmpKontrolor1 = 0;
            int tmpKontrolor2 = 0;
            if (chkDokumentacijaSpremna.Checked == true)
            {
                tmpCekiranaDokumentacija = 1;
            }
            else
            {
                tmpCekiranaDokumentacija = 0;
            }
            if (chkPaleta1.Checked == true)
            {
                tmpKontrolor1 = 1;
            }
            else
            {
                tmpKontrolor1 = 0;
            }
            if (chkPaleta2.Checked == true)
            {
                tmpKontrolor2 = 1;
            }
            else
            {
                tmpKontrolor2 = 0;
            }
            upd.UpdStatusNajave(Convert.ToInt32(txtSifra.Text), txtRegistarskiBroj.Text, txtPrevozniKlijent.Text, txtPartner.Text, Convert.ToDateTime(dtpDatumVremeDolaska.Value), txtNapomena.Text, txtUtovarBroj.Text, txtKorisnikOtvorio.Text, Convert.ToDateTime(dtpVremeOtvaranja.Value), txtGate.Text, Convert.ToDateTime(dtpDatumVremePrijem.Value), txtkorisnikPrimio.Text, txtNapomenaPrijem.Text, tmpKontrolor1, Convert.ToDateTime(dtpDatum1.Value), tmpKontrolor2, Convert.ToDateTime(dtpDatum2.Value), txtKontrolor1.Text, txtKontrolor2.Text, tmpCekiranaDokumentacija, txtProdajaKorisnik.Text, Convert.ToDateTime(dtpDokumentacijaSpremna.Value), DateTime.Now, UlogovaniKorisnik, cboDatumUtovara.Text, cboVozila.Text, txtNapomenaKontrola.Text, txtNapomenaIzlaz.Text);
            InsertLogNajave inslog = new InsertLogNajave();
            inslog.InsLogNajave(Convert.ToInt32(txtSifra.Text), "Zavrseno promena", UlogovaniKorisnik, DateTime.Now);
            upd.UpdRkShippingZavrseno(cboDatumUtovara.Text, cboVozila.Text);
            izašliToolStripMenuItem.BackColor = default;

            string select = "  SELECT [ID] ,[UtovarBroj] , DatumNajave,  Vozilo,[RegistarskiBroj] ,[DatumVremeDolaska],[PrevoznikKlijent] ,[Partner] ,[Napomena],  " +
              " [KorisnikOtvorio], [DatumVremeOtvaranja], [Gate], [KorinsikPrimio], [DatumVremePrijema],  [NapomenaPrijema] , " +
              " CASE WHEN [PaletaSpremne1] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [PaletaSpremne1] , [DatumSpremne1] , [KontrolorSpremne1],  " +
              " CASE WHEN [PaletaSpremne2] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [PaletaSpremne2] , [DatumSpremne2] , " +
              " [KontrolorSpremne2] , " +
              " CASE WHEN [DokumentacijaSpremne] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [DokumentacijaSpremne] , " +
              "  [ProdajaKorisnik], [DatumDokumentSpremne],[NapomenaKontrola]," +
              " CASE WHEN korisnik <> '' THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Spremljeno, " +
              " [DatumKamionIzasao],[Korisnik],[NapomenaIzlaz] FROM [dbo].[StatusNajave] " +
    " where Korisnik = '' and ProdajaKorisnik <> ''  order by ID desc";


            string s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlConnection c = new SqlConnection(s_connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(select, c);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Otpremnica";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Datum isporuke";
            dataGridView1.Columns[2].Width = 120;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Vozilo";
            dataGridView1.Columns[3].Width = 120;


            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Registarski broj vozila";
            dataGridView1.Columns[4].Width = 120;


            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Datum i vreme dolaska";
            dataGridView1.Columns[5].Width = 120;


            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Prevozni klijent";
            dataGridView1.Columns[6].Width = 40;


            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Partner";
            dataGridView1.Columns[7].Width = 120;


            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Napomena najave";
            dataGridView1.Columns[8].Width = 40;


            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Korisnik najavio vozilo";
            dataGridView1.Columns[9].Width = 40;


            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Datum i vreme najave";
            dataGridView1.Columns[10].Width = 80;


            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Gate";
            dataGridView1.Columns[11].Width = 50;


            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Korisnik primio vozilo";
            dataGridView1.Columns[12].Width = 40;


            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Datum i vreme prijema";
            dataGridView1.Columns[13].Width = 120;


            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Napomena prijema";
            dataGridView1.Columns[14].Width = 40;


            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Isporuka paletirana 1";
            dataGridView1.Columns[15].Width = 40;


            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Datum i vreme zavr paletiranja 1";
            dataGridView1.Columns[16].Width = 40;


            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Paletirao kontrolor 1";
            dataGridView1.Columns[17].Width = 40;


            DataGridViewColumn column19 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "Isporuka utovarena 2";
            dataGridView1.Columns[18].Width = 40;


            DataGridViewColumn column20 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "Datum i vreme zavr utovara 2";
            dataGridView1.Columns[19].Width = 40;


            DataGridViewColumn column21 = dataGridView1.Columns[20];
            dataGridView1.Columns[20].HeaderText = "Utovario kontrolor 2";
            dataGridView1.Columns[20].Width = 40;


            DataGridViewColumn column22 = dataGridView1.Columns[21];
            dataGridView1.Columns[21].HeaderText = "Dokumentacija spremna";
            dataGridView1.Columns[21].Width = 40;



            DataGridViewColumn column23 = dataGridView1.Columns[22];
            dataGridView1.Columns[22].HeaderText = "Dokumentaciju spremio i izdao";
            dataGridView1.Columns[22].Width = 120;


            DataGridViewColumn column24 = dataGridView1.Columns[23];
            dataGridView1.Columns[23].HeaderText = "Datum i vreme izdavanja dokumentacije";
            dataGridView1.Columns[23].Width = 40;

            dataGridView1.Columns[24].HeaderText = "Napomena Kontrolor";
            dataGridView1.Columns[24].Width = 40;


            DataGridViewColumn column25 = dataGridView1.Columns[24];
            dataGridView1.Columns[25].HeaderText = "Vozilo izašlo";
            dataGridView1.Columns[25].Width = 40;


            DataGridViewColumn column26 = dataGridView1.Columns[25];
            dataGridView1.Columns[26].HeaderText = "Datum i vreme izlaska vozila";
            dataGridView1.Columns[26].Width = 120;


            DataGridViewColumn column27 = dataGridView1.Columns[26];
            dataGridView1.Columns[27].HeaderText = "Korisnik propustio vozilo";
            dataGridView1.Columns[27].Width = 120;

            dataGridView1.Columns[28].HeaderText = "Napomena Izlaz";
            dataGridView1.Columns[28].Width = 40;
            ObojDatagrid();
            //RefreshDataGrid();
        }

        private void logFajlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noviUnosToolStripMenuItem.BackColor = default;
            logFajlToolStripMenuItem.BackColor = Color.Orange;
            najavljeniToolStripMenuItem.BackColor = default;
            primljeniToolStripMenuItem.BackColor = default;
            spremniZaIzlazakToolStripMenuItem.BackColor = default;
            izašliToolStripMenuItem.BackColor = default;
            posaljiMailPesicuToolStripMenuItem.BackColor = default;
            StatusNajaveLog snl = new StatusNajaveLog(Convert.ToInt32(txtSifra.Text));
            snl.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void statusNajavePregledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatusNajavePregled snp = new StatusNajavePregled();
            snp.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ZAMENA
            //Select Distinct Vozilo From RkShipping where Stanje = 1
            string select = " Select Distinct RTrim(Vozilo) as Vozilo From RkShippingPocetna where Najavljen <> 1 and  DatumUtovara = '" + cboDatumUtovara.Text + "'" +
                " union " +
               " Select Distinct RTrim(Vozilo) as Vozilo From RkShipping where Najavljen<>1 and DatumUtovara = '" + cboDatumUtovara.Text + "'" +
                " union " +
                " Select Distinct RTrim(Vozilo) as Vozilo From RkShippingZavrseno where Izaslo<> 1 and Najavljen<>1 and  DatumUtovara = '" + cboDatumUtovara.Text + "'";
            string s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlConnection c = new SqlConnection(s_connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(select, c);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            cboVozila.DataSource = ds.Tables[0];
            cboVozila.DisplayMember = "Vozilo";
            cboVozila.ValueMember = "Vozilo";
        }


        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void najavljeniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            button5.Enabled = true;

            noviUnosToolStripMenuItem.BackColor = default;
            logFajlToolStripMenuItem.BackColor = default;
            najavljeniToolStripMenuItem.BackColor = Color.Orange;
            primljeniToolStripMenuItem.BackColor = default;
            spremniZaIzlazakToolStripMenuItem.BackColor = default;
            izašliToolStripMenuItem.BackColor = default;
            posaljiMailPesicuToolStripMenuItem.BackColor = default;

            string select = "  SELECT [ID] ,[UtovarBroj] , DatumNajave,  Vozilo,[RegistarskiBroj] ,[DatumVremeDolaska],[PrevoznikKlijent] ,[Partner] ,[Napomena],  " +
                " [KorisnikOtvorio], [DatumVremeOtvaranja], [Gate], [KorinsikPrimio], [DatumVremePrijema],  [NapomenaPrijema] , " +
                " CASE WHEN [PaletaSpremne1] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [PaletaSpremne1] , [DatumSpremne1] , [KontrolorSpremne1],  " +
                " CASE WHEN [PaletaSpremne2] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [PaletaSpremne2] , [DatumSpremne2] , " +
                " [KontrolorSpremne2] , " +
                " CASE WHEN [DokumentacijaSpremne] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [DokumentacijaSpremne] , " +
                "  [ProdajaKorisnik], [DatumDokumentSpremne],[NapomenaKontrola]," +
                " CASE WHEN korisnik <> '' THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Spremljeno, " +
                " [DatumKamionIzasao],[Korisnik],[NapomenaIzlaz] FROM [dbo].[StatusNajave] " +
         " where KorinsikPrimio = '' order by ID desc";


            string s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlConnection c = new SqlConnection(s_connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(select, c);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Otpremnica";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Datum isporuke";
            dataGridView1.Columns[2].Width = 120;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Vozilo";
            dataGridView1.Columns[3].Width = 120;


            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Registarski broj vozila";
            dataGridView1.Columns[4].Width = 120;


            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Datum i vreme dolaska";
            dataGridView1.Columns[5].Width = 120;


            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Prevozni klijent";
            dataGridView1.Columns[6].Width = 40;


            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Partner";
            dataGridView1.Columns[7].Width = 120;


            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Napomena najave";
            dataGridView1.Columns[8].Width = 40;


            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Korisnik najavio vozilo";
            dataGridView1.Columns[9].Width = 40;


            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Datum i vreme najave";
            dataGridView1.Columns[10].Width = 80;


            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Gate";
            dataGridView1.Columns[11].Width = 50;


            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Korisnik primio vozilo";
            dataGridView1.Columns[12].Width = 40;


            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Datum i vreme prijema";
            dataGridView1.Columns[13].Width = 120;


            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Napomena prijema";
            dataGridView1.Columns[14].Width = 40;


            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Isporuka paletirana 1";
            dataGridView1.Columns[15].Width = 40;


            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Datum i vreme zavr paletiranja 1";
            dataGridView1.Columns[16].Width = 40;


            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Paletirao kontrolor 1";
            dataGridView1.Columns[17].Width = 40;


            DataGridViewColumn column19 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "Isporuka utovarena 2";
            dataGridView1.Columns[18].Width = 40;


            DataGridViewColumn column20 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "Datum i vreme zavr utovara 2";
            dataGridView1.Columns[19].Width = 40;


            DataGridViewColumn column21 = dataGridView1.Columns[20];
            dataGridView1.Columns[20].HeaderText = "Utovario kontrolor 2";
            dataGridView1.Columns[20].Width = 40;


            DataGridViewColumn column22 = dataGridView1.Columns[21];
            dataGridView1.Columns[21].HeaderText = "Dokumentacija spremna";
            dataGridView1.Columns[21].Width = 40;



            DataGridViewColumn column23 = dataGridView1.Columns[22];
            dataGridView1.Columns[22].HeaderText = "Dokumentaciju spremio i izdao";
            dataGridView1.Columns[22].Width = 120;


            DataGridViewColumn column24 = dataGridView1.Columns[23];
            dataGridView1.Columns[23].HeaderText = "Datum i vreme izdavanja dokumentacije";
            dataGridView1.Columns[23].Width = 40;

            dataGridView1.Columns[24].HeaderText = "Napomena Kontrolor";
            dataGridView1.Columns[24].Width = 40;


            DataGridViewColumn column25 = dataGridView1.Columns[24];
            dataGridView1.Columns[25].HeaderText = "Vozilo izašlo";
            dataGridView1.Columns[25].Width = 40;


            DataGridViewColumn column26 = dataGridView1.Columns[25];
            dataGridView1.Columns[26].HeaderText = "Datum i vreme izlaska vozila";
            dataGridView1.Columns[26].Width = 120;


            DataGridViewColumn column27 = dataGridView1.Columns[26];
            dataGridView1.Columns[27].HeaderText = "Korisnik propustio vozilo";
            dataGridView1.Columns[27].Width = 120;

            dataGridView1.Columns[28].HeaderText = "Napomena Izlaz";
            dataGridView1.Columns[28].Width = 40;

            ObojDatagrid();
        }

        private void primljeniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button5.Enabled = false;
            button9.Enabled = false;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;

            noviUnosToolStripMenuItem.BackColor = default;
            logFajlToolStripMenuItem.BackColor = default;
            najavljeniToolStripMenuItem.BackColor = default;
            primljeniToolStripMenuItem.BackColor = Color.Orange;
            spremniZaIzlazakToolStripMenuItem.BackColor = default;
            izašliToolStripMenuItem.BackColor = default;
            posaljiMailPesicuToolStripMenuItem.BackColor = default;

            string select = "  SELECT [ID] ,[UtovarBroj] , DatumNajave,  Vozilo,[RegistarskiBroj] ,[DatumVremeDolaska],[PrevoznikKlijent] ,[Partner] ,[Napomena],  " +
              " [KorisnikOtvorio], [DatumVremeOtvaranja], [Gate], [KorinsikPrimio], [DatumVremePrijema],  [NapomenaPrijema] , " +
              " CASE WHEN [PaletaSpremne1] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [PaletaSpremne1] , [DatumSpremne1] , [KontrolorSpremne1],  " +
              " CASE WHEN [PaletaSpremne2] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [PaletaSpremne2] , [DatumSpremne2] , " +
              " [KontrolorSpremne2] , " +
              " CASE WHEN [DokumentacijaSpremne] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [DokumentacijaSpremne] , " +
              "  [ProdajaKorisnik], [DatumDokumentSpremne],[NapomenaKontrola]," +
              " CASE WHEN korisnik <> '' THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Spremljeno, " +
              " [DatumKamionIzasao],[Korisnik],[NapomenaIzlaz] FROM [dbo].[StatusNajave] " +
         " where KorinsikPrimio <> '' and ProdajaKorisnik = '' order by ID desc";


            string s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlConnection c = new SqlConnection(s_connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(select, c);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Otpremnica";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Datum isporuke";
            dataGridView1.Columns[2].Width = 120;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Vozilo";
            dataGridView1.Columns[3].Width = 120;


            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Registarski broj vozila";
            dataGridView1.Columns[4].Width = 120;


            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Datum i vreme dolaska";
            dataGridView1.Columns[5].Width = 120;


            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Prevozni klijent";
            dataGridView1.Columns[6].Width = 40;


            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Partner";
            dataGridView1.Columns[7].Width = 120;


            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Napomena najave";
            dataGridView1.Columns[8].Width = 40;


            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Korisnik najavio vozilo";
            dataGridView1.Columns[9].Width = 40;


            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Datum i vreme najave";
            dataGridView1.Columns[10].Width = 80;


            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Gate";
            dataGridView1.Columns[11].Width = 50;


            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Korisnik primio vozilo";
            dataGridView1.Columns[12].Width = 40;


            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Datum i vreme prijema";
            dataGridView1.Columns[13].Width = 120;


            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Napomena prijema";
            dataGridView1.Columns[14].Width = 40;


            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Isporuka paletirana 1";
            dataGridView1.Columns[15].Width = 40;


            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Datum i vreme zavr paletiranja 1";
            dataGridView1.Columns[16].Width = 40;


            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Paletirao kontrolor 1";
            dataGridView1.Columns[17].Width = 40;


            DataGridViewColumn column19 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "Isporuka utovarena 2";
            dataGridView1.Columns[18].Width = 40;


            DataGridViewColumn column20 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "Datum i vreme zavr utovara 2";
            dataGridView1.Columns[19].Width = 40;


            DataGridViewColumn column21 = dataGridView1.Columns[20];
            dataGridView1.Columns[20].HeaderText = "Utovario kontrolor 2";
            dataGridView1.Columns[20].Width = 40;


            DataGridViewColumn column22 = dataGridView1.Columns[21];
            dataGridView1.Columns[21].HeaderText = "Dokumentacija spremna";
            dataGridView1.Columns[21].Width = 40;



            DataGridViewColumn column23 = dataGridView1.Columns[22];
            dataGridView1.Columns[22].HeaderText = "Dokumentaciju spremio i izdao";
            dataGridView1.Columns[22].Width = 120;


            DataGridViewColumn column24 = dataGridView1.Columns[23];
            dataGridView1.Columns[23].HeaderText = "Datum i vreme izdavanja dokumentacije";
            dataGridView1.Columns[23].Width = 40;

            dataGridView1.Columns[24].HeaderText = "Napomena Kontrolor";
            dataGridView1.Columns[24].Width = 40;


            DataGridViewColumn column25 = dataGridView1.Columns[24];
            dataGridView1.Columns[25].HeaderText = "Vozilo izašlo";
            dataGridView1.Columns[25].Width = 40;


            DataGridViewColumn column26 = dataGridView1.Columns[25];
            dataGridView1.Columns[26].HeaderText = "Datum i vreme izlaska vozila";
            dataGridView1.Columns[26].Width = 120;


            DataGridViewColumn column27 = dataGridView1.Columns[26];
            dataGridView1.Columns[27].HeaderText = "Korisnik propustio vozilo";
            dataGridView1.Columns[27].Width = 120;

            dataGridView1.Columns[28].HeaderText = "Napomena Izlaz";
            dataGridView1.Columns[28].Width = 40;

            ObojDatagrid();

        }

        private void spremniZaIzlazakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button5.Enabled = false;
            button9.Enabled = true;

            noviUnosToolStripMenuItem.BackColor = default;
            logFajlToolStripMenuItem.BackColor = default;
            najavljeniToolStripMenuItem.BackColor = default;
            primljeniToolStripMenuItem.BackColor = default;
            spremniZaIzlazakToolStripMenuItem.BackColor = Color.Orange;
            izašliToolStripMenuItem.BackColor = default;
            posaljiMailPesicuToolStripMenuItem.BackColor = default;
            string select = "  SELECT [ID] ,[UtovarBroj] , DatumNajave,  Vozilo,[RegistarskiBroj] ,[DatumVremeDolaska],[PrevoznikKlijent] ,[Partner] ,[Napomena],  " +
               " [KorisnikOtvorio], [DatumVremeOtvaranja], [Gate], [KorinsikPrimio], [DatumVremePrijema],  [NapomenaPrijema] , " +
               " CASE WHEN [PaletaSpremne1] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [PaletaSpremne1] , [DatumSpremne1] , [KontrolorSpremne1],  " +
               " CASE WHEN [PaletaSpremne2] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [PaletaSpremne2] , [DatumSpremne2] , " +
               " [KontrolorSpremne2] , " +
               " CASE WHEN [DokumentacijaSpremne] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [DokumentacijaSpremne] , " +
               "  [ProdajaKorisnik], [DatumDokumentSpremne],[NapomenaKontrola]," +
               " CASE WHEN korisnik <> '' THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Spremljeno, " +
               " [DatumKamionIzasao],[Korisnik],[NapomenaIzlaz] FROM [dbo].[StatusNajave] " +
     " where Korisnik = '' and ProdajaKorisnik <> ''  order by ID desc";


            string s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlConnection c = new SqlConnection(s_connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(select, c);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Otpremnica";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Datum isporuke";
            dataGridView1.Columns[2].Width = 120;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Vozilo";
            dataGridView1.Columns[3].Width = 120;


            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Registarski broj vozila";
            dataGridView1.Columns[4].Width = 120;


            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Datum i vreme dolaska";
            dataGridView1.Columns[5].Width = 120;


            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Prevozni klijent";
            dataGridView1.Columns[6].Width = 40;


            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Partner";
            dataGridView1.Columns[7].Width = 120;


            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Napomena najave";
            dataGridView1.Columns[8].Width = 40;


            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Korisnik najavio vozilo";
            dataGridView1.Columns[9].Width = 40;


            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Datum i vreme najave";
            dataGridView1.Columns[10].Width = 80;


            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Gate";
            dataGridView1.Columns[11].Width = 50;


            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Korisnik primio vozilo";
            dataGridView1.Columns[12].Width = 40;


            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Datum i vreme prijema";
            dataGridView1.Columns[13].Width = 120;


            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Napomena prijema";
            dataGridView1.Columns[14].Width = 40;


            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Isporuka paletirana 1";
            dataGridView1.Columns[15].Width = 40;


            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Datum i vreme zavr paletiranja 1";
            dataGridView1.Columns[16].Width = 40;


            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Paletirao kontrolor 1";
            dataGridView1.Columns[17].Width = 40;


            DataGridViewColumn column19 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "Isporuka utovarena 2";
            dataGridView1.Columns[18].Width = 40;


            DataGridViewColumn column20 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "Datum i vreme zavr utovara 2";
            dataGridView1.Columns[19].Width = 40;


            DataGridViewColumn column21 = dataGridView1.Columns[20];
            dataGridView1.Columns[20].HeaderText = "Utovario kontrolor 2";
            dataGridView1.Columns[20].Width = 40;


            DataGridViewColumn column22 = dataGridView1.Columns[21];
            dataGridView1.Columns[21].HeaderText = "Dokumentacija spremna";
            dataGridView1.Columns[21].Width = 40;



            DataGridViewColumn column23 = dataGridView1.Columns[22];
            dataGridView1.Columns[22].HeaderText = "Dokumentaciju spremio i izdao";
            dataGridView1.Columns[22].Width = 120;


            DataGridViewColumn column24 = dataGridView1.Columns[23];
            dataGridView1.Columns[23].HeaderText = "Datum i vreme izdavanja dokumentacije";
            dataGridView1.Columns[23].Width = 40;

            dataGridView1.Columns[24].HeaderText = "Napomena Kontrolor";
            dataGridView1.Columns[24].Width = 40;


            DataGridViewColumn column25 = dataGridView1.Columns[24];
            dataGridView1.Columns[25].HeaderText = "Vozilo izašlo";
            dataGridView1.Columns[25].Width = 40;


            DataGridViewColumn column26 = dataGridView1.Columns[25];
            dataGridView1.Columns[26].HeaderText = "Datum i vreme izlaska vozila";
            dataGridView1.Columns[26].Width = 120;


            DataGridViewColumn column27 = dataGridView1.Columns[26];
            dataGridView1.Columns[27].HeaderText = "Korisnik propustio vozilo";
            dataGridView1.Columns[27].Width = 120;

            dataGridView1.Columns[28].HeaderText = "Napomena Izlaz";
            dataGridView1.Columns[28].Width = 40;

            ObojDatagrid();

        }

        private void izašliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            button5.Enabled = false;

            noviUnosToolStripMenuItem.BackColor = default;
            logFajlToolStripMenuItem.BackColor = default;
            najavljeniToolStripMenuItem.BackColor = default;
            primljeniToolStripMenuItem.BackColor = default;
            spremniZaIzlazakToolStripMenuItem.BackColor = default;
            izašliToolStripMenuItem.BackColor = Color.Orange;
            posaljiMailPesicuToolStripMenuItem.BackColor = default;
            string select = "  SELECT [ID] ,[UtovarBroj] , DatumNajave,  Vozilo,[RegistarskiBroj] ,[DatumVremeDolaska],[PrevoznikKlijent] ,[Partner] ,[Napomena],  " +
                " [KorisnikOtvorio], [DatumVremeOtvaranja], [Gate], [KorinsikPrimio], [DatumVremePrijema],  [NapomenaPrijema] , " +
                " CASE WHEN [PaletaSpremne1] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [PaletaSpremne1] , [DatumSpremne1] , [KontrolorSpremne1],  " +
                " CASE WHEN [PaletaSpremne2] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [PaletaSpremne2] , [DatumSpremne2] , " +
                " [KontrolorSpremne2] , " +
                " CASE WHEN [DokumentacijaSpremne] > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as [DokumentacijaSpremne] , " +
                "  [ProdajaKorisnik], [DatumDokumentSpremne],[NapomenaKontrola]," +
                " CASE WHEN korisnik <> '' THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Spremljeno, " +
                " [DatumKamionIzasao],[Korisnik],[NapomenaIzlaz] FROM [dbo].[StatusNajave] " +
      " where Korisnik <> '' order by ID desc";


            string s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlConnection c = new SqlConnection(s_connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(select, c);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Otpremnica";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Datum isporuke";
            dataGridView1.Columns[2].Width = 120;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Vozilo";
            dataGridView1.Columns[3].Width = 120;


            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Registarski broj vozila";
            dataGridView1.Columns[4].Width = 120;


            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Datum i vreme dolaska";
            dataGridView1.Columns[5].Width = 120;


            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Prevozni klijent";
            dataGridView1.Columns[6].Width = 40;


            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Partner";
            dataGridView1.Columns[7].Width = 120;


            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Napomena najave";
            dataGridView1.Columns[8].Width = 40;


            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Korisnik najavio vozilo";
            dataGridView1.Columns[9].Width = 40;


            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Datum i vreme najave";
            dataGridView1.Columns[10].Width = 80;


            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Gate";
            dataGridView1.Columns[11].Width = 50;


            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Korisnik primio vozilo";
            dataGridView1.Columns[12].Width = 40;


            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Datum i vreme prijema";
            dataGridView1.Columns[13].Width = 120;


            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Napomena prijema";
            dataGridView1.Columns[14].Width = 40;


            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Isporuka paletirana 1";
            dataGridView1.Columns[15].Width = 40;


            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Datum i vreme zavr paletiranja 1";
            dataGridView1.Columns[16].Width = 40;


            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Paletirao kontrolor 1";
            dataGridView1.Columns[17].Width = 40;


            DataGridViewColumn column19 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "Isporuka utovarena 2";
            dataGridView1.Columns[18].Width = 40;


            DataGridViewColumn column20 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "Datum i vreme zavr utovara 2";
            dataGridView1.Columns[19].Width = 40;


            DataGridViewColumn column21 = dataGridView1.Columns[20];
            dataGridView1.Columns[20].HeaderText = "Utovario kontrolor 2";
            dataGridView1.Columns[20].Width = 40;


            DataGridViewColumn column22 = dataGridView1.Columns[21];
            dataGridView1.Columns[21].HeaderText = "Dokumentacija spremna";
            dataGridView1.Columns[21].Width = 40;



            DataGridViewColumn column23 = dataGridView1.Columns[22];
            dataGridView1.Columns[22].HeaderText = "Dokumentaciju spremio i izdao";
            dataGridView1.Columns[22].Width = 120;


            DataGridViewColumn column24 = dataGridView1.Columns[23];
            dataGridView1.Columns[23].HeaderText = "Datum i vreme izdavanja dokumentacije";
            dataGridView1.Columns[23].Width = 40;

            dataGridView1.Columns[24].HeaderText = "Napomena Kontrolor";
            dataGridView1.Columns[24].Width = 40;


            DataGridViewColumn column25 = dataGridView1.Columns[24];
            dataGridView1.Columns[25].HeaderText = "Vozilo izašlo";
            dataGridView1.Columns[25].Width = 40;


            DataGridViewColumn column26 = dataGridView1.Columns[25];
            dataGridView1.Columns[26].HeaderText = "Datum i vreme izlaska vozila";
            dataGridView1.Columns[26].Width = 120;


            DataGridViewColumn column27 = dataGridView1.Columns[26];
            dataGridView1.Columns[27].HeaderText = "Korisnik propustio vozilo";
            dataGridView1.Columns[27].Width = 120;

            dataGridView1.Columns[28].HeaderText = "Napomena Izlaz";
            dataGridView1.Columns[28].Width = 40;

            ObojDatagrid();

        }

        private void posaljiMailPesicuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noviUnosToolStripMenuItem.BackColor = default;
            logFajlToolStripMenuItem.BackColor = default;
            najavljeniToolStripMenuItem.BackColor = default;
            primljeniToolStripMenuItem.BackColor = default;
            spremniZaIzlazakToolStripMenuItem.BackColor = default;
            izašliToolStripMenuItem.BackColor = default;
            posaljiMailPesicuToolStripMenuItem.BackColor = Color.Orange;

            string zadnjibroj = txtSifra.Text;

            string mail = "panta0307@yahoo.com";
            string body = "";
            mailMessage = new MailMessage("reporter@jela.rs", mail)
            {
                Subject = "Test automatskih mailova"
            };



            body = body + "<br /> <br /> ";
            body = body + "A bre Pešiću jel ovo moguće" + "<br />";
            body = body + "S poštovanjem Petrović Pantelija" + "<br />";
            body = body + "OVO TI JE ZAVRSENO AKO STIZU MAILOVI" + "<br />" + "<br />" + "<br />";

            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient
            {
                Host = "mailsrv.jela.rs",

                Port = 465,
                UseDefaultCredentials = true,

                Credentials = new NetworkCredential("reporter@jela.rs", "repass2021"),

                EnableSsl = true
            };
            smtpClient.Send(mailMessage);

        }

        private void dtpVremeOtvaranja_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpDatumVremeDolaska_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_EnabledChanged(object sender, EventArgs e)
        {
            if (button2.Enabled == false)
            {
                button2.BackColor = Color.DimGray;
            }
            else
            {
                button2.BackColor = Color.Black;

            }
        }

        private void button1_EnabledChanged(object sender, EventArgs e)
        {
            if (button1.Enabled == false)
            {
                button1.BackColor = Color.DimGray;
            }
            else
            {
                button1.BackColor = Color.Black;

            }
        }

        private void button5_EnabledChanged(object sender, EventArgs e)
        {
            if (button5.Enabled == false)
            {
                button5.BackColor = Color.DimGray;
            }
            else
            {
                button5.BackColor = Color.Black;

            }
        }

        private void button6_EnabledChanged(object sender, EventArgs e)
        {
            if (button6.Enabled == false)
            {
                button6.BackColor = Color.DimGray;
            }
            else
            {
                button6.BackColor = Color.Black;

            }
        }

        private void button7_EnabledChanged(object sender, EventArgs e)
        {
            if (button7.Enabled == false)
            {
                button7.BackColor = Color.DimGray;
            }
            else
            {
                button7.BackColor = Color.Black;

            }
        }

        private void button8_EnabledChanged(object sender, EventArgs e)
        {
            if (button8.Enabled == false)
            {
                button8.BackColor = Color.DimGray;
            }
            else
            {
                button8.BackColor = Color.Black;

            }
        }

        private void button9_EnabledChanged(object sender, EventArgs e)
        {
            if (button9.Enabled == false)
            {
                button9.BackColor = Color.DimGray;
            }
            else
            {
                button9.BackColor = Color.Black;
            }
        }
    }


}
