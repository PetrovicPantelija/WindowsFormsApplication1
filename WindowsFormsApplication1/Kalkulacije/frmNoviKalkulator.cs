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
    public partial class frmNoviKalkulator : Form
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        int kalk;
        string naz;
        int idK;
        public frmNoviKalkulator()
        {
            InitializeComponent();
            
        }
        public frmNoviKalkulator(int noviK,string naziv)
        {
            InitializeComponent();
            kalk = noviK;
            naz = naziv;
            FillCombo();
        }

        private void frmNoviKalkulator_Load(object sender, EventArgs e)
        {

        }
        private void FillCombo()
        {
            var query = " Select * from Kalkulator";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "Naziv";
            comboBox1.ValueMember = "ID";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_Ne_Click(object sender, EventArgs e)
        {
            Kalkulacije.frmKalkulatorStavke kalk = new frmKalkulatorStavke();
            kalk.Show();
        }

        private void btn_da_Click(object sender, EventArgs e)
        {
            idK = Convert.ToInt32(comboBox1.SelectedValue);
            var query = "select * from KalkulatorStavke where IDKalkulator="+idK;
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand(query, conn);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow row in dt.Rows)
            {
                int mpSifra = Convert.ToInt32(row["MPSifra"].ToString());
                decimal trosak = Convert.ToDecimal(row["TrosakProizvodnje"].ToString());
                decimal avans = Convert.ToDecimal(row["Avans"].ToString());
                decimal provizija = Convert.ToDecimal(row["Provizija"].ToString());
                decimal transport = Convert.ToDecimal(row["Transport"].ToString());
                decimal vp = Convert.ToDecimal(row["VpCena"].ToString());
                decimal rabat = Convert.ToDecimal(row["Rabat"].ToString());
                decimal marza = Convert.ToDecimal(row["Marza"].ToString());
                decimal akcija = Convert.ToDecimal(row["Akcija"].ToString());
                decimal mp = Convert.ToDecimal(row["MpCena"].ToString());

                Kalkulacije.InsertKalkulatorStavke klk = new InsertKalkulatorStavke();
                klk.InsKalkulatorStavke(mpSifra, kalk, trosak, avans, provizija, transport, vp, rabat, marza, akcija, mp);
            }
            Kalkulacije.frmKalkulatorStavke stavke = new frmKalkulatorStavke(kalk, naz);
            stavke.Show();
        }
    }
}
