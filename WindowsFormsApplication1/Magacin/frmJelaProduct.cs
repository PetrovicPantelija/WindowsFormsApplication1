using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Events;

namespace WindowsFormsApplication1.Magacin
{
    /// <summary>Jela product - pregled tabele dbo.jela_products sa unosom, izmenom, deaktiviranjem i importom iz Excela.</summary>
    public partial class frmJelaProduct : Form
    {
        public static string code = "frmJelaProduct";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";

        private readonly string _cs =
            ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

        public frmJelaProduct()
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
            if (niz.Length == 0 || idForme == 0)
            {
                MessageBox.Show("Nemate prava za pristup ovoj formi", code);
                Pravo = false;
            }
            else
            {
                using (var conn = new SqlConnection(_cs))
                using (var cmd = new SqlCommand("Select * From GrupaForma Where IdGrupe in (" + niz + ") and IdForme = @IdForme", conn))
                {
                    cmd.Parameters.AddWithValue("@IdForme", idForme);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Nemate prava za pristup ovoj formi", code);
                            Pravo = false;
                        }
                        else
                        {
                            Pravo = true;
                            // grupa koja ima pravo u bilo kojoj od svojih grupa, ne samo u poslednjoj
                            while (reader.Read())
                            {
                                insert = insert || Convert.ToBoolean(reader["Upis"]);
                                update = update || Convert.ToBoolean(reader["Izmena"]);
                                delete = delete || Convert.ToBoolean(reader["Brisanje"]);
                            }
                        }
                    }
                }
            }

            btnUnos.Enabled = Pravo && insert;
            btnIzmena.Enabled = Pravo && update;
            btnDeaktiviranje.Enabled = Pravo && delete;
            btnImportExcel.Enabled = Pravo && insert && update;   // import i dodaje i menja
            chkNeaktivni.Enabled = Pravo;
        }

        // ------------------------------------------------------------------ ucitavanje

        private void frmJelaProduct_Load(object sender, EventArgs e)
        {
            if (Pravo) Ucitaj(null);
        }

        private void Ucitaj(string selectBarCode)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DataTable table = JelaProductRepo.Load(_cs, chkNeaktivni.Checked);
                SfGridPomocnik.Popuni(sfGrid, table);
                Text = "Jela product (" + table.Rows.Count.ToString("N0") + ")";
                if (selectBarCode != null) IzaberiRed(selectBarCode);
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

        /// <summary>Vraca selekciju na red sa datim BarCode-om (posle izmene). Ako ga nema u prikazu, preskace se.</summary>
        private void IzaberiRed(string barCode)
        {
            try
            {
                foreach (SfRed r in SfGridPomocnik.Redovi(sfGrid))
                {
                    if (string.Equals(r.Stavka["BarCode"] as string, barCode, StringComparison.Ordinal))
                    {
                        sfGrid.SelectedIndex = r.Index;
                        return;
                    }
                }
            }
            catch (Exception) { /* nebitno */ }
        }

        private DataRowView Tekuci()
        {
            return sfGrid.SelectedItem as DataRowView;
        }

        private static Dictionary<string, object> UVrednosti(DataRowView row)
        {
            var d = new Dictionary<string, object>();
            foreach (var f in JelaProductRepo.Fields)
                d[f.Name] = row[f.Name] == DBNull.Value ? null : row[f.Name];
            return d;
        }

        private void chkNeaktivni_CheckedChanged(object sender, EventArgs e)
        {
            if (!Pravo) return;
            var cur = Tekuci();
            Ucitaj(cur == null ? null : cur["BarCode"] as string);
        }

        // ------------------------------------------------------------------ unos

        private void btnUnos_Click(object sender, EventArgs e)
        {
            using (var dlg = new frmJelaProductEdit(null, true))
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    JelaProductRepo.Insert(_cs, dlg.Values, dlg.Active);
                    Ucitaj((string)dlg.Values["BarCode"]);
                }
                catch (SqlException ex)
                {
                    if (JelaProductRepo.IsDuplicateKey(ex))
                        MessageBox.Show(this, "Zapis sa BarCode-om " + dlg.Values["BarCode"] + " već postoji (možda je neaktivan).",
                            "Jela product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        Greska(ex);
                }
                catch (Exception ex)
                {
                    Greska(ex);
                }
            }
        }

        // ------------------------------------------------------------------ izmena

        private void btnIzmena_Click(object sender, EventArgs e)
        {
            DataRowView row = Tekuci();
            if (row == null)
            {
                MessageBox.Show(this, "Izaberi red za izmenu.", "Jela product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string barCode = (string)row["BarCode"];
            using (var dlg = new frmJelaProductEdit(UVrednosti(row), (bool)row["Active"]))
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    JelaProductRepo.Update(_cs, barCode, dlg.Values, dlg.Active);
                    Ucitaj(barCode);
                }
                catch (Exception ex)
                {
                    Greska(ex);
                }
            }
        }

        // ------------------------------------------------------------------ deaktiviranje (Active = false)

        private void btnDeaktiviranje_Click(object sender, EventArgs e)
        {
            DataRowView row = Tekuci();
            if (row == null)
            {
                MessageBox.Show(this, "Izaberi red za deaktiviranje.", "Jela product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!(bool)row["Active"])
            {
                MessageBox.Show(this, "Zapis je već neaktivan.", "Jela product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string barCode = (string)row["BarCode"];
            string naziv = row["SifraProizvodaLargo"] as string;
            if (MessageBox.Show(this, "Deaktivirati zapis " + naziv + " (" + barCode + ")?", "Jela product",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                JelaProductRepo.SetActive(_cs, barCode, false);
                Ucitaj(barCode);
            }
            catch (Exception ex)
            {
                Greska(ex);
            }
        }

        // ------------------------------------------------------------------ Import from Excel

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            string path;
            using (var ofd = new OpenFileDialog { Filter = "Excel (*.xlsx)|*.xlsx", Title = "Import from Excel" })
            {
                if (ofd.ShowDialog(this) != DialogResult.OK) return;
                path = ofd.FileName;
            }

            ImportPlan plan;
            try
            {
                Cursor = Cursors.WaitCursor;
                plan = JelaProductImport.Build(_cs, path);       // samo priprema, nista se jos ne upisuje
            }
            catch (Exception ex)
            {
                Greska(ex);
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            if (plan.Fatal != null)
            {
                MessageBox.Show(this, plan.Fatal, "Import from Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string rezime =
                "Redova u fajlu: " + plan.TotalRows + "\n" +
                "Novi zapisi: " + plan.Inserts.Count + "\n" +
                "Izmenjeni zapisi: " + plan.Updates.Count + "\n" +
                "Bez promene: " + plan.Unchanged + "\n" +
                "Preskočeno zbog greške: " + plan.Skipped;
            if (plan.Errors.Count > 0)
                rezime += "\n\nGreške (prvih 15):\n" + string.Join("\n", plan.Errors.Take(15)) +
                          (plan.Errors.Count > 15 ? "\n... i još " + (plan.Errors.Count - 15) : "");

            if (plan.Inserts.Count + plan.Updates.Count == 0)
            {
                MessageBox.Show(this, rezime + "\n\nNema šta da se upiše.", "Import from Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show(this, rezime + "\n\nSnimiti izmene u bazu?", "Import from Excel",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                Cursor = Cursors.WaitCursor;
                JelaProductImport.Apply(_cs, plan);              // jedna transakcija: ili sve ili ništa
            }
            catch (Exception ex)
            {
                Greska(ex);
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            Ucitaj(null);
            MessageBox.Show(this, "Import završen.\nDodato: " + plan.Inserts.Count + "\nIzmenjeno: " + plan.Updates.Count,
                "Import from Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ------------------------------------------------------------------ grid

        private void sfGrid_AutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
        {
            e.Column.HeaderText = e.Column.MappingName;

            GridNumericColumn num = e.Column as GridNumericColumn;
            if (num == null) return;

            int digits;
            switch (e.Column.MappingName)
            {
                case "Bruto":
                case "Neto":
                    digits = 3;
                    break;
                case "price_rs_WP":
                case "price_WP":
                    digits = 2;
                    break;
                default: // ID-jevi, BrojPaketa, Kompleta
                    digits = 0;
                    break;
            }

            NumberFormatInfo nfi = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
            nfi.NumberDecimalDigits = digits;
            if (digits == 0) nfi.NumberGroupSeparator = "";   // ID-jevi bez separatora hiljada
            num.NumberFormatInfo = nfi;
        }

        private void Greska(Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Jela product", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
