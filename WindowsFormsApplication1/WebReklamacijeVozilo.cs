using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace WindowsFormsApplication1
{
    public partial class WebReklamacijeVozilo : Form
    {
        Boolean prviulaz = true;
        int barkodrb = 1;
        public static string code = "WebReklamacijeVozilo";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public WebReklamacijeVozilo()
        {
            InitializeComponent();
            IdGrupe();
            IdForme();
            PravoPristupa();
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
        private void button1_Click(object sender, EventArgs e)
        {
            if (chkUcitanR.Checked == true)
                return;
            string connString =
          ConfigurationManager.AppSettings["webs"];

            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM RkShipping where  RkShipping.VoziloIDz  = (Select Min(VoziloID) from TrVozila where TrVozila.RegistarskiBroj = '" + cboVozila.Text + "') and DatumUtovara =  CDate('" + cboDatumUtovara.Text + "')", conn);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                adapter.Fill(dataTableRes);
                dataGridView1.DataSource = dataTableRes;
                DataGridViewColumn column = dataGridView1.Columns[0];
                dataGridView1.Columns[0].HeaderText = "Isporuka ID";
                dataGridView1.Columns[0].Width = 50;

                DataGridViewColumn column2 = dataGridView1.Columns[1];
                dataGridView1.Columns[1].HeaderText = "Barkod isporuke";
                dataGridView1.Columns[1].Width = 150;

                DataGridViewColumn column3 = dataGridView1.Columns[2];
                dataGridView1.Columns[2].HeaderText = "Datum utovara";
                dataGridView1.Columns[2].Width = 100;

                DataGridViewColumn column4 = dataGridView1.Columns[3];
                dataGridView1.Columns[3].HeaderText = "Datum istovara";
                dataGridView1.Columns[3].Width = 100;

                DataGridViewColumn column5 = dataGridView1.Columns[4];
                dataGridView1.Columns[4].HeaderText = "Broj utovara";
                dataGridView1.Columns[4].Width = 70;

                DataGridViewColumn column6 = dataGridView1.Columns[5];
                dataGridView1.Columns[5].HeaderText = "Broj istovara";
                dataGridView1.Columns[5].Width = 70;

                DataGridViewColumn column7 = dataGridView1.Columns[6];
                dataGridView1.Columns[6].HeaderText = "Broj palete";
                dataGridView1.Columns[6].Width = 70;

                DataGridViewColumn column8 = dataGridView1.Columns[7];
                dataGridView1.Columns[7].HeaderText = "Kupac";
                dataGridView1.Columns[7].Width = 70;

                DataGridViewColumn column9 = dataGridView1.Columns[8];
                dataGridView1.Columns[8].HeaderText = "Salon";
                dataGridView1.Columns[8].Width = 70;

                DataGridViewColumn column10 = dataGridView1.Columns[9];
                dataGridView1.Columns[9].HeaderText = "Vozilo";
                dataGridView1.Columns[9].Width = 70;

                DataGridViewColumn column11 = dataGridView1.Columns[10];
                dataGridView1.Columns[10].HeaderText = "Priključno vozilo";
                dataGridView1.Columns[10].Width = 70;

                DataGridViewColumn column12 = dataGridView1.Columns[11];
                dataGridView1.Columns[11].HeaderText = "Broj paketa";
                dataGridView1.Columns[11].Width = 70;

                DataGridViewColumn column13 = dataGridView1.Columns[12];
                dataGridView1.Columns[12].HeaderText = "Štampa";
                dataGridView1.Columns[12].Width = 70;

                DataGridViewColumn column14 = dataGridView1.Columns[13];
                dataGridView1.Columns[13].HeaderText = "Export Largo";
                dataGridView1.Columns[13].Width = 70;

                DataGridViewColumn column15 = dataGridView1.Columns[14];
                dataGridView1.Columns[14].HeaderText = "Export Largo - Može";
                dataGridView1.Columns[14].Width = 70;

                conn.Close();
            }
            /*
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd2 = new OleDbCommand("SELECT * FROM RkShippingItemRec inner join RkShipping on  RkShipping.ShippingID = RkShippingItemRec.ShipingIdz where  RkShipping.VoziloIDz  = (Select Min(VoziloID) from TrVozila where TrVozila.RegistarskiBroj = '" + cboVozila.Text + "') and DatumIstovara =  CDate('" + cboDatumUtovara.Text + "')", conn);

                conn.Open();
                DataTable dataTableResProizvodi = new DataTable();
                OleDbDataAdapter adapter2 = new OleDbDataAdapter(cmd2);

                adapter2.Fill(dataTableResProizvodi);
                dataGridView2.DataSource = dataTableResProizvodi;
                conn.Close();
            }
             * */
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                
                DataTable dataTableResPaketi = new DataTable();
                OleDbCommand cmd3 = new OleDbCommand("SELECT RkShippingItemRec.* FROM RkShippingItemRec inner join RkShipping on  RkShipping.ShippingID = RkShippingItemRec.ShipingIdz where RkShipping.VoziloIDz  = (Select Min(VoziloID) from TrVozila where TrVozila.RegistarskiBroj = '" + cboVozila.Text + "') and DatumUtovara =  CDate('" + cboDatumUtovara.Text + "') ", conn);

                conn.Open();

                OleDbDataAdapter adapter3 = new OleDbDataAdapter(cmd3);

                adapter3.Fill(dataTableResPaketi);
                dataGridView3.DataSource = dataTableResPaketi;
                conn.Close();
                chkUcitanR.Checked = true; 
            }

        
        }

        private void WebReklamacijeVozilo_Load(object sender, EventArgs e)
        {
            string connString =
              ConfigurationManager.AppSettings["webs"];

            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT Distinct(DatumUtovara) as DatumUtovara FROM RkShipping", conn);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                adapter.Fill(dataTableRes);
                cboDatumUtovara.DataSource = dataTableRes;
                cboDatumUtovara.DisplayMember = "DatumUtovara";
                cboDatumUtovara.ValueMember = "DatumUtovara";

                conn.Close();
            }

            prviulaz = false;
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (prviulaz == false)
            {
                string connString =
           ConfigurationManager.AppSettings["webs"];

                DataTable dataTableRes = new DataTable();

                using (OleDbConnection conn = new OleDbConnection(connString))
                {
                    OleDbCommand cmd = new OleDbCommand("SELECT Distinct(TrVozila.RegistarskiBroj) as RegistarskiBroj FROM RkShipping INNER JOIN TrVozila ON RkShipping.VoziloIDz = TrVozila.VoziloID where  DatumUtovara= CDate('" + cboDatumUtovara.Text + "')", conn);

                    conn.Open();

                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                    adapter.Fill(dataTableRes);
                    cboVozila.DataSource = dataTableRes;
                    cboVozila.DisplayMember = "RegistarskiBroj";
                    cboVozila.ValueMember = "RegistarskiBroj";

                    conn.Close();
                }

                prviulaz = false;

            }
        }

        private void btnInsertRefTabela_Click(object sender, EventArgs e)
        {
            if (chkPrenesenR.Checked == true)
                return;
            //InsertIntoReklamacije
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                //ProveriReklamacija
                var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
                SqlConnection myConnection = new SqlConnection(s_connection);
                using (SqlConnection conn = new SqlConnection(s_connection))
                using (SqlCommand cmd = new SqlCommand("dbo.ProveriPostojiReklamacijaID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // set up the parameters
                    cmd.Parameters.Add("@BrojReklamacije", SqlDbType.Int);
                    cmd.Parameters.Add("@BrojReklamacijePovratni", SqlDbType.Int).Direction = ParameterDirection.Output;

                    // set parameter values
                    cmd.Parameters["@BrojReklamacije"].Value = row.Cells[0].Value;

                    // open connection and execute stored procedure
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // read output value from @NewId
                    int broj = Convert.ToInt32(cmd.Parameters["@BrojReklamacijePovratni"].Value);
                    if (broj==0 && row != null && row.Cells[0].Value != null)
                         {
                    string pom = row.Cells[8].Value.ToString();
                    int isporucenokupcu = 0;
                    if (pom == "true")
                    {isporucenokupcu = 1;}
                    else
                    { isporucenokupcu = 0; }
                    InsertReklamacije Reklamacije = new InsertReklamacije();
                    Reklamacije.InsertReklam(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), row.Cells[4].Value.ToString(), Convert.ToDecimal(row.Cells[5].Value.ToString()), row.Cells[6].Value.ToString(), row.Cells[7].Value.ToString(), isporucenokupcu,Convert.ToInt32(row.Cells[12].Value.ToString()), Convert.ToDouble(row.Cells[13].Value.ToString()));
                }
                    conn.Close();
                }
               
            }
            chkPrenesenR.Checked = true;
            MessageBox.Show("Prenete su reklamacije za Vozilo");
        }

        private void cboDatumUtovara_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
