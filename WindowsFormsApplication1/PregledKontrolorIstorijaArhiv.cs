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

namespace WindowsFormsApplication1
{
     
    public partial class PregledKontrolorIstorijaArhiv : Form
    {
        private int pregled = 1;

        public PregledKontrolorIstorijaArhiv()
        {
            InitializeComponent();
        }

        private void PregledKontrolorIstorijaArhiv_Load(object sender, EventArgs e)
        {
            var select = " Select Distinct DatumUtovara, Cast(SUBSTRING(DatumUtovara, 1, Charindex('.',DatumUtovara) -1) as int)  as  Dan, Cast(Replace(SUBSTRING(DatumUtovara, Charindex('.',DatumUtovara)+1, Len(DatumUtovara)-12 -1 -( (Charindex('.',DatumUtovara) ))),'.','') as int) as  Mesec  From RkShippingArhiv   order by  Mesec, Dan";
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
            var select = " Select Distinct RTrim(Vozilo) as Vozilo From RkShippingArhiv where  DatumUtovara = '" + cboDatumUtovara.Text + "'";
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

        private void button3_Click(object sender, EventArgs e)
        {
            var select = " Select Distinct RkShippingItemPakArhiv.Paleta From RkShippingArhiv inner Join RkShippingItemPakArhiv on RkShippingArhiv.ShippingId = RkShippingItemPakArhiv.ShipingIdz where  RkShippingArhiv.Vozilo = '" + cboVozila.Text + "' and RkShippingArhiv.DatumUtovara = '" + cboDatumUtovara.Text + "'";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboPaleta.DataSource = ds.Tables[0];
            cboPaleta.DisplayMember = "Paleta";
            cboPaleta.ValueMember = "Paleta";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refresh1();
            refresh3();
        }

        private void refresh1()
        {
            // var select = " SELECT Paleta, RkShipping.BrojIstovara as BrojUtovara, RkShipping.BrojUtovara as BrojIsporuke , MestoIsporuke, RkShippingItemPak.ShippingItemPakID , RkShippingItemPak.PaketName, RkShippingItemPak.LargoPakID as SifLargo , RkShippingItemPak.LargoNaziv, Kolicina, KolicinaOrg,  Pakerista as Paletirano, Pakuje as Paletar, VremePakovanja as VremePaletiranja,Kontrolisao as Kontrolisano, KOntrolise as Kontrolor, VremeKontrole FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";

            var select = " SELECT Paleta, RkShippingArhiv.BrojIstovara as BrojUtovara, RkShippingArhiv.BrojUtovara as BrojIsporuke , MestoIsporuke, RkShippingItemPakArhiv.ShippingItemPakID , RkShippingItemPakArhiv.PaketName, RkShippingItemPakArhiv.LargoPakID as SifLargo , RkShippingItemPakArhiv.LargoNaziv, KolicinaOrg, Kolicina,  Pakerista as Paletirano, Pakuje as Paletar, VremePakovanja as VremePaletiranja,Kontrolisao as Kontrolisano, KOntrolise as Kontrolor, VremeKontrole, Razlog FROM [dbo].RkShippingItemPakArhiv inner join RkShippingArhiv on [dbo].RkShippingItemPakArhiv.ShipingIDz = RkShippingArhiv.[ShippingID] inner join Saloni on RkShippingArhiv.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShippingArhiv.Vozilo  = '" + cboVozila.Text + "' and RkShippingArhiv.DatumUtovara = '" + cboDatumUtovara.Text + "'";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            // string value = dataGridView1.Rows[0].Cells[0].Value.ToString();

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "Paleta";
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Broj utovara";
            dataGridView1.Columns[1].Width = 45;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Broj isporuke";
            dataGridView1.Columns[2].Width = 45;


            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Mesto isporuke";
            dataGridView1.Columns[3].Width = 190;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Paket ID";
            dataGridView1.Columns[4].Width = 70;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Paket name Webs";
            dataGridView1.Columns[5].Width = 190;



            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Šif. Largo";
            dataGridView1.Columns[6].Width = 50;


            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Paket name Largo";
            dataGridView1.Columns[7].Width = 190;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Količ. org";
            dataGridView1.Columns[8].Width = 40;


            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Količina";
            dataGridView1.Columns[9].Width = 45;


            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Palet. ček";
            dataGridView1.Columns[10].Width = 50;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Paletar";
            dataGridView1.Columns[11].Width = 150;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Vreme paletiranja";
            dataGridView1.Columns[12].Width = 100;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Kontro. ček";
            dataGridView1.Columns[13].Width = 50;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Kontrolor";
            dataGridView1.Columns[14].Width = 150;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Vreme kontrole";
            dataGridView1.Columns[15].Width = 100;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Nije isporučeno - RAZLOG";
            dataGridView1.Columns[16].Width = 400;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string Pakerista = row.Cells[10].Value.ToString();
                string Kontrolisao = row.Cells[13].Value.ToString();
                if ((Pakerista == "1") && (Kontrolisao == "1"))
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.SelectionBackColor = Color.Green;
                }
                else if ((Pakerista == "1") && (Kontrolisao == "0"))
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                    row.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                }
                else if ((Pakerista == "0") && (Kontrolisao == "1"))
                {
                    row.DefaultCellStyle.BackColor = Color.GreenYellow;
                    row.DefaultCellStyle.SelectionBackColor = Color.GreenYellow;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.SelectionBackColor = Color.Red;
                }
            }
            dataGridView1.Refresh();
            // dataGridView5.Visible = false;
        }

        private void refresh3()
        {
            //var select = " SELECT Paleta, RkShipping.BrojIstovara as BrojUtovara, RkShipping.BrojUtovara as BrojIsporuke , MestoIsporuke, RkShippingItemRec.ShippingItemRecID , RkShippingItemREC.NazivReklamacije, RkShippingItemRec.SifraElementa as SifLargo , RkShippingItemREC.Opis, Kolicina, KolicinaOrg,  0 as Paletirano, '' as Paletar, VremePakovanja as VremePaletiranja,RkShippingItemRec.IsporucenoKupcu as Kontrolisano, dbo.RkShippingItemRec.Zapisnik AS Zapisnik, dbo.RkShippingItemRec.VremeKontrole FROM [dbo].RkShippingItemRec inner join RkShipping on [dbo].RkShippingItemRec.ShipingIDz = RkShipping.[ShippingID] inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";

            var select = " SELECT Paleta, RkShippingArhiv.BrojIstovara as BrojUtovara, RkShippingArhiv.BrojUtovara as BrojIsporuke , MestoIsporuke, RkShippingItemRecArhiv.ShippingItemRecID , RkShippingItemRecArhiv.Zapisnik AS Zapisnik, RkShippingItemRecArhiv.SifraReklamacije as SifLargo , RkShippingItemRECArhiv.NazivReklamacije,  RkShippingItemRECArhiv.Opis,KolicinaOrg,  Kolicina,   0 as Paletirano, Paletar as Paletar, VremePakovanja as VremePaletiranja, RkShippingItemRecArhiv.KontrolisaoVideo as Kontrolisano, Kontrolor as KOntrolor ,  RkShippingItemRecArhiv.VremeKontrole, Razlog FROM [dbo].RkShippingItemRecArhiv inner join RkShippingArhiv on [dbo].RkShippingItemRecArhiv.ShipingIDz = RkShippingArhiv.[ShippingID] inner join Saloni on RkShippingArhiv.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShippingArhiv.Vozilo  = '" + cboVozila.Text + "' and RkShippingArhiv.DatumUtovara = '" + cboDatumUtovara.Text + "'";
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
            dataGridView2.Columns[9].HeaderText = "Količ. org";
            dataGridView2.Columns[9].Width = 40;


            DataGridViewColumn column11 = dataGridView2.Columns[10];
            dataGridView2.Columns[10].HeaderText = "Količina";
            dataGridView2.Columns[10].Width = 45;

            DataGridViewColumn column12 = dataGridView2.Columns[11];
            dataGridView2.Columns[11].HeaderText = "Pal. Ček";
            dataGridView2.Columns[11].Width = 35;

            DataGridViewColumn column13 = dataGridView2.Columns[12];
            dataGridView2.Columns[12].HeaderText = "Paletar";
            dataGridView2.Columns[12].Width = 150;

            DataGridViewColumn column14 = dataGridView2.Columns[13];
            dataGridView2.Columns[13].HeaderText = "Vreme pal.";
            dataGridView2.Columns[13].Width = 100;

            DataGridViewColumn column15 = dataGridView2.Columns[14];
            dataGridView2.Columns[14].HeaderText = "Kont. ček";
            dataGridView2.Columns[14].Width = 35;

            DataGridViewColumn column16 = dataGridView2.Columns[15];
            dataGridView2.Columns[15].HeaderText = "Kontr.";
            dataGridView2.Columns[15].Width = 150;

            DataGridViewColumn column17 = dataGridView2.Columns[16];
            dataGridView2.Columns[16].HeaderText = "Vreme kont";
            dataGridView2.Columns[16].Width = 100;

            DataGridViewColumn column18 = dataGridView2.Columns[17];
            dataGridView2.Columns[17].HeaderText = "Nije isporučeno - RAZLOG";
            dataGridView2.Columns[17].Width = 400;


            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                string Pakerista = row.Cells[11].Value.ToString();
                string Kontrolisao = row.Cells[14].Value.ToString();
                if ((Pakerista == "1") && (Kontrolisao == "1"))
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.SelectionBackColor = Color.Green;
                }
                else if ((Pakerista == "1") && (Kontrolisao == "0"))
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                    row.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                }
                else if ((Pakerista == "0") && (Kontrolisao == "1"))
                {
                    row.DefaultCellStyle.BackColor = Color.GreenYellow;
                    row.DefaultCellStyle.SelectionBackColor = Color.GreenYellow;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.SelectionBackColor = Color.Red;
                }
            }
            dataGridView2.Refresh();
            // dataGridView5.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pregled = 2;//Pregled po paleti
            var select = " SELECT Paleta, RkShipping.BrojIstovara as BrojUtovara, RkShipping.BrojUtovara as BrojIsporuke , MestoIsporuke, RkShippingItemPak.ShippingItemPakID , RkShippingItemPak.PaketName, RkShippingItemPak.LargoPakID as SifLargo , RkShippingItemPak.LargoNaziv, Kolicina, KolicinaOrg,  Pakerista as Paletirano, Pakuje as Paletar, VremePakovanja as VremePaletiranja,Kontrolisao as Kontrolisano, KOntrolise as Kontrolor, VremeKontrole, Razlog FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "' and [dbo].RkShippingItemPak.Paleta = '" + cboPaleta.Text + "'";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            string value = dataGridView1.Rows[0].Cells[0].Value.ToString();

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "Paleta";
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Broj utovara";
            dataGridView1.Columns[1].Width = 45;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Broj isporuke";
            dataGridView1.Columns[2].Width = 45;


            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Mesto isporuke";
            dataGridView1.Columns[3].Width = 190;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Paket ID";
            dataGridView1.Columns[4].Width = 70;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Paket name Webs";
            dataGridView1.Columns[5].Width = 190;



            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Šif. Largo";
            dataGridView1.Columns[6].Width = 50;


            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Paket name Largo";
            dataGridView1.Columns[7].Width = 190;


            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Količina";
            dataGridView1.Columns[8].Width = 45;


            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Količ. org";
            dataGridView1.Columns[9].Width = 40;


            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Palet. ček";
            dataGridView1.Columns[10].Width = 50;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Paletar";
            dataGridView1.Columns[11].Width = 150;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Vreme paletiranja";
            dataGridView1.Columns[12].Width = 100;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Kontro. ček";
            dataGridView1.Columns[13].Width = 50;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Kontrolor";
            dataGridView1.Columns[14].Width = 150;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Vreme kontrole";
            dataGridView1.Columns[15].Width = 100;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Nije isporučeno - RAZLOG";
            dataGridView1.Columns[16].Width = 400;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string Pakerista = row.Cells[10].Value.ToString();
                string Kontrolisao = row.Cells[13].Value.ToString();
                if ((Pakerista == "1") && (Kontrolisao == "1"))
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.SelectionBackColor = Color.Green;
                }
                else if ((Pakerista == "1") && (Kontrolisao == "0"))
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                    row.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                }
                else if ((Pakerista == "0") && (Kontrolisao == "1"))
                {
                    row.DefaultCellStyle.BackColor = Color.GreenYellow;
                    row.DefaultCellStyle.SelectionBackColor = Color.GreenYellow;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.SelectionBackColor = Color.Red;
                }
            }
            dataGridView1.Refresh();
            refresh4();
        }

        private void refresh4()
        {
            var select = " SELECT Paleta, RkShippingArhiv.BrojIstovara as BrojUtovara, RkShippingArhiv.BrojUtovara as BrojIsporuke , MestoIsporuke, RkShippingItemRecArhiv.ShippingItemRecID , dbo.RkShippingItemRecArhiv.Zapisnik AS Zapisnik, RkShippingItemRecArhiv.SifraReklamacije as SifLargo , RkShippingItemRECArhiv.NazivReklamacije,  RkShippingItemRECArhiv.Opis,KolicinaOrg,  Kolicina,   0 as Paletirano, Paletar as Paletar, VremePakovanja as VremePaletiranja, RkShippingItemRecArhiv.IsporucenoKupcu as Kontrolisano, Kontrolor as KOntrolor , dbo.RkShippingItemRecArhiv.VremeKontrole, RkShippingItemRecArhiv.Razlog FROM [dbo].RkShippingItemRecArhiv inner join RkShippingArhiv on [dbo].RkShippingItemRecArhiv.ShipingIDz = RkShippingArhiv.[ShippingID] inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShippingArhiv.Vozilo  = '" + cboVozila.Text + "' and RkShippingArhiv.DatumUtovara = '" + cboDatumUtovara.Text + "' and [dbo].RkShippingItemRecArhiv.Paleta = '" + cboPaleta.Text + "'";

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
            dataGridView2.Columns[9].HeaderText = "Količ. org";
            dataGridView2.Columns[9].Width = 40;


            DataGridViewColumn column11 = dataGridView2.Columns[10];
            dataGridView2.Columns[10].HeaderText = "Količina";
            dataGridView2.Columns[10].Width = 45;

            DataGridViewColumn column12 = dataGridView2.Columns[11];
            dataGridView2.Columns[11].HeaderText = "Pal. Ček";
            dataGridView2.Columns[11].Width = 35;

            DataGridViewColumn column13 = dataGridView2.Columns[12];
            dataGridView2.Columns[12].HeaderText = "Paletar";
            dataGridView2.Columns[12].Width = 150;

            DataGridViewColumn column14 = dataGridView2.Columns[13];
            dataGridView2.Columns[13].HeaderText = "Vreme pal.";
            dataGridView2.Columns[13].Width = 100;

            DataGridViewColumn column15 = dataGridView2.Columns[14];
            dataGridView2.Columns[14].HeaderText = "Kont. ček";
            dataGridView2.Columns[14].Width = 35;

            DataGridViewColumn column16 = dataGridView2.Columns[15];
            dataGridView2.Columns[15].HeaderText = "Kontr.";
            dataGridView2.Columns[15].Width = 150;

            DataGridViewColumn column17 = dataGridView2.Columns[16];
            dataGridView2.Columns[16].HeaderText = "Vreme kont";
            dataGridView2.Columns[16].Width = 100;

            DataGridViewColumn column18 = dataGridView2.Columns[17];
            dataGridView2.Columns[17].HeaderText = "Nije isporučeno - RAZLOG";
            dataGridView2.Columns[17].Width = 100;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                string Pakerista = row.Cells[11].Value.ToString();
                string Kontrolisao = row.Cells[14].Value.ToString();
                if ((Pakerista == "1") && (Kontrolisao == "1"))
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.SelectionBackColor = Color.Green;
                }
                else if ((Pakerista == "1") && (Kontrolisao == "0"))
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                    row.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                }
                else if ((Pakerista == "0") && (Kontrolisao == "1"))
                {
                    row.DefaultCellStyle.BackColor = Color.GreenYellow;
                    row.DefaultCellStyle.SelectionBackColor = Color.GreenYellow;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.SelectionBackColor = Color.Red;
                }
            }
            dataGridView2.Refresh();
            // dataGridView5.Visible = false;
        }
    }
}
