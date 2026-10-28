using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    // ZAH-008: zajednicki izgled formi izracuna cena (boje kao MainPakovanje).
    // Primeni(forma) prolazi kroz kontrole: dugmad dobijaju ravan stil; dugme sa Tag = "glavno" je zeleno.
    public static class Izgled
    {
        public static readonly Color Zelena = Color.FromArgb(58, 77, 57);
        public static readonly Color Krem = Color.FromArgb(248, 247, 244);
        public static readonly Color Ivica = Color.FromArgb(210, 205, 195);
        public static readonly Color Akcenat = Color.FromArgb(190, 150, 110);
        public static readonly Color Obavestenje = Color.FromArgb(255, 240, 180);
        public static readonly Font Slova = new Font("Segoe UI", 9F);

        public static void Primeni(Control koren)
        {
            foreach (Control c in koren.Controls)
            {
                var b = c as Button;
                if (b != null)
                {
                    bool glavno = (b.Tag as string) == "glavno";
                    b.FlatStyle = FlatStyle.Flat;
                    b.FlatAppearance.BorderColor = glavno ? Zelena : Ivica;
                    b.BackColor = glavno ? Zelena : Color.White;
                    b.ForeColor = glavno ? Color.White : Color.FromArgb(40, 40, 40);
                    b.Font = Slova;
                    b.Cursor = Cursors.Hand;
                }
                if (c.HasChildren) Primeni(c);
            }
        }

        // prikaz jedne ili vise tabela (npr. redovi bez troska, problemi uvoza)
        public static void PrikaziTabele(IWin32Window vlasnik, string naslov, params object[] imeITabela)
        {
            using (var f = new Form())
            {
                f.Text = naslov;
                f.Size = new Size(900, 520);
                f.StartPosition = FormStartPosition.CenterParent;
                f.Font = Slova;
                var tab = new TabControl { Dock = DockStyle.Fill };
                for (int i = 0; i + 1 < imeITabela.Length; i += 2)
                {
                    var dt = imeITabela[i + 1] as DataTable;
                    var str = new TabPage(imeITabela[i] + " (" + (dt == null ? 0 : dt.Rows.Count) + ")");
                    var g = new DataGridView
                    {
                        Dock = DockStyle.Fill, DataSource = dt, ReadOnly = true, AllowUserToAddRows = false,
                        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells,
                        BackgroundColor = Krem, BorderStyle = BorderStyle.None
                    };
                    str.Controls.Add(g);
                    tab.TabPages.Add(str);
                }
                var zatvori = new Button { Text = "Zatvori", Dock = DockStyle.Bottom, Height = 34, DialogResult = DialogResult.OK };
                f.Controls.Add(tab);
                f.Controls.Add(zatvori);
                f.AcceptButton = zatvori;
                Primeni(f);
                f.ShowDialog(vlasnik);
            }
        }

        // unos jednog teksta (naziv cenovnika i sl.); null = odustao
        public static string Unos(IWin32Window vlasnik, string naslov, string pitanje, string pocetno)
        {
            using (var f = new Form())
            {
                f.Text = naslov;
                f.FormBorderStyle = FormBorderStyle.FixedDialog;
                f.MinimizeBox = false; f.MaximizeBox = false;
                f.StartPosition = FormStartPosition.CenterParent;
                f.ClientSize = new Size(420, 120);
                f.Font = Slova;
                var lbl = new Label { Text = pitanje, Left = 12, Top = 12, Width = 396 };
                var txt = new TextBox { Text = pocetno, Left = 12, Top = 38, Width = 396 };
                var ok = new Button { Text = "U redu", Left = 232, Top = 76, Width = 84, Height = 30, DialogResult = DialogResult.OK, Tag = "glavno" };
                var ne = new Button { Text = "Odustani", Left = 324, Top = 76, Width = 84, Height = 30, DialogResult = DialogResult.Cancel };
                f.Controls.AddRange(new Control[] { lbl, txt, ok, ne });
                f.AcceptButton = ok; f.CancelButton = ne;
                Primeni(f);
                return f.ShowDialog(vlasnik) == DialogResult.OK && txt.Text.Trim() != "" ? txt.Text.Trim() : null;
            }
        }

        public static void Greska(IWin32Window vlasnik, string naslov, System.Exception ex)
        {
            MessageBox.Show(vlasnik, ex.Message, naslov, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
