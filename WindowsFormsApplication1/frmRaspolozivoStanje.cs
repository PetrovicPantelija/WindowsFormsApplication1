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

namespace WindowsFormsApplication1
{
    public partial class frmRaspolozivoStanje : Form
    {
        public static string code = "frmRaspolozivoStanje";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public frmRaspolozivoStanje()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
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
        private void frmRaspolozivoStanje_Load(object sender, EventArgs e)
        {
            var select2 = " Select SkSifra, (Cast(SkSifra as nvarchar(3)) + '-' + SkNaziv) as SkNaziv from Sklad order by SkSifra";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboSkladiste1.DataSource = ds2.Tables[0];
            cboSkladiste1.DisplayMember = "SkNaziv";
            cboSkladiste1.ValueMember = "SkSifra";


            var select3 = " Select SkSifra, (Cast(SkSifra as nvarchar(3))  + '-' + SkNaziv) as SkNaziv from Sklad order by SkSifra";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboSkladiste2.DataSource = ds3.Tables[0];
            cboSkladiste2.DisplayMember = "SkNaziv";
            cboSkladiste2.ValueMember = "SkSifra";
        }

        private void RefreshDataGrid1()
        {
            var select = " SELECT   [MpSifra] ,[MPKod] ,[Skladiste1] " +
      " ,[Skladiste2] ,[Stanje] ,[Stanje1] as Isporuka1 " +
      " ,[Raspolozivo1] ,[Stanje2] as Isporuka2 " +
      " ,[Raspolozivo2] ,[Stanje3] as Isporuka3 " +
      " ,[Raspolozivo3] ,[Stanje4] as Isporuka4 " +
      " ,[Raspolozivo4] ,[Stanje5] as Isporuka5 " +
      " ,[Raspolozivo5] ,[Nedostaje] " +
      " ,[IzdatoRN] ,[IzdatiRN] " +
  " FROM [Jela_test].[dbo].[AnalizaRaspolozivosti]";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
           
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "MP";
            dataGridView1.Columns[0].Width = 60;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Mp kod";
            dataGridView1.Columns[1].Width = 150;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Skladište 1";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Skladište 2";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Stanje";
            dataGridView1.Columns[4].Width = 60;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Isporuka 1";
            dataGridView1.Columns[5].Width = 60;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Raspoloživo 1";
            dataGridView1.Columns[6].Width = 60;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Isporuka 2";
            dataGridView1.Columns[7].Width = 60;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Raspoloživo 2";
            dataGridView1.Columns[8].Width = 60;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Isporuka 3";
            dataGridView1.Columns[9].Width = 60;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Raspoloživo 3";
            dataGridView1.Columns[10].Width = 60;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Isporuka 4";
            dataGridView1.Columns[11].Width = 60;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Raspoloživo 4";
            dataGridView1.Columns[12].Width = 60;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Isporuka 5";
            dataGridView1.Columns[13].Width = 60;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Raspoloživo 5";
            dataGridView1.Columns[14].Width = 60;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Nedostaje";
            dataGridView1.Columns[15].Width = 70;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Izdato RN";
            dataGridView1.Columns[16].Width = 70;

            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Potrebno izdati RN";
            dataGridView1.Columns[17].Width = 70;
            
        }

        private void btnOsvezi_Click(object sender, EventArgs e)
        {
            Magacin.InsertRaspolozivostZaliha isp = new Magacin.InsertRaspolozivostZaliha();
            isp.InsAnalizaRasp1(Convert.ToInt32(cboSkladiste1.SelectedValue), Convert.ToInt32(cboSkladiste2.SelectedValue), dtpVremeStanje.Value);
            isp.InsAnalizaRasp2(dtpDatum1.Text);
            isp.InsAnalizaRasp3(dtpDatum22.Text);
            isp.InsAnalizaRasp4(dtpDatum3.Text);
            isp.InsAnalizaRasp5(dtpDatum4.Text);
            isp.InsAnalizaRasp6(dtpDatum5.Text);
           // Izbacio da probam
            isp.InsAnalizaRasp7();
            isp.InsAnalizaRasp8();

            RefreshDataGrid1();
                  
            MessageBox.Show("Zavrsena je obrada");
        }
    }
}
