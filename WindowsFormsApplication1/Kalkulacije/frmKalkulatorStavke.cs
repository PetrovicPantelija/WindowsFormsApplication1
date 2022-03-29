using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Kalkulacije
{
    public partial class frmKalkulatorStavke : Form
    {
        int kalkulator=0;
        string zaglavlje = "";
        int zavrsen;
        bool status = false;
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public static string code = "frmKalkulatorStavke";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";

        public frmKalkulatorStavke()
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
                        tsNew.Enabled = false;
                    }
                    update = Convert.ToBoolean(reader["Izmena"]);
                    if (update == false)
                    {
                        tsSave.Enabled = false;
                    }
                    delete = Convert.ToBoolean(reader["Brisanje"]);
                    if (delete == false)
                    {
                       tsDelete.Enabled = false;
                    }
                }
            }

            conn.Close();
        }
        public frmKalkulatorStavke(int idK,string zag)
        {

            kalkulator = idK;
            zaglavlje = zag;

            InitializeComponent();
            FillCombo();
           
        }
        private void RefreshGV()
        {
            if (kalkulator == 0)
            {
                FillGV();
            }
            else
            {

                FillGvID();
            }
            
        }
        public void FillGvID()
        {
            
            var query = "Select KalkulatorStavke.ID,KalkulatorStavke.WSifra, Program.Naziv as Program, Kolekcije.Naziv as Kolekcija, KalkulatorStavke.MPSifra,MaticniPodatki.MpNaziv,KalkulatorStavke.IDKalkulator," +
                "Kalkulator.Naziv,TrosakProizvodnje,Avans,Provizija,Transport,VpCena,Rabat,Marza,Akcija,MPCena,PrimenaTroskovnogCenovnika.MpSifraVeznog " +
                "From KalkulatorStavke " +
                "Inner join MaticniPodatki on KalkulatorStavke.MPSifra = MaticniPodatki.MpSifra " +
                "Inner join Kalkulator on KalkulatorStavke.IDKalkulator = Kalkulator.ID " +
                "Inner join Program on KalkulatorStavke.ProgramID = Program.ID " +
                "Inner join Kolekcije on KalkulatorStavke.KolekcijaID = Kolekcije.ID " +
                "left join PrimenaTroskovnogCenovnika on KalkulatorStavke.MPSifra = PrimenaTroskovnogCenovnika.MpSifra " +
                "Where IDKalkulator = "+kalkulator;
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void FillGV()
        {
            var query =  "Select KalkulatorStavke.ID,KalkulatorStavke.WSifra, Program.Naziv as Program, Kolekcije.Naziv as Kolekcija, KalkulatorStavke.MPSifra,MaticniPodatki.MpNaziv,KalkulatorStavke.IDKalkulator," +
                "Kalkulator.Naziv,TrosakProizvodnje,Avans,Provizija,Transport,VpCena,Rabat,Marza,Akcija,MPCena,PrimenaTroskovnogCenovnika.MpSifraVeznog " +
                "From KalkulatorStavke " +
                "Inner join MaticniPodatki on KalkulatorStavke.MPSifra = MaticniPodatki.MpSifra " +
                "Inner join Kalkulator on KalkulatorStavke.IDKalkulator = Kalkulator.ID " +
                "Inner join Program on KalkulatorStavke.ProgramID = Program.ID " +
                "Inner join Kolekcije on KalkulatorStavke.KolekcijaID = Kolekcije.ID " +
                "left join PrimenaTroskovnogCenovnika on KalkulatorStavke.MPSifra = PrimenaTroskovnogCenovnika.MpSifra ";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void FillCombo()
        {
            var query = " Select * from Kalkulator";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            combo_Zaglavlje.DataSource = ds.Tables[0];
            combo_Zaglavlje.DisplayMember = "Naziv";
            combo_Zaglavlje.ValueMember = "ID";
        }
        private void txt_mpSifra_Leave(object sender, EventArgs e)
        {
            Trosak();
            Cene();
            
        }
        private void Trosak()
        {
            if (txt_mpSifra.Text == "")
            {
                txt_Trosak.Text = "";
            }
            else
            {
                var query = "Select Distinct KcSifMp as SifMP,  KcDatSpremembe,(KcCena_D + KcCena_P + KcCena_M + KcFStr + KcCena_DD) as Ukupno " +
                "From KalkCenik, MaticniPodatki " +
                "Where KcValid = 1 and KcTipCn = 'G' and KcSifMp=" + Convert.ToInt32(txt_mpSifra.Text);
                SqlConnection conn = new SqlConnection(connect);
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows == false)
                {
                   // MessageBox.Show("Nema proizvoda sa zadatom sifrom");
                    return;
                }
                while (dr.Read())
                {
                    decimal trosak = Convert.ToDecimal(dr["Ukupno"].ToString());
                    txt_Trosak.Text = trosak.ToString();
                }
                conn.Close();
            }
        }
        private void CenaVeznog()
        {
            int mpVeznog=0;
            decimal trosak=0;
            var query = "select MpSifraVeznog from PrimenaTroskovnogCenovnika Where MpSifra= " + Convert.ToInt32(txt_mpSifra.Text);
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == false)
            {
                return;
            }
            while (dr.Read())
            {
                mpVeznog = Convert.ToInt32(dr["MpSifraVeznog"].ToString());
            }
            conn.Close();
            var query2 = "Select Distinct KcSifMp as SifMP,  KcDatSpremembe,(KcCena_D + KcCena_P + KcCena_M + KcFStr + KcCena_DD) as Ukupno " +
               "From KalkCenik, MaticniPodatki " +
               "Where KcValid = 1 and KcTipCn = 'G' and KcSifMp=" + mpVeznog;
            
            conn.Open();
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();

            if (dr2.HasRows == false)
            {
                MessageBox.Show("Nema proizvoda sa zadatom sifrom");
                return;
            }
            while (dr2.Read())
            {
                trosak = Convert.ToDecimal(dr2["Ukupno"].ToString());
            }
            conn.Close();
            decimal VCena, MCena;
            VCena = trosak + Convert.ToDecimal(txt_Marza.Text) + Convert.ToDecimal(txt_Avans.Text) + Convert.ToDecimal(txt_Provizija.Text) +
                Convert.ToDecimal(txt_Transport.Text) - Convert.ToDecimal(txt_Rabat.Text) - Convert.ToDecimal(txt_Akcija.Text);

            MCena = VCena + (VCena * 25 / 100) * Convert.ToDecimal(1.2);

            txt_Mp2.Text = MCena.ToString();
        }
        private void Cene()
        {
            if (txt_Trosak.Text != "" && txt_Avans.Text!="" &&  txt_Provizija.Text != "" && txt_Transport.Text != "" && txt_Rabat.Text != "" && txt_Marza.Text != "" 
                && txt_Akcija.Text != "")
            {
                decimal VCena, MCena;
                VCena = Convert.ToDecimal(txt_Trosak.Text) + Convert.ToDecimal(txt_Marza.Text) + Convert.ToDecimal(txt_Avans.Text) + Convert.ToDecimal(txt_Provizija.Text) +
                    Convert.ToDecimal(txt_Transport.Text) - Convert.ToDecimal(txt_Rabat.Text) - Convert.ToDecimal(txt_Akcija.Text);

                MCena = VCena + (VCena * 25 / 100) * Convert.ToDecimal(1.2);

                txt_VpCena.Text = VCena.ToString();
                txt_MpCena.Text = MCena.ToString();
                CenaVeznog();
            }
            else
            {
                txt_VpCena.Text = "";
                txt_MpCena.Text = "";
                txt_Mp2.Text = "";
            }
        }

        private void txt_Avans_Leave(object sender, EventArgs e)
        {
            Cene();
        }

        private void txt_Provizija_Leave(object sender, EventArgs e)
        {
            Cene();
        }

        private void txt_Transport_Leave(object sender, EventArgs e)
        {
            Cene();
        }

        private void txt_Rabat_Leave(object sender, EventArgs e)
        {
            Cene();
        }

        private void txt_Marza_Leave(object sender, EventArgs e)
        {
            Cene();
        }

        private void txt_Akcija_Leave(object sender, EventArgs e)
        {
            Cene();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach(DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txt_ID.Text = row.Cells[0].Value.ToString();
                        txt_mpSifra.Text = row.Cells[1].Value.ToString();
                        combo_Zaglavlje.Text = row.Cells[4].Value.ToString();
                        txt_Avans.Text = row.Cells[6].Value.ToString();
                        txt_Provizija.Text = row.Cells[7].Value.ToString();
                        txt_Transport.Text = row.Cells[8].Value.ToString();
                        txt_Rabat.Text = row.Cells[10].Value.ToString();
                        txt_Marza.Text = row.Cells[11].Value.ToString();
                        txt_Akcija.Text = row.Cells[12].Value.ToString();
                        Trosak();
                        Cene();
                    }
                }
            }
            catch { }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            tsNew.Checked = true;
            tsNew.Enabled = false;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                Kalkulacije.InsertKalkulatorStavke kalk = new InsertKalkulatorStavke();
                kalk.InsKalkulatorStavke(Convert.ToInt32(txt_mpSifra.Text), Convert.ToInt32(combo_Zaglavlje.SelectedValue), Convert.ToDecimal(txt_Trosak.Text),
                    Convert.ToDecimal(txt_Avans.Text), Convert.ToDecimal(txt_Provizija.Text), Convert.ToDecimal(txt_Transport.Text), Convert.ToDecimal(txt_VpCena.Text),
                    Convert.ToDecimal(txt_Rabat.Text), Convert.ToDecimal(txt_Marza.Text), Convert.ToDecimal(txt_Akcija.Text), Convert.ToDecimal(txt_MpCena.Text));
                status = false;
                tsNew.Checked = false;
                tsNew.Enabled = true;
                RefreshGV();
            }
            else
            {
                Kalkulacije.InsertKalkulatorStavke kalk = new InsertKalkulatorStavke();
                kalk.UpdKalkulatorStavke(Convert.ToInt32(txt_ID.Text), Convert.ToInt32(txt_mpSifra.Text), Convert.ToInt32(combo_Zaglavlje.SelectedValue), Convert.ToDecimal(txt_Trosak.Text),
                    Convert.ToDecimal(txt_Avans.Text), Convert.ToDecimal(txt_Provizija.Text), Convert.ToDecimal(txt_Transport.Text), Convert.ToDecimal(txt_VpCena.Text),
                    Convert.ToDecimal(txt_Rabat.Text), Convert.ToDecimal(txt_Marza.Text), Convert.ToDecimal(txt_Akcija.Text), Convert.ToDecimal(txt_MpCena.Text));
                RefreshGV();
            }
            
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            Kalkulacije.InsertKalkulatorStavke kalk = new InsertKalkulatorStavke();
            kalk.DelKalkulatorStavke(Convert.ToInt32(txt_ID.Text));
            RefreshGV();
        }

        private void btn_Rabat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da za sve stavke želite da primenite rabat od " + txt_Rabat.Text + "% ?","Rabat", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        var query = "Select * from KalkulatorStavke Where IDKalkulator="+Convert.ToInt32(combo_Zaglavlje.SelectedValue);
                        SqlConnection conn = new SqlConnection(connect);
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            int id = Convert.ToInt32(dr["ID"].ToString());
                            int mpSifre = Convert.ToInt32(dr["MPSifra"].ToString());
                            int idKalk = Convert.ToInt32(dr["IDKalkulator"].ToString());
                            decimal trosak = Convert.ToDecimal(dr["TrosakProizvodnje"].ToString());
                            decimal avans = Convert.ToDecimal(dr["Avans"].ToString());
                            decimal provizija = Convert.ToDecimal(dr["Provizija"].ToString());
                            decimal transport = Convert.ToDecimal(dr["Transport"].ToString());
                            decimal marza = Convert.ToDecimal(dr["Marza"].ToString());
                            decimal akcija = Convert.ToDecimal(dr["Akcija"].ToString());

                            decimal VCena, MCena;
                            VCena = trosak + marza + avans + provizija + transport - Convert.ToDecimal(txt_Rabat.Text) - akcija;
                            MCena = VCena + (VCena * 25 / 100) * Convert.ToDecimal(1.2);

                            Kalkulacije.InsertKalkulatorStavke kalk = new InsertKalkulatorStavke();
                            kalk.UpdKalkulatorStavke(id, mpSifre, idKalk, trosak, avans, provizija, transport, VCena, Convert.ToDecimal(txt_Rabat.Text), marza, akcija, MCena);
                        }
                        conn.Close();
                    }
                   
                    RefreshGV();
                }
                catch { }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void btn_Akcija_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da za sve stavke želite da primenite akciju: " + txt_Akcija.Text + "% ?", "Akcija",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        var query = "Select * from KalkulatorStavke Where IDKalkulator=" + Convert.ToInt32(combo_Zaglavlje.SelectedValue);
                        SqlConnection conn = new SqlConnection(connect);
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            int id = Convert.ToInt32(dr["ID"].ToString());
                            int mpSifre = Convert.ToInt32(dr["MPSifra"].ToString());
                            int idKalk = Convert.ToInt32(dr["IDKalkulator"].ToString());
                            decimal trosak = Convert.ToDecimal(dr["TrosakProizvodnje"].ToString());
                            decimal avans = Convert.ToDecimal(dr["Avans"].ToString());
                            decimal provizija = Convert.ToDecimal(dr["Provizija"].ToString());
                            decimal transport = Convert.ToDecimal(dr["Transport"].ToString());
                            decimal marza = Convert.ToDecimal(dr["Marza"].ToString());
                            decimal rabat = Convert.ToDecimal(dr["Rabat"].ToString());

                            decimal VCena, MCena;
                            VCena = trosak + marza + avans + provizija + transport - rabat -Convert.ToDecimal(txt_Akcija.Text);
                            MCena = VCena + (VCena * 25 / 100) * Convert.ToDecimal(1.2);

                            Kalkulacije.InsertKalkulatorStavke kalk = new InsertKalkulatorStavke();
                            kalk.UpdKalkulatorStavke(id, mpSifre, idKalk, trosak, avans, provizija, transport, VCena, rabat, marza, Convert.ToDecimal(txt_Akcija.Text), MCena);
                        }
                        conn.Close();
                    }
                    RefreshGV();
                }
                catch { }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int mpVeznog = 0;
            decimal trosak = 0;
            var query = "select MpSifraVeznog from PrimenaTroskovnogCenovnika Where MpSifra= " + Convert.ToInt32(txt_mpSifra.Text);
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == false)
            {
                return;
            }
            while (dr.Read())
            {
                mpVeznog = Convert.ToInt32(dr["MpSifraVeznog"].ToString());
            }
            conn.Close();
            var query2 = "Select Distinct KcSifMp as SifMP,  KcDatSpremembe,(KcCena_D + KcCena_P + KcCena_M + KcFStr + KcCena_DD) as Ukupno " +
               "From KalkCenik, MaticniPodatki " +
               "Where KcValid = 1 and KcTipCn = 'G' and KcSifMp=" + mpVeznog;

            conn.Open();
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();

            if (dr2.HasRows == false)
            {
                MessageBox.Show("Nema proizvoda sa zadatom sifrom");
                return;
            }
            while (dr2.Read())
            {
                trosak = Convert.ToDecimal(dr2["Ukupno"].ToString());
            }
            conn.Close();
            decimal VCena, MCena;
            VCena = trosak + Convert.ToDecimal(txt_Marza.Text) + Convert.ToDecimal(txt_Avans.Text) + Convert.ToDecimal(txt_Provizija.Text) +
                Convert.ToDecimal(txt_Transport.Text) - Convert.ToDecimal(txt_Rabat.Text) - Convert.ToDecimal(txt_Akcija.Text);

            MCena = VCena + (VCena * 25 / 100) * Convert.ToDecimal(1.2);

            txt_Mp2.Text = MCena.ToString();

            Kalkulacije.InsertKalkulatorStavke kalk = new InsertKalkulatorStavke();
            kalk.InsKalkulatorStavke(mpVeznog, Convert.ToInt32(combo_Zaglavlje.SelectedValue), trosak, Convert.ToDecimal(txt_Avans.Text),
                Convert.ToDecimal(txt_Provizija.Text), Convert.ToDecimal(txt_Transport.Text), VCena, Convert.ToDecimal(txt_Rabat.Text),
                Convert.ToDecimal(txt_Marza.Text), Convert.ToDecimal(txt_Akcija.Text), MCena);
        }

        private void PrikaziProgrameIKolekcije()
        {
            var query = "Select * from Program";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            var query2 = "Select Kolekcije.ID as ID,Kolekcije.Naziv as Naziv " +
                " from Kolekcije ";
            SqlConnection conn2 = new SqlConnection(connect);
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds2.Tables[0];

        }

        private void frmKalkulatorStavke_Load(object sender, EventArgs e)
        {
            PrikaziProgrameIKolekcije();
            
            RefreshGV();
            FillCombo();

            txt_Trosak.Enabled = false;
            txt_ID.Enabled = false;
            txt_MpCena.Enabled = false;
            txt_Mp2.Enabled = false;
            txt_VpCena.Enabled = false;
            combo_Zaglavlje.Text = zaglavlje;

            var query = "Select Zavrsen from Kalkulator Where Rtrim(Naziv) = '" + zaglavlje.Trim() + "'";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                zavrsen = Convert.ToInt32(dr["Zavrsen"].ToString());
            }
            conn.Close();

            if (zavrsen == 1)
            {
                tsNew.Enabled = false;
                tsSave.Enabled = false;
                tsDelete.Enabled = false;
                toolStrip1.Enabled = false;
                toolStripButton2.Enabled = false;
                btn_Akcija.Enabled = false;
                btn_Rabat.Enabled = false;
                button1.Enabled = false;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DateTime dt;
            string naziv;
            decimal kurs;
            int cenovnik;
            Kalkulacije.InsertKalkulator kalk = new InsertKalkulator();
            var query = "select * from Kalkulator Where ID= " + Convert.ToInt32(combo_Zaglavlje.SelectedValue);
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                dt = Convert.ToDateTime(dr["DatumVazenja"].ToString());
                naziv = dr["Naziv"].ToString();
                kurs = Convert.ToDecimal(dr["Kurs"].ToString());
                cenovnik = Convert.ToInt32(dr["VpCenovnik"].ToString());

                kalk.UpdKalkulator(Convert.ToInt32(combo_Zaglavlje.SelectedValue), dt, naziv, kurs, cenovnik, 1);
            }
            
            conn.Close();
            RefreshGV();
            tsNew.Enabled = false;
            tsSave.Enabled = false;
            tsDelete.Enabled = false;
            toolStrip1.Enabled = false;
            toolStripButton2.Enabled = false;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Kalkulacije.InsertKalkulatorStavke kalk = new InsertKalkulatorStavke();
            kalk.AzurirajCeneTroskaStavke(Convert.ToInt32(kalkulator));
            RefreshGV();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string programi = "";
            int prviprogram = 0;

            string kolekcije = "";
            int prvakolekcija = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected)
                {
                    if (prviprogram == 0)
                    {
                        programi = row.Cells[0].Value.ToString();
                        prviprogram = prviprogram + 1;
                    }
                    else
                    {
                        programi = programi + "," + row.Cells[0].Value.ToString();
                        prviprogram = prviprogram + 1;
                    }
                }
            }

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected)
                {
                    if (prvakolekcija== 0)
                    {
                        kolekcije = row.Cells[0].Value.ToString();
                        prvakolekcija = prvakolekcija + 1;
                    }
                    else
                    {
                        kolekcije = kolekcije + "," + row.Cells[0].Value.ToString();
                        prvakolekcija = prvakolekcija + 1;
                    }
                }
            }

            var query = "Select KalkulatorStavke.ID,KalkulatorStavke.WSifra, Program.Naziv as Program, Kolekcije.Naziv as Kolekcija, KalkulatorStavke.MPSifra,MaticniPodatki.MpNaziv,KalkulatorStavke.IDKalkulator," +
               "Kalkulator.Naziv,TrosakProizvodnje,Avans,Provizija,Transport,VpCena,Rabat,Marza,Akcija,MPCena,PrimenaTroskovnogCenovnika.MpSifraVeznog " +
               "From KalkulatorStavke " +
               "Inner join MaticniPodatki on KalkulatorStavke.MPSifra = MaticniPodatki.MpSifra " +
               "Inner join Kalkulator on KalkulatorStavke.IDKalkulator = Kalkulator.ID " +
               "Inner join Program on KalkulatorStavke.ProgramID = Program.ID " +
               "Inner join Kolekcije on KalkulatorStavke.KolekcijaID = Kolekcije.ID " +
               "left join PrimenaTroskovnogCenovnika on KalkulatorStavke.MPSifra = PrimenaTroskovnogCenovnika.MpSifra " +
               "Where KalkulatorStavke.ProgramID in (" + programi + ") and KalkulatorStavke.KolekcijaID in (" + kolekcije + ")";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
