using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// Zajednicka tema "G - Svetli minimal" za unutrasnje forme (samo boje i stil, bez izmene rasporeda i funkcionalnosti).
    /// Upotreba: u konstruktoru forme, odmah posle InitializeComponent();  JelaTema.Primeni(this);
    /// </summary>
    public static class JelaTema
    {
        public static readonly Color Pozadina = Color.FromArgb(243, 242, 238);   // #F3F2EE
        public static readonly Color Okvir = Color.FromArgb(228, 226, 218);      // #E4E2DA
        public static readonly Color Tekst = Color.FromArgb(37, 39, 42);         // #25272A
        public static readonly Color ZaglavljeGrida = Color.FromArgb(241, 240, 234); // #F1F0EA
        public static readonly Color TekstZaglavlja = Color.FromArgb(58, 60, 64);    // #3A3C40
        public static readonly Color RedAlt = Color.FromArgb(250, 250, 247);     // #FAFAF7
        public static readonly Color Selekcija = Color.FromArgb(253, 239, 210);  // #FDEFD2
        public static readonly Color ObrubDugmeta = Color.FromArgb(207, 208, 211); // #CFD0D3
        public static readonly Color Zelena = Color.FromArgb(124, 187, 0);       // #7CBB00
        public static readonly Color TekstNaslova = Color.FromArgb(95, 98, 103);  // #5F6267 (naslovi polja)

        // Tipografija "G - Svetli minimal": Segoe UI, tekst 13px (9.75pt), naslovi polja 12px poluboldirani (9pt).
        // Fontovi se prave jednom i dele (kontrole ih ne oslobadjaju).
        public static readonly Font FontTekst = NapraviFont("Segoe UI", 9.75F);
        public static readonly Font FontNaslov = NapraviFont("Segoe UI Semibold", 9F);
        public static readonly Font FontDugme = NapraviFont("Segoe UI Semibold", 9.75F);

        private static Font NapraviFont(string porodica, float velicina)
        {
            try { return new Font(porodica, velicina, FontStyle.Regular, GraphicsUnit.Point); }
            catch (Exception) { return SystemFonts.DefaultFont; }
        }

        // Kontrole sa namerno velikim fontom (naslovi, brojila, skeneri) ostaju kakve jesu.
        private static bool MozeDaSePromeni(Control c)
        {
            return c.Font.SizeInPoints <= 12.5f;
        }

        public static void Primeni(Form forma)
        {
            if (forma == null) return;
            try
            {
                forma.BackColor = Pozadina;
                // Tanka zelena linija na vrhu (crta se, ne dodaje kontrolu - neke forme prolaze kroz this.Controls).
                forma.Paint += delegate (object sender, PaintEventArgs e)
                {
                    using (SolidBrush cetka = new SolidBrush(Zelena))
                    {
                        e.Graphics.FillRectangle(cetka, 0, 0, ((Control)sender).ClientSize.Width, 3);
                    }
                };
                Obidji(forma);
            }
            catch (Exception)
            {
                // Tema je samo kozmetika - nikad ne sme da spreci otvaranje forme.
            }
        }

        private static void Obidji(Control roditelj)
        {
            foreach (Control c in roditelj.Controls)
            {
                try { Stilizuj(c); } catch (Exception) { }
                if (c.HasChildren) Obidji(c);
            }
        }

        private static void Stilizuj(Control c)
        {
            DataGridView grid = c as DataGridView;
            if (grid != null) { StilGrid(grid); return; }

            ToolStrip ts = c as ToolStrip;
            if (ts != null) { ts.BackColor = Color.White; ts.GripStyle = ToolStripGripStyle.Hidden; return; }

            TabPage strana = c as TabPage;
            if (strana != null) { strana.UseVisualStyleBackColor = false; strana.BackColor = Pozadina; return; }

            Button dugme = c as Button;
            if (dugme != null) { StilDugme(dugme); return; }

            if (c is Label || c is CheckBox || c is RadioButton || c is GroupBox)
            {
                bool boldNaslov = c.Font.Bold;
                bool promenljiv = MozeDaSePromeni(c);
                // Beli ili skoro beli tekst (bio je pisan za zelenu pozadinu) postaje tamni.
                if (c.ForeColor.GetBrightness() > 0.85f) c.ForeColor = (c is Label && boldNaslov) ? TekstNaslova : Tekst;
                if (promenljiv && !(c is GroupBox)) c.Font = (c is Label && boldNaslov) ? FontNaslov : FontTekst;
                return;
            }

            if (c is TextBox || c is ComboBox || c is DateTimePicker || c is NumericUpDown || c is MaskedTextBox || c is ListBox || c is RichTextBox)
            {
                if (MozeDaSePromeni(c)) c.Font = FontTekst;
                TextBox tb = c as TextBox;
                if (tb != null && tb.BorderStyle == BorderStyle.Fixed3D) tb.BorderStyle = BorderStyle.FixedSingle;
                return;
            }

            if (c is Panel || c is SplitContainer)
            {
                Color b = c.BackColor;
                // Stara zelena (#7CBB00) i plavkasta (#7D9EC0) pozadina panela -> nova pozadina.
                if (JednakaBoja(b, 124, 187, 0) || JednakaBoja(b, 125, 158, 192)) c.BackColor = Pozadina;
            }
        }

        private static void StilGrid(DataGridView g)
        {
            g.EnableHeadersVisualStyles = false;
            g.DefaultCellStyle.Font = FontTekst;
            g.ColumnHeadersDefaultCellStyle.Font = FontNaslov;
            g.BackgroundColor = Color.White;
            g.BorderStyle = BorderStyle.None;
            g.GridColor = Okvir;

            g.ColumnHeadersDefaultCellStyle.BackColor = ZaglavljeGrida;
            g.ColumnHeadersDefaultCellStyle.ForeColor = TekstZaglavlja;
            g.ColumnHeadersDefaultCellStyle.SelectionBackColor = ZaglavljeGrida;
            g.ColumnHeadersDefaultCellStyle.SelectionForeColor = TekstZaglavlja;

            g.RowHeadersDefaultCellStyle.BackColor = ZaglavljeGrida;
            g.RowHeadersDefaultCellStyle.SelectionBackColor = Selekcija;

            // Boje zadate u dizajneru (plava selekcija, beli tekst na svetloj pozadini...) ne smeju da pokvare izgled.
            g.DefaultCellStyle.BackColor = Color.White;
            if (Svetla(g.DefaultCellStyle.ForeColor) || g.DefaultCellStyle.ForeColor.IsEmpty) g.DefaultCellStyle.ForeColor = Tekst;
            g.DefaultCellStyle.SelectionBackColor = Selekcija;
            g.DefaultCellStyle.SelectionForeColor = Tekst;

            OcistiStil(g.RowsDefaultCellStyle);
            OcistiStil(g.AlternatingRowsDefaultCellStyle);
            if (g.RowTemplate != null) OcistiStil(g.RowTemplate.DefaultCellStyle);
            g.AlternatingRowsDefaultCellStyle.BackColor = RedAlt;
        }

        private static bool Svetla(Color c)
        {
            return !c.IsEmpty && c.A != 0 && c.GetBrightness() > 0.6f;
        }

        // Skida boje iz dizajnera sa stila redova: selekcija -> G, svetao tekst -> tamni, svetla pozadina -> nasledjuje.
        private static void OcistiStil(DataGridViewCellStyle s)
        {
            if (s == null) return;
            s.Font = null;
            if (!s.SelectionBackColor.IsEmpty) s.SelectionBackColor = Selekcija;
            if (!s.SelectionForeColor.IsEmpty) s.SelectionForeColor = Tekst;
            if (Svetla(s.ForeColor)) s.ForeColor = Tekst;
            if (!s.BackColor.IsEmpty && s.BackColor.GetBrightness() > 0.8f) s.BackColor = Color.Empty;
        }

        private static void StilDugme(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            PostaviFontDugmeta(b);
            if (b.UseVisualStyleBackColor)
            {
                // Dugme bez zadate boje: sekundarni stil (belo sa tankim obrubom).
                Minimalno(b, ObrubDugmeta, Tekst, Color.FromArgb(247, 246, 242));
                return;
            }

            Color boja = b.BackColor;
            if (boja.A == 0 || boja.GetBrightness() > 0.96f)
            {
                b.FlatAppearance.BorderSize = 0;
                return;
            }

            float ton = boja.GetHue();
            float zasicenje = boja.GetSaturation();
            if (zasicenje < 0.15f)
            {
                // Sivo dugme -> sekundarni stil.
                Minimalno(b, ObrubDugmeta, Tekst, Color.FromArgb(247, 246, 242));
            }
            else if (ton >= 70f && ton <= 160f && zasicenje > 0.25f)
            {
                // Zeleno dugme -> beli fond, zeleni obrub i tekst.
                Minimalno(b, Zelena, Color.FromArgb(74, 120, 0), Color.FromArgb(243, 248, 232));
            }
            else if (ton >= 20f && ton <= 45f && zasicenje > 0.4f)
            {
                // Narandzasto dugme -> beli fond, narandzasti obrub i tekst.
                Minimalno(b, Color.FromArgb(225, 142, 4), Color.FromArgb(170, 100, 0), Color.FromArgb(253, 243, 225));
            }
            else
            {
                // Ostale boje (crvena, plava...) ostaju iste.
                b.FlatAppearance.BorderSize = 0;
            }
        }

        private static void PostaviFontDugmeta(Button b)
        {
            if (!MozeDaSePromeni(b)) return;
            // Veci font samo ako tekst staje u dugme (dugmad su crtana za stari, manji font).
            Font[] kandidati = new Font[] { FontDugme, FontNaslov };
            foreach (Font f in kandidati)
            {
                Size m = TextRenderer.MeasureText(b.Text ?? "", f);
                if (m.Width + 20 <= b.Width || string.IsNullOrEmpty(b.Text))
                {
                    b.Font = f;
                    return;
                }
            }
        }

        private static void Minimalno(Button b, Color obrub, Color tekst, Color prelazMisa)
        {
            b.UseVisualStyleBackColor = false;
            b.BackColor = Color.White;
            b.ForeColor = tekst;
            b.FlatAppearance.BorderSize = 1;
            b.FlatAppearance.BorderColor = obrub;
            b.FlatAppearance.MouseOverBackColor = prelazMisa;
            b.FlatAppearance.MouseDownBackColor = Okvir;
            b.Cursor = Cursors.Hand;
            FontAwesome.Sharp.IconButton ikona = b as FontAwesome.Sharp.IconButton;
            if (ikona != null) ikona.IconColor = tekst;
        }

        private static bool JednakaBoja(Color c, int r, int g, int b)
        {
            return c.R == r && c.G == g && c.B == b;
        }
    }
}
