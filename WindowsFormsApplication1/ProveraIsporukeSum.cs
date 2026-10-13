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
    public partial class ProveraIsporukeSum : Form
    {
        public static string code = "ProveraIsporukeSum";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public ProveraIsporukeSum()
        {
            InitializeComponent(); JelaTema.Primeni(this); IdGrupe();
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
        private void ProveraIsporukeSum_Load(object sender, EventArgs e)
        {
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            var select = " SELECT RkShipping.ShippingID as ID,  RkShipping.Vozilo, RkShipping.ShippingNo as BarkodUtovara, RkShipping.BrojUtovara as BrojIstovara, RkShipping.DatumUtovara as DatumIstovara,RkShipping.BrojIstovara as BrojUtovara, RkShipping.DatumIstovara as DatumUtovara, RkShipping.KupacIdz, SysKomitenti.NazivKomintenta, RkShipping.SalonIdz, Saloni.MestoIsporuke FROM  RkShipping  inner join SysKomitenti on RkShipping.KupacIDz = SysKomitenti.KomintentID inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";

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


            dataGridView1.Refresh();


            var select2 = "SELECT RkShipping.BrojIstovara as BrojUtovara, RkShipping.BrojUtovara as BrojIstovara,[ShipingIDz],Paleta, [PaketName],[Kolicina],[LargoPakID],[Pakerista],[Kontrolisao] FROM [dbo].[RkShippingItemPak] inner join RkShipping on RkShipping.ShippingID=RkShippingItemPak.ShipingIdz where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";

            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds2.Tables[0];
         // [ShipingIDz],Paleta, [PaketName],[Kolicina],[LargoPakID],[Pakerista],[Kontrolisao], Tezina
            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "Br. utovara";
            dataGridView3.Columns[0].Width = 30;
            
            DataGridViewColumn column1 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "Br. istovara";
            dataGridView3.Columns[1].Width = 30;

            DataGridViewColumn column2 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "Br. naloga";
            dataGridView3.Columns[2].Width = 50;

            DataGridViewColumn column3 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "Paleta";
            dataGridView3.Columns[3].Width = 50;

            DataGridViewColumn column4 = dataGridView3.Columns[4];
            dataGridView3.Columns[4].HeaderText = "Paket";
            dataGridView3.Columns[4].Width = 180;

            DataGridViewColumn column5 = dataGridView3.Columns[5];
            dataGridView3.Columns[5].HeaderText = "Količina";
            dataGridView3.Columns[5].Width = 80;

            DataGridViewColumn column6 = dataGridView3.Columns[6];
            dataGridView3.Columns[6].HeaderText = "Largo ID";
            dataGridView3.Columns[6].Width = 50;

            DataGridViewColumn column7 = dataGridView3.Columns[7];
            dataGridView3.Columns[7].HeaderText = "Spakovano";
            dataGridView3.Columns[7].Width = 30;

            DataGridViewColumn column8 = dataGridView3.Columns[8];
            dataGridView3.Columns[8].HeaderText = "Kontrolisano";
            dataGridView3.Columns[8].Width = 30;

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                string RowType = row.Cells[7].Value.ToString();
                string RowType2 = row.Cells[8].Value.ToString();
                if (RowType == "0" && RowType2 == "0")
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.SelectionBackColor = Color.Red;
                }
                if (RowType == "1" && RowType2 == "0")
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                    row.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                }
               // string RowType2 = row.Cells[6].Value.ToString();
                if (RowType == "1" && RowType2 == "1")
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.SelectionBackColor = Color.Green;
                }
                if (RowType == "0" && RowType2 == "1")
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.SelectionBackColor = Color.White;
                }

            }

//var select3 = "SELECT [ShipingIDz],Paleta, [PaketName],[Kolicina],[LargoPakID],[Pakerista],[Kontrolisao], Tezina FROM [dbo].[RkShippingItemPak] inner join RkShipping on RkShipping.ShippingID=RkShippingItemPak.ShipingIdz where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";

            var select3 = " SELECT ShipingIDz, SUM(KolicinaKontrolisanih) AS [Kol Kontrol], SUM(KontrolisanihStavki) AS [Kontrol Stavki], SUM(KolicinaNekontrolisanih) AS [Kol NeKontrolisanih], Sum(NeKontrolisanihStavki) AS [NeKontrol Stavki], SUM(KolicinaPakovanih) AS [Kol Pakovanih], "
                  + " SUM(PakovanihStavki) AS [Pak Stavki], SUM(KolicinaNePakovanih) AS [Kol NePakovanih], SUM(NePakovanihStavki) AS [NePak Stavki]"
               + " FROM     (SELECT ShipingIDz, SUM(Kolicina) AS KolicinaKontrolisanih, COUNT(*) AS KontrolisanihStavki, 0 AS KolicinaNekontrolisanih, 0 AS NeKontrolisanihStavki, 0 AS KolicinaPakovanih, 0 AS PakovanihStavki, 0 AS KolicinaNePakovanih, "
               + "                      0 AS NePakovanihStavki"
               + "    FROM      dbo.RkShippingItemPak inner join RkShipping on RkShippingItemPak.ShipingIDz = RkShipping.ShippingID "
               + "    WHERE   (Kontrolisao = 1) and RkShipping.Vozilo = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'"
                + "   GROUP BY ShipingIDz"
               + "    UNION"
                + "   SELECT ShipingIDz, 0 AS KolicinaKontrolisanih, 0 AS KontrolisanihStavki, SUM(Kolicina) AS KolicinaNekontrolisanih, SUM(Kolicina) AS NeKontrolisanihStavki, 0 AS KolicinaPakovanih, 0 AS PakovanihStavki, 0 AS KolicinaNePakovanih,"
                + "                     0 AS NePakovanihStavki"
                  + " FROM     dbo.RkShippingItemPak  inner join RkShipping on RkShippingItemPak.ShipingIDz = RkShipping.ShippingID "
                  + " WHERE  (Kontrolisao = 0) and RkShipping.Vozilo = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'"
                  + " GROUP BY ShipingIDz"
                  + " UNION"
                  + " SELECT ShipingIDz, 0 AS KolicinaKontrolisanih, 0 AS KontrolisanihStavki, 0 AS KolicinaNekontrolisanih, 0 AS NeKontrolisanihStavki, SUM(Kolicina) AS KolicinaPakovanih, COUNT(*) AS PakovanihStavki, 0 AS KolicinaNePakovanih, "
                  + "                   0 AS NePakovanihStavki"
                  + " FROM     dbo.RkShippingItemPak inner join RkShipping on RkShippingItemPak.ShipingIDz = RkShipping.ShippingID"
                  + " WHERE  (Pakerista = 1) and RkShipping.Vozilo = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'"
                  + " GROUP BY ShipingIDz"
                  + " UNION"
                  + " SELECT ShipingIDz, 0 AS KolicinaKontrolisanih, 0 AS KontrolisanihStavki, 0 AS KolicinaNekontrolisanih, 0 AS NeKontrolisanihStavki, 0 AS KolicinaPakovanih, 0 AS PakovanihStavki, SUM(Kolicina) AS KolicinaNePakovanih, COUNT(*) "
                   + "                  AS NePakovanihStavki"
                   + " FROM     dbo.RkShippingItemPak inner join RkShipping on RkShippingItemPak.ShipingIDz = RkShipping.ShippingID"
                   + " WHERE  (Pakerista = 0) and RkShipping.Vozilo = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'"
                   + " GROUP BY ShipingIDz) AS t1 group by t1.ShipingIDz";

            
            
            
            
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds3.Tables[0];
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (row.Selected)
                {
                    //Tezina.Text = (Convert.ToDecimal(Tezina.Text) + Convert.ToDecimal(row.Cells[11].Value.ToString())).ToString();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var select = " Select Distinct RkShippingItemPak.Paleta From RkShipping inner Join RkShippingItemPak on RkShipping.ShippingId = RkShippingItemPak.ShipingIdz where RkShipping.Stanje = 1 and RkShipping.Vozilo = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";
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

        private void button4_Click(object sender, EventArgs e)
        {
            var select = " SELECT RkShipping.ShippingID as ID,  RkShipping.Vozilo, RkShipping.ShippingNo as BarkodUtovara, RkShipping.BrojUtovara as BrojIstovara, RkShipping.DatumUtovara as DatumUtovara, RkShipping.KupacIdz, SysKomitenti.NazivKomintenta, RkShipping.SalonIdz, Saloni.MestoIsporuke FROM  RkShipping  inner join SysKomitenti on RkShipping.KupacIDz = SysKomitenti.KomintentID inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";

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


            dataGridView1.Refresh();


            var select2 = "SELECT RkShipping.BrojIstovara as BrojUtovara, RkShipping.BrojUtovara as BrojIstovara, ShipingIDz, Paleta, [PaketName],[Kolicina],[LargoPakID],[Pakerista],[Kontrolisao] FROM [dbo].[RkShippingItemPak] inner join RkShipping on RkShipping.ShippingID=RkShippingItemPak.ShipingIdz where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "' and RkShippingItemPak.Paleta = '" + cboPaleta.Text + "'";

            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds2.Tables[0];
            // [ShipingIDz],Paleta, [PaketName],[Kolicina],[LargoPakID],[Pakerista],[Kontrolisao], Tezina
            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "Br. utovara";
            dataGridView3.Columns[0].Width = 30;

            DataGridViewColumn column1 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "Br. istovara";
            dataGridView3.Columns[1].Width = 30;

            DataGridViewColumn column2 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "Br. naloga";
            dataGridView3.Columns[2].Width = 50;

            DataGridViewColumn column3 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "Paleta";
            dataGridView3.Columns[3].Width = 50;

            DataGridViewColumn column4 = dataGridView3.Columns[4];
            dataGridView3.Columns[4].HeaderText = "Paket";
            dataGridView3.Columns[4].Width = 180;

            DataGridViewColumn column5 = dataGridView3.Columns[5];
            dataGridView3.Columns[5].HeaderText = "Količina";
            dataGridView3.Columns[5].Width = 80;

            DataGridViewColumn column6 = dataGridView3.Columns[6];
            dataGridView3.Columns[6].HeaderText = "Largo ID";
            dataGridView3.Columns[6].Width = 50;

            DataGridViewColumn column7 = dataGridView3.Columns[7];
            dataGridView3.Columns[7].HeaderText = "Spakovano";
            dataGridView3.Columns[7].Width = 30;

            DataGridViewColumn column8 = dataGridView3.Columns[8];
            dataGridView3.Columns[8].HeaderText = "Kontrolisano";
            dataGridView3.Columns[8].Width = 30;

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                string RowType = row.Cells[7].Value.ToString();
                string RowType2 = row.Cells[8].Value.ToString();
                if (RowType == "0" && RowType2 == "0")
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.SelectionBackColor = Color.Red;
                }
                if (RowType == "1" && RowType2 == "0")
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                    row.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                }
                // string RowType2 = row.Cells[6].Value.ToString();
                if (RowType2 == "1")
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.SelectionBackColor = Color.Green;
                }

            }

            //var select3 = "SELECT [ShipingIDz],Paleta, [PaketName],[Kolicina],[LargoPakID],[Pakerista],[Kontrolisao], Tezina FROM [dbo].[RkShippingItemPak] inner join RkShipping on RkShipping.ShippingID=RkShippingItemPak.ShipingIdz where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";

            var select3 = " SELECT ShipingIDz, SUM(KolicinaKontrolisanih) AS [Kol Kontrol], SUM(KontrolisanihStavki) AS [Kontrol Stavki], SUM(KolicinaNekontrolisanih) AS [Kol NeKontrolisanih], Sum(NeKontrolisanihStavki) AS [NeKontrol Stavki], SUM(KolicinaPakovanih) AS [Kol Pakovanih], "
                  + " SUM(PakovanihStavki) AS [Pak Stavki], SUM(KolicinaNePakovanih) AS [Kol NePakovanih], SUM(NePakovanihStavki) AS [NePak Stavki]"
               + " FROM     (SELECT ShipingIDz, SUM(Kolicina) AS KolicinaKontrolisanih, COUNT(*) AS KontrolisanihStavki, 0 AS KolicinaNekontrolisanih, 0 AS NeKontrolisanihStavki, 0 AS KolicinaPakovanih, 0 AS PakovanihStavki, 0 AS KolicinaNePakovanih, "
               + "                      0 AS NePakovanihStavki"
               + "    FROM      dbo.RkShippingItemPak inner join RkShipping on RkShippingItemPak.ShipingIDz = RkShipping.ShippingID "
               + "    WHERE   (Kontrolisao = 1) and RkShipping.Vozilo = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "' and RkShippingItemPak.Paleta = '" + cboPaleta.Text + "'"
                + "   GROUP BY ShipingIDz"
               + "    UNION"
                + "   SELECT ShipingIDz, 0 AS KolicinaKontrolisanih, 0 AS KontrolisanihStavki, SUM(Kolicina) AS KolicinaNekontrolisanih, SUM(Kolicina) AS NeKontrolisanihStavki, 0 AS KolicinaPakovanih, 0 AS PakovanihStavki, 0 AS KolicinaNePakovanih,"
                + "                     0 AS NePakovanihStavki"
                  + " FROM     dbo.RkShippingItemPak  inner join RkShipping on RkShippingItemPak.ShipingIDz = RkShipping.ShippingID "
                  + " WHERE  (Kontrolisao = 0) and RkShipping.Vozilo = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "' and RkShippingItemPak.Paleta = '" + cboPaleta.Text + "'"
                  + " GROUP BY ShipingIDz"
                  + " UNION"
                  + " SELECT ShipingIDz, 0 AS KolicinaKontrolisanih, 0 AS KontrolisanihStavki, 0 AS KolicinaNekontrolisanih, 0 AS NeKontrolisanihStavki, SUM(Kolicina) AS KolicinaPakovanih, COUNT(*) AS PakovanihStavki, 0 AS KolicinaNePakovanih, "
                  + "                   0 AS NePakovanihStavki"
                  + " FROM     dbo.RkShippingItemPak inner join RkShipping on RkShippingItemPak.ShipingIDz = RkShipping.ShippingID"
                  + " WHERE  (Pakerista = 1) and RkShipping.Vozilo = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "' and RkShippingItemPak.Paleta = '" + cboPaleta.Text + "'"
                  + " GROUP BY ShipingIDz"
                  + " UNION"
                  + " SELECT ShipingIDz, 0 AS KolicinaKontrolisanih, 0 AS KontrolisanihStavki, 0 AS KolicinaNekontrolisanih, 0 AS NeKontrolisanihStavki, 0 AS KolicinaPakovanih, 0 AS PakovanihStavki, SUM(Kolicina) AS KolicinaNePakovanih, COUNT(*) "
                   + "                  AS NePakovanihStavki"
                   + " FROM     dbo.RkShippingItemPak inner join RkShipping on RkShippingItemPak.ShipingIDz = RkShipping.ShippingID"
                   + " WHERE  (Pakerista = 0) and RkShipping.Vozilo = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "' and RkShippingItemPak.Paleta = '" + cboPaleta.Text + "'"
                   + " GROUP BY ShipingIDz) AS t1 group by t1.ShipingIDz";





            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds3.Tables[0];
        }
    }
}
