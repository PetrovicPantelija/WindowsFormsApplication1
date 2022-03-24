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
        public frmDodeliGrupu()
        {
            InitializeComponent();
            txt_ID.Enabled = false;
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
            var query = "Select k.Korisnik,Rtrim(DeIme) + ' ' +Rtrim(DePriimek)as Zaposleni From Delavci d,Korisnici k Where d.DeSifra=k.SifraLargo Order By DeIme";
            da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            combo_Korisnik.DataSource = ds.Tables[0];
            combo_Korisnik.DisplayMember = "Zaposleni";
            combo_Korisnik.ValueMember = "Korisnik";

            var query2 = "Select * From Grupe";
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            combo_Grupa.DataSource = ds2.Tables[0];
            combo_Grupa.DisplayMember = "Naziv";
            combo_Grupa.ValueMember = "IdGrupe";
        }
        private void FillGV()
        {
            var query = "select k.ID,k.Korisnik,Rtrim(DeIme) + ' ' +Rtrim(DePriimek)as Zaposleni,k.IdGrupe,g.Naziv as 'Naziv Grupe' " +
                "From KorisnikGrupa k,Grupe g,Delavci d,Korisnici a " +
                "Where k.IdGrupe=g.IdGrupe and a.Korisnik=k.Korisnik and a.SifraLargo=d.DeSifra order by Zaposleni";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 60;

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
