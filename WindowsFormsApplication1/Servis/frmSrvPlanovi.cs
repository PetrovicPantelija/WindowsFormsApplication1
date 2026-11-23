using System;
using System.Data;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;

namespace WindowsFormsApplication1.Servis
{
    // ZAH-010: plan odrzavanja po vrsti sredstva (SR-15) i normativ delova po servisu (MD-R09);
    // donja kartica pokazuje status plana po sredstvima (U redu > Upozorenje > Dospelo > Kasni).
    public class frmSrvPlanovi : SrvForma
    {
        readonly SfDataGrid grid, gridNormativ, gridStatus;
        readonly TextBox txtTrazi;
        readonly CheckBox chkNeaktivni;

        public frmSrvPlanovi() : base("frmSrvPlanovi", "Plan održavanja")
        {
            if (!Pravo) return;
            txtTrazi = Pretraga(() => Osvezi(grid));
            chkNeaktivni = Filter("I neaktivni", false, Ucitaj);
            Dugme("Osveži", Uloga.Sporedna, Ucitaj);
            Dugme("Excel", Uloga.Sporedna, () => Excel(grid, "Planovi održavanja"));
            Dugme("+ Dodaj", Uloga.Glavna, () => Uredi(null), true).Enabled = Upis;
            Dugme("Izmeni", Uloga.Sporedna, () => Uredi(IzabranID(grid)), true).Enabled = Izmena;
            Pomoc("Plan važi za sva sredstva svoje vrste; normativ se rezerviše pri otvaranju redovnog naloga.");

            grid = NoviGrid();
            Dvoklik(grid, () => Uredi(IzabranID(grid)));
            grid.SelectionChanged += (s, e) => Radi(UcitajDetalje);

            gridNormativ = NoviGrid();
            gridStatus = NoviGrid();
            BojaStatusa(gridStatus, "StatusPlana", StandardnaBoja);

            var tabovi = new TabControl { Dock = DockStyle.Fill };
            tabovi.TabPages.Add(Strana("Normativ delova", gridNormativ,
                MaloDugme("Dodaj deo u normativ", Uloga.Glavna, DodajNormativ, Izmena),
                MaloDugme("Ukloni", Uloga.Sporedna, UkloniNormativ, Brisanje || Izmena)));
            tabovi.TabPages.Add(Strana("Status po sredstvima", gridStatus));

            var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterDistance = 260 };
            split.Panel1.Controls.Add(grid);
            split.Panel2.Controls.Add(tabovi);
            Controls.Add(split);
            Zavrsi();
            Radi(Ucitaj);
        }

        void Ucitaj()
        {
            var t = baza.Upit(@"SELECT p.ID, v.Naziv AS Vrsta, p.Naziv, p.IntervalKm, p.IntervalMH, p.IntervalDana, p.IntervalMeseci,
                                       p.UpozorenjeKm, p.UpozorenjeMH, p.UpozorenjeDana,
                                       (SELECT COUNT(*) FROM dbo.SRV_PlanNormativ n WHERE n.PlanID = p.ID) AS Normativ,
                                       p.Napomena, p.Aktivno, p.VrstaSredstvaID
                                FROM dbo.SRV_PlanOdrzavanja p JOIN dbo.SRV_VrstaSredstva v ON v.ID = p.VrstaSredstvaID
                                WHERE p.Aktivno = 1 OR @Sve = 1 ORDER BY v.Naziv, p.Naziv", "@Sve", chkNeaktivni.Checked);
            Postavi(grid, t, o => Sadrzi((DataRowView)o, txtTrazi.Text, "Naziv", "Vrsta"));
            Status(BrojRedova(grid) + " planova");
            UcitajDetalje();
        }

        void UcitajDetalje()
        {
            int id = IzabranID(grid) ?? 0;
            Postavi(gridNormativ, baza.Upit(@"SELECT n.ID, d.Sifra, d.Naziv AS Deo, n.Kolicina, d.JM, z.Raspolozivo
                                             FROM dbo.SRV_PlanNormativ n JOIN dbo.SRV_Deo d ON d.ID = n.DeoID
                                             JOIN dbo.SRV_vw_Zalihe z ON z.DeoID = n.DeoID
                                             WHERE n.PlanID = @P ORDER BY d.Sifra", "@P", id));
            Postavi(gridStatus, baza.Upit(@"SELECT SredstvoID AS ID, Oznaka, NazivSredstva, StatusPlana, TrenutnoKm, SledeciKm, TrenutnoMH, SledeciMH,
                                                  SledeciDatum, OstaloDana, PoslednjiServis, PoslednjiNalog, OtvorenNalog
                                           FROM dbo.SRV_vw_PlanStatus WHERE PlanID = @P ORDER BY Rang DESC, Oznaka", "@P", id));
        }

        void Uredi(int? id)
        {
            if (id.HasValue ? !Smem(Izmena, "Nemate pravo izmene.") : !Smem(Upis, "Nemate pravo upisa.")) return;
            DataRowView r = id.HasValue ? Izabran(grid) : null;
            using (var d = new SrvDijalog(id.HasValue ? "Izmena plana održavanja" : "Novi plan održavanja"))
            {
                var p = d.Polja;
                var vrsta = p.Lista("Vrsta sredstva", baza.VrsteSredstava(), r == null ? null : r["VrstaSredstvaID"], true);
                var naziv = p.Tekst("Naziv", r == null ? "" : SrvFormat.Str(r["Naziv"]), true, false, 100);
                var iKm = p.Broj("Interval km", r == null ? 0 : SrvFormat.Dec(r["IntervalKm"]), false, 0);
                var iMh = p.Broj("Interval moto-sati", r == null ? 0 : SrvFormat.Dec(r["IntervalMH"]), false, 0);
                var iDana = p.Broj("Interval dana", r == null ? 0 : SrvFormat.Dec(r["IntervalDana"]), false, 0, 36500, 0);
                var iMes = p.Broj("Interval meseci", r == null ? 0 : SrvFormat.Dec(r["IntervalMeseci"]), false, 0, 1200, 0);
                var uKm = p.Broj("Upozorenje km pre", r == null ? 0 : SrvFormat.Dec(r["UpozorenjeKm"]), false, 0);
                var uMh = p.Broj("Upozorenje mh pre", r == null ? 0 : SrvFormat.Dec(r["UpozorenjeMH"]), false, 0);
                var uDana = p.Broj("Upozorenje dana pre", r == null ? 0 : SrvFormat.Dec(r["UpozorenjeDana"]), false, 0, 36500, 0);
                var napomena = p.Tekst("Napomena", r == null ? "" : SrvFormat.Str(r["Napomena"]), false, true, 400);
                var aktivno = p.Kvacica("Aktivan", r == null || (bool)r["Aktivno"]);
                Func<TextBox, decimal> b = t => PoljaPanel.BrojIz(t) ?? 0;
                d.Provera = () =>
                {
                    if (b(iKm) <= 0 && b(iMh) <= 0 && b(iDana) <= 0 && b(iMes) <= 0) return "Unesite bar jedan interval veći od 0.";
                    if (b(uKm) > b(iKm) || b(uMh) > b(iMh) || (b(uDana) > 0 && b(uDana) > b(iDana) && b(uDana) > b(iMes) * 30))
                        return "Upozorenje ne sme biti veće od intervala.";
                    return null;
                };
                d.Snimi = () =>
                {
                    baza.Proc("dbo.SRV_SacuvajPlan", "@ID", id, "@VrstaID", PoljaPanel.Vrednost(vrsta), "@Naziv", naziv.Text,
                              "@IntervalKm", b(iKm), "@IntervalMH", b(iMh), "@IntervalDana", (int)b(iDana), "@IntervalMeseci", (int)b(iMes),
                              "@UpozorenjeKm", b(uKm), "@UpozorenjeMH", b(uMh), "@UpozorenjeDana", (int)b(uDana),
                              "@Napomena", PoljaPanel.Tekst(napomena), "@Aktivno", aktivno.Checked);
                    return true;
                };
                if (d.ShowDialog(this) == DialogResult.OK) Ucitaj();
            }
        }

        void DodajNormativ()
        {
            int? plan = IzabranID(grid);
            if (!plan.HasValue) { Poruke.Info(this, "Izaberite plan."); return; }
            using (var d = new SrvDijalog("Dodaj deo u normativ"))
            {
                var deo = d.Polja.Lista("Deo", baza.Delovi(), null, true, true);
                var kol = d.Polja.Broj("Količina", null, true);
                d.Provera = () => PoljaPanel.BrojIz(kol) <= 0 ? "Polje „Količina“ mora biti veće od 0." : null;
                d.Snimi = () =>
                {
                    baza.Proc("dbo.SRV_DodajNormativ", "@PlanID", plan, "@DeoID", PoljaPanel.Vrednost(deo), "@Kolicina", PoljaPanel.BrojIz(kol));
                    return true;
                };
                if (d.ShowDialog(this) == DialogResult.OK) Ucitaj();
            }
        }

        void UkloniNormativ()
        {
            var r = Izabran(gridNormativ);
            if (r == null) { Poruke.Info(this, "Izaberite stavku normativa."); return; }
            if (!Poruke.Potvrda(this, "Ukloniti deo " + r["Sifra"] + " iz normativa?")) return;
            baza.Proc("dbo.SRV_UkloniNormativ", "@ID", r["ID"]);
            Ucitaj();
        }
    }
}
