using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;

namespace WindowsFormsApplication1.Servis
{
    // ZAH-010: potrebe za nabavkom (6.4): predlog (MD-R06), narudzbenice sa odobravanjem po nivoima (MD-R07, SR-13), pravila.
    public class frmSrvNabavka : SrvForma
    {
        readonly SfDataGrid gridPredlog, gridNar, gridStavke;
        readonly ComboBox cboStatus;
        readonly Button btnKreiraj, btnIzmeni, btnPosalji, btnOdobri, btnVrati, btnOtkazi;
        readonly TabControl tabovi = new TabControl();

        public frmSrvNabavka() : base("frmSrvNabavka", "Potrebe za nabavkom")
        {
            if (!Pravo) return;
            Dugme("Osveži", Uloga.Sporedna, Ucitaj);
            Dugme("Excel", Uloga.Sporedna, () => Excel(tabovi.SelectedIndex == 1 ? gridNar : gridPredlog, tabovi.SelectedIndex == 1 ? "Narudžbenice" : "Predlog nabavke"));
            Pomoc("Predlog = maksimum − raspoloživo + čeka na nalozima − naručeno.");

            gridPredlog = NoviGrid(true);
            btnKreiraj = MaloDugme("Kreiraj narudžbenice", Uloga.Glavna, Kreiraj, Upis);

            gridNar = NoviGrid();
            BojaStatusa(gridNar, "Status", StandardnaBoja);
            gridNar.SelectionChanged += (s, e) => Radi(UcitajStavke);
            Dvoklik(gridNar, Izmeni);
            gridStavke = NoviGrid();
            cboStatus = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 170, Margin = new Padding(0, 2, 12, 0) };
            cboStatus.Items.AddRange(new object[] { "Otvorene", "Sve", "Nacrt", "Čeka odobrenje", "Poručeno", "Delimično isporučeno", "Isporučeno", "Otkazano" });
            cboStatus.SelectedIndex = 0;
            cboStatus.SelectedIndexChanged += (s, e) => Osvezi(gridNar);
            var btnNova = MaloDugme("+ Nova narudžbenica", Uloga.Glavna, () => Uredi(null), Upis);
            btnIzmeni = MaloDugme("Izmeni", Uloga.Sporedna, Izmeni);
            btnPosalji = MaloDugme("Pošalji na odobrenje", Uloga.Paznja, () => Akcija("Posalji"));
            btnOdobri = MaloDugme("Odobri", Uloga.Paznja, () => Akcija("Odobri"));
            btnVrati = MaloDugme("Vrati", Uloga.Sporedna, () => Akcija("Vrati"));
            btnOtkazi = MaloDugme("Otkaži", Uloga.Sporedna, () => Akcija("Otkazi"));

            var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterDistance = 260 };
            split.Panel1.Controls.Add(gridNar);
            split.Panel2.Controls.Add(Odeljak("Stavke narudžbenice", gridStavke));

            var pravila = new TextBox { Dock = DockStyle.Fill, Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical, BackColor = System.Drawing.Color.White };
            tabovi.Dock = DockStyle.Fill;
            tabovi.TabPages.Add(Strana("Predlog", gridPredlog, btnKreiraj));
            tabovi.TabPages.Add(Strana("Narudžbenice", split, cboStatus, btnNova, btnIzmeni, btnPosalji, btnOdobri, btnVrati, btnOtkazi));
            tabovi.TabPages.Add(Strana("Pravila", pravila));
            Controls.Add(tabovi);
            Zavrsi();
            Radi(() => { Ucitaj(); pravila.Text = Pravila(); });
        }

        string Pravila()
        {
            string n1 = SrvFormat.Broj(decimal.Parse(baza.Podesavanje("Servis.Odobravanje.Nivo1")), 0);
            string n2 = SrvFormat.Broj(decimal.Parse(baza.Podesavanje("Servis.Odobravanje.Nivo2")), 0);
            return "PREDLOG NABAVKE" + Environment.NewLine
                + "• Deo je na spisku kada je raspoloživo + naručeno manje od minimuma ili kada neka količina čeka na servisnim nalozima." + Environment.NewLine
                + "• Predlog = maksimum − raspoloživo + čeka na nalozima − naručeno (ne manje od 0)." + Environment.NewLine
                + "• „Kreiraj narudžbenice“ pravi po jednu narudžbenicu (Nacrt) za svakog dobavljača iz izabranih redova (bez izbora: svi redovi)." + Environment.NewLine + Environment.NewLine
                + "ODOBRAVANJE NARUDŽBENICA (po iznosu)" + Environment.NewLine
                + "• do " + n1 + " RSD: nivo 1 – " + baza.Podesavanje("Servis.Odobravanje.GrupaNivo1") + Environment.NewLine
                + "• do " + n2 + " RSD: nivo 2 – " + baza.Podesavanje("Servis.Odobravanje.GrupaNivo2") + Environment.NewLine
                + "• preko " + n2 + " RSD: nivo 3 – " + baza.Podesavanje("Servis.Odobravanje.GrupaNivo3") + Environment.NewLine
                + "• Korisnik koji je poslao narudžbenicu ne sme da je odobri." + Environment.NewLine
                + "• Prijem se radi u „Dokumenta delova › + Novi prijem“; preko naručene količine samo uz komentar.";
        }

        void Ucitaj()
        {
            Postavi(gridPredlog, baza.Upit(@"SELECT DeoID AS ID, Sifra, Naziv, Grupa, JM, Raspolozivo, MinZaliha, MaxZaliha, CekaNaNalozima, Naruceno,
                                                   Predlog, Cena, ProcenaIznosa, Nivo, Dobavljac, Kritican
                                            FROM dbo.SRV_vw_PredlogNabavke ORDER BY Kritican DESC, Sifra"));
            btnKreiraj.Enabled = Upis && BrojRedova(gridPredlog) > 0;
            Postavi(gridNar, baza.Upit(@"SELECT ID, Broj, Datum, Dobavljac, OcekivaniDatum, Status, Stavki, Iznos, Nivo, Podneo, Odobrio, DatumOdobrenja, Napomena, Komentar
                                        FROM dbo.SRV_vw_Narudzbenica ORDER BY ID DESC"), PrikazanaNar);
            Status(BrojRedova(gridPredlog) + " delova u predlogu · " + BrojRedova(gridNar) + " narudžbenica");
            UcitajStavke();
        }

        bool PrikazanaNar(object o)
        {
            string s = SrvFormat.Str(((DataRowView)o)["Status"]), f = (string)cboStatus.SelectedItem;
            if (f == "Sve") return true;
            if (f == "Otvorene") return s != "Isporučeno" && s != "Otkazano";
            return s == f;
        }

        void UcitajStavke()
        {
            var r = Izabran(gridNar);
            Postavi(gridStavke, baza.Upit(@"SELECT s.ID, d.Sifra, d.Naziv AS Deo, s.Kolicina, d.JM, s.Cena, CAST(s.Kolicina * s.Cena AS decimal(18,2)) AS Vrednost,
                                                  s.PrimljenoKolicina AS Primljeno
                                           FROM dbo.SRV_NarudzbenicaStavka s JOIN dbo.SRV_Deo d ON d.ID = s.DeoID
                                           WHERE s.NarudzbenicaID = @N ORDER BY d.Sifra", "@N", r == null ? 0 : r["ID"]));
            string st = r == null ? "" : SrvFormat.Str(r["Status"]);
            bool imaStavki = r != null && SrvFormat.Int(r["Stavki"]) > 0;
            btnIzmeni.Enabled = st == "Nacrt" && Izmena;
            btnPosalji.Enabled = st == "Nacrt" && imaStavki && Izmena;
            btnOdobri.Enabled = st == "Čeka odobrenje" && Izmena;
            btnVrati.Enabled = st == "Čeka odobrenje" && Izmena;
            btnOtkazi.Enabled = (st == "Nacrt" || st == "Čeka odobrenje" || st == "Poručeno") && Izmena;
        }

        void Kreiraj()
        {
            if (BrojRedova(gridPredlog) == 0) return;
            var redovi = Izabrani(gridPredlog);
            if (redovi.Count <= 1) redovi = Vidljivi(gridPredlog).ToList();
            redovi = redovi.Where(r => SrvFormat.Dec(r["Predlog"]) > 0).ToList();
            if (redovi.Count == 0) { Poruke.Info(this, "Nema količina za naručivanje."); return; }
            var bez = redovi.Where(r => r["Dobavljac"] == DBNull.Value).Select(r => SrvFormat.Str(r["Sifra"])).ToList();
            if (bez.Count > 0) { Poruke.Upozorenje(this, "Delovi bez dobavljača: " + string.Join(", ", bez) + ". Unesite dobavljača u kartici dela."); return; }
            if (!Poruke.Potvrda(this, "Napraviti narudžbenice (Nacrt) za " + redovi.Count + " delova, grupisano po dobavljaču?")) return;
            var tvp = ServisBaza.Stavke();
            foreach (var r in redovi) tvp.Rows.Add(r["ID"], r["Predlog"], r["Cena"], null);
            var ds = baza.Proc("dbo.SRV_KreirajNarudzbenice", ServisBaza.Tvp("@Stavke", "dbo.SRV_StavkaTip", tvp));
            var brojevi = ds.Tables[ds.Tables.Count - 1].AsEnumerable().Select(x => SrvFormat.Str(x["Broj"]));
            Poruke.Info(this, "Napravljene narudžbenice: " + string.Join(", ", brojevi) + ". Proverite ih i pošaljite na odobrenje.");
            Ucitaj();
            tabovi.SelectedIndex = 1;
        }

        void Izmeni()
        {
            var r = Izabran(gridNar);
            if (r == null) return;
            if (SrvFormat.Str(r["Status"]) != "Nacrt") { Poruke.Info(this, "Narudžbenica se menja samo u statusu „Nacrt“."); return; }
            if (!Smem(Izmena, "Nemate pravo izmene.")) return;
            Uredi(SrvFormat.Int(r["ID"]));
        }

        void Uredi(int? id)
        {
            if (!id.HasValue && !Smem(Upis, "Nemate pravo upisa.")) return;
            using (var d = new dlgSrvStavke(VrstaStavki.Narudzbenica, id))
                if (d.ShowDialog(this) == DialogResult.OK) Ucitaj();
        }

        void Akcija(string akcija)
        {
            var r = Izabran(gridNar);
            if (r == null) return;
            string komentar = null;
            if (akcija == "Vrati" || akcija == "Otkazi")
            {
                using (var d = new SrvDijalog(akcija == "Vrati" ? "Vrati narudžbenicu u nacrt" : "Otkaži narudžbenicu", "Potvrdi"))
                {
                    var k = d.Polja.Tekst("Komentar", "", true, true, 400);
                    if (d.ShowDialog(this) != DialogResult.OK) return;
                    komentar = k.Text.Trim();
                }
            }
            else if (!Poruke.Potvrda(this, (akcija == "Odobri" ? "Odobriti" : "Poslati na odobrenje") + " narudžbenicu " + r["Broj"]
                                           + " (" + SrvFormat.Broj(r["Iznos"]) + " RSD)?")) return;
            baza.Proc("dbo.SRV_NarudzbenicaAkcija", "@ID", r["ID"], "@Akcija", akcija, "@Komentar", komentar);
            Ucitaj();
        }
    }
}
