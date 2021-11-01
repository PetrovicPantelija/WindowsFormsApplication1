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
using System.Globalization;

namespace WindowsFormsApplication1
{
    public partial class frmPorudzbinaPregled : Form
    {
        public frmPorudzbinaPregled()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Panta2
            //Obrnuto Datum i broj Utovara
            var select = " SELECT [NameOper],[Datum],[NaPVpisalIme] ,[NaPVpisalDat] " +
           " ,  [NaPNarZap] ,[NaPStNar] ,[NaPIntParent],[NaPStPos] " +
           " ,[NaPStatus],[NaPAliasStatus],[NaPStatPlan] ,[NaPOznaka] " +
           " ,[NaPSifra] ,[NaPSifMpPopa] ,[NaPNaziv] ,[NaPEM] " +
           " ,[NaPEM2],[NaPKolNar],[NaPKolNar2],[NaPKolOdpr] " + 
           " ,[NaPTipCen] ,[NaPCenaEM],[NaPKoda] from NarociloPostav " +
            " where NaPStNar  = " + textBox1.Text ;



            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];
           
            // string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            /*
            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "Barkod utovara";
            dataGridView3.Columns[1].Width = 140;

            DataGridViewColumn column3 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "Br. utovara";
            dataGridView3.Columns[2].Width = 45;


            DataGridViewColumn column4 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "Datum utovara";
            dataGridView3.Columns[3].Width = 90;

            DataGridViewColumn column5 = dataGridView3.Columns[4];
            dataGridView3.Columns[4].HeaderText = "Br. isporuke";
            dataGridView3.Columns[4].Width = 45;

            DataGridViewColumn column6 = dataGridView3.Columns[5];
            dataGridView3.Columns[5].HeaderText = "Datum isporuke";
            dataGridView3.Columns[5].Width = 90;



            DataGridViewColumn column7 = dataGridView3.Columns[6];
            dataGridView3.Columns[6].HeaderText = "Mesto isporuke";
            dataGridView3.Columns[6].Width = 170;


            DataGridViewColumn column8 = dataGridView3.Columns[7];
            dataGridView3.Columns[7].HeaderText = "Paket name";
            dataGridView3.Columns[7].Width = 200;

            DataGridViewColumn column9 = dataGridView3.Columns[8];
            dataGridView3.Columns[8].HeaderText = "Šif. Largo";
            dataGridView3.Columns[8].Width = 50;


            DataGridViewColumn column10 = dataGridView3.Columns[9];
            dataGridView3.Columns[9].HeaderText = "Largo naziv";
            dataGridView3.Columns[9].Width = 195;


            DataGridViewColumn column11 = dataGridView3.Columns[10];
            dataGridView3.Columns[10].HeaderText = "Paleta";
            dataGridView3.Columns[10].Width = 50;

            DataGridViewColumn column12 = dataGridView3.Columns[11];
            dataGridView3.Columns[11].HeaderText = "Težina(KG)";
            dataGridView3.Columns[11].Width = 60;

            DataGridViewColumn column13 = dataGridView3.Columns[12];
            dataGridView3.Columns[12].HeaderText = "Largo Dimenzija";
            dataGridView3.Columns[12].Width = 120;
             */
        }
    }
}
