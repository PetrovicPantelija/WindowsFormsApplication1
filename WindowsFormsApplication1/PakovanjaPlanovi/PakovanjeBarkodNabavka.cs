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

namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    public partial class PakovanjeBarkodNabavka : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        string Korisnik;
        public PakovanjeBarkodNabavka()
        {
            InitializeComponent();
        }

        public PakovanjeBarkodNabavka(string korisnik)
        {
            InitializeComponent();
            Korisnik = korisnik;
        }
        private void btnPoslatoCM_Click(object sender, EventArgs e)
        {
            FillGVCMOblak();
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


            int totalWidth = panel1.Width;
            int newDistance = Math.Max(0, totalWidth - 20);
            dataGridView2.Width = newDistance;


            int totalHeight = panel1.Height;
            int newHEight = Math.Max(0, totalHeight - 20);
            dataGridView2.Height = newHEight;
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
                frmPrebacivanjeMeđPrenosaRN pmp = new frmPrebacivanjeMeđPrenosaRN(BrojRN, 302, 110001, Korisnik);
                pmp.Show();

            }
            catch
            { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MSPZACMOBLAK();
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

            int totalWidth = panel1.Width;
            int newDistance = Math.Max(0, totalWidth - 20);
            dataGridView3.Width = newDistance;


            int totalHeight = panel1.Height;
            int newHEight = Math.Max(0, totalHeight - 20);
            dataGridView3.Height = newHEight;


        }

        private void FORMIRANMSPZACMOBLAK()
        {
            var query = " select Distinct PrometTransPos.BrRN , PrometTrans.PrTStDok as MSP, StigloNaCloudCM, PotvrdaPrijemCMOblak, Artikal, Kod, KolicinaZaProizvodnju " +
                " from PrometTransPos " +
" inner join PrometTrans on PrometTrans.PrTStDok = PrometTransPos.PrTpStDokumenta " +
" inner join StavkePlanaPakovanja on StavkePlanaPakovanja.RNBroj = PrometTransPos.BrRN " +
 " where PrTpSifVrDok = 'MSP' and PrometTrans.PrTStatPrTrans = 'OD' and StavkePlanaPakovanja.PoslatonaCM = 1 and StavkePlanaPakovanja.StigloNaCloudCM = 0";
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

        }



        private void PakovanjeBarkodNabavka_Load(object sender, EventArgs e)
        {
           
    }

        private void button24_Click(object sender, EventArgs e)
        {
            FORMIRANMSPZACMOBLAK();
        }
    }
}
