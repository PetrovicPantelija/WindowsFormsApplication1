using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class MainNabavka : Form
    {
        string KorisnikM = "";
        int ReadNabavkaM = 0;
        public MainNabavka()
        {
            InitializeComponent();
        }

        public MainNabavka(string KorisnikN, int ReadNabavka)
        {
            KorisnikM = KorisnikN;
            ReadNabavka = ReadNabavkaM;
            InitializeComponent();
        }

        private void međuskladišniPrenosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMedjuskaldisniPrenosi frmM = new frmMedjuskaldisniPrenosi();
            frmM.Show();
        }

        private void rkShippingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPrijemnica frmP = new frmPrijemnica();
            frmP.Show();
        }

        private void pregledZalihaToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void pregledZalihaFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPregledZalihaFilter frpzf = new frmPregledZalihaFilter();
            frpzf.Show();
        }

        private void međuskladišniPrenosFormiranjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Magacin.frmPozicije poz = new Magacin.frmPozicije();
            poz.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Magacin.frmPaleta paleta = new Magacin.frmPaleta();
            paleta.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Magacin.frmMaticniPodatak maticni = new Magacin.frmMaticniPodatak();
            maticni.Show();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            frmMedjuskaldisniPrenosi frmM = new frmMedjuskaldisniPrenosi();
            frmM.Show();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            frmPrebacivanjeMedjuskladisniPrenos mprp = new frmPrebacivanjeMedjuskladisniPrenos();
            mprp.Show();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Magacin.frmStampaPozicije poz = new Magacin.frmStampaPozicije();
            poz.Show();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
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

        private void uvozIObradaPlanaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Ulaz u Magacin
            Magacin.frmPlanImport planimport = new Magacin.frmPlanImport(KorisnikM, ReadNabavkaM);
            planimport.Show();
        }

        private void uvozNetoPotrebaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ///Ulaz u magacin
            Magacin.frmPlanImportNeto planimpportneto = new Magacin.frmPlanImportNeto(KorisnikM, ReadNabavkaM);
            planimpportneto.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void formiranjeMeđuskladišnogPrenosaIzRadnogNalogaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPrebacivanjeMeđPrenosaRN medRN = new frmPrebacivanjeMeđPrenosaRN(KorisnikM);
            medRN.Show();
        }

        private void magacinTrenutnaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmMagacinTrenutna mt = new Magacin.frmMagacinTrenutna();
            mt.Show();
        }

        private void mSPPrijemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmMagacinTrenutnaIzPrijema mip = new Magacin.frmMagacinTrenutnaIzPrijema(KorisnikM);
            mip.Show();
        }

        private void promenaKorisnikaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPromenaKorisnika move = new Magacin.frmPromenaKorisnika();
            move.Show();
        }

        private void formiranjMSPIzRNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPrebacivanjeMeđPrenosaRN medRN = new frmPrebacivanjeMeđPrenosaRN(KorisnikM);
            medRN.Show();
        }

        private void pregledZalihaUporedniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPregledZaliha pz = new frmPregledZaliha();
            pz.Show();
        }

        private void međuskladišniPrenosFormiranjeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmPrebacivanjeMedjuskladisniPrenos mprp = new frmPrebacivanjeMedjuskladisniPrenos();
            mprp.Show();
        }

        private void trebovanjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magacin.frmPregledTrebovanja treb = new Magacin.frmPregledTrebovanja();
            treb.Show();
        }
    }
}
