using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.MenijiNabavka
{
    public partial class frmMagacinNabavka : Form
    {
        string KorisnikM = "";
        int ReadNabavkaM = 0;
        public frmMagacinNabavka()
        {
            InitializeComponent();
        }

        private void iconButton13_Click(object sender, EventArgs e)
        {
            frmPrebacivanjeMedjuskladisniPrenos mprp = new frmPrebacivanjeMedjuskladisniPrenos();
            mprp.Show();
        }

        private void iconButton12_Click(object sender, EventArgs e)
        {
            frmPregledZaliha pz = new frmPregledZaliha();
            pz.Show();
        }

        private void iconButton11_Click(object sender, EventArgs e)
        {
            frmPrebacivanjeMeđPrenosaRN medRN = new frmPrebacivanjeMeđPrenosaRN(KorisnikM);
            medRN.Show();
        }

        private void iconButton10_Click(object sender, EventArgs e)
        {
            Magacin.frmPregledRadnihNaloga radninalozi = new Magacin.frmPregledRadnihNaloga();
            radninalozi.Show();
        }

        private void iconButton9_Click(object sender, EventArgs e)
        {
            Magacin.frmPrijavaStanja prijstan = new Magacin.frmPrijavaStanja();
            prijstan.Show();
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            Magacin.frmPregledPrijemi pre = new Magacin.frmPregledPrijemi();
            pre.Show();
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            Magacin.frmPregledIzdavanja izd = new Magacin.frmPregledIzdavanja();
            izd.Show();
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            Magacin.frmPlanMedjuskladisni plan = new Magacin.frmPlanMedjuskladisni();
            plan.Show();
        }

        private void btnsZaposleni_Click(object sender, EventArgs e)
        {
            Magacin.frmPozicije poz = new Magacin.frmPozicije();
            poz.Show();
        }

        private void btnsVrstaRobe_Click(object sender, EventArgs e)
        {
            Magacin.frmPaleta paleta = new Magacin.frmPaleta();
            paleta.Show();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            Magacin.frmMaticniPodatak maticni = new Magacin.frmMaticniPodatak();
            maticni.Show();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            frmMedjuskaldisniPrenosi frmM = new frmMedjuskaldisniPrenosi();
            frmM.Show();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            frmPrebacivanjeMedjuskladisniPrenos mprp = new frmPrebacivanjeMedjuskladisniPrenos();
            mprp.Show();
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            Magacin.frmStampaPozicije poz = new Magacin.frmStampaPozicije();
            poz.Show();
        }
    }
}
