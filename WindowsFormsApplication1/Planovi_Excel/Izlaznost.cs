using Aspose.Cells;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Planovi_Excel
{
    public partial class Izlaznost : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public Izlaznost()
        {
            InitializeComponent();
        }

        private void FillGV()
        {
            var query = "Select ID,Izlaznost.MPSifra,MpStaraSif,MpNaziv,Mesec,Godina,Kolicina " +
            "From Izlaznost " +
            "Inner join MaticniPodatki on Izlaznost.MpSifra=MaticniPodatki.MpSifra " +
            "Order by ID asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);

            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["ID"].Width = 65;
        }

        private void Izlaznost_Load(object sender, EventArgs e)
        {
            FillGV();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int mesec;
            int godina;
            mesec = Convert.ToInt32(dateTimePicker1.Value.Month);
            godina = Convert.ToInt32(dateTimePicker1.Value.Year);
            InsertPlanovi ins = new InsertPlanovi();
            ins.ArhivirajIzlaznost(mesec, godina);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var query = "Select ID,Izlaznost.MPSifra,MpStaraSif,MpNaziv,Mesec,Godina,Kolicina " +
                        "From Izlaznost " +
                        "Inner join MaticniPodatki on Izlaznost.MpSifra=MaticniPodatki.MpSifra " +
                        "Order by ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);

            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["ID"].Width = 65;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertPlanovi ins = new InsertPlanovi();
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    ins.DeleteIzlaznost(Convert.ToInt32(row.Cells["ID"].Value));
                }
            }
            FillGV();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int numRows = dataGridView2.Rows.Count;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "ODS files (*.ods)|*.ods";
                openFileDialog.Title = "Select an ODS File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    LoadOdsData(openFileDialog.FileName);

                }
            }
           
        }
        private void LoadOdsData(string filePath)
        {
            try
            {
                Workbook workbook = new Workbook(filePath);
                Worksheet worksheet = workbook.Worksheets[0];
                Cells cells = worksheet.Cells;

                DataTable dataTable = new DataTable();

                // Read header row
                int maxColumn = cells.MaxColumn + 1;  // Adjust for zero-based index
                int maxRow = cells.MaxRow + 1;        // Adjust for zero-based index

                for (int i = 0; i < maxColumn; i++)
                {
                    // Check if header cell is not empty
                    string headerValue = cells[0, i]?.StringValue;
                    if (!string.IsNullOrEmpty(headerValue))
                    {
                        dataTable.Columns.Add(headerValue);
                    }
                }

                // Read data rows
                for (int i = 1; i < maxRow; i++)
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int j = 0; j < maxColumn; j++)
                    {
                        // Avoid index out of range by checking column existence
                        if (j < dataTable.Columns.Count)
                        {
                            dataRow[j] = cells[i, j]?.StringValue;
                        }
                    }
                    dataTable.Rows.Add(dataRow);
                }

                // Bind the DataTable to the DataGridView
                dataGridView2.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading file: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InsertPlanovi ins = new InsertPlanovi();
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells["ID"].ToString() != "")
                {
                    ins.UpisiIzlaznostRucno(Convert.ToInt32(row.Cells[0].Value), Convert.ToInt32(row.Cells[1].Value), Convert.ToInt32(row.Cells[2].Value),
                        Convert.ToInt32(row.Cells[3].Value), Convert.ToDecimal(row.Cells[4].Value));
                }
            }
            FillGV();
        }
    }
}