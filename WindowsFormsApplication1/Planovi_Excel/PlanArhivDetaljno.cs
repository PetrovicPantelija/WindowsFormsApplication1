using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Planovi_Excel
{
    public partial class PlanArhivDetaljno : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public int ID;

        public PlanArhivDetaljno()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
        }

        public PlanArhivDetaljno(int id)
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            ID = id;
        }

        private void PlanDetaljno_Load(object sender, EventArgs e)
        {
            textBox1.Text = ID.ToString();
            FillPanel();
            FillGV();
            panel1.Visible = true;
        }

        private void FillPanel()
        {
            var query = "Select * from PodesavanjePlan";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);

            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.Columns[0].ReadOnly = true;
            dataGridView2.Columns[1].ReadOnly = true;
        }

        private void FillGV()
        {
            var query = "Select pas.ID,IDPlana,PlanArhiv.Naziv as [Plan],IDPrograma,Program.Naziv as [Program],IDKolekcije,Kolekcije.Naziv as [Kolekcija],pas.MPSifra,RTrim(MaticniPodatki.MpStaraSif) as MpStaraSif," +
                "MaticniPodatki.MpOsnSklad as [Skladiste],MaticniPodatki.MpOsnLokacija as [Lokacija],UOpticaju,Stanje,I36,I35,I34,I33,I32,I31,I30,I29,I28,I27,I26,I25,I24,I23,I22,I21,I20,I19,I18,I17,I16," +
                "I15,I14,I13,I12,I11,I10,I9,I8,I7,I6,I5,I4,I3,I2,I1,Tekuci,UkupnoStanje,PL1_ID as [Plan1],PL1_KOl as [Plan1 kolicina],PL2_ID as [Plan2],PL2_Kol as [Plan2 kolicina],PL3_ID as [Plan3]," +
                "PL3_Kol as [Plan3 kolicina],PL4_ID as [Plan4],PL4_KOL as [Plan4 kolicina],PL5_ID as [Plan5],PL5_KOL as [Plan5 kolicina],Realizacija,Neisporuceno,Raspolozivo,PR12 as [Prosek - 12]," +
                "PR11 as [Prosek - 11],PR10 as [Prosek - 10],PR9 as [Prosek - 9],PR8 as [Prosek - 8],PR7 as [Prosek - 7],PR6 as [Prosek - 6],PR5 as [Prosek - 5],PR4 as [Prosek - 4],PR3 as [Prosek - 3]," +
                "PR2 as [Prosek - 2],Prosek as [Mesecni prosek],Koeficijent,Predlog,UsvojenaOdluka,Prosek2,Predlog2,LagerOdluka as [Lager + Odluka],TrenutnoLager,Trend as [Trend +/ -],TrajnostLagera," +
                "IzdatiNeisporuceni,OttoKol,SteinerKol,DatumAzuriranja " +
                "From PlanArhivStavke pas " +
                "Inner Join PlanArhiv on pas.IDPlana = PlanArhiv.ID " +
                "inner join Program on pas.IDPrograma = Program.ID " +
                "inner join Kolekcije on pas.IDKolekcije = Kolekcije.ID " +
                "Inner join MaticniPodatki on pas.MpSifra = MaticniPodatki.MpSifra " +
                "Where IDPlana = " + Convert.ToInt32(textBox1.Text.ToString()) + " order by pas.ID asc";

            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);

            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            string query2 = "Select * from PodesavanjePlan";
            conn.Open();
            SqlCommand cmd = new SqlCommand(query2, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    if (col.HeaderText == dr["Kolona"].ToString())
                    {
                        int pom = Convert.ToInt32(dr["Visible"].ToString());
                        if (pom == 1)
                        {
                            col.Visible = true;
                        }
                        else
                        {
                            col.Visible = false;
                        }
                    }
                }
            }
            conn.Close();
            //dataGridView1.Columns["UsvojenaOdluka"].ReadOnly = false;
            dataGridView1.Columns["ID"].Width = 50;
            dataGridView1.Columns["IDPlana"].Width = 50;
            dataGridView1.Columns["IdPrograma"].Width = 50;
            dataGridView1.Columns["IdKolekcije"].Width = 50;
            dataGridView1.Columns["Plan"].Width = 150;
            dataGridView1.Columns["Program"].Width = 150;
            dataGridView1.Columns["Kolekcija"].Width = 150;
            dataGridView1.Columns["MpSifra"].Width = 60;
            dataGridView1.Columns["MpStaraSif"].Width = 120;
            dataGridView1.Columns["Skladiste"].Width = 70;
            dataGridView1.Columns["UOpticaju"].Width = 70;
            dataGridView1.Columns["Stanje"].Width = 70;
            dataGridView1.Columns["I36"].Width = 60;
            dataGridView1.Columns["I35"].Width = 60;
            dataGridView1.Columns["I34"].Width = 60;
            dataGridView1.Columns["I33"].Width = 60;
            dataGridView1.Columns["I32"].Width = 60;
            dataGridView1.Columns["I31"].Width = 60;
            dataGridView1.Columns["I30"].Width = 60;
            dataGridView1.Columns["I29"].Width = 60;
            dataGridView1.Columns["I28"].Width = 60;
            dataGridView1.Columns["I27"].Width = 60;
            dataGridView1.Columns["I26"].Width = 60;
            dataGridView1.Columns["I25"].Width = 60;
            dataGridView1.Columns["I24"].Width = 60;
            dataGridView1.Columns["I23"].Width = 60;
            dataGridView1.Columns["I22"].Width = 60;
            dataGridView1.Columns["I21"].Width = 60;
            dataGridView1.Columns["I20"].Width = 60;
            dataGridView1.Columns["I19"].Width = 60;
            dataGridView1.Columns["I18"].Width = 60;
            dataGridView1.Columns["I17"].Width = 60;
            dataGridView1.Columns["I16"].Width = 60;
            dataGridView1.Columns["I15"].Width = 60;
            dataGridView1.Columns["I14"].Width = 60;
            dataGridView1.Columns["I13"].Width = 60;
            dataGridView1.Columns["I12"].Width = 60;
            dataGridView1.Columns["I11"].Width = 60;
            dataGridView1.Columns["I10"].Width = 60;
            dataGridView1.Columns["I9"].Width = 60;
            dataGridView1.Columns["I8"].Width = 60;
            dataGridView1.Columns["I7"].Width = 60;
            dataGridView1.Columns["I6"].Width = 60;
            dataGridView1.Columns["I5"].Width = 60;
            dataGridView1.Columns["I4"].Width = 60;
            dataGridView1.Columns["I3"].Width = 60;
            dataGridView1.Columns["I2"].Width = 60;
            dataGridView1.Columns["I1"].Width = 60;
            dataGridView1.Columns["Tekuci"].Width = 70;
            dataGridView1.Columns["UkupnoStanje"].Width = 70;
            dataGridView1.Columns["Plan1"].Width = 50;
            dataGridView1.Columns["Plan1 kolicina"].Width = 70;
            dataGridView1.Columns["Plan2"].Width = 50;
            dataGridView1.Columns["Plan2 kolicina"].Width = 70;
            dataGridView1.Columns["Plan3"].Width = 50;
            dataGridView1.Columns["Plan3 kolicina"].Width = 70;
            dataGridView1.Columns["Plan4"].Width = 50;
            dataGridView1.Columns["Plan4 kolicina"].Width = 70;
            dataGridView1.Columns["Plan5"].Width = 50;
            dataGridView1.Columns["Plan5 kolicina"].Width = 70;
            dataGridView1.Columns["Realizacija"].Width = 70;
            dataGridView1.Columns["Neisporuceno"].Width = 60;
            dataGridView1.Columns["Raspolozivo"].Width = 60;
            dataGridView1.Columns["Prosek - 12"].Width = 70;
            dataGridView1.Columns["Prosek - 11"].Width = 70;
            dataGridView1.Columns["Prosek - 10"].Width = 70;
            dataGridView1.Columns["Prosek - 9"].Width = 70;
            dataGridView1.Columns["Prosek - 8"].Width = 70;
            dataGridView1.Columns["Prosek - 7"].Width = 70;
            dataGridView1.Columns["Prosek - 6"].Width = 70;
            dataGridView1.Columns["Prosek - 5"].Width = 70;
            dataGridView1.Columns["Prosek - 4"].Width = 70;
            dataGridView1.Columns["Prosek - 3"].Width = 70;
            dataGridView1.Columns["Prosek - 2"].Width = 70;
            dataGridView1.Columns["Mesecni prosek"].Width = 70;
            dataGridView1.Columns["Koeficijent"].Width = 70;
            dataGridView1.Columns["Predlog"].Width = 70;
            dataGridView1.Columns["UsvojenaOdluka"].Width = 70;
            dataGridView1.Columns["Prosek2"].Width = 70;
            dataGridView1.Columns["Lager + Odluka"].Width = 70;
            dataGridView1.Columns["TrenutnoLager"].Width = 70;
            dataGridView1.Columns["TrajnostLagera"].Width = 70;
            dataGridView1.Columns["IzdatiNeisporuceni"].Width = 80;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (dataGridView1[78, row.Index].Value.ToString() != "" || dataGridView1[78, row.Index].Value.ToString() != "0.00")
                {
                    dataGridView1[78, row.Index].Style.BackColor = Color.LightGreen; //doesn't work
                }
                //row.Cells[col.Index].Style.BackColor = Color.Green; //doesn't work
                //col.Cells[row.Index].Style.BackColor = Color.Green; //doesn't work
                //dataGridView1[col.Index, row.Index].Style.BackColor = Color.Green; //doesn't work
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Show();
        }

        private void btnPodesavanje_Click(object sender, EventArgs e)
        {
            InsertPlanovi ins = new InsertPlanovi();
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                ins.UpdatePodesavanje(Convert.ToInt32(row.Cells[0].Value), Convert.ToInt32(row.Cells[2].Value));
            }

            panel1.Visible = false;
            FillGV();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}