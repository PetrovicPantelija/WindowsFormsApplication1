using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Servis
{
    public enum VrstaStavki { PocetnoStanje, Prijem, Narudzbenica }

    // ZAH-010: dokument sa stavkama delova: pocetno stanje (MD-R13), prijem (MD-R05, MD-R05a) i narudzbenica.
    // Pomocni dijalog (DataGridView za unos stavki); pravo nasledjuje od ekrana iz kog se otvara.
    public class dlgSrvStavke : Form
    {
        readonly ServisBaza baza = new ServisBaza();
        readonly VrstaStavki vrsta;
        readonly int? narudzbenicaID;           // izmena narudzbenice (Nacrt)
        readonly PoljaPanel zaglavlje = new PoljaPanel();
        readonly DataGridView grid = new DataGridView();
        readonly DataTable stavke = new DataTable();
        readonly Label zbir = new Label();
        ComboBox cboNarudzbenica, cboDobavljac;
        DateTimePicker dtDatum, dtOcekivano;
        TextBox txtRazlog, txtKomentar, txtNapomena;
        public string Broj;

        public dlgSrvStavke(VrstaStavki vrsta, int? narudzbenicaID = null)
        {
            this.vrsta = vrsta;
            this.narudzbenicaID = narudzbenicaID;
            Text = vrsta == VrstaStavki.PocetnoStanje ? "Servis – Početno stanje delova"
                 : vrsta == VrstaStavki.Prijem ? "Servis – Novi prijem delova"
                 : narudzbenicaID.HasValue ? "Servis – Izmena narudžbenice" : "Servis – Nova narudžbenica";
            Font = JelaTema.FontTekst;
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(920, 640);
            MinimumSize = new Size(760, 520);
            ShowInTaskbar = false;
            MinimizeBox = false;

            stavke.Columns.Add("DeoID", typeof(int));
            stavke.Columns.Add("Sifra", typeof(string));
            stavke.Columns.Add("Deo", typeof(string));
            stavke.Columns.Add("JM", typeof(string));
            stavke.Columns.Add("Preostalo", typeof(decimal));
            stavke.Columns.Add("Kolicina", typeof(decimal));
            stavke.Columns.Add("Cena", typeof(decimal));
            stavke.Columns.Add("Vrednost", typeof(decimal), "ISNULL(Kolicina, 0) * ISNULL(Cena, 0)");

            NapraviZaglavlje();
            NapraviGrid();

            var trakaStavki = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40, Padding = new Padding(0, 6, 0, 0) };
            var btnDodaj = new Button { Text = "Dodaj deo", BackColor = SrvFormat.Zelena };
            btnDodaj.Click += (s, e) => DodajDeo();
            var btnUkloni = new Button { Text = "Ukloni stavku", UseVisualStyleBackColor = true };
            btnUkloni.Click += (s, e) => { if (grid.CurrentRow != null && !grid.CurrentRow.IsNewRow) { stavke.Rows.RemoveAt(grid.CurrentRow.Index); Zbir(); } };
            foreach (var b in new[] { btnDodaj, btnUkloni }) { b.AutoSize = true; b.MinimumSize = new Size(96, 28); trakaStavki.Controls.Add(b); }

            var gore = new Panel { Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(12, 8, 12, 0) };
            gore.Controls.Add(zaglavlje);
            var sredina = new Panel { Dock = DockStyle.Fill, Padding = new Padding(12, 0, 12, 0) };
            sredina.Controls.Add(grid);
            sredina.Controls.Add(trakaStavki);

            var dole = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 48, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(8) };
            var btnOdustani = new Button { Text = "Odustani", AutoSize = true, MinimumSize = new Size(96, 30), UseVisualStyleBackColor = true, DialogResult = DialogResult.Cancel };
            var btnGlavno = new Button
            {
                Text = vrsta == VrstaStavki.PocetnoStanje ? "Proknjiži početno stanje" : vrsta == VrstaStavki.Prijem ? "Proknjiži prijem" : "Sačuvaj",
                AutoSize = true, MinimumSize = new Size(96, 30), BackColor = SrvFormat.Zelena
            };
            btnGlavno.Click += (s, e) => Snimi();
            zbir.AutoSize = true;
            zbir.Margin = new Padding(0, 8, 24, 0);
            dole.Controls.Add(btnOdustani);
            dole.Controls.Add(btnGlavno);
            dole.Controls.Add(zbir);
            CancelButton = btnOdustani;

            Controls.Add(sredina);
            Controls.Add(gore);
            Controls.Add(dole);
            JelaTema.Primeni(this);
            SrvFormat.SiviIskljucena(this);
            Load += (s, e) => UcitajPostojecu();
        }

        void NapraviZaglavlje()
        {
            if (vrsta == VrstaStavki.Prijem)
            {
                var nar = baza.Upit(@"SELECT ID, Broj + N' – ' + ISNULL(Dobavljac, N'') + N' (' + Status + N')' AS Naziv
                                      FROM dbo.SRV_vw_Narudzbenica WHERE Status IN (N'Poručeno', N'Delimično isporučeno') ORDER BY Broj");
                cboNarudzbenica = zaglavlje.Lista("Narudžbenica", nar, null, false, false, "— bez narudžbenice —");
                cboNarudzbenica.SelectedIndexChanged += (s, e) => NarudzbenicaIzabrana();
                cboDobavljac = zaglavlje.Lista("Dobavljač (bez narudžbenice)", baza.Partneri(), null, false, true, "— nije naveden —");
                dtDatum = zaglavlje.Datum("Datum prijema", DateTime.Today);
                dtDatum.MaxDate = DateTime.Today;
                txtRazlog = zaglavlje.Tekst("Razlog prijema bez narudžbenice", "", false, false, 400);
                txtKomentar = zaglavlje.Tekst("Komentar (prijem preko naručenog)", "", false, false, 400);
                txtNapomena = zaglavlje.Tekst("Napomena", "", false, false, 400);
            }
            else if (vrsta == VrstaStavki.PocetnoStanje)
            {
                dtDatum = zaglavlje.Datum("Datum", DateTime.Today);
                dtDatum.MaxDate = DateTime.Today;
                txtNapomena = zaglavlje.Tekst("Napomena", "Početno stanje iz popisa", false, false, 400);
            }
            else
            {
                cboDobavljac = zaglavlje.Lista("Dobavljač", baza.Partneri(), null, true, true);
                dtDatum = zaglavlje.Datum("Datum", DateTime.Today);
                dtOcekivano = zaglavlje.Datum("Očekivani datum isporuke", null, false);
                txtNapomena = zaglavlje.Tekst("Napomena", "", false, false, 400);
            }
        }

        void NapraviGrid()
        {
            grid.Dock = DockStyle.Fill;
            grid.AutoGenerateColumns = false;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.EditMode = DataGridViewEditMode.EditOnEnter;
            Kolona("Sifra", "Šifra", 100, true, null);
            Kolona("Deo", "Deo", 260, true, null);
            Kolona("JM", "JM", 50, true, null);
            var preostalo = Kolona("Preostalo", "Preostalo po narudžbenici", 120, true, "N2");
            preostalo.Visible = vrsta == VrstaStavki.Prijem;
            Kolona("Kolicina", vrsta == VrstaStavki.Prijem ? "Prima se" : "Količina", 110, false, "N2");
            Kolona("Cena", "Cena RSD", 110, false, "N2");
            Kolona("Vrednost", "Vrednost RSD", 120, true, "N2");
            grid.DataSource = stavke;
            grid.CellParsing += (s, e) =>
            {
                decimal x;
                if (e.Value is string && SrvFormat.ProbajBroj((string)e.Value, out x)) { e.Value = x; e.ParsingApplied = true; }
                else if (e.Value is string && ((string)e.Value).Trim() == "") { e.Value = DBNull.Value; e.ParsingApplied = true; }
            };
            grid.DataError += (s, e) => { Poruke.Upozorenje(this, "Unesite broj (npr. 12,5)."); e.ThrowException = false; };
            grid.CellEndEdit += (s, e) => Zbir();
        }

        DataGridViewTextBoxColumn Kolona(string ime, string naslov, int sirina, bool samoCitanje, string format)
        {
            var c = new DataGridViewTextBoxColumn { DataPropertyName = ime, Name = ime, HeaderText = naslov, Width = sirina, ReadOnly = samoCitanje };
            if (format != null)
            {
                c.DefaultCellStyle.Format = format;
                c.DefaultCellStyle.FormatProvider = SrvFormat.Kultura;
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (samoCitanje) c.DefaultCellStyle.BackColor = Color.FromArgb(247, 246, 242);
            grid.Columns.Add(c);
            return c;
        }

        void Zbir()
        {
            decimal v = stavke.AsEnumerable().Sum(r => r.IsNull("Vrednost") ? 0 : (decimal)r["Vrednost"]);
            zbir.Text = stavke.Rows.Count + " stavki · vrednost " + SrvFormat.Broj(v) + " RSD"
                      + (vrsta == VrstaStavki.Narudzbenica ? " · nivo odobrenja " + NivoTekst(v) : "");
        }

        string NivoTekst(decimal iznos)
        {
            object n = baza.Vrednost("SELECT dbo.SRV_NivoOdobrenja(@I)", "@I", iznos);
            return SrvFormat.Str(n);
        }

        void UcitajPostojecu()
        {
            try
            {
                if (vrsta != VrstaStavki.Narudzbenica || !narudzbenicaID.HasValue) { Zbir(); return; }
                var z = baza.Upit("SELECT PaSifra, Datum, OcekivaniDatum, Napomena, Status FROM dbo.SRV_Narudzbenica WHERE ID = @N", "@N", narudzbenicaID);
                if (z.Rows.Count == 0) return;
                cboDobavljac.SelectedValue = z.Rows[0]["PaSifra"];
                dtDatum.Value = (DateTime)z.Rows[0]["Datum"];
                if (z.Rows[0]["OcekivaniDatum"] != DBNull.Value) { dtOcekivano.Value = (DateTime)z.Rows[0]["OcekivaniDatum"]; dtOcekivano.Checked = true; }
                txtNapomena.Text = SrvFormat.Str(z.Rows[0]["Napomena"]);
                foreach (DataRow r in baza.Upit(@"SELECT s.DeoID, d.Sifra, d.Naziv, d.JM, s.Kolicina, s.Cena FROM dbo.SRV_NarudzbenicaStavka s
                                                   JOIN dbo.SRV_Deo d ON d.ID = s.DeoID WHERE s.NarudzbenicaID = @N ORDER BY d.Sifra", "@N", narudzbenicaID).Rows)
                    stavke.Rows.Add(r["DeoID"], r["Sifra"], r["Naziv"], r["JM"], DBNull.Value, r["Kolicina"], r["Cena"]);
                Zbir();
            }
            catch (Exception ex) { Poruke.Greska(this, ex); }
        }

        void NarudzbenicaIzabrana()
        {
            try
            {
                var n = PoljaPanel.Vrednost(cboNarudzbenica);
                cboDobavljac.Enabled = txtRazlog.Enabled = n == null;
                stavke.Rows.Clear();
                if (n != null)
                {
                    foreach (DataRow r in baza.Upit(@"SELECT s.DeoID, d.Sifra, d.Naziv, d.JM, s.Kolicina - s.PrimljenoKolicina AS Preostalo, s.Cena
                                                       FROM dbo.SRV_NarudzbenicaStavka s JOIN dbo.SRV_Deo d ON d.ID = s.DeoID
                                                       WHERE s.NarudzbenicaID = @N AND s.Kolicina > s.PrimljenoKolicina ORDER BY d.Sifra", "@N", n).Rows)
                        stavke.Rows.Add(r["DeoID"], r["Sifra"], r["Naziv"], r["JM"], r["Preostalo"], r["Preostalo"], r["Cena"]);
                }
                Zbir();
            }
            catch (Exception ex) { Poruke.Greska(this, ex); }
        }

        void DodajDeo()
        {
            var delovi = baza.Delovi();
            using (var d = new SrvDijalog("Dodaj deo", "Dodaj"))
            {
                var deo = d.Polja.Lista("Deo", delovi, null, true, true);
                var kol = d.Polja.Broj(vrsta == VrstaStavki.Prijem ? "Prima se" : "Količina", null, true);
                var cena = d.Polja.Broj("Cena RSD", null, false, 0);
                deo.SelectedIndexChanged += (s, e) =>
                {
                    var v = PoljaPanel.Vrednost(deo);
                    if (v == null) return;
                    var r = delovi.Select("ID = " + SrvFormat.Int(v)).FirstOrDefault();
                    if (r != null && cena.Text.Trim() == "") cena.Text = SrvFormat.Broj(r["ProsecnaCena"]);
                };
                d.Provera = () =>
                {
                    if (PoljaPanel.BrojIz(kol) <= 0) return "Polje „Količina“ mora biti veće od 0.";
                    int id = SrvFormat.Int(PoljaPanel.Vrednost(deo));
                    if (stavke.Select("DeoID = " + id).Length > 0) return "Deo je već u listi; promenite količinu u tabeli.";
                    return null;
                };
                if (d.ShowDialog(this) != DialogResult.OK) return;
                var izabran = delovi.Select("ID = " + SrvFormat.Int(PoljaPanel.Vrednost(deo))).First();
                decimal? c = PoljaPanel.BrojIz(cena);
                stavke.Rows.Add(izabran["ID"], izabran["Sifra"], izabran["NazivDela"], izabran["JM"], DBNull.Value, PoljaPanel.BrojIz(kol),
                                c.HasValue ? (object)c.Value : izabran["ProsecnaCena"]);
                Zbir();
            }
        }

        void Snimi()
        {
            grid.EndEdit();
            ValidateChildren();
            string greska = zaglavlje.Proveri();
            if (greska == null && stavke.Rows.Count == 0) greska = "Dodajte bar jednu stavku.";
            if (greska == null)
            {
                var r = stavke.AsEnumerable().FirstOrDefault(x => x.IsNull("Kolicina") || (decimal)x["Kolicina"] <= 0);
                if (r != null) greska = "Polje „Količina“ mora biti veće od 0 (deo " + r["Sifra"] + ").";
            }
            if (greska == null)
            {
                var r = stavke.AsEnumerable().FirstOrDefault(x => !x.IsNull("Cena") && (decimal)x["Cena"] < 0);
                if (r != null) greska = "Polje „Cena“ ne sme biti negativno (deo " + r["Sifra"] + ").";
            }
            if (greska == null && vrsta == VrstaStavki.Prijem && PoljaPanel.Vrednost(cboNarudzbenica) == null && txtRazlog.Text.Trim() == "")
                greska = "Unesite polje „Razlog prijema bez narudžbenice“.";
            if (greska == null && vrsta == VrstaStavki.Prijem && PoljaPanel.Vrednost(cboNarudzbenica) != null && txtKomentar.Text.Trim() == "")
            {
                var r = stavke.AsEnumerable().FirstOrDefault(x => !x.IsNull("Preostalo") && (decimal)x["Kolicina"] > (decimal)x["Preostalo"]);
                if (r != null) greska = "Količina je veća od preostale po narudžbenici (" + SrvFormat.Broj(r["Preostalo"]) + "). Unesite komentar.";
            }
            if (greska != null) { Poruke.Upozorenje(this, greska); return; }

            // MD-R13 / 14.9: cena 0 ne prolazi tiho
            if (vrsta != VrstaStavki.Narudzbenica && stavke.AsEnumerable().Any(x => x.IsNull("Cena") || (decimal)x["Cena"] == 0)
                && baza.Podesavanje("Servis.Delovi.CenaNulaDozvoljena") != "Da"
                && !Poruke.Potvrda(this, "Cena je 0, vrednost zaliha neće biti tačna. Nastaviti?")) return;

            var tvp = ServisBaza.Stavke();
            foreach (DataRow r in stavke.Rows) tvp.Rows.Add(r["DeoID"], r["Kolicina"], r.IsNull("Cena") ? (object)0m : r["Cena"], null);
            var p = ServisBaza.Tvp("@Stavke", "dbo.SRV_StavkaTip", tvp);
            try
            {
                Cursor = Cursors.WaitCursor;
                if (vrsta == VrstaStavki.PocetnoStanje)
                {
                    Broj = SrvFormat.Str(baza.ProcRed("dbo.SRV_PocetnoStanje", p, "@Datum", dtDatum.Value.Date, "@Napomena", PoljaPanel.Tekst(txtNapomena))["Broj"]);
                    Poruke.Info(this, "Početno stanje je proknjiženo: " + Broj + ".");
                }
                else if (vrsta == VrstaStavki.Prijem)
                {
                    Broj = SrvFormat.Str(baza.ProcRed("dbo.SRV_Prijem", p, "@NarudzbenicaID", PoljaPanel.Vrednost(cboNarudzbenica),
                                                      "@PaSifra", PoljaPanel.Vrednost(cboDobavljac), "@Datum", dtDatum.Value.Date,
                                                      "@Razlog", PoljaPanel.Tekst(txtRazlog), "@Komentar", PoljaPanel.Tekst(txtKomentar),
                                                      "@Napomena", PoljaPanel.Tekst(txtNapomena))["Broj"]);
                    Poruke.Info(this, "Prijem je proknjižen: " + Broj + ".");
                }
                else
                {
                    var r = baza.ProcRed("dbo.SRV_SacuvajNarudzbenicu", p, "@ID", narudzbenicaID, "@PaSifra", PoljaPanel.Vrednost(cboDobavljac),
                                         "@Datum", dtDatum.Value.Date, "@OcekivaniDatum", PoljaPanel.DatumIz(dtOcekivano), "@Napomena", PoljaPanel.Tekst(txtNapomena));
                    Broj = SrvFormat.Str(r["Broj"]);
                }
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex) { Poruke.Greska(this, ex); }
            finally { Cursor = Cursors.Default; }
        }
    }
}
