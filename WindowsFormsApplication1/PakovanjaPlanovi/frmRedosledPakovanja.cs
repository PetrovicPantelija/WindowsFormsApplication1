using ClosedXML.Excel;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApplication1.PakovanjaPlanovi
{
    public partial class frmRedosledPakovanja : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public frmRedosledPakovanja()
        {
            InitializeComponent();
        }

        private void frmRedosledPakovanja_Load(object sender, EventArgs e)
        {

        }

        // Click handler for the "Obriši" button.
        private void btnObrisi_Click(object sender, EventArgs e)
        {
            try
            {
                // Ask for mpSifra and mpNadredjeni from selected row or inputs
                int mpSifra = 0;
                int mpNadredjeni = 0;

                // If a row is selected use values from first selected row
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    var row = dataGridView1.SelectedRows[0];
                    int.TryParse(row.Cells["MpSifra"].Value?.ToString(), out mpSifra);
                    int.TryParse(row.Cells["MpNadredjeni"].Value?.ToString(), out mpNadredjeni);
                }
                else
                {
                    // fallback to textboxes
                    int.TryParse(txtMpSifra.Text.Trim(), out mpSifra);
                    int.TryParse(txtMpNadredjeni.Text.Trim(), out mpNadredjeni);
                }

                if (mpSifra == 0)
                {
                    MessageBox.Show("MpSifra nije zadat ili nije validan broj.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirm = MessageBox.Show($"Da li želite obrisati zapis sa MpSifra={mpSifra} i MpNadredjeni={mpNadredjeni}?", "Potvrda brisanja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes)
                    return;

                var helper = new InsertRedosledPlaniranja();
                helper.DeleteRedosledPlaniranja(mpSifra, mpNadredjeni);

                // Remove matching rows from grid (all rows that match criteria)
                for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
                {
                    var r = dataGridView1.Rows[i];
                    if (r.IsNewRow) continue;

                    int.TryParse(r.Cells["MpSifra"].Value?.ToString(), out int rowMpSifra);
                    int.TryParse(r.Cells["MpNadredjeni"].Value?.ToString(), out int rowMpNd);

                    if (rowMpSifra == mpSifra && rowMpNd == mpNadredjeni)
                        dataGridView1.Rows.RemoveAt(i);
                }

                MessageBox.Show("Brisanje završeno.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri brisanju: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Click handler for the "Uvoz iz Excela" button.
        private void btnUvozExcel_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Izaberite excel fajl";
                ofd.Filter = "Excel fajlovi (*.xlsx;*.xls;*.ods)|*.xlsx;*.xls;*.ods";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                string fileName = ofd.FileName;
                try
                {
                    UcitajExcelFajl(fileName);

                    MessageBox.Show("Uvoz je završen.", "Uvoz iz Excela", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška pri uvozu: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Click handler for the "Dodaj" button.
        private void btnDodaj_Click(object sender, EventArgs e)
        {
            try
            {
                EnsureGridColumns();

                string redosled = txtRedosled?.Text?.Trim() ?? "";
                string mpSifra = txtMpSifra?.Text?.Trim() ?? "";
                string mpCode = txtMpCode?.Text?.Trim() ?? "";
                string kolicina = txtKolicina?.Text?.Trim() ?? "";
                string mpNadredjeni = txtMpNadredjeni?.Text?.Trim() ?? "";

                // Validate required fields
                if (string.IsNullOrWhiteSpace(redosled) || string.IsNullOrWhiteSpace(mpSifra) || 
                    string.IsNullOrWhiteSpace(mpCode) || string.IsNullOrWhiteSpace(kolicina))
                {
                    MessageBox.Show("Molimo popunite sva obavezna polja (Redosled, MpSifra, MpCode, Količina).", 
                        "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dataGridView1.Rows.Add(redosled, mpSifra, mpCode, kolicina, mpNadredjeni);

                // Clear input fields
                txtRedosled.Text = "";
                txtMpSifra.Text = "";
                txtMpCode.Text = "";
                txtKolicina.Text = "";
                txtMpNadredjeni.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri dodavanju zapisa: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Click handler for the "Prebaci u Bazu" button.
        private void btnPrebaci_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Nema zapisa za prebacivanje u bazu.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                InsertRedosledPlaniranja insertHelper = new InsertRedosledPlaniranja();
                int upisanoZapisa = 0;
                int preskocenihZapisa = 0;
                StringBuilder preskoceniIzvestaj = new StringBuilder();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    try
                    {
                        // Read values from grid
                        string redosled = row.Cells["Redosled"].Value?.ToString()?.Trim() ?? "";
                        string mpSifraStr = row.Cells["MpSifra"].Value?.ToString()?.Trim() ?? "";
                        string mpCode = row.Cells["MpCode"].Value?.ToString()?.Trim() ?? "";
                        string kolicinaStr = row.Cells["Kolicina"].Value?.ToString()?.Trim() ?? "";
                        string mpNadredjeniStr = row.Cells["MpNadredjeni"].Value?.ToString()?.Trim() ?? "";

                        // Validate required fields
                        if (string.IsNullOrWhiteSpace(mpSifraStr) || 
                            string.IsNullOrWhiteSpace(redosled) || 
                            string.IsNullOrWhiteSpace(mpCode) ||
                            string.IsNullOrWhiteSpace(kolicinaStr))
                        {
                            preskocenihZapisa++;
                            preskoceniIzvestaj.AppendLine($"Red {row.Index + 1}: Nedostaju obavezna polja (Redosled, MpSifra, MpCode, Količina)");
                            continue;
                        }

                        // Parse numeric values
                        if (!int.TryParse(mpSifraStr, out int mpSifra))
                        {
                            preskocenihZapisa++;
                            preskoceniIzvestaj.AppendLine($"Red {row.Index + 1}: MpSifra nije validan broj (vrednost: {mpSifraStr})");
                            continue;
                        }

                        if (!decimal.TryParse(kolicinaStr, out decimal kolicina))
                        {
                            preskocenihZapisa++;
                            preskoceniIzvestaj.AppendLine($"Red {row.Index + 1}: Količina nije validan broj (vrednost: {kolicinaStr})");
                            continue;
                        }

                        if (!int.TryParse(mpNadredjeniStr, out int mpNadredjeni))
                        {
                            mpNadredjeni = 0;
                        }

                        // Check if record already exists
                        if (ZapisPostoji(mpSifra))
                        {
                            preskocenihZapisa++;
                            preskoceniIzvestaj.AppendLine($"Red {row.Index + 1}: Zapis sa MpSifra={mpSifra} već postoji u bazi");
                            continue;
                        }

                        // Insert into database
                        insertHelper.InsertRedosledPlan(redosled, mpSifra, mpCode, kolicina, mpNadredjeni);
                        upisanoZapisa++;
                    }
                    catch (Exception rowEx)
                    {
                        preskocenihZapisa++;
                        preskoceniIzvestaj.AppendLine($"Red {row.Index + 1}: {rowEx.Message}");
                    }
                }

                // Show summary message
                StringBuilder message = new StringBuilder();
                message.AppendLine($"Ukupno učitano: {dataGridView1.Rows.Count}");
                message.AppendLine($"Uspešno upisano: {upisanoZapisa}");
                message.AppendLine($"Preskočeno: {preskocenihZapisa}");

                if (preskocenihZapisa > 0)
                {
                    message.AppendLine();
                    message.AppendLine("Detalji preskočenih zapisa:");
                    message.AppendLine(preskoceniIzvestaj.ToString());
                }

                MessageBox.Show(message.ToString(), "Rezultat prebacivanja", MessageBoxButtons.OK, 
                    preskocenihZapisa > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

                // Clear grid if all records were inserted successfully
                if (preskocenihZapisa == 0 && upisanoZapisa > 0)
                {
                    dataGridView1.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri prebacivanju zapisa u bazu: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper method to check if record exists in database
        private bool ZapisPostoji(int mpSifra)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM RedosledPlaniranja WHERE MpSifra = @MpSifra", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@MpSifra", SqlDbType.Int) { Value = mpSifra });
                        
                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int count))
                        {
                            return count > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška pri proveri zapisa: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return false;
        }

        // ============================================================
        // EXCEL IMPORT IMPLEMENTATION (xlsx, xls, ods)
        // Pattern follows MpMDF import logic but simplified for these columns:
        // Columns expected in Excel (in order): Redosled, MpSifra, MpCode, Kolicina, MpNadredjeni
        // ============================================================

        private void UcitajExcelFajl(string fileName)
        {
            string ext = Path.GetExtension(fileName).ToLowerInvariant();

            if (ext == ".xlsx")
            {
                UcitajXlsxClosedXml(fileName);
                return;
            }

            if (ext == ".xls")
            {
                UcitajXlsExcelDataReader(fileName);
                return;
            }

            if (ext == ".ods")
            {
                UcitajOdsBezLibreOffice(fileName);
                return;
            }

            throw new Exception("Dozvoljeni su samo .xlsx, .xls i .ods fajlovi.");
        }

        private void UcitajXlsxClosedXml(string fileName)
        {
            using (var workbook = new XLWorkbook(fileName))
            {
                var worksheet = workbook.Worksheet(1);

                int rowCount = worksheet.LastRowUsed() == null
                    ? 0
                    : worksheet.LastRowUsed().RowNumber();

                EnsureGridColumns();

                // First row assumed header
                for (int row = 2; row <= rowCount; row++)
                {
                    object redosledObj = worksheet.Cell(row, 1).Value;
                    object mpSifraObj = worksheet.Cell(row, 2).Value;
                    object mpCodeObj = worksheet.Cell(row, 3).Value;
                    object kolicinaObj = worksheet.Cell(row, 4).Value;
                    object mpNadredjeniObj = worksheet.Cell(row, 5).Value;

                    bool prazno = string.IsNullOrWhiteSpace(ToStringExcel(redosledObj)) &&
                                  string.IsNullOrWhiteSpace(ToStringExcel(mpSifraObj)) &&
                                  string.IsNullOrWhiteSpace(ToStringExcel(mpCodeObj)) &&
                                  string.IsNullOrWhiteSpace(ToStringExcel(kolicinaObj)) &&
                                  string.IsNullOrWhiteSpace(ToStringExcel(mpNadredjeniObj));

                    if (prazno)
                        continue;

                    dataGridView1.Rows.Add(
                        ToStringExcel(redosledObj),
                        ToStringExcel(mpSifraObj),
                        ToStringExcel(mpCodeObj),
                        ToStringExcel(kolicinaObj),
                        ToStringExcel(mpNadredjeniObj));
                }
            }
        }

        private void UcitajXlsExcelDataReader(string fileName)
        {
            // ExcelDataReader may require code pages provider on some platforms
            using (FileStream stream =
                   File.Open(
                       fileName,
                       FileMode.Open,
                       FileAccess.Read,
                       FileShare.ReadWrite))
            using (IExcelDataReader reader =
                ExcelReaderFactory.CreateReader(stream))
            {
                int row = 0;
                EnsureGridColumns();

                while (reader.Read())
                {
                    row++;

                    // First row header
                    if (row == 1)
                        continue;

                    object redosledObj = reader.FieldCount > 0 ? reader.GetValue(0) : null;
                    object mpSifraObj = reader.FieldCount > 1 ? reader.GetValue(1) : null;
                    object mpCodeObj = reader.FieldCount > 2 ? reader.GetValue(2) : null;
                    object kolicinaObj = reader.FieldCount > 3 ? reader.GetValue(3) : null;
                    object mpNadredjeniObj = reader.FieldCount > 4 ? reader.GetValue(4) : null;

                    bool prazno = string.IsNullOrWhiteSpace(ToStringExcel(redosledObj)) &&
                                  string.IsNullOrWhiteSpace(ToStringExcel(mpSifraObj)) &&
                                  string.IsNullOrWhiteSpace(ToStringExcel(mpCodeObj)) &&
                                  string.IsNullOrWhiteSpace(ToStringExcel(kolicinaObj)) &&
                                  string.IsNullOrWhiteSpace(ToStringExcel(mpNadredjeniObj));

                    if (prazno)
                        continue;

                    dataGridView1.Rows.Add(
                        ToStringExcel(redosledObj),
                        ToStringExcel(mpSifraObj),
                        ToStringExcel(mpCodeObj),
                        ToStringExcel(kolicinaObj),
                        ToStringExcel(mpNadredjeniObj));
                }
            }
        }

        private void UcitajOdsBezLibreOffice(string fileName)
        {
            XNamespace officeNs = "urn:oasis:names:tc:opendocument:xmlns:office:1.0";
            XNamespace tableNs = "urn:oasis:names:tc:opendocument:xmlns:table:1.0";
            XNamespace textNs = "urn:oasis:names:tc:opendocument:xmlns:text:1.0";

            using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (ZipArchive archive = new ZipArchive(fs, ZipArchiveMode.Read))
            {
                ZipArchiveEntry contentEntry = archive.GetEntry("content.xml");

                if (contentEntry == null)
                    throw new Exception("ODS fajl nema content.xml i ne može biti pročitan.");

                XDocument doc;
                using (Stream contentStream = contentEntry.Open())
                {
                    doc = XDocument.Load(contentStream);
                }

                XElement firstTable = doc.Descendants(tableNs + "table").FirstOrDefault();
                if (firstTable == null)
                    throw new Exception("ODS fajl nema tabelu za čitanje.");

                EnsureGridColumns();

                int logicalRow = 0;
                foreach (XElement rowElement in firstTable.Elements(tableNs + "table-row"))
                {
                    int repeatedRows = GetRepeatedCount(rowElement.Attribute(tableNs + "number-rows-repeated"), 1);

                    List<string> cells = ProcitajOdsCelijeIzReda(rowElement, tableNs, textNs, officeNs, 5);

                    bool prazanRed = cells.All(x => string.IsNullOrWhiteSpace(x));
                    if (prazanRed)
                    {
                        logicalRow += repeatedRows;
                        continue;
                    }

                    for (int i = 0; i < repeatedRows; i++)
                    {
                        logicalRow++;
                        if (logicalRow == 1)
                            continue;

                        dataGridView1.Rows.Add(
                            cells[0],
                            cells[1],
                            cells[2],
                            cells[3],
                            cells[4]);
                    }
                }
            }
        }

        private List<string> ProcitajOdsCelijeIzReda(XElement rowElement, XNamespace tableNs, XNamespace textNs, XNamespace officeNs, int brojKolona)
        {
            List<string> values = new List<string>();

            foreach (XElement cell in rowElement.Elements(tableNs + "table-cell"))
            {
                int repeatedColumns = GetRepeatedCount(cell.Attribute(tableNs + "number-columns-repeated"), 1);

                string value = ProcitajOdsVrednostCelije(cell, textNs, officeNs);

                for (int i = 0; i < repeatedColumns && values.Count < brojKolona; i++)
                {
                    values.Add(value);
                }

                if (values.Count >= brojKolona)
                    break;
            }

            while (values.Count < brojKolona)
            {
                values.Add("");
            }

            return values;
        }

        private string ProcitajOdsVrednostCelije(XElement cell, XNamespace textNs, XNamespace officeNs)
        {
            string textValue = string.Join(" ", cell.Descendants(textNs + "p").Select(x => x.Value));
            if (!string.IsNullOrWhiteSpace(textValue))
                return textValue.Trim();

            XAttribute valueAttr = cell.Attribute(officeNs + "value");
            if (valueAttr != null)
                return valueAttr.Value.Trim();

            XAttribute dateAttr = cell.Attribute(officeNs + "date-value");
            if (dateAttr != null)
                return dateAttr.Value.Trim();

            XAttribute boolAttr = cell.Attribute(officeNs + "boolean-value");
            if (boolAttr != null)
                return boolAttr.Value.Trim();

            return "";
        }

        private int GetRepeatedCount(XAttribute attr, int defaultValue)
        {
            if (attr == null)
                return defaultValue;

            int result;
            if (int.TryParse(attr.Value, out result) && result > 0)
                return result;

            return defaultValue;
        }

        // Normalize object to string similar to MpMDF helpers
        private string ToStringExcel(object value)
        {
            if (value == null || value == DBNull.Value)
                return "";

            if (value is string)
                return value.ToString().Trim();

            if (value is DateTime date)
                return date.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);

            if (value is double d)
            {
                if (Math.Abs(d - Math.Round(d)) < 0.0000001)
                    return Convert.ToInt64(Math.Round(d)).ToString(CultureInfo.InvariantCulture);

                return d.ToString(CultureInfo.InvariantCulture).Trim();
            }

            if (value is float f)
            {
                if (Math.Abs(f - Math.Round(f)) < 0.0000001)
                    return Convert.ToInt64(Math.Round(f)).ToString(CultureInfo.InvariantCulture);

                return f.ToString(CultureInfo.InvariantCulture).Trim();
            }

            if (value is decimal dec)
            {
                if (dec == Math.Round(dec))
                    return Convert.ToInt64(Math.Round(dec)).ToString(CultureInfo.InvariantCulture);

                return dec.ToString(CultureInfo.InvariantCulture).Trim();
            }

            return Convert.ToString(value, CultureInfo.InvariantCulture).Trim();
        }

        private void EnsureGridColumns()
        {
            if (dataGridView1 == null)
                throw new InvalidOperationException("dataGridView1 nije pronađen na formi.");

            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add("Redosled", "Redosled");
                dataGridView1.Columns.Add("MpSifra", "MpSifra");
                dataGridView1.Columns.Add("MpCode", "MpCode");
                dataGridView1.Columns.Add("Kolicina", "Količina");
                dataGridView1.Columns.Add("MpNadredjeni", "MpNadredjeni");
            }
        }
    }
}
