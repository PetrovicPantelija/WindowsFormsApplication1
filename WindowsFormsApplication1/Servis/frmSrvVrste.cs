using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;

namespace WindowsFormsApplication1.Servis
{
    // ZAH-010: vrste sredstava sa brojilima (SR-01), dodatnim poljima (SR-02) i dokumentima.
    public class frmSrvVrste : SrvForma
    {
        readonly SfDataGrid grid, gridPolja, gridDok;
        readonly TextBox txtTrazi;
        readonly CheckBox chkNeaktivni;

        public frmSrvVrste() : base("frmSrvVrste", "Vrste sredstava")
        {
            if (!Pravo) return;
            txtTrazi = Pretraga(() => Osvezi(grid));
            chkNeaktivni = Filter("I neaktivne", false, Ucitaj);
            Dugme("Osveži", Uloga.Sporedna, Ucitaj);
            Dugme("Excel", Uloga.Sporedna, () => Excel(grid, "Vrste sredstava"));
            Dugme("+ Dodaj", Uloga.Glavna, () => UrediVrstu(null), true).Enabled = Upis;
            Dugme("Izmeni", Uloga.Sporedna, () => UrediVrstu(IzabranID(grid)), true).Enabled = Izmena;
            Pomoc("Vrsta mora imati bar jedno brojilo (KM, MH...). Izaberite vrstu da vidite njena polja i dokumenta.");

            grid = NoviGrid();
            Dvoklik(grid, () => UrediVrstu(IzabranID(grid)));
            grid.SelectionChanged += (s, e) => Radi(UcitajDetalje);

            gridPolja = NoviGrid();
            Dvoklik(gridPolja, () => UrediPolje(IzabranID(gridPolja)));
            gridDok = NoviGrid();
            Dvoklik(gridDok, () => UrediDokument(IzabranID(gridDok)));

            var tabovi = new TabControl { Dock = DockStyle.Fill };
            tabovi.TabPages.Add(Strana("Dodatna polja", gridPolja,
                MaloDugme("+ Dodaj polje", Uloga.Glavna, () => UrediPolje(null), Izmena),
                MaloDugme("Izmeni polje", Uloga.Sporedna, () => UrediPolje(IzabranID(gridPolja)), Izmena)));
            tabovi.TabPages.Add(Strana("Dokumenta", gridDok,
                MaloDugme("+ Dodaj dokument", Uloga.Glavna, () => UrediDokument(null), Izmena),
                MaloDugme("Izmeni dokument", Uloga.Sporedna, () => UrediDokument(IzabranID(gridDok)), Izmena)));

            var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterDistance = 240 };
            split.Panel1.Controls.Add(grid);
            split.Panel2.Controls.Add(tabovi);
            Controls.Add(split);
            Zavrsi();
            Radi(Ucitaj);
        }

        void Ucitaj()
        {
            var t = baza.Upit(@"SELECT v.ID, v.Naziv,
                                       STUFF((SELECT N', ' + b.Tip FROM dbo.SRV_VrstaSredstvaBrojilo b
                                              WHERE b.VrstaSredstvaID = v.ID AND b.Aktivno = 1 ORDER BY b.Redosled, b.ID FOR XML PATH('')), 1, 2, N'') AS Brojila,
                                       (SELECT COUNT(*) FROM dbo.SRV_VrstaSredstvaPolje p WHERE p.VrstaSredstvaID = v.ID AND p.Aktivno = 1) AS Polja,
                                       (SELECT COUNT(*) FROM dbo.SRV_VrstaSredstvaDokument d WHERE d.VrstaSredstvaID = v.ID AND d.Aktivno = 1) AS Dokumenata,
                                       v.Napomena, v.Aktivno
                                FROM dbo.SRV_VrstaSredstva v WHERE v.Aktivno = 1 OR @Sve = 1 ORDER BY v.Naziv",
                              "@Sve", chkNeaktivni.Checked);
            Postavi(grid, t, o => Sadrzi((DataRowView)o, txtTrazi.Text, "Naziv", "Brojila"));
            Status(BrojRedova(grid) + " vrsta sredstava");
            UcitajDetalje();
        }

        void UcitajDetalje()
        {
            int id = IzabranID(grid) ?? 0;
            Postavi(gridPolja, baza.Upit(@"SELECT ID, Naziv, Tip, JM, Opcije, Obavezno, Redosled, Aktivno FROM dbo.SRV_VrstaSredstvaPolje
                                          WHERE VrstaSredstvaID = @V ORDER BY Redosled, ID", "@V", id));
            Postavi(gridDok, baza.Upit(@"SELECT ID, Naziv, Obavezno, UpozorenjeDana, Aktivno FROM dbo.SRV_VrstaSredstvaDokument
                                        WHERE VrstaSredstvaID = @V ORDER BY Naziv", "@V", id));
        }

        void UrediVrstu(int? id)
        {
            if (id.HasValue ? !Smem(Izmena, "Nemate pravo izmene.") : !Smem(Upis, "Nemate pravo upisa.")) return;
            DataRowView r = id.HasValue ? Izabran(grid) : null;
            using (var d = new SrvDijalog(id.HasValue ? "Izmena vrste sredstva" : "Nova vrsta sredstva"))
            {
                var naziv = d.Polja.Tekst("Naziv", r == null ? "" : SrvFormat.Str(r["Naziv"]), true, false, 100);
                var brojila = d.Polja.Tekst("Brojila (npr. KM, MH)", r == null ? "" : SrvFormat.Str(r["Brojila"]), true, false, 200);
                var napomena = d.Polja.Tekst("Napomena", r == null ? "" : SrvFormat.Str(r["Napomena"]), false, true, 400);
                var aktivno = d.Polja.Kvacica("Aktivna", r == null || (bool)r["Aktivno"]);
                d.Snimi = () =>
                {
                    var tipovi = brojila.Text.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToUpperInvariant());
                    baza.Proc("dbo.SRV_SacuvajVrstu", "@ID", id, "@Naziv", naziv.Text, "@Napomena", PoljaPanel.Tekst(napomena),
                              "@Aktivno", aktivno.Checked, ServisBaza.Tvp("@Brojila", "dbo.SRV_TekstTip", ServisBaza.Tekstovi(tipovi)));
                    return true;
                };
                if (d.ShowDialog(this) == DialogResult.OK) Ucitaj();
            }
        }

        void UrediPolje(int? id)
        {
            int? vrsta = IzabranID(grid);
            if (!vrsta.HasValue) { Poruke.Info(this, "Izaberite vrstu sredstva."); return; }
            if (!Smem(Izmena, "Nemate pravo izmene.")) return;
            DataRowView r = id.HasValue ? Izabran(gridPolja) : null;
            using (var d = new SrvDijalog(id.HasValue ? "Izmena dodatnog polja" : "Novo dodatno polje"))
            {
                var naziv = d.Polja.Tekst("Naziv", r == null ? "" : SrvFormat.Str(r["Naziv"]), true, false, 100);
                var tip = d.Polja.Izbor("Tip", new[] { "Tekst", "Broj", "Lista", "Datum" }, r == null ? "Tekst" : SrvFormat.Str(r["Tip"]), true);
                var jm = d.Polja.Tekst("Jedinica mere", r == null ? "" : SrvFormat.Str(r["JM"]), false, false, 20);
                var opcije = d.Polja.Tekst("Opcije (za Listu, odvojene sa ;)", r == null ? "" : SrvFormat.Str(r["Opcije"]), false, false, 400);
                var obavezno = d.Polja.Kvacica("Obavezno", r != null && (bool)r["Obavezno"]);
                var redosled = d.Polja.Broj("Redosled", r == null ? 1 : SrvFormat.Dec(r["Redosled"]), true, 0, 999, 0);
                var aktivno = d.Polja.Kvacica("Aktivno", r == null || (bool)r["Aktivno"]);
                d.Provera = () => (string)tip.SelectedItem == "Lista" && opcije.Text.Trim() == "" ? "Za tip „Lista“ unesite polje „Opcije“." : null;
                d.Snimi = () =>
                {
                    baza.Proc("dbo.SRV_SacuvajPoljeVrste", "@ID", id, "@VrstaID", vrsta, "@Naziv", naziv.Text, "@Tip", tip.SelectedItem,
                              "@JM", PoljaPanel.Tekst(jm), "@Opcije", PoljaPanel.Tekst(opcije), "@Obavezno", obavezno.Checked,
                              "@Redosled", (int)PoljaPanel.BrojIz(redosled).Value, "@Aktivno", aktivno.Checked);
                    return true;
                };
                if (d.ShowDialog(this) == DialogResult.OK) Ucitaj();
            }
        }

        void UrediDokument(int? id)
        {
            int? vrsta = IzabranID(grid);
            if (!vrsta.HasValue) { Poruke.Info(this, "Izaberite vrstu sredstva."); return; }
            if (!Smem(Izmena, "Nemate pravo izmene.")) return;
            DataRowView r = id.HasValue ? Izabran(gridDok) : null;
            using (var d = new SrvDijalog(id.HasValue ? "Izmena dokumenta vrste" : "Novi dokument vrste"))
            {
                var naziv = d.Polja.Tekst("Naziv", r == null ? "" : SrvFormat.Str(r["Naziv"]), true, false, 100);
                var obavezno = d.Polja.Kvacica("Obavezno", r != null && (bool)r["Obavezno"]);
                var upozorenje = d.Polja.Broj("Upozorenje (dana pre isteka)", r == null ? 30 : SrvFormat.Dec(r["UpozorenjeDana"]), true, 0, 3650, 0);
                var aktivno = d.Polja.Kvacica("Aktivno", r == null || (bool)r["Aktivno"]);
                d.Snimi = () =>
                {
                    baza.Proc("dbo.SRV_SacuvajDokumentVrste", "@ID", id, "@VrstaID", vrsta, "@Naziv", naziv.Text, "@Obavezno", obavezno.Checked,
                              "@UpozorenjeDana", (int)PoljaPanel.BrojIz(upozorenje).Value, "@Aktivno", aktivno.Checked);
                    return true;
                };
                if (d.ShowDialog(this) == DialogResult.OK) Ucitaj();
            }
        }
    }
}
