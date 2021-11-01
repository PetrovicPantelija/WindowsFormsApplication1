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
    public partial class Reklamacije : Form
    {
        public Reklamacije()
        {
            InitializeComponent();
        }

        private void Reklamacije_Load(object sender, EventArgs e)
        {
            string connString =
          ConfigurationManager.AppSettings["webs"];
            
            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM NalogZaUtovarReklamacije", conn);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                adapter.Fill(dataTableRes);
                dataGridView1.DataSource = dataTableRes;
                conn.Close();
            }
           
        }

        private void btnPoDatumuIVozilu_Click(object sender, EventArgs e)
        {

            string connString =
            ConfigurationManager.AppSettings["webs"];
            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM NalogZaUtovarReklamacije where Vozilo = " + "'" + txtVozilo.Text + "' and DatumIsporuke =" + "DateValue('" + txtDatumIsporuke.Text + "')", conn);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                adapter.Fill(dataTableRes);
            }

            dataGridView1.DataSource = dataTableRes;

        }

        private void btnInsertNakNalog_Click(object sender, EventArgs e)
        {
            string constring = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            int BrojNakNalog = 0;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("SelectNakNAlog", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MaxBroj", SqlDbType.Int);
                    cmd.Parameters["@MaxBroj"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    BrojNakNalog = Convert.ToInt32(cmd.Parameters["@MaxBroj"].Value.ToString());
                }
            }

            // Unosstavki
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // currQty += row.Cells["qty"].Value;
                //More code here
                if (row != null && row.Cells[0].Value != null)
                {

                    InsertNakNalogR innp = new InsertNakNalogR();
                    //string kod = row.Cells[7].Value.ToString() + "-" + row.Cells[15].Value.ToString() + "-" + row.Cells[17].Value.ToString();
                    innp.InsertNakNalogRV(BrojNakNalog, row.Cells[4].Value.ToString(), "KOM", Convert.ToDouble(row.Cells[2].Value.ToString()),row.Cells[5].Value.ToString(), row.Cells[10].Value.ToString(), Convert.ToInt32(row.Cells[16].Value.ToString()), row.Cells[17].Value.ToString(), Convert.ToInt32(row.Cells[18].Value.ToString()), row.Cells[19].Value.ToString(), row.Cells[11].Value.ToString(), row.Cells[7].Value.ToString());

                }
            }
   
        }
    }
}
