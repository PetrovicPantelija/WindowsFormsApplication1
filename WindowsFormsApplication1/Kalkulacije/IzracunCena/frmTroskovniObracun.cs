using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid.Enums;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    // ZAH-011: Troskovni obracun - ista funkcija kao stara forma frmTroskoviIzracun (dugme u Kalkulaciji stavki),
    // u izgledu izracuna cena. Obradi: procita fajl, posle potvrde obrise spisak, upise redove i pozove obracun
    // (sve u jednoj transakciji, vidi TroskovniObracunBaza); rezultat je [0Kalk].
    public partial class frmTroskovniObracun : Form
    {
        public static string code = "frmTroskovniObracun";
        public bool Pravo;

        readonly TroskovniObracunBaza baza = new TroskovniObracunBaza();
        readonly bool smeObradu;
        UlazTroskova poslednji;
        bool radi;

        public frmTroskovniObracun()
        {
            InitializeComponent();
            Izgled.Primeni(this);
            var prava = new PravaForme(code);
            Pravo = prava.Pravo;
            if (!Pravo) return;
            smeObradu = prava.Upis || prava.Izmena;
            btnObradi.Enabled = smeObradu;
            if (!smeObradu)
            {
                lblInfo.ForeColor = Color.FromArgb(200, 40, 40);
                lblInfo.Text = "Nemate pravo upisa ni izmene za ovu formu (frmTroskovniObracun), pa je Obradi zaključano. Pregled i izvoz rade.";
            }
            lblKorisnik.Text = "Korisnik: " + PravaForme.Korisnik;
            PodesiGrid();
            Osvezi();
        }

        void PodesiGrid()
        {
            grid.AllowEditing = false;
            grid.AllowSorting = true;
            grid.AllowFiltering = true;          // filter u zaglavlju kolone, kao u Excelu
            grid.AllowResizingColumns = true;
            grid.AutoSizeColumnsMode = AutoSizeColumnsMode.AllCells;
            grid.SelectionMode = GridSelectionMode.Extended;
            grid.Style.HeaderStyle.BackColor = Izgled.Zelena;
            grid.Style.HeaderStyle.TextColor = Color.White;
            grid.Style.HeaderStyle.FilterIconColor = Color.White;
            grid.Style.HeaderStyle.SortIconColor = Color.White;
            grid.Style.HeaderStyle.HoverFilterIconColor = Izgled.Akcenat;
            grid.Style.HeaderStyle.HoverSortIconColor = Izgled.Akcenat;
            grid.Style.SelectionStyle.BackColor = Color.FromArgb(232, 228, 218);
            grid.Style.SelectionStyle.TextColor = Color.FromArgb(40, 40, 40);
            grid.FilterChanged += (s, e) => OsveziStatus();
            grid.SelectionChanged += (s, e) => OsveziStatus();
        }

        void Osvezi()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                grid.DataSource = baza.Rezultat();
                OsveziStatus();
            }
            catch (Exception ex) { Izgled.Greska(this, "Učitavanje", ex); }
            finally { Cursor = Cursors.Default; }
        }

        void OsveziStatus()
        {
            var dt = grid.DataSource as DataTable;
            int prikazano = grid.View == null ? 0 : grid.View.Records.Count;
            lblPrikazano.Text = "Prikazano: " + prikazano + " / " + (dt == null ? 0 : dt.Rows.Count);
            lblIzabrano.Text = "Izabrano: " + grid.SelectedItems.Count;
        }

        // tokom obrade forma se ne zamrzava, ali dugmad su zakljucana
        void Radi(bool da)
        {
            radi = da;
            btnObradi.Enabled = !da && smeObradu;
            btnOsvezi.Enabled = btnIzvozCsv.Enabled = btnIzvozExcel.Enabled = btnIzaberi.Enabled = !da;
            btnPreskoceni.Enabled = !da && poslednji != null && poslednji.Preskoceni.Count > 0;
            txtPutanja.ReadOnly = da;
            progress.Visible = da;
            if (!da) lblNapredak.Text = "";
            Cursor = da ? Cursors.AppStarting : Cursors.Default;
        }

        void Faza(string tekst, int ukupno)
        {
            lblNapredak.Text = tekst;
            if (ukupno > 0)
            {
                progress.Style = ProgressBarStyle.Blocks;
                progress.Maximum = ukupno;
                progress.Value = 0;
            }
            else
                progress.Style = ProgressBarStyle.Marquee;
        }

        /* ---------- fajl ---------- */

        void btnIzaberi_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Excel i CSV (*.xlsx;*.xls;*.csv)|*.xlsx;*.xls;*.csv|Svi fajlovi (*.*)|*.*";
                dlg.Title = "Ulazni fajl za troškovni obračun";
                try
                {
                    string folder = Path.GetDirectoryName(txtPutanja.Text.Trim());
                    if (!string.IsNullOrEmpty(folder) && Directory.Exists(folder)) dlg.InitialDirectory = folder;
                }
                catch (ArgumentException) { /* u polju nije ispravna putanja; dijalog krece od podrazumevanog foldera */ }
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                txtPutanja.Text = dlg.FileName;
            }
        }

        /* ---------- obrada ---------- */

        async void btnObradi_Click(object sender, EventArgs e)
        {
            string fajl = txtPutanja.Text.Trim();
            if (fajl == "") { MessageBox.Show(this, "Izaberite ulazni fajl (Excel ili CSV).", Text); return; }
            if (!File.Exists(fajl)) { MessageBox.Show(this, "Fajl ne postoji:\n" + fajl, Text); return; }
            if (!UlazTroskova.JeExcel(fajl) && !UlazTroskova.JeCsv(fajl))
            {
                MessageBox.Show(this, "Podržani su Excel (.xlsx, .xls) i CSV (.csv) fajlovi.", Text);
                return;
            }

            var sat = Stopwatch.StartNew();
            Radi(true);
            try
            {
                Faza("Čitam fajl...", 0);
                var ulaz = await Task.Run(() => UlazTroskova.Procitaj(fajl));
                if (ulaz.Redovi.Count == 0)
                {
                    Radi(false);
                    MessageBox.Show(this, "U fajlu nema nijednog ispravnog reda (prvi red je zaglavlje).\nSpisak nije menjan.",
                                    Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (ulaz.Preskoceni.Count > 0) PrikaziPreskocene(ulaz);
                    return;
                }

                int postojeci = await Task.Run(() => baza.BrojRedova());
                if (postojeci > 0)
                {
                    sat.Stop();
                    string pitanje = "Postojeći spisak (" + postojeci + " redova) biće obrisan.\n\n" +
                                     "Iz fajla za upis: " + ulaz.Redovi.Count + " redova" +
                                     (ulaz.PreskocenoRedova > 0 ? ", preskočeno: " + ulaz.PreskocenoRedova + " (spisak posle obrade)" : "") +
                                     ".\n\nNastaviti?";
                    if (MessageBox.Show(this, pitanje, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                        MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    {
                        Radi(false);
                        lblInfo.Text = "Obrada nije pokrenuta, postojeći spisak nije menjan.";
                        return;
                    }
                    sat.Start();
                }

                int ukupno = ulaz.Redovi.Count;
                Faza("Red 0 / " + ukupno, ukupno);
                var napredak = new Progress<int>(n =>
                {
                    progress.Value = Math.Min(n, progress.Maximum);
                    if (n < ukupno) lblNapredak.Text = "Red " + n + " / " + ukupno;
                    else Faza("Red " + n + " / " + ukupno + " - obračun u toku...", 0);
                });
                await Task.Run(() => baza.Obracunaj(ulaz.Redovi, napredak));
                sat.Stop();

                poslednji = ulaz;
                Radi(false);
                Osvezi();
                PrikaziRezime(ulaz, sat.Elapsed);
            }
            catch (Exception ex)
            {
                Radi(false);
                Izgled.Greska(this, "Troškovni obračun", ex);
            }
        }

        void PrikaziRezime(UlazTroskova ulaz, TimeSpan trajanje)
        {
            string vreme = trajanje.TotalMinutes >= 1 ? trajanje.ToString(@"m\:ss") + " min" : trajanje.TotalSeconds.ToString("0.0") + " s";
            int preskoceno = ulaz.PreskocenoRedova;
            int listova = ulaz.SakrivenihListova;
            lblObrada.Text = "Poslednja obrada: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            lblInfo.ForeColor = Color.FromArgb(80, 80, 80);
            lblInfo.Text = Path.GetFileName(ulaz.Fajl) + "   ·   pročitano " + ulaz.Procitano + ", upisano " + ulaz.Redovi.Count +
                           ", preskočeno " + preskoceno + (ulaz.Praznih > 0 ? ", praznih redova " + ulaz.Praznih : "") +
                           (listova > 0 ? ", sakrivenih listova " + listova : "") + "   ·   trajanje " + vreme;

            MessageBox.Show(this,
                "Obrada je završena.\n\n" +
                "Pročitano redova: " + ulaz.Procitano + "\n" +
                "Upisano: " + ulaz.Redovi.Count + "\n" +
                "Preskočeno: " + preskoceno + "\n" +
                (ulaz.Praznih > 0 ? "Praznih redova (ne računaju se): " + ulaz.Praznih + "\n" : "") +
                (listova > 0 ? "Sakrivenih listova (nisu čitani): " + listova + "\n" : "") +
                "Trajanje: " + vreme,
                Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (ulaz.Preskoceni.Count > 0) PrikaziPreskocene(ulaz);
        }

        void PrikaziPreskocene(UlazTroskova ulaz)
        {
            Izgled.PrikaziTabele(this, "Preskočeni redovi - " + Path.GetFileName(ulaz.Fajl), "Preskočeni", ulaz.TabelaPreskocenih());
        }

        void btnPreskoceni_Click(object sender, EventArgs e)
        {
            if (poslednji != null) PrikaziPreskocene(poslednji);
        }

        void btnOsvezi_Click(object sender, EventArgs e)
        {
            Osvezi();
        }

        /* ---------- izvoz (tekuci prikaz: vidljive kolone, redovi posle filtera i sortiranja) ---------- */

        List<KeyValuePair<string, string>> VidljiveKolone()
        {
            return grid.Columns.Where(c => c.Visible)
                       .Select(c => new KeyValuePair<string, string>(c.MappingName, c.HeaderText)).ToList();
        }

        List<DataRow> PrikazaniRedovi()
        {
            if (grid.View == null) return new List<DataRow>();
            return grid.View.Records.Select(r => r.Data as DataRowView).Where(v => v != null).Select(v => v.Row).ToList();
        }

        void Izvezi(string naslov, string filter, string nastavak, Action<string, List<KeyValuePair<string, string>>, List<DataRow>> izvoz)
        {
            var redovi = PrikazaniRedovi();
            if (redovi.Count == 0) { MessageBox.Show(this, "Nema redova za izvoz.", naslov); return; }
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = filter;
                dlg.FileName = IzvozTroskova.ImeFajla(nastavak, DateTime.Now);
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    Cursor = Cursors.WaitCursor;
                    izvoz(dlg.FileName, VidljiveKolone(), redovi);
                    Cursor = Cursors.Default;
                    MessageBox.Show(this, "Izvezeno redova: " + redovi.Count + "\n" + dlg.FileName, naslov,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { Cursor = Cursors.Default; Izgled.Greska(this, naslov, ex); }
            }
        }

        void btnIzvozCsv_Click(object sender, EventArgs e)
        {
            Izvezi("Izvoz CSV", "CSV (*.csv)|*.csv", ".csv", IzvozTroskova.Csv);
        }

        void btnIzvozExcel_Click(object sender, EventArgs e)
        {
            Izvezi("Izvoz u Excel", "Excel (*.xlsx)|*.xlsx", ".xlsx", IzvozTroskova.Excel);
        }

        void frmTroskovniObracun_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!radi) return;
            e.Cancel = true;
            MessageBox.Show(this, "Obrada je u toku. Sačekajte da se završi.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
