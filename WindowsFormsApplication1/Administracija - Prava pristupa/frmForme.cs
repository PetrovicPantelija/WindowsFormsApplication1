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

namespace WindowsFormsApplication1.Administracija___Prava_pristupa
{
    public partial class frmForme : Form
    {
        bool status = false;
        public string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public frmForme()
        {
            InitializeComponent();
            txt_ID.Enabled = false;
            txt_IDForme.Enabled = false;
            FillGV();
        }
        private void FillGV()
        {
            var query = "Select * from Forme";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].Width = 170;
            dataGridView1.Columns[3].Width = 130;
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
                        txt_IDForme.Text = row.Cells[1].Value.ToString();
                        txt_Naziv.Text = row.Cells[2].Value.ToString().TrimEnd();
                        txt_Code.Text = row.Cells[3].Value.ToString().TrimEnd();
                    }
                }
            }
            catch { }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txt_ID.Text = "";
            txt_IDForme.Text = "";
            status = true;
            tsNew.Enabled = false;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertForme forme = new InsertForme();
            if (status == true)
            {
                forme.InsForme(txt_Naziv.Text.ToString().TrimEnd(), txt_Code.Text.ToString().TrimEnd());
                status = false;
                tsNew.Enabled = true;
            }
            else
            {
                forme.UpdForme(Convert.ToInt32(txt_ID.Text), Convert.ToInt32(txt_IDForme.Text), txt_Naziv.Text.ToString().TrimEnd(), txt_Code.Text.ToString().TrimEnd());
            }
            FillGV();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertForme forme = new InsertForme();
            forme.DelForme(Convert.ToInt32(txt_ID.Text));
            FillGV();
        }
    }
}
