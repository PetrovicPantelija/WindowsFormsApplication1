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
    public partial class PlanStavke : Form
    {
        public int ID;
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public PlanStavke()
        {
            InitializeComponent();
        }
        public PlanStavke(int id)
        {
            InitializeComponent();
            txtIdPlana.Text = id.ToString();
        }
        private void FillGV()
        {
            var query = "Select Kolekcije.ID as [IDKol], RTrim(Kolekcije.Naziv) as  Kolekcija,Program.ID as [IDProgram],RTrim(Program.Naziv) as Program,pkp.MpSifra as [Sifra], " +
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


            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].Width = 0; //kolekcija
            dataGridView1.Columns[2].Width = 0; //program
            //MpSifra 4
            //Kod 6
            //Tip 7
            dataGridView1.Columns[8].Width = 0; //sklad
            //lokacija 10
        }
        int idPlana, idPrograma, idKolekcije, mpSifra;
        private void button1_Click(object sender, EventArgs e)
        {
            
            InsertPlanovi ins = new InsertPlanovi();
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        idPlana = Convert.ToInt32(txtIdPlana.Text);
                        idPrograma = Convert.ToInt32(row.Cells[2].Value.ToString());
                        idKolekcije = Convert.ToInt32(row.Cells[0].Value.ToString());
                        mpSifra = Convert.ToInt32(row.Cells[4].Value.ToString());

                        ins.InsertPlanStavke(idPlana, idPrograma, idKolekcije, mpSifra);
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.ToString()); }
            FillStavke();
        }
        private void FillStavke()
        {
            var query2 = "Select ps.ID,ps.IdPlana,RTrim(PlanAktiv.Naziv) as [NazivPlana],RTrim(Kolekcije.Naziv) as [Kolekcija],RTrim(Program.Naziv) as [Program],ps.MpSifra," +
            "RTrim(MpNaziv) as [Naziv] " +
            "From PlanAktivStavke ps " +
            "inner join PlanAktiv on ps.IDPlana = PlanAktiv.ID " +
            "Inner join Kolekcije on ps.IdKolekcije = Kolekcije.ID " +
            "Inner join Program on ps.IDPrograma = Program.ID " +
            "inner join MaticniPodatki on ps.MpSifra = MaticniPodatki.MpSifra " +
            "Where ps.IDPlana = " + Convert.ToInt32(txtIdPlana.Text) + " order by ps.ID,ps.MpSifra";

            SqlConnection conn = new SqlConnection(connect);
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds2.Tables[0];
        }
        private void PlanStavke_Load(object sender, EventArgs e)
        {
            FillGV();
            FillStavke();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertPlanovi ins = new InsertPlanovi();
            try
            {
                foreach(DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        ins.DeletePlanStavke(Convert.ToInt32(row.Cells[0].Value));
                    }
                }
            }
            catch { }
            FillStavke();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertPlanovi ins = new InsertPlanovi();
            ins.ProglasiIzlaznost(Convert.ToInt32(txtIdPlana.Text.ToString()));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Planovi_Excel.PlanDetaljno frm = new PlanDetaljno(Convert.ToInt32(txtIdPlana.Text.ToString()));
            frm.Show();
        }
    }
}
