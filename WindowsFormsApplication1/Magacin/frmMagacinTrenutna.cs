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
using DocumentFormat.OpenXml.Wordprocessing;

namespace WindowsFormsApplication1.Magacin
{
    public partial class frmMagacinTrenutna : Form
    {
        string KorisnikForme = "";

        public static string code = "frmMagacinTrenutna";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public frmMagacinTrenutna()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            SfGridPomocnik.Stilizuj(dataGridView1);
            SfGridPomocnik.Stilizuj(dataGridView2);
            dataGridView1.SelectionMode = Syncfusion.WinForms.DataGrid.Enums.GridSelectionMode.Extended;
            IdGrupe();
            IdForme();
            PravoPristupa();
            VratiKorisnike();
        }

        public frmMagacinTrenutna(string Korisnik)
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            SfGridPomocnik.Stilizuj(dataGridView1);
            SfGridPomocnik.Stilizuj(dataGridView2);
            dataGridView1.SelectionMode = Syncfusion.WinForms.DataGrid.Enums.GridSelectionMode.Extended;
            KorisnikForme = Korisnik;
            IdGrupe();
            IdForme();
            PravoPristupa();

            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button5.Enabled = false;
            VratiKorisnike();
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
        private void frmMagacinTrenutna_Load(object sender, System.EventArgs e)
        {
            var select2 = " Select distinct Rtrim(t1.Korisnik)  as Korisnik from ( " +
            " Select distinct Korisnik from aUzimanjeBarkod " +
            " union " +
            " Select distinct Korisnik from aUlaganjeBarkod ) t1";
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


            var select3 = "select top 100 NPrStPre from NPre Where NPrStatus='OD' or NPrStatus='PO' order by NPrStPre desc";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            comboBox1.DataSource = ds3.Tables[0];
            comboBox1.DisplayMember = "NprStPre";
            comboBox1.ValueMember = "NprStPre";
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            RefreshDataGrid();
            RefreshDataGridColorUzimanje();
            RefreshDataGridColorUlaganje();
         }

        private void RefreshDataGridColorUzimanje()
        {

            foreach (SfRed row in SfGridPomocnik.Redovi(dataGridView1))
            {


                if (row.Cells[9].Value.ToString() != "" && row.Cells[9].Value.ToString() == "null")
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.Green;
                    row.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Green;
                }
                else
                {
                  
                }
            }

            dataGridView1.Refresh();
        
        }

        private void RefreshDataGridColorUlaganje()
        {

            foreach (SfRed row in SfGridPomocnik.Redovi(dataGridView2))
            {


                if (row.Cells[8].Value.ToString() != "" && row.Cells[8].Value.ToString() == "null")
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.Green;
                    row.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Green;
                }
                else
                {

                }
            }

            dataGridView2.Refresh();

        }

        private void RefreshDataGrid()
        {
            if (chkNezavrsene.Checked == false)
            {
                var select = "   SELECT [Korisnik],[Artikal], MpStaraSif as Kod, " +
              "  [Pozicija],[Kolicina],[ID], (Kolicina -  KolicinaUzeta) as Preostalo,  LokOpis as LokacijaNaziv, FormiranDokument, Broj " +
              " FROM [dbo].[aUzimanjeBarkod] " +
              " inner join MaticniPodatki on MaticniPodatki.MpSifra = aUzimanjeBarkod.Artikal " +
                " inner join Lokac on Rtrim(Cast(aUzimanjeBarkod.Pozicija as char(12)))  = Rtrim(Lokac.LokSifra) " +
                "  where   (Zavrsen <> 1) and Rtrim(Korisnik) = Rtrim('" + cboKorisnik.SelectedValue + "')";

                var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
                SqlConnection myConnection = new SqlConnection(s_connection);
                var c = new SqlConnection(s_connection);
                var dataAdapter = new SqlDataAdapter(select, c);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                SfGridPomocnik.PopuniK(dataGridView1, ds.Tables[0],
                    new Kol(0, "Korisnik", 50, -1),
                    new Kol(1, "Artikal", 50, -1),
                    new Kol(2, "Kod", 120, -1),
                    new Kol(3, "Lokacija", 40, -1),
                    new Kol(4, "Kolicina", 70, -1),
                    new Kol(5, "ID", 50, -1),
                    new Kol(6, "Preostalo", 50, -1),
                    new Kol(7, "Lok opis", 150, -1),
                    new Kol(8, "FormiranDokument", 40, -1),
                    new Kol(9, "Broj", 40, -1));
            }
            else
            {

                var select = "   SELECT [Korisnik],[Artikal], MpStaraSif as Kod" +
                            "  ,[Pozicija],[Kolicina],[ID], (Kolicina -  KolicinaUzeta) as Preostalo,   LokOpis as PozicijaNaziv, FormiranDokument, Broj " +
                            " FROM [dbo].[aUzimanjeBarkod] " +
                           " inner join MaticniPodatki on MaticniPodatki.MpSifra = aUzimanjeBarkod.Artikal " +
                           " inner join Lokac on Rtrim(Cast(aUzimanjeBarkod.Pozicija as char(12)))  = Rtrim(Lokac.LokSifra) " +
                "  where    Kolicina > KolicinaUzeta and Rtrim(Korisnik) = Rtrim('" + cboKorisnik.SelectedValue + "')";

                var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
                SqlConnection myConnection = new SqlConnection(s_connection);
                var c = new SqlConnection(s_connection);
                var dataAdapter = new SqlDataAdapter(select, c);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                SfGridPomocnik.PopuniK(dataGridView1, ds.Tables[0],
                    new Kol(0, "Korisnik", 50, -1),
                    new Kol(1, "Artikal", 50, -1),
                    new Kol(2, "Kod", 120, -1),
                    new Kol(3, "Lokacija", 40, -1),
                    new Kol(4, "Kolicina", 70, -1),
                    new Kol(5, "ID", 50, -1),
                    new Kol(6, "Preostalo", 50, -1),
                    new Kol(7, "Lok opis", 150, -1),
                    new Kol(8, "FormiranDokument", 40, -1),
                    new Kol(9, "Broj", 40, -1));
            }

            var select2 = "   SELECT [Korisnik] " +
     " ,[Artikal],MpStaraSif as Kod,[Pozicija] ,[Kolicina] " +
     " ,[ID] ,  LokOpis as LokacijaNaziv, FormiranDokument, Broj " +
     " FROM [dbo].[aUlaganjeBarkod] inner join MaticniPodatki on MaticniPodatki.MpSifra = aUlaganjeBarkod.Artikal " +
       " inner join Lokac on Rtrim(Cast(aUlaganjeBarkod.Pozicija as char(12)))  = Rtrim(Lokac.LokSifra) " +
       "  where   (Zavrsen <> 1 or Zavrsen is null) and  Rtrim(Korisnik) = Rtrim('" + cboKorisnik.SelectedValue + "')";

            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            SfGridPomocnik.PopuniK(dataGridView2, ds2.Tables[0],
                new Kol(0, "Korisnik", 50, -1),
                new Kol(1, "Artikal", 50, -1),
                new Kol(2, "Kod", 120, -1),
                new Kol(3, "Lokacija", 50, -1),
                new Kol(4, "Kolicina", 70, -1),
                new Kol(5, "ID", 30, -1),
                new Kol(6, "Lokacija naziv", 150, -1),
                new Kol(7, "FormiranDokument", 40, -1),
                new Kol(8, "Broj", 40, -1));
        
        }

        private void RefreshDataGrid2()
        {
            if (chkNezavrsene.Checked == false)
            {
                var select = "   SELECT [Korisnik],[Artikal], MpStaraSif as Kod, " +
              "  [Pozicija],[Kolicina],[ID], (Kolicina -  KolicinaUzeta) as Preostalo,  LokOpis as LokacijaNaziv, FormiranDokument, Broj " +
              " FROM [dbo].[aUzimanjeBarkod] " +
              " inner join MaticniPodatki on MaticniPodatki.MpSifra = aUzimanjeBarkod.Artikal " +
                " inner join Lokac on Rtrim(Cast(aUzimanjeBarkod.Pozicija as char(12)))  = Rtrim(Lokac.LokSifra) " +
                "  where   (Zavrsen <> 1) and Rtrim(Korisnik) = Rtrim('" + "" + "')";

                var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
                SqlConnection myConnection = new SqlConnection(s_connection);
                var c = new SqlConnection(s_connection);
                var dataAdapter = new SqlDataAdapter(select, c);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                SfGridPomocnik.PopuniK(dataGridView1, ds.Tables[0],
                    new Kol(0, "Korisnik", 50, -1),
                    new Kol(1, "Artikal", 50, -1),
                    new Kol(2, "Kod", 120, -1),
                    new Kol(3, "Lokacija", 40, -1),
                    new Kol(4, "Kolicina", 70, -1),
                    new Kol(5, "ID", 50, -1),
                    new Kol(6, "Preostalo", 50, -1),
                    new Kol(7, "Lok opis", 150, -1),
                    new Kol(8, "FormiranDokument", 40, -1),
                    new Kol(9, "Broj", 40, -1));
            }
            else
            {

                var select = "   SELECT [Korisnik],[Artikal], MpStaraSif as Kod" +
                            "  ,[Pozicija],[Kolicina],[ID], (Kolicina -  KolicinaUzeta) as Preostalo,   LokOpis as PozicijaNaziv, FormiranDokument, Broj " +
                            " FROM [dbo].[aUzimanjeBarkod] " +
                           " inner join MaticniPodatki on MaticniPodatki.MpSifra = aUzimanjeBarkod.Artikal " +
                           " inner join Lokac on Rtrim(Cast(aUzimanjeBarkod.Pozicija as char(12)))  = Rtrim(Lokac.LokSifra) " +
                "  where    Kolicina > KolicinaUzeta and Rtrim(Korisnik) = Rtrim('" + " " + "')";

                var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
                SqlConnection myConnection = new SqlConnection(s_connection);
                var c = new SqlConnection(s_connection);
                var dataAdapter = new SqlDataAdapter(select, c);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                SfGridPomocnik.PopuniK(dataGridView1, ds.Tables[0],
                    new Kol(0, "Korisnik", 50, -1),
                    new Kol(1, "Artikal", 50, -1),
                    new Kol(2, "Kod", 120, -1),
                    new Kol(3, "Lokacija", 40, -1),
                    new Kol(4, "Kolicina", 70, -1),
                    new Kol(5, "ID", 50, -1),
                    new Kol(6, "Preostalo", 50, -1),
                    new Kol(7, "Lok opis", 150, -1),
                    new Kol(8, "FormiranDokument", 40, -1),
                    new Kol(9, "Broj", 40, -1));
            }

            var select2 = "   SELECT [Korisnik] " +
     " ,[Artikal],MpStaraSif as Kod,[Pozicija] ,[Kolicina] " +
     " ,[ID] ,  LokOpis as LokacijaNaziv, FormiranDokument, Broj " +
     " FROM [dbo].[aUlaganjeBarkod] inner join MaticniPodatki on MaticniPodatki.MpSifra = aUlaganjeBarkod.Artikal " +
       " inner join Lokac on Rtrim(Cast(aUlaganjeBarkod.Pozicija as char(12)))  = Rtrim(Lokac.LokSifra) " +
       "  where   (Zavrsen <> 1 or Zavrsen is null) and  Rtrim(Korisnik) = Rtrim('" + " " + "')";

            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            SfGridPomocnik.PopuniK(dataGridView2, ds2.Tables[0],
                new Kol(0, "Korisnik", 50, -1),
                new Kol(1, "Artikal", 50, -1),
                new Kol(2, "Kod", 120, -1),
                new Kol(3, "Lokacija", 50, -1),
                new Kol(4, "Kolicina", 70, -1),
                new Kol(5, "ID", 30, -1),
                new Kol(6, "Lokacija naziv", 150, -1),
                new Kol(7, "FormiranDokument", 40, -1),
                new Kol(8, "Broj", 40, -1));

        }


        private void button1_Click(object sender, System.EventArgs e)
        {

            decimal KolicinaZaIzdavanje = 0;
            
            InsertMedjuskladisni isporuka = new InsertMedjuskladisni();
           
            isporuka.InsertIZDTrenutna(cboKorisnik.Text.Trim());
          
            foreach (SfRed row in SfGridPomocnik.Redovi(dataGridView1))
            {
                if (row != null && row.Cells[0].Value != null)
                {
                  
                    if (Convert.ToDecimal(row.Cells[4].Value.ToString()) >= Convert.ToDecimal(row.Cells[6].Value.ToString()))
                    {
                        KolicinaZaIzdavanje = Convert.ToDecimal(row.Cells[4].Value.ToString()) - Convert.ToDecimal(row.Cells[6].Value.ToString());
                    }
                    else
                    {
                        KolicinaZaIzdavanje = Convert.ToDecimal(row.Cells[4].Value.ToString()) - Convert.ToDecimal(row.Cells[6].Value.ToString());
                    }
                    
                    isporuka.InsertIZDPostavTrenutno(Convert.ToInt32(row.Cells[1].Value.ToString()), KolicinaZaIzdavanje, row.Cells[3].Value.ToString(), cboKorisnik.Text.Trim());
                    isporuka.UpdateaUzimanjeFormiranje(Convert.ToInt32(row.Cells[5].Value.ToString()));
                    // MessageBox.Show(row.Cells[2].Value.ToString());
                }
            }
           ///ENd of formiranje izdatnice
            InsertMedjuskladisni isporuka2 = new InsertMedjuskladisni();

            isporuka2.InsertPRETrenutno(cboKorisnik.Text);

            foreach (SfRed row in SfGridPomocnik.Redovi(dataGridView2))
            {
                if (row != null && row.Cells[0].Value != null)
                {
                    isporuka2.InsertPREPostavTrenutno(Convert.ToInt32(row.Cells[1].Value.ToString()), Convert.ToDecimal(row.Cells[4].Value.ToString()), row.Cells[3].Value.ToString(), cboKorisnik.Text.Trim());
                    isporuka.UpdateaUlaganjeFormiranje(Convert.ToInt32(row.Cells[5].Value.ToString()));
                    // MessageBox.Show(row.Cells[2].Value.ToString());
                }
            }
            MessageBox.Show("Formirana je izdatnica i prijemnica");
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertMedjuskladisni isporuka = new InsertMedjuskladisni();

            isporuka.InsertPRETrenutno(cboKorisnik.Text.Trim());

            foreach (SfRed row in SfGridPomocnik.Redovi(dataGridView1))
            {
                if (row != null && row.Cells[0].Value != null)
                {
                    isporuka.InsertPREPostavTrenutno(Convert.ToInt32(row.Cells[1].Value.ToString()), Convert.ToDecimal(row.Cells[4].Value.ToString()), row.Cells[3].Value.ToString(), cboKorisnik.Text.Trim());

                }
            }
            MessageBox.Show("Formirana je izdatnica");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            foreach (SfRed row in SfGridPomocnik.Redovi(dataGridView1))
            {
                if (Convert.ToInt32(row.Cells[8].Value) == 0)
                {
                    
                    MessageBox.Show("Niste formirali dokumenta");
                    return;
                }
            }

            
            
            InsertMedjuskladisni zavrsi = new InsertMedjuskladisni();

            zavrsi.ZavrsiPrivremeni(cboKorisnik.Text.Trim());
            RefreshDataGrids();
            RefreshDataGridColorUzimanje();
            RefreshDataGridColorUlaganje();
        }

        private void RefreshDataGrids()
        {
            var select2 = " Select distinct Rtrim(t1.Korisnik)  as Korisnik from ( " +
              " Select distinct Korisnik from aUzimanjeBarkod where Zavrsen <> 1 " +
              " union " +
              " Select distinct Korisnik from aUlaganjeBarkod where Zavrsen <> 1 ) t1 order by Korisnik";
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

            RefreshDataGrid2();
        
        
        }
        private void VratiKorisnike()
        {
            var select2 = " Select distinct Rtrim(t1.Korisnik)  as Korisnik from ( " +
           " Select distinct Korisnik from aUzimanjeBarkod where Zavrsen <> 1" +
           " union " +
           " Select distinct Korisnik from aUlaganjeBarkod where Zavrsen <> 1) t1 order by Korisnik";
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var select = "   SELECT [Korisnik],[Artikal], MpStaraSif as Kod, " +
             "  [Pozicija],[Kolicina],[ID], (Kolicina -  KolicinaUzeta) as Preostalo,  LokOpis as LokacijaNaziv, FormiranDokument, Broj " +
             " FROM [dbo].[aUzimanjeBarkod] " +
             " inner join MaticniPodatki on MaticniPodatki.MpSifra = aUzimanjeBarkod.Artikal " +
               " inner join Lokac on Rtrim(Cast(aUzimanjeBarkod.Pozicija as char(12)))  = Rtrim(Lokac.LokSifra) " +
               "  where   (Zavrsen <> 1) and aUzimanjeBarkod.Korisnik='" + cboKorisnik.SelectedValue.ToString().TrimEnd()+"'";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            SfGridPomocnik.PopuniK(dataGridView1, ds.Tables[0],
                new Kol(0, "Korisnik", 50, -1),
                new Kol(1, "Artikal", 50, -1),
                new Kol(2, "Kod", 120, -1),
                new Kol(3, "Lokacija", 40, -1),
                new Kol(4, "Kolicina", 70, -1),
                new Kol(5, "ID", 50, -1),
                new Kol(6, "Preostalo", 50, -1),
                new Kol(7, "Lok opis", 150, -1),
                new Kol(8, "FormiranDokument", 40, -1),
                new Kol(9, "Broj", 40, -1));


            var select2 = "   SELECT [Korisnik] " +
" ,[Artikal],MpStaraSif as Kod,[Pozicija] ,[Kolicina] " +
" ,[ID] ,  LokOpis as LokacijaNaziv, FormiranDokument, Broj " +
" FROM [dbo].[aUlaganjeBarkod] inner join MaticniPodatki on MaticniPodatki.MpSifra = aUlaganjeBarkod.Artikal " +
" inner join Lokac on Rtrim(Cast(aUlaganjeBarkod.Pozicija as char(12)))  = Rtrim(Lokac.LokSifra) " +
"  where   (Zavrsen <> 1 or Zavrsen is null) and aUlaganjeBarkod.Korisnik='" + cboKorisnik.SelectedValue.ToString().TrimEnd()+"'";

            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            SfGridPomocnik.PopuniK(dataGridView2, ds2.Tables[0],
                new Kol(0, "Korisnik", 50, -1),
                new Kol(1, "Artikal", 50, -1),
                new Kol(2, "Kod", 120, -1),
                new Kol(3, "Lokacija", 50, -1),
                new Kol(4, "Kolicina", 70, -1),
                new Kol(5, "ID", 30, -1),
                new Kol(6, "Lokacija naziv", 150, -1),
                new Kol(7, "FormiranDokument", 40, -1),
                new Kol(8, "Broj", 40, -1));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InsertMedjuskladisni uzimanje = new InsertMedjuskladisni();

          

            foreach (SfRed row in SfGridPomocnik.Redovi(dataGridView1))
            {
                if (row.Selected == true)
                {
                    uzimanje.DeleteStavkuaUzimanje(Convert.ToInt32(row.Cells[5].Value.ToString()));
                    // MessageBox.Show(row.Cells[2].Value.ToString());
                }
            }

            RefreshDataGrid();
            RefreshDataGridColorUzimanje();
            RefreshDataGridColorUlaganje();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var select = "   SELECT [Korisnik],[Artikal], MpStaraSif as Kod, " +
              "  [Pozicija],[Kolicina],[ID], (Kolicina -  KolicinaUzeta) as Preostalo,  LokOpis as LokacijaNaziv, FormiranDokument, Broj " +
              " FROM [dbo].[aUzimanjeBarkod] " +
              " inner join MaticniPodatki on MaticniPodatki.MpSifra = aUzimanjeBarkod.Artikal " +
                " inner join Lokac on Rtrim(Cast(aUzimanjeBarkod.Pozicija as char(12)))  = Rtrim(Lokac.LokSifra) " +
                "  where   (Zavrsen <> 1) and Broj=" + Convert.ToInt32(comboBox1.SelectedValue);

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            SfGridPomocnik.PopuniK(dataGridView1, ds.Tables[0],
                new Kol(0, "Korisnik", 50, -1),
                new Kol(1, "Artikal", 50, -1),
                new Kol(2, "Kod", 120, -1),
                new Kol(3, "Lokacija", 40, -1),
                new Kol(4, "Kolicina", 70, -1),
                new Kol(5, "ID", 50, -1),
                new Kol(6, "Preostalo", 50, -1),
                new Kol(7, "Lok opis", 150, -1),
                new Kol(8, "FormiranDokument", 40, -1),
                new Kol(9, "Broj", 40, -1));


            var select2 = "   SELECT [Korisnik] " +
" ,[Artikal],MpStaraSif as Kod,[Pozicija] ,[Kolicina] " +
" ,[ID] ,  LokOpis as LokacijaNaziv, FormiranDokument, Broj " +
" FROM [dbo].[aUlaganjeBarkod] inner join MaticniPodatki on MaticniPodatki.MpSifra = aUlaganjeBarkod.Artikal " +
" inner join Lokac on Rtrim(Cast(aUlaganjeBarkod.Pozicija as char(12)))  = Rtrim(Lokac.LokSifra) " +
"  where   (Zavrsen <> 1 or Zavrsen is null) and Broj=" + Convert.ToInt32(comboBox1.SelectedValue);

            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            SfGridPomocnik.PopuniK(dataGridView2, ds2.Tables[0],
                new Kol(0, "Korisnik", 50, -1),
                new Kol(1, "Artikal", 50, -1),
                new Kol(2, "Kod", 120, -1),
                new Kol(3, "Lokacija", 50, -1),
                new Kol(4, "Kolicina", 70, -1),
                new Kol(5, "ID", 30, -1),
                new Kol(6, "Lokacija naziv", 150, -1),
                new Kol(7, "FormiranDokument", 40, -1),
                new Kol(8, "Broj", 40, -1));
        }
    }
}
