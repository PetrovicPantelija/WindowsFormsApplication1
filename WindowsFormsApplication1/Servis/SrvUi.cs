using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Servis
{
    // ZAH-010: format brojeva i datuma u lokalnom obliku (1.234,50 i 09.11.2026.) i boje uloga dugmadi.
    public static class SrvFormat
    {
        public static readonly CultureInfo Kultura = NapraviKulturu();

        // boje po ulozi; JelaTema ih pretvara u beli stil sa obojenim rubom
        public static readonly Color Zelena = Color.FromArgb(124, 187, 0);
        public static readonly Color Narandzasta = Color.FromArgb(225, 142, 4);
        // paleta G za status (uvek uz tekst statusa)
        public static readonly Color StatusZelena = Color.FromArgb(74, 120, 0);
        public static readonly Color StatusNarandzasta = Color.FromArgb(170, 100, 0);
        public static readonly Color StatusCrvena = Color.FromArgb(176, 48, 40);

        static CultureInfo NapraviKulturu()
        {
            CultureInfo c;
            try { c = (CultureInfo)new CultureInfo("sr-Latn-RS").Clone(); }
            catch (Exception) { c = (CultureInfo)CultureInfo.InvariantCulture.Clone(); }
            c.NumberFormat.NumberDecimalSeparator = ",";
            c.NumberFormat.NumberGroupSeparator = ".";
            c.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy.";
            return c;
        }

        public static string Broj(decimal x, int decimala = 2)
        {
            return x.ToString("N" + decimala, Kultura);
        }

        public static string Broj(object x, int decimala = 2)
        {
            if (x == null || x == DBNull.Value) return "";
            return Broj(Convert.ToDecimal(x), decimala);
        }

        public static string Datum(DateTime d)
        {
            return d.ToString("dd.MM.yyyy.", Kultura);
        }

        // prihvata 1.234,5 i 1234,5; tacka kao decimalni znak samo kada nije oznaka hiljada (npr. 2.5)
        public static bool ProbajBroj(string tekst, out decimal vrednost)
        {
            vrednost = 0;
            string s = (tekst ?? "").Trim().Replace(" ", "").Replace(" ", "");
            if (s == "") return false;
            int zarez = s.LastIndexOf(','), tacka = s.LastIndexOf('.');
            if (zarez >= 0 && tacka >= 0)
            {
                if (zarez > tacka) s = s.Replace(".", "").Replace(",", ".");
                else s = s.Replace(",", "");
            }
            else if (zarez >= 0) s = s.Replace(",", ".");
            else if (tacka >= 0 && (s.Count(ch => ch == '.') > 1 || s.Length - tacka - 1 == 3)) s = s.Replace(".", "");
            return decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out vrednost);
        }

        // Jela tema crta dugmad u "flat" stilu sa obojenim tekstom; iskljuceno dugme mora vidno da posivi (14.6).
        // Poziva se posle JelaTema.Primeni (pamti boje koje je tema postavila).
        public static void SiviIskljucena(Control koren)
        {
            foreach (Control c in koren.Controls)
            {
                var b = c as Button;
                if (b != null && b.FlatStyle == FlatStyle.Flat && !(b.Tag is Color[]))
                {
                    b.Tag = new[] { b.ForeColor, b.FlatAppearance.BorderColor };
                    EventHandler primeni = (s, e) =>
                    {
                        var boje = (Color[])b.Tag;
                        b.ForeColor = b.Enabled ? boje[0] : Color.FromArgb(165, 166, 170);
                        b.FlatAppearance.BorderColor = b.Enabled ? boje[1] : Color.FromArgb(225, 225, 225);
                        var ikona = b as FontAwesome.Sharp.IconButton;
                        if (ikona != null) ikona.IconColor = b.ForeColor;
                    };
                    b.EnabledChanged += primeni;
                    primeni(b, EventArgs.Empty);
                }
                if (c.HasChildren) SiviIskljucena(c);
            }
        }

        public static int Int(object o) { return Convert.ToInt32(o); }
        public static int? IntN(object o) { return o == null || o == DBNull.Value ? (int?)null : Convert.ToInt32(o); }
        public static decimal Dec(object o) { return o == null || o == DBNull.Value ? 0 : Convert.ToDecimal(o); }
        public static decimal? DecN(object o) { return o == null || o == DBNull.Value ? (decimal?)null : Convert.ToDecimal(o); }
        public static string Str(object o) { return o == null || o == DBNull.Value ? "" : o.ToString(); }
    }

    // Polja za unos u dve kolone (naziv, kontrola); obavezna polja nose zvezdicu i proveravaju se pre baze.
    public class PoljaPanel : TableLayoutPanel
    {
        class Polje
        {
            public string Naziv;
            public Control Kontrola;
            public bool Obavezno;
            public bool JeBroj;
            public decimal? Min, Max;
        }

        readonly List<Polje> polja = new List<Polje>();
        public int SirinaKontrole = 300;

        public PoljaPanel()
        {
            ColumnCount = 2;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            Padding = new Padding(0, 4, 0, 4);
        }

        void Red(string naziv, Control c, bool obavezno)
        {
            var lbl = new Label
            {
                Text = naziv + (obavezno ? " *" : ""),
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(0, 6, 16, 6),
                Font = JelaTema.FontNaslov,
                ForeColor = JelaTema.TekstNaslova
            };
            c.Margin = new Padding(0, 4, 0, 4);
            if (c.Width < 40) c.Width = SirinaKontrole;
            RowCount++;
            Controls.Add(lbl, 0, RowCount - 1);
            Controls.Add(c, 1, RowCount - 1);
        }

        public TextBox Tekst(string naziv, string vrednost, bool obavezno = false, bool viseRedova = false, int maks = 0)
        {
            var t = new TextBox { Text = vrednost ?? "", Width = SirinaKontrole };
            if (maks > 0) t.MaxLength = maks;
            if (viseRedova) { t.Multiline = true; t.Height = 64; t.ScrollBars = ScrollBars.Vertical; t.AcceptsReturn = true; }
            Red(naziv, t, obavezno);
            polja.Add(new Polje { Naziv = naziv, Kontrola = t, Obavezno = obavezno });
            return t;
        }

        public TextBox Broj(string naziv, decimal? vrednost, bool obavezno = false, decimal? min = null, decimal? max = null, int decimala = 2)
        {
            var t = new TextBox { Text = vrednost.HasValue ? SrvFormat.Broj(vrednost.Value, decimala) : "", Width = 140, TextAlign = HorizontalAlignment.Right };
            Red(naziv, t, obavezno);
            polja.Add(new Polje { Naziv = naziv, Kontrola = t, Obavezno = obavezno, JeBroj = true, Min = min, Max = max });
            return t;
        }

        // lista iz tabele (ID, Naziv); pretraga = moze da se kuca deo naziva
        public ComboBox Lista(string naziv, DataTable izvor, object izabrano, bool obavezno = false, bool pretraga = false, string prazno = null)
        {
            var c = new ComboBox { Width = SirinaKontrole };
            PostaviListu(c, izvor, izabrano, pretraga, prazno);
            Red(naziv, c, obavezno);
            polja.Add(new Polje { Naziv = naziv, Kontrola = c, Obavezno = obavezno });
            return c;
        }

        public static void PostaviListu(ComboBox c, DataTable izvor, object izabrano, bool pretraga = false, string prazno = null)
        {
            DataTable t = izvor.Copy();
            if (prazno != null)
            {
                var r = t.NewRow();
                r["ID"] = DBNull.Value; r["Naziv"] = prazno;
                t.Rows.InsertAt(r, 0);
            }
            // sopstveni BindingContext: lista ima stavke i pre nego sto je panel na formi
            c.BindingContext = new BindingContext();
            c.DataSource = null;
            c.DisplayMember = "Naziv";
            c.ValueMember = "ID";
            c.DataSource = t;
            if (pretraga)
            {
                c.DropDownStyle = ComboBoxStyle.DropDown;
                c.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                c.AutoCompleteSource = AutoCompleteSource.ListItems;
                c.Validating -= UskladiIzbor;
                c.Validating += UskladiIzbor;
            }
            else c.DropDownStyle = ComboBoxStyle.DropDownList;
            if (izabrano != null && izabrano != DBNull.Value) c.SelectedValue = izabrano;
            else c.SelectedIndex = t.Rows.Count > 0 && prazno != null ? 0 : -1;
        }

        // kucani tekst -> stavka liste sa istim nazivom
        static void UskladiIzbor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var c = (ComboBox)sender;
            if (c.SelectedIndex >= 0 && c.Text == c.GetItemText(c.SelectedItem)) return;
            int i = c.FindStringExact(c.Text);
            c.SelectedIndex = i;
        }

        public ComboBox Izbor(string naziv, string[] opcije, string izabrano, bool obavezno = false)
        {
            var c = new ComboBox { Width = SirinaKontrole, DropDownStyle = ComboBoxStyle.DropDownList };
            c.Items.AddRange(opcije);
            if (izabrano != null) c.SelectedItem = izabrano;
            Red(naziv, c, obavezno);
            polja.Add(new Polje { Naziv = naziv, Kontrola = c, Obavezno = obavezno });
            return c;
        }

        public DateTimePicker Datum(string naziv, DateTime? vrednost, bool obavezno = true)
        {
            var d = new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "dd.MM.yyyy.", Width = 140 };
            if (!obavezno) d.ShowCheckBox = true;
            if (vrednost.HasValue) d.Value = vrednost.Value; else if (!obavezno) d.Checked = false;
            Red(naziv, d, obavezno);
            polja.Add(new Polje { Naziv = naziv, Kontrola = d, Obavezno = obavezno });
            return d;
        }

        public CheckBox Kvacica(string naziv, bool vrednost)
        {
            var c = new CheckBox { Checked = vrednost, AutoSize = true, Width = 20 };
            Red(naziv, c, false);
            return c;
        }

        public Label Prikaz(string naziv, string tekst)
        {
            var l = new Label { Text = tekst, AutoSize = true, MaximumSize = new Size(SirinaKontrole, 0), Width = 1 };
            Red(naziv, l, false);
            return l;
        }

        public void Kontrola(string naziv, Control c, bool obavezno = false)
        {
            Red(naziv, c, obavezno);
        }

        // provera obaveznih polja i brojeva; vraca poruku ili null
        public string Proveri()
        {
            foreach (var p in polja)
            {
                if (!p.Kontrola.Enabled || !p.Kontrola.Visible) continue;
                var t = p.Kontrola as TextBox;
                var c = p.Kontrola as ComboBox;
                if (t != null)
                {
                    if (p.Obavezno && t.Text.Trim() == "") return Fokus(p, "Unesite polje „" + p.Naziv + "“.");
                    if (p.JeBroj && t.Text.Trim() != "")
                    {
                        decimal x;
                        if (!SrvFormat.ProbajBroj(t.Text, out x)) return Fokus(p, "Polje „" + p.Naziv + "“ mora biti broj.");
                        if (p.Min.HasValue && p.Max.HasValue && (x < p.Min || x > p.Max))
                            return Fokus(p, "Polje „" + p.Naziv + "“ mora biti između " + SrvFormat.Broj(p.Min.Value, 0) + " i " + SrvFormat.Broj(p.Max.Value, 0) + ".");
                        if (p.Min.HasValue && !p.Max.HasValue && x < p.Min)
                            return Fokus(p, p.Min.Value == 0 ? "Polje „" + p.Naziv + "“ ne sme biti negativno." : "Polje „" + p.Naziv + "“ mora biti najmanje " + SrvFormat.Broj(p.Min.Value, 0) + ".");
                    }
                }
                else if (c != null && p.Obavezno)
                {
                    bool prazno = c.DataSource != null ? (c.SelectedValue == null || c.SelectedValue == DBNull.Value) : c.SelectedIndex < 0;
                    if (prazno) return Fokus(p, "Unesite polje „" + p.Naziv + "“.");
                }
            }
            return null;
        }

        static string Fokus(Polje p, string poruka)
        {
            p.Kontrola.Focus();
            return poruka;
        }

        public static decimal? BrojIz(TextBox t)
        {
            decimal x;
            return SrvFormat.ProbajBroj(t.Text, out x) ? x : (decimal?)null;
        }

        public static object Vrednost(ComboBox c)
        {
            if (c.DataSource != null) return c.SelectedValue == DBNull.Value ? null : c.SelectedValue;
            return c.SelectedItem;
        }

        public static object Tekst(TextBox t)
        {
            string s = t.Text.Trim();
            return s == "" ? null : s;
        }

        public static DateTime? DatumIz(DateTimePicker d)
        {
            if (d.ShowCheckBox && !d.Checked) return null;
            return d.Value.Date;
        }
    }

    // Standardni dijalog za unos: naslov, polja, Sacuvaj (zeleno) i Odustani.
    // Snimi vraca true kada je upis uspeo (dijalog se zatvara); greske prikazuje dijalog i ostaje otvoren.
    public class SrvDijalog : Form
    {
        public readonly PoljaPanel Polja = new PoljaPanel();
        public readonly Button BtnSacuvaj = new Button();
        public readonly Button BtnOdustani = new Button();
        readonly FlowLayoutPanel sadrzaj = new FlowLayoutPanel();
        public Func<string> Provera;     // dodatna provera posle obaveznih polja (poruka ili null)
        public Func<bool> Snimi;

        public SrvDijalog(string naslov, string tekstSacuvaj = "Sačuvaj")
        {
            Text = naslov;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false; MaximizeBox = false; ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Font = JelaTema.FontTekst;
            AutoSize = true; AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Padding = new Padding(16, 12, 16, 12);

            sadrzaj.FlowDirection = FlowDirection.TopDown;
            sadrzaj.AutoSize = true;
            sadrzaj.WrapContents = false;
            sadrzaj.Controls.Add(Polja);

            var dole = new FlowLayoutPanel { FlowDirection = FlowDirection.RightToLeft, AutoSize = true, Anchor = AnchorStyles.Right, Margin = new Padding(0, 12, 0, 0) };
            BtnOdustani.Text = "Odustani";
            BtnOdustani.UseVisualStyleBackColor = true;
            BtnOdustani.DialogResult = DialogResult.Cancel;
            BtnSacuvaj.Text = tekstSacuvaj;
            BtnSacuvaj.BackColor = SrvFormat.Zelena;
            foreach (var b in new[] { BtnOdustani, BtnSacuvaj }) { b.AutoSize = true; b.MinimumSize = new Size(96, 30); b.Margin = new Padding(8, 0, 0, 0); }
            BtnSacuvaj.Click += (s, e) => Sacuvaj();
            dole.Controls.Add(BtnOdustani);
            dole.Controls.Add(BtnSacuvaj);
            CancelButton = BtnOdustani;

            var t = new TableLayoutPanel { ColumnCount = 1, AutoSize = true, Dock = DockStyle.Fill };
            t.Controls.Add(sadrzaj);
            t.Controls.Add(dole);
            Controls.Add(t);
        }

        public void DodajIspod(Control c)
        {
            c.Margin = new Padding(0, 8, 0, 0);
            sadrzaj.Controls.Add(c);
        }

        protected override void OnLoad(EventArgs e)
        {
            JelaTema.Primeni(this);
            SrvFormat.SiviIskljucena(this);
            base.OnLoad(e);
        }

        void Sacuvaj()
        {
            ValidateChildren();
            string greska = Polja.Proveri() ?? (Provera != null ? Provera() : null);
            if (greska != null) { Poruke.Upozorenje(this, greska); return; }
            if (Snimi == null) { DialogResult = DialogResult.OK; return; }
            try
            {
                Cursor = Cursors.WaitCursor;
                if (Snimi()) DialogResult = DialogResult.OK;
            }
            catch (Exception ex) { Poruke.Greska(this, ex); }
            finally { Cursor = Cursors.Default; }
        }
    }
}
