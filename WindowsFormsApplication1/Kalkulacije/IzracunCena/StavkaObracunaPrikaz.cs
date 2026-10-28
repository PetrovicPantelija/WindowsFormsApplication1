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

        /* ---------- kolone koje se menjaju ---------- */

        public decimal VPUnos
        {
            get { return VP; }
            set { Zapamti(PoljeVP, value); }
        }

        public decimal PovecanjeVPPct
        {
            get { return PovecanjeVP * 100; }
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
            get { return CenaPosleRabata; }
            set { Zapamti(PoljeCenaRabat, value); }
        }

        /* ---------- samo prikaz ---------- */

        public decimal? PromenaTroskaPct { get { return Pct(PromenaTroska); } }
        public decimal? MarzaStaraPct { get { return Pct(MarzaStara); } }
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
