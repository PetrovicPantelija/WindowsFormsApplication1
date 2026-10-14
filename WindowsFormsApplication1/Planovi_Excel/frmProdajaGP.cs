using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGridConverter;
using Syncfusion.XlsIO;
using WindowsFormsApplication1.Magacin;

namespace WindowsFormsApplication1.Planovi_Excel
{
    /// <summary>Prodaja GP - zavrsene isporuke (RkShippingZavrseno) po proizvodima iz dbo.jela_products, filter po kolekciji i programu.</summary>
    public partial class frmProdajaGP : Form
    {
        public static string code = "frmProdajaGP";
        public bool Pravo;
        int idForme;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        bool _bezDogadjaja;   // true dok kod menja selekciju/checkbox, da se dogadjaji ne pozivaju za svaki red

        private readonly string _cs =
            ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        private const string SqlProdaja =
            "SELECT dbo.RkShippingZavrseno.DatumUtovara, dbo.jela_products.SifraProizvodaLargo, dbo.jela_products.NazivRobneGrupe, dbo.jela_products.NazivGrupe, dbo.jela_products.KorpusOpis, " +
            "dbo.jela_products.FrontOpis, dbo.RkShippingItemProZavrseno.ProizvodIDz, dbo.RkShippingItemProZavrseno.BojaKorpusIDz, dbo.RkShippingItemProZavrseno.BojaFrontIDz, " +
            "dbo.jela_products.BarCode, dbo.RkShippingItemProZavrseno.BarCode AS Expr1, dbo.RkShippingItemProZavrseno.Kolicina " +
            "FROM dbo.RkShippingZavrseno INNER JOIN " +
            "dbo.RkShippingItemProZavrseno ON dbo.RkShippingZavrseno.ShippingID = dbo.RkShippingItemProZavrseno.ShipingIDz INNER JOIN " +
            "dbo.jela_products ON dbo.jela_products.BarCode = dbo.RkShippingItemProZavrseno.BarCode";

        public frmProdajaGP()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            SfGridPomocnik.Stilizuj(sfGrid);
            IdGrupe();
            IdForme();
            PravoPristupa();
        }

        // ------------------------------------------------------------------ prava pristupa (isti model kao ostale forme)

        public string IdGrupe()
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("Select IdGrupe from KorisnikGrupa Where Korisnik = @Korisnik", conn))
            {
                cmd.Parameters.AddWithValue("@Korisnik", Kor);
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    var grupe = new List<string>();
                    while (dr.Read()) grupe.Add(Convert.ToInt32(dr["IdGrupe"]).ToString(CultureInfo.InvariantCulture));
                    niz = string.Join(",", grupe);   // samo brojevi, pa je bezbedno za IN (...)
                }
            }
            return niz;
        }

        private int IdForme()
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("Select IdForme from Forme where Rtrim(Code) = @Code", conn))
            {
                cmd.Parameters.AddWithValue("@Code", code);
                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) idForme = Convert.ToInt32(dr["IdForme"]);
                }
            }
            return idForme;
        }

        private void PravoPristupa()
        {
            Pravo = false;
            if (niz.Length > 0 && idForme != 0)
            {
                using (var conn = new SqlConnection(_cs))
                using (var cmd = new SqlCommand("Select * From GrupaForma Where IdGrupe in (" + niz + ") and IdForme = @IdForme", conn))
                {
                    cmd.Parameters.AddWithValue("@IdForme", idForme);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                        Pravo = reader.HasRows;
                }
            }
            if (!Pravo) MessageBox.Show("Nemate prava za pristup ovoj formi", code);

            lbKolekcija.Enabled = Pravo;
            lbProgram.Enabled = Pravo;
            chkSveKolekcije.Enabled = Pravo;
            chkSvePrograme.Enabled = Pravo;
            btnPrikazi.Enabled = Pravo;
            btnPonisti.Enabled = Pravo;
            btnExcel.Enabled = Pravo;
        }

        // ------------------------------------------------------------------ ucitavanje

        private void frmProdajaGP_Load(object sender, EventArgs e)
        {
            if (!Pravo) return;
            try
            {
                PuniListu(lbKolekcija, "SELECT DISTINCT NazivRobneGrupe FROM dbo.jela_products WHERE NazivRobneGrupe IS NOT NULL ORDER BY NazivRobneGrupe", null, null);
                PuniProgram();
                Ucitaj();
            }
            catch (Exception ex)
            {
                Greska(ex);
            }
        }

        /// <summary>Puni listbox distinct vrednostima; opciono samo za izabrane vrednosti kolone filterKolona.</summary>
        private void PuniListu(ListBox lb, string sql, string filterKolona, List<string> filter)
        {
            var izabrano = new HashSet<string>(lb.SelectedItems.Cast<string>());
            var vrednosti = new List<string>();
            using (var cn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = cn;
                cmd.CommandText = filterKolona == null ? sql : string.Format(sql, Uslov(cmd, filterKolona, "f", filter));
                cn.Open();
                using (var r = cmd.ExecuteReader())
                    while (r.Read()) vrednosti.Add(r.GetString(0));
            }

            bool bilo = _bezDogadjaja;
            _bezDogadjaja = true;
            lb.BeginUpdate();
            lb.Items.Clear();
            foreach (string v in vrednosti)
            {
                int i = lb.Items.Add(v);
                if (izabrano.Contains(v)) lb.SetSelected(i, true);
            }
            lb.EndUpdate();
            _bezDogadjaja = bilo;
        }

        /// <summary>Programi (NazivGrupe) - ako je izabrana kolekcija, samo programi iz izabranih kolekcija.</summary>
        private void PuniProgram()
        {
            List<string> kolekcije = Filter(lbKolekcija);
            if (kolekcije.Count == 0)
                PuniListu(lbProgram, "SELECT DISTINCT NazivGrupe FROM dbo.jela_products WHERE NazivGrupe IS NOT NULL ORDER BY NazivGrupe", null, null);
            else
                PuniListu(lbProgram, "SELECT DISTINCT NazivGrupe FROM dbo.jela_products WHERE NazivGrupe IS NOT NULL AND {0} ORDER BY NazivGrupe",
                    "NazivRobneGrupe", kolekcije);

            if (chkSvePrograme.Checked) IzaberiSve(lbProgram, true);   // "Izaberi sve" vazi i za novu listu programa
            else UskladiCheck(lbProgram, chkSvePrograme);
        }

        /// <summary>Selektuje ili skida selekciju sa svih stavki, bez dogadjaja za svaku stavku.</summary>
        private void IzaberiSve(ListBox lb, bool sve)
        {
            bool bilo = _bezDogadjaja;
            _bezDogadjaja = true;
            lb.BeginUpdate();
            if (sve)
                for (int i = 0; i < lb.Items.Count; i++) lb.SetSelected(i, true);
            else
                lb.ClearSelected();
            if (lb.Items.Count > 0) lb.TopIndex = 0;
            lb.EndUpdate();
            _bezDogadjaja = bilo;
        }

        /// <summary>Checkbox "Izaberi sve" je stikliran samo ako su sve stavke izabrane.</summary>
        private void UskladiCheck(ListBox lb, CheckBox chk)
        {
            bool bilo = _bezDogadjaja;
            _bezDogadjaja = true;
            chk.Checked = lb.Items.Count > 0 && lb.SelectedItems.Count == lb.Items.Count;
            _bezDogadjaja = bilo;
        }

        private void Ucitaj()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                var table = new DataTable();
                using (var cn = new SqlConnection(_cs))
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    var uslovi = new List<string>();
                    List<string> kolekcije = Filter(lbKolekcija);
                    List<string> programi = Filter(lbProgram);
                    if (kolekcije.Count > 0) uslovi.Add(Uslov(cmd, "dbo.jela_products.NazivRobneGrupe", "k", kolekcije));
                    if (programi.Count > 0) uslovi.Add(Uslov(cmd, "dbo.jela_products.NazivGrupe", "p", programi));

                    cmd.CommandText = SqlProdaja +
                                      (uslovi.Count > 0 ? " WHERE " + string.Join(" AND ", uslovi) : "") +
                                      " ORDER BY dbo.RkShippingZavrseno.DatumUtovara DESC";
                    using (var da = new SqlDataAdapter(cmd))
                        da.Fill(table);
                }
                SfGridPomocnik.Popuni(sfGrid, table);
                Text = "Prodaja GP (" + table.Rows.Count.ToString("N0") + ")";
            }
            catch (Exception ex)
            {
                Greska(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>Izabrane vrednosti za filter; prazna lista = bez filtera (nista izabrano ili sve izabrano).</summary>
        private static List<string> Filter(ListBox lb)
        {
            if (lb.SelectedItems.Count == lb.Items.Count) return new List<string>();
            return lb.SelectedItems.Cast<string>().ToList();
        }

        /// <summary>"kolona IN (@k0, @k1, ...)" uz dodate parametre.</summary>
        private static string Uslov(SqlCommand cmd, string kolona, string prefiks, List<string> vrednosti)
        {
            var imena = new List<string>();
            for (int i = 0; i < vrednosti.Count; i++)
            {
                string ime = "@" + prefiks + i.ToString(CultureInfo.InvariantCulture);
                cmd.Parameters.Add(ime, SqlDbType.NVarChar, 200).Value = vrednosti[i];
                imena.Add(ime);
            }
            return kolona + " IN (" + string.Join(", ", imena) + ")";
        }

        // ------------------------------------------------------------------ dogadjaji

        private void lbKolekcija_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Pravo || _bezDogadjaja) return;
            UskladiCheck(lbKolekcija, chkSveKolekcije);
            OsveziProgram();
        }

        private void lbProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Pravo || _bezDogadjaja) return;
            UskladiCheck(lbProgram, chkSvePrograme);
        }

        private void chkSveKolekcije_CheckedChanged(object sender, EventArgs e)
        {
            if (!Pravo || _bezDogadjaja) return;
            IzaberiSve(lbKolekcija, chkSveKolekcije.Checked);
            OsveziProgram();
        }

        private void chkSvePrograme_CheckedChanged(object sender, EventArgs e)
        {
            if (!Pravo || _bezDogadjaja) return;
            IzaberiSve(lbProgram, chkSvePrograme.Checked);
        }

        private void OsveziProgram()
        {
            try
            {
                PuniProgram();
            }
            catch (Exception ex)
            {
                Greska(ex);
            }
        }

        private void btnPrikazi_Click(object sender, EventArgs e)
        {
            Ucitaj();
        }

        private void btnPonisti_Click(object sender, EventArgs e)
        {
            _bezDogadjaja = true;
            chkSveKolekcije.Checked = false;
            chkSvePrograme.Checked = false;
            _bezDogadjaja = false;
            IzaberiSve(lbKolekcija, false);
            IzaberiSve(lbProgram, false);
            OsveziProgram();
            Ucitaj();
        }

        // ------------------------------------------------------------------ izvoz u Excel (postuje filter i sortiranje u gridu)

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (sfGrid.View == null || sfGrid.View.Records.Count == 0)
            {
                MessageBox.Show(this, "Nema podataka za izvoz. Kliknite Prikaži.", "Izvoz", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string putanja;
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "Excel (*.xlsx)|*.xlsx";
                dlg.DefaultExt = "xlsx";
                dlg.FileName = "ProdajaGP_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xlsx";
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                putanja = dlg.FileName;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                var options = new ExcelExportingOptions { ExcelVersion = ExcelVersion.Xlsx };
                using (ExcelEngine excelEngine = sfGrid.ExportToExcel(sfGrid.View, options))
                {
                    IWorkbook workbook = excelEngine.Excel.Workbooks[0];
                    workbook.Worksheets[0].UsedRange.AutofitColumns();
                    workbook.SaveAs(putanja);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Greška pri izvozu u Excel:\n" + ex.Message, "Izvoz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            if (MessageBox.Show(this, "Fajl je sačuvan:\n" + putanja + "\n\nOtvoriti ga?", "Izvoz",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Process.Start(putanja);
        }

        private void sfGrid_AutoGeneratingColumn(object sender, Syncfusion.WinForms.DataGrid.Events.AutoGeneratingColumnArgs e)
        {
            e.Column.HeaderText = e.Column.MappingName;

            var datum = e.Column as Syncfusion.WinForms.DataGrid.GridDateTimeColumn;
            if (datum != null) datum.Format = "dd.MM.yyyy";

            var num = e.Column as Syncfusion.WinForms.DataGrid.GridNumericColumn;
            if (num == null) return;
            NumberFormatInfo nfi = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
            nfi.NumberDecimalDigits = e.Column.MappingName == "Kolicina" ? 2 : 0;
            if (e.Column.MappingName != "Kolicina") nfi.NumberGroupSeparator = "";   // ID-jevi bez separatora hiljada
            num.NumberFormatInfo = nfi;
        }

        private void Greska(Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Prodaja GP", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
