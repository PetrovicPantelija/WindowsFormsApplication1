using DocumentFormat.OpenXml.Office.Word;
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

namespace WindowsFormsApplication1.Neto_potrebe
{
    public partial class PregledPodPlan : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        int tmpPlanID = 0;
        string tmpPodPlan = "";

        private readonly Dictionary<string, HashSet<string>> _hiddenColumnsByGrid =
            new Dictionary<string, HashSet<string>>();

        private readonly string _columnSettingsFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "WindowsFormsApplication1",
            "PregledPodPlan_Kolone.txt"
        );

        private ContextMenuStrip _columnMenu;
        private DataGridView _activeColumnMenuGrid;
        private string _activeColumnMenuGridKey;

        public PregledPodPlan()
        {
            InitializeComponent();
            panel1.Visible = false;
            InitColumnVisibilityMenu();
        }

        public PregledPodPlan(int PlanID, string PodPlan)
        {
            InitializeComponent();
            tmpPlanID = PlanID;
            tmpPodPlan = PodPlan;
            panel1.Visible = false;
            InitColumnVisibilityMenu();
        }

        private void FillPlanovi()
        {
            var query = "Select ID,Naziv From PlanArhiv order by ID asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboPlan.DataSource = ds.Tables[0];
            cboPlan.DisplayMember = "Naziv";
            cboPlan.ValueMember = "ID";
        }

        private void PregledPodPlan_Load(object sender, EventArgs e)
        {
            if (tmpPlanID != 0 && tmpPodPlan != "")
            {
                FillPlanovi();
                txtPodPlan.Text = tmpPodPlan;
                cboPlan.SelectedValue = tmpPlanID;
                FillGV();
            }
            else
            {
                FillPlanovi();
            }
        }

        private void InitColumnVisibilityMenu()
        {
            LoadColumnVisibilitySettings();

            _columnMenu = new ContextMenuStrip();

            RegisterColumnMenu(dataGridView1, "dataGridView1");
            RegisterColumnMenu(dataGridView2, "dataGridView2");
            RegisterColumnMenu(dataGridView3, "dataGridView3");
            RegisterColumnMenu(dataGridView4, "dataGridView4");
            RegisterColumnMenu(dataGridView5, "dataGridView5");
        }

        private void RegisterColumnMenu(DataGridView dgv, string gridKey)
        {
            if (dgv == null)
                return;

            dgv.Tag = gridKey;

            dgv.ColumnHeaderMouseClick -= Grid_ColumnHeaderMouseClick;
            dgv.ColumnHeaderMouseClick += Grid_ColumnHeaderMouseClick;
        }

        private void Grid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            DataGridView dgv = sender as DataGridView;

            if (dgv == null)
                return;

            string gridKey = dgv.Tag == null ? dgv.Name : dgv.Tag.ToString();

            ShowColumnMenu(dgv, gridKey);
        }

        private void ShowColumnMenu(DataGridView dgv, string gridKey)
        {
            _activeColumnMenuGrid = dgv;
            _activeColumnMenuGridKey = gridKey;

            _columnMenu.Items.Clear();

            ToolStripMenuItem title = new ToolStripMenuItem("Kolone");
            title.Enabled = false;
            _columnMenu.Items.Add(title);

            _columnMenu.Items.Add(new ToolStripSeparator());

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();

                string header = string.IsNullOrWhiteSpace(col.HeaderText)
                    ? col.Name
                    : col.HeaderText;

                item.Text = header;
                item.Tag = col.Name;
                item.Checked = col.Visible;
                item.CheckOnClick = true;
                item.Click += ColumnMenuItem_Click;

                _columnMenu.Items.Add(item);
            }

            _columnMenu.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem showAll = new ToolStripMenuItem("Prikaži sve kolone");
            showAll.Click += ShowAllColumns_Click;
            _columnMenu.Items.Add(showAll);

            ToolStripMenuItem hideAllOperations = new ToolStripMenuItem("Sakrij operacije");
            hideAllOperations.Click += HideOperationColumns_Click;
            _columnMenu.Items.Add(hideAllOperations);

            ToolStripMenuItem showAllOperations = new ToolStripMenuItem("Prikaži operacije");
            showAllOperations.Click += ShowOperationColumns_Click;
            _columnMenu.Items.Add(showAllOperations);

            _columnMenu.Show(Cursor.Position);
        }

        private void ColumnMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            if (item == null || _activeColumnMenuGrid == null)
                return;

            string columnName = item.Tag == null ? "" : item.Tag.ToString();

            if (string.IsNullOrWhiteSpace(columnName))
                return;

            if (!_activeColumnMenuGrid.Columns.Contains(columnName))
                return;

            if (!item.Checked && CountVisibleColumns(_activeColumnMenuGrid) <= 1)
            {
                item.Checked = true;
                MessageBox.Show("Mora ostati prikazana bar jedna kolona.", "Kolone",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _activeColumnMenuGrid.Columns[columnName].Visible = item.Checked;

            SetColumnHidden(_activeColumnMenuGridKey, columnName, !item.Checked);
            SaveColumnVisibilitySettings();
        }

        private int CountVisibleColumns(DataGridView dgv)
        {
            int count = 0;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Visible)
                    count++;
            }

            return count;
        }

        private void ShowAllColumns_Click(object sender, EventArgs e)
        {
            if (_activeColumnMenuGrid == null)
                return;

            foreach (DataGridViewColumn col in _activeColumnMenuGrid.Columns)
            {
                col.Visible = true;
            }

            if (_hiddenColumnsByGrid.ContainsKey(_activeColumnMenuGridKey))
                _hiddenColumnsByGrid[_activeColumnMenuGridKey].Clear();

            SaveColumnVisibilitySettings();
        }

        private void HideOperationColumns_Click(object sender, EventArgs e)
        {
            if (_activeColumnMenuGrid == null)
                return;

            foreach (DataGridViewColumn col in _activeColumnMenuGrid.Columns)
            {
                if (IsOperacijaColumn(col.Name))
                {
                    col.Visible = false;
                    SetColumnHidden(_activeColumnMenuGridKey, col.Name, true);
                }
            }

            SaveColumnVisibilitySettings();
        }

        private void ShowOperationColumns_Click(object sender, EventArgs e)
        {
            if (_activeColumnMenuGrid == null)
                return;

            foreach (DataGridViewColumn col in _activeColumnMenuGrid.Columns)
            {
                if (IsOperacijaColumn(col.Name))
                {
                    col.Visible = true;
                    SetColumnHidden(_activeColumnMenuGridKey, col.Name, false);
                }
            }

            SaveColumnVisibilitySettings();
        }

        private void SetColumnHidden(string gridKey, string columnName, bool hidden)
        {
            if (string.IsNullOrWhiteSpace(gridKey) || string.IsNullOrWhiteSpace(columnName))
                return;

            if (!_hiddenColumnsByGrid.ContainsKey(gridKey))
                _hiddenColumnsByGrid[gridKey] = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (hidden)
                _hiddenColumnsByGrid[gridKey].Add(columnName);
            else
                _hiddenColumnsByGrid[gridKey].Remove(columnName);
        }

        private void ApplyColumnVisibility(DataGridView dgv, string gridKey)
        {
            if (dgv == null || string.IsNullOrWhiteSpace(gridKey))
                return;

            if (!_hiddenColumnsByGrid.ContainsKey(gridKey))
                return;

            HashSet<string> hiddenColumns = _hiddenColumnsByGrid[gridKey];

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.Visible = !hiddenColumns.Contains(col.Name);
            }

            if (CountVisibleColumns(dgv) == 0)
            {
                foreach (DataGridViewColumn col in dgv.Columns)
                    col.Visible = true;

                hiddenColumns.Clear();
                SaveColumnVisibilitySettings();
            }
        }

        private void LoadColumnVisibilitySettings()
        {
            _hiddenColumnsByGrid.Clear();

            if (!File.Exists(_columnSettingsFile))
                return;

            string[] lines = File.ReadAllLines(_columnSettingsFile, Encoding.UTF8);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split('|');

                if (parts.Length != 2)
                    continue;

                string gridKey = parts[0].Trim();
                string columnName = parts[1].Trim();

                if (string.IsNullOrWhiteSpace(gridKey) || string.IsNullOrWhiteSpace(columnName))
                    continue;

                if (!_hiddenColumnsByGrid.ContainsKey(gridKey))
                    _hiddenColumnsByGrid[gridKey] = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                _hiddenColumnsByGrid[gridKey].Add(columnName);
            }
        }

        private void SaveColumnVisibilitySettings()
        {
            string folder = Path.GetDirectoryName(_columnSettingsFile);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            List<string> lines = new List<string>();

            foreach (var grid in _hiddenColumnsByGrid)
            {
                foreach (string columnName in grid.Value)
                {
                    if (!string.IsNullOrWhiteSpace(columnName))
                        lines.Add(grid.Key + "|" + columnName);
                }
            }

            File.WriteAllLines(_columnSettingsFile, lines.ToArray(), Encoding.UTF8);
        }

        private void FillGV()
        {
            using (SqlConnection conn = new SqlConnection(connect))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;

                cmd.CommandText = @"
SELECT N.ID,N.IdPlana,N.PodPlan,N.MP,M.MpStaraSif, ((select isnull(Sum(DNpKolZaIzdelavo - DNpIzdelanaKol),0) as UkupnoNaRN from DelNalogProizvodni where DnPSifStat in ('PL', 'LA') and  DnpSifMp = N.MP)  +  ISNULL(ST.Stanje,0)
	  - (SELECT SUM(ReKolDvig)
     FROM Rezervacije
    WHERE ReSifMp = N.MP
      AND ReBrisati = 0))as Proizvodnja,N.PlaniranaKolicina,
ISNULL(RN.RNKol,0) AS RNKol,
(select Sum(DNpKolZaIzdelavo - DNpIzdelanaKol) as UkupnoNaRN from DelNalogProizvodni where DnPSifStat in ('PL', 'LA','OD') and  DnpSifMp = N.MP) as RNsvi,
(N.Kolicina - ISNULL(RN.RNKol,0)) AS Preostala,
 (SELECT SUM(ReKolDvig)
      FROM Rezervacije
     WHERE ReSifMp = N.MP
       AND ReBrisati = 0) as RezSve, 
(select SUM(ReRezKOl) From Rezervacije inner join DelNalogProizvodni on rezervacije.ReStDokumenta=DelNalogProizvodni.DNpStevilka  Where ReSifMp=N.Mp and DNpStvProjekt=N.IDPlana) as RezervisanoPlan,
ISNULL(ST.Stanje,0) AS StanjeNaMag,
N.Kolicina as Kol,
RTrim(DP.DpKrNaziv) AS Operacija,
DP.DPInfZapSt AS OpID,
ISNULL(EU.EUKolDobrih,0) AS Odradjeno
FROM NetoPotrebeIgor N 
INNER JOIN MaticniPodatki M ON N.MP = M.MpSifra
LEFT JOIN (SELECT DnPSifMp, SUM(DNpKolZaIzdelavo) AS RNKol FROM DelNalogProizvodni WHERE DNpStvProjekt = @Plan GROUP BY DnPSifMp) RN ON RN.DnPSifMp = N.MP
LEFT JOIN (SELECT PrSifMp, SUM(PrPrevKol - PrIzdKol) AS Stanje FROM Promet where PrSifSklad <> 101 GROUP BY PrSifMp) ST ON ST.PrSifMp = N.MP
LEFT JOIN DelPostopek DP ON DP.DpSifMp = N.MP
LEFT JOIN DelNalogProizvodni DNP ON DNP.DnPSifMp = N.MP AND DNP.DNpStvProjekt = @Plan
LEFT JOIN EvidencaUr EU ON EU.EuStDelNaloga = DNP.DNpStevilka AND EU.EuStZapOperacije = DP.DPInfZapSt
WHERE N.IdPlana = @Plan AND N.PodPlan = RTRIM(@PodPlan) ORDER BY N.RB, N.ID, DP.DPInfZapSt;";


                //OD dodat na PodPlan
                cmd.Parameters.AddWithValue("@Plan", Convert.ToInt32(cboPlan.SelectedValue));
                cmd.Parameters.AddWithValue("@PodPlan",  txtPodPlan.Text );

                DataTable dt = new DataTable();
                new SqlDataAdapter(cmd).Fill(dt);

                DataTable pivot = BuildPivot(dt);

                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;

                dataGridView1.MultiSelect = false;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                dataGridView1.DataSource = pivot;

                FormatGrid();

                if (!_hiddenColumnsByGrid.ContainsKey("dataGridView1") && dataGridView1.Columns.Contains("IDPlana"))
                {
                    dataGridView1.Columns["IDPlana"].Visible = false;
                    SetColumnHidden("dataGridView1", "IDPlana", true);
                    SaveColumnVisibilitySettings();
                }

                ApplyColumnVisibility(dataGridView1, "dataGridView1");

                FillSvi(pivot);

                FillStanjeZaIzabraniRed();

                FillRezervisanoZaIzabraniRed();
            }
        }

        private void FillRezervisanoZaIzabraniRed()
        {
            if (dataGridView1.Rows.Count == 0 || !dataGridView1.Columns.Contains("MP"))
            {
                dataGridView4.DataSource = null;
                return;
            }

            DataGridViewRow row = null;

            if (dataGridView1.CurrentRow != null && !dataGridView1.CurrentRow.IsNewRow)
            {
                row = dataGridView1.CurrentRow;
            }
            else if (dataGridView1.SelectedRows.Count > 0 && !dataGridView1.SelectedRows[0].IsNewRow)
            {
                row = dataGridView1.SelectedRows[0];
            }

            if (row == null || row.Cells["MP"].Value == null || row.Cells["MP"].Value == DBNull.Value)
            {
                dataGridView4.DataSource = null;
                return;
            }

            int mp = GetIntValue(row.Cells["MP"].Value);

            if (mp == 0)
            {
                dataGridView4.DataSource = null;
                return;
            }

            string query = @"
SELECT 
    DnpStvProjekt AS PlanID,
    PlanArhiv.Naziv AS [Plan],
    SUM(ReRezKOl) AS kolicina
FROM Rezervacije
INNER JOIN DelNalogProizvodni 
    ON Rezervacije.ReStDokumenta = DelNalogProizvodni.DNpStevilka
INNER JOIN PlanArhiv 
    ON DnpStvProjekt = PlanArhiv.ID
WHERE 
    DnPSifStat IN ('PL', 'LA')
    AND ReSifMp = @MP
GROUP BY 
    DnpStvProjekt,
    PlanArhiv.Naziv
ORDER BY 
    DnpStvProjekt DESC;";

            using (SqlConnection conn = new SqlConnection(connect))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("@MP", mp);

                DataTable dtStanje = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dtStanje);
                }

                dataGridView4.ReadOnly = true;
                dataGridView4.AllowUserToAddRows = false;
                dataGridView4.DataSource = dtStanje;

                FormatSvi(dataGridView4);
                ApplyColumnVisibility(dataGridView4, "dataGridView4");
            }
        }

        private bool IsOperacijaColumn(string columnName)
        {
            return columnName != null &&
                   columnName.StartsWith("Operacija", StringComparison.OrdinalIgnoreCase);
        }

        private string CleanText(object value)
        {
            if (value == null || value == DBNull.Value)
                return "";

            return value.ToString()
                .Replace('\u00A0', ' ')
                .TrimEnd();
        }

        private int GetIntValue(object value)
        {
            if (value == null || value == DBNull.Value)
                return 0;

            int result;
            if (int.TryParse(value.ToString(), out result))
                return result;

            return 0;
        }

        private decimal GetDecimalValue(object value)
        {
            if (value == null || value == DBNull.Value)
                return 0m;

            if (value is decimal)
                return (decimal)value;

            if (value is int)
                return Convert.ToDecimal(value);

            if (value is double)
                return Convert.ToDecimal(value);

            if (value is float)
                return Convert.ToDecimal(value);

            string text = value.ToString().Trim();

            decimal result;

            if (decimal.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out result))
                return result;

            if (decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                return result;

            return 0m;
        }

        private DataTable BuildPivot(DataTable dt)
        {
            DataTable result = new DataTable();

            result.Columns.Add("ID", typeof(int));
            result.Columns.Add("IdPlana", typeof(int));
            result.Columns.Add("PodPlan", typeof(string));
            result.Columns.Add("MP", typeof(int));
            result.Columns.Add("MpStaraSif", typeof(string));
            result.Columns.Add("Proizvodnja", typeof(decimal));
            result.Columns.Add("PlaniranaKolicina", typeof(decimal));
            result.Columns.Add("RNKol", typeof(decimal));
            result.Columns.Add("RNSvi", typeof(decimal));
            result.Columns.Add("Preostala", typeof(decimal));
            result.Columns.Add("RezSve", typeof(decimal));
            result.Columns.Add("RezervisanoPlan", typeof(decimal));
            result.Columns.Add("StanjeNaMag", typeof(decimal));
            result.Columns.Add("Kol", typeof(decimal));

            if (dt == null || dt.Rows.Count == 0)
                return result;

            var groups = dt.AsEnumerable()
                .GroupBy(r => GetIntValue(r["ID"]))
                .ToList();

            int maxBrojOperacija = groups
                .Select(g =>
                    g.Where(r => r["OpID"] != DBNull.Value &&
                                 !string.IsNullOrWhiteSpace(r["OpID"].ToString()))
                     .GroupBy(r => GetIntValue(r["OpID"]))
                     .Count()
                )
                .DefaultIfEmpty(0)
                .Max();

            for (int i = 1; i <= maxBrojOperacija; i++)
            {
                result.Columns.Add("Operacija" + i, typeof(string));
            }

            foreach (var g in groups)
            {
                DataRow row = result.NewRow();
                DataRow first = g.First();

                row["ID"] = GetIntValue(first["ID"]);
                row["IdPlana"] = GetIntValue(first["IdPlana"]);
                row["PodPlan"] = CleanText(first["PodPlan"]);
                row["MP"] = GetIntValue(first["MP"]);
                row["MpStaraSif"] = CleanText(first["MpStaraSif"]);
                row["Proizvodnja"] = GetDecimalValue(first["Proizvodnja"]);
                row["PlaniranaKolicina"] = GetDecimalValue(first["PlaniranaKolicina"]);
                row["RNKol"] = GetDecimalValue(first["RNKol"]);
                row["RNSvi"] = GetDecimalValue(first["RNSvi"]);
                row["Preostala"] = GetDecimalValue(first["Preostala"]);
                row["RezSve"] = GetDecimalValue(first["RezSve"]);
                row["RezervisanoPlan"] = GetDecimalValue(first["RezervisanoPlan"]);
                row["StanjeNaMag"] = GetDecimalValue(first["StanjeNaMag"]);
                row["Kol"] = GetDecimalValue(first["Kol"]);

                var operacije = g
                    .Where(r => r["OpID"] != DBNull.Value &&
                                !string.IsNullOrWhiteSpace(r["OpID"].ToString()))
                    .GroupBy(r => GetIntValue(r["OpID"]))
                    .Select(opGroup => new
                    {
                        OpID = opGroup.Key,
                        Naziv = CleanText(opGroup.First()["Operacija"]),
                        Odradjeno = opGroup.Sum(x => GetDecimalValue(x["Odradjeno"]))
                    })
                    .OrderBy(x => x.OpID)
                    .ToList();

                int redniBroj = 1;

                foreach (var op in operacije)
                {
                    string colName = "Operacija" + redniBroj;

                    if (!result.Columns.Contains(colName))
                        break;

                    row[colName] = op.Naziv + "-" + op.Odradjeno.ToString("0.###");

                    redniBroj++;
                }

                result.Rows.Add(row);
            }

            return result;
        }

        private void FormatGrid()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1.AllowUserToAddRows = false;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;

                if (IsOperacijaColumn(col.Name))
                {
                    col.HeaderText = "";
                    col.MinimumWidth = 35;
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    col.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                }
                else
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }
            }

            SetColumnWidthIfExists("ID", 55);
            SetColumnWidthIfExists("IdPlana", 65);
            SetColumnWidthIfExists("PodPlan", 130);
            SetColumnWidthIfExists("MP", 70);
            SetColumnWidthIfExists("MpStaraSif", 150);
            SetColumnWidthIfExists("Kolicina", 90);
            SetColumnWidthIfExists("PlaniranaKolicina", 120);
            SetColumnWidthIfExists("RNKol", 90);
            SetColumnWidthIfExists("RNSvi", 90);
            SetColumnWidthIfExists("Preostala", 90);
            SetColumnWidthIfExists("RezSve", 100);
            SetColumnWidthIfExists("RezervisanoPlan", 100);
            SetColumnWidthIfExists("StanjeNaMag", 100);
            SetColumnWidthIfExists("ZalihaLargo", 90);

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (IsOperacijaColumn(col.Name))
                {
                    dataGridView1.AutoResizeColumn(
                        col.Index,
                        DataGridViewAutoSizeColumnMode.AllCells
                    );
                }
            }
        }

        private void SetColumnWidthIfExists(string columnName, int width)
        {
            if (!dataGridView1.Columns.Contains(columnName))
                return;

            dataGridView1.Columns[columnName].Width = width;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string colName = dataGridView1.Columns[e.ColumnIndex].Name;

            if (!IsOperacijaColumn(colName))
                return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            if (!dataGridView1.Columns.Contains("RNKol"))
                return;

            if (row.Cells["RNKol"].Value == null || row.Cells["RNKol"].Value == DBNull.Value)
                return;

            if (e.Value == null || e.Value == DBNull.Value)
                return;

            string val = e.Value.ToString();

            if (string.IsNullOrWhiteSpace(val))
                return;

            int index = val.LastIndexOf('-');

            if (index < 0)
                return;

            string odrText = val.Substring(index + 1).Trim();

            decimal potrebno = GetDecimalValue(row.Cells["RNKol"].Value);
            decimal odr = GetDecimalValue(odrText);

            if (odr == 0)
            {
                e.CellStyle.BackColor = Color.LightCoral;
            }
            else if (odr < potrebno)
            {
                e.CellStyle.BackColor = Color.Khaki;
            }
            else
            {
                e.CellStyle.BackColor = Color.LightGreen;
            }
        }

        private DataTable BuildArtikliTable(DataTable pivot)
        {
            DataTable artikli = new DataTable();
            artikli.Columns.Add("MP", typeof(int));
            artikli.Columns.Add("Kolicina", typeof(decimal));

            if (pivot == null || pivot.Rows.Count == 0)
                return artikli;

            var podaci = pivot.AsEnumerable()
                .Where(r => r["MP"] != DBNull.Value)
                .GroupBy(r => Convert.ToInt32(r["MP"]))
                .Select(g =>
                {
                    decimal ukupnaKolicina = 0;

                    foreach (DataRow r in g)
                    {
                        if (r["PlaniranaKolicina"] != DBNull.Value)
                        {
                            decimal tmp;
                            if (decimal.TryParse(r["PlaniranaKolicina"].ToString(), out tmp))
                                ukupnaKolicina += tmp;
                        }
                    }

                    return new
                    {
                        MP = g.Key,
                        Kolicina = ukupnaKolicina
                    };
                })
                .ToList();

            foreach (var p in podaci)
                artikli.Rows.Add(p.MP, p.Kolicina);

            return artikli;
        }

        private void CreateAndFillTempArtikli(SqlConnection conn, DataTable artikli)
        {
            using (SqlCommand cmd = new SqlCommand(@"
CREATE TABLE #Artikli
(
    MP INT NOT NULL,
    Kolicina DECIMAL(18,4) NOT NULL
);

CREATE CLUSTERED INDEX IX_Artikli_MP ON #Artikli(MP);
", conn))
            {
                cmd.CommandTimeout = 120;
                cmd.ExecuteNonQuery();
            }

            using (SqlBulkCopy bulk = new SqlBulkCopy(conn))
            {
                bulk.DestinationTableName = "#Artikli";
                bulk.BulkCopyTimeout = 120;

                bulk.ColumnMappings.Add("MP", "MP");
                bulk.ColumnMappings.Add("Kolicina", "Kolicina");

                bulk.WriteToServer(artikli);
            }
        }

        private DataTable ExecuteDataTable(SqlConnection conn, string query)
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.CommandTimeout = 120;

                DataTable dt = new DataTable();
                cmd.Parameters.AddWithValue("@Plan", Convert.ToInt32(cboPlan.SelectedValue));
                cmd.Parameters.AddWithValue("@PodPlan", txtPodPlan.Text);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                return dt;
            }
        }

        private void FillSvi(DataTable pivot)
        {
            DataTable artikli = BuildArtikliTable(pivot);

            if (artikli.Rows.Count == 0)
            {
                dataGridView2.DataSource = null;
                dataGridView3.DataSource = null;
                return;
            }

            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();

                CreateAndFillTempArtikli(conn, artikli);

                string queryPotrebneKolicine = @"
;WITH Osnova AS
(
    SELECT
        K.KoPodSifMp AS KoPodSifMp,

        RTrim(M.MpStaraSif) + ' / ' + ISNULL(RTrim(M.MpNaziv), '') AS NazivMP,

        CONVERT(NUMERIC(22,4),
            SUM(
                (
                    (K.KoKolMateriala * 30 * (1 + (K.KoIzmet / 100.0))) / 30
                ) * A.Kolicina
            )
        ) AS KoKolMateriala,

        ISNULL(
            (
                SELECT SUM(ISNULL(Pr.PrIzdKol, 0))
                FROM Promet Pr
                WHERE 
                    Pr.PrSifMp = K.KoPodSifMp
                    AND Pr.PrStDokumenta IN
                    (
                        SELECT DnPStevilka
                        FROM DelNalogProizvodni
                        WHERE 
                            DnpStvProjekt = @Plan
                            AND CONVERT(nvarchar, dnpNote) = @PodPlan
                    )
            ), 0
        ) AS Potroseno,

        M.MpSifEnoteMere1 AS JM,

        ISNULL(M.MpOsnSklad, 0) AS MpOsnSklad,

        ISNULL(M.MpOsnLokacija, '') AS MpOsnLokacija

    FROM #Artikli A
    INNER JOIN Kosovnica K 
        ON K.KoNadSifMp = A.MP
    INNER JOIN MaticniPodatki M 
        ON M.MpSifra = K.KoPodSifMp
    WHERE 
        K.KoVarianta = '*'
        AND K.KoSifStat = 'A'
    GROUP BY
        K.KoPodSifMp,
        RTrim(M.MpStaraSif) + ' / ' + ISNULL(RTrim(M.MpNaziv), ''),
        M.MpSifEnoteMere1,
        ISNULL(M.MpOsnSklad, 0),
        ISNULL(M.MpOsnLokacija, '')
)
SELECT
    KoPodSifMp,
    NazivMP,
    KoKolMateriala,
    Potroseno,
    CONVERT(NUMERIC(22,4), KoKolMateriala - Potroseno) AS Potrebno,
    JM,
    MpOsnSklad,
    MpOsnLokacija
FROM Osnova
ORDER BY
    KoPodSifMp;
";

                DataTable dtKolicine = ExecuteDataTable(conn, queryPotrebneKolicine);

                dataGridView3.ReadOnly = true;
                dataGridView3.AllowUserToAddRows = false;
                dataGridView3.DataSource = dtKolicine;

                FormatSvi(dataGridView3);
                ApplyColumnVisibility(dataGridView3, "dataGridView3");
            }
        }

        private void FillStanjeZaIzabraniRed()
        {
            if (dataGridView1.Rows.Count == 0 || !dataGridView1.Columns.Contains("MP"))
            {
                dataGridView2.DataSource = null;
                return;
            }

            DataGridViewRow row = null;

            if (dataGridView1.CurrentRow != null && !dataGridView1.CurrentRow.IsNewRow)
            {
                row = dataGridView1.CurrentRow;
            }
            else if (dataGridView1.SelectedRows.Count > 0 && !dataGridView1.SelectedRows[0].IsNewRow)
            {
                row = dataGridView1.SelectedRows[0];
            }

            if (row == null || row.Cells["MP"].Value == null || row.Cells["MP"].Value == DBNull.Value)
            {
                dataGridView2.DataSource = null;
                return;
            }

            int mp = GetIntValue(row.Cells["MP"].Value);

            if (mp == 0)
            {
                dataGridView2.DataSource = null;
                return;
            }

            string query = @"
SELECT
    P.PrSifSklad AS [Sif.skl.],
    P.PrSifMp AS [Sif.Mp.],
    M.MpStaraSif AS [Code],
    M.MpNaziv AS [Naziv MP],
    SUM(ISNULL(P.PrPrevKol, 0) - ISNULL(P.PrIzdKol, 0)) AS [Stv.kol],
    M.MPSifEnoteMere1 AS [JM]
FROM Promet P
INNER JOIN MaticniPodatki M
    ON M.MPSifra = P.PrSifMP
WHERE
    P.PrSifMp = @MP
GROUP BY
    P.PrSifSklad,
    P.PrSifMp,
    M.MpStaraSif,
    M.MpNaziv,
    M.MPSifEnoteMere1
HAVING SUM(ISNULL(P.PrPrevKol, 0) - ISNULL(P.PrIzdKol, 0)) <> 0
ORDER BY
    P.PrSifSklad;
";

            using (SqlConnection conn = new SqlConnection(connect))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("@MP", mp);

                DataTable dtStanje = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dtStanje);
                }

                dataGridView2.ReadOnly = true;
                dataGridView2.AllowUserToAddRows = false;
                dataGridView2.DataSource = dtStanje;

                FormatSvi(dataGridView2);
                ApplyColumnVisibility(dataGridView2, "dataGridView2");
            }
        }

        private void FormatSvi(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            FillPodplanovi();
        }

        private void FillPodplanovi()
        {
            var query = @"select distinct Podplan from NetopotrebeIgor Where IDPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + " and PodPlan<>'' order by Podplan";

            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView5.ReadOnly = true;
            dataGridView5.DataSource = ds.Tables[0];


            DataGridViewColumn column2 = dataGridView5.Columns[0];
            dataGridView5.Columns[0].HeaderText = "PodPlan";
            dataGridView5.Columns[0].Width = 260;

            //FormatSvi(dataGridView5);
            ApplyColumnVisibility(dataGridView5, "dataGridView5");
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            FillStanjeZaIzabraniRed();
            FillRezervisanoZaIzabraniRed();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillGV();
        }

        private void dataGridView5_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                if (row.Selected)
                {
                    txtPodPlan.Text = row.Cells[0].Value.ToString();
                    panel1.Visible = false;
                    FillGV();
                }
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            InsertStampaNP inp = new InsertStampaNP();
            inp.DeleteAll();

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row == null || row.IsNewRow)
                    continue;

                if (row.Cells["KoPodSifMp"].Value == null || row.Cells["KoPodSifMp"].Value == DBNull.Value)
                    continue;

                inp.InsertNP(
                    Convert.ToInt32(row.Cells["KoPodSifMp"].Value.ToString()),
                    row.Cells["NazivMP"].Value == null ? "" : row.Cells["NazivMP"].Value.ToString(),
                    Convert.ToDouble(row.Cells["KoKolMateriala"].Value.ToString()),
                    Convert.ToDouble(row.Cells["Potroseno"].Value.ToString()),
                    row.Cells["JM"].Value == null ? "" : row.Cells["JM"].Value.ToString(),
                    cboPlan.Text,
                    txtPodPlan.Text
                );
            }

            StampaNP staNP = new StampaNP(Convert.ToInt32(cboPlan.SelectedValue), txtPodPlan.Text);
            staNP.Show();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            StampaPodPlana sp = new StampaPodPlana(Convert.ToInt32(cboPlan.SelectedValue), txtPodPlan.Text);
            sp.Show();
        }
    }
}