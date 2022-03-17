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
    public partial class frmKolekcije : Form
    {
        bool status = false;
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public frmKolekcije()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
            txt_ID.Enabled = false;
        }
        private void FillGV()
        {
            var query = "Select Kolekcije.ID as ID,Kolekcije.Naziv as Naziv,Kolekcije.ProgramID as ProgramID,Program.Naziv as Program " +
                "from Kolekcije " +
                "Inner join Program on Kolekcije.ProgramID = Program.ID";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void FillCombo()
        {
            var query = " Select * from Program";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            combo_Program.DataSource = ds.Tables[0];
            combo_Program.DisplayMember = "Naziv";
            combo_Program.ValueMember = "ID";
        }
        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                Kalkulacije.InsertKolekcije kol = new InsertKolekcije();
                kol.InsKolekcije(txt_Naziv.Text.ToString().TrimEnd(),Convert.ToInt32(combo_Program.SelectedValue));
                status = false;
            }
            else
            {
                Kalkulacije.InsertKolekcije kol = new InsertKolekcije();
                kol.UpdKolekcije(Convert.ToInt32(txt_ID.Text), txt_Naziv.Text.ToString().TrimEnd(),Convert.ToInt32(combo_Program.SelectedValue));
            }
            FillGV();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            Kalkulacije.InsertKolekcije kol = new InsertKolekcije();
            kol.DelKolekcije(Convert.ToInt32(txt_ID.Text));
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
                        combo_Program.Text = row.Cells[3].Value.ToString();
                    }
                }
            }
            catch
            {

            }
        }
    }
}
