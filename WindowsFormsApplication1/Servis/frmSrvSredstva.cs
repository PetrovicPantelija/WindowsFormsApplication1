using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;
using DataRow = System.Data.DataRow;

namespace WindowsFormsApplication1.Servis
{
    // ZAH-010: sredstva (vozila, masine, viljuskari...) sa dodatnim poljima vrste (SR-02), brojilima (SR-03)
    // i dokumentima (SR-16). Sredstvo sa istorijom se arhivira, ne brise (SR-18).
    public class frmSrvSredstva : SrvForma
    {
        readonly SfDataGrid grid, gridBrojila, gridDok, gridPolja;
        readonly TextBox txtTrazi;
        readonly CheckBox chkArhiva;

        public frmSrvSredstva() : base("frmSrvSredstva", "Sredstva")
        {
            if (!Pravo) return;
            txtTrazi = Pretraga(() => Osvezi(grid));
            chkArhiva = Filter("I arhivirana", false, Ucitaj);
            Dugme("Osveži", Uloga.Sporedna, Ucitaj);
            Dugme("Excel", Uloga.Sporedna, () => Excel(grid, "Sredstva"));
            Dugme("+ Dodaj", Uloga.Glavna, () => Uredi(null), true).Enabled = Upis;
            Dugme("Izmeni", Uloga.Sporedna, () => Uredi(IzabranID(grid)), true).Enabled = Izmena;
            Pomoc("Dvoklik menja sredstvo. Sredstvo se ne briše: status „Arhivirano“.");

            grid = NoviGrid();
            BojaStatusa(grid, "Status", StandardnaBoja);
            Dvoklik(grid, () => Uredi(IzabranID(grid)));
            grid.SelectionChanged += (s, e) => Radi(UcitajDetalje);

            gridBrojila = NoviGrid();
            gridDok = NoviGrid();
            BojaStatusa(gridDok, "Status", StandardnaBoja);
            Dvoklik(gridDok, () => UrediDokument(IzabranID(gridDok)));
            gridPolja = NoviGrid();

            var tabovi = new TabControl { Dock = DockStyle.Fill };
            tabovi.TabPages.Add(Strana("Brojila", gridBrojila, MaloDugme("+ Upiši očitavanje", Uloga.Glavna, UpisiBrojilo, Izmena)));
            tabovi.TabPages.Add(Strana("Dokumenta", gridDok,
                MaloDugme("+ Dodaj dokument", Uloga.Glavna, () => UrediDokument(null), Izmena),
                MaloDugme("Izmeni / produži", Uloga.Sporedna, () => UrediDokument(IzabranID(gridDok)), Izmena)));
            tabovi.TabPages.Add(Strana("Dodatna polja", gridPolja));

            var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterDistance = 300 };
            split.Panel1.Controls.Add(grid);
            split.Panel2.Controls.Add(tabovi);
            Controls.Add(split);
            Zavrsi();
            Radi(Ucitaj);
        }

        void Ucitaj()
        {
            var t = baza.Upit(@"SELECT ID, Oznaka, Naziv, Vrsta, Proizvodjac, Model, Godina, SerijskiBroj, Lokacija, Status,
                                       BrojiloTip, Brojilo, DatumBrojila, OtvorenihNaloga, Napomena, VrstaSredstvaID, VoziloID, Aktivno
                                FROM dbo.SRV_vw_Sredstvo WHERE Aktivno = 1 OR @Sve = 1 ORDER BY Oznaka", "@Sve", chkArhiva.Checked);
            Postavi(grid, t, o => Sadrzi((DataRowView)o, txtTrazi.Text, "Oznaka", "Naziv", "Vrsta", "Lokacija", "SerijskiBroj"));
            Status(BrojRedova(grid) + " sredstava");
            UcitajDetalje();
        }

        void UcitajDetalje()
        {
            int id = IzabranID(grid) ?? 0;
            Postavi(gridBrojila, baza.Upit(@"SELECT ID, Tip, Vrednost AS Brojilo, Datum, Izvor, Razlog, Kreirao AS Korisnik
                                            FROM dbo.SRV_SredstvoBrojilo WHERE SredstvoID = @S ORDER BY Datum DESC, ID DESC", "@S", id));
            Postavi(gridDok, baza.Upit(@"SELECT ID, Dokument, Broj, DatumIzdavanja, DatumIsteka, DanaDoIsteka, Status, Prilog, Napomena, VrstaDokumentaID
                                        FROM dbo.SRV_vw_DokumentStatus WHERE SredstvoID = @S ORDER BY Dokument", "@S", id));
            Postavi(gridPolja, baza.Upit(@"SELECT p.ID, p.Naziv, sp.Vrednost, p.JM, p.Obavezno
                                          FROM dbo.SRV_Sredstvo s JOIN dbo.SRV_VrstaSredstvaPolje p ON p.VrstaSredstvaID = s.VrstaSredstvaID AND p.Aktivno = 1
                                          LEFT JOIN dbo.SRV_SredstvoPolje sp ON sp.SredstvoID = s.ID AND sp.PoljeID = p.ID
                                          WHERE s.ID = @S ORDER BY p.Redosled, p.ID", "@S", id));
        }

        void Uredi(int? id)
        {
            if (id.HasValue ? !Smem(Izmena, "Nemate pravo izmene.") : !Smem(Upis, "Nemate pravo upisa.")) return;
            DataRowView r = id.HasValue ? Izabran(grid) : null;
            int vrsta;
            if (r != null) vrsta = SrvFormat.Int(r["VrstaSredstvaID"]);
            else
            {
                // novo sredstvo: prvo vrsta, jer od nje zavise dodatna polja i brojilo
                using (var d = new SrvDijalog("Novo sredstvo – vrsta", "Dalje"))
                {
                    var cbo = d.Polja.Lista("Vrsta sredstva", baza.VrsteSredstava(), null, true);
                    if (d.ShowDialog(this) != DialogResult.OK) return;
                    vrsta = SrvFormat.Int(PoljaPanel.Vrednost(cbo));
                }
            }

            var polja = baza.Upit(@"SELECT p.ID, p.Naziv, p.Tip, p.JM, p.Opcije, p.Obavezno, sp.Vrednost
                                    FROM dbo.SRV_VrstaSredstvaPolje p
                                    LEFT JOIN dbo.SRV_SredstvoPolje sp ON sp.PoljeID = p.ID AND sp.SredstvoID = @S
                                    WHERE p.VrstaSredstvaID = @V AND p.Aktivno = 1 ORDER BY p.Redosled, p.ID",
                                  "@S", id ?? 0, "@V", vrsta);
            string nazivVrste = SrvFormat.Str(baza.Vrednost("SELECT Naziv FROM dbo.SRV_VrstaSredstva WHERE ID = @V", "@V", vrsta));
            string brojilo = SrvFormat.Str(baza.Vrednost("SELECT TOP 1 Tip FROM dbo.SRV_VrstaSredstvaBrojilo WHERE VrstaSredstvaID = @V AND Aktivno = 1 ORDER BY Redosled, ID", "@V", vrsta));

            using (var d = new SrvDijalog(id.HasValue ? "Izmena sredstva" : "Novo sredstvo"))
            {
                var p = d.Polja;
                var oznaka = p.Tekst("Oznaka (inventarski broj)", r == null ? "" : SrvFormat.Str(r["Oznaka"]), true, false, 30);
                var naziv = p.Tekst("Naziv", r == null ? "" : SrvFormat.Str(r["Naziv"]), true, false, 150);
                p.Prikaz("Vrsta sredstva", nazivVrste);
                var proizvodjac = p.Tekst("Proizvođač", r == null ? "" : SrvFormat.Str(r["Proizvodjac"]), false, false, 100);
                var model = p.Tekst("Model", r == null ? "" : SrvFormat.Str(r["Model"]), false, false, 100);
                var godina = p.Broj("Godina", r == null ? null : SrvFormat.DecN(r["Godina"]), false, 1900, 2100, 0);
                if (godina.Text != "") godina.Text = godina.Text.Replace(".", "");
                var serijski = p.Tekst("Serijski broj", r == null ? "" : SrvFormat.Str(r["SerijskiBroj"]), false, false, 100);
                var lokacija = p.Tekst("Matična lokacija", r == null ? "" : SrvFormat.Str(r["Lokacija"]), false, false, 100);
                var statusS = p.Izbor("Status", new[] { "U radu", "U kvaru", "Van upotrebe", "Arhivirano" }, r == null ? "U radu" : SrvFormat.Str(r["Status"]), true);
                var vozilo = p.Broj("Veza sa vozilom (ID iz isporuka)", r == null ? null : SrvFormat.DecN(r["VoziloID"]), false, 0, 999999999, 0);
                TextBox pocetno = null;
                if (!id.HasValue && brojilo != "") pocetno = p.Broj("Početno brojilo (" + brojilo + ")", null, false, 0);

                // dodatna polja vrste
                var kontrole = new Dictionary<int, Control>();
                foreach (DataRow pr in polja.Rows)
                {
                    string ime = SrvFormat.Str(pr["Naziv"]) + (SrvFormat.Str(pr["JM"]) != "" ? " (" + pr["JM"] + ")" : "");
                    bool obav = (bool)pr["Obavezno"];
                    string v = SrvFormat.Str(pr["Vrednost"]);
                    switch (SrvFormat.Str(pr["Tip"]))
                    {
                        case "Broj":
                            decimal x;
                            kontrole[SrvFormat.Int(pr["ID"])] = p.Broj(ime, SrvFormat.ProbajBroj(v, out x) ? x : (decimal?)null, obav);
                            break;
                        case "Lista":
                            kontrole[SrvFormat.Int(pr["ID"])] = p.Izbor(ime, SrvFormat.Str(pr["Opcije"]).Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries), v == "" ? null : v, obav);
                            break;
                        case "Datum":
                            DateTime dt;
                            bool ima = DateTime.TryParseExact(v, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                            kontrole[SrvFormat.Int(pr["ID"])] = p.Datum(ime, ima ? dt : (DateTime?)null, obav);
                            break;
                        default:
                            kontrole[SrvFormat.Int(pr["ID"])] = p.Tekst(ime, v, obav, false, 400);
                            break;
                    }
                }
                var napomena = p.Tekst("Napomena", r == null ? "" : SrvFormat.Str(r["Napomena"]), false, true, 400);

                d.Provera = () =>
                {
                    if ((string)statusS.SelectedItem == "Arhivirano" && r != null && SrvFormat.Int(r["OtvorenihNaloga"]) > 0)
                        return "Sredstvo ima otvoren servisni nalog i ne može se arhivirati.";
                    return null;
                };
                d.Snimi = () =>
                {
                    var vrednosti = ServisBaza.PoljaVrednosti();
                    foreach (var k in kontrole)
                    {
                        string v = null;
                        var tb = k.Value as TextBox; var cb = k.Value as ComboBox; var dp = k.Value as DateTimePicker;
                        if (tb != null) v = tb.Text.Trim();
                        else if (cb != null) v = cb.SelectedItem as string;
                        else if (dp != null) { var dd = PoljaPanel.DatumIz(dp); v = dd.HasValue ? dd.Value.ToString("yyyy-MM-dd") : null; }
                        vrednosti.Rows.Add(k.Key, v);
                    }
                    decimal? god = PoljaPanel.BrojIz(godina), voz = PoljaPanel.BrojIz(vozilo);
                    baza.Proc("dbo.SRV_SacuvajSredstvo", "@ID", id, "@Oznaka", oznaka.Text, "@Naziv", naziv.Text, "@VrstaID", vrsta,
                              "@Proizvodjac", PoljaPanel.Tekst(proizvodjac), "@Model", PoljaPanel.Tekst(model),
                              "@Godina", god.HasValue ? (object)(int)god.Value : null, "@SerijskiBroj", PoljaPanel.Tekst(serijski),
                              "@Lokacija", PoljaPanel.Tekst(lokacija), "@Status", statusS.SelectedItem,
                              "@VoziloID", voz.HasValue ? (object)(int)voz.Value : null, "@Napomena", PoljaPanel.Tekst(napomena),
                              ServisBaza.Tvp("@Polja", "dbo.SRV_PoljeVrednostTip", vrednosti),
                              "@PocetnoBrojilo", pocetno == null ? null : PoljaPanel.BrojIz(pocetno));
                    return true;
                };
                if (d.ShowDialog(this) == DialogResult.OK) Ucitaj();
            }
        }

        void UpisiBrojilo()
        {
            var r = Izabran(grid);
            if (r == null) { Poruke.Info(this, "Izaberite sredstvo."); return; }
            if (!Smem(Izmena, "Nemate pravo izmene.")) return;
            var tipovi = baza.Upit(@"SELECT Tip AS ID, Tip AS Naziv FROM dbo.SRV_VrstaSredstvaBrojilo
                                     WHERE VrstaSredstvaID = @V AND Aktivno = 1 ORDER BY Redosled, ID", "@V", r["VrstaSredstvaID"]);
            using (var d = new SrvDijalog("Očitavanje brojila – " + r["Oznaka"]))
            {
                var tip = d.Polja.Lista("Brojilo", tipovi, r["BrojiloTip"], true);
                d.Polja.Prikaz("Poslednje očitavanje", SrvFormat.Broj(r["Brojilo"]) + (r["DatumBrojila"] == DBNull.Value ? "" : " (" + SrvFormat.Datum((DateTime)r["DatumBrojila"]) + ")"));
                var vrednost = d.Polja.Broj("Vrednost", null, true, 0);
                var datum = d.Polja.Datum("Datum očitavanja", DateTime.Today);
                datum.MaxDate = DateTime.Today;
                var razlog = d.Polja.Tekst("Razlog (ako brojilo ide unazad)", "", false, false, 200);
                d.Snimi = () =>
                {
                    baza.Proc("dbo.SRV_UpisiBrojilo", "@SredstvoID", r["ID"], "@Tip", PoljaPanel.Vrednost(tip), "@Vrednost", PoljaPanel.BrojIz(vrednost),
                              "@Datum", datum.Value.Date, "@Razlog", PoljaPanel.Tekst(razlog));
                    return true;
                };
                if (d.ShowDialog(this) == DialogResult.OK) Ucitaj();
            }
        }

        void UrediDokument(int? id)
        {
            var s = Izabran(grid);
            if (s == null) { Poruke.Info(this, "Izaberite sredstvo."); return; }
            if (!Smem(Izmena, "Nemate pravo izmene.")) return;
            DataRowView r = id.HasValue ? Izabran(gridDok) : null;
            var vrste = baza.Upit("SELECT ID, Naziv FROM dbo.SRV_VrstaSredstvaDokument WHERE VrstaSredstvaID = @V AND Aktivno = 1 ORDER BY Naziv",
                                  "@V", s["VrstaSredstvaID"]);
            if (vrste.Rows.Count == 0) { Poruke.Info(this, "Za vrstu ovog sredstva nisu definisana dokumenta (Vrste sredstava › Dokumenta)."); return; }
            using (var d = new SrvDijalog((id.HasValue ? "Dokument – " : "Novi dokument – ") + s["Oznaka"]))
            {
                var vrsta = d.Polja.Lista("Dokument", vrste, r == null ? null : r["VrstaDokumentaID"], true);
                var broj = d.Polja.Tekst("Broj dokumenta", r == null ? "" : SrvFormat.Str(r["Broj"]), false, false, 50);
                var izdat = d.Polja.Datum("Datum izdavanja", r == null || r["DatumIzdavanja"] == DBNull.Value ? DateTime.Today : (DateTime)r["DatumIzdavanja"]);
                var istice = d.Polja.Datum("Datum isteka", r == null || r["DatumIsteka"] == DBNull.Value ? DateTime.Today.AddYears(1) : (DateTime)r["DatumIsteka"]);
                var prilog = d.Polja.Tekst("Prilog (putanja)", r == null ? "" : SrvFormat.Str(r["Prilog"]), false, false, 400);
                var napomena = d.Polja.Tekst("Napomena", r == null ? "" : SrvFormat.Str(r["Napomena"]), false, true, 400);
                d.Provera = () => istice.Value.Date < izdat.Value.Date ? "Datum isteka ne sme biti pre datuma izdavanja." : null;
                d.Snimi = () =>
                {
                    baza.Proc("dbo.SRV_SacuvajDokumentSredstva", "@ID", id, "@SredstvoID", s["ID"], "@VrstaDokumentaID", PoljaPanel.Vrednost(vrsta),
                              "@Broj", PoljaPanel.Tekst(broj), "@DatumIzdavanja", izdat.Value.Date, "@DatumIsteka", istice.Value.Date,
                              "@Prilog", PoljaPanel.Tekst(prilog), "@Napomena", PoljaPanel.Tekst(napomena), "@Aktivno", true);
                    return true;
                };
                if (d.ShowDialog(this) == DialogResult.OK) UcitajDetalje();
            }
        }
    }
}
