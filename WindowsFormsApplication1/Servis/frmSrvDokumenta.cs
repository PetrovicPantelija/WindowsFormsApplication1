using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;

namespace WindowsFormsApplication1.Servis
{
    // ZAH-010: dokumenta delova (6.3): izdatnice, prijemi, povrati, pocetno stanje, popis i korekcije.
    // Gore dokumenti (iz kretanja), dole stavke izabranog dokumenta.
    public class frmSrvDokumenta : SrvForma
    {
        readonly SfDataGrid grid, gridStavke;
        readonly TextBox txtTrazi;
        readonly ComboBox cboVrsta;
        readonly CheckBox chkPeriod;

        public frmSrvDokumenta() : base("frmSrvDokumenta", "Dokumenta delova")
        {
            if (!Pravo) return;
            txtTrazi = Pretraga(() => { Osvezi(grid); Zbir(); });
            cboVrsta = FilterLista(null, new[] { "Sve", "Izdavanje", "Prijem", "Povrat", "Popis i korekcija", "Početno stanje" }, () => { Osvezi(grid); Zbir(); });
            chkPeriod = Filter("Poslednjih 30 dana", true, Ucitaj);
            Dugme("Osveži", Uloga.Sporedna, Ucitaj);
            Dugme("Excel", Uloga.Sporedna, () => Excel(grid, "Dokumenta delova"));
            Dugme("+ Novi prijem", Uloga.Glavna, NoviPrijem, true).Enabled = Upis;
            Pomoc("Izdavanje i povrat se rade na servisnom nalogu; ovde su pregled i prijem.");

            grid = NoviGrid();
            grid.SelectionChanged += (s, e) => Radi(UcitajStavke);
            gridStavke = NoviGrid();

            var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterDistance = 330 };
            split.Panel1.Controls.Add(grid);
            split.Panel2.Controls.Add(Odeljak("Stavke dokumenta", gridStavke));
            Controls.Add(split);
            Zavrsi();
            Radi(Ucitaj);
        }

        void Ucitaj()
        {
            var t = baza.Upit(@"SELECT Dokument AS ID, Datum, Vrsta, Dokument, Nalog, Narudzbenica, Dobavljac, Stavki, UlazRSD, IzlazRSD, Napomena, Korisnik
                                FROM dbo.SRV_vw_DeoDokument WHERE @Period = 0 OR Datum >= DATEADD(day, -30, GETDATE())
                                ORDER BY Datum DESC", "@Period", chkPeriod.Checked);
            Postavi(grid, t, Prikazan);
            Zbir();
            UcitajStavke();
        }

        bool Prikazan(object o)
        {
            var r = (DataRowView)o;
            string v = SrvFormat.Str(r["Vrsta"]), f = (string)cboVrsta.SelectedItem;
            if (f == "Popis i korekcija" ? (v != "Popis" && v != "Korekcija") : (f != "Sve" && v != f)) return false;
            return Sadrzi(r, txtTrazi.Text, "Dokument", "Nalog", "Narudzbenica", "Dobavljac");
        }

        void Zbir()
        {
            var redovi = Vidljivi(grid).ToList();
            Status(redovi.Count + " dokumenata · ulaz " + SrvFormat.Broj(redovi.Sum(r => SrvFormat.Dec(r["UlazRSD"]))) + " RSD · izlaz "
                   + SrvFormat.Broj(redovi.Sum(r => SrvFormat.Dec(r["IzlazRSD"]))) + " RSD");
        }

        void UcitajStavke()
        {
            var r = Izabran(grid);
            Postavi(gridStavke, baza.Upit(@"SELECT k.ID, d.Sifra, d.Naziv AS Deo, ABS(k.Kolicina) AS Kolicina, d.JM, k.Cena,
                                                  CAST(ABS(k.Kolicina) * k.Cena AS decimal(18,2)) AS Vrednost,
                                                  CASE WHEN k.Tip = N'Izdavanje' THEN nd.VracenoKolicina END AS Vraceno, k.Napomena
                                           FROM dbo.SRV_DeoKretanje k JOIN dbo.SRV_Deo d ON d.ID = k.DeoID
                                           LEFT JOIN dbo.SRV_NalogDeo nd ON nd.ID = k.NalogDeoID
                                           WHERE k.DokumentBroj = @B ORDER BY k.ID", "@B", r == null ? "" : r["Dokument"]));
        }

        void NoviPrijem()
        {
            using (var d = new dlgSrvStavke(VrstaStavki.Prijem))
                if (d.ShowDialog(this) == DialogResult.OK) Ucitaj();
        }
    }
}
