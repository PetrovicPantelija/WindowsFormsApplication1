using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Planovi_Excel
{
    public partial class PoveziRN : Form
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public PoveziRN()
        {
            InitializeComponent();
        }
        private void PoveziRN_Load(object sender, EventArgs e)
        {
            FillGV();
        }
        private void FillGV()
        {
            var query = "Select ID,RTrim(Naziv) as Naziv,DatumOd,DatumDo From PlanArhiv order by ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);

            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 60;

            var query2 = "select DnPStZapisa as [Sifra RN], DNPStevilka as [Broj RN],DNpSifVrsteDN as [Vrsta RN],DNpSifStat as [Status],IDPlana from DelNalogProizvodni " +
                "where (DNpSifVrsteDN=40 or DNpSifVrsteDN=41) AND DNpSifStat IN ('OD','LA','PO','PF','PL') order by DNpStZapisa desc";
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds2.Tables[0];
            dataGridView2.Columns[0].Width = 60;
            dataGridView2.Columns[1].Width = 60;
            dataGridView2.Columns[2].Width = 60;
            dataGridView2.Columns[3].Width = 60;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertPlanovi ins = new InsertPlanovi();
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        ins.DodeliPlan(Convert.ToInt32(textBox1.Text), Convert.ToInt32(row.Cells[0].Value));
                    }
                }
            }
            catch { }
            FillGV();
        }
        string Naziv = "";
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    textBox1.Text = row.Cells[0].Value.ToString();
                    Naziv = row.Cells[1].Value.ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertPlanovi ins = new InsertPlanovi();
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        ins.IskljuciPlan(Convert.ToInt32(row.Cells[0].Value));
                    }
                }
            }
            catch { }
            FillGV();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] path = { Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Plan-RN-" };
            string folder = Path.Combine(path);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                ShouldQuote = (field) => false,
                Delimiter = ";"
            };
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            var query = "Select '' as a,'' as b,'' as c,PlanArhivStavke.MpSifra,RTrim(MaticniPodatki.MpStaraSif) as MpStaraSif,'','',UsvojenaOdluka from PlanArhivStavke Inner join MaticniPodatki on PlanArhivStavke.MpSifra=MaticniPodatki.MpSifra Where IDPlana=" + Convert.ToInt32(textBox1.Text) + " order by PlanArhivStavke.ID";
            SqlCommand cmd = new SqlCommand(query, conn);
            string file = folder + Naziv + ".csv";
            try
            {
                using (var textWriter = File.CreateText(file))
                {
                    using (CsvWriter csv = new CsvWriter(textWriter, config))
                    {
                        DataTable dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());
                        int count = 1110;
                        foreach (DataRow row in dt.Rows)
                        {
                            count++;
                            csv.WriteField(count);
                            for (var i = 0; i < dt.Columns.Count; i++)
                            {
                                csv.WriteField(row[i]);
                            }
                            csv.NextRecord();
                        }
                    }
                }
                MessageBox.Show("Kreiran dokument", "CSV");
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "CSV"); }
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string[] path = { Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Plan-RN-" };
            string folder = Path.Combine(path);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                ShouldQuote = (field) => false,
                Delimiter = ";"
            };
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Nije odabran plan", "CSV");
                return;
            }
            var query = "Select '' as a,'' as b,'' as c,PlanArhivStavke.MpSifra,RTrim(MaticniPodatki.MpStaraSif) as MpStaraSif,'','',UsvojenaOdluka from PlanArhivStavke Inner join MaticniPodatki on PlanArhivStavke.MpSifra=MaticniPodatki.MpSifra Where IDPlana=" + Convert.ToInt32(textBox1.Text) + " order by PlanArhivStavke.ID";
            SqlCommand cmd = new SqlCommand(query, conn);
            string file = folder + Naziv + ".csv";
            try
            {
                using (var textWriter = File.CreateText(file))
                {
                    using (CsvWriter csv = new CsvWriter(textWriter, config))
                    {
                        DataTable dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());
                        int count = Convert.ToInt32(textBox1.Text);

                        foreach (DataRow row in dt.Rows)
                        {

                            csv.WriteField(count);
                            for (var i = 0; i < dt.Columns.Count; i++)
                            {
                                csv.WriteField(row[i]);
                            }
                            csv.NextRecord();
                        }
                    }

                }

                MessageBox.Show("Kreiran dokument", "CSV");
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "CSV"); }
            conn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InsertPlanovi ins = new InsertPlanovi();
           
            ins.InsertNetoPotrebeIgor(Convert.ToInt32(textBox1.Text));

            MessageBox.Show("Završene Neto potrebe");
               
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Neto_potrebe.NetoPotrebe np = new Neto_potrebe.NetoPotrebe();
            np.Show();
        }
    }
    }

