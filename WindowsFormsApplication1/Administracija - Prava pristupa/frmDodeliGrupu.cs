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

namespace WindowsFormsApplication1.Administracija___Prava_pristupa
{
    public partial class frmDodeliGrupu : Form
    {
        public string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        bool status = false;

        public static string code = "frmDodeliGrupu";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";

        public frmDodeliGrupu()
        {
            InitializeComponent();
            txt_ID.Enabled = false;
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
        private void frmDodeliGrupu_Load(object sender, EventArgs e)
        {
            FillCombo();
            FillGV();
        }
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter();
            //var query = "Select k.Korisnik,Rtrim(DeIme) + ' ' +Rtrim(DePriimek)as Zaposleni From Delavci d,Korisnici k Where d.DeSifra=k.SifraLargo Order By DeIme";
            var query = "Select * from Korisnici order by Korisnik";
            da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            combo_Korisnik.DataSource = ds.Tables[0];
            //combo_Korisnik.DisplayMember = "Zaposleni";
            combo_Korisnik.DisplayMember = "korisnik";
            combo_Korisnik.ValueMember = "Korisnik";

            var query2 = "Select * From Grupe order by Naziv";
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            combo_Grupa.DataSource = ds2.Tables[0];
            combo_Grupa.DisplayMember = "Naziv";
            combo_Grupa.ValueMember = "IdGrupe";
        }
        private void FillGV()
        {
            var query = "select k.ID,k.Korisnik,k.IdGrupe,g.Naziv as 'Naziv Grupe' " +
                "From KorisnikGrupa k,Grupe g, Korisnici a" +
                " Where k.IdGrupe = g.IdGrupe and a.Korisnik = k.Korisnik order by k.Korisnik";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 160;
            dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns[3].Width = 160;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var query = "select k.ID,k.Korisnik,Rtrim(DeIme) + ' ' +Rtrim(DePriimek)as Zaposleni,k.IdGrupe,g.Naziv as 'Naziv Grupe' " +
                "From KorisnikGrupa k,Grupe g, Delavci d,Korisnici a " +
                "Where k.IdGrupe = g.IdGrupe and a.Korisnik = k.Korisnik and a.SifraLargo = d.DeSifra and a.korisnik = '" + combo_Korisnik.SelectedValue + "'" + " order by Zaposleni";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Administracija___Prava_pristupa.InsertKorisnikGrupa grupa = new InsertKorisnikGrupa();
            grupa.InsKorisnikGrupa(combo_Korisnik.SelectedValue.ToString().TrimEnd(), Convert.ToInt32(combo_Grupa.SelectedValue));
            FillGV();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Administracija___Prava_pristupa.InsertKorisnikGrupa grupa = new InsertKorisnikGrupa();
            grupa.DelKorisnikGrupa(Convert.ToInt32(txt_ID.Text));
            FillGV();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txt_ID.Text = row.Cells[0].Value.ToString();
                        combo_Grupa.Text = row.Cells[4].Value.ToString();
                        combo_Korisnik.Text = row.Cells[1].Value.ToString();
                        
                    }
                }
            }
            catch { }
        }
    }
}
