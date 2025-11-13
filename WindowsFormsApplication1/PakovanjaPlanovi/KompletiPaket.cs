using CsvHelper;
using CsvHelper.Configuration;
using DocumentFormat.OpenXml.Office2019.Drawing.Ink;
using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.Ajax.Utilities;
using MySql.Data.MySqlClient;
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
        {/*
            if (!panel1.ClientRectangle.Contains(panel1.PointToClient(Cursor.Position)))
            {
                panel1.Visible = false;
            }*/
        }

        private void CenterPanel()
        {
            int centerX = (this.ClientSize.Width - panel1.Width) / 2;
            int centerY = (this.ClientSize.Height - panel1.Height) / 2;

            panel1.Location = new Point(centerX, centerY);
        }


        private void FillSviPaketi()
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                var query = @"
select distinct LargoProizvodID as ID,LargoProizvodNaziv
from PomWebsUporedna 
inner join PomWebsExportActive on PomWebsUporedna.WebsGTIN=PomWebsExportActive.BarCode
Where LargoProizvodID not in (Select ProizvodID From PomGotoviProizvodiNeaktivni) and PomWebsExportActive.active=1";

                var da = new SqlDataAdapter(query, conn);
                var ds = new DataSet();
                da.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 70;
                dataGridView1.Columns[1].Width = 250;

                StyleGrid(dataGridView1);
            }
        }

        private static void StyleGrid(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.Fixed3D;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkTurquoise;
            dgv.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dgv.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
            dgv.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            dgv.BackgroundColor = System.Drawing.Color.FromArgb(125, 158, 192);
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    SqlConnection conn = new SqlConnection(connect);
                    var query = @"
select 
    KoPodSifMp as MP,
    RTrim(MpNaziv) as Naziv,
    KoKolPodrMp as Potrebno,
    (SUM(PrPrevKol) - SUM(PrIzdKol)) as Zaliha
from Kosovnica k
inner join MaticniPodatki m on k.KoPodSifMp = m.MpSifra
inner join Promet p on k.KoPodSifMp = p.PrSifMp
where KoNadSifMp = @id and KoVarianta='*' 
group by KoPodSifMp, MpNaziv, KoKolPodrMp
order by KoPodSifMp";

                    var da = new SqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@id", Convert.ToInt32(row.Cells[0].Value));
                    var ds = new DataSet();
                    da.Fill(ds);

                    dgvWebs.DataSource = ds.Tables[0];
                    if (dgvWebs.Columns.Count >= 4)
                    {
                        dgvWebs.Columns[0].Width = 70;
                        dgvWebs.Columns[1].Width = 150;
                        dgvWebs.Columns[2].Width = 70;
                        dgvWebs.Columns[3].Width = 70;
                    }
                    StyleGrid(dgvWebs);

                    foreach (DataGridViewRow r in dgvWebs.Rows)
                    {
                        if (r.IsNewRow) continue;
                        decimal potrebno = Convert.ToDecimal(r.Cells["Potrebno"].Value);
                        decimal zaliha = Convert.ToDecimal(r.Cells["Zaliha"].Value);
                        r.DefaultCellStyle.BackColor = (potrebno <= zaliha)
                            ? System.Drawing.Color.LightGreen
                            : System.Drawing.Color.Firebrick;
                    }

                    panel1.Visible = true;
                    CenterPanel();
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

        public class Kompleti
        {
            public int LargoProizvodID { get; set; }
            public int Kompleta { get; set; }
        }

        public class KompletiDetaljno
        {
            public int LargoProizvodID { get; set; }
            public int Komplet { get; set; }
            public int MP { get; set; }
            public decimal PreostalaZaliha { get; set; }
        }

        List<Kosovnica> kosovnicaPom = new List<Kosovnica>();
        List<Kosovnica> kosovnica = new List<Kosovnica>();

        private void PopuniWebs()
        {
            kosovnicaPom.Clear();
            kosovnica.Clear();

            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                var ids = new List<int>();
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    if (r.IsNewRow) continue;
                    if (r.Cells[0].Value == null) continue;
                    ids.Add(Convert.ToInt32(r.Cells[0].Value));
                }
                if (ids.Count == 0) return;

                string inList = string.Join(",", ids.Distinct());

                string queryAll = $@"
select 
    k.KoNadSifMp as LargoProizvodID,
    k.KoPodSifMp as MP,
    (k.KoKolPodrMp * k.KoKolNadrMp) as Potrebno,
    (SUM(p.PrPrevKol) - SUM(p.PrIzdKol)) as Zaliha
from Kosovnica k
inner join Promet p on k.KoPodSifMp = p.PrSifMp
where k.KoNadSifMp in ({inList}) and KoVarianta='*'
group by k.KoNadSifMp, k.KoPodSifMp, k.KoKolPodrMp, k.KoKolNadrMp
order by k.KoNadSifMp, k.KoPodSifMp";

                using (SqlCommand cmd = new SqlCommand(queryAll, conn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var k = new Kosovnica
                        {
                            LargoProizvodID = dr.IsDBNull(0) ? 0 : dr.GetInt32(0),
                            MP = dr.IsDBNull(1) ? 0 : dr.GetInt32(1),
                            Potrebno = dr.IsDBNull(2) ? 0 : Convert.ToDecimal(dr[2]),
                            Zaliha = dr.IsDBNull(3) ? 0 : Convert.ToDecimal(dr[3])
                        };
                        kosovnicaPom.Add(k);
                    }
                }
            }

            var grupa = kosovnicaPom.GroupBy(k => k.LargoProizvodID);
            foreach (var item in grupa)
            {
                bool ima = true;
                foreach (var i in item)
                {
                    if (i.Zaliha < i.Potrebno || i.Zaliha <= 0)
                    {
                        ima = false;
                        break;
                    }
                }
                if (ima)
                {
                    kosovnica.AddRange(item);
                }
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow || row.Cells[0].Value == null) continue;
                int id = Convert.ToInt32(row.Cells[0].Value);
                bool found = kosovnica.Any(k => k.LargoProizvodID == id);
                row.DefaultCellStyle.BackColor = found ? Color.Green : Color.Red;
            }
        }

        private void IzracunajKomplete()
        {
            var kompleti = new Dictionary<int, int>();
            foreach (var item in kosovnica.GroupBy(k => k.LargoProizvodID))
            {
                int min = int.MaxValue;
                foreach (var i in item)
                {
                    int komplet = (i.Potrebno <= 0) ? 0 : Convert.ToInt32(Math.Floor(i.Zaliha / i.Potrebno));
                    if (komplet < min) min = komplet;
                }
                if (min == int.MaxValue) min = 0;
                kompleti[item.Key] = Math.Max(min, 0);
            }

            var list = kompleti.Select(k => new Kompleti
            {
                LargoProizvodID = k.Key,
                Kompleta = k.Value
            }).ToList();

            dataGridView2.DataSource = list;
            StyleGrid(dataGridView2);
        }

        private void IzracunajKompleteDetaljno()
        {
            var mpZaliha = new Dictionary<int, decimal>();
            foreach (var i in kosovnica)
            {
                if (!mpZaliha.ContainsKey(i.MP))
                    mpZaliha[i.MP] = i.Zaliha;
            }

            List<KompletiDetaljno> kompletiDetaljno = new List<KompletiDetaljno>();
            var grupisano = kosovnica.GroupBy(k => k.LargoProizvodID);

            foreach (var item in grupisano)
            {
                int total = 0;
                while (true)
                {
                    bool moze = true;
                    foreach (var i in item)
                    {
                        if (!mpZaliha.ContainsKey(i.MP) || mpZaliha[i.MP] < i.Potrebno)
                        {
                            moze = false;
                            break;
                        }
                    }
                    if (!moze) break;

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

            var filteredList =
                kompletiDetaljno
                .GroupBy(k => k.LargoProizvodID)
                .SelectMany(g => {
                    int maxK = g.Any() ? g.Max(x => x.Komplet) : 0;
                    return g.Where(x => x.Komplet == maxK);
                })
                .ToList();

            dataGridView3.DataSource = filteredList;
            StyleGrid(dataGridView3);
        }
        private class Sastavnica
        {
            public int MP { get; set; }
            public decimal Potrebno { get; set; }
        }
        private void UcitajZaSekvencijalno(out List<int> paketIdsInOrder,
                                           out Dictionary<int, List<Sastavnica>> paketDoSastavnice,
                                           out Dictionary<int, decimal> zalihaPoMP)
        {
            paketIdsInOrder = new List<int>();
            paketDoSastavnice = new Dictionary<int, List<Sastavnica>>();
            zalihaPoMP = new Dictionary<int, decimal>();
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (r.IsNewRow || r.Cells[0].Value == null) continue;
                paketIdsInOrder.Add(Convert.ToInt32(r.Cells[0].Value));
            }

            if (paketIdsInOrder.Count == 0) return;

            string inList = string.Join(",", paketIdsInOrder.Distinct());

            using (var conn = new SqlConnection(connect))
            {
                conn.Open();
                string q = $@"
;with S as (
    select 
        k.KoNadSifMp as LargoProizvodID,
        k.KoPodSifMp as MP,
        cast(k.KoKolPodrMp * k.KoKolNadrMp as decimal(18,4)) as Potrebno
    from Kosovnica k
    where k.KoNadSifMp in ({inList}) and KoVarianta='*'
),
Z as (
    select 
        p.PrSifMp as MP,
        cast(SUM(p.PrPrevKol) - SUM(p.PrIzdKol) as decimal(18,4)) as Zaliha
    from Promet p
    group by p.PrSifMp
)
select 
    S.LargoProizvodID,
    S.MP,
    S.Potrebno,
    ISNULL(Z.Zaliha, 0) as Zaliha
from S
left join Z on S.MP = Z.MP
order by S.LargoProizvodID, S.MP;";

                var cmd = new SqlCommand(q, conn);
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        int pid = rdr.IsDBNull(0) ? 0 : rdr.GetInt32(0);
                        int mp = rdr.IsDBNull(1) ? 0 : rdr.GetInt32(1);
                        decimal potrebno = rdr.IsDBNull(2) ? 0 : Convert.ToDecimal(rdr[2]);
                        decimal zaliha = rdr.IsDBNull(3) ? 0 : Convert.ToDecimal(rdr[3]);

                        if (!paketDoSastavnice.ContainsKey(pid))
                            paketDoSastavnice[pid] = new List<Sastavnica>();

                        paketDoSastavnice[pid].Add(new Sastavnica { MP = mp, Potrebno = potrebno });

                        if (!zalihaPoMP.ContainsKey(mp))
                            zalihaPoMP[mp] = zaliha;
                    }
                }
            }
        }
        private Dictionary<int, string> GetNazivMap(IEnumerable<int> ids)
        {
            var map = new Dictionary<int, string>();
            var list = ids?.Distinct().ToList() ?? new List<int>();
            if (list.Count == 0) return map;

            string inList = string.Join(",", list);
            using (var conn = new SqlConnection(connect))
            using (var cmd = new SqlCommand($@"
                SELECT MpSifra, RTrim(MpStaraSif) as Naziv
                FROM MaticniPodatki
                WHERE MpSifra IN ({inList})", conn))
            {
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        int id = rdr.IsDBNull(0) ? 0 : rdr.GetInt32(0);
                        string naziv = rdr.IsDBNull(1) ? null : rdr.GetString(1);
                        if (!map.ContainsKey(id))
                            map[id] = naziv;
                    }
                }
            }
            return map;
        }
        public class KompletiRow
        {
            public int LargoProizvodID { get; set; }
            public string Naziv { get; set; }
            public int Kompleta { get; set; }
        }

        private void Rekurzivno()
        {
            UcitajZaSekvencijalno(out var paketIdsInOrder,
                                  out var paketDoSastavnice,
                                  out var zalihaPoMP);

            if (paketIdsInOrder.Count == 0 || paketDoSastavnice.Count == 0)
            {
                dataGridView4.DataSource = null;
                dataGridView5.DataSource = null;
                return;
            }

            var kompletCountPerPackage = paketIdsInOrder.ToDictionary(id => id, id => 0);

            var detalji = new List<KompletiDetaljno>();

            bool barJedanNapravljenUProlazu = true;

            while (barJedanNapravljenUProlazu)
            {
                barJedanNapravljenUProlazu = false;

                foreach (var pid in paketIdsInOrder)
                {
                    if (!paketDoSastavnice.ContainsKey(pid)) continue;

                    var sast = paketDoSastavnice[pid];
                    if (sast == null || sast.Count == 0) continue;

                    bool moze = true;
                    foreach (var s in sast)
                    {
                        if (!zalihaPoMP.TryGetValue(s.MP, out var z))
                        {
                            moze = false;
                            break;
                        }
                        if (z < s.Potrebno || s.Potrebno <= 0)
                        {
                            moze = false;
                            break;
                        }
                    }

                    if (!moze) continue;

                    foreach (var s in sast)
                    {
                        zalihaPoMP[s.MP] -= s.Potrebno;
                        if (zalihaPoMP[s.MP] < 0) zalihaPoMP[s.MP] = 0;
                    }

                    kompletCountPerPackage[pid]++;

                    int redniKomplet = kompletCountPerPackage[pid];
                    foreach (var s in sast)
                    {
                        detalji.Add(new KompletiDetaljno
                        {
                            LargoProizvodID = pid,
                            Komplet = redniKomplet,
                            MP = s.MP,
                            PreostalaZaliha = zalihaPoMP[s.MP]
                        });
                    }

                    barJedanNapravljenUProlazu = true;
                }
            }

            var nazivMap = GetNazivMap(kompletCountPerPackage.Keys);

            var dgv4List = kompletCountPerPackage
                .Select(kv => new KompletiRow
                {
                    LargoProizvodID = kv.Key,
                    Naziv = nazivMap.TryGetValue(kv.Key, out var name) ? name : null,
                    Kompleta = kv.Value
                })
                .OrderBy(x => x.LargoProizvodID)
                .ToList();

            dataGridView4.DataSource = dgv4List;
            StyleGrid(dataGridView4);

            var finalPerPackageAndMp =
     detalji
     .GroupBy(d => new { d.LargoProizvodID, d.MP })
     .Select(g =>
     {
         int maxKomplet = g.Max(x => x.Komplet);
         return g.First(x => x.Komplet == maxKomplet);
     })
     .OrderBy(d => d.LargoProizvodID)
     .ThenBy(d => d.MP)
     .ToList();

            dataGridView5.DataSource = finalPerPackageAndMp;
            StyleGrid(dataGridView5);
        }

        private void izračunajKompleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PopuniWebs();
            IzracunajKomplete();
            IzracunajKompleteDetaljno();
            Rekurzivno();
            Update0();

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
            ExportToCSV(dataGridView2, dataGridView3, file);
            MessageBox.Show("Kreirani dokumenti");
        }

        private void ExportToCSV(DataGridView dgv1, DataGridView dgv2, string filePath)
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
            if (dgv?.Columns == null) return;

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
        private void Update0()
        {
            var list = dataGridView4.DataSource as IList<KompletiRow>;
            if (list == null) return;

            string[] g8 = { "P2", "P3", "P4", "L1", "L2", "L3", "L4", "LP1", "LP2", "LP3", "LP4", "PK181" };
            string[] g5 = { "P5", "P6", "L5", "L6", "LP5", "LP6", "PK231" };
            string[] g3 = { "P7", "P8", "L7", "L8", "LP7", "LP8", "PK281" };

            var highlightIds = new HashSet<int>(
                list.Where(it => it.Kompleta == 0)
                    .Select(it => it.LargoProizvodID)
            );

            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                if (row.IsNewRow) continue;
                int id = 0;
                var idVal = row.Cells["LargoProizvodID"].Value;
                if (idVal != null && int.TryParse(idVal.ToString(), out id) && highlightIds.Contains(id))
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
            }

            foreach (var item in list)
            {
                if (item.Kompleta != 0) continue; 

                var name = (item.Naziv ?? string.Empty).Trim().ToUpperInvariant();

                if (g8.Any(p => name.StartsWith(p))) item.Kompleta = 8;
                else if (g5.Any(p => name.StartsWith(p))) item.Kompleta = 5;
                else if (g3.Any(p => name.StartsWith(p))) item.Kompleta = 3;
                else item.Kompleta = 3;
            }


            var cm = BindingContext[dataGridView4.DataSource] as CurrencyManager;
            cm?.Refresh();


            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                if (row.IsNewRow) continue;
                int id = 0;
                var idVal = row.Cells["LargoProizvodID"].Value;
                if (idVal != null && int.TryParse(idVal.ToString(), out id) && highlightIds.Contains(id))
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
            }

            dataGridView4.Invalidate();
        }

        private void upišiUBazuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView4 == null || dataGridView4.Rows.Count == 0)
            {
                MessageBox.Show("Nema podataka u dataGridView4 za upis.");
                return;
            }


            string mysqlConnString =
    ConfigurationManager.ConnectionStrings[
        "WindowsFormsApplication1.Properties.Settings.MySqlJelaWebs"
    ].ConnectionString;
            try
            {
                using (var conn = new MySqlConnection(mysqlConnString))
                {
                    conn.Open();

                    using (var tran = conn.BeginTransaction())
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = tran;

                         cmd.CommandText = "TRUNCATE TABLE jela_lager;";
                         cmd.ExecuteNonQuery();

                        cmd.CommandText = @"
INSERT INTO jela_lager
    (LargoProizvodID, Naziv, Kompleta)
VALUES
    (@id, @naziv, @kompleta);";

                        cmd.Parameters.Add("@id", MySqlDbType.Int32);
                        cmd.Parameters.Add("@naziv", MySqlDbType.VarChar, 255);
                        cmd.Parameters.Add("@kompleta", MySqlDbType.Int32);

                        foreach (DataGridViewRow row in dataGridView4.Rows)
                        {
                            if (row.IsNewRow) continue;

                            if (row.Cells["LargoProizvodID"].Value == null) continue;

                            int id;
                            if (!int.TryParse(row.Cells["LargoProizvodID"].Value.ToString(), out id))
                                continue;

                            string naziv = row.Cells["Naziv"].Value?.ToString() ?? string.Empty;

                            int kompleta = 0;
                            int.TryParse(row.Cells["Kompleta"].Value?.ToString() ?? "0", out kompleta);

                            cmd.Parameters["@id"].Value = id;
                            cmd.Parameters["@naziv"].Value = naziv;
                            cmd.Parameters["@kompleta"].Value = kompleta;

                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                }

                MessageBox.Show("Podaci iz dataGridView4 su uspešno upisani u MySQL (jela_lager).");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri upisu u MySQL: " + ex.Message);
            }
        }

    }
}
