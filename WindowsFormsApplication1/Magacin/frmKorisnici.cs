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

using Microsoft.Reporting.WinForms;

namespace WindowsFormsApplication1.Magacin
{
    public partial class frmKorisnici : Form
    {
        bool status = false;
        public static string code = "frmKorisnici";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        public frmKorisnici()
        {
            InitializeComponent();
            IdGrupe();
            IdForme();
            PravoPristupa();
        }
        public int IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            //Sifarnici.frmLogovanje frm = new Sifarnici.frmLogovanje();         
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idGrupe = Convert.ToInt32(dr["IdGrupe"].ToString());
            }
            conn.Close();
            return idGrupe;
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
            string query = "Select * From GrupaForma Where IdGrupe=" + idGrupe + " and IdForme=" + idForme;
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
                        tsNew.Enabled = false;
                    }
                    update = Convert.ToBoolean(reader["Izmena"]);
                    if (update == false)
                    {
                        tsSave.Enabled = false;
                    }
                    delete = Convert.ToBoolean(reader["Brisanje"]);
                    if (delete == false)
                    {
                        tsDelete.Enabled = false;
                    }
                }
            }

            conn.Close();
        }


        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
          
            txtPassword.Text = "";
            txtMagacini.Text = "";
            txtSifraLargo.Text = "";
        }

        private void RefreshDataGRid()
        {

            var select = "  select Korisnik, SifraLargo from Korisnici";


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
            dataGridView1.Columns[0].HeaderText = "Korisnik";
            dataGridView1.Columns[0].Width = 200;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Sifra largo";
            dataGridView1.Columns[1].Width = 100;

        }

        private void tsSave_Click(object sender, EventArgs e)
        {
          

            if (status == true)
            {
                InsertAdministracija ins = new InsertAdministracija();
                ins.InsKorisnik(cboKorisnik.SelectedValue.ToString(), txtPassword.Text, Convert.ToInt32(txtSifraLargo.Text), Convert.ToInt32(cboOrganizacionaJedinica.SelectedValue), txtMagacini.Text);
                MessageBox.Show("Insert");
                status = false;
            }
            else
            {
                InsertAdministracija upd = new InsertAdministracija();
                upd.UpdKorisnik(cboKorisnik.SelectedValue.ToString(), txtPassword.Text, Convert.ToInt32(txtSifraLargo.Text), Convert.ToInt32(cboOrganizacionaJedinica.SelectedValue), txtMagacini.Text);
            }
            RefreshDataGRid();
        }

        private void frmKorisnici_Load(object sender, EventArgs e)
        {
            var select2 = " Select Korisnik from Korisnici ";

            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboKorisnik.DataSource = ds2.Tables[0];
            cboKorisnik.DisplayMember = "Korisnik";
            cboKorisnik.ValueMember = "Korisnik";

            var select3 = " Select ID, Naziv from OrganizacionaJedinica";

            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboOrganizacionaJedinica.DataSource = ds3.Tables[0];
            cboOrganizacionaJedinica.DisplayMember = "Naziv";
            cboOrganizacionaJedinica.ValueMember = "ID";

            RefreshDataGRid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertAdministracija ins = new InsertAdministracija();
            ins.DeleteKorisnik(cboKorisnik.SelectedValue.ToString());
            status = false;
            RefreshDataGRid();
        }

        private void VratiPodatke(string ID)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [Korisnik]      ,[Password] " +
          " ,[SifraLargo]      ,[OrganizacionaJedinica]      ,[Magacini]  FROM [Jela_test].[dbo].[Korisnici] " +
            "  where Rtrim(Korisnik) ='" + ID.Trim() + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                cboKorisnik.SelectedValue = dr["Korisnik"].ToString().Trim();
                txtPassword.Text = dr["Password"].ToString().Trim();
                txtSifraLargo.Text = dr["SifraLargo"].ToString();
                cboOrganizacionaJedinica.SelectedValue = Convert.ToInt32(dr["OrganizacionaJedinica"].ToString());
                txtMagacini.Text = dr["Magacini"].ToString();
            }

            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        cboKorisnik.SelectedValue = row.Cells[0].Value.ToString().Trim();
                        VratiPodatke(cboKorisnik.SelectedValue.ToString().Trim());
                        // txtOpis.Text = row.Cells[1].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }
    }
}
