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
    public partial class frmPregledZaliha : Form
    {
        public static string code = "frmPrikazZaliha";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public frmPregledZaliha()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            SfGridPomocnik.Stilizuj(dataGridView1);
            SfGridPomocnik.Stilizuj(dataGridView2);
            SfGridPomocnik.Stilizuj(dataGridView3);
            SfGridPomocnik.Stilizuj(dataGridView4);
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
        private void txtLokacija_Leave(object sender, EventArgs e)
        {
            RefreshDataGrid1();
        }

        private void RefreshDataGrid1()
        {
            var select = " select  ZlLok,  ZlSifMP,  MaticniPodatki.MpStaraSif as Artikal, ZlDejanskaKol from ZalogaLokacija inner join MaticniPodatki on MaticniPodatki.MpSifra = ZaLogaLokacija.ZlSifMp";
            select = select + " where ZlSifSklad = " + cboSkladiste.SelectedValue + " and ZlLok like ( '" + txtLokacija.Text + "%' )" + " order by ZlLok  "; ;

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            SfGridPomocnik.PopuniK(dataGridView1, ds.Tables[0],
                new Kol(0, "Lokacija", 30, -1),
                new Kol(1, "Artikal", 30, -1),
                new Kol(2, "Kod", 130, -1),
                new Kol(3, "Količina", 50, -1));
        }

        private void RefreshDataGrid2()
        {
            var select = " select  ZlLok,  ZlSifMP,  MaticniPodatki.MpStaraSif as Artikal, ZlDejanskaKol from ZalogaLokacija inner join MaticniPodatki on MaticniPodatki.MpSifra = ZaLogaLokacija.ZlSifMp";
            select = select + " where ZlSifSklad = " + cboSkladiste2.SelectedValue + " and ZlLok like ( '" + txtLokacija2.Text + "%' )" + " order by ZlLok  "; ;

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            SfGridPomocnik.PopuniK(dataGridView2, ds.Tables[0],
                new Kol(0, "Lokacija", 30, -1),
                new Kol(1, "Artikal", 30, -1),
                new Kol(2, "Kod", 130, -1),
                new Kol(3, "Količina", 50, -1));

        }

        private void RefreshDataGrid3()
        {
            var select = " select  ZlLok,  ZlSifMP,  MaticniPodatki.MpStaraSif as Artikal, ZlDejanskaKol from ZalogaLokacija inner join MaticniPodatki on MaticniPodatki.MpSifra = ZaLogaLokacija.ZlSifMp";
            select = select + " where ZlSifSklad = " + cboSkladiste2.SelectedValue + " and ZlLok like ( '" + txtLokacija2.Text + "%' )" + " order by ZlLok  "; ;

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            SfGridPomocnik.PopuniK(dataGridView3, ds.Tables[0],
                new Kol(0, "Lokacija", 30, -1),
                new Kol(1, "Artikal", 30, -1),
                new Kol(2, "Kod", 130, -1),
                new Kol(3, "Količina", 50, -1));

        }

        private void RefreshDataGrid4()
        {
            var select = " select  ZlLok,  ZlSifMP,  MaticniPodatki.MpStaraSif as Artikal, ZlDejanskaKol from ZalogaLokacija inner join MaticniPodatki on MaticniPodatki.MpSifra = ZaLogaLokacija.ZlSifMp";
            select = select + " where ZlSifSklad = " + cboSkladiste2.SelectedValue + " and ZlLok like ( '" + txtLokacija2.Text + "%' )" + " order by ZlLok  "; ;

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            SfGridPomocnik.PopuniK(dataGridView4, ds.Tables[0],
                new Kol(0, "Lokacija", 30, -1),
                new Kol(1, "Artikal", 30, -1),
                new Kol(2, "Kod", 130, -1),
                new Kol(3, "Količina", 50, -1));

        }

        private void frmPrikazSkladista_Load(object sender, EventArgs e)
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

            ///

            var select3 = " Select SkSifra, (Cast(SkSifra as nvarchar(4)) + '-' + SkNaziv) as SkNaziv from Sklad order by SkSifra";
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

            ///
            ///

            var select4 = " Select SkSifra, (Cast(SkSifra as nvarchar(4)) + '-' + SkNaziv) as SkNaziv from Sklad order by SkSifra";
            var s_connection4 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection4= new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection3);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboSkladiste3.DataSource = ds4.Tables[0];
            cboSkladiste3.DisplayMember = "SkNaziv";
            cboSkladiste3.ValueMember = "SkSifra";
            //
            var select5 = " Select SkSifra, (Cast(SkSifra as nvarchar(4)) + '-' + SkNaziv) as SkNaziv from Sklad order by SkSifra";
            var s_connection5 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection5 = new SqlConnection(s_connection5);
            var c5 = new SqlConnection(s_connection4);
            var dataAdapter5 = new SqlDataAdapter(select5, c5);

            var commandBuilder5 = new SqlCommandBuilder(dataAdapter5);
            var ds5 = new DataSet();
            dataAdapter5.Fill(ds5);
            cboSkladiste4.DataSource = ds5.Tables[0];
            cboSkladiste4.DisplayMember = "SkNaziv";
            cboSkladiste4.ValueMember = "SkSifra";
        }

        private void txtLokacija2_Leave(object sender, EventArgs e)
        {
            RefreshDataGrid2();
        }

        private void txtLokacija3_Leave(object sender, EventArgs e)
        {
            RefreshDataGrid3();
        }

        private void txtLokacija4_Leave(object sender, EventArgs e)
        {
            RefreshDataGrid4();
        }

        private void cboSkladiste_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    
    }
}

