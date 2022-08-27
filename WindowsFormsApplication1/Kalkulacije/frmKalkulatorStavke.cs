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
        decimal VPCenaBP = 0;
        int kalkulator = 0;
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
        string KursPrenesen = "0";
        int Deviznicenovnik = 0;
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
        public frmKalkulatorStavke(int idK, string zag, string Kurs, int DevizniCen)
        {

            InitializeComponent();
            kalkulator = idK;
            zaglavlje = zag;
            txtKurs.Text = Kurs;
            KursPrenesen = Kurs;
            FillCombo();
            if (DevizniCen == 1)
            {
                chkDevizni.Checked = true;
                Deviznicenovnik = 1;
            }
            else
            {
                chkDevizni.Checked = false;
                Deviznicenovnik = 0;
            }

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
        private void ObojDatagrid()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                decimal pom = Convert.ToDecimal(row.Cells[9].Value);

                if (pom < 0)
                {
                    row.DefaultCellStyle.BackColor = Color.IndianRed;
                    row.DefaultCellStyle.SelectionBackColor = Color.IndianRed;
                }
            }
        }
        private void PrikaziDevizniCeo()
        {
            var query = "Select KalkulatorStavke.ID,Program.Naziv, Kolekcije.Naziv, KalkulatorStavke.MPSifra, " +
   " KalkulatorStavke.WSifra, KalkulatorStavke.TrosakProizvodnje, " +
   " Round((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs) - 1) * 100, 1) as Marza, " +
   " Round(KalkulatorStavke.MarzaRef, 1) as MarzaRef, Round(PragMarze,1) as PragMarze, Round(((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)) - 1) * 100 - PragMarze, 0) as RazlikaOdPraga," +
   " Round(KalkulatorStavke.VpCena,0) as VPCena, KalkulatorStavke.Rabat, " +
   " Round(((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)) - 1) * 100, 0) - " +
   " (Round(((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)) - 1) * 100 * Rabat * 0.01, 0)) as MarzaRabat, " +
   " Round(KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Rabat * 0.01),0) as VPCenaRabat, " +
   " KalkulatorStavke.Akcija, Round(KalkulatorStavke.Marza,1) as MarzaAkcija, " +
   " Round((KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Rabat * 0.01) - ((KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Rabat * 0.01))  * akcija * 0.01)),0) " +
   "  as VPCEnaAkcija " +
   " from KalkulatorStavke " +
   " inner join Program on KalkulatorStavke.ProgramID = Program.ID " +
   " Inner " +
   " join Kolekcije on KalkulatorStavke.KolekcijaID = Kolekcije.ID " +
   " inner " +
   " join Kalkulator on Kalkulator.ID = KalkulatorStavke.IDKalkulator " +
   " Where IDKalkulator = " + kalkulator;

            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];


            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;


            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Program";
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Kolekcija";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Mp sifra";
            dataGridView1.Columns[3].Width = 70;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "WS Sifra";
            dataGridView1.Columns[4].Width = 150;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Trosak";
            dataGridView1.Columns[5].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Marza";
            dataGridView1.Columns[6].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Referencijala";
            dataGridView1.Columns[7].DefaultCellStyle.Format = "N1";

            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Prag";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Razlika";
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[9].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[9].Width = 50;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "VP Cena";
            dataGridView1.Columns[10].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[10].Width = 80;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Rabat";
            dataGridView1.Columns[11].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[11].Width = 80;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Marza Rabat";
            dataGridView1.Columns[12].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[12].Width = 80;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "VP cena rabat";
            dataGridView1.Columns[13].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[13].Width = 80;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Akcija";
            dataGridView1.Columns[14].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[14].Width = 80;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Marza Akcija";
            dataGridView1.Columns[15].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[15].Width = 80;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "VP Cena akcija";
            dataGridView1.Columns[16].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[16].Width = 80;

            ObojDatagrid();
        }

        private void PrikaziDevizniPK()
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
                    if (prvakolekcija == 0)
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
            var query = "Select KalkulatorStavke.ID,Program.Naziv, Kolekcije.Naziv, KalkulatorStavke.MPSifra, " +
" KalkulatorStavke.WSifra, KalkulatorStavke.TrosakProizvodnje, " +
" Round((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs) - 1) * 100, 1) as Marza, " +
" Round(KalkulatorStavke.MarzaRef, 1) as MarzaRef, Round(PragMarze,1) as PragMarze, Round(((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)) - 1) * 100 - PragMarze, 0) as RazlikaOdPraga," +
" Round(KalkulatorStavke.VpCena,0) as VPCena, KalkulatorStavke.Rabat, " +
" Round(((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)) - 1) * 100, 0) - " +
" (Round(((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)) - 1) * 100 * Rabat * 0.01, 0)) as MarzaRabat, " +
" Round(KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Rabat * 0.01),0) as VPCenaRabat, " +
" KalkulatorStavke.Akcija, Round(KalkulatorStavke.Marza,1) as MarzaAkcija, " +
" Round((KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Rabat * 0.01) - ((KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Rabat * 0.01))  * akcija * 0.01)),0) " +
"  as VPCEnaAkcija " +
" from KalkulatorStavke " +
" inner join Program on KalkulatorStavke.ProgramID = Program.ID " +
" Inner " +
" join Kolekcije on KalkulatorStavke.KolekcijaID = Kolekcije.ID " +
" inner " +
" join Kalkulator on Kalkulator.ID = KalkulatorStavke.IDKalkulator " +
 "Where KalkulatorStavke.ProgramID in (" + programi + ") and KalkulatorStavke.KolekcijaID in (" + kolekcije + ") and KalkulatorStavke.IDKalkulator = " + kalkulator;

            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;


            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Program";
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Kolekcija";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Mp sifra";
            dataGridView1.Columns[3].Width = 70;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "WS Sifra";
            dataGridView1.Columns[4].Width = 150;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Trosak";
            dataGridView1.Columns[5].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Marza";
            dataGridView1.Columns[6].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Referencijala";
            dataGridView1.Columns[7].DefaultCellStyle.Format = "N1";

            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Prag";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Razlika";
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[9].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[9].Width = 80;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "VP Cena";
            dataGridView1.Columns[10].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[10].Width = 80;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Rabat";
            dataGridView1.Columns[11].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[11].Width = 80;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Marza Rabat";
            dataGridView1.Columns[12].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[12].Width = 80;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "VP cena rabat";
            dataGridView1.Columns[13].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[13].Width = 80;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Akcija";
            dataGridView1.Columns[14].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[14].Width = 80;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Marza Akcija";
            dataGridView1.Columns[15].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[15].Width = 80;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "VP Cena akcija";
            dataGridView1.Columns[16].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[16].Width = 80;

            ObojDatagrid();
        }
        private void PrikaziDinarskiCeo()
        {
            var query = "Select KalkulatorStavke.ID,Program.Naziv, Kolekcije.Naziv, KalkulatorStavke.MPSifra, " +
 " KalkulatorStavke.WSifra, KalkulatorStavke.TrosakProizvodnje, " +
 " Round(((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)) - 1) * 100, 1) as Marza, " +
 " Round(KalkulatorStavke.MarzaRef,1) as MarzaRef, Round(PragMarze,1) as PragMarze, " +
 " Round(((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)) - 1) * 100 - PragMarze, 0) as RazlikaOdPraga, " +
 " Round((KalkulatorStavke.VpCena * Kurs),0) as VpCena, " +
 " KalkulatorStavke.Rabat," +
 " KalkulatorStavke.Akcija, " +
 " Round(KalkulatorStavke.Marza,1) as MarzaAkcija, " +
 " Round(((KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Rabat * 0.01) - (KalkulatorStavke.VpCena * akcija * 0.01 * Rabat * 0.01)) - (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)),0) as Zarada, " +
 " Round((KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Rabat * 0.01) - ((KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Rabat * 0.01))  * akcija * 0.01)),0) " +
 "as VpAkcija,  " +
 " Round((MpCena ),0) as MpCena " +
 " from KalkulatorStavke " +
 " inner join Program on KalkulatorStavke.ProgramID = Program.ID " +
 " Inner " +
 " join Kolekcije on KalkulatorStavke.KolekcijaID = Kolekcije.ID " +
 " inner " +
 " join Kalkulator on Kalkulator.ID = KalkulatorStavke.IDKalkulator " +
 " Where IDKalkulator = " + kalkulator;

            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Program";
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Kolekcija";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Mp sifra";
            dataGridView1.Columns[3].Width = 80;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "WS Sifra";
            dataGridView1.Columns[4].Width = 150;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Trosak";
            dataGridView1.Columns[5].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Marza";
            dataGridView1.Columns[6].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Referencijala";
            dataGridView1.Columns[7].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Prag";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Razlika";
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[9].Width = 50;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "VP Cena";
            dataGridView1.Columns[10].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[10].Width = 80;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Rabat";
            dataGridView1.Columns[11].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[11].Width = 40;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Akcija";
            dataGridView1.Columns[12].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[12].Width = 40;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Marza Akcija";
            dataGridView1.Columns[13].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[13].Width = 80;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Zarada";
            dataGridView1.Columns[14].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[14].Width = 80;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "MP cena Akcija";
            dataGridView1.Columns[15].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[15].Width = 80;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Mp cena";
            dataGridView1.Columns[16].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[16].Width = 80;


            ObojDatagrid();
        }

        private void PrikaziDinarskiPK()
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
                    if (prvakolekcija == 0)
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
            var query = "Select KalkulatorStavke.ID,Program.Naziv, Kolekcije.Naziv, KalkulatorStavke.MPSifra, " +
" KalkulatorStavke.WSifra, KalkulatorStavke.TrosakProizvodnje, " +
" Round(((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)) - 1) * 100, 1) as Marza, " +
" Round(KalkulatorStavke.MarzaRef,1) as MarzaRef, Round(PragMarze,1) as PragMarze, " +
" Round(((KalkulatorStavke.VpCena / (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs)) - 1) * 100 - PragMarze, 0) as RazlikaOdPraga, " +
" Round((KalkulatorStavke.VpCena * Kurs),0) as VpCena, " +
" KalkulatorStavke.Rabat," +
" KalkulatorStavke.Akcija, " +
" Round(KalkulatorStavke.Marza,1) as MarzaAkcija, " +
" Round(KalkulatorStavke.VpCena -  (KalkulatorStavke.VpCena * akcija * 0.01  * 0.01) - (KalkulatorStavke.TrosakProizvodnje / Kalkulator.Kurs),0) as Zarada, " +
" Round( (KalkulatorStavke.VpCena - (KalkulatorStavke.VpCena * Akcija * 0.01) ),0) " +
"as VpAkcija,  " +
" Round((MpCena ),0) as MpCena " +
" from KalkulatorStavke " +
" inner join Program on KalkulatorStavke.ProgramID = Program.ID " +
" Inner " +
" join Kolekcije on KalkulatorStavke.KolekcijaID = Kolekcije.ID " +
" inner " +
" join Kalkulator on Kalkulator.ID = KalkulatorStavke.IDKalkulator " +
            " Where KalkulatorStavke.ProgramID in (" + programi + ") and KalkulatorStavke.KolekcijaID in (" + kolekcije + ") and KalkulatorStavke.IDKalkulator = " + kalkulator;

            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Program";
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Kolekcija";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[2].Width = 100;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Mp sifra";
            dataGridView1.Columns[3].Width = 70;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "WS Sifra";
            dataGridView1.Columns[4].Width = 150;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Trosak";
            dataGridView1.Columns[5].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Marza";
            dataGridView1.Columns[6].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Referencijala";
            dataGridView1.Columns[7].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Prag";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Razlika";
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[9].Width = 50;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "VP Cena";
            dataGridView1.Columns[10].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[10].Width = 80;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Rabat";
            dataGridView1.Columns[11].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[11].Width = 40;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Akcija";
            dataGridView1.Columns[12].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[12].Width = 40;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Marza Akcija";
            dataGridView1.Columns[13].DefaultCellStyle.Format = "N1";
            dataGridView1.Columns[13].Width = 80;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Zarada";
            dataGridView1.Columns[14].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[14].Width = 80;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "MP cena Akcija";
            dataGridView1.Columns[15].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[15].Width = 80;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Mp cena";
            dataGridView1.Columns[16].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[16].Width = 80;

            ObojDatagrid();
        }
        public void FillGvID()
        {
            if (chkDevizni.Checked == true)
            { 
                PrikaziDevizniCeo(); 
            }
            else
            {
                PrikaziDinarskiCeo();
            }
            /*
            var query = "Select KalkulatorStavke.ID,KalkulatorStavke.WSifra, Program.Naziv as Program, Kolekcije.Naziv as Kolekcija, KalkulatorStavke.MPSifra,MaticniPodatki.MpNaziv,KalkulatorStavke.IDKalkulator," +
                "Kalkulator.Naziv,TrosakProizvodnje,Avans,Provizija,Transport,VpCena,Rabat,Marza,Akcija,MPCena,PrimenaTroskovnogCenovnika.MpSifraVeznog, MarzaRef, PragMarze " +
                "From KalkulatorStavke " +
                "Inner join MaticniPodatki on KalkulatorStavke.MPSifra = MaticniPodatki.MpSifra " +
                "Inner join Kalkulator on KalkulatorStavke.IDKalkulator = Kalkulator.ID " +
                "Inner join Program on KalkulatorStavke.ProgramID = Program.ID " +
                "Inner join Kolekcije on KalkulatorStavke.KolekcijaID = Kolekcije.ID " +
                "left join PrimenaTroskovnogCenovnika on KalkulatorStavke.MPSifra = PrimenaTroskovnogCenovnika.MpSifra " +
                "Where IDKalkulator = " + kalkulator;
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "WKOD";
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Program";
            dataGridView1.Columns[2].Width = 70;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Kolekcija";
            dataGridView1.Columns[3].Width = 70;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "MP Sif";
            dataGridView1.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "MP Naziv";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "KALKID";
            dataGridView1.Columns[6].Width = 10;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "KALKNAZ";
            dataGridView1.Columns[7].Width = 70;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Trosak";
            dataGridView1.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Avans";
            dataGridView1.Columns[9].Width = 40;
            */
        }
        private void FillGV()
        {
            if (chkDevizni.Checked == true)
            {
                PrikaziDevizniPK();
            }
            else
            {
                PrikaziDinarskiPK();
            }
            /*

            var query = "Select KalkulatorStavke.ID,KalkulatorStavke.WSifra, Program.Naziv as Program, Kolekcije.Naziv as Kolekcija, KalkulatorStavke.MPSifra,MaticniPodatki.MpNaziv,KalkulatorStavke.IDKalkulator," +
                "Kalkulator.Naziv,TrosakProizvodnje,Avans,Provizija,Transport,VpCena,Rabat,Marza,Akcija,MPCena,PrimenaTroskovnogCenovnika.MpSifraVeznog, MarzaRef, PragMarze " +
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

            string value = dataGridView1.Rows[0].Cells[0].Value.ToString();

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "WKOD";
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Program";
            dataGridView1.Columns[2].Width = 70;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Kolekcija";
            dataGridView1.Columns[3].Width = 70;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "MP Sif";
            dataGridView1.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "MP Naziv";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "KALKID";
            dataGridView1.Columns[6].Width = 10;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "KALKNAZ";
            dataGridView1.Columns[7].Width = 70;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Trosak";
            dataGridView1.Columns[8].Width = 80;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Avans";
            dataGridView1.Columns[9].Width = 40;
            */
            ObojDatagrid();
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
        }
        private void Cene()
        {
            /*
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
            */
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

        private void VratiIzBP(int MP)
        {
            
            var query = "select VPCena from PrimenaTroskovnogCenovnika Where MpSifra= " + MP;
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
                VPCenaBP = Convert.ToDecimal(dr["VPCena"].ToString());
            }
            conn.Close();

        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        if (chkDevizni.Checked == true)
                        {
                            txt_ID.Text = row.Cells[0].Value.ToString();
                            txt_mpSifra.Text = row.Cells[3].Value.ToString();
                            txtWebsSifra.Text = row.Cells[4].Value.ToString();
                            txt_Trosak.Text = Convert.ToDecimal(row.Cells[5].Value.ToString()).ToString();
                            txt_MarzaPocetna.Text = Convert.ToDecimal(row.Cells[6].Value.ToString()).ToString();
                            //combo_Zaglavlje.SelectedValue = row.Cells[6].Value.ToString();
                            txtMarzaRefer.Text = Convert.ToDecimal(row.Cells[7].Value.ToString()).ToString();
                            txtPragMarze.Text = Convert.ToDecimal(row.Cells[8].Value.ToString()).ToString();
                            //txt_Trosak.Text = row.Cells[8].Value.ToString();

                            txt_Avans.Text = "0";
                            txt_Provizija.Text = "0";
                            txt_Transport.Text = "0";

                            txt_VpCena.Text = Convert.ToDecimal(row.Cells[10].Value.ToString()).ToString();
                            txt_VpCenaRSD.Text = (Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(txtKurs.Text)).ToString();
                            txt_Rabat.Text = Convert.ToDecimal(row.Cells[11].Value.ToString()).ToString();
                            txt_MarzaPArtner.Text = Convert.ToDecimal(row.Cells[11].Value.ToString()).ToString();
                            txt_VpCenaRabat.Text = Convert.ToDecimal(row.Cells[13].Value.ToString()).ToString();
                            txt_Akcija.Text = Convert.ToDecimal(row.Cells[14].Value.ToString()).ToString();
                            txt_Marza.Text = Convert.ToDecimal(row.Cells[15].Value.ToString()).ToString();
                            txt_VpCenaAkcija.Text = Convert.ToDecimal(row.Cells[16].Value).ToString();
                            txt_VpCenaAkcijaRSD.Text = (Convert.ToDecimal(row.Cells[16].Value) * Convert.ToDecimal(txtKurs.Text)).ToString();
                            label32.Visible = false;
                            txt_MpCenaRSD.Visible = false;
                            label12.Visible = false;
                            txt_MpCena.Visible = false;
                            label30.Visible = true;
                            txt_VpCenaRabat.Visible = true;

                           // txt_VpCenaAkcija.Text = (Convert.ToDecimal(txt_VpCenaRabat.Text) - (Convert.ToDecimal(txt_VpCenaRabat.Text) * Convert.ToDecimal(txt_Akcija.Text) * Convert.ToDecimal(0.01))).ToString();
                            // txt_MpCena.Text = row.Cells[16].Value.ToString();

                        }
                        else
                        {
                            txt_ID.Text = row.Cells[0].Value.ToString();
                            txt_mpSifra.Text = row.Cells[3].Value.ToString();
                            txtWebsSifra.Text = row.Cells[4].Value.ToString();
                            txt_Trosak.Text = Convert.ToDecimal(row.Cells[5].Value.ToString()).ToString();
                            txt_MarzaPocetna.Text = Convert.ToDecimal(row.Cells[6].Value.ToString()).ToString();
                            txtMarzaRefer.Text = Convert.ToDecimal(row.Cells[7].Value.ToString()).ToString();
                            txtPragMarze.Text = Convert.ToDecimal(row.Cells[8].Value.ToString()).ToString();
                            //  combo_Zaglavlje.SelectedValue = row.Cells[6].Value.ToString();
                            txt_Avans.Text = "0";
                            txt_Provizija.Text = "0";
                            txt_Transport.Text = "0";
                            //txt_Trosak.Text = row.Cells[8].Value.ToString();
                            // txt_Avans.Text = row.Cells[9].Value.ToString();
                            // txt_Provizija.Text = row.Cells[10].Value.ToString();
                            // txt_Transport.Text = row.Cells[11].Value.ToString();
                            
                            txt_VpCena.Text = Convert.ToDecimal((Convert.ToDecimal(row.Cells[10].Value.ToString()) / Convert.ToDecimal(txtKurs.Text)).ToString()).ToString();
                            txt_VpCenaRSD.Text = ((Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(txtKurs.Text)).ToString());

                            txt_Rabat.Text = Convert.ToDecimal(row.Cells[11].Value.ToString()).ToString();
                            txt_Akcija.Text = Convert.ToDecimal(row.Cells[12].Value.ToString()).ToString();
                            txt_Marza.Text = Convert.ToDecimal(row.Cells[13].Value.ToString()).ToString();
                            txt_VpCenaAkcija.Text = row.Cells[15].Value.ToString() ;
                            txt_VpCenaAkcijaRSD.Text = (Convert.ToDecimal(row.Cells[15].Value) * Convert.ToDecimal(txtKurs.Text)).ToString();
                            txt_MpCena.Text = Convert.ToDecimal((Convert.ToDecimal(row.Cells[16].Value.ToString()) / Convert.ToDecimal(txtKurs.Text)).ToString()).ToString();
                            txt_MpCenaRSD.Text = Convert.ToDecimal((Convert.ToDecimal(txt_MpCena.Text) * Convert.ToDecimal(txtKurs.Text)).ToString()).ToString();
                            label32.Visible = true;
                            txt_MpCenaRSD.Visible = true;
                            label12.Visible = true;
                            txt_MpCena.Visible = true;
                            label30.Visible = false;
                            txt_VpCenaRabat.Visible = false;

                            txt_VpCenaRSD.Text = ((Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(txtKurs.Text)).ToString());

                        }
                        txtAvansUK.Text = (Convert.ToDecimal(txt_Avans.Text) * Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(0.01)).ToString();
                        txtProvizijaUK.Text = (Convert.ToDecimal(txt_Provizija.Text) * Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(0.01)).ToString();
                        txtTransportUK.Text = (Convert.ToDecimal(txt_Transport.Text) * Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(0.01)).ToString();

                        
                        
                        
                      

                        
                        decimal marza1 = Convert.ToDecimal(txt_Marza.Text) - (Convert.ToDecimal(txt_Avans.Text) + Convert.ToDecimal(txt_Provizija.Text) + Convert.ToDecimal(txt_Transport.Text));
                        decimal marza2 = ((Convert.ToDecimal(txt_VpCena.Text) / (Convert.ToDecimal(txt_Trosak.Text) / Convert.ToDecimal(txtKurs.Text))) - 1) * 100;
                        txt_MarzaPocetna.Text = marza2.ToString();

                        decimal VPCENASaRabatom = Convert.ToDecimal(txt_VpCena.Text) - (Convert.ToDecimal(txt_VpCena.Text) * (Convert.ToDecimal(txt_Rabat.Text) * Convert.ToDecimal(0.01)));
                        // decimal 
                        // decimal MarzaNova = marza - (Convert.ToDecimal(txt_Rabat.Text)- rabat);
                        decimal MarzaNova2 = ((VPCENASaRabatom / (Convert.ToDecimal(txt_Trosak.Text) / Convert.ToDecimal(txtKurs.Text))) - 1) * 100;

                        txt_MarzaPArtner.Text = MarzaNova2.ToString();
                        txt_MarzaPArtnerIznos.Text = (VPCENASaRabatom - (Convert.ToDecimal(txt_Trosak.Text) / Convert.ToDecimal(txtKurs.Text))).ToString();
                        decimal VPCENASaAkcijom = VPCENASaRabatom - (VPCENASaRabatom * (Convert.ToDecimal(txt_Akcija.Text) * Convert.ToDecimal(0.01)));
                        // decimal 
                        // decimal MarzaNova = marza - (Convert.ToDecimal(txt_Rabat.Text)- rabat);
                        decimal MarzaNova = ((VPCENASaAkcijom / (Convert.ToDecimal(txt_Trosak.Text) / Convert.ToDecimal(txtKurs.Text))) - 1) * 100;

                       // txt_Marza.Text = MarzaNova.ToString();
                        txt_MarzaIznos.Text = (VPCENASaAkcijom - (Convert.ToDecimal(txt_Trosak.Text) / Convert.ToDecimal(txtKurs.Text))).ToString();


                        txt_MarzaPocetna.Text = marza2.ToString();
                        txt_MarzaPocetnaIznos.Text = (Convert.ToDecimal(txt_VpCena.Text) - (Convert.ToDecimal(txt_Trosak.Text) / Convert.ToDecimal(txtKurs.Text))).ToString();


                        
                        txt_VpCenaRabat.Text = (Convert.ToDecimal(txt_VpCena.Text) - (Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(txt_Rabat.Text) * Convert.ToDecimal(0.01))).ToString();
                       // txt_VpCenaAkcija.Text = (Convert.ToDecimal(txt_VpCenaRabat.Text) - (Convert.ToDecimal(txt_VpCenaRabat.Text) * Convert.ToDecimal(txt_Akcija.Text) * Convert.ToDecimal(0.01))).ToString();
                        txt_MpCenaRSD.Text = (Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(txtKurs.Text) * Convert.ToDecimal(1.25) * Convert.ToDecimal(1.2)).ToString();
                        txt_MpCena.Text = (Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(1.25) * Convert.ToDecimal(1.2)).ToString();
                        // txt_MarzaPocetnaIznos
                        //  Trosak();
                        // Cene();
                        // IzracunajUkupnoIProsek();
                    }
                }
            }
            catch { }
            ProsecneMarze();
        }

        private void ProsecneMarze()
        {
            double ProsecnaMarzaSvi = 0;
            double ProsecnaMarzaSelektovani = 0;
            double ProsecnaMarzaSviRabat = 0;
            double ProsecnaMarzaSelektovaniRabat = 0;
            double ProsecnaMarzaSviAkcija = 0;
            double ProsecnaMarzaSelektovaniAkcija = 0;

            int ProsecnaMarzaSviCounter = 0;
            int ProsecnaMarzaSelektovaniCounter = 0;

            int ProsecnaMarzaSviCounterRabat = 0;
            int ProsecnaMarzaSelektovaniCounterRabat = 0;

            int ProsecnaMarzaSviCounterAkcija = 0;
            int ProsecnaMarzaSelektovaniCounterAkcija = 0;


            if (chkDevizni.Checked == true)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    ProsecnaMarzaSvi = ProsecnaMarzaSvi + Convert.ToDouble(row.Cells[6].Value.ToString());
                    ProsecnaMarzaSviCounter = ProsecnaMarzaSviCounter + 1;
                    txtSA.Text = (ProsecnaMarzaSvi / ProsecnaMarzaSviCounter).ToString("0.##");

                    ProsecnaMarzaSviRabat = ProsecnaMarzaSviRabat + Convert.ToDouble(row.Cells[12].Value.ToString());
                    ProsecnaMarzaSviCounterRabat = ProsecnaMarzaSviCounterRabat + 1;
                    txtSA1.Text = (ProsecnaMarzaSviRabat / ProsecnaMarzaSviCounterRabat).ToString("0.##");

                    ProsecnaMarzaSviAkcija = ProsecnaMarzaSviAkcija + Convert.ToDouble(row.Cells[15].Value.ToString());
                    ProsecnaMarzaSviCounterAkcija = ProsecnaMarzaSviCounterAkcija + 1;
                    txtSA2.Text = (ProsecnaMarzaSviAkcija / ProsecnaMarzaSviCounterAkcija).ToString("0.##");

                    if (row.Selected)
                    {
                        ProsecnaMarzaSelektovani = ProsecnaMarzaSelektovani + Convert.ToDouble(row.Cells[6].Value.ToString());
                        ProsecnaMarzaSelektovaniCounter = ProsecnaMarzaSelektovaniCounter + 1;
                        txtSR.Text = (ProsecnaMarzaSelektovani / ProsecnaMarzaSelektovaniCounter).ToString("0.##");

                        ProsecnaMarzaSelektovaniRabat = ProsecnaMarzaSelektovaniRabat + Convert.ToDouble(row.Cells[12].Value.ToString());
                        ProsecnaMarzaSelektovaniCounterRabat = ProsecnaMarzaSelektovaniCounterRabat + 1;
                        txtSR1.Text = (ProsecnaMarzaSelektovaniRabat / ProsecnaMarzaSelektovaniCounterRabat).ToString("0.##");

                        ProsecnaMarzaSelektovaniAkcija = ProsecnaMarzaSelektovaniAkcija + Convert.ToDouble(row.Cells[15].Value.ToString());
                        ProsecnaMarzaSelektovaniCounterAkcija = ProsecnaMarzaSelektovaniCounterAkcija + 1;
                        txtSR2.Text = (ProsecnaMarzaSelektovaniAkcija / ProsecnaMarzaSelektovaniCounterAkcija).ToString("0.##");
                    }
                }

            }

            if (chkDevizni.Checked == false)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    ProsecnaMarzaSvi = ProsecnaMarzaSvi + Convert.ToDouble(row.Cells[6].Value.ToString());
                    ProsecnaMarzaSviCounter = ProsecnaMarzaSviCounter + 1;
                    txtSA.Text = (ProsecnaMarzaSvi / ProsecnaMarzaSviCounter).ToString();

                    //  ProsecnaMarzaSviRabat = ProsecnaMarzaSviRabat + Convert.ToDouble(row.Cells[12].Value.ToString());
                    //  ProsecnaMarzaSviCounterRabat = ProsecnaMarzaSviCounterRabat + 1;
                    txtSA1.Text = "0";

                    ProsecnaMarzaSviAkcija = ProsecnaMarzaSviAkcija + Convert.ToDouble(row.Cells[13].Value.ToString());
                    ProsecnaMarzaSviCounterAkcija = ProsecnaMarzaSviCounterAkcija + 1;
                    txtSA2.Text = (ProsecnaMarzaSviAkcija / ProsecnaMarzaSviCounterAkcija).ToString();

                    if (row.Selected)
                    {
                        ProsecnaMarzaSelektovani = ProsecnaMarzaSelektovani + Convert.ToDouble(row.Cells[6].Value.ToString());
                        ProsecnaMarzaSelektovaniCounter = ProsecnaMarzaSelektovaniCounter + 1;
                        txtSR.Text = (ProsecnaMarzaSelektovani / ProsecnaMarzaSelektovaniCounter).ToString();

                        //  ProsecnaMarzaSelektovaniRabat = ProsecnaMarzaSelektovaniRabat + Convert.ToDouble(row.Cells[12].Value.ToString());
                        //  ProsecnaMarzaSelektovaniCounterRabat = ProsecnaMarzaSelektovaniCounterRabat + 1;
                        txtSR1.Text = "0";

                        ProsecnaMarzaSelektovaniAkcija = ProsecnaMarzaSelektovaniAkcija + Convert.ToDouble(row.Cells[13].Value.ToString());
                        ProsecnaMarzaSelektovaniCounterAkcija = ProsecnaMarzaSelektovaniCounterAkcija + 1;
                        txtSR2.Text = (ProsecnaMarzaSelektovaniAkcija / ProsecnaMarzaSelektovaniCounterAkcija).ToString();
                    }
                }

            }


        }
        


        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            tsNew.Checked = true;
            tsNew.Enabled = false;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int ProgramID = 0;
            int ProgramSelektovan = 0;
            int KolekcijaSelektovana = 0;
            int KolekcijaID = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected == true)
                {
                    ProgramID = Convert.ToInt32(row.Cells[0].Value.ToString());
                    ProgramSelektovan = 1;
                }
            }

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected == true)
                {
                    KolekcijaID = Convert.ToInt32(row.Cells[0].Value.ToString());
                    KolekcijaSelektovana = 1;
                }
            }

            if (KolekcijaSelektovana == 0)
            {
                MessageBox.Show("Morate selektovati kolekciju");
                return;
            }

            if (ProgramSelektovan == 0)
            {
                MessageBox.Show("Morate selektovati program");
                return;
            }

            if (status == true)
            {
                Kalkulacije.InsertKalkulatorStavke kalk = new InsertKalkulatorStavke();
                kalk.InsKalkulatorStavke(Convert.ToInt32(txt_mpSifra.Text), Convert.ToInt32(combo_Zaglavlje.SelectedValue), Convert.ToDecimal(txt_Trosak.Text),
                    Convert.ToDecimal(txt_Avans.Text), Convert.ToDecimal(txt_Provizija.Text), Convert.ToDecimal(txt_Transport.Text), Convert.ToDecimal(txt_VpCena.Text),
                    Convert.ToDecimal(txt_Rabat.Text), Convert.ToDecimal(txt_Marza.Text), Convert.ToDecimal(txt_Akcija.Text), Convert.ToDecimal(txt_MpCena.Text), txtWebsSifra.Text, Convert.ToInt32(ProgramID), Convert.ToInt32(KolekcijaID), Convert.ToDecimal(txtMarzaRefer.Text),Convert.ToDecimal(txtPragMarze.Text));
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
                    Convert.ToDecimal(txt_Rabat.Text), Convert.ToDecimal(txt_Marza.Text), Convert.ToDecimal(txt_Akcija.Text), Convert.ToDecimal(txt_MpCena.Text), txtWebsSifra.Text, ProgramID, KolekcijaID,  Convert.ToDecimal(txtMarzaRefer.Text),Convert.ToDecimal(txtPragMarze.Text));
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
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da za sve stavke želite da primenite rabat od " + txt_Rabat.Text + "% ?", "Rabat",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            int ProgramID = 0;
            int ProgramSelektovan = 0;
            int KolekcijaSelektovana = 0;
            int KolekcijaID = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected == true)
                {
                    ProgramID = Convert.ToInt32(row.Cells[0].Value.ToString());
                    ProgramSelektovan = 1;
                }
            }

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected == true)
                {
                    KolekcijaID = Convert.ToInt32(row.Cells[0].Value.ToString());
                    KolekcijaSelektovana = 1;
                }
            }

            if (KolekcijaSelektovana == 0)
            {
                MessageBox.Show("Morate selektovati kolekciju");
                return;
            }

            if (ProgramSelektovan == 0)
            {
                MessageBox.Show("Morate selektovati program");
                return;
            }



            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Selected == true)
                        {
                            //Resen avans
                            var query = "Select * from KalkulatorStavke Where ID=" + Convert.ToInt32(row.Cells[0].Value.ToString());
                            //  var query = "Select * from KalkulatorStavke Where IDKalkulator="+Convert.ToInt32(combo_Zaglavlje.SelectedValue);
                            SqlConnection conn = new SqlConnection(connect);
                            conn.Open();
                            SqlCommand cmd = new SqlCommand(query, conn);
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                int id = Convert.ToInt32(dr["ID"].ToString());
                                string wSifra = dr["WSifra"].ToString();
                                int mpSifre = Convert.ToInt32(dr["MPSifra"].ToString());
                                int idKalk = Convert.ToInt32(dr["IDKalkulator"].ToString());
                                decimal trosak = Convert.ToDecimal(dr["TrosakProizvodnje"].ToString());
                                decimal avans = Convert.ToDecimal(dr["Avans"].ToString());
                                decimal provizija = Convert.ToDecimal(dr["Provizija"].ToString());
                                decimal transport = Convert.ToDecimal(dr["Transport"].ToString());
                                decimal rabat = Convert.ToDecimal(txt_Rabat.Text);
                                decimal marza = Convert.ToDecimal(dr["Marza"].ToString()); ;
                                //= Convert.ToDecimal(dr["Marza"].ToString());
                                decimal akcija = Convert.ToDecimal(dr["Akcija"].ToString());
                                decimal mpCena = Convert.ToDecimal(dr["MpCena"].ToString());
                                decimal marzaRef = Convert.ToDecimal(dr["MarzaRef"].ToString());
                                decimal PragMarze = Convert.ToDecimal(dr["PragMarze"].ToString());
                                decimal VCena = Convert.ToDecimal(dr["VPCena"].ToString());


                                decimal marza1 = Convert.ToDecimal(marza) - (Convert.ToDecimal(avans) + Convert.ToDecimal(provizija) + Convert.ToDecimal(transport));
                                decimal marza2 = ((VCena / (Convert.ToDecimal(trosak) / Convert.ToDecimal(txtKurs.Text))) - 1) * 100;


                                decimal VPCENASaRabatom = VCena - (VCena * (Convert.ToDecimal(rabat) * Convert.ToDecimal(0.01)));
                                decimal MarzaNova2 = ((VPCENASaRabatom / (Convert.ToDecimal(trosak) / Convert.ToDecimal(txtKurs.Text))) - 1) * 100;
                                decimal VPCenaRabat = (Convert.ToDecimal(VCena) - (Convert.ToDecimal(VCena) * Convert.ToDecimal(rabat) * Convert.ToDecimal(0.01)));
                                
                                
                                decimal VPCenaAkcija = 0;
                                decimal VPCENASaAkcijom = 0;
                                if (chkDevizni.Checked == true)
                                {
                                    VPCenaAkcija = (Convert.ToDecimal(VPCenaRabat) - (Convert.ToDecimal(rabat) * Convert.ToDecimal(akcija) * Convert.ToDecimal(0.01)));
                                    VPCENASaAkcijom = VPCENASaRabatom - (VPCENASaRabatom * (Convert.ToDecimal(akcija) * Convert.ToDecimal(0.01)));
                                }
                                else
                                {
                                    VPCenaAkcija = VCena - (VCena * (Convert.ToDecimal(akcija) * Convert.ToDecimal(0.01)));
                                    VPCENASaAkcijom = VCena - (VCena * (Convert.ToDecimal(akcija) * Convert.ToDecimal(0.01)));

                                }
                                decimal MarzaNova = ((VPCENASaAkcijom / (Convert.ToDecimal(trosak) / Convert.ToDecimal(txtKurs.Text))) - 1) * 100;
                                // txt_MpCenaRSD.Text = (Convert.ToDecimal(VPCenaAkcija) * Convert.ToDecimal(txtKurs.Text) * Convert.ToDecimal(1.25) * Convert.ToDecimal(1.2)).ToString();
                                //  decimal MCena = (Convert.ToDecimal(VPCenaAkcija) * Convert.ToDecimal(1.25) * Convert.ToDecimal(1.2));
                                decimal MCena = (Convert.ToDecimal(VPCENASaAkcijom) * Convert.ToDecimal(1.25) * Convert.ToDecimal(1.2));


                                int programid = Convert.ToInt32(dr["ProgramID"].ToString());
                                int kolekcijaid = Convert.ToInt32(dr["kolekcijaID"].ToString());
                                Kalkulacije.InsertKalkulatorStavke kalk = new InsertKalkulatorStavke();
                                kalk.UpdKalkulatorStavke(id, mpSifre, idKalk, trosak, avans, provizija, transport, VCena, Convert.ToDecimal(txt_Rabat.Text), MarzaNova, akcija, MCena, wSifra, programid, kolekcijaid, Convert.ToDecimal(txtMarzaRefer.Text), PragMarze);

                                //ROW UPDATE
                                //txt_mpSifra.Text = row.Cells[4].Value.ToString();
                                //txt_ID.Text = row.Cells[0].Value.ToString();

                                //combo_Zaglavlje.SelectedValue = row.Cells[6].Value.ToString();
                                //txtWebsSifra.Text = row.Cells[1].Value.ToString();
                                //txt_Trosak.Text = row.Cells[8].Value.ToString();
                                //txt_Avans.Text = row.Cells[9].Value.ToString();
                                //txt_Provizija.Text = row.Cells[10].Value.ToString();
                                // txt_Transport.Text = row.Cells[11].Value.ToString();
                                if (chkDevizni.Checked == true)
                                {
                                    // row.Cells[10].Value = VCena.ToString();
                                    row.Cells[16].Value = VPCenaAkcija.ToString();
                                    row.Cells[15].Value = MarzaNova.ToString(); 
                                    row.Cells[14].Value = akcija.ToString();
                                    row.Cells[13].Value = VPCenaRabat.ToString();
                                    row.Cells[12].Value = MarzaNova2.ToString();
                                    row.Cells[11].Value = rabat.ToString();
                                }
                                else
                                {
                                    row.Cells[11].Value = rabat.ToString();
                                    row.Cells[12].Value = akcija.ToString();
                                    row.Cells[13].Value = MarzaNova.ToString();
                                    row.Cells[15].Value = VPCenaAkcija.ToString();
                                    row.Cells[16].Value = MCena.ToString();
                                }
                              
                              


                                //ROW END UPDATE

                            }
                            conn.Close();
                        }
                    }




                   ///ZAKOMENTARISAO refreshGGGG();
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
                        if (row.Selected == true)
                        {

                            var query = "Select * from KalkulatorStavke Where ID=" + Convert.ToInt32(row.Cells[0].Value.ToString());
                            //  var query = "Select * from KalkulatorStavke Where IDKalkulator=" + Convert.ToInt32(combo_Zaglavlje.SelectedValue);
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
                                decimal rabat = Convert.ToDecimal(dr["Rabat"].ToString());
                                decimal marza;
                                //= Convert.ToDecimal(dr["Marza"].ToString());
                                decimal akcija = Convert.ToDecimal(dr["Akcija"].ToString());
                                decimal PragMarze = Convert.ToDecimal(dr["PragMarze"].ToString());
                                decimal VCena = Convert.ToDecimal(dr["VPCena"].ToString());
                                trosak = trosak / Convert.ToDecimal(txtKurs.Text); //Proracun cene troska u EUR
                                decimal MCena;
                                decimal Razlika = VCena - trosak;
                                decimal Av = (avans * Razlika);
                                decimal Tr = (transport * Razlika);
                                decimal Pr = (provizija * Razlika);
                                decimal Razlika2 = Razlika - (Av + Tr + Pr); // Troskovi sa uracunatim dodatnim troskovima oduzimam
                                                                             //Sada od dobijenog rezultata oduzmem Akciju i Novi RAbat
                                decimal Razlika3 = Razlika2 - (Convert.ToDecimal(txt_Akcija.Text) * Razlika2) - (rabat * Razlika2);
                                marza = VCena - Razlika3 / 100; // Procenat Marze
                                MCena = VCena + (VCena * 25 / 100) * Convert.ToDecimal(1.2);
                                //Maloprodajna cena dedam 25 % i pomnozim sa 20% za PDV
                                Kalkulacije.InsertKalkulatorStavke kalk = new InsertKalkulatorStavke();
                                // kalk.UpdKalkulatorStavke(id, mpSifre, idKalk, trosak, avans, provizija, transport, VCena, Convert.ToDecimal(txt_Rabat.Text), marza, akcija, MCena);
                                string wsifra = dr["WSifra"].ToString();
                                txtMarzaRefer.Text = dr["MarzaRef"].ToString();
                                int programid = Convert.ToInt32(dr["ProgramID"].ToString());
                                int kolekcijaid = Convert.ToInt32(dr["kolekcijaID"].ToString());
                                // kalk.UpdKalkulatorStavke(id, mpSifre, idKalk, trosak * Convert.ToDecimal(txtKurs.Text), avans, provizija, transport, VCena, Convert.ToDecimal(txt_Rabat.Text), MarzaNova, akcija, MCena, wsifra, programid, kolekcijaid);

                                kalk.UpdKalkulatorStavke(id, mpSifre, idKalk, trosak * Convert.ToDecimal(txtKurs.Text), avans, provizija, transport, VCena, rabat, marza, Convert.ToDecimal(txt_Akcija.Text), MCena, wsifra, programid, kolekcijaid, Convert.ToDecimal(txtMarzaRefer.Text), PragMarze);
                            }
                            conn.Close();
                        }
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
            // kalk.InsKalkulatorStavke(mpVeznog, Convert.ToInt32(combo_Zaglavlje.SelectedValue), trosak, Convert.ToDecimal(txt_Avans.Text),
            //   Convert.ToDecimal(txt_Provizija.Text), Convert.ToDecimal(txt_Transport.Text), VCena, Convert.ToDecimal(txt_Rabat.Text),
            // Convert.ToDecimal(txt_Marza.Text), Convert.ToDecimal(txt_Akcija.Text), MCena);
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

            string value = dataGridView2.Rows[0].Cells[0].Value.ToString();

            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Program";
            dataGridView2.Columns[1].Width = 145;

            var query2 = "Select Kolekcije.ID as ID,Kolekcije.Naziv as Naziv " +
                " from Kolekcije ";
            SqlConnection conn2 = new SqlConnection(connect);
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds2.Tables[0];

            string value2 = dataGridView3.Rows[0].Cells[0].Value.ToString();

            DataGridViewColumn column3 = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 20;

            DataGridViewColumn column4 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "Kolekcija";
            dataGridView3.Columns[1].Width = 145;
        }

        private void frmKalkulatorStavke_Load(object sender, EventArgs e)
        {
            PrikaziProgrameIKolekcije();
            txtKurs.Text = KursPrenesen;
            RefreshGV();
            FillCombo();

            txt_Trosak.Enabled = false;
            txt_ID.Enabled = false;
            txt_MpCena.Enabled = true;
            txt_Mp2.Enabled = false;
            txt_VpCena.Enabled = true;
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
                //  btn_Akcija.Enabled = false;
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
            //Ayurira se marza

            Kalkulacije.InsertKalkulatorStavke kalk = new InsertKalkulatorStavke();
            kalk.AzurirajCeneTroskaStavke(Convert.ToInt32(kalkulator));
            kalk.AzurirajMarzeTroskaStavke(Convert.ToInt32(kalkulator), Convert.ToDouble(txtKurs.Text));
            RefreshGV();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FillGV();
        }

        private void refreshGGGG()
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
                    if (prvakolekcija == 0)
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
               "Kalkulator.Naziv,TrosakProizvodnje,Avans,Provizija,Transport,VpCena,Rabat,Marza,Akcija,MPCena,PrimenaTroskovnogCenovnika.MpSifraVeznog, MarzaRef, PragMarze " +
               "From KalkulatorStavke " +
               "Inner join MaticniPodatki on KalkulatorStavke.MPSifra = MaticniPodatki.MpSifra " +
               "Inner join Kalkulator on KalkulatorStavke.IDKalkulator = Kalkulator.ID " +
               "Inner join Program on KalkulatorStavke.ProgramID = Program.ID " +
               "Inner join Kolekcije on KalkulatorStavke.KolekcijaID = Kolekcije.ID " +
               "left join PrimenaTroskovnogCenovnika on KalkulatorStavke.MPSifra = PrimenaTroskovnogCenovnika.MpSifra " +
               "Where KalkulatorStavke.ProgramID in (" + programi + ") and KalkulatorStavke.KolekcijaID in (" + kolekcije + ") and KalkulatorStavke.IDKalkulator = " + kalkulator;
            // MessageBox.Show(query);

            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];



        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da za sve stavke želite da primenite marzu: " + txt_Marza.Text + "% ?", "Akcija",
               MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Selected == true)
                        {



                            var query = "Select * from KalkulatorStavke Where ID=" + Convert.ToInt32(row.Cells[0].Value.ToString());
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
                                decimal rabat = Convert.ToDecimal(dr["Rabat"].ToString());
                                decimal marza = Convert.ToDecimal(dr["Marza"].ToString());
                                //= Convert.ToDecimal(dr["Marza"].ToString());
                                decimal akcija = Convert.ToDecimal(dr["Akcija"].ToString());

                                decimal VCena = Convert.ToDecimal(dr["VPCena"].ToString());
                                trosak = trosak / Convert.ToDecimal(txtKurs.Text);
                                decimal MCena;
                                decimal Razlika = VCena - trosak;
                                decimal ProcenatDodavanja = (((avans + provizija + transport + akcija + rabat + Convert.ToDecimal(txt_Marza.Text)) * Convert.ToDecimal(0.01)) + 1);
                                // Troskovi sa uracunatim dodatnim troskovima oduzimam
                                // MessageBox.Show("VCena " + VCena + "marza" + marza + "Rabat " + rabat + "Akcija " + akcija);
                                decimal VCenaBezStareMarze = VCena - (VCena * marza * Convert.ToDecimal(0.01));
                                //Izracunati Cenu sa novom Marzom, poveca se cena kao zbir svih procenata
                                decimal RazlikaUMarzi = Convert.ToDecimal(txt_Marza.Text) - marza;
                                // MessageBox.Show("Razlika u marzi " + RazlikaUMarzi.ToString());
                                decimal VCenaSaNovomMarzom = trosak * ProcenatDodavanja;

                                // decimal VCenaSaNovomMarzom = VCena +  (RazlikaUMarzi * Convert.ToDecimal(0.01) * VCena);
                                //  decimal ProcenatPromene = (VCenaSaNovomMarzom / VCena);
                                //decimal NoviRabat = rabat * ProcenatPromene;
                                //decimal NovaAkcija = akcija * ProcenatPromene;
                                //decimal NovaProvizija = provizija * ProcenatPromene;
                                //decimal NoviTransport = transport * ProcenatPromene;
                                //decimal NoviAvans = avans * ProcenatPromene;

                                //  MessageBox.Show("Cena Bez stare marze" + VCenaBezStareMarze);
                                // MessageBox.Show("Cena nova marza " + VCenaSaNovomMarzom);
                                // MessageBox.Show("Procenat promene " + ProcenatPromene);
                                MCena = VCenaSaNovomMarzom + (VCenaSaNovomMarzom * 25 / 100) * Convert.ToDecimal(1.2);
                                //Maloprodajna cena dedam 25 % i pomnozim sa 20% za PDV
                                Kalkulacije.InsertKalkulatorStavke kalk = new InsertKalkulatorStavke();
                                // kalk.UpdKalkulatorStavke(id, mpSifre, idKalk, trosak, avans, provizija, transport, VCena, Convert.ToDecimal(txt_Rabat.Text), marza, akcija, MCena);
                                string wsifra = dr["WSifra"].ToString();
                                txtMarzaRefer.Text = dr["MarzaRef"].ToString();
                                int programid = Convert.ToInt32(dr["ProgramID"].ToString());
                                int kolekcijaid = Convert.ToInt32(dr["kolekcijaID"].ToString());
                                decimal PragMarze = Convert.ToDecimal(dr["PragMarze"].ToString());
                                // kalk.UpdKalkulatorStavke(id, mpSifre, idKalk, trosak * Convert.ToDecimal(txtKurs.Text), avans, provizija, transport, VCena, Convert.ToDecimal(txt_Rabat.Text), MarzaNova, akcija, MCena, wsifra, programid, kolekcijaid);

                                kalk.UpdKalkulatorStavke(id, mpSifre, idKalk, trosak * Convert.ToDecimal(txtKurs.Text), avans, provizija, transport, VCenaSaNovomMarzom, rabat, Convert.ToDecimal(txt_Marza.Text), akcija, MCena, wsifra, programid, kolekcijaid, Convert.ToDecimal(txtMarzaRefer.Text), PragMarze);
                            }
                            conn.Close();
                        }
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

        private void button4_Click(object sender, EventArgs e)
        {
            int ProgramID = 0;
            int ProgramSelektovan = 0;
            int KolekcijaSelektovana = 0;
            int KolekcijaID = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected == true)
                {
                    ProgramID = Convert.ToInt32(row.Cells[0].Value.ToString());
                    ProgramSelektovan = 1;
                }
            }

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected == true)
                {
                    KolekcijaID = Convert.ToInt32(row.Cells[0].Value.ToString());
                    KolekcijaSelektovana = 1;
                }
            }

            if (KolekcijaSelektovana == 0)
            {
                MessageBox.Show("Morate selektovati kolekciju");
                return;
            }

            if (ProgramSelektovan == 0)
            {
                MessageBox.Show("Morate selektovati program");
                return;
            }
            //Promenom troska menja se samo Marza

            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        if (chkDevizni.Checked == true)
                        {
                            txt_ID.Text = row.Cells[0].Value.ToString();
                            txt_mpSifra.Text = row.Cells[3].Value.ToString();
                            txtWebsSifra.Text = row.Cells[4].Value.ToString();
                            txt_Trosak.Text = Convert.ToDecimal(row.Cells[5].Value.ToString()).ToString();
                            txt_MarzaPocetna.Text = Convert.ToDecimal(row.Cells[6].Value.ToString()).ToString();
                            //combo_Zaglavlje.SelectedValue = row.Cells[6].Value.ToString();
                            txtMarzaRefer.Text = Convert.ToDecimal(row.Cells[7].Value.ToString()).ToString();
                            txtPragMarze.Text = Convert.ToDecimal(row.Cells[8].Value.ToString()).ToString();
                            //txt_Trosak.Text = row.Cells[8].Value.ToString();

                            txt_Avans.Text = "0";
                            txt_Provizija.Text = "0";
                            txt_Transport.Text = "0";

                            txt_VpCena.Text = Convert.ToDecimal(row.Cells[10].Value.ToString()).ToString();
                            txt_VpCenaRSD.Text = (Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(txtKurs.Text)).ToString();
                            txt_Rabat.Text = Convert.ToDecimal(row.Cells[11].Value.ToString()).ToString();
                            txt_MarzaPArtner.Text = Convert.ToDecimal(row.Cells[11].Value.ToString()).ToString();
                            txt_VpCenaRabat.Text = Convert.ToDecimal(row.Cells[13].Value.ToString()).ToString();
                            txt_Akcija.Text = Convert.ToDecimal(row.Cells[14].Value.ToString()).ToString();
                            txt_Marza.Text = Convert.ToDecimal(row.Cells[15].Value.ToString()).ToString();
                            txt_VpCenaAkcija.Text = Convert.ToDecimal(row.Cells[16].Value).ToString();
                            txt_VpCenaAkcijaRSD.Text = (Convert.ToDecimal(row.Cells[16].Value) * Convert.ToDecimal(txtKurs.Text)).ToString();
                            label32.Visible = false;
                            txt_MpCenaRSD.Visible = false;
                            label12.Visible = false;
                            txt_MpCena.Visible = false;
                            label30.Visible = true;
                            txt_VpCenaRabat.Visible = true;

                            // txt_VpCenaAkcija.Text = (Convert.ToDecimal(txt_VpCenaRabat.Text) - (Convert.ToDecimal(txt_VpCenaRabat.Text) * Convert.ToDecimal(txt_Akcija.Text) * Convert.ToDecimal(0.01))).ToString();
                            // txt_MpCena.Text = row.Cells[16].Value.ToString();

                        }
                        else
                        {
                            txt_ID.Text = row.Cells[0].Value.ToString();
                            txt_mpSifra.Text = row.Cells[3].Value.ToString();
                            txtWebsSifra.Text = row.Cells[4].Value.ToString();
                            txt_Trosak.Text = Convert.ToDecimal(row.Cells[5].Value.ToString()).ToString();
                            txt_MarzaPocetna.Text = Convert.ToDecimal(row.Cells[6].Value.ToString()).ToString();
                            txtMarzaRefer.Text = Convert.ToDecimal(row.Cells[7].Value.ToString()).ToString();
                            txtPragMarze.Text = Convert.ToDecimal(row.Cells[8].Value.ToString()).ToString();
                            //  combo_Zaglavlje.SelectedValue = row.Cells[6].Value.ToString();
                            txt_Avans.Text = "0";
                            txt_Provizija.Text = "0";
                            txt_Transport.Text = "0";
                            //txt_Trosak.Text = row.Cells[8].Value.ToString();
                            // txt_Avans.Text = row.Cells[9].Value.ToString();
                            // txt_Provizija.Text = row.Cells[10].Value.ToString();
                            // txt_Transport.Text = row.Cells[11].Value.ToString();

                            txt_VpCena.Text = Convert.ToDecimal((Convert.ToDecimal(row.Cells[10].Value.ToString()) / Convert.ToDecimal(txtKurs.Text)).ToString()).ToString();
                            txt_VpCenaRSD.Text = ((Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(txtKurs.Text)).ToString());

                            txt_Rabat.Text = Convert.ToDecimal(row.Cells[11].Value.ToString()).ToString();
                            txt_Akcija.Text = Convert.ToDecimal(row.Cells[12].Value.ToString()).ToString();
                            txt_Marza.Text = Convert.ToDecimal(row.Cells[13].Value.ToString()).ToString();
                            txt_VpCenaAkcija.Text = row.Cells[15].Value.ToString();
                            txt_VpCenaAkcijaRSD.Text = (Convert.ToDecimal(row.Cells[15].Value) * Convert.ToDecimal(txtKurs.Text)).ToString();
                            txt_MpCena.Text = Convert.ToDecimal((Convert.ToDecimal(row.Cells[16].Value.ToString()) / Convert.ToDecimal(txtKurs.Text)).ToString()).ToString();
                            txt_MpCenaRSD.Text = Convert.ToDecimal((Convert.ToDecimal(txt_MpCena.Text) * Convert.ToDecimal(txtKurs.Text)).ToString()).ToString();
                            label32.Visible = true;
                            txt_MpCenaRSD.Visible = true;
                            label12.Visible = true;
                            txt_MpCena.Visible = true;
                            label30.Visible = false;
                            txt_VpCenaRabat.Visible = false;

                            txt_VpCenaRSD.Text = (Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(txtKurs.Text)).ToString();

                        }

                        txtAvansUK.Text = (Convert.ToDecimal(txt_Avans.Text) * Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(0.01)).ToString();
                        txtProvizijaUK.Text = (Convert.ToDecimal(txt_Provizija.Text) * Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(0.01)).ToString();
                        txtTransportUK.Text = (Convert.ToDecimal(txt_Transport.Text) * Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(0.01)).ToString();


                        decimal VCena = Convert.ToDecimal(txt_VpCena.Text);
                        decimal marza1 = Convert.ToDecimal(txt_Marza.Text) - (Convert.ToDecimal(txt_Avans.Text) + Convert.ToDecimal(txt_Provizija.Text) + Convert.ToDecimal(txt_Transport.Text));
                        decimal marza2 = ((VCena / (Convert.ToDecimal(txt_Trosak.Text) / Convert.ToDecimal(txtKurs.Text))) - 1) * 100;
                        txt_MarzaPocetna.Text = marza2.ToString();

                        decimal VPCENASaRabatom = VCena - (VCena * (Convert.ToDecimal(txt_Rabat.Text) * Convert.ToDecimal(0.01)));

                        decimal MarzaNova2 = ((VPCENASaRabatom / (Convert.ToDecimal(txt_Trosak.Text) / Convert.ToDecimal(txtKurs.Text))) - 1) * 100;

                        txt_MarzaPArtner.Text = MarzaNova2.ToString();
                        txt_MarzaPArtnerIznos.Text = (VPCENASaRabatom - (Convert.ToDecimal(txt_Trosak.Text) / Convert.ToDecimal(txtKurs.Text))).ToString();
                        decimal VPCenaAkcija = 0;
                        decimal VPCENASaAkcijom = 0;
                        if (chkDevizni.Checked == true)
                        {
                            VPCenaAkcija = (Convert.ToDecimal(VPCENASaRabatom) - (Convert.ToDecimal(txt_Rabat.Text) * Convert.ToDecimal(txt_Akcija.Text) * Convert.ToDecimal(0.01)));
                            VPCENASaAkcijom = VPCENASaRabatom - (VPCENASaRabatom * (Convert.ToDecimal(txt_Akcija.Text) * Convert.ToDecimal(0.01)));
                        }
                        else
                        {
                            VPCenaAkcija = VCena - (VCena * (Convert.ToDecimal(txt_Akcija.Text) * Convert.ToDecimal(0.01)));
                            VPCENASaAkcijom = VCena - (VCena * (Convert.ToDecimal(txt_Akcija.Text) * Convert.ToDecimal(0.01)));

                        }
                       

                         // decimal 
                        // decimal MarzaNova = marza - (Convert.ToDecimal(txt_Rabat.Text)- rabat);
                        decimal MarzaNova = ((VPCENASaAkcijom / (Convert.ToDecimal(txt_Trosak.Text) / Convert.ToDecimal(txtKurs.Text))) - 1) * 100;

                        txt_Marza.Text = MarzaNova.ToString();
                        txt_MarzaIznos.Text = (VPCENASaAkcijom - (Convert.ToDecimal(txt_Trosak.Text) / Convert.ToDecimal(txtKurs.Text))).ToString();


                        txt_MarzaPocetna.Text = marza2.ToString();
                        txt_MarzaPocetnaIznos.Text = (VCena - (Convert.ToDecimal(txt_Trosak.Text) / Convert.ToDecimal(txtKurs.Text))).ToString();


                        txt_VpCenaRSD.Text = (Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(txtKurs.Text)).ToString();
                        txt_VpCenaRabat.Text = (Convert.ToDecimal(txt_VpCena.Text) - (Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(txt_Rabat.Text) * Convert.ToDecimal(0.01))).ToString();
                        txt_VpCenaAkcija.Text = (Convert.ToDecimal(txt_VpCenaRabat.Text) - (Convert.ToDecimal(txt_VpCenaRabat.Text) * Convert.ToDecimal(txt_Akcija.Text) * Convert.ToDecimal(0.01))).ToString();
                        txt_MpCenaRSD.Text = (Convert.ToDecimal(txt_VpCenaAkcija.Text) * Convert.ToDecimal(txtKurs.Text) * Convert.ToDecimal(1.25) * Convert.ToDecimal(1.2)).ToString();
                        txt_MpCena.Text = (Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(1.25) * Convert.ToDecimal(1.2)).ToString();
                        Kalkulacije.InsertKalkulatorStavke kalk = new InsertKalkulatorStavke();
                        kalk.UpdKalkulatorStavke(Convert.ToInt32(txt_ID.Text), Convert.ToInt32(txt_mpSifra.Text), Convert.ToInt32(combo_Zaglavlje.SelectedValue), Convert.ToDecimal(txt_Trosak.Text), Convert.ToDecimal(txt_Avans.Text), Convert.ToDecimal(txt_Provizija.Text), Convert.ToDecimal(txt_Transport.Text), VCena, Convert.ToDecimal(txt_Rabat.Text), marza2, Convert.ToDecimal(txt_Akcija.Text), Convert.ToDecimal(txt_MpCena), txtWebsSifra.Text, ProgramID, KolekcijaID, Convert.ToDecimal(txtMarzaRefer.Text), Convert.ToDecimal(txtPragMarze.Text));


                        //ROW UPDATE
                        //txt_mpSifra.Text = row.Cells[4].Value.ToString();
                        //txt_ID.Text = row.Cells[0].Value.ToString();

                        //combo_Zaglavlje.SelectedValue = row.Cells[6].Value.ToString();
                        //txtWebsSifra.Text = row.Cells[1].Value.ToString();
                        //txt_Trosak.Text = row.Cells[8].Value.ToString();
                        //txt_Avans.Text = row.Cells[9].Value.ToString();
                        //txt_Provizija.Text = row.Cells[10].Value.ToString();
                        // txt_Transport.Text = row.Cells[11].Value.ToString();

                        if (chkDevizni.Checked == true)
                        {
                            row.Cells[16].Value = VPCENASaAkcijom.ToString();
                            row.Cells[15].Value = MarzaNova.ToString();
                            row.Cells[14].Value = txt_Akcija.Text.ToString();
                            row.Cells[13].Value = VPCENASaRabatom.ToString();
                            row.Cells[12].Value = MarzaNova2.ToString();
                            row.Cells[11].Value = txt_Rabat.Text.ToString();
                            // row.Cells[16].Value = MCena.ToString();
                        }
                        else
                        {
                            row.Cells[10].Value = VCena.ToString();
                            row.Cells[11].Value = txt_Rabat.Text;
                            row.Cells[13].Value = MarzaNova.ToString();
                            row.Cells[12].Value = txt_Akcija.Text;
                            row.Cells[16].Value = txt_MpCena.Text;
                        }

                        /* Bilo
                        row.Cells[13].Value = txt_Rabat.Text;
                        row.Cells[14].Value = marza2.ToString();
                        row.Cells[15].Value = txt_Akcija.Text;
                        row.Cells[12].Value = VCena.ToString();
                        row.Cells[16].Value = txt_MpCena.Text;
                        */
                        // txtMarzaRefer.Text = row.Cells[18].Value.ToString();
                        // txtPragMarze.Text = row.Cells[19].Value.ToString();

                        // txt_MarzaPocetnaIznos
                        //  Trosak();
                        // Cene();
                        // IzracunajUkupnoIProsek();
                    }
                }
            }
            catch { }






            //End of promena troska














            /*


            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da za sve stavke želite da primenite rabat od " + txt_Rabat.Text + "% ?", "Rabat",
               MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        //Reseno avans
                        if (row.Selected == true)
                        {

                            //Promenom troska se menja Marza

                            var query = "Select * from KalkulatorStavke Where ID=" + Convert.ToInt32(row.Cells[0].Value.ToString());

                            //  var query = "Select * from KalkulatorStavke Where IDKalkulator="+Convert.ToInt32(combo_Zaglavlje.SelectedValue);
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
                                decimal rabat = Convert.ToDecimal(dr["Rabat"].ToString());
                                decimal marza;
                                //= Convert.ToDecimal(dr["Marza"].ToString());
                                decimal akcija = Convert.ToDecimal(dr["Akcija"].ToString());

                                decimal VCena = Convert.ToDecimal(dr["VPCena"].ToString());
                                trosak = trosak / Convert.ToDecimal(txtKurs.Text);
                                decimal MCena;
                                decimal Razlika = VCena - trosak; //500
                                decimal Av = (avans * VCena * Convert.ToDecimal(0.01));
                                decimal Tr = (transport * VCena * Convert.ToDecimal(0.01));
                                decimal Pr = (provizija * VCena * Convert.ToDecimal(0.01));
                                decimal UKTrosak = trosak + (Av + Tr + Pr); // Troskovi sa uracunatim dodatnim troskovima oduzimam
                                                                            // decimal NoviRabat = VCena *                                             //Sada od dobijenog rezultata oduzmem Akciju i Novi RAbat
                                                                            //  decimal Razlika3 = Razlika2 - (akcija * Razlika2) - (Convert.ToDecimal(txt_Rabat.Text) * Razlika2);
                                decimal ARProcenatAkcijeIRabata = avans + transport + provizija + Convert.ToDecimal(txt_Rabat.Text) + Convert.ToDecimal(txt_Akcija.Text);
                                decimal Razlika3 = VCena - (VCena * ARProcenatAkcijeIRabata * Convert.ToDecimal(0.01));
                                marza = (Razlika3 / trosak) * 100; // Procenat Marze
                                                                   // MessageBox.Show("Akcija " + txt_Akcija.Text + " RABAT " + txt_Rabat + " MARZA " + marza);
                                MCena = VCena + (VCena * 25 / 100) * Convert.ToDecimal(1.2);
                                //Maloprodajna cena dedam 25 % i pomnozim sa 20% za PDV
                                Kalkulacije.InsertKalkulatorStavke kalk = new InsertKalkulatorStavke();
                                string wsifra = dr["WSifra"].ToString();
                                txtMarzaRefer.Text = dr["MarzaRef"].ToString();
                                int programid = Convert.ToInt32(dr["ProgramID"].ToString());
                                int kolekcijaid = Convert.ToInt32(dr["kolekcijaID"].ToString());
                                // kalk.UpdKalkulatorStavke(id, mpSifre, idKalk, trosak * Convert.ToDecimal(txtKurs.Text), avans, provizija, transport, VCena, Convert.ToDecimal(txt_Rabat.Text), MarzaNova, akcija, MCena, wsifra, programid, kolekcijaid);
                               
                                kalk.UpdKalkulatorStavke(id, mpSifre, idKalk, trosak * Convert.ToDecimal(txtKurs.Text), avans, provizija, transport, VCena, Convert.ToDecimal(txt_Rabat.Text), marza, Convert.ToDecimal(txt_Akcija.Text), MCena, wsifra, programid, kolekcijaid);
                            }
                            conn.Close();
                        }
                    }

                    RefreshGV();
                }
                catch { }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
            */
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da za sve stavke želite da primenite akciju od " + txt_Akcija.Text + "% ?", "Akcija",
          MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            int ProgramID = 0;
            int ProgramSelektovan = 0;
            int KolekcijaSelektovana = 0;
            int KolekcijaID = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected == true)
                {
                    ProgramID = Convert.ToInt32(row.Cells[0].Value.ToString());
                    ProgramSelektovan = 1;
                }
            }

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected == true)
                {
                    KolekcijaID = Convert.ToInt32(row.Cells[0].Value.ToString());
                    KolekcijaSelektovana = 1;
                }
            }

            if (KolekcijaSelektovana == 0)
            {
                MessageBox.Show("Morate selektovati kolekciju");
                return;
            }

            if (ProgramSelektovan == 0)
            {
                MessageBox.Show("Morate selektovati program");
                return;
            }



            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Selected == true)
                        {
                            //Resen avans
                            var query = "Select * from KalkulatorStavke Where ID=" + Convert.ToInt32(row.Cells[0].Value.ToString());
                            //  var query = "Select * from KalkulatorStavke Where IDKalkulator="+Convert.ToInt32(combo_Zaglavlje.SelectedValue);
                            SqlConnection conn = new SqlConnection(connect);
                            conn.Open();
                            SqlCommand cmd = new SqlCommand(query, conn);
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                int id = Convert.ToInt32(dr["ID"].ToString());
                                string wSifra = dr["WSifra"].ToString();
                                int mpSifre = Convert.ToInt32(dr["MPSifra"].ToString());
                                int idKalk = Convert.ToInt32(dr["IDKalkulator"].ToString());
                                decimal trosak = Convert.ToDecimal(dr["TrosakProizvodnje"].ToString());
                                decimal avans = Convert.ToDecimal(dr["Avans"].ToString());
                                decimal provizija = Convert.ToDecimal(dr["Provizija"].ToString());
                                decimal transport = Convert.ToDecimal(dr["Transport"].ToString());
                                decimal rabat = Convert.ToDecimal(dr["Transport"].ToString());
                                decimal marza = Convert.ToDecimal(dr["Marza"].ToString()); ;
                                //= Convert.ToDecimal(dr["Marza"].ToString());
                                decimal akcija = Convert.ToDecimal(txt_Akcija.Text);
                                decimal mpCena = Convert.ToDecimal(dr["MpCena"].ToString());
                                decimal marzaRef = Convert.ToDecimal(dr["MpCena"].ToString());
                                decimal PragMarze = Convert.ToDecimal(dr["PragMarze"].ToString());
                                decimal VCena = Convert.ToDecimal(dr["VPCena"].ToString());


                                decimal marza1 = Convert.ToDecimal(marza) - (Convert.ToDecimal(avans) + Convert.ToDecimal(provizija) + Convert.ToDecimal(transport));
                                decimal marza2 = ((VCena / (Convert.ToDecimal(trosak) / Convert.ToDecimal(txtKurs.Text))) - 1) * 100;


                                decimal VPCENASaRabatom = VCena - (VCena * (Convert.ToDecimal(rabat) * Convert.ToDecimal(0.01)));
                                decimal MarzaNova2 = ((VPCENASaRabatom / (Convert.ToDecimal(trosak) / Convert.ToDecimal(txtKurs.Text))) - 1) * 100;

                             //   decimal VPCENASaAkcijom = VPCENASaRabatom - (VPCENASaRabatom * (Convert.ToDecimal(akcija) * Convert.ToDecimal(0.01)));
                             //   decimal MarzaNova = ((VPCENASaAkcijom / (Convert.ToDecimal(trosak) / Convert.ToDecimal(txtKurs.Text))) - 1) * 100;

                                decimal VPCenaRabat = (Convert.ToDecimal(VCena) - (Convert.ToDecimal(VCena) * Convert.ToDecimal(rabat) * Convert.ToDecimal(0.01)));
                                decimal VPCenaAkcija = 0;
                                decimal VPCENASaAkcijom = 0;
                                if (chkDevizni.Checked == true)
                                {
                                    VPCenaAkcija = (Convert.ToDecimal(VPCenaRabat) - (Convert.ToDecimal(rabat) * Convert.ToDecimal(akcija) * Convert.ToDecimal(0.01)));
                                    VPCENASaAkcijom = VPCENASaRabatom - (VPCENASaRabatom * (Convert.ToDecimal(akcija) * Convert.ToDecimal(0.01)));
                                }
                                else
                                {
                                    VPCenaAkcija = VCena - (VCena * (Convert.ToDecimal(akcija) * Convert.ToDecimal(0.01)));
                                    VPCENASaAkcijom = VCena - (VCena * (Convert.ToDecimal(akcija) * Convert.ToDecimal(0.01)));

                                }
                                decimal MarzaNova = ((VPCENASaAkcijom / (Convert.ToDecimal(trosak) / Convert.ToDecimal(txtKurs.Text))) - 1) * 100;
                                // txt_MpCenaRSD.Text = (Convert.ToDecimal(VPCenaAkcija) * Convert.ToDecimal(txtKurs.Text) * Convert.ToDecimal(1.25) * Convert.ToDecimal(1.2)).ToString();
                                //  decimal MCena = (Convert.ToDecimal(VPCenaAkcija) * Convert.ToDecimal(1.25) * Convert.ToDecimal(1.2));
                                decimal MCena = (Convert.ToDecimal(VPCENASaAkcijom) * Convert.ToDecimal(1.25) * Convert.ToDecimal(1.2));


                                int programid = Convert.ToInt32(dr["ProgramID"].ToString());
                                int kolekcijaid = Convert.ToInt32(dr["kolekcijaID"].ToString());
                                Kalkulacije.InsertKalkulatorStavke kalk = new InsertKalkulatorStavke();
                                kalk.UpdKalkulatorStavke(id, mpSifre, idKalk, trosak, avans, provizija, transport, VCena, Convert.ToDecimal(txt_Rabat.Text), MarzaNova, akcija, MCena, wSifra, programid, kolekcijaid, Convert.ToDecimal(txtMarzaRefer.Text), PragMarze);

                                //ROW UPDATE
                                //txt_mpSifra.Text = row.Cells[4].Value.ToString();
                                //txt_ID.Text = row.Cells[0].Value.ToString();

                                //combo_Zaglavlje.SelectedValue = row.Cells[6].Value.ToString();
                                //txtWebsSifra.Text = row.Cells[1].Value.ToString();
                                //txt_Trosak.Text = row.Cells[8].Value.ToString();
                                //txt_Avans.Text = row.Cells[9].Value.ToString();
                                //txt_Provizija.Text = row.Cells[10].Value.ToString();
                                // txt_Transport.Text = row.Cells[11].Value.ToString();

                                if (chkDevizni.Checked == true)
                                {
                                    row.Cells[16].Value = VPCENASaAkcijom.ToString();
                                    row.Cells[15].Value = MarzaNova.ToString();
                                    row.Cells[14].Value = txt_Akcija.Text.ToString();
                                    row.Cells[13].Value = VPCENASaRabatom.ToString();
                                    row.Cells[12].Value = MarzaNova2.ToString();
                                    row.Cells[11].Value = txt_Rabat.Text.ToString();
                                    // row.Cells[16].Value = MCena.ToString();
                                }
                                else
                                {
                                    row.Cells[10].Value = VCena.ToString();
                                    row.Cells[11].Value = rabat.ToString();
                                    row.Cells[13].Value = MarzaNova.ToString();
                                    row.Cells[12].Value = akcija.ToString();
                                    row.Cells[16].Value = MCena.ToString();
                                }
                                // txtMarzaRefer.Text = row.Cells[18].Value.ToString();
                                // txtPragMarze.Text = row.Cells[19].Value.ToString();


                            }
                            conn.Close();
                        }
                    }

                   // refreshGGGG();
                }
                catch { }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da za sve stavke želite da primenite rabat od " + txt_Rabat.Text + "% ?", "Rabat",
             MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Selected == true)
                        {
                            //Resen avans
                            var query = "Select * from KalkulatorStavke Where ID=" + Convert.ToInt32(row.Cells[0].Value.ToString());
                            //  var query = "Select * from KalkulatorStavke Where IDKalkulator="+Convert.ToInt32(combo_Zaglavlje.SelectedValue);
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
                                decimal rabat = Convert.ToDecimal(dr["Rabat"].ToString());
                                decimal marza = Convert.ToDecimal(dr["Marza"].ToString()); ;
                                //= Convert.ToDecimal(dr["Marza"].ToString());
                                decimal akcija = Convert.ToDecimal(dr["Akcija"].ToString());

                                decimal VCena = Convert.ToDecimal(dr["VPCena"].ToString());
                                decimal MarzaNova = marza - (Convert.ToDecimal(txt_Avans.Text) - avans);
                                decimal MCena = VCena + (VCena * 25 / 100) * Convert.ToDecimal(1.2);
                                //Maloprodajna cena dedam 25 % i pomnozim sa 20% za PDV
                                Kalkulacije.InsertKalkulatorStavke kalk = new InsertKalkulatorStavke();
                                string wsifra = dr["WSifra"].ToString();
                                txtMarzaRefer.Text = dr["MarzaRef"].ToString();
                                txtPragMarze.Text = dr["PragMarze"].ToString();
                                int programid = Convert.ToInt32(dr["ProgramID"].ToString());
                                int kolekcijaid = Convert.ToInt32(dr["kolekcijaID"].ToString());
                                // kalk.UpdKalkulatorStavke(id, mpSifre, idKalk, trosak * Convert.ToDecimal(txtKurs.Text), avans, provizija, transport, VCena, Convert.ToDecimal(txt_Rabat.Text), MarzaNova, akcija, MCena, wsifra, programid, kolekcijaid);
                                //  kalk.UpdKalkulatorStavke(id, mpSifre, idKalk, trosak * Convert.ToDecimal(txtKurs.Text), avans, provizija, transport, VCena, rabat, MarzaNova, Convert.ToDecimal(txt_Akcija.Text), MCena, wsifra, programid, kolekcijaid);

                                kalk.UpdKalkulatorStavke(id, mpSifre, idKalk, trosak * Convert.ToDecimal(txtKurs.Text), Convert.ToDecimal(txt_Avans.Text), provizija, transport, VCena, rabat, MarzaNova, akcija, MCena, wsifra, programid, kolekcijaid, Convert.ToDecimal(txtMarzaRefer.Text), Convert.ToDecimal(txtPragMarze.Text));
                            }
                            conn.Close();
                        }
                    }

                    refreshGGGG();
                }
                catch { }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da za sve stavke želite da primenite rabat od " + txt_Rabat.Text + "% ?", "Rabat",
             MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Selected == true)
                        {
                            //Resen avans
                            var query = "Select * from KalkulatorStavke Where ID=" + Convert.ToInt32(row.Cells[0].Value.ToString());
                            //  var query = "Select * from KalkulatorStavke Where IDKalkulator="+Convert.ToInt32(combo_Zaglavlje.SelectedValue);
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
                                decimal rabat = Convert.ToDecimal(dr["Rabat"].ToString());
                                decimal marza = Convert.ToDecimal(dr["Marza"].ToString()); ;
                                //= Convert.ToDecimal(dr["Marza"].ToString());
                                decimal akcija = Convert.ToDecimal(dr["Akcija"].ToString());

                                decimal VCena = Convert.ToDecimal(dr["VPCena"].ToString());
                                decimal MarzaNova = marza - (Convert.ToDecimal(txt_Provizija.Text) - provizija);

                                // MessageBox.Show("Akcija " + txt_Akcija.Text + " RABAT " + txt_Rabat + " MARZA " + marza);
                                decimal MCena = VCena + (VCena * 25 / 100) * Convert.ToDecimal(1.2);
                                //Maloprodajna cena dedam 25 % i pomnozim sa 20% za PDV
                                Kalkulacije.InsertKalkulatorStavke kalk = new InsertKalkulatorStavke();
                                string wsifra = dr["WSifra"].ToString();
                                txtMarzaRefer.Text = dr["MarzaRef"].ToString();
                                txtPragMarze.Text = dr["PragMArze"].ToString();
                                int programid = Convert.ToInt32(dr["ProgramID"].ToString());
                                int kolekcijaid = Convert.ToInt32(dr["kolekcijaID"].ToString());

                                kalk.UpdKalkulatorStavke(id, mpSifre, idKalk, trosak * Convert.ToDecimal(txtKurs.Text), avans, Convert.ToDecimal(txt_Provizija.Text), transport, VCena, rabat, MarzaNova, akcija, MCena, wsifra, programid, kolekcijaid, Convert.ToDecimal(txtMarzaRefer.Text), Convert.ToDecimal(txtPragMarze.Text));
                            }
                            conn.Close();
                        }
                    }

                    refreshGGGG();
                }
                catch { }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da za sve stavke želite da primenite rabat od " + txt_Rabat.Text + "% ?", "Rabat",
             MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Selected == true)
                        {
                            //Resen avans
                            var query = "Select * from KalkulatorStavke Where ID=" + Convert.ToInt32(row.Cells[0].Value.ToString());
                            //  var query = "Select * from KalkulatorStavke Where IDKalkulator="+Convert.ToInt32(combo_Zaglavlje.SelectedValue);
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
                                decimal rabat = Convert.ToDecimal(dr["Rabat"].ToString());
                                decimal marza = Convert.ToDecimal(dr["Marza"].ToString()); ;
                                //= Convert.ToDecimal(dr["Marza"].ToString());
                                decimal akcija = Convert.ToDecimal(dr["Akcija"].ToString());

                                decimal VCena = Convert.ToDecimal(dr["VPCena"].ToString());
                                decimal MarzaNova = marza - (Convert.ToDecimal(txt_Transport.Text) - transport);

                                decimal MCena = VCena + (VCena * 25 / 100) * Convert.ToDecimal(1.2);
                                //Maloprodajna cena dedam 25 % i pomnozim sa 20% za PDV
                                Kalkulacije.InsertKalkulatorStavke kalk = new InsertKalkulatorStavke();
                                string wsifra = dr["WSifra"].ToString();
                                txtMarzaRefer.Text = dr["MarzaRef"].ToString();
                                txtPragMarze.Text = dr["PragMarze"].ToString();
                                int programid = Convert.ToInt32(dr["ProgramID"].ToString());
                                int kolekcijaid = Convert.ToInt32(dr["kolekcijaID"].ToString());

                                // kalk.UpdKalkulatorStavke(id, mpSifre, idKalk, trosak * Convert.ToDecimal(txtKurs.Text), avans, Convert.ToDecimal(txt_Provizija.Text), transport, VCena, rabat, MarzaNova, akcija, MCena, wsifra, programid, kolekcijaid);


                                kalk.UpdKalkulatorStavke(id, mpSifre, idKalk, trosak * Convert.ToDecimal(txtKurs.Text), avans, provizija, Convert.ToDecimal(txt_Transport.Text), VCena, rabat, MarzaNova, akcija, MCena, wsifra, programid, kolekcijaid, Convert.ToDecimal(txtMarzaRefer.Text), Convert.ToDecimal(txtPragMarze.Text));
                            }
                            conn.Close();
                        }
                    }

                    refreshGGGG();
                }
                catch { }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBezDecimala.Checked == true)
            {
                txt_VpCena.Text = Math.Round(Convert.ToDecimal(txt_VpCena.Text), 0).ToString();
                txt_VpCenaRabat.Text = Math.Round(Convert.ToDecimal(txt_VpCenaRabat.Text), 0).ToString();
                txt_MpCenaRSD.Text = Math.Round(Convert.ToDecimal(txt_MpCenaRSD.Text), 0).ToString();
                txt_VpCenaRSD.Text = Math.Round(Convert.ToDecimal(txt_VpCenaRSD.Text), 0).ToString();
                txt_VpCenaAkcija.Text = Math.Round(Convert.ToDecimal(txt_VpCenaAkcija.Text), 0).ToString();
                txt_MpCena.Text = Math.Round(Convert.ToDecimal(txt_MpCena.Text), 0).ToString();
                txt_MarzaPArtner.Text = Math.Round(Convert.ToDecimal(txt_MarzaPArtner.Text), 0).ToString();
                txt_Marza.Text = Math.Round(Convert.ToDecimal(txt_Marza.Text), 0).ToString();
                txt_MarzaPocetna.Text = Math.Round(Convert.ToDecimal(txt_MarzaPocetna.Text), 0).ToString();
                txt_MarzaPArtnerIznos.Text = Math.Round(Convert.ToDecimal(txt_MarzaPArtnerIznos.Text), 0).ToString();
                txt_MarzaIznos.Text = Math.Round(Convert.ToDecimal(txt_MarzaIznos.Text), 0).ToString();
                txt_MarzaPocetnaIznos.Text = Math.Round(Convert.ToDecimal(txt_MarzaPocetnaIznos.Text), 0).ToString();

            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            int ProgramID = 0;
            int ProgramSelektovan = 0;
            int KolekcijaSelektovana = 0;
            int KolekcijaID = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected == true)
                {
                    ProgramID = Convert.ToInt32(row.Cells[0].Value.ToString());
                    ProgramSelektovan = 1;
                }
            }

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected == true)
                {
                    KolekcijaID = Convert.ToInt32(row.Cells[0].Value.ToString());
                    KolekcijaSelektovana = 1;
                }
            }

            if (KolekcijaSelektovana == 0)
            {
                MessageBox.Show("Morate selektovati kolekciju");
                return;
            }

            if (ProgramSelektovan == 0)
            {
                MessageBox.Show("Morate selektovati program");
                return;
            }
            //Promenom troska menja se samo Marza

            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        //PANTA
                        if (chkDevizni.Checked == true)
                        {
                            txt_ID.Text = row.Cells[0].Value.ToString();
                            txt_mpSifra.Text = row.Cells[3].Value.ToString();
                            txtWebsSifra.Text = row.Cells[4].Value.ToString();
                            txt_Trosak.Text = Convert.ToDecimal(row.Cells[5].Value.ToString()).ToString();
                            txt_MarzaPocetna.Text = Convert.ToDecimal(row.Cells[6].Value.ToString()).ToString();
                            //combo_Zaglavlje.SelectedValue = row.Cells[6].Value.ToString();
                            txtMarzaRefer.Text = Convert.ToDecimal(row.Cells[7].Value.ToString()).ToString();
                            txtPragMarze.Text = Convert.ToDecimal(row.Cells[8].Value.ToString()).ToString();
                            //txt_Trosak.Text = row.Cells[8].Value.ToString();

                            txt_Avans.Text = "0";
                            txt_Provizija.Text = "0";
                            txt_Transport.Text = "0";

                            txt_VpCena.Text = Convert.ToDecimal(row.Cells[10].Value.ToString()).ToString();
                            txt_VpCenaRSD.Text = (Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(txtKurs.Text)).ToString();
                            txt_Rabat.Text = Convert.ToDecimal(row.Cells[11].Value.ToString()).ToString();
                            txt_MarzaPArtner.Text = Convert.ToDecimal(row.Cells[11].Value.ToString()).ToString();
                            txt_VpCenaRabat.Text = Convert.ToDecimal(row.Cells[13].Value.ToString()).ToString();
                            txt_Akcija.Text = Convert.ToDecimal(row.Cells[14].Value.ToString()).ToString();
                            txt_Marza.Text = Convert.ToDecimal(row.Cells[15].Value.ToString()).ToString();
                            txt_VpCenaAkcija.Text = Convert.ToDecimal(row.Cells[16].Value).ToString();
                            txt_VpCenaAkcijaRSD.Text = (Convert.ToDecimal(row.Cells[16].Value) * Convert.ToDecimal(txtKurs.Text)).ToString();
                            label32.Visible = false;
                            txt_MpCenaRSD.Visible = false;
                            label12.Visible = false;
                            txt_MpCena.Visible = false;
                            label30.Visible = true;
                            txt_VpCenaRabat.Visible = true;

                            // txt_VpCenaAkcija.Text = (Convert.ToDecimal(txt_VpCenaRabat.Text) - (Convert.ToDecimal(txt_VpCenaRabat.Text) * Convert.ToDecimal(txt_Akcija.Text) * Convert.ToDecimal(0.01))).ToString();
                            // txt_MpCena.Text = row.Cells[16].Value.ToString();

                        }
                        else
                        {
                            txt_ID.Text = row.Cells[0].Value.ToString();
                            txt_mpSifra.Text = row.Cells[3].Value.ToString();
                            txtWebsSifra.Text = row.Cells[4].Value.ToString();
                            txt_Trosak.Text = Convert.ToDecimal(row.Cells[5].Value.ToString()).ToString();
                            txt_MarzaPocetna.Text = Convert.ToDecimal(row.Cells[6].Value.ToString()).ToString();
                            txtMarzaRefer.Text = Convert.ToDecimal(row.Cells[7].Value.ToString()).ToString();
                            txtPragMarze.Text = Convert.ToDecimal(row.Cells[8].Value.ToString()).ToString();
                            //  combo_Zaglavlje.SelectedValue = row.Cells[6].Value.ToString();
                            txt_Avans.Text = "0";
                            txt_Provizija.Text = "0";
                            txt_Transport.Text = "0";
                            //txt_Trosak.Text = row.Cells[8].Value.ToString();
                            // txt_Avans.Text = row.Cells[9].Value.ToString();
                            // txt_Provizija.Text = row.Cells[10].Value.ToString();
                            // txt_Transport.Text = row.Cells[11].Value.ToString();

                            txt_VpCena.Text = Convert.ToDecimal((Convert.ToDecimal(row.Cells[10].Value.ToString()) / Convert.ToDecimal(txtKurs.Text)).ToString()).ToString();
                            txt_VpCenaRSD.Text = ((Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(txtKurs.Text)).ToString());

                            txt_Rabat.Text = Convert.ToDecimal(row.Cells[11].Value.ToString()).ToString();
                            txt_Akcija.Text = Convert.ToDecimal(row.Cells[12].Value.ToString()).ToString();
                            txt_Marza.Text = Convert.ToDecimal(row.Cells[13].Value.ToString()).ToString();
                            txt_VpCenaAkcija.Text = row.Cells[15].Value.ToString();
                            txt_VpCenaAkcijaRSD.Text = (Convert.ToDecimal(row.Cells[15].Value) * Convert.ToDecimal(txtKurs.Text)).ToString();
                            txt_MpCena.Text = Convert.ToDecimal((Convert.ToDecimal(row.Cells[16].Value.ToString()) / Convert.ToDecimal(txtKurs.Text)).ToString()).ToString();
                            txt_MpCenaRSD.Text = Convert.ToDecimal((Convert.ToDecimal(txt_MpCena.Text) * Convert.ToDecimal(txtKurs.Text)).ToString()).ToString();
                            label32.Visible = true;
                            txt_MpCenaRSD.Visible = true;
                            label12.Visible = true;
                            txt_MpCena.Visible = true;
                            label30.Visible = false;
                            txt_VpCenaRabat.Visible = false;

                            txt_VpCenaRSD.Text = ((Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(txtKurs.Text)).ToString());

                        }

                        decimal VCena = Convert.ToDecimal(txt_VpCena.Text);
                        decimal marza1 = Convert.ToDecimal(txt_Marza.Text) - (Convert.ToDecimal(txt_Avans.Text) + Convert.ToDecimal(txt_Provizija.Text) + Convert.ToDecimal(txt_Transport.Text));
                        decimal marza2 = ((VCena / (Convert.ToDecimal(txt_Trosak.Text) / Convert.ToDecimal(txtKurs.Text))) - 1) * 100;
                        marza2 = Convert.ToDecimal(txtMarzaRefer.Text);
                        //Izracun 
                        VCena = ((marza2 / 100) + 1) * (Convert.ToDecimal(txt_Trosak.Text) / Convert.ToDecimal(txtKurs.Text));
                        txt_MarzaPocetna.Text = marza2.ToString();


                        decimal VPCENASaRabatom = VCena - (VCena * (Convert.ToDecimal(txt_Rabat.Text) * Convert.ToDecimal(0.01)));
                        // decimal 
                        // decimal MarzaNova = marza - (Convert.ToDecimal(txt_Rabat.Text)- rabat);
                        decimal MarzaNova2 = ((VPCENASaRabatom / (Convert.ToDecimal(txt_Trosak.Text) / Convert.ToDecimal(txtKurs.Text))) - 1) * 100;

                        txt_MarzaPArtner.Text = MarzaNova2.ToString();
                        txt_MarzaPArtnerIznos.Text = (VPCENASaRabatom - (Convert.ToDecimal(txt_Trosak.Text) / Convert.ToDecimal(txtKurs.Text))).ToString();
                        decimal VPCENASaAkcijom = VPCENASaRabatom - (VPCENASaRabatom * (Convert.ToDecimal(txt_Akcija.Text) * Convert.ToDecimal(0.01)));
                        // decimal 
                        // decimal MarzaNova = marza - (Convert.ToDecimal(txt_Rabat.Text)- rabat);
                        decimal MarzaNova = ((VPCENASaAkcijom / (Convert.ToDecimal(txt_Trosak.Text) / Convert.ToDecimal(txtKurs.Text))) - 1) * 100;

                        txt_Marza.Text = MarzaNova.ToString();
                        txt_MarzaIznos.Text = (VPCENASaAkcijom - (Convert.ToDecimal(txt_Trosak.Text) / Convert.ToDecimal(txtKurs.Text))).ToString();


                        txt_MarzaPocetnaIznos.Text = (VCena - (Convert.ToDecimal(txt_Trosak.Text) / Convert.ToDecimal(txtKurs.Text))).ToString();


                        txt_VpCenaRSD.Text = (Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(txtKurs.Text)).ToString();
                        txt_VpCenaRabat.Text = (Convert.ToDecimal(txt_VpCena.Text) - (Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(txt_Rabat.Text) * Convert.ToDecimal(0.01))).ToString();
                        txt_VpCenaAkcija.Text = (Convert.ToDecimal(txt_VpCenaRabat.Text) - (Convert.ToDecimal(txt_VpCenaRabat.Text) * Convert.ToDecimal(txt_Akcija.Text) * Convert.ToDecimal(0.01))).ToString();
                        txt_MpCenaRSD.Text = (Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(txtKurs.Text) * Convert.ToDecimal(1.25) * Convert.ToDecimal(1.2)).ToString();
                        txt_MpCena.Text = (Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(1.25) * Convert.ToDecimal(1.2)).ToString();



                        Kalkulacije.InsertKalkulatorStavke kalk = new InsertKalkulatorStavke();

                        // MessageBox.Show("Nova marza" + txtMarzaRefer.Text + "ID" + txt_ID);
                        kalk.UpdKalkulatorStavke(Convert.ToInt32(txt_ID.Text), Convert.ToInt32(txt_mpSifra.Text), Convert.ToInt32(combo_Zaglavlje.SelectedValue), Convert.ToDecimal(txt_Trosak.Text),
                            Convert.ToDecimal(txt_Avans.Text), Convert.ToDecimal(txt_Provizija.Text), Convert.ToDecimal(txt_Transport.Text), Convert.ToDecimal(VCena),
                            Convert.ToDecimal(txt_Rabat.Text), Convert.ToDecimal(txtMarzaRefer.Text), Convert.ToDecimal(txt_Akcija.Text), Convert.ToDecimal(txt_MpCena.Text), txtWebsSifra.Text, ProgramID, KolekcijaID, Convert.ToDecimal(txtMarzaRefer.Text), Convert.ToDecimal(txtPragMarze.Text));
                        if (chkDevizni.Checked == true)
                        {
                            row.Cells[10].Value = VCena.ToString();
                            row.Cells[11].Value = Convert.ToDecimal(txt_Rabat.Text);
                            row.Cells[15].Value = MarzaNova.ToString(); //Marza akcija
                            row.Cells[14].Value = Convert.ToDecimal(txt_Akcija.Text);
                            row.Cells[7].Value = txtMarzaRefer.Text.ToString();
                        }
                        else
                        {
                            row.Cells[10].Value = VCena.ToString();
                            row.Cells[11].Value = Convert.ToDecimal(txt_Rabat.Text);
                            row.Cells[13].Value = MarzaNova.ToString();
                            row.Cells[12].Value = Convert.ToDecimal(txt_Akcija.Text);
                            row.Cells[16].Value = Convert.ToDecimal(txt_MpCena.Text);
                        }


                      

                    }
                }
            }
            catch { }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            frmPregledEURRSD per = new frmPregledEURRSD(kalkulator);
            per.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void combo_Zaglavlje_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            int ProgramID = 0;
            int ProgramSelektovan = 0;
            int KolekcijaSelektovana = 0;
            int KolekcijaID = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected == true)
                {
                    ProgramID = Convert.ToInt32(row.Cells[0].Value.ToString());
                    ProgramSelektovan = 1;
                }
            }

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected == true)
                {
                    KolekcijaID = Convert.ToInt32(row.Cells[0].Value.ToString());
                    KolekcijaSelektovana = 1;
                }
            }

            if (KolekcijaSelektovana == 0)
            {
                MessageBox.Show("Morate selektovati kolekciju");
                return;
            }

            if (ProgramSelektovan == 0)
            {
                MessageBox.Show("Morate selektovati program");
                return;
            }



            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    if (chkDevizni.Checked == true)
                    {
                        txt_ID.Text = row.Cells[0].Value.ToString();
                        txt_mpSifra.Text = row.Cells[3].Value.ToString();
                        txtWebsSifra.Text = row.Cells[4].Value.ToString();
                        txt_Trosak.Text = Convert.ToDecimal(row.Cells[5].Value.ToString()).ToString();
                        txt_MarzaPocetna.Text = Convert.ToDecimal(row.Cells[6].Value.ToString()).ToString();
                        //combo_Zaglavlje.SelectedValue = row.Cells[6].Value.ToString();
                        txtMarzaRefer.Text = Convert.ToDecimal(row.Cells[7].Value.ToString()).ToString();
                        txtPragMarze.Text = Convert.ToDecimal(row.Cells[8].Value.ToString()).ToString();
                        //txt_Trosak.Text = row.Cells[8].Value.ToString();

                        txt_Avans.Text = "0";
                        txt_Provizija.Text = "0";
                        txt_Transport.Text = "0";

                        txt_VpCena.Text = Convert.ToDecimal(row.Cells[10].Value.ToString()).ToString();
                        txt_Rabat.Text = Convert.ToDecimal(row.Cells[11].Value.ToString()).ToString();
                        txt_MarzaPArtner.Text = Convert.ToDecimal(row.Cells[11].Value.ToString()).ToString();
                        txt_VpCenaRabat.Text = Convert.ToDecimal(row.Cells[13].Value.ToString()).ToString();
                        txt_Akcija.Text = Convert.ToDecimal(row.Cells[14].Value.ToString()).ToString();
                        txt_Marza.Text = Convert.ToDecimal(row.Cells[15].Value.ToString()).ToString();
                        txt_VpCenaAkcija.Text = Convert.ToDecimal(row.Cells[16].Value).ToString();
                        txt_VpCenaAkcijaRSD.Text = (Convert.ToDecimal(row.Cells[16].Value) * Convert.ToDecimal(txtKurs.Text)).ToString();
                        label32.Visible = false;
                        txt_MpCenaRSD.Visible = false;
                        label12.Visible = false;
                        txt_MpCena.Visible = false;
                        label30.Visible = true;
                        txt_VpCenaRabat.Visible = true;

                        // txt_VpCenaAkcija.Text = (Convert.ToDecimal(txt_VpCenaRabat.Text) - (Convert.ToDecimal(txt_VpCenaRabat.Text) * Convert.ToDecimal(txt_Akcija.Text) * Convert.ToDecimal(0.01))).ToString();
                        // txt_MpCena.Text = row.Cells[16].Value.ToString();

                    }
                    else
                    {
                        txt_ID.Text = row.Cells[0].Value.ToString();
                        txt_mpSifra.Text = row.Cells[3].Value.ToString();
                        txtWebsSifra.Text = row.Cells[4].Value.ToString();
                        txt_Trosak.Text = Convert.ToDecimal(row.Cells[5].Value.ToString()).ToString();
                        txt_MarzaPocetna.Text = Convert.ToDecimal(row.Cells[6].Value.ToString()).ToString();
                        txtMarzaRefer.Text = Convert.ToDecimal(row.Cells[7].Value.ToString()).ToString();
                        txtPragMarze.Text = Convert.ToDecimal(row.Cells[8].Value.ToString()).ToString();
                        //  combo_Zaglavlje.SelectedValue = row.Cells[6].Value.ToString();
                        txt_Avans.Text = "0";
                        txt_Provizija.Text = "0";
                        txt_Transport.Text = "0";
                        //txt_Trosak.Text = row.Cells[8].Value.ToString();
                        // txt_Avans.Text = row.Cells[9].Value.ToString();
                        // txt_Provizija.Text = row.Cells[10].Value.ToString();
                        // txt_Transport.Text = row.Cells[11].Value.ToString();

                        txt_VpCena.Text = Convert.ToDecimal((Convert.ToDecimal(row.Cells[10].Value.ToString()) / Convert.ToDecimal(txtKurs.Text)).ToString()).ToString();
                        txt_VpCenaRSD.Text = ((Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(txtKurs.Text)).ToString());

                        txt_Rabat.Text = Convert.ToDecimal(row.Cells[11].Value.ToString()).ToString();
                        txt_Akcija.Text = Convert.ToDecimal(row.Cells[12].Value.ToString()).ToString();
                        txt_Marza.Text = Convert.ToDecimal(row.Cells[13].Value.ToString()).ToString();
                        txt_VpCenaAkcija.Text = row.Cells[15].Value.ToString();
                        txt_VpCenaAkcijaRSD.Text = (Convert.ToDecimal(row.Cells[15].Value) * Convert.ToDecimal(txtKurs.Text)).ToString();
                        txt_MpCena.Text = Convert.ToDecimal((Convert.ToDecimal(row.Cells[16].Value.ToString()) / Convert.ToDecimal(txtKurs.Text)).ToString()).ToString();
                        txt_MpCenaRSD.Text = Convert.ToDecimal((Convert.ToDecimal(txt_MpCena.Text) * Convert.ToDecimal(txtKurs.Text)).ToString()).ToString();
                        label32.Visible = true;
                        txt_MpCenaRSD.Visible = true;
                        label12.Visible = true;
                        txt_MpCena.Visible = true;
                        label30.Visible = false;
                        txt_VpCenaRabat.Visible = false;

                        txt_VpCenaRSD.Text = ((Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(txtKurs.Text)).ToString());

                    }

                    txtAvansUK.Text = (Convert.ToDecimal(txt_Avans.Text) * Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(0.01)).ToString();
                    txtProvizijaUK.Text = (Convert.ToDecimal(txt_Provizija.Text) * Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(0.01)).ToString();
                    txtTransportUK.Text = (Convert.ToDecimal(txt_Transport.Text) * Convert.ToDecimal(txt_VpCena.Text) * Convert.ToDecimal(0.01)).ToString();
                    

                    decimal PocVPCENA = Convert.ToDecimal(txt_MpCenaRSD.Text) / (Convert.ToDecimal(1.25) * Convert.ToDecimal(1.2)) ;
                    PocVPCENA = PocVPCENA / Convert.ToDecimal(txtKurs.Text);

                    decimal VPCENASaRabatom = PocVPCENA - (PocVPCENA * (Convert.ToDecimal(txt_Rabat.Text) * Convert.ToDecimal(0.01)));
                    decimal MarzaNova2 = ((VPCENASaRabatom / (Convert.ToDecimal(txt_Trosak.Text) / Convert.ToDecimal(txtKurs.Text))) - 1) * 100;

                    decimal VPCenaAkcija = 0;
                    decimal VPCENASaAkcijom = 0;
                    if (chkDevizni.Checked == true)
                    {
                        VPCenaAkcija = (Convert.ToDecimal(VPCENASaRabatom) - (Convert.ToDecimal(txt_Rabat.Text) * Convert.ToDecimal(txt_Akcija.Text) * Convert.ToDecimal(0.01)));
                        VPCENASaAkcijom = VPCENASaRabatom - (VPCENASaRabatom * (Convert.ToDecimal(txt_Akcija.Text) * Convert.ToDecimal(0.01)));
                    }
                    else
                    {
                        VPCenaAkcija = PocVPCENA - (PocVPCENA * (Convert.ToDecimal(txt_Akcija.Text) * Convert.ToDecimal(0.01)));
                        VPCENASaAkcijom = PocVPCENA - (PocVPCENA * (Convert.ToDecimal(txt_Akcija.Text) * Convert.ToDecimal(0.01)));

                    }
                    decimal MarzaNova = ((VPCENASaAkcijom / (Convert.ToDecimal(txt_Trosak.Text) / Convert.ToDecimal(txtKurs.Text))) - 1) * 100;
                    // txt_MpCenaRSD.Text = (Convert.ToDecimal(VPCenaAkcija) * Convert.ToDecimal(txtKurs.Text) * Convert.ToDecimal(1.25) * Convert.ToDecimal(1.2)).ToString();
                    //  decimal MCena = (Convert.ToDecimal(VPCenaAkcija) * Convert.ToDecimal(1.25) * Convert.ToDecimal(1.2));
                    decimal MCena = (Convert.ToDecimal(VPCENASaAkcijom) * Convert.ToDecimal(1.25) * Convert.ToDecimal(1.2));




                    Kalkulacije.InsertKalkulatorStavke kalk = new InsertKalkulatorStavke();

                    // MessageBox.Show("Nova marza" + txtMarzaRefer.Text + "ID" + txt_ID);
                    kalk.UpdKalkulatorStavke(Convert.ToInt32(txt_ID.Text), Convert.ToInt32(txt_mpSifra.Text), Convert.ToInt32(combo_Zaglavlje.SelectedValue), Convert.ToDecimal(txt_Trosak.Text),
                        Convert.ToDecimal(txt_Avans.Text), Convert.ToDecimal(txt_Provizija.Text), Convert.ToDecimal(txt_Transport.Text), Convert.ToDecimal(PocVPCENA),
                        Convert.ToDecimal(txt_Rabat.Text), MarzaNova, Convert.ToDecimal(txt_Akcija.Text),Convert.ToDecimal(txt_MpCenaRSD.Text), txtWebsSifra.Text, ProgramID, KolekcijaID, Convert.ToDecimal(txtMarzaRefer.Text), Convert.ToDecimal(txtPragMarze.Text));

                    //ROW UPDATE
                    //txt_mpSifra.Text = row.Cells[4].Value.ToString();
                    //txt_ID.Text = row.Cells[0].Value.ToString();

                    //combo_Zaglavlje.SelectedValue = row.Cells[6].Value.ToString();
                    //txtWebsSifra.Text = row.Cells[1].Value.ToString();
                    //txt_Trosak.Text = row.Cells[8].Value.ToString();
                    //txt_Avans.Text = row.Cells[9].Value.ToString();
                    //txt_Provizija.Text = row.Cells[10].Value.ToString();
                    // txt_Transport.Text = row.Cells[11].Value.ToString();
                    if (chkDevizni.Checked == true)
                    {
                        // row.Cells[10].Value = VCena.ToString();
                        row.Cells[16].Value = VPCenaAkcija.ToString();
                        row.Cells[15].Value = MarzaNova.ToString();
                        row.Cells[14].Value = Convert.ToDecimal(txt_Akcija.Text);
                        row.Cells[13].Value = VPCENASaRabatom.ToString();
                        row.Cells[12].Value = MarzaNova2.ToString();
                        row.Cells[11].Value = Convert.ToDecimal(txt_Rabat.Text);
                        row.Cells[10].Value = PocVPCENA.ToString();
                    }
                    else
                    {
                        row.Cells[10].Value = PocVPCENA.ToString();
                        row.Cells[11].Value = Convert.ToDecimal(txt_Rabat.Text);
                        row.Cells[12].Value = Convert.ToDecimal(txt_Akcija.Text);
                        row.Cells[13].Value = MarzaNova.ToString();
                        row.Cells[15].Value = VPCenaAkcija.ToString();
                        row.Cells[16].Value = MCena.ToString();
                    }

                  
                     
                }
            }
        }
    
    
    }
}