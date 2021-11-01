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

namespace WindowsFormsApplication1.Magacin
{
    public partial class frmMaticniPodaciPrikaz : Form
    {
        public frmMaticniPodaciPrikaz()
        {
            InitializeComponent();
        }

        private void frmMaticniPodaciPrikaz_Load(object sender, EventArgs e)
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
        }

        private void btnOsvezi_Click(object sender, EventArgs e)
        {
            var select = " select MaticniPodatkiMagacin.[MpSifra] " +
         " ,MaticniPodatki.[MpStaraSif] " +
         " ,MaticniPodatki.[MpTeza] as Neto" +
         " ,MaticniPodatki.[MpSuhaTeza] as Tara" +
         " ,MaticniPodatki.[MpBrutoTeza]  as Bruto" +
         " ,MaticniPodatki.[MpMera1] as Duzina" +
         " ,MaticniPodatki.[MpMera2] as Sirina" +
         " ,MaticniPodatki.MpMera3 as Visina" +
         "   ,MaticniPodatkiMagacin.[Paleta] " +
         "   ,MaticniPodatkiMagacin.[MaxKolicina] as MaxKolicinaPoPaleti " +
         "  ,MaticniPodatkiMagacin.[BrutoPaketa] as BrutoPaleta " +
         "  ,MaticniPodatkiMagacin.[TezinaPalete] as MaxPaketiPlusPaleta   from MaticniPodatkiMagacin " +
         "  inner join MaticniPodatki On MaticniPodatkiMagacin.MpSifra = MaticniPodatki.MpSifra " +
         "  where MaticniPodatki.[MpOsnSklad] =  " + Convert.ToInt32(cboSkladisteIzbor.SelectedValue) ;
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
            dataGridView1.Columns[0].HeaderText = "Šifra";
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv(Kod)";
            dataGridView1.Columns[1].Width = 140;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Neto";
            dataGridView1.Columns[2].Width = 70;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Tara";
            dataGridView1.Columns[3].Width = 70;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Bruto";
            dataGridView1.Columns[4].Width = 70;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Dužina";
            dataGridView1.Columns[5].Width = 70;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Širina";
            dataGridView1.Columns[6].Width = 70;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Visina";
            dataGridView1.Columns[7].Width = 70;

            DataGridViewColumn column8 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Paleta";
            dataGridView1.Columns[8].Width = 70;

            DataGridViewColumn column9 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Max količina po paleti";
            dataGridView1.Columns[9].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Bruto paleta";
            dataGridView1.Columns[10].Width = 70;

            DataGridViewColumn column11 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Maks paketi + paleta";
            dataGridView1.Columns[11].Width = 70;
        }
    }
}
