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
    public partial class frmMenijiWEBSPodaci : Form
    {
        public frmMenijiWEBSPodaci()
        {
            InitializeComponent();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            Vozila voz = new Vozila();
            voz.Show();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            Komitenti part = new Komitenti();
            part.Show();
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            Saloni sal = new Saloni();
            sal.Show();
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            Artikal art = new Artikal();
            art.Show();
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {

            v123v vs = new v123v();
            vs.Show();
        }
    }
}
