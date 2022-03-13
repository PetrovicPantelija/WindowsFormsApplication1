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
    public partial class frmPrimenaTroskovnogCenovnika : Form
    {
        bool status = false;
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public frmPrimenaTroskovnogCenovnika()
        {
            InitializeComponent();
            txt_ID.Enabled = false;
            FillGV();
        }
        private void FillGV()
        {
            var query = "Select PrimenaTroskovnogCenovnika.ID,PrimenaTroskovnogCenovnika.MpSifra,MaticniPodatki.MpNaziv,MpSifraVeznog,mp.MpNaziv " +
                "from PrimenaTroskovnogCenovnika " +
                "inner join MaticniPodatki on PrimenaTroskovnogCenovnika.MpSifra = MaticniPodatki.MpSifra " +
                "inner join MaticniPodatki as mp on PrimenaTroskovnogCenovnika.MpSifraVeznog = mp.MpSifra";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btn_mp_Click(object sender, EventArgs e)
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
                    return;
                }
                else if (dialogResult == DialogResult.No)
                {
                    txt_MpSifra.Text = "";
                }
            }
        }

        private void btn_mpVezni_Click(object sender, EventArgs e)
        {
            string MpNaziv, MpDoNaziv, MpStaraSif;
            var query = "Select MpSifra,MpStaraSif,MpNaziv,MpDoNaziv FROM MaticniPodatki Where MpSifra=" + Convert.ToInt32(txt_MpVezni.Text);
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == false)
            {
                MessageBox.Show("Ne postoji artikal sa ovom šifrom");
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
                    return;
                }
                else if (dialogResult == DialogResult.No)
                {
                    txt_MpVezni.Text = "";
                }
            }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            tsNew.Checked = true;
            tsNew.Enabled = false;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                Kalkulacije.InsertPrimenaTroskovnogCenovnika tc = new InsertPrimenaTroskovnogCenovnika();
                tc.InsTroskovniCenovnik(Convert.ToInt32(txt_MpSifra.Text), Convert.ToInt32(txt_MpVezni.Text));
                tsNew.Checked = false;
                tsNew.Enabled = true;
            }
            else
            {
                Kalkulacije.InsertPrimenaTroskovnogCenovnika tc = new InsertPrimenaTroskovnogCenovnika();
                tc.UpdTroskovniCenovnik(Convert.ToInt32(txt_ID.Text), Convert.ToInt32(txt_MpSifra.Text), Convert.ToInt32(txt_MpVezni.Text));
            }
            FillGV();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            Kalkulacije.InsertPrimenaTroskovnogCenovnika tc = new InsertPrimenaTroskovnogCenovnika();
            tc.DelTroskovniCenovnik(Convert.ToInt32(txt_ID.Text));
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
                        txt_MpSifra.Text = row.Cells[1].Value.ToString();
                        txt_MpVezni.Text = row.Cells[3].Value.ToString();
                    }
                }
            }
            catch
            {
            }
        }
    }
}
