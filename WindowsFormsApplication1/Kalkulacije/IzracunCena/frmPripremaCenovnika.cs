using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    // ZAH-008: Priprema cenovnika (drugi korisnik). Radi na radnoj kopiji opsteg cenovnika:
    // menja rabat, vazecu akciju, VP i zeljenu cenu posle rabata; opsti cenovnik se ne menja.
    // Proglasenjem vazecim nastaje zamrznut cenovnik sa nazivom, periodom i partnerom.
    public partial class frmPripremaCenovnika : Form
    {
        public static string code = "frmPripremaCenovnika";
        public bool Pravo;

        readonly IzracunCenaBaza baza = new IzracunCenaBaza();
        readonly string korisnik;
        bool ucitavanje;

        public frmPripremaCenovnika() : this(null) { }

        public frmPripremaCenovnika(int? radniCenovnikID)
        {
            InitializeComponent();
            Izgled.Primeni(this);
            var prava = new PravaForme(code);
            Pravo = prava.Pravo;
            if (!Pravo) return;
            korisnik = PravaForme.Korisnik;
            lblKorisnik.Text = "Korisnik: " + korisnik;
            uc.Promenjeno += (s, e) => OsveziStatus();
            UcitajListu(radniCenovnikID);
        }

        void UcitajListu(int? izaberi)
        {
            var dt = baza.Cenovnici("Radni");
            ucitavanje = true;
            cboRadna.DisplayMember = "Naziv";
            cboRadna.ValueMember = "CenovnikID";
            cboRadna.DataSource = dt;
            ucitavanje = false;
            bool ima = dt.Rows.Count > 0;
            foreach (Control c in new Control[] { btnSacuvaj, btnOdustani, btnPrimeni, btnIzvoz, btnProglasi, btnObrisi, uc })
                c.Enabled = ima;
            if (!ima)
            {
                lblInfo.Text = "Nema radnih kopija. Napravite novu od opšteg cenovnika.";
                pnlObavestenje.Visible = false;
                return;
            }
            cboRadna.SelectedValue = izaberi ?? (int)dt.Rows[0]["CenovnikID"];
            UcitajCenovnik();
        }

        int? IzabraniID
        {
            get { return cboRadna.SelectedValue is int ? (int)cboRadna.SelectedValue : (int?)null; }
        }

        void UcitajCenovnik()
        {
            if (!IzabraniID.HasValue) return;
            try
            {
                uc.Ucitaj(IzabraniID.Value, RezimCenovnika.Korisnik);
                var z = uc.Zaglavlje;
                bool rsd = uc.Valuta == ObracunCene.RSD;
                chkEUR.Visible = rsd;
                chkEUR.Checked = false;
                pnlObavestenje.Visible = Convert.ToBoolean(z["IzvorPromenjen"]);
                Text = "Priprema cenovnika - " + z["Naziv"];
                lblInfo.Text = "Izvor: " + z["IzvorniNaziv"] + "   ·   Valuta " + uc.Valuta
                    + "   ·   Kurs " + Convert.ToDecimal(z["Kurs"]).ToString("N2")
                    + "   ·   Napravljena " + Convert.ToDateTime(z["DatumKreiranja"]).ToString("dd.MM.yyyy HH:mm") + " (" + z["Kreirao"] + ")";
                OsveziStatus();
            }
            catch (Exception ex) { Izgled.Greska(this, "Učitavanje", ex); }
        }

        void OsveziStatus()
        {
            lblPrikazano.Text = "Prikazano: " + uc.Prikazani.Count + " / " + uc.Stavke.Count;
            lblIzmene.Text = "Nesačuvanih izmena: " + uc.BrojIzmena;
        }

        bool PitajZaIzmene(string sta)
        {
            if (uc.BrojIzmena == 0) return true;
            var odg = MessageBox.Show(this, "Ima nesačuvanih izmena. Sačuvati ih pre " + sta + "?", Text,
                                      MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (odg == DialogResult.Cancel) return false;
            if (odg == DialogResult.Yes) return Sacuvaj();
            return true;
        }

        bool Sacuvaj()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                uc.Sacuvaj(korisnik);
                return true;
            }
            catch (Exception ex) { Izgled.Greska(this, "Čuvanje", ex); return false; }
            finally { Cursor = Cursors.Default; }
        }

        /* ---------- radna kopija ---------- */

        void cboRadna_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ucitavanje) return;
            if (!PitajZaIzmene("promene radne kopije")) { cboRadna.SelectedValue = uc.CenovnikID; return; }
            UcitajCenovnik();
        }

        void btnNova_Click(object sender, EventArgs e)
        {
            if (!PitajZaIzmene("nove radne kopije")) return;
            var opsti = baza.Cenovnici("Opsti");
            if (opsti.Rows.Count == 0)
            {
                MessageBox.Show(this, "Nema opšteg cenovnika od kog bi se napravila kopija.", "Nova radna kopija");
                return;
            }

            using (var f = new Form())
            {
                f.Text = "Nova radna kopija";
                f.FormBorderStyle = FormBorderStyle.FixedDialog;
                f.MinimizeBox = false; f.MaximizeBox = false;
                f.StartPosition = FormStartPosition.CenterParent;
                f.ClientSize = new Size(440, 150);
                f.Font = Izgled.Slova;
                var l1 = new Label { Text = "Od opšteg cenovnika:", Left = 12, Top = 14, AutoSize = true };
                var cbo = new ComboBox { Left = 150, Top = 10, Width = 278, DropDownStyle = ComboBoxStyle.DropDownList, DisplayMember = "Naziv", ValueMember = "CenovnikID", DataSource = opsti };
                var l2 = new Label { Text = "Naziv kopije:", Left = 12, Top = 50, AutoSize = true };
                var txt = new TextBox { Left = 150, Top = 46, Width = 278 };
                cbo.SelectedIndexChanged += (s, a) => txt.Text = "Radna - " + cbo.Text + " - " + DateTime.Now.ToString("dd.MM.yyyy HH.mm");
                var ok = new Button { Text = "Napravi", Left = 252, Top = 104, Width = 84, Height = 30, DialogResult = DialogResult.OK, Tag = "glavno" };
                var ne = new Button { Text = "Odustani", Left = 344, Top = 104, Width = 84, Height = 30, DialogResult = DialogResult.Cancel };
                f.Controls.AddRange(new Control[] { l1, cbo, l2, txt, ok, ne });
                f.AcceptButton = ok; f.CancelButton = ne;
                Izgled.Primeni(f);
                f.Shown += (s, a) => txt.Text = "Radna - " + cbo.Text + " - " + DateTime.Now.ToString("dd.MM.yyyy HH.mm");
                if (f.ShowDialog(this) != DialogResult.OK || txt.Text.Trim() == "") return;

                try
                {
                    Cursor = Cursors.WaitCursor;
                    int id = baza.NapraviRadniCenovnik((int)cbo.SelectedValue, txt.Text.Trim(), korisnik);
                    Cursor = Cursors.Default;
                    UcitajListu(id);
                }
                catch (Exception ex) { Cursor = Cursors.Default; Izgled.Greska(this, "Nova radna kopija", ex); }
            }
        }

        void btnObrisi_Click(object sender, EventArgs e)
        {
            if (!IzabraniID.HasValue) return;
            if (MessageBox.Show(this, "Obrisati radnu kopiju \"" + cboRadna.Text + "\"?\nVažeći cenovnici napravljeni od nje ostaju.",
                    "Obriši radnu kopiju", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                baza.ObrisiRadniCenovnik(IzabraniID.Value);
                uc.Stavke.Clear();
                UcitajListu(null);
            }
            catch (Exception ex) { Izgled.Greska(this, "Obriši radnu kopiju", ex); }
        }

        void btnPreuzmi_Click(object sender, EventArgs e)
        {
            if (!PitajZaIzmene("preuzimanja izmena")) return;
            try
            {
                var r = baza.PreuzmiIzmeneOpsteg(uc.CenovnikID, korisnik);
                UcitajCenovnik();
                MessageBox.Show(this, "Preuzeto iz opšteg cenovnika:\n  VP cena: " + r["PreuzeteCene"] +
                                "\n  troškova: " + r["PreuzetiTroskovi"] + "\n  novih redova: " + r["NoviRedovi"] +
                                "\n\nCode koje ste sami menjali zadržavaju vašu VP.", "Preuzmi izmene");
            }
            catch (Exception ex) { Izgled.Greska(this, "Preuzmi izmene", ex); }
        }

        /* ---------- izmene ---------- */

        static bool Procitaj(TextBox t, string naziv, decimal min, decimal max, out decimal? vrednost)
        {
            vrednost = null;
            string s = t.Text.Trim();
            if (s == "") return true;
            decimal d;
            if (!decimal.TryParse(s, NumberStyles.Number, CultureInfo.CurrentCulture, out d)
                && !decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out d))
            {
                MessageBox.Show(naziv + ": unesite broj.", "Primeni na prikazane");
                return false;
            }
            if (d < min || d > max)
            {
                MessageBox.Show(naziv + " mora biti između " + min + " i " + max + ".", "Primeni na prikazane");
                return false;
            }
            vrednost = d;
            return true;
        }

        void btnPrimeni_Click(object sender, EventArgs e)
        {
            decimal? rabat, akcija, povecanje;
            if (!Procitaj(txtRabat, "Rabat", 0, 100, out rabat)) return;
            if (!Procitaj(txtAkcija, "Akcija", 0, 100, out akcija)) return;
            if (!Procitaj(txtPovecanje, "% povećanja VP", -99, 1000, out povecanje)) return;
            if (!rabat.HasValue && !akcija.HasValue && !povecanje.HasValue)
            {
                MessageBox.Show(this, "Upišite rabat, akciju ili % povećanja VP.", "Primeni na prikazane");
                return;
            }
            int broj = uc.Prikazani.Count;
            if (MessageBox.Show(this, "Primeniti na " + broj + " prikazanih redova?", "Primeni na prikazane",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            uc.PrimeniNaPrikazane(rabat, akcija, povecanje);
            txtRabat.Clear(); txtAkcija.Clear(); txtPovecanje.Clear();
        }

        void chkEUR_CheckedChanged(object sender, EventArgs e)
        {
            uc.PrikaziEUR(chkEUR.Checked);
        }

        void btnSacuvaj_Click(object sender, EventArgs e)
        {
            if (Sacuvaj())
                MessageBox.Show(this, "Izmene su sačuvane.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void btnOdustani_Click(object sender, EventArgs e)
        {
            if (uc.BrojIzmena > 0 && MessageBox.Show(this, "Odbaciti sve nesačuvane izmene?", Text,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            UcitajCenovnik();
        }

        void btnIzvoz_Click(object sender, EventArgs e)
        {
            if (uc.Zaglavlje == null) return;
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "Excel (*.xlsx)|*.xlsx";
                dlg.FileName = IzvozExcel.ImeFajla(uc.Zaglavlje["Naziv"].ToString(), DateTime.Now);
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    Cursor = Cursors.WaitCursor;
                    IzvozExcel.Izvezi(dlg.FileName, uc.Zaglavlje, uc.Prikazani, chkEUR.Checked);
                    baza.UpisiPresek(uc.CenovnikID, dlg.FileName, korisnik);
                    Cursor = Cursors.Default;
                    MessageBox.Show(this, "Izvezeno: " + dlg.FileName, "Izvoz u Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { Cursor = Cursors.Default; Izgled.Greska(this, "Izvoz u Excel", ex); }
            }
        }

        void btnProglasi_Click(object sender, EventArgs e)
        {
            if (uc.BrojIzmena > 0)
            {
                if (MessageBox.Show(this, "Izmene moraju biti sačuvane pre proglašenja. Sačuvati sada?", "Proglasi važećim",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                if (!Sacuvaj()) return;
            }
            using (var dlg = new dlgProglasiVazecim(uc.CenovnikID, uc.Valuta, korisnik))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    MessageBox.Show(this, "Cenovnik \"" + dlg.Naziv + "\" je proglašen važećim.\nVidi se u formi Pregled cenovnika.",
                                    "Proglasi važećim", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void frmPripremaCenovnika_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Pravo && !PitajZaIzmene("zatvaranja")) e.Cancel = true;
        }
    }
}
