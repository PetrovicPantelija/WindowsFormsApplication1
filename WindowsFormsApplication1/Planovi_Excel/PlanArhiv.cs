using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Planovi_Excel
{
    public partial class PlanArhiv : Form
    {
        string Kor = frmLogovanje.user.ToString();
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        private bool status = false;
        public int ID;

        public PlanArhiv()
        {
            InitializeComponent();
            if (Kor.Trim() == "stanojevic.ivan" || Kor.Trim() == "stanojlovic.djordje" || Kor.Trim() == "hasanovic.igor" || Kor.Trim() == "pantelija")
            {
                button1.Enabled = true;

            }
            else
            {
                button1.Enabled = false;

            }
        }

        private void FillGV()
        {
            var query = "Select * from PlanArhiv order by ID desc";
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
            Planovi_Excel.PlanArhivDetaljno frm = new PlanArhivDetaljno(Convert.ToInt32(txtID.Text.ToString()));
            frm.Show();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            status = false;
            // DeletePlanSaStavkama
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                RNPlanIzvestaj frm = new RNPlanIzvestaj(Convert.ToInt32(txtID.Text.ToString().TrimEnd()));
                frm.Show();
            }
            else
            {
                MessageBox.Show("Izaberite plan");
            }
            
        }
    }
}