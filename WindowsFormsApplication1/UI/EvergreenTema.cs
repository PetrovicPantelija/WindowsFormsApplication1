using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// Zajednicka "Evergreen" tema za unutrasnje forme (samo boje i stil, bez izmene rasporeda i funkcionalnosti).
    /// Upotreba: u konstruktoru forme, odmah posle InitializeComponent();  EvergreenTema.Primeni(this);
    /// </summary>
    public static class EvergreenTema
    {
        public static readonly Color Zelena = Color.FromArgb(58, 77, 57);        // #3A4D39
        public static readonly Color TamnoZelena = Color.FromArgb(46, 62, 45);   // #2E3E2D
        public static readonly Color Pozadina = Color.FromArgb(248, 247, 244);   // #F8F7F4
        public static readonly Color Okvir = Color.FromArgb(227, 225, 218);      // #E3E1DA
        public static readonly Color Tekst = Color.FromArgb(37, 39, 42);         // #25272A
        public static readonly Color RedAlt = Color.FromArgb(241, 245, 238);     // #F1F5EE
        public static readonly Color Selekcija = Color.FromArgb(228, 235, 223);  // #E4EBDF
        public static readonly Color Akcenat = Color.FromArgb(249, 165, 26);     // #F9A51A

        public static void Primeni(Form forma)
        {
            if (forma == null) return;
            try
            {
                forma.BackColor = Pozadina;
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
                Stilizuj(c);
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
                // Beli ili skoro beli tekst (bio je pisan za zelenu pozadinu) postaje tamni.
                if (c.ForeColor.GetBrightness() > 0.85f) c.ForeColor = Tekst;
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
            g.BackgroundColor = Pozadina;
            g.BorderStyle = BorderStyle.None;
            g.GridColor = Okvir;

            g.ColumnHeadersDefaultCellStyle.BackColor = Zelena;
            g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            g.ColumnHeadersDefaultCellStyle.SelectionBackColor = Zelena;
            g.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            g.RowHeadersDefaultCellStyle.BackColor = RedAlt;
            g.RowHeadersDefaultCellStyle.SelectionBackColor = Selekcija;

            g.AlternatingRowsDefaultCellStyle.BackColor = RedAlt;
            g.DefaultCellStyle.SelectionBackColor = Selekcija;
            g.DefaultCellStyle.SelectionForeColor = Tekst;
        }

        private static void StilDugme(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            if (b.UseVisualStyleBackColor)
            {
                // Dugme bez zadate boje: sekundarni stil (belo sa tankim obrubom).
                b.UseVisualStyleBackColor = false;
                b.BackColor = Color.White;
                b.ForeColor = Tekst;
                b.FlatAppearance.BorderSize = 1;
                b.FlatAppearance.BorderColor = Okvir;
            }
            else
            {
                // Dugme sa sopstvenom bojom (narandzasta, zelena, crvena...): boja ostaje ista.
                b.FlatAppearance.BorderSize = 0;
            }
        }

        private static bool JednakaBoja(Color c, int r, int g, int b)
        {
            return c.R == r && c.G == g && c.B == b;
        }
    }
}
