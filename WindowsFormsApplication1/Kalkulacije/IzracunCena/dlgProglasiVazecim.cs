using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    // ZAH-008: Proglasi vazecim - naziv (obavezan, jedinstven), vazi od (obavezno), vazi do (prazno = do daljnjeg),
    // opsti ili partner (Partnerji.PaSifra), valuta iz izvora. Ako postoji isti naziv ili preklapanje perioda
    // za istog partnera i valutu, trazi potvrdu; isti konacni cenovnik se tada prepisuje (zadrzava ID).
    public class dlgProglasiVazecim : Form
    {
        readonly IzracunCenaBaza baza = new IzracunCenaBaza();
        readonly int radniID;
        readonly string korisnik;

        readonly TextBox txtNaziv = new TextBox();
        readonly DateTimePicker dtpOd = new DateTimePicker();
        readonly DateTimePicker dtpDo = new DateTimePicker();
        readonly RadioButton rbOpsti = new RadioButton();
        readonly RadioButton rbPartner = new RadioButton();
        readonly ComboBox cboPartner = new ComboBox();

        public string Naziv { get { return txtNaziv.Text.Trim(); } }
        public int? VazeciCenovnikID { get; private set; }

        public dlgProglasiVazecim(int radniCenovnikID, string valuta, string korisnik)
        {
            radniID = radniCenovnikID;
            this.korisnik = korisnik;

            Text = "Proglasi važećim";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false; MaximizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(470, 260);
            Font = Izgled.Slova;
            BackColor = Izgled.Krem;

            int y = 16;
            Action<string, Control, int> red = (tekst, c, sirina) =>
            {
                Controls.Add(new Label { Text = tekst, Left = 14, Top = y + 3, AutoSize = true });
                c.Left = 130; c.Top = y; c.Width = sirina;
                Controls.Add(c);
                y += 34;
            };

            txtNaziv.Text = (valuta == ObracunCene.EUR ? "Devizni" : "Dinarski") + " - " + DateTime.Today.ToString("yy - MM");
            red("Naziv:", txtNaziv, 320);

            dtpOd.Format = DateTimePickerFormat.Short;
            dtpOd.Value = DateTime.Today;
            red("Važi od:", dtpOd, 120);

            dtpDo.Format = DateTimePickerFormat.Short;
            dtpDo.ShowCheckBox = true;
            dtpDo.Checked = false;
            red("Važi do:", dtpDo, 120);
            Controls.Add(new Label { Text = "(bez kvačice = do daljnjeg)", Left = 258, Top = y - 31, AutoSize = true, ForeColor = Color.Gray });

            rbOpsti.Text = "Opšti"; rbOpsti.Checked = true; rbOpsti.AutoSize = true;
            rbOpsti.Left = 130; rbOpsti.Top = y;
            rbPartner.Text = "Partner:"; rbPartner.AutoSize = true;
            rbPartner.Left = 200; rbPartner.Top = y;
            Controls.Add(new Label { Text = "Za:", Left = 14, Top = y + 3, AutoSize = true });
            Controls.Add(rbOpsti); Controls.Add(rbPartner);
            cboPartner.Left = 280; cboPartner.Top = y - 2; cboPartner.Width = 170;
            cboPartner.DropDownStyle = ComboBoxStyle.DropDown;
            cboPartner.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboPartner.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboPartner.Enabled = false;
            Controls.Add(cboPartner);
            rbPartner.CheckedChanged += (s, e) => cboPartner.Enabled = rbPartner.Checked;
            y += 34;

            Controls.Add(new Label { Text = "Valuta:", Left = 14, Top = y + 3, AutoSize = true });
            Controls.Add(new Label { Text = valuta, Left = 130, Top = y + 3, AutoSize = true, Font = new Font(Izgled.Slova, FontStyle.Bold) });

            var ok = new Button { Text = "Proglasi", Left = 274, Top = 214, Width = 90, Height = 30, Tag = "glavno" };
            var ne = new Button { Text = "Odustani", Left = 370, Top = 214, Width = 90, Height = 30, DialogResult = DialogResult.Cancel };
            ok.Click += (s, e) => Proglasi();
            Controls.Add(ok); Controls.Add(ne);
            AcceptButton = ok; CancelButton = ne;
            Izgled.Primeni(this);

            try
            {
                var p = baza.Partneri();
                cboPartner.DisplayMember = "PaNaziv";
                cboPartner.ValueMember = "PaSifra";
                cboPartner.DataSource = p;
                cboPartner.SelectedIndex = -1;
            }
            catch (Exception ex) { Izgled.Greska(this, "Partneri", ex); }
        }

        void Proglasi()
        {
            if (Naziv == "")
            {
                MessageBox.Show(this, "Naziv je obavezan.", Text);
                return;
            }
            int? paSifra = null;
            if (rbPartner.Checked)
            {
                if (!(cboPartner.SelectedValue is int))
                {
                    MessageBox.Show(this, "Izaberite partnera iz liste.", Text);
                    return;
                }
                paSifra = (int)cboPartner.SelectedValue;
            }
            DateTime? vaziDo = dtpDo.Checked ? dtpDo.Value.Date : (DateTime?)null;
            if (vaziDo.HasValue && vaziDo.Value < dtpOd.Value.Date)
            {
                MessageBox.Show(this, "Datum \"Važi do\" je pre datuma \"Važi od\".", Text);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                var r = baza.ProglasiVazecim(radniID, Naziv, dtpOd.Value.Date, vaziDo, paSifra, korisnik, false);
                Cursor = Cursors.Default;
                if (r.TrebaPotvrda)
                {
                    bool isti = r.Sporni.AsEnumerable().Any(x => x["Razlog"].ToString() == "IstiNaziv");
                    string spisak = string.Join("\n", r.Sporni.AsEnumerable().Select(x =>
                        "  " + x["Naziv"] + "  (" + Convert.ToDateTime(x["VaziOd"]).ToString("dd.MM.yyyy") + " - " +
                        (x["VaziDo"] == DBNull.Value ? "do daljnjeg" : Convert.ToDateTime(x["VaziDo"]).ToString("dd.MM.yyyy")) + ")" +
                        (x["Razlog"].ToString() == "IstiNaziv" ? "  - isti naziv" : "  - preklapa se period")));
                    string pitanje = (isti
                        ? "Cenovnik sa ovim nazivom već postoji i biće PREPISAN (zadržava ID, cene i period se zamenjuju).\n\n"
                        : "Za istog partnera i valutu već postoji važeći cenovnik u preklopljenom periodu.\n\n")
                        + spisak + "\n\nNastaviti?";
                    if (MessageBox.Show(this, pitanje, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                    Cursor = Cursors.WaitCursor;
                    r = baza.ProglasiVazecim(radniID, Naziv, dtpOd.Value.Date, vaziDo, paSifra, korisnik, true);
                    Cursor = Cursors.Default;
                }
                VazeciCenovnikID = r.VazeciCenovnikID;
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex) { Cursor = Cursors.Default; Izgled.Greska(this, Text, ex); }
        }
    }
}
