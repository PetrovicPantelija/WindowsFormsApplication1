using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.MenijiNabavka
{
    public partial class frmPlan : Form
    {
        string KorisnikM = "";
        int ReadNabavkaM = 0;
        public frmPlan()
        {
            InitializeComponent();
        }

        private void btnsZaposleni_Click(object sender, EventArgs e)
        {
            //Ulaz u Magacin
            Magacin.frmPlanImport planimport = new Magacin.frmPlanImport(KorisnikM, ReadNabavkaM);
            planimport.Show();
        }

        private void btnsVrstaRobe_Click(object sender, EventArgs e)
        {
            ///Ulaz u magacin
            Magacin.frmPlanImportNeto planimpportneto = new Magacin.frmPlanImportNeto(KorisnikM, ReadNabavkaM);
            planimpportneto.Show();
        }
    }
}
