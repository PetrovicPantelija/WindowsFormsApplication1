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


namespace WindowsFormsApplication1.Portirnica
{
    public partial class StatusNajaveLog : Form
    {
        int tmpSifra = 0;
        public StatusNajaveLog()
        {
            InitializeComponent();
        }

        public StatusNajaveLog(int ID)
        {
            InitializeComponent();
            tmpSifra = ID;
        }

        private void StatusNajaveLog_Load(object sender, EventArgs e)
        {
            var select = "  SELECT [ID] ,[IDNajave],[Promena],[Korisnik],[DatumPromene]  FROM [dbo].[LogNajave] where IDNajave = " + tmpSifra;


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
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "ID najave";

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Promena";
            dataGridView1.Columns[2].Width = 350;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Korisnik";
            dataGridView1.Columns[3].Width = 100;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Datum i vreme promene";
            dataGridView1.Columns[4].Width = 100;

          
        }
    }
}
