using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;

namespace WindowsFormsApplication1.Servis
{
    // ZAH-010: stanje sredstava (6.9). Broj na plocici = broj redova liste ispod (racuna se iz istih podataka).
    public class frmSrvStanje : SrvForma
    {
        readonly SfDataGrid gridSredstva, gridServisi, gridDokumenta, gridDelovi;
        readonly TabControl tabovi = new TabControl();
        readonly FlowLayoutPanel plocice = new FlowLayoutPanel();
        string filterStatusa;

        public frmSrvStanje() : base("frmSrvStanje", "Stanje sredstava")
        {
            if (!Pravo) return;
            Dugme("Osveži", Uloga.Sporedna, Ucitaj);
            Dugme("Excel", Uloga.Sporedna, () =>
            {
                var g = new[] { gridSredstva, gridServisi, gridDokumenta, gridDelovi }[tabovi.SelectedIndex];
                Excel(g, tabovi.SelectedTab.Text);
            });
            Dugme("Otvori redovni nalog", Uloga.Paznja, OtvoriNalog, true).Enabled = Upis;
            Pomoc("Klik na pločicu prikazuje odgovarajuću listu.");

            plocice.Dock = DockStyle.Top;
            plocice.Height = 92;
            plocice.Padding = new Padding(8, 8, 8, 0);

            gridSredstva = NoviGrid();
            BojaStatusa(gridSredstva, "Status", StandardnaBoja);
            gridServisi = NoviGrid();
            BojaStatusa(gridServisi, "StatusPlana", StandardnaBoja);
            gridDokumenta = NoviGrid();
            BojaStatusa(gridDokumenta, "Status", StandardnaBoja);
            gridDelovi = NoviGrid();
            BojaStatusa(gridDelovi, "Status", StandardnaBoja);

            tabovi.Dock = DockStyle.Fill;
            tabovi.TabPages.Add(Strana("Sredstva", gridSredstva));
            tabovi.TabPages.Add(Strana("Servisi za planiranje", gridServisi));
            tabovi.TabPages.Add(Strana("Dokumenta koja ističu", gridDokumenta));
            tabovi.TabPages.Add(Strana("Delovi ispod minimuma", gridDelovi));
            Controls.Add(tabovi);
            Controls.Add(plocice);
            Zavrsi();
            Radi(() => { baza.Proc("dbo.SRV_ProveriRokove"); Ucitaj(); });
        }

        void Ucitaj()
        {
            Postavi(gridSredstva, baza.Upit(@"SELECT ID, Oznaka, Naziv, Vrsta, Status, Lokacija, BrojiloTip, Brojilo, DatumBrojila, OtvorenihNaloga
                                             FROM dbo.SRV_vw_Sredstvo WHERE Aktivno = 1 ORDER BY Oznaka"),
                    o => filterStatusa == null || SrvFormat.Str(((DataRowView)o)["Status"]) == filterStatusa);
            Postavi(gridServisi, baza.Upit(@"SELECT SredstvoID AS ID, Oznaka, NazivSredstva, PlanNaziv, StatusPlana, TrenutnoKm, SledeciKm, TrenutnoMH, SledeciMH,
                                                    SledeciDatum, OstaloDana, PoslednjiServis, OtvorenNalog, PlanID, SredstvoID
                                             FROM dbo.SRV_vw_PlanStatus WHERE Rang >= 1 ORDER BY Rang DESC, Oznaka"), null, "PlanID");
            Postavi(gridDokumenta, baza.Upit(@"SELECT ID, Oznaka, NazivSredstva, Dokument, Broj, DatumIsteka, DanaDoIsteka, Status
                                              FROM dbo.SRV_vw_DokumentStatus WHERE Status <> N'Važeći' ORDER BY ISNULL(DanaDoIsteka, -99999), Oznaka"));
            Postavi(gridDelovi, baza.Upit(@"SELECT DeoID AS ID, Sifra, Naziv, Grupa, JM, Stanje, Rezervisano, Raspolozivo, MinZaliha, Naruceno, Kritican, Status, Dobavljac
                                           FROM dbo.SRV_vw_Zalihe WHERE Aktivno = 1 AND Status <> N'Na stanju' ORDER BY Kritican DESC, Sifra"));

            // plocice: brojevi iz istih tabela kao liste
            var sredstva = ((DataTable)gridSredstva.DataSource).AsEnumerable();
            plocice.Controls.Clear();
            Plocica("U radu", sredstva.Count(r => SrvFormat.Str(r["Status"]) == "U radu"), SrvFormat.StatusZelena, () => Prikazi(0, "U radu"));
            Plocica("U kvaru", sredstva.Count(r => SrvFormat.Str(r["Status"]) == "U kvaru"), SrvFormat.StatusCrvena, () => Prikazi(0, "U kvaru"));
            Plocica("Van upotrebe", sredstva.Count(r => SrvFormat.Str(r["Status"]) == "Van upotrebe"), SrvFormat.StatusNarandzasta, () => Prikazi(0, "Van upotrebe"));
            Plocica("Servisi za planiranje", ((DataTable)gridServisi.DataSource).Rows.Count, SrvFormat.StatusNarandzasta, () => Prikazi(1, null));
            Plocica("Dokumenta koja ističu", ((DataTable)gridDokumenta.DataSource).Rows.Count, SrvFormat.StatusNarandzasta, () => Prikazi(2, null));
            Plocica("Delovi ispod minimuma", ((DataTable)gridDelovi.DataSource).Rows.Count, SrvFormat.StatusCrvena, () => Prikazi(3, null));
            Status(sredstva.Count() + " aktivnih sredstava");
        }

        void Plocica(string naslov, int broj, Color boja, Action klik)
        {
            var p = new Panel { Size = new Size(176, 76), BackColor = Color.White, Margin = new Padding(0, 0, 8, 0), Cursor = Cursors.Hand };
            var b = new Label { Text = broj.ToString(), Font = new Font("Segoe UI Semibold", 20F), ForeColor = broj > 0 ? boja : JelaTema.TekstNaslova,
                                Location = new Point(12, 4), AutoSize = true, Cursor = Cursors.Hand };
            var t = new Label { Text = naslov, Location = new Point(14, 48), AutoSize = true, Font = JelaTema.FontTekst, ForeColor = JelaTema.TekstNaslova, Cursor = Cursors.Hand };
            foreach (Control c in new Control[] { p, b, t }) c.Click += (s, e) => Radi(klik);
            p.Controls.Add(b);
            p.Controls.Add(t);
            plocice.Controls.Add(p);
        }

        void Prikazi(int kartica, string status)
        {
            filterStatusa = status;
            Osvezi(gridSredstva);
            tabovi.SelectedIndex = kartica;
        }

        void OtvoriNalog()
        {
            tabovi.SelectedIndex = 1;
            var r = Izabran(gridServisi);
            if (r == null) { Poruke.Info(this, "Izaberite servis u listi „Servisi za planiranje“."); return; }
            if (r["OtvorenNalog"] != DBNull.Value) { Poruke.Info(this, "Za ovaj plan već postoji otvoren nalog " + r["OtvorenNalog"] + "."); return; }
            if (frmSrvNalozi.NoviNalog(this, SrvFormat.Int(r["SredstvoID"]), null, SrvFormat.Int(r["PlanID"])) != null) Ucitaj();
        }
    }
}
