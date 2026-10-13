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
using WindowsFormsApplication1.PakovanjaPlanovi;

namespace WindowsFormsApplication1.MenijiNabavka
{
    public partial class Nabavka : Form
    {
        public bool PravoP;
        string KorisnikM = "";
        int ReadNabavkaM = 0;
        public static string code = "frmNabavka";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string niz = "";
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        public Nabavka()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
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
        private void btnsZaposleni_Click(object sender, EventArgs e)
        {
            frmPrijemnica frmP = new frmPrijemnica();
            PravoP = frmP.Pravo;
            if (PravoP == true) { frmP.Show(); } else { return; }
        }

        private void btnsVrstaRobe_Click(object sender, EventArgs e)
        {
            frmMedjuskaldisniPrenosi frmM = new frmMedjuskaldisniPrenosi();
            PravoP = frmM.Pravo;
            if (PravoP == true) { frmM.Show(); } else { return; }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            frmPregledZalihaFilter frpzf = new frmPregledZalihaFilter();
            PravoP = frpzf.Pravo;
            if (PravoP == true) { frpzf.Show(); } else { return; }
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            Magacin.frmMagacinTrenutna mt = new Magacin.frmMagacinTrenutna();
            PravoP = mt.Pravo;
            if (PravoP == true) { mt.Show(); } else { return; }
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            Magacin.frmMagacinTrenutnaIzPrijema mip = new Magacin.frmMagacinTrenutnaIzPrijema(KorisnikM);
            PravoP = mip.Pravo;
            if (PravoP == true) { mip.Show(); } else { return; }
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            Magacin.frmPromenaKorisnika move = new Magacin.frmPromenaKorisnika();
            PravoP = move.Pravo;
            if (PravoP == true) { move.Show(); } else { return; }
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            frmPrebacivanjeMeđPrenosaRN medRN = new frmPrebacivanjeMeđPrenosaRN(KorisnikM);
            PravoP = medRN.Pravo;
            if (PravoP == true) { medRN.Show(); } else { return; }
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            Magacin.frmPregledTrebovanja treb = new Magacin.frmPregledTrebovanja();
            PravoP = treb.Pravo;
            if (PravoP == true) { treb.Show(); } else { return; }
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            Magacin.frmMagacinTrenutnaTrebovanje fMt = new Magacin.frmMagacinTrenutnaTrebovanje();
            PravoP = fMt.Pravo;
            if (PravoP == true) { fMt.Show(); } else { return; }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            PakovanjeBarkod pb = new PakovanjeBarkod(Kor);
            pb.Show();
        }
    }
}
