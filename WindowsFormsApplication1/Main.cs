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
    public partial class Main : Form
    {
        int trebaazurirati = 0;
        public Main()
        {
            InitializeComponent();
        }

        public Main(int azuriraj)
        {
            InitializeComponent();
            trebaazurirati = azuriraj;
        }

        private void partneriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Partneri part = new Partneri();
            part.Show();
        }

        private void naloziZaUtovarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 nn = new Form1();
            nn.Show();
        }

        private void reklamacijeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reklamacije rek = new Reklamacije();
            rek.Show();

        }

        private void rkShippingToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void rkShippingWEBSLargoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReferencijalnaTabela rt = new ReferencijalnaTabela();
            rt.Show();
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
            pi.Show();
        }

        private void paketiPakeristiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PregledPaketi pak = new PregledPaketi();
            pak.Show();
        }

        private void pregledPreostaliPaketiPoIsporuciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreostaliPaketi preos = new PreostaliPaketi();
            preos.Show();
        }

        private void pregledKontroloriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PregledKontrolor pk = new PregledKontrolor();
            pk.Show();
        }

        private void wEBSPodaciToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void trVozilaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vozila voz = new Vozila();
            voz.Show();
        }

        private void partneriToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Komitenti part = new Komitenti();
            part.Show();
        }

        private void saloniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Saloni sal = new Saloni();
            sal.Show();
        }

        private void isporukaUvozToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebIsporuka wi = new WebIsporuka();
            wi.Show();
        }

        private void kontrolorObradaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObradaIsporuke oi = new ObradaIsporuke();
            oi.Show();
        }

        private void isporukaUvozPoVoziluToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebIsporukaVozilo wiv = new WebIsporukaVozilo();
            wiv.Show();
        }

        private void istorijaRadaKontroloraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IstorijaRadaKontrolor irk = new IstorijaRadaKontrolor();
            irk.Show();
        }

        private void štampaZaPakeristuToolStripMenuItem_Click(object sender, EventArgs e)
        {
           StampaP sp = new StampaP();
             sp.Show();
        }

        private void poništiKontroloraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateKontrolor upk = new UpdateKontrolor();
            upk.Show();

        }

        private void poništiPakeristuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdatePakerista upp = new UpdatePakerista();
            upp.Show();
        }

        private void istorijaRadaPoKontroloruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IstorijaRadaKontrolor isk = new IstorijaRadaKontrolor();
            isk.Show();
        }

        private void istorijaRadaPoPaletaruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IstorijaRadaPakeriste isp = new IstorijaRadaPakeriste();
            isp.Show();
        }

        private void sumarnoIsporukaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProveraIsporukeSum pis = new ProveraIsporukeSum();
            pis.Show();
        }

        private void završetakIsporukeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZakljucavanjeIsporuke zak = new ZakljucavanjeIsporuke();
            zak.Show();
        }

        private void artikalProveraBarkodaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Artikal art = new Artikal();
            art.Show();
        }

        private void štampaZaPaletuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StampajPal sp = new StampajPal();
            sp.Show();
        }

        private void vToolStripMenuItem_Click(object sender, EventArgs e)
        {
            v123v vs = new v123v();
            vs.Show();
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
            op.Show();
        }

        private void štampajNeisporučenePaketeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StampajNeisporucene sneis = new StampajNeisporucene();
            sneis.Show();
        }

        private void promenaVozilaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PromenaVozila pvoz = new PromenaVozila();
            pvoz.Show();
        }

        private void štampajSvePaletePoVoziluToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StampajPalSve sps = new StampajPalSve();
            sps.Show();
        }

        private void štampajNedostajućePaketePoVoziluToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StampajPalSveP spsp = new StampajPalSveP();
            spsp.Show();
        }

        private void uvozReklamacijaPoVoziluToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebReklamacijeVozilo wrv = new WebReklamacijeVozilo();
            wrv.Show();
        }

        private void štampajPregledPoVremenuPakovanjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StampajPalVremePak spkk = new StampajPalVremePak();
            spkk.Show();
        }

        private void pregledPaletarKontrolorIstorijaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PregledKontrolorIstorija pki = new PregledKontrolorIstorija();
            pki.Show();
        }

        private void štampajReklamacijeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ŠtampajReklamacijeP srp = new ŠtampajReklamacijeP();
            srp.Show();
        }

        private void izbrisiCeluIsporukuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IzbrisiIsporuku izbr = new IzbrisiIsporuku();
            izbr.Show();
        }

        private void otpremnicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOtpremnicaUvoz frmOU = new frmOtpremnicaUvoz();
            frmOU.Show();
        }

        private void pregledKontrolorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pregled2018 prgl2018 = new Pregled2018();
            prgl2018.Show();
        }

        private void porudžbinaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExcelImportPorudzbina frmExcelimp = new frmExcelImportPorudzbina();
            frmExcelimp.Show();
        }

        private void formiranjeTimovaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTimovi nft = new frmTimovi();
            nft.Show();
        }

        private void reklamacijeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmReklamacijaUvoz frek = new frmReklamacijaUvoz();
            frek.Show();
        }

        private void porudzbinaDirektnoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPorudzbinaImport pimp = new frmPorudzbinaImport();
            pimp.Show();
        }

        private void pregledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPrikazStanja prikstan = new frmPrikazStanja();
            prikstan.Show();
        }

        private void pregledPoOtpremniciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOtpremnicaPregled otp = new frmOtpremnicaPregled();
            otp.Show();
        }

        private void pregledPorudžbineLargoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPorudzbinaPregled pregled = new frmPorudzbinaPregled();
            pregled.Show();
        }

        private void arhivirajDoBrojaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 pr = new Form2();
            pr.Show();
        }

        private void pregledPodatakaArhivToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PregledKontrolorIstorijaArhiv pkis = new PregledKontrolorIstorijaArhiv();
            pkis.Show();
        }

        private void obradaReklamacijeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObradaReklamacije obr = new ObradaReklamacije();
            obr.Show();
        }

        private void pozicijeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPozicije poz = new Magacin.frmPozicije();
            poz.Show();
        }

        private void paleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPaleta paleta = new Magacin.frmPaleta();
            paleta.Show();
        }

        private void matičniPodaaciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmMaticniPodatak maticni = new Magacin.frmMaticniPodatak();
            maticni.Show();
        }

        private void međuskladišniPrenosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMedjuskaldisniPrenosi frmM = new frmMedjuskaldisniPrenosi();
            frmM.Show();
        }

        private void formiranjeMedjuskladišnogPrenosaIzZalihaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPrebacivanjeMedjuskladisniPrenos mprp = new frmPrebacivanjeMedjuskladisniPrenos();
            mprp.Show();
        }

        private void štampaEtiketeRegalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmStampaPozicije poz = new Magacin.frmStampaPozicije();
            poz.Show();
        }

        private void formiranjeMedjuskladišnogOperativniPešićToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPlanMedjuskladisni plan = new Magacin.frmPlanMedjuskladisni();
            plan.Show();
        }

        private void direktnaIzdavanjaTrebovanjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPregledIzdavanja izd = new Magacin.frmPregledIzdavanja();
            izd.Show();
        }

        private void direktniPrijemiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPregledPrijemi pre = new Magacin.frmPregledPrijemi();
            pre.Show();
        }

        private void pregledStanjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPrijavaStanja prijstan = new Magacin.frmPrijavaStanja();
            prijstan.Show();
        }

        private void pregledRadnihNalogaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPregledRadnihNaloga radninalozi = new Magacin.frmPregledRadnihNaloga();
            radninalozi.Show();
        }

        private void pregledStanjaPoPozicijamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void pregledStanjaPoPozicijamaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Magacin.frmPregledStanjaPoPozicijama pesic = new Magacin.frmPregledStanjaPoPozicijama();
            pesic.Show();
        }

        private void raspolozivoStanjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRaspolozivoStanje pesic2 = new frmRaspolozivoStanje();
            pesic2.Show();
        }

        private void maticniPodaciPrikazToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmMaticniPodaciPrikaz mpPrikaz = new Magacin.frmMaticniPodaciPrikaz();
            mpPrikaz.Show();
        }

        private void pregledRezervacijaPoPorudžbinamaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void kalkulatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReklmacijaKalkulator rkl = new ReklmacijaKalkulator();
            rkl.Show();
        }

        private void šifreReklamacijeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSifraReklamacije fSR = new frmSifraReklamacije();
            fSR.Show();
        }

        private void pregledReklamacijaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPregledReklamacijaSonja prs = new frmPregledReklamacijaSonja();
            prs.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ObradaIsporukePocetna oi = new ObradaIsporukePocetna();
            oi.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            WebIsporukaVoziloPocetna wiv = new WebIsporukaVoziloPocetna();
            wiv.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            PregledKontrolorZavrseno pkz = new PregledKontrolorZavrseno();
            pkz.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Magacin.frmOtpremnicaUvozPocetna pocot = new Magacin.frmOtpremnicaUvozPocetna();
            pocot.Show();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            frmReklamacijeUvozPocetna reklpoc = new frmReklamacijeUvozPocetna();
            reklpoc.Show();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            StampajPalZavrseno sp = new StampajPalZavrseno();
            sp.Show();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            StampajReklamacijePZavrseno srp = new StampajReklamacijePZavrseno();
            srp.Show();
        }
    }
}
