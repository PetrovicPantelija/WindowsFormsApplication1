using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    // ZAH-008: Pripadnost kolekcijama (izracun cena) - rucna dodela maticnog podatka kolekciji
    // (IC_KolekcijaPripadnost). Ima prednost nad dodelom po nazivu iz jela_products.
    // Ne koristi staru tabelu KolekcijePripadnost. Promena odmah vazi i u opstim i radnim
    // cenovnicima; vazeci cenovnici ostaju zamrznuti.
    public class frmICPripadnost : Form
    {
        public static string code = "frmICPripadnost";
        public bool Pravo;

        readonly IzracunCenaBaza baza = new IzracunCenaBaza();
        readonly PravaForme prava;
        readonly SfDataGrid grid = new SfDataGrid();
        readonly TextBox txtTrazi = new TextBox();
        readonly ComboBox cboFilterProgram = new ComboBox();
        readonly CheckBox chkSamoRucno = new CheckBox();
        readonly ComboBox cboProgram = new ComboBox();
        readonly ComboBox cboKolekcija = new ComboBox();
        readonly Button btnDodeli = new Button(), btnUkloni = new Button();
        readonly Label lblStatus = new Label();
        DataTable podaci, kolekcije;

        public frmICPripadnost()
        {
            Text = "Pripadnost kolekcijama (izračun cena)";
            Size = new Size(1250, 720);
            StartPosition = FormStartPosition.CenterScreen;
            Font = Izgled.Slova;
            BackColor = Color.White;
            NapraviIzgled();
            Izgled.Primeni(this);

            prava = new PravaForme(code);
            Pravo = prava.Pravo;
            if (!Pravo) return;
            btnDodeli.Enabled = prava.Upis || prava.Izmena;
            btnUkloni.Enabled = prava.Brisanje || prava.Izmena;
            UcitajSifarnike();
            Ucitaj();
        }

        void NapraviIzgled()
        {
            var gore = new Panel { Dock = DockStyle.Top, Height = 84, BackColor = Izgled.Krem };
            gore.Controls.Add(new Label { Text = "Traži (ID, Code, naziv):", Left = 12, Top = 15, AutoSize = true });
            txtTrazi.SetBounds(160, 12, 220, 23);
            gore.Controls.Add(new Label { Text = "Program:", Left = 395, Top = 15, AutoSize = true });
            cboFilterProgram.SetBounds(460, 12, 200, 23);
            cboFilterProgram.DropDownStyle = ComboBoxStyle.DropDownList;
            chkSamoRucno.Text = "Samo ručno dodeljeni";
            chkSamoRucno.SetBounds(675, 13, 170, 22);
            txtTrazi.TextChanged += (s, e) => Filtriraj();
            cboFilterProgram.SelectedIndexChanged += (s, e) => Filtriraj();
            chkSamoRucno.CheckedChanged += (s, e) => Filtriraj();

            gore.Controls.Add(new Label { Text = "Dodeli označene:", Left = 12, Top = 51, AutoSize = true, Font = new Font(Izgled.Slova, FontStyle.Bold) });
            cboProgram.SetBounds(160, 48, 220, 23);
            cboProgram.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKolekcija.SetBounds(395, 48, 265, 23);
            cboKolekcija.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProgram.SelectedIndexChanged += (s, e) => PopuniKolekcije();
            btnDodeli.Text = "Dodeli kolekciju označenima";
            btnDodeli.Tag = "glavno";
            btnDodeli.SetBounds(675, 45, 210, 28);
            btnDodeli.Click += (s, e) => Sacuvaj(true);
            btnUkloni.Text = "Ukloni ručnu dodelu";
            btnUkloni.SetBounds(895, 45, 160, 28);
            btnUkloni.Click += (s, e) => Sacuvaj(false);
            gore.Controls.AddRange(new Control[] { txtTrazi, cboFilterProgram, chkSamoRucno, cboProgram, cboKolekcija, btnDodeli, btnUkloni });

            lblStatus.Dock = DockStyle.Bottom;
            lblStatus.Height = 24;
            lblStatus.BackColor = Izgled.Krem;
            lblStatus.Padding = new Padding(8, 4, 0, 0);

            grid.Dock = DockStyle.Fill;
            grid.AutoGenerateColumns = false;
            grid.AllowEditing = false;
            grid.AllowFiltering = true;
            grid.AllowResizingColumns = true;
            grid.SelectionMode = GridSelectionMode.Extended;
            grid.FrozenColumnCount = 3;
            grid.HeaderRowHeight = 42;
            grid.Style.HeaderStyle.BackColor = Izgled.Zelena;
            grid.Style.HeaderStyle.TextColor = Color.White;
            var bezGrupa = new System.Globalization.NumberFormatInfo { NumberDecimalDigits = 0, NumberGroupSeparator = "" };
            grid.Columns.Add(new GridNumericColumn { MappingName = "LargoProizvodID", HeaderText = "ID", Width = 75, NumberFormatInfo = bezGrupa });
            grid.Columns.Add(new GridTextColumn { MappingName = "Code", HeaderText = "Code", Width = 120 });
            grid.Columns.Add(new GridTextColumn { MappingName = "NazivProizvoda", HeaderText = "Naziv", Width = 200 });
            grid.Columns.Add(new GridTextColumn { MappingName = "SifraProizvodaLargo", HeaderText = "Šifra Largo", Width = 130 });
            grid.Columns.Add(new GridTextColumn { MappingName = "ProgramIzProizvoda", HeaderText = "Program (jela_products)", Width = 140 });
            grid.Columns.Add(new GridTextColumn { MappingName = "KolekcijaIzProizvoda", HeaderText = "Kolekcija (jela_products)", Width = 170 });
            grid.Columns.Add(new GridTextColumn { MappingName = "Program", HeaderText = "Program (važi)", Width = 140 });
            grid.Columns.Add(new GridTextColumn { MappingName = "Kolekcija", HeaderText = "Kolekcija (važi)", Width = 170 });
            grid.Columns.Add(new GridCheckBoxColumn { MappingName = "RucnaPripadnost", HeaderText = "Ručno", Width = 80 });
            foreach (var c in grid.Columns) c.AllowHeaderTextWrapping = true;
            grid.QueryCellStyle += (s, e) =>
            {
                var r = e.DataRow == null ? null : e.DataRow.RowData as DataRowView;
                if (r == null) return;
                if ((bool)r["RucnaPripadnost"] && (e.Column.MappingName == "Program" || e.Column.MappingName == "Kolekcija"))
                    e.Style.BackColor = Color.FromArgb(255, 226, 190);
                else if (r["KolekcijaID"] == DBNull.Value)
                    e.Style.BackColor = Color.FromArgb(225, 225, 225);   // bez kolekcije - ne ulazi u cenovnik
            };

            Controls.Add(grid);
            Controls.Add(gore);
            Controls.Add(lblStatus);
        }

        void UcitajSifarnike()
        {
            var programi = baza.IcProgrami();
            var filter = programi.Copy();
            var sve = filter.NewRow(); sve["ID"] = 0; sve["Naziv"] = "(svi)"; filter.Rows.InsertAt(sve, 0);
            cboFilterProgram.DisplayMember = "Naziv"; cboFilterProgram.ValueMember = "ID"; cboFilterProgram.DataSource = filter;
            cboProgram.DisplayMember = "Naziv"; cboProgram.ValueMember = "ID"; cboProgram.DataSource = programi;
            kolekcije = baza.IcKolekcije();
            PopuniKolekcije();
        }

        void PopuniKolekcije()
        {
            if (kolekcije == null || !(cboProgram.SelectedValue is int)) return;
            int p = (int)cboProgram.SelectedValue;
            var dv = new DataView(kolekcije, "ProgramID = " + p, "Naziv", DataViewRowState.CurrentRows);
            cboKolekcija.DisplayMember = "Naziv"; cboKolekcija.ValueMember = "ID"; cboKolekcija.DataSource = dv;
        }

        void Ucitaj()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                podaci = baza.MaticniZaPripadnost();
                grid.DataSource = podaci;
                grid.View.Filter = Prikazan;
                Filtriraj();
            }
            catch (Exception ex) { Izgled.Greska(this, Text, ex); }
            finally { Cursor = Cursors.Default; }
        }

        bool Prikazan(object o)
        {
            var r = o as DataRowView;
            if (r == null) return true;
            if (chkSamoRucno.Checked && !(bool)r["RucnaPripadnost"]) return false;
            int p = cboFilterProgram.SelectedValue is int ? (int)cboFilterProgram.SelectedValue : 0;
            if (p != 0 && (r["ProgramID"] == DBNull.Value || (int)r["ProgramID"] != p)) return false;
            string t = txtTrazi.Text.Trim();
            if (t == "") return true;
            return r["LargoProizvodID"].ToString() == t
                || r["Code"].ToString().IndexOf(t, StringComparison.CurrentCultureIgnoreCase) >= 0
                || r["NazivProizvoda"].ToString().IndexOf(t, StringComparison.CurrentCultureIgnoreCase) >= 0
                || r["SifraProizvodaLargo"].ToString().IndexOf(t, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

        void Filtriraj()
        {
            if (grid.View == null || podaci == null) return;
            grid.View.RefreshFilter();
            lblStatus.Text = "Prikazano: " + grid.View.Records.Count + " / " + podaci.Rows.Count
                + "   ·   ručno dodeljenih: " + podaci.AsEnumerable().Count(r => (bool)r["RucnaPripadnost"])
                + "   ·   bez kolekcije: " + podaci.AsEnumerable().Count(r => r["KolekcijaID"] == DBNull.Value);
        }

        void Sacuvaj(bool dodeli)
        {
            var ids = grid.SelectedItems.OfType<DataRowView>().Select(r => (int)r["LargoProizvodID"]).ToList();
            if (ids.Count == 0)
            {
                MessageBox.Show(this, "Označite matične podatke u gridu (Ctrl / Shift + klik).", Text);
                return;
            }
            int? kol = null;
            if (dodeli)
            {
                if (!(cboKolekcija.SelectedValue is int)) { MessageBox.Show(this, "Izaberite program i kolekciju.", Text); return; }
                kol = (int)cboKolekcija.SelectedValue;
            }
            string pitanje = dodeli
                ? "Dodeliti " + ids.Count + " označenih kolekciji \"" + cboKolekcija.Text + "\" (" + cboProgram.Text + ")?"
                : "Ukloniti ručnu dodelu za " + ids.Count + " označenih? Važiće opet kolekcija iz jela_products.";
            if (MessageBox.Show(this, pitanje + "\n\nPromena važi odmah i u opštim i radnim cenovnicima; važeći cenovnici se ne menjaju.",
                    Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                var r = baza.SacuvajPripadnost(ids, kol, PravaForme.Korisnik);
                Ucitaj();
                MessageBox.Show(this, "Sačuvano za " + r["Dodeljeno"] + " matičnih podataka.\nPromenjeno redova u cenovnicima: "
                                + r["PromenjenoURedovimaCenovnika"] + ".", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { Izgled.Greska(this, Text, ex); }
        }
    }
}
