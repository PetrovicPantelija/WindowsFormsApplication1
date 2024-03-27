using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    public partial class NamenskoPakovanje : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        string Korisnik = frmLogovanje.user;
        public NamenskoPakovanje()
        {
            InitializeComponent();
        }
        int ID;
        public NamenskoPakovanje(int id)
        {
            ID= id;
            InitializeComponent();
        }
        DateTime datum,datum2;

        private void FillGV()
        {
            datum = dateTimePicker1.Value;
            string datumPom = datum.ToShortDateString();
            var query = "select PrSifMp as MP,MpStaraSif as Sifra,MpNaziv as Naziv,PrSifSklad as Skladiste,0-(Sum(PrPRevKol)-Sum(PrIzdKol)) as [|Potrebno|] " +
                "from Promet " +
                "inner join MaticniPodatki on Promet.PrSifMp=MaticniPodatki.MpSifra " +
                "Where PrDatTrans<=GetDate() and PrSifSklad in (13,14) " +
                "Group by PrSifMp,MpStaraSif,MpNaziv,PrSifSklad " +
                "Having (Sum(PrPRevKol)-Sum(PrIzdKol))<0 " +
                "order by PrSifSklad asc";

            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.FromArgb(125, 158, 192);
            dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            //dataGridView1.MultiSelect = false;

            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 180;
            dataGridView1.Columns[2].Width = 180;
            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[4].Width = 70;

            var query2 = "Select ID,IDPlanPakovanja,Artikal,Kod,Naziv,KolicinaZaProizvodnju,PlaniraniKrajnjiDatumZavrsetka,Planer from StavkePlanaPakovanja WHere IdPlanPakovanja=" + ID;
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds2.Tables[0];
        }

        private void NamenskoPakovanje_Load(object sender, EventArgs e)
        {
            FillGV();
            dateTimePicker1.Value= DateTime.Now;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] path = { Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "NamenskoPakovanje_Plan-" };
            string folder = Path.Combine(path);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                ShouldQuote = (field) => false,
                Delimiter = ";"
            };
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            var query = "select '' as a,''as b,''as c,PrSifMp as MP,RTrim(MpStaraSif) as Sifra,'','',0-(Sum(PrPRevKol)-Sum(PrIzdKol)) as [|Potrebno|] " +
                "from Promet " +
                "inner join MaticniPodatki on Promet.PrSifMp=MaticniPodatki.MpSifra " +
                "Where PrDatTrans<='"+dateTimePicker1.Value.ToString()+"' and PrSifSklad in (13,14) " +
                "Group by PrSifMp,MpStaraSif,MpNaziv,PrSifSklad " +
                "Having (Sum(PrPRevKol)-Sum(PrIzdKol))<0 " +
                "order by PrSifSklad asc";
            SqlCommand cmd = new SqlCommand(query, conn);
            string file = folder + DateTime.Now.ToShortDateString() + ".csv";
            try
            {
                using (var textWriter = File.CreateText(file))
                {
                    using (CsvWriter csv = new CsvWriter(textWriter, config))
                    {
                        DataTable dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());
                        foreach (DataColumn column in dt.Columns)
                        {
                            csv.WriteField(column.ColumnName);
                        }
                        csv.NextRecord();

                        foreach (DataRow row in dt.Rows)
                        {
                            
                            
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

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void brnDodaj_Click(object sender, EventArgs e)
        {
            InsertPakovanja ins = new InsertPakovanja();
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        ins.InsertStavkePakovanjaNamensko(ID, Convert.ToInt32(row.Cells[0].Value.ToString()), row.Cells[1].Value.ToString().Trim(), row.Cells[2].Value.ToString().Trim(), Convert.ToDecimal(row.Cells[4].Value.ToString()),Convert.ToDateTime(dateTimePicker2.Value.ToString()),Korisnik);
                    }
                }
            }
            catch { }
            FillGV();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            datum = dateTimePicker1.Value;
            datum2 = dateTimePicker2.Value;

            string datumPom = datum.ToString("yyyy-MM-dd");
            string datumPom2 = datum2.ToString("yyyy-MM-dd");

            var query = "select PrSifMp as MP,MpStaraSif as Sifra,MpNaziv as Naziv,PrSifSklad as Skladiste,0-(Sum(PrPRevKol)-Sum(PrIzdKol)) as [|Potrebno|] " +
                "from Promet " +
                "inner join MaticniPodatki on Promet.PrSifMp=MaticniPodatki.MpSifra " +
                "Where (PrDatTrans between '"+datumPom+"' and '"+datumPom2+"') and PrSifSklad in (13,14) " +
                "Group by PrSifMp,MpStaraSif,MpNaziv,PrSifSklad " +
                "Having (Sum(PrPRevKol)-Sum(PrIzdKol))<0 " +
                "order by PrSifSklad asc";  
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.FromArgb(125, 158, 192);
            dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);

            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 180;
            dataGridView1.Columns[2].Width = 180;
            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[4].Width = 70;
        }
    }
}
