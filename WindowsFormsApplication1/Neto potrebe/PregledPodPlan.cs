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

namespace WindowsFormsApplication1.Neto_potrebe
{
    public partial class PregledPodPlan : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        int tmpPlanID = 0;
        string tmpPodPlan = "";
        public PregledPodPlan()
        {
            InitializeComponent();
        }

        public PregledPodPlan(int PlanID, string PodPlan)
        {
            InitializeComponent();
            tmpPlanID = PlanID;
            tmpPodPlan = PodPlan;
        }

        private void FillPlanovi()
        {
            var query = "Select ID,Naziv From PlanArhiv order by ID asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboPlan.DataSource = ds.Tables[0];
            cboPlan.DisplayMember = "Naziv";
            cboPlan.ValueMember = "ID";
        }

        private void PregledPodPlan_Load(object sender, EventArgs e)
        {
            FillPlanovi();
            txtPodPlan.Text = tmpPodPlan;
            cboPlan.SelectedValue = tmpPlanID;
            FillGV();
        }

        private void FillGV()
        {
            var query = @"select NetoPotrebeIgor.ID, NetoPotrebeIGor.IDPlana, Podplan, Mp,MpStaraSif,Kolicina,  PlaniranaKolicina, (select Sum(DNpKolZaIzdelavo) from DelNalogProizvodni where DNpStvProjekt = " + Convert.ToInt32(cboPlan.SelectedValue) + " and DnPSifMp = Mp) as RNKol, (Kolicina -  (select Sum(DNpKolZaIzdelavo) from DelNalogProizvodni where DNpStvProjekt = " + Convert.ToInt32(cboPlan.SelectedValue) + " and DnPSifMp = Mp) ) as Preostala, 0 as Rezervisano, " +
                " (select   sum(PrPrevKol - PrIzdKol)[Stv.kol]   from Promet   inner join MaticniPodatki on MPSifra = PrSifMP " +
 " where PrSifMp = Mp   ) as StanjeNaMAg, " +
" (select  Rtrim(PDnNazivDP) + ' ' + Cast(EUKolDobrih as nvarchar(30)) as Operacije from DelNalogProizvodni " +
" inner join EvidencaUR on EuStDelNaloga = DelNalogProizvodni.DNpStevilka " +
" inner join PotekDelovnegaNaloga on PDnStZapisa = EUStZapPotekaDN " +
" where DNpStvProjekt =  " + Convert.ToInt32(cboPlan.SelectedValue) + " and DnPSifMp = MP )  as Operacije" +
 " From NetoPotrebeIgor inner join MaticniPodatki on NetoPotrebeIgor.MP=MaticniPodatki.MpSifra " +
" where  IdPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + " and NetoPotrebeIgor.PodPlan  Like ('%" + txtPodPlan.Text + "%') order by NetoPotrebeIgor.id asc";


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
            dataGridView1.Columns[1].HeaderText = "IDPlana";
            dataGridView1.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Podplan";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Mp";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Code";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Kolicina";
            dataGridView1.Columns[5].Width = 90;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Planirana kol";
            dataGridView1.Columns[6].Width = 90;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "RN kol";
            dataGridView1.Columns[7].Width = 90;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Preostala";
            dataGridView1.Columns[8].Width = 90;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Rezervisano";
            dataGridView1.Columns[9].Width = 90;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Stanje";
            dataGridView1.Columns[10].Width = 90;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Po operacijama";
            dataGridView1.Columns[11].Width = 290;

        }

        private void FillGV2(int MP)
        {
            var query = @"select
  PrSifSklad[Sif.skl.], 
  PrSifMp[Sif.Mp.], MpStaraSif[Code],
  MpNaziv[Naziv MP], 
  sum(PrPrevKol - PrIzdKol)[Stv.kol],
   MPSifEnoteMere1['JM']
  from Promet
  inner join MaticniPodatki on MPSifra = PrSifMP
  where PrSifMp =  " + MP + " group by PrSifSklad, PrSifMp, MpStaraSif,MpNaziv,MPSifEnoteMere1"
  ;


            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];
            /*
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "IDPlana";
            dataGridView1.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Podplan";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Mp";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Code";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Kolicina";
            dataGridView1.Columns[5].Width = 90;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Planirana kol";
            dataGridView1.Columns[6].Width = 90;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "RN kol";
            dataGridView1.Columns[7].Width = 90;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Preostala";
            dataGridView1.Columns[8].Width = 90;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Rezervisano";
            dataGridView1.Columns[9].Width = 90;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Stanje";
            dataGridView1.Columns[10].Width = 90;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Po operacijama";
            dataGridView1.Columns[11].Width = 290;
            */

        }

        private void FillGV3(int MP, decimal Kol)
        {
            var query = @"select
    KoPodSifMp,
    MpNaziv + ' ' + ISNULL(MpDoNaziv,'') as NazivMP,
    MpSifEnoteMere1,
    convert(numeric(22,4),
        (sum(KoKolMateriala * 30 * (1 + (KoIzmet / 100.0))))/30
    ) * " + Kol + "  as KoKolMateriala, " +
   " ISNULL(MpOsnSklad, 0) as MpOsnSklad, " +
   " ISNULL(MpOsnLokacija, '') as MpOsnLokacija " +
" from kosovnica, maticnipodatki " +
" where KoNadSifMp =  " + MP + "   and KoVarianta = '*'   and KoPodSifMp = MpSifra   and KoSifStat = 'A' group by     KoPodSifMP,    MpNaziv + ' ' + ISNULL(MpDoNaziv,''),     MpSifEnoteMere1,    ISNULL(MpOsnSklad, 0),     ISNULL(MpOsnLokacija, '') "
  ;


            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];
            /*
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "IDPlana";
            dataGridView1.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Podplan";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Mp";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Code";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Kolicina";
            dataGridView1.Columns[5].Width = 90;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Planirana kol";
            dataGridView1.Columns[6].Width = 90;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "RN kol";
            dataGridView1.Columns[7].Width = 90;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Preostala";
            dataGridView1.Columns[8].Width = 90;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Rezervisano";
            dataGridView1.Columns[9].Width = 90;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Stanje";
            dataGridView1.Columns[10].Width = 90;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Po operacijama";
            dataGridView1.Columns[11].Width = 290;
            */

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    FillGV2(Convert.ToInt32(row.Cells[3].Value.ToString()));
                    FillGV3(Convert.ToInt32(row.Cells[3].Value.ToString()), Convert.ToDecimal(row.Cells[5].Value.ToString()));
                } 
            }
        }
    }
}
