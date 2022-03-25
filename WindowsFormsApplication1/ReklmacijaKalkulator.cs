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
    public partial class ReklmacijaKalkulator : Form
    {
        public static string code = "ReklamacijeKalkulator";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        public ReklmacijaKalkulator()
        {
            InitializeComponent(); IdGrupe();
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
        private void ReklmacijaKalkulator_Load(object sender, EventArgs e)
        {
            var select5 = " select MpSifra , (Cast(MpSifra as nvarchar(6)) + '-'  + MpStaraSif) as MpStaraSif from MaticniPodatki order by MpSifra";
            var s_connection5 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection5 = new SqlConnection(s_connection5);
            var c5 = new SqlConnection(s_connection5);
            var dataAdapter5 = new SqlDataAdapter(select5, c5);

            var commandBuilder5 = new SqlCommandBuilder(dataAdapter5);
            var ds5 = new DataSet();
            dataAdapter5.Fill(ds5);
            cboMaticni.DataSource = ds5.Tables[0];
            cboMaticni.DisplayMember = "MpStaraSif";
            cboMaticni.ValueMember = "MpSifra";


           
        }

        private void btnOsvezi_Click(object sender, EventArgs e)
        {
            var select6 = " select distinct KoVarianta from Kosovnica where KoNadSifMp = " + Convert.ToInt32(cboMaticni.SelectedValue);
            var s_connection6 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection6 = new SqlConnection(s_connection6);
            var c6 = new SqlConnection(s_connection6);
            var dataAdapter6 = new SqlDataAdapter(select6, c6);

            var commandBuilder6 = new SqlCommandBuilder(dataAdapter6);
            var ds6 = new DataSet();
            dataAdapter6.Fill(ds6);
            cboVarijanta .DataSource = ds6.Tables[0];
            cboVarijanta.DisplayMember = "KoVarianta";
            cboVarijanta.ValueMember = "KoVarianta";


            VratiRINO(Convert.ToInt32(cboMaticni.SelectedValue));
            VratiRSR(Convert.ToInt32(cboMaticni.SelectedValue));
            VratiROT(Convert.ToInt32(cboMaticni.SelectedValue));

            VratiObracunKalkCena(Convert.ToInt32(cboMaticni.SelectedValue));
            VratiPodatkeKod();

        }

        private void VratiObracunKalkCena(int Maticni)
        {

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("Select Top 1 KcSifMp as SifMP,  KcDatSpremembe as DatumCene, " +
                    " KcCena_D, KcCena_M, " +   
                    " KcFStr,  " +  
                    " KcCena_DD " +
                    ", (KcCena_D + KcCena_M + KcFStr + KcCena_DD + KcCena_P) as Ukupno " +
                    " From KalkCenik, MaticniPodatki " + 
                    " Where   KcValid = 1 and KcSifMp  = MpSifra " +
                    " and KCSifMP = " + Maticni + " and KcTipCn = 'G' " +
                    " order by KcDatSpremembe desc " , con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtCenaD.Text = dr["KcCena_D"].ToString();
                txtCenaM.Text = dr["KcCena_M"].ToString();
                txtFiksni.Text = dr["KcFStr"].ToString();
                txtCenaDD.Text = dr["KcCena_DD"].ToString();
                txtProizvodna.Text = dr["Ukupno"].ToString();
            }

            con.Close();
        }


        private void VratiRINO(int Maticni)
        {
            txtRINO.Text = "0";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select CnSifMp,  CnCena from ProdCenikIzd where CnRazTipCn = 'RINO' and CnSifMp =" + Maticni, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtRINO.Text = dr["CnCena"].ToString();
            }

            con.Close();
        
        
        
        }

        private void VratiROT(int Maticni)
        {
            txtROT.Text = "0";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select CnSifMp,  CnCena from ProdCenikIzd where CnRazTipCn = 'ROT' and CnSifMp =" + Maticni, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtROT.Text = dr["CnCena"].ToString();
            }

            con.Close();



        }

        private void VratiRSR(int Maticni)
        {
            txtRSR.Text = "0";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select CnSifMp,  CnCena from ProdCenikIzd where CnRazTipCn = 'RSR' and CnSifMp =" + Maticni, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtRSR.Text = dr["CnCena"].ToString();
            }

            con.Close();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            var select = "  WITH UserCTE AS ( " +
         " SELECT Kosovnica.KoNadSifMp, Kosovnica.KoPodSifMp,MaticniPodatki.MpStaraSif,1 AS steps, KoSePrikazuje, Cena1, Cena2,Cena3, Cena4, '' as Cena5 ,KoKolPodrMp, (Cena1 * KoKolPodrMp) as UkupnaRSR, (Cena2 * KoKolPodrMp) as UkupnaROT,(Cena3 * KoKolPodrMp) as UkupnaRINO,(Cena4 * KoKolPodrMp) as UkupnaProizvodna, '' as UkupnoProizvodna2 " +
         " FROM dbo.Kosovnica " +
         " inner join MaticniPodatki on Kosovnica.KoPodSifMp = MaticniPodatki.MpSifra " +
         " WHERE Kosovnica.KoNadSifMp =  " + Convert.ToInt32(cboMaticni.SelectedValue) + " and Kosovnica.KoVarianta = '" + cboVarijanta.SelectedValue +  "' "  +
         " UNION ALL " +
         " SELECT mgr.KoNadSifMp, mgr.KoPodSifMp, MaticniPodatki.MpStaraSif, usr.steps +1 AS steps, mgr.KoSePrikazuje, mgr.Cena1, mgr.Cena2,mgr.Cena3,mgr.Cena4, '' as Cena5 ,mgr.KoKolPodrMp, (mgr.Cena1 * mgr.KoKolPodrMp) as UkupnaRSR,  (mgr.Cena2 * mgr.KoKolPodrMp) as UkupnaROT, (mgr.Cena3 * mgr.KoKolPodrMp) as UkupnaRINO,(mgr.Cena4 * mgr.KoKolPodrMp) as UkupnaProizvodna, '' as UkupnoProizvodna2  " +
         " FROM UserCTE AS usr " +
         "   INNER JOIN dbo.Kosovnica AS mgr " +
         "     ON mgr.KoNadSifMp = usr.KoPodSifMp " +
         "      inner join MaticniPodatki on mgr.KoPodSifMp = MaticniPodatki.MpSifra " +
         "     where   mgr.KoVarianta = '" + cboVarijanta.SelectedValue +  "') " +
         " SELECT * FROM UserCTE where KoSePrikazuje <> 1;";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

          
             DataGridViewColumn column = dataGridView1.Columns[0];
             dataGridView1.Columns[0].HeaderText = "Šifra nadredjenog";
             dataGridView1.Columns[0].Width = 60;

             DataGridViewColumn column1 = dataGridView1.Columns[1];
             dataGridView1.Columns[1].HeaderText = "Šifra podredjenog";
             dataGridView1.Columns[1].Width = 60;

             DataGridViewColumn column2 = dataGridView1.Columns[2];
             dataGridView1.Columns[2].HeaderText = "Kod podredjenog";
             dataGridView1.Columns[2].Width = 120;

             DataGridViewColumn column3 = dataGridView1.Columns[3];
             dataGridView1.Columns[3].HeaderText = "Nivo";
             dataGridView1.Columns[3].Width = 40;

             DataGridViewColumn column4 = dataGridView1.Columns[4];
             dataGridView1.Columns[4].HeaderText = "Ne prikazuje se";
             dataGridView1.Columns[4].Width = 40;

             DataGridViewColumn column5 = dataGridView1.Columns[5];
             dataGridView1.Columns[5].HeaderText = "RSR";
             dataGridView1.Columns[5].Width = 80;

             DataGridViewColumn column6 = dataGridView1.Columns[6];
             dataGridView1.Columns[6].HeaderText = "ROT";
             dataGridView1.Columns[6].Width = 80;

             DataGridViewColumn column7 = dataGridView1.Columns[7];
             dataGridView1.Columns[7].HeaderText = "RINO";
             dataGridView1.Columns[7].Width = 80;


             DataGridViewColumn column8 = dataGridView1.Columns[8];
             dataGridView1.Columns[8].HeaderText = "Proizvodna/JM";
             dataGridView1.Columns[8].Width = 80;

             DataGridViewColumn column9 = dataGridView1.Columns[9];
             dataGridView1.Columns[9].HeaderText = "Proizvodna2/JM";
             dataGridView1.Columns[9].Width = 80;

             DataGridViewColumn column10 = dataGridView1.Columns[10];
             dataGridView1.Columns[10].HeaderText = "Količina";
             dataGridView1.Columns[10].Width = 80;

             DataGridViewColumn column11 = dataGridView1.Columns[11];
             dataGridView1.Columns[11].HeaderText = "UK-RSR";
             dataGridView1.Columns[11].Width = 80;

             DataGridViewColumn column12 = dataGridView1.Columns[12];
             dataGridView1.Columns[12].HeaderText = "UK-ROT";
             dataGridView1.Columns[12].Width = 80;
             
            DataGridViewColumn column13 = dataGridView1.Columns[13];
             dataGridView1.Columns[13].HeaderText = "UK-RINO";
             dataGridView1.Columns[13].Width = 80;

             DataGridViewColumn column14 = dataGridView1.Columns[14];
             dataGridView1.Columns[14].HeaderText = "Proizvodna";
             dataGridView1.Columns[14].Width = 100;

             DataGridViewColumn column15 = dataGridView1.Columns[15];
             dataGridView1.Columns[15].HeaderText = "Proizvodna2";
             dataGridView1.Columns[15].Width = 100;

             MessageBox.Show("Tabela");
             PovuciProizvodneCene();
             
        }

        private void PovuciProizvodneCene()
        {
           //PantaProizvodne
            
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                //IzvuciCenu
                var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(s_connection);

                con.Open();

                SqlCommand cmd = new SqlCommand("Select Top 1 KcSifMp as SifMP,  KcDatSpremembe as DatumCene, " +
                        " KcCena_D, KcCena_M, " +
                        " KcFStr,  " +
                        " KcCena_DD " +
                        ", (KcCena_D + KcCena_M + KcFStr + KcCena_DD + KcCena_P) as Ukupno " +
                        " From KalkCenik, MaticniPodatki " +
                        " Where   KcValid = 1 and KcSifMp  = MpSifra " +
                        " and KCSifMP = " + Convert.ToInt32(row.Cells[1].Value.ToString()) + " and KcTipCn = 'G' " +
                        " order by KcDatSpremembe desc ", con);
                SqlDataReader dr = cmd.ExecuteReader();
              
                while (dr.Read())
                {
                    row.Cells[9].Value = dr["Ukupno"].ToString();
                    row.Cells[15].Value = (Convert.ToDouble(dr["Ukupno"].ToString()) * Convert.ToDouble(row.Cells[10].Value)).ToString();
                   
                }

                con.Close();
               
            }
        
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var select = "  WITH UserCTE AS ( " +
       " SELECT Kosovnica.KoNadSifMp, Kosovnica.KoPodSifMp,MaticniPodatki.MpStaraSif,1 AS steps, KoSePrikazuje, Cena1, Cena2,Cena3,Cena4, '' as Cena5,  KoKolPodrMp, (Cena1 * KoKolPodrMp) as UkupnaRSR, (Cena2 * KoKolPodrMp) as UkupnaROT,(Cena3 * KoKolPodrMp) as UkupnaRINO,(Cena4 *  KoKolPodrMp)  as UkupnoProizvodna ,''  as UkupnoProizvodna2  " +
       " FROM dbo.Kosovnica " +
       " inner join MaticniPodatki on Kosovnica.KoPodSifMp = MaticniPodatki.MpSifra " +
       " WHERE Kosovnica.KoNadSifMp =  " + Convert.ToInt32(cboMaticni.SelectedValue) + " and Kosovnica.KoVarianta = '" + cboVarijanta.SelectedValue + "' " +
       " UNION ALL " +
       " SELECT mgr.KoNadSifMp, mgr.KoPodSifMp, MaticniPodatki.MpStaraSif, usr.steps +1 AS steps, mgr.KoSePrikazuje, mgr.Cena1, mgr.Cena2,mgr.Cena3,mgr.Cena4, '' as Cena5, mgr.KoKolPodrMp, (mgr.Cena1 * mgr.KoKolPodrMp) as UkupnaRSR,  (mgr.Cena2 * mgr.KoKolPodrMp) as UkupnaROT, (mgr.Cena3 * mgr.KoKolPodrMp) as UkupnaRINO, (mgr.Cena4 * mgr.KoKolPodrMp) as UkupnoProizvodna, ''  as UkupnoProizvodna2  " +
       " FROM UserCTE AS usr " +
       "   INNER JOIN dbo.Kosovnica AS mgr " +
       "     ON mgr.KoNadSifMp = usr.KoPodSifMp " +
       "      inner join MaticniPodatki on mgr.KoPodSifMp = MaticniPodatki.MpSifra " +
       "     where  mgr.KoVarianta = '" + cboVarijanta.SelectedValue + "') " +
       " SELECT * FROM UserCTE AS u;";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "Šifra nadredjenog";
            dataGridView1.Columns[0].Width = 60;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Šifra podredjenog";
            dataGridView1.Columns[1].Width = 60;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Kod podredjenog";
            dataGridView1.Columns[2].Width = 120;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Nivo";
            dataGridView1.Columns[3].Width = 40;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Ne prikazuje se";
            dataGridView1.Columns[4].Width = 40;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "RSR";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "ROT";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "RINO";
            dataGridView1.Columns[7].Width = 80;


            DataGridViewColumn column8 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Proizvodna/JM";
            dataGridView1.Columns[8].Width = 80;

            DataGridViewColumn column9 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Proizvodna2/JM";
            dataGridView1.Columns[9].Width = 80;

            DataGridViewColumn column10 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Količina";
            dataGridView1.Columns[10].Width = 80;

            DataGridViewColumn column11 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "UK-RSR";
            dataGridView1.Columns[11].Width = 80;

            DataGridViewColumn column12 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "UK-ROT";
            dataGridView1.Columns[12].Width = 80;

            DataGridViewColumn column13 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "UK-RINO";
            dataGridView1.Columns[13].Width = 80;

            DataGridViewColumn column14 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Proizvodna";
            dataGridView1.Columns[14].Width = 100;

            DataGridViewColumn column15 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Proizvodna2";
            dataGridView1.Columns[15].Width = 100;

            PovuciProizvodneCene();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                InsertReklamacije ins = new InsertReklamacije(); 
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        ins.UpdateKosovnicaMP(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), cboVarijanta.SelectedValue.ToString());
                      
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                InsertReklamacije ins = new InsertReklamacije();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        ins.UpdateKosovnicaMPUvek(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), cboVarijanta.SelectedValue.ToString());

                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                InsertReklamacije ins = new InsertReklamacije();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        ins.UpdateKosovnicaGranaUvek(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), cboVarijanta.SelectedValue.ToString());
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                InsertReklamacije ins = new InsertReklamacije();
                ins.AzuriranjeCena(Convert.ToInt32(txtBrZapisa.Text), Convert.ToInt32(txtKalk.Text));
                MessageBox.Show("Cene uspešno analizirane");
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            InsertReklamacijaKalkulator ins = new InsertReklamacijaKalkulator();
            ins.InsertCenaSonja(Convert.ToInt32(cboMaticni.SelectedValue), "ROT","EUR", Convert.ToDecimal(txtROT.Text), txtKod.Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            InsertReklamacijaKalkulator ins = new InsertReklamacijaKalkulator();
            ins.InsertCenaSonja(Convert.ToInt32(cboMaticni.SelectedValue), "RINO", "EUR", Convert.ToDecimal(txtRINO.Text), txtKod.Text);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            InsertReklamacijaKalkulator ins = new InsertReklamacijaKalkulator();
            ins.InsertCenaSonja(Convert.ToInt32(cboMaticni.SelectedValue), "RSR", "RSD", Convert.ToDecimal(txtRSR.Text), txtKod.Text);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                      
                        txtStavkaSifra.Text = row.Cells[1].Value.ToString();
                        txtRINOStavka.Text = row.Cells[7].Value.ToString();
                        txtROTStavka.Text = row.Cells[6].Value.ToString();
                        txtRSRStavka.Text = row.Cells[5].Value.ToString();
                        VratiPodatkeKodStavka();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            InsertReklamacijaKalkulator ins = new InsertReklamacijaKalkulator();
            ins.InsertCenaSonja(Convert.ToInt32(txtStavkaSifra.Text), "ROT", "EUR", Convert.ToDecimal(txtROTStavka.Text), txtStavkaKod.Text);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            InsertReklamacijaKalkulator ins = new InsertReklamacijaKalkulator();
            ins.InsertCenaSonja(Convert.ToInt32(txtStavkaSifra.Text), "RINO", "EUR", Convert.ToDecimal(txtRINOStavka.Text), txtStavkaKod.Text);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            InsertReklamacijaKalkulator ins = new InsertReklamacijaKalkulator();
            ins.InsertCenaSonja(Convert.ToInt32(txtStavkaSifra.Text), "RSR", "RSD", Convert.ToDecimal(txtRSRStavka.Text), txtStavkaKod.Text);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPoNazivu.Checked == true)
            {
                var select5 = " select MpSifra ,  MpStaraSif from MaticniPodatki order by  MpStaraSif";
                var s_connection5 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
                SqlConnection myConnection5 = new SqlConnection(s_connection5);
                var c5 = new SqlConnection(s_connection5);
                var dataAdapter5 = new SqlDataAdapter(select5, c5);

                var commandBuilder5 = new SqlCommandBuilder(dataAdapter5);
                var ds5 = new DataSet();
                dataAdapter5.Fill(ds5);
                cboMaticni.DataSource = ds5.Tables[0];
                cboMaticni.DisplayMember = "MpStaraSif";
                cboMaticni.ValueMember = "MpSifra";
            
            
            }
            else
            {
                var select5 = " select MpSifra , (Cast(MpSifra as nvarchar(6)) + '-'  + MpStaraSif) as MpStaraSif from MaticniPodatki order by MpSifra";
                var s_connection5 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
                SqlConnection myConnection5 = new SqlConnection(s_connection5);
                var c5 = new SqlConnection(s_connection5);
                var dataAdapter5 = new SqlDataAdapter(select5, c5);

                var commandBuilder5 = new SqlCommandBuilder(dataAdapter5);
                var ds5 = new DataSet();
                dataAdapter5.Fill(ds5);
                cboMaticni.DataSource = ds5.Tables[0];
                cboMaticni.DisplayMember = "MpStaraSif";
                cboMaticni.ValueMember = "MpSifra";
            }
        }

        private void VratiPodatkeKod()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT MpStaraSif " +
             "  FROM MaticniPodatki where MpSifra=" + Convert.ToInt32(cboMaticni.SelectedValue), con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtKod.Text = dr["MpStaraSif"].ToString();
            }

            con.Close();
        }

        private void VratiPodatkeKodStavka()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT MpStaraSif " +
             "  FROM MaticniPodatki where MpSifra=" + Convert.ToInt32(txtStavkaSifra.Text), con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                txtStavkaKod.Text = dr["MpStaraSif"].ToString();
            }

            con.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            InsertReklamacijaNalepnica ReklNal = new InsertReklamacijaNalepnica();
            ReklNal.InsertReklROTStampaj(Convert.ToInt32(cboMaticni.SelectedValue));

            Perftech_BeogradDataSet1TableAdapters.ROTStampaTableAdapter ta = new Perftech_BeogradDataSet1TableAdapters.ROTStampaTableAdapter();
            Perftech_BeogradDataSet1.ROTStampaDataTable dt = new Perftech_BeogradDataSet1.ROTStampaDataTable();
            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "report9.rdlc";

            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();


        }
    }
}
