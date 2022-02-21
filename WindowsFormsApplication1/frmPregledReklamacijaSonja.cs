using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

using Microsoft.Reporting.WinForms;

namespace WindowsFormsApplication1
{
    public partial class frmPregledReklamacijaSonja : Form
    {
        public frmPregledReklamacijaSonja()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnOsvezi_Click(object sender, EventArgs e)
        {
            var select = " select KomintentID, SifraKomintentaMestoIsporuke, MestoIsporuke, " +
           " MestoIsporukeAdresa from Saloni where " +
           " KomintentID = " + Convert.ToInt32(cboKomintenti.SelectedValue);

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            // string value = dataGridView2.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "KomintentID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Sifra Mesta Isporuke";
            dataGridView1.Columns[1].Width = 70;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Mesto Isporuke";
            dataGridView1.Columns[2].Width = 200;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Adresa";
            dataGridView1.Columns[3].Width = 250;
        }

        private void frmPregledReklamacijaSonja_Load(object sender, EventArgs e)
        {
            var select = " select Distinct SysKomitenti.KomintentID, Rtrim(SysKomitenti.NazivKomintenta) as NazivKomintenta from SysKomitenti " +
          " inner join RkShippingZavrseno on SysKomitenti.KomintentID = RkShippingZavrseno.KupacIDz " +
          " inner join RkShippingItemRecZavrseno on RkShippingZavrseno.ShippingID = RkShippingItemRecZavrseno.ShipingIDz";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboKomintenti.DataSource = ds.Tables[0];
            cboKomintenti.DisplayMember = "NazivKomintenta";
            cboKomintenti.ValueMember = "KomintentID";


            var select2 = " select Distinct SysKomitenti.KomintentID, Rtrim(SysKomitenti.NazivKomintenta) as NazivKomintenta from SysKomitenti " +
            " where KomintentID > 1000";

            // var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection);
            var c2 = new SqlConnection(s_connection);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboKupciP.DataSource = ds2.Tables[0];
            cboKupciP.DisplayMember = "NazivKomintenta";
            cboKupciP.ValueMember = "KomintentID";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string saloni = "";
            int prvi = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    if (prvi == 0)
                    {
                        saloni = row.Cells[1].Value.ToString();
                        prvi = 1;
                    }
                    else
                    {
                        saloni = saloni + " , " + row.Cells[1].Value.ToString();
                    }
                }
            }
            var select = " Select Distinct DatumUtovara, Cast(SUBSTRING(DatumUtovara, 1, Charindex('.',DatumUtovara) -1) as int)  as  Dan, Cast(Replace(SUBSTRING(DatumUtovara, Charindex('.',DatumUtovara)+1, Len(DatumUtovara)-12 -1 -( (Charindex('.',DatumUtovara) ))),'.','') as int) as  Mesec " +
            " From RkShippingZavrseno " +
            " inner join RkShippingItemRecZavrseno on RkShippingZavrseno.ShippingID = RkShippingItemRecZavrseno.ShipingIDz " +
            " where SalonIDz in (  " + saloni + " ) order by  Mesec, Dan";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboDatumUtovara.DataSource = ds.Tables[0];
            cboDatumUtovara.DisplayMember = "DatumUtovara";
            cboDatumUtovara.ValueMember = "DatumUtovara";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string saloni = "";
            int prvi = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    if (prvi == 0)
                    {
                        saloni = row.Cells[1].Value.ToString();
                        prvi = 1;
                    }
                    else
                    {
                        saloni = saloni + " , " + row.Cells[1].Value.ToString();
                    }
                }
            }

            var select = " Select Distinct RTrim(Vozilo) as Vozilo From RkShippingZavrseno " +
              " inner join RkShippingItemRecZavrseno on RkShippingZavrseno.ShippingID = RkShippingItemRecZavrseno.ShipingIDz " +
              "  where SalonIDz in ( " + saloni + ") and DatumUtovara = '" + cboDatumUtovara.Text + "'";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboVozila.DataSource = ds.Tables[0];
            cboVozila.DisplayMember = "Vozilo";
            cboVozila.ValueMember = "Vozilo";
        }

        private void refresh1()
        {
            string saloni = "";
            int prvi = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    if (prvi == 0)
                    {
                        saloni = row.Cells[1].Value.ToString();
                        prvi = 1;
                    }
                    else
                    {
                        saloni = saloni + " , " + row.Cells[1].Value.ToString();
                    }
                }
            }

            var select = " SELECT Paleta, RkShippingZavrseno.BrojIstovara as BrojUtovara, RkShippingZavrseno.BrojUtovara as BrojIsporuke , " +
           " MestoIsporuke, RkShippingItemRecZavrseno.ShippingItemRecID , RkShippingItemRecZavrseno.Zapisnik AS Zapisnik, " +
           " RkShippingItemRecZavrseno.SifraReklamacije as SifLargo , RkShippingItemRECZavrseno.NazivReklamacije, " +
           " RkShippingItemRECZavrseno.Opis, RkShippingItemRECZavrseno.Paket, KolicinaOrg,  Kolicina,  " +
           " Paletirano, Paletar as Paletar, VremePakovanja as VremePaletiranja, " +
           " RkShippingItemRecZavrseno.IsporucenoKupcu as Kontrolisano, Kontrolor as KOntrolor , " +
           " RkShippingItemRecZavrseno.VremeKontrole, Razlog as Napomena FROM [dbo].RkShippingItemRecZavrseno " +
           " inner join RkShippingZavrseno on [dbo].RkShippingItemRecZavrseno.ShipingIDz = RkShippingZavrseno.[ShippingID] " +
           " inner join Saloni on RkShippingZavrseno.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where SalonIDz in ( " + saloni + ") and  RkShippingZavrseno.Vozilo  = '" + cboVozila.Text + "' and RkShippingZavrseno.DatumUtovara = '" + cboDatumUtovara.Text + "'";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];
            // string value = dataGridView1.Rows[0].Cells[0].Value.ToString();

            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "Paleta";
            dataGridView2.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Broj utovara";
            dataGridView2.Columns[1].Width = 45;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Broj isporuke";
            dataGridView2.Columns[2].Width = 45;


            DataGridViewColumn column4 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "Mesto isporuke";
            dataGridView2.Columns[3].Width = 190;

            DataGridViewColumn column5 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "Reklamacija ID";
            dataGridView2.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView2.Columns[5];
            dataGridView2.Columns[5].HeaderText = "Zapisnik";
            dataGridView2.Columns[5].Width = 80;



            DataGridViewColumn column7 = dataGridView2.Columns[6];
            dataGridView2.Columns[6].HeaderText = "Šifra reklamacije";
            dataGridView2.Columns[6].Width = 195;


            DataGridViewColumn column8 = dataGridView2.Columns[7];
            dataGridView2.Columns[7].HeaderText = "Naziv Reklamacije";
            dataGridView2.Columns[7].Width = 90;




            DataGridViewColumn column9 = dataGridView2.Columns[8];
            dataGridView2.Columns[8].HeaderText = "Opis";
            dataGridView2.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView2.Columns[9];
            dataGridView2.Columns[9].HeaderText = "Paket";
            dataGridView2.Columns[9].Width = 40;

            DataGridViewColumn column11 = dataGridView2.Columns[10];
            dataGridView2.Columns[10].HeaderText = "Količ. org";
            dataGridView2.Columns[10].Width = 40;


            DataGridViewColumn column12 = dataGridView2.Columns[11];
            dataGridView2.Columns[11].HeaderText = "Količina";
            dataGridView2.Columns[11].Width = 45;

            DataGridViewColumn column13 = dataGridView2.Columns[12];
            dataGridView2.Columns[12].HeaderText = "Pal. Ček";
            dataGridView2.Columns[12].Width = 35;

            DataGridViewColumn column14 = dataGridView2.Columns[13];
            dataGridView2.Columns[13].HeaderText = "Paletar";
            dataGridView2.Columns[13].Width = 150;

            DataGridViewColumn column15 = dataGridView2.Columns[14];
            dataGridView2.Columns[14].HeaderText = "Vreme pal.";
            dataGridView2.Columns[14].Width = 100;

            DataGridViewColumn column16 = dataGridView2.Columns[15];
            dataGridView2.Columns[15].HeaderText = "Kont. ček";
            dataGridView2.Columns[15].Width = 35;

            DataGridViewColumn column17 = dataGridView2.Columns[16];
            dataGridView2.Columns[16].HeaderText = "Kontr.";
            dataGridView2.Columns[16].Width = 150;

            DataGridViewColumn column18 = dataGridView2.Columns[17];
            dataGridView2.Columns[17].HeaderText = "Vreme kont";
            dataGridView2.Columns[17].Width = 100;

            DataGridViewColumn column19 = dataGridView2.Columns[18];
            dataGridView2.Columns[18].HeaderText = "Napomena";
            dataGridView2.Columns[18].Width = 400;



            dataGridView2.Refresh();
            // dataGridView5.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refresh1();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            InsertReklamacijaSonja ins = new InsertReklamacijaSonja();
            ins.DelReklSonja();

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                ins.InsertReklSonja(Convert.ToInt32(row.Cells[4].Value.ToString()));
            }
            Perftech_BeogradDataSet3TableAdapters.RepReklamacijeSonjaTableAdapter ta = new Perftech_BeogradDataSet3TableAdapters.RepReklamacijeSonjaTableAdapter();
            Perftech_BeogradDataSet3.RepReklamacijeSonjaDataTable dt = new Perftech_BeogradDataSet3.RepReklamacijeSonjaDataTable();

            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;


            //  ReportParameter[] par = new ReportParameter[0];

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptReklamacijaSonja.rdlc";
            //            reportViewer1.LocalReport.SetParameters(par);

            reportViewer1.LocalReport.DataSources.Add(rds);
            // reportViewer1.LocalReport.DataSources.Add(rds2);
            reportViewer1.RefreshReport();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var select = " select KomintentID, SifraKomintentaMestoIsporuke, MestoIsporuke, " +
         " MestoIsporukeAdresa from Saloni where " +
         " KomintentID = " + Convert.ToInt32(cboKupciP.SelectedValue);

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];

            // string value = dataGridView2.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "KomintentID";
            dataGridView3.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "Sifra Mesta Isporuke";
            dataGridView3.Columns[1].Width = 70;

            DataGridViewColumn column2 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "Mesto Isporuke";
            dataGridView3.Columns[2].Width = 200;

            DataGridViewColumn column3 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "Adresa";
            dataGridView3.Columns[3].Width = 250;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string saloni = "";
            int prvi = 0;
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected)
                {
                    if (prvi == 0)
                    {
                        saloni = row.Cells[1].Value.ToString();
                        prvi = 1;
                    }
                    else
                    {
                        saloni = saloni + " , " + row.Cells[1].Value.ToString();
                        // MessageBox.Show(saloni);
                    }
                }
            }
            var select = " Select Distinct DatumUtovara, Cast(SUBSTRING(DatumUtovara, 1, Charindex('.',DatumUtovara) -1) as int)  as  Dan, Cast(Replace(SUBSTRING(DatumUtovara, Charindex('.',DatumUtovara)+1, Len(DatumUtovara)-12 -1 -( (Charindex('.',DatumUtovara) ))),'.','') as int) as  Mesec " +
            " From RkShippingZavrseno " +
            " inner join RkShippingItemPakZavrseno on RkShippingZavrseno.ShippingID = RkShippingItemPakZavrseno.ShipingIDz " +
            " where SalonIDz in (  " + saloni + " ) order by  Mesec, Dan";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboDatumUtovaraP.DataSource = ds.Tables[0];
            cboDatumUtovaraP.DisplayMember = "DatumUtovara";
            cboDatumUtovaraP.ValueMember = "DatumUtovara";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string saloni = "";
            int prvi = 0;
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected)
                {
                    if (prvi == 0)
                    {
                        saloni = row.Cells[1].Value.ToString();
                        prvi = 1;
                    }
                    else
                    {
                        saloni = saloni + " , " + row.Cells[1].Value.ToString();
                    }
                }
            }

            var select = " Select Distinct RTrim(Vozilo) as Vozilo From RkShipping " +
              " inner join RkShippingItemPakZavrseno on RkShippingZavrseno.ShippingID = RkShippingItemPakZavrseno.ShipingIDz " +
              "  where SalonIDz in ( " + saloni + ") and DatumUtovara = '" + cboDatumUtovaraP.Text + "'";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboVozilaP.DataSource = ds.Tables[0];
            cboVozilaP.DisplayMember = "Vozilo";
            cboVozilaP.ValueMember = "Vozilo";
        }
        private void refresh1Pak()
        {
            string saloni = "";
            int prvi = 0;
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected)
                {
                    if (prvi == 0)
                    {
                        saloni = row.Cells[1].Value.ToString();
                        prvi = 1;
                    }
                    else
                    {
                        saloni = saloni + " , " + row.Cells[1].Value.ToString();
                    }
                }
            }

            var select = " SELECT Paleta, RkShippingZavrseno.BrojIstovara as BrojUtovara, RkShippingZavrseno.BrojUtovara as BrojIsporuke , " +
            " MestoIsporuke, RkShippingItemPakZavrseno.ShippingItemPakID , RkShippingItemPakZavrseno.PaketName, " +
            " RkShippingItemPakZavrseno.LargoPakID as SifLargo , RkShippingItemPakZavrseno.LargoNaziv, KolicinaOrg, Kolicina, " +
            " Pakerista as Paletirano, Pakuje as Paletar, VremePakovanja as VremePaletiranja,Kontrolisao as Kontrolisano, " +
            " KOntrolise as Kontrolor, VremeKontrole FROM [dbo].RkShippingItemPakZavrseno inner join RkShippingZavrseno  " +
            " on [dbo].RkShippingItemPakZavrseno.ShipingIDz = RkShippingZavrseno.[ShippingID] " +
            " inner join Saloni on RkShippingZavrseno.SalonIDz = Saloni.SifraKomintentaMestoIsporuke " +
            " where SalonIDz in ( " + saloni + ") and RkShippingZavrseno.Vozilo  = '" + cboVozilaP.Text + "' and RkShippingZavrseno.DatumUtovara = '" + cboDatumUtovaraP.Text + "'";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView4.ReadOnly = true;
            dataGridView4.DataSource = ds.Tables[0];
            // string value = dataGridView1.Rows[0].Cells[0].Value.ToString();

            DataGridViewColumn column = dataGridView4.Columns[0];
            dataGridView4.Columns[0].HeaderText = "Paleta";
            dataGridView4.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView4.Columns[1];
            dataGridView4.Columns[1].HeaderText = "Broj utovara";
            dataGridView4.Columns[1].Width = 45;

            DataGridViewColumn column3 = dataGridView4.Columns[2];
            dataGridView4.Columns[2].HeaderText = "Broj isporuke";
            dataGridView4.Columns[2].Width = 45;


            DataGridViewColumn column4 = dataGridView4.Columns[3];
            dataGridView4.Columns[3].HeaderText = "Mesto isporuke";
            dataGridView4.Columns[3].Width = 190;

            DataGridViewColumn column5 = dataGridView4.Columns[4];
            dataGridView4.Columns[4].HeaderText = "Paket ID";
            dataGridView4.Columns[4].Width = 70;

            DataGridViewColumn column6 = dataGridView4.Columns[5];
            dataGridView4.Columns[5].HeaderText = "Paket name Webs";
            dataGridView4.Columns[5].Width = 190;



            DataGridViewColumn column7 = dataGridView4.Columns[6];
            dataGridView4.Columns[6].HeaderText = "Šif. Largo";
            dataGridView4.Columns[6].Width = 50;


            DataGridViewColumn column8 = dataGridView4.Columns[7];
            dataGridView4.Columns[7].HeaderText = "Paket name Largo";
            dataGridView4.Columns[7].Width = 190;

            DataGridViewColumn column9 = dataGridView4.Columns[8];
            dataGridView4.Columns[8].HeaderText = "Količ. org";
            dataGridView4.Columns[8].Width = 40;


            DataGridViewColumn column10 = dataGridView4.Columns[9];
            dataGridView4.Columns[9].HeaderText = "Količina";
            dataGridView4.Columns[9].Width = 45;


            DataGridViewColumn column11 = dataGridView4.Columns[10];
            dataGridView4.Columns[10].HeaderText = "Palet. ček";
            dataGridView4.Columns[10].Width = 50;

            DataGridViewColumn column12 = dataGridView4.Columns[11];
            dataGridView4.Columns[11].HeaderText = "Paletar";
            dataGridView4.Columns[11].Width = 150;

            DataGridViewColumn column13 = dataGridView4.Columns[12];
            dataGridView4.Columns[12].HeaderText = "Vreme paletiranja";
            dataGridView4.Columns[12].Width = 100;

            DataGridViewColumn column14 = dataGridView4.Columns[13];
            dataGridView4.Columns[13].HeaderText = "Kontro. ček";
            dataGridView4.Columns[13].Width = 50;

            DataGridViewColumn column15 = dataGridView4.Columns[14];
            dataGridView4.Columns[14].HeaderText = "Kontrolor";
            dataGridView4.Columns[14].Width = 150;

            DataGridViewColumn column16 = dataGridView4.Columns[15];
            dataGridView4.Columns[15].HeaderText = "Vreme kontrole";
            dataGridView4.Columns[15].Width = 100;




            dataGridView4.Refresh();
            // dataGridView5.Visible = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            refresh1Pak();
        }

        private void button9_Click(object sender, EventArgs e)
        {


            Perftech_BeogradDataSet4TableAdapters.repPaketiSonjaTableAdapter ta = new Perftech_BeogradDataSet4TableAdapters.repPaketiSonjaTableAdapter();
            Perftech_BeogradDataSet4.repPaketiSonjaDataTable dt = new Perftech_BeogradDataSet4.repPaketiSonjaDataTable();

            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;


            //  ReportParameter[] par = new ReportParameter[0];

            reportViewer2.LocalReport.DataSources.Clear();
            reportViewer2.LocalReport.ReportPath = "Report10.rdlc";
            //            reportViewer1.LocalReport.SetParameters(par);

            reportViewer2.LocalReport.DataSources.Add(rds);
            // reportViewer1.LocalReport.DataSources.Add(rds2);
            reportViewer2.RefreshReport();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            InsertReklamacijaSonja ins = new InsertReklamacijaSonja();
            ins.DelPakSonja();

            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                if (row.Selected)
                {
                    ins.InsertPakSonja(Convert.ToInt32(row.Cells[4].Value.ToString()));
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            PovuciSveKupce();
        }

        private void PovuciSveKupce()
        {
            //select Distinct SysKomitenti.KomintentID, Rtrim(SysKomitenti.NazivKomintenta) as NazivKomintenta from SysKomitenti " +
            //" where KomintentID > 1000
            var select = " select Distinct SysKomitenti.KomintentID, Rtrim(SysKomitenti.NazivKomintenta) as NazivKomintenta from SysKomitenti  where KomintentID > 1000";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView5.ReadOnly = true;
            dataGridView5.DataSource = ds.Tables[0];

            // string value = dataGridView2.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView5.Columns[0];
            dataGridView5.Columns[0].HeaderText = "KomintentID";
            dataGridView5.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView5.Columns[1];
            dataGridView5.Columns[1].HeaderText = "Naziv";
            dataGridView5.Columns[1].Width = 70;


        }


        private void PovuciSveDatumeP()
        {
            string kupci = "";
            int prvi = 0;
            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                if (row.Selected)
                {
                    if (prvi == 0)
                    {
                        kupci = row.Cells[0].Value.ToString();
                        prvi = 1;
                    }
                    else
                    {
                        kupci = kupci + " , " + row.Cells[0].Value.ToString();
                        // MessageBox.Show(saloni);
                    }
                }
            }
            var select = " Select t1.* from (Select Distinct  DatumUtovara, Cast(SUBSTRING(DatumUtovara, 1, Charindex('.',DatumUtovara) -1) as int)  as  Dan, Cast(Replace(SUBSTRING(DatumUtovara, Charindex('.',DatumUtovara)+1, Len(DatumUtovara)-12 -1 -( (Charindex('.',DatumUtovara) ))),'.','') as int) as  Mesec " +
  " ,SysKomitenti.KomintentID From RkShippingZavrseno " +
  " inner join RkShippingItemPakZavrseno on RkShippingZavrseno.ShippingID = RkShippingItemPakZavrseno.ShipingIDz " +
  " inner join SysKomitenti on SysKomitenti.KomintentID = RkShippingZavrseno.KupacIdz " +
 " union " +
 " Select Distinct  DatumUtovara, Cast(SUBSTRING(DatumUtovara, 1, Charindex('.', DatumUtovara) - 1) as int) as Dan, Cast(Replace(SUBSTRING(DatumUtovara, Charindex('.', DatumUtovara) + 1, Len(DatumUtovara) - 12 - 1 - ((Charindex('.', DatumUtovara)))), '.', '') as int) as Mesec " +
 " ,SysKomitenti.KomintentID From RkShippingArhiv " +
 " inner join RkShippingItemPakArhiv on RkShippingArhiv.ShippingID = RkShippingItemPakArhiv.ShipingIDz " +
 " inner join SysKomitenti on SysKomitenti.KomintentID = RkShippingArhiv.KupacIdz " +
 " where RkShippingArhiv.ShippingID > 2100000) t1 " +
 " where t1.KomintentID in (" + kupci + ") order by  Mesec, Dan";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView7.ReadOnly = true;
            dataGridView7.DataSource = ds.Tables[0];

            // string value = dataGridView2.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView7.Columns[0];
            dataGridView7.Columns[0].HeaderText = "Datum";
            dataGridView7.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView7.Columns[1];
            dataGridView7.Columns[1].HeaderText = "Dan";
            dataGridView7.Columns[1].Width = 70;

            DataGridViewColumn column2 = dataGridView7.Columns[1];
            dataGridView7.Columns[1].HeaderText = "Mesec";
            dataGridView7.Columns[1].Width = 70;

        }

        private void button12_Click(object sender, EventArgs e)
        {
            PovuciSveDatumeP();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            PozoviStavkePoKomitentu();
        }

        private void PozoviStavkePoKomitentu()
        {
            string kupci = "";
            int prvi = 0;
            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                if (row.Selected)
                {
                    if (prvi == 0)
                    {
                        kupci = row.Cells[0].Value.ToString();
                        prvi = 1;
                    }
                    else
                    {
                        kupci = kupci + " , " + row.Cells[0].Value.ToString();
                        // MessageBox.Show(saloni);
                    }
                }
            }


            string datumi = "";
            int prvid = 0;
            foreach (DataGridViewRow row in dataGridView7.Rows)
            {
                if (row.Selected)
                {
                    if (prvid == 0)
                    {
                        datumi = "'" + row.Cells[0].Value.ToString() + "'";
                        prvid = 1;
                    }
                    else
                    {
                        datumi = datumi + " , " + "'" +  row.Cells[0].Value.ToString() + "'";
                        // MessageBox.Show(saloni);
                    }
                }
            }
            var select = " Select Sum(t1.Kolicina) as kolicina, t1.LargoPakID, t1.LargoNaziv, t1.NazivKomintenta from " +
" (Select SysKomitenti.KomintentID, RkShippingZavrseno.DatumUtovara, " +
"  KolicinaOrg as kolicina, LargoPakID, LargoNaziv, NazivKomintenta " +
"  From RkShippingZavrseno " +
 " inner join RkShippingItemPakZavrseno on RkShippingZavrseno.ShippingID = RkShippingItemPakZavrseno.ShipingIDz " +
"  inner join SysKomitenti on SysKomitenti.KomintentID = RkShippingZavrseno.KupacIdz " +
"  union " +
"  Select SysKomitenti.KomintentID, RkShippingArhiv.DatumUtovara, KolicinaOrg as kolicina, LargoPakID, LargoNaziv, NazivKomintenta " +
"  From RkShippingArhiv " +
"  inner join RkShippingItemPakArhiv on RkShippingArhiv.ShippingID = RkShippingItemPakArhiv.ShipingIDz " +
"  inner join SysKomitenti on SysKomitenti.KomintentID = RkShippingArhiv.KupacIdz " +
"  where RkShippingArhiv.ShippingID > 2100000 " +
" ) t1 " +
"  where t1.KomintentID in (" + kupci + ") and t1.DatumUtovara in (" + datumi + ") " +
 " group by t1.LargoPakID, t1.LargoNaziv, t1.NazivKomintenta ";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView6.ReadOnly = true;
            dataGridView6.DataSource = ds.Tables[0];

            // string value = dataGridView2.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView6.Columns[0];
            dataGridView6.Columns[0].HeaderText = "Kolicina";
            dataGridView6.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView6.Columns[1];
            dataGridView6.Columns[1].HeaderText = "LargoID";
            dataGridView6.Columns[1].Width = 70;

            DataGridViewColumn column2 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "Largo naziv";
            dataGridView6.Columns[2].Width = 170;

            DataGridViewColumn column3 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "Klijent";
            dataGridView6.Columns[3].Width = 250;

        }
    }
}

