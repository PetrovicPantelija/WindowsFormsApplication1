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
    public partial class frmBazaProdajnihCena : Form
    {
        bool status = false;
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public frmBazaProdajnihCena()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
            txt_ID.Enabled = false;
        }
        private void FillGV()
        {
            var query = "Select * From BazaProdajnihCenovnika";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void FillCombo()
        {
            var query = " Select * from Kalkulator";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "Naziv";
            comboBox1.ValueMember = "ID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string naziv;
            decimal vpCena;
            var query = "Select KalkulatorStavke.MpSifra as Sifra,MaticniPodatki.MpNaziv as Naziv,VpCena " +
                "from KalkulatorStavke " +
                "inner join MaticniPodatki on KalkulatorStavke.MPSifra=MaticniPodatki.MpSifra " +
                "where IDKalkulator= "+Convert.ToInt32(comboBox1.SelectedValue)+" and KalkulatorStavke.MpSifra= "+Convert.ToInt32(txt_mpSifra.Text);
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == false)
            {
                MessageBox.Show("U ovom kalkulatoru ne postoji artikal sa ovom šifrom");
                txt_mpNaziv.Text = "";
                txt_Cena.Text = "";
                return;
            }
            while (dr.Read())
            {
                naziv = dr["Naziv"].ToString();
                vpCena = Convert.ToDecimal(dr["VpCena"].ToString());
                txt_mpNaziv.Text = naziv.ToString();
                txt_Cena.Text = vpCena.ToString();
            }
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
                        txt_mpSifra.Text = row.Cells[1].Value.ToString();
                        txt_Slika.Text = row.Cells[2].Value.ToString();
                        txt_Kod.Text = row.Cells[3].Value.ToString();
                        txt_mpNaziv.Text = row.Cells[4].Value.ToString();
                        txt_Sirina.Text = row.Cells[5].Value.ToString();
                        txt_Visina.Text = row.Cells[6].Value.ToString();
                        txt_Duzina.Text = row.Cells[7].Value.ToString();
                        txt_Opis.Text = row.Cells[8].Value.ToString();
                        txt_Vrste.Text = row.Cells[9].Value.ToString();
                        txt_Kg.Text = row.Cells[10].Value.ToString();
                        txt_Koleto.Text = row.Cells[11].Value.ToString();
                        txt_M3.Text = row.Cells[12].Value.ToString();
                        txt_Cena.Text = row.Cells[13].Value.ToString();
                    }
                }
            }
            catch
            {
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
                Kalkulacije.InsertBazaProdajnihCena baza = new InsertBazaProdajnihCena();
                baza.InsBazaProdajnihCena(Convert.ToInt32(txt_mpSifra.Text), txt_Slika.Text.ToString().TrimEnd(), txt_Kod.Text.ToString().TrimEnd(),
                    txt_mpNaziv.Text.ToString().TrimEnd(), Convert.ToDecimal(txt_Sirina.Text), Convert.ToDecimal(txt_Visina.Text), Convert.ToDecimal(txt_Duzina.Text),
                    txt_Opis.Text.ToString().TrimEnd(), txt_Vrste.Text.ToString().TrimEnd(), Convert.ToDecimal(txt_Kg.Text), Convert.ToDecimal(txt_Koleto.Text),
                    Convert.ToDecimal(txt_M3.Text), Convert.ToDecimal(txt_Cena.Text));
                tsNew.Checked = false;
                tsNew.Enabled = true;
            }
            else
            {
                Kalkulacije.InsertBazaProdajnihCena baza = new InsertBazaProdajnihCena();
                baza.UpdBazaProdajnihCena(Convert.ToInt32(txt_ID.Text),Convert.ToInt32(txt_mpSifra.Text), txt_Slika.Text.ToString().TrimEnd(), txt_Kod.Text.ToString().TrimEnd(),
                    txt_mpNaziv.Text.ToString().TrimEnd(), Convert.ToDecimal(txt_Sirina.Text), Convert.ToDecimal(txt_Visina.Text), Convert.ToDecimal(txt_Duzina.Text),
                    txt_Opis.Text.ToString().TrimEnd(), txt_Vrste.Text.ToString().TrimEnd(), Convert.ToDecimal(txt_Kg.Text), Convert.ToDecimal(txt_Koleto.Text),
                    Convert.ToDecimal(txt_M3.Text), Convert.ToDecimal(txt_Cena.Text));
            }
            FillGV();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            Kalkulacije.InsertBazaProdajnihCena baza = new InsertBazaProdajnihCena();
            baza.DelBazaProdajnihCena(Convert.ToInt32(txt_ID.Text));
            FillGV();
        }
    }
}
