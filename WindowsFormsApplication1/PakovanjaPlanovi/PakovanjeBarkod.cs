using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.VariantTypes;
using Irony.Parsing;
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
using System.Web.Security;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    public partial class PakovanjeBarkod : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        string Korisnik = frmLogovanje.user.ToString().TrimEnd();

        //   1 PoslatonaCM  --- PAkovanje poslalo Zahtev CM
        // 2- StigloNaCloudCM CM - postavo ma Cloud
        // 5 -  PotvrdaPrijemCMOblak Potvrda Pakovanja da je OK na Cloudu CM
        // 3 - Potvrda da je prebaceno na StigloNaRoler1 0
        // 4 -  StigloNaRoler2 Potvrda da je Stiglo na Roler 2

        public PakovanjeBarkod()
        {
            InitializeComponent();
        }

        private bool panel1Maximized = false;

        private void btnMaxPanel1_Click(object sender, EventArgs e)
        {
            // Toggle: when maximizing panel1 set SplitterDistance to full width minus a small minimum for panel2
            if (!panel1Maximized)
            {
                // Minimize Panel2 by setting SplitterDistance to almost full width
                // Keep a small width for panel2 (e.g., 20 pixels)
                int totalWidth = splitContainer1.Width;
                int newDistance = Math.Max(0, totalWidth - 20);
                splitContainer1.SplitterDistance = newDistance;
                panel1Maximized = true;
            }
            else
            {
                // Restore to original/default split. Use a sensible default (e.g., 284 as in designer)
                splitContainer1.SplitterDistance = 284;
                panel1Maximized = false;
            }
        }

        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);
            var query = "select ID,Naziv from PlanoviPakovanja order by ID desc";
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboPlan.DataSource = ds.Tables[0];
            cboPlan.DisplayMember = "Naziv";
            cboPlan.ValueMember = "ID";


        }
        private void FillGV()
        {
            var query = "SELECT ID,IDPlanPakovanja,RNBroj," +
                 "CASE WHEN PoslatoNaCM > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as PoslatoNaCM," +
                 "CASE WHEN StigloNaCloudCM  > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as StigloNaCloudCM," +
                " StatusRN,VrstaRN,Artikal,Kod,KolicinaZaProizvodnju,PlaniraniKrajnjiDatumZavrsetka,Termin,Planer," +
                " RasporedPakovanja as Raspored,RTrim(Varijanta) as Varijanta " +
                "FROM StavkePlanaPakovanja " +
                "WHERE IDPlanPakovanja=" + Convert.ToInt32(cboPlan.SelectedValue) + " order by RNBroj asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 60;
            dataGridView1.Columns[4].Width = 60;
            dataGridView1.Columns[5].Width = 60;
            dataGridView1.Columns[6].Width = 60;
            dataGridView1.Columns[7].Width = 80;
            dataGridView1.Columns[8].Width = 180;


            // dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            // dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;



        }

        private void FillGVCMOblak()
        {
            var query = "   SELECT ID, IDPlanPakovanja, RNBroj, " +
" CASE WHEN PoslatoNaCM > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as PoslatoNaCM, " +
" CASE WHEN StigloNaCloudCM > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as StigloNaCloudCM, " +
" StatusRN,VrstaRN,Artikal,Kod,KolicinaZaProizvodnju,PlaniraniKrajnjiDatumZavrsetka,Termin,Planer,RasporedPakovanja as Raspored,RTrim(Varijanta) as Varijanta " +
" FROM StavkePlanaPakovanja " +
" WHERE PoslatoNaCM = 1 and StigloNaCloudCM = 0  order by RNBroj asc";
            // Nema formiran MSP
            /*
            " CASE WHEN PotvrdaPrijemCMOblak > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as CMOblak, " +
" CASE WHEN StigloNaRoler1 > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as StigloNaRoler1, " +
" CASE WHEN PotvrdaPrijemRoler1 > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Roler1, " +
" CASE WHEN StigloNaRoler2 > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as StigloNaRoler2, " +
" CASE WHEN PotvrdaPrijemRoler2 > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Roler2, " +
            */
            //" and  StavkePlanaPakovanja.RNBroj not in (Select Distinct BrRN from PrometTransPos where brRN is not null and PrTpSifVrDok = 'MSP')  ";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.Columns[0].Width = 60;
            dataGridView2.Columns[1].Visible = false;
            dataGridView2.Columns[2].Width = 80;
            dataGridView2.Columns[3].Width = 60;
            dataGridView2.Columns[4].Width = 60;
            dataGridView2.Columns[5].Width = 60;
            dataGridView2.Columns[6].Width = 60;
            dataGridView2.Columns[7].Width = 80;
            dataGridView2.Columns[8].Width = 180;
            //PANTA
            /*
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                string PostojiRN = VratiPostojiRN(row.Cells[2].Value.ToString());
                if (PostojiRN == "0")
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                    row.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightGray;
                }
            }
            */
            dataGridView2.Visible = true;
            dataGridView3.Visible = false;
            dataGridView4.Visible = false;

            int totalWidth = panel2.Width;
            int newDistance = Math.Max(0, totalWidth - 20);
            dataGridView2.Width = newDistance;


            int totalHeight = panel2.Height;
            int newHEight = Math.Max(0, totalHeight - 20);
            dataGridView2.Height = newHEight;

        }

       string VratiPostojiRN(string BrojRN)
        {
            string RNPostojiMSP = "";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Count(*) as Broj from PrometTransPos where BrRN = " + BrojRN, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                RNPostojiMSP =  dr["Broj"].ToString();
            }

            con.Close();
            return RNPostojiMSP;
        }

        private void FillGVCMNaOblaku()
        {
            var query = " SELECT Distinct ID, IDPlanPakovanja, RNBroj, " +
" CASE WHEN PoslatoNaCM > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as PoslatoNaCM, " +
" CASE WHEN StigloNaCloudCM > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as StigloNaCloudCM, " +
" CASE WHEN PotvrdaPrijemCMOblak > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as PotvrdaPrijemOblak, " +
" StatusRN,VrstaRN,Artikal,Kod,Naziv,KolicinaZaProizvodnju,PlaniraniKrajnjiDatumZavrsetka,Termin,Planer,RasporedPakovanja as Raspored,RTrim(Varijanta) as Varijanta " +
  " from PrometTransPos " +
" inner join PrometTrans on PrometTrans.PrTStDok = PrometTransPos.PrTpStDokumenta " +
" inner join StavkePlanaPakovanja on StavkePlanaPakovanja.RNBroj = PrometTransPos.BrRN " +
 " where PrTpSifVrDok = 'MSP' and PrometTrans.PrTStatPrTrans = 'PO' and  StigloNaCloudCM = 1 and PotvrdaPrijemCMOblak = 0  order by RNBroj asc";
            /*
            " CASE WHEN StigloNaRoler1 > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as StigloNaRoler1, " +
" CASE WHEN PotvrdaPrijemRoler1 > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Roler1, " +
" CASE WHEN StigloNaRoler2 > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as StigloNaRoler2, " +
" CASE WHEN PotvrdaPrijemRoler2 > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Roler2, " +
            */
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.Columns[0].Width = 60;
            dataGridView2.Columns[1].Visible = false;
            dataGridView2.Columns[2].Width = 80;
            dataGridView2.Columns[3].Width = 60;
            dataGridView2.Columns[4].Width = 60;
            dataGridView2.Columns[5].Width = 60;
            dataGridView2.Columns[6].Width = 60;
            dataGridView2.Columns[7].Width = 80;
            dataGridView2.Columns[8].Width = 180;

            dataGridView2.Visible = true;
            dataGridView3.Visible = false;
            dataGridView4.Visible = false;

            int totalWidth = panel2.Width;
            int newDistance = Math.Max(0, totalWidth - 20);
            dataGridView2.Width = newDistance;


            int totalHeight = panel2.Height;
            int newHEight = Math.Max(0, totalHeight - 20);
            dataGridView2.Height = newHEight;

        }

        private void FillGVCMNaOblakuPrihvaceno()
        {
            var query = "SELECT ID,IDPlanPakovanja,RNBroj, PrometTransPos.Kolicina2, KolicinaPreuzeta, PrometTransPos.PrTpOpomba, " +
                  "CASE WHEN PotvrdaPrijemCMOblak  > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as PotvrdjenoNaCMOblaku," +
                 "CASE WHEN StigloNaRoler1   > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as StigloNaRoler1," +
                " Artikal,Kod,Naziv,KolicinaZaProizvodnju,PlaniraniKrajnjiDatumZavrsetka,Termin,Planer,RasporedPakovanja as Raspored,RTrim(Varijanta) as Varijanta " +
                " from PrometTransPos " +
" inner join PrometTrans on PrometTrans.PrTStDok = PrometTransPos.PrTpStDokumenta " +
" inner join StavkePlanaPakovanja on StavkePlanaPakovanja.RNBroj = PrometTransPos.BrRN " +
 " where PrTpSifVrDok = 'MSP' and PrometTrans.PrTStatPrTrans = 'PO' and    PotvrdaPrijemCMOblak=1 and StigloNaRoler1 = 0  order by RNBroj desc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.Columns[0].Width = 60;
            dataGridView2.Columns[1].Visible = false;
            dataGridView2.Columns[2].Width = 80;
            dataGridView2.Columns[3].Width = 60;
            dataGridView2.Columns[4].Width = 60;
            dataGridView2.Columns[5].Width = 160;
            dataGridView2.Columns[6].Width = 60;
            dataGridView2.Columns[7].Width = 60;
            dataGridView2.Columns[8].Width = 180;

            dataGridView2.Visible = true;
            dataGridView3.Visible = false;
            dataGridView4.Visible = false;

            int totalWidth = panel2.Width;
            int newDistance = Math.Max(0, totalWidth - 20);
            dataGridView2.Width = newDistance;


            int totalHeight = panel2.Height;
            int newHEight = Math.Max(0, totalHeight - 20);
            dataGridView2.Height = newHEight;


            foreach (DataGridViewRow row in dataGridView2.Rows)
            {

                if (row.Cells[3].Value.ToString() == row.Cells[4].Value.ToString())
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                    // row.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightGreen;
                }
                else 
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                }
            }

        }

        private void MSPZACMOBLAK()
        {
            var query = " select Distinct PrometTransPos.BrRN , PrometTrans.PrTStDok as MSP, StigloNaCloudCM, PotvrdaPrijemCMOblak, Artikal, Kod, KolicinaZaProizvodnju " +
                " from PrometTransPos " +
" inner join PrometTrans on PrometTrans.PrTStDok = PrometTransPos.PrTpStDokumenta " +
" inner join StavkePlanaPakovanja on StavkePlanaPakovanja.RNBroj = PrometTransPos.BrRN " +
 " where PrTpSifVrDok = 'MSP' and PrometTrans.PrTStatPrTrans = 'OD' and StavkePlanaPakovanja.StigloNaCloudCM = 1 and StavkePlanaPakovanja.PotvrdaPrijemCMOblak = 0";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView3.ReadOnly = false;
            dataGridView3.DataSource = ds.Tables[0];

            /*

                  dataGridView2.Columns[0].ReadOnly = true;
                  dataGridView2.Columns[1].ReadOnly = true;
                  dataGridView2.Columns[2].ReadOnly = true;
                  dataGridView2.Columns[3].ReadOnly = true;
                  dataGridView2.Columns[4].ReadOnly = true;
                  dataGridView2.Columns[5].ReadOnly = true;
                  dataGridView2.Columns[6].ReadOnly = true;
                  dataGridView2.Columns[7].ReadOnly = true;
                  dataGridView2.Columns[9].ReadOnly = true;
                  dataGridView2.Columns[10].ReadOnly = true;
                  dataGridView2.Columns[11].ReadOnly = true;
            */

            dataGridView2.Visible = false;
            dataGridView3.Visible = true;
            dataGridView4.Visible = false;

            int totalWidth = panel2.Width;
            int newDistance = Math.Max(0, totalWidth - 20);
            dataGridView3.Width = newDistance;


            int totalHeight = panel2.Height;
            int newHEight = Math.Max(0, totalHeight - 20);
            dataGridView3.Height = newHEight;

        }

        private void MSPZARoler1()
        {
            var query = "select Distinct PrometTransPos.BrRN , PrometTrans.PrTStDok as MSP, StigloNaCloudCM, PotvrdaPrijemCMOblak, Artikal, Kod, KolicinaZaProizvodnju " +
               " from PrometTransPos " +
" inner join PrometTrans on PrometTrans.PrTStDok = PrometTransPos.PrTpStDokumenta " +
" inner join StavkePlanaPakovanja on StavkePlanaPakovanja.RNBroj = PrometTransPos.BrRN " +
 " where PrTpSifVrDok = 'MSP' and PrometTrans.PrTStatPrTrans = 'OD' and  StavkePlanaPakovanja.PotvrdaPrijemCMOblak = 1 and StavkePlanaPakovanja.StigloNaroler1 = 0 ";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView3.ReadOnly = false;
            dataGridView3.DataSource = ds.Tables[0];

            dataGridView2.Visible = false;
            dataGridView3.Visible = true;
            dataGridView4.Visible = false;

            int totalWidth = panel2.Width;
            int newDistance = Math.Max(0, totalWidth - 20);
            dataGridView3.Width = newDistance;


            int totalHeight = panel2.Height;
            int newHEight = Math.Max(0, totalHeight - 20);
            dataGridView3.Height = newHEight;

            /*

                  dataGridView2.Columns[0].ReadOnly = true;
                  dataGridView2.Columns[1].ReadOnly = true;
                  dataGridView2.Columns[2].ReadOnly = true;
                  dataGridView2.Columns[3].ReadOnly = true;
                  dataGridView2.Columns[4].ReadOnly = true;
                  dataGridView2.Columns[5].ReadOnly = true;
                  dataGridView2.Columns[6].ReadOnly = true;
                  dataGridView2.Columns[7].ReadOnly = true;
                  dataGridView2.Columns[9].ReadOnly = true;
                  dataGridView2.Columns[10].ReadOnly = true;
                  dataGridView2.Columns[11].ReadOnly = true;
            */

        }


        private void MSPZARolerRealizovani()
        {
            var query = "SELECT PrTpStDokumenta as MSP,PrTpIzdKol as Izdato, KolicinaPreuzeta as Preuzeto, PrTpSifMp as MP, PrTpStaraSifMp as Nayiv, PrTpEnotaMere as JM " +
" FROM PrometTransPos " +
" inner join PrometTrans on PrometTrans.PrTStDok = PrometTransPos.PrTpStDokumenta " +
" WHERE PrTpSifVrDok = 'MSP' and PRTStatPRTrans = 'OD' and PrometTransPos.PrTpSifSklad in (301, 303, 304) ";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView3.ReadOnly = false;
            dataGridView3.DataSource = ds.Tables[0];

            dataGridView3.Columns[0].Width = 60;
            dataGridView3.Columns[1].Width = 80;
            dataGridView3.Columns[2].Width = 80;
            dataGridView3.Columns[3].Width = 80;
            dataGridView3.Columns[4].Width = 160;
            dataGridView3.Columns[5].Width = 80;

            dataGridView2.Visible = false;
            dataGridView3.Visible = true;
            dataGridView4.Visible = false;

            int totalWidth = panel2.Width;
            int newDistance = Math.Max(0, totalWidth - 20);
            dataGridView3.Width = newDistance;


            int totalHeight = panel2.Height;
            int newHEight = Math.Max(0, totalHeight - 20);
            dataGridView3.Height = newHEight;

            /*

                  dataGridView2.Columns[0].ReadOnly = true;
                  dataGridView2.Columns[1].ReadOnly = true;
                  dataGridView2.Columns[2].ReadOnly = true;
                  dataGridView2.Columns[3].ReadOnly = true;
                  dataGridView2.Columns[4].ReadOnly = true;
                  dataGridView2.Columns[5].ReadOnly = true;
                  dataGridView2.Columns[6].ReadOnly = true;
                  dataGridView2.Columns[7].ReadOnly = true;
                  dataGridView2.Columns[9].ReadOnly = true;
                  dataGridView2.Columns[10].ReadOnly = true;
                  dataGridView2.Columns[11].ReadOnly = true;
            */

        }

        private void MSPZARoler2()
        {
            var query = "select PrometTransPos.BrRN , PrometTrans.PrTStDok as MSP, StigloNaCloudCM, PotvrdaPrijemCMOblak, Artikal, Kod, KolicinaZaProizvodnju from PrometTransPos " +
" inner join PrometTrans on PrometTrans.PrTStDok = PrometTransPos.PrTpStDokumenta " +
" inner join StavkePlanaPakovanja on StavkePlanaPakovanja.RNBroj = PrometTransPos.BrRN " +
" where PrTpSifVrDok = 'MSP' and PrometTrans.PrTStatPrTrans = 'OD' and  StavkePlanaPakovanja.PotvrdaPrijemCMOblak = 1 and StavkePlanaPakovanja.StigloNaRoler1 = 1 and StavkePlanaPakovanja.PotvrdaPrijemRoler1 = 1 And StavkePlanaPakovanja.PotvrdaPrijemRoler2 = 1 And StavkePlanaPakovanja.StigloNaRoler2 = 0";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView3.ReadOnly = false;
            dataGridView3.DataSource = ds.Tables[0];

            /*

                  dataGridView2.Columns[0].ReadOnly = true;
                  dataGridView2.Columns[1].ReadOnly = true;
                  dataGridView2.Columns[2].ReadOnly = true;
                  dataGridView2.Columns[3].ReadOnly = true;
                  dataGridView2.Columns[4].ReadOnly = true;
                  dataGridView2.Columns[5].ReadOnly = true;
                  dataGridView2.Columns[6].ReadOnly = true;
                  dataGridView2.Columns[7].ReadOnly = true;
                  dataGridView2.Columns[9].ReadOnly = true;
                  dataGridView2.Columns[10].ReadOnly = true;
                  dataGridView2.Columns[11].ReadOnly = true;
            */

        }

        private void FillGVCMPotvrdjenPrijemNaOblaku()
        {
            var query = "SELECT ID,IDPlanPakovanja,RNBroj," +
                 "CASE WHEN PoslatoNaCM > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as PoslatoNaCM," +
                   "CASE WHEN StigloNaCloudCM  > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as StigloNaCloudNaCM," +
                 "CASE WHEN StigloNaRoler1   > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as StigloNaRolere1," +
                 "CASE WHEN StigloNaRoler2   > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as StigloNaRolere2," +
                " StatusRN,VrstaRN,Artikal,Kod,Naziv,KolicinaZaProizvodnju,PlaniraniKrajnjiDatumZavrsetka,Termin,Planer,RasporedPakovanja as Raspored,RTrim(Varijanta) as Varijanta " +
                "FROM StavkePlanaPakovanja " +
                "WHERE IDPlanPakovanja=" + Convert.ToInt32(cboPlan.SelectedValue) + " and PoslatoNaCM = 1 and StigloNaCloudCM = 1 and PotvrdaPrijemCMOblak=1 and StigloNaRoler1 = 0  order by RNBroj asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.Columns[0].Width = 60;
            dataGridView2.Columns[1].Visible = false;
            dataGridView2.Columns[3].Width = 80;
            dataGridView2.Columns[7].Width = 300;
            dataGridView2.Columns[9].HeaderText = "KrajnjiDatum";

            dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;

            dataGridView2.Columns[0].ReadOnly = true;
            dataGridView2.Columns[1].ReadOnly = true;
            dataGridView2.Columns[2].ReadOnly = true;
            dataGridView2.Columns[3].ReadOnly = true;
            dataGridView2.Columns[4].ReadOnly = true;
            dataGridView2.Columns[5].ReadOnly = true;
            dataGridView2.Columns[6].ReadOnly = true;
            dataGridView2.Columns[7].ReadOnly = true;
            dataGridView2.Columns[9].ReadOnly = true;
            dataGridView2.Columns[10].ReadOnly = true;
            dataGridView2.Columns[11].ReadOnly = true;

        }

        private void PakovanjeBarkod_Load(object sender, EventArgs e)
        {
            FillCombo();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            FillGV();
        }

        private void btnKolicine_Click(object sender, EventArgs e)
        {
            PakovanjaPlanovi.InsertPakovanja ins = new InsertPakovanja();
            var result = MessageBox.Show("Da li ste sigurni da želite da pustite RN!", "Proglasi za CM", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Selected)
                        {
                            int id = Convert.ToInt32(row.Cells[0].Value);
                            ins.UpisiStatusPlanaPakovanja(id, 1);
                        }
                    }

                }
                catch
                { }

                FillGV();
            }
        }

        private void btnPoslatoCM_Click(object sender, EventArgs e)
        {
            FillGVCMOblak();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int BrojRN = 0;
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        BrojRN = Convert.ToInt32(row.Cells[2].Value);
                    }
                }
                //Ovde treba promeniti u Skladiste i lokacija za Cloud CM
                frmPrebacivanjeMeđPrenosaRN pmp = new frmPrebacivanjeMeđPrenosaRN(BrojRN, 302, 110001);
                pmp.Show();

            }
            catch
            { }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            // 5 - Znaci potvrda prijema da je stiglo na oblak od strane pakovanja
            PakovanjaPlanovi.InsertPakovanja ins = new InsertPakovanja();
            var result = MessageBox.Show("Da li ste sigurni da želite da pustite RN!", "Proglasi za CM", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Selected)
                        {
                            int id = Convert.ToInt32(row.Cells[0].Value);
                            ins.UpisiStatusPlanaPakovanja(id, 5);
                        }
                    }

                }
                catch
                { }

                // FillGV();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FillGVCMNaOblaku();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            FillGVCMNaOblakuPrihvaceno();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MSPZACMOBLAK();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PakovanjaPlanovi.InsertPakovanja ins = new InsertPakovanja();
            var result = MessageBox.Show("Da li ste sigurni da želite da pustite RN!", "Proglasi za CM", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Selected)
                        {
                            int id = Convert.ToInt32(row.Cells[0].Value);
                            ins.UpisiStatusPlanaPakovanja(id, 2);
                        }
                    }

                }
                catch
                { }

                // FillGV();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int BrojRN = 0;
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        BrojRN = Convert.ToInt32(row.Cells[2].Value);
                    }
                }
                frmPrebacivanjeMeđPrenosaRN pmp = new frmPrebacivanjeMeđPrenosaRN(BrojRN, 0, 0);
                pmp.Show();

            }
            catch
            { }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            MSPZARoler1();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            PakovanjaPlanovi.InsertPakovanja ins = new InsertPakovanja();
            var result = MessageBox.Show("Da li ste sigurni da želite da pustite RN!", "Proglasi za primljen na Roler 2", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Selected)
                        {
                            int id = Convert.ToInt32(row.Cells[0].Value);
                            ins.UpisiStatusPlanaPakovanja(id, 6);
                        }
                    }

                }
                catch
                { }
            }
        }

        private void FillGVPrijemNaRoler1()
        {
            var query = " sELECT * fROM (SELECT ID,IDPlanPakovanja,RNBroj, " +
" PrTpStDokumenta as MSP, " +
" PrTpSifMp as MP, PrTpStaraSifMp as Naziv, PrTpIzdKol as KOLICINA " +
" FROM StavkePlanaPakovanja " +
" inner join PrometTransPos on StavkePlanaPakovanja.RNBroj = PrometTransPos.BrRN " +
" inner join PrometTrans on PrometTrans.PrTStDok = PrometTransPos.PrTpStDokumenta " +
" WHERE PrTpSifVrDok = 'MSP' and PRTStatPRTrans = 'PO' and PrometTransPos.PrTpSifSklad = 302 AND " +
" PoslatoNaCM = 1 and PotvrdaPrijemCMOblak = 1 and StigloNaRoler1 = 1 and StigloNaRoler2 = 0 " +
" union all " +
" Select 0 as ID ,0 as IDPlanPakovanja ,0 as RNBroj, 0 as MSP, PRSifMp,RTrim(MpNaziv) as Naziv,(Sum(PrPrevKol) - Sum(PrIzdKol)) as Kolicina " +
  "                  From Promet  inner join MaticniPodatki on Promet.PrSifMp = MaticniPodatki.MpSifra " +
   "                 Where PrSifSklad = 305 And MaticniPodatki.MpOsnSklad in (301, 303, 304) " +
    "                Group by PrSifMp,MaticniPodatki.MpNaziv " +
     "               Having(SUM(PrPrevKol) - SUM(PrIzdKol)) != 0 ) AS t1 " +
      "              ORDER BY T1.rnbROJ";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.Columns[0].Width = 60;
            dataGridView2.Columns[1].Visible = false;
            dataGridView2.Columns[2].Width = 80;
            dataGridView2.Columns[3].Width = 80;
            dataGridView2.Columns[4].Width = 160;
            dataGridView2.Columns[5].Width = 80;
            

            dataGridView4.Visible = false;
            dataGridView3.Visible = false;
            dataGridView2.Visible = true;

            int totalWidth = panel2.Width;
            int newDistance = Math.Max(0, totalWidth - 20);
            dataGridView2.Width = newDistance;


            int totalHeight = panel2.Height;
            int newHEight = Math.Max(0, totalHeight - 20);
            dataGridView2.Height = newHEight;

        }
        private void button10_Click(object sender, EventArgs e)
        {
            FillGVPrijemNaRoler1();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int BrojRN = 0;
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        BrojRN = Convert.ToInt32(row.Cells[2].Value);
                    }
                }
                frmPrebacivanjeMeđPrenosaRN pmp = new frmPrebacivanjeMeđPrenosaRN(BrojRN, 0, 0);
                pmp.Show();

            }
            catch
            { }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            PakovanjaPlanovi.InsertPakovanja ins = new InsertPakovanja();
            var result = MessageBox.Show("Da li ste sigurni da želite da pustite RN!", "Proglasi za primljen na Roler 1", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Selected)
                        {
                            int id = Convert.ToInt32(row.Cells[0].Value);
                            ins.UpisiStatusPlanaPakovanja(id, 7);
                        }
                    }

                }
                catch
                { }
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            PakovanjaPlanovi.InsertPakovanja ins = new InsertPakovanja();
            var result = MessageBox.Show("Da li ste sigurni da želite da je stiglo na Roler 1!", "Proglasi Roler 1", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Selected)
                        {
                            int id = Convert.ToInt32(row.Cells[0].Value);
                            ins.UpisiStatusPlanaPakovanja(id, 3);
                        }
                    }

                }
                catch
                { }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            PakovanjaPlanovi.InsertPakovanja ins = new InsertPakovanja();
            var result = MessageBox.Show("Da li ste sigurni da želite da je stiglo na Roler 1!", "Proglasi Roler 1", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Selected)
                        {
                            int id = Convert.ToInt32(row.Cells[0].Value);
                            ins.UpisiStatusPlanaPakovanja(id, 4);
                        }
                    }

                }
                catch
                { }
            }
        }

        private void FillGVProizvodnjaCloud()
        {
            // Ovde sam yakucao skladista jer jos uvek ne znam gde zavrsava Proizvodnja
            //Pregled skladista iz proiyvodnje za Pakovanje
            var query = "  select    PrSifSklad as Skladiste, PrLokacija as LokSifra, LokOpis as Lokacija, " +
           " PrSifMp as MP, MpStaraSif as Kod,    MpNaziv as Naziv,  " +
           " sum(PrPrevKol - PrIzdKol) as Kolicina, " +
           " MPSifEnoteMere1 as JM    from Promet   " +
           " join MaticniPodatki on MPSifra = PrSifMP " +
           " inner join Lokac on Lokac.LokSifra = PrLokacija" +
           " Where  PrSifSklad In (301,302,303,304) and PrStZapisaVez = 0  group by PrSifSklad, PrLokacija" +
           "   ,PrSifMP, MpStaraSif, MpNaziv, MpDoNaziv, MPSifEnoteMere1, LokOpis having sum(PrPrevKol - PrIzdKol) <> 0 ";

           
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView4.ReadOnly = false;
            dataGridView4.DataSource = ds.Tables[0];

            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
            dataGridView4.Visible = true;

            int totalWidth = panel2.Width;
            int newDistance = Math.Max(0, totalWidth - 20);
            dataGridView4.Width = newDistance;


            int totalHeight = panel2.Height;
            int newHEight= Math.Max(0, totalHeight - 20);
            dataGridView4.Height = newHEight;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            FillGVProizvodnjaCloud();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int Skladiste = 0;
            try
            {
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Selected)
                    {
                        Skladiste = Convert.ToInt32(row.Cells[0].Value);
                    }
                }
                //Ovde treba promeniti u Skladiste i lokacija za Cloud CM
                frmPrebacivanjeMedjuskladisniPrenos pmp = new frmPrebacivanjeMedjuskladisniPrenos(Skladiste, 305);
                pmp.Show();

            }
            catch
            { }


         
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {
            MSPZARolerRealizovani();
        }

        private void MSPZARolerRealizovaniRoler2()
        {
            var query = "SELECT PrTpStDokumenta as MSP,PrTpIzdKol as Izdato, KolicinaPreuzeta as Preuzeto, PrTpSifMp as MP, PrTpStaraSifMp as Naziv, PrTpEnotaMere as JM " +
" FROM PrometTransPos " +
" inner join PrometTrans on PrometTrans.PrTStDok = PrometTransPos.PrTpStDokumenta " +
" inner join StavkePlanaPakovanja on StavkePlanaPakovanja.RNBroj = PrometTransPos.BrRN " +
" WHERE PrTpSifVrDok = 'MSP' and PRTStatPRTrans = 'OD' and PrometTransPos.PrTpSifSkladPr in (310,311,312,313) and StigloNaRoler2 = 0 ";

           // PredatoNaRoler2 = 0
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView3.ReadOnly = false;
            dataGridView3.DataSource = ds.Tables[0];

            dataGridView3.Columns[0].Width = 60;
            dataGridView3.Columns[1].Width = 80;
            dataGridView3.Columns[2].Width = 80;
            dataGridView3.Columns[3].Width = 80;
            dataGridView3.Columns[4].Width = 160;
            dataGridView3.Columns[5].Width = 80;

            dataGridView2.Visible = false;
            dataGridView3.Visible = true;
            dataGridView4.Visible = false;

            int totalWidth = panel2.Width;
            int newDistance = Math.Max(0, totalWidth - 20);
            dataGridView3.Width = newDistance;


            int totalHeight = panel2.Height;
            int newHEight = Math.Max(0, totalHeight - 20);
            dataGridView3.Height = newHEight;

            /*

                  dataGridView2.Columns[0].ReadOnly = true;
                  dataGridView2.Columns[1].ReadOnly = true;
                  dataGridView2.Columns[2].ReadOnly = true;
                  dataGridView2.Columns[3].ReadOnly = true;
                  dataGridView2.Columns[4].ReadOnly = true;
                  dataGridView2.Columns[5].ReadOnly = true;
                  dataGridView2.Columns[6].ReadOnly = true;
                  dataGridView2.Columns[7].ReadOnly = true;
                  dataGridView2.Columns[9].ReadOnly = true;
                  dataGridView2.Columns[10].ReadOnly = true;
                  dataGridView2.Columns[11].ReadOnly = true;
            */

        }

        private void MSPZARolerRealizovaniNePotvrdjeniRoler2()
        {
            var query = "SELECT PrTpStDokumenta as MSP,PrTpIzdKol as Izdato, KolicinaPreuzeta as Preuzeto, PrTpSifMp as MP, PrTpStaraSifMp as Naziv, PrTpEnotaMere as JM " +
" FROM PrometTransPos " +
" inner join PrometTrans on PrometTrans.PrTStDok = PrometTransPos.PrTpStDokumenta " +
" inner join StavkePlanaPakovanja on StavkePlanaPakovanja.RNBroj = PrometTransPos.BrRN " +
" WHERE PrTpSifVrDok = 'MSP' and PRTStatPRTrans = 'OD' and PrometTransPos.PrTpSifSkladPr in (310,311,312,313) and StigloNaRoler2 = 1 and PotvrdaPrijemRoler2=0 ";

            // PredatoNaRoler2 = 0
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView3.ReadOnly = false;
            dataGridView3.DataSource = ds.Tables[0];

            dataGridView3.Columns[0].Width = 60;
            dataGridView3.Columns[1].Width = 80;
            dataGridView3.Columns[2].Width = 80;
            dataGridView3.Columns[3].Width = 80;
            dataGridView3.Columns[4].Width = 160;
            dataGridView3.Columns[5].Width = 80;

            dataGridView2.Visible = false;
            dataGridView3.Visible = true;
            dataGridView4.Visible = false;

            int totalWidth = panel2.Width;
            int newDistance = Math.Max(0, totalWidth - 20);
            dataGridView3.Width = newDistance;


            int totalHeight = panel2.Height;
            int newHEight = Math.Max(0, totalHeight - 20);
            dataGridView3.Height = newHEight;

            /*

                  dataGridView2.Columns[0].ReadOnly = true;
                  dataGridView2.Columns[1].ReadOnly = true;
                  dataGridView2.Columns[2].ReadOnly = true;
                  dataGridView2.Columns[3].ReadOnly = true;
                  dataGridView2.Columns[4].ReadOnly = true;
                  dataGridView2.Columns[5].ReadOnly = true;
                  dataGridView2.Columns[6].ReadOnly = true;
                  dataGridView2.Columns[7].ReadOnly = true;
                  dataGridView2.Columns[9].ReadOnly = true;
                  dataGridView2.Columns[10].ReadOnly = true;
                  dataGridView2.Columns[11].ReadOnly = true;
            */

        }

        private void PripremaPakovanjeNePregledani()
        {
            var query = "SELECT PrTpStDokumenta as MSP,PrTpIzdKol as Izdato, KolicinaPreuzeta as Preuzeto, PrTpSifMp as MP, PrTpStaraSifMp as Naziv, PrTpEnotaMere as JM " +
" FROM PrometTransPos " +
" inner join PrometTrans on PrometTrans.PrTStDok = PrometTransPos.PrTpStDokumenta " +
" inner join StavkePlanaPakovanja on StavkePlanaPakovanja.RNBroj = PrometTransPos.BrRN " +
" WHERE PrTpSifVrDok = 'MSP' and PRTStatPRTrans = 'PO' and PrometTransPos.PrTpSifSkladPr in (310,311,312,313) and StigloNaRoler2 = 1 and PotvrdaPrijemRoler2=0 ";

            // PredatoNaRoler2 = 0
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView3.ReadOnly = false;
            dataGridView3.DataSource = ds.Tables[0];

            dataGridView3.Columns[0].Width = 60;
            dataGridView3.Columns[1].Width = 80;
            dataGridView3.Columns[2].Width = 80;
            dataGridView3.Columns[3].Width = 80;
            dataGridView3.Columns[4].Width = 160;
            dataGridView3.Columns[5].Width = 80;

            dataGridView2.Visible = false;
            dataGridView3.Visible = true;
            dataGridView4.Visible = false;

            int totalWidth = panel2.Width;
            int newDistance = Math.Max(0, totalWidth - 20);
            dataGridView3.Width = newDistance;


            int totalHeight = panel2.Height;
            int newHEight = Math.Max(0, totalHeight - 20);
            dataGridView3.Height = newHEight;

            /*

                  dataGridView2.Columns[0].ReadOnly = true;
                  dataGridView2.Columns[1].ReadOnly = true;
                  dataGridView2.Columns[2].ReadOnly = true;
                  dataGridView2.Columns[3].ReadOnly = true;
                  dataGridView2.Columns[4].ReadOnly = true;
                  dataGridView2.Columns[5].ReadOnly = true;
                  dataGridView2.Columns[6].ReadOnly = true;
                  dataGridView2.Columns[7].ReadOnly = true;
                  dataGridView2.Columns[9].ReadOnly = true;
                  dataGridView2.Columns[10].ReadOnly = true;
                  dataGridView2.Columns[11].ReadOnly = true;
            */

        }

        private void PripremaPakovanjePregledani()
        {
            var query = "SELECT PrTpStDokumenta as MSP,PrTpIzdKol as Izdato, KolicinaPreuzeta as Preuzeto, PrTpSifMp as MP, PrTpStaraSifMp as Naziv, PrTpEnotaMere as JM " +
" FROM PrometTransPos " +
" inner join PrometTrans on PrometTrans.PrTStDok = PrometTransPos.PrTpStDokumenta " +
" inner join StavkePlanaPakovanja on StavkePlanaPakovanja.RNBroj = PrometTransPos.BrRN " +
" WHERE PrTpSifVrDok = 'MSP' and PRTStatPRTrans = 'PO' and PrometTransPos.PrTpSifSkladPr in (310,311,312,313) and StigloNaRoler2 = 1 and PotvrdaPrijemRoler2=1 ";

            // PredatoNaRoler2 = 0
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView3.ReadOnly = false;
            dataGridView3.DataSource = ds.Tables[0];

            dataGridView3.Columns[0].Width = 60;
            dataGridView3.Columns[1].Width = 80;
            dataGridView3.Columns[2].Width = 80;
            dataGridView3.Columns[3].Width = 80;
            dataGridView3.Columns[4].Width = 160;
            dataGridView3.Columns[5].Width = 80;


            dataGridView2.Visible = false;
            dataGridView3.Visible = true;
            dataGridView4.Visible = false;

            int totalWidth = panel2.Width;
            int newDistance = Math.Max(0, totalWidth - 20);
            dataGridView3.Width = newDistance;


            int totalHeight = panel2.Height;
            int newHEight = Math.Max(0, totalHeight - 20);
            dataGridView3.Height = newHEight;

            /*

                  dataGridView2.Columns[0].ReadOnly = true;
                  dataGridView2.Columns[1].ReadOnly = true;
                  dataGridView2.Columns[2].ReadOnly = true;
                  dataGridView2.Columns[3].ReadOnly = true;
                  dataGridView2.Columns[4].ReadOnly = true;
                  dataGridView2.Columns[5].ReadOnly = true;
                  dataGridView2.Columns[6].ReadOnly = true;
                  dataGridView2.Columns[7].ReadOnly = true;
                  dataGridView2.Columns[9].ReadOnly = true;
                  dataGridView2.Columns[10].ReadOnly = true;
                  dataGridView2.Columns[11].ReadOnly = true;
            */

        }


        private void MSPZARolerRealizovaniRolerPotvrdjeni()
        {
            var query = "SELECT PrTpStDokumenta as MSP,PrTpIzdKol as Izdato, KolicinaPreuzeta as Preuzeto, PrTpSifMp as MP, PrTpStaraSifMp as Naziv, PrTpEnotaMere as JM " +
" FROM PrometTransPos " +
" inner join PrometTrans on PrometTrans.PrTStDok = PrometTransPos.PrTpStDokumenta " +
" WHERE PrTpSifVrDok = 'MSP' and PRTStatPRTrans = 'OD' and PrometTransPos.PrTpSifSkladPr in (310,311,312,313,314) ";

            // PredatoNaRoler2 = 1 and ProveroNaRoler2 = 0
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView3.ReadOnly = false;
            dataGridView3.DataSource = ds.Tables[0];

            dataGridView3.Columns[0].Width = 60;
            dataGridView3.Columns[1].Width = 80;
            dataGridView3.Columns[2].Width = 80;
            dataGridView3.Columns[3].Width = 80;
            dataGridView3.Columns[4].Width = 160;
            dataGridView3.Columns[5].Width = 80;

            dataGridView2.Visible = false;
            dataGridView3.Visible = true;
            dataGridView4.Visible = false;

            int totalWidth = panel2.Width;
            int newDistance = Math.Max(0, totalWidth - 20);
            dataGridView3.Width = newDistance;


            int totalHeight = panel2.Height;
            int newHEight = Math.Max(0, totalHeight - 20);
            dataGridView3.Height = newHEight;

            /*

                  dataGridView2.Columns[0].ReadOnly = true;
                  dataGridView2.Columns[1].ReadOnly = true;
                  dataGridView2.Columns[2].ReadOnly = true;
                  dataGridView2.Columns[3].ReadOnly = true;
                  dataGridView2.Columns[4].ReadOnly = true;
                  dataGridView2.Columns[5].ReadOnly = true;
                  dataGridView2.Columns[6].ReadOnly = true;
                  dataGridView2.Columns[7].ReadOnly = true;
                  dataGridView2.Columns[9].ReadOnly = true;
                  dataGridView2.Columns[10].ReadOnly = true;
                  dataGridView2.Columns[11].ReadOnly = true;
            */

        }

        private void button19_Click(object sender, EventArgs e)
        {
            MSPZARolerRealizovaniRoler2();
        }

        private void button18_Click_1(object sender, EventArgs e)
        {
            MSPZARolerRealizovaniNePotvrdjeniRoler2();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PripremaPakovanjeNePregledani();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            PripremaPakovanjePregledani();
        }
    }
}
