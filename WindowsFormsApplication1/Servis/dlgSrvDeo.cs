using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;
using DataRow = System.Data.DataRow;

namespace WindowsFormsApplication1.Servis
{
    // ZAH-010: deo (sifarnik + kartica). Dijalog nema svoju sifru prava: nasledjuje pravo ekrana iz kog se otvara.
    // Kartice: Osnovni podaci, Primenljivost, Kartica dela (stanje posle svakog kretanja, racuna se ovde), Dobavljac.
    public class dlgSrvDeo : Form
    {
        readonly ServisBaza baza = new ServisBaza();
        int? deoID;
        readonly bool smeUpis, smeIzmena;
        readonly PoljaPanel osnovno = new PoljaPanel(), dobavljac = new PoljaPanel();
        readonly Label sazetak = new Label();
        readonly SfDataGrid gridPrimena = new SfDataGrid(), gridKartica = new SfDataGrid();
        readonly TabControl tabovi = new TabControl();
        readonly Button btnSacuvaj = new Button(), btnDodajPrimenu = new Button(), btnUkloniPrimenu = new Button();
        TextBox txtSifra, txtNaziv, txtLokacija, txtMin, txtMax, txtNapomena, txtRok;
        ComboBox cboGrupa, cboJM, cboDobavljac;
        CheckBox chkKritican, chkAktivan;
        public bool Promenjeno;

        public dlgSrvDeo(int? id, bool smeUpis, bool smeIzmena)
        {
            deoID = id;
            this.smeUpis = smeUpis;
            this.smeIzmena = smeIzmena;
            Text = id.HasValue ? "Servis – Deo" : "Servis – Novi deo";
            Font = JelaTema.FontTekst;
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(960, 640);
            MinimumSize = new Size(760, 520);
            ShowInTaskbar = false;
            MinimizeBox = false;

            sazetak.Dock = DockStyle.Top;
            sazetak.Height = 34;
            sazetak.Padding = new Padding(12, 9, 0, 0);
            sazetak.BackColor = Color.White;
            sazetak.Font = JelaTema.FontNaslov;

            NapraviOsnovno();
            var strOsnovno = new TabPage("Osnovni podaci") { AutoScroll = true, Padding = new Padding(12) };
            strOsnovno.Controls.Add(osnovno);
            var strDobavljac = new TabPage("Dobavljač") { Padding = new Padding(12) };
            strDobavljac.Controls.Add(dobavljac);

            foreach (var g in new[] { gridPrimena, gridKartica })
            {
                g.Dock = DockStyle.Fill;
                g.AutoGenerateColumns = true;
                SfGridPomocnik.Stilizuj(g);
                g.AutoGeneratingColumn += SrvForma.FormatKolone;
            }
            btnDodajPrimenu.Text = "+ Dodaj";
            btnDodajPrimenu.BackColor = SrvFormat.Zelena;
            btnDodajPrimenu.Click += (s, e) => DodajPrimenu();
            btnUkloniPrimenu.Text = "Ukloni";
            btnUkloniPrimenu.UseVisualStyleBackColor = true;
            btnUkloniPrimenu.Click += (s, e) => UkloniPrimenu();
            var trakaPrimena = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40, Padding = new Padding(4, 6, 4, 0) };
            foreach (var b in new[] { btnDodajPrimenu, btnUkloniPrimenu }) { b.AutoSize = true; b.MinimumSize = new Size(80, 28); trakaPrimena.Controls.Add(b); }
            var strPrimena = new TabPage("Primenljivost");
            strPrimena.Controls.Add(gridPrimena);
            strPrimena.Controls.Add(trakaPrimena);
            var strKartica = new TabPage("Kartica dela");
            strKartica.Controls.Add(gridKartica);

            tabovi.Dock = DockStyle.Fill;
            tabovi.TabPages.AddRange(new[] { strOsnovno, strPrimena, strKartica, strDobavljac });

            var dole = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 46, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(8) };
            var btnOdustani = new Button { Text = "Zatvori", AutoSize = true, MinimumSize = new Size(96, 30), UseVisualStyleBackColor = true, DialogResult = DialogResult.Cancel };
            btnSacuvaj.Text = "Sačuvaj";
            btnSacuvaj.AutoSize = true;
            btnSacuvaj.MinimumSize = new Size(96, 30);
            btnSacuvaj.BackColor = SrvFormat.Zelena;
            btnSacuvaj.Click += (s, e) => Sacuvaj();
            dole.Controls.Add(btnOdustani);
            dole.Controls.Add(btnSacuvaj);
            CancelButton = btnOdustani;

            Controls.Add(tabovi);
            Controls.Add(sazetak);
            Controls.Add(dole);
            JelaTema.Primeni(this);
            SrvFormat.SiviIskljucena(this);
            Load += (s, e) => Ucitaj();
        }

        void NapraviOsnovno()
        {
            txtSifra = osnovno.Tekst("Šifra", "", true, false, 30);
            txtNaziv = osnovno.Tekst("Naziv", "", true, false, 150);
            cboGrupa = osnovno.Lista("Grupa", baza.GrupeDelova(), null, true);
            // jedinica mere: ponudjene vrednosti ili slobodan unos (provera u Sacuvaj)
            cboJM = new ComboBox { Width = 140, DropDownStyle = ComboBoxStyle.DropDown, MaxLength = 10, Text = "kom" };
            cboJM.Items.AddRange(new object[] { "kom", "l", "kg", "m", "par", "set", "pak" });
            osnovno.Kontrola("Jedinica mere", cboJM, true);
            txtLokacija = osnovno.Tekst("Lokacija", "", false, false, 50);
            txtMin = osnovno.Broj("Minimalna zaliha", 0, true, 0);
            txtMax = osnovno.Broj("Maksimalna zaliha", 0, true, 0);
            chkKritican = osnovno.Kvacica("Kritičan deo", false);
            chkAktivan = osnovno.Kvacica("Aktivan", true);
            txtNapomena = osnovno.Tekst("Napomena", "", false, true, 400);

            cboDobavljac = dobavljac.Lista("Dobavljač", baza.Partneri(), null, false, true, "— bez dobavljača —");
            txtRok = dobavljac.Broj("Rok isporuke (dana)", null, false, 0, 3650, 0);
        }

        void Ucitaj()
        {
            try
            {
                bool postoji = deoID.HasValue;
                tabovi.TabPages[1].Enabled = postoji;
                tabovi.TabPages[2].Enabled = postoji;
                btnSacuvaj.Enabled = postoji ? smeIzmena : smeUpis;
                btnDodajPrimenu.Enabled = btnUkloniPrimenu.Enabled = postoji && smeIzmena;
                if (!postoji) { sazetak.Text = "Novi deo – posle snimanja su dostupne primenljivost i kartica."; return; }

                var t = baza.Upit("SELECT * FROM dbo.SRV_vw_Zalihe WHERE DeoID = @D", "@D", deoID);
                if (t.Rows.Count == 0) return;
                var r = t.Rows[0];
                Text = "Servis – Deo " + r["Sifra"] + " " + r["Naziv"];
                txtSifra.Text = SrvFormat.Str(r["Sifra"]);
                txtNaziv.Text = SrvFormat.Str(r["Naziv"]);
                cboGrupa.SelectedValue = r["DeoGrupaID"];
                cboJM.Text = SrvFormat.Str(r["JM"]);
                txtLokacija.Text = SrvFormat.Str(r["Lokacija"]);
                txtMin.Text = SrvFormat.Broj(r["MinZaliha"]);
                txtMax.Text = SrvFormat.Broj(r["MaxZaliha"]);
                chkKritican.Checked = (bool)r["Kritican"];
                chkAktivan.Checked = (bool)r["Aktivno"];
                txtNapomena.Text = SrvFormat.Str(r["Napomena"]);
                if (r["DobavljacPaSifra"] != DBNull.Value) cboDobavljac.SelectedValue = r["DobavljacPaSifra"]; else cboDobavljac.SelectedIndex = 0;
                txtRok.Text = r["RokIsporukeDana"] == DBNull.Value ? "" : r["RokIsporukeDana"].ToString();

                string jm = " " + r["JM"];
                sazetak.Text = "Stanje " + SrvFormat.Broj(r["Stanje"]) + jm + "   ·   Rezervisano " + SrvFormat.Broj(r["Rezervisano"]) + jm
                             + "   ·   Raspoloživo " + SrvFormat.Broj(r["Raspolozivo"]) + jm + "   ·   Naručeno " + SrvFormat.Broj(r["Naruceno"]) + jm
                             + "   ·   Prosečna cena " + SrvFormat.Broj(r["Cena"]) + "   ·   Vrednost " + SrvFormat.Broj(r["Vrednost"]) + " RSD   ·   " + r["Status"];
                sazetak.ForeColor = SrvForma.StandardnaBoja(SrvFormat.Str(r["Status"])) ?? JelaTema.Tekst;

                gridPrimena.DataSource = baza.Upit(@"SELECT p.ID, v.Naziv AS Vrsta, ISNULL(s.Oznaka + N' – ' + s.Naziv, N'sva sredstva ove vrste') AS Sredstvo
                                                     FROM dbo.SRV_DeoPrimenljivost p JOIN dbo.SRV_VrstaSredstva v ON v.ID = p.VrstaSredstvaID
                                                     LEFT JOIN dbo.SRV_Sredstvo s ON s.ID = p.SredstvoID
                                                     WHERE p.DeoID = @D ORDER BY v.Naziv, s.Oznaka", "@D", deoID);

                // kartica: stanje posle svakog kretanja (2008 R2 nema SUM() OVER (ORDER BY ...), racuna se ovde)
                var k = baza.Upit(@"SELECT ID, Datum, Tip, DokumentBroj, Kolicina, Cena, Vrednost, Nalog, Narudzbenica, Napomena, Korisnik
                                    FROM dbo.SRV_vw_DeoKartica WHERE DeoID = @D ORDER BY Datum, ID", "@D", deoID);
                k.Columns.Add("StanjePosle", typeof(decimal)).SetOrdinal(5);
                decimal stanje = 0;
                foreach (DataRow x in k.Rows) { stanje += (decimal)x["Kolicina"]; x["StanjePosle"] = stanje; }
                var prikaz = k.Clone();
                for (int i = k.Rows.Count - 1; i >= 0; i--) prikaz.ImportRow(k.Rows[i]);   // najnovije gore
                gridKartica.DataSource = prikaz;
            }
            catch (Exception ex) { Poruke.Greska(this, ex); }
        }

        void Sacuvaj()
        {
            ValidateChildren();
            string greska = osnovno.Proveri() ?? dobavljac.Proveri();
            decimal min = PoljaPanel.BrojIz(txtMin) ?? 0, max = PoljaPanel.BrojIz(txtMax) ?? 0;
            if (greska == null && max < min) greska = "Maksimalna zaliha mora biti veća ili jednaka minimalnoj.";
            if (greska == null && cboJM.Text.Trim() == "") greska = "Unesite polje „Jedinica mere“.";
            if (greska != null) { tabovi.SelectedIndex = greska.Contains("Rok") ? 3 : 0; Poruke.Upozorenje(this, greska); return; }
            try
            {
                Cursor = Cursors.WaitCursor;
                decimal? rok = PoljaPanel.BrojIz(txtRok);
                var red = baza.ProcRed("dbo.SRV_SacuvajDeo", "@ID", deoID, "@Sifra", txtSifra.Text, "@Naziv", txtNaziv.Text,
                                       "@GrupaID", PoljaPanel.Vrednost(cboGrupa), "@JM", cboJM.Text.Trim(), "@MinZaliha", min, "@MaxZaliha", max,
                                       "@Lokacija", PoljaPanel.Tekst(txtLokacija), "@Kritican", chkKritican.Checked,
                                       "@DobavljacPaSifra", PoljaPanel.Vrednost(cboDobavljac), "@RokIsporukeDana", rok.HasValue ? (object)(int)rok.Value : null,
                                       "@Napomena", PoljaPanel.Tekst(txtNapomena), "@Aktivno", chkAktivan.Checked);
                bool bioNov = !deoID.HasValue;
                deoID = SrvFormat.Int(red["ID"]);
                Promenjeno = true;
                Ucitaj();
                if (bioNov) Poruke.Info(this, "Deo je sačuvan. Sada možete dodati primenljivost.");
                else DialogResult = DialogResult.OK;
            }
            catch (Exception ex) { Poruke.Greska(this, ex); }
            finally { Cursor = Cursors.Default; }
        }

        void DodajPrimenu()
        {
            if (!deoID.HasValue) return;
            var sredstva = baza.Upit("SELECT ID, Oznaka + N' – ' + Naziv AS Naziv, VrstaSredstvaID FROM dbo.SRV_Sredstvo WHERE Aktivno = 1 ORDER BY Oznaka");
            using (var d = new SrvDijalog("Primenljivost dela"))
            {
                var vrsta = d.Polja.Lista("Vrsta sredstva", baza.VrsteSredstava(), null, true);
                var sredstvo = d.Polja.Lista("Samo za sredstvo (prazno = sva sredstva ove vrste)", sredstva.Clone(), null, false, false, "— sva sredstva ove vrste —");
                vrsta.SelectedIndexChanged += (s, e) =>
                {
                    var v = PoljaPanel.Vrednost(vrsta);
                    var f = sredstva.Clone();
                    if (v != null) foreach (DataRow r in sredstva.Select("VrstaSredstvaID = " + SrvFormat.Int(v))) f.ImportRow(r);
                    PoljaPanel.PostaviListu(sredstvo, f, null, false, "— sva sredstva ove vrste —");
                };
                d.Snimi = () =>
                {
                    baza.Proc("dbo.SRV_DodajPrimenljivost", "@DeoID", deoID, "@VrstaID", PoljaPanel.Vrednost(vrsta), "@SredstvoID", PoljaPanel.Vrednost(sredstvo));
                    return true;
                };
                if (d.ShowDialog(this) == DialogResult.OK) Ucitaj();
            }
        }

        void UkloniPrimenu()
        {
            var r = gridPrimena.SelectedItem as DataRowView;
            if (r == null) { Poruke.Info(this, "Izaberite primenljivost."); return; }
            if (!Poruke.Potvrda(this, "Ukloniti izabranu primenljivost?")) return;
            try
            {
                baza.Proc("dbo.SRV_UkloniPrimenljivost", "@ID", r["ID"]);
                Ucitaj();
            }
            catch (Exception ex) { Poruke.Greska(this, ex); }
        }
    }
}
