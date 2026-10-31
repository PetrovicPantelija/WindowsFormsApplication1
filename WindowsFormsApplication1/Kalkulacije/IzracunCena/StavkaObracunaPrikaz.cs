using System.Linq;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    // Svojstva za grid: procenti u jedinicama procenta (25 = 25%), kako korisnik unosi.
    // Kolone koje se menjaju su "...Unos" / "...Pct" sa set-om: set samo pamti unetu vrednost
    // (Unos, UnosPolje), a kontrola ucCenovnik je primenjuje na sve redove istog Code.
    public partial class StavkaObracuna
    {
        public const string PoljeVP = "VP";
        public const string PoljePovecanje = "Povecanje";
        public const string PoljeMarza = "Marza";
        public const string PoljeRabat = "Rabat";
        public const string PoljeAkcija = "Akcija";
        public const string PoljeCenaRabat = "CenaRabat";

        // poslednji unos iz grida, ceka primenu
        public decimal? Unos { get; private set; }
        public string UnosPolje { get; private set; }

        void Zapamti(string polje, decimal? vrednost)
        {
            UnosPolje = polje;
            Unos = vrednost;
        }

        public void OcistiUnos()
        {
            UnosPolje = null;
            Unos = null;
        }

        static decimal? Pct(decimal? razlomak)
        {
            return razlomak.HasValue ? razlomak.Value * 100 : (decimal?)null;
        }

        /* ---------- prikaz iznosa u evrima ("Devizni") ----------
           Dinarski cenovnik: iznosi se prikazuju podeljeni kursom sa forme; unos u evrima se
           pre primene vraca u dinare (iznos * kurs). Obracun i cuvanje su uvek u valuti cenovnika. */

        public bool PrikazEUR { get; set; }

        bool UEUR { get { return PrikazEUR && Kurs > 0; } }

        decimal? Iznos(decimal? iznos)
        {
            return iznos.HasValue && UEUR ? iznos.Value / Kurs : iznos;
        }

        decimal Iznos(decimal iznos)
        {
            return UEUR ? iznos / Kurs : iznos;
        }

        decimal UValutu(decimal unos)
        {
            return UEUR ? unos * Kurs : unos;
        }

        /* ---------- kolone koje se menjaju ---------- */

        // VP nova
        public decimal VPUnos
        {
            get { return Iznos(VP); }
            set { Zapamti(PoljeVP, UValutu(value)); }
        }

        // stvarni % (VP nova / VP aktuelna - 1); unos postavlja VP = VP aktuelna * (1 + %)
        public decimal PovecanjeVPPct
        {
            get { return PovecanjeVPStvarno * 100; }
            set { Zapamti(PoljePovecanje, value); }
        }

        public decimal? MarzaPct
        {
            get { return Pct(Marza); }
            set { Zapamti(PoljeMarza, value); }
        }

        public decimal RabatPct
        {
            get { return Rabat * 100; }
            set { Zapamti(PoljeRabat, value); }
        }

        public decimal? AkcijaPct
        {
            get { return Pct(Akcija); }
            set { Zapamti(PoljeAkcija, value); }
        }

        public decimal CenaPosleRabataUnos
        {
            get { return Iznos(CenaPosleRabata); }
            set { Zapamti(PoljeCenaRabat, UValutu(value)); }
        }

        /* ---------- samo prikaz: iznosi (u evrima kad je ukljucen Devizni) ---------- */

        public decimal? TrosakStariPrikaz { get { return Iznos(TrosakStari); } }
        public decimal? TrosakNoviPrikaz { get { return Iznos(TrosakNovi); } }
        public decimal? VPAktuelnaPrikaz { get { return Iznos(VPAktuelna); } }
        public decimal? RazlikaVPPrikaz { get { return Iznos(RazlikaVP); } }
        public decimal? VPsaPDVPrikaz { get { return Iznos(VPsaPDV); } }
        public decimal? CenaAkcijaPrikaz { get { return Iznos(CenaAkcija); } }
        public decimal? Poredjenje1CenaPrikaz { get { return Iznos(Poredjenje1Cena); } }
        public decimal? Poredjenje2CenaPrikaz { get { return Iznos(Poredjenje2Cena); } }
        public decimal? Poredjenje3CenaPrikaz { get { return Iznos(Poredjenje3Cena); } }
        public decimal? PredlogVPPratiTrosakPrikaz { get { return Iznos(PredlogVPPratiTrosak); } }
        public decimal? PredlogVPStaraMarzaPrikaz { get { return Iznos(PredlogVPStaraMarza); } }

        /* ---------- samo prikaz: procenti ---------- */

        public decimal? PromenaTroskaPct { get { return Pct(PromenaTroska); } }
        public decimal? MarzaStaraPct { get { return Pct(MarzaStaraObracun); } }
        public decimal? PromenaMarzePct { get { return Pct(PromenaMarze); } }   // procentni poeni
        public decimal? JelaMarzaPct { get { return Pct(JelaMarza); } }
        public decimal? SalonMarzaPct { get { return Pct(SalonMarza); } }
        public decimal? JelaMarzaAkcijaPct { get { return Pct(JelaMarzaAkcija); } }
        public decimal? Poredjenje1JelaPct { get { return Pct(Poredjenje1Jela); } }
        public decimal? Poredjenje2JelaPct { get { return Pct(Poredjenje2Jela); } }
        public decimal? Poredjenje3JelaPct { get { return Pct(Poredjenje3Jela); } }

        // "5% / 13%" - akcije za poredjenje koje vaze za red
        public string AkcijePoredjenjeTekst
        {
            get { return string.Join(" / ", AkcijePoredjenje.Select(a => (a * 100).ToString("0.##") + "%")); }
        }
    }
}
