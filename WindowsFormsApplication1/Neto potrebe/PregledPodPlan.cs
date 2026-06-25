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
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString; int tmpPlanID = 0; string tmpPodPlan = ""; public PregledPodPlan() { InitializeComponent(); }

        public PregledPodPlan(int PlanID, string PodPlan)
        {
            InitializeComponent();
            tmpPlanID = PlanID;
            tmpPodPlan = PodPlan;
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
            FillPlanovi();
            txtPodPlan.Text = tmpPodPlan;
            cboPlan.SelectedValue = tmpPlanID;
            FillGV();
        }

        private void FillGV()
        {
            using (SqlConnection conn = new SqlConnection(connect))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;

                cmd.CommandText = @"
SELECT N.ID,N.IdPlana,N.PodPlan,N.MP,M.MpStaraSif,N.Kolicina,N.PlaniranaKolicina,
ISNULL(RN.RNKol,0) AS RNKol,(N.Kolicina - ISNULL(RN.RNKol,0)) AS Preostala,
(select SUM(ReRezKOl) From Rezervacije inner join DelNalogProizvodni on rezervacije.ReStDokumenta=DelNalogProizvodni.DNpStevilka  Where ReSifMp=N.Mp and DNpStvProjekt=N.IDPlana) as Rezervisano,
ISNULL(ST.Stanje,0) AS StanjeNaMag,
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
                dataGridView1.DataSource = pivot;


                dataGridView1.Columns["IDPlana"].Visible = false;

                FormatGrid();

                FillSvi(pivot);
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
            result.Columns.Add("Preostala", typeof(decimal));
            result.Columns.Add("Rezervisano", typeof(decimal));
            result.Columns.Add("StanjeNaMag", typeof(decimal));

            if (dt == null || dt.Rows.Count == 0)
                return result;

            // Važno:
            // Grupisanje po ID znači da svaka stavka iz NetoPotrebeIgor ostaje poseban red.
            // Ako baš želiš jedan red po MP, promeni "ID" u "MP".
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
                row["Preostala"] = GetDecimalValue(first["Preostala"]);
                row["Rezervisano"] = GetDecimalValue(first["Rezervisano"]);
                row["StanjeNaMag"] = GetDecimalValue(first["StanjeNaMag"]);

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
            SetColumnWidthIfExists("Preostala", 90);
            SetColumnWidthIfExists("Rezervisano", 100);
            SetColumnWidthIfExists("StanjeNaMag", 100);

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

                string queryStanje = @"
SELECT
    P.PrSifSklad AS [Sif.skl.],
    P.PrSifMp AS [Sif.Mp.],
    M.MpStaraSif AS [Code],
    M.MpNaziv AS [Naziv MP],
    SUM(ISNULL(P.PrPrevKol, 0) - ISNULL(P.PrIzdKol, 0)) AS [Stv.kol],
    M.MPSifEnoteMere1 AS [JM]
FROM #Artikli A
INNER JOIN Promet P
    ON P.PrSifMp = A.MP
INNER JOIN MaticniPodatki M
    ON M.MPSifra = P.PrSifMp
GROUP BY
    P.PrSifSklad,
    P.PrSifMp,
    M.MpStaraSif,
    M.MpNaziv,
    M.MPSifEnoteMere1
ORDER BY
    P.PrSifMp,
    P.PrSifSklad;
";

                string queryPotrebneKolicine = @"
SELECT
    K.KoPodSifMp AS KoPodSifMp,
    RTrim(M.MpNaziv) + ' ' + ISNULL(RTrim(M.MpDoNaziv), '') AS NazivMP,
    CONVERT(NUMERIC(22,4),
        SUM(
            (
                (K.KoKolMateriala * 30 * (1 + (K.KoIzmet / 100.0))) / 30
            ) * A.Kolicina
        )
    ) AS KoKolMateriala,
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
    RTrim(M.MpNaziv) + ' ' + ISNULL(RTrim(M.MpDoNaziv), ''),
    M.MpSifEnoteMere1,
    ISNULL(M.MpOsnSklad, 0),
    ISNULL(M.MpOsnLokacija, '')
ORDER BY
    K.KoPodSifMp;
";

                DataTable dtStanje = ExecuteDataTable(conn, queryStanje);
                DataTable dtKolicine = ExecuteDataTable(conn, queryPotrebneKolicine);

                dataGridView2.ReadOnly = true;
                dataGridView2.AllowUserToAddRows = false;
                dataGridView2.DataSource = dtStanje;

                dataGridView3.ReadOnly = true;
                dataGridView3.AllowUserToAddRows = false;
                dataGridView3.DataSource = dtKolicine;

                FormatSvi(dataGridView2);
                FormatSvi(dataGridView3);
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
        /*

        public class Stanje
        {
            public int SifSklad { get; set; }
            public int MpSifra { get; set; }
            public string Code { get; set; }
            public string NazivMP { get; set; }
            public decimal StvKol { get; set; }
            public string JM { get; set; }
        }
        List<Stanje> stanje = new List<Stanje>();
        private void FillGV2Svi()
        {
            stanje.Clear();
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    int MP = Convert.ToInt32(row.Cells["MP"].Value);
                    using (SqlConnection conn = new SqlConnection(connect))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(@"select PrSifSklad [Sif.skl.],PrSifMp [Sif.Mp.], MpStaraSif [Code],MpNaziv [Naziv MP],
sum(PrPrevKol - PrIzdKol) [Stv.kol],MPSifEnoteMere1 ['JM'] 
from Promet 
inner join MaticniPodatki on MPSifra = PrSifMP 
where PrSifMp = " + MP + " group by PrSifSklad, PrSifMp, MpStaraSif,MpNaziv,MPSifEnoteMere1", conn))
                        {
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    var i = new Stanje
                                    {
                                        SifSklad = Convert.ToInt32(dr[0].ToString()),
                                        MpSifra = Convert.ToInt32(dr[1].ToString()),
                                        Code = dr[2].ToString().TrimEnd(),
                                        NazivMP = dr[3].ToString().TrimEnd(),
                                        StvKol = Convert.ToDecimal(dr[4].ToString()),
                                        JM = dr[5].ToString().TrimEnd()
                                    };

                                    stanje.Add(i);
                                }
                            }
                        }
                        conn.Close();
                    }
                }
            }
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = stanje.ToList();
        }
        public class PotrebneKolicine
        {
            public int KoPodSifMp { get; set; }
            public string NazivMP { get; set; }
            public string MpSifEnoteMere1 { get; set; }
            public decimal Potrebno { get; set; }
            public decimal KoKolMaterijala { get; set; }
            public int MpOsnSklad { get; set; }
            public string MpOsnLokacija { get; set; }
        }
        List<PotrebneKolicine> kolicine = new List<PotrebneKolicine>();
        private void FillGV3Svi()
        {
            kolicine.Clear();
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    int MP = Convert.ToInt32(row.Cells["MP"].Value);
                    decimal Kolicina = Convert.ToDecimal(row.Cells["Kolicina"].Value);
                    using (SqlConnection conn = new SqlConnection(connect))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(@"select KoPodSifMp,MpNaziv + ' ' + ISNULL(MpDoNaziv,'') as NazivMP,MpSifEnoteMere1,
convert(numeric(22,4),(sum(KoKolMateriala * 30 * (1 + (KoIzmet / 100.0))))/30) * " + Kolicina + "  as KoKolMateriala,ISNULL(MpOsnSklad, 0) as MpOsnSklad, " + " ISNULL(MpOsnLokacija, '') as MpOsnLokacija " +
"from kosovnica, maticnipodatki " +
" where KoNadSifMp =  " + MP + "   and KoVarianta = '*'   and KoPodSifMp = MpSifra   and KoSifStat = 'A' " +
"group by KoPodSifMP, MpNaziv + ' ' + ISNULL(MpDoNaziv,''), MpSifEnoteMere1, ISNULL(MpOsnSklad, 0),ISNULL(MpOsnLokacija, '') ", conn))
                        {
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    var i = new PotrebneKolicine
                                    {
                                        KoPodSifMp = Convert.ToInt32(dr[0].ToString()),
                                        NazivMP = dr[1].ToString().TrimEnd(),
                                        MpSifEnoteMere1 = dr[2].ToString().TrimEnd(),
                                        Potrebno = Convert.ToDecimal(dr[3].ToString()),
                                        KoKolMaterijala = Convert.ToDecimal(dr[4].ToString()),
                                        MpOsnSklad = Convert.ToInt32(dr[5].ToString()),
                                        MpOsnLokacija = dr[6].ToString().TrimEnd()
                                    };

                                    kolicine.Add(i);
                                }
                            }
                        }
                        conn.Close();
                    }
                }
            }
            dataGridView3.DataSource = null;
            dataGridView3.DataSource = kolicine.ToList();
        }
        

        private void FillGV2(int MP)
        {
            var query = @"select PrSifSklad [Sif.skl.],PrSifMp [Sif.Mp.], MpStaraSif [Code],MpNaziv [Naziv MP],sum(PrPrevKol - PrIzdKol) [Stv.kol],MPSifEnoteMere1 ['JM'] 
from Promet 
inner join MaticniPodatki on MPSifra = PrSifMP 
where PrSifMp =  " + MP + " group by PrSifSklad, PrSifMp, MpStaraSif,MpNaziv,MPSifEnoteMere1";

            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];
            
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "IDPlana";
            dataGridView1.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Podplan";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Mp";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Code";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Kolicina";
            dataGridView1.Columns[5].Width = 90;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Planirana kol";
            dataGridView1.Columns[6].Width = 90;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "RN kol";
            dataGridView1.Columns[7].Width = 90;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Preostala";
            dataGridView1.Columns[8].Width = 90;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Rezervisano";
            dataGridView1.Columns[9].Width = 90;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Stanje";
            dataGridView1.Columns[10].Width = 90;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Po operacijama";
            dataGridView1.Columns[11].Width = 290;
            

        }


        private void FillGV3(int MP, decimal Kol)
        {
            var query = @"select KoPodSifMp,MpNaziv + ' ' + ISNULL(MpDoNaziv,'') as NazivMP,MpSifEnoteMere1,
convert(numeric(22,4),(sum(KoKolMateriala * 30 * (1 + (KoIzmet / 100.0))))/30) * " + Kol + "  as KoKolMateriala,ISNULL(MpOsnSklad, 0) as MpOsnSklad, " + " ISNULL(MpOsnLokacija, '') as MpOsnLokacija " +
"from kosovnica, maticnipodatki " +
" where KoNadSifMp =  " + MP + "   and KoVarianta = '*'   and KoPodSifMp = MpSifra   and KoSifStat = 'A' " +
"group by KoPodSifMP, MpNaziv + ' ' + ISNULL(MpDoNaziv,''), MpSifEnoteMere1, ISNULL(MpOsnSklad, 0),ISNULL(MpOsnLokacija, '') ";

            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];
            
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "IDPlana";
            dataGridView1.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Podplan";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Mp";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Code";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Kolicina";
            dataGridView1.Columns[5].Width = 90;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Planirana kol";
            dataGridView1.Columns[6].Width = 90;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "RN kol";
            dataGridView1.Columns[7].Width = 90;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Preostala";
            dataGridView1.Columns[8].Width = 90;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Rezervisano";
            dataGridView1.Columns[9].Width = 90;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Stanje";
            dataGridView1.Columns[10].Width = 90;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Po operacijama";
            dataGridView1.Columns[11].Width = 290;
            

        }
        */
        private void iconButton1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            var row = dataGridView1.SelectedRows[0];

            if (row == null || row.Cells["MP"].Value == null)
                return;

            int mp = Convert.ToInt32(row.Cells["MP"].Value);

            decimal kol = 0;
            decimal.TryParse(row.Cells["Kolicina"].Value?.ToString(), out kol);

            //FillGV2(mp);
            //FillGV3(mp, kol);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillGV();
        }
    }

}