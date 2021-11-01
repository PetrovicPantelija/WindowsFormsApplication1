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
    public partial class Saloni : Form
    {
        public Saloni()
        {
            InitializeComponent();
        }

        private void Saloni_Load(object sender, EventArgs e)
        {
            string connString =
           ConfigurationManager.AppSettings["webs"];

            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT KomintentIDz, SifraKomintentaMestoIsporuke, MestoIsporuke, MestoIsporukeAdresa FROM SysKomintentiMestoIsporuke", conn);

                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(dataTableRes);
                dataGridView1.DataSource = dataTableRes;
                dataGridView1.DataSource = dataTableRes;
                DataGridViewColumn column = dataGridView1.Columns[0];
                dataGridView1.Columns[0].HeaderText = "Komitent ID";
                dataGridView1.Columns[0].Width = 50;

                DataGridViewColumn column2 = dataGridView1.Columns[1];
                dataGridView1.Columns[1].HeaderText = "Šifra Salona";
                dataGridView1.Columns[1].Width = 50;

                DataGridViewColumn column3 = dataGridView1.Columns[2];
                dataGridView1.Columns[2].HeaderText = "Salon";
                dataGridView1.Columns[2].Width = 300;

                DataGridViewColumn column4 = dataGridView1.Columns[3];
                dataGridView1.Columns[3].HeaderText = "Adresa Salona";
                dataGridView1.Columns[3].Width = 300;
                conn.Close();
            }
        }

        private void btnPrenos_Click(object sender, EventArgs e)
        {
            InsertSaloni ins = new InsertSaloni();
            ins.DeleteSaloni();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row != null && row.Cells[0].Value != null)
                {
                    ins.InsertSal(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString());
                }
            }
            MessageBox.Show("Uspešno preneti saloni");
        }
    }
}
