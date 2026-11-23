using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using WindowsFormsApplication1.Kalkulacije.IzracunCena;

namespace WindowsFormsApplication1.Servis
{
    // ZAH-010: meni modula Servis (kao MainKalkulacije): tamni tekst, braon pri prelasku misem, zeleni naslovi.
    // Pre otvaranja forme proverava pravo (forma.Pravo); forma bez prava se ne prikazuje.
    public class MainServis : Form
    {
        public bool Pravo;
        public static string code = "frmMainServis";
        static readonly Color MeniTekst = Color.FromArgb(40, 40, 40);
        static readonly Color MeniHover = Color.FromArgb(190, 150, 110);
        static readonly Color MeniNaslov = Color.FromArgb(58, 77, 57);

        Label lblObavestenja;

        public MainServis()
        {
            Text = "Servis";
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.FromArgb(248, 247, 244);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            ClientSize = new Size(800, 330);
            Icon = Properties.Resources.JelaIkona;

            try { Pravo = new PravaForme(code).Pravo; }
            catch (Exception ex) { Pravo = false; Poruke.Greska(null, ex); }
            if (!Pravo) return;

            var magacin = Kolona("Magacin delova", IconChar.Boxes, 12);
            Stavka(magacin, "Zalihe i šifarnik delova", () => Otvori(new frmSrvZalihe()));
            Stavka(magacin, "Dokumenta delova", () => Otvori(new frmSrvDokumenta()));
            Stavka(magacin, "Potrebe za nabavkom", () => Otvori(new frmSrvNabavka()));
            Stavka(magacin, "Popis zaliha", () => Otvori(new frmSrvPopis()));
            Stavka(magacin, "Korekcije zaliha", () => Otvori(new frmSrvKorekcije()));
            lblObavestenja = Stavka(magacin, "Obaveštenja", () => Otvori(new frmSrvObavestenja()));

            var servis = Kolona("Servis", IconChar.Tools, 272);
            Stavka(servis, "Servisni nalozi", () => Otvori(new frmSrvNalozi()));
            Stavka(servis, "Prijava kvarova", () => Otvori(new frmSrvKvarovi()));
            Stavka(servis, "Spoljni servis", () => Otvori(new frmSrvSpoljniServis()));
            Stavka(servis, "Stanje sredstava", () => Otvori(new frmSrvStanje()));

            var sifarnici = Kolona("Sredstva i šifarnici", IconChar.Archive, 532);
            Stavka(sifarnici, "Sredstva", () => Otvori(new frmSrvSredstva()));
            Stavka(sifarnici, "Plan održavanja", () => Otvori(new frmSrvPlanovi()));
            Stavka(sifarnici, "Vrste sredstava", () => Otvori(new frmSrvVrste()));
            Stavka(sifarnici, "Grupe delova", () => Otvori(new frmSrvGrupeDelova()));

            Activated += (s, e) => OsveziBrojObavestenja();
            Load += (s, e) =>
            {
                // obavestenja o rokovima dokumenata i dospelim servisima (pravi se jednom po pragu)
                try { new ServisBaza().Proc("dbo.SRV_ProveriRokove"); }
                catch (Exception ex) { new ServisBaza().LogGreske(ex.ToString()); }
                OsveziBrojObavestenja();
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!Pravo) { Close(); return; }
            base.OnLoad(e);
        }

        Panel Kolona(string naslov, IconChar ikona, int x)
        {
            var p = new Panel { BackColor = Color.White, Location = new Point(x, 12), Size = new Size(256, 300) };
            var b = new IconButton
            {
                Text = naslov,
                Dock = DockStyle.Top,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 10.5F),
                ForeColor = MeniNaslov,
                IconChar = ikona,
                IconColor = MeniNaslov,
                IconSize = 24,
                ImageAlign = ContentAlignment.MiddleLeft,
                TextAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                Padding = new Padding(12, 0, 12, 0),
                TabStop = false
            };
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseOverBackColor = Color.White;
            b.FlatAppearance.MouseDownBackColor = Color.White;
            p.Controls.Add(b);
            p.Tag = 52;     // y sledece stavke
            Controls.Add(p);
            return p;
        }

        Label Stavka(Panel kolona, string tekst, Action klik)
        {
            int y = (int)kolona.Tag;
            var l = new Label
            {
                Text = "• " + tekst,
                Tag = tekst,
                AutoSize = true,
                Font = new Font("Segoe UI", 10F),
                ForeColor = MeniTekst,
                Location = new Point(16, y),
                Cursor = Cursors.Hand
            };
            l.MouseEnter += (s, e) => l.ForeColor = MeniHover;
            l.MouseLeave += (s, e) => l.ForeColor = MeniTekst;
            l.Click += (s, e) =>
            {
                try { klik(); }
                catch (Exception ex) { Poruke.Greska(this, ex); }
            };
            kolona.Controls.Add(l);
            kolona.Tag = y + 34;
            return l;
        }

        void Otvori(SrvForma f)
        {
            if (f.Pravo) f.Show(); else f.Dispose();
        }

        void OsveziBrojObavestenja()
        {
            if (lblObavestenja == null) return;
            try
            {
                int n = new ServisBaza().NeprocitanaObavestenja();
                lblObavestenja.Text = "• Obaveštenja" + (n > 0 ? " (" + n + ")" : "");
            }
            catch (Exception) { lblObavestenja.Text = "• Obaveštenja"; }
        }
    }
}
