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
    public partial class frmTimovi : Form
    {
        public frmTimovi()
        {
            InitializeComponent();
        }

        private void btnsZaposleni_Click(object sender, EventArgs e)
        {
            frmTimovi nft = new frmTimovi();
            nft.Show();
        }
    }
}
