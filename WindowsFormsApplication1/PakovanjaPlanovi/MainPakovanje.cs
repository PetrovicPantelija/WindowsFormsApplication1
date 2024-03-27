using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    public partial class MainPakovanje : Form
    {
        string tip;
        public MainPakovanje()
        {
            InitializeComponent();
        }
        private void btnRedovno_Click(object sender, EventArgs e)
        {
            tip = "RD";
            PakovanjaPlanovi.NoviPlanPakovanja frm = new PakovanjaPlanovi.NoviPlanPakovanja(tip);
            frm.Show(); 
        }
        private void btnNamensko_Click(object sender, EventArgs e)
        {
            tip = "NP";
            PakovanjaPlanovi.NoviPlanPakovanja frm = new PakovanjaPlanovi.NoviPlanPakovanja(tip);
            frm.Show();
        }
        private void btnReklamacije_Click(object sender, EventArgs e)
        {
            tip = "RK";
            PakovanjaPlanovi.NoviPlanPakovanja frm = new PakovanjaPlanovi.NoviPlanPakovanja(tip);
            frm.Show();
        }
    }
}
