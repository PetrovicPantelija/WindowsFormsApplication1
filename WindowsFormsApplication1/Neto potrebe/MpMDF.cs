using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
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
using WindowsFormsApplication1.Kalkulacije;

namespace WindowsFormsApplication1.Neto_potrebe
{
    public partial class MpMDF : Form
    {
        private string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        //komentar1
        public MpMDF()
        {
            InitializeComponent();
        }

        private void MpMDF_Load(object sender, EventArgs e)
        {
            FillGV();
            FillGrupe();
        }
        private void FillGV()
        {
            string PrviAnd = "";
            string DrugiAnd = "";
            string TreciEnd = "";
            if (textBoxSearch.Text != "")
            {
                PrviAnd = " and  MpStaraSif like '%" + textBoxSearch.Text + "%' ";
            }
            if (textBoxSearch2.Text != "")
            {
                DrugiAnd = " and  MpStaraSif like '%" + textBoxSearch2.Text + "%' ";
            }
            if (chkGrupa.Checked == true)
            {
                TreciEnd = " and mdfGrupa <> ''  order by MpStaraSif asc";
            }
            string MpAnd = "";
            if (textBoxSearchMP.Text != "")
            {
                MpAnd = " and MaticniPodatki.MpSifra =" + Convert.ToInt32(textBoxSearchMP.Text);
            }



            var query = " Select  MAticniPodatki.MpSifra as Sifra, MpStaraSif as Kod, MpNaziv as NAziv, MpSifEnoteMere1 as JM, MpOsnSklad, MpOsnLokacija, MpStatus,  " +
                " MdfGrupe.Naziv as MDFGrupa,  MDFPodgrupe.Naziv as Podgrupa, MAticniPodatkiIgor.Materijal1, MAticniPodatkiIgor.Materijal2, MAticniPodatkiIgor.Debljina," +
                " MAticniPodatkiIgor.Tehnologija1, MAticniPodatkiIgor.Tehnologija2,ISNull(MdfGrupe.ID,0) as GrupaID,ISNull(MdfPodgrupe.ID,0) as PodgrupaID  from MaticniPodatki " +
                " left join MaticniPodatkiIgor on MaticniPodatkiIgor.MpSifra = MaticniPOdatki.MpSifra " +
                " Left join MDFGrupe on MdfGrupe.ID = MaticniPodatkiIgor.MDFGrupa " +
                " Left join MdfPodgrupe on MAticniPodatkiIgor.MDFPodgrupa = MDFPodgrupe.ID where MpStatus = 0 ";
            query = query + MpAnd+ PrviAnd + DrugiAnd + TreciEnd;
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];


            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "MP sifra";
            dataGridView1.Columns[0].Width = 80;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Kod";
            dataGridView1.Columns[1].Width = 180;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Naziv";
            dataGridView1.Columns[2].Width = 160;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "JM";
            dataGridView1.Columns[3].Width = 40;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Sklad";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Lokac";
            dataGridView1.Columns[5].Width = 80;

            dataGridView1.Columns["GrupaID"].Visible = false;
            dataGridView1.Columns["PodgrupaID"].Visible = false;
        }
        private void FillGrupe()
        {
            var query = "Select ID,Naziv From MdfGrupe order by ID asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboGrupa.DataSource = ds.Tables[0];
            cboGrupa.DisplayMember = "Naziv";
            cboGrupa.ValueMember = "ID";
        }
        private void FillPodgrupe()
        {
            var query = "Select ID,Naziv From MDFPodgrupe Where IDNadredjena=" + Convert.ToInt32(cboGrupa.SelectedValue) + " order by ID asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboPodgrupa.DataSource = ds.Tables[0];
            cboPodgrupa.DisplayMember = "Naziv";
            cboPodgrupa.ValueMember = "ID";
        }

        private void cboGrupa_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void cboGrupa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillPodgrupe();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillGV();
        }

        private void btnDodeli_Click(object sender, EventArgs e)
        {
            int SifraMp;
            int grupa;
            int podgrupa;
            
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {

                    SifraMp = Convert.ToInt32(row.Cells[0].Value.ToString());
                    grupa = Convert.ToInt32(row.Cells["GrupaID"].Value.ToString());
                    podgrupa = Convert.ToInt32(row.Cells["PodgrupaID"].Value.ToString());
                    
                    InsertVeza program = new InsertVeza();
                    program.InsProgram(Convert.ToInt32(SifraMp), Convert.ToInt32(cboGrupa.SelectedValue), Convert.ToInt32(cboPodgrupa.SelectedValue), txtMaterijal1.Text, txtMaterijal2.Text, txtDebljina.Text, txtTehnologija1.Text, txtTehnologija2.Text);
                }
            }
            FillGV();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FillGV();
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            int SifraMp;
            int grupa;
            int podgrupa;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {

                    SifraMp = Convert.ToInt32(row.Cells[0].Value.ToString());
                    grupa = Convert.ToInt32(row.Cells["GrupaID"].Value.ToString());
                    podgrupa = Convert.ToInt32(row.Cells["PodgrupaID"].Value.ToString());

                    InsertVeza program = new InsertVeza();
                    program.DelProgram(Convert.ToInt32(SifraMp));
                }
            }
            FillGV();
        }

        private void btnPromeni_Click(object sender, EventArgs e)
        {
            int SifraMp;
            int grupa;
            int podgrupa;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {

                    SifraMp = Convert.ToInt32(row.Cells[0].Value.ToString());
                    grupa = Convert.ToInt32(row.Cells["GrupaID"].Value.ToString());
                    podgrupa = Convert.ToInt32(row.Cells["PodgrupaID"].Value.ToString());

                    InsertVeza program = new InsertVeza();
                    program.UpdProgram(Convert.ToInt32(SifraMp), Convert.ToInt32(cboGrupa.SelectedValue), Convert.ToInt32(cboPodgrupa.SelectedValue), txtMaterijal1.Text, txtMaterijal2.Text, txtDebljina.Text, txtTehnologija1.Text, txtTehnologija2.Text);
                }
            }
            FillGV();
        }
        public class MaticniPodatak
        {
            public int MpSifra { get; set; }
            public int Grupa { get; set; }
            public int Podgrupa { get; set; }
            public string Materija1 { get; set; }
            public string Materijal2 { get; set; }
            public string Debljina { get; set; }
            public string Tehnologija1 { get; set; }
            public string Tehnologija2 { get; set; }
        }

        private List<MaticniPodatak> list = new List<MaticniPodatak>();


        // ============================================================
        // IMPORT EXCEL
        // ============================================================

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
                FillGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Greška pri importu:\n\n" + ex.Message,
                    "Greška",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // ============================================================
        // UČITAVANJE FAJLA
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
                    object mpSifraObj =
                        worksheet.Cell(row, 1).Value;

                    object grupaObj =
                        worksheet.Cell(row, 2).Value;

                    object podgrupaObj =
                        worksheet.Cell(row, 3).Value;

                    object materijal1Obj =
                        worksheet.Cell(row, 4).Value;

                    object materijal2Obj =
                        worksheet.Cell(row, 5).Value;

                    object debljinaObj =
                        worksheet.Cell(row, 6).Value;

                    object tehnologija1Obj =
                        worksheet.Cell(row, 7).Value;

                    object tehnologija2Obj =
                        worksheet.Cell(row, 8).Value;

                    DodajMaticniPodatakIzReda(
                        row,
                        mpSifraObj,
                        grupaObj,
                        podgrupaObj,
                        materijal1Obj,
                        materijal2Obj,
                        debljinaObj,
                        tehnologija1Obj,
                        tehnologija2Obj);
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

                    object mpSifraObj =
                        reader.FieldCount > 0
                            ? reader.GetValue(0)
                            : null;

                    object grupaObj =
                        reader.FieldCount > 1
                            ? reader.GetValue(1)
                            : null;

                    object podgrupaObj =
                        reader.FieldCount > 2
                            ? reader.GetValue(2)
                            : null;

                    object materijal1Obj =
                        reader.FieldCount > 3
                            ? reader.GetValue(3)
                            : null;

                    object materijal2Obj =
                        reader.FieldCount > 4
                            ? reader.GetValue(4)
                            : null;

                    object debljinaObj =
                        reader.FieldCount > 5
                            ? reader.GetValue(5)
                            : null;

                    object tehnologija1Obj =
                        reader.FieldCount > 6
                            ? reader.GetValue(6)
                            : null;

                    object tehnologija2Obj =
                        reader.FieldCount > 7
                            ? reader.GetValue(7)
                            : null;

                    DodajMaticniPodatakIzReda(
                        row,
                        mpSifraObj,
                        grupaObj,
                        podgrupaObj,
                        materijal1Obj,
                        materijal2Obj,
                        debljinaObj,
                        tehnologija1Obj,
                        tehnologija2Obj);
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
                            8);

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

                        DodajMaticniPodatakIzReda(
                            logicalRow,
                            cells[0],
                            cells[1],
                            cells[2],
                            cells[3],
                            cells[4],
                            cells[5],
                            cells[6],
                            cells[7]);
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
            List<string> values =
                new List<string>();

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
                     i < repeatedColumns &&
                     values.Count < brojKolona;
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
        // ODS - PONOVLJENI REDOVI/KOLONE
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
        // DODAVANJE PODATKA IZ REDA
        // ============================================================

        private void DodajMaticniPodatakIzReda(
            int red,
            object mpSifraObj,
            object grupaObj,
            object podgrupaObj,
            object materijal1Obj,
            object materijal2Obj,
            object debljinaObj,
            object tehnologija1Obj,
            object tehnologija2Obj)
        {
            bool prazanRed =
                string.IsNullOrWhiteSpace(
                    ToStringExcel(mpSifraObj)) &&
                string.IsNullOrWhiteSpace(
                    ToStringExcel(grupaObj)) &&
                string.IsNullOrWhiteSpace(
                    ToStringExcel(podgrupaObj)) &&
                string.IsNullOrWhiteSpace(
                    ToStringExcel(materijal1Obj)) &&
                string.IsNullOrWhiteSpace(
                    ToStringExcel(materijal2Obj)) &&
                string.IsNullOrWhiteSpace(
                    ToStringExcel(debljinaObj)) &&
                string.IsNullOrWhiteSpace(
                    ToStringExcel(tehnologija1Obj)) &&
                string.IsNullOrWhiteSpace(
                    ToStringExcel(tehnologija2Obj));

            if (prazanRed)
                return;


            int mpSifra =
                ToIntExcel(mpSifraObj);

            int grupa =
                ToIntExcel(grupaObj);

            int podgrupa =
                ToIntExcel(podgrupaObj);


            // --------------------------------------------------------
            // PROVERA MPSIFRE
            // --------------------------------------------------------

            if (mpSifra == 0)
            {
                throw new Exception(
                    "MPSifra nije ispravna.\n\n" +
                    "Vrednost: " +
                    ToStringExcel(mpSifraObj) +
                    "\n" +
                    "Red u fajlu: " +
                    red);
            }


            // --------------------------------------------------------
            // PROVERA GRUPE
            // --------------------------------------------------------

            if (grupa == 0)
            {
                throw new Exception(
                    "Grupa nije ispravna.\n\n" +
                    "Vrednost: " +
                    ToStringExcel(grupaObj) +
                    "\n" +
                    "Red u fajlu: " +
                    red);
            }


            // --------------------------------------------------------
            // PROVERA PODGRUPE
            // --------------------------------------------------------

            if (podgrupa == 0)
            {
                throw new Exception(
                    "Podgrupa nije ispravna.\n\n" +
                    "Vrednost: " +
                    ToStringExcel(podgrupaObj) +
                    "\n" +
                    "Red u fajlu: " +
                    red);
            }


            MaticniPodatak podatak =
                new MaticniPodatak
                {
                    MpSifra = mpSifra,
                    Grupa = grupa,
                    Podgrupa = podgrupa,

                    Materija1 =
                        ToStringExcel(materijal1Obj),

                    Materijal2 =
                        ToStringExcel(materijal2Obj),

                    Debljina =
                        ToStringExcel(debljinaObj),

                    Tehnologija1 =
                        ToStringExcel(tehnologija1Obj),

                    Tehnologija2 =
                        ToStringExcel(tehnologija2Obj)
                };


            list.Add(podatak);
        }


        // ============================================================
        // OBRADA I VALIDACIJA
        // ============================================================

        private void ObradiImportovanuListu()
        {
            // ========================================================
            // UČITAVANJE PODATAKA IZ BAZE
            // ========================================================

            HashSet<int> postojeceMpSifre =
                new HashSet<int>();

            HashSet<int> postojeceGrupe =
                new HashSet<int>();

            Dictionary<int, int> podgrupe =
                new Dictionary<int, int>();


            using (SqlConnection conn =
                   new SqlConnection(connect))
            {
                conn.Open();


                // ====================================================
                // POSTOJEĆE MPSIFRE
                // ====================================================

                using (SqlCommand cmd =
                       new SqlCommand(
                           @"SELECT DISTINCT MpSifra
                     FROM MaticniPodatkiIgor",
                           conn))
                {
                    using (SqlDataReader dr =
                           cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (dr["MpSifra"] != DBNull.Value)
                            {
                                postojeceMpSifre.Add(
                                    Convert.ToInt32(
                                        dr["MpSifra"]));
                            }
                        }
                    }
                }


                // ====================================================
                // POSTOJEĆE GRUPE
                // ====================================================

                using (SqlCommand cmd =
                       new SqlCommand(
                           @"SELECT ID
                     FROM MDfGrupe",
                           conn))
                {
                    using (SqlDataReader dr =
                           cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (dr["ID"] != DBNull.Value)
                            {
                                postojeceGrupe.Add(
                                    Convert.ToInt32(
                                        dr["ID"]));
                            }
                        }
                    }
                }


                // ====================================================
                // PODGRUPE I NJIHOVE GRUPE
                // ====================================================

                using (SqlCommand cmd =
                       new SqlCommand(
                           @"SELECT ID, IDNadredjena
                     FROM MDFPodgrupe",
                           conn))
                {
                    using (SqlDataReader dr =
                           cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (dr["ID"] == DBNull.Value)
                                continue;

                            int podgrupaID =
                                Convert.ToInt32(
                                    dr["ID"]);

                            int grupaID = 0;

                            if (dr["IDNadredjena"] != DBNull.Value)
                            {
                                grupaID =
                                    Convert.ToInt32(
                                        dr["IDNadredjena"]);
                            }

                            if (!podgrupe.ContainsKey(
                                    podgrupaID))
                            {
                                podgrupe.Add(
                                    podgrupaID,
                                    grupaID);
                            }
                        }
                    }
                }
            }


            // ========================================================
            // DUPLIKATI UNUTAR EXCELA
            // ========================================================

            var duplikatiUExcelu =
                list
                    .GroupBy(x => x.MpSifra)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

            if (duplikatiUExcelu.Count > 0)
            {
                throw new Exception(
                    "U Excel fajlu postoje duplirane MPSifre:\n\n" +
                    string.Join(
                        ", ",
                        duplikatiUExcelu));
            }


            // ========================================================
            // PROVERA DA LI MPSIFRE VEĆ POSTOJE U BAZI
            // ========================================================

            var duplikatiUBazi =
                list
                    .Where(x =>
                        postojeceMpSifre.Contains(
                            x.MpSifra))
                    .ToList();

            if (duplikatiUBazi.Count > 0)
            {
                StringBuilder sb =
                    new StringBuilder();

                sb.AppendLine(
                    "Sledeće MPSifre već postoje u tabeli MaticniPodatkiIgor:");
                sb.AppendLine();

                foreach (MaticniPodatak item
                         in duplikatiUBazi)
                {
                    sb.AppendLine(
                        "MPSifra: " +
                        item.MpSifra);
                }

                sb.AppendLine();
                sb.AppendLine(
                    "Import je prekinut.");

                throw new Exception(
                    sb.ToString());
            }


            // ========================================================
            // VALIDACIJA GRUPE I PODGRUPE
            // ========================================================

            foreach (MaticniPodatak item in list)
            {
                // ----------------------------------------------------
                // DA LI GRUPA POSTOJI
                // ----------------------------------------------------

                if (!postojeceGrupe.Contains(
                        item.Grupa))
                {
                    throw new Exception(
                        "Grupa ne postoji u tabeli MDfGrupe.\n\n" +
                        "MPSifra: " +
                        item.MpSifra +
                        "\n" +
                        "Grupa: " +
                        item.Grupa);
                }


                // ----------------------------------------------------
                // DA LI PODGRUPA POSTOJI
                // ----------------------------------------------------

                if (!podgrupe.ContainsKey(
                        item.Podgrupa))
                {
                    throw new Exception(
                        "Podgrupa ne postoji u tabeli MDFPodgrupe.\n\n" +
                        "MPSifra: " +
                        item.MpSifra +
                        "\n" +
                        "Grupa: " +
                        item.Grupa +
                        "\n" +
                        "Podgrupa: " +
                        item.Podgrupa);
                }


                // ----------------------------------------------------
                // DA LI PODGRUPA PRIPADA GRUPI
                // ----------------------------------------------------

                int nadredjenaGrupa =
                    podgrupe[item.Podgrupa];

                if (nadredjenaGrupa != item.Grupa)
                {
                    throw new Exception(
                        "Podgrupa ne pripada izabranoj grupi.\n\n" +
                        "MPSifra: " +
                        item.MpSifra +
                        "\n" +
                        "Grupa iz Excel-a: " +
                        item.Grupa +
                        "\n" +
                        "Podgrupa: " +
                        item.Podgrupa +
                        "\n" +
                        "Podgrupa pripada grupi: " +
                        nadredjenaGrupa);
                }
            }


            // ========================================================
            // SVE JE VALIDNO - UPIS
            // ========================================================

            InsertVeza program =
                new InsertVeza();

            int brojUpisanih = 0;

            foreach (MaticniPodatak item in list)
            {
                program.InsProgram(
                    item.MpSifra,
                    item.Grupa,
                    item.Podgrupa,
                    item.Materija1,
                    item.Materijal2,
                    item.Debljina,
                    item.Tehnologija1,
                    item.Tehnologija2);

                brojUpisanih++;
            }


            // ========================================================
            // OSVEŽAVANJE GRID-A
            // ========================================================

            FillGV();


            // ========================================================
            // PORUKA
            // ========================================================

            MessageBox.Show(
                "Import je uspešno završen.\n\n" +
                "Ukupno učitano: " +
                list.Count +
                "\n" +
                "Uspešno upisano: " +
                brojUpisanih,
                "Import",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }


        // ============================================================
        // STRING IZ EXCELA
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
        // INT IZ EXCELA
        // ============================================================

        private int ToIntExcel(object value)
        {
            string text =
                ToStringExcel(value);

            if (string.IsNullOrWhiteSpace(text))
                return 0;


            int intResult;

            if (int.TryParse(
                text,
                NumberStyles.Any,
                CultureInfo.CurrentCulture,
                out intResult))
            {
                return intResult;
            }


            if (int.TryParse(
                text,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out intResult))
            {
                return intResult;
            }


            decimal decResult;

            if (decimal.TryParse(
                text,
                NumberStyles.Any,
                CultureInfo.CurrentCulture,
                out decResult))
            {
                return Convert.ToInt32(decResult);
            }


            if (decimal.TryParse(
                text,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out decResult))
            {
                return Convert.ToInt32(decResult);
            }


            text = text.Replace(".", ",");

            if (decimal.TryParse(
                text,
                NumberStyles.Any,
                CultureInfo.CurrentCulture,
                out decResult))
            {
                return Convert.ToInt32(decResult);
            }


            text = text.Replace(",", ".");

            if (decimal.TryParse(
                text,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out decResult))
            {
                return Convert.ToInt32(decResult);
            }


            return 0;
        }
    }
}
