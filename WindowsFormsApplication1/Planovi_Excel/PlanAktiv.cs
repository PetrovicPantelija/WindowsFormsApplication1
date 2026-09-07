using ClosedXML.Excel;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using static WindowsFormsApplication1.Planovi_Excel.PripadnostKP;

namespace WindowsFormsApplication1.Planovi_Excel
{
    public partial class PlanAktiv : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        private bool status = false;
        public int ID;
        string Kor = frmLogovanje.user.ToString().TrimEnd();

        public PlanAktiv()
        {
            InitializeComponent();
            if (Kor.Trim() == "stanojevic.ivan" || Kor.Trim() == "stanojlovic.djordje" || Kor.Trim() == "hasanovic.igor" || Kor.Trim() == "pantelija")
            {
                toolStrip1.Visible = true;
                button3.Enabled = true;
                btnGrupisi.Enabled = true;
                button1.Enabled = true;
            }
            else
            {
                toolStrip1.Visible = false;
                button3.Enabled = false;
                btnGrupisi.Enabled = false;
                button1.Enabled = false;
            }
        }

        private void FillGV()
        {
            var query = "Select * from PlanAktiv order by ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void PlanAktiv_Load(object sender, EventArgs e)
        {
            FillGV();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                Planovi_Excel.InsertPlanovi ins = new InsertPlanovi();
                ins.InsertPlan(txtNaziv.Text.ToString().TrimEnd(), Convert.ToDateTime(dateTimePicker1.Value.ToString("dd.MM.yyyy")), Convert.ToDateTime(dateTimePicker2.Value.ToString("dd.MM.yyyy")),
                    txtNapomena.Text.ToString().TrimEnd(), "Aktivan");
                status = false;
            }
            else
            {
                Planovi_Excel.InsertPlanovi ins = new InsertPlanovi();
                ins.UpdatePlakAktiv(Convert.ToInt32(txtID.Text), txtNaziv.Text.ToString().TrimEnd(), Convert.ToDateTime(dateTimePicker1.Value.ToString("dd.MM.yyyy")), Convert.ToDateTime(dateTimePicker2.Value.ToString("dd.MM.yyyy")),
                    txtNapomena.Text.ToString().TrimEnd());
            }
            FillGV();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    txtID.Text = row.Cells[0].Value.ToString();
                    txtNaziv.Text = row.Cells[1].Value.ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(row.Cells[2].Value);
                    dateTimePicker2.Value = Convert.ToDateTime(row.Cells[3].Value);
                    txtNapomena.Text = row.Cells[4].Value.ToString();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ID = Convert.ToInt32(txtID.Text);
            PlanStavke frm = new PlanStavke(ID);
            frm.Show();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PoveziRN frm = new PoveziRN();
            frm.Show();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            Planovi_Excel.InsertPlanovi ins = new InsertPlanovi();
            ins.DeletePlanSaStavkama(Convert.ToInt32(txtID.Text));
            FillGV();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int stariplan = Convert.ToInt32(txtID.Text);
            int noviPlan = 0;
            InsertPlanovi ins = new InsertPlanovi();
            ins.InsertPlan(txtNaziv.Text.ToString().TrimEnd(), DateTime.Now, DateTime.Now, txtNapomena.Text.ToString().TrimEnd(), "Aktivan");

            string query = "(select (Max(t1.ID)) from (select ID from PlanArhiv union Select ID from PlanAktiv) as t1 )";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                noviPlan = Convert.ToInt32(dr[0].ToString());
            }
            conn.Close();
            var query2 = "Select IDPrograma,IDKolekcije,MPSifra From PLanAktivStavke Where IDPLana=" + stariplan;
            conn.Open();
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                ins.InsertPlanStavke(noviPlan, Convert.ToInt32(dr2["IDPrograma"].ToString()), Convert.ToInt32(dr2["IdKolekcije"].ToString()),
                    Convert.ToInt32(dr2["MpSifra"].ToString()));
            }
            conn.Close();
            FillGV();
        }
        int stariPlanovi;
        int noviPlan;
        private void btnGrupisi_Click(object sender, EventArgs e)
        {
            
            InsertPlanovi ins = new InsertPlanovi();
            ins.InsertPlan(txtNaziv.Text.ToString().TrimEnd(), DateTime.Now, DateTime.Now, txtNapomena.Text.ToString().TrimEnd(), "Aktivan");
            var query = "(select (Max(t1.ID)) from (select ID from PlanArhiv union Select ID from PlanAktiv) as t1 )";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd=new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                noviPlan = Convert.ToInt32(dr[0].ToString());
            }
            conn.Close();
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    stariPlanovi = Convert.ToInt32(row.Cells[0].Value.ToString());

                    var updateQuery = "Update PlanAktivStavke Set IDPlana=" + noviPlan + " Where IDPlana=" + stariPlanovi;
                    var deletePlan = "Delete From PlanAktiv Where ID=" + stariPlanovi;
                    conn.Open();
                    SqlCommand cmd2= new SqlCommand(updateQuery, conn);
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    SqlCommand cmd3=new SqlCommand(deletePlan, conn);
                    cmd3.ExecuteNonQuery();
                    conn.Close();
                }
            }

            FillGV();
        }
        public class Plan
        {
            public string Naziv { get; set; }
            public DateTime DatumOd { get; set; }
            public DateTime DatumDo { get; set; }
            public string Napomena { get; set; }
        }

        private List<Plan> list = new List<Plan>();

        private void btnExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Izaberite excel fajl";
            ofd.Filter = "Excel fajlovi (*.xlsx;*.xls;*.ods)|*.xlsx;*.xls;*.ods";
            ofd.Multiselect = false;

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            list.Clear();

            try
            {
                UcitajExcelFajl(ofd.FileName);

                if (list.Count == 0)
                {
                    MessageBox.Show(
                        "U izabranom fajlu nije pronađena nijedna stavka za import.",
                        "Import",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                ObradiImportovanuListu();

                MessageBox.Show(
                    "Import je uspešno završen.\n\n" +
                    "Ukupno importovanih planova: " + list.Count,
                    "Import",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                FillGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Greška pri učitavanju Excel fajla:\n\n" + ex.Message,
                    "Greška",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // ============================================================
        // GLAVNA METODA ZA UČITAVANJE FAJLA
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

            throw new Exception(
                "Dozvoljeni su samo .xlsx, .xls i .ods fajlovi.");
        }


        // ============================================================
        // XLSX - CLOSED XML
        // ============================================================

        private void UcitajXlsxClosedXml(string fileName)
        {
            using (var workbook = new XLWorkbook(fileName))
            {
                var worksheet = workbook.Worksheet(1);

                int rowCount =
                    worksheet.LastRowUsed() == null
                        ? 0
                        : worksheet.LastRowUsed().RowNumber();

                // Prvi red je zaglavlje
                for (int row = 2; row <= rowCount; row++)
                {
                    object nazivObj =
                        worksheet.Cell(row, 1).GetValue<string>();

                    object datumOdObj =
                        worksheet.Cell(row, 2).Value;

                    object datumDoObj =
                        worksheet.Cell(row, 3).Value;

                    object napomenaObj =
                        worksheet.Cell(row, 4).GetValue<string>();

                    DodajPlanIzReda(
                        row,
                        nazivObj,
                        datumOdObj,
                        datumDoObj,
                        napomenaObj);
                }
            }
        }


        // ============================================================
        // XLS - EXCEL DATA READER
        // ============================================================

        private void UcitajXlsExcelDataReader(string fileName)
        {
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

                while (reader.Read())
                {
                    row++;

                    // Prvi red je zaglavlje
                    if (row == 1)
                        continue;

                    object nazivObj =
                        reader.FieldCount > 0
                            ? reader.GetValue(0)
                            : null;

                    object datumOdObj =
                        reader.FieldCount > 1
                            ? reader.GetValue(1)
                            : null;

                    object datumDoObj =
                        reader.FieldCount > 2
                            ? reader.GetValue(2)
                            : null;

                    object napomenaObj =
                        reader.FieldCount > 3
                            ? reader.GetValue(3)
                            : null;

                    DodajPlanIzReda(
                        row,
                        nazivObj,
                        datumOdObj,
                        datumDoObj,
                        napomenaObj);
                }
            }
        }


        // ============================================================
        // ODS
        // ============================================================

        private void UcitajOdsBezLibreOffice(string fileName)
        {
            XNamespace officeNs =
                "urn:oasis:names:tc:opendocument:xmlns:office:1.0";

            XNamespace tableNs =
                "urn:oasis:names:tc:opendocument:xmlns:table:1.0";

            XNamespace textNs =
                "urn:oasis:names:tc:opendocument:xmlns:text:1.0";

            using (FileStream fs =
                File.Open(
                    fileName,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.ReadWrite))
            using (ZipArchive archive =
                new ZipArchive(fs, ZipArchiveMode.Read))
            {
                ZipArchiveEntry contentEntry =
                    archive.GetEntry("content.xml");

                if (contentEntry == null)
                {
                    throw new Exception(
                        "ODS fajl nema content.xml i ne može biti pročitan.");
                }

                XDocument doc;

                using (Stream contentStream = contentEntry.Open())
                {
                    doc = XDocument.Load(contentStream);
                }

                XElement firstTable =
                    doc.Descendants(tableNs + "table")
                       .FirstOrDefault();

                if (firstTable == null)
                {
                    throw new Exception(
                        "ODS fajl nema tabelu za čitanje.");
                }

                int logicalRow = 0;

                foreach (XElement rowElement in
                         firstTable.Elements(tableNs + "table-row"))
                {
                    int repeatedRows =
                        GetRepeatedCount(
                            rowElement.Attribute(
                                tableNs + "number-rows-repeated"),
                            1);

                    List<string> cells =
                        ProcitajOdsCelijeIzReda(
                            rowElement,
                            tableNs,
                            textNs,
                            officeNs,
                            4);

                    bool prazanRed =
                        cells.All(x =>
                            string.IsNullOrWhiteSpace(x));

                    if (prazanRed)
                    {
                        logicalRow += repeatedRows;
                        continue;
                    }

                    for (int i = 0; i < repeatedRows; i++)
                    {
                        logicalRow++;

                        // Prvi red je zaglavlje
                        if (logicalRow == 1)
                            continue;

                        DodajPlanIzReda(
                            logicalRow,
                            cells[0],
                            cells[1],
                            cells[2],
                            cells[3]);
                    }
                }
            }
        }


        // ============================================================
        // ODS - ČITANJE ĆELIJA
        // ============================================================

        private List<string> ProcitajOdsCelijeIzReda(
            XElement rowElement,
            XNamespace tableNs,
            XNamespace textNs,
            XNamespace officeNs,
            int brojKolona)
        {
            List<string> values = new List<string>();

            foreach (XElement cell in
                     rowElement.Elements(tableNs + "table-cell"))
            {
                int repeatedColumns =
                    GetRepeatedCount(
                        cell.Attribute(
                            tableNs + "number-columns-repeated"),
                        1);

                string value =
                    ProcitajOdsVrednostCelije(
                        cell,
                        textNs,
                        officeNs);

                for (int i = 0;
                     i < repeatedColumns && values.Count < brojKolona;
                     i++)
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


        // ============================================================
        // ODS - VREDNOST ĆELIJE
        // ============================================================

        private string ProcitajOdsVrednostCelije(
            XElement cell,
            XNamespace textNs,
            XNamespace officeNs)
        {
            string textValue =
                string.Join(
                    " ",
                    cell.Descendants(textNs + "p")
                        .Select(x => x.Value));

            if (!string.IsNullOrWhiteSpace(textValue))
                return textValue.Trim();

            XAttribute valueAttr =
                cell.Attribute(officeNs + "value");

            if (valueAttr != null)
                return valueAttr.Value.Trim();

            XAttribute dateAttr =
                cell.Attribute(officeNs + "date-value");

            if (dateAttr != null)
                return dateAttr.Value.Trim();

            XAttribute boolAttr =
                cell.Attribute(officeNs + "boolean-value");

            if (boolAttr != null)
                return boolAttr.Value.Trim();

            return "";
        }


        // ============================================================
        // BROJ PONOVLJENIH REDOVA/KOLONA U ODS
        // ============================================================

        private int GetRepeatedCount(
            XAttribute attr,
            int defaultValue)
        {
            if (attr == null)
                return defaultValue;

            int result;

            if (int.TryParse(attr.Value, out result) &&
                result > 0)
            {
                return result;
            }

            return defaultValue;
        }


        // ============================================================
        // DODAVANJE PLANA IZ REDA
        // ============================================================

        private void DodajPlanIzReda(
            int red,
            object nazivObj,
            object datumOdObj,
            object datumDoObj,
            object napomenaObj)
        {
            bool prazanRed =
                string.IsNullOrWhiteSpace(
                    ToStringExcel(nazivObj)) &&
                string.IsNullOrWhiteSpace(
                    ToStringExcel(datumOdObj)) &&
                string.IsNullOrWhiteSpace(
                    ToStringExcel(datumDoObj)) &&
                string.IsNullOrWhiteSpace(
                    ToStringExcel(napomenaObj));

            if (prazanRed)
                return;

            string naziv =
                ToStringExcel(nazivObj);

            string napomena =
                ToStringExcel(napomenaObj);

            if (string.IsNullOrWhiteSpace(naziv))
            {
                throw new Exception(
                    "Naziv plana nije popunjen.\n" +
                    "Red u fajlu: " + red +
                    "\nProverite ispravnost podataka u fajlu.");
            }

            DateTime datumOd;

            if (!TryParseExcelDate(
                datumOdObj,
                out datumOd))
            {
                throw new Exception(
                    "Datum OD nije ispravan.\n" +
                    "Vrednost: " +
                    ToStringExcel(datumOdObj) +
                    "\nRed u fajlu: " +
                    red +
                    "\nOčekivani format je npr. 1.9.2026.");
            }

            DateTime datumDo;

            if (!TryParseExcelDate(
                datumDoObj,
                out datumDo))
            {
                throw new Exception(
                    "Datum DO nije ispravan.\n" +
                    "Vrednost: " +
                    ToStringExcel(datumDoObj) +
                    "\nRed u fajlu: " +
                    red +
                    "\nOčekivani format je npr. 30.9.2026.");
            }

            if (datumDo < datumOd)
            {
                throw new Exception(
                    "Datum DO ne može biti pre datuma OD.\n" +
                    "Datum OD: " +
                    datumOd.ToString("dd.MM.yyyy") +
                    "\n" +
                    "Datum DO: " +
                    datumDo.ToString("dd.MM.yyyy") +
                    "\n" +
                    "Red u fajlu: " +
                    red);
            }

            Plan plan = new Plan
            {
                Naziv = naziv,
                DatumOd = datumOd,
                DatumDo = datumDo,
                Napomena = napomena
            };

            list.Add(plan);
        }


        // ============================================================
        // OBRADA IMPORTOVANE LISTE
        // ============================================================

        private void ObradiImportovanuListu()
        {
            Planovi_Excel.InsertPlanovi ins =
                new Planovi_Excel.InsertPlanovi();

            foreach (Plan item in list)
            {
                ins.InsertPlan(
                    item.Naziv,
                    item.DatumOd,
                    item.DatumDo,
                    item.Napomena,
                    "Aktivan");
            }
        }


        // ============================================================
        // PRETVARANJE EXCEL VREDNOSTI U STRING
        // ============================================================

        private string ToStringExcel(object value)
        {
            if (value == null ||
                value == DBNull.Value)
            {
                return "";
            }

            if (value is string)
            {
                return value.ToString().Trim();
            }

            if (value is DateTime)
            {
                DateTime date =
                    (DateTime)value;

                return date.ToString(
                    "dd.MM.yyyy",
                    CultureInfo.InvariantCulture);
            }

            if (value is double)
            {
                double d = (double)value;

                if (Math.Abs(
                        d - Math.Round(d)) < 0.0000001)
                {
                    return Convert.ToInt64(
                        Math.Round(d))
                        .ToString(
                            CultureInfo.InvariantCulture);
                }

                return d.ToString(
                    CultureInfo.InvariantCulture)
                    .Trim();
            }

            if (value is float)
            {
                float f = (float)value;

                if (Math.Abs(
                        f - Math.Round(f)) < 0.0000001)
                {
                    return Convert.ToInt64(
                        Math.Round(f))
                        .ToString(
                            CultureInfo.InvariantCulture);
                }

                return f.ToString(
                    CultureInfo.InvariantCulture)
                    .Trim();
            }

            if (value is decimal)
            {
                decimal d = (decimal)value;

                if (d == Math.Round(d))
                {
                    return Convert.ToInt64(
                        Math.Round(d))
                        .ToString(
                            CultureInfo.InvariantCulture);
                }

                return d.ToString(
                    CultureInfo.InvariantCulture)
                    .Trim();
            }

            return Convert.ToString(
                value,
                CultureInfo.InvariantCulture)
                .Trim();
        }


        // ============================================================
        // PARSIRANJE DATUMA
        // ============================================================

        private bool TryParseExcelDate(
            object value,
            out DateTime result)
        {
            result = DateTime.MinValue;

            if (value == null ||
                value == DBNull.Value)
            {
                return false;
            }

            // Ako ExcelDataReader vrati DateTime
            if (value is DateTime)
            {
                result = ((DateTime)value).Date;
                return true;
            }

            // Ako je Excel datum serijski broj
            if (value is double)
            {
                double number = (double)value;

                if (number > 0 && number < 100000)
                {
                    try
                    {
                        result =
                            DateTime.FromOADate(number).Date;

                        return true;
                    }
                    catch
                    {
                        // Nastavljamo sa parsiranjem kao string
                    }
                }
            }

            if (value is decimal)
            {
                decimal number = (decimal)value;

                if (number > 0 && number < 100000)
                {
                    try
                    {
                        result =
                            DateTime.FromOADate(
                                Convert.ToDouble(number))
                            .Date;

                        return true;
                    }
                    catch
                    {
                        // Nastavljamo dalje
                    }
                }
            }

            string text =
                ToStringExcel(value);

            if (string.IsNullOrWhiteSpace(text))
                return false;

            text = text.Trim();

            // Najčešći formati
            string[] formats =
            {
        "d.M.yyyy",
        "dd.M.yyyy",
        "d.MM.yyyy",
        "dd.MM.yyyy",

        "d/M/yyyy",
        "dd/MM/yyyy",

        "d-M-yyyy",
        "dd-MM-yyyy",

        "yyyy-MM-dd",
        "yyyy/MM/dd",

        "d.M.yy",
        "dd.MM.yy"
    };

            if (DateTime.TryParseExact(
                text,
                formats,
                new CultureInfo("sr-Latn-RS"),
                DateTimeStyles.AllowWhiteSpaces,
                out result))
            {
                result = result.Date;
                return true;
            }

            // Pokušaj sa srpskom kulturom
            if (DateTime.TryParse(
                text,
                new CultureInfo("sr-Latn-RS"),
                DateTimeStyles.AllowWhiteSpaces,
                out result))
            {
                result = result.Date;
                return true;
            }

            // Pokušaj sa trenutnom kulturom
            if (DateTime.TryParse(
                text,
                CultureInfo.CurrentCulture,
                DateTimeStyles.AllowWhiteSpaces,
                out result))
            {
                result = result.Date;
                return true;
            }

            // Pokušaj invariant kulture
            if (DateTime.TryParse(
                text,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AllowWhiteSpaces,
                out result))
            {
                result = result.Date;
                return true;
            }

            return false;
        }
    }
}