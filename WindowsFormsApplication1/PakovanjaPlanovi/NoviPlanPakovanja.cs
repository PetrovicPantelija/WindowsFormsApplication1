using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    public partial class NoviPlanPakovanja : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        int ID, Konacan;
        string Naziv, Napomena;
        DateTime DatumOd, DatumDo;
        bool status = false;
        string Tip;
        public NoviPlanPakovanja()
        {
            InitializeComponent();
            FillGV();
        }
        public NoviPlanPakovanja(string tip)
        {
            Tip = tip;
            InitializeComponent();
            FillGV();
        }
        private void FillGV()
        {
            var query = "Select * from PlanoviPakovanja Where Tip='"+Tip+"' order by ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 170;
            dataGridView1.Columns[4].Width = 250;
            dataGridView1.Columns[5].Width = 60;
        }
        private void NoviPlanPakovanja_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            dateTimePicker1.Value= DateTime.Now;
            dateTimePicker2.Value= DateTime.Now;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            status = true;
            groupBox1.Visible = true;
            btnDodaj.Text = "Dodaj";
        }

        private void btnNazad_Click(object sender, EventArgs e)
        {
            status = false;
            groupBox1.Visible = false;
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Da li ste sigurni da želite da obrište plan?", "Potvrdi brisanje", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                PakovanjaPlanovi.InsertPakovanja ins = new InsertPakovanja();
                ins.DeletePlanPakovanja(ID);
                FillGV();
            }
            else { return; }
        }

        private void btnKonacan_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            var query = "select s.ID,IDPlanPakovanja,RasporedPakovanja,RnBroj,StatusRn,(Count(ArtikalIzPrvogNivoaSastavnice)) as [Delova],VrstaRn,Artikal,Kod,Naziv,s.KolicinaZaProizvodnju,PlaniraniKrajnjiDatumZavrsetka,Termin " +
                "from StavkePlanaPakovanja s " +
                "inner join PregledStavkiPlanaPakovanja on s.id=PregledStavkiPlanaPakovanja.IDStavkePlanaPakovanja " +
                "Where IDPlanPakovanja=" + ID + " " +
                "Group by s.id,IDPlanPakovanja,RnBroj,StatusRn,VrstaRn,Artikal,Kod,Naziv,s.KolicinaZaProizvodnju,PlaniraniKrajnjiDatumZavrsetka,Termin,RasporedPakovanja order by Delova asc";

            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.ForeColor = System.Drawing.Color.Black;
            dataGridView2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8);
            dataGridView2.BorderStyle = BorderStyle.Fixed3D;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            dataGridView2.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            dataGridView2.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Tomato;
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dataGridView2.MultiSelect = false;

            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[1].Visible = false;
            dataGridView2.Columns[3].Width = 60;
            dataGridView2.Columns[4].Width = 60;
            dataGridView2.Columns[5].Width = 50;
            dataGridView2.Columns[2].HeaderText = "Raspored pakovanja";
            dataGridView2.Columns[2].Width = 60;
            dataGridView2.Columns[6].Width = 50;
            dataGridView2.Columns[7].Width = 60;
            dataGridView2.Columns[10].HeaderText = "Količina";
            dataGridView2.Columns[10].Width = 60;
            dataGridView2.Columns[11].HeaderText = "Planirani datum";
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dataGridView1["Konacan", e.RowIndex].Value.ToString() == "1")
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
            }
        }

        private void btnOtvori_Click(object sender, EventArgs e)
        {
            if (Tip == "RD")
            {
                PakovanjaPlanovi.StavkePlanaPakovanjaNovi frm = new StavkePlanaPakovanjaNovi(ID);
                frm.Show();
            }
            else if(Tip == "NP")
            {
                NamenskoPakovanjeNovi frm = new NamenskoPakovanjeNovi(ID);
                //NamenskoPakovanje frm = new NamenskoPakovanje(ID);
                frm.Show();
            }
            else if (Tip == "RK")
            {
                Reklamacije frm = new Reklamacije(ID);
                frm.Show();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PakovanjaPlanovi.InsertPakovanja ins = new InsertPakovanja();
            var result = MessageBox.Show("Da li ste sigurni da želite da proglasite plan konačnim?\nNakon potvrđivanja više neće biti moguće menjati plan!", "Proglasi konačan", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    foreach(DataGridViewRow row in dataGridView2.Rows)
                    {
                        int id = Convert.ToInt32(row.Cells[0].Value);
                        int raspored = Convert.ToInt32(row.Cells["RasporedPakovanja"].Value);
                        if (raspored > 0)
                        {
                            ins.UpisiRaspored(id, raspored);
                        }
                        else
                        {
                            MessageBox.Show("Mora se upisati pozicija pakovanja!");
                            return;
                        }
                    }
                    ins.ProgalsiPlanPakovanja(ID);
                }
                catch
                { }
                
                FillGV();
            }
            else { return; }
            groupBox2.Visible=false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
        }
        private void btnIzmeni_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;

            txtNaziv.Text = Naziv;
            txtNapomena.Text = Napomena;
            dateTimePicker1.Value = DatumOd;
            dateTimePicker2.Value = DatumDo;

            btnDodaj.Text = "Izmeni plan";
            status = false;
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            PakovanjaPlanovi.InsertPakovanja ins = new InsertPakovanja();
            if (status == true)
            {
                ins.InsertPlanPakovanja(txtNaziv.Text.ToString().TrimEnd(), Convert.ToDateTime(dateTimePicker1.Value.ToString()), Convert.ToDateTime(dateTimePicker2.Value.ToString()), txtNapomena.Text.ToString().TrimEnd(),Tip);
            }
            else
            {
                ins.UpdatePlanPakovanja(ID, txtNaziv.Text.ToString().TrimEnd(), Convert.ToDateTime(dateTimePicker1.Value.ToString()), Convert.ToDateTime(dateTimePicker2.Value.ToString()), txtNapomena.Text.ToString().TrimEnd(), 0);
            }
            groupBox1.Visible = false;
            FillGV();
            status = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach(DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                        Naziv = row.Cells[1].Value.ToString().TrimEnd();
                        DateTime datumOd = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        DateTime datumDo = Convert.ToDateTime(row.Cells[3].Value.ToString());
                        Napomena = row.Cells[4].Value.ToString().TrimEnd();
                        Konacan = Convert.ToInt32(row.Cells[5].Value.ToString());
                        DatumOd = Convert.ToDateTime(datumOd.ToString("dd.MM.yyyy hh:mm"));
                        DatumDo= Convert.ToDateTime(datumDo.ToString("dd.MM.yyyy hh:mm"));
                    }
                }
            }
            catch { }
        }
    }
}
