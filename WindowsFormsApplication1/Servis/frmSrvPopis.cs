using System;
using System.Data;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;

namespace WindowsFormsApplication1.Servis
{
    // ZAH-010: popis zaliha (6.5, MD-R10). Razlika se vidi cim se unese prebrojano; zaliha se menja tek
    // kada razlike odobri drugi korisnik iz grupe nivoa 1. Dva otvorena popisa za iste delove nisu dozvoljena.
    public class frmSrvPopis : SrvForma
    {
        readonly SfDataGrid grid, gridStavke;
        readonly CheckBox chkRazlika;
        readonly Button btnUnesi, btnPosalji, btnOdobri, btnVrati, btnStorniraj;

        public frmSrvPopis() : base("frmSrvPopis", "Popis zaliha")
        {
            if (!Pravo) return;
            btnUnesi = Dugme("Unesi prebrojano", Uloga.Sporedna, Unesi);
            chkRazlika = Filter("Samo sa razlikom", false, () => Osvezi(gridStavke));
            btnPosalji = Dugme("Pošalji na odobrenje", Uloga.Paznja, () => Akcija("Posalji", "Poslati popis na odobrenje?"));
            btnOdobri = Dugme("Odobri razlike", Uloga.Paznja, () => Akcija("Odobri", "Odobriti razlike? Zaliha se menja po prebrojanim količinama."));
            btnVrati = Dugme("Vrati na brojanje", Uloga.Sporedna, () => Akcija("Vrati", "Vratiti popis na brojanje?"));
            btnStorniraj = Dugme("Storniraj", Uloga.Sporedna, () => Akcija("Storniraj", "Stornirati popis? Zaliha se ne menja."));
            Dugme("Excel", Uloga.Sporedna, () => Excel(gridStavke, "Popisna lista"));
            Dugme("+ Novi popis", Uloga.Glavna, NoviPopis, true).Enabled = Upis;
            Pomoc("Dvoklik na stavku: unos prebrojane količine. Neprebrojana stavka ne menja zalihu.");

            grid = NoviGrid();
            BojaStatusa(grid, "Status", StandardnaBoja);
            grid.SelectionChanged += (s, e) => Radi(UcitajStavke);
            gridStavke = NoviGrid();
            Dvoklik(gridStavke, Unesi);

            var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterDistance = 220 };
            split.Panel1.Controls.Add(grid);
            split.Panel2.Controls.Add(Odeljak("Popisna lista", gridStavke));
            Controls.Add(split);
            Zavrsi();
            Radi(Ucitaj);
        }

        void Ucitaj()
        {
            Postavi(grid, baza.Upit(@"SELECT ID, Broj, Datum, Grupa, Status, Stavki, Prebrojano, SaRazlikom, ManjakRSD, VisakRSD, Podneo, Odobrio, Napomena
                                     FROM dbo.SRV_vw_Popis ORDER BY ID DESC"));
            Status(BrojRedova(grid) + " popisa");
            UcitajStavke();
        }

        string StatusPopisa()
        {
            var r = Izabran(grid);
            return r == null ? "" : SrvFormat.Str(r["Status"]);
        }

        void UcitajStavke()
        {
            Postavi(gridStavke, baza.Upit(@"SELECT ID, Sifra, Deo, Lokacija, JM, Knjigovodstveno, Prebrojano, Razlika, Cena, VrednostRazlike, Napomena
                                           FROM dbo.SRV_vw_PopisStavka WHERE PopisID = @P ORDER BY Sifra", "@P", IzabranID(grid) ?? 0),
                    o => !chkRazlika.Checked || SrvFormat.Dec(((DataRowView)o)["Razlika"]) != 0);
            string st = StatusPopisa();
            btnUnesi.Enabled = st == "U toku" && (Upis || Izmena);
            btnPosalji.Enabled = st == "U toku" && (Upis || Izmena);
            btnOdobri.Enabled = st == "Čeka odobrenje" && Izmena;
            btnVrati.Enabled = st == "Čeka odobrenje" && Izmena;
            btnStorniraj.Enabled = (st == "U toku" || st == "Čeka odobrenje") && Izmena;
        }

        void NoviPopis()
        {
            using (var d = new SrvDijalog("Novi popis"))
            {
                var datum = d.Polja.Datum("Datum", DateTime.Today);
                var grupa = d.Polja.Lista("Grupa delova (prazno = svi delovi)", baza.GrupeDelova(), null, false, false, "— svi delovi —");
                var napomena = d.Polja.Tekst("Napomena", "", false, true, 400);
                string broj = null;
                d.Snimi = () =>
                {
                    broj = SrvFormat.Str(baza.ProcRed("dbo.SRV_PopisNovi", "@Datum", datum.Value.Date, "@GrupaID", PoljaPanel.Vrednost(grupa),
                                                      "@Napomena", PoljaPanel.Tekst(napomena))["Broj"]);
                    return true;
                };
                if (d.ShowDialog(this) != DialogResult.OK) return;
                Poruke.Info(this, "Napravljen je popis " + broj + ". Unesite prebrojane količine.");
                Ucitaj();
            }
        }

        void Unesi()
        {
            var r = Izabran(gridStavke);
            if (r == null) return;
            if (StatusPopisa() != "U toku") { Poruke.Info(this, "Popis nije u toku; unos prebrojane količine nije moguć."); return; }
            if (!Smem(Upis || Izmena, "Nemate pravo upisa.")) return;
            using (var d = new SrvDijalog("Prebrojana količina"))
            {
                d.Polja.Prikaz("Deo", r["Sifra"] + " " + r["Deo"]);
                d.Polja.Prikaz("Knjigovodstveno", SrvFormat.Broj(r["Knjigovodstveno"]) + " " + r["JM"]);
                var preb = d.Polja.Broj("Prebrojano", SrvFormat.DecN(r["Prebrojano"]), true, 0, 999999999);
                var napomena = d.Polja.Tekst("Napomena", SrvFormat.Str(r["Napomena"]), false, false, 400);
                d.Snimi = () =>
                {
                    baza.Proc("dbo.SRV_PopisUnesi", "@StavkaID", r["ID"], "@Prebrojano", PoljaPanel.BrojIz(preb), "@Napomena", PoljaPanel.Tekst(napomena));
                    return true;
                };
                if (d.ShowDialog(this) == DialogResult.OK) Ucitaj();
            }
        }

        void Akcija(string akcija, string pitanje)
        {
            var r = Izabran(grid);
            if (r == null) return;
            if (!Poruke.Potvrda(this, pitanje + " (" + r["Broj"] + ")")) return;
            baza.Proc("dbo.SRV_PopisAkcija", "@ID", r["ID"], "@Akcija", akcija);
            Ucitaj();
        }
    }
}
