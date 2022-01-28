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
    public partial class frmPrenos : Form
    {
        public frmPrenos()
        {
            InitializeComponent();
        }

        private void btnsZaposleni_Click(object sender, EventArgs e)
        {
            Magacin.frmOtpremnicaUvozPocetna pocot = new Magacin.frmOtpremnicaUvozPocetna();
            pocot.Show();
        }

        private void btnsVrstaRobe_Click(object sender, EventArgs e)
        {
            frmOtpremnicaUvoz frmOU = new frmOtpremnicaUvoz();
            frmOU.Show();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            frmExcelImportPorudzbina frmExcelimp = new frmExcelImportPorudzbina();
            frmExcelimp.Show();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            frmReklamacijeUvozPocetna reklpoc = new frmReklamacijeUvozPocetna();
            reklpoc.Show();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            frmReklamacijaUvoz frek = new frmReklamacijaUvoz();
            frek.Show();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            frmPorudzbinaImport pimp = new frmPorudzbinaImport();
            pimp.Show();
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            frmPrikazStanja prikstan = new frmPrikazStanja();
            prikstan.Show();
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            frmOtpremnicaPregled otp = new frmOtpremnicaPregled();
            otp.Show();
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            frmPorudzbinaPregled pregled = new frmPorudzbinaPregled();
            pregled.Show();
        }
    }
}
