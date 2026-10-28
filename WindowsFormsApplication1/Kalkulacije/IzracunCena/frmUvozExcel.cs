using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    // ZAH-008: Novi opsti cenovnik (administrator), sa ili bez uvoza iz Excela (KALK RSD / KALK EUR).
    // Cenovnik uvek dobija sve maticne podatke iz baze; Excel daje VP, TP-Stari, marzu staru, rabate i akcije.
    // Posle upisa se odmah racuna TP-Novi (IC_AzurirajTroskove).
    public partial class frmUvozExcel : Form
    {
        public static string code = "frmUvozExcel";
        public bool Pravo;

        readonly IzracunCenaBaza baza = new IzracunCenaBaza();
        readonly string korisnik;
        UvozExcel uvoz;

        public frmUvozExcel()
        {
            InitializeComponent();
            Izgled.Primeni(this);
            var prava = new PravaForme(code);
            Pravo = prava.Pravo;
            if (!Pravo) return;
            korisnik = PravaForme.Korisnik;
            btnNapravi.Enabled = prava.Upis;
            cboValuta.SelectedIndex = 0;
        }

        void cboValuta_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool rsd = cboValuta.Text == ObracunCene.RSD;
            txtNaziv.Text = (rsd ? "Opšti" : "Devizni") + " - " + DateTime.Today.ToString("yy - MM");
            numPDV.Enabled = rsd;
        }

        void btnIzaberi_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Excel (*.xls;*.xlsx)|*.xls;*.xlsx";
                dlg.Title = "KALK " + cboValuta.Text;
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                txtFajl.Text = dlg.FileName;
                uvoz = null;
                dgvSheetovi.DataSource = null;
                dgvProblemi.DataSource = null;
                lblSazetak.Text = "Pritisnite Pročitaj za pregled pre upisa.";
            }
        }

        void btnProcitaj_Click(object sender, EventArgs e)
        {
            if (txtFajl.Text == "") { MessageBox.Show(this, "Izaberite Excel fajl.", Text); return; }
            try
            {
                Cursor = Cursors.WaitCursor;
                baza.OsveziProgrameIKolekcije();
                uvoz = UvozExcel.Procitaj(txtFajl.Text, baza.MaticniPodaci());
                Cursor = Cursors.Default;

                dgvSheetovi.DataSource = uvoz.Listovi.Select(l => new
                {
                    Sheet = l.Naziv,
                    Preskocen = l.Preskocen ? "da: " + l.Razlog : "",
                    Nadjeno = l.Nadjeno,
                    Povezano = l.Povezano,
                    Rabat = l.Rabat.HasValue ? (l.Rabat.Value * 100).ToString("0.##") + "%" : "",
                    Akcije = string.Join(" / ", l.Akcije.Select(a => (a * 100).ToString("0.##") + "%")),
                    Kurs = l.Kurs.HasValue ? l.Kurs.Value.ToString("N2") : "",
                    Nivo = l.Nivo
                }).ToList();
                dgvProblemi.DataSource = uvoz.Problemi.Select(p => new { p.List, p.Red, p.Vrsta, p.Opis }).ToList();
                tabProblemi.Text = "Problemi (" + uvoz.Problemi.Count + ")";

                if (uvoz.Kurs.HasValue && cboValuta.Text == ObracunCene.EUR && uvoz.Kurs.Value >= numKurs.Minimum && uvoz.Kurs.Value <= numKurs.Maximum)
                    numKurs.Value = Math.Round(uvoz.Kurs.Value, 2);

                var cen = uvoz.Parametri.Svi.FirstOrDefault(p => p.Nivo == NivoParametra.Cenovnik);
                lblSazetak.Text = "Sheetova: " + uvoz.Listovi.Count(l => !l.Preskocen) + " (preskočeno " + uvoz.Listovi.Count(l => l.Preskocen) + ")"
                    + ", redova: " + uvoz.Redovi.Count + ", cena po Code: " + uvoz.Cene.Count
                    + ", Code bez cene u Excelu (cena iz baze): " + uvoz.CodeBezCeneUExcelu
                    + "\nRabat celog cenovnika: " + (cen != null && cen.Rabat.HasValue ? (cen.Rabat.Value * 100).ToString("0.##") + "%" : "-")
                    + ", kurs iz D1: " + (uvoz.Kurs.HasValue ? uvoz.Kurs.Value.ToString("N2") : "-");
            }
            catch (Exception ex) { Cursor = Cursors.Default; uvoz = null; Izgled.Greska(this, "Čitanje Excela", ex); }
        }

        void btnNapravi_Click(object sender, EventArgs e)
        {
            string naziv = txtNaziv.Text.Trim();
            if (naziv == "") { MessageBox.Show(this, "Naziv je obavezan.", Text); return; }
            if (txtFajl.Text != "" && uvoz == null) { MessageBox.Show(this, "Pritisnite Pročitaj pre upisa.", Text); return; }

            string opis = uvoz == null
                ? "Napraviti cenovnik \"" + naziv + "\" od svih proizvoda iz baze, sa cenama iz jela_products i troškovima iz KalkCenik?"
                : "Napraviti cenovnik \"" + naziv + "\" iz Excela (" + uvoz.Cene.Count + " cena, " + uvoz.Redovi.Count + " redova) i ostalih proizvoda iz baze?";
            if (MessageBox.Show(this, opis, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                Cursor = Cursors.WaitCursor;
                var r = baza.UpisiUvoz(naziv, cboValuta.Text, numKurs.Value, numPDV.Value / 100, korisnik,
                    uvoz == null ? new Dictionary<string, decimal>() : uvoz.Cene,
                    uvoz == null ? new List<IzracunCenaBaza.UvozRed>() : uvoz.Redovi,
                    uvoz == null ? new List<ParametarCenovnika>() : uvoz.Parametri.Svi.ToList());
                Cursor = Cursors.Default;

                if (r.Preskoceni.Rows.Count > 0 || r.BezTroska.Rows.Count > 0)
                    Izgled.PrikaziTabele(this, "Cenovnik \"" + naziv + "\" je napravljen",
                        "Preskočeni (nema Code, programa ili kolekcije)", r.Preskoceni,
                        "Bez važećeg troška", r.BezTroska);

                if (MessageBox.Show(this, "Cenovnik \"" + naziv + "\" je napravljen. Otvoriti ga u formi Opšti cenovnik?",
                        Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    var f = new frmOpstiCenovnik(r.CenovnikID);
                    if (f.Pravo) f.Show();
                }
            }
            catch (Exception ex) { Cursor = Cursors.Default; Izgled.Greska(this, "Upis cenovnika", ex); }
        }

        void btnZatvori_Click(object sender, EventArgs e) { Close(); }
    }
}
