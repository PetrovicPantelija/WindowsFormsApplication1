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

using Microsoft.Reporting.WinForms;

namespace WindowsFormsApplication1
{
    public partial class frmPrebacivanjeMeđPrenosaRN : Form
    {
        bool usao = false;
        string Korisnik = "";
        int RNPrenet = 0;

        public static string code = "frmPrebacivanjeMedjPrenosaRN";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string niz = "";
        string Kor = frmLogovanje.user.ToString().TrimEnd();

        string MPSaRN = "";
        int DefoultSkladisteU = 0;
        int DefoultLokacijaU = 0;
        string SkladistaMP = "30";
        public frmPrebacivanjeMeđPrenosaRN()
        {
            InitializeComponent();
            JelaTema.Primeni(this);

        }
        public frmPrebacivanjeMeđPrenosaRN(string KorisnikU)
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            Korisnik = KorisnikU;
            Korisnik = Kor;
            txtIzdaje.Text = Korisnik;

        }

        public frmPrebacivanjeMeđPrenosaRN(int RN,  int SkladisteU, int LokacijaU,string korisnik)
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            Korisnik = korisnik;
            txtIzdaje.Text = Korisnik;

           // PravoPristupa();
            RNPrenet = RN;
            DefoultSkladisteU = SkladisteU;
            DefoultLokacijaU = LokacijaU;
            if (DefoultSkladisteU == 30)
            {
                SkladistaMP = "30,4,5"; //Skladista CM
            }
        }
        /*public string IdGrupe()
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
        }*/
        private void btnOsvezi_Click(object sender, EventArgs e)
        {
            /*
            Select DNpStZapisa, DNpSifVrDok, DNpStevilka, DNpStPos, DNpSifStat, DNpSifVrsteDN,    
       DNpSifMp, DNpTeh, DNpSifStor, DNpSifPlanerja, DNpKolZaIzdelavo, DNpIzdelanaKol,   
        DNpIzlocenaKol, DNpPlanDatLans, DNpDejDatLans, DNpAvtLans, DNpPlanDatZac, DNpDejDatZac,    
              DNpPlanDatZak, DNpDejDatZak, DNpOznSled, DNpSifSklad, DNpLokacija, DNpStNabNalK,   
                DNpSifStroskMesta, DNpKonto, DNpOpomba, DNpSifVrDokRef, DNpStDokumentaRef,  
                       NameOper, Datum From DelnalogProizvodni 
                       where DnpSifStat = 'LA' */
            var select = "  Select DNpStZapisa, DNpSifVrDok, DNpStevilka,  DNpSifStat, DNpSifVrsteDN, " +    
            " DNpSifMp, DNpTeh,  DNpSifPlanerja, DNpKolZaIzdelavo, DNpIzdelanaKol,  " + 
            "  DNpIzlocenaKol, DNpPlanDatLans, DNpDejDatLans,  DNpPlanDatZac, DNpDejDatZac,  " +  
            "       DNpPlanDatZak, DNpDejDatZak, DNpOznSled, DNpSifSklad, DNpLokacija,  " +  
            "         DNpOpomba, " +
            "              NameOper, Datum From DelnalogProizvodni " +
            "             where DnpSifStat in ('LA', 'KZ') ";
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
            dataGridView1.Columns[0].HeaderText = "Šifra zapisa";
            dataGridView1.Columns[0].Width = 40;
        }

        private void VratiStavkeRN(int BrojRN)
        {
            /*
                 select ReSifMP as KoPodSifMp, MpNaziv +' '+ ISNULL(MpDoNaziv,'') as NazivMP, MpSifEnoteMere1, sum(ReRezKol) / 60 as KoKolMateriala,
                    MpOsnSklad, MpOsnLokacija, max(MpHasTrackNo) as MpHasTrackNo
               from Rezervacije, maticnipodatki
              where ReSifVrDok = 'PDN'
                and ReStDokumenta = 909723
                and ReStPostavke = 0
                and ReSifMP = MpSifra

           group by ReSifMP, MpNaziv +' '+ ISNULL(MpDoNaziv,''), MPSifEnoteMere1, MpOsnSklad, MpOsnLokacija
           order by MpHasTRackNo desc
             */
            double koeficijent = 1;
            koeficijent = Convert.ToDouble(txtKoeficijent.Value);
            double x = 100000.2333;
            string y = string.Empty;
            y = string.Format("{0:###0.##}", koeficijent);

            double ukupnaKolicina = 0;
            string MPRN2 = "";
            string BRRN2 = "";

           
            // MessageBox.Show("Broj MP" + BRRN2 + " BRMP" + MPRN2);
            double kk = koeficijent; // ukupnaKolicina;
            string yy = string.Format("{0:###0.##}", kk);
            yy = yy.Replace(",", ".");
            // MessageBox.Show(yy.ToString());
            // Cast((Cast(" + y + " as numeric(18,2))
            /*
              var select = "   select ReStDokumenta, ReSifMP as KoPodSifMp, MpStaraSif as NazivMP, MpSifEnoteMere1,  " +
             "    MpOsnSklad, MpOsnLokacija, (Cast(('" + yy.ToString() + "') as numeric(18,2)) * Cast(sum(ReRezKol) as numeric(18,2)))   as KoKolMateriala " +
            " from Rezervacije, maticnipodatki " +
            "  where ReStDokumenta = " + BrojRN +
            " and ReStPostavke = 0 " +
            " and ReSifMP = MpSifra " +
       " group by ReStDokumenta, ReSifMP, MpStaraSif, MPSifEnoteMere1, MpOsnSklad, MpOsnLokacija ";
             * convert(numeric(22,4),
              */

            // (Cast('" + yy.ToString() + "' as numeric(18,2))))

            // MessageBox.Show("Kolicina" + yy);

            var select = "";
            select = " select " + RNPrenet + ", KoPodSifMp, MpStaraSif as KoPodSifMp,  MpSifEnoteMere1, (sum(KoKolMateriala) * (Cast('" + yy.ToString() + "' as numeric(18,2))))   as KoKolMateriala, " +
             "  MpOsnSklad, MpOsnLokacija " +
             " from kosovnica, maticnipodatki " +
             "  where KoNadSifMp =  " + MPSaRN +" and KoVarianta = '*' " +" and KoPodSifMp = MpSifra"+" and KoSifStat = 'A' and maticnipodatki.MpOsnSklad in ( " + SkladistaMP + " )" +
      " group by KoPodSifMP,MpStaraSif ,MpNaziv +' '+ ISNULL(MpDoNaziv,''), MPSifEnoteMere1, MpOsnSklad, MpOsnLokacija ";
            ;



            //  MessageBox.Show(select.ToString());
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            //string value = dataGridView2.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "Šifra zapisa";
            dataGridView2.Columns[0].Width = 40;
            //Panta
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                string RowType = row.Cells[5].Value.ToString().TrimEnd();
                if (RowType == "30")
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                    row.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightGray;
                }
            }

        }

        


        private void VratiStavke(int BrojRN)
        { 
       /*
            select ReSifMP as KoPodSifMp, MpNaziv +' '+ ISNULL(MpDoNaziv,'') as NazivMP, MpSifEnoteMere1, sum(ReRezKol) / 60 as KoKolMateriala,
               MpOsnSklad, MpOsnLokacija, max(MpHasTrackNo) as MpHasTrackNo
          from Rezervacije, maticnipodatki
         where ReSifVrDok = 'PDN'
           and ReStDokumenta = 909723
           and ReStPostavke = 0
           and ReSifMP = MpSifra
            
      group by ReSifMP, MpNaziv +' '+ ISNULL(MpDoNaziv,''), MPSifEnoteMere1, MpOsnSklad, MpOsnLokacija
      order by MpHasTRackNo desc
        */
            double koeficijent = 1;
            koeficijent = Convert.ToDouble(txtKoeficijent.Value);
            double x = 100000.2333;
            string y = string.Empty;
            y = string.Format("{0:###0.##}", koeficijent);

            double ukupnaKolicina = 0;
            string MPRN2 = "";
            string BRRN2 = "";
           
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    ukupnaKolicina = Convert.ToDouble(row.Cells[8].Value);
                    MPRN2 = row.Cells[5].Value.ToString();
                    BRRN2 = row.Cells[2].Value.ToString();
                }
            }
            // MessageBox.Show("Broj MP" + BRRN2 + " BRMP" + MPRN2);
            double kk = koeficijent; // ukupnaKolicina;
            string yy = string.Format("{0:###0.##}", kk);
            yy = yy.Replace(",", ".");
           // MessageBox.Show(yy.ToString());
           // Cast((Cast(" + y + " as numeric(18,2))
            /*
              var select = "   select ReStDokumenta, ReSifMP as KoPodSifMp, MpStaraSif as NazivMP, MpSifEnoteMere1,  " +
             "    MpOsnSklad, MpOsnLokacija, (Cast(('" + yy.ToString() + "') as numeric(18,2)) * Cast(sum(ReRezKol) as numeric(18,2)))   as KoKolMateriala " +
            " from Rezervacije, maticnipodatki " +
            "  where ReStDokumenta = " + BrojRN +
            " and ReStPostavke = 0 " +
            " and ReSifMP = MpSifra " +
       " group by ReStDokumenta, ReSifMP, MpStaraSif, MPSifEnoteMere1, MpOsnSklad, MpOsnLokacija ";
             * convert(numeric(22,4),
              */

           // (Cast('" + yy.ToString() + "' as numeric(18,2))))

           // MessageBox.Show("Kolicina" + yy);

            var select = " select " + BRRN2 + ", KoPodSifMp, MpStaraSif as KoPodSifMp,  MpSifEnoteMere1, (sum(KoKolMateriala) * (Cast('" + yy.ToString() + "' as numeric(18,2))))   as KoKolMateriala, " +
             "  MpOsnSklad, MpOsnLokacija " +
             " from kosovnica, maticnipodatki " +
             "  where KoNadSifMp =  " + MPRN2 +
             "   and KoVarianta = '*' " +
             " and KoPodSifMp = MpSifra " +
             " and KoSifStat = 'A' " +
      " group by KoPodSifMP,MpStaraSif ,MpNaziv +' '+ ISNULL(MpDoNaziv,''), MPSifEnoteMere1, MpOsnSklad, MpOsnLokacija ";
      ;



          //  MessageBox.Show(select.ToString());
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            //string value = dataGridView2.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "Šifra zapisa";
            dataGridView2.Columns[0].Width = 40;
            //Panta
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                string RowType = row.Cells[5].Value.ToString();
                if (RowType == "30")
                {
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                    row.DefaultCellStyle.SelectionBackColor = Color.LightGray;
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtKoeficijent.Value > txtKolicinaRN.Value)
            {
                return;
            }
            
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    VratiStavke(Convert.ToInt32(row.Cells[2].Value));
                }
            }
        }

        private void frmPrebacivanjeMeđPrenosaRN_Load(object sender, EventArgs e)
        {
            var select2 = " Select SkSifra, (Cast(SkSifra as nvarchar(3)) + '-' + SkNaziv) as SkNaziv from Sklad order by SkSifra";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboSkladiste.DataSource = ds2.Tables[0];
            cboSkladiste.DisplayMember = "SkNaziv";
            cboSkladiste.ValueMember = "SkSifra";


            var select3 = " Select SkSifra, (Cast(SkSifra as nvarchar(3)) + '-' + SkNaziv) as SkNaziv from Sklad order by SkSifra";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboSkladisteU.DataSource = ds3.Tables[0];
            cboSkladisteU.DisplayMember = "SkNaziv";
            cboSkladisteU.ValueMember = "SkSifra";
            usao = true;

            if (RNPrenet != 0)
            {
                txtBrRn.Text = RNPrenet.ToString();
               // dataGridView1.Rows[0].Selected = true;

                PostaviDGV1();
                VratiVrednostRN(RNPrenet);
                VratiStavkeRN(RNPrenet);
               // VratiKoeficijent(RNPrenet);
                cboSkladisteU.SelectedValue = DefoultSkladisteU;
                NapuniLokacijuU(DefoultSkladisteU);
                cboLokacijaU.SelectedValue = DefoultLokacijaU;
              

               
            }
        }

        private void VratiVrednostRN(int RnPR)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" Select  DNpStevilka, DNpSifStat,DnPSifMp,  DNpSifVrsteDN, " +
                "  DNpKolZaIzdelavo, DNpIzdelanaKol  " +
          "  From DelnalogProizvodni " +
                "  where DNpStevilka =  " + RnPR , con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                txtKoeficijent.Value = Convert.ToDecimal(1);
                txtKolicinaRN.Value = Convert.ToDecimal(dr["DNpKolZaIzdelavo"].ToString());
                txtKoeficijent.Value = Convert.ToDecimal(dr["DNpKolZaIzdelavo"].ToString());
                MPSaRN = dr["DnPSifMp"].ToString();
            }

            con.Close();

      
            // txtBrRn.Text = row.Cells["DNpStevilka"].Value.ToString().TrimEnd();

        }

        private void cboSkladiste_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (usao == true)
            {
                /*
                var select = " select LokZapSt, Rtrim(LokSifra) as LokSifra  from Lokac where LokSkSifra = " + Convert.ToInt32(cboSkladiste.SelectedValue);
                var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
                SqlConnection myConnection = new SqlConnection(s_connection);
                var c = new SqlConnection(s_connection);
                var dataAdapter = new SqlDataAdapter(select, c);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                cboSkladisteU.DataSource = ds.Tables[0];
                cboSkladisteU.DisplayMember = "LokSifra";
                cboSkladisteU.ValueMember = "LokSifra";
                 * */
            }
        }

        public int ProveriKolicinuLeviDesni()
        {
            int potrebanMP = 0;
            double potrebnaKolicina = 0;
            

            foreach (DataGridViewRow row2 in dataGridView2.Rows)
            {
                if (row2.Selected)
                {
                    potrebanMP = Convert.ToInt32(row2.Cells[1].Value);
                    potrebnaKolicina = Convert.ToDouble(row2.Cells[4].Value);
                }
            }
            double Kolicina3 = 0;
            foreach (DataGridViewRow row3 in dataGridView3.Rows)
            {

                if (potrebanMP == Convert.ToInt32(row3.Cells[0].Value))
                {
                    Kolicina3 = Kolicina3 + Convert.ToDouble(row3.Cells[5].Value);
                }
            }
            if (Kolicina3 > potrebnaKolicina)
            {
                MessageBox.Show("Količina je veća od izabrane nije dozvoljeno");
                return 1;

            }


            return 0;
        }

        int ProveriUkupnuKolicinuDesni()
        {
            double KolicinaSelektovanaDesni = 0;
            double KolicinaSelektovanaLevi = 0;
            foreach (DataGridViewRow row2 in dataGridView5.Rows)
            {
                if (row2.Selected)
                {

                    KolicinaSelektovanaDesni = KolicinaSelektovanaDesni + Convert.ToDouble(row2.Cells[2].Value.ToString());
                }
            }

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected)
                {

                    KolicinaSelektovanaLevi = Convert.ToDouble(row.Cells[4].Value.ToString());
                }
            }
            if (KolicinaSelektovanaDesni == 0)
            {
                MessageBox.Show("Niste selektovali stavku");
                return 1;
            }
            if (KolicinaSelektovanaLevi == 0)
            {
                MessageBox.Show("Niste selektovali stavku");
                return 1;
            }
            if (KolicinaSelektovanaDesni > KolicinaSelektovanaLevi)
            { 
                return 1; }
            else
            { return 0; }
        
        }

        private void btnUbaci_Click(object sender, EventArgs e)
        {
           
           // MessageBox.Show(dataGridView2.SelectedRows.Count.ToString());

            if (dataGridView2.SelectedRows.Count > 1)
            {
                MessageBox.Show("Selektovali ste više od jedne stavke !!!");
                return;
            }

            int ProveriKolicinu = 0;
            int ProveriUkupnuKolicinu = 0;
            ProveriKolicinu = ProveriKolicinuLeviDesni();
            ProveriUkupnuKolicinu = ProveriUkupnuKolicinuDesni();

            if (ProveriUkupnuKolicinu == 1)
            {
                MessageBox.Show("Količina je veća nego na sastavnici");
                return;
            
            }

            if (ProveriKolicinu == 1)
            { return; }
            //dataGridView2.

            for (int i = 0; i < dataGridView2.SelectedRows.Count; i++)
            {
                string Kolicina = "0";
              
                string LokacijaIzA = "";
                string SkladisteIzA = "";
              
                foreach (DataGridViewRow row2 in dataGridView5.Rows)
                {
                    if (row2.Selected)
                    {
                        Kolicina = row2.Cells[2].Value.ToString(); ;
                        LokacijaIzA = row2.Cells[1].Value.ToString(); ;
                        SkladisteIzA = row2.Cells[0].Value.ToString(); ;

                        //Panta
                        int index = dataGridView3.Rows.Add();
                        // dataGridView3.Rows[index].Cells[0].Value = cboSkladiste.SelectedValue;
                        dataGridView3.Rows[index].Cells[0].Value =dataGridView2.SelectedRows[i].Cells[1].Value.ToString();     //     SkladisteIzA; OK
                        dataGridView3.Rows[index].Cells[1].Value =dataGridView2.SelectedRows[i].Cells[2].Value.ToString(); // cboSkladisteU.SelectedValue;
                        dataGridView3.Rows[index].Cells[2].Value =dataGridView2.SelectedRows[i].Cells[3].Value.ToString();   // Kolicina;
                        dataGridView3.Rows[index].Cells[3].Value = SkladisteIzA;                  			//	dataGridView2.SelectedRows[i].Cells[1].Value.ToString(); //SifMP
                        dataGridView3.Rows[index].Cells[4].Value = LokacijaIzA;			 				// dataGridView2.SelectedRows[i].Cells[2].Value.ToString(); //Naziv MP
                        dataGridView3.Rows[index].Cells[5].Value = Kolicina;  						//	dataGridView2.SelectedRows[i].Cells[3].Value.ToString(); // JM
                        dataGridView3.Rows[index].Cells[6].Value = dataGridView2.SelectedRows[i].Cells[6].Value.ToString(); //Kolicina
                        dataGridView3.Rows[index].Cells[7].Value = cboSkladisteU.SelectedValue;//dataGridView2.SelectedRows[i].Cells[4].Value.ToString(); //Skladiste U
                        dataGridView3.Rows[index].Cells[8].Value = cboLokacijaU.SelectedValue;//cboLokacijaU.SelectedValue.ToString();//dataGridView2.SelectedRows[i].Cells[5].Value.ToString(); //Lokacija U
                        dataGridView3.Rows[index].Cells[9].Value = txtNapomena.Text;
                        dataGridView3.Rows[index].Cells[10].Value = cboLokacijaU.Text;   //  LokacijaIzA;

                        ProverBrisanjeIzLevogPanela();
                      
                      //END of Panta
                    }
                }

             
             
               // dataGridView3.Rows[index].Cells[10].Value = txtBrojNalepnica.Text; 
            }
        }

        private void ProverBrisanjeIzLevogPanela()
        {
          //1 kolicine
            int potrebanMP = 0;
            double potrebnaKolicina = 0;
            int KolicinaVeca = 0;

            foreach (DataGridViewRow row2 in dataGridView2.Rows)
            {
                if (row2.Selected)
                {
                    potrebanMP = Convert.ToInt32(row2.Cells[1].Value);
                    potrebnaKolicina = Convert.ToDouble(row2.Cells[4].Value);
                }
            }
            double Kolicina3 = 0; 
            foreach (DataGridViewRow row3 in dataGridView3.Rows)
            {

                if (potrebanMP == Convert.ToInt32(row3.Cells[0].Value))
                {
                    Kolicina3 = Kolicina3 + Convert.ToDouble(row3.Cells[5].Value);
                }
            }
            if (Kolicina3 >= potrebnaKolicina)
            {
                foreach (DataGridViewRow selRow in dataGridView2.SelectedRows.OfType<DataGridViewRow>().ToArray())
                {
                    dataGridView2.Rows.Remove(selRow);
                }
            
            }
        }
        int PrtstDok;
        private void button3_Click(object sender, EventArgs e)
        {
            InsertMedjuskladisni isporuka = new InsertMedjuskladisni();
            isporuka.InsertMedju(txtNapomena.Text, txtIzdaje.Text);
            var connectionString = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select MAx(PrTStDok)  from PrometTrans", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                PrtstDok = Convert.ToInt32(dr[0].ToString());
            }
            conn.Close();
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row != null && row.Cells[0].Value != null)
                {
                   
                   // 6 kolicina
                    /*
                    dataGridView3.Rows[index].Cells[0].Value = cboSkladiste.SelectedValue;
                    dataGridView3.Rows[index].Cells[1].Value = cboPozicija.SelectedValue;
                    dataGridView3.Rows[index].Cells[2].Value = dataGridView2.SelectedRows[i].Cells[0].Value.ToString(); // RN
                    dataGridView3.Rows[index].Cells[3].Value = dataGridView2.SelectedRows[i].Cells[1].Value.ToString(); //SifMP
                    dataGridView3.Rows[index].Cells[4].Value = dataGridView2.SelectedRows[i].Cells[2].Value.ToString(); //Naziv MP
                    dataGridView3.Rows[index].Cells[5].Value = dataGridView2.SelectedRows[i].Cells[3].Value.ToString(); // JM
                    dataGridView3.Rows[index].Cells[6].Value = dataGridView2.SelectedRows[i].Cells[4].Value.ToString(); //Kolicina
                    dataGridView3.Rows[index].Cells[7].Value = dataGridView2.SelectedRows[i].Cells[5].Value.ToString(); //Skladiste U
                    dataGridView3.Rows[index].Cells[8].Value = dataGridView2.SelectedRows[i].Cells[6].Value.ToString(); //Lokacija U
                    */ 
                    // Izmenjeno je pozicija ide u Pozicija U
                    /*
                    int P1 = Convert.ToInt32(row.Cells[3].Value.ToString());
                    MessageBox.Show(P1.ToString());
                    double P2 =  Convert.ToDouble(row.Cells[6].Value.ToString());
                    MessageBox.Show(P2.ToString());
                    int P3 =  Convert.ToInt32(row.Cells[0].Value.ToString());
                    MessageBox.Show(P3.ToString());
                    int P4 = Convert.ToInt32(row.Cells[1].Value.ToString());
                    MessageBox.Show(P4.ToString());
                    string p5 = row.Cells[10].Value.ToString();
                    MessageBox.Show(p5.ToString());
                    string p6 = row.Cells[8].Value.ToString();
                    MessageBox.Show(p6.ToString());
                    int p7 = Convert.ToInt32(row.Cells[2].Value.ToString());
                    MessageBox.Show(p7.ToString());
                    string p8 = row.Cells[9].Value.ToString();
                    MessageBox.Show(p8.ToString());
                    */
                    //PANTA VRATIf10
                    //isporuka.InsertMedjuPostavRN(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToDouble(row.Cells[5].Value.ToString()), Convert.ToInt32(30), Convert.ToInt32(30), "101119", "101120", Convert.ToInt32(txtBrRn.Text), row.Cells[9].Value.ToString(), txtIzdaje.Text.Trim());
                    // isporuka.InsertMedjuPostavRN(Convert.ToInt32(row.Cells[3].Value.ToString()), Convert.ToDouble(row.Cells[2].Value.ToString()), Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[10].Value.ToString(), row.Cells[8].Value.ToString(), Convert.ToInt32(txtBrRn.Text), row.Cells[9].Value.ToString());


                   isporuka.InsertMedjuPostavRN(PrtstDok,Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToDouble(row.Cells[5].Value.ToString()), Convert.ToInt32(row.Cells[3].Value.ToString()), Convert.ToInt32(row.Cells[7].Value.ToString()), row.Cells[4].Value.ToString(), row.Cells[8].Value.ToString(), Convert.ToInt32(txtBrRn.Text), row.Cells[9].Value.ToString(), txtIzdaje.Text.Trim());

                }
            }
            VratiMSP();
            MessageBox.Show("Formiran je Međuskladišni prenos");
        }


        private void VratiMSP()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select MAX(PrTStDok) as PrTStDok from PrometTrans where PrTTipPrTrans = 'MS'", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtBrojMSP.Text = dr["PrTStDok"].ToString();
            }
         
            con.Close();
        }

        private void VratiMSPGrid()
        {
            var select = "   Select PrTpSifSklad as SkladisteIz, " +
                    " PrTpLok as LokIz, BrRN, PrTpSifMp as MP,PrTpStaraSifMp as Kod, " +
                    " PrTpEnotaMere as JM, PrTpPrevKol as Kol, " +
                    " PrTpSifSkladpr as SkladisteU, " +
                    " PrTpLokpr as LokU, PrTpOpomba, PrTpStZapisa as Zapis " +
                    " from PrometTransPos " +
                    " where PrTpStDokumenta = " + Convert.ToInt32(txtBrojMSP.Text) + " and PrTpSifVrDok = 'MSP' ";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView4.ReadOnly = true;
            dataGridView4.DataSource = ds.Tables[0];

            //string value = dataGridView2.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView4.Columns[0];
            dataGridView4.Columns[0].HeaderText = "Sifra";
            dataGridView4.Columns[0].Width = 40;
        
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VratiMSPGrid();
           
            
            
            Barcodes.Barcode39 b = new Barcodes.Barcode39();

            Magacin.InsertMSPNalepnica MspNal = new Magacin.InsertMSPNalepnica();
            MspNal.DeleteNalepnice();
            //Skupiti sve parametre

            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                            MspNal.InsMspNalepnica(Convert.ToInt32(txtBrojMSP.Text),
                                Convert.ToInt32(row.Cells[2].Value.ToString())
                                , row.Cells[4].Value.ToString()
                                ,row.Cells[7].Value.ToString() 
                               ,row.Cells[0].Value.ToString() 
                               ,Convert.ToInt32(row.Cells[3].Value.ToString())
                                , txtIzdaje.Text
                                ,Convert.ToDouble(row.Cells[6].Value.ToString())
                               , row.Cells[9].Value.ToString()
                               , b.GenerateBarcodeImage(row.Cells[3].Value.ToString()),
                               b.GenerateBarcodeImage(txtBrojMSP.Text));
            }

            
        }
        

        private void button4_Click(object sender, EventArgs e)
        {
            Perftech_BeogradDataSet2TableAdapters.MSPNalepniceTableAdapter ta = new Perftech_BeogradDataSet2TableAdapters.MSPNalepniceTableAdapter();
            Perftech_BeogradDataSet2.MSPNalepniceDataTable dt = new Perftech_BeogradDataSet2.MSPNalepniceDataTable();
            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptMSPNal.rdlc";

            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
        }

        private void PostaviDGV1()
        {
            var select = "  Select DNpStZapisa as ZapisID, DNpSifVrDok, DNpStevilka,  DNpSifStat, DNpSifVrsteDN, " +
                " DNpSifMp, DNpTeh,  DNpSifPlanerja, DNpKolZaIzdelavo , DNpIzdelanaKol,  " +
                "  DNpIzlocenaKol, DNpPlanDatLans, DNpDejDatLans,  DNpPlanDatZac, DNpDejDatZac,  " +
                "       DNpPlanDatZak, DNpDejDatZak, DNpOznSled, DNpSifSklad, DNpLokacija,  " +
                "         DNpOpomba, " +
                "              NameOper, Datum From DelnalogProizvodni " +
                "             where DNpStevilka =  " + Convert.ToInt32(txtBrRn.Text);
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
            dataGridView1.Columns[0].HeaderText = "Šifra zapisa";
            dataGridView1.Columns[0].Width = 40;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            /*
         Select DNpStZapisa, DNpSifVrDok, DNpStevilka, DNpStPos, DNpSifStat, DNpSifVrsteDN,    
    DNpSifMp, DNpTeh, DNpSifStor, DNpSifPlanerja, DNpKolZaIzdelavo, DNpIzdelanaKol,   
     DNpIzlocenaKol, DNpPlanDatLans, DNpDejDatLans, DNpAvtLans, DNpPlanDatZac, DNpDejDatZac,    
           DNpPlanDatZak, DNpDejDatZak, DNpOznSled, DNpSifSklad, DNpLokacija, DNpStNabNalK,   
             DNpSifStroskMesta, DNpKonto, DNpOpomba, DNpSifVrDokRef, DNpStDokumentaRef,  
                    NameOper, Datum From DelnalogProizvodni 
                    where DnpSifStat = 'LA' */
            var select = "  Select DNpStZapisa, DNpSifVrDok, DNpStevilka,  DNpSifStat, DNpSifVrsteDN, " +
            " DNpSifMp, DNpTeh,  DNpSifPlanerja, DNpKolZaIzdelavo, DNpIzdelanaKol,  " +
            "  DNpIzlocenaKol, DNpPlanDatLans, DNpDejDatLans,  DNpPlanDatZac, DNpDejDatZac,  " +
            "       DNpPlanDatZak, DNpDejDatZak, DNpOznSled, DNpSifSklad, DNpLokacija,  " +
            "         DNpOpomba, " +
            "              NameOper, Datum From DelnalogProizvodni " +
            "             where DNpStevilka =  " + Convert.ToInt32(txtBrRn.Text);
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
            dataGridView1.Columns[0].HeaderText = "Šifra zapisa";
            dataGridView1.Columns[0].Width = 40;
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Magacin.InsertMSPNalepnica MspNal = new Magacin.InsertMSPNalepnica();
            
            try
            {
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Selected)
                    {
                        MessageBox.Show(row.Cells[3].Value.ToString());
                        MspNal.UpdMspNalepnica(Convert.ToInt32(row.Cells[3].Value.ToString()), Convert.ToInt32(txtBN.Text));
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija ili promena");
            }
        }

        private void NapuniGrid5(int SifraMP)
        {
            var select = "   select   PrSifSklad, Promet.PrLokacija,  " +
              " sum(PrPrevKol - PrIzdKol) as Kolicina, " +
              " sum(PrPrevKol - PrIzdKol) as Pocetna , " +
              " LokOpis " +
              " from Promet " +
              " join MaticniPodatki on MPSifra = PrSifMP" +
              " inner join Lokac on Lokac.LokSifra =  Promet.PrLokacija" +
              " group by PrSifSklad, Promet.PrLokacija, PrSifMp, LokOpis  " +
              " having PrSifMp = " + SifraMP + " and sum(PrPrevKol - PrIzdKol)>0 ";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView5.ReadOnly = false;
            dataGridView5.DataSource = ds.Tables[0];

            //string value = dataGridView2.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView5.Columns[0];
            dataGridView5.Columns[0].HeaderText = "Sklad";
            dataGridView5.Columns[0].Width = 40;
            dataGridView5.Columns[0].ReadOnly = true;

            DataGridViewColumn column1 = dataGridView5.Columns[1];
            dataGridView5.Columns[1].HeaderText = "Lokac";
            dataGridView5.Columns[1].Width = 80;
            dataGridView5.Columns[1].Visible = false;


            DataGridViewColumn column2 = dataGridView5.Columns[2];
            dataGridView5.Columns[2].HeaderText = "Kol";
            dataGridView5.Columns[2].Width = 50;

            DataGridViewColumn column3 = dataGridView5.Columns[3];
            dataGridView5.Columns[3].HeaderText = "Poc";
            dataGridView5.Columns[3].Width = 50;
            
            DataGridViewColumn column4 = dataGridView5.Columns[4];
            dataGridView5.Columns[4].HeaderText = "Lokac";
            dataGridView5.Columns[4].Width = 100;

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected == true)
                {
                   //Napuni grid 5
                    NapuniGrid5(Convert.ToInt32(row.Cells[1].Value));
                    //VratiStavke(Convert.ToInt32(row.Cells[2].Value));
                }
            }
        }

        private void cboSkladisteU_Leave(object sender, EventArgs e)
        {
            var select = " select LokZapSt, Rtrim(LokSifra) as LokSifra, Rtrim(LokOpis) as LokOpis  from Lokac where LokSkSifra = " + Convert.ToInt32(cboSkladisteU.SelectedValue);
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboLokacijaU.DataSource = ds.Tables[0];
            cboLokacijaU.DisplayMember = "LokOpis";
            cboLokacijaU.ValueMember = "LokSifra";
        }

        private void NapuniLokacijuU(int Sklad)
        {
            var select = " select LokZapSt, Rtrim(LokSifra) as LokSifra, Rtrim(LokOpis) as LokOpis  from Lokac where LokSkSifra = " + Convert.ToInt32(Sklad);
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboLokacijaU.DataSource = ds.Tables[0];
            cboLokacijaU.DisplayMember = "LokOpis";
            cboLokacijaU.ValueMember = "LokSifra";

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    //Napuni grid 5
                    txtKoeficijent.Value = Convert.ToDecimal(row.Cells[8].Value.ToString());
                    txtKolicinaRN.Value = Convert.ToDecimal(row.Cells[8].Value.ToString());
                    txtBrRn.Text = row.Cells["DNpStevilka"].Value.ToString().TrimEnd();
                    //VratiStavke(Convert.ToInt32(row.Cells[2].Value));
                }
            }
        }

        private void dataGridView5_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            double pomocni = Convert.ToDouble(dataGridView5.Rows[e.RowIndex].Cells[3].Value);
            double nova = Convert.ToDouble(dataGridView5.Rows[e.RowIndex].Cells[2].Value);

            if (nova > pomocni)
            {
                MessageBox.Show("Količina je veća od stanja na zalihama količina će biti vraćena");
                dataGridView5.Rows[e.RowIndex].Cells[2].Value = pomocni;
            }

        }

        private void txtKoeficijent_Leave(object sender, EventArgs e)
        {
            if (txtKoeficijent.Value > txtKolicinaRN.Value)
            {
                MessageBox.Show("Količina za koju se traže potrebe je veća od one na Radnom nalogu");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            VratiMSPGrid();



            Barcodes.Barcode39 b = new Barcodes.Barcode39();

            Magacin.InsertMSPNalepnica MspNal = new Magacin.InsertMSPNalepnica();
            MspNal.DeleteNalepnice();
            //Skupiti sve parametre
            //DRUGI BARKOD JE BROJ RADNOG NALOGA
            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                MspNal.InsMspNalepnica(Convert.ToInt32(txtBrojMSP.Text),
                    Convert.ToInt32(row.Cells[2].Value.ToString())
                    , row.Cells[4].Value.ToString()
                    , row.Cells[7].Value.ToString()
                   , row.Cells[0].Value.ToString()
                   , Convert.ToInt32(row.Cells[3].Value.ToString())
                    , txtIzdaje.Text
                    , Convert.ToDouble(row.Cells[6].Value.ToString())
                   , row.Cells[9].Value.ToString()
                   , b.GenerateBarcodeImage(row.Cells[3].Value.ToString()),
                   b.GenerateBarcodeImage(row.Cells[2].Value.ToString()));
            }
        }
    }
}
