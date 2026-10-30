using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    public enum VrstaSifarnika { Program, Kolekcija }

    // ZAH-008: sifarnici izracuna cena - Programi (IC_Program) i Kolekcije (IC_Kolekcija).
    // Ne koristi stare tabele Program / Kolekcije (planiranje, pakovanje).
    // Proizvodi se automatski vezuju po nazivu iz jela_products (NazivRobneGrupe / NazivGrupe);
    // rucna dodela je u formi Pripadnost kolekcijama.
    public class frmICSifarnik : Form
    {
        public bool Pravo;

        readonly VrstaSifarnika vrsta;
        readonly IzracunCenaBaza baza = new IzracunCenaBaza();
        readonly PravaForme prava;
        readonly SfDataGrid grid = new SfDataGrid();
        readonly TextBox txtNaziv = new TextBox();
        readonly ComboBox cboProgram = new ComboBox();
        readonly TextBox txtTrazi = new TextBox();
        readonly Label lblStatus = new Label();
        readonly Button btnNovi = new Button(), btnSacuvaj = new Button(), btnObrisi = new Button(), btnOsvezi = new Button();
        DataTable podaci;
        int? izabraniID;

        bool Kolekcija { get { return vrsta == VrstaSifarnika.Kolekcija; } }

        public static string CodeZa(VrstaSifarnika v)
        {
            return v == VrstaSifarnika.Program ? "frmICProgrami" : "frmICKolekcije";
        }

        public frmICSifarnik(VrstaSifarnika vrsta)
        {
            this.vrsta = vrsta;
            Text = Kolekcija ? "Kolekcije (izračun cena)" : "Programi (izračun cena)";
            Size = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Font = Izgled.Slova;
            BackColor = Color.White;
            NapraviIzgled();
            Izgled.Primeni(this);

            prava = new PravaForme(CodeZa(vrsta));
            Pravo = prava.Pravo;
            if (!Pravo) return;
            btnNovi.Enabled = prava.Upis;
            btnOsvezi.Enabled = prava.Upis;
            btnSacuvaj.Enabled = prava.Upis || prava.Izmena;
            btnObrisi.Enabled = prava.Brisanje;
            Ucitaj(null);
        }

        void NapraviIzgled()
        {
            var gore = new Panel { Dock = DockStyle.Top, Height = Kolekcija ? 112 : 80, BackColor = Izgled.Krem };
            int y = 12;
            gore.Controls.Add(new Label { Text = "Naziv:", Left = 12, Top = y + 3, AutoSize = true });
            txtNaziv.SetBounds(90, y, 330, 23);
            gore.Controls.Add(txtNaziv);
            if (Kolekcija)
            {
                y += 32;
                gore.Controls.Add(new Label { Text = "Program:", Left = 12, Top = y + 3, AutoSize = true });
                cboProgram.SetBounds(90, y, 330, 23);
                cboProgram.DropDownStyle = ComboBoxStyle.DropDownList;
                gore.Controls.Add(cboProgram);
            }
            y += 34;
            var dugmad = new[] { btnNovi, btnSacuvaj, btnObrisi };
            string[] nazivi = { "Novi", "Sačuvaj", "Obriši" };
            for (int i = 0; i < dugmad.Length; i++)
            {
                dugmad[i].Text = nazivi[i];
                dugmad[i].SetBounds(90 + i * 96, y - 2, 90, 28);
                gore.Controls.Add(dugmad[i]);
            }
            btnSacuvaj.Tag = "glavno";
            btnNovi.Click += (s, e) => Novi();
            btnSacuvaj.Click += (s, e) => Sacuvaj();
            btnObrisi.Click += (s, e) => Obrisi();

            btnOsvezi.Text = "Dopuni iz jela_products";
            btnOsvezi.SetBounds(470, 10, 190, 28);
            btnOsvezi.Click += (s, e) => DopuniIzProizvoda();
            gore.Controls.Add(btnOsvezi);
            gore.Controls.Add(new Label { Text = "Traži:", Left = 470, Top = 50, AutoSize = true });
            txtTrazi.SetBounds(520, 47, 200, 23);
            txtTrazi.TextChanged += (s, e) => { if (grid.View != null) grid.View.RefreshFilter(); };
            gore.Controls.Add(txtTrazi);

            lblStatus.Dock = DockStyle.Bottom;
            lblStatus.Height = 24;
            lblStatus.BackColor = Izgled.Krem;
            lblStatus.ForeColor = Color.FromArgb(80, 80, 80);
            lblStatus.Padding = new Padding(8, 4, 0, 0);
            lblStatus.Text = "Proizvodi se vezuju po nazivu iz jela_products (" + (Kolekcija ? "NazivGrupe" : "NazivRobneGrupe")
                           + "); ručna dodela je u formi Pripadnost kolekcijama.";

            grid.Dock = DockStyle.Fill;
            grid.AutoGenerateColumns = false;
            grid.AllowEditing = false;
            grid.AllowFiltering = true;
            grid.AllowResizingColumns = true;
            grid.Style.HeaderStyle.BackColor = Izgled.Zelena;
            grid.Style.HeaderStyle.TextColor = Color.White;
            grid.Columns.Add(new GridNumericColumn { MappingName = "ID", HeaderText = "ID", Width = 70, NumberFormatInfo = new System.Globalization.NumberFormatInfo { NumberDecimalDigits = 0, NumberGroupSeparator = "" } });
            grid.Columns.Add(new GridTextColumn { MappingName = "Naziv", HeaderText = "Naziv", Width = 300 });
            if (Kolekcija)
                grid.Columns.Add(new GridTextColumn { MappingName = "Program", HeaderText = "Program", Width = 200 });
            else
                grid.Columns.Add(new GridNumericColumn { MappingName = "Kolekcija", HeaderText = "Kolekcija", Width = 90, NumberFormatInfo = new System.Globalization.NumberFormatInfo { NumberDecimalDigits = 0 } });
            grid.Columns.Add(new GridNumericColumn { MappingName = "Proizvoda", HeaderText = "Proizvoda", Width = 90, NumberFormatInfo = new System.Globalization.NumberFormatInfo { NumberDecimalDigits = 0 } });
            grid.SelectionChanged += (s, e) => PrikaziIzabrani();

            Controls.Add(grid);
            Controls.Add(gore);
            Controls.Add(lblStatus);
        }

        void Ucitaj(int? izaberi)
        {
            try
            {
                if (Kolekcija)
                {
                    var programi = baza.IcProgrami();
                    cboProgram.DisplayMember = "Naziv";
                    cboProgram.ValueMember = "ID";
                    cboProgram.DataSource = programi;
                }
                podaci = Kolekcija ? baza.IcKolekcije() : baza.IcProgrami();
                grid.DataSource = podaci;
                grid.View.Filter = Prikazan;
                grid.View.RefreshFilter();
                if (izaberi.HasValue)
                {
                    var red = podaci.AsEnumerable().FirstOrDefault(r => (int)r["ID"] == izaberi.Value);
                    if (red != null) grid.SelectedItem = podaci.DefaultView.Cast<DataRowView>().FirstOrDefault(v => v.Row == red);
                }
            }
            catch (Exception ex) { Izgled.Greska(this, Text, ex); }
        }

        bool Prikazan(object o)
        {
            var r = o as DataRowView;
            string t = txtTrazi.Text.Trim();
            if (r == null || t == "") return true;
            return r["Naziv"].ToString().IndexOf(t, StringComparison.CurrentCultureIgnoreCase) >= 0
                || (Kolekcija && r["Program"].ToString().IndexOf(t, StringComparison.CurrentCultureIgnoreCase) >= 0);
        }

        void PrikaziIzabrani()
        {
            var r = grid.SelectedItem as DataRowView;
            if (r == null) return;
            izabraniID = (int)r["ID"];
            txtNaziv.Text = r["Naziv"].ToString();
            if (Kolekcija) cboProgram.SelectedValue = (int)r["ProgramID"];
        }

        void Novi()
        {
            izabraniID = null;
            grid.SelectedItem = null;
            txtNaziv.Clear();
            txtNaziv.Focus();
        }

        void Sacuvaj()
        {
            string naziv = txtNaziv.Text.Trim();
            if (naziv == "") { MessageBox.Show(this, "Naziv je obavezan.", Text); return; }
            if (Kolekcija && !(cboProgram.SelectedValue is int)) { MessageBox.Show(this, "Izaberite program.", Text); return; }
            if (!izabraniID.HasValue && !prava.Upis) { MessageBox.Show(this, "Nemate pravo upisa.", Text); return; }
            if (izabraniID.HasValue && !prava.Izmena) { MessageBox.Show(this, "Nemate pravo izmene.", Text); return; }
            try
            {
                int id = Kolekcija
                    ? baza.SacuvajIcKolekciju(izabraniID, naziv, (int)cboProgram.SelectedValue)
                    : baza.SacuvajIcProgram(izabraniID, naziv);
                Ucitaj(id);
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                MessageBox.Show(this, Kolekcija ? "Ta kolekcija već postoji u izabranom programu." : "Program sa tim nazivom već postoji.", Text);
            }
            catch (Exception ex) { Izgled.Greska(this, Text, ex); }
        }

        void Obrisi()
        {
            if (!izabraniID.HasValue) return;
            if (MessageBox.Show(this, "Obrisati \"" + txtNaziv.Text + "\"?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                if (Kolekcija) baza.ObrisiIcKolekciju(izabraniID.Value); else baza.ObrisiIcProgram(izabraniID.Value);
                Novi();
                Ucitaj(null);
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                MessageBox.Show(this, (Kolekcija ? "Kolekcija" : "Program") + " se koristi (u cenovnicima, pripadnosti"
                                + (Kolekcija ? "" : " ili kolekcijama") + ") i ne može se obrisati.", Text);
            }
            catch (Exception ex) { Izgled.Greska(this, Text, ex); }
        }

        void DopuniIzProizvoda()
        {
            try
            {
                baza.OsveziProgrameIKolekcije();
                Ucitaj(izabraniID);
                MessageBox.Show(this, "Dodati su programi i kolekcije iz jela_products kojih nije bilo.", Text);
            }
            catch (Exception ex) { Izgled.Greska(this, Text, ex); }
        }
    }
}
