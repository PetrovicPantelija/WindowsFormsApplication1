using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Events;
using Syncfusion.WinForms.DataGridConverter;
using Syncfusion.XlsIO;

namespace WindowsFormsApplication1.Nabavka
{
    public partial class frmProizvodnjaPotrebe : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        private const string SviPodPlanovi = "* All";

        public static string code = "frmProizvodnjaPotrebe";
        public bool Pravo;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string niz = "";
        string Kor = frmLogovanje.user.ToString().TrimEnd();

        // sprecava rekurziju kada se u ItemCheck menja stanje drugih stavki
        private bool _menjamPodPlanove;

        // podaci iz baze (pre umanjenja tabli) i tabela prikazana u gridu
        private DataTable _dtOriginal;
        private DataTable _dtPrikaz;
        private string _osnovniInfo = "";

        private class PlanStavka
        {
            public int ID;
            public string Naziv;
            public override string ToString() { return ID + " - " + Naziv; }
        }

        public frmProizvodnjaPotrebe()
        {
            InitializeComponent();
            JelaTema.Primeni(this);

            IdGrupe();
            IdForme();
            PravoPristupa();

            // Boje Jela dekora (Evergreen mat i Soft bež), kao MainNabavka
            this.BackColor = Color.FromArgb(248, 247, 244);
            panelFilter.BackColor = Color.FromArgb(248, 247, 244);
            sfGrid.Style.HeaderStyle.BackColor = Color.FromArgb(58, 77, 57);
            sfGrid.Style.HeaderStyle.TextColor = Color.White;
            sfGrid.Style.HeaderStyle.Font.Bold = true;
        }

        public string IdGrupe()
        {
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    int count = 0;
                    while (dr.Read())
                    {
                        if (count == 0)
                            niz = dr["IdGrupe"].ToString();
                        else
                            niz = niz + "," + dr["IdGrupe"].ToString();
                        count++;
                    }
                }
            }
            return niz;
        }

        private int IdForme()
        {
            string query = "Select IdForme from Forme where Rtrim(Code)=" + "'" + code + "'";
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        idForme = Convert.ToInt32(dr["IdForme"].ToString());
                    }
                }
            }
            return idForme;
        }

        private void PravoPristupa()
        {
            if (string.IsNullOrWhiteSpace(niz) || idForme == 0)
            {
                MessageBox.Show("Nemate prava za pristup ovoj formi", code);
                Pravo = false;
                return;
            }

            string query = "Select * From GrupaForma Where IdGrupe in (" + niz + ") and IdForme=" + idForme;
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
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
                            update = Convert.ToBoolean(reader["Izmena"]);
                            delete = Convert.ToBoolean(reader["Brisanje"]);
                        }
                    }
                }
            }
        }

        private void frmProizvodnjaPotrebe_Load(object sender, EventArgs e)
        {
            FillPlanovi();
            FillPodPlanovi();
        }

        // Iste vrednosti kao cboPlan u formi PregledPodPlan
        private void FillPlanovi()
        {
            clbPlanovi.Items.Clear();

            using (SqlConnection conn = new SqlConnection(connect))
            using (SqlCommand cmd = new SqlCommand("Select ID,Naziv From PlanArhiv order by ID asc", conn))
            {
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        clbPlanovi.Items.Add(new PlanStavka
                        {
                            ID = Convert.ToInt32(dr["ID"]),
                            Naziv = dr["Naziv"] == DBNull.Value ? "" : dr["Naziv"].ToString().TrimEnd()
                        });
                    }
                }
            }
        }

        private List<int> CekiraniPlanovi()
        {
            return clbPlanovi.CheckedItems.Cast<PlanStavka>().Select(p => p.ID).ToList();
        }

        private bool IzabranoSviPodPlanovi()
        {
            return clbPodPlanovi.CheckedItems.Cast<object>().Any(o => o.ToString() == SviPodPlanovi);
        }

        private List<string> CekiraniPodPlanovi()
        {
            return clbPodPlanovi.CheckedItems.Cast<object>()
                .Select(o => o.ToString())
                .Where(s => s != SviPodPlanovi)
                .ToList();
        }

        // Distinct podplanovi svih cekiranih planova + opcija "* All"
        private void FillPodPlanovi()
        {
            bool bioAll = clbPodPlanovi.Items.Count == 0 || IzabranoSviPodPlanovi();
            HashSet<string> prethodni = new HashSet<string>(CekiraniPodPlanovi());

            List<int> planovi = CekiraniPlanovi();

            _menjamPodPlanove = true;
            try
            {
                clbPodPlanovi.Items.Clear();
                clbPodPlanovi.Items.Add(SviPodPlanovi, bioAll);

                if (planovi.Count == 0)
                    return;

                string query = "select distinct RTrim(PodPlan) as PodPlan from NetoPotrebeIgor Where IdPlana in (" +
                               string.Join(",", planovi) + ") and PodPlan<>'' order by 1";

                using (SqlConnection conn = new SqlConnection(connect))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandTimeout = 120;
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string pp = dr["PodPlan"].ToString();
                            clbPodPlanovi.Items.Add(pp, !bioAll && prethodni.Contains(pp));
                        }
                    }
                }
            }
            finally
            {
                _menjamPodPlanove = false;
            }
        }

        private void clbPlanovi_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // CheckedItems se azurira tek posle ItemCheck dogadjaja
            BeginInvoke((MethodInvoker)FillPodPlanovi);
        }

        // "* All" i pojedinacni podplanovi se iskljucuju medjusobno
        private void clbPodPlanovi_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_menjamPodPlanove || e.NewValue != CheckState.Checked)
                return;

            _menjamPodPlanove = true;
            try
            {
                if (clbPodPlanovi.Items[e.Index].ToString() == SviPodPlanovi)
                {
                    for (int i = 0; i < clbPodPlanovi.Items.Count; i++)
                    {
                        if (i != e.Index)
                            clbPodPlanovi.SetItemChecked(i, false);
                    }
                }
                else
                {
                    for (int i = 0; i < clbPodPlanovi.Items.Count; i++)
                    {
                        if (clbPodPlanovi.Items[i].ToString() == SviPodPlanovi)
                            clbPodPlanovi.SetItemChecked(i, false);
                    }
                }
            }
            finally
            {
                _menjamPodPlanove = false;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillGV();
        }

        // Isto kao dataGridView3 (Sumirano po artiklu) u PregledPodPlan,
        // samo sto #Artikli obuhvata sve cekirane planove i podplanove.
        private void FillGV()
        {
            List<int> planovi = CekiraniPlanovi();
            bool svi = IzabranoSviPodPlanovi();
            List<string> podPlanovi = CekiraniPodPlanovi();

            if (planovi.Count == 0)
            {
                MessageBox.Show("Izaberite bar jedan plan.", "Proizvodnja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!svi && podPlanovi.Count == 0)
            {
                MessageBox.Show("Izaberite bar jedan podplan ili " + SviPodPlanovi + ".", "Proizvodnja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connect))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandTimeout = 120;

                string uslovPodPlan = "";
                if (!svi)
                {
                    List<string> parametri = new List<string>();
                    for (int i = 0; i < podPlanovi.Count; i++)
                    {
                        parametri.Add("@pp" + i);
                        cmd.Parameters.AddWithValue("@pp" + i, podPlanovi[i]);
                    }
                    uslovPodPlan = " AND RTRIM(N.PodPlan) IN (" + string.Join(",", parametri) + ")";
                }

                cmd.CommandText = @"
CREATE TABLE #Izbor
(
    IdPlana INT NOT NULL,
    PodPlan NVARCHAR(200) COLLATE DATABASE_DEFAULT NOT NULL
);

INSERT INTO #Izbor (IdPlana, PodPlan)
SELECT DISTINCT N.IdPlana, RTRIM(N.PodPlan)
FROM NetoPotrebeIgor N
WHERE N.IdPlana IN (" + string.Join(",", planovi) + @")
  AND N.PodPlan <> ''" + uslovPodPlan + @";

CREATE TABLE #Artikli
(
    MP INT NOT NULL,
    Kolicina DECIMAL(18,4) NOT NULL
);

INSERT INTO #Artikli (MP, Kolicina)
SELECT N.MP, SUM(ISNULL(N.PlaniranaKolicina, 0))
FROM NetoPotrebeIgor N
INNER JOIN MaticniPodatki M ON N.MP = M.MpSifra
INNER JOIN #Izbor I ON I.IdPlana = N.IdPlana AND I.PodPlan = N.PodPlan
GROUP BY N.MP;

CREATE CLUSTERED INDEX IX_Artikli_MP ON #Artikli(MP);

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
                        SELECT D.DnPStevilka
                        FROM DelNalogProizvodni D
                        INNER JOIN #Izbor I
                            ON D.DnpStvProjekt = I.IdPlana
                           AND CONVERT(nvarchar, D.dnpNote) = I.PodPlan
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
    KoPodSifMp;";

                DataTable dt = new DataTable();

                Cursor = Cursors.WaitCursor;
                try
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                finally
                {
                    Cursor = Cursors.Default;
                }

                _dtOriginal = dt.Copy();
                _dtPrikaz = dt;
                sfGrid.DataSource = dt;

                _osnovniInfo = "Planova: " + planovi.Count +
                               "   Podplanova: " + (svi ? "svi" : podPlanovi.Count.ToString()) +
                               "   Stavki: " + dt.Rows.Count;
                lblInfo.Text = _osnovniInfo;
            }
        }

        // Kolicine table (JM1 = M2, JM2 = TAB, MpOzBlagSkup = 1) se dele faktorom umanjenja.
        // Uvek se racuna od originalnih vrednosti, pa ponovni klik (i promena faktora) ne deli dva puta.
        private void btnUmanji_Click(object sender, EventArgs e)
        {
            if (_dtPrikaz == null || _dtOriginal == null || _dtPrikaz.Rows.Count == 0)
            {
                MessageBox.Show("Nema podataka. Kliknite Refresh.", "Umanjenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal faktor;
            if (!decimal.TryParse(txtFaktor.Text.Trim().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out faktor) || faktor <= 0)
            {
                MessageBox.Show("Faktor umanjenja mora biti broj veći od 0 (npr. 5,796).", "Umanjenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFaktor.Focus();
                return;
            }

            List<int> artikli = _dtOriginal.AsEnumerable()
                .Where(r => r["KoPodSifMp"] != DBNull.Value)
                .Select(r => Convert.ToInt32(r["KoPodSifMp"]))
                .Distinct()
                .ToList();

            HashSet<int> table = new HashSet<int>();

            string query = @"
Select MpSifra
From MaticniPodatki
Where MpSifra in (" + string.Join(",", artikli) + @")
  and MpSifEnoteMere1 = 'M2'
  and MpSifEnoteMere2 = 'TAB'
  and MpOzBlagSkup = 1";

            using (SqlConnection conn = new SqlConnection(connect))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.CommandTimeout = 120;
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        table.Add(Convert.ToInt32(dr["MpSifra"]));
                }
            }

            if (table.Count == 0)
            {
                MessageBox.Show("U gridu nema artikala koji su table (M2 / TAB).", "Umanjenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string[] kolicine = { "KoKolMateriala", "Potroseno", "Potrebno" };
            int umanjeno = 0;

            for (int i = 0; i < _dtPrikaz.Rows.Count; i++)
            {
                System.Data.DataRow original = _dtOriginal.Rows[i];
                if (original["KoPodSifMp"] == DBNull.Value || !table.Contains(Convert.ToInt32(original["KoPodSifMp"])))
                    continue;

                System.Data.DataRow red = _dtPrikaz.Rows[i];
                foreach (string kol in kolicine)
                {
                    if (original[kol] != DBNull.Value)
                        red[kol] = Math.Round(Convert.ToDecimal(original[kol]) / faktor, 4);
                }
                red["JM"] = "TAB";
                umanjeno++;
            }

            lblInfo.Text = _osnovniInfo + "   Umanjeno tabli: " + umanjeno + " (faktor " + faktor.ToString(CultureInfo.CurrentCulture) + ")";
        }

        private void sfGrid_AutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
        {
            switch (e.Column.MappingName)
            {
                case "KoPodSifMp": e.Column.HeaderText = "Šifra MP"; break;
                case "NazivMP": e.Column.HeaderText = "Naziv MP"; break;
                case "KoKolMateriala": e.Column.HeaderText = "Količina materijala"; break;
                case "Potroseno": e.Column.HeaderText = "Potrošeno"; break;
                case "Potrebno": e.Column.HeaderText = "Potrebno"; break;
                case "JM": e.Column.HeaderText = "JM"; break;
                case "MpOsnSklad": e.Column.HeaderText = "Osn. skladište"; break;
                case "MpOsnLokacija": e.Column.HeaderText = "Osn. lokacija"; break;
            }

            if (e.Column.MappingName == "KoKolMateriala" ||
                e.Column.MappingName == "Potroseno" ||
                e.Column.MappingName == "Potrebno")
            {
                GridNumericColumn num = e.Column as GridNumericColumn;
                if (num != null)
                {
                    NumberFormatInfo nfi = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
                    nfi.NumberDecimalDigits = 4;
                    num.NumberFormatInfo = nfi;
                }
            }
        }

        private bool ImaPodataka()
        {
            if (sfGrid.View == null || sfGrid.View.Records.Count == 0)
            {
                MessageBox.Show("Nema podataka za izvoz. Kliknite Refresh.", "Izvoz", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private string IzaberiFajl(string filter, string ekstenzija)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = filter;
                dlg.DefaultExt = ekstenzija;
                dlg.FileName = "ProizvodnjaPotrebe_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + "." + ekstenzija;
                return dlg.ShowDialog(this) == DialogResult.OK ? dlg.FileName : null;
            }
        }

        private void PonudiOtvaranje(string putanja)
        {
            if (MessageBox.Show("Fajl je sačuvan:\n" + putanja + "\n\nOtvoriti ga?", "Izvoz",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Process.Start(putanja);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (!ImaPodataka())
                return;

            string putanja = IzaberiFajl("Excel (*.xlsx)|*.xlsx", "xlsx");
            if (putanja == null)
                return;

            try
            {
                ExcelExportingOptions options = new ExcelExportingOptions();
                options.ExcelVersion = ExcelVersion.Xlsx;

                using (ExcelEngine excelEngine = sfGrid.ExportToExcel(sfGrid.View, options))
                {
                    IWorkbook workbook = excelEngine.Excel.Workbooks[0];
                    workbook.Worksheets[0].UsedRange.AutofitColumns();
                    workbook.SaveAs(putanja);
                }

                PonudiOtvaranje(putanja);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri izvozu u Excel:\n" + ex.Message, "Izvoz", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // CSV sa ';' (srpska podesavanja Excel-a), UTF-8 sa BOM; postuje filter i sortiranje u gridu
        private void btnCsv_Click(object sender, EventArgs e)
        {
            if (!ImaPodataka())
                return;

            string putanja = IzaberiFajl("CSV (*.csv)|*.csv", "csv");
            if (putanja == null)
                return;

            try
            {
                List<GridColumn> kolone = sfGrid.Columns.Where(c => c.Visible).ToList();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(string.Join(";", kolone.Select(c => CsvVrednost(c.HeaderText))));

                foreach (var record in sfGrid.View.Records)
                {
                    DataRowView red = record.Data as DataRowView;
                    if (red == null)
                        continue;

                    sb.AppendLine(string.Join(";", kolone.Select(c => CsvVrednost(red[c.MappingName]))));
                }

                File.WriteAllText(putanja, sb.ToString(), new UTF8Encoding(true));
                PonudiOtvaranje(putanja);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri izvozu u CSV:\n" + ex.Message, "Izvoz", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string CsvVrednost(object value)
        {
            if (value == null || value == DBNull.Value)
                return "";

            string text = value is IFormattable
                ? ((IFormattable)value).ToString(null, CultureInfo.CurrentCulture)
                : value.ToString();

            text = text.TrimEnd();

            if (text.IndexOfAny(new[] { ';', '"', '\r', '\n' }) >= 0)
                text = "\"" + text.Replace("\"", "\"\"") + "\"";

            return text;
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            if (!ImaPodataka())
                return;

            string putanja = IzaberiFajl("PDF (*.pdf)|*.pdf", "pdf");
            if (putanja == null)
                return;

            try
            {
                PdfExportingOptions options = new PdfExportingOptions();
                options.AutoColumnWidth = true;

                PdfGrid pdfGrid = sfGrid.ExportToPdfGrid(sfGrid.View, options);

                // TrueType font zbog slova č, ć, š, đ, ž
                PdfTrueTypeFont font = new PdfTrueTypeFont(new Font("Arial", 8f), true);
                PdfTrueTypeFont fontNaslov = new PdfTrueTypeFont(new Font("Arial", 11f, FontStyle.Bold), true);
                PdfTrueTypeFont fontInfo = new PdfTrueTypeFont(new Font("Arial", 8f), true);

                foreach (PdfGridRow red in pdfGrid.Headers)
                    foreach (PdfGridCell celija in red.Cells)
                        celija.Style.Font = font;

                foreach (PdfGridRow red in pdfGrid.Rows)
                    foreach (PdfGridCell celija in red.Cells)
                        celija.Style.Font = font;

                using (PdfDocument doc = new PdfDocument())
                {
                    doc.PageSettings.Orientation = PdfPageOrientation.Landscape;
                    doc.PageSettings.Margins.All = 25;

                    PdfPage page = doc.Pages.Add();

                    string planovi = string.Join(", ", clbPlanovi.CheckedItems.Cast<PlanStavka>().Select(p => p.Naziv));
                    string podPlanovi = IzabranoSviPodPlanovi() ? "svi" : string.Join(", ", CekiraniPodPlanovi());

                    float sirina = page.GetClientSize().Width;
                    page.Graphics.DrawString("Proizvodnja - potrebe materijala", fontNaslov, PdfBrushes.Black, new PointF(0, 0));

                    string info = "Planovi: " + planovi + "\nPodplanovi: " + podPlanovi +
                                  "\nDatum: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                    PdfTextElement infoElement = new PdfTextElement(info, fontInfo, PdfBrushes.Black);
                    PdfLayoutResult rezultat = infoElement.Draw(page, new RectangleF(0, 18, sirina, 200));

                    PdfGridLayoutFormat format = new PdfGridLayoutFormat();
                    format.Layout = PdfLayoutType.Paginate;
                    pdfGrid.Draw(page, new PointF(0, rezultat.Bounds.Bottom + 8), format);

                    doc.Save(putanja);
                    doc.Close(true);
                }

                PonudiOtvaranje(putanja);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri izradi PDF-a:\n" + ex.Message, "Izvoz", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
