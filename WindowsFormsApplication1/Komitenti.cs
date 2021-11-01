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

namespace WindowsFormsApplication1
{
    public partial class Komitenti : Form
    {
        public Komitenti()
        {
            InitializeComponent();
        }

        private void Komitenti_Load(object sender, EventArgs e)
        {
            string connString =
          ConfigurationManager.AppSettings["webs"];

            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT KomintentID, NazivKomintenta, Adresa FROM SysKomintenti", conn);

                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(dataTableRes);
                dataGridView1.DataSource = dataTableRes;
                DataGridViewColumn column = dataGridView1.Columns[0];
                dataGridView1.Columns[0].HeaderText = "Komitent ID";
                dataGridView1.Columns[0].Width = 50;

                DataGridViewColumn column2 = dataGridView1.Columns[1];
                dataGridView1.Columns[1].HeaderText = "Naziv komitenta";
                dataGridView1.Columns[1].Width = 450;

                DataGridViewColumn column3 = dataGridView1.Columns[2];
                dataGridView1.Columns[2].HeaderText = "Adresa";
                dataGridView1.Columns[2].Width = 300;
                conn.Close();
            }
        }

        private void btnPrenos_Click(object sender, EventArgs e)
        {
            InsertKomitenti ins = new InsertKomitenti();
            ins.DeleteSysKomitenti();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                
                if (row != null && row.Cells[0].Value != null)
                {
                    ins.InsertKom(Convert.ToInt32(row.Cells[0].Value.ToString()), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString());
                }
            }
            MessageBox.Show("Uspešno preneti komitenti");
        }
    }
}
