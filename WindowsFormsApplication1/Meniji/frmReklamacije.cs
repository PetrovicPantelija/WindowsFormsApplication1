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
    public partial class frmReklamacije : Form
    {
        public frmReklamacije()
        {
            InitializeComponent();
        }

        private void btnsZaposleni_Click(object sender, EventArgs e)
        {
            ObradaReklamacije obr = new ObradaReklamacije();
            obr.Show();
        }

        private void btnsVrstaRobe_Click(object sender, EventArgs e)
        {
            ReklmacijaKalkulator rkl = new ReklmacijaKalkulator();
            rkl.Show();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            frmSifraReklamacije fSR = new frmSifraReklamacije();
            fSR.Show();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            frmPregledReklamacijaSonja prs = new frmPregledReklamacijaSonja();
            prs.Show();
        }
    }
}
