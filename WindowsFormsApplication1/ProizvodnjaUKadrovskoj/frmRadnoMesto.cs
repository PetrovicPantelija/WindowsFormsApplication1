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

namespace WindowsFormsApplication1.ProizvodnjaUKadrovskoj
{
    public partial class frmRadnoMesto: Form
    {
        bool status = false;
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public frmRadnoMesto()
        {
            InitializeComponent();
            txt_ID.Enabled = false;
        }
        private void FillGV()
        {
            var query = "SELECT RadnaMesta.ID as ID,RadnaMesta.Naziv as Naziv " +
                        "FROM RadnaMesta " +
                        "Order by RadnaMesta.ID asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            if (dataGridView1.Columns.Contains("ID"))
                dataGridView1.Columns["ID"].Width = 60;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txt_ID.Text = row.Cells[0].Value.ToString();
                        txt_Naziv.Text = row.Cells[1].Value.ToString().TrimEnd();
                    }
                }
            }
            catch
            {

            }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txt_Naziv.Text = "";
            txt_ID.Text = "";

        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (txt_Naziv.Text.Length > 0)
            {
                if (status == true)
                {
                    ProizvodnjaUKadrovskoj.InsertRadnaMesta kol = new InsertRadnaMesta();
                    kol.InsRadnaMesta(txt_Naziv.Text.ToString().TrimEnd());
                    status = false;
                    txt_Naziv.Text = "";
                }
                else
                {
                    ProizvodnjaUKadrovskoj.InsertRadnaMesta kol = new InsertRadnaMesta();
                    kol.UpdRadnaMesta(Convert.ToInt32(txt_ID.Text), txt_Naziv.Text.ToString().TrimEnd());
                }
            }
            else
            {
                MessageBox.Show("Niste uneli naziv radnog mesta.");
            }
            FillGV();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            if (txt_ID.Text.Length > 0)
            {
                ProizvodnjaUKadrovskoj.InsertRadnaMesta kol = new InsertRadnaMesta();
                kol.DelRadnaMesta(Convert.ToInt32(txt_ID.Text));
                txt_Naziv.Text = "";
                txt_ID.Text = "";
                FillGV();
            }
        }

        private void frmRadnoMesto_Load(object sender, EventArgs e)
        {
            FillGV();
        }
    }
}
