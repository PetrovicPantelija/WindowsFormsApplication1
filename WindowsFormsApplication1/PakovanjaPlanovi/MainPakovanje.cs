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

namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    public partial class MainPakovanje : Form
    {
        string tip;
        string Korisnik; //Ovaj korisnik nije potreban
        bool status = false;
        public static string code = "MainPakovanje";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public MainPakovanje(string korisnik)
        {
            InitializeComponent();
            Korisnik = korisnik;
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

        private void btnPaketi_Click(object sender, EventArgs e)
        {
            KompletiPaket frm = new KompletiPaket();
            frm.Show();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            PakovanjeBarkod pb = new PakovanjeBarkod(Korisnik);
            pb.Show();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            Magacin.frmPozicije poz = new Magacin.frmPozicije();
           poz.Show();
        }

        private void MainPakovanje_Load(object sender, EventArgs e)
        {

        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            frmRedosledPakovanja rp = new frmRedosledPakovanja();
            rp.Show();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            Pakovanja.ReklamacijePakovanja rp = new Pakovanja.ReklamacijePakovanja(); 
            rp.Show();
        }
    }
}
