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
using System.Net;
using System.Net.Mail;

using Microsoft.Reporting.WinForms;

namespace WindowsFormsApplication1.Magacin
{
    public partial class frmMagacinTrenutnaIzPrijema : Form
    {
        string KorisnikForme = "";
        public static string code = "frmMagacinTrenutnaIzPrijema";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        public frmMagacinTrenutnaIzPrijema()
        {
            InitializeComponent();
        }

        public frmMagacinTrenutnaIzPrijema(string Korisnik)
        {
            InitializeComponent();
            KorisnikForme = Korisnik;
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
        private void frmMagacinTrenutnaIzPrijema_Load(object sender, EventArgs e)
        {
            var select2 = " Select Rtrim(korisnik) as Korisnik from Korisnici order by Korisnik ";
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

            var select3 = " select NPrStPre from Npre where NPrStatus = 'PO' ";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboPrijemnice.DataSource = ds3.Tables[0];
            cboPrijemnice.DisplayMember = "NPrStPre";
            cboPrijemnice.ValueMember = "NPrStPre";

            var select4 = " select top 1000 PrTStDok from PrometTrans where PrTStatPrTrans = 'PO' and PrTTipPrTrans = 'MS' order by PrTStDok desc";
            var s_connection4 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);

            cboMedjuskladisni.DataSource = ds4.Tables[0];
            cboMedjuskladisni.DisplayMember = "PrTStDok";
            cboMedjuskladisni.ValueMember = "PrTStDok";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var select = "   select NPre.NPrStPre as Prijemnica,NPreP.NPrPSifra as MP, MaticniPodatki.MpStaraSif as Kod ,NPrPKolPre as Kolicina, " +
            " NPrPSifSkl as Skladiste, NPrPLokac as Lokacija, Lokac.LokOpis as LOpis  from NPreP inner join NPre on NPreP.NPrPStPre = NPre.NPrStPre " +
             " inner join MaticniPodatki on MaticniPodatki.MpSifra = NPreP.NPrPSifra " +
            " inner join Lokac on NPrPLokac = LokSifra  " +
            " where NPre.NPrStPre = " + cboPrijemnice.SelectedValue;

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            //string value = dataGridView2.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertMedjuskladisni isporuka = new InsertMedjuskladisni();



            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row != null && row.Cells[0].Value != null)
                {
                   // MessageBox.Show(row.Cells[4].Value.ToString());
                    isporuka.aUzimanjePrijemnica(cboKorisnik.Text, Convert.ToInt32(row.Cells[1].Value.ToString()), Convert.ToInt32(row.Cells[4].Value.ToString()), row.Cells[5].Value.ToString(), Convert.ToDouble(row.Cells[3].Value.ToString()));

                }
            }

            MessageBox.Show("Formirano je uzimanje!!!!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var select3 = " select NPrStPre from Npre where NPrStatus = 'PO' ";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboPrijemnice.DataSource = ds3.Tables[0];
            cboPrijemnice.DisplayMember = "NPrStPre";
            cboPrijemnice.ValueMember = "NPrStPre";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var select4 = " select top 1000 PrTStDok from PrometTrans where PrTStatPrTrans = 'PO' and PrTTipPrTrans = 'MS' order by PrTStDok desc";
            var s_connection4 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);

            cboMedjuskladisni.DataSource = ds4.Tables[0];
            cboMedjuskladisni.DisplayMember = "PrTStDok";
            cboMedjuskladisni.ValueMember = "PrTStDok";
        }

        private void button5_Click(object sender, EventArgs e)
        {
          // var select = "   select NPre.NPrStPre as Prijemnica,NPreP.NPrPSifra as MP, NPrPKolPre as Kolicina, " +
          // " NPrPSifSkl as Skladiste, NPrPLokac as Lokacija  from NPreP inner join NPre on NPreP.NPrPStPre = NPre.NPrStPre " +
          // " where NPre.NPrStPre = " + cboPrijemnice.SelectedValue;
            var select = "  select PrTpStDokumenta as ID,  PrTpSifMp as Sifra, MaticniPodatki.MpStaraSif as Kod, PrTpPrevKol as Kolicina, " +
            "  PrTpSifSklad as SkladIz, PrTpLok as LokIz, Lokac1.LokOpis as LokOpis from PrometTransPos " +
            "  inner Join Lokac as Lokac1 on Lokac1.LokSifra = PrometTransPos.PrTpLok " +
              "  inner Join MaticniPodatki  on MaticniPodatki.MpSifra = PrometTransPos.PrTpSifMp " +
            "  where  PrTpStDokumenta =  " + Convert.ToInt32(cboMedjuskladisni.SelectedValue);
            //PrTpStaraSifMp as Kod,
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
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 30;

       
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var select = "   select NPre.NPrStPre as Prijemnica,NPreP.NPrPSifra as MP, MaticniPodatki.MpStaraSif as Kod ,NPrPKolPre as Kolicina, " +
           " NPrPSifSkl as Skladiste, NPrPLokac as Lokacija, Lokac.LokOpis as LOpis  from NPreP inner join NPre on NPreP.NPrPStPre = NPre.NPrStPre " +
            " inner join MaticniPodatki on MaticniPodatki.MpSifra = NPreP.NPrPSifra " +
           " inner join Lokac on NPrPLokac = LokSifra  " +
           " where NPre.NPrStPre = " + Convert.ToInt32(txtPrijemnica.Text);

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            //string value = dataGridView2.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var select = "  select PrTpStDokumenta as ID,  PrTpSifMp as Sifra, MaticniPodatki.MpStaraSif as Kod, PrTpPrevKol as Kolicina, " +
        "  PrTpSifSklad as SkladIz, PrTpLok as LokIz, Lokac1.LokOpis as LokOpis from PrometTransPos " +
        "  inner Join Lokac as Lokac1 on Lokac1.LokSifra = PrometTransPos.PrTpLok " +
          "  inner Join MaticniPodatki  on MaticniPodatki.MpSifra = PrometTransPos.PrTpSifMp " +
        "  where  PrTpStDokumenta =  " + Convert.ToInt32(txtMSP.Text);
            //PrTpStaraSifMp as Kod,
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
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 30;
        }
    }
}
