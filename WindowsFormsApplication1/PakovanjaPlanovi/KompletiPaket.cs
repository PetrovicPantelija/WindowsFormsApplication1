using CsvHelper;
using CsvHelper.Configuration;
using DocumentFormat.OpenXml.Office2019.Drawing.Ink;
using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.Ajax.Utilities;
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

namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    public partial class KompletiPaket : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public KompletiPaket()
        {
            InitializeComponent();
        }

        private void KompletiPaket_Load(object sender, EventArgs e)
        {
            this.Click += new EventHandler(KompletiPaket_Click);
            panel1.Visible = false;
            FillSviPaketi();

        }
        private void KompletiPaket_Click(object sender, EventArgs e)
        {
            if (!panel1.ClientRectangle.Contains(panel1.PointToClient(Cursor.Position)))
            {
                panel1.Visible = false;
            }
        }
        private void CenterPanel()
        {
            int centerX=(this.ClientSize.Width-panel1.Width)/2;
            int centerY=(this.ClientSize.Height-panel1.Height)/2;

            panel1.Location=new Point(centerX, centerY);
        }
        private void FillSviPaketi()
        {
            SqlConnection conn = new SqlConnection(connect);

            var query = "Select distinct LargoProizvodID as ID,RTrim(MpNaziv) as Naziv from RkShippingLinkWebsLargo inner join MaticniPodatki on RkShippingLinkWebsLargo.LargoProizvodID=MaticniPodatki.MpSifra order by LargoProizvodID asc";
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[1].Width = 250;

            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    if (dgvWebs.Rows.Count > 0)
                    {
                        SqlConnection conn = new SqlConnection(connect);

                        var query = "select KoPodSifMp as MP,RTrim(MPNaziv) as Naziv,KoKolPodrMp as Potrebno,(SUm(PrPrevKol)-SUm(PrIzdKol)) as Zaliha " +
                            "From Kosovnica " +
                            "inner join MaticniPodatki on Kosovnica.KoPodSifMp=MaticniPodatki.MpSifra " +
                            "inner join Promet on Kosovnica.KoPodSifMp=Promet.PrSifMp " +
                            "Where KoNadSifMp=" + Convert.ToInt32(row.Cells[0].Value) + " Group by KoPodSifMp,MpNaziv,KoKolPodrMp order by KoPodSifMp";
                        var da = new SqlDataAdapter(query, conn);
                        var ds = new DataSet();
                        da.Fill(ds);

                        dgvWebs.DataSource = ds.Tables[0];
                        dgvWebs.Columns[0].Width = 70;
                        dgvWebs.Columns[1].Width = 150;
                        dgvWebs.Columns[2].Width = 70;
                        dgvWebs.Columns[3].Width = 70;

                        dgvWebs.BorderStyle = BorderStyle.Fixed3D;
                        dgvWebs.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        dgvWebs.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                        dgvWebs.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkTurquoise;
                        dgvWebs.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
                        dgvWebs.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
                        dgvWebs.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                        dgvWebs.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);

                        foreach (DataGridViewRow r in dgvWebs.Rows)
                        {
                            decimal potrebno = Convert.ToDecimal(r.Cells["Potrebno"].Value);
                            decimal zaliha = Convert.ToDecimal(r.Cells["Zaliha"].Value);
                            if (potrebno <= zaliha)
                            {
                                r.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                            }
                            else
                            {
                                r.DefaultCellStyle.BackColor = System.Drawing.Color.Firebrick;
                            }
                        }

                        panel1.Visible = true;
                        CenterPanel();
                    }
                }
            }
        }
        public class Kosovnica
        {
            public int LargoProizvodID { get; set; }
            public int MP { get; set; }
            public decimal Potrebno { get; set; }
            public decimal Zaliha { get; set; }
        }
        List<Kosovnica> kosovnicaPom = new List<Kosovnica>();
        List<Kosovnica> kosovnica = new List<Kosovnica>();
        private void PopuniWebs()
        {
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string query = "select KoPodSifMp as MP,(KoKolPodrMp * KoKolNadrMp) as Potrebno,(SUm(PrPrevKol) - SUm(PrIzdKol)) as Zaliha " +
                    "From Kosovnica " +
                    "inner join Promet on Kosovnica.KoPodSifMp = Promet.PrSifMp " +
                    "Where KoNadSifMp =" + Convert.ToInt32(row.Cells[0].Value) + " Group by KoPodSifMp,KoKolPodrMp,KoKolNadrMp order by KoPodSifMp";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        
                        while (dr.Read())
                        {
                            Kosovnica k = new Kosovnica
                            {
                                LargoProizvodID = Convert.ToInt32(row.Cells[0].Value.ToString()),
                                MP = Convert.ToInt32(dr[0].ToString()),
                                Potrebno = Convert.ToDecimal(dr[1].ToString()),
                                Zaliha = Convert.ToDecimal(dr[2].ToString())
                            };
                            kosovnicaPom.Add(k);
                        }
                    }
                }   
            }
            conn.Close();

            var grupa = kosovnicaPom.GroupBy(k => k.LargoProizvodID);
            foreach(var item in grupa)
            {
                bool ima = true;
                foreach (var i in item)
                {
                    if(i.Zaliha<i.Potrebno || i.Zaliha <= 0)
                    {
                        ima = false;
                        break;
                    }
                }
                if (ima == true)
                {
                    kosovnica.AddRange(item);
                }
            }
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                foreach (var item in kosovnica)
                {
                    int kosovnicaID = Convert.ToInt32(item.LargoProizvodID);
                    if (id == kosovnicaID)
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.Green;
                        break;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
        public class Kompleti
        {
            public int LargoProizvodID { get; set; }
            public int Kompleta { get; set; }
        }
        private void IzracunajKomplete()
        {
            var kompleti = new Dictionary<int, int>();
            foreach(var item in kosovnica.GroupBy(k=>k.LargoProizvodID))
            {
                int min = int.MaxValue;
                foreach(var i in item)
                {
                    int komplet = Convert.ToInt32(i.Zaliha / i.Potrebno);
                    if(komplet < min)
                    {
                        min = komplet;
                    }
                }
                kompleti[item.Key] = min;
            }
            var list = kompleti.Select(k => new Kompleti
            {
                LargoProizvodID = k.Key,
                Kompleta = k.Value
            }).ToList();

            dataGridView2.DataSource= list;
            dataGridView2.BorderStyle = BorderStyle.Fixed3D;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            dataGridView2.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
        }
        public class KompletiDetaljno
        {
            public int LargoProizvodID { get; set; }
            public int Komplet { get; set; }
            public int MP { get; set; }
            public decimal PreostalaZaliha { get; set; }    
        }
        private void IzracunajKompleteDetaljno()
        {
            
            var mpZaliha=new Dictionary<int, decimal>();
            foreach(var i in kosovnica)
            {
                if (!mpZaliha.ContainsKey(i.MP))
                {
                    mpZaliha[i.MP] = i.Zaliha;
                }
            }
            List<KompletiDetaljno> kompletiDetaljno = new List<KompletiDetaljno>();
            var grupisano = kosovnica.GroupBy(k => k.LargoProizvodID);
            foreach (var item in grupisano)
            {
                for (int j = 0; j < 1; j++)
                {
                    bool moze = true;
                    int total = 0;
                    while (moze)
                    {
                        moze = true;
                        foreach (var i in item)
                        {
                            if (mpZaliha[i.MP] < i.Potrebno)
                            {
                                moze = false;
                                break;
                            }
                        }
                        if (moze)
                        {
                            total++;
                            foreach (var i in item)
                            {
                                mpZaliha[i.MP] -= i.Potrebno;
                                kompletiDetaljno.Add(new KompletiDetaljno
                                {
                                    LargoProizvodID = item.Key,
                                    Komplet = total,
                                    MP = i.MP,
                                    PreostalaZaliha = mpZaliha[i.MP]
                                });
                            }
                        }
                    }
                }
            }
            var filteredList = kompletiDetaljno.GroupBy(k => k.LargoProizvodID).SelectMany(g => g.Where(k => k.Komplet == g.Max(x => x.Komplet))).ToList();
            dataGridView3.DataSource = filteredList;
            dataGridView3.BorderStyle = BorderStyle.Fixed3D;
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView3.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkTurquoise;
            dataGridView3.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridView3.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            dataGridView3.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dataGridView3.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
        }

        private void izračunajKompleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PopuniWebs();
            IzracunajKomplete();
            IzracunajKompleteDetaljno();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] path = { Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Paketi_Komplet-" };
            string folder = Path.Combine(path);
            string file = folder + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            ExportToCSV(dataGridView2,dataGridView3, file);
            MessageBox.Show("Kreirani dokumenti");
        }
        private void ExportToCSV(DataGridView dgv1,DataGridView dgv2, string filePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                ShouldQuote = (field) => false,
                Delimiter = ";"
            };

            using (var textWriter = File.CreateText(filePath))
            using (var csv = new CsvWriter(textWriter, config))
            {
                ExportDataGridViewToCsv(dgv1, csv, 0);
                csv.NextRecord();
                ExportDataGridViewToCsv(dgv2, csv, dgv1.Columns.Count);
            }
        }
        private void ExportDataGridViewToCsv(DataGridView dgv, CsvWriter csv, int startingColumn)
        {
            for (int col = 0; col < dgv.Columns.Count; col++)
            {
                csv.WriteField(dgv.Columns[col].HeaderText);
            }
            csv.NextRecord();

            for (int row = 0; row < dgv.Rows.Count; row++)
            {
                if (!dgv.Rows[row].IsNewRow)
                {
                    for (int col = 0; col < dgv.Columns.Count; col++)
                    {
                        csv.WriteField(dgv.Rows[row].Cells[col].Value?.ToString() ?? "");
                    }
                    csv.NextRecord();
                }
            }
        }
    }
}
