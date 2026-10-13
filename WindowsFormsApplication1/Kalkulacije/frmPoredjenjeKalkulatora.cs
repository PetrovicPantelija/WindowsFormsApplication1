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
    public partial class frmPoredjenjeKalkulatora : Form
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public frmPoredjenjeKalkulatora()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
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

        private void frmPoredjenjeKalkulatora_Load(object sender, EventArgs e)
        {
            var query = " Select * from Kalkulator";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            combo_Zaglavlje.DataSource = ds.Tables[0];
            combo_Zaglavlje.DisplayMember = "Naziv";
            combo_Zaglavlje.ValueMember = "ID";

            var query2 = " Select * from Kalkulator";
            SqlConnection conn2 = new SqlConnection(connect);
            var da2 = new SqlDataAdapter(query2, conn2);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            combo_Zaglavlje1.DataSource = ds2.Tables[0];
            combo_Zaglavlje1.DisplayMember = "Naziv";
            combo_Zaglavlje1.ValueMember = "ID";

            PrikaziProgrameIKolekcije();
        }

        private void btn_Rabat_Click(object sender, EventArgs e)
        {
            var query = " SELECT     dbo.KalkulatorStavke.IDKalkulator, KalkulatorStavke_1.IDKalkulator AS IDKalkulator2, dbo.KalkulatorStavke.MPSifra, dbo.KalkulatorStavke.WSifra, " +
                       " dbo.KalkulatorStavke.TrosakProizvodnje, KalkulatorStavke_1.TrosakProizvodnje AS TrosakProizvodnje2, " +
                       " (dbo.KalkulatorStavke.TrosakProizvodnje - KalkulatorStavke_1.TrosakProizvodnje) as RazlikaTrosak, " +
                       " ((dbo.KalkulatorStavke.TrosakProizvodnje / KalkulatorStavke_1.TrosakProizvodnje)) as KoeficijentPromeneTroska, " +
" dbo.KalkulatorStavke.Marza, " +
                     "  KalkulatorStavke_1.Marza AS Marza2, " +
" (dbo.KalkulatorStavke.Marza - KalkulatorStavke_1.Marza) as RazlikaMarza, " +
                      " dbo.KalkulatorStavke.VpCena, KalkulatorStavke_1.VpCena AS VPCena2, " +
" (dbo.KalkulatorStavke.VpCena - KalkulatorStavke_1.VpCena) as RazlikaCena, " +
" ((dbo.KalkulatorStavke.VpCena / KalkulatorStavke_1.VpCena)) as KoeficijentPromeneCene, " +
" dbo.KalkulatorStavke.Rabat, " +
                     "  KalkulatorStavke_1.Rabat AS Rabat2, " +
" (dbo.KalkulatorStavke.Rabat - " +
                    "   KalkulatorStavke_1.Rabat) as RazlikaRabat, " +
"  dbo.KalkulatorStavke.Akcija, KalkulatorStavke_1.Akcija AS Akcija2, " +
" (dbo.KalkulatorStavke.Akcija - KalkulatorStavke_1.Akcija) as RazlikaAkcija " +
" FROM dbo.KalkulatorStavke INNER JOIN " +
                    "   dbo.KalkulatorStavke AS KalkulatorStavke_1 ON dbo.KalkulatorStavke.MPSifra = KalkulatorStavke_1.MPSifra " +
"  WHERE (dbo.KalkulatorStavke.IDKalkulator = " + Convert.ToInt32(combo_Zaglavlje1.SelectedValue) + ") AND (KalkulatorStavke_1.IDKalkulator = " + Convert.ToInt32(combo_Zaglavlje.SelectedValue) + ") ";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            string value = dataGridView1.Rows[0].Cells[0].Value.ToString();

           
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID KAL1";
            dataGridView1.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "ID KAL2";
            dataGridView1.Columns[1].Width = 20;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "MPSifra";
            dataGridView1.Columns[2].Width = 70;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "WSifra";
            dataGridView1.Columns[3].Width = 100;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Trosak K1";
            dataGridView1.Columns[4].Width = 70;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Trosak K2";
            dataGridView1.Columns[5].Width = 70;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Razlika";
            dataGridView1.Columns[6].Width = 50;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Promena";
            dataGridView1.Columns[7].Width = 70;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Marza K1";
            dataGridView1.Columns[8].Width = 70;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Marza K2";
            dataGridView1.Columns[9].Width = 70;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "RAZ M";
            dataGridView1.Columns[10].Width = 50;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "VP K1";
            dataGridView1.Columns[11].Width = 70;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "VP K2";
            dataGridView1.Columns[12].Width = 70;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "RAZ VP";
            dataGridView1.Columns[13].Width = 50;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Prom VP";
            dataGridView1.Columns[14].Width = 60;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int ProgramID = 0;
            int KolekcijaID = 0;

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected == true)
                {
                    ProgramID = Convert.ToInt32(dataGridView3.Rows[0].Cells[0].Value.ToString());
                }
            }

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected == true)
                {
                    KolekcijaID = Convert.ToInt32(dataGridView2.Rows[0].Cells[0].Value.ToString());
                }
            }


            var query = " SELECT     dbo.KalkulatorStavke.IDKalkulator, KalkulatorStavke_1.IDKalkulator AS IDKalkulator2, dbo.KalkulatorStavke.MPSifra, dbo.KalkulatorStavke.WSifra, " +
                     " dbo.KalkulatorStavke.TrosakProizvodnje, KalkulatorStavke_1.TrosakProizvodnje AS TrosakProizvodnje2, " +
                     " (dbo.KalkulatorStavke.TrosakProizvodnje - KalkulatorStavke_1.TrosakProizvodnje) as RazlikaTrosak, " +
                     " ((dbo.KalkulatorStavke.TrosakProizvodnje / KalkulatorStavke_1.TrosakProizvodnje)) as KoeficijentPromeneTroska, " +
" dbo.KalkulatorStavke.Marza, " +
                   "  KalkulatorStavke_1.Marza AS Marza2, " +
" (dbo.KalkulatorStavke.Marza - KalkulatorStavke_1.Marza) as RazlikaMarza, " +
                    " dbo.KalkulatorStavke.VpCena, KalkulatorStavke_1.VpCena AS VPCena2, " +
" (dbo.KalkulatorStavke.VpCena - KalkulatorStavke_1.VpCena) as RazlikaCena, " +
" ((dbo.KalkulatorStavke.VpCena / KalkulatorStavke_1.VpCena)) as KoeficijentPromeneCene, " +
" dbo.KalkulatorStavke.Rabat, " +
                   "  KalkulatorStavke_1.Rabat AS Rabat2, " +
" (dbo.KalkulatorStavke.Rabat - " +
                  "   KalkulatorStavke_1.Rabat) as RazlikaRabat, " +
"  dbo.KalkulatorStavke.Akcija, KalkulatorStavke_1.Akcija AS Akcija2, " +
" (dbo.KalkulatorStavke.Akcija - KalkulatorStavke_1.Akcija) as RazlikaAkcija " +
" FROM dbo.KalkulatorStavke INNER JOIN " +
                  "   dbo.KalkulatorStavke AS KalkulatorStavke_1 ON dbo.KalkulatorStavke.MPSifra = KalkulatorStavke_1.MPSifra " +
"  WHERE (dbo.KalkulatorStavke.IDKalkulator = " + Convert.ToInt32(combo_Zaglavlje1.SelectedValue) + ") AND (KalkulatorStavke_1.IDKalkulator = " + Convert.ToInt32(combo_Zaglavlje.SelectedValue) + ") and KalkulatorStavke.ProgramID = " + ProgramID + " KalkulatorStavke.KolekcijaID = " + KolekcijaID ;
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            string value = dataGridView1.Rows[0].Cells[0].Value.ToString();


            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID KAL1";
            dataGridView1.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "ID KAL2";
            dataGridView1.Columns[1].Width = 20;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "MPSifra";
            dataGridView1.Columns[2].Width = 70;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "WSifra";
            dataGridView1.Columns[3].Width = 100;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Trosak K1";
            dataGridView1.Columns[4].Width = 70;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Trosak K2";
            dataGridView1.Columns[5].Width = 70;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Razlika";
            dataGridView1.Columns[6].Width = 50;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Promena";
            dataGridView1.Columns[7].Width = 70;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Marza K1";
            dataGridView1.Columns[8].Width = 70;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Marza K2";
            dataGridView1.Columns[9].Width = 70;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "RAZ M";
            dataGridView1.Columns[10].Width = 50;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "VP K1";
            dataGridView1.Columns[11].Width = 70;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "VP K2";
            dataGridView1.Columns[12].Width = 70;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "RAZ VP";
            dataGridView1.Columns[13].Width = 50;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Prom VP";
            dataGridView1.Columns[14].Width = 60;
        }
    }
}
