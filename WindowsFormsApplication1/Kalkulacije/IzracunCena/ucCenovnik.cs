using System;
using System.Collections.Generic;
using System.Configuration;
using DataRow = System.Data.DataRow;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Syncfusion.Data;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    public enum RezimCenovnika
    {
        Administrator,   // menja VP, marzu i % povecanja (opsti cenovnik)
        Korisnik,        // menja rabat, vazecu akciju, VP i zeljenu cenu posle rabata (radna kopija)
        Citanje          // samo pregled
    }

    // ZAH-008: stablo Program -> Kolekcija, grid cenovnika i slika izabranog reda.
    // Cenovnik se ucitava ceo jednom; stablo samo filtrira, a izmene se racunaju u C# (ObracunCene).
    public partial class ucCenovnik : UserControl
    {
        static readonly Color Zelena = Color.FromArgb(58, 77, 57);
        static readonly Color Krem = Color.FromArgb(248, 247, 244);
        static readonly Color ZaUnos = Color.FromArgb(255, 226, 190);      // narandzaste celije za unos
        static readonly Color Siva = Color.FromArgb(225, 225, 225);        // red bez troska
        static readonly Color Zuta = Color.FromArgb(255, 245, 160);        // Code sa razlicitim marzama
        static readonly Color Izmenjeno = Color.FromArgb(200, 225, 255);   // nesacuvana izmena
        static readonly Color Rast = Color.FromArgb(200, 40, 40);
        static readonly Color Pad = Color.FromArgb(30, 130, 60);

        readonly IzracunCenaBaza baza = new IzracunCenaBaza();
        readonly Dictionary<string, string> slike = new Dictionary<string, string>();
        HashSet<int> izabraneKolekcije = new HashSet<int>();
        bool stabloSeMenja;

        public RezimCenovnika Rezim { get; private set; }
        public int CenovnikID { get; private set; }
        public DataRow Zaglavlje { get; private set; }
        public string Valuta { get; private set; }
        public List<StavkaObracuna> Stavke { get; private set; } = new List<StavkaObracuna>();
        public ParametriCenovnika Parametri { get; private set; } = new ParametriCenovnika();

        // svaka izmena u gridu ili parametrima (forma osvezava status)
        public event EventHandler Promenjeno;

        public ucCenovnik()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            PodesiGrid();
        }

        /* =====================================================================
           UCITAVANJE
           ===================================================================== */

        public void Ucitaj(int cenovnikID, RezimCenovnika rezim)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Rezim = rezim;
                CenovnikID = cenovnikID;
                Zaglavlje = baza.Zaglavlje(cenovnikID);
                Valuta = Zaglavlje["Valuta"].ToString();
                Stavke = baza.Stavke(cenovnikID);
                Parametri = baza.Parametri(cenovnikID);
                Parametri.Primeni(Stavke);
                OznaciRazliciteMarze(Stavke);
                slike.Clear();

                PopuniStablo();
                NapraviKolone();
                grid.DataSource = Stavke;
                grid.View.Filter = Prikazan;
                grid.View.RefreshFilter();
                tsmDodeliSliku.Visible = rezim != RezimCenovnika.Citanje;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            JaviPromenu();
        }

        // osvezava troskove samo zadatih redova i zaglavlje; stablo, filter i izbor ostaju
        public void OsveziTroskove(IEnumerable<int> stavkaIDs)
        {
            var dt = baza.TroskoviStavki(CenovnikID, stavkaIDs);
            var poID = Stavke.ToDictionary(s => s.StavkaID);
            var promenjeni = new List<StavkaObracuna>();
            foreach (DataRow r in dt.Rows)
            {
                StavkaObracuna s;
                if (!poID.TryGetValue((int)r["StavkaID"], out s)) continue;
                s.TrosakStari = r.IsNull("TrosakStari") ? (decimal?)null : Convert.ToDecimal(r["TrosakStari"]);
                s.TrosakNovi = r.IsNull("TrosakNovi") ? (decimal?)null : Convert.ToDecimal(r["TrosakNovi"]);
                s.MarzaStara = r.IsNull("MarzaStara") ? (decimal?)null : Convert.ToDecimal(r["MarzaStara"]);
                s.BezTroskaUBazi = Convert.ToBoolean(r["BezTroska"]);
                promenjeni.Add(s);
            }
            foreach (var s in promenjeni) s.Obavesti();
            Zaglavlje = baza.Zaglavlje(CenovnikID);
            grid.Invalidate();
            JaviPromenu();
        }

        void PopuniStablo()
        {
            stabloSeMenja = true;
            trvProgrami.BeginUpdate();
            trvProgrami.Nodes.Clear();
            foreach (var p in Stavke.GroupBy(s => new { s.ProgramID, s.Program }).OrderBy(g => g.Key.Program))
            {
                var np = new TreeNode(p.Key.Program + " (" + p.Count() + ")") { Tag = p.Key.ProgramID, Checked = true };
                foreach (var k in p.GroupBy(s => new { s.KolekcijaID, s.Kolekcija }).OrderBy(g => g.Key.Kolekcija))
                    np.Nodes.Add(new TreeNode(k.Key.Kolekcija + " (" + k.Count() + ")") { Tag = k.Key.KolekcijaID, Checked = true });
                trvProgrami.Nodes.Add(np);
            }
            trvProgrami.ExpandAll();
            trvProgrami.EndUpdate();
            stabloSeMenja = false;
            izabraneKolekcije = new HashSet<int>(Stavke.Select(s => s.KolekcijaID));
        }

        /* =====================================================================
           GRID
           ===================================================================== */

        void PodesiGrid()
        {
            grid.AllowEditing = true;
            grid.EditMode = EditMode.SingleClick;
            grid.AllowGrouping = false;
            grid.ShowGroupDropArea = false;
            grid.AutoExpandGroups = true;
            grid.FrozenColumnCount = 3;
            grid.SelectionMode = GridSelectionMode.Extended;
            grid.LiveDataUpdateMode = LiveDataUpdateMode.AllowSummaryUpdate;
            grid.GroupCaptionTextFormat = "{Key}";   // broj redova je u stablu
            grid.Style.HeaderStyle.BackColor = Zelena;
            grid.Style.HeaderStyle.TextColor = Color.White;
            grid.Style.GroupSummaryRowStyle.BackColor = Krem;
            grid.Style.CaptionSummaryRowStyle.BackColor = Color.FromArgb(232, 228, 218);

            var prosek = new GridSummaryRow { Name = "Prosek", ShowSummaryInRow = false, Title = "Prosek marže" };
            prosek.SummaryColumns.Add(new GridSummaryColumn
            {
                Name = "ProsekMarze", MappingName = "MarzaPct",
                SummaryType = SummaryType.DoubleAggregate, Format = "{Average:N2}"
            });
            prosek.SummaryColumns.Add(new GridSummaryColumn
            {
                Name = "ProsekJela", MappingName = "JelaMarzaPct",
                SummaryType = SummaryType.DoubleAggregate, Format = "{Average:N2}"
            });
            grid.GroupSummaryRows.Add(prosek);

            grid.QueryCellStyle += Grid_QueryCellStyle;
            grid.CurrentCellBeginEdit += Grid_CurrentCellBeginEdit;
            grid.CurrentCellValidating += Grid_CurrentCellValidating;
            grid.CurrentCellEndEdit += Grid_CurrentCellEndEdit;
            grid.SelectionChanged += (s, e) => PrikaziSliku();
        }

        static NumberFormatInfo Brojevi(int decimala)
        {
            var nf = (NumberFormatInfo)CultureInfo.GetCultureInfo("sr-Latn-RS").NumberFormat.Clone();
            nf.NumberDecimalDigits = decimala;
            return nf;
        }

        GridNumericColumn Broj(string mapa, string naslov, int decimala, int sirina, bool unos = false)
        {
            return new GridNumericColumn
            {
                MappingName = mapa, HeaderText = naslov, Width = sirina,
                NumberFormatInfo = Brojevi(decimala), AllowEditing = unos, AllowNull = true
            };
        }

        void NapraviKolone()
        {
            bool admin = Rezim == RezimCenovnika.Administrator;
            bool korisnik = Rezim == RezimCenovnika.Korisnik;
            bool rsd = Valuta == ObracunCene.RSD;
            string v = rsd ? "din" : "€";

            grid.Columns.Clear();
            var bezGrupa = Brojevi(0);
            bezGrupa.NumberGroupSeparator = "";
            grid.Columns.Add(new GridNumericColumn { MappingName = "LargoProizvodID", HeaderText = "ID", Width = 70, NumberFormatInfo = bezGrupa, AllowEditing = false });
            grid.Columns.Add(new GridTextColumn { MappingName = "Code", HeaderText = "Code", Width = 110, AllowEditing = false });
            grid.Columns.Add(new GridTextColumn { MappingName = "NazivProizvoda", HeaderText = "Naziv", Width = 200, AllowEditing = false });
            grid.Columns.Add(new GridTextColumn { MappingName = "SifraProizvodaLargo", HeaderText = "Šifra Largo", Width = 120, AllowEditing = false });
            grid.Columns.Add(new GridTextColumn { MappingName = "Program", HeaderText = "Program", Visible = false });
            grid.Columns.Add(new GridTextColumn { MappingName = "Kolekcija", HeaderText = "Kolekcija", Visible = false });

            grid.Columns.Add(Broj("TrosakStari", "TP-Stari", 2, 90));
            grid.Columns.Add(Broj("TrosakNovi", "TP-Novi", 2, 90));
            grid.Columns.Add(Broj("PromenaTroskaPct", "Promena TP %", 2, 85));
            grid.Columns.Add(Broj("VPAktuelna", "VP aktuelna", 0, 85));
            grid.Columns.Add(Broj("PovecanjeVPPct", "% povećanja VP", 2, 90, admin));
            grid.Columns.Add(Broj("VPUnos", "VP (" + v + ")", 0, 85, admin || korisnik));
            grid.Columns.Add(Broj("MarzaPct", "Marža %", 2, 80, admin));
            grid.Columns.Add(Broj("MarzaStaraPct", "Marža stara %", 2, 85));
            if (rsd) grid.Columns.Add(Broj("VPsaPDV", "VP sa PDV", 0, 85));
            grid.Columns.Add(Broj("RabatPct", "Rabat %", 2, 70, korisnik));
            grid.Columns.Add(Broj("CenaPosleRabataUnos", "Posle rabata", 0, 90, korisnik));
            grid.Columns.Add(Broj("JelaMarzaPct", "Jela marža %", 2, 85));
            if (rsd) grid.Columns.Add(Broj("SalonMarzaPct", "Salon marža %", 2, 90));
            grid.Columns.Add(Broj("AkcijaPct", "Akcija %", 2, 70, korisnik));
            grid.Columns.Add(Broj("CenaAkcija", "Cena u akciji", 0, 90));
            grid.Columns.Add(Broj("JelaMarzaAkcijaPct", "Jela marža akcija %", 2, 110));
            grid.Columns.Add(new GridTextColumn { MappingName = "AkcijePoredjenjeTekst", HeaderText = "Akcije za poređenje", Width = 120, AllowEditing = false });
            grid.Columns.Add(Broj("Poredjenje1Cena", "Por. 1 cena", 0, 80));
            grid.Columns.Add(Broj("Poredjenje1JelaPct", "Por. 1 Jela %", 2, 85));
            grid.Columns.Add(Broj("Poredjenje2Cena", "Por. 2 cena", 0, 80));
            grid.Columns.Add(Broj("Poredjenje2JelaPct", "Por. 2 Jela %", 2, 85));
            grid.Columns.Add(Broj("Poredjenje3Cena", "Por. 3 cena", 0, 80));
            grid.Columns.Add(Broj("Poredjenje3JelaPct", "Por. 3 Jela %", 2, 85));
            if (admin)
            {
                grid.Columns.Add(Broj("PredlogVPPratiTrosak", "Predlog: prati trošak", 0, 115));
                grid.Columns.Add(Broj("PredlogVPStaraMarza", "Predlog: stara marža", 0, 115));
            }
            if (rsd)
            {
                grid.Columns.Add(Broj("VPuEUR", "VP €", 0, 70));
                grid.Columns.Add(Broj("CenaPosleRabatauEUR", "Posle rabata €", 0, 90));
                PrikaziEUR(false);
            }

            // kolone za poredjenje se vide samo ako postoje akcije za poredjenje
            int najvise = Stavke.Count == 0 ? 0 : Stavke.Max(s => s.AkcijePoredjenje.Count);
            for (int i = 1; i <= 3; i++)
            {
                grid.Columns["Poredjenje" + i + "Cena"].Visible = i <= najvise;
                grid.Columns["Poredjenje" + i + "JelaPct"].Visible = i <= najvise;
            }
            grid.Columns["AkcijePoredjenjeTekst"].Visible = najvise > 0;
            grid.AllowEditing = Rezim != RezimCenovnika.Citanje;

            // grupe se dodaju posle kolona (Columns.Clear ih brise)
            grid.GroupColumnDescriptions.Clear();
            grid.GroupColumnDescriptions.Add(new GroupColumnDescription { ColumnName = "Program" });
            grid.GroupColumnDescriptions.Add(new GroupColumnDescription { ColumnName = "Kolekcija" });
        }

        public void PrikaziEUR(bool prikazi)
        {
            if (grid.Columns["VPuEUR"] != null) grid.Columns["VPuEUR"].Visible = prikazi;
            if (grid.Columns["CenaPosleRabatauEUR"] != null) grid.Columns["CenaPosleRabatauEUR"].Visible = prikazi;
        }

        void Grid_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
        {
            if (e.DataRow == null || e.DataRow.RowType != RowType.DefaultRow) return;
            var r = e.DataRow.RowData as StavkaObracuna;
            if (r == null || e.Column == null) return;
            string m = e.Column.MappingName;

            if (r.BezTroska)
                e.Style.BackColor = Siva;
            else if (e.Column.AllowEditing && grid.AllowEditing)
                e.Style.BackColor = ZaUnos;

            if (r.RazliciteMarze && (m == "Code" || m == "MarzaPct"))
                e.Style.BackColor = Zuta;
            if (r.Izmenjen && (m == "VPUnos" || m == "MarzaPct" || m == "PovecanjeVPPct"))
                e.Style.BackColor = Izmenjeno;
            if (m == "VPUnos" && r.VPIzBaze)
                e.Style.TextColor = Color.FromArgb(150, 100, 40);

            if (m == "PromenaTroskaPct" && r.PromenaTroska.HasValue)
            {
                if (r.PromenaTroska.Value > 0) e.Style.TextColor = Rast;
                else if (r.PromenaTroska.Value < 0) e.Style.TextColor = Pad;
            }
        }

        /* =====================================================================
           IZMENE U GRIDU
           ===================================================================== */

        void Grid_CurrentCellBeginEdit(object sender, CurrentCellBeginEditEventArgs e)
        {
            var r = e.DataRow == null ? null : e.DataRow.RowData as StavkaObracuna;
            if (r != null && e.DataColumn != null && e.DataColumn.GridColumn.MappingName == "MarzaPct" && r.BezTroska)
            {
                e.Cancel = true;
                MessageBox.Show("Red nema trošak, pa se marža ne može uneti. Unesite VP cenu.", "Marža");
            }
        }

        void Grid_CurrentCellValidating(object sender, CurrentCellValidatingEventArgs e)
        {
            if (e.NewValue == null || e.NewValue == DBNull.Value) return;
            decimal v;
            try { v = Convert.ToDecimal(e.NewValue); }
            catch { e.IsValid = false; e.ErrorMessage = "Unesite broj."; return; }

            switch (e.Column.MappingName)
            {
                case "VPUnos":
                case "CenaPosleRabataUnos":
                    if (v <= 0) { e.IsValid = false; e.ErrorMessage = "Cena mora biti veća od nule."; }
                    break;
                case "MarzaPct":
                    if (v <= -100) { e.IsValid = false; e.ErrorMessage = "Marža mora biti veća od -100%."; }
                    break;
                case "RabatPct":
                case "AkcijaPct":
                    if (v < 0 || v > 100) { e.IsValid = false; e.ErrorMessage = "Procenat mora biti između 0 i 100."; }
                    break;
                case "PovecanjeVPPct":
                    if (v <= -100) { e.IsValid = false; e.ErrorMessage = "Povećanje mora biti veće od -100%."; }
                    break;
            }
        }

        void Grid_CurrentCellEndEdit(object sender, CurrentCellEndEditEventArgs e)
        {
            var r = e.DataRow == null ? null : e.DataRow.RowData as StavkaObracuna;
            if (r != null) PrimeniUnos(r);
        }

        // primenjuje unos iz reda na sve varijante istog Code
        void PrimeniUnos(StavkaObracuna red)
        {
            string polje = red.UnosPolje;
            decimal? v = red.Unos;
            red.OcistiUnos();
            if (polje == null) return;

            var istiCode = Stavke.Where(s => s.Code == red.Code).ToList();
            switch (polje)
            {
                case StavkaObracuna.PoljeVP:
                    if (!v.HasValue) return;
                    ObracunCene.PostaviVP(istiCode, red.Code, ObracunCene.Zaokruzi(v.Value));
                    OznaciIzmenjene(istiCode);
                    break;
                case StavkaObracuna.PoljePovecanje:
                    ObracunCene.PostaviPovecanje(istiCode, red, (v ?? 0) / 100);
                    OznaciIzmenjene(istiCode);
                    break;
                case StavkaObracuna.PoljeMarza:
                    if (!v.HasValue || ObracunCene.PostaviMarzu(istiCode, red, v.Value / 100) == null) return;
                    OznaciIzmenjene(istiCode);
                    break;
                case StavkaObracuna.PoljeRabat:
                    PostaviParametar(NivoParametra.Code, null, null, red.Code, (v ?? 0) / 100, true, null, false);
                    break;
                case StavkaObracuna.PoljeAkcija:
                    PostaviParametar(NivoParametra.Code, null, null, red.Code, null, false, v.HasValue ? v / 100 : null, true);
                    break;
                case StavkaObracuna.PoljeCenaRabat:
                    if (!v.HasValue || red.VP == 0) return;
                    PostaviParametar(NivoParametra.Code, null, null, red.Code, ObracunCene.RabatIzCene(red.VP, v.Value), true, null, false);
                    break;
            }
            Parametri.Primeni(istiCode);
            OznaciRazliciteMarze(istiCode);
            foreach (var s in istiCode) s.Obavesti();
            JaviPromenu();
        }

        static void OznaciIzmenjene(IEnumerable<StavkaObracuna> redovi)
        {
            foreach (var r in redovi) r.Izmenjen = true;
        }

        void PostaviParametar(NivoParametra nivo, int? programID, int? kolekcijaID, string code,
                              decimal? rabat, bool menjaRabat, decimal? akcija, bool menjaAkciju)
        {
            var p = Parametri.Nadji(nivo, programID, kolekcijaID, code, true);
            if (menjaRabat) { p.Rabat = rabat; p.PromenjenRabat = true; }
            if (menjaAkciju) { p.Akcija = akcija; p.PromenjenaAkcija = true; }
        }

        void OznaciRazliciteMarze(IEnumerable<StavkaObracuna> redovi)
        {
            var lista = redovi.ToList();
            var razliciti = ObracunCene.CodeSaRazlicitimMarzama(lista);
            foreach (var r in lista) r.RazliciteMarze = razliciti.Contains(r.Code);
        }

        void JaviPromenu()
        {
            Promenjeno?.Invoke(this, EventArgs.Empty);
        }

        /* =====================================================================
           GRUPNE IZMENE
           ===================================================================== */

        // redovi koje grid trenutno prikazuje (stablo + filteri kolona)
        public List<StavkaObracuna> Prikazani
        {
            get
            {
                if (grid.View == null) return new List<StavkaObracuna>();
                return grid.View.Records.Select(x => (StavkaObracuna)x.Data).ToList();
            }
        }

        public List<StavkaObracuna> Oznaceni
        {
            get { return grid.SelectedItems.OfType<StavkaObracuna>().ToList(); }
        }

        // Primeni na prikazane: ceo cenovnik / program / kolekcija dobija parametar tog nivoa,
        // inace (filter po koloni) svaki Code posebno. Vrednosti su u procentima, null = ne menja.
        public void PrimeniNaPrikazane(decimal? rabatPct, decimal? akcijaPct, decimal? povecanjePct)
        {
            var prikazani = Prikazani;
            if (prikazani.Count == 0) return;
            decimal? rabat = rabatPct / 100, akcija = akcijaPct / 100;

            if (rabatPct.HasValue || akcijaPct.HasValue)
            {
                bool filterKolona = prikazani.Count != Stavke.Count(s => izabraneKolekcije.Contains(s.KolekcijaID));
                var nivoi = new List<ParametarCenovnika>();

                if (filterKolona)
                {
                    foreach (var code in prikazani.Select(s => s.Code).Distinct())
                        nivoi.Add(new ParametarCenovnika { Nivo = NivoParametra.Code, Code = code });
                }
                else if (izabraneKolekcije.Count == Stavke.Select(s => s.KolekcijaID).Distinct().Count())
                {
                    nivoi.Add(new ParametarCenovnika { Nivo = NivoParametra.Cenovnik });
                }
                else
                {
                    foreach (TreeNode np in trvProgrami.Nodes)
                    {
                        var kol = np.Nodes.Cast<TreeNode>().ToList();
                        if (kol.All(k => k.Checked))
                            nivoi.Add(new ParametarCenovnika { Nivo = NivoParametra.Program, ProgramID = (int)np.Tag });
                        else
                            foreach (var k in kol.Where(k => k.Checked))
                                nivoi.Add(new ParametarCenovnika { Nivo = NivoParametra.Kolekcija, KolekcijaID = (int)k.Tag });
                    }
                }

                foreach (var n in nivoi)
                {
                    PostaviParametar(n.Nivo, n.ProgramID, n.KolekcijaID, n.Code,
                                     rabat, rabatPct.HasValue, akcija, akcijaPct.HasValue);
                    PregaziNizeNivoe(n, rabatPct.HasValue, akcijaPct.HasValue);
                }
            }

            if (povecanjePct.HasValue)
            {
                foreach (var g in prikazani.GroupBy(s => s.Code))
                {
                    var istiCode = Stavke.Where(s => s.Code == g.Key).ToList();
                    ObracunCene.PostaviPovecanje(istiCode, g.First(), povecanjePct.Value / 100);
                    OznaciIzmenjene(istiCode);
                }
            }

            Parametri.Primeni(Stavke);
            OznaciRazliciteMarze(Stavke);
            OsveziGrid();
            JaviPromenu();
        }

        // posle "Primeni na prikazane" nizi nivoi unutar izabranog vise ne vaze;
        // brisu se ovde (i salju kao izmenjeni), pa baza ne mora sama da ih brise
        void PregaziNizeNivoe(ParametarCenovnika visi, bool rabat, bool akcija)
        {
            var redovi = Stavke.Where(visi.Vazi).ToList();
            foreach (var p in Parametri.Svi.Where(p => p.Nivo > visi.Nivo && redovi.Any(p.Vazi)))
            {
                if (rabat && p.Rabat.HasValue) { p.Rabat = null; p.PromenjenRabat = true; }
                if (akcija && p.Akcija.HasValue) { p.Akcija = null; p.PromenjenaAkcija = true; }
            }
        }

        // Primeni predlog na oznacene redove (administrator)
        public int PrimeniPredlog(bool pratiTrosak)
        {
            int broj = 0;
            foreach (var g in Oznaceni.GroupBy(s => s.Code))
            {
                var red = g.First();
                decimal? vp = pratiTrosak ? red.PredlogVPPratiTrosak : red.PredlogVPStaraMarza;
                if (!vp.HasValue || vp.Value <= 0) continue;
                var istiCode = Stavke.Where(s => s.Code == g.Key).ToList();
                ObracunCene.PostaviVP(istiCode, g.Key, vp.Value);
                OznaciIzmenjene(istiCode);
                broj++;
            }
            OznaciRazliciteMarze(Stavke);
            OsveziGrid();
            JaviPromenu();
            return broj;
        }

        public void PostaviKurs(decimal kurs)
        {
            foreach (var s in Stavke) s.Kurs = kurs;
            OsveziGrid();
            JaviPromenu();
        }

        public void PostaviPDV(decimal pdv)
        {
            foreach (var s in Stavke) s.PDV = pdv;
            OsveziGrid();
            JaviPromenu();
        }

        public void OsveziGrid()
        {
            if (grid.View == null) return;
            grid.View.Refresh();
        }

        /* =====================================================================
           CUVANJE
           ===================================================================== */

        public List<StavkaObracuna> IzmenjeneCene
        {
            get { return Stavke.Where(s => s.Izmenjen).ToList(); }
        }

        public List<ParametarCenovnika> IzmenjeniParametri
        {
            get { return Parametri.Svi.Where(p => p.PromenjenRabat || p.PromenjenaAkcija).ToList(); }
        }

        public int BrojIzmena
        {
            get { return IzmenjeneCene.Select(s => s.Code).Distinct().Count() + IzmenjeniParametri.Count; }
        }

        public void Sacuvaj(string korisnik)
        {
            if (Rezim == RezimCenovnika.Citanje) return;
            var cene = IzmenjeneCene;
            if (cene.Count > 0) baza.SacuvajCene(CenovnikID, cene, korisnik);
            var par = IzmenjeniParametri;
            if (par.Count > 0) baza.SacuvajParametre(CenovnikID, par, korisnik, false);

            foreach (var s in cene) s.Izmenjen = false;
            foreach (var p in par) { p.PromenjenRabat = false; p.PromenjenaAkcija = false; }
            OsveziGrid();
            JaviPromenu();
        }

        /* =====================================================================
           STABLO
           ===================================================================== */

        bool Prikazan(object o)
        {
            var s = o as StavkaObracuna;
            return s != null && izabraneKolekcije.Contains(s.KolekcijaID);
        }

        void trvProgrami_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (stabloSeMenja) return;
            stabloSeMenja = true;
            if (e.Node.Parent == null)
                foreach (TreeNode k in e.Node.Nodes) k.Checked = e.Node.Checked;
            else
                e.Node.Parent.Checked = e.Node.Parent.Nodes.Cast<TreeNode>().Any(k => k.Checked);
            stabloSeMenja = false;
            PrimeniStablo();
        }

        void PrimeniStablo()
        {
            izabraneKolekcije = new HashSet<int>(trvProgrami.Nodes.Cast<TreeNode>()
                .SelectMany(p => p.Nodes.Cast<TreeNode>())
                .Where(k => k.Checked)
                .Select(k => (int)k.Tag));
            if (grid.View != null) grid.View.RefreshFilter();
            JaviPromenu();
        }

        void OznaciSve(bool oznaci)
        {
            stabloSeMenja = true;
            foreach (TreeNode p in trvProgrami.Nodes)
            {
                p.Checked = oznaci;
                foreach (TreeNode k in p.Nodes) k.Checked = oznaci;
            }
            stabloSeMenja = false;
            PrimeniStablo();
        }

        void btnSve_Click(object sender, EventArgs e) { OznaciSve(true); }

        void btnNista_Click(object sender, EventArgs e) { OznaciSve(false); }

        /* =====================================================================
           SLIKE
           ===================================================================== */

        public static string KorenSlika
        {
            get { return ConfigurationManager.AppSettings["IC_KorenSlika"] ?? ""; }
        }

        void PrikaziSliku()
        {
            var r = grid.SelectedItem as StavkaObracuna;
            var stara = picSlika.Image;
            picSlika.Image = null;
            if (stara != null) stara.Dispose();
            lblSlika.Text = "Slika";
            if (r == null) return;

            string putanja;
            if (!slike.TryGetValue(r.Code, out putanja))
            {
                putanja = baza.GlavnaSlika(r.Code);
                slike[r.Code] = putanja;
            }
            lblSlika.Text = r.Code;
            if (string.IsNullOrEmpty(putanja)) { lblSlika.Text = r.Code + " - nema slike"; return; }

            string pun = Path.Combine(KorenSlika, putanja);
            if (!File.Exists(pun)) { lblSlika.Text = r.Code + " - fajl ne postoji"; return; }
            // kopija u memoriji, da fajl ostane slobodan
            using (var fs = new FileStream(pun, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var img = Image.FromStream(fs))
                picSlika.Image = new Bitmap(img);
        }

        void tsmDodeliSliku_Click(object sender, EventArgs e)
        {
            var r = grid.SelectedItem as StavkaObracuna;
            if (r == null) return;
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Slika za " + r.Code;
                dlg.Filter = "Slike (*.jpg;*.png)|*.jpg;*.jpeg;*.png";
                if (Directory.Exists(KorenSlika)) dlg.InitialDirectory = KorenSlika;
                if (dlg.ShowDialog() != DialogResult.OK) return;

                string koren = Path.GetFullPath(KorenSlika).TrimEnd('\\') + "\\";
                string fajl = Path.GetFullPath(dlg.FileName);
                if (!fajl.StartsWith(koren, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Slika mora biti u folderu sa slikama:\n" + koren, "Dodeli sliku");
                    return;
                }
                baza.DodeliSlikuRucno(r.Code, fajl.Substring(koren.Length), true);
                slike.Remove(r.Code);
                PrikaziSliku();
            }
        }
    }
}
