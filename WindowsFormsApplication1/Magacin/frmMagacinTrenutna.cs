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
    public partial class frmMagacinTrenutna : Form
    {
        string KorisnikForme = "";
        public frmMagacinTrenutna()
        {
            InitializeComponent();
        }

        public frmMagacinTrenutna(string Korisnik)
        {
            InitializeComponent();
            KorisnikForme = Korisnik;
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
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            RefreshDataGrid();
            RefreshDataGridColorUzimanje();
            RefreshDataGridColorUlaganje();
         }

        private void RefreshDataGridColorUzimanje()
        {

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {


                if (row.Cells[9].Value.ToString() != "" && row.Cells[9].Value.ToString() == "null")
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.SelectionBackColor = Color.Green;
                }
                else
                {
                  
                }
            }

            dataGridView1.Refresh();
        
        }

        private void RefreshDataGridColorUlaganje()
        {

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {


                if (row.Cells[9].Value.ToString() != "" && row.Cells[9].Value.ToString() == "null")
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.SelectionBackColor = Color.Green;
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
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = ds.Tables[0];

                //string value = dataGridView2.Rows[0].Cells[0].Value.ToString();
                DataGridViewColumn column = dataGridView1.Columns[0];
                dataGridView1.Columns[0].HeaderText = "Korisnik";
                dataGridView1.Columns[0].Width = 50;

                DataGridViewColumn column2 = dataGridView1.Columns[1];
                dataGridView1.Columns[1].HeaderText = "Artikal";
                dataGridView1.Columns[1].Width = 50;

                DataGridViewColumn column3 = dataGridView1.Columns[2];
                dataGridView1.Columns[2].HeaderText = "Kod";
                dataGridView1.Columns[2].Width = 120;

                DataGridViewColumn column4 = dataGridView1.Columns[3];
                dataGridView1.Columns[3].HeaderText = "Lokacija";
                dataGridView1.Columns[3].Width = 40;

                DataGridViewColumn column5 = dataGridView1.Columns[4];
                dataGridView1.Columns[4].HeaderText = "Kolicina";
                dataGridView1.Columns[4].Width = 70;

                DataGridViewColumn column6 = dataGridView1.Columns[5];
                dataGridView1.Columns[5].HeaderText = "ID";
                dataGridView1.Columns[5].Width = 50;

                DataGridViewColumn column7 = dataGridView1.Columns[6];
                dataGridView1.Columns[6].HeaderText = "Preostalo";
                dataGridView1.Columns[6].Width = 50;

                DataGridViewColumn column8 = dataGridView1.Columns[7];
                dataGridView1.Columns[7].HeaderText = "Lok opis";
                dataGridView1.Columns[7].Width = 150;

                DataGridViewColumn column9 = dataGridView1.Columns[8];
                dataGridView1.Columns[8].HeaderText = "FormiranDokument";
                dataGridView1.Columns[8].Width = 40;

                DataGridViewColumn column10 = dataGridView1.Columns[9];
                dataGridView1.Columns[9].HeaderText = "Broj";
                dataGridView1.Columns[9].Width = 40;
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
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = ds.Tables[0];

                //string value = dataGridView2.Rows[0].Cells[0].Value.ToString();
                DataGridViewColumn column = dataGridView1.Columns[0];
                dataGridView1.Columns[0].HeaderText = "Korisnik";
                dataGridView1.Columns[0].Width = 50;

                DataGridViewColumn column2 = dataGridView1.Columns[1];
                dataGridView1.Columns[1].HeaderText = "Artikal";
                dataGridView1.Columns[1].Width = 50;

                DataGridViewColumn column3 = dataGridView1.Columns[2];
                dataGridView1.Columns[2].HeaderText = "Kod";
                dataGridView1.Columns[2].Width = 120;

                DataGridViewColumn column4 = dataGridView1.Columns[3];
                dataGridView1.Columns[3].HeaderText = "Lokacija";
                dataGridView1.Columns[3].Width = 40;

                DataGridViewColumn column5 = dataGridView1.Columns[4];
                dataGridView1.Columns[4].HeaderText = "Kolicina";
                dataGridView1.Columns[4].Width = 70;

                DataGridViewColumn column6 = dataGridView1.Columns[5];
                dataGridView1.Columns[5].HeaderText = "ID";
                dataGridView1.Columns[5].Width = 50;

                DataGridViewColumn column7 = dataGridView1.Columns[6];
                dataGridView1.Columns[6].HeaderText = "Preostalo";
                dataGridView1.Columns[6].Width = 50;

                DataGridViewColumn column8 = dataGridView1.Columns[7];
                dataGridView1.Columns[7].HeaderText = "Lok opis";
                dataGridView1.Columns[7].Width = 150;

                DataGridViewColumn column9 = dataGridView1.Columns[8];
                dataGridView1.Columns[8].HeaderText = "FormiranDokument";
                dataGridView1.Columns[8].Width = 40;

                DataGridViewColumn column10 = dataGridView1.Columns[9];
                dataGridView1.Columns[9].HeaderText = "Broj";
                dataGridView1.Columns[9].Width = 40;
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
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds2.Tables[0];

            DataGridViewColumn column20 = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "Korisnik";
            dataGridView2.Columns[0].Width = 50;

            DataGridViewColumn column21 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Artikal";
            dataGridView2.Columns[1].Width = 50;

            DataGridViewColumn column31 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Kod";
            dataGridView2.Columns[2].Width = 120;

            DataGridViewColumn column41 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "Lokacija";
            dataGridView2.Columns[3].Width = 50;

            DataGridViewColumn column51 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "Kolicina";
            dataGridView2.Columns[4].Width = 70;

            DataGridViewColumn column61 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "ID";
            dataGridView1.Columns[5].Width = 30;

            DataGridViewColumn column71 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Preostalo";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column81 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Lokacija naziv";
            dataGridView1.Columns[7].Width = 150;

            DataGridViewColumn column91 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "FormiranDokument";
            dataGridView1.Columns[8].Width = 40;


            DataGridViewColumn column101 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Broj";
            dataGridView1.Columns[9].Width = 40;
        
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
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = ds.Tables[0];

                //string value = dataGridView2.Rows[0].Cells[0].Value.ToString();
                DataGridViewColumn column = dataGridView1.Columns[0];
                dataGridView1.Columns[0].HeaderText = "Korisnik";
                dataGridView1.Columns[0].Width = 50;

                DataGridViewColumn column2 = dataGridView1.Columns[1];
                dataGridView1.Columns[1].HeaderText = "Artikal";
                dataGridView1.Columns[1].Width = 50;

                DataGridViewColumn column3 = dataGridView1.Columns[2];
                dataGridView1.Columns[2].HeaderText = "Kod";
                dataGridView1.Columns[2].Width = 120;

                DataGridViewColumn column4 = dataGridView1.Columns[3];
                dataGridView1.Columns[3].HeaderText = "Lokacija";
                dataGridView1.Columns[3].Width = 40;

                DataGridViewColumn column5 = dataGridView1.Columns[4];
                dataGridView1.Columns[4].HeaderText = "Kolicina";
                dataGridView1.Columns[4].Width = 70;

                DataGridViewColumn column6 = dataGridView1.Columns[5];
                dataGridView1.Columns[5].HeaderText = "ID";
                dataGridView1.Columns[5].Width = 50;

                DataGridViewColumn column7 = dataGridView1.Columns[6];
                dataGridView1.Columns[6].HeaderText = "Preostalo";
                dataGridView1.Columns[6].Width = 50;

                DataGridViewColumn column8 = dataGridView1.Columns[7];
                dataGridView1.Columns[7].HeaderText = "Lok opis";
                dataGridView1.Columns[7].Width = 150;

                DataGridViewColumn column9 = dataGridView1.Columns[8];
                dataGridView1.Columns[8].HeaderText = "FormiranDokument";
                dataGridView1.Columns[8].Width = 40;

                DataGridViewColumn column10 = dataGridView1.Columns[9];
                dataGridView1.Columns[9].HeaderText = "Broj";
                dataGridView1.Columns[9].Width = 40;
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
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = ds.Tables[0];

                //string value = dataGridView2.Rows[0].Cells[0].Value.ToString();
                DataGridViewColumn column = dataGridView1.Columns[0];
                dataGridView1.Columns[0].HeaderText = "Korisnik";
                dataGridView1.Columns[0].Width = 50;

                DataGridViewColumn column2 = dataGridView1.Columns[1];
                dataGridView1.Columns[1].HeaderText = "Artikal";
                dataGridView1.Columns[1].Width = 50;

                DataGridViewColumn column3 = dataGridView1.Columns[2];
                dataGridView1.Columns[2].HeaderText = "Kod";
                dataGridView1.Columns[2].Width = 120;

                DataGridViewColumn column4 = dataGridView1.Columns[3];
                dataGridView1.Columns[3].HeaderText = "Lokacija";
                dataGridView1.Columns[3].Width = 40;

                DataGridViewColumn column5 = dataGridView1.Columns[4];
                dataGridView1.Columns[4].HeaderText = "Kolicina";
                dataGridView1.Columns[4].Width = 70;

                DataGridViewColumn column6 = dataGridView1.Columns[5];
                dataGridView1.Columns[5].HeaderText = "ID";
                dataGridView1.Columns[5].Width = 50;

                DataGridViewColumn column7 = dataGridView1.Columns[6];
                dataGridView1.Columns[6].HeaderText = "Preostalo";
                dataGridView1.Columns[6].Width = 50;

                DataGridViewColumn column8 = dataGridView1.Columns[7];
                dataGridView1.Columns[7].HeaderText = "Lok opis";
                dataGridView1.Columns[7].Width = 150;

                DataGridViewColumn column9 = dataGridView1.Columns[8];
                dataGridView1.Columns[8].HeaderText = "FormiranDokument";
                dataGridView1.Columns[8].Width = 40;

                DataGridViewColumn column10 = dataGridView1.Columns[9];
                dataGridView1.Columns[9].HeaderText = "Broj";
                dataGridView1.Columns[9].Width = 40;
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
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds2.Tables[0];

            DataGridViewColumn column20 = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "Korisnik";
            dataGridView2.Columns[0].Width = 50;

            DataGridViewColumn column21 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Artikal";
            dataGridView2.Columns[1].Width = 50;

            DataGridViewColumn column31 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Kod";
            dataGridView2.Columns[2].Width = 120;

            DataGridViewColumn column41 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "Lokacija";
            dataGridView2.Columns[3].Width = 50;

            DataGridViewColumn column51 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "Kolicina";
            dataGridView2.Columns[4].Width = 70;

            DataGridViewColumn column61 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "ID";
            dataGridView1.Columns[5].Width = 30;

            DataGridViewColumn column71 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Preostalo";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column81 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Lokacija naziv";
            dataGridView1.Columns[7].Width = 150;

            DataGridViewColumn column91 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "FormiranDokument";
            dataGridView1.Columns[8].Width = 40;

            DataGridViewColumn column101 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Broj";
            dataGridView1.Columns[9].Width = 40;

        }


        private void button1_Click(object sender, System.EventArgs e)
        {

            decimal KolicinaZaIzdavanje = 0;

         
            
            InsertMedjuskladisni isporuka = new InsertMedjuskladisni();
           
            isporuka.InsertIZDTrenutna(cboKorisnik.Text.Trim());
          
            foreach (DataGridViewRow row in dataGridView1.Rows)
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

            foreach (DataGridViewRow row in dataGridView2.Rows)
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

            foreach (DataGridViewRow row in dataGridView1.Rows)
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
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToInt32(row.Cells[8].Value) == '0')
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

        private void button4_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            InsertMedjuskladisni uzimanje = new InsertMedjuskladisni();

          

            foreach (DataGridViewRow row in dataGridView1.Rows)
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
    }
}
