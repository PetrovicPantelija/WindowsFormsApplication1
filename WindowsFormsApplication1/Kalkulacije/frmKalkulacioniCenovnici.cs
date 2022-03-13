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
    public partial class frmKalkulacioniCenovnici : Form
    {
        bool status = false;
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public frmKalkulacioniCenovnici()
        {
            InitializeComponent();
            FillGV();
            txt_ID.Enabled = false;
        }
        private void FillGV()
        {
            var query = "Select * From KalkulacioniCenovnici";
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
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                Kalkulacije.InsertKalkulacioniCenovnik kalk = new InsertKalkulacioniCenovnik();
                kalk.InsKalCenovnici(txt_Naziv.Text.ToString().TrimEnd());
                status = false;
            }
            else
            {
                Kalkulacije.InsertKalkulacioniCenovnik kalk = new InsertKalkulacioniCenovnik();
                kalk.UpdKalCenovnici(Convert.ToInt32(txt_ID.Text), txt_Naziv.Text.ToString().TrimEnd());
            }
            FillGV();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            Kalkulacije.InsertKalkulacioniCenovnik kalk = new InsertKalkulacioniCenovnik();
            kalk.DelKalCenovnici(Convert.ToInt32(txt_ID.Text));
            FillGV();
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
    }
}
