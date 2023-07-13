using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Planovi_Excel
{
    public partial class PlanAktiv : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        private bool status = false;
        public int ID;

        public PlanAktiv()
        {
            InitializeComponent();
        }

        private void FillGV()
        {
            var query = "Select * from PlanAktiv order by ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void PlanAktiv_Load(object sender, EventArgs e)
        {
            FillGV();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                Planovi_Excel.InsertPlanovi ins = new InsertPlanovi();
                ins.InsertPlan(txtNaziv.Text.ToString().TrimEnd(), Convert.ToDateTime(dateTimePicker1.Value.ToString("dd.MM.yyyy")), Convert.ToDateTime(dateTimePicker2.Value.ToString("dd.MM.yyyy")),
                    txtNapomena.Text.ToString().TrimEnd(), "Aktivan");
                status = false;
            }
            else
            {
                Planovi_Excel.InsertPlanovi ins = new InsertPlanovi();
                ins.UpdatePlakAktiv(Convert.ToInt32(txtID.Text), txtNaziv.Text.ToString().TrimEnd(), Convert.ToDateTime(dateTimePicker1.Value.ToString("dd.MM.yyyy")), Convert.ToDateTime(dateTimePicker2.Value.ToString("dd.MM.yyyy")),
                    txtNapomena.Text.ToString().TrimEnd());
            }
            FillGV();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    txtID.Text = row.Cells[0].Value.ToString();
                    txtNaziv.Text = row.Cells[1].Value.ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(row.Cells[2].Value);
                    dateTimePicker2.Value = Convert.ToDateTime(row.Cells[3].Value);
                    txtNapomena.Text = row.Cells[4].Value.ToString();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ID = Convert.ToInt32(txtID.Text);
            PlanStavke frm = new PlanStavke(ID);
            frm.Show();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PoveziRN frm = new PoveziRN();
            frm.Show();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            Planovi_Excel.InsertPlanovi ins = new InsertPlanovi();
            ins.DeletePlanSaStavkama(Convert.ToInt32(txtID.Text));
            FillGV();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int stariplan = Convert.ToInt32(txtID.Text);
            int noviPlan = 0;
            InsertPlanovi ins = new InsertPlanovi();
            ins.InsertPlan(txtNaziv.Text.ToString().TrimEnd(), DateTime.Now, DateTime.Now, txtNapomena.Text.ToString().TrimEnd(), "Aktivan");

            string query = "(select (Max(t1.ID)) from (select ID from PlanArhiv union Select ID from PlanAktiv) as t1 )";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                noviPlan = Convert.ToInt32(dr[0].ToString());
            }
            conn.Close();
            var query2 = "Select IDPrograma,IDKolekcije,MPSifra From PLanAktivStavke Where IDPLana=" + stariplan;
            conn.Open();
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                ins.InsertPlanStavke(noviPlan, Convert.ToInt32(dr2["IDPrograma"].ToString()), Convert.ToInt32(dr2["IdKolekcije"].ToString()),
                    Convert.ToInt32(dr2["MpSifra"].ToString()));
            }
            conn.Close();
            FillGV();
        }
    }
}