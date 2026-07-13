using System;
using System.Windows.Forms;
using WindowsFormsApplication1.Neto_potrebe;

namespace WindowsFormsApplication1.Planovi_Excel
{
    public partial class MainPlaniranje : Form
    {
        public bool PravoP;
        public static string code = "frmMainPlaniranje";
        public bool Pravo;
        private int idGrupe;
        private int idForme;
        private bool insert;
        private bool update;
        private bool delete;
        private string Kor = frmLogovanje.user.ToString().TrimEnd();
        private string niz = "";

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
            if (Kor.Trim() == "stanojevic.ivan" || Kor.Trim() == "stanojlovic.djordje" || Kor.Trim() == "hasanovic.igor" || Kor.Trim() == "pantelija")
            {
                Planovi_Excel.frmPrikazPorudzbinaWebs web = new frmPrikazPorudzbinaWebs();
                web.Show();
            }
            else
            {
                frmPrikazPorudzbinaWebsOgraniceno frm = new frmPrikazPorudzbinaWebsOgraniceno();
                frm.Show();
            }
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

        private void label10_Click(object sender, EventArgs e)
        {
            Neto_potrebe.NetoPotrebe pa = new Neto_potrebe.NetoPotrebe();
            pa.Show();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Neto_potrebe.MdfGrupe pa = new Neto_potrebe.MdfGrupe();
            pa.Show();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Neto_potrebe.MdfPodgrupe pa = new Neto_potrebe.MdfPodgrupe();
            pa.Show();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Neto_potrebe.MpMDF pa = new Neto_potrebe.MpMDF();
            pa.Show();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Neto_potrebe.NetoPotrebe pa = new Neto_potrebe.NetoPotrebe();
            pa.Show();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            PoveziRN2 frm = new PoveziRN2();
            frm.Show();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Magacin.frmPozicije poz = new Magacin.frmPozicije();
            PravoP = poz.Pravo;
            if (PravoP == true) { poz.Show(); } else { return; }
        }

        private void label16_Click(object sender, EventArgs e)
        {
            NetoPotrebe frm = new NetoPotrebe();
            frm.Show();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            KrojnaLista frm = new KrojnaLista();
            frm.Show();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            PregledPodPlan frm = new PregledPodPlan();
            frm.Show();
        }

        private void label10_Click_1(object sender, EventArgs e)
        {
            RadniCentarLog rcl = new RadniCentarLog();
            rcl.Show();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            RazloziZaustavljanja rz = new RazloziZaustavljanja();
            rz.Show();
        }
    }
}