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
    public partial class frmPregledEURRSD : Form
    {
        int kalkulator = 0;
        int devizni = 0;
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public frmPregledEURRSD(int idK)
        {
            InitializeComponent();
            kalkulator = idK;
        }

        private void frmPregledEURRSD_Load(object sender, EventArgs e)
        {
            PrikaziProgrameIKolekcije();
        }
        private void PrikaziProgrameIKolekcije()
        {
            var query = "Select * from Program";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            string value = dataGridView2.Rows[0].Cells[0].Value.ToString();

            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Program";
            dataGridView2.Columns[1].Width = 145;

            var query2 = "Select Kolekcije.ID as ID,Kolekcije.Naziv as Naziv " +
                " from Kolekcije ";
            SqlConnection conn2 = new SqlConnection(connect);
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds2.Tables[0];

            string value2 = dataGridView3.Rows[0].Cells[0].Value.ToString();

            DataGridViewColumn column3 = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 20;

            DataGridViewColumn column4 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "Kolekcija";
            dataGridView3.Columns[1].Width = 145;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            devizni = 1;
            var query = "Select KalkulatorStavke.ID,Program.Naziv, Kolekcije.Naziv, KalkulatorStavke.MPSifra, " +
" KalkulatorStavke.WSifra, KalkulatorStavke.TrosakProizvodnje, " +
" Round((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs) - 1) * 100, 1) as Marza, " +
" Round(KalkulatorStavke.MarzaRef, 1) as MarzaRef, Round(PragMarze,1) as PragMarze, Round(((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)) - 1) * 100 - PragMarze, 0) as RazlikaOdPraga," +
" Round(KalkulatorStavke.VpCena,0) as VPCena, KalkulatorStavke.Rabat, " +
" Round(((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)) - 1) * 100, 0) - " +
" (Round(((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)) - 1) * 100 * Rabat * 0.01, 0)) as MarzaRabat, " +
" Round(KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Rabat * 0.01),0) as VPCenaRabat, " +
" KalkulatorStavke.Akcija, Round(KalkulatorStavke.Marza,1) as MarzaAkcija, " +
" Round((KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Rabat * 0.01) - ((KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Rabat * 0.01))  * akcija * 0.01)),0) " +
"  as VPCEnaAkcija " +
" from KalkulatorStavke " +
" inner join Program on KalkulatorStavke.ProgramID = Program.ID " +
" Inner " +
" join Kolekcije on KalkulatorStavke.KolekcijaID = Kolekcije.ID " +
" inner " +
" join Kalkulator on Kalkulator.ID = KalkulatorStavke.IDKalkulator " +
" Where IDKalkulator = " + kalkulator;
           
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];


            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;


            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Program";
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Kolekcija";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Mp sifra";
            dataGridView1.Columns[3].Width = 70;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "WS Sifra";
            dataGridView1.Columns[4].Width = 150;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Trosak";
            dataGridView1.Columns[5].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Marza";
            dataGridView1.Columns[6].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Referencijala";
            dataGridView1.Columns[7].DefaultCellStyle.Format = "N1";

            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Prag";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Razlika";
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[9].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[9].Width = 50;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "VP Cena";
            dataGridView1.Columns[10].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[10].Width = 80;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Rabat";
            dataGridView1.Columns[11].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[11].Width = 80;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Marza Rabat";
            dataGridView1.Columns[12].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[12].Width = 80;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "VP cena rabat";
            dataGridView1.Columns[13].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[13].Width = 80;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Akcija";
            dataGridView1.Columns[14].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[14].Width = 80;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Marza Akcija";
            dataGridView1.Columns[15].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[15].Width = 80;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "VP Cena akcija";
            dataGridView1.Columns[16].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[16].Width = 80;

            ObojDatagrid();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            devizni = 0;
            
            var query = "Select KalkulatorStavke.ID,Program.Naziv, Kolekcije.Naziv, KalkulatorStavke.MPSifra, " +
" KalkulatorStavke.WSifra, KalkulatorStavke.TrosakProizvodnje, " +
" Round(((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)) - 1) * 100, 1) as Marza, " +
" Round(KalkulatorStavke.MarzaRef,1) as MarzaRef, Round(PragMarze,1) as PragMarze, " +
" Round(((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)) - 1) * 100 - PragMarze, 0) as RazlikaOdPraga, " +
" Round((KalkulatorStavke.VpCena * Kurs),0) as VpCena, " +
" KalkulatorStavke.Rabat," +
" KalkulatorStavke.Akcija, " +
" Round(KalkulatorStavke.Marza,1) as MarzaAkcija, " +
" Round(((KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Rabat * 0.01) - (KalkulatorStavke.VpCena * akcija * 0.01 * Rabat * 0.01)) - (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)),0) as Zarada, " +
" Round((KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Rabat * 0.01) - ((KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Rabat * 0.01))  * akcija * 0.01)),0) " +
"as VpAkcija,  " +
" Round((MpCena ),0) as MpCena " +
" from KalkulatorStavke " +
" inner join Program on KalkulatorStavke.ProgramID = Program.ID " +
" Inner " +
" join Kolekcije on KalkulatorStavke.KolekcijaID = Kolekcije.ID " +
" inner " +
" join Kalkulator on Kalkulator.ID = KalkulatorStavke.IDKalkulator " +
" Where IDKalkulator = " + kalkulator;

            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Program";
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Kolekcija";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Mp sifra";
            dataGridView1.Columns[3].Width = 80;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "WS Sifra";
            dataGridView1.Columns[4].Width = 150;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Trosak";
            dataGridView1.Columns[5].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Marza";
            dataGridView1.Columns[6].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Referencijala";
            dataGridView1.Columns[7].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Prag";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Razlika";
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[9].Width = 50;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "VP Cena";
            dataGridView1.Columns[10].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[10].Width = 80;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Rabat";
            dataGridView1.Columns[11].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[11].Width = 80;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Akcija";
            dataGridView1.Columns[12].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[12].Width = 80;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Marza Akcija";
            dataGridView1.Columns[13].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[13].Width = 80;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Zarada";
            dataGridView1.Columns[14].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[14].Width = 80;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "MP cena Akcija";
            dataGridView1.Columns[15].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[15].Width = 80;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Mp cena";
            dataGridView1.Columns[16].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[16].Width = 80;


            ObojDatagrid();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            devizni = 1;
            
            string programi = "";
            int prviprogram = 0;

            string kolekcije = "";
            int prvakolekcija = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected)
                {
                    if (prviprogram == 0)
                    {
                        programi = row.Cells[0].Value.ToString();
                        prviprogram = prviprogram + 1;
                    }
                    else
                    {
                        programi = programi + "," + row.Cells[0].Value.ToString();
                        prviprogram = prviprogram + 1;
                    }
                }
            }

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected)
                {
                    if (prvakolekcija == 0)
                    {
                        kolekcije = row.Cells[0].Value.ToString();
                        prvakolekcija = prvakolekcija + 1;
                    }
                    else
                    {
                        kolekcije = kolekcije + "," + row.Cells[0].Value.ToString();
                        prvakolekcija = prvakolekcija + 1;
                    }
                }
            }
            var query = "Select KalkulatorStavke.ID,Program.Naziv, Kolekcije.Naziv, KalkulatorStavke.MPSifra, " +
" KalkulatorStavke.WSifra, KalkulatorStavke.TrosakProizvodnje, " +
" Round((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs) - 1) * 100, 1) as Marza, " +
" Round(KalkulatorStavke.MarzaRef, 1) as MarzaRef, Round(PragMarze,1) as PragMarze, Round(((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)) - 1) * 100 - PragMarze, 0) as RazlikaOdPraga," +
" Round(KalkulatorStavke.VpCena,0) as VPCena, KalkulatorStavke.Rabat, " +
" Round(((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)) - 1) * 100, 0) - " +
" (Round(((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)) - 1) * 100 * Rabat * 0.01, 0)) as MarzaRabat, " +
" Round(KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Rabat * 0.01),0) as VPCenaRabat, " +
" KalkulatorStavke.Akcija, Round(KalkulatorStavke.Marza,1) as MarzaAkcija, " +
" Round((KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Rabat * 0.01) - ((KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Rabat * 0.01))  * akcija * 0.01)),0) " +
"  as VPCEnaAkcija " +
" from KalkulatorStavke " +
" inner join Program on KalkulatorStavke.ProgramID = Program.ID " +
" Inner " +
" join Kolekcije on KalkulatorStavke.KolekcijaID = Kolekcije.ID " +
" inner " +
" join Kalkulator on Kalkulator.ID = KalkulatorStavke.IDKalkulator " +
 "Where KalkulatorStavke.ProgramID in (" + programi + ") and KalkulatorStavke.KolekcijaID in (" + kolekcije + ") and KalkulatorStavke.IDKalkulator = " + kalkulator;

            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;


            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Program";
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Kolekcija";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Mp sifra";
            dataGridView1.Columns[3].Width = 70;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "WS Sifra";
            dataGridView1.Columns[4].Width = 150;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Trosak";
            dataGridView1.Columns[5].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Marza";
            dataGridView1.Columns[6].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Referencijala";
            dataGridView1.Columns[7].DefaultCellStyle.Format = "N1";

            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Prag";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Razlika";
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[9].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[9].Width = 80;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "VP Cena";
            dataGridView1.Columns[10].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[10].Width = 80;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Rabat";
            dataGridView1.Columns[11].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[11].Width = 80;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Marza Rabat";
            dataGridView1.Columns[12].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[12].Width = 80;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "VP cena rabat";
            dataGridView1.Columns[13].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[13].Width = 80;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Akcija";
            dataGridView1.Columns[14].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[14].Width = 80;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Marza Akcija";
            dataGridView1.Columns[15].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[15].Width = 80;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "VP Cena akcija";
            dataGridView1.Columns[16].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[16].Width = 80;

            ObojDatagrid();
        }
        private void ObojDatagrid()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                decimal pom = Convert.ToDecimal(row.Cells[9].Value);

                if (pom < 0)
                {
                    row.DefaultCellStyle.BackColor = Color.IndianRed ;
                    row.DefaultCellStyle.SelectionBackColor = Color.IndianRed;
                }
               

            }


        }

        private void ProsecneMarze()
        {
            double ProsecnaMarzaSvi = 0;
            double ProsecnaMarzaSelektovani = 0;
            double ProsecnaMarzaSviRabat = 0;
            double ProsecnaMarzaSelektovaniRabat = 0;
            double ProsecnaMarzaSviAkcija = 0;
            double ProsecnaMarzaSelektovaniAkcija = 0;
           
            int ProsecnaMarzaSviCounter= 0;
            int ProsecnaMarzaSelektovaniCounter = 0;

            int ProsecnaMarzaSviCounterRabat = 0;
            int ProsecnaMarzaSelektovaniCounterRabat = 0;

            int ProsecnaMarzaSviCounterAkcija = 0;
            int ProsecnaMarzaSelektovaniCounterAkcija = 0;


            if (devizni == 1)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    ProsecnaMarzaSvi = ProsecnaMarzaSvi + Convert.ToDouble(row.Cells[6].Value.ToString());
                    ProsecnaMarzaSviCounter = ProsecnaMarzaSviCounter + 1;
                    txtSA.Text = (ProsecnaMarzaSvi / ProsecnaMarzaSviCounter).ToString("0.##");

                    ProsecnaMarzaSviRabat = ProsecnaMarzaSviRabat + Convert.ToDouble(row.Cells[12].Value.ToString());
                    ProsecnaMarzaSviCounterRabat = ProsecnaMarzaSviCounterRabat + 1;
                    txtSA1.Text = (ProsecnaMarzaSviRabat / ProsecnaMarzaSviCounterRabat).ToString("0.##");

                    ProsecnaMarzaSviAkcija = ProsecnaMarzaSviAkcija + Convert.ToDouble(row.Cells[15].Value.ToString());
                    ProsecnaMarzaSviCounterAkcija = ProsecnaMarzaSviCounterAkcija + 1;
                    txtSA2.Text = (ProsecnaMarzaSviAkcija / ProsecnaMarzaSviCounterAkcija).ToString("0.##");

                    if (row.Selected)
                    {
                        ProsecnaMarzaSelektovani = ProsecnaMarzaSelektovani + Convert.ToDouble(row.Cells[6].Value.ToString());
                        ProsecnaMarzaSelektovaniCounter = ProsecnaMarzaSelektovaniCounter + 1;
                        txtSR.Text = (ProsecnaMarzaSelektovani / ProsecnaMarzaSelektovaniCounter).ToString("0.##");

                        ProsecnaMarzaSelektovaniRabat = ProsecnaMarzaSelektovaniRabat + Convert.ToDouble(row.Cells[12].Value.ToString());
                        ProsecnaMarzaSelektovaniCounterRabat = ProsecnaMarzaSelektovaniCounterRabat + 1;
                        txtSR1.Text = (ProsecnaMarzaSelektovaniRabat / ProsecnaMarzaSelektovaniCounterRabat).ToString("0.##");

                        ProsecnaMarzaSelektovaniAkcija = ProsecnaMarzaSelektovaniAkcija + Convert.ToDouble(row.Cells[15].Value.ToString());
                        ProsecnaMarzaSelektovaniCounterAkcija = ProsecnaMarzaSelektovaniCounterAkcija + 1;
                        txtSR2.Text = (ProsecnaMarzaSelektovaniAkcija / ProsecnaMarzaSelektovaniCounterAkcija).ToString("0.##");
                    }
                }

            }

            if (devizni == 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    ProsecnaMarzaSvi = ProsecnaMarzaSvi + Convert.ToDouble(row.Cells[6].Value.ToString());
                    ProsecnaMarzaSviCounter = ProsecnaMarzaSviCounter + 1;
                    txtSA.Text = (ProsecnaMarzaSvi / ProsecnaMarzaSviCounter).ToString();

                  //  ProsecnaMarzaSviRabat = ProsecnaMarzaSviRabat + Convert.ToDouble(row.Cells[12].Value.ToString());
                  //  ProsecnaMarzaSviCounterRabat = ProsecnaMarzaSviCounterRabat + 1;
                    txtSA1.Text = "0";

                    ProsecnaMarzaSviAkcija = ProsecnaMarzaSviAkcija + Convert.ToDouble(row.Cells[13].Value.ToString());
                    ProsecnaMarzaSviCounterAkcija = ProsecnaMarzaSviCounterAkcija + 1;
                    txtSA2.Text = (ProsecnaMarzaSviAkcija / ProsecnaMarzaSviCounterAkcija).ToString();

                    if (row.Selected)
                    {
                        ProsecnaMarzaSelektovani = ProsecnaMarzaSelektovani + Convert.ToDouble(row.Cells[6].Value.ToString());
                        ProsecnaMarzaSelektovaniCounter = ProsecnaMarzaSelektovaniCounter + 1;
                        txtSR.Text = (ProsecnaMarzaSelektovani / ProsecnaMarzaSelektovaniCounter).ToString();

                      //  ProsecnaMarzaSelektovaniRabat = ProsecnaMarzaSelektovaniRabat + Convert.ToDouble(row.Cells[12].Value.ToString());
                      //  ProsecnaMarzaSelektovaniCounterRabat = ProsecnaMarzaSelektovaniCounterRabat + 1;
                        txtSR1.Text = "0";

                        ProsecnaMarzaSelektovaniAkcija = ProsecnaMarzaSelektovaniAkcija + Convert.ToDouble(row.Cells[13].Value.ToString());
                        ProsecnaMarzaSelektovaniCounterAkcija = ProsecnaMarzaSelektovaniCounterAkcija + 1;
                        txtSR2.Text = (ProsecnaMarzaSelektovaniAkcija / ProsecnaMarzaSelektovaniCounterAkcija).ToString();
                    }
                }

            }


        }


        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            devizni = 0;
            string programi = "";
            int prviprogram = 0;

            string kolekcije = "";
            int prvakolekcija = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected)
                {
                    if (prviprogram == 0)
                    {
                        programi = row.Cells[0].Value.ToString();
                        prviprogram = prviprogram + 1;
                    }
                    else
                    {
                        programi = programi + "," + row.Cells[0].Value.ToString();
                        prviprogram = prviprogram + 1;
                    }
                }
            }

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected)
                {
                    if (prvakolekcija == 0)
                    {
                        kolekcije = row.Cells[0].Value.ToString();
                        prvakolekcija = prvakolekcija + 1;
                    }
                    else
                    {
                        kolekcije = kolekcije + "," + row.Cells[0].Value.ToString();
                        prvakolekcija = prvakolekcija + 1;
                    }
                }
            }
            var query = "Select KalkulatorStavke.ID,Program.Naziv, Kolekcije.Naziv, KalkulatorStavke.MPSifra, " +
" KalkulatorStavke.WSifra, KalkulatorStavke.TrosakProizvodnje, " +
" Round(((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)) - 1) * 100, 1) as Marza, " +
" Round(KalkulatorStavke.MarzaRef,1) as MarzaRef, Round(PragMarze,1) as PragMarze, " +
" Round(((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)) - 1) * 100 - PragMarze, 0) as RazlikaOdPraga, " +
" Round((KalkulatorStavke.VpCena * Kurs),0) as VpCena, " +
" KalkulatorStavke.Rabat," +
" KalkulatorStavke.Akcija, " +
" Round(KalkulatorStavke.Marza,1) as MarzaAkcija, " +
" Round(((KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Rabat * 0.01) - (KalkulatorStavke.VpCena * akcija * 0.01 * Rabat * 0.01)) - (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)),0) as Zarada, " +
" Round((KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Rabat * 0.01) - ((KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Rabat * 0.01))  * akcija * 0.01)),0) " +
"as VpAkcija,  " +
" Round((MpCena ),0) as MpCena " +
" from KalkulatorStavke " +
" inner join Program on KalkulatorStavke.ProgramID = Program.ID " +
" Inner " +
" join Kolekcije on KalkulatorStavke.KolekcijaID = Kolekcije.ID " +
" inner " +
" join Kalkulator on Kalkulator.ID = KalkulatorStavke.IDKalkulator " +
            " Where KalkulatorStavke.ProgramID in (" + programi + ") and KalkulatorStavke.KolekcijaID in (" + kolekcije + ") and KalkulatorStavke.IDKalkulator = " + kalkulator;

            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Program";
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Kolekcija";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Mp sifra";
            dataGridView1.Columns[3].Width = 70;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "WS Sifra";
            dataGridView1.Columns[4].Width = 150;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Trosak";
            dataGridView1.Columns[5].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Marza";
            dataGridView1.Columns[6].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Referencijala";
            dataGridView1.Columns[7].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Prag";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Razlika";
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[9].Width = 50;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "VP Cena";
            dataGridView1.Columns[10].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[10].Width = 80;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Rabat";
            dataGridView1.Columns[11].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[11].Width = 80;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Akcija";
            dataGridView1.Columns[12].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[12].Width = 80;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Marza Akcija";
            dataGridView1.Columns[13].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[13].Width = 80;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Zarada";
            dataGridView1.Columns[14].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[14].Width = 80;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "MP cena Akcija";
            dataGridView1.Columns[15].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[15].Width = 80;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Mp cena";
            dataGridView1.Columns[16].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[16].Width = 80;

            ObojDatagrid();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            ProsecneMarze();
        }
    }
}
