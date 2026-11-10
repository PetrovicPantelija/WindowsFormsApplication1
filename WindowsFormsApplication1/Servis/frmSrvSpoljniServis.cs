using System;
using System.Data;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;

namespace WindowsFormsApplication1.Servis
{
    // ZAH-010: zahtevi za spoljni servis sa odobravanjem po nivoima iznosa (SR-12) i razdvajanjem duznosti (SR-13).
    public class frmSrvSpoljniServis : SrvForma
    {
        readonly SfDataGrid grid, gridIstorija;
        readonly CheckBox chkCekaju;
        readonly Button btnOdobri, btnOdbij, btnVrati, btnDorada, btnZatvori;

        public frmSrvSpoljniServis() : base("frmSrvSpoljniServis", "Spoljni servis")
        {
            if (!Pravo) return;
            chkCekaju = Filter("Čekaju odobrenje", false, () => Osvezi(grid));
            Dugme("Osveži", Uloga.Sporedna, Ucitaj);
            btnOdobri = Dugme("Odobri", Uloga.Paznja, () => Odluka("Odobri"));
            btnOdbij = Dugme("Odbij", Uloga.Sporedna, () => Odluka("Odbij"));
            btnVrati = Dugme("Vrati na doradu", Uloga.Sporedna, () => Odluka("Vrati"));
            btnDorada = Dugme("Izmeni (dorada)", Uloga.Sporedna, () => Uredi(IzabranID(grid)));
            btnZatvori = Dugme("Zatvori (faktura)", Uloga.Sporedna, Zatvori);
            Dugme("Excel", Uloga.Sporedna, () => Excel(grid, "Spoljni servis"));
            Dugme("+ Novi zahtev", Uloga.Glavna, () => Uredi(null), true).Enabled = Upis;
            Pomoc("Podnosilac ne sme da odobri svoj zahtev; nivo zavisi od iznosa.");

            grid = NoviGrid();
            BojaStatusa(grid, "Status", StandardnaBoja);
            grid.SelectionChanged += (s, e) => Radi(UcitajIstoriju);
            gridIstorija = NoviGrid();

            var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterDistance = 330 };
            split.Panel1.Controls.Add(grid);
            split.Panel2.Controls.Add(Odeljak("Istorija odobravanja", gridIstorija));
            Controls.Add(split);
            Zavrsi();
            Radi(Ucitaj);
        }

        void Ucitaj()
        {
            Postavi(grid, baza.Upit(@"SELECT ID, Broj, Datum, Nalog, Sredstvo, Partner, BrojPonude, Razlog, Iznos, Nivo, Status, Podneo, Odobrio,
                                            DatumOdobrenja, Faktura, GarancijaMeseci, NalogID, PaSifra
                                     FROM dbo.SRV_vw_SpoljniServis ORDER BY ID DESC"),
                    o => !chkCekaju.Checked || SrvFormat.Str(((DataRowView)o)["Status"]) == "Čeka odobrenje");
            Status(BrojRedova(grid) + " zahteva");
            UcitajIstoriju();
        }

        void UcitajIstoriju()
        {
            Postavi(gridIstorija, baza.Upit("SELECT ID, Vreme, Korisnik, Radnja, Komentar FROM dbo.SRV_SpoljniServisIstorija WHERE ZahtevID = @Z ORDER BY ID",
                                            "@Z", IzabranID(grid) ?? 0));
            var r = Izabran(grid);
            string s = r == null ? "" : SrvFormat.Str(r["Status"]);
            btnOdobri.Enabled = btnOdbij.Enabled = btnVrati.Enabled = s == "Čeka odobrenje" && Izmena;
            btnDorada.Enabled = s == "Vraćeno na doradu" && (Upis || Izmena);
            btnZatvori.Enabled = s == "Odobreno" && Izmena;
        }

        string OpisNivoa(decimal iznos)
        {
            int n = SrvFormat.Int(baza.Vrednost("SELECT dbo.SRV_NivoOdobrenja(@I)", "@I", iznos));
            if (n == 0) return "iznos mora biti veći od 0";
            return "nivo " + n + " – " + baza.Podesavanje("Servis.Odobravanje.GrupaNivo" + n);
        }

        void Uredi(int? id)
        {
            DataRowView r = id.HasValue ? Izabran(grid) : null;
            if (r != null && SrvFormat.Str(r["Status"]) != "Vraćeno na doradu") { Poruke.Info(this, "Zahtev se menja samo kada je vraćen na doradu."); return; }
            var nalozi = baza.Upit(@"SELECT ID, Broj + N' – ' + Sredstvo + N' (' + Status + N')' AS Naziv FROM dbo.SRV_vw_Nalog
                                     WHERE Zakljucan = 0 ORDER BY ID DESC");
            using (var d = new SrvDijalog(r == null ? "Novi zahtev za spoljni servis" : "Dorada zahteva " + r["Broj"], "Pošalji na odobrenje"))
            {
                var p = d.Polja;
                var nalog = p.Lista("Servisni nalog", nalozi, r == null ? null : r["NalogID"], true, true);
                var partner = p.Lista("Spoljni servis (partner)", baza.Partneri(), r == null ? null : r["PaSifra"], true, true);
                var ponuda = p.Tekst("Broj ponude", r == null ? "" : SrvFormat.Str(r["BrojPonude"]), false, false, 50);
                var iznos = p.Broj("Iznos (RSD)", r == null ? null : SrvFormat.DecN(r["Iznos"]), true);
                var nivo = p.Prikaz("Odobravanje", "");
                var razlog = p.Tekst("Razlog", r == null ? "" : SrvFormat.Str(r["Razlog"]), true, true, 1000);
                iznos.TextChanged += (s, e) => { decimal? x = PoljaPanel.BrojIz(iznos); nivo.Text = x.HasValue ? OpisNivoa(x.Value) : ""; };
                if (r != null) nivo.Text = OpisNivoa(SrvFormat.Dec(r["Iznos"]));
                d.Provera = () => !(PoljaPanel.BrojIz(iznos) > 0) ? "Polje „Iznos“ mora biti veće od 0." : null;
                string broj = null;
                d.Snimi = () =>
                {
                    var red = baza.ProcRed("dbo.SRV_ZahtevSacuvaj", "@ID", id, "@NalogID", PoljaPanel.Vrednost(nalog), "@PaSifra", PoljaPanel.Vrednost(partner),
                                           "@BrojPonude", PoljaPanel.Tekst(ponuda), "@Razlog", razlog.Text, "@Iznos", PoljaPanel.BrojIz(iznos));
                    broj = SrvFormat.Str(red["Broj"]) + " (nivo " + red["Nivo"] + ")";
                    return true;
                };
                if (d.ShowDialog(this) != DialogResult.OK) return;
                Poruke.Info(this, "Zahtev je poslat na odobrenje: " + broj + ".");
                Ucitaj();
            }
        }

        void Odluka(string akcija)
        {
            var r = Izabran(grid);
            if (r == null) return;
            string komentar = null;
            if (akcija == "Odobri")
            {
                if (!Poruke.Potvrda(this, "Odobriti zahtev " + r["Broj"] + " na " + SrvFormat.Broj(r["Iznos"]) + " RSD (nivo " + r["Nivo"] + ")?")) return;
            }
            else
            {
                using (var d = new SrvDijalog(akcija == "Odbij" ? "Odbijanje zahteva " + r["Broj"] : "Vraćanje na doradu " + r["Broj"], "Potvrdi"))
                {
                    var k = d.Polja.Tekst("Komentar", "", true, true, 1000);
                    if (d.ShowDialog(this) != DialogResult.OK) return;
                    komentar = k.Text.Trim();
                }
            }
            baza.Proc("dbo.SRV_ZahtevAkcija", "@ID", r["ID"], "@Akcija", akcija, "@Komentar", komentar);
            Ucitaj();
        }

        void Zatvori()
        {
            var r = Izabran(grid);
            if (r == null) return;
            using (var d = new SrvDijalog("Zatvaranje zahteva " + r["Broj"], "Zatvori zahtev"))
            {
                var faktura = d.Polja.Tekst("Broj fakture", "", true, false, 50);
                var garancija = d.Polja.Broj("Garancija (meseci)", null, false, 0, 600, 0);
                var komentar = d.Polja.Tekst("Komentar", "", false, true, 1000);
                d.Snimi = () =>
                {
                    decimal? g = PoljaPanel.BrojIz(garancija);
                    baza.Proc("dbo.SRV_ZahtevAkcija", "@ID", r["ID"], "@Akcija", "Zatvori", "@Komentar", PoljaPanel.Tekst(komentar),
                              "@Faktura", faktura.Text, "@GarancijaMeseci", g.HasValue ? (object)(int)g.Value : null);
                    return true;
                };
                if (d.ShowDialog(this) == DialogResult.OK) Ucitaj();
            }
        }
    }
}
