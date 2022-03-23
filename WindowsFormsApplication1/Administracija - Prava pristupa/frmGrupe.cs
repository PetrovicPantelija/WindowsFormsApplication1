using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Administracija___Prava_pristupa
{
    public partial class frmGrupe : Form
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public static string code = "frmGrupe";
        bool status = false;
        public frmGrupe()
        {
            InitializeComponent();
            txt_ID.Enabled = false;
            txt_IDGrupe.Enabled = false;
        }
        private void frmGrupe_Load(object sender, EventArgs e)
        {
            FillGV();
        }
        private void FillGV()
        {
            var query = "Select * from Grupe";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[1].Width = 70;
            dataGridView1.Columns[2].Width = 190;
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
                        txt_IDGrupe.Text = row.Cells[1].Value.ToString();
                        txt_Naziv.Text = row.Cells[2].Value.ToString();
                    }
                }
            }
            catch { }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txt_ID.Text = "";
            txt_IDGrupe.Text = "";
            status = true;
            tsNew.Enabled = false;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertGrupe grupe = new InsertGrupe();
            if (status == true)
            {
                grupe.InsGrupe(txt_Naziv.Text.ToString().TrimEnd());
                status = false;
                tsNew.Enabled = true;
            }
            else
            {
                grupe.UpdGrupe(Convert.ToInt32(txt_ID.Text), Convert.ToInt32(txt_IDGrupe.Text), txt_Naziv.Text.ToString().TrimEnd());
            }
            FillGV();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertGrupe grupa = new InsertGrupe();
            grupa.DelGrupe(Convert.ToInt32(txt_ID.Text));
            FillGV();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmDodeliGrupu dodeli = new frmDodeliGrupu();
            dodeli.Show();
        }
    }
}
