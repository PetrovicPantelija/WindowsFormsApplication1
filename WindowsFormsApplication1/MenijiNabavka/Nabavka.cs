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
    public partial class Nabavka : Form
    {
        string KorisnikM = "";
        int ReadNabavkaM = 0;
        public Nabavka()
        {
            InitializeComponent();
        }

        private void btnsZaposleni_Click(object sender, EventArgs e)
        {
            frmPrijemnica frmP = new frmPrijemnica();
            frmP.Show();
        }

        private void btnsVrstaRobe_Click(object sender, EventArgs e)
        {
            frmMedjuskaldisniPrenosi frmM = new frmMedjuskaldisniPrenosi();
            frmM.Show();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            frmPregledZalihaFilter frpzf = new frmPregledZalihaFilter();
            frpzf.Show();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            Magacin.frmMagacinTrenutna mt = new Magacin.frmMagacinTrenutna();
            mt.Show();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            Magacin.frmMagacinTrenutnaIzPrijema mip = new Magacin.frmMagacinTrenutnaIzPrijema(KorisnikM);
            mip.Show();
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            Magacin.frmPromenaKorisnika move = new Magacin.frmPromenaKorisnika();
            move.Show();
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            frmPrebacivanjeMeđPrenosaRN medRN = new frmPrebacivanjeMeđPrenosaRN(KorisnikM);
            medRN.Show();
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            Magacin.frmPregledTrebovanja treb = new Magacin.frmPregledTrebovanja();
            treb.Show();
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            Magacin.frmMagacinTrenutnaTrebovanje fMt = new Magacin.frmMagacinTrenutnaTrebovanje();
            fMt.Show();
        }
    }
}
