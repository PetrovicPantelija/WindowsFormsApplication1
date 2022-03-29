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
using System.Data.OleDb;

namespace WindowsFormsApplication1
{
    public partial class ObradaIsporuke : Form
    {
        public static string code = "ObradaIsporuke";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public ObradaIsporuke()
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
        private void RefreshDataGrid()
        {
        
            
            //Panta2
            //Obrnuto Datum i broj Utovara
            var select = " SELECT RkShippingItemPak.ShippingItemPakId as ID, RkShipping.ShippingNo as BarkodUtovara, RkShipping.BrojIstovara as BrojUtovara, RkShipping.DatumIstovara as DatumUtovara, RkShipping.BrojUtovara as BrojIstovara,  RkShipping.DatumUtovara as DatumIstovara , Saloni.MestoIsporuke, RkShippingItemPak.PaketName, RkShippingItemPak.LargoPakId, RkShippingItemPak.LargoNaziv, RkShippingItemPak.Paleta, RkShippingItemPak.Tezina,  RkShippingItemPak.LargoDimenzija  FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] inner join SysKomitenti on RkShipping.KupacIDz = SysKomitenti.KomintentID inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";

            

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
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Barkod utovara";
            dataGridView1.Columns[1].Width = 140;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Br. utovara";
            dataGridView1.Columns[2].Width = 45;


            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Datum utovara";
            dataGridView1.Columns[3].Width = 90;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Br. isporuke";
            dataGridView1.Columns[4].Width = 45;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Datum isporuke";
            dataGridView1.Columns[5].Width = 90;



            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Mesto isporuke";
            dataGridView1.Columns[6].Width = 170;


            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Paket name";
            dataGridView1.Columns[7].Width = 200;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Šif. Largo";
            dataGridView1.Columns[8].Width = 50;


            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Largo naziv";
            dataGridView1.Columns[9].Width = 195;


            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Paleta";
            dataGridView1.Columns[10].Width = 50;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Težina(KG)";
            dataGridView1.Columns[11].Width = 60;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Largo Dimenzija";
            dataGridView1.Columns[12].Width = 120;

            dataGridView1.Refresh();        
        }

        private void RefreshReklamacijeUtovar()
        {
            //panta3
            //Obrnuto Datum i broj Utovara
            // var select = " SELECT RkShippingItemPak.ShippingItemPakId as ID, RkShipping.ShippingNo as BarkodUtovara, RkShipping.BrojIstovara as BrojUtovara, RkShipping.DatumIstovara as DatumUtovara, RkShipping.BrojUtovara as BrojIstovara, RkShipping.DatumUtovara as DatumIstovara ,RkShipping.KupacIdz, SysKomitenti.NazivKomintenta, RkShipping.SalonIdz, Saloni.MestoIsporuke, RkShippingItemPak.PaketName, RkShippingItemPak.LargoPakId, RkShippingItemPak.LargoNaziv, RkShippingItemPak.Paleta, RkShippingItemPak.Tezina, RkShippingItemPak.LargoDimenzija FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] inner join SysKomitenti on RkShipping.KupacIDz = SysKomitenti.KomintentID inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";
            // var select = "SELECT RkShippingItemRec.*, RkShipping.ShippingNo as BarkodIstovara, RkShipping.BrojIstovara as BrojUtovara,RkShipping.BrojUtovara as BrojIstovara FROM RkShippingItemRec inner join RkShipping on  RkShipping.ShippingID = RkShippingItemRec.ShipingIdz where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";
            var select = " Select RkShippingItemRec.ShippingItemRecID, RkShippingItemRec.ShipingIdz,  RkShipping.ShippingNo as BarkodIstovara, RkShipping.BrojIstovara as BrojUtovara,RkShipping.BrojUtovara as BrojIstovara, Saloni.MestoIsporuke, RkShippingItemRec.SifraReklamacije, RkShippingItemRec.NazivReklamacije, RkShippingItemRec.KolicinaOrg, RkShippingItemRec.Zapisnik, RkShippingItemRec.Opis, RkShippingItemRec.Paleta, RkShippingItemRec.Tezina, RkShippingItemRec.LargoDimenzija  FROM RkShippingItemRec inner join RkShipping on  RkShipping.ShippingID = RkShippingItemRec.ShipingIdz  inner join SysKomitenti on RkShipping.KupacIDz = SysKomitenti.KomintentID inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'" + "  and RkShipping.BrojIstovara = '" + cboUtovar.Text + "'";
            // var select = "SELECT RkShippingItemPak.ShippingItemPakId as ID, RkShipping.ShippingNo as BarkodUtovara, RkShipping.BrojIstovara as BrojUtovara, RkShipping.DatumIstovara as DatumUtovara, RkShipping.BrojUtovara as BrojIstovara,  RkShipping.DatumUtovara as DatumIstovara , Saloni.MestoIsporuke, RkShippingItemPak.PaketName, RkShippingItemPak.LargoPakId, RkShippingItemPak.LargoNaziv, RkShippingItemPak.Paleta, RkShippingItemPak.Tezina,  RkShippingItemPak.LargoDimenzija  FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] inner join SysKomitenti on RkShipping.KupacIDz = SysKomitenti.KomintentID inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";
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
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "Isporuka ID";
            dataGridView3.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "Barkod";
            dataGridView3.Columns[2].Width = 140;


            DataGridViewColumn column4 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "Br. utovara";
            dataGridView3.Columns[3].Width = 45;

            DataGridViewColumn column5 = dataGridView3.Columns[4];
            dataGridView3.Columns[4].HeaderText = "Br. istovara";
            dataGridView3.Columns[4].Width = 45;

            DataGridViewColumn column6 = dataGridView3.Columns[5];
            dataGridView3.Columns[5].HeaderText = "Mesto isporuke";
            dataGridView3.Columns[5].Width = 170;



            DataGridViewColumn column7 = dataGridView3.Columns[6];
            dataGridView3.Columns[6].HeaderText = "Šifra reklamacije";
            dataGridView3.Columns[6].Width = 200;


            DataGridViewColumn column8 = dataGridView3.Columns[7];
            dataGridView3.Columns[7].HeaderText = "Naziv reklamacije";
            dataGridView3.Columns[7].Width = 140;

            DataGridViewColumn column9 = dataGridView3.Columns[8];
            dataGridView3.Columns[8].HeaderText = "Količina org.";
            dataGridView3.Columns[8].Width = 40;


            DataGridViewColumn column10 = dataGridView3.Columns[9];
            dataGridView3.Columns[9].HeaderText = "Zapisnik";
            dataGridView3.Columns[9].Width = 70;


            DataGridViewColumn column11 = dataGridView3.Columns[10];
            dataGridView3.Columns[10].HeaderText = "Opis";
            dataGridView3.Columns[10].Width = 100;

            DataGridViewColumn column12 = dataGridView3.Columns[11];
            dataGridView3.Columns[11].HeaderText = "Paleta";
            dataGridView3.Columns[11].Width = 40;

            DataGridViewColumn column13 = dataGridView3.Columns[12];
            dataGridView3.Columns[12].HeaderText = "Težina(KG)";
            dataGridView3.Columns[12].Width = 50;

            DataGridViewColumn column14 = dataGridView3.Columns[13];
            dataGridView3.Columns[13].HeaderText = "Largo Dimenzija";
            dataGridView3.Columns[13].Width = 100;


            dataGridView3.Refresh();
        }


        private void RefreshDataGrid3()
        {
            //panta3
            //Obrnuto Datum i broj Utovara
            // var select = " SELECT RkShippingItemPak.ShippingItemPakId as ID, RkShipping.ShippingNo as BarkodUtovara, RkShipping.BrojIstovara as BrojUtovara, RkShipping.DatumIstovara as DatumUtovara, RkShipping.BrojUtovara as BrojIstovara, RkShipping.DatumUtovara as DatumIstovara ,RkShipping.KupacIdz, SysKomitenti.NazivKomintenta, RkShipping.SalonIdz, Saloni.MestoIsporuke, RkShippingItemPak.PaketName, RkShippingItemPak.LargoPakId, RkShippingItemPak.LargoNaziv, RkShippingItemPak.Paleta, RkShippingItemPak.Tezina, RkShippingItemPak.LargoDimenzija FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] inner join SysKomitenti on RkShipping.KupacIDz = SysKomitenti.KomintentID inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";
           // var select = "SELECT RkShippingItemRec.*, RkShipping.ShippingNo as BarkodIstovara, RkShipping.BrojIstovara as BrojUtovara,RkShipping.BrojUtovara as BrojIstovara FROM RkShippingItemRec inner join RkShipping on  RkShipping.ShippingID = RkShippingItemRec.ShipingIdz where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";
            var select = " Select RkShippingItemRec.ShippingItemRecID, RkShippingItemRec.ShipingIdz,  RkShipping.ShippingNo as BarkodIstovara, RkShipping.BrojIstovara as BrojUtovara,RkShipping.BrojUtovara as BrojIstovara, Saloni.MestoIsporuke, RkShippingItemRec.SifraReklamacije, RkShippingItemRec.NazivReklamacije, RkShippingItemRec.KolicinaOrg, RkShippingItemRec.Zapisnik, RkShippingItemRec.Opis, RkShippingItemRec.Paleta, RkShippingItemRec.Tezina, RkShippingItemRec.LargoDimenzija  FROM RkShippingItemRec inner join RkShipping on  RkShipping.ShippingID = RkShippingItemRec.ShipingIdz  inner join SysKomitenti on RkShipping.KupacIDz = SysKomitenti.KomintentID inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";
           // var select = "SELECT RkShippingItemPak.ShippingItemPakId as ID, RkShipping.ShippingNo as BarkodUtovara, RkShipping.BrojIstovara as BrojUtovara, RkShipping.DatumIstovara as DatumUtovara, RkShipping.BrojUtovara as BrojIstovara,  RkShipping.DatumUtovara as DatumIstovara , Saloni.MestoIsporuke, RkShippingItemPak.PaketName, RkShippingItemPak.LargoPakId, RkShippingItemPak.LargoNaziv, RkShippingItemPak.Paleta, RkShippingItemPak.Tezina,  RkShippingItemPak.LargoDimenzija  FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] inner join SysKomitenti on RkShipping.KupacIDz = SysKomitenti.KomintentID inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";
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
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "Isporuka ID";
            dataGridView3.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "Barkod";
            dataGridView3.Columns[2].Width = 140;


            DataGridViewColumn column4 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "Br. utovara";
            dataGridView3.Columns[3].Width = 45;

            DataGridViewColumn column5 = dataGridView3.Columns[4];
            dataGridView3.Columns[4].HeaderText = "Br. istovara";
            dataGridView3.Columns[4].Width = 45;

            DataGridViewColumn column6 = dataGridView3.Columns[5];
            dataGridView3.Columns[5].HeaderText = "Mesto isporuke";
            dataGridView3.Columns[5].Width = 170;



            DataGridViewColumn column7 = dataGridView3.Columns[6];
            dataGridView3.Columns[6].HeaderText = "Šifra reklamacije";
            dataGridView3.Columns[6].Width = 200;


            DataGridViewColumn column8 = dataGridView3.Columns[7];
            dataGridView3.Columns[7].HeaderText = "Naziv reklamacije";
            dataGridView3.Columns[7].Width = 140;

            DataGridViewColumn column9 = dataGridView3.Columns[8];
            dataGridView3.Columns[8].HeaderText = "Količina org.";
            dataGridView3.Columns[8].Width = 40;


            DataGridViewColumn column10 = dataGridView3.Columns[9];
            dataGridView3.Columns[9].HeaderText = "Zapisnik";
            dataGridView3.Columns[9].Width = 70;


            DataGridViewColumn column11 = dataGridView3.Columns[10];
            dataGridView3.Columns[10].HeaderText = "Opis";
            dataGridView3.Columns[10].Width = 100;

            DataGridViewColumn column12 = dataGridView3.Columns[11];
            dataGridView3.Columns[11].HeaderText = "Paleta";
            dataGridView3.Columns[11].Width = 40;

            DataGridViewColumn column13 = dataGridView3.Columns[12];
            dataGridView3.Columns[12].HeaderText = "Težina(KG)";
            dataGridView3.Columns[12].Width = 50;

            DataGridViewColumn column14 = dataGridView3.Columns[13];
            dataGridView3.Columns[13].HeaderText = "Largo Dimenzija";
            dataGridView3.Columns[13].Width = 100;


            dataGridView3.Refresh();
        }

        private void RefreshDataGrid2()
        {
           // var select = " SELECT RkShippingItemPak.Paleta , Sum(RkShippingItemPak.Tezina) as Tezina_Palete FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] inner join SysKomitenti on RkShipping.KupacIDz = SysKomitenti.KomintentID inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "' group by RkShippingItemPak.Paleta";
           // var select = " SELECT RkShippingItemPak.Paleta , Sum(RkShippingItemPak.Tezina) as Tezina_Palete, Sum(RkShippingItemRec.Tezina) as TezinaPoReklamaciji, (Sum(RkShippingItemPak.Tezina) + Sum(RkShippingItemRec.Tezina)) as UkupnaTezina FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] inner join RkShippingItemRec on RkShipping.ShippingID = RkShippingItemRec.ShipingIDz where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "' group by RkShippingItemPak.Paleta";
           var select = " Select t1.Paleta, Sum(t1.Tezina_Palete) as Tezina_Palete, Sum(t1.TezinaPoReklamaciji) as TezinaPoReklamaciji, Sum(T1.Tezina_Palete + t1.TezinaPoReklamaciji) as UkupnaTezina from "
 + " (SELECT RkShippingItemPak.Paleta , Sum(RkShippingItemPak.Tezina) as Tezina_Palete, 0 as TezinaPoReklamaciji FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "' group by RkShippingItemPak.Paleta"
+ " union "
+ " SELECT RkShippingItemRec.Paleta , 0 as Tezina_Palete, Sum(RkShippingItemRec.Tezina) as TezinaPoReklamaciji FROM [dbo].RkShippingItemRec inner join RkShipping on [dbo].RkShippingItemRec.ShipingIDz = RkShipping.[ShippingID]  where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "' group by RkShippingItemRec.Paleta"
+" ) t1 group by t1.Paleta";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];
           // string value = dataGridView2.Rows[0].Cells[0].Value.ToString();


            dataGridView2.Refresh();
        }

        private void REfreshUkupnaKOlicina()
        {

            txtCeoNalog.Text = "0";

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //VRATI PANTA
                txtCeoNalog.Text = (Convert.ToDecimal(txtCeoNalog.Text) + Convert.ToDecimal(row.Cells[11].Value.ToString())).ToString();
            }
        }

        private void REfreshUkupnaKOlicinaUtovara()
        {
           
            txtPakovanja.Text = "0";

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //Panta VrATI
                txtPakovanja.Text = (Convert.ToDecimal(txtPakovanja.Text) + Convert.ToDecimal(row.Cells[11].Value.ToString())).ToString();
            }
        }

        private void btnPozovi_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
            REfreshUkupnaKOlicina();
            RefreshDataGrid2();
            RefreshDataGrid3();
        }

        private void ObradaIsporuke_Load(object sender, EventArgs e)
        {
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


            var select2 = " Select Distinct PrezimeIme From TrVozaci";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboVozaci.DataSource = ds2.Tables[0];
            cboVozaci.DisplayMember = "PrezimeIme";
            cboVozaci.ValueMember = "PrezimeIme";
           

            
            var select3 = " Select Oznaka From OznakaPalete";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboPaleta.DataSource = ds3.Tables[0];
            cboPaleta.DisplayMember = "Oznaka";
            cboPaleta.ValueMember = "Oznaka";
          

        }

        private void cboVozila_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Select Distinct DatumUtovara From RkShipping where Stanje = 1 and Vozilo = '" + Vozilo +"'"
           // select distinct datumutovara, (Substring(datumutovara,7,4) + substring(datumutovara, 4,2) + substring(datumutovara, 1,2)) as Broj from RkShipping order by Broj asc
           
        }

        private void cboDatumUtovara_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Tezina.Text = "0";

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (row.Selected)
                {
                    //Panta1
                    Tezina.Text = (Convert.ToDecimal(Tezina.Text) + Convert.ToDecimal(row.Cells[11].Value.ToString())).ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    if (row.Selected)
                    {
                        var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

                        SqlConnection connection = new SqlConnection(s_connection);
                        SqlCommand command = new SqlCommand("SELECT * FROM Korisnici", connection);
                        command.Connection.Open();

                        string querystr = "UPDATE RkShippingItemPak SET Paleta='" + cboPaleta.Text + "' WHERE ShippingItemPakId ='" +  row.Cells[0].Value.ToString() + "'";
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
                RefreshDataGrid();
                RefreshDataGrid2();
                
            }
            catch
            {
                MessageBox.Show("Nije uspela promena stavki");
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var select = " Select Distinct BrojIstovara From RkShipping where Stanje = 1 and Vozilo = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboUtovar.DataSource = ds.Tables[0];
            cboUtovar.DisplayMember = "BrojIstovara";
            cboUtovar.ValueMember = "BrojIstovara";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Panta
            //RkShippingItemPak.ShippingItemPakId as ID, RkShipping.ShippingNo as BarkodUtovara, RkShipping.BrojIstovara as BrojUtovara, RkShipping.DatumIstovara as DatumUtovara, RkShipping.BrojUtovara as BrojIstovara,  RkShipping.DatumUtovara as DatumIstovara , Saloni.MestoIsporuke, RkShippingItemPak.PaketName, RkShippingItemPak.LargoPakId, RkShippingItemPak.LargoNaziv, RkShippingItemPak.Paleta, RkShippingItemPak.Tezina,  RkShippingItemPak.LargoDimenzija  FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] inner join SysKomitenti on RkShipping.KupacIDz = SysKomitenti.KomintentID inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "' and RkShipping.BrojIstovara = '" + cboUtovar.Text + "'";
           //       SELECT RkShippingItemPak.ShippingItemPakId as ID, RkShipping.ShippingNo as BarkodUtovara, RkShipping.BrojIstovara as BrojUtovara, RkShipping.DatumIstovara as DatumUtovara, RkShipping.BrojUtovara as BrojIstovara,  RkShipping.DatumUtovara as DatumIstovara , Saloni.MestoIsporuke, RkShippingItemPak.PaketName, RkShippingItemPak.LargoPakId, RkShippingItemPak.LargoNaziv, RkShippingItemPak.Paleta, RkShippingItemPak.Tezina,  RkShippingItemPak.LargoDimenzija  FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] inner join SysKomitenti on RkShipping.KupacIDz = SysKomitenti.KomintentID inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";
            var select = " Select RkShippingItemPak.ShippingItemPakId as ID, RkShipping.ShippingNo as BarkodUtovara, RkShipping.BrojIstovara as BrojUtovara, RkShipping.DatumIstovara as DatumUtovara, RkShipping.BrojUtovara as BrojIstovara,  RkShipping.DatumUtovara as DatumIstovara , Saloni.MestoIsporuke, RkShippingItemPak.PaketName, RkShippingItemPak.LargoPakId, RkShippingItemPak.LargoNaziv, RkShippingItemPak.Paleta, RkShippingItemPak.Tezina,  RkShippingItemPak.LargoDimenzija  FROM [dbo].RkShippingItemPak inner join RkShipping on [dbo].RkShippingItemPak.ShipingIDz = RkShipping.[ShippingID] inner join SysKomitenti on RkShipping.KupacIDz = SysKomitenti.KomintentID inner join Saloni on RkShipping.SalonIDz = Saloni.SifraKomintentaMestoIsporuke where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "' and RkShipping.BrojIstovara = '" + cboUtovar.Text + "'";

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
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Barkod utovara";
            dataGridView1.Columns[1].Width = 140;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Br. utovara";
            dataGridView1.Columns[2].Width = 45;


            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Datum utovara";
            dataGridView1.Columns[3].Width = 90;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Br. isporuke";
            dataGridView1.Columns[4].Width = 45;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Datum isporuke";
            dataGridView1.Columns[5].Width = 90;



            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Mesto isporuke";
            dataGridView1.Columns[6].Width = 170;


            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Paket name";
            dataGridView1.Columns[7].Width = 200;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Šif. Largo";
            dataGridView1.Columns[8].Width = 50;


            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Largo naziv";
            dataGridView1.Columns[9].Width = 195;


            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Paleta";
            dataGridView1.Columns[10].Width = 50;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Težina(KG)";
            dataGridView1.Columns[11].Width = 60;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Largo Dimenzija";
            dataGridView1.Columns[12].Width = 120;

            dataGridView1.Refresh();
            REfreshUkupnaKOlicinaUtovara();
            RefreshReklamacijeUtovar();
        }

        private void txtCeoNalog_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           //ZAMENA
            //Select Distinct Vozilo From RkShipping where Stanje = 1
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

        private void button4_Click(object sender, EventArgs e)
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

                        string querystr = "UPDATE RkShippingItemRec SET Paleta='" + cboPaleta.Text + "' WHERE ShippingItemRecId ='" + row.Cells[0].Value.ToString() + "'";
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
                RefreshDataGrid3();
                RefreshDataGrid2();

            }
            catch
            {
                MessageBox.Show("Nije uspela promena stavki");
            }

        }

        private void button5_Click(object sender, EventArgs e)
{
                string connString2 = ConfigurationManager.AppSettings["webs"];
                using (OleDbConnection conn = new OleDbConnection(connString2))
                {
                    conn.Open();
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row != null && row.Cells[0].Value != null)
                        {
                            OleDbCommand cmd2 = new OleDbCommand("update RkShippingItemPak set BrojPalete =  CStr('" +  row.Cells[10].Value.ToString() + "')  where ShippingItemPakID  = " + row.Cells[0].Value + "", conn);
                           // 
                            cmd2.ExecuteNonQuery();
                            //
                        }
                    }
                    conn.Close();
                   
                }
            button5.BackColor = Color.OrangeRed;
            MessageBox.Show("Posao završen");

        }

        private void button6_Click(object sender, EventArgs e)
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

                        string querystr = "Delete from RkShippingItemRec" + " WHERE ShippingItemRecId ='" + row.Cells[0].Value.ToString() + "'";
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
                RefreshDataGrid3();
                RefreshDataGrid2();

            }
            catch
            {
                MessageBox.Show("Nije uspela promena stavki");
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand();
            string sqlc = "Update [dbo].RkShipping set Vozac = '" + cboVozaci.Text + "' where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";
            cmd.CommandText = sqlc;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Obrada izvršena vozač postavljen");
            con.Close();
        }
    }
}
