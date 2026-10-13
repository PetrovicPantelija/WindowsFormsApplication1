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
    public partial class Pregled2018 : Form
    {
        private int pregled = 1; 
        public static string code = "Pregled2018";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public Pregled2018()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
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
        private void refresh4()
        {
            var select = " SELECT Paleta, RkShippingArhiv.BrojIstovara as BrojUtovara, RkShippingArhiv.BrojUtovara as BrojIsporuke , MestoIsporuke, RkShippingArhivItemRec.ShippingItemRecID , dbo.RkShippingArhivItemRec.Zapisnik AS Zapisnik, RkShippingArhivItemRec.SifraReklamacije as SifLargo , RkShippingArhivItemREC.NazivReklamacije,  RkShippingArhivItemREC.Opis,KolicinaOrg,  Kolicina,   0 as Paletirano, Paletar as Paletar, VremePakovanja as VremePaletiranja, RkShippingArhivItemRec.IsporucenoKupcu as Kontrolisano, Kontrolor as KOntrolor , dbo.RkShippingArhivItemRec.VremeKontrole FROM [dbo].RkShippingArhivItemRec inner join RkShippingArhiv on [dbo].RkShippingArhivItemRec.ShipingIDz = RkShippingArhiv.[ShippingID] inner join Saloni on RkShippingArhiv.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShippingArhiv.Vozilo  = '" + cboVozila.Text + "' and RkShippingArhiv.DatumUtovara = '" + cboDatumUtovara.Text + "' and [dbo].RkShippingArhivItemRec.Paleta = '" + cboPaleta.Text + "'";

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

        private void refresh1()
        {
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
            dataGridView1.Columns[16].HeaderText = "Razlog";
            dataGridView1.Columns[16].Width = 350;

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

        private void refresh2()
        {
            pregled = 2;//Pregled po paleti
            var select = " SELECT Paleta, PaketName, Kolicina, LargoPakID, Kontrolisao, KOntrolise, VremeKontrole, Pakerista, Pakuje, VremePakovanja FROM [dbo].RkShippingItemPakArhiv inner join RkShippingArhiv on [dbo].RkShippingItemPakArhiv.ShipingIDz = RkShippingArhiv.[ShippingID] where RkShippingArhiv.Vozilo  = '" + cboVozila.Text + "' and RkShippingArhiv.DatumUtovara = '" + cboDatumUtovara.Text + "' and [dbo].RkShippingItemPakArhiv.Paleta = '" + cboPaleta.Text + "'";
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

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string Pakerista = row.Cells[7].Value.ToString();
                string Kontrolisao = row.Cells[4].Value.ToString();
                if (Kontrolisao == "1")
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.SelectionBackColor = Color.Green;
                }
                else if ((Pakerista == "0") && (Kontrolisao == "1"))
                {
                    row.DefaultCellStyle.BackColor = Color.GreenYellow;
                    row.DefaultCellStyle.SelectionBackColor = Color.GreenYellow;
                }
                else if ((Pakerista == "1") && (Kontrolisao == "0"))
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                    row.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.SelectionBackColor = Color.Red;
                }
            }
            dataGridView1.Refresh();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            var select = " Select Distinct RTrim(Vozilo) as Vozilo From RkShippingArhiv where DatumUtovara = '" + cboDatumUtovara.Text + "'";
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
            var select = " Select Distinct RkShippingItemPakArhiv.Paleta From RkShippingArhiv inner Join RkShippingItemPakArhiv on RkShippingArhiv.ShippingId = RkShippingItemPakArhiv.ShipingIdz where RkShippingArhiv.Stanje = 1 and RkShippingArhiv.Vozilo = '" + cboVozila.Text + "' and RkShippingArhiv.DatumUtovara = '" + cboDatumUtovara.Text + "'";
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

        private void refresh3()
        {
            var select = " SELECT Paleta, RkShippingArhiv.BrojIstovara as BrojUtovara, RkShippingArhiv.BrojUtovara as BrojIsporuke , MestoIsporuke, RkShippingItemRecArhiv.ShippingItemRecID , RkShippingItemRecArhiv.Zapisnik AS Zapisnik, RkShippingItemRecArhiv.SifraReklamacije as SifLargo , RkShippingItemRecArhiv.NazivReklamacije,  RkShippingItemRecArhiv.Opis,KolicinaOrg,  Kolicina,   0 as Paletirano, Paletar as Paletar, VremePakovanja as VremePaletiranja, RkShippingItemRecArhiv.IsporucenoKupcu as Kontrolisano, Kontrolor as KOntrolor ,  RkShippingItemRecArhiv.VremeKontrole, Razlog FROM [dbo].RkShippingItemRecArhiv inner join RkShippingArhiv on [dbo].RkShippingItemRecArhiv.ShipingIDz = RkShippingArhiv.[ShippingID] inner join Saloni on RkShippingArhiv.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShippingArhiv.Vozilo  = '" + cboVozila.Text + "' and RkShippingArhiv.DatumUtovara = '" + cboDatumUtovara.Text + "'";
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
            dataGridView2.Columns[17].HeaderText = "Razlog";
            dataGridView2.Columns[17].Width = 450;


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
            var select = " SELECT Paleta, RkShippingArhiv.BrojIstovara as BrojUtovara, RkShippingArhiv.BrojUtovara as BrojIsporuke , MestoIsporuke, RkShippingItemPakArhiv.ShippingItemPakID , RkShippingItemPakArhiv.PaketName, RkShippingItemPakArhiv.LargoPakID as SifLargo , RkShippingItemPakArhiv.LargoNaziv, Kolicina, KolicinaOrg,  Pakerista as Paletirano, Pakuje as Paletar, VremePakovanja as VremePaletiranja,Kontrolisao as Kontrolisano, KOntrolise as Kontrolor, VremeKontrole FROM [dbo].RkShippingItemPakArhiv inner join RkShippingArhiv on [dbo].RkShippingItemPakArhiv.ShipingIDz = RkShippingArhiv.[ShippingID] inner join Saloni on RkShippingArhiv.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShippingArhiv.Vozilo  = '" + cboVozila.Text + "' and RkShippingArhiv.DatumUtovara = '" + cboDatumUtovara.Text + "' and [dbo].RkShippingItemPakArhiv.Paleta = '" + cboPaleta.Text + "'";
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
            //refresh4();
        }

        private void Pregled2018_Load(object sender, EventArgs e)
        {
            var select = " Select Distinct DatumUtovara, Cast(SUBSTRING(DatumUtovara, 1, Charindex('.',DatumUtovara) -1) as int)  as  Dan, Cast(Replace(SUBSTRING(DatumUtovara, Charindex('.',DatumUtovara)+1, Len(DatumUtovara)-12 -1 -( (Charindex('.',DatumUtovara) ))),'.','') as int) as  Mesec  From RkShippingArhiv  order by  Mesec, Dan";
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
    }
}
