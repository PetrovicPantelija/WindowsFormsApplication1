using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ClosedXML.Excel;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;
using WindowsFormsApplication1.Kalkulacije.IzracunCena;
using DataRow = System.Data.DataRow;

namespace WindowsFormsApplication1.Servis
{
    public enum Uloga { Glavna, Paznja, Sporedna }

    // ZAH-010: osnova za ekrane modula Servis.
    // - prava: PravaForme(code), Code = ime klase; bez prava forma se ne otvara (Pravo = false, Load je zatvara);
    // - izgled: Jela tema "G - Svetli minimal", traka (pretraga, filteri, Osvezi, Excel, desno glavno dugme), statusna traka;
    // - grid: SfDataGrid sa lokalnim formatom brojeva/datuma, naslovi kolona na srpskom, kolone *ID sakrivene.
    public class SrvForma : Form
    {
        public bool Pravo;
        protected readonly PravaForme prava;
        protected readonly ServisBaza baza = new ServisBaza();
        protected readonly FlowLayoutPanel traka = new FlowLayoutPanel();
        protected readonly FlowLayoutPanel trakaDesno = new FlowLayoutPanel();
        protected readonly Panel gore = new Panel();
        protected readonly Label status = new Label();
        protected readonly ToolTip savet = new ToolTip();
        string pomoc = "";

        public bool Upis { get { return prava != null && prava.Upis; } }
        public bool Izmena { get { return prava != null && prava.Izmena; } }
        public bool Brisanje { get { return prava != null && prava.Brisanje; } }

        protected SrvForma(string code, string ekran)
        {
            Text = "Servis – " + ekran;
            Font = JelaTema.FontTekst;
            StartPosition = FormStartPosition.CenterScreen;
            var radna = Screen.PrimaryScreen.WorkingArea;
            Size = new Size(Math.Min(1280, radna.Width - 40), Math.Min(720, radna.Height - 40));
            MinimumSize = new Size(900, 520);
            Icon = Properties.Resources.JelaIkona;

            try
            {
                prava = new PravaForme(code);
                Pravo = prava.Pravo;
            }
            catch (Exception ex)
            {
                Pravo = false;
                Poruke.Greska(null, ex);
            }

            gore.Dock = DockStyle.Top;
            gore.Height = 48;
            gore.Padding = new Padding(8, 10, 8, 4);
            traka.Dock = DockStyle.Fill;
            traka.WrapContents = false;
            trakaDesno.Dock = DockStyle.Right;
            trakaDesno.AutoSize = true;
            trakaDesno.FlowDirection = FlowDirection.RightToLeft;
            trakaDesno.WrapContents = false;
            gore.Controls.Add(traka);
            gore.Controls.Add(trakaDesno);

            status.Dock = DockStyle.Bottom;
            status.Height = 26;
            status.Padding = new Padding(10, 5, 0, 0);
            status.BackColor = Color.FromArgb(234, 232, 225);
            status.ForeColor = Color.FromArgb(70, 72, 76);

            Controls.Add(gore);
            Controls.Add(status);
        }

        // poziva se na kraju konstruktora izvedene forme (posle dodavanja kontrola)
        protected void Zavrsi()
        {
            // sadrzaj (Dock = Fill) mora biti ispred trake i statusa u redosledu kontrola
            gore.SendToBack();
            status.SendToBack();
            JelaTema.Primeni(this);
            SrvFormat.SiviIskljucena(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!Pravo) { Close(); return; }
            base.OnLoad(e);
        }

        /* ---------------- traka ---------------- */

        protected Button Dugme(string tekst, Uloga uloga, Action klik, bool desno = false, string opis = null)
        {
            var b = new Button { Text = tekst, AutoSize = true, MinimumSize = new Size(80, 30), Height = 30, Margin = new Padding(0, 0, 8, 0) };
            if (uloga == Uloga.Glavna) b.BackColor = SrvFormat.Zelena;
            else if (uloga == Uloga.Paznja) b.BackColor = SrvFormat.Narandzasta;
            else b.UseVisualStyleBackColor = true;
            b.Click += (s, e) => Radi(klik);
            if (opis != null) savet.SetToolTip(b, opis);
            if (desno) { b.Margin = new Padding(8, 0, 0, 0); trakaDesno.Controls.Add(b); }
            else traka.Controls.Add(b);
            return b;
        }

        // dugme za traku unutar kartice / panela (nije u glavnoj traci)
        protected Button MaloDugme(string tekst, Uloga uloga, Action klik, bool omoguceno = true)
        {
            var b = new Button { Text = tekst, AutoSize = true, MinimumSize = new Size(80, 28), Margin = new Padding(0, 0, 8, 0), Enabled = omoguceno };
            if (uloga == Uloga.Glavna) b.BackColor = SrvFormat.Zelena;
            else if (uloga == Uloga.Paznja) b.BackColor = SrvFormat.Narandzasta;
            else b.UseVisualStyleBackColor = true;
            b.Click += (s, e) => Radi(klik);
            return b;
        }

        // kartica: traka sa dugmadima gore, sadrzaj ispod
        protected static TabPage Strana(string naslov, Control sadrzaj, params Control[] dugmad)
        {
            var p = new TabPage(naslov);
            p.Controls.Add(sadrzaj);
            if (dugmad.Length > 0)
            {
                var t = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40, Padding = new Padding(4, 6, 4, 0), WrapContents = false };
                t.Controls.AddRange(dugmad);
                p.Controls.Add(t);
            }
            return p;
        }

        // panel sa naslovom i trakom iznad grida (za donji deo ekrana master/detalj)
        protected static Panel Odeljak(string naslov, Control sadrzaj, params Control[] dugmad)
        {
            var p = new Panel { Dock = DockStyle.Fill };
            p.Controls.Add(sadrzaj);
            var t = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40, Padding = new Padding(4, 6, 4, 0), WrapContents = false };
            t.Controls.Add(new Label { Text = naslov, AutoSize = true, Font = JelaTema.FontNaslov, ForeColor = JelaTema.TekstNaslova, Margin = new Padding(0, 6, 16, 0) });
            t.Controls.AddRange(dugmad);
            p.Controls.Add(t);
            return p;
        }

        protected TextBox Pretraga(Action promena, int sirina = 180)
        {
            traka.Controls.Add(new Label { Text = "Traži:", AutoSize = true, Margin = new Padding(0, 7, 4, 0) });
            var t = new TextBox { Width = sirina, Margin = new Padding(0, 3, 12, 0) };
            t.TextChanged += (s, e) => promena();
            traka.Controls.Add(t);
            return t;
        }

        protected CheckBox Filter(string tekst, bool ukljucen, Action promena)
        {
            var c = new CheckBox { Text = tekst, AutoSize = true, Checked = ukljucen, Margin = new Padding(0, 6, 12, 0) };
            c.CheckedChanged += (s, e) => Radi(promena);
            traka.Controls.Add(c);
            return c;
        }

        protected ComboBox FilterLista(string naziv, string[] opcije, Action promena, int sirina = 150)
        {
            if (naziv != null) traka.Controls.Add(new Label { Text = naziv, AutoSize = true, Margin = new Padding(0, 7, 4, 0) });
            var c = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = sirina, Margin = new Padding(0, 3, 12, 0) };
            c.Items.AddRange(opcije);
            c.SelectedIndex = 0;
            c.SelectedIndexChanged += (s, e) => Radi(promena);
            traka.Controls.Add(c);
            return c;
        }

        protected void Pomoc(string tekst) { pomoc = tekst; }

        protected void Status(string tekst)
        {
            status.Text = tekst + (pomoc != "" ? "   ·   " + pomoc : "");
        }

        // svaka radnja: kursor cekanja i poruka o gresci (nikad tiho)
        protected bool Radi(Action a)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                a();
                return true;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                Poruke.Greska(this, ex);
                return false;
            }
            finally { Cursor = Cursors.Default; }
        }

        protected bool Smem(bool pravo, string poruka)
        {
            if (!pravo) Poruke.Upozorenje(this, poruka);
            return pravo;
        }

        /* ---------------- grid ---------------- */

        static readonly Dictionary<string, string> Naslovi = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Sifra", "Šifra" }, { "JM", "JM" }, { "Rezervisano", "Rezervisano" }, { "Raspolozivo", "Raspoloživo" },
            { "MinZaliha", "Min" }, { "MaxZaliha", "Max" }, { "Naruceno", "Naručeno" }, { "Cena", "Cena RSD" },
            { "Vrednost", "Vrednost RSD" }, { "Kritican", "Kritičan" }, { "CekaNaNalozima", "Čeka na nalozima" },
            { "ProcenaIznosa", "Procena iznosa RSD" }, { "Nivo", "Nivo odobrenja" }, { "Dobavljac", "Dobavljač" },
            { "RokIsporukeDana", "Rok isporuke (dana)" }, { "UlazRSD", "Ulaz RSD" }, { "IzlazRSD", "Izlaz RSD" },
            { "Narudzbenica", "Narudžbenica" }, { "Nalog", "Servisni nalog" }, { "Stavki", "Stavki" },
            { "Napomena", "Razlog / napomena" }, { "DokumentBroj", "Dokument" }, { "Kolicina", "Količina" },
            { "Vraceno", "Vraćeno" }, { "Knjigovodstveno", "Knjigovodstveno" }, { "VrednostRazlike", "Vrednost razlike RSD" },
            { "ManjakRSD", "Manjak RSD" }, { "VisakRSD", "Višak RSD" }, { "SaRazlikom", "Sa razlikom" },
            { "StanjeSada", "Stanje sada" }, { "Trazio", "Tražio" }, { "Odlucio", "Odlučio" }, { "DatumOdluke", "Datum odluke" },
            { "Obavestenje", "Obaveštenje" }, { "RaspolozivoSada", "Raspoloživo sada" }, { "Procitao", "Pročitao" },
            { "VremeCitanja", "Pročitano" }, { "DatumOtvaranja", "Otvoren" }, { "NazivSredstva", "Naziv sredstva" },
            { "VrstaSredstva", "Vrsta sredstva" }, { "PlanNaziv", "Plan" }, { "Izvrsilac", "Izvršilac" },
            { "BrojiloTip", "Brojilo" }, { "BrojiloOtvaranje", "Brojilo otv." }, { "BrojiloZatvaranje", "Brojilo zatv." },
            { "DatumZavrsetka", "Završen" }, { "RazlogOtkazivanja", "Razlog otkazivanja" }, { "Ukupno", "Ukupno RSD" },
            { "Rad", "Rad RSD" }, { "Delovi", "Delovi RSD" }, { "Spoljni", "Spoljni RSD" }, { "Trosak", "Trošak RSD" },
            { "IzdatnicaBroj", "Izdatnica" }, { "IzNormativa", "Iz normativa" }, { "DatumPrijave", "Prijavljeno" },
            { "MozeDaRadi", "Može da radi" }, { "RazlogOdbacivanja", "Razlog odbacivanja" }, { "Priloga", "Priloga" },
            { "BrojPonude", "Broj ponude" }, { "Iznos", "Iznos RSD" }, { "DatumOdobrenja", "Odobreno" },
            { "GarancijaMeseci", "Garancija (meseci)" }, { "OcekivaniDatum", "Očekivano" }, { "Podneo", "Podneo" },
            { "Proizvodjac", "Proizvođač" }, { "SerijskiBroj", "Serijski broj" }, { "OtvorenihNaloga", "Otvorenih naloga" },
            { "DatumBrojila", "Očitano" }, { "IntervalKm", "Interval km" }, { "IntervalMH", "Interval mh" },
            { "IntervalDana", "Interval dana" }, { "IntervalMeseci", "Interval meseci" }, { "UpozorenjeKm", "Upozorenje km" },
            { "UpozorenjeMH", "Upozorenje mh" }, { "UpozorenjeDana", "Upozorenje dana" }, { "StatusPlana", "Status" },
            { "PoslednjiServis", "Poslednji servis" }, { "PoslednjiNalog", "Poslednji nalog" }, { "TrenutnoKm", "Trenutno km" },
            { "TrenutnoMH", "Trenutno mh" }, { "SledeciKm", "Sledeći na km" }, { "SledeciMH", "Sledeći na mh" },
            { "SledeciDatum", "Sledeći datum" }, { "OstaloDana", "Ostalo dana" }, { "OtvorenNalog", "Otvoren nalog" },
            { "DanaDoIsteka", "Dana do isteka" }, { "DatumIzdavanja", "Izdat" }, { "DatumIsteka", "Ističe" },
            { "Oznaka", "Oznaka" }, { "Prijavio", "Prijavio" }, { "Partner", "Partner" }, { "Datum", "Datum" },
            { "Zakljucan", "Zaključan" }, { "Kreirao", "Kreirao" }, { "Aktivno", "Aktivan" }, { "Polja", "Dodatna polja" },
            { "Dokumenata", "Dokumenata" }, { "Normativ", "Delova u normativu" }, { "Delova", "Delova" },
            { "Zanimanje", "Zanimanje" }, { "Sati", "Sati" }, { "CenaSata", "Cena po satu" }, { "Osoba", "Izvršilac" },
            { "Radnja", "Radnja" }, { "Komentar", "Komentar" }, { "Korisnik", "Korisnik" }, { "Vreme", "Vreme" },
            { "StanjePosle", "Stanje posle" }, { "Obavezno", "Obavezno" }, { "Opcije", "Opcije" }, { "Redosled", "Redosled" },
            { "Tip", "Tip" }, { "Brojila", "Brojila" }, { "Izvor", "Izvor" }, { "Lokacija", "Lokacija" },
            { "Preostalo", "Preostalo" }, { "Primljeno", "Primljeno" }, { "Prebrojano", "Prebrojano" }, { "Razlika", "Razlika" },
            { "Odobrio", "Odobrio" }, { "Grupa", "Grupa" }, { "Vrsta", "Vrsta" }, { "Naziv", "Naziv" }, { "Deo", "Deo" },
            { "Stanje", "Stanje" }, { "Status", "Status" }, { "Broj", "Broj" }, { "Razlog", "Razlog" }, { "Opis", "Opis" },
            { "Sredstvo", "Sredstvo" }, { "Hitnost", "Hitnost" }, { "Kategorija", "Kategorija" }, { "Kanal", "Kanal" },
            { "Model", "Model" }, { "Godina", "Godina" }, { "Brojilo", "Brojilo" }, { "Faktura", "Faktura" },
            { "Zahtev", "Zahtev" }, { "Korekcija", "Korekcija" }, { "Dokument", "Dokument" }, { "Kvar", "Prijava kvara" },
            { "Predlog", "Predlog" }, { "Prilog", "Prilog" }
        };

        static readonly HashSet<string> SaVremenom = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        { "Vreme", "VremeCitanja", "DatumPrijave", "DatumOtvaranja", "DatumZavrsetka", "DatumOdobrenja", "DatumOdluke" };

        static readonly HashSet<string> CelBroj = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        { "Godina", "Stavki", "Prebrojano_", "SaRazlikom", "Nivo", "OtvorenihNaloga", "Priloga", "OstaloDana", "DanaDoIsteka",
          "IntervalDana", "IntervalMeseci", "UpozorenjeDana", "RokIsporukeDana", "GarancijaMeseci", "Delova", "Polja",
          "Dokumenata", "Normativ", "Redosled" };

        public static NumberFormatInfo FormatBroja(int decimala)
        {
            var n = (NumberFormatInfo)SrvFormat.Kultura.NumberFormat.Clone();
            n.NumberDecimalDigits = decimala;
            return n;
        }

        protected SfDataGrid NoviGrid(bool visestruki = false)
        {
            var g = new SfDataGrid
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = true,
                SelectionMode = visestruki ? GridSelectionMode.Extended : GridSelectionMode.Single,
                RowHeight = 26,
                AutoSizeColumnsMode = AutoSizeColumnsMode.None
            };
            SfGridPomocnik.Stilizuj(g);
            g.AutoGeneratingColumn += FormatKolone;
            return g;
        }

        public static void FormatKolone(object sender, AutoGeneratingColumnArgs e)
        {
            string ime = e.Column.MappingName;
            string naslov;
            if (Naslovi.TryGetValue(ime, out naslov)) e.Column.HeaderText = naslov;
            if (ime == "ID" || ime.EndsWith("ID") || ime == "PaSifra" || ime == "DobavljacPaSifra" || ime == "Zakljucan" || ime == "Procitano")
                e.Column.Visible = false;
            e.Column.ShowToolTip = true;
            e.Column.ShowHeaderToolTip = true;

            var num = e.Column as GridNumericColumn;
            if (num != null)
            {
                var g = (SfDataGrid)sender;
                var t = g.DataSource as DataTable;
                Type tip = t != null && t.Columns.Contains(ime) ? t.Columns[ime].DataType : typeof(decimal);
                bool ceo = tip == typeof(int) || tip == typeof(short) || tip == typeof(byte) || tip == typeof(long) || CelBroj.Contains(ime);
                num.NumberFormatInfo = FormatBroja(ceo ? 0 : 2);
                if (ime == "Godina") num.NumberFormatInfo.NumberGroupSeparator = "";
                num.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
                e.Column.Width = Math.Max(90, TextRenderer.MeasureText(e.Column.HeaderText, JelaTema.FontNaslov).Width + 34);
            }
            var dat = e.Column as GridDateTimeColumn;
            if (dat != null)
            {
                dat.Pattern = Syncfusion.WinForms.Input.Enums.DateTimePattern.Custom;
                dat.Format = SaVremenom.Contains(ime) ? "dd.MM.yyyy. HH:mm" : "dd.MM.yyyy.";
                dat.Width = SaVremenom.Contains(ime) ? 130 : 100;
            }
            var txt = e.Column as GridTextColumn;
            if (txt != null)
            {
                if (ime == "Naziv" || ime == "Deo" || ime == "NazivSredstva") txt.Width = 220;
                else if (ime == "Opis" || ime == "Napomena" || ime == "Razlog" || ime == "Komentar") txt.Width = 260;
                else if (ime == "Obavestenje") txt.Width = 560;
                else txt.Width = Math.Max(80, Math.Min(160, TextRenderer.MeasureText(e.Column.HeaderText, JelaTema.FontNaslov).Width + 40));
            }
        }

        // status u boji palete G, uvek uz tekst statusa
        protected static void BojaStatusa(SfDataGrid g, string kolona, Func<string, Color?> boja)
        {
            g.QueryCellStyle += (s, e) =>
            {
                if (e.Column == null || e.Column.MappingName != kolona) return;
                var r = e.DataRow == null ? null : e.DataRow.RowData as DataRowView;
                if (r == null || !r.Row.Table.Columns.Contains(kolona)) return;
                Color? c = boja(SrvFormat.Str(r[kolona]));
                if (c.HasValue) { e.Style.TextColor = c.Value; e.Style.Font.Bold = true; }
            };
        }

        public static Color? StandardnaBoja(string status)
        {
            switch (status)
            {
                case "Kritičan": case "Isteklo": case "Kasni": case "Odbijeno": case "Odbačeno": case "Otkazan": case "Otkazano":
                case "Nema na stanju": case "U kvaru": case "Nedostaje": case "Nepročitano": case "Storniran":
                    return SrvFormat.StatusCrvena;
                case "Ispod minimuma": case "Upozorenje": case "Ističe": case "Dospelo": case "Čeka odobrenje": case "Čeka delove":
                case "Vraćeno na doradu": case "Delimično isporučeno": case "U toku": case "Nova": case "Rezervisano": case "Van upotrebe":
                    return SrvFormat.StatusNarandzasta;
                case "Na stanju": case "Važeći": case "U redu": case "Odobreno": case "Odobren": case "Odobrena": case "Završen":
                case "Isporučeno": case "Poručeno": case "Izdato": case "U radu": case "Zatvoreno": case "Pročitano":
                    return SrvFormat.StatusZelena;
            }
            return null;
        }

        // postavlja podatke i zadrzava izabrani zapis (po koloni ID); ne radi nista drugo na izbor reda
        protected void Postavi(SfDataGrid g, DataTable t, Predicate<object> filter = null, string kljuc = "ID")
        {
            object bio = null;
            var stari = g.SelectedItem as DataRowView;
            if (stari != null && stari.Row.Table.Columns.Contains(kljuc)) bio = stari[kljuc];
            g.DataSource = t;
            if (g.View != null)
            {
                g.View.Filter = filter;
                g.View.RefreshFilter();
            }
            if (bio != null && t.Columns.Contains(kljuc))
            {
                foreach (var z in g.View.Records)
                {
                    var v = z.Data as DataRowView;
                    if (v != null && Equals(v[kljuc], bio)) { g.SelectedItem = v; break; }
                }
            }
            else if (g.View != null && g.View.Records.Count > 0)
            {
                g.SelectedIndex = 0;
            }
        }

        protected static void Osvezi(SfDataGrid g)
        {
            if (g.View != null) g.View.RefreshFilter();
        }

        protected static DataRowView Izabran(SfDataGrid g)
        {
            return g.SelectedItem as DataRowView;
        }

        protected static int? IzabranID(SfDataGrid g, string kolona = "ID")
        {
            var r = Izabran(g);
            return r == null ? (int?)null : SrvFormat.IntN(r[kolona]);
        }

        protected static List<DataRowView> Izabrani(SfDataGrid g)
        {
            return g.SelectedItems.OfType<DataRowView>().ToList();
        }

        protected static int BrojRedova(SfDataGrid g)
        {
            return g.View == null ? 0 : g.View.Records.Count;
        }

        protected static IEnumerable<DataRowView> Vidljivi(SfDataGrid g)
        {
            if (g.View == null) yield break;
            foreach (var z in g.View.Records)
            {
                var v = z.Data as DataRowView;
                if (v != null) yield return v;
            }
        }

        // tekst pretrage u bilo kojoj od kolona
        protected static bool Sadrzi(DataRowView r, string trazi, params string[] kolone)
        {
            if (string.IsNullOrWhiteSpace(trazi)) return true;
            foreach (string k in kolone)
                if (SrvFormat.Str(r[k]).IndexOf(trazi.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0) return true;
            return false;
        }

        protected static void Dvoklik(SfDataGrid g, Action a)
        {
            g.CellDoubleClick += (s, e) =>
            {
                if (e.DataRow == null || e.DataRow.RowType != RowType.DefaultRow) return;
                a();
            };
        }

        /* ---------------- Excel ---------------- */

        protected void Excel(SfDataGrid g, string naziv)
        {
            if (BrojRedova(g) == 0) { Poruke.Info(this, "Nema podataka za izvoz."); return; }
            string ime = naziv + " " + DateTime.Now.ToString("yyyy-MM-dd HHmm") + ".xlsx";
            foreach (var c in Path.GetInvalidFileNameChars()) ime = ime.Replace(c, '_');
            using (var dlg = new SaveFileDialog { Filter = "Excel (*.xlsx)|*.xlsx", FileName = ime })
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                Cursor = Cursors.WaitCursor;
                try
                {
                    Izvezi(g, dlg.FileName, naziv);
                    Poruke.Info(this, "Izvezeno u Excel: " + Path.GetFileName(dlg.FileName));
                }
                finally { Cursor = Cursors.Default; }
            }
        }

        public static void Izvezi(SfDataGrid g, string fajl, string naziv)
        {
            var kolone = g.Columns.Where(c => c.Visible).ToList();
            using (var wb = new XLWorkbook())
            {
                string list = Regex.Replace(naziv, @"[:\\/?*\[\]]", "_");
                var ws = wb.Worksheets.Add(list.Length > 31 ? list.Substring(0, 31) : list);
                for (int k = 0; k < kolone.Count; k++)
                {
                    ws.Cell(1, k + 1).Value = kolone[k].HeaderText;
                    ws.Cell(1, k + 1).Style.Font.Bold = true;
                    ws.Cell(1, k + 1).Style.Fill.BackgroundColor = XLColor.FromArgb(241, 240, 234);
                }
                int red = 2;
                foreach (var r in Vidljivi(g))
                {
                    for (int k = 0; k < kolone.Count; k++)
                    {
                        object v = r[kolone[k].MappingName];
                        var cell = ws.Cell(red, k + 1);
                        if (v == null || v == DBNull.Value) continue;
                        if (v is DateTime)
                        {
                            cell.Value = (DateTime)v;
                            cell.Style.DateFormat.Format = kolone[k] is GridDateTimeColumn && ((GridDateTimeColumn)kolone[k]).Format.Contains("HH") ? "dd.mm.yyyy. hh:mm" : "dd.mm.yyyy.";
                        }
                        else if (v is decimal || v is double || v is int || v is short || v is byte || v is long)
                        {
                            cell.Value = Convert.ToDouble(v);
                            cell.Style.NumberFormat.Format = v is decimal || v is double ? "#,##0.00" : "0";
                        }
                        else if (v is bool) cell.Value = (bool)v ? "Da" : "Ne";
                        else cell.Value = v.ToString();
                    }
                    red++;
                }
                ws.SheetView.FreezeRows(1);
                ws.Columns().AdjustToContents(1, 200, 8, 60);
                wb.SaveAs(fajl);
            }
        }
    }
}
