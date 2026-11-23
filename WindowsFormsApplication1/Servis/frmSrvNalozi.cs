using System;
using System.Data;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;
using DataRow = System.Data.DataRow;

namespace WindowsFormsApplication1.Servis
{
    // ZAH-010: servisni nalozi (6.9). Dvoklik otvara nalog (osnovni podaci, rad, delovi iz magacina, troskovi).
    public class frmSrvNalozi : SrvForma
    {
        readonly SfDataGrid grid;
        readonly TextBox txtTrazi;
        readonly ComboBox cboStatus;

        public frmSrvNalozi() : base("frmSrvNalozi", "Servisni nalozi")
        {
            if (!Pravo) return;
            txtTrazi = Pretraga(() => { Osvezi(grid); Zbir(); });
            cboStatus = FilterLista(null, new[] { "Otvoreni", "Svi", "Otvoren", "U radu", "Čeka delove", "Čeka odobrenje", "Završen", "Otkazan" },
                                    () => { Osvezi(grid); Zbir(); });
            Dugme("Osveži", Uloga.Sporedna, Ucitaj);
            Dugme("Otvori", Uloga.Sporedna, Otvori);
            Dugme("Excel", Uloga.Sporedna, () => Excel(grid, "Servisni nalozi"));
            Dugme("+ Novi nalog", Uloga.Glavna, () => { if (NoviNalog(this, null, null, null) != null) Ucitaj(); }, true).Enabled = Upis;
            Pomoc("Završen ili otkazan nalog je zaključan.");

            grid = NoviGrid();
            BojaStatusa(grid, "Status", StandardnaBoja);
            Dvoklik(grid, Otvori);
            Controls.Add(grid);
            Zavrsi();
            Radi(Ucitaj);
        }

        void Ucitaj()
        {
            Postavi(grid, baza.Upit(@"SELECT ID, Broj, DatumOtvaranja, Sredstvo, NazivSredstva, Vrsta, Kvar, PlanNaziv, Izvrsilac, Status, Opis,
                                            Rad, Delovi, Spoljni, Ukupno, DatumZavrsetka, Kreirao
                                     FROM dbo.SRV_vw_Nalog ORDER BY ID DESC"), Prikazan);
            Zbir();
        }

        bool Prikazan(object o)
        {
            var r = (DataRowView)o;
            string s = SrvFormat.Str(r["Status"]), f = (string)cboStatus.SelectedItem;
            if (f == "Otvoreni" ? (s == "Završen" || s == "Otkazan") : (f != "Svi" && s != f)) return false;
            return Sadrzi(r, txtTrazi.Text, "Broj", "Sredstvo", "NazivSredstva", "Kvar", "Opis");
        }

        void Zbir()
        {
            decimal ukupno = 0; int n = 0;
            foreach (var r in Vidljivi(grid)) { ukupno += SrvFormat.Dec(r["Ukupno"]); n++; }
            Status(n + " naloga · trošak " + SrvFormat.Broj(ukupno) + " RSD");
        }

        void Otvori()
        {
            int? id = IzabranID(grid);
            if (!id.HasValue) return;
            using (var d = new dlgSrvNalog(id.Value, Upis, Izmena))
            {
                d.ShowDialog(this);
                if (d.Promenjeno) Ucitaj();
            }
        }

        // novi servisni nalog; koriste ga i Prijava kvarova i Stanje sredstava. Vraca broj ili null.
        public static string NoviNalog(IWin32Window vlasnik, int? sredstvoID, int? kvarID, int? planID)
        {
            var baza = new ServisBaza();
            var sredstva = baza.Sredstva();
            var planovi = baza.Upit("SELECT ID, Naziv, VrstaSredstvaID FROM dbo.SRV_PlanOdrzavanja WHERE Aktivno = 1 ORDER BY Naziv");
            var kvarovi = baza.Upit(@"SELECT ID, Broj + N' – ' + LEFT(Opis, 60) AS Naziv, SredstvoID FROM dbo.SRV_Kvar
                                      WHERE Status IN (N'Nova', N'U obradi') ORDER BY ID DESC");
            using (var d = new SrvDijalog("Novi servisni nalog", "Otvori nalog"))
            {
                var p = d.Polja;
                var cboSredstvo = p.Lista("Sredstvo", sredstva, sredstvoID, true, true);
                var cboVrsta = p.Izbor("Vrsta naloga", new[] { "Vanredni", "Redovni" }, planID.HasValue ? "Redovni" : "Vanredni", true);
                var cboPlan = p.Lista("Plan održavanja (redovni)", planovi.Clone(), null, false, false, "— bez plana —");
                var cboKvar = p.Lista("Prijava kvara", kvarovi.Clone(), null, false, false, "— bez prijave —");
                var cboIzvrsilac = p.Izbor("Izvršilac", new[] { "Interni", "Spoljni" }, "Interni", true);
                var txtBrojilo = p.Broj("Brojilo pri otvaranju", null, false, 0);
                var txtOpis = p.Tekst("Opis", "", false, true, 1000);

                // SR-11: planovi i prijave samo za izabrano sredstvo / njegovu vrstu
                Action filtriraj = () =>
                {
                    var s = PoljaPanel.Vrednost(cboSredstvo);
                    var fp = planovi.Clone(); var fk = kvarovi.Clone();
                    if (s != null)
                    {
                        int sid = SrvFormat.Int(s);
                        var sr = sredstva.Select("ID = " + sid);
                        int vrsta = sr.Length > 0 ? SrvFormat.Int(sr[0]["VrstaSredstvaID"]) : 0;
                        foreach (DataRow r in planovi.Select("VrstaSredstvaID = " + vrsta)) fp.ImportRow(r);
                        foreach (DataRow r in kvarovi.Select("SredstvoID = " + sid)) fk.ImportRow(r);
                        object b = baza.Vrednost(@"SELECT TOP 1 bt.Vrednost FROM dbo.SRV_vw_Sredstvo v
                                                   JOIN dbo.SRV_vw_BrojiloTrenutno bt ON bt.SredstvoID = v.ID AND bt.Tip = v.BrojiloTip WHERE v.ID = @S", "@S", sid);
                        txtBrojilo.Text = b == null ? "" : SrvFormat.Broj(b);
                    }
                    PoljaPanel.PostaviListu(cboPlan, fp, planID, false, "— bez plana —");
                    PoljaPanel.PostaviListu(cboKvar, fk, kvarID, false, "— bez prijave —");
                };
                cboSredstvo.SelectedIndexChanged += (s, e) => filtriraj();
                filtriraj();

                bool drugi = false;
                d.Provera = () =>
                {
                    if ((string)cboVrsta.SelectedItem == "Redovni" && PoljaPanel.Vrednost(cboPlan) == null) return "Unesite polje „Plan održavanja“.";
                    // SR-05: drugi nalog iz iste prijave samo uz potvrdu
                    var k = PoljaPanel.Vrednost(cboKvar);
                    drugi = false;
                    if (k != null)
                    {
                        object post = baza.Vrednost("SELECT TOP 1 Broj FROM dbo.SRV_Nalog WHERE KvarID = @K AND Status <> N'Otkazan' ORDER BY ID DESC", "@K", k);
                        if (post != null)
                        {
                            if (!Poruke.Potvrda(d, "Za ovu prijavu kvara već postoji servisni nalog " + post + ". Otvoriti još jedan?")) return "Nalog nije otvoren.";
                            drugi = true;
                        }
                    }
                    return null;
                };
                string broj = null;
                d.Snimi = () =>
                {
                    var r = baza.ProcRed("dbo.SRV_NalogNovi", "@SredstvoID", PoljaPanel.Vrednost(cboSredstvo), "@Vrsta", cboVrsta.SelectedItem,
                                         "@KvarID", PoljaPanel.Vrednost(cboKvar), "@PlanID", PoljaPanel.Vrednost(cboPlan),
                                         "@Izvrsilac", cboIzvrsilac.SelectedItem, "@Opis", PoljaPanel.Tekst(txtOpis),
                                         "@BrojiloOtvaranje", PoljaPanel.BrojIz(txtBrojilo), "@DozvoliDrugiNalog", drugi);
                    broj = SrvFormat.Str(r["Broj"]);
                    return true;
                };
                if (d.ShowDialog(vlasnik) != DialogResult.OK) return null;
                object nema = baza.Vrednost(@"SELECT COUNT(*) FROM dbo.SRV_NalogDeo nd JOIN dbo.SRV_Nalog n ON n.ID = nd.NalogID
                                              WHERE n.Broj = @B AND nd.Status = N'Nema na stanju'", "@B", broj);
                Poruke.Info(vlasnik, "Otvoren je servisni nalog " + broj + "."
                    + (SrvFormat.Int(nema) > 0 ? " Deo delova iz normativa nema na stanju; nalog čeka delove (vidi Potrebe za nabavkom)." : ""));
                return broj;
            }
        }
    }
}
