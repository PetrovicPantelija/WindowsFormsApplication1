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

namespace WindowsFormsApplication1.Magacin
{
    public partial class frmPrijavaStanja : Form
    {
        public frmPrijavaStanja()
        {
            InitializeComponent();
        }

        private void frmPrijavaStanja_Load(object sender, EventArgs e)
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
            var select = "  select SkSifra, SkNaziv, LokSkSifra, LokSifra, Kolicina" +
           " from PregledStanja   " +
            " inner join Lokac.LokSkSifra = PregledStanja.BarkodPozicije" +
           " inner join Sklad on Lokac.LokSkSifra = Sklad.SkSifra" +
           " Where Lokac.LokSkSifra = " + Convert.ToInt32(cboSkladisteIzbor.SelectedValue);
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
            dataGridView1.Columns[0].HeaderText = "Šifra skladista";
            dataGridView1.Columns[0].Width = 80;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv skladista";
            dataGridView1.Columns[1].Width = 180;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Pozicija";
            dataGridView1.Columns[2].Width = 80;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Pozicija naziv";
            dataGridView1.Columns[3].Width = 180;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Količina";
            dataGridView1.Columns[4].Width = 80;
        }
    }
}
