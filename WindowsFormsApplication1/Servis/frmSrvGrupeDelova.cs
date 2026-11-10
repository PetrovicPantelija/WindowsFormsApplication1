using System;
using System.Data;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;

namespace WindowsFormsApplication1.Servis
{
    // ZAH-010: sifarnik grupa delova (sablon 6.1). Grupa se ne brise nego deaktivira.
    public class frmSrvGrupeDelova : SrvForma
    {
        readonly SfDataGrid grid;
        readonly TextBox txtTrazi;
        readonly CheckBox chkNeaktivni;
        readonly Button btnNovi, btnIzmeni;

        public frmSrvGrupeDelova() : base("frmSrvGrupeDelova", "Grupe delova")
        {
            if (!Pravo) return;
            MinimumSize = new System.Drawing.Size(600, 400);
            Size = new System.Drawing.Size(760, 560);
            txtTrazi = Pretraga(() => Osvezi(grid));
            chkNeaktivni = Filter("I neaktivne", false, Ucitaj);
            Dugme("Osveži", Uloga.Sporedna, Ucitaj);
            Dugme("Excel", Uloga.Sporedna, () => Excel(grid, "Grupe delova"));
            btnNovi = Dugme("+ Dodaj", Uloga.Glavna, () => Uredi(null), true);
            btnIzmeni = Dugme("Izmeni", Uloga.Sporedna, () => Uredi(IzabranID(grid)), true);
            btnNovi.Enabled = Upis;
            btnIzmeni.Enabled = Izmena;
            Pomoc("Dvoklik menja grupu; grupa se ne briše nego deaktivira.");

            grid = NoviGrid();
            Dvoklik(grid, () => Uredi(IzabranID(grid)));
            Controls.Add(grid);
            Zavrsi();
            Radi(Ucitaj);
        }

        void Ucitaj()
        {
            var t = baza.Upit(@"SELECT g.ID, g.Naziv,
                                       (SELECT COUNT(*) FROM dbo.SRV_Deo d WHERE d.DeoGrupaID = g.ID AND d.Aktivno = 1) AS Delova, g.Aktivno
                                FROM dbo.SRV_DeoGrupa g WHERE g.Aktivno = 1 OR @Sve = 1 ORDER BY g.Naziv",
                              "@Sve", chkNeaktivni.Checked);
            Postavi(grid, t, o => Sadrzi((DataRowView)o, txtTrazi.Text, "Naziv"));
            Status(BrojRedova(grid) + " grupa");
        }

        void Uredi(int? id)
        {
            if (id.HasValue ? !Smem(Izmena, "Nemate pravo izmene.") : !Smem(Upis, "Nemate pravo upisa.")) return;
            DataRowView r = id.HasValue ? Izabran(grid) : null;
            using (var d = new SrvDijalog(id.HasValue ? "Izmena grupe delova" : "Nova grupa delova"))
            {
                var naziv = d.Polja.Tekst("Naziv", r == null ? "" : SrvFormat.Str(r["Naziv"]), true, false, 100);
                var aktivno = d.Polja.Kvacica("Aktivna", r == null || (bool)r["Aktivno"]);
                d.Snimi = () =>
                {
                    baza.Proc("dbo.SRV_SacuvajGrupuDelova", "@ID", id, "@Naziv", naziv.Text, "@Aktivno", aktivno.Checked);
                    return true;
                };
                if (d.ShowDialog(this) == DialogResult.OK) Ucitaj();
            }
        }
    }
}
