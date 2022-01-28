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
    public partial class frmArhiviranje : Form
    {
        public frmArhiviranje()
        {
            InitializeComponent();
        }

        private void btnsZaposleni_Click(object sender, EventArgs e)
        {
            Form2 pr = new Form2();
            pr.Show();
        }

        private void btnsVrstaRobe_Click(object sender, EventArgs e)
        {
            PregledKontrolorIstorijaArhiv pkis = new PregledKontrolorIstorijaArhiv();
            pkis.Show();
        }
    }
}
