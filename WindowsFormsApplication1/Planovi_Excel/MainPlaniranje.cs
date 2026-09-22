using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApplication1.Neto_potrebe;

namespace WindowsFormsApplication1.Planovi_Excel
{
    public partial class MainPlaniranje : Form
    {
        public bool PravoP;
        public static string code = "MainPlaniranje";
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
            IdGrupe();
            IdForme();
            PravoPristupa();
        }

        public string IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            //Sifarnici.frmLogovanje frm = new Sifarnici.frmLogovanje();         
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            int count = 0;

            while (dr.Read())
            {
                if (count == 0)
                {
                    niz = dr["IdGrupe"].ToString();
                    count++;
                }
                else
                {
                    niz = niz + "," + dr["IdGrupe"].ToString();
                    count++;
                }

            }
            conn.Close();
            return niz;
        }
        private int IdForme()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select IdForme from Forme where Rtrim(Code)=" + "'" + code + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idForme = Convert.ToInt32(dr["IdForme"].ToString());
            }
            conn.Close();
            return idForme;
        }

        private void PravoPristupa()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select * From GrupaForma Where IdGrupe in (" + niz + ") and IdForme=" + idForme;
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nemate prava za pristup ovoj formi", code);
                Pravo = false;
            }
            else
            {
                Pravo = true;
                while (reader.Read())
                {
                    insert = Convert.ToBoolean(reader["Upis"]);
                    if (insert == false)
                    {
                        //tsNew.Enabled = false;
                    }
                    update = Convert.ToBoolean(reader["Izmena"]);
                    if (update == false)
                    {
                        //tsSave.Enabled = false;
                    }
                    delete = Convert.ToBoolean(reader["Brisanje"]);
                    if (delete == false)
                    {
                        //tsDelete.Enabled = false;
                    }
                }
            }

            conn.Close();
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

        private void label21_Click(object sender, EventArgs e)
        {
            frmMaterijali1 materijali1 = new frmMaterijali1();
            materijali1.Show();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            frmMaterijali2 materijali2 = new frmMaterijali2();
            materijali2.Show();
        }

        private void label23_Click(object sender, EventArgs e)
        {
            frmTehnologija1 tehnologija1 = new frmTehnologija1();
            tehnologija1.Show();
        }

        private void label24_Click(object sender, EventArgs e)
        {
            frmTehnologija2 tehnologija2 = new frmTehnologija2();
            tehnologija2.Show();
        }

        private void MainPlaniranje_Load(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {
            frmSifarniciPlaniranje sp = new frmSifarniciPlaniranje();
            sp.Show();
        }
    }
}