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
    public partial class ReferencijalnaTabela : Form
    {
        public ReferencijalnaTabela()
        {
            InitializeComponent();
        }

        public ReferencijalnaTabela(string DolazecaForma)
        {
            InitializeComponent();
           /// ReferencijalnaTabela.Text = "" + "/" + DolazecaForma;
        }

        private void ReferencijalnaTabela_Load(object sender, EventArgs e)
        {
            string connString =
          ConfigurationManager.AppSettings["webs"];

            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM RkShippingLinkWebsLargo", conn);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                adapter.Fill(dataTableRes);
                dataGridView1.DataSource = dataTableRes;
                conn.Close();
            }

        }

        private void btnInsertRefTabela_Click(object sender, EventArgs e)
        {
            InsertReferencijalnaTabela irt = new InsertReferencijalnaTabela();
            irt.DeleteRefTab();
            
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // currQty += row.Cells["qty"].Value;
                //More code here
                if (row != null && row.Cells[0].Value != null)
                {
                   


                    irt.InsertRefTab(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), Convert.ToInt32(row.Cells[2].Value.ToString()), Convert.ToInt32(row.Cells[3].Value.ToString()), Convert.ToInt32(row.Cells[4].Value.ToString()), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString(), row.Cells[7].Value.ToString(), row.Cells[8].Value.ToString(), row.Cells[9].Value.ToString(), row.Cells[10].Value.ToString(), row.Cells[11].Value.ToString(), Convert.ToInt32(row.Cells[12].Value.ToString()), row.Cells[13].Value.ToString(), Convert.ToInt32(row.Cells[14].Value.ToString()), row.Cells[15].Value.ToString());
                }
            }
        }
    }
}
