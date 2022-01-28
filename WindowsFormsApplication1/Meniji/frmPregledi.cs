using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Meniji
{
    public partial class frmPregledi : Form
    {
        public frmPregledi()
        {
            InitializeComponent();
        }

        private void btnsZaposleni_Click(object sender, EventArgs e)
        {
            PregledPaketi pak = new PregledPaketi();
            pak.Show();
        }

        private void btnsVrstaRobe_Click(object sender, EventArgs e)
        {
            PregledIsporuka pi = new PregledIsporuka();
            pi.Show();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            PregledKontrolorZavrseno pkz = new PregledKontrolorZavrseno();
            pkz.Show();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            PregledKontrolor pk = new PregledKontrolor();
            pk.Show();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            PreostaliPaketi preos = new PreostaliPaketi();
            preos.Show();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            ProveraIsporukeSum pis = new ProveraIsporukeSum();
            pis.Show();
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            StampajNeisporucene sneis = new StampajNeisporucene();
            sneis.Show();
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            StampajPalSve sps = new StampajPalSve();
            sps.Show();
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            StampajPalSveP spsp = new StampajPalSveP();
            spsp.Show();
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            StampajPalVremePak spkk = new StampajPalVremePak();
            spkk.Show();
        }

        private void iconButton9_Click(object sender, EventArgs e)
        {
            PregledKontrolorIstorija pki = new PregledKontrolorIstorija();
            pki.Show();
        }

        private void iconButton11_Click(object sender, EventArgs e)
        {
            StampajPalSveZavrseno sps = new StampajPalSveZavrseno();
            sps.Show();
        }
    }
}
