using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Runtime.InteropServices;
using WindowsFormsApplication1.PakovanjaPlanovi;

namespace WindowsFormsApplication1
{
  
    public partial class frmLogovanje : Form
    {
        public static string user = "";
        public bool PravoP;
        int usao = 0;
        public frmLogovanje()
        {
            InitializeComponent();
        }

        // Pomeranje forme prevlacenjem naslovne trake (forma nema standardni okvir)
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0xA1, 0x2, 0); // WM_NCLBUTTONDOWN, HTCAPTION
            }
        }

        private void frmLogovanje_Load(object sender, EventArgs e)
        {
            var select = " Select Distinct RTrim(Korisnik) as Korisnik From Korisnici order by Korisnik asc";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
           dataAdapter.Fill(ds);
            cboKorisnik.DataSource = ds.Tables[0];
            cboKorisnik.DisplayMember = "Korisnik";
            cboKorisnik.ValueMember = "Korisnik";
        }

        private void ProveriAzuriranje()
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show("Da li želite update verzije", "Azuriranje", buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                       // MessageBox.Show("PA0");
                    Main mainf = new Main(0);
                if (PravoP == true) { mainf.Show(); } else { return; }
                PravoP = mainf.Pravo;
                      
                    }
                    else
	                {
                       // MessageBox.Show("PA1");
                    Main mainf = new Main(1);
                    mainf.Show();
                PravoP = mainf.Pravo;
                if (PravoP == true) { mainf.Show(); } else { return; }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            user = cboKorisnik.Text.ToString();
            // var select = " Select Distinct DatumUtovara From RkShipping where Stanje = 1 and Vozilo = '" + cboVozila.Text + "'";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            SqlCommand command = new SqlCommand("SELECT Korisnik FROM Korisnici where Password = '" + txtPassword.Text + "'", myConnection);
            myConnection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nije ispravno uneta lozinka");
                return;
            }
            int nasao = 0;
            while (reader.Read())
            {
                string myString = reader.GetString(0); //The 0 stands for "the 0'th column", so the first column of the result.
                // Do somthing with this rows string, for example to put them in to a list
                if (myString.TrimEnd() == cboKorisnik.Text)
                {
                    user = myString;
                    if (cboKorisnik.Text.Trim() == "NEbojša pešic") 
                    {
                       // MessageBox.Show("Korisnik");
                        ProveriAzuriranje();
                        nasao = 1;
                    }
                     if (cboKorisnik.Text.Trim() == "Reklamacije")  
                    {
                       // MessageBox.Show("Korisnik");
                        ProveriAzuriranje();
                        nasao = 1;
                    }
                     if (cboKorisnik.Text.Trim() == "Tanja D. Tanaskovic") 
                    {
                       // MessageBox.Show("Korisnik");
                        ProveriAzuriranje();
                        nasao = 1;
                    }

                    if (nasao == 0)
                    {
                       // MessageBox.Show("Nije korisnik");
                        Main mainf = new Main(0);
                        PravoP = mainf.Pravo;
                        if (PravoP == true) { mainf.Show(); this.WindowState = FormWindowState.Minimized;
                        }
                        else { return; }
                    }
                  

                }
            }
                reader.Close();
                myConnection.Close();


        }
        private void button2_Click(object sender, EventArgs e)
        {
            user = cboKorisnik.Text.ToString();
            int ReadNabavka = 0;
            string KorisnikN = "";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            SqlCommand command = new SqlCommand("SELECT Korisnik, ReadNabavka FROM Korisnici where Password = '" + txtPassword.Text + "' and Nabavka = 1 ", myConnection);
            myConnection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nije ispravno uneta lozinka");
                return;
            }
            while (reader.Read())
            {
                string myString = reader.GetString(0); //The 0 stands for "the 0'th column", so the first column of the result.
                // Do somthing with this rows string, for example to put them in to a list
                if (myString.TrimEnd() == cboKorisnik.Text)
                {
                    user = myString;
                    KorisnikN = reader["Korisnik"].ToString();
                    ReadNabavka = Convert.ToInt32(reader["ReadNabavka"].ToString());
                    MainNabavka mn = new MainNabavka(KorisnikN, ReadNabavka);
                    PravoP = mn.Pravo;
                    if (PravoP == true) { mn.Show(); this.WindowState = FormWindowState.Minimized;
                    }
                    else { return; }
                }
                else
                {
                    MessageBox.Show("Nije ispravno uneta lozinka");
                    return;
                }
            }
            reader.Close();
            myConnection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            user = cboKorisnik.Text.ToString();
            if (txtPassword.Text == "9145")
            {
                Magacin.MainAdministracija ma = new Magacin.MainAdministracija();
                ma.Show();
              /*
                PravoP = ma.Pravo;
                if (PravoP == true) { ma.Show(); 
                this.WindowState = FormWindowState.Minimized;
                }
                else { return; }
              */
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            user = cboKorisnik.Text.ToString();
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            SqlCommand command = new SqlCommand("SELECT Korisnik FROM Korisnici where Password = '" + txtPassword.Text + "'", myConnection);
            myConnection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nije ispravno uneta lozinka");
                return;
            }
            else
            {
                Portirnica.StatusNajave stat = new Portirnica.StatusNajave(cboKorisnik.Text);
                PravoP = stat.Pravo;
                if (PravoP == true) { stat.Show(); this.WindowState = FormWindowState.Minimized;
                }
                else { return; }
            }
            myConnection.Close();
        }

        private void iconPictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconPictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // ZAH-012: pristup po grupama (forma frmMainKalkulacije u Dodeli pravo), ne po imenu korisnika
            user = cboKorisnik.Text.ToString().TrimEnd();
            if (!IspravnaLozinka(user, txtPassword.Text))
            {
                MessageBox.Show("Nije ispravno uneta lozinka");
                return;
            }
            Kalkulacije.MainKalkulacije kalk = new Kalkulacije.MainKalkulacije();
            PravoP = kalk.Pravo;
            if (PravoP == true) { kalk.Show(); this.WindowState = FormWindowState.Minimized; }
            else { kalk.Dispose(); }
        }

        // lozinka izabranog korisnika (ne bilo kog korisnika sa tom lozinkom), kroz parametre
        private static bool IspravnaLozinka(string korisnik, string lozinka)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            using (var conn = new SqlConnection(s_connection))
            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM Korisnici WHERE RTRIM(Korisnik) = @Korisnik AND Password = @Password", conn))
            {
                cmd.Parameters.AddWithValue("@Korisnik", korisnik);
                cmd.Parameters.AddWithValue("@Password", lozinka);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            user = cboKorisnik.Text.ToString();
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            SqlCommand command = new SqlCommand("SELECT Korisnik FROM Korisnici where Password = '" + txtPassword.Text + "'", myConnection);
            myConnection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nije ispravno uneta lozinka");
                return;
            }
            else
            {
                Planovi_Excel.MainPlaniranje plp = new Planovi_Excel.MainPlaniranje();
                PravoP = plp.Pravo;
                if (PravoP == true)
                {
                    this.WindowState = FormWindowState.Minimized;
                    plp.Show();
                }
                else { return; }
            }
            myConnection.Close();

            /*
            if (txtPassword.Text != "")
            {
                user = cboKorisnik.Text.ToString().TrimEnd();
                Planovi_Excel.MainPlaniranje plp = new Planovi_Excel.MainPlaniranje();
                plp.Show();
                this.WindowState = FormWindowState.Minimized;

            }
            else { MessageBox.Show("Nije ispravno uneta lozinka"); }
            */
        }

        private void button6_Click(object sender, EventArgs e)
        {
            user = cboKorisnik.Text.ToString();
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            SqlCommand command = new SqlCommand("SELECT Korisnik FROM Korisnici where Password = '" + txtPassword.Text + "'", myConnection);
            myConnection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nije ispravno uneta lozinka");
                return;
            }
            else
            {
                MainPakovanje frm = new MainPakovanje(user);
                PravoP = frm.Pravo;
                if (PravoP == true)
                {
                    frm.Show(); this.WindowState = FormWindowState.Minimized;
                }
                else { return; }
            }
            myConnection.Close();

            /*
            user = cboKorisnik.Text;
            MainPakovanje frm = new MainPakovanje(user);
            frm.Show();
            this.WindowState = FormWindowState.Minimized;
            */

        }

        private void button8_Click(object sender, EventArgs e)
        {
            user= cboKorisnik.Text;
            Barcode.Pregled frm = new Barcode.Pregled();
            frm.Show();
            this.WindowState = FormWindowState.Minimized;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != "")
            {
                user = cboKorisnik.Text.ToString().TrimEnd();
                var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
                SqlConnection myConnection = new SqlConnection(s_connection);
                var c = new SqlConnection(s_connection);
                SqlCommand command = new SqlCommand("SELECT SifraLargo FROM Korisnici where LTRIM(RTRIM(Korisnik)) = '" + user + "' AND Password = '" + txtPassword.Text + "'", myConnection);
                myConnection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false)
                {
                    MessageBox.Show("Nije ispravno uneta lozinka");
                    return;
                }
                int SifraLargo = 0;
                while (reader.Read())
                {
                    SifraLargo = reader.GetInt32(0);
                }

                reader.Close();
                myConnection.Close();

                ProizvodnjaUKadrovskoj.MainZaposleni plp = new ProizvodnjaUKadrovskoj.MainZaposleni(SifraLargo);
                plp.Show();
                this.WindowState = FormWindowState.Minimized;


            }
            else
            {
                MessageBox.Show("Nije ispravno uneta lozinka");

            }

        }


        private void button10_Click(object sender, EventArgs e)
        {
            KompletiPaket frm=new KompletiPaket();
            frm.Show();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            user = cboKorisnik.Text;
            PakovanjeBarkod pb = new PakovanjeBarkod(user);
            pb.Show();
        }

        private void button10_Click_2(object sender, EventArgs e)
        {

        }

        // ZAH-010: modul Servis; lozinka se proverava parametrizovanim upitom, pravo na meni preko Forme.Code = frmMainServis
        private void btnServis_Click(object sender, EventArgs e)
        {
            user = cboKorisnik.Text.ToString().TrimEnd();
            try
            {
                var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
                bool ispravno;
                using (var conn = new SqlConnection(s_connection))
                using (var cmd = new SqlCommand("SELECT COUNT(*) FROM Korisnici WHERE RTRIM(Korisnik) = @Korisnik AND Password = @Lozinka", conn))
                {
                    cmd.Parameters.AddWithValue("@Korisnik", user);
                    cmd.Parameters.AddWithValue("@Lozinka", txtPassword.Text);
                    conn.Open();
                    ispravno = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
                if (!ispravno)
                {
                    MessageBox.Show("Nije ispravno uneta lozinka");
                    return;
                }
                Servis.MainServis ms = new Servis.MainServis();
                if (ms.Pravo)
                {
                    ms.Show();
                    this.WindowState = FormWindowState.Minimized;
                }
                else ms.Dispose();
            }
            catch (Exception ex)
            {
                Servis.Poruke.Greska(this, ex);
            }
        }
    }
}
