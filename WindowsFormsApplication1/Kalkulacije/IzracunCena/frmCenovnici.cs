using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Events;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    // ZAH-008: Pregled cenovnika - lista vazecih cenovnika (naziv, valuta, partner ili opsti, period,
    // status, ko i kada). Otvori (samo citanje), Izvoz u Excel, Kopiraj u novu radnu kopiju.
    public partial class frmCenovnici : Form
    {
        public static string code = "frmCenovnici";
        public bool Pravo;

        readonly IzracunCenaBaza baza = new IzracunCenaBaza();
        readonly string korisnik;

        public frmCenovnici()
        {
            InitializeComponent();
            Izgled.Primeni(this);
            var prava = new PravaForme(code);
            Pravo = prava.Pravo;
            if (!Pravo) return;
            korisnik = PravaForme.Korisnik;
            btnKopiraj.Enabled = prava.Upis;
            PodesiGrid();
            cboPrikaz.SelectedIndex = 0;   // ucitava listu
        }

        void PodesiGrid()
        {
            grid.Style.HeaderStyle.BackColor = Izgled.Zelena;
            grid.Style.HeaderStyle.TextColor = Color.White;
            grid.Columns.Add(new GridTextColumn { MappingName = "Naziv", HeaderText = "Naziv", Width = 230 });
            grid.Columns.Add(new GridTextColumn { MappingName = "Vrsta", HeaderText = "Vrsta", Width = 70 });
            grid.Columns.Add(new GridTextColumn { MappingName = "Valuta", HeaderText = "Valuta", Width = 60 });
            grid.Columns.Add(new GridTextColumn { MappingName = "OpstiIliPartner", HeaderText = "Partner / opšti", Width = 200 });
            grid.Columns.Add(new GridDateTimeColumn { MappingName = "VaziOd", HeaderText = "Važi od", Width = 90, Format = "dd.MM.yyyy" });
            grid.Columns.Add(new GridDateTimeColumn { MappingName = "VaziDo", HeaderText = "Važi do", Width = 90, Format = "dd.MM.yyyy", AllowNull = true, NullDisplayText = "do daljnjeg" });
            grid.Columns.Add(new GridTextColumn { MappingName = "Status", HeaderText = "Status", Width = 80 });
            grid.Columns.Add(new GridNumericColumn { MappingName = "BrojStavki", HeaderText = "Redova", Width = 70, NumberFormatInfo = new System.Globalization.NumberFormatInfo { NumberDecimalDigits = 0 } });
            grid.Columns.Add(new GridTextColumn { MappingName = "Kreirao", HeaderText = "Napravio", Width = 100 });
            grid.Columns.Add(new GridDateTimeColumn { MappingName = "DatumKreiranja", HeaderText = "Napravljen", Width = 120, Format = "dd.MM.yyyy HH:mm" });
            grid.Columns.Add(new GridTextColumn { MappingName = "Izmenio", HeaderText = "Izmenio", Width = 100 });
            grid.Columns.Add(new GridDateTimeColumn { MappingName = "DatumIzmene", HeaderText = "Izmenjen", Width = 120, Format = "dd.MM.yyyy HH:mm", AllowNull = true });
            grid.QueryCellStyle += (s, e) =>
            {
                if (e.Column == null || e.Column.MappingName != "Status" || e.DataRow == null) return;
                var r = e.DataRow.RowData as DataRowView;
                if (r == null) return;
                string st = r["Status"].ToString();
                if (st == "Važeći") e.Style.TextColor = Color.FromArgb(30, 130, 60);
                else if (st == "Istekao") e.Style.TextColor = Color.Gray;
            };
        }

        void Ucitaj()
        {
            try
            {
                grid.DataSource = baza.Cenovnici(cboPrikaz.SelectedIndex == 0 ? "Vazeci" : null);
                grid.Columns["Vrsta"].Visible = cboPrikaz.SelectedIndex != 0;
            }
            catch (Exception ex) { Izgled.Greska(this, "Učitavanje", ex); }
        }

        DataRowView Izabrani
        {
            get { return grid.SelectedItem as DataRowView; }
        }

        void cboPrikaz_SelectedIndexChanged(object sender, EventArgs e) { Ucitaj(); }

        void btnOsvezi_Click(object sender, EventArgs e) { Ucitaj(); }

        void grid_CellDoubleClick(object sender, CellClickEventArgs e) { Otvori(); }

        void btnOtvori_Click(object sender, EventArgs e) { Otvori(); }

        void Otvori()
        {
            var r = Izabrani;
            if (r == null) return;
            var f = new frmCenovnikPregled((int)r["CenovnikID"]);
            f.Show();
        }

        void btnIzvoz_Click(object sender, EventArgs e)
        {
            var r = Izabrani;
            if (r == null) return;
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "Excel (*.xlsx)|*.xlsx";
                dlg.FileName = IzvozExcel.ImeFajla(r["Naziv"].ToString(), DateTime.Now);
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    Cursor = Cursors.WaitCursor;
                    int id = (int)r["CenovnikID"];
                    var stavke = baza.Stavke(id);
                    baza.Parametri(id).Primeni(stavke);
                    IzvozExcel.Izvezi(dlg.FileName, baza.Zaglavlje(id), stavke, false);
                    baza.UpisiPresek(id, dlg.FileName, korisnik);
                    Cursor = Cursors.Default;
                    MessageBox.Show(this, "Izvezeno: " + dlg.FileName, "Izvoz u Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { Cursor = Cursors.Default; Izgled.Greska(this, "Izvoz u Excel", ex); }
            }
        }

        void btnKopiraj_Click(object sender, EventArgs e)
        {
            var r = Izabrani;
            if (r == null) return;
            string naziv = Izgled.Unos(this, "Kopiraj u novu radnu kopiju", "Naziv radne kopije:",
                                       "Radna - " + r["Naziv"] + " - " + DateTime.Now.ToString("dd.MM.yyyy HH.mm"));
            if (naziv == null) return;
            try
            {
                int id = baza.NapraviRadniCenovnik((int)r["CenovnikID"], naziv, korisnik);
                if (MessageBox.Show(this, "Radna kopija \"" + naziv + "\" je napravljena. Otvoriti je u Pripremi cenovnika?",
                        Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var f = new frmPripremaCenovnika(id);
                    if (f.Pravo) f.Show();
                }
            }
            catch (Exception ex) { Izgled.Greska(this, "Kopiraj u novu radnu kopiju", ex); }
        }
    }
}
