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
    public partial class PoveziRN : Form
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public PoveziRN()
        {
            InitializeComponent();
        }
        private void PoveziRN_Load(object sender, EventArgs e)
        {
            FillGV();
        }
        private void FillGV()
        {
            var query = "Select ID,RTrim(Naziv) as Naziv,DatumOd,DatumDo From PlanAktiv order by ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);

            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 60;

            var query2 = "select DnpStZapisa as [RN],DNpSifVrsteDN as [Vrsta RN],DNpSifStat as [Status],IDPlana from DelNalogProizvodni " +
                "where (DNpSifVrsteDN=40 or DNpSifVrsteDN=41) AND DNpSifStat IN ('OD','LA','PO','PF','PL') order by DNpStZapisa desc";
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds2.Tables[0];
            dataGridView2.Columns[3].Width = 60;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertPlanovi ins = new InsertPlanovi();
            try
            {
                foreach(DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        ins.DodeliPlan(Convert.ToInt32(textBox1.Text), Convert.ToInt32(row.Cells[0].Value));
                    }
                }
            }
            catch { }
            FillGV();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    textBox1.Text = row.Cells[0].Value.ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertPlanovi ins = new InsertPlanovi();
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        ins.IskljuciPlan(Convert.ToInt32(row.Cells[0].Value));
                    }
                }
            }
            catch { }
            FillGV();
        }
    }
}
