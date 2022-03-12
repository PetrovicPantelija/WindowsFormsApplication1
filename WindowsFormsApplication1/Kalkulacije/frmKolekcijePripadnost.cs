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

        public frmKolekcijePripadnost()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
            txt_ID.Enabled = false;
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
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
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
    }
}
