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

namespace WindowsFormsApplication1.Planovi_Excel
{
    public partial class PlanVeza : Form
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public PlanVeza()
        {
            InitializeComponent();
        }
        private void PlanVeza_Load(object sender, EventArgs e)
        {
            FillGV();
        }
        private void FillGV()
        {
            var query = "Select Kolekcije.ID as [IDKol], RTrim(Kolekcije.Naziv) as  Kolekcija,Program.ID as [IDProgram],RTrim(Program.Naziv) as Program,pkp.MpSifra as [MpSifra], " +
                "RTrim(MpNaziv) as [Naziv],MpStaraSif as [Kod],TipPaketa,SkSifra as [Sklad], " +
                "RTrim(SkNaziv) as [Skladista],MpOsnLokacija as [Osnovna lokacija] " +
                "from PripadnostKolProgram pkp " +
                "Inner join Kolekcije on pkp.IdKolekcije = Kolekcije.ID " +
                "inner join Program on pkp.IdPrograma = Program.ID " +
                "inner join MaticniPodatki on pkp.MpSifra = MaticniPodatki.MpSifra " +
                "inner join Sklad on MaticniPodatki.MpOsnSklad = sklad.SkSifra " +
                "Where MPStatus = 0 " +
                "order by Kolekcija asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[2].Visible = false;
            dataGridView2.Columns[8].Visible = false;
            dataGridView2.Columns[9].Visible = false;
            dataGridView2.Columns[10].Visible = false;

            var query2 = "Select * from PlanMpVezni";
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds2.Tables[0];
        }

        private void btnDodeli_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    InsertPlanovi ins = new InsertPlanovi();
                    foreach(DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (row.Selected)
                        {
                            ins.DodeliGrupi(Convert.ToInt32(textBox1.Text), Convert.ToInt32(row.Cells["MpSifra"].Value.ToString()));
                        }
                    }
                }
                catch { }
                FillGV();
            }
            else
            {
                MessageBox.Show("Mora se izabrati ID grupe kojoj se dodeljuje MP");
            }
            
        }

        private void btnNovaGrupa_Click(object sender, EventArgs e)
        {
            InsertPlanovi ins = new InsertPlanovi();
            ins.KreirajNovuGrupu();
            try
            {
                foreach(DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        ins.DodeliNovuGrupu(Convert.ToInt32(row.Cells["MpSifra"].Value));   
                    }
                }
            }
            catch { }
            FillGV();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    textBox1.Text = row.Cells[0].Value.ToString().TrimEnd();
                }
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            InsertPlanovi ins = new InsertPlanovi();
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        ins.ObrisiIzGrupe(Convert.ToInt32(row.Cells["MpSifra"].Value), Convert.ToInt32(row.Cells["IDGrupe"].Value));
                    }
                }

            }
            catch { }
            FillGV();
        }
    }
}
