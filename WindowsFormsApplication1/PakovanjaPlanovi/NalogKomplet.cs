using Microsoft.Reporting.WinForms;
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
using System.Web.Configuration;
using System.Windows.Forms;

namespace WindowsFormsApplication1.PakovanjaPlanovi
{

    public partial class NalogKomplet : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        int plan;
        int kompletirano;
        public NalogKomplet(int IDPlana)
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            plan=IDPlana;
            kompletirano = 0;
            FillCkList();
            FillGV();
        }
        string pomMagacini;
        private void FillCkList()
        {
            var query = "select distinct SkSifra,SkNaziv " +
                "from sklad " +
                "inner join MaticniPodatki on sklad.SkSifra=MaticniPodatki.MpOsnSklad " +
                "inner join PregledStavkiPlanaPakovanja on MaticniPodatki.MpSifra=PregledStavkiPlanaPakovanja.ArtikalIzPrvogNivoaSastavnice " +
                "inner join StavkePlanaPakovanja on PregledStavkiPlanaPakovanja.IDStavkePlanaPakovanja=StavkePlanaPakovanja.ID " +
                "Where IDPlanPakovanja=" + plan;
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            checkedListBox1.DataSource = ds.Tables[0];
            checkedListBox1.DisplayMember = "SkNaziv";
            checkedListBox1.ValueMember = "SkSifra";

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }
        private void GetChecked()
        {
            List<int> magacini = new List<int>();
            foreach (var item in checkedListBox1.CheckedItems)
            {
                DataRowView rw = (DataRowView)item;
                int skSifra = Convert.ToInt32(rw.Row["SkSifra"]);
                magacini.Add(skSifra);
            }
            pomMagacini = "";
            pomMagacini = string.Join(",", magacini);
        }
        private void FillGV()
        {
            var query = @"
        SELECT 
            p.RN,
            StatusRN,
            Artikal,
            RTRIM(Naziv) AS Naziv,
            KolicinaZaProizvodnju,
            Konacan,
            Raspored
        FROM KompletiraniPlanovi p
        INNER JOIN StavkePlanaPakovanja spp ON p.RN = spp.RNBroj
        WHERE spp.IDPlanPakovanja = " + plan;

            using (SqlConnection conn = new SqlConnection(connect))
            {
                var da = new SqlDataAdapter(query, conn);
                var ds = new DataSet();
                da.Fill(ds);

                dataGridView1.ReadOnly = false;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = ds.Tables[0];

                dataGridView1.Columns.Clear(); // Ensure a clean start

                // RN
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "RN",
                    Name = "RN", // 🔑 Needed for row.Cells["RN"]
                    HeaderText = "RN",
                    Width = 80,
                    ReadOnly = true
                });

                // StatusRN
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "StatusRN",
                    HeaderText = "Status",
                    Width = 60,
                    ReadOnly = true
                });

                // Artikal
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Artikal",
                    HeaderText = "Artikal",
                    Width = 100,
                    ReadOnly = true
                });

                // Naziv
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Naziv",
                    HeaderText = "Naziv",
                    Width = 350,
                    ReadOnly = true
                });

                // KolicinaZaProizvodnju
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "KolicinaZaProizvodnju",
                    HeaderText = "Potrebno",
                    Width = 80,
                    ReadOnly = true
                });

                // ✅ Konacan (CheckBox Column - data bound)
                dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
                {
                    DataPropertyName = "Konacan",
                    HeaderText = "Konacan",
                    Name = "Konacan",
                    Width = 60
                });

                // Raspored
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Raspored",
                    HeaderText = "Raspored",
                    Width = 60,
                    ReadOnly = false
                });

                // Optional: prevent the user from adding rows
                dataGridView1.AllowUserToAddRows = false;
            }

            // Optional: Set checked rows' styles or perform post-load logic
            GetChecked();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            InsertPakovanja ins = new InsertPakovanja();
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    ins.DeleteKompletiraniPlanovi(plan, Convert.ToInt32(row.Cells[0].Value.ToString()));
                }
            }
            FillGV();
            FillRPT();
        }
        private void FillRPT()
        {
            Jela_TestDataSet9TableAdapters.KompletiraniPlanoviRprTableAdapter ta = new Jela_TestDataSet9TableAdapters.KompletiraniPlanoviRprTableAdapter();
            Jela_TestDataSet9.KompletiraniPlanoviRprDataTable dt = new Jela_TestDataSet9.KompletiraniPlanoviRprDataTable();
            ta.Fill(dt,plan,kompletirano);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;
            ReportParameter[] par = new ReportParameter[3];
            par[0] = new ReportParameter("id", plan.ToString());
            par[1]= new ReportParameter("magacini", pomMagacini.ToString());
            par[2] = new ReportParameter("kompletirano", kompletirano.ToString());

            Jela_TestDataSet10TableAdapters.KompletiraniPlanoviNedostajeRptTableAdapter ta2 = new Jela_TestDataSet10TableAdapters.KompletiraniPlanoviNedostajeRptTableAdapter();
            Jela_TestDataSet10.KompletiraniPlanoviNedostajeRptDataTable dt2 = new Jela_TestDataSet10.KompletiraniPlanoviNedostajeRptDataTable();
            ta2.Fill(dt2,plan,pomMagacini);
            ReportDataSource rds2 = new ReportDataSource();
            rds2.Name = "DataSet2";
            rds2.Value = dt2;
            

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "KompletiranoRpt.rdlc";
            reportViewer1.LocalReport.SetParameters(par);
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.DataSources.Add(rds2);
            reportViewer1.RefreshReport();

            System.Drawing.Printing.PageSettings newPageSettings = new System.Drawing.Printing.PageSettings();
            newPageSettings.Margins = new System.Drawing.Printing.Margins(5, 0, 5, 0);
            newPageSettings.Landscape = false;
            reportViewer1.SetPageSettings(newPageSettings);
        }

        private void NalogKomplet_Load(object sender, EventArgs e)
        {
            FillRPT();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            kompletirano = 0;
            GetChecked();
            FillRPT();
        }

        private void btnMagacin_Click(object sender, EventArgs e)
        {
            GetChecked();
            FillRPT();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            int rb = 0;
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    if (row.IsNewRow) continue;
                    var konacanValue = row.Cells["Konacan"].Value;
                    bool isChecked = false;
                    if (konacanValue != DBNull.Value && konacanValue != null)
                        bool.TryParse(konacanValue.ToString(), out isChecked);

                    if (isChecked)
                    {
                        rb++;
                        var rnValue = row.Cells["RN"].Value;
                        if (rnValue != DBNull.Value && rnValue != null)
                        {
                            string query = "Update KompletiraniPlanovi Set Konacan=1,Raspored=@RB Where IDPlana=@IDPlana and RN=@RN";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@RB", rb);
                                cmd.Parameters.AddWithValue("@IDPlana", plan);
                                cmd.Parameters.AddWithValue("@RN", rnValue);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        var rnValue = row.Cells["RN"].Value;
                        if (rnValue != DBNull.Value && rnValue != null)
                        {
                            string query = "Update KompletiraniPlanovi Set Konacan=0,Raspored=null Where IDPlana=@IDPlana and RN=@RN";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@RB", rb);
                                cmd.Parameters.AddWithValue("@IDPlana", plan);
                                cmd.Parameters.AddWithValue("@RN", rnValue);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                conn.Close();
            }
            kompletirano = 1;
            FillGV();
            FillRPT();
        }
    }
}
