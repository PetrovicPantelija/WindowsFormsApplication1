using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    // ZAH-008: Opsti cenovnik (administrator) - dinarski i devizni, ista forma.
    // Menjaju se VP, marza i % povecanja VP; kurs i PDV u zaglavlju; troskovi iz KalkCenik.
    public partial class frmOpstiCenovnik : Form
    {
        public static string code = "frmOpstiCenovnik";
        public bool Pravo;

        readonly IzracunCenaBaza baza = new IzracunCenaBaza();
        readonly string korisnik;
        bool ucitavanje;
        bool zaglavljeIzmenjeno;

        public frmOpstiCenovnik() : this(null) { }

        // cenovnikID: otvori ovaj cenovnik (npr. posle uvoza)
        public frmOpstiCenovnik(int? cenovnikID)
        {
            InitializeComponent();
            Izgled.Primeni(this);
            var prava = new PravaForme(code);
            Pravo = prava.Pravo;
            if (!Pravo) return;
            korisnik = PravaForme.Korisnik;
            btnSacuvaj.Enabled = prava.Izmena || prava.Upis;
            cboPredlog.SelectedIndex = 0;
            lblKorisnik.Text = "Korisnik: " + korisnik;
            uc.Promenjeno += (s, e) => OsveziStatus();
            UcitajListu(cenovnikID);
        }

        void UcitajListu(int? izaberi)
        {
            var dt = baza.Cenovnici("Opsti");
            ucitavanje = true;
            cboCenovnik.DisplayMember = "Naziv";
            cboCenovnik.ValueMember = "CenovnikID";
            cboCenovnik.DataSource = dt;
            ucitavanje = false;
            if (dt.Rows.Count == 0)
            {
                lblInfo.Text = "Nema opštih cenovnika. Napravite ga u formi Uvoz iz Excela.";
                return;
            }
            cboCenovnik.SelectedValue = izaberi ?? (int)dt.Rows[0]["CenovnikID"];
            UcitajCenovnik();
        }

        int? IzabraniID
        {
            get { return cboCenovnik.SelectedValue is int ? (int)cboCenovnik.SelectedValue : (int?)null; }
        }

        void UcitajCenovnik()
        {
            if (!IzabraniID.HasValue) return;
            try
            {
                uc.Ucitaj(IzabraniID.Value, RezimCenovnika.Administrator);
                var z = uc.Zaglavlje;
                bool rsd = uc.Valuta == ObracunCene.RSD;
                ucitavanje = true;
                numKurs.Value = Math.Min(numKurs.Maximum, Math.Max(numKurs.Minimum, Convert.ToDecimal(z["Kurs"])));
                numPDV.Value = Convert.ToDecimal(z["PDV"]) * 100;
                ucitavanje = false;
                numPDV.Enabled = rsd;
                chkEUR.Visible = rsd;
                chkEUR.Checked = false;
                zaglavljeIzmenjeno = false;
                Text = "Opšti cenovnik - " + z["Naziv"];
                PrikaziInfo();
            }
            catch (Exception ex) { Izgled.Greska(this, "Učitavanje", ex); }
        }

        void PrikaziInfo()
        {
            var z = uc.Zaglavlje;
            var cen = uc.Parametri.Svi.FirstOrDefault(p => p.Nivo == NivoParametra.Cenovnik);
            int programa = uc.Parametri.Svi.Count(p => p.Nivo == NivoParametra.Program && (p.Rabat.HasValue || p.Akcija.HasValue));
            int kolekcija = uc.Parametri.Svi.Count(p => p.Nivo == NivoParametra.Kolekcija && (p.Rabat.HasValue || p.Akcija.HasValue));
            string rabat = cen != null && cen.Rabat.HasValue ? (cen.Rabat.Value * 100).ToString("0.##") + "%" : "-";
            string akcije = cen != null && cen.AkcijePoredjenje.Count > 0
                ? string.Join(" / ", cen.AkcijePoredjenje.Select(a => (a * 100).ToString("0.##") + "%")) : "-";
            lblInfo.Text = "Valuta " + uc.Valuta + "   ·   Rabat cenovnika " + rabat + ", akcije " + akcije
                + "   ·   Posebni rabat/akcija: " + programa + " programa, " + kolekcija + " kolekcija"
                + "   ·   Troškovi ažurirani: " + Datum(z["DatumAzuriranjaTroska"])
                + ", proglašeni novim: " + Datum(z["DatumProglasenjaTroska"]);
        }

        static string Datum(object d)
        {
            return d == null || d == DBNull.Value ? "-" : Convert.ToDateTime(d).ToString("dd.MM.yyyy HH:mm");
        }

        void OsveziStatus()
        {
            var prikazani = uc.Prikazani;
            lblPrikazano.Text = "Prikazano: " + prikazani.Count + " / " + uc.Stavke.Count;
            lblBezTroska.Text = "Bez troška: " + uc.Stavke.Count(s => s.BezTroska);
            lblIzmene.Text = "Nesačuvanih izmena: " + (uc.BrojIzmena + (zaglavljeIzmenjeno ? 1 : 0));
        }

        bool ImaIzmena
        {
            get { return uc.BrojIzmena > 0 || zaglavljeIzmenjeno; }
        }

        // vraca false ako korisnik odustane
        bool PitajZaIzmene(string sta)
        {
            if (!ImaIzmena) return true;
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
                if (zaglavljeIzmenjeno)
                    baza.SacuvajZaglavlje(uc.CenovnikID, numKurs.Value, numPDV.Value / 100, korisnik);
                uc.Sacuvaj(korisnik);
                zaglavljeIzmenjeno = false;
                OsveziStatus();
                return true;
            }
            catch (Exception ex) { Izgled.Greska(this, "Čuvanje", ex); return false; }
            finally { Cursor = Cursors.Default; }
        }

        /* ---------- zaglavlje ---------- */

        void cboCenovnik_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ucitavanje) return;
            if (!PitajZaIzmene("promene cenovnika")) { cboCenovnik.SelectedValue = uc.CenovnikID; return; }
            UcitajCenovnik();
        }

        void numKurs_ValueChanged(object sender, EventArgs e)
        {
            if (ucitavanje || uc.Stavke.Count == 0) return;
            zaglavljeIzmenjeno = true;
            uc.PostaviKurs(numKurs.Value);
        }

        void numPDV_ValueChanged(object sender, EventArgs e)
        {
            if (ucitavanje || uc.Stavke.Count == 0) return;
            zaglavljeIzmenjeno = true;
            uc.PostaviPDV(numPDV.Value / 100);
        }

        void chkEUR_CheckedChanged(object sender, EventArgs e)
        {
            uc.PrikaziEUR(chkEUR.Checked);
        }

        /* ---------- dugmad ---------- */

        void btnSacuvaj_Click(object sender, EventArgs e)
        {
            if (Sacuvaj())
                MessageBox.Show(this, "Izmene su sačuvane.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void btnOdustani_Click(object sender, EventArgs e)
        {
            if (ImaIzmena && MessageBox.Show(this, "Odbaciti sve nesačuvane izmene?", Text,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            UcitajCenovnik();
        }

        void btnAzurirajTroskove_Click(object sender, EventArgs e)
        {
            if (!PitajZaIzmene("ažuriranja troškova")) return;
            try
            {
                Cursor = Cursors.WaitCursor;
                var bez = baza.AzurirajTroskove(uc.CenovnikID);
                Cursor = Cursors.Default;
                UcitajCenovnik();
                if (bez.Rows.Count > 0)
                    Izgled.PrikaziTabele(this, "Redovi bez važećeg troška (zadržali su staru vrednost)", "Bez troška", bez);
                else
                    MessageBox.Show(this, "TP-Novi je izračunat za sve redove.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { Cursor = Cursors.Default; Izgled.Greska(this, "Ažuriranje troškova", ex); }
        }

        void btnProglasiTroskove_Click(object sender, EventArgs e)
        {
            // samo oznaceni redovi
            var oznaceni = uc.Oznaceni.Select(s => s.StavkaID).ToList();
            if (oznaceni.Count == 0)
            {
                MessageBox.Show(this, "Označite redove za koje se troškovi proglašavaju novim (Ctrl / Shift + klik).",
                                "Proglasi troškove novim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!PitajZaIzmene("proglašenja troškova")) return;
            try
            {
                int broj = baza.BrojZaProglasenjeTroskova(uc.CenovnikID, oznaceni, korisnik);
                if (broj == 0)
                {
                    MessageBox.Show(this, "Među " + oznaceni.Count + " označenih redova nema nijednog u kome se TP-Novi razlikuje od TP-Stari.",
                                    "Proglasi troškove novim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show(this,
                        "Označeno redova: " + oznaceni.Count + ", od toga se menja: " + broj + ".\n\n" +
                        "U tim redovima TP-Novi postaje TP-Stari, a trenutna marža postaje marža stara.\n" +
                        "Poređenje sa starim troškovima se za njih gubi. Ostali redovi se ne menjaju.\n\nNastaviti?",
                        "Proglasi troškove novim", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
                int promenjeno = baza.ProglasiTroskoveNovim(uc.CenovnikID, oznaceni, korisnik);
                UcitajCenovnik();
                MessageBox.Show(this, "Troškovi su proglašeni novim u " + promenjeno + " redova.",
                                "Proglasi troškove novim", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { Izgled.Greska(this, "Proglasi troškove novim", ex); }
        }

        void btnPredlog_Click(object sender, EventArgs e)
        {
            if (uc.Oznaceni.Count == 0)
            {
                MessageBox.Show(this, "Označite redove u gridu (Ctrl / Shift + klik).", "Primeni predlog");
                return;
            }
            int broj = uc.PrimeniPredlog(cboPredlog.SelectedIndex == 0);
            MessageBox.Show(this, "Predlog je primenjen na " + broj + " Code. Izmene još nisu sačuvane.", "Primeni predlog");
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

        void btnPoveziSlike_Click(object sender, EventArgs e)
        {
            string koren = ucCenovnik.KorenSlika;
            if (!Directory.Exists(koren))
            {
                MessageBox.Show(this, "Folder sa slikama ne postoji:\n" + koren + "\n\nPodešava se u App.config (IC_KorenSlika).", "Poveži slike");
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                var slike = SlikeFolder.Skeniraj(koren);
                var ds = baza.PoveziSlikeAutomatski(slike, true);
                Cursor = Cursors.Default;
                var br = ds.Tables[1].Rows[0];
                MessageBox.Show(this, "Nađeno slika: " + slike.Count + "\nNovo povezanih: " + br["Dodato"] +
                                "\nUklonjenih (fajl ne postoji): " + br["Obrisano"] +
                                "\nSlika bez Code u bazi: " + ds.Tables[0].Rows.Count, "Poveži slike");
                if (ds.Tables[0].Rows.Count > 0)
                    Izgled.PrikaziTabele(this, "Slike čiji Code ne postoji u bazi", "Slika bez Code", ds.Tables[0]);
            }
            catch (Exception ex) { Cursor = Cursors.Default; Izgled.Greska(this, "Poveži slike", ex); }
        }

        void btnIzvestajSlika_Click(object sender, EventArgs e)
        {
            try
            {
                var ds = baza.IzvestajSlika();
                Izgled.PrikaziTabele(this, "Izveštaj slika", "Code bez slike", ds.Tables[0], "Više slika bez glavne", ds.Tables[1]);
            }
            catch (Exception ex) { Izgled.Greska(this, "Izveštaj slika", ex); }
        }

        void frmOpstiCenovnik_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Pravo && !PitajZaIzmene("zatvaranja")) e.Cancel = true;
        }
    }
}
