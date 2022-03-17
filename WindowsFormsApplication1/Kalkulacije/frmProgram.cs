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

namespace WindowsFormsApplication1.Kalkulacije
{
    public partial class frmProgram : Form
    {
        bool status = false;
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public frmProgram()
        {
            InitializeComponent();
            FillGV();
            txt_ID.Enabled = false;
        }
        private void FillGV()
        {
            var query = "Select * from Program";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            tsNew.Enabled = false;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                Kalkulacije.InsertProgram program = new InsertProgram();
                program.InsProgram(txt_Naziv.Text.ToString().TrimEnd());
                status = false;
                tsNew.Enabled = true;
            }
            else
            {
                Kalkulacije.InsertProgram program = new InsertProgram();
                program.UpdProgram(Convert.ToInt32(txt_ID.Text), txt_Naziv.Text.ToString().TrimEnd());
            }
            FillGV();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            Kalkulacije.InsertProgram program = new InsertProgram();
            program.DelProgram(Convert.ToInt32(txt_ID.Text));
            FillGV();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach(DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txt_ID.Text = row.Cells[0].Value.ToString();
                        txt_Naziv.Text = row.Cells[1].Value.ToString().TrimEnd();
                    }
                }
            }
            catch { }
        }
    }
}
