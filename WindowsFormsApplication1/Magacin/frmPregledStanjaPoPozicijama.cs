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
    public partial class frmPregledStanjaPoPozicijama : Form
    {
        public frmPregledStanjaPoPozicijama()
        {
            InitializeComponent();
        }

        private void frmPregledStanjaPoPozicijama_Load(object sender, EventArgs e)
        {
            var select2 = " Select SkSifra, (Cast(SkSifra as nvarchar(3)) + '-' + SkNaziv) as SkNaziv from Sklad order by SkSifra";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboSkladisteIzbor.DataSource = ds2.Tables[0];
            cboSkladisteIzbor.DisplayMember = "SkNaziv";
            cboSkladisteIzbor.ValueMember = "SkSifra";


            var select3 = " Select SkSifra, (Cast(SkSifra as nvarchar(3))  + '-' + SkNaziv) as SkNaziv from Sklad order by SkSifra";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboSkladisteIzbor2.DataSource = ds3.Tables[0];
            cboSkladisteIzbor2.DisplayMember = "SkNaziv";
            cboSkladisteIzbor2.ValueMember = "SkSifra";
        }

        private void btnOsvezi_Click(object sender, EventArgs e)
        {
            var select = " select   PrSifSklad as Skladiste, PrLokacija As Lokacija, " +
            " PrSifMp as MP, MIN(MpStaraSif) as Kod,   Min(MpNaziv) as ImeMP, " +
            " Min(MPSifEnoteMere1) as JM,   MIN(Lokac.Red) as Red, MIN(Lokac.Vertikala) as Vertikala, MIN(Lokac.Celija) as Celija, MIN(Lokac.Sirina) as Sirina, MIN(Lokac.MaxTezina) as MaxTezina, MIN(Lokac.Aktivna) as Aktivna, MIN(Lokac.MAticniPodatak) as MPOsnovni " +
            " ,MIN(Paleta.Naziv) as Paleta ,MIN(MaticniPodatkiMagacin.TezinaPalete) as MPTezinaPalete, MIN(MaticniPodatkiMagacin.BrutoPaketa) as MPBrutoPaketa, MIN(MaticniPodatkiMagacin.MaxKolicina) as MPMaxKolicina, " +
            " sum(PrPrevKol - PrIzdKol) as Kolicina " +
            " from MaticniPodatki " +
            " Left join Promet on MPSifra = PrSifMP " +
            " inner join Lokac On Lokac.LokSifra = Promet.PrLokacija " +
            " inner join MaticniPodatkiMagacin on MaticniPodatkiMagacin.MpSifra=MaticniPodatki.MpSifra " +
            " inner join Paleta on Paleta.ID = MaticniPodatkiMagacin.Paleta " +
            " where MaticniPodatki.MpOsnSklad = " + Convert.ToInt32(cboSkladisteIzbor.SelectedValue) + 
            " group By PrSifSklad, PrLokacija, PrSifMp ";

              // " where PrSifSklad = " + Convert.ToInt32(cboSkladisteIzbor.SelectedValue) + 
            ///MaticniPodatki.MpOsnSklad

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
            dataGridView1.Columns[0].HeaderText = "Skladište";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Lokacija";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Naziv(Kod)";
            dataGridView1.Columns[2].Width = 120;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Naziv";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "JM";
            dataGridView1.Columns[4].Width = 50;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Red";
            dataGridView1.Columns[5].Width = 50;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Vertikala";
            dataGridView1.Columns[6].Width = 50;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Ćelija";
            dataGridView1.Columns[7].Width = 50;

            DataGridViewColumn column8 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Širina";
            dataGridView1.Columns[8].Width = 60;

            DataGridViewColumn column9 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Max težina";
            dataGridView1.Columns[9].Width = 60;

            DataGridViewColumn column10 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Aktivna";
            dataGridView1.Columns[10].Width = 60;

            DataGridViewColumn column11 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "MpOsnovna";
            dataGridView1.Columns[11].Width = 60;

            DataGridViewColumn column12 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Paleta";
            dataGridView1.Columns[12].Width = 60;

            DataGridViewColumn column13 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Max paketi + Paleta";
            dataGridView1.Columns[13].Width = 60;

            DataGridViewColumn column14 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "Bruto palete";
            dataGridView1.Columns[14].Width = 60;

            DataGridViewColumn column15 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Max količina";
            dataGridView1.Columns[15].Width = 60;

            DataGridViewColumn column16 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Količina";
            dataGridView1.Columns[16].Width = 60; 
        }

        private void RefreshDataGrid2(int SifraMaticniPodatak )
        {
            /*
    select top 30 PrStZapisa , PrSifVrDok , PrStDokumenta, PrSifMP,PrSifVrstePrometa, PrPrevKol, PrIzdKol, PrSifSklad, PrLokacija from Promet
    where PrSifMp = 16
    Order by PrStZapisa desc
            */
            var select = "  select top 30 PrStZapisa , PrSifVrDok , PrStDokumenta, PrSifMP,PrSifVrstePrometa, PrPrevKol, PrIzdKol, PrSifSklad, PrLokacija from Promet " +
   " where PrSifMp = " + SifraMaticniPodatak + " and PrSifSklad = " + Convert.ToInt32(cboSkladisteIzbor.SelectedValue) + " Order by PrStZapisa desc ";
    

            ///MaticniPodatki.MpOsnSklad

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];


            //Ovde napisati redosled
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        RefreshDataGrid2(Convert.ToInt32(row.Cells[2].Value.ToString()));
                       // txtSifra.Text = row.Cells[0].Value.ToString();

                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }
    }
}
