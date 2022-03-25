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
    public partial class frmPrikazStanja : Form
    {
        public static string code = "frmPrikaziStanja";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        public frmPrikazStanja()
        {
            InitializeComponent();
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
        /*
        private void RefreshDataGrid()
        {
            var select2 = "  select  PrSifMp['Šif.Mp.'], MpStaraSif['Kod'],  MpNaziv['Ime MP'],  " +
   "  sum(PrPrevKol - PrIzdKol)['Stv.kol'],  " +
   "   (select sum(ReKolDvig) from Rezervacije  " +
   "   where ReSifMp = PrSifMP and  " +
   "   ReSifSklad = PrSifSklad and ReLokacija = PrLokacija and ReBrisati = 0)['Rezervacije'],  " +
   "    ( select sum(DNpKolZaIzdelavo - DNpIzdelanaKol)     from DelNalogProizvodni  " +
   "      where DNpSifMp = PrSifMP AND DNpSifStat IN ('OD','LA','PL','PF')) ['Proizvodnja'],  " +
    "     (SELECT SUM(NNaPKolNar - NNaPKolDob) FROM NNAL,NNALP   " +
    "      WHERE NNaPStNar = NNaStNal AND NNAPOznaka = 'I' AND NNaPSifra = PrSifMP   " +
    "      AND NNaStatus = 'PO') ['Nabavka'],   " +
     "     (Select Sum(Kolicina) as Kolicina2  " +
    "      from RkShipping, RkShippingItemPak   WHERE RkShipping.ShippingId =  " +
   "       RkShippingItemPak.ShipingIdz and  LargoPakID= PrSifMP   " +
    "       AND Cast(SUBSTRING(RkShipping.DatumIstovara,7,4) + '-' +  " +
    "       SUBSTRING(RkShipping.DatumIstovara,4,2) + '-' + SUBSTRING(RkShipping.DatumIstovara,1,2)  " +
    "       as DateTime) =   DATEADD(day, -2, CONVERT(VARCHAR(10),'" + dtpPredvidjeno.Text + "' ,101))   ) " +
    "        ['Rezervacija - 2'],    " +
     "       (Select Sum(Kolicina) as Kolicina2 from RkShipping, RkShippingItemPak   " +
     "       WHERE RkShipping.ShippingId = RkShippingItemPak.ShipingIdz and  LargoPakID= PrSifMP   " +
     "        AND Cast(SUBSTRING(RkShipping.DatumIstovara,7,4) + '-' +  " +
     "        SUBSTRING(RkShipping.DatumIstovara,4,2) + '-' +  " +
      "       SUBSTRING(RkShipping.DatumIstovara,1,2) as DateTime) =    " +
      "       DATEADD(day, -1, CONVERT(VARCHAR(10),'" + dtpPredvidjeno.Text + "' ,101))   ) ['Rezervacija - 1'],  " +
         "       (Select Sum(Kolicina) as Kolicina2 from RkShipping, RkShippingItemPak   " +
     "       WHERE RkShipping.ShippingId = RkShippingItemPak.ShipingIdz and  LargoPakID= PrSifMP   " +
     "        AND Cast(SUBSTRING(RkShipping.DatumIstovara,7,4) + '-' +  " +
     "        SUBSTRING(RkShipping.DatumIstovara,4,2) + '-' +  " +
      "       SUBSTRING(RkShipping.DatumIstovara,1,2) as DateTime) =    " +
      "       DATEADD(day, 0, CONVERT(VARCHAR(10),'" + dtpPredvidjeno.Text + "' ,101))   ) ['Rezervacija - 0'],  " +
      "       (SELECT SUM(DobavnicaPostav.DoPKolOdpr) FROM Dobavnica,DobavnicaPostav    WHERE DoStDob = DoPStDob  AND DoPSifra = PrSifMP    AND DoStatus = 'PO'  " +
      "        and Dobavnica.DoDatDob =   DATEADD(day, -2, CONVERT(VARCHAR(10),'" + dtpPredvidjeno.Text + "' ,101))) ['Otpremnica - 2'],   (SELECT SUM(DobavnicaPostav.DoPKolOdpr) FROM Dobavnica,DobavnicaPostav    WHERE DoStDob = DoPStDob  AND DoPSifra = PrSifMP    AND DoStatus = 'PO' and Dobavnica.DoDatDob =   DATEADD(day, -1, CONVERT(VARCHAR(10),'" + dtpPredvidjeno.Text + "' ,101))) ['Otpremnica - 1'],    (SELECT SUM(DobavnicaPostav.DoPKolOdpr) FROM Dobavnica,DobavnicaPostav    WHERE DoStDob = DoPStDob  AND DoPSifra = PrSifMP    AND DoStatus = 'PO' and Dobavnica.DoDatDob =  CONVERT(VARCHAR(10),'" + dtpPredvidjeno.Text + "' ,101)) ['Otpremnica'],    (SELECT SUM(NarociloPostav.NaPKolNar) FROM Narocilo,NarociloPostav    WHERE NaPStNar = NaStNar  AND NaPSifra = PrSifMP    AND NaStatus = 'PO'  and Narocilo.NaDatNar = CONVERT(VARCHAR(10),'" + dtpPredvidjeno.Text + "' ,101)) ['Neisporuceni']    from Promet    join MaticniPodatki on MPSifra = PrSifMP    " +
      "          where Promet.PrDatTrans <= + '" + dtpPredvidjeno.Text + "'  and PrSifMP in  " +
      "          ( " + txtMaticniPodatak.Text +
      "          )   " +
       "     group by  PrSifMp, PrSifSklad, PrLokacija, MpStaraSif,  MpNaziv  ";


            /*
             "        (select distinct LargoPakId from RkShippingItemPak   " +
      "        inner join RkShipping on RkShipping.ShippingID = RkShippingItemPak.ShipingIDz   " +
      "         where Cast(SUBSTRING(RkShipping.DatumIstovara,7,4) + '-' +  " +
      "         SUBSTRING(RkShipping.DatumIstovara,4,2) + '-' +  " +
      "         SUBSTRING(RkShipping.DatumIstovara,1,2) as DateTime) >   " +
       "         DATEADD(day, -2, CONVERT(VARCHAR(10),'" + dtpPredvidjeno.Text + "'  ,101)) ))   " +
             */


        /*
            textBox1.Text = select2;

            /*
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds2.Tables[0];
           } */



        private void RefreshDataGrid()
        {

            var select2 = " select t1.['Šif.Mp.'], t1.['Kod'], t1.['Ime MP'],  t1.['Stv.kol'], t1.['Proizvodnja'], " +
" (CASE WHEN t1.['Rezervacija2'] is null THEN 0 ELSE t1.['Rezervacija2'] END) as Rezervacija2 ," +
" (t1.['Stv.kol'] - ['Rezervacija2']) as Minus1,  " +
" (CASE WHEN t1.['Rezervacija1'] is null THEN 0 ELSE t1.['Rezervacija1'] END ) as Rezervacija1," +
" (t1.['Stv.kol'] - t1.['Rezervacija2']- t1.['Rezervacija1']) as Minus2, " +
" (CASE WHEN t1.['Rezervacija0'] is null THEN 0 ELSE t1.['Rezervacija0'] END) as Rezervacija0 ," +
" (t1.['Stv.kol']- t1.['Rezervacija2'] - t1.['Rezervacija1'] - t1.['Rezervacija0']) as Minus0, " +
" t1.['Neisporuceni'] ," +
" (t1.['Stv.kol'] - t1.['Rezervacija2'] - t1.['Rezervacija1'] - t1.['Rezervacija0'] - t1.['Neisporuceni']) as MinusNeisporuceni " +
" from ((select  PrSifMp['Šif.Mp.'], MpStaraSif['Kod'],  MpNaziv['Ime MP']," +
"  sum(PrPrevKol - PrIzdKol)['Stv.kol'], " +
" (select (CASE WHEN sum(DNpKolZaIzdelavo - DNpIzdelanaKol) is null THEN 0 ELSE sum(DNpKolZaIzdelavo - DNpIzdelanaKol) END) from DelNalogProizvodni   where DNpSifMp = PrSifMP AND DNpSifStat IN ('LA')) ['Proizvodnja'], " +
" (Select (CASE WHEN sum(Kolicina) is null THEN 0 ELSE sum(Kolicina) END)  from RkShipping, RkShippingItemPak   WHERE RkShipping.ShippingId =         RkShippingItemPak.ShipingIdz and  LargoPakID= PrSifMP          AND Cast(SUBSTRING(RkShipping.DatumIstovara,7,4) + '-' +         SUBSTRING(RkShipping.DatumIstovara,4,2) + '-' + SUBSTRING(RkShipping.DatumIstovara,1,2)         as DateTime) =   DATEADD(day, -2, CONVERT(VARCHAR(10),+ '" + dtpPredvidjeno.Text + "'  ,101))   ) ['Rezervacija2'],  " +
" (Select (CASE WHEN sum(Kolicina) is null THEN 0 ELSE sum(Kolicina) END) from RkShipping, RkShippingItemPak WHERE RkShipping.ShippingId = RkShippingItemPak.ShipingIdz and  LargoPakID= PrSifMP           AND Cast(SUBSTRING(RkShipping.DatumIstovara,7,4) + '-' +          SUBSTRING(RkShipping.DatumIstovara,4,2) + '-' +         SUBSTRING(RkShipping.DatumIstovara,1,2) as DateTime) =           DATEADD(day, -1, CONVERT(VARCHAR(10),+ '" + dtpPredvidjeno.Text + "'  ,101))) ['Rezervacija1'], " +
" (Select (CASE WHEN sum(Kolicina) is null THEN 0 ELSE sum(Kolicina) END)from RkShipping, RkShippingItemPak WHERE RkShipping.ShippingId = RkShippingItemPak.ShipingIdz and  LargoPakID= PrSifMP           AND Cast(SUBSTRING(RkShipping.DatumIstovara,7,4) + '-' +          SUBSTRING(RkShipping.DatumIstovara,4,2) + '-' +         SUBSTRING(RkShipping.DatumIstovara,1,2) as DateTime) =           DATEADD(day, 0, CONVERT(VARCHAR(10),+ '" + dtpPredvidjeno.Text + "'  ,101))) ['Rezervacija0'],  " +
" (SELECT (CASE WHEN sum(NarociloPostav.NaPKolNar) is null THEN 0 ELSE sum(NarociloPostav.NaPKolNar) END) FROM Narocilo,NarociloPostav WHERE NaPStNar = NaStNar  AND NaPSifra = PrSifMP    AND NaStatus = 'PO'  and Narocilo.NaDatNar = CONVERT(VARCHAR(10),+ '" + dtpPredvidjeno.Text + "'  ,101)) ['Neisporuceni'] " +
" from Promet    join MaticniPodatki on MPSifra = PrSifMP where Promet.PrDatTrans <= + '" + dtpPredvidjeno.Text + "'   and PrSifMP in ( " + txtMaticniPodatak.Text + " ) " +
" group by  PrSifMp, PrSifSklad, PrLokacija, MpStaraSif,  MpNaziv  )) t1 ";


            /*
             "        (select distinct LargoPakId from RkShippingItemPak   " +
      "        inner join RkShipping on RkShipping.ShippingID = RkShippingItemPak.ShipingIDz   " +
      "         where Cast(SUBSTRING(RkShipping.DatumIstovara,7,4) + '-' +  " +
      "         SUBSTRING(RkShipping.DatumIstovara,4,2) + '-' +  " +
      "         SUBSTRING(RkShipping.DatumIstovara,1,2) as DateTime) >   " +
       "         DATEADD(day, -2, CONVERT(VARCHAR(10),'" + dtpPredvidjeno.Text + "'  ,101)) ))   " +
             */



            textBox1.Text = select2;

           
             var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
             SqlConnection myConnection2 = new SqlConnection(s_connection2);
             var c2 = new SqlConnection(s_connection2);
             var dataAdapter2 = new SqlDataAdapter(select2, c2);

             var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
             var ds2 = new DataSet();
             dataAdapter2.Fill(ds2);
             dataGridView3.ReadOnly = true;
             dataGridView3.DataSource = ds2.Tables[0];
            
        }

        private void RefreshDataGrid2()
        {
            var select2 = "SELECT [LargoPakID],[KolicinaOrg], RkShippingItemPak.ShippingItemPakID, [PaketName], RkShippingItemPak.ProizvodGTIN  FROM [dbo].[RkShippingItemPak] inner join RkShipping on RkShipping.ShippingID=RkShippingItemPak.ShipingIdz where  RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";

            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds2.Tables[0];
            // [ShipingIDz],Paleta, [PaketName],[Kolicina],[LargoPakID],[Pakerista],[Kontrolisao], Tezina
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "LargoID";
            dataGridView1.Columns[0].Width = 80;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Kolicina";
            dataGridView1.Columns[1].Width = 80;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "ID";
            dataGridView1.Columns[2].Width = 80;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Ime paketa";
            dataGridView1.Columns[3].Width = 250;


            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Proizvod GTIN";
            dataGridView1.Columns[4].Width = 150;

        }

        private void frmPrikazStanja_Load(object sender, EventArgs e)
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtMaticniPodatak.Text = "";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    txtMaticniPodatak.Text = txtMaticniPodatak.Text + "," + row.Cells[0].Value.ToString();
                }
            }
        }
    }
}