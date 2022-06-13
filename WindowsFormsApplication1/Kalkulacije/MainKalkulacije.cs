using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Kalkulacije
{
    public partial class MainKalkulacije : Form
    {
        public bool PravoP;
        public static string code = "frmMainKalkulacije";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public MainKalkulacije()
        {
            InitializeComponent();
            this.Focus();
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
            int count=0;
            
            while (dr.Read())
            {
                if (count == 0)
                {
                    niz = dr["IdGrupe"].ToString();
                    count++;
                }
                else
                {
                    niz = niz+"," + dr["IdGrupe"].ToString();
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
        private void label1_Click(object sender, EventArgs e)
        {
            Kalkulacije.frmKalkulacioniCenovnici kalk = new frmKalkulacioniCenovnici();
            PravoP = kalk.Pravo;
            if (PravoP == true) { kalk.Show(); } else { return; }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Kalkulacije.frmKolekcije kol = new frmKolekcije();
            PravoP = kol.Pravo;
            if (PravoP == true) { kol.Show(); } else { return; }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Kalkulacije.frmKolekcijePripadnost kolP = new frmKolekcijePripadnost();
            PravoP = kolP.Pravo;
            if (PravoP == true) { kolP.Show(); } else { return; }
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Blue;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.White;
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Blue;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.White;
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Blue;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.White;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Kalkulacije.frmZaglavlja zag = new frmZaglavlja();
            PravoP = zag.Pravo;
            if (PravoP == true) { zag.Show(); } else { return; }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Kalkulacije.frmKalkulatorStavke kalk = new frmKalkulatorStavke();
            PravoP = kalk.Pravo;
            if (PravoP == true) { kalk.Show(); } else { return; }
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Blue;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.White;
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Blue;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.ForeColor = Color.White;
        }
        private void btnSifarnici_MouseEnter(object sender, EventArgs e)
        {
            btnSifarnici.ForeColor = Color.FromArgb(53, 54, 56);
            btnSifarnici.IconColor= Color.FromArgb(53, 54, 56);
        }

        private void btn_Kalkulator_MouseEnter(object sender, EventArgs e)
        {
            btn_Kalkulator.ForeColor = Color.FromArgb(53, 54, 56);
            btn_Kalkulator.IconColor= Color.FromArgb(53, 54, 56);
        }

        private void btnSifarnici_MouseLeave(object sender, EventArgs e)
        {
            btnSifarnici.ForeColor = Color.White;
            btnSifarnici.IconColor = Color.White;
        }

        private void btn_Kalkulator_MouseLeave(object sender, EventArgs e)
        {
            btn_Kalkulator.ForeColor = Color.White;
            btn_Kalkulator.IconColor = Color.White;
        }
        private void label6_MouseLeave(object sender, EventArgs e)
        {
            label6.ForeColor = Color.White;
        }
        private void label6_MouseEnter(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Blue;
        }
        private void label6_Click(object sender,EventArgs e)
        {
            Kalkulacije.frmPrimenaTroskovnogCenovnika tc = new frmPrimenaTroskovnogCenovnika();
            PravoP = tc.Pravo;
            if (PravoP == true) { tc.Show(); } else { return; }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Kalkulacije.frmCenaKamiona kam = new frmCenaKamiona();
            PravoP = kam.Pravo;
            if (PravoP == true) { kam.Show(); } else { return; }
        }
        private void label7_MouseLeave(object sender, EventArgs e)
        {
            label7.ForeColor = Color.White;
        }
        private void label7_MouseEnter(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Blue;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            frmBazaProdajnihCena cene = new frmBazaProdajnihCena();
            PravoP = cene.Pravo;
            if (PravoP == true) { cene.Show(); } else { return; }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            frmProgram program = new frmProgram();
            PravoP = program.Pravo;
            if (PravoP == true) { program.Show(); } else { return; }
        }

        private void label8_MouseEnter(object sender, EventArgs e)
        {
            label8.ForeColor = Color.Blue;
        }

        private void label8_MouseLeave(object sender, EventArgs e)
        {
            label8.ForeColor = Color.White;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            frmPoredjenjeKalkulatora program = new frmPoredjenjeKalkulatora();
            program.Show();
        }
    }
}
