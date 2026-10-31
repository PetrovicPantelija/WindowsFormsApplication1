using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;
using WindowsFormsApplication1.Magacin;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    // Proizvodi (dbo.jela_products) za izracun cena: unos, izmena, deaktiviranje, slika i uvoz iz CSV.
    // Program (NazivRobneGrupe) i kolekcija (NazivGrupe) biraju se samo iz sifarnika IC_Program / IC_Kolekcija.
    // Slika se vezuje za Code u IC_SlikaProizvoda; slika van foldera IC_KorenSlika se kopira u podfolder Proizvodi.
    // Stara forma Magacin\frmJelaProduct (MainAdministracija) ostaje kakva jeste; obe koriste JelaProductRepo.
    public class frmICProizvodi : Form
    {
        public const string Code = "frmICProizvodi";
        public bool Pravo;

        const string KolonaProgram = "NazivRobneGrupe";
        const string KolonaKolekcija = "NazivGrupe";
        const string FolderKopija = "Proizvodi";

        readonly string cs = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        readonly IzracunCenaBaza baza = new IzracunCenaBaza();
        readonly PravaForme prava;

        readonly SfDataGrid grid = new SfDataGrid();
        readonly TextBox txtTrazi = new TextBox();
        readonly CheckBox chkNeaktivni = new CheckBox();
        readonly Button btnProgrami = new Button(), btnUvozCsv = new Button(), btnOsvezi = new Button();
        readonly Button btnNovi = new Button(), btnSacuvaj = new Button(), btnDeaktiviraj = new Button(), btnSlika = new Button();
        readonly Dictionary<string, TextBox> polja = new Dictionary<string, TextBox>();
        readonly ComboBox cboProgram = new ComboBox(), cboKolekcija = new ComboBox();
        readonly CheckBox chkActive = new CheckBox();
        readonly PictureBox picSlika = new PictureBox();
        readonly Label lblSlika = new Label(), lblStatus = new Label(), lblRezim = new Label();

        DataTable podaci;
        List<string> programi = new List<string>();
        DataTable kolekcije;
        string izabraniBarCode;     // null = novi zapis
        string izabraniCode;        // Code snimljenog zapisa (za sliku)

        public frmICProizvodi()
        {
            Text = "Proizvodi (jela_products)";
            Size = new Size(1360, 820);
            StartPosition = FormStartPosition.CenterScreen;
            Font = Izgled.Slova;
            BackColor = Color.White;
            NapraviIzgled();
            Izgled.Primeni(this);

            prava = new PravaForme(Code);
            Pravo = prava.Pravo;
            if (!Pravo) return;
            btnNovi.Enabled = prava.Upis;
            btnSacuvaj.Enabled = prava.Upis || prava.Izmena;
            btnDeaktiviraj.Enabled = prava.Brisanje;
            btnUvozCsv.Enabled = prava.Upis && prava.Izmena;   // uvoz i dodaje i menja
            UcitajSifarnike();
            Ucitaj(null);
            Novi();
        }

        /* =====================================================================
           IZGLED
           ===================================================================== */

        void NapraviIzgled()
        {
            // zaglavlje: pozivi sifarnika, uvoz, pretraga
            var gore = new Panel { Dock = DockStyle.Top, Height = 48, BackColor = Izgled.Krem };
            int x = 12;
            foreach (var b in new[] { btnProgrami, btnUvozCsv, btnOsvezi })
            {
                b.SetBounds(x, 10, b == btnUvozCsv ? 130 : 100, 28);
                gore.Controls.Add(b);
                x += b.Width + 8;
            }
            btnProgrami.Text = "Programi";
            btnUvozCsv.Text = "Uvoz iz CSV...";
            btnOsvezi.Text = "Osveži";
            btnProgrami.Click += (s, e) => OtvoriProgrami();
            btnUvozCsv.Click += (s, e) => UvozCsv();
            btnOsvezi.Click += (s, e) => { UcitajSifarnike(); Ucitaj(izabraniBarCode); };

            chkNeaktivni.Text = "Prikaži neaktivne";
            chkNeaktivni.AutoSize = true;
            chkNeaktivni.Location = new Point(x + 16, 15);
            chkNeaktivni.CheckedChanged += (s, e) => { if (Pravo) Ucitaj(izabraniBarCode); };
            gore.Controls.Add(chkNeaktivni);
            gore.Controls.Add(new Label { Text = "Traži:", Left = x + 160, Top = 15, AutoSize = true });
            txtTrazi.SetBounds(x + 205, 12, 260, 23);
            txtTrazi.TextChanged += (s, e) => { if (grid.View != null) grid.View.RefreshFilter(); };
            gore.Controls.Add(txtTrazi);

            // desno: podaci izabranog proizvoda i slika
            var desno = new Panel { Dock = DockStyle.Right, Width = 460, AutoScroll = true, BackColor = Izgled.Krem, Padding = new Padding(8) };
            lblRezim.SetBounds(12, 8, 420, 22);
            lblRezim.Font = new Font("Segoe UI Semibold", 10F);
            lblRezim.ForeColor = Izgled.Zelena;
            desno.Controls.Add(lblRezim);

            int y = 36;
            foreach (var f in JelaProductRepo.Fields)
            {
                string natpis = f.Name == KolonaProgram ? "Program" : f.Name == KolonaKolekcija ? "Kolekcija" : f.Name;
                desno.Controls.Add(new Label
                {
                    Text = natpis + (f.Required ? " *" : ""),
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleRight,
                    Bounds = new Rectangle(4, y, 150, 23)
                });

                Control c;
                if (f.Name == KolonaProgram) c = cboProgram;
                else if (f.Name == KolonaKolekcija) c = cboKolekcija;
                else
                {
                    var t = new TextBox();
                    if (f.Type == JelaType.Text) t.MaxLength = f.Size;
                    if (f.Type != JelaType.Text) t.TextAlign = HorizontalAlignment.Right;
                    polja[f.Name] = t;
                    c = t;
                }
                c.SetBounds(160, y, 270, 23);
                desno.Controls.Add(c);
                y += 27;
            }
            foreach (var cbo in new[] { cboProgram, cboKolekcija })
            {
                cbo.DropDownStyle = ComboBoxStyle.DropDownList;
                cbo.DropDownWidth = 360;
            }
            cboProgram.SelectedIndexChanged += (s, e) => PopuniKolekcije(cboKolekcija.SelectedItem as string);

            chkActive.Text = "Aktivan";
            chkActive.AutoSize = true;
            chkActive.Location = new Point(160, y + 2);
            desno.Controls.Add(chkActive);
            y += 32;

            var dugmad = new[] { btnNovi, btnSacuvaj, btnDeaktiviraj };
            string[] nazivi = { "Novi", "Sačuvaj", "Deaktiviraj" };
            for (int i = 0; i < dugmad.Length; i++)
            {
                dugmad[i].Text = nazivi[i];
                dugmad[i].SetBounds(160 + i * 92, y, 86, 28);
                desno.Controls.Add(dugmad[i]);
            }
            btnSacuvaj.Tag = "glavno";
            btnNovi.Click += (s, e) => Novi();
            btnSacuvaj.Click += (s, e) => Sacuvaj();
            btnDeaktiviraj.Click += (s, e) => Deaktiviraj();
            y += 42;

            lblSlika.SetBounds(12, y, 300, 22);
            lblSlika.Text = "Slika";
            desno.Controls.Add(lblSlika);
            btnSlika.Text = "Dodaj sliku...";
            btnSlika.SetBounds(316, y - 3, 114, 28);
            btnSlika.Click += (s, e) => DodajSliku();
            desno.Controls.Add(btnSlika);
            y += 28;
            picSlika.SetBounds(12, y, 418, 280);
            picSlika.SizeMode = PictureBoxSizeMode.Zoom;
            picSlika.BorderStyle = BorderStyle.FixedSingle;
            picSlika.BackColor = Color.White;
            desno.Controls.Add(picSlika);

            lblStatus.Dock = DockStyle.Bottom;
            lblStatus.Height = 24;
            lblStatus.BackColor = Izgled.Krem;
            lblStatus.ForeColor = Color.FromArgb(80, 80, 80);
            lblStatus.Padding = new Padding(8, 4, 0, 0);

            grid.Dock = DockStyle.Fill;
            grid.AutoGenerateColumns = false;
            grid.AllowEditing = false;
            grid.AllowFiltering = true;
            grid.AllowSorting = true;
            grid.AllowResizingColumns = true;
            grid.Style.HeaderStyle.BackColor = Izgled.Zelena;
            grid.Style.HeaderStyle.TextColor = Color.White;
            grid.Columns.Add(new GridCheckBoxColumn { MappingName = "Active", HeaderText = "Aktivan", Width = 60 });
            grid.Columns.Add(new GridTextColumn { MappingName = "BarCode", HeaderText = "BarCode", Width = 115 });
            grid.Columns.Add(new GridTextColumn { MappingName = "Code", HeaderText = "Code", Width = 100 });
            grid.Columns.Add(new GridTextColumn { MappingName = "SifraProizvodaLargo", HeaderText = "Šifra Largo", Width = 150 });
            grid.Columns.Add(new GridTextColumn { MappingName = "NazivProizvoda", HeaderText = "Naziv proizvoda", Width = 220 });
            grid.Columns.Add(new GridTextColumn { MappingName = KolonaProgram, HeaderText = "Program", Width = 130 });
            grid.Columns.Add(new GridTextColumn { MappingName = KolonaKolekcija, HeaderText = "Kolekcija", Width = 150 });
            grid.Columns.Add(Broj("price_rs_WP", "Cena RSD", 2));
            grid.Columns.Add(Broj("price_WP", "Cena EUR", 2));
            grid.Columns.Add(Broj("ProizvodID", "ProizvodID", 0));
            grid.SelectionChanged += (s, e) => PrikaziIzabrani();

            Controls.Add(grid);
            Controls.Add(desno);
            Controls.Add(gore);
            Controls.Add(lblStatus);
        }

        static GridNumericColumn Broj(string kolona, string naslov, int decimala)
        {
            var nfi = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
            nfi.NumberDecimalDigits = decimala;
            if (decimala == 0) nfi.NumberGroupSeparator = "";
            return new GridNumericColumn { MappingName = kolona, HeaderText = naslov, Width = 90, NumberFormatInfo = nfi };
        }

        /* =====================================================================
           UCITAVANJE
           ===================================================================== */

        void UcitajSifarnike()
        {
            try
            {
                programi = baza.IcProgrami().AsEnumerable().Select(r => r["Naziv"].ToString()).ToList();
                kolekcije = baza.IcKolekcije();
            }
            catch (Exception ex) { Izgled.Greska(this, Text, ex); }
        }

        void Ucitaj(string izaberi)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                podaci = JelaProductRepo.Load(cs, chkNeaktivni.Checked);
                grid.DataSource = podaci;
                grid.View.Filter = Prikazan;
                grid.View.RefreshFilter();
                lblStatus.Text = "Proizvoda: " + podaci.Rows.Count.ToString("N0")
                               + "   |   Program i kolekcija se biraju iz šifarnika izračuna cena (dugme Programi; kolekcije u MainKalkulacije → Kolekcije).";
                if (izaberi != null)
                {
                    var red = podaci.DefaultView.Cast<DataRowView>()
                                    .FirstOrDefault(v => string.Equals(v["BarCode"] as string, izaberi, StringComparison.Ordinal));
                    if (red != null) { grid.SelectedItem = red; PrikaziIzabrani(); }
                }
            }
            catch (Exception ex) { Izgled.Greska(this, Text, ex); }
            finally { Cursor = Cursors.Default; }
        }

        bool Prikazan(object o)
        {
            var r = o as DataRowView;
            string t = txtTrazi.Text.Trim();
            if (r == null || t == "") return true;
            foreach (var k in new[] { "BarCode", "Code", "SifraProizvodaLargo", "NazivProizvoda", KolonaProgram, KolonaKolekcija })
                if (r[k].ToString().IndexOf(t, StringComparison.CurrentCultureIgnoreCase) >= 0) return true;
            return false;
        }

        // lista programa; vrednost koje nema u sifarniku (stari podatak) se dodaje da bi se videla
        void PopuniProgram(string vrednost)
        {
            cboProgram.BeginUpdate();
            cboProgram.Items.Clear();
            foreach (var p in programi) cboProgram.Items.Add(p);
            if (!string.IsNullOrEmpty(vrednost) && !programi.Contains(vrednost)) cboProgram.Items.Add(vrednost);
            cboProgram.EndUpdate();
            cboProgram.SelectedItem = string.IsNullOrEmpty(vrednost) ? null : vrednost;
            if (cboProgram.SelectedItem == null) PopuniKolekcije(null);
        }

        // kolekcije izabranog programa
        void PopuniKolekcije(string vrednost)
        {
            string program = cboProgram.SelectedItem as string;
            var lista = kolekcije == null || program == null
                ? new List<string>()
                : kolekcije.AsEnumerable().Where(r => r["Program"].ToString() == program).Select(r => r["Naziv"].ToString()).ToList();
            cboKolekcija.BeginUpdate();
            cboKolekcija.Items.Clear();
            foreach (var k in lista) cboKolekcija.Items.Add(k);
            if (!string.IsNullOrEmpty(vrednost) && !lista.Contains(vrednost)) cboKolekcija.Items.Add(vrednost);
            cboKolekcija.EndUpdate();
            cboKolekcija.SelectedItem = string.IsNullOrEmpty(vrednost) ? null : vrednost;
        }

        void PrikaziIzabrani()
        {
            var r = grid.SelectedItem as DataRowView;
            if (r == null) return;

            izabraniBarCode = (string)r["BarCode"];
            izabraniCode = r["Code"] as string;
            foreach (var f in JelaProductRepo.Fields)
                if (polja.ContainsKey(f.Name)) polja[f.Name].Text = JelaProductRepo.ToText(r[f.Name]);
            polja[JelaProductRepo.Key].ReadOnly = true;   // primarni kljuc se ne menja
            string program = r[KolonaProgram] as string, kolekcija = r[KolonaKolekcija] as string;
            PopuniProgram(program);
            PopuniKolekcije(kolekcija);
            chkActive.Checked = (bool)r["Active"];

            var upozorenja = new List<string>();
            if (!string.IsNullOrEmpty(program) && !programi.Contains(program)) upozorenja.Add("program");
            if (!string.IsNullOrEmpty(kolekcija) && !KolekcijaUSifarniku(program, kolekcija)) upozorenja.Add("kolekcija");
            lblRezim.Text = "Izmena: " + izabraniBarCode
                          + (upozorenja.Count > 0 ? "   (" + string.Join(" i ", upozorenja) + " nije u šifarniku)" : "");
            btnSacuvaj.Enabled = prava.Izmena;
            btnDeaktiviraj.Enabled = prava.Brisanje && chkActive.Checked;
            btnSlika.Enabled = (prava.Upis || prava.Izmena) && !string.IsNullOrEmpty(izabraniCode);
            PrikaziSliku();
        }

        bool KolekcijaUSifarniku(string program, string kolekcija)
        {
            return kolekcije != null && kolekcije.AsEnumerable()
                .Any(r => r["Program"].ToString() == program && r["Naziv"].ToString() == kolekcija);
        }

        /* =====================================================================
           UNOS / IZMENA / DEAKTIVIRANJE
           ===================================================================== */

        void Novi()
        {
            izabraniBarCode = null;
            izabraniCode = null;
            grid.SelectedItem = null;
            foreach (var t in polja.Values) t.Clear();
            polja[JelaProductRepo.Key].ReadOnly = false;
            PopuniProgram(null);
            chkActive.Checked = true;
            lblRezim.Text = "Novi proizvod";
            btnSacuvaj.Enabled = prava.Upis;
            btnDeaktiviraj.Enabled = false;
            btnSlika.Enabled = false;
            PrikaziSliku();
            polja["ProizvodID"].Focus();
        }

        void Sacuvaj()
        {
            bool nov = izabraniBarCode == null;
            if (nov && !prava.Upis) { MessageBox.Show(this, "Nemate pravo upisa.", Text); return; }
            if (!nov && !prava.Izmena) { MessageBox.Show(this, "Nemate pravo izmene.", Text); return; }

            var vrednosti = new Dictionary<string, object>();
            var greske = new List<string>();
            foreach (var f in JelaProductRepo.Fields)
            {
                string tekst = f.Name == KolonaProgram ? cboProgram.SelectedItem as string
                             : f.Name == KolonaKolekcija ? cboKolekcija.SelectedItem as string
                             : polja[f.Name].Text;
                object v;
                string greska;
                if (JelaProductRepo.TryParse(f, tekst, out v, out greska)) vrednosti[f.Name] = v;
                else greske.Add((f.Name == KolonaProgram ? "Program" : f.Name == KolonaKolekcija ? "Kolekcija" : f.Name) + ": " + greska);
            }
            if (greske.Count > 0)
            {
                MessageBox.Show(this, string.Join("\n", greske), "Proveri unos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string barCode = nov ? (string)vrednosti[JelaProductRepo.Key] : izabraniBarCode;
            try
            {
                if (nov) JelaProductRepo.Insert(cs, vrednosti, chkActive.Checked);
                else JelaProductRepo.Update(cs, barCode, vrednosti, chkActive.Checked);
                if (!chkActive.Checked) chkNeaktivni.Checked = true;   // da snimljeni red ostane vidljiv
                Ucitaj(barCode);
            }
            catch (SqlException ex) when (JelaProductRepo.IsDuplicateKey(ex))
            {
                MessageBox.Show(this, "Proizvod sa BarCode-om " + barCode + " već postoji (možda je neaktivan).", Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex) { Izgled.Greska(this, Text, ex); }
        }

        void Deaktiviraj()
        {
            if (izabraniBarCode == null) return;
            if (MessageBox.Show(this, "Deaktivirati proizvod " + polja["SifraProizvodaLargo"].Text + " (" + izabraniBarCode + ")?", Text,
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                JelaProductRepo.SetActive(cs, izabraniBarCode, false);
                string bc = izabraniBarCode;
                Ucitaj(bc);
                if (grid.SelectedItem == null) Novi();
            }
            catch (Exception ex) { Izgled.Greska(this, Text, ex); }
        }

        /* =====================================================================
           SIFARNIK PROGRAMA (zaglavlje)
           ===================================================================== */

        void OtvoriProgrami()
        {
            var f = new frmICSifarnik(VrstaSifarnika.Program);
            if (!f.Pravo) { f.Dispose(); return; }
            using (f) f.ShowDialog(this);
            UcitajSifarnike();
            string program = cboProgram.SelectedItem as string, kolekcija = cboKolekcija.SelectedItem as string;
            PopuniProgram(program);
            PopuniKolekcije(kolekcija);
        }

        /* =====================================================================
           SLIKA (IC_SlikaProizvoda po Code)
           ===================================================================== */

        void PrikaziSliku()
        {
            var stara = picSlika.Image;
            picSlika.Image = null;
            if (stara != null) stara.Dispose();
            if (string.IsNullOrEmpty(izabraniCode)) { lblSlika.Text = "Slika"; return; }

            try
            {
                string putanja = baza.GlavnaSlika(izabraniCode);
                if (string.IsNullOrEmpty(putanja)) { lblSlika.Text = izabraniCode + " - nema slike"; return; }
                string pun = Path.Combine(ucCenovnik.KorenSlika, putanja);
                if (!File.Exists(pun)) { lblSlika.Text = izabraniCode + " - fajl ne postoji"; return; }
                // kopija u memoriji, da fajl ostane slobodan
                using (var fs = new FileStream(pun, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var img = Image.FromStream(fs))
                    picSlika.Image = new Bitmap(img);
                lblSlika.Text = izabraniCode + " - " + putanja;
            }
            catch (Exception ex) { lblSlika.Text = izabraniCode + " - greška: " + ex.Message; }
        }

        void DodajSliku()
        {
            if (string.IsNullOrEmpty(izabraniCode)) { MessageBox.Show(this, "Prvo sačuvajte proizvod sa Code.", Text); return; }
            if (izabraniCode.Length > 30) { MessageBox.Show(this, "Code je duži od 30 znakova i ne može dobiti sliku.", Text); return; }

            string koren = ucCenovnik.KorenSlika;
            if (string.IsNullOrEmpty(koren) || !Directory.Exists(koren))
            {
                MessageBox.Show(this, "Folder sa slikama ne postoji:\n" + koren + "\n\nPodešava se u App.config (IC_KorenSlika).", Text);
                return;
            }
            koren = Path.GetFullPath(koren).TrimEnd('\\') + "\\";

            string fajl;
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Slika za " + izabraniCode;
                dlg.Filter = "Slike (*.jpg;*.png)|*.jpg;*.jpeg;*.png";
                dlg.InitialDirectory = koren;
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                fajl = Path.GetFullPath(dlg.FileName);
            }

            try
            {
                // slika van foldera se kopira u <koren>\Proizvodi pod imenom po Code (kao kod automatskog povezivanja)
                if (!fajl.StartsWith(koren, StringComparison.OrdinalIgnoreCase))
                    fajl = KopirajUFolder(fajl, koren);
                string rel = fajl.Substring(koren.Length);
                if (rel.Length > 400) { MessageBox.Show(this, "Putanja slike je predugačka.", Text); return; }
                baza.DodeliSlikuRucno(izabraniCode, rel, true);
                PrikaziSliku();
            }
            catch (Exception ex) { Izgled.Greska(this, Text, ex); }
        }

        string KopirajUFolder(string izvor, string koren)
        {
            string folder = Path.Combine(koren, FolderKopija);
            Directory.CreateDirectory(folder);

            string ime = izabraniCode.Replace('/', '_');
            foreach (char c in Path.GetInvalidFileNameChars()) ime = ime.Replace(c, '_');
            string ext = Path.GetExtension(izvor).ToLowerInvariant();

            // P62A_GL.jpg, pa P62A_GL (2).jpg ... (SlikeFolder cita "(n)" kao dodatnu sliku istog Code)
            string cilj = Path.Combine(folder, ime + ext);
            for (int n = 2; File.Exists(cilj); n++)
                cilj = Path.Combine(folder, ime + " (" + n + ")" + ext);
            File.Copy(izvor, cilj);
            return cilj;
        }

        /* =====================================================================
           UVOZ IZ CSV
           ===================================================================== */

        void UvozCsv()
        {
            string putanja;
            using (var dlg = new OpenFileDialog { Filter = "CSV (*.csv;*.txt)|*.csv;*.txt", Title = "Uvoz proizvoda iz CSV" })
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                putanja = dlg.FileName;
            }

            ImportPlan plan;
            try
            {
                Cursor = Cursors.WaitCursor;
                plan = JelaProductImport.BuildFromCsv(cs, putanja);   // samo priprema, nista se jos ne upisuje
            }
            catch (Exception ex) { Izgled.Greska(this, Text, ex); return; }
            finally { Cursor = Cursors.Default; }

            if (plan.Fatal != null)
            {
                MessageBox.Show(this, plan.Fatal, "Uvoz iz CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string rezime =
                "Redova u fajlu: " + plan.TotalRows + "\n" +
                "Novi proizvodi: " + plan.Inserts.Count + "\n" +
                "Izmenjeni proizvodi: " + plan.Updates.Count + "\n" +
                "Bez promene: " + plan.Unchanged + "\n" +
                "Preskočeno zbog greške: " + plan.Skipped;
            if (plan.Errors.Count > 0)
                rezime += "\n\nGreške (prvih 15):\n" + string.Join("\n", plan.Errors.Take(15)) +
                          (plan.Errors.Count > 15 ? "\n... i još " + (plan.Errors.Count - 15) : "");

            string van = VanSifarnika(plan);
            if (van != null) rezime += "\n\n" + van;

            if (plan.Inserts.Count + plan.Updates.Count == 0)
            {
                MessageBox.Show(this, rezime + "\n\nNema šta da se upiše.", "Uvoz iz CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show(this, rezime + "\n\nSnimiti u bazu?", "Uvoz iz CSV",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                Cursor = Cursors.WaitCursor;
                JelaProductImport.Apply(cs, plan);   // jedna transakcija: ili sve ili nista
            }
            catch (Exception ex) { Izgled.Greska(this, Text, ex); return; }
            finally { Cursor = Cursors.Default; }

            Ucitaj(izabraniBarCode);
            MessageBox.Show(this, "Uvoz završen.\nDodato: " + plan.Inserts.Count + "\nIzmenjeno: " + plan.Updates.Count,
                            "Uvoz iz CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // programi i kolekcije iz fajla kojih nema u sifarniku (samo upozorenje; uvoz ih ne dodaje)
        string VanSifarnika(ImportPlan plan)
        {
            var svi = plan.Inserts.Select(i => i.Values).Concat(plan.Updates.Select(u => u.Values)).ToList();
            var prog = svi.Where(v => v.ContainsKey(KolonaProgram)).Select(v => v[KolonaProgram] as string)
                          .Where(p => !string.IsNullOrEmpty(p) && !programi.Contains(p)).Distinct().ToList();
            var kolNazivi = kolekcije == null ? new HashSet<string>()
                          : new HashSet<string>(kolekcije.AsEnumerable().Select(r => r["Naziv"].ToString()));
            var kol = svi.Where(v => v.ContainsKey(KolonaKolekcija)).Select(v => v[KolonaKolekcija] as string)
                         .Where(k => !string.IsNullOrEmpty(k) && !kolNazivi.Contains(k)).Distinct().ToList();
            if (prog.Count + kol.Count == 0) return null;

            string s = "PAŽNJA - nema ih u šifarniku izračuna cena:";
            if (prog.Count > 0) s += "\nProgrami: " + string.Join(", ", prog.Take(10)) + (prog.Count > 10 ? " ..." : "");
            if (kol.Count > 0) s += "\nKolekcije: " + string.Join(", ", kol.Take(10)) + (kol.Count > 10 ? " ..." : "");
            return s + "\n(posle uvoza: Programi / Kolekcije → Dopuni iz jela_products)";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && picSlika.Image != null) picSlika.Image.Dispose();
            base.Dispose(disposing);
        }
    }
}
