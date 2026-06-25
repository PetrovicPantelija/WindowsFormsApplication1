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

namespace WindowsFormsApplication1.Planovi_Excel
{
    public partial class PripadnostKP : Form
    {
        string Kor = frmLogovanje.user.ToString().TrimEnd();

        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public PripadnostKP()
        {
            InitializeComponent();

            if (Kor.Trim() == "stanojevic.ivan" || Kor.Trim() == "stanojlovic.djordje" || Kor.Trim() == "hasanovic.igor" || Kor.Trim() == "pantelija")
            {
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
            }
            else
            {
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
            }
        }

        private void PripadnostKP_Load(object sender, EventArgs e)
        {
            FillGV();
            FillCombo();
        }

        private void FillGV()
        {
            var query = "select pk.ID,RTrim(Kolekcije.Naziv) as [Kolekcija],RTrim(Program.Naziv) as [Program],pk.MpSifra,RTrim(MpNaziv) as [Naziv],TipPaketa,Koeficijent,MinimalnaKol " +
                "From PripadnostKolProgram pk " +
                "Inner join Kolekcije on pk.IdKolekcije = Kolekcije.ID " +
                "Inner join Program on pk.IDPrograma = Program.ID " +
                "inner join MaticniPodatki on pk.MpSifra = MaticniPodatki.MpSifra order by pk.ID desc";

            using (SqlConnection conn = new SqlConnection(connect))
            {
                var da = new SqlDataAdapter(query, conn);
                var ds = new DataSet();
                da.Fill(ds);
                dataGridView2.ReadOnly = true;
                dataGridView2.DataSource = ds.Tables[0];

                dataGridView2.Columns[0].Width = 50;
                dataGridView2.Columns["TipPaketa"].Width = 60;
                dataGridView2.Columns["Koeficijent"].Width = 60;
                dataGridView2.Columns["MinimalnaKol"].Width = 60;

                var query2 = "Select MpSifra,MpStaraSif as [Kod],RTrim(MpNaziv) as [Naziv],MpOsnLokacija as [Osnovna lokacija] From MaticniPodatki Where MpStatus=0 order by MpSifra asc";
                var da2 = new SqlDataAdapter(query2, conn);
                var ds2 = new DataSet();
                da2.Fill(ds2);
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = ds2.Tables[0];
                dataGridView1.Columns[0].Width = 50;
            }
        }

        private void FillCombo()
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                var query = "Select ID,Naziv from Program";
                var da = new SqlDataAdapter(query, conn);
                var ds = new DataSet();
                da.Fill(ds);
                cboProgram.DataSource = ds.Tables[0];
                cboProgram.DisplayMember = "Naziv";
                cboProgram.ValueMember = "ID";

                var query2 = "Select ID,Naziv from Kolekcije";
                var da2 = new SqlDataAdapter(query2, conn);
                var ds2 = new DataSet();
                da2.Fill(ds2);
                cboKolekcija.DataSource = ds2.Tables[0];
                cboKolekcija.DisplayMember = "Naziv";
                cboKolekcija.ValueMember = "ID";
            }
        }

        int MpSifra;
        int boja;

        private void Dodela()
        {
            if (checkBox1.Checked == true)
            {
                boja = 1;
            }
            else
            {
                boja = 0;
            }

            InsertPlanovi ins = new InsertPlanovi();

            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        MpSifra = Convert.ToInt32(row.Cells[0].Value.ToString());
                        ins.InsertPripadnostKP(
                            Convert.ToInt32(cboKolekcija.SelectedValue),
                            Convert.ToInt32(cboProgram.SelectedValue),
                            MpSifra,
                            boja,
                            Convert.ToDecimal(textBox1.Text.ToString().TrimEnd()),
                            Convert.ToDecimal(textBox3.Text.ToString()));
                    }
                }

                if (textBox3.Text == "0")
                {
                    using (SqlConnection conn = new SqlConnection(connect))
                    using (SqlCommand cmd = new SqlCommand("Update PripadnostKolProgram Set MinimalnaKol='' Where MpSifra=@MpSifra", conn))
                    {
                        cmd.Parameters.AddWithValue("@MpSifra", MpSifra);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
            }

            ObrisiDuplirano();
            FillGV();
        }

        private void ObrisiDuplirano()
        {
            using (SqlConnection conn = new SqlConnection(connect))
            using (SqlCommand cmd = new SqlCommand(
                "With cte as (Select ID,IDKolekcije,IDPrograma,MpSifra,TipPaketa,ROW_NUMBER() Over(Partition by IDKolekcije,IDPrograma,MpSifra,TipPaketa " +
                "order by IDKolekcije,IDPrograma,MpSifra,TipPaketa) row_num From PripadnostKolProgram) " +
                "delete from cte Where row_num>1;", conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        textBox1.Text = row.Cells[6].Value.ToString();
                        textBox3.Text = row.Cells[7].Value.ToString();

                        if (Convert.ToInt32(row.Cells[5].Value.ToString()) == 1)
                        {
                            checkBox1.Checked = true;
                        }
                        else
                        {
                            checkBox1.Checked = false;
                        }

                        cboKolekcija.SelectedValue = row.Cells[1].Value.ToString();
                        cboProgram.SelectedValue = row.Cells[2].Value.ToString();
                    }
                }
            }
            catch
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dodela();
        }

        int ID;

        private void button2_Click(object sender, EventArgs e)
        {
            InsertPlanovi ins = new InsertPlanovi();

            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        if (checkBox1.Checked == true)
                        {
                            boja = 1;
                        }
                        else
                        {
                            boja = 0;
                        }

                        ID = Convert.ToInt32(row.Cells[0].Value.ToString());

                        ins.UpdatePripadnostKP(
                            ID,
                            Convert.ToInt32(cboKolekcija.SelectedValue),
                            Convert.ToInt32(cboProgram.SelectedValue),
                            Convert.ToInt32(row.Cells[3].Value),
                            boja,
                            Convert.ToDecimal(textBox1.Text.ToString().TrimEnd()),
                            Convert.ToDecimal(textBox3.Text.ToString().TrimEnd()));
                    }
                }
            }
            catch
            {
            }

            FillGV();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                        InsertPlanovi ins = new InsertPlanovi();
                        ins.DeletePripadnostKP(ID);
                    }
                }

                FillGV();
            }
            catch
            {
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Planovi_Excel.Izlaznost frm = new Izlaznost();
            frm.Show();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (Convert.ToInt32(row.Cells["MpSifra"].Value.ToString()) == Convert.ToInt32(textBox2.Text))
                    {
                        row.Selected = true;
                        dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.SelectedRows[0].Index;
                    }
                }

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (Convert.ToInt32(row.Cells["MpSifra"].Value.ToString()) == Convert.ToInt32(textBox2.Text))
                    {
                        row.Selected = true;
                        dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.SelectedRows[0].Index;
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void cboProgram_TextChanged(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        public class MaticniPodaci
        {
            public string ID { get; set; }
            public int KolekcijaID { get; set; }
            public string Kolekcija { get; set; }
            public string Program { get; set; }
            public int ProgramID { get; set; }
            public int MpSifra { get; set; }
            public string Naziv { get; set; }
            public int TipPaketa { get; set; }
            public decimal Koeficijent { get; set; }
        }

        private List<MaticniPodaci> list = new List<MaticniPodaci>();

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
                    MessageBox.Show("U izabranom fajlu nije pronađena nijedna stavka za import.");
                    return;
                }

                ObradiImportovanuListu();
                ObrisiDuplirano();
                FillGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju Excel fajla: " + ex.Message);
            }
        }

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
                int rowCount = worksheet.LastRowUsed() == null ? 0 : worksheet.LastRowUsed().RowNumber();

                for (int row = 2; row <= rowCount; row++)
                {
                    object idObj = worksheet.Cell(row, 1).GetFormattedString();
                    object kolekcijaObj = worksheet.Cell(row, 2).GetFormattedString();
                    object programObj = worksheet.Cell(row, 3).GetFormattedString();
                    object mpObj = worksheet.Cell(row, 4).GetFormattedString();
                    object nazivObj = worksheet.Cell(row, 5).GetFormattedString();
                    object paketObj = worksheet.Cell(row, 6).GetFormattedString();
                    object koeficijentObj = worksheet.Cell(row, 7).GetFormattedString();

                    DodajStavkuIzReda(row, idObj, kolekcijaObj, programObj, mpObj, nazivObj, paketObj, koeficijentObj);
                }
            }
        }

        private void UcitajXlsExcelDataReader(string fileName)
        {
            using (FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
            {
                int row = 0;

                while (reader.Read())
                {
                    row++;

                    if (row == 1)
                        continue;

                    object idObj = reader.FieldCount > 0 ? reader.GetValue(0) : null;
                    object kolekcijaObj = reader.FieldCount > 1 ? reader.GetValue(1) : null;
                    object programObj = reader.FieldCount > 2 ? reader.GetValue(2) : null;
                    object mpObj = reader.FieldCount > 3 ? reader.GetValue(3) : null;
                    object nazivObj = reader.FieldCount > 4 ? reader.GetValue(4) : null;
                    object paketObj = reader.FieldCount > 5 ? reader.GetValue(5) : null;
                    object koeficijentObj = reader.FieldCount > 6 ? reader.GetValue(6) : null;

                    DodajStavkuIzReda(row, idObj, kolekcijaObj, programObj, mpObj, nazivObj, paketObj, koeficijentObj);
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

                int logicalRow = 0;

                foreach (XElement rowElement in firstTable.Elements(tableNs + "table-row"))
                {
                    int repeatedRows = GetRepeatedCount(rowElement.Attribute(tableNs + "number-rows-repeated"), 1);
                    List<string> cells = ProcitajOdsCelijeIzReda(rowElement, tableNs, textNs, officeNs, 7);
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

                        DodajStavkuIzReda(
                            logicalRow,
                            cells[0],
                            cells[1],
                            cells[2],
                            cells[3],
                            cells[4],
                            cells[5],
                            cells[6]);
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

        private void DodajStavkuIzReda(
            int red,
            object idObj,
            object kolekcijaObj,
            object programObj,
            object mpObj,
            object nazivObj,
            object paketObj,
            object koeficijentObj)
        {
            bool prazanRed =
                string.IsNullOrWhiteSpace(ToStringExcel(idObj)) &&
                string.IsNullOrWhiteSpace(ToStringExcel(kolekcijaObj)) &&
                string.IsNullOrWhiteSpace(ToStringExcel(programObj)) &&
                string.IsNullOrWhiteSpace(ToStringExcel(mpObj)) &&
                string.IsNullOrWhiteSpace(ToStringExcel(nazivObj)) &&
                string.IsNullOrWhiteSpace(ToStringExcel(paketObj)) &&
                string.IsNullOrWhiteSpace(ToStringExcel(koeficijentObj));

            if (prazanRed)
                return;

            string kolekcijaNaziv = ToStringExcel(kolekcijaObj);
            string programNaziv = ToStringExcel(programObj);

            int kolekcijaID = VratiKolekcijaID(kolekcijaNaziv);

            if (kolekcijaID == 0)
            {
                throw new Exception(
                    "Ne postoji kolekcija: " + kolekcijaNaziv +
                    "\nRed u fajlu: " + red +
                    "\nProverite ispravnost podataka u fajlu.");
            }

            int programID = VratiProgramID(programNaziv);

            if (programID == 0)
            {
                throw new Exception(
                    "Ne postoji program: " + programNaziv +
                    "\nRed u fajlu: " + red +
                    "\nProverite ispravnost podataka u fajlu.");
            }

            var stavka = new MaticniPodaci
            {
                ID = ToStringExcel(idObj),
                KolekcijaID = kolekcijaID,
                Kolekcija = kolekcijaNaziv,
                ProgramID = programID,
                Program = programNaziv,
                MpSifra = ToIntExcel(mpObj),
                Naziv = ToStringExcel(nazivObj),
                TipPaketa = ToIntExcel(paketObj),
                Koeficijent = ToDecimalExcel(koeficijentObj)
            };

            list.Add(stavka);
        }

        private int VratiKolekcijaID(string naziv)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT TOP 1 ID
                  FROM Kolekcije
                  WHERE Naziv = @Naziv", conn))
            {
                cmd.Parameters.AddWithValue("@Naziv", naziv.Trim());
                conn.Open();

                object result = cmd.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    return 0;

                return Convert.ToInt32(result);
            }
        }

        private int VratiProgramID(string naziv)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT TOP 1 ID
                  FROM Program
                  WHERE Naziv = @Naziv", conn))
            {
                cmd.Parameters.AddWithValue("@Naziv", naziv.Trim());
                conn.Open();

                object result = cmd.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    return 0;

                return Convert.ToInt32(result);
            }
        }

        private void ObradiImportovanuListu()
        {
            InsertPlanovi ins = new InsertPlanovi();

            try
            {
                HashSet<int> postojeceMpSifre = new HashSet<int>();

                using (SqlConnection conn = new SqlConnection(connect))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT DISTINCT MpSifra FROM PripadnostKolProgram",
                        conn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                postojeceMpSifre.Add(Convert.ToInt32(dr["MpSifra"]));
                            }
                        }
                    }
                }

                var duplikati = list
                    .Where(x => postojeceMpSifre.Contains(x.MpSifra))
                    .ToList();

                bool pregaziPostojece = false;

                if (duplikati.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine($"Pronađeno je {duplikati.Count} artikala koji već postoje u tabeli.");
                    sb.AppendLine();
                    sb.AppendLine("Da li želite da pregazite postojeće zapise?");

                    DialogResult dr = MessageBox.Show(
                        sb.ToString(),
                        "Pronađeni duplikati",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    pregaziPostojece = dr == DialogResult.Yes;
                }

                foreach (var item in list)
                {
                    bool postoji = postojeceMpSifre.Contains(item.MpSifra);

                    if (postoji && !pregaziPostojece)
                        continue;

                    if (!postoji)
                    {
                        ins.InsertPripadnostKP(
                            item.KolekcijaID,
                            item.ProgramID,
                            item.MpSifra,
                            item.TipPaketa,
                            item.Koeficijent,
                            0);
                    }
                    else
                    {
                        int id = 0;

                        using (SqlConnection conn = new SqlConnection(connect))
                        {
                            conn.Open();

                            using (SqlCommand cmd = new SqlCommand(
                                @"SELECT TOP 1 ID
                                  FROM PripadnostKolProgram
                                  WHERE MpSifra = @MpSifra",
                                conn))
                            {
                                cmd.Parameters.AddWithValue("@MpSifra", item.MpSifra);

                                object result = cmd.ExecuteScalar();

                                if (result != null && result != DBNull.Value)
                                    id = Convert.ToInt32(result);
                            }
                        }

                        if (id > 0)
                        {
                            ins.UpdatePripadnostKP(
                                id,
                                item.KolekcijaID,
                                item.ProgramID,
                                item.MpSifra,
                                item.TipPaketa,
                                item.Koeficijent,
                                0);
                        }
                    }
                }

                MessageBox.Show(
                    $"Obrada završena.\n" +
                    $"Ukupno stavki: {list.Count}\n" +
                    $"Duplikata: {duplikati.Count}",
                    "Import",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private string ToStringExcel(object value)
        {
            if (value == null || value == DBNull.Value)
                return "";

            if (value is string)
                return value.ToString().Trim();

            if (value is double)
            {
                double d = (double)value;

                if (Math.Abs(d - Math.Round(d)) < 0.0000001)
                    return Convert.ToInt64(Math.Round(d)).ToString(CultureInfo.InvariantCulture);

                return d.ToString(CultureInfo.InvariantCulture).Trim();
            }

            if (value is float)
            {
                float f = (float)value;

                if (Math.Abs(f - Math.Round(f)) < 0.0000001)
                    return Convert.ToInt64(Math.Round(f)).ToString(CultureInfo.InvariantCulture);

                return f.ToString(CultureInfo.InvariantCulture).Trim();
            }

            if (value is decimal)
            {
                decimal d = (decimal)value;

                if (d == Math.Round(d))
                    return Convert.ToInt64(Math.Round(d)).ToString(CultureInfo.InvariantCulture);

                return d.ToString(CultureInfo.InvariantCulture).Trim();
            }

            return Convert.ToString(value, CultureInfo.InvariantCulture).Trim();
        }

        private int ToIntExcel(object value)
        {
            string text = ToStringExcel(value);

            if (string.IsNullOrWhiteSpace(text))
                return 0;

            int intResult;

            if (int.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out intResult))
                return intResult;

            if (int.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out intResult))
                return intResult;

            decimal decResult;

            if (decimal.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out decResult))
                return Convert.ToInt32(decResult);

            if (decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out decResult))
                return Convert.ToInt32(decResult);

            text = text.Replace(".", ",");

            if (decimal.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out decResult))
                return Convert.ToInt32(decResult);

            text = text.Replace(",", ".");

            if (decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out decResult))
                return Convert.ToInt32(decResult);

            return 0;
        }

        private decimal ToDecimalExcel(object value)
        {
            string text = ToStringExcel(value);

            if (string.IsNullOrWhiteSpace(text))
                return 0;

            text = text.Replace(" ", "");

            decimal result;

            if (decimal.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out result))
                return result;

            if (decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                return result;

            string textComma = text.Replace(".", ",");

            if (decimal.TryParse(textComma, NumberStyles.Any, CultureInfo.CurrentCulture, out result))
                return result;

            string textDot = text.Replace(",", ".");

            if (decimal.TryParse(textDot, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                return result;

            return 0;
        }
    }
}
