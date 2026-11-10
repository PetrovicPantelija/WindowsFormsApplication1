using System;
using System.Data;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;

namespace WindowsFormsApplication1.Servis
{
    // ZAH-010: korekcije zaliha (MD-R11): zahtev sa razlogom, odobrava drugi korisnik iz grupe nivoa 1 (SR-13).
    public class frmSrvKorekcije : SrvForma
    {
        readonly SfDataGrid grid;
        readonly CheckBox chkCekaju;
        readonly Button btnOdobri, btnOdbij;

        public frmSrvKorekcije() : base("frmSrvKorekcije", "Korekcije zaliha")
        {
            if (!Pravo) return;
            chkCekaju = Filter("Čekaju odobrenje", true, Ucitaj);
            Dugme("Osveži", Uloga.Sporedna, Ucitaj);
            btnOdobri = Dugme("Odobri", Uloga.Paznja, () => Odluka(true));
            btnOdbij = Dugme("Odbij", Uloga.Sporedna, () => Odluka(false));
            Dugme("Excel", Uloga.Sporedna, () => Excel(grid, "Korekcije zaliha"));
            Dugme("+ Nova korekcija", Uloga.Glavna, () => { if (NovaKorekcija(this, null)) Ucitaj(); }, true).Enabled = Upis;
            Pomoc("Korekciju odobrava drugi korisnik (ne podnosilac); odobrena ide u karticu dela.");

            grid = NoviGrid();
            BojaStatusa(grid, "Status", StandardnaBoja);
            grid.SelectionChanged += (s, e) => Dugmad();
            Controls.Add(grid);
            Zavrsi();
            Radi(Ucitaj);
        }

        void Ucitaj()
        {
            var t = baza.Upit(@"SELECT ID, Zahtev, Datum, Sifra, Deo, Korekcija, JM, StanjeSada, Razlog, Status, Trazio, Odlucio, DatumOdluke, Dokument, DeoID
                                FROM dbo.SRV_vw_Korekcija WHERE @Cekaju = 0 OR Status = N'Čeka odobrenje' ORDER BY ID DESC", "@Cekaju", chkCekaju.Checked);
            Postavi(grid, t);
            Status(BrojRedova(grid) + " zahteva");
            Dugmad();
        }

        void Dugmad()
        {
            var r = Izabran(grid);
            bool ceka = r != null && SrvFormat.Str(r["Status"]) == "Čeka odobrenje";
            btnOdobri.Enabled = ceka && Izmena;
            btnOdbij.Enabled = ceka && Izmena;
        }

        void Odluka(bool odobri)
        {
            var r = Izabran(grid);
            if (r == null) return;
            if (!Poruke.Potvrda(this, (odobri ? "Odobriti" : "Odbiti") + " korekciju " + r["Zahtev"] + " (" + r["Sifra"] + ", " + SrvFormat.Broj(r["Korekcija"]) + " " + r["JM"] + ")?")) return;
            baza.Proc("dbo.SRV_KorekcijaOdluka", "@ID", r["ID"], "@Odobri", odobri);
            Ucitaj();
        }

        // zahtev za korekciju; koristi ga i ekran Zalihe
        public static bool NovaKorekcija(IWin32Window vlasnik, int? deoID)
        {
            var baza = new ServisBaza();
            using (var d = new SrvDijalog("Zahtev za korekciju zalihe", "Pošalji zahtev"))
            {
                var deo = d.Polja.Lista("Deo", baza.Delovi(), deoID, true, true);
                var kol = d.Polja.Broj("Korekcija (+ višak, − manjak)", null, true);
                var razlog = d.Polja.Tekst("Razlog", "", true, true, 400);
                d.Provera = () => PoljaPanel.BrojIz(kol) == 0 ? "Polje „Korekcija (+ višak, − manjak)“ ne sme biti 0." : null;
                string broj = null;
                d.Snimi = () =>
                {
                    broj = SrvFormat.Str(baza.ProcRed("dbo.SRV_KorekcijaNova", "@DeoID", PoljaPanel.Vrednost(deo), "@Kolicina", PoljaPanel.BrojIz(kol),
                                                      "@Razlog", razlog.Text)["Broj"]);
                    return true;
                };
                if (d.ShowDialog(vlasnik) != DialogResult.OK) return false;
                Poruke.Info(vlasnik, "Zahtev za korekciju je poslat na odobrenje: " + broj + ".");
                return true;
            }
        }
    }
}
