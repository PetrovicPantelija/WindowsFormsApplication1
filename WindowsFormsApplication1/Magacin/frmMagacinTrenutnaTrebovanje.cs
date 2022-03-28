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
using System.Net;
using System.Net.Mail;

using Microsoft.Reporting.WinForms;

namespace WindowsFormsApplication1.Magacin
{
    public partial class frmMagacinTrenutnaTrebovanje : Form
    {
        string KorisnikForme = "";
        public static string code = "frmMagacinTrenutnaTrebovanje";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public frmMagacinTrenutnaTrebovanje()
        {
            InitializeComponent();
        }

        public frmMagacinTrenutnaTrebovanje(string Korisnik)
        {
            InitializeComponent();
            KorisnikForme = Korisnik;
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
        private void button6_Click(object sender, EventArgs e)
        {
            var select = "  select PrTpStDokumenta as ID,  PrTpSifMp as Sifra, MaticniPodatki.MpStaraSif as Kod, Min(PrTpIzdKol) as Kolicina, " +
              "   Sum(isnull(aUzimanjeBarkodTreb.Kolicina, 0)) as Uzeto, " +
              " (MIN(PrTpIzdKol) - SUM(isnull(aUzimanjeBarkodTreb.Kolicina, 0))) as Preostalo " +
       "   from PrometTransPos " +
         "  inner Join MaticniPodatki  on MaticniPodatki.MpSifra = PrometTransPos.PrTpSifMp " +
         " left join aUzimanjeBarkodTreb on aUzimanjeBarkodTreb.Artikal = PrTpSifMp " +
       "  where  PrTpStDokumenta =  " + Convert.ToInt32(txtTrebovanje.Text) + 
       " group by PrTpStDokumenta, PrTpSifMp, MaticniPodatki.MpStaraSif";

            


            //PrTpStaraSifMp as Kod,
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
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 30;

            PopuniUzeteStavke();

        }

        private void PopuniUzeteStavke()
        {

            var select = "   SELECT [Korisnik],[Artikal], MpStaraSif as Kod" +
                            "  ,[Pozicija],[Kolicina],[ID], LokOpis as PozicijaNaziv, Broj " +
                            " FROM [dbo].[aUzimanjeBarkodTreb] " +
                           " inner join MaticniPodatki on MaticniPodatki.MpSifra = aUzimanjeBarkodTreb.Artikal " +
                           " inner join Lokac on Rtrim(Cast(aUzimanjeBarkodTreb.Pozicija as char(12)))  = Rtrim(Lokac.LokSifra) " +
                "  where    Broj = " + Convert.ToInt32(txtTrebovanje.Text);

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];



        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal KolicinaZaIzdavanje = 0;



            InsertMedjuskladisni isporuka = new InsertMedjuskladisni();
            isporuka.DeleteUzimanjeTrebovanje(Convert.ToInt32(txtTrebovanje.Text));
            // isporuka.InsertIZDTrenutna(cboKorisnik.Text.Trim());
/*
            SELECT[Korisnik] - 0,[Artikal] - 1, MpStaraSif as Kod -2" +
                            "  ,[Pozicija] -3,[Kolicina] -4,[ID], LokOpis as PozicijaNaziv, Broj " +
                            " FROM [dbo].[aUzimanjeBarkodTreb] " +
                           " inner join MaticniPodatki on MaticniPodatki.MpSifra = aUzimanjeBarkodTreb.Artikal " +
                           " inner join Lokac on Rtrim(Cast(aUzimanjeBarkodTreb.Pozicija as char(12)))  = Rtrim(Lokac.LokSifra) " +
                "  where    Broj = " + Convert.ToInt32(txtTrebovanje.Text);
*/
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row != null && row.Cells[0].Value != null)
                {


                    KolicinaZaIzdavanje = Convert.ToDecimal(row.Cells[4].Value.ToString());

                   // MessageBox.Show(row.Cells[3].Value.ToString());
                    isporuka.InsertIZDPostavTrenutnoRevers(Convert.ToInt32(row.Cells[1].Value.ToString()), KolicinaZaIzdavanje, row.Cells[3].Value.ToString(), "sa", Convert.ToInt32(txtTrebovanje.Text));
                  // isporuka.UpdateaUzimanjeFormiranje(Convert.ToInt32(row.Cells[5].Value.ToString()));
                    // MessageBox.Show(row.Cells[2].Value.ToString());
                }
            }
            ///ENd of formiranje izdatnice

        }
    }
}
