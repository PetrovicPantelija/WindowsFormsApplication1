using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;

namespace WindowsFormsApplication1.Servis
{
    // ZAH-010: prijava kvarova (SR-04, SR-05, SR-06) sa prilozima (koren foldera u Servis.KorenPriloga).
    public class frmSrvKvarovi : SrvForma
    {
        readonly SfDataGrid grid;
        readonly TextBox txtTrazi;
        readonly CheckBox chkOtvorene;
        readonly Button btnNalog, btnOdbaci, btnZatvori, btnIzmeni;

        public frmSrvKvarovi() : base("frmSrvKvarovi", "Prijava kvarova")
        {
            if (!Pravo) return;
            txtTrazi = Pretraga(() => Osvezi(grid));
            chkOtvorene = Filter("Samo otvorene", true, () => { Osvezi(grid); Status(BrojRedova(grid) + " prijava"); });
            Dugme("Osveži", Uloga.Sporedna, Ucitaj);
            btnNalog = Dugme("Otvori nalog", Uloga.Paznja, OtvoriNalog);
            btnIzmeni = Dugme("Izmeni", Uloga.Sporedna, () => Uredi(IzabranID(grid)));
            btnOdbaci = Dugme("Odbaci", Uloga.Sporedna, Odbaci);
            btnZatvori = Dugme("Zatvori", Uloga.Sporedna, Zatvori);
            Dugme("Prilozi", Uloga.Sporedna, Prilozi);
            Dugme("Excel", Uloga.Sporedna, () => Excel(grid, "Prijave kvarova"));
            Dugme("+ Nova prijava", Uloga.Glavna, () => Uredi(null), true).Enabled = Upis;
            Pomoc("„Ne može da nastavi rad“ stavlja sredstvo u kvar do završetka naloga.");

            grid = NoviGrid();
            BojaStatusa(grid, "Status", StandardnaBoja);
            BojaStatusa(grid, "Hitnost", s => s == "Hitno" ? SrvFormat.StatusCrvena : s == "Visoka" ? SrvFormat.StatusNarandzasta : (Color?)null);
            Dvoklik(grid, () => Uredi(IzabranID(grid)));
            grid.SelectionChanged += (s, e) => Dugmad();
            Controls.Add(grid);
            Zavrsi();
            Radi(Ucitaj);
        }

        void Ucitaj()
        {
            Postavi(grid, baza.Upit(@"SELECT ID, Broj, DatumPrijave, Sredstvo, NazivSredstva, Prijavio, Kanal, Kategorija, Hitnost, MozeDaRadi,
                                            Brojilo, Opis, Status, Nalog, RazlogOdbacivanja, Priloga, SredstvoID
                                     FROM dbo.SRV_vw_Kvar ORDER BY ID DESC"),
                    o =>
                    {
                        var r = (DataRowView)o;
                        string s = SrvFormat.Str(r["Status"]);
                        if (chkOtvorene.Checked && s != "Nova" && s != "U obradi") return false;
                        return Sadrzi(r, txtTrazi.Text, "Broj", "Sredstvo", "NazivSredstva", "Opis", "Prijavio");
                    });
            Status(BrojRedova(grid) + " prijava");
            Dugmad();
        }

        void Dugmad()
        {
            var r = Izabran(grid);
            string s = r == null ? "" : SrvFormat.Str(r["Status"]);
            bool otvorena = s == "Nova" || s == "U obradi";
            btnNalog.Enabled = otvorena && Upis;
            btnIzmeni.Enabled = otvorena && Izmena;
            btnOdbaci.Enabled = otvorena && Izmena;
            btnZatvori.Enabled = otvorena && Izmena;
        }

        void Uredi(int? id)
        {
            if (id.HasValue ? !Smem(Izmena, "Nemate pravo izmene.") : !Smem(Upis, "Nemate pravo upisa.")) return;
            DataRowView r = id.HasValue ? Izabran(grid) : null;
            if (r != null && SrvFormat.Str(r["Status"]) != "Nova" && SrvFormat.Str(r["Status"]) != "U obradi")
            { Poruke.Info(this, "Zatvorena ili odbačena prijava se ne menja."); return; }
            using (var d = new SrvDijalog(id.HasValue ? "Izmena prijave " + r["Broj"] : "Nova prijava kvara", id.HasValue ? "Sačuvaj" : "Prijavi kvar"))
            {
                var p = d.Polja;
                ComboBox sredstvo = null;
                if (r == null) sredstvo = p.Lista("Sredstvo", baza.Sredstva(), null, true, true);
                else p.Prikaz("Sredstvo", r["Sredstvo"] + " – " + r["NazivSredstva"]);
                var prijavio = p.Tekst("Prijavio", r == null ? ServisBaza.Korisnik : SrvFormat.Str(r["Prijavio"]), true, false, 100);
                var kanal = p.Izbor("Kanal", new[] { "Lično", "Telefon", "Poruka", "E-mail" }, r == null ? "Lično" : SrvFormat.Str(r["Kanal"]));
                var kategorija = p.Izbor("Kategorija", new[] { "Mehanika", "Elektrika", "Hidraulika", "Gume", "Karoserija", "Ostalo" },
                                         r == null ? null : SrvFormat.Str(r["Kategorija"]));
                var hitnost = p.Izbor("Hitnost", new[] { "Niska", "Srednja", "Visoka", "Hitno" }, r == null ? "Srednja" : SrvFormat.Str(r["Hitnost"]), true);
                var moze = p.Izbor("Može da nastavi rad", new[] { "Da", "Ne" }, r == null ? "Da" : SrvFormat.Str(r["MozeDaRadi"]), true);
                TextBox brojilo = r == null ? p.Broj("Brojilo pri kvaru", null, false, 0) : null;
                var opis = p.Tekst("Opis kvara", r == null ? "" : SrvFormat.Str(r["Opis"]), true, true, 1000);
                opis.Height = 90;
                string broj = null;
                d.Snimi = () =>
                {
                    var red = baza.ProcRed("dbo.SRV_KvarSacuvaj", "@ID", id, "@SredstvoID", r == null ? PoljaPanel.Vrednost(sredstvo) : r["SredstvoID"],
                                           "@Prijavio", prijavio.Text, "@Kanal", kanal.SelectedItem, "@Kategorija", kategorija.SelectedItem,
                                           "@Hitnost", hitnost.SelectedItem, "@MozeDaRadi", (string)moze.SelectedItem == "Da",
                                           "@Opis", opis.Text, "@Brojilo", brojilo == null ? null : PoljaPanel.BrojIz(brojilo));
                    broj = SrvFormat.Str(red["Broj"]);
                    return true;
                };
                if (d.ShowDialog(this) != DialogResult.OK) return;
                if (r == null)
                    Poruke.Info(this, "Kvar je prijavljen: " + broj + "." + ((string)moze.SelectedItem == "Ne" ? " Sredstvo je u statusu „U kvaru“." : ""));
                Ucitaj();
            }
        }

        void OtvoriNalog()
        {
            var r = Izabran(grid);
            if (r == null) return;
            if (frmSrvNalozi.NoviNalog(this, SrvFormat.Int(r["SredstvoID"]), SrvFormat.Int(r["ID"]), null) != null) Ucitaj();
        }

        void Odbaci()
        {
            var r = Izabran(grid);
            if (r == null) return;
            using (var d = new SrvDijalog("Odbacivanje prijave " + r["Broj"], "Odbaci prijavu"))
            {
                var razlog = d.Polja.Tekst("Razlog odbacivanja", "", true, true, 400);
                d.Snimi = () => { baza.Proc("dbo.SRV_KvarAkcija", "@ID", r["ID"], "@Akcija", "Odbaci", "@Razlog", razlog.Text); return true; };
                if (d.ShowDialog(this) == DialogResult.OK) Ucitaj();
            }
        }

        void Zatvori()
        {
            var r = Izabran(grid);
            if (r == null) return;
            if (r["Nalog"] != DBNull.Value && !Poruke.Potvrda(this, "Prijava ima nalog " + r["Nalog"] + ". Zatvoriti prijavu?")) return;
            if (r["Nalog"] == DBNull.Value && !Poruke.Potvrda(this, "Zatvoriti prijavu " + r["Broj"] + " bez servisnog naloga?")) return;
            baza.Proc("dbo.SRV_KvarAkcija", "@ID", r["ID"], "@Akcija", "Zatvori");
            Ucitaj();
        }

        // prilozi: fajl se kopira u <KorenPriloga>\<broj prijave>\ (ako je koren podesen), u bazi ostaje relativna putanja
        void Prilozi()
        {
            var kvar = Izabran(grid);
            if (kvar == null) return;
            string koren = baza.Podesavanje("Servis.KorenPriloga");
            using (var f = new Form { Text = "Servis – Prilozi " + kvar["Broj"], Size = new Size(720, 420), StartPosition = FormStartPosition.CenterParent, Font = JelaTema.FontTekst, ShowInTaskbar = false })
            {
                var lista = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, RowHeadersVisible = false,
                                               SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
                Action ucitaj = () =>
                {
                    lista.DataSource = baza.Upit("SELECT ID, Naziv, Putanja, Kreirao AS Dodao, DatumKreiranja AS Dodato FROM dbo.SRV_KvarPrilog WHERE KvarID = @K AND Aktivno = 1 ORDER BY ID",
                                                 "@K", kvar["ID"]);
                    lista.Columns["ID"].Visible = false;
                };
                Func<string, string> puna = p => Path.IsPathRooted(p) || koren == "" ? p : Path.Combine(koren, p);
                var traka = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40, Padding = new Padding(4, 6, 4, 0) };
                var bDodaj = new Button { Text = "+ Dodaj prilog", AutoSize = true, BackColor = SrvFormat.Zelena, Enabled = Upis || Izmena };
                var bOtvori = new Button { Text = "Otvori", AutoSize = true, UseVisualStyleBackColor = true };
                var bUkloni = new Button { Text = "Ukloni", AutoSize = true, UseVisualStyleBackColor = true, Enabled = Izmena };
                bDodaj.Click += (s, e) =>
                {
                    using (var o = new OpenFileDialog { Title = "Izaberite prilog" })
                    {
                        if (o.ShowDialog(f) != DialogResult.OK) return;
                        try
                        {
                            string putanja = o.FileName;
                            if (koren != "" && Directory.Exists(koren))
                            {
                                string rel = Path.Combine(SrvFormat.Str(kvar["Broj"]), Path.GetFileName(o.FileName));
                                Directory.CreateDirectory(Path.Combine(koren, SrvFormat.Str(kvar["Broj"])));
                                File.Copy(o.FileName, Path.Combine(koren, rel), true);
                                putanja = rel;
                            }
                            baza.Proc("dbo.SRV_KvarPrilogDodaj", "@KvarID", kvar["ID"], "@Putanja", putanja, "@Naziv", Path.GetFileName(o.FileName));
                            ucitaj();
                        }
                        catch (Exception ex) { Poruke.Greska(f, ex); }
                    }
                };
                bOtvori.Click += (s, e) =>
                {
                    if (lista.CurrentRow == null) return;
                    string p = puna(SrvFormat.Str(lista.CurrentRow.Cells["Putanja"].Value));
                    if (!File.Exists(p)) { Poruke.Upozorenje(f, "Fajl nije dostupan: " + p); return; }
                    try { Process.Start(p); } catch (Exception ex) { Poruke.Greska(f, ex); }
                };
                bUkloni.Click += (s, e) =>
                {
                    if (lista.CurrentRow == null || !Poruke.Potvrda(f, "Ukloniti izabrani prilog?")) return;
                    try { baza.Proc("dbo.SRV_KvarPrilogUkloni", "@ID", lista.CurrentRow.Cells["ID"].Value); ucitaj(); }
                    catch (Exception ex) { Poruke.Greska(f, ex); }
                };
                traka.Controls.AddRange(new Control[] { bDodaj, bOtvori, bUkloni });
                f.Controls.Add(lista);
                f.Controls.Add(traka);
                JelaTema.Primeni(f);
                SrvFormat.SiviIskljucena(f);
                try { ucitaj(); } catch (Exception ex) { Poruke.Greska(this, ex); return; }
                f.ShowDialog(this);
            }
            Ucitaj();
        }
    }
}
