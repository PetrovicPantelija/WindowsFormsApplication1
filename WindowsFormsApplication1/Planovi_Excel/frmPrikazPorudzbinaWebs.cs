using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Planovi_Excel
{
    public partial class frmPrikazPorudzbinaWebs : Form
    {
        public frmPrikazPorudzbinaWebs()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connString =
          ConfigurationManager.AppSettings["webs"];

            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                //Panta E fakture
                // OleDbCommand cmd = new OleDbCommand("SELECT * FROM RkShipping where ExportLargo = false and RkShipping.VoziloIDz  = (Select Min(VoziloID) from TrVozila where TrVozila.RegistarskiBroj = '" + cboVozila.Text + "') and DatumIstovara =  CDate('" + cboDatumUtovara.Text + "')", conn);

                OleDbCommand cmd = new OleDbCommand("SELECT * FROM RkShippingExportPorudzbina ", conn);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                adapter.Fill(dataTableRes);
                dataGridView1.DataSource = dataTableRes;

                /*DataGridViewColumn column = dataGridView1.Columns[0];
                dataGridView1.Columns[0].HeaderText = "Isporuka ID";
                dataGridView1.Columns[0].Width = 50;

                DataGridViewColumn column2 = dataGridView1.Columns[1];
                dataGridView1.Columns[1].HeaderText = "Barkod isporuke";
                dataGridView1.Columns[1].Width = 150;

                DataGridViewColumn column3 = dataGridView1.Columns[2];
                dataGridView1.Columns[2].HeaderText = "Datum utovara";
                dataGridView1.Columns[2].Width = 100;

                DataGridViewColumn column4 = dataGridView1.Columns[3];
                dataGridView1.Columns[3].HeaderText = "Datum istovara";
                dataGridView1.Columns[3].Width = 100;

                DataGridViewColumn column5 = dataGridView1.Columns[4];
                dataGridView1.Columns[4].HeaderText = "Broj utovara";
                dataGridView1.Columns[4].Width = 70;

                DataGridViewColumn column6 = dataGridView1.Columns[5];
                dataGridView1.Columns[5].HeaderText = "Broj istovara";
                dataGridView1.Columns[5].Width = 70;

                DataGridViewColumn column7 = dataGridView1.Columns[6];
                dataGridView1.Columns[6].HeaderText = "Broj palete";
                dataGridView1.Columns[6].Width = 70;

                DataGridViewColumn column8 = dataGridView1.Columns[7];
                dataGridView1.Columns[7].HeaderText = "Kupac";
                dataGridView1.Columns[7].Width = 70;

                DataGridViewColumn column9 = dataGridView1.Columns[8];
                dataGridView1.Columns[8].HeaderText = "Salon";
                dataGridView1.Columns[8].Width = 70;

                DataGridViewColumn column10 = dataGridView1.Columns[9];
                dataGridView1.Columns[9].HeaderText = "Vozilo";
                dataGridView1.Columns[9].Width = 70;

                DataGridViewColumn column11 = dataGridView1.Columns[10];
                dataGridView1.Columns[10].HeaderText = "Priključno vozilo";
                dataGridView1.Columns[10].Width = 70;

                DataGridViewColumn column12 = dataGridView1.Columns[11];
                dataGridView1.Columns[11].HeaderText = "Broj paketa";
                dataGridView1.Columns[11].Width = 70;

                DataGridViewColumn column13 = dataGridView1.Columns[12];
                dataGridView1.Columns[12].HeaderText = "Štampa";
                dataGridView1.Columns[12].Width = 70;

                DataGridViewColumn column14 = dataGridView1.Columns[13];
                dataGridView1.Columns[13].HeaderText = "Export Largo";
                dataGridView1.Columns[13].Width = 70;

                DataGridViewColumn column15 = dataGridView1.Columns[14];
                dataGridView1.Columns[14].HeaderText = "Export Largo - Može";
                dataGridView1.Columns[14].Width = 70;
                */
                conn.Close();
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

            dtpVremeOd.Value = DateTime.Now;

                InsertPorudzbina porudzbina = new InsertPorudzbina();

            porudzbina.InsertPor(dtpVremeOd.Text);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // porudzbina.InsertPorudzbinaPostav(Convert.ToInt32(LargoPaketID), Convert.ToDecimal(Kolicina));
                porudzbina.InsertPorudzbinaPostav(Convert.ToInt32(row.Cells[3].Value.ToString()), Convert.ToInt32(row.Cells[5].Value.ToString()));
               
            }
            MessageBox.Show("Zavrsen uvoz porudzbine");

          
            string NazivBeleske = DateTime.Now.ToString() + '-' + "Porudzbina";
            MessageBox.Show(NazivBeleske);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // porudzbina.InsertPorudzbinaPostav(Convert.ToInt32(LargoPaketID), Convert.ToDecimal(Kolicina));
                porudzbina.InsertBeleskaPostav(NazivBeleske, Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[2].Value.ToString(), Convert.ToInt32(row.Cells[3].Value.ToString()), row.Cells[4].Value.ToString(), Convert.ToDouble(row.Cells[5].Value.ToString()));
               
            }
            MessageBox.Show("Zavrsen uvoz porudzbine");

        }
    }
}