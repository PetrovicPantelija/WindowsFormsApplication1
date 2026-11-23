using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;

namespace WindowsFormsApplication1.Servis
{
    // ZAH-010: obavestenja (6.7): kriticni delovi ispod minimuma (MD-R08), rokovi dokumenata i dospeli servisi (SR-17).
    public class frmSrvObavestenja : SrvForma
    {
        readonly SfDataGrid grid;
        readonly CheckBox chkNeprocitana;

        public frmSrvObavestenja() : base("frmSrvObavestenja", "Obaveštenja")
        {
            if (!Pravo) return;
            chkNeprocitana = Filter("Nepročitana", true, Ucitaj);
            Dugme("Osveži", Uloga.Sporedna, Ucitaj);
            Dugme("Otvori deo", Uloga.Sporedna, OtvoriDeo);
            Dugme("Označi kao pročitano", Uloga.Glavna, Procitano, true).Enabled = Izmena || Upis;
            Pomoc("Selektujte red (Ctrl za više) pa „Označi kao pročitano“. Dvoklik otvara deo.");

            grid = NoviGrid(true);
            BojaStatusa(grid, "Status", StandardnaBoja);
            Dvoklik(grid, OtvoriDeo);
            Controls.Add(grid);
            Zavrsi();
            Radi(() => { baza.Proc("dbo.SRV_ProveriRokove"); Ucitaj(); });
        }

        void Ucitaj()
        {
            var t = baza.Upit(@"SELECT ID, Vreme, Vrsta, Obavestenje, RaspolozivoSada, Status, Procitao, VremeCitanja, DeoID, SredstvoID, Procitano
                                FROM dbo.SRV_vw_Obavestenje WHERE @Nep = 0 OR Procitano = 0 ORDER BY Vreme DESC", "@Nep", chkNeprocitana.Checked);
            Postavi(grid, t);
            Status(BrojRedova(grid) + " obaveštenja");
        }

        void Procitano()
        {
            var izabrani = Izabrani(grid);
            if (izabrani.Count == 0) { Poruke.Info(this, "Selektujte obaveštenje pa kliknite „Označi kao pročitano“."); return; }
            baza.Proc("dbo.SRV_ObavestenjaProcitana", ServisBaza.Tvp("@IDs", "dbo.SRV_IdTip", ServisBaza.Idovi(izabrani.Select(r => SrvFormat.Int(r["ID"])))));
            Ucitaj();
        }

        void OtvoriDeo()
        {
            var r = Izabran(grid);
            if (r == null || r["DeoID"] == DBNull.Value) { Poruke.Info(this, "Izabrano obaveštenje nije vezano za deo."); return; }
            using (var d = new dlgSrvDeo(SrvFormat.Int(r["DeoID"]), false, Izmena))
                d.ShowDialog(this);
            Ucitaj();
        }
    }
}
