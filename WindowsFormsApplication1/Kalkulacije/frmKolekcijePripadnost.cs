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
    public partial class frmKolekcijePripadnost : Form
    {
        bool status = false;
        bool mp = false;
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public static string code = "frmKolekcijePripadnost";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";

        public frmKolekcijePripadnost()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            SfGridPomocnik.Stilizuj(dataGridView1);
            FillGV();
            FillCombo();
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
                        tsNew.Enabled = false;
                    }
                    update = Convert.ToBoolean(reader["Izmena"]);
                    if (update == false)
                    {
                        tsSave.Enabled = false;
                    }
                    delete = Convert.ToBoolean(reader["Brisanje"]);
                    if (delete == false)
                    {
                        tsDelete.Enabled = false;
                    }
                }
            }

            conn.Close();
        }
        private void frmKolekcijePripadnost_Load(object sender, EventArgs e)
        {

        }
        private void FillGV()
        {
            var query = "Select KolekcijePripadnost.ID,KolekcijePripadnost.IDKolekcije,KolekcijePripadnost.MpSifra,MaticniPodatki.MpStaraSif,MaticniPodatki.MpNaziv " +
                "From KolekcijePripadnost " +
                "Inner Join MaticniPodatki on KolekcijePripadnost.MpSifra=MaticniPodatki.MpSifra " +
                "Inner Join Kolekcije on KolekcijePripadnost.IDKolekcije=Kolekcije.ID";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            SfGridPomocnik.Popuni(dataGridView1, ds.Tables[0]);
        }
        private void FillCombo()
        {
            var query = " Select * from Kolekcije";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            combo_Kolekcija.DataSource = ds.Tables[0];
            combo_Kolekcija.DisplayMember = "Naziv";
            combo_Kolekcija.ValueMember = "ID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string MpNaziv, MpDoNaziv, MpStaraSif;
            var query = "Select MpSifra,MpStaraSif,MpNaziv,MpDoNaziv FROM MaticniPodatki Where MpSifra=" + Convert.ToInt32(txt_MpSifra.Text);
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == false)
            {
                MessageBox.Show("Ne postoji artikal sa ovom šifrom");
                mp = false;
                return;
            }
            while (dr.Read())
            {
                MpStaraSif = dr["MpStaraSif"].ToString();
                MpNaziv = dr["MpNaziv"].ToString().TrimEnd();
                MpDoNaziv = dr["MpDoNaziv"].ToString().TrimEnd();

                DialogResult dialogResult = MessageBox.Show("Šifra proizvoda: " + txt_MpSifra.Text.ToString() + "\nStara šifra: " + MpStaraSif +
                    "\nNaziv: " + MpNaziv + "\nNazivDo: " + MpDoNaziv + "\n\nPotvrdi/Odustani", MpNaziv, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (dialogResult == DialogResult.Yes)
                {
                    mp = true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    txt_MpSifra.Text = "";
                    mp = false;
                }
            }
        }
        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if(status==true && mp == true)
            {
                Kalkulacije.InsertKolekcijePripadnost kol = new InsertKolekcijePripadnost();
                kol.InsKolekcijePripadnost(Convert.ToInt32(txt_MpSifra.Text), Convert.ToInt32(combo_Kolekcija.SelectedValue));
                status = false;
            }
            else
            {
                Kalkulacije.InsertKolekcijePripadnost kol = new InsertKolekcijePripadnost();
                kol.UpdKolekcijePripadnost(Convert.ToInt32(txt_ID.Text), Convert.ToInt32(txt_MpSifra.Text), Convert.ToInt32(combo_Kolekcija.SelectedValue));
            }
            FillGV();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            Kalkulacije.InsertKolekcijePripadnost kol = new InsertKolekcijePripadnost();
            kol.DelKolekcijePripadnost(Convert.ToInt32(txt_ID.Text));
        }

        private void dataGridView1_SelectionChanged(object sender, Syncfusion.WinForms.DataGrid.Events.SelectionChangedEventArgs e)
        {
            try
            {
                foreach (SfRed row in SfGridPomocnik.Redovi(dataGridView1))
                {
                    if (row.Selected)
                    {
                        txt_ID.Text = row.Cells[0].ToString();
                        txt_MpSifra.Text = row.Cells[3].ToString().TrimEnd();
                        combo_Kolekcija.Text = row.Cells[2].ToString();
                    }
                }
            }
            catch
            {

            }
        }

        private void combo_Kolekcija_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
