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

namespace WindowsFormsApplication1.Planovi_Excel
{
    public partial class frmPrikazPorudzbinaWebsOgraniceno : Form
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public frmPrikazPorudzbinaWebsOgraniceno()
        {
            InitializeComponent();
            FillGV();
        }
        private void FillGV()
        {
            var select = "select rk.*,CAST(SUM((MaticniPodatki.MpBrutoTeza*rk.Kolicina))as decimal(18,2))as Tezina " +
                       "From RkShippingExportPorudzbine rk " +
                       "inner join MaticniPodatki on rk.LargoPaketID = MaticniPodatki.MpSifra " +
                       "group by ID,LargoProizvodID,LargoProizvodNaziv,LargoPaketID,LargoPaketNaziv,rk.Kolicina,Relacija,RelacijaNaziv,RelacijaDatum " +
                       "order by ID asc";
            SqlConnection conn2 = new SqlConnection(connect);
            var da2 = new SqlDataAdapter(select, conn2);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds2.Tables[0];

            var query = "select distinct RelacijaNaziv from RkShippingExportPorudzbine order by RelacijaNaziv asc";
            var da = new SqlDataAdapter(query, conn2);
            var ds = new DataSet();
            da.Fill(ds);
            cboRelacija.DataSource = ds.Tables[0];
            cboRelacija.DisplayMember = "RelacijaNaziv";
            cboRelacija.ValueMember = "RelacijaNaziv";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var select = "select rk.*,CAST(SUM((MaticniPodatki.MpBrutoTeza*rk.Kolicina))as decimal(18,2))as Tezina " +
                       "From RkShippingExportPorudzbine rk  Where RelacijaNaziv='" + cboRelacija.SelectedValue.ToString() + "' " +
            "group by ID,LargoProizvodID,LargoProizvodNaziv,LargoPaketID,LargoPaketNaziv,rk.Kolicina,Relacija,RelacijaNaziv,RelacijaDatum " +
                       "order by ID asc";
            SqlConnection conn2 = new SqlConnection(connect);
            var da2 = new SqlDataAdapter(select, conn2);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds2.Tables[0];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var select = "select rk.*,CAST(SUM((MaticniPodatki.MpBrutoTeza*rk.Kolicina))as decimal(18,2))as Tezina " +
                       "From RkShippingExportPorudzbine rk " +
                       "inner join MaticniPodatki on rk.LargoPaketID = MaticniPodatki.MpSifra " +
                       "group by ID,LargoProizvodID,LargoProizvodNaziv,LargoPaketID,LargoPaketNaziv,rk.Kolicina,Relacija,RelacijaNaziv,RelacijaDatum " +
                       "order by ID asc";
            SqlConnection conn2 = new SqlConnection(connect);
            var da2 = new SqlDataAdapter(select, conn2);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds2.Tables[0];

            dataGridView2.Columns[0].Width = 60;
            dataGridView2.Columns[1].Width = 80;
            dataGridView2.Columns[3].Width = 80;
            dataGridView2.Columns[5].Width = 50;
            dataGridView2.Columns[6].Width = 50;

            var query = "select distinct RelacijaNaziv from RkShippingExportPorudzbine order by RelacijaNaziv asc";
            var da = new SqlDataAdapter(query, conn2);
            var ds = new DataSet();
            da.Fill(ds);
            cboRelacija.DataSource = ds.Tables[0];
            cboRelacija.DisplayMember = "RelacijaNaziv";
            cboRelacija.ValueMember = "RelacijaNaziv";
        }

        private void btnIzvestaj_Click(object sender, EventArgs e)
        {
            IzvestajNeisporuceni frm = new IzvestajNeisporuceni();
            frm.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var select = "select rk.*,CAST(SUM((MaticniPodatki.MpBrutoTeza*rk.Kolicina))as decimal(18,2))as Tezina " +
                       "From RkShippingExportPorudzbine rk   Where RelacijaNaziv='" + cboRelacija.SelectedValue.ToString() + "' and RelacijaDatum='" + Convert.ToDateTime(dateTimePicker1.Value) + "'";
            SqlConnection conn2 = new SqlConnection(connect);
            var da2 = new SqlDataAdapter(select, conn2);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds2.Tables[0];
        }
    }
}
