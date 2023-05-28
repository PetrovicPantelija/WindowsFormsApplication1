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
    public partial class PlanDetaljno : Form
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public int ID;
        public PlanDetaljno()
        {
            InitializeComponent();
        }
        public PlanDetaljno(int id)
        {
            InitializeComponent();
            ID = id;
            textBox1.Text = ID.ToString();
            FillGV();
            FillCombo();
        }

        private void PlanDetaljno_Load(object sender, EventArgs e)
        {
            
        }
        private void FillGV()
        {
            var query = "Select pas.ID,IDPlana,PlanAktiv.Naziv as [Plan],IDPrograma,Program.Naziv as [Program],IDKolekcije,Kolekcije.Naziv as [Kolekcija],pas.MPSifra,MaticniPodatki.MpStaraSif," +
                "MaticniPodatki.MpOsnSklad as [Skladiste],MaticniPodatki.MpOsnLokacija as [Lokacija],UOpticaju,Stanje,I36,I35,I34,I33,I32,I31,I30,I29,I28,I27,I26,I25,I24,I23,I22,I21,I20,I19,I18,I17,I16," +
                "I15,I14,I13,I12,I11,I10,I9,I8,I7,I6,I5,I4,I3,I2,I1,Tekuci,UkupnoStanje,PL1_ID as [Plan1],PL1_KOl as [Plan1 kolicina],PL2_ID as [Plan2],PL2_Kol as [Plan2 kolicina],PL3_ID as [Plan3]," +
                "PL3_Kol as [Plan3 kolicina],PL4_ID as [Plan4],PL4_KOL as [Plan 4 kolicina],PL5_ID as [Plan5],PL5_KOL as [Plan5 kolicina],Realizacija,Neisporuceno,Raspolozivo,PR12 as [Prosek - 12]," +
                "PR11 as [Prosek - 11],PR10 as [Prosek - 10],PR9 as [Prosek - 9],PR8 as [Prosek - 8],PR7 as [Prosek - 7],PR6 as [Prosek - 6],PR5 as [Prosek - 5],PR4 as [Prosek - 4],PR3 as [Prosek - 3]," +
                "PR2 as [Prosek - 2],Prosek as [Mesecni prosek],Koeficijent,Predlog,UsvojenaOdluka,Prosek2,Predlog2,LagerOdluka as [Lager + Odluka],TrenutnoLager,Trend as [Trend +/ -],TrajnostLagera," +
                "IzdatiNeisporuceni,OttoKol,SteinerKol,DatumAzuriranja " +
                "From PlanAktivStavke pas " +
                "Inner Join PlanAktiv on pas.IDPlana = PlanAktiv.ID " +
                "inner join Program on pas.IDPrograma = Program.ID " +
                "inner join Kolekcije on pas.IDKolekcije = Kolekcije.ID " +
                "Inner join MaticniPodatki on pas.MpSifra = MaticniPodatki.MpSifra " +
                "Where IDPlana = "+Convert.ToInt32(textBox1.Text.ToString());

            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);

            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];
            //dataGridView1.Columns["UsvojenaOdluka"].ReadOnly = false;

        }
        private void Azuriraj()
        {
            InsertPlanovi ins = new InsertPlanovi();
            ins.PoveziIzlaznost();
            ins.PoveziStanja();
            ins.PoveziPlanove(pl1, pl2, pl3, pl4, pl5);
            ins.PoveziProseke();
            ins.PoveziPredloge(Convert.ToInt32(textBox2.Text.ToString()));
            ins.PoveziTrend();
            FillGV();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            Azuriraj();
        }

        decimal? odluka;
        int id, idkolekcije, idprograma, mpsifra;
        
        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                InsertPlanovi ins = new InsertPlanovi();
                ins.UpdatePlanAktivStavke(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(textBox1.Text.ToString()), Convert.ToInt32(row.Cells[5].Value.ToString()),
                    Convert.ToInt32(row.Cells[3].Value.ToString()), Convert.ToInt32(row.Cells[7].Value.ToString()), Convert.ToDecimal(row.Cells[78].Value.ToString()));

            }
        }
        int pl1=0, pl2=0, pl3=0, pl4=0, pl5;
        private void FillCombo()
        {
            var query = "Select distinct ID,Naziv from PlanArhiv order by ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboPlan1.DataSource = ds.Tables[0];
            cboPlan1.DisplayMember = "Naziv";
            cboPlan1.ValueMember = "ID";

            var query2 = "Select distinct ID,Naziv from PlanArhiv order by ID desc";
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            cboPlan2.DataSource = ds2.Tables[0];
            cboPlan2.DisplayMember = "Naziv";
            cboPlan2.ValueMember = "ID";

            var query3 = "Select distinct ID,Naziv from PlanArhiv order by ID desc";
            var da3 = new SqlDataAdapter(query3, conn);
            var ds3 = new DataSet();
            da3.Fill(ds3);
            cboPlan3.DataSource = ds3.Tables[0];
            cboPlan3.DisplayMember = "Naziv";
            cboPlan3.ValueMember = "ID";

            var query4 = "Select distinct ID,Naziv from PlanArhiv order by ID desc";
            var da4 = new SqlDataAdapter(query4, conn);
            var ds4 = new DataSet();
            da4.Fill(ds4);
            cboPlan4.DataSource = ds4.Tables[0];
            cboPlan4.DisplayMember = "Naziv";
            cboPlan4.ValueMember = "ID";

            var query5 = "Select distinct ID,Naziv from PlanArhiv order by ID desc";
            var da5 = new SqlDataAdapter(query5, conn);
            var ds5 = new DataSet();
            da5.Fill(ds5);
            cboPlan5.DataSource = ds5.Tables[0];
            cboPlan5.DisplayMember = "Naziv";
            cboPlan5.ValueMember = "ID";

            if (checkBox1.Checked)
            {
                pl1 = Convert.ToInt32(cboPlan1.SelectedValue);
            }

            if (checkBox2.Checked)
            {
                pl2 = Convert.ToInt32(cboPlan2.SelectedValue);
            }
            if (checkBox3.Checked)
            {
                pl3 = Convert.ToInt32(cboPlan3.SelectedValue);
            }

            if (checkBox4.Checked)
            {
                pl4 = Convert.ToInt32(cboPlan4.SelectedValue);
            }

            if (checkBox5.Checked)
            {
                pl5 = Convert.ToInt32(cboPlan5.SelectedValue);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertPlanovi ins = new InsertPlanovi();
            ins.ProglasiIzlaznost(Convert.ToInt32(textBox1.Text.ToString()));
            ins.ArhivirajPlan(Convert.ToInt32(textBox1.Text.ToString()));
            this.Close();
        }
    }
}
