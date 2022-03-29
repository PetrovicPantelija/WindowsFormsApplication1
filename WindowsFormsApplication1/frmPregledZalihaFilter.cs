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
    public partial class frmPregledZalihaFilter : Form
    {
        public static string code = "frmPregledZalihaFilter";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string niz = "";
        string Kor = frmLogovanje.user.ToString().TrimEnd();

        public frmPregledZalihaFilter()
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
        private void frmPregledZalihaFilter_Load(object sender, EventArgs e)
        {
            var select2 = " Select SkSifra, (Cast(SkSifra as nvarchar(4)) + '-' + SkNaziv) as SkNaziv from Sklad order by SkSifra";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboSkladiste.DataSource = ds2.Tables[0];
            cboSkladiste.DisplayMember = "SkNaziv";
            cboSkladiste.ValueMember = "SkSifra";

            var select3 = " Select MpSifra, MpStaraSif from MaticniPodatki order by MpStaraSif";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboArtikal.DataSource = ds3.Tables[0];
            cboArtikal.DisplayMember = "MpStaraSif";
            cboArtikal.ValueMember = "MpSifra";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid1();
        }

        private void RefreshDataGrid1()
        {
            var select = " select (Lokac.LokSifra + '-' + Lokac.LokOpis) as Regal, ZlSifMp as Sifra, MpStaraSif as Kod, ZlDejanskaKol as Kolicina   from Lokac " +
            " left join ZalogaLokacija on ZlLok = Lokac.LokSifra " + 
            " left join MaticniPodatki on MaticniPOdatki.MpSifra = ZlSifMp " +
            " where  LokSkSifra = " +  cboSkladiste.SelectedValue + 
            " order by Lokac.LokSifra";
            
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
            dataGridView1.Columns[0].HeaderText = "Regal";
            dataGridView1.Columns[0].Width = 150;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Artikal";
            dataGridView1.Columns[1].Width = 70;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Kod";
            dataGridView1.Columns[2].Width = 130;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Količina";
            dataGridView1.Columns[3].Width = 50;
        }

        private void RefreshDataGrid2()
        {
            var select = " select (Lokac.LokSifra + '-' + Lokac.LokOpis) as Regal, ZlSifMp as Sifra, MpStaraSif as Kod, ZlDejanskaKol as Kolicina   from Lokac " +
            " left join ZalogaLokacija on ZlLok = Lokac.LokSifra " +
            " left join MaticniPodatki on MaticniPOdatki.MpSifra = ZlSifMp " +
            " where  LokSkSifra = " + cboSkladiste.SelectedValue + " and ZlLok is null " + 
            " order by Lokac.LokSifra";

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
            dataGridView1.Columns[0].HeaderText = "Regal";
            dataGridView1.Columns[0].Width = 150;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Artikal";
            dataGridView1.Columns[1].Width = 70;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Kod";
            dataGridView1.Columns[2].Width = 130;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Količina";
            dataGridView1.Columns[3].Width = 50;
        }

        private void RefreshDataGrid3()
        {
            var select = " select (Lokac.LokSifra + '-' + Lokac.LokOpis) as Regal, ZlSifMp as Sifra, MpStaraSif as Kod, ZlDejanskaKol as Kolicina   from Lokac " +
            " left join ZalogaLokacija on ZlLok = Lokac.LokSifra " +
            " left join MaticniPodatki on MaticniPOdatki.MpSifra = ZlSifMp " +
            " where  LokSkSifra = " + cboSkladiste.SelectedValue + " and ZlSifMp =  " + Convert.ToInt32(cboArtikal.SelectedValue) +
            " order by Lokac.LokSifra";

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
            dataGridView1.Columns[0].HeaderText = "Regal";
            dataGridView1.Columns[0].Width = 150;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Artikal";
            dataGridView1.Columns[1].Width = 70;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Kod";
            dataGridView1.Columns[2].Width = 130;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Količina";
            dataGridView1.Columns[3].Width = 50;
        }

        private void RefreshDataGrid4()
        {
            var select = " select (Lokac.LokSifra + '-'  + Lokac.LokOpis) as Regal, ZlSifMp as Sifra, MpStaraSif as Kod, ZlDejanskaKol as Kolicina   from Lokac " +
            " left join ZalogaLokacija on ZlLok = Lokac.LokSifra " +
            " left join MaticniPodatki on MaticniPOdatki.MpSifra = ZlSifMp " +
            " where  MpStaraSif Like ( '%" + txtFilter.Text + "%' ) " + 
            " order by Lokac.LokSifra";

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
            dataGridView1.Columns[0].HeaderText = "Regal";
            dataGridView1.Columns[0].Width = 150;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Artikal";
            dataGridView1.Columns[1].Width = 70;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Kod";
            dataGridView1.Columns[2].Width = 130;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Količina";
            dataGridView1.Columns[3].Width = 50;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshDataGrid3();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Tezina.Text = "0";

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (row.Selected)
                {
                    //Panta1
                    Tezina.Text = (Convert.ToDecimal(Tezina.Text) + Convert.ToDecimal(row.Cells[3].Value.ToString())).ToString();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RefreshDataGrid4();
        }
    }
}
