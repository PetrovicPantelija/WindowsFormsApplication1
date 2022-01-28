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
    public partial class frmIstorija : Form
    {
        public frmIstorija()
        {
            InitializeComponent();
        }

        private void btnsZaposleni_Click(object sender, EventArgs e)
        {
            IstorijaRadaKontrolor isk = new IstorijaRadaKontrolor();
            isk.Show();
        }

        private void btnsVrstaRobe_Click(object sender, EventArgs e)
        {
            IstorijaRadaPakeriste isp = new IstorijaRadaPakeriste();
            isp.Show();
        }
    }
}
