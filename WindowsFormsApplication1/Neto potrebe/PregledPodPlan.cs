using DocumentFormat.OpenXml.Office.Word;
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

namespace WindowsFormsApplication1.Neto_potrebe
{
    public partial class PregledPodPlan : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        int tmpPlanID = 0;
        string tmpPodPlan = "";

        public PregledPodPlan()
        {
            InitializeComponent();
            panel1.Visible = false;
        }

        public PregledPodPlan(int PlanID, string PodPlan)
        {
            InitializeComponent();
            tmpPlanID = PlanID;
            tmpPodPlan = PodPlan;
            panel1.Visible = false;
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

        private void FillGV()
        {
            using (SqlConnection conn = new SqlConnection(connect))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;

                cmd.CommandText = @"
SELECT N.ID,N.IdPlana,N.PodPlan,N.MP,M.MpStaraSif,N.Kolicina,N.PlaniranaKolicina,
ISNULL(RN.RNKol,0) AS RNKol,
(select Sum(DNpKolZaIzdelavo - DNpIzdelanaKol) as UkupnoNaRN from DelNalogProizvodni where DnPSifStat in ('PL', 'LA') and  DnpSifMp = N.MP) as RNsvi,
(N.Kolicina - ISNULL(RN.RNKol,0)) AS Preostala,
 (SELECT SUM(ReKolDvig)
      FROM Rezervacije
     WHERE ReSifMp = N.MP
       AND ReBrisati = 0) as RezSve, 
(select SUM(ReRezKOl) From Rezervacije inner join DelNalogProizvodni on rezervacije.ReStDokumenta=DelNalogProizvodni.DNpStevilka  Where ReSifMp=N.Mp and DNpStvProjekt=N.IDPlana) as RezervisanoPlan,
ISNULL(ST.Stanje,0) AS StanjeNaMag,
((select Sum(DNpKolZaIzdelavo - DNpIzdelanaKol) as UkupnoNaRN from DelNalogProizvodni where DnPSifStat in ('PL', 'LA') and  DnpSifMp = N.MP)  +  ISNULL(ST.Stanje,0)
	  - (SELECT SUM(ReKolDvig)
     FROM Rezervacije
    WHERE ReSifMp = N.MP
      AND ReBrisati = 0)) as ZalihaLargo,
RTrim(DP.DpKrNaziv) AS Operacija,
DP.DPInfZapSt AS OpID,
ISNULL(EU.EUKolDobrih,0) AS Odradjeno
FROM NetoPotrebeIgor N 
INNER JOIN MaticniPodatki M ON N.MP = M.MpSifra
LEFT JOIN (SELECT DnPSifMp, SUM(DNpKolZaIzdelavo) AS RNKol FROM DelNalogProizvodni WHERE DNpStvProjekt = @Plan GROUP BY DnPSifMp) RN ON RN.DnPSifMp = N.MP
LEFT JOIN (SELECT PrSifMp, SUM(PrPrevKol - PrIzdKol) AS Stanje FROM Promet GROUP BY PrSifMp) ST ON ST.PrSifMp = N.MP
LEFT JOIN DelPostopek DP ON DP.DpSifMp = N.MP
LEFT JOIN DelNalogProizvodni DNP ON DNP.DnPSifMp = N.MP AND DNP.DNpStvProjekt = @Plan
LEFT JOIN EvidencaUr EU ON EU.EuStDelNaloga = DNP.DNpStevilka AND EU.EuStZapOperacije = DP.DPInfZapSt
WHERE N.IdPlana = @Plan AND N.PodPlan LIKE @PodPlan ORDER BY N.ID, DP.DPInfZapSt;";

                cmd.Parameters.AddWithValue("@Plan", Convert.ToInt32(cboPlan.SelectedValue));
                cmd.Parameters.AddWithValue("@PodPlan", "%" + txtPodPlan.Text + "%");

                DataTable dt = new DataTable();
                new SqlDataAdapter(cmd).Fill(dt);

                DataTable pivot = BuildPivot(dt);

                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;

                dataGridView1.MultiSelect = false;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                dataGridView1.DataSource = pivot;

                dataGridView1.Columns["IDPlana"].Visible = false;

                FormatGrid();

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

            string query = " select DnpStvProjekt as PlanID, SUM(ReRezKOl) as kolinina  From Rezervacije inner join DelNalogProizvodni on rezervacije.ReStDokumenta=DelNalogProizvodni.DNpStevilka " +
" Where ReSifMp= " + mp +
" group by DnpStvProjekt order by DnpStvProjekt desc";


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
            result.Columns.Add("Kolicina", typeof(decimal));
            result.Columns.Add("PlaniranaKolicina", typeof(decimal));
            result.Columns.Add("RNKol", typeof(decimal));
            result.Columns.Add("RNSvi", typeof(decimal));
            result.Columns.Add("Preostala", typeof(decimal));
            result.Columns.Add("RezSve", typeof(decimal));
            result.Columns.Add("RezervisanoPlan", typeof(decimal));
            result.Columns.Add("StanjeNaMag", typeof(decimal));
            result.Columns.Add("ZalihaLargo", typeof(decimal));

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
                row["Kolicina"] = GetDecimalValue(first["Kolicina"]);
                row["PlaniranaKolicina"] = GetDecimalValue(first["PlaniranaKolicina"]);
                row["RNKol"] = GetDecimalValue(first["RNKol"]);
                row["RNSvi"] = GetDecimalValue(first["RNSvi"]);
                row["Preostala"] = GetDecimalValue(first["Preostala"]);
                row["RezSve"] = GetDecimalValue(first["RezSve"]);
                row["RezervisanoPlan"] = GetDecimalValue(first["RezervisanoPlan"]);
                row["StanjeNaMag"] = GetDecimalValue(first["StanjeNaMag"]);
                row["ZalihaLargo"] = GetDecimalValue(first["ZalihaLargo"]);

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
                        if (r["Kolicina"] != DBNull.Value)
                        {
                            decimal tmp;
                            if (decimal.TryParse(r["Kolicina"].ToString(), out tmp))
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
                cmd.Parameters.AddWithValue("@PodPlan", txtPodPlan.Text );

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
SELECT
    K.KoPodSifMp AS KoPodSifMp,
    RTrim(M.MpStaraSif) + ' / ' + ISNULL(RTrim(M.MpNaziv), '') AS NazivMP,
    CONVERT(NUMERIC(22,4),SUM(((K.KoKolMateriala * 30 * (1 + (K.KoIzmet / 100.0))) / 30) * A.Kolicina)) AS KoKolMateriala,
 ISNULL((SELECT SUM(ISNULL(Pr.PrIzdKol,0)) FROM Promet Pr WHERE Pr.PrSifMp = K.KoPodSifMp and PrStDokumenta in (Select DnPStevilka from DelNalogProizvodni where DnpStvProjekt =   @Plan  and CONVERT(nvarchar,dnpNote) =  @PodPlan)), 0) AS Potroseno,
    M.MpSifEnoteMere1 AS JM,
    ISNULL(M.MpOsnSklad, 0) AS MpOsnSklad,
    ISNULL(M.MpOsnLokacija, '') AS MpOsnLokacija
    -- Potroseno: ukupno izdato/utrošeno iz promena (Promet) za dati MP
FROM #Artikli A
INNER JOIN Kosovnica K ON K.KoNadSifMp = A.MP
INNER JOIN MaticniPodatki M ON M.MpSifra = K.KoPodSifMp
WHERE K.KoVarianta = '*' AND K.KoSifStat = 'A'
GROUP BY
    K.KoPodSifMp,
    RTrim(M.MpStaraSif) + ' / ' + ISNULL(RTrim(M.MpNaziv), ''),
    M.MpSifEnoteMere1,
    ISNULL(M.MpOsnSklad, 0),
    ISNULL(M.MpOsnLokacija, '')
ORDER BY
    K.KoPodSifMp;
";
               

                DataTable dtKolicine = ExecuteDataTable(conn, queryPotrebneKolicine);

                dataGridView3.ReadOnly = true;
                dataGridView3.AllowUserToAddRows = false;
                dataGridView3.DataSource = dtKolicine;

                FormatSvi(dataGridView3);
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
	HAVING SUM(ISNULL(P.PrPrevKol, 0) - ISNULL(P.PrIzdKol, 0))<>0
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
            var query = @"select distinct Podplan from NetopotrebeIgor Where IDPlana=" + Convert.ToInt32(cboPlan.SelectedValue) + " and PodPlan<>''";

            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView5.ReadOnly = true;
            dataGridView5.DataSource = ds.Tables[0];
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
                if (row != null && row.Cells[0].Value != null)
                {
                    inp.InsertNP(Convert.ToInt32(row.Cells[0].Value.ToString()), row.Cells[1].Value.ToString(), Convert.ToDouble(row.Cells[2].Value.ToString()), Convert.ToDouble(row.Cells[3].Value.ToString()), row.Cells[4].Value.ToString(), cboPlan.Text, txtPodPlan.Text);
            
                }
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