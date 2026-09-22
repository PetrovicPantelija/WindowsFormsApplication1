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
        public static string code = "Izlaznost";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";

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
            IdGrupe();
            IdForme();
            PravoPristupa();
            if (Pravo == false)
            {
                this.Close();
                return;
            }

            FillGV();
        }

        public string IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            //Sifarnici.frmLogovanje frm = new Sifarnici.frmLogovanje();         
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            int count = 0;

            while (dr.Read())
            {
                if (count == 0)
                {
                    niz = dr["IdGrupe"].ToString();
                    count++;
                }
                else
                {
                    niz = niz + "," + dr["IdGrupe"].ToString();
                    count++;
                }

            }
            conn.Close();
            return niz;
        }

        private int IdForme()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select IdForme from Forme where Rtrim(Code)=" + "'" + code + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idForme = Convert.ToInt32(dr["IdForme"].ToString());
            }
            conn.Close();
            return idForme;
        }

        private void PravoPristupa()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select * From GrupaForma Where IdGrupe in (" + niz + ") and IdForme=" + idForme;
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nemate prava za pristup ovoj formi", code);
                Pravo = false;
            }
            else
            {
                Pravo = true;
                while (reader.Read())
                {
                    insert = Convert.ToBoolean(reader["Upis"]);
                    if (insert == false)
                    {
                        //tsNew.Enabled = false;
                    }
                    update = Convert.ToBoolean(reader["Izmena"]);
                    if (update == false)
                    {
                        //tsSave.Enabled = false;
                    }
                    delete = Convert.ToBoolean(reader["Brisanje"]);
                    if (delete == false)
                    {
                        //tsDelete.Enabled = false;
                    }
                }
            }

            conn.Close();
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