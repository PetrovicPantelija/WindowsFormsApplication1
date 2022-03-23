using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Magacin
{
    public partial class MainAdministracija : Form
    {
        public MainAdministracija()
        {
            InitializeComponent();
        }

        private void rkShippingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOrganizacionaJedinica orgjed = new frmOrganizacionaJedinica();
            orgjed.Show();
        }

        private void korisniciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKorisnici korisnici = new frmKorisnici();
            korisnici.Show();
        }

        private void grupeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Administracija___Prava_pristupa.frmGrupe grupe= new Administracija___Prava_pristupa.frmGrupe();
            grupe.Show();
        }

        private void formeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Administracija___Prava_pristupa.frmForme forme = new Administracija___Prava_pristupa.frmForme();
            forme.Show();
        }

        private void dodeliGrupuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Administracija___Prava_pristupa.frmDodeliGrupu grupe = new Administracija___Prava_pristupa.frmDodeliGrupu();
            grupe.Show();
        }

        private void dodeliFormuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Administracija___Prava_pristupa.frmDodeliPravo pravo = new Administracija___Prava_pristupa.frmDodeliPravo();
            pravo.Show();
        }
    }
}
