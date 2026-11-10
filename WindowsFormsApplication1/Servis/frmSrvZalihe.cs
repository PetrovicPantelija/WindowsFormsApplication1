using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;

namespace WindowsFormsApplication1.Servis
{
    // ZAH-010: zalihe i sifarnik delova (6.2). Stanje = zbir kretanja; raspolozivo = stanje - rezervisano.
    public class frmSrvZalihe : SrvForma
    {
        readonly SfDataGrid grid;
        readonly TextBox txtTrazi;
        readonly CheckBox chkIspod, chkKriticni, chkNeaktivni;
        readonly Label lblZbir = new Label();

        public frmSrvZalihe() : base("frmSrvZalihe", "Zalihe delova")
        {
            if (!Pravo) return;
            txtTrazi = Pretraga(() => { Osvezi(grid); Zbir(); });
            chkIspod = Filter("Ispod minimuma", false, () => { Osvezi(grid); Zbir(); });
            chkKriticni = Filter("Kritični", false, () => { Osvezi(grid); Zbir(); });
            chkNeaktivni = Filter("I neaktivni", false, Ucitaj);
            Dugme("Osveži", Uloga.Sporedna, Ucitaj);
            Dugme("Početno stanje", Uloga.Sporedna, PocetnoStanje).Enabled = Upis;
            Dugme("Korekcija", Uloga.Sporedna, () => { if (frmSrvKorekcije.NovaKorekcija(this, IzabranID(grid, "DeoID"))) Ucitaj(); }).Enabled = Upis;
            Dugme("Excel", Uloga.Sporedna, () => Excel(grid, "Zalihe delova"));
            Dugme("+ Novi deo", Uloga.Glavna, () => OtvoriDeo(null), true).Enabled = Upis;
            Pomoc("Dvoklik otvara deo (osnovni podaci, primenljivost, kartica, dobavljač).");

            lblZbir.Dock = DockStyle.Top;
            lblZbir.Height = 30;
            lblZbir.Padding = new Padding(10, 7, 0, 0);
            lblZbir.Font = JelaTema.FontNaslov;
            lblZbir.BackColor = System.Drawing.Color.White;

            grid = NoviGrid();
            BojaStatusa(grid, "Status", StandardnaBoja);
            Dvoklik(grid, () => OtvoriDeo(IzabranID(grid, "DeoID")));
            Controls.Add(grid);
            Controls.Add(lblZbir);
            Zavrsi();
            Radi(Ucitaj);
        }

        void Ucitaj()
        {
            var t = baza.Upit(@"SELECT DeoID, Sifra, Naziv, Grupa, JM, Stanje, Rezervisano, Raspolozivo, MinZaliha, Naruceno, Cena, Vrednost,
                                       Lokacija, Kritican, Status, Dobavljac, Aktivno
                                FROM dbo.SRV_vw_Zalihe WHERE Aktivno = 1 OR @Sve = 1 ORDER BY Sifra", "@Sve", chkNeaktivni.Checked);
            Postavi(grid, t, Prikazan, "DeoID");
            Zbir();
        }

        bool Prikazan(object o)
        {
            var r = (DataRowView)o;
            if (chkIspod.Checked && SrvFormat.Str(r["Status"]) == "Na stanju") return false;
            if (chkKriticni.Checked && !(bool)r["Kritican"]) return false;
            return Sadrzi(r, txtTrazi.Text, "Sifra", "Naziv", "Lokacija");
        }

        // zbir u zaglavlju = isti redovi kao u gridu (aktivni delovi)
        void Zbir()
        {
            var redovi = Vidljivi(grid).ToList();
            decimal vrednost = redovi.Where(r => (bool)r["Aktivno"]).Sum(r => SrvFormat.Dec(r["Vrednost"]));
            int ispod = redovi.Count(r => SrvFormat.Str(r["Status"]) != "Na stanju");
            int kriticnih = redovi.Count(r => SrvFormat.Str(r["Status"]) == "Kritičan");
            lblZbir.Text = "Vrednost zaliha: " + SrvFormat.Broj(vrednost) + " RSD     ·     Delova ispod minimuma: " + ispod
                         + "     ·     Kritičnih ispod minimuma: " + kriticnih;
            Status(redovi.Count + " delova");
        }

        void OtvoriDeo(int? id)
        {
            if (!id.HasValue && !Smem(Upis, "Nemate pravo upisa.")) return;
            using (var d = new dlgSrvDeo(id, Upis, Izmena))
            {
                d.ShowDialog(this);
                if (d.Promenjeno) Ucitaj();
            }
        }

        void PocetnoStanje()
        {
            using (var d = new dlgSrvStavke(VrstaStavki.PocetnoStanje))
                if (d.ShowDialog(this) == DialogResult.OK) Ucitaj();
        }
    }
}
