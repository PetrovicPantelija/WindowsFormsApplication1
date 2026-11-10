using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;
using DataRow = System.Data.DataRow;
using Syncfusion.WinForms.DataGrid.Enums;

namespace WindowsFormsApplication1.Servis
{
    // ZAH-010: servisni nalog (kartice Osnovni podaci, Rad, Delovi iz magacina, Troskovi).
    // Zavrsen ili otkazan nalog je zakljucan (SR-10): sva dugmad za izmene su iskljucena, a proveru ponavlja i procedura.
    public class dlgSrvNalog : Form
    {
        readonly ServisBaza baza = new ServisBaza();
        readonly int nalogID;
        readonly bool smeUpis, smeIzmena;
        public bool Promenjeno;

        readonly Label sazetak = new Label();
        readonly TabControl tabovi = new TabControl();
        readonly SfDataGrid gridRad = new SfDataGrid(), gridDelovi = new SfDataGrid(), gridSpoljni = new SfDataGrid();
        readonly Label lblTroskovi = new Label();
        readonly PoljaPanel osnovno = new PoljaPanel();
        Label lblSredstvo, lblVrsta, lblIzvor, lblBrojilo, lblStatus;
        ComboBox cboIzvrsilac;
        TextBox txtOpis;
        Button btnSacuvaj, btnZavrsi, btnOtkazi, btnRad, btnUkloniRad, btnDeo, btnIzdaj, btnPovrat, btnStorno;
        DataRow nalog;

        public dlgSrvNalog(int id, bool smeUpis, bool smeIzmena)
        {
            nalogID = id;
            this.smeUpis = smeUpis;
            this.smeIzmena = smeIzmena;
            Text = "Servis – Servisni nalog";
            Font = JelaTema.FontTekst;
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1100, 660);
            MinimumSize = new Size(860, 520);
            ShowInTaskbar = false;
            MinimizeBox = false;

            sazetak.Dock = DockStyle.Top;
            sazetak.Height = 34;
            sazetak.Padding = new Padding(12, 9, 0, 0);
            sazetak.BackColor = Color.White;
            sazetak.Font = JelaTema.FontNaslov;

            foreach (var g in new[] { gridRad, gridDelovi, gridSpoljni })
            {
                g.Dock = DockStyle.Fill;
                g.AutoGenerateColumns = true;
                SfGridPomocnik.Stilizuj(g);
                g.AutoGeneratingColumn += SrvForma.FormatKolone;
            }
            gridDelovi.SelectionMode = GridSelectionMode.Extended;
            gridDelovi.QueryCellStyle += (s, e) =>
            {
                if (e.Column == null || e.Column.MappingName != "Status" || e.DataRow == null) return;
                var r = e.DataRow.RowData as DataRowView;
                Color? c = r == null ? null : SrvForma.StandardnaBoja(SrvFormat.Str(r["Status"]));
                if (c.HasValue) { e.Style.TextColor = c.Value; e.Style.Font.Bold = true; }
            };

            // osnovni podaci
            lblSredstvo = osnovno.Prikaz("Sredstvo", "");
            lblVrsta = osnovno.Prikaz("Vrsta naloga", "");
            lblIzvor = osnovno.Prikaz("Izvor", "");
            lblBrojilo = osnovno.Prikaz("Brojilo", "");
            lblStatus = osnovno.Prikaz("Status", "");
            cboIzvrsilac = osnovno.Izbor("Izvršilac", new[] { "Interni", "Spoljni" }, "Interni", true);
            txtOpis = osnovno.Tekst("Opis", "", false, true, 1000);
            txtOpis.Height = 90;
            btnSacuvaj = Dugme("Sačuvaj", SrvFormat.Zelena, Sacuvaj);
            btnZavrsi = Dugme("Završi nalog", SrvFormat.Zelena, Zavrsi);
            btnOtkazi = Dugme("Otkaži nalog", Color.Empty, Otkazi);
            var akcije = new FlowLayoutPanel { AutoSize = true, Margin = new Padding(0, 12, 0, 0) };
            akcije.Controls.AddRange(new Control[] { btnSacuvaj, btnZavrsi, btnOtkazi });
            var tOsnovno = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, Padding = new Padding(12), WrapContents = false, AutoScroll = true };
            tOsnovno.Controls.Add(osnovno);
            tOsnovno.Controls.Add(akcije);
            var strOsnovno = new TabPage("Osnovni podaci");
            strOsnovno.Controls.Add(tOsnovno);

            btnRad = Dugme("+ Dodaj rad", SrvFormat.Zelena, DodajRad);
            btnUkloniRad = Dugme("Ukloni rad", Color.Empty, UkloniRad);
            btnDeo = Dugme("+ Dodaj deo", SrvFormat.Zelena, DodajDeo);
            btnIzdaj = Dugme("Izdaj rezervisane delove", SrvFormat.Narandzasta, Izdaj);
            btnPovrat = Dugme("Povrat", Color.Empty, Povrat);
            btnStorno = Dugme("Storniraj stavku", Color.Empty, Storniraj);

            lblTroskovi.Dock = DockStyle.Top;
            lblTroskovi.Height = 80;
            lblTroskovi.Padding = new Padding(12, 10, 0, 0);
            lblTroskovi.Font = JelaTema.FontNaslov;

            tabovi.Dock = DockStyle.Fill;
            tabovi.TabPages.Add(strOsnovno);
            tabovi.TabPages.Add(Strana("Rad", gridRad, btnRad, btnUkloniRad));
            tabovi.TabPages.Add(Strana("Delovi iz magacina", gridDelovi, btnDeo, btnIzdaj, btnPovrat, btnStorno));
            var strTroskovi = new TabPage("Troškovi");
            strTroskovi.Controls.Add(gridSpoljni);
            strTroskovi.Controls.Add(lblTroskovi);
            tabovi.TabPages.Add(strTroskovi);

            var dole = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 46, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(8) };
            var btnZatvori = new Button { Text = "Zatvori", AutoSize = true, MinimumSize = new Size(96, 30), UseVisualStyleBackColor = true, DialogResult = DialogResult.Cancel };
            dole.Controls.Add(btnZatvori);
            CancelButton = btnZatvori;

            Controls.Add(tabovi);
            Controls.Add(sazetak);
            Controls.Add(dole);
            JelaTema.Primeni(this);
            SrvFormat.SiviIskljucena(this);
            Load += (s, e) => Ucitaj();
        }

        Button Dugme(string tekst, Color boja, Action klik)
        {
            var b = new Button { Text = tekst, AutoSize = true, MinimumSize = new Size(90, 28), Margin = new Padding(0, 0, 8, 0) };
            if (boja.IsEmpty) b.UseVisualStyleBackColor = true; else b.BackColor = boja;
            b.Click += (s, e) =>
            {
                try { Cursor = Cursors.WaitCursor; klik(); }
                catch (Exception ex) { Cursor = Cursors.Default; Poruke.Greska(this, ex); Ucitaj(); }
                finally { Cursor = Cursors.Default; }
            };
            return b;
        }

        static TabPage Strana(string naslov, Control sadrzaj, params Control[] dugmad)
        {
            var p = new TabPage(naslov);
            p.Controls.Add(sadrzaj);
            var t = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40, Padding = new Padding(4, 6, 4, 0), WrapContents = false };
            t.Controls.AddRange(dugmad);
            p.Controls.Add(t);
            return p;
        }

        bool Zakljucan { get { return nalog == null || SrvFormat.Int(nalog["Zakljucan"]) == 1; } }

        void Ucitaj()
        {
            try
            {
                var t = baza.Upit("SELECT * FROM dbo.SRV_vw_Nalog WHERE ID = @N", "@N", nalogID);
                if (t.Rows.Count == 0) return;
                nalog = t.Rows[0];
                string st = SrvFormat.Str(nalog["Status"]);
                Text = "Servis – Servisni nalog " + nalog["Broj"];
                sazetak.Text = nalog["Broj"] + "   ·   " + nalog["Sredstvo"] + " " + nalog["NazivSredstva"] + "   ·   " + st
                             + "   ·   trošak " + SrvFormat.Broj(nalog["Ukupno"]) + " RSD" + (Zakljucan ? "   ·   zaključan" : "");
                sazetak.ForeColor = SrvForma.StandardnaBoja(st) ?? JelaTema.Tekst;
                lblSredstvo.Text = nalog["Sredstvo"] + " – " + nalog["NazivSredstva"] + " (" + nalog["VrstaSredstva"] + ")";
                lblVrsta.Text = SrvFormat.Str(nalog["Vrsta"]);
                lblIzvor.Text = nalog["Kvar"] != DBNull.Value ? "Prijava kvara " + nalog["Kvar"] : nalog["PlanNaziv"] != DBNull.Value ? "Plan „" + nalog["PlanNaziv"] + "“" : "—";
                lblBrojilo.Text = nalog["BrojiloTip"] == DBNull.Value ? "—" : nalog["BrojiloTip"] + ": pri otvaranju " + SrvFormat.Broj(nalog["BrojiloOtvaranje"])
                                  + (nalog["BrojiloZatvaranje"] == DBNull.Value ? "" : ", pri zatvaranju " + SrvFormat.Broj(nalog["BrojiloZatvaranje"]));
                lblStatus.Text = st + (nalog["RazlogOtkazivanja"] != DBNull.Value ? " – " + nalog["RazlogOtkazivanja"] : "");
                cboIzvrsilac.SelectedItem = SrvFormat.Str(nalog["Izvrsilac"]);
                txtOpis.Text = SrvFormat.Str(nalog["Opis"]);

                gridRad.DataSource = baza.Upit(@"SELECT ID, Osoba, Zanimanje, Datum, Sati, CenaSata, CAST(Sati * CenaSata AS decimal(18,2)) AS Iznos, Opis
                                                FROM dbo.SRV_NalogRad WHERE NalogID = @N AND Aktivno = 1 ORDER BY Datum, ID", "@N", nalogID);
                gridDelovi.DataSource = baza.Upit(@"SELECT ID, Sifra, Naziv, JM, Kolicina, Status, Raspolozivo, Cena, Trosak, Vraceno, IzdatnicaBroj, IzNormativa
                                                   FROM dbo.SRV_vw_NalogDeo WHERE NalogID = @N ORDER BY ID", "@N", nalogID);
                gridSpoljni.DataSource = baza.Upit(@"SELECT ID, Broj, Datum, Partner, Iznos, Nivo, Status, Odobrio, Faktura, GarancijaMeseci
                                                    FROM dbo.SRV_vw_SpoljniServis WHERE NalogID = @N ORDER BY ID", "@N", nalogID);
                lblTroskovi.Text = "Rad: " + SrvFormat.Broj(nalog["Rad"]) + " RSD" + Environment.NewLine
                                 + "Izdati delovi (umanjeno za povrat): " + SrvFormat.Broj(nalog["Delovi"]) + " RSD" + Environment.NewLine
                                 + "Odobren spoljni servis: " + SrvFormat.Broj(nalog["Spoljni"]) + " RSD" + Environment.NewLine
                                 + "Ukupno: " + SrvFormat.Broj(nalog["Ukupno"]) + " RSD";

                bool menja = !Zakljucan && smeIzmena;
                cboIzvrsilac.Enabled = txtOpis.Enabled = btnSacuvaj.Enabled = menja;
                btnZavrsi.Enabled = btnOtkazi.Enabled = menja;
                btnRad.Enabled = btnUkloniRad.Enabled = !Zakljucan && (smeUpis || smeIzmena);
                btnDeo.Enabled = btnIzdaj.Enabled = btnPovrat.Enabled = btnStorno.Enabled = !Zakljucan && (smeUpis || smeIzmena);
            }
            catch (Exception ex) { Poruke.Greska(this, ex); }
        }

        void Gotovo()
        {
            Promenjeno = true;
            Ucitaj();
        }

        void Sacuvaj()
        {
            baza.Proc("dbo.SRV_NalogSacuvaj", "@ID", nalogID, "@Izvrsilac", cboIzvrsilac.SelectedItem, "@Opis", PoljaPanel.Tekst(txtOpis));
            Gotovo();
            Poruke.Info(this, "Nalog je sačuvan.");
        }

        void DodajRad()
        {
            using (var d = new SrvDijalog("Rad na nalogu " + nalog["Broj"]))
            {
                var osoba = d.Polja.Tekst("Izvršilac (osoba)", "", true, false, 100);
                var zanimanje = d.Polja.Tekst("Zanimanje", "", false, false, 50);
                var datum = d.Polja.Datum("Datum", DateTime.Today);
                datum.MaxDate = DateTime.Today;
                var sati = d.Polja.Broj("Sati", null, true);
                var cena = d.Polja.Broj("Cena po satu (RSD)", 0, false, 0);
                var opis = d.Polja.Tekst("Opis rada", "", false, true, 400);
                d.Provera = () => PoljaPanel.BrojIz(sati) <= 0 ? "Polje „Sati“ mora biti veće od 0." : null;
                d.Snimi = () =>
                {
                    baza.Proc("dbo.SRV_NalogDodajRad", "@NalogID", nalogID, "@Osoba", osoba.Text, "@Zanimanje", PoljaPanel.Tekst(zanimanje),
                              "@Datum", datum.Value.Date, "@Sati", PoljaPanel.BrojIz(sati), "@CenaSata", PoljaPanel.BrojIz(cena) ?? 0, "@Opis", PoljaPanel.Tekst(opis));
                    return true;
                };
                if (d.ShowDialog(this) == DialogResult.OK) Gotovo();
            }
        }

        void UkloniRad()
        {
            var r = gridRad.SelectedItem as DataRowView;
            if (r == null) { Poruke.Info(this, "Izaberite rad."); return; }
            if (!Poruke.Potvrda(this, "Ukloniti rad „" + r["Osoba"] + "“ (" + SrvFormat.Broj(r["Sati"]) + " h)?")) return;
            baza.Proc("dbo.SRV_NalogUkloniRad", "@ID", r["ID"]);
            Gotovo();
        }

        void DodajDeo()
        {
            var delovi = baza.Upit(@"SELECT d.ID, d.Sifra + N' – ' + d.Naziv + N' (raspoloživo ' + CAST(CAST(z.Raspolozivo AS decimal(18,2)) AS nvarchar(30)) + N' ' + d.JM + N')' AS Naziv
                                     FROM dbo.SRV_Deo d JOIN dbo.SRV_vw_Zalihe z ON z.DeoID = d.ID WHERE d.Aktivno = 1 ORDER BY d.Sifra");
            using (var d = new SrvDijalog("Deo na nalogu " + nalog["Broj"], "Dodaj"))
            {
                var deo = d.Polja.Lista("Deo", delovi, null, true, true);
                var kol = d.Polja.Broj("Količina", null, true);
                d.Provera = () => PoljaPanel.BrojIz(kol) <= 0 ? "Polje „Količina“ mora biti veće od 0." : null;
                string status = null;
                d.Snimi = () =>
                {
                    status = SrvFormat.Str(baza.ProcRed("dbo.SRV_NalogDodajDeo", "@NalogID", nalogID, "@DeoID", PoljaPanel.Vrednost(deo),
                                                        "@Kolicina", PoljaPanel.BrojIz(kol))["Status"]);
                    return true;
                };
                if (d.ShowDialog(this) != DialogResult.OK) return;
                Gotovo();
                if (status == "Nema na stanju")
                    Poruke.Info(this, "Nema dovoljno na stanju: stavka je „Nema na stanju“, nalog čeka delove, a deo ide u predlog nabavke.");
            }
        }

        // MD-R02: izdatnica za izabrane stavke (bez izbora: sve rezervisane)
        void Izdaj()
        {
            var izabrani = gridDelovi.SelectedItems.OfType<DataRowView>()
                .Where(r => SrvFormat.Str(r["Status"]) == "Rezervisano" || SrvFormat.Str(r["Status"]) == "Nema na stanju").ToList();
            var svi = gridDelovi.DataSource as DataTable;
            var zaIzdavanje = izabrani.Count > 0 ? izabrani.Select(r => r.Row).ToList()
                            : svi.AsEnumerable().Where(r => SrvFormat.Str(r["Status"]) == "Rezervisano").ToList();
            if (zaIzdavanje.Count == 0) { Poruke.Info(this, "Nema rezervisanih stavki za izdavanje."); return; }
            bool cenaNula = zaIzdavanje.Any(r => SrvFormat.Dec(r["Cena"]) == 0);
            if (cenaNula && !Poruke.Potvrda(this, "Neki delovi imaju cenu 0, pa trošak naloga neće biti tačan. Izdati ipak?")) return;
            if (!Poruke.Potvrda(this, "Izdati " + zaIzdavanje.Count + " stavki sa naloga " + nalog["Broj"] + "?")) return;
            var ids = izabrani.Count > 0 ? ServisBaza.Idovi(izabrani.Select(r => SrvFormat.Int(r["ID"]))) : ServisBaza.Idovi(new int[0]);
            string broj = SrvFormat.Str(baza.ProcRed("dbo.SRV_NalogIzdaj", "@NalogID", nalogID, ServisBaza.Tvp("@IDs", "dbo.SRV_IdTip", ids),
                                                     "@DozvoliCenuNula", cenaNula)["Broj"]);
            Gotovo();
            Poruke.Info(this, "Izdatnica je proknjižena: " + broj + ".");
        }

        // MD-R04: povrat najvise izdato - vec vraceno, po ceni sa izdatnice
        void Povrat()
        {
            var r = gridDelovi.SelectedItem as DataRowView;
            if (r == null || SrvFormat.Str(r["Status"]) != "Izdato") { Poruke.Info(this, "Izaberite izdatu stavku."); return; }
            decimal max = SrvFormat.Dec(r["Kolicina"]) - SrvFormat.Dec(r["Vraceno"]);
            if (max <= 0) { Poruke.Info(this, "Za ovu stavku je već vraćena cela izdata količina."); return; }
            using (var d = new SrvDijalog("Povrat dela " + r["Sifra"]))
            {
                d.Polja.Prikaz("Deo", r["Sifra"] + " " + r["Naziv"]);
                d.Polja.Prikaz("Može da se vrati najviše", SrvFormat.Broj(max) + " " + r["JM"]);
                var kol = d.Polja.Broj("Količina", null, true);
                var napomena = d.Polja.Tekst("Napomena", "", false, false, 400);
                d.Provera = () =>
                {
                    decimal? k = PoljaPanel.BrojIz(kol);
                    return k <= 0 || k > max ? "Količina mora biti veća od 0 i najviše " + SrvFormat.Broj(max) + "." : null;
                };
                string broj = null;
                d.Snimi = () =>
                {
                    broj = SrvFormat.Str(baza.ProcRed("dbo.SRV_NalogPovrat", "@NalogDeoID", r["ID"], "@Kolicina", PoljaPanel.BrojIz(kol),
                                                      "@Napomena", PoljaPanel.Tekst(napomena))["Broj"]);
                    return true;
                };
                if (d.ShowDialog(this) != DialogResult.OK) return;
                Gotovo();
                Poruke.Info(this, "Povrat je proknjižen: " + broj + ".");
            }
        }

        void Storniraj()
        {
            var r = gridDelovi.SelectedItem as DataRowView;
            if (r == null) { Poruke.Info(this, "Izaberite stavku."); return; }
            string st = SrvFormat.Str(r["Status"]);
            if (st == "Izdato") { Poruke.Info(this, "Izdata stavka se ne stornira; uradite povrat."); return; }
            if (st == "Storno") { Poruke.Info(this, "Stavka je već stornirana."); return; }
            if (!Poruke.Potvrda(this, "Stornirati stavku " + r["Sifra"] + " (" + SrvFormat.Broj(r["Kolicina"]) + " " + r["JM"] + ")?")) return;
            baza.Proc("dbo.SRV_NalogStornirajStavku", "@NalogDeoID", r["ID"]);
            Gotovo();
        }

        // SR-07, SR-08
        void Zavrsi()
        {
            var stavke = gridDelovi.DataSource as DataTable;
            bool rezervisano = stavke.AsEnumerable().Any(r => SrvFormat.Str(r["Status"]) == "Rezervisano");
            bool nema = stavke.AsEnumerable().Any(r => SrvFormat.Str(r["Status"]) == "Nema na stanju");
            string akcija = null;
            bool cenaNula = false;
            if (rezervisano || nema)
            {
                int izbor = Poruke.Izbor(this, "Nalog ima rezervisane a neizdate delove. Šta uraditi sa njima?"
                                               + (nema ? Environment.NewLine + "Stavke „Nema na stanju“ se storniraju u oba slučaja." : ""),
                                         "Izdaj rezervisane", "Storniraj rezervacije", "Odustani");
                if (izbor < 0 || izbor == 2) return;
                akcija = izbor == 0 ? "Izdaj" : "Storniraj";
                cenaNula = akcija == "Izdaj" && stavke.AsEnumerable().Any(r => SrvFormat.Str(r["Status"]) == "Rezervisano" && SrvFormat.Dec(r["Cena"]) == 0);
                if (cenaNula && !Poruke.Potvrda(this, "Neki rezervisani delovi imaju cenu 0, pa trošak naloga neće biti tačan. Izdati ipak?")) return;
            }
            object trenutno = baza.Vrednost(@"SELECT bt.Vrednost FROM dbo.SRV_Nalog n
                                              JOIN dbo.SRV_vw_BrojiloTrenutno bt ON bt.SredstvoID = n.SredstvoID AND bt.Tip = n.BrojiloTip WHERE n.ID = @N", "@N", nalogID);
            bool imaBrojilo = nalog["BrojiloTip"] != DBNull.Value;
            using (var d = new SrvDijalog("Završetak naloga " + nalog["Broj"], "Završi nalog"))
            {
                TextBox brojilo = null;
                if (imaBrojilo)
                {
                    d.Polja.Prikaz("Brojilo pri otvaranju", SrvFormat.Broj(nalog["BrojiloOtvaranje"]) + " " + nalog["BrojiloTip"]);
                    brojilo = d.Polja.Broj("Brojilo pri zatvaranju", SrvFormat.DecN(trenutno), true, 0);
                }
                else d.Polja.Prikaz("Brojilo", "vrsta sredstva nema brojilo");
                d.Polja.Prikaz("Napomena", "Posle završetka nalog je zaključan.");
                d.Provera = () =>
                {
                    if (brojilo != null && nalog["BrojiloOtvaranje"] != DBNull.Value && PoljaPanel.BrojIz(brojilo) < SrvFormat.Dec(nalog["BrojiloOtvaranje"]))
                        return "Brojilo pri zatvaranju ne sme biti manje od brojila pri otvaranju (" + SrvFormat.Broj(nalog["BrojiloOtvaranje"]) + ").";
                    return null;
                };
                d.Snimi = () =>
                {
                    baza.Proc("dbo.SRV_NalogZavrsi", "@ID", nalogID, "@BrojiloZatvaranje", brojilo == null ? null : PoljaPanel.BrojIz(brojilo),
                              "@NeizdatoAkcija", akcija, "@DozvoliCenuNula", cenaNula);
                    return true;
                };
                if (d.ShowDialog(this) != DialogResult.OK) return;
            }
            Gotovo();
            Poruke.Info(this, "Nalog " + nalog["Broj"] + " je završen.");
        }

        // SR-09
        void Otkazi()
        {
            var stavke = gridDelovi.DataSource as DataTable;
            var izdato = stavke.AsEnumerable().FirstOrDefault(r => SrvFormat.Str(r["Status"]) == "Izdato" && SrvFormat.Dec(r["Kolicina"]) > SrvFormat.Dec(r["Vraceno"]));
            if (izdato != null)
            {
                Poruke.Upozorenje(this, "Na nalogu ima izdatih delova (npr. " + izdato["Sifra"] + "). Pre otkazivanja uradite povrat (Delovi iz magacina › Povrat).");
                tabovi.SelectedIndex = 2;
                return;
            }
            using (var d = new SrvDijalog("Otkazivanje naloga " + nalog["Broj"], "Otkaži nalog"))
            {
                var razlog = d.Polja.Tekst("Razlog otkazivanja", "", true, true, 400);
                d.Polja.Prikaz("Napomena", "Rezervacije se oslobađaju; nalog postaje zaključan.");
                d.Snimi = () => { baza.Proc("dbo.SRV_NalogOtkazi", "@ID", nalogID, "@Razlog", razlog.Text); return true; };
                if (d.ShowDialog(this) != DialogResult.OK) return;
            }
            Gotovo();
        }
    }
}
