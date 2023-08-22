using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CsvHelper;
using CsvHelper.Configuration;


namespace WindowsFormsApplication1.Planovi_Excel
{
    public partial class PlanDetaljno : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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
            FillPanel();
            FillGV();
            FillCombo();
            panel1.Visible = true;
            FillChecks();
        }

        private void FillChecks()
        {
            DateTime date = DateTime.Now;
            pr1cb1.Text = "01/" + date.AddYears(-1).ToString("yyyy");
            pr1cb2.Text = "02/" + date.AddYears(-1).ToString("yyyy");
            pr1cb3.Text = "03/" + date.AddYears(-1).ToString("yyyy");
            pr1cb4.Text = "04/" + date.AddYears(-1).ToString("yyyy");
            pr1cb5.Text = "05/" + date.AddYears(-1).ToString("yyyy");
            pr1cb6.Text = "06/" + date.AddYears(-1).ToString("yyyy");
            pr1cb7.Text = "07/" + date.AddYears(-1).ToString("yyyy");
            pr1cb8.Text = "08/" + date.AddYears(-1).ToString("yyyy");
            pr1cb9.Text = "09/" + date.AddYears(-1).ToString("yyyy");
            pr1cb10.Text = "10/" + date.AddYears(-1).ToString("yyyy");
            pr1cb11.Text = "11/" + date.AddYears(-1).ToString("yyyy");
            pr1cb12.Text = "12/" + date.AddYears(-1).ToString("yyyy");

            //Predlog 2
            pr2cb1.Text = date.AddMonths(-1).ToString("MM/yyyy");
            pr2cb2.Text = date.AddMonths(-2).ToString("MM/yyyy");
            pr2cb3.Text = date.AddMonths(-3).ToString("MM/yyyy");
            pr2cb4.Text = date.AddMonths(-4).ToString("MM/yyyy");
            pr2cb5.Text = date.AddMonths(-5).ToString("MM/yyyy");
            pr2cb6.Text = date.AddMonths(-6).ToString("MM/yyyy");
            pr2cb7.Text = date.AddMonths(-7).ToString("MM/yyyy");
            pr2cb8.Text = date.AddMonths(-8).ToString("MM/yyyy");
            pr2cb9.Text = date.AddMonths(-9).ToString("MM/yyyy");
            pr2cb10.Text = date.AddMonths(-10).ToString("MM/yyyy");
            pr2cb11.Text = date.AddMonths(-11).ToString("MM/yyyy");
            pr2cb12.Text = date.AddMonths(-12).ToString("MM/yyyy");
        }

        private void PlanDetaljno_Load(object sender, EventArgs e)
        {
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

        private static List<string> Kolekcije(List<string> list, int n)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                if ((i % n) == 0)
                {
                    result.Add(list[i]);
                }
            }
            return result;
        }

        private List<string> IdLista;

        private void FillGV()
        {
            var query = "";
            if (checkBox6.Checked == true)
            {
                query = "Select pas.ID,IDPlana,PlanAktiv.Naziv as [Plan],pas.IDPrograma,Program.Naziv as [Program],pas.IDKolekcije,Kolekcije.Naziv as [Kolekcija],pas.MPSifra,RTrim(MaticniPodatki.MpStaraSif) as MpStaraSif, " +
                    "MaticniPodatki.MpOsnSklad as [Skladiste],MaticniPodatki.MpOsnLokacija as [Lokacija],UOpticaju,Stanje,I36,I35,I34,I33,I32,I31,I30,I29,I28,I27,I26,I25,I24,I23,I22,I21,I20,I19,I18,I17,I16, " +
                    "I15,I14,I13,I12,I11,I10,I9,I8,I7,I6,I5,I4,I3,I2,I1,Tekuci,UkupnoStanje,PL1_ID as [Plan1],Cast(PL1_KOL as integer) as [Plan1 kolicina],PL2_ID as [Plan2],Cast(PL2_KOL as integer) as [Plan2 kolicina],PL3_ID as [Plan3], " +
                    "Cast(PL3_KOL as integer) as [Plan3 kolicina],PL4_ID as [Plan4],Cast(PL4_KOL as integer) as [Plan4 kolicina],PL5_ID as [Plan5],Cast(PL5_KOL as integer) as [Plan5 kolicina],Realizacija,Neisporuceno,Raspolozivo,PR12 as [Prosek - 12], " +
                    "PR11 as [Prosek - 11],PR10 as [Prosek - 10],PR9 as [Prosek - 9],PR8 as [Prosek - 8],PR7 as [Prosek - 7],PR6 as [Prosek - 6],PR5 as [Prosek - 5],PR4 as [Prosek - 4],PR3 as [Prosek - 3], " +
                    "PR2 as [Prosek - 2],PripadnostKolProgram.MinimalnaKol as [MinimalnaKol],Prosek as [Mesecni prosek],pas.Koeficijent,Predlog,UsvojenaOdluka,Prosek2,Predlog2,LagerOdluka as [Lager + Odluka],TrenutnoLager,Trend as [Trend +/ -],TrajnostLagera, " +
                    "IzdatiNeisporuceni,OttoKol,SteinerKol,DatumAzuriranja " +
                    "From PlanAktivStavke pas " +
                    "Inner Join PlanAktiv on pas.IDPlana = PlanAktiv.ID " +
                    "inner join Program on pas.IDPrograma = Program.ID " +
                    "inner join Kolekcije on pas.IDKolekcije = Kolekcije.ID " +
                    "Inner join MaticniPodatki on pas.MpSifra = MaticniPodatki.MpSifra " +
                    "inner join PripadnostKolProgram on pas.MpSifra = PripadnostKolProgram.MPSifra " +
                    "Where IDPlana = " + Convert.ToInt32(textBox1.Text.ToString()) + " and PripadnostKolProgram.TipPaketa = 1 order by pas.ID";
            }
            if (checkBox7.Checked == true)
            {
                query = "Select pas.ID,IDPlana,PlanAktiv.Naziv as [Plan],pas.IDPrograma,Program.Naziv as [Program],pas.IDKolekcije,Kolekcije.Naziv as [Kolekcija],pas.MPSifra,RTrim(MaticniPodatki.MpStaraSif) as MpStaraSif, " +
                    "MaticniPodatki.MpOsnSklad as [Skladiste],MaticniPodatki.MpOsnLokacija as [Lokacija],UOpticaju,Stanje,I36,I35,I34,I33,I32,I31,I30,I29,I28,I27,I26,I25,I24,I23,I22,I21,I20,I19,I18,I17,I16, " +
                    "I15,I14,I13,I12,I11,I10,I9,I8,I7,I6,I5,I4,I3,I2,I1,Tekuci,UkupnoStanje,PL1_ID as [Plan1],Cast(PL1_KOL as integer) as [Plan1 kolicina],PL2_ID as [Plan2],Cast(PL2_KOL as integer) as [Plan2 kolicina],PL3_ID as [Plan3], " +
                    "Cast(PL3_KOL as integer) as [Plan3 kolicina],PL4_ID as [Plan4],Cast(PL4_KOL as integer) as [Plan4 kolicina],PL5_ID as [Plan5],Cast(PL5_KOL as integer) as [Plan5 kolicina],Realizacija,Neisporuceno,Raspolozivo,PR12 as [Prosek - 12], " +
                    "PR11 as [Prosek - 11],PR10 as [Prosek - 10],PR9 as [Prosek - 9],PR8 as [Prosek - 8],PR7 as [Prosek - 7],PR6 as [Prosek - 6],PR5 as [Prosek - 5],PR4 as [Prosek - 4],PR3 as [Prosek - 3], " +
                    "PR2 as [Prosek - 2],PripadnostKolProgram.MinimalnaKol as [MinimalnaKol],Prosek as [Mesecni prosek],pas.Koeficijent,Predlog,UsvojenaOdluka,Prosek2,Predlog2,LagerOdluka as [Lager + Odluka],TrenutnoLager,Trend as [Trend +/ -],TrajnostLagera, " +
                    "IzdatiNeisporuceni,OttoKol,SteinerKol,DatumAzuriranja " +
                    "From PlanAktivStavke pas " +
                    "Inner Join PlanAktiv on pas.IDPlana = PlanAktiv.ID " +
                    "inner join Program on pas.IDPrograma = Program.ID " +
                    "inner join Kolekcije on pas.IDKolekcije = Kolekcije.ID " +
                    "Inner join MaticniPodatki on pas.MpSifra = MaticniPodatki.MpSifra " +
                    "inner join PripadnostKolProgram on pas.MpSifra = PripadnostKolProgram.MPSifra " +
                    "Where IDPlana = " + Convert.ToInt32(textBox1.Text.ToString()) + " and PripadnostKolProgram.TipPaketa = 0 order by pas.ID";
            }
            if ((checkBox6.Checked == true && checkBox7.Checked == true) || (checkBox6.Checked == false && checkBox7.Checked == false))
            {
                query = "Select pas.ID,IDPlana,PlanAktiv.Naziv as [Plan],pas.IDPrograma,Program.Naziv as [Program],pas.IDKolekcije,Kolekcije.Naziv as [Kolekcija],pas.MPSifra,RTrim(MaticniPodatki.MpStaraSif) as MpStaraSif," +
                    "MaticniPodatki.MpOsnSklad as [Skladiste],MaticniPodatki.MpOsnLokacija as [Lokacija],UOpticaju,Stanje,I36,I35,I34,I33,I32,I31,I30,I29,I28,I27,I26,I25,I24,I23,I22,I21,I20,I19,I18,I17,I16," +
                    "I15,I14,I13,I12,I11,I10,I9,I8,I7,I6,I5,I4,I3,I2,I1,Tekuci,UkupnoStanje,PL1_ID as [Plan1],Cast(PL1_KOL as integer) as [Plan1 kolicina],PL2_ID as [Plan2],Cast(PL2_KOL as integer) as [Plan2 kolicina],PL3_ID as [Plan3], " +
                    "Cast(PL3_KOL as integer) as [Plan3 kolicina],PL4_ID as [Plan4],Cast(PL4_KOL as integer) as [Plan4 kolicina],PL5_ID as [Plan5],Cast(PL5_KOL as integer) as [Plan5 kolicina],Realizacija,Neisporuceno,Raspolozivo,PR12 as [Prosek - 12]," +
                    "PR11 as [Prosek - 11],PR10 as [Prosek - 10],PR9 as [Prosek - 9],PR8 as [Prosek - 8],PR7 as [Prosek - 7],PR6 as [Prosek - 6],PR5 as [Prosek - 5],PR4 as [Prosek - 4],PR3 as [Prosek - 3]," +
                    "PR2 as [Prosek - 2],PripadnostKolProgram.MinimalnaKol as [MinimalnaKol],Prosek as [Mesecni prosek],pas.Koeficijent,Predlog,UsvojenaOdluka,Prosek2,Predlog2,LagerOdluka as [Lager + Odluka],TrenutnoLager,Trend as [Trend +/ -],TrajnostLagera," +
                    "IzdatiNeisporuceni,OttoKol,SteinerKol,DatumAzuriranja " +
                    "From PlanAktivStavke pas " +
                    "Inner Join PlanAktiv on pas.IDPlana = PlanAktiv.ID " +
                    "inner join Program on pas.IDPrograma = Program.ID " +
                    "inner join Kolekcije on pas.IDKolekcije = Kolekcije.ID " +
                    "Inner join MaticniPodatki on pas.MpSifra = MaticniPodatki.MpSifra inner join PripadnostKolProgram on pas.MpSifra=PripadnostKolProgram.MpSifra " +
                    "Where IDPlana = " + Convert.ToInt32(textBox1.Text.ToString()) + " order by pas.ID";
            }
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.ReadOnly = false;
            dataGridView1.Columns["UsvojenaOdluka"].ReadOnly = false;

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
            

            dataGridView1.Columns["ID"].Width = 50;
            dataGridView1.Columns["IDPlana"].Width = 50;
            dataGridView1.Columns["IdPrograma"].Width = 50;
            dataGridView1.Columns["IdKolekcije"].Width = 50;
            dataGridView1.Columns["Plan"].Width = 150;
            dataGridView1.Columns["Program"].Width = 150;
            dataGridView1.Columns["Kolekcija"].Width = 100;
            dataGridView1.Columns["MpSifra"].Width = 60;
            dataGridView1.Columns["MpStaraSif"].Width = 120;
            dataGridView1.Columns["Skladiste"].Width = 70;
            dataGridView1.Columns["UOpticaju"].Width = 55;
            dataGridView1.Columns["Stanje"].Width = 55;
            dataGridView1.Columns["I36"].Width = 50;
            dataGridView1.Columns["I35"].Width = 50;
            dataGridView1.Columns["I34"].Width = 50;
            dataGridView1.Columns["I33"].Width = 50;
            dataGridView1.Columns["I32"].Width = 50;
            dataGridView1.Columns["I31"].Width = 50;
            dataGridView1.Columns["I30"].Width = 50;
            dataGridView1.Columns["I29"].Width = 50;
            dataGridView1.Columns["I28"].Width = 50;
            dataGridView1.Columns["I27"].Width = 50;
            dataGridView1.Columns["I26"].Width = 50;
            dataGridView1.Columns["I25"].Width = 50;
            dataGridView1.Columns["I24"].Width = 50;
            dataGridView1.Columns["I23"].Width = 50;
            dataGridView1.Columns["I22"].Width = 50;
            dataGridView1.Columns["I21"].Width = 50;
            dataGridView1.Columns["I20"].Width = 50;
            dataGridView1.Columns["I19"].Width = 50;
            dataGridView1.Columns["I18"].Width = 50;
            dataGridView1.Columns["I17"].Width = 50;
            dataGridView1.Columns["I16"].Width = 50;
            dataGridView1.Columns["I15"].Width = 50;
            dataGridView1.Columns["I14"].Width = 50;
            dataGridView1.Columns["I13"].Width = 50;
            dataGridView1.Columns["I12"].Width = 50;
            dataGridView1.Columns["I11"].Width = 50;
            dataGridView1.Columns["I10"].Width = 50;
            dataGridView1.Columns["I9"].Width = 50;
            dataGridView1.Columns["I8"].Width = 50;
            dataGridView1.Columns["I7"].Width = 50;
            dataGridView1.Columns["I6"].Width = 50;
            dataGridView1.Columns["I5"].Width = 50;
            dataGridView1.Columns["I4"].Width = 50;
            dataGridView1.Columns["I3"].Width = 50;
            dataGridView1.Columns["I2"].Width = 50;
            dataGridView1.Columns["I1"].Width = 50;
            dataGridView1.Columns["Tekuci"].Width = 60;
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
            dataGridView1.Columns["Realizacija"].Width = 60;
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
            dataGridView1.Columns["Predlog2"].Width = 70;
            dataGridView1.Columns["Lager + Odluka"].Width = 70;
            dataGridView1.Columns["TrenutnoLager"].Width = 70;
            dataGridView1.Columns["Trend +/ -"].Width = 70;
            dataGridView1.Columns["TrajnostLagera"].Width = 70;
            dataGridView1.Columns["IzdatiNeisporuceni"].Width = 80;
            dataGridView1.Columns["MinimalnaKol"].Width = 60;

            List<string> Lista = dataGridView1.Rows.Cast<DataGridViewRow>().Select(x => x.Cells["IdKolekcije"].Value.ToString()).Distinct().ToList();
            IdLista = Kolekcije(Lista, 2);

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (IdLista.Contains(row.Cells["IdKolekcije"].Value.ToString()))
                {
                    dataGridView1["Kolekcija", row.Index].Style.BackColor = Color.LightGray;
                    dataGridView1["MPSifra", row.Index].Style.BackColor = Color.LightGray;
                    dataGridView1["MpStaraSif", row.Index].Style.BackColor = Color.LightGray;
                }

                if (dataGridView1[78, row.Index].Value.ToString() != "")
                {
                    dataGridView1[78, row.Index].Style.BackColor = Color.LightGreen; //doesn't work
                }
                dataGridView1["Tekuci", row.Index].Style.BackColor = Color.FromArgb(41, 244, 159);
                dataGridView1["I36", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I35", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I34", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I33", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I32", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I31", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I30", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I29", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I28", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I27", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I26", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I25", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I24", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I23", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I22", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I21", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I20", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I19", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I18", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I17", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I16", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I15", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I14", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I13", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I12", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I11", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I10", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I9", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I8", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I7", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I6", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I5", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I4", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I3", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I2", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);
                dataGridView1["I1", row.Index].Style.BackColor = Color.FromArgb(110, 255, 194);

                dataGridView1["Plan1", row.Index].Style.BackColor = Color.FromArgb(134, 219, 246);
                dataGridView1["Plan1 kolicina", row.Index].Style.BackColor = Color.FromArgb(134, 219, 246);
                dataGridView1["Plan2", row.Index].Style.BackColor = Color.FromArgb(134, 219, 246);
                dataGridView1["Plan2 kolicina", row.Index].Style.BackColor = Color.FromArgb(134, 219, 246);
                dataGridView1["Plan3", row.Index].Style.BackColor = Color.FromArgb(134, 219, 246);
                dataGridView1["Plan3 kolicina", row.Index].Style.BackColor = Color.FromArgb(134, 219, 246);
                dataGridView1["Plan4", row.Index].Style.BackColor = Color.FromArgb(134, 219, 246);
                dataGridView1["Plan4 kolicina", row.Index].Style.BackColor = Color.FromArgb(134, 219, 246);
                dataGridView1["Plan5", row.Index].Style.BackColor = Color.FromArgb(134, 219, 246);
                dataGridView1["Plan5 kolicina", row.Index].Style.BackColor = Color.FromArgb(134, 219, 246);
                dataGridView1["Realizacija", row.Index].Style.BackColor = Color.FromArgb(51, 191, 236);

                dataGridView1["Mesecni prosek", row.Index].Style.BackColor = Color.FromArgb(169, 239, 233);
                dataGridView1["Predlog", row.Index].Style.BackColor = Color.FromArgb(169, 239, 233);
                dataGridView1["Prosek2", row.Index].Style.BackColor = Color.FromArgb(169, 239, 233);
                dataGridView1["Predlog2", row.Index].Style.BackColor = Color.FromArgb(169, 239, 233);
                dataGridView1["Koeficijent", row.Index].Style.BackColor = Color.FromArgb(138, 204, 198);

                dataGridView1["Lager + Odluka", row.Index].Style.BackColor = Color.FromArgb(65, 187, 233);
                dataGridView1["TrenutnoLager", row.Index].Style.BackColor = Color.FromArgb(65, 187, 233);
                dataGridView1["Trend +/ -", row.Index].Style.BackColor = Color.FromArgb(65, 187, 233);
                if (row.Cells["Stanje"].Value.ToString() != "")
                {
                    if (Convert.ToInt32(row.Cells["Stanje"].Value) < 0)
                    {
                        row.Cells["Stanje"].Style.BackColor = Color.Red;
                    }
                    if (row.Cells["MinimalnaKol"].Value.ToString() != "")
                    {
                        if (Convert.ToInt32(row.Cells["MinimalnaKol"].Value) > Convert.ToInt32(row.Cells["Stanje"].Value))
                        {
                            row.Cells["MinimalnaKol"].Style.BackColor = Color.Red;
                        }
                    }
                }
                if (row.Cells["Raspolozivo"].Value.ToString() != "")
                {
                    if (Convert.ToInt32(row.Cells["Raspolozivo"].Value) < 0)
                    {
                        row.Cells["Raspolozivo"].Style.BackColor = Color.Red;
                    }
                }
                
            }
            string query3 = "select distinct PlanAktivStavke.MpSifra from PlanAktivStavke inner join VezaMpPlan on PlanAktivStavke.MpSifra=VezaMpPlan.MpSifra Where IDPlana=" + textBox1.Text;
            conn.Open();
            SqlCommand cmd3 = new SqlCommand(query3, conn);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                if (dr3.HasRows)
                {
                    int mp = Convert.ToInt32(dr3[0].ToString());
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (Convert.ToInt32(row.Cells["MpSifra"].Value.ToString()) == mp)
                        {
                            dataGridView1["UsvojenaOdluka", row.Index].Style.BackColor = Color.Yellow;
                            dataGridView1["MpSifra", row.Index].Style.BackColor = Color.Yellow;
                        }
                    }
                }
            }
            conn.Close();
        }

        private int countPR1 = 0;
        private int countPR2 = 0;
        private int p1m1 = 0, p1m2 = 0, p1m3 = 0, p1m4 = 0, p1m5 = 0, p1m6 = 0, p1m7 = 0, p1m8 = 0, p1m9 = 0, p1m10 = 0, p1m11 = 0, p1m12 = 0;
        private int p2m1 = 0, p2m2 = 0, p2m3 = 0, p2m4 = 0, p2m5 = 0, p2m6 = 0, p2m7 = 0, p2m8 = 0, p2m9 = 0, p2m10 = 0, p2m11 = 0, p2m12 = 0;

        private void button5_Click(object sender, EventArgs e)
        {
            string naziv = "";
            SqlConnection conn = new SqlConnection(connect);
            string query = "Select Naziv From PlanAktiv Where ID=" + textBox1.Text;
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows)
                {
                    naziv = dr[0].ToString().TrimEnd();
                }
            }
            conn.Close();

            string[] path = { Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Plan-" };
            string folder = Path.Combine(path);
            string file = folder + naziv+ ".csv";

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                ShouldQuote = (field) => false,
                Delimiter=";",
            };
            try
            {
                using (var textWriter = File.CreateText(file))
                {
                    using (CsvWriter csv = new CsvWriter(textWriter, config))
                    {
                        List<DataGridViewColumn> listVisible = new List<DataGridViewColumn>();
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                        {
                            if (col.Visible)
                            {
                                listVisible.Add(col);
                            }
                        }
                        
                        for (int i = 0; i < listVisible.Count; i++)
                        {
                            csv.WriteField(listVisible[i].HeaderText);
                        }
csv.NextRecord();
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            for (int j = 0; j < listVisible.Count; j++)
                            {
                                csv.WriteField(dataGridView1.Rows[i].Cells[listVisible[j].Name].Value.ToString());

                            }
                            csv.NextRecord();
                            /*
                            DataTable dt = new DataTable();
                            dt.Load(cmd.ExecuteReader());
                            int count = 1110;
                            foreach (DataRow row in dt.Rows)
                            {
                                count++;
                                csv.WriteField(count);
                                for (var i = 0; i < dt.Columns.Count; i++)
                                {
                                    csv.WriteField(row[i]);
                                }
                                csv.NextRecord();
                            }*/
                        }
                    }
                    MessageBox.Show("Kreiran dokument", file.ToString());
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "CSV"); }

            /*
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = naziv;

            List<DataGridViewColumn> listVisible = new List<DataGridViewColumn>();
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.Visible)
                    listVisible.Add(col);
            }

            for (int i = 0; i < listVisible.Count; i++)
            {
                worksheet.Cells[1, i + 1] = listVisible[i].HeaderText;
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < listVisible.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[listVisible[j].Name].Value.ToString();
                }
            }

            workbook.SaveAs(file, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            
            MessageBox.Show("Kreiran dokument");
            app.Quit();*/
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            InsertPlanovi ins = new InsertPlanovi();
            ins.VezaMP(Convert.ToInt32(textBox1.Text));
            FillGV();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            int mp = 0;
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox3.Text != "")
                {
                    SqlConnection conn = new SqlConnection(connect);
                    string query = "Select PlanAktivStavke.MpSifra From PlanAktivStavke inner join MaticniPodatki on PlanAktivStavke.MpSifra=MaticniPodatki.MpSifra Where IDPlana=" + textBox1.Text + " and MpStaraSif like '%" + textBox3.Text.ToString() + "%'";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr.HasRows)
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                mp = Convert.ToInt32(dr[0].ToString());
                                if (Convert.ToInt32(row.Cells["MpSifra"].Value.ToString()) == mp)
                                {
                                    row.Selected = true;
                                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.SelectedRows[0].Index;
                                }
                            }
                        }
                    }
                    conn.Close();
                }
                else
                {
                    dataGridView1.ClearSelection();
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox2.Text != "")
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (Convert.ToInt32(row.Cells["MpSifra"].Value.ToString()) == Convert.ToInt32(textBox2.Text.ToString()))
                        {
                            row.Selected = true;
                            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.SelectedRows[0].Index;
                        }
                    }
                }
                else
                {
                    dataGridView1.ClearSelection();
                }
            }
        }

        private void Predlozi()
        {
            if (pr1cb1.Checked == true) { countPR1++; p1m1 = 1; } else { p1m1 = 0; }
            if (pr1cb2.Checked == true) { countPR1++; p1m2 = 1; } else { p1m2 = 0; }
            if (pr1cb3.Checked == true) { countPR1++; p1m3 = 1; } else { p1m3 = 0; }
            if (pr1cb4.Checked == true) { countPR1++; p1m4 = 1; } else { p1m4 = 0; }
            if (pr1cb5.Checked == true) { countPR1++; p1m5 = 1; } else { p1m5 = 0; }
            if (pr1cb6.Checked == true) { countPR1++; p1m6 = 1; } else { p1m6 = 0; }
            if (pr1cb7.Checked == true) { countPR1++; p1m7 = 1; } else { p1m7 = 0; }
            if (pr1cb8.Checked == true) { countPR1++; p1m8 = 1; } else { p1m8 = 0; }
            if (pr1cb9.Checked == true) { countPR1++; p1m9 = 1; } else { p1m9 = 0; }
            if (pr1cb10.Checked == true) { countPR1++; p1m10 = 1; } else { p1m10 = 0; }
            if (pr1cb11.Checked == true) { countPR1++; p1m11 = 1; } else { p1m11 = 0; }
            if (pr1cb12.Checked == true) { countPR1++; p1m12 = 1; } else { p1m12 = 0; }

            if (pr2cb1.Checked == true) { countPR2++; p2m1 = 1; } else { p2m1 = 0; }
            if (pr2cb2.Checked == true) { countPR2++; p2m2 = 1; } else { p2m2 = 0; }
            if (pr2cb3.Checked == true) { countPR2++; p2m3 = 1; } else { p2m3 = 0; }
            if (pr2cb4.Checked == true) { countPR2++; p2m4 = 1; } else { p2m4 = 0; }
            if (pr2cb5.Checked == true) { countPR2++; p2m5 = 1; } else { p2m5 = 0; }
            if (pr2cb6.Checked == true) { countPR2++; p2m6 = 1; } else { p2m6 = 0; }
            if (pr2cb7.Checked == true) { countPR2++; p2m7 = 1; } else { p2m7 = 0; }
            if (pr2cb8.Checked == true) { countPR2++; p2m8 = 1; } else { p2m8 = 0; }
            if (pr2cb9.Checked == true) { countPR2++; p2m9 = 1; } else { p2m9 = 0; }
            if (pr2cb10.Checked == true) { countPR2++; p2m10 = 1; } else { p2m10 = 0; }
            if (pr2cb11.Checked == true) { countPR2++; p2m11 = 1; } else { p2m11 = 0; }
            if (pr2cb12.Checked == true) { countPR2++; p2m12 = 1; } else { p2m12 = 0; }

            InsertPlanovi ins = new InsertPlanovi();
            ins.PoveziPredloge(Convert.ToInt32(textBox1.Text.ToString()), countPR1, countPR2, p1m1, p1m2, p1m3, p1m4, p1m5, p1m6, p1m7, p1m8, p1m9, p1m10, p1m11, p1m12, p2m1, p2m2, p2m3, p2m4, p2m5, p2m6, p2m7, p2m8, p2m9, p2m10, p2m11, p2m12);
            countPR1 = 0;
            countPR2 = 0;
        }

        private int tekuci = 0;

        private void Azuriraj()
        {

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

            InsertPlanovi ins = new InsertPlanovi();
            ins.PoveziIzlaznost(Convert.ToInt32(textBox1.Text.ToString()));
            ins.PoveziStanja(Convert.ToInt32(textBox1.Text.ToString()));
            ins.PoveziPlanove(Convert.ToInt32(textBox1.Text.ToString()), pl1, pl2, pl3, pl4, pl5);
            ins.PoveziProseke(Convert.ToInt32(textBox1.Text.ToString()));
            Predlozi();
            if (checkBox8.Checked == true) { tekuci = 1; } else { tekuci = 0; }
            ins.PoveziTrend(Convert.ToInt32(textBox1.Text.ToString()), tekuci);
            FillGV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Azuriraj();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                InsertPlanovi ins = new InsertPlanovi();
                ins.UpdatePlanAktivStavke(Convert.ToInt32(row.Cells["ID"].Value.ToString()), Convert.ToInt32(textBox1.Text.ToString()), Convert.ToInt32(row.Cells["IDKolekcije"].Value.ToString()),
                    Convert.ToInt32(row.Cells["IDPrograma"].Value.ToString()), Convert.ToInt32(row.Cells["MPSifra"].Value.ToString()), Convert.ToDecimal(row.Cells["UsvojenaOdluka"].Value.ToString()));
            }
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
            InsertPlanovi ins = new InsertPlanovi();
            var cell = dataGridView1[e.ColumnIndex, e.RowIndex];
            if (cell.OwningColumn.Name == "UsvojenaOdluka")
            {
                if (cell.OwningRow.Cells["UsvojenaOdluka"].Value.ToString() != "")
                {
                    int mp = Convert.ToInt32(cell.OwningRow.Cells["MpSifra"].Value);
                    decimal odluka = (decimal)cell.Value;

                        ins.UpdatePlanAktivStavke(Convert.ToInt32(cell.OwningRow.Cells["ID"].Value), Convert.ToInt32(textBox1.Text.ToString()), Convert.ToInt32(cell.OwningRow.Cells["IDKolekcije"].Value.ToString()),
                    Convert.ToInt32(cell.OwningRow.Cells["IDPrograma"].Value.ToString()), mp, odluka);
                        ins.PoveziTrend(Convert.ToInt32(textBox1.Text), tekuci);
                    int row = cell.RowIndex;
                    int col = cell.ColumnIndex;
                    FillGV();

                    dataGridView1.Rows[row].Selected = true;
                    dataGridView1.Rows[row].Cells[col].Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = row;
                    dataGridView1.CurrentCell = dataGridView1[col, row];
                    //dataGridView1.CurrentCell = dataGridView1.Rows[row].Cells[col];
                    //dataGridView1.Focus();
                }
            }
            if (cell.OwningColumn.Name == "Koeficijent")
            {
                decimal koef = (decimal)cell.Value;
                ins.UpdateKoeficijent(Convert.ToInt32(cell.OwningRow.Cells["IDKolekcije"].Value), koef);
                Predlozi();
                ins.PoveziTrend(Convert.ToInt32(textBox1.Text), tekuci);

                int row = cell.RowIndex;
                int col = cell.ColumnIndex;
                FillGV();

                dataGridView1.Rows[row].Cells[col].Selected = true;
                dataGridView1.Focus();
                dataGridView1.CurrentCell = dataGridView1.Rows[row].Cells[col];
            }
        }

        private int pl1 = 0, pl2 = 0, pl3 = 0, pl4 = 0, pl5 = 0;

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CausesValidationChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
        }

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