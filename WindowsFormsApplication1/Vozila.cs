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
    public partial class Vozila : Form
    {
        public Vozila()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Vozila_Load(object sender, EventArgs e)
        {
            string connString =
            ConfigurationManager.AppSettings["webs"];

            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM TrVozila", conn);

                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(dataTableRes);
                dataGridView1.DataSource = dataTableRes;
                conn.Close();
            }
        }

        private void btnPrenos_Click(object sender, EventArgs e)
        {
            InsertVozilo ins = new InsertVozilo();
            ins.DeleteVozila();
            ins.DeleteVozilaLargo();
            
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                int transport = 0;
                int prikljvoz = 0;
                
                if (row != null && row.Cells[0].Value != null)
                {
                    if (row.Cells[8].Value.ToString() == "true")
                    { transport = 1; }
                    else
                    {
                        transport = 0;
                    }
                    if (row.Cells[4].Value.ToString() == "true")
                    { prikljvoz = 1; }
                    else
                    {
                        prikljvoz = 0;
                    }
                    ins.InsertVoz(Convert.ToInt32(row.Cells[0].Value.ToString()), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), prikljvoz, Convert.ToInt32(row.Cells[5].Value.ToString()),    Convert.ToInt32(row.Cells[6].Value.ToString()), Convert.ToInt32(row.Cells[7].Value.ToString()), transport);
                    ins.InsertVozLargo(Convert.ToInt32(row.Cells[0].Value.ToString()), row.Cells[1].Value.ToString());
                }
            }
            MessageBox.Show("Vozila uspešno preneta");
        }
    }
}
