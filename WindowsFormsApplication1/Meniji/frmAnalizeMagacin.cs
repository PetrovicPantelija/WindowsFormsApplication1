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
    public partial class frmAnalizeMagacin : Form
    {
        public frmAnalizeMagacin()
        {
            InitializeComponent();
        }

        private void btnsZaposleni_Click(object sender, EventArgs e)
        {
            Magacin.frmPregledStanjaPoPozicijama pesic = new Magacin.frmPregledStanjaPoPozicijama();
            pesic.Show();
        }

        private void btnsVrstaRobe_Click(object sender, EventArgs e)
        {
            frmRaspolozivoStanje pesic2 = new frmRaspolozivoStanje();
            pesic2.Show();
        }
    }
}
