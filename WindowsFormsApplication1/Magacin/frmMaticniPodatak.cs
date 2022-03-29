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
    public partial class frmMaticniPodatak : Form
    {
        bool status = false;
        public static string code = "frmMaticniPodatak";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public frmMaticniPodatak()
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
            txtRegalniRed.Text = "";
        }

        private void frmMaticniPodatak_Load(object sender, EventArgs e)
        {
            /*
            var select3 = " select SkSifra , (Cast(SkSifra as nvarchar(6)) + '-' + SkNaziv) as Naziv from Sklad order by SkSifra";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboSkladiste.DataSource = ds3.Tables[0];
            cboSkladiste.DisplayMember = "Naziv";
            cboSkladiste.ValueMember = "SkSifra";
            */

            var select4 = " select ID , Naziv from Paleta order by ID";
            var s_connection4 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboPaleta.DataSource = ds4.Tables[0];
            cboPaleta.DisplayMember = "Naziv";
            cboPaleta.ValueMember = "ID";

            var select5 = " select MpSifra , MpStaraSif from MaticniPodatki order by MpStaraSif";
            var s_connection5 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection5 = new SqlConnection(s_connection5);
            var c5 = new SqlConnection(s_connection5);
            var dataAdapter5 = new SqlDataAdapter(select5, c5);

            var commandBuilder5 = new SqlCommandBuilder(dataAdapter5);
            var ds5 = new DataSet();
            dataAdapter5.Fill(ds5);
            cboMaticni.DataSource = ds5.Tables[0];
            cboMaticni.DisplayMember = "MpStaraSif";
            cboMaticni.ValueMember = "MpSifra";

            var select6 = " select MpSifra , MpStaraSif from MaticniPodatki order by MpSifra";
            var s_connection6= ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection6 = new SqlConnection(s_connection6);
            var c6 = new SqlConnection(s_connection6);
            var dataAdapter6 = new SqlDataAdapter(select6, c6);

            var commandBuilder6 = new SqlCommandBuilder(dataAdapter6);
            var ds6 = new DataSet();
            dataAdapter6.Fill(ds6);
            cboMaticniSifra.DataSource = ds6.Tables[0];
            cboMaticniSifra.DisplayMember = "MpSifra";
            cboMaticniSifra.ValueMember = "MpSifra";

            RefreshDataGrid();
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                InsertMaticni ins = new InsertMaticni();
                ins.InsMaticni(Convert.ToInt32(cboMaticni.SelectedValue), Convert.ToInt32(cboPaleta.SelectedValue), Convert.ToDouble(txtMaxKol.Value), Convert.ToDouble(txtBruto.Value), Convert.ToDouble(txtTezinaPalete.Value), txtRegalniRed.Text);
            }
            else
            {
                InsertMaticni upd = new InsertMaticni();
                upd.UpdMaticni(Convert.ToInt32(cboMaticni.SelectedValue), Convert.ToInt32(cboPaleta.SelectedValue), Convert.ToDouble(txtMaxKol.Value), Convert.ToDouble(txtBruto.Value), Convert.ToDouble(txtTezinaPalete.Value), txtRegalniRed.Text);
            }
             
              RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {

            var select = " select mpm.[MpSifra]  , mp.MpStaraSif " +
          " ,mpm.[Paleta] ,mpm.[MaxKolicina] " +
          " ,mpm.[BrutoPaketa] ,mpm.[TezinaPalete], mpm.RegalniRed, mp.MpMera1 as Duzina, mp.MpMera2 as Sirina, mp.MpMera3 as Visina from MaticniPodatkiMagacin mpm " +
          " inner join MaticniPodatki mp on mp.MpSifra = mpm.MpSifra" ;

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
            dataGridView1.Columns[0].HeaderText = "Šifra";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv(Kod)";
            dataGridView1.Columns[1].Width = 200;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Paleta";
            dataGridView1.Columns[2].Width = 50;


            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Max Količina";
            dataGridView1.Columns[3].Width = 80;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "MAX Paketi + Paleta";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Bruto paleta";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Regalni red";
            dataGridView1.Columns[6].Width = 50;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Dužina";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Širina";
            dataGridView1.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Visina";
            dataGridView1.Columns[9].Width = 80;

        }

        private void VratiPodatke()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select mpm.MpSifra, mpm.Paleta,mpm.MaxKolicina,mpm.BrutoPaketa, " +
            " mpm.TezinaPalete, mpm.RegalniRed, mp.MpMera1 as Duzina, mp.MpMera2 as Sirina, mp.MPMera3 as Visina from MaticniPodatkiMagacin mpm" +
            " inner join MaticniPodatki mp on mp.MpSifra = mpm.MpSifra" +
            " where mpm.MpSifra =" + cboMaticni.SelectedValue, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                txtRegalniRed.Text = dr["RegalniRed"].ToString();
                txtMaxKol.Value = Convert.ToDecimal(dr["MaxKolicina"].ToString());
                txtBruto.Value = Convert.ToDecimal(dr["BrutoPaketa"].ToString());
                txtTezinaPalete.Value = Convert.ToDecimal(dr["TezinaPalete"].ToString());
                cboPaleta.SelectedValue = Convert.ToInt32(dr["Paleta"].ToString());
                txtDuzina.Text = dr["Duzina"].ToString();
                txtSirina.Text = dr["Sirina"].ToString();
                txtVisina.Text = dr["Visina"].ToString();

            }

            con.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        cboMaticni.SelectedValue = Convert.ToInt32(row.Cells[0].Value.ToString());
                        VratiPodatke();
                        // txtOpis.Text = row.Cells[1].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void cboSkladiste_Leave(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {

            InsertMaticni del = new InsertMaticni();
            del.DeleteMaticni(Convert.ToInt32(cboMaticni.SelectedValue));
            RefreshDataGrid();
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        cboMaticni.SelectedValue = Convert.ToInt32(row.Cells[0].Value.ToString());
                        VratiPodatke();
                        // txtOpis.Text = row.Cells[1].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void btnOsvezi_Click(object sender, EventArgs e)
        {
            var select = " select mpm.[MpSifra]  , mp.MpStaraSif " +
         " ,mpm.[Paleta] ,mpm.[MaxKolicina] " +
         " ,mpm.[BrutoPaketa] ,mpm.[TezinaPalete], mpm.RegalniRed, mp.MpMera1 as Duzina, mp.MpMera2 as Sirina, mp.MpMera3 as Visina from MaticniPodatkiMagacin mpm " +
         " inner join MaticniPodatki mp on mp.MpSifra = mpm.MpSifra where mpm.MpSifra = " + Convert.ToInt32(cboMaticni.SelectedValue);

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
            dataGridView1.Columns[0].HeaderText = "Sifra";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv(Kod)";
            dataGridView1.Columns[1].Width = 200;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Paleta";
            dataGridView1.Columns[2].Width = 50;


            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Max Količina";
            dataGridView1.Columns[3].Width = 80;
           
            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "MAX Paketi + Paleta";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Bruto paleta";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Regalni red";
            dataGridView1.Columns[6].Width = 50;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Dužina";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Širina";
            dataGridView1.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Visina";
            dataGridView1.Columns[9].Width = 80;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var select = " select mpm.[MpSifra]  , mp.MpStaraSif " +
       " ,mpm.[Paleta] ,mpm.[MaxKolicina] " +
       " ,mpm.[BrutoPaketa] ,mpm.[TezinaPalete], mpm.RegalniRed, mp.MpMera1 as Duzina, mp.MpMera2 as Sirina, mp.MpMera3 as Visina from MaticniPodatkiMagacin mpm " +
       " inner join MaticniPodatki mp on mp.MpSifra = mpm.MpSifra where mpm.MpSifra = " + Convert.ToInt32(cboMaticniSifra.SelectedValue);

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
            dataGridView1.Columns[0].HeaderText = "Sifra";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv(Kod)";
            dataGridView1.Columns[1].Width = 200;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Paleta";
            dataGridView1.Columns[2].Width = 50;


            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Max Količina";
            dataGridView1.Columns[3].Width = 80;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "MAX Paketi + Paleta";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Bruto paleta";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Regalni red";
            dataGridView1.Columns[6].Width = 50;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Dužina";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Širina";
            dataGridView1.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Visina";
            dataGridView1.Columns[9].Width = 80;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var select5 = " select MpSifra , MpStaraSif from MaticniPodatki order by MpStaraSif";
            var s_connection5 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection5 = new SqlConnection(s_connection5);
            var c5 = new SqlConnection(s_connection5);
            var dataAdapter5 = new SqlDataAdapter(select5, c5);

            var commandBuilder5 = new SqlCommandBuilder(dataAdapter5);
            var ds5 = new DataSet();
            dataAdapter5.Fill(ds5);
            cboMaticni.DataSource = ds5.Tables[0];
            cboMaticni.DisplayMember = "MpStaraSif";
            cboMaticni.ValueMember = "MpSifra";

          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var select6 = " select MpSifra , MpStaraSif from MaticniPodatki order by MpSifra";
            var s_connection6 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection6 = new SqlConnection(s_connection6);
            var c6 = new SqlConnection(s_connection6);
            var dataAdapter6 = new SqlDataAdapter(select6, c6);

            var commandBuilder6 = new SqlCommandBuilder(dataAdapter6);
            var ds6 = new DataSet();
            dataAdapter6.Fill(ds6);
            cboMaticniSifra.DataSource = ds6.Tables[0];
            cboMaticniSifra.DisplayMember = "MpSifra";
            cboMaticniSifra.ValueMember = "MpSifra";
        }
    }
}
