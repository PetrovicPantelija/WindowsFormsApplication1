using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Planovi_Excel
{
    public partial class MainPlaniranje : Form
    {
        public bool PravoP;
        public static string code = "frmMainPlaniranje";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public MainPlaniranje()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Kalkulacije.frmKolekcije kol = new Kalkulacije.frmKolekcije();
            kol.Show(); 
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Kalkulacije.frmProgram program = new Kalkulacije.frmProgram();
            program.Show(); 
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Planovi_Excel.PripadnostKP pripkp = new PripadnostKP();
            pripkp.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Planovi_Excel.Podesavanje pod = new Podesavanje();
            pod.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Planovi_Excel.Izlaznost pod = new Izlaznost();
            pod.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Planovi_Excel.PlanAktiv pa = new PlanAktiv();
            pa.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Planovi_Excel.frmPrikazPorudzbinaWebs web = new frmPrikazPorudzbinaWebs();
            web.Show();
            // Planovi_Excel.PlanStavke pas = new PlanStavke();
            // pas.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Planovi_Excel.PlanArhiv pa = new PlanArhiv();
            pa.Show();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Planovi_Excel.PlanVeza pv = new PlanVeza();
            pv.Show();
        }
    }
}
