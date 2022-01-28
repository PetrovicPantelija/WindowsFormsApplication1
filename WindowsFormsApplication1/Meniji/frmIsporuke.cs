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
    public partial class frmIsporuke : Form
    {
        public frmIsporuke()
        {
            InitializeComponent();
        }

        private void btnsZaposleni_Click(object sender, EventArgs e)
        {
            WebIsporuka wi = new WebIsporuka();
            wi.Show();
        }

        private void btnsVrstaRobe_Click(object sender, EventArgs e)
        {
            ObradaIsporukePocetna oi = new ObradaIsporukePocetna();
            oi.Show();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            ObradaIsporuke oi = new ObradaIsporuke();
            oi.Show();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            WebIsporukaVoziloPocetna wiv = new WebIsporukaVoziloPocetna();
            wiv.Show();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            StampaP sp = new StampaP();
            sp.Show();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            ZakljucavanjeIsporuke zak = new ZakljucavanjeIsporuke();
            zak.Show();
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            StampajPal sp = new StampajPal();
            sp.Show();
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            ObradaPaletar op = new ObradaPaletar();
            op.Show();
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            PromenaVozila pvoz = new PromenaVozila();
            pvoz.Show();
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            ŠtampajReklamacijeP srp = new ŠtampajReklamacijeP();
            srp.Show();
        }

        private void iconButton9_Click(object sender, EventArgs e)
        {
            IzbrisiIsporuku izbr = new IzbrisiIsporuku();
            izbr.Show();
        }

        private void iconButton10_Click(object sender, EventArgs e)
        {
            StampajPalZavrseno sp = new StampajPalZavrseno();
            sp.Show();
        }

        private void iconButton11_Click(object sender, EventArgs e)
        {
            StampajReklamacijePZavrseno srp = new StampajReklamacijePZavrseno();
            srp.Show();
        }
    }
}
