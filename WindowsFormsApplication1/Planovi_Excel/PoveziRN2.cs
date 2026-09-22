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
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using Microsoft.Reporting.WinForms;

namespace WindowsFormsApplication1.Planovi_Excel
{
    public partial class PoveziRN2 : Form
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public static string code = "PoveziRN2";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
      
        public PoveziRN2()
        {
            InitializeComponent();
    
        }
        public string IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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

            var query2 = "select DnPStZapisa as [Zapis RN], DNPStevilka as [Broj RN],DNpSifStat as [Status],IDPlana, DnpSifMp as MP, MaticniPodatki.MpStaraSif as Kod, DNpKolZaIzdelavo as Kolicina, DelNalogProizvodni.DNpDejDatLans from DelNalogProizvodni " +
" inner join MaticniPodatki on MaticniPodatki.MpSifra = DelNalogProizvodni.DNpSifMp " +
" where DNpSifStat IN('OD', 'LA', 'PO', 'PF', 'PL')  order by DNpStZapisa desc";
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

        string Naziv = "";

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    textBox1.Text = row.Cells[0].Value.ToString();
                    textBox2.Text = row.Cells[1].Value.ToString();
                    Naziv = row.Cells[1].Value.ToString();
                }
            }
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
          



            Barcodes.Barcode39 b = new Barcodes.Barcode39();

            Magacin.InsertMSPNalepnica MspNal = new Magacin.InsertMSPNalepnica();
            MspNal.DeleteNalepnice();
            //Skupiti sve parametre

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected == true)
                {
                    MspNal.InsMspNalepnica(Convert.ToInt32(row.Cells[1].Value.ToString()),
                       Convert.ToInt32(row.Cells[4].Value.ToString())
                       , row.Cells[5].Value.ToString()
                       , "0"
                      , "0"
                      , Convert.ToInt32(row.Cells[4].Value.ToString())
                       , "sa"
                       , Convert.ToDouble(txtKolicina.Text)
                      , textBox2.Text
                      , b.GenerateBarcodeImage(row.Cells[5].Value.ToString()),
                      b.GenerateBarcodeImage(row.Cells[1].Value.ToString()));



                }

                

            }
            Perftech_BeogradDataSet2TableAdapters.MSPNalepniceTableAdapter ta = new Perftech_BeogradDataSet2TableAdapters.MSPNalepniceTableAdapter();
            Perftech_BeogradDataSet2.MSPNalepniceDataTable dt = new Perftech_BeogradDataSet2.MSPNalepniceDataTable();
            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptLogisticar.rdlc";

            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();

        }

        private void PoveziRN2_Load(object sender, EventArgs e)
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
