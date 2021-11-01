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
    public partial class Partneri : Form
    {
        public Partneri()
        {
            InitializeComponent();
        }

        private void Partneri_Load(object sender, EventArgs e)
        {
            string connString =
            ConfigurationManager.AppSettings["webs"];

            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM SysKomintenti", conn);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                adapter.Fill(dataTableRes);
                dataGridView1.DataSource = dataTableRes;
                conn.Close();
            }
        }

        private void btnPrenos_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                Nedra.InsertDB ins = new Nedra.InsertDB();
                if (row != null && row.Cells[0].Value != null)
                {
                ins.InsertPartnerji(Convert.ToInt32(row.Cells[0].Value.ToString()),row.Cells[4].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[6].Value.ToString(), row.Cells[5].Value.ToString(), row.Cells[9].Value.ToString(), row.Cells[10].Value.ToString(), row.Cells[11].Value.ToString(),row.Cells[12].Value.ToString());
                }//InsertPartnerji(int PaSifra, string NazivKomintenta, string PIBKomintenta, string MaticniBrojKomintenta, int PostanskiBrojIDz, string Adresa, string Telefon, string Fax, string WWWAdresa, string TekuciRacun)
            }
        }
    }
}
