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
    public partial class frmPregledTrebovanja : Form
    {
        string KorisnikForme = "";
        public static string code = "frmPregledTrebovanja";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public frmPregledTrebovanja()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            SfGridPomocnik.Stilizuj(dataGridView1);
        }

        public frmPregledTrebovanja(string Korisnik)
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            SfGridPomocnik.Stilizuj(dataGridView1);
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
        private void frmPregledTrebovanja_Load(object sender, EventArgs e)
        {
            var select2 = " Select distinct Rtrim(Korisnik)  as Korisnik from aUzimanjeBarkodTreb ";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboKorisnik.DataSource = ds2.Tables[0];
            cboKorisnik.DisplayMember = "Korisnik";
            cboKorisnik.ValueMember = "Korisnik";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            var select = "   SELECT [Korisnik],[Artikal], MpStaraSif as Kod, " +
                "  [Pozicija],[Kolicina],[ID], (Kolicina -  KolicinaUzeta) as Preostalo,  LokOpis as LokacijaNaziv, FormiranDokument, Broj " +
                " FROM [dbo].[aUzimanjeBarkodTreb] " +
                " inner join MaticniPodatki on MaticniPodatki.MpSifra = aUzimanjeBarkodTreb.Artikal " +
                  " inner join Lokac on Rtrim(Cast(aUzimanjeBarkodTreb.Pozicija as char(12)))  = Rtrim(Lokac.LokSifra) " +
                  "  where   (Zavrsen <> 1) and Rtrim(Korisnik) = Rtrim('" + cboKorisnik.SelectedValue + "')";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            SfGridPomocnik.PopuniK(dataGridView1, ds.Tables[0], new Kol(0, "Korisnik", 50, -1), new Kol(1, "Artikal", 50, -1), new Kol(2, "Kod", 120, -1), new Kol(3, "Lokacija", 40, -1), new Kol(4, "Kolicina", 70, -1), new Kol(5, "ID", 50, -1), new Kol(6, "Preostalo", 50, -1), new Kol(7, "Lok opis", 150, -1), new Kol(8, "FormiranDokument", 40, -1), new Kol(9, "Broj", 40, -1));
        
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal KolicinaZaIzdavanje = 0;



            InsertMedjuskladisni isporuka = new InsertMedjuskladisni();

            isporuka.InsertIZDTrenutna(cboKorisnik.Text.Trim());

            foreach (SfRed row in SfGridPomocnik.Redovi(dataGridView1))
            {
                if (row != null && row.Cells[0].Value != null)
                {

                    if (Convert.ToDecimal(row.Cells[4].Value.ToString()) >= Convert.ToDecimal(row.Cells[6].Value.ToString()))
                    {
                        KolicinaZaIzdavanje = Convert.ToDecimal(row.Cells[4].Value.ToString()) - Convert.ToDecimal(row.Cells[6].Value.ToString());
                    }
                    else
                    {
                        KolicinaZaIzdavanje = Convert.ToDecimal(row.Cells[4].Value.ToString()) - Convert.ToDecimal(row.Cells[6].Value.ToString());
                    }

                    isporuka.InsertIZDPostavTrenutno(Convert.ToInt32(row.Cells[1].Value.ToString()), KolicinaZaIzdavanje, row.Cells[3].Value.ToString(), cboKorisnik.Text.Trim());
                   // isporuka.UpdateaUzimanjeFormiranje(Convert.ToInt32(row.Cells[5].Value.ToString()));
                    //Zakomentarisano 
                    // MessageBox.Show(row.Cells[2].Value.ToString());
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            InsertMedjuskladisni uzimanje = new InsertMedjuskladisni();
            foreach (SfRed row in SfGridPomocnik.Redovi(dataGridView1))
            {
                if (row.Selected == true)
                {
                    //Ovde treba brisanje stavke uzimanja 

                    uzimanje.DeleteStavkuaUzimanje(Convert.ToInt32(row.Cells[5].Value.ToString()));
                    // MessageBox.Show(row.Cells[2].Value.ToString());
                }
            }

            RefreshDataGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (SfRed row in SfGridPomocnik.Redovi(dataGridView1))
            {
                if (Convert.ToInt32(row.Cells[8].Value) == '0')
                {

                    MessageBox.Show("Niste formirali dokumenta");
                    return;
                }
            }



            InsertMedjuskladisni zavrsi = new InsertMedjuskladisni();

            zavrsi.ZavrsiPrivremeni(cboKorisnik.Text.Trim());
            RefreshDataGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
