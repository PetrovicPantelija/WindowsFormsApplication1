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
        public bool PravoP;
        decimal Kurs = 0;
        public static string code = "frmNoviKalkulator";
        public bool Pravo;
        int idGrupe;
        decimal kurs = 0;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public frmNoviKalkulator()
        {
            InitializeComponent();
            IdGrupe();
            IdForme();
            PravoPristupa();

        }
        public frmNoviKalkulator(int noviK,string naziv, string sKurs)
        {
            InitializeComponent();
            kalk = noviK;
            naz = naziv;
            Kurs = Convert.ToDecimal(sKurs);
            FillCombo();
        }
        public string IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            //Sifarnici.frmLogovanje frm = new Sifarnici.frmLogovanje();         
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            int count = 0;

            while (dr.Read())
            {
                if (count == 0)
                {
                    niz = dr["IdGrupe"].ToString();
                    count++;
                }
                else
                {
                    niz = niz + "," + dr["IdGrupe"].ToString();
                    count++;
                }

            }
            conn.Close();
            return niz;
        }
        private int IdForme()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select IdForme from Forme where Rtrim(Code)=" + "'" + code + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idForme = Convert.ToInt32(dr["IdForme"].ToString());
            }
            conn.Close();
            return idForme;
        }

        private void PravoPristupa()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select * From GrupaForma Where IdGrupe in (" + niz + ") and IdForme=" + idForme;
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nemate prava za pristup ovoj formi", code);
                Pravo = false;
            }
            else
            {
                Pravo = true;
                while (reader.Read())
                {
                    insert = Convert.ToBoolean(reader["Upis"]);
                    if (insert == false)
                    {
                        //tsNew.Enabled = false;
                    }
                    update = Convert.ToBoolean(reader["Izmena"]);
                    if (update == false)
                    {
                        //tsSave.Enabled = false;
                    }
                    delete = Convert.ToBoolean(reader["Brisanje"]);
                    if (delete == false)
                    {
                        //tsDelete.Enabled = false;
                    }
                }
            }

            conn.Close();
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
            PravoP = kalk.Pravo;
            if (PravoP == true) { kalk.Show(); } else { return; }
            
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
                
                //klk.InsKalkulatorStavke(mpSifra, kalk, trosak, avans, provizija, transport, vp, rabat, marza, akcija, mp);
            }
            Kalkulacije.frmKalkulatorStavke stavke = new frmKalkulatorStavke(kalk, naz, Kurs.ToString(),1);
            stavke.Show();
        }
    }
}
