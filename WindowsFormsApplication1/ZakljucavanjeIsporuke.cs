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
using System.Globalization;

namespace WindowsFormsApplication1
{
    public partial class ZakljucavanjeIsporuke : Form
    {
        public ZakljucavanjeIsporuke()
        {
            InitializeComponent();
        }

        private void ZakljucavanjeIsporuke_Load(object sender, EventArgs e)
        { /*
            var select = " Select Distinct RTrim(Vozilo) as Vozilo From RkShipping where Stanje = 1";
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
            */
            var select = " Select Distinct DatumUtovara, Cast(SUBSTRING(DatumUtovara, 1, Charindex('.',DatumUtovara) -1) as int)  as  Dan, Cast(Replace(SUBSTRING(DatumUtovara, Charindex('.',DatumUtovara)+1, Len(DatumUtovara)-12 -1 -( (Charindex('.',DatumUtovara) ))),'.','') as int) as  Mesec  From RkShipping where Stanje = 1  order by  Mesec, Dan";
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
        { /*
            var select = " Select Distinct DatumUtovara, Cast(SUBSTRING(DatumUtovara, 1, Charindex('.',DatumUtovara) -1) as int)  as  Dan, Cast(Replace(SUBSTRING(DatumUtovara, Charindex('.',DatumUtovara)+1, Len(DatumUtovara)-12 -1 -( (Charindex('.',DatumUtovara) ))),'.','') as int) as  Mesec from RkShipping  where Stanje = 1 and Vozilo = '" + cboVozila.Text + "' order by Mesec, Dan";
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
           */
            var select = " Select Distinct RTrim(Vozilo) as Vozilo From RkShipping where Stanje = 1 and DatumUtovara = '" + cboDatumUtovara.Text + "'";
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

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            var select = " SELECT Paleta, RkShipping.BrojIstovara as BrojUtovara, RkShipping.BrojUtovara as BrojIsporuke , MestoIsporuke, RkShippingItemPak.ShippingItemPakID , RkShippingItemPak.PaketName, RkShippingItemPak.LargoPakID as SifLargo , RkShippingItemPak.LargoNaziv, KolicinaOrg, Kolicina,  Pakerista as Paletirano, Pakuje as Paletar, VremePakovanja as VremePaletiranja,Kontrolisao as Kontrolisano, KOntrolise as Kontrolor, VremeKontrole FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";
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

           

            */










                        //     SELECT Paleta, RkShipping.BrojIstovara as BrojUtovara, RkShipping.BrojUtovara as BrojIsporuke , MestoIsporuke, RkShippingItemPak.ShippingItemPakID , RkShippingItemPak.PaketName, RkShippingItemPak.LargoPakID as SifLargo , RkShippingItemPak.LargoNaziv, KolicinaOrg, Kolicina,  Pakerista as Paletirano, Pakuje as Paletar, VremePakovanja as VremePaletiranja,Kontrolisao as Kontrolisano, KOntrolise as Kontrolor, VremeKontrole FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'"
                         //   SELECT Paleta, RkShipping.BrojIstovara as BrojUtovara, RkShipping.BrojUtovara as BrojIsporuke , MestoIsporuke, RkShippingItemPak.ShippingItemPakID , RkShippingItemPak.PaketName, RkShippingItemPak.LargoPakID as SifLargo , RkShippingItemPak.LargoNaziv, KolicinaOrg, Kolicina,  Pakerista as Paletirano, Pakuje as Paletar, VremePakovanja as VremePaletiranja,Kontrolisao as Kontrolisano, KOntrolise as Kontrolor, VremeKontrole FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "' and Kontrolisao = 1";
            
            
            string upit2 = " SELECT Paleta, RkShipping.BrojIstovara as BrojUtovara, RkShipping.BrojUtovara as BrojIsporuke , MestoIsporuke, RkShippingItemPak.ShippingItemPakID , RkShippingItemPak.PaketName, RkShippingItemPak.LargoPakID as SifLargo , RkShippingItemPak.LargoNaziv, KolicinaOrg, Kolicina,  Pakerista as Paletirano, Pakuje as Paletar, VremePakovanja as VremePaletiranja,Kontrolisao as Kontrolisano, KOntrolise as Kontrolor, VremeKontrole FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "' and Kontrolisao = 1";
            
            
           // 
            
            var select = upit2;

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            //string value = dataGridView1.Rows[0].Cells[0].Value.ToString();
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
            dataGridView1.Columns[4].Width = 50;

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
            dataGridView1.Columns[11].Width = 50;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Vreme paletiranja";
            dataGridView1.Columns[12].Width = 100;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Kontro. ček";
            dataGridView1.Columns[13].Width = 50;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Kontrolor";
            dataGridView1.Columns[14].Width = 50;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Vreme kontrole";
            dataGridView1.Columns[15].Width = 100;

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
            //Panta

            dataGridView1.Refresh();
            RefreshNeKontrolisano();
            RefreshReklamacije();

           // dataGridView2.EditMode = DataGridViewEditMode.EditOnEnter;

        }

        private void RefreshNeKontrolisano()
        {
            string upit = " SELECT RkShippingItemPak.ShippingItemPakID ,Paleta, RkShipping.BrojIstovara as BrojUtovara, RkShipping.BrojUtovara as BrojIsporuke , MestoIsporuke,  RkShippingItemPak.PaketName, RkShippingItemPak.LargoPakID as SifLargo , RkShippingItemPak.LargoNaziv, KolicinaOrg, Kolicina,   KOntrolise as Kontrolor , RkShippingItemPak.Razlog FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "' and Kontrolisao = 0";

            var select2 = upit;
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds2.Tables[0];
           // ID, Paleta, Broj utovara , Broj isporuke, Mesto isporuke
            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "Paket ID";
            dataGridView2.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Paleta";
            dataGridView2.Columns[1].Width = 40;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Broj utovara";
            dataGridView2.Columns[2].Width = 45;

            DataGridViewColumn column4 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "Broj isporuke";
            dataGridView2.Columns[3].Width = 45;


            DataGridViewColumn column5 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "Mesto isporuke";
            dataGridView2.Columns[4].Width = 190;

            DataGridViewColumn column6 = dataGridView2.Columns[5];
            dataGridView2.Columns[5].HeaderText = "Paket name Webs";
            dataGridView2.Columns[5].Width = 190;



            DataGridViewColumn column7 = dataGridView2.Columns[6];
            dataGridView2.Columns[6].HeaderText = "Šif. Largo";
            dataGridView2.Columns[6].Width = 50;


            DataGridViewColumn column8 = dataGridView2.Columns[7];
            dataGridView2.Columns[7].HeaderText = "Paket name Largo";
            dataGridView2.Columns[7].Width = 190;

            DataGridViewColumn column9 = dataGridView2.Columns[8];
            dataGridView2.Columns[8].HeaderText = "Količ. org";
            dataGridView2.Columns[8].Width = 40;


            DataGridViewColumn column10 = dataGridView2.Columns[9];
            dataGridView2.Columns[9].HeaderText = "Količina";
            dataGridView2.Columns[9].Width = 45;


            DataGridViewColumn column11 = dataGridView2.Columns[10];
            dataGridView2.Columns[10].HeaderText = "Kontrolor";
            dataGridView2.Columns[10].Width = 50;

            DataGridViewColumn column12 = dataGridView2.Columns[11];
            dataGridView2.Columns[11].HeaderText = "Nije isporučeno - RAZLOG";
            dataGridView2.Columns[11].Width = 350;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
              
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.SelectionBackColor = Color.Red;
              

            }
            // string value2 = dataGridView2.Rows[0].Cells[0].Value.ToString();


            dataGridView2.Refresh();
        
        }

        private void RefreshReklamacije()
        {
            var select = "SELECT RkShippingItemRec.ShippingItemRecID ,Paleta, RkShipping.BrojIstovara as BrojUtovara, RkShipping.BrojUtovara as BrojIsporuke , MestoIsporuke,  RkShippingItemRec.Zapisnik AS Zapisnik, RkShippingItemRec.SifraReklamacije as SifLargo , RkShippingItemREC.NazivReklamacije,  RkShippingItemREC.Opis,KolicinaOrg,  Kolicina,  Paletirano,Paletar,VremePakovanja, Kontrolor as Kontrolor, RkShippingItemRec.Razlog, IsporucenoKupcu FROM [dbo].RkShippingItemRec inner join RkShipping on [dbo].RkShippingItemRec.ShipingIDz = RkShipping.[ShippingID] inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";
            
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];
            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();

            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "Reklamacija ID";
            dataGridView3.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "Paleta";
            dataGridView3.Columns[1].Width = 40;

            DataGridViewColumn column3 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "Broj utovara";
            dataGridView3.Columns[2].Width = 45;

            DataGridViewColumn column4 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "Broj isporuke";
            dataGridView3.Columns[3].Width = 45;


            DataGridViewColumn column5 = dataGridView3.Columns[4];
            dataGridView3.Columns[4].HeaderText = "Mesto isporuke";
            dataGridView3.Columns[4].Width = 190;

            DataGridViewColumn column6 = dataGridView3.Columns[5];
            dataGridView3.Columns[5].HeaderText = "Zapisnik";
            dataGridView3.Columns[5].Width = 100;



            DataGridViewColumn column7 = dataGridView3.Columns[6];
            dataGridView3.Columns[6].HeaderText = "Šifra reklamacije";
            dataGridView3.Columns[6].Width = 195;


            DataGridViewColumn column8 = dataGridView3.Columns[7];
            dataGridView3.Columns[7].HeaderText = "Naziv Reklamacije";
            dataGridView3.Columns[7].Width = 90;


            DataGridViewColumn column9 = dataGridView3.Columns[8];
            dataGridView3.Columns[8].HeaderText = "Opis";
            dataGridView3.Columns[8].Width = 80;


            DataGridViewColumn column10 = dataGridView3.Columns[9];
            dataGridView3.Columns[9].HeaderText = "Količ. org";
            dataGridView3.Columns[9].Width = 40;


            DataGridViewColumn column11 = dataGridView3.Columns[10];
            dataGridView3.Columns[10].HeaderText = "Količina";
            dataGridView3.Columns[10].Width = 45;


            DataGridViewColumn column12 = dataGridView3.Columns[11];
            dataGridView3.Columns[11].HeaderText = "Kontrolor";
            dataGridView3.Columns[11].Width = 50;

            DataGridViewColumn column13 = dataGridView3.Columns[12];
            dataGridView3.Columns[12].HeaderText = "Nije isporučeno - RAZLOG";
            dataGridView3.Columns[12].Width = 300;

            DataGridViewColumn column14 = dataGridView3.Columns[13];
            dataGridView3.Columns[13].HeaderText = "Isporučeno kupcu";
            dataGridView3.Columns[13].Width = 50;

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                int IsporucenoKupcu = Convert.ToInt32(row.Cells[16].Value);
                //int KolicinaOrg = Convert.ToInt32(row.Cells[9].Value.ToString());
                if ((IsporucenoKupcu == 1))
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.SelectionBackColor = Color.Green;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.SelectionBackColor = Color.Red;
                }


            }
            dataGridView3.Refresh();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            int brojredova = 0;
            int brojredovareklamacije = 0;
            brojredova = dataGridView2.RowCount;
            brojredovareklamacije = dataGridView3.RowCount;
            int postojired = 0; //proverava da li je unet razlog
            int imapravo = 0;

            if (brojredova > 0)
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Cells[1].Value.ToString() == "")
                    {
                        postojired = 1;
                        MessageBox.Show("Postoji red za koji nije unet razlog");
                        return;
                    }
                }
            }
            /*
            if (brojredovareklamacije > 0)
            {
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.Cells[1].Value.ToString() == "")
                    {
                        postojired = 1;
                        MessageBox.Show("Postoji red za koji nije unet razlog reklmacije");
                        return;
                    }
                }
            }
             * */
           if (postojired == 0)
                { 
                ///
                    var select = " Select Distinct DatumUtovara From RkShipping where Stanje = 1 and Vozilo = '" + cboVozila.Text + "'";
                    var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    SqlCommand command = new SqlCommand("SELECT Korisnik FROM Korisnici where Password = '" + txtSpecijalnaDozvola.Text + "' and Zakljucavanje = 1", myConnection);
                    myConnection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        imapravo = 1;
                    }
                    else {
                        MessageBox.Show("Vaša dozvola nema prava završetka isporuke unesite ispravnu lozinku");
                        return;
                    }
                    reader.Close();
                    myConnection.Close();
                }
        
                
            
            if (imapravo == 1)
            {
                Zatvori();
                if (chkRezimZavrsetka.Checked == true)
                {
                    PrebaciUZavrseno();
                }
            }   
           // if (brojredova == 0)
           // { 
           // Zatvori();
           // }
            MessageBox.Show("Zatvoreno isporuka");
        }
        private void PrebaciUZavrseno()
        { 
        //
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Distinct ShippingID from RKShipping " +
            "  where RTRIM(RkShipping.Vozilo)  = '" + cboVozila.Text + "' and RTRim(RkShipping.DatumUtovara) = '" + cboDatumUtovara.Text + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();

            InsertArhiva insArh = new InsertArhiva();
            
            while (dr.Read())
            {

                 insArh.PrebaciIzRKShipingZavrseno(Convert.ToInt32(dr["ShippingID"].ToString()));
                 insArh.PrebaciIzRKShipingItemPakZavrseno(Convert.ToInt32(dr["ShippingID"].ToString()));
                 insArh.PrebaciIzRkShippingItemProZavreno(Convert.ToInt32(dr["ShippingID"].ToString()));
                 insArh.PrebaciIzRKShipingItemRecZavrseno(Convert.ToInt32(dr["ShippingID"].ToString()));

            }

            con.Close();


        //
        }
private void Zatvori()
{
string cs = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand();
                string sqlc = "Update [dbo].RkShipping set Stanje = 2 where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";
                cmd.CommandText = sqlc;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
              

                string connString2 = ConfigurationManager.AppSettings["webs"];
                using (OleDbConnection conn = new OleDbConnection(connString2))
                {
                    conn.Open();
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row != null && row.Cells[0].Value != null)
                        {
                            OleDbCommand cmd2 = new OleDbCommand("update RkShippingItemPak set IsporucenoKupcu = true where ShippingItemPakID  = " + row.Cells[4].Value + "", conn);
                           // 
                            cmd2.ExecuteNonQuery();
                            //
                        }
                    }
                    conn.Close();
                   
                }

                string connString3 = ConfigurationManager.AppSettings["webs"];
                using (OleDbConnection conn3 = new OleDbConnection(connString3))
                {
                    conn3.Open();
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row != null && row.Cells[0].Value != null)
                        {
                            if (row.Cells[12].Value.ToString() == "")
                            {
                                MessageBox.Show("Paket nema vreme paletiranja");
                            }
                            else
                            {
                                OleDbCommand cmd2 = new OleDbCommand("update RkShippingItemPak set Pakovano = CDate('" + row.Cells[12].Value.ToString() + "') where ShippingItemPakID  = " + row.Cells[4].Value + "", conn3);
                                cmd2.ExecuteNonQuery();
                            }
                            // 

                            //
                        }
                    }
                    conn3.Close();

                    MessageBox.Show("Ažurirano je vreme paletiranja");

                }

}



        private void button4_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand();
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected)
                {
                    string sqlc = "Update [dbo].RkShippingItemPak set Razlog =  '" + txtRazlog.Text + "' where RkShippingItemPak.ShippingItemPakID = " + row.Cells[0].Value.ToString();
                    cmd.CommandText = sqlc;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
           con.Close();
           RefreshNeKontrolisano();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {

                    if (row.Selected)
                    {
                        var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

                        SqlConnection connection = new SqlConnection(s_connection);
                        SqlCommand command = new SqlCommand("SELECT * FROM Korisnici", connection);
                        command.Connection.Open();

                        string querystr = "UPDATE RkShippingItemRec SET IsporucenoKupcu='" + 1 + "' WHERE ShippingItemRecId ='" + row.Cells[0].Value.ToString() + "'";
                        SqlCommand query = new SqlCommand(querystr, connection);
                        query.ExecuteNonQuery();

                        /*
                        string User_ID = Session["ID"].ToString();
                        string User_FirstName = FirstNameEdit.Text;
                        string User_LastName = LastNameEdit.Text;

                        query.Parameters.Add("@User_ID", User_ID);
                        query.Parameters.Add("@User_FirstName", User_FirstName);
                        query.Parameters.Add("@User_LastName", User_LastName);
                        
                        */

                        command.Connection.Close();

                    }
                }
                RefreshReklamacije();
                //RefreshDataGrid2();

            }
            catch
            {
                MessageBox.Show("Nije uspela promena stavki");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand();
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected == true)
                {
                    string sqlc = "Update [dbo].RkShippingItemRec set Razlog =  '" + txtRazlog.Text + "' where RkShippingItemRec.ShippingItemRecID = " + row.Cells[0].Value.ToString();
                    cmd.CommandText = sqlc;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            con.Close();
            RefreshReklamacije();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
           
        }

        private void tabControl1_TabStopChanged(object sender, EventArgs e)
        {
            RefreshReklamacije();
            RefreshNeKontrolisano();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string connString2 = ConfigurationManager.AppSettings["webs"];
            using (OleDbConnection conn = new OleDbConnection(connString2))
            {
                conn.Open();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row != null && row.Cells[0].Value != null)
                    {
                        if (row.Cells[12].Value.ToString() == "")
                        {
                            MessageBox.Show("Paket nema vreme paletiranja");
                        }
                        else
                        { 
                        OleDbCommand cmd2 = new OleDbCommand("update RkShippingItemPak set Pakovano = CDate('" + row.Cells[12].Value.ToString() + "') where ShippingItemPakID  = " + row.Cells[4].Value + "", conn);
                        cmd2.ExecuteNonQuery();
                        }
                            // 
                      
                        //
                    }
                }
                conn.Close();

            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            if ((txtSpecijalnaDozvola.Text != "03079777") || (txtSpecijalnaDozvola.Text != "03079123"))
            {
                return;
            }

            string cs = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand();
            string sqlc = "Update [dbo].RkShipping set Stanje = 1 where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";
            cmd.CommandText = sqlc;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if ((txtSpecijalnaDozvola.Text != "03079777") || (txtSpecijalnaDozvola.Text != "03079123"))
            {
            return;
            }
            try
            {
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {

                    if (row.Selected)
                    {
                        var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

                        SqlConnection connection = new SqlConnection(s_connection);
                        SqlCommand command = new SqlCommand("SELECT * FROM Korisnici", connection);
                        command.Connection.Open();

                        string querystr = "UPDATE RkShippingItemRec SET IsporucenoKupcu='" + 0 + "' WHERE ShippingItemRecId ='" + row.Cells[0].Value.ToString() + "'";
                        SqlCommand query = new SqlCommand(querystr, connection);
                        query.ExecuteNonQuery();

                        /*
                        string User_ID = Session["ID"].ToString();
                        string User_FirstName = FirstNameEdit.Text;
                        string User_LastName = LastNameEdit.Text;

                        query.Parameters.Add("@User_ID", User_ID);
                        query.Parameters.Add("@User_FirstName", User_FirstName);
                        query.Parameters.Add("@User_LastName", User_LastName);
                        
                        */

                        command.Connection.Close();

                    }
                }
                RefreshReklamacije();
                //RefreshDataGrid2();

            }
            catch
            {
                MessageBox.Show("Nije uspela promena stavki");
            }
        }
    }
}
