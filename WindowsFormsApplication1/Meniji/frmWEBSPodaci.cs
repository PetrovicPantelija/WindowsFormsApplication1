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

namespace WindowsFormsApplication1.Meniji
{
    public partial class frmWEBSPodaci : Form
    {
        public bool PravoP;
        public static string code = "frmWEBSPodaci";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public frmWEBSPodaci()
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
        private void iconButton3_Click(object sender, EventArgs e)
        {
            Vozila voz = new Vozila();
            PravoP = voz.Pravo;
            if (PravoP == true) { voz.Show(); } else { return; }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            ReferencijalnaTabela rt = new ReferencijalnaTabela();
            PravoP = rt.Pravo;
            if (PravoP == true) { rt.Show(); } else { return; }
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            Komitenti part = new Komitenti();
            PravoP = part.Pravo;
            if (PravoP == true) { part.Show(); } else { return; }
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            Saloni sal = new Saloni();
            PravoP = sal.Pravo;
            if (PravoP == true) { sal.Show(); } else { return; }
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            Artikal art = new Artikal();
            PravoP = art.Pravo;
            if (PravoP == true) { art.Show(); } else { return; }
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            v123v vs = new v123v();
            PravoP = vs.Pravo;
            if (PravoP == true) { vs.Show(); } else { return; }
        }

        private void frmWEBSPodaci_Load(object sender, EventArgs e)
        {

        }
    }
}
