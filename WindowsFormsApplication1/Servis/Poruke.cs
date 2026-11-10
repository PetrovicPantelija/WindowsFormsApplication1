using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Servis
{
    // ZAH-010: poruke korisniku. Potvrde su uvek "Da / Ne" (sopstveni dijalog, ne zavisi od jezika Windows-a).
    // Greske baze se prevode po nazivu ogranicenja (UQ_/CK_/FK_), ne po tekstu greske (zavisi od jezika servera).
    public static class Poruke
    {
        public const string Naslov = "Servis";
        public const string Opsta = "Radnja nije uspela. Pokušajte ponovo; ako se ponovi, javite administratoru.";

        static readonly Dictionary<string, string> PorukePoOgranicenju = new Dictionary<string, string>
        {
            { "UQ_SRV_VrstaSredstva_Naziv", "Vrsta sredstva sa tim nazivom već postoji." },
            { "UQ_SRV_Sredstvo_Oznaka", "Sredstvo sa tom oznakom već postoji." },
            { "UQ_SRV_PlanNormativ_Deo", "Taj deo je već u normativu ovog plana." },
            { "UQ_SRV_DeoPrimenljivost_Vrsta", "Ova vrsta sredstva je već dodata za deo." },
            { "UQ_SRV_DeoGrupa_Naziv", "Grupa delova sa tim nazivom već postoji." },
            { "UQ_SRV_Deo_Sifra", "Deo sa tom šifrom već postoji." },
            { "UQ_SRV_VrstaSredstvaPolje_Naziv", "Polje sa tim nazivom već postoji za ovu vrstu sredstva." },
            { "UQ_SRV_VrstaSredstvaDokument_Naziv", "Dokument sa tim nazivom već postoji za ovu vrstu sredstva." },
            { "UQ_SRV_PlanOdrzavanja_Naziv", "Plan sa tim nazivom već postoji za ovu vrstu sredstva." },
            { "UQ_SRV_NarudzbenicaStavka_Deo", "Deo je već na narudžbenici." },
            { "UQ_SRV_PopisStavka_Deo", "Deo je već na popisnoj listi." },
            { "CK_SRV_Deo_Max", "Maksimalna zaliha mora biti veća ili jednaka minimalnoj." },
            { "CK_SRV_Deo_Min", "Polje „Minimalna zaliha“ ne sme biti negativno." },
            { "CK_SRV_PlanOdrzavanja_Interval", "Unesite bar jedan interval veći od 0." },
            { "CK_SRV_PlanOdrzavanja_Upozorenje", "Upozorenje ne sme biti veće od intervala." },
            { "CK_SRV_SredstvoDokument_Datumi", "Datum isteka ne sme biti pre datuma izdavanja." },
            { "CK_SRV_Nalog_Brojilo", "Brojilo pri zatvaranju ne sme biti manje od brojila pri otvaranju." },
            { "CK_SRV_Korekcija_Kolicina", "Polje „Korekcija (+ višak, − manjak)“ ne sme biti 0." },
            { "CK_SRV_PopisStavka_Prebrojano", "Polje „Prebrojano“ mora biti između 0 i 999.999.999." },
            { "CK_SRV_Sredstvo_Godina", "Polje „Godina“ mora biti između 1900 i 2100." },
            { "CK_SRV_VrstaSredstvaPolje_Opcije", "Za tip „Lista“ unesite polje „Opcije“." },
            { "CK_SRV_NalogDeo_Kolicina", "Polje „Količina“ mora biti veće od 0." },
            { "CK_SRV_PlanNormativ_Kolicina", "Polje „Količina“ mora biti veće od 0." },
            { "CK_SRV_SpoljniServisZahtev_Iznos", "Polje „Iznos“ mora biti veće od 0." },
        };

        public static void Info(IWin32Window vlasnik, string tekst)
        {
            MessageBox.Show(vlasnik, tekst, Naslov, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Upozorenje(IWin32Window vlasnik, string tekst)
        {
            MessageBox.Show(vlasnik, tekst, Naslov, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Da / Ne
        public static bool Potvrda(IWin32Window vlasnik, string tekst)
        {
            return Izbor(vlasnik, tekst, "Da", "Ne") == 0;
        }

        // izbor jedne od ponudjenih radnji; vraca indeks dugmeta ili -1 (zatvoreno)
        public static int Izbor(IWin32Window vlasnik, string tekst, params string[] dugmad)
        {
            using (var f = new Form())
            {
                f.Text = Naslov;
                f.FormBorderStyle = FormBorderStyle.FixedDialog;
                f.MinimizeBox = false; f.MaximizeBox = false; f.ShowInTaskbar = false;
                f.StartPosition = vlasnik == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;
                f.Font = JelaTema.FontTekst;
                f.AutoSize = true; f.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                f.Padding = new Padding(16);

                var lbl = new Label { Text = tekst, AutoSize = true, MaximumSize = new Size(440, 0), Margin = new Padding(0, 8, 0, 16) };
                var dole = new FlowLayoutPanel { FlowDirection = FlowDirection.RightToLeft, AutoSize = true, Dock = DockStyle.Fill, Margin = new Padding(0) };
                int izbor = -1;
                for (int i = dugmad.Length - 1; i >= 0; i--)
                {
                    int indeks = i;
                    var b = new Button { Text = dugmad[i], AutoSize = true, MinimumSize = new Size(88, 30), Margin = new Padding(8, 0, 0, 0) };
                    if (i == 0) b.BackColor = SrvFormat.Zelena; else b.UseVisualStyleBackColor = true;
                    b.Click += (s, e) => { izbor = indeks; f.Close(); };
                    dole.Controls.Add(b);
                    if (i == 0) f.AcceptButton = b;
                }
                var t = new TableLayoutPanel { AutoSize = true, ColumnCount = 1, Dock = DockStyle.Fill };
                t.Controls.Add(lbl);
                t.Controls.Add(dole);
                f.Controls.Add(t);
                JelaTema.Primeni(f);
                f.ShowDialog(vlasnik);
                return izbor;
            }
        }

        // prikaz greske: nase poruke kakve jesu, ogranicenja po katalogu, ostalo opsta poruka + log
        public static void Greska(IWin32Window vlasnik, Exception ex)
        {
            Upozorenje(vlasnik, Tekst(ex));
        }

        public static string Tekst(Exception ex)
        {
            var sql = ex as SqlException;
            string poruka = sql != null ? PrevediSqlGresku(sql) : null;
            if (poruka == null)
            {
                new ServisBaza().LogGreske(ex.ToString());
                poruka = Opsta;
            }
            return poruka;
        }

        // null = nepoznata greska (pozivalac pise u log i daje opstu poruku)
        public static string PrevediSqlGresku(SqlException ex)
        {
            int broj = ex.Number;
            string tekst = ex.Message;

            // procedure ponovo javljaju sistemske greske kao "#SQL<broj># <tekst>" (SRV_PonoviGresku)
            var oznaka = Regex.Match(tekst, @"^#SQL(\d+)#\s*");
            if (oznaka.Success)
            {
                broj = int.Parse(oznaka.Groups[1].Value);
                tekst = tekst.Substring(oznaka.Length);
            }
            else if (broj == 50000)
            {
                return tekst;   // nase RAISERROR poruke (srpski)
            }

            if (broj == 2627 || broj == 2601 || broj == 547)
            {
                var m = Regex.Match(tekst, "(UQ|CK|FK)_[A-Za-z0-9_]+");
                string poruka;
                if (m.Success && PorukePoOgranicenju.TryGetValue(m.Value, out poruka)) return poruka;
                if (m.Success && m.Value.StartsWith("FK_")) return "Zapis je povezan sa drugim podacima; radnja nije moguća.";
                if (broj != 547) return "Takav zapis već postoji.";
            }
            if (broj == -2) return "Baza nije odgovorila na vreme. Pokušajte ponovo.";
            if (broj == 53 || broj == 2 || broj == 4060 || broj == 18456) return "Nema veze sa bazom podataka. Proverite mrežu i pokušajte ponovo.";
            return null;
        }
    }
}
