using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    // ZAH-008: formule iz specifikacije "Izracun cena" (iste kao u sheetovima i u vw_IC_CenovnikObracun).
    // Procenti su razlomci: 0.25m = 25%.
    // Zaokruzivanje: VP, VP sa PDV, cene posle rabata i akcije na ceo dinar / evro;
    // marze se racunaju od NEzaokruzenih cena, kao u Excelu.
    public static class ObracunCene
    {
        public const string RSD = "RSD";
        public const string EUR = "EUR";

        // kao SQL ROUND(x, 0): polovina se zaokruzuje od nule
        public static decimal Zaokruzi(decimal iznos)
        {
            return Math.Round(iznos, 0, MidpointRounding.AwayFromZero);
        }

        public static decimal? Zaokruzi(decimal? iznos)
        {
            return iznos.HasValue ? Zaokruzi(iznos.Value) : (decimal?)null;
        }

        static bool Devizni(string valuta)
        {
            return valuta == EUR;
        }

        // TP u valuti cenovnika: dinarski TP, devizni TP / kurs
        public static decimal? TPuValuti(decimal? tp, string valuta, decimal kurs)
        {
            if (!tp.HasValue || tp.Value <= 0) return null;
            return Devizni(valuta) ? tp.Value / kurs : tp.Value;
        }

        // Marza: VP / TP - 1 (dinarski), VP * kurs / TP - 1 (devizni)
        public static decimal? Marza(decimal vp, decimal? tpUValuti)
        {
            if (!tpUValuti.HasValue || tpUValuti.Value <= 0) return null;
            return vp / tpUValuti.Value - 1;
        }

        // VP iz marze: TP * (1 + marza) [/ kurs], zaokruzeno
        public static decimal VPIzMarze(decimal tp, decimal marza, string valuta, decimal kurs)
        {
            decimal vp = tp * (1 + marza);
            if (Devizni(valuta)) vp = vp / kurs;
            return Zaokruzi(vp);
        }

        // VP = VP aktuelna * (1 + % povecanja), zaokruzeno
        public static decimal VPSaPovecanjem(decimal vpAktuelna, decimal povecanje)
        {
            return Zaokruzi(vpAktuelna * (1 + povecanje));
        }

        // VP sa PDV, samo dinarski
        public static decimal? VPsaPDV(decimal vp, decimal pdv, string valuta)
        {
            if (Devizni(valuta)) return null;
            return Zaokruzi(vp * (1 + pdv));
        }

        // Cena posle rabata, nezaokruzena (za marze)
        public static decimal CenaPosleRabata(decimal vp, decimal rabat)
        {
            return vp * (1 - rabat);
        }

        // Cena u akciji, nezaokruzena: svaki nivo od cene posle rabata, ne jedna na drugu
        public static decimal CenaAkcija(decimal cenaPosleRabata, decimal akcija)
        {
            return cenaPosleRabata * (1 - akcija);
        }

        // Jela marza: cena / TP - 1 (dinarski), cena / (TP / kurs) - 1 (devizni)
        public static decimal? JelaMarza(decimal cena, decimal? tpUValuti)
        {
            return Marza(cena, tpUValuti);
        }

        // Salon marza: (VP - rabatna) / rabatna, samo dinarski
        public static decimal? SalonMarza(decimal vp, decimal cenaPosleRabata, string valuta)
        {
            if (Devizni(valuta) || cenaPosleRabata == 0) return null;
            return (vp - cenaPosleRabata) / cenaPosleRabata;
        }

        // Iznos u evrima (dinarski cenovnik)
        public static decimal UEvrima(decimal iznos, decimal kurs)
        {
            return Zaokruzi(iznos / kurs);
        }

        // Rabat iz zeljene cene posle rabata: 1 - cena / VP
        public static decimal RabatIzCene(decimal vp, decimal cenaPosleRabata)
        {
            if (vp == 0) return 0;
            return 1 - cenaPosleRabata / vp;
        }

        // Promena troska: TP-Novi / TP-Stari - 1
        public static decimal? PromenaTroska(decimal? trosakNovi, decimal? trosakStari)
        {
            if (!trosakNovi.HasValue || !trosakStari.HasValue || trosakStari.Value == 0) return null;
            return trosakNovi.Value / trosakStari.Value - 1;
        }

        // Predlog: VP prati trosak = VP aktuelna * TP-Novi / TP-Stari
        public static decimal? PredlogVPPratiTrosak(decimal vpAktuelna, decimal? trosakNovi, decimal? trosakStari)
        {
            if (!trosakNovi.HasValue || !trosakStari.HasValue || trosakStari.Value == 0) return null;
            return Zaokruzi(vpAktuelna * trosakNovi.Value / trosakStari.Value);
        }

        // Predlog: VP za staru marzu = TP-Novi * (1 + marza stara) [/ kurs]
        public static decimal? PredlogVPStaraMarza(decimal? trosakNovi, decimal? marzaStara, string valuta, decimal kurs)
        {
            if (!trosakNovi.HasValue || !marzaStara.HasValue) return null;
            return VPIzMarze(trosakNovi.Value, marzaStara.Value, valuta, kurs);
        }

        /* ---------- preracun po Code ---------- */

        // VP se vodi po Code i vazi za sve njegove varijante (redove).
        public static void PostaviVP(IEnumerable<StavkaObracuna> redovi, string code, decimal vp)
        {
            foreach (var r in redovi.Where(x => x.Code == code))
                r.VP = vp;
        }

        // Administrator unosi marzu u red: VP se racuna iz TP tog reda, zaokruzuje,
        // pa se upisuje na sve varijante istog Code (njihove marze se racunaju iz te VP).
        // Vraca novu VP ili null ako red nema trosak.
        public static decimal? PostaviMarzu(IEnumerable<StavkaObracuna> redovi, StavkaObracuna red, decimal marza)
        {
            if (!red.TP.HasValue || red.TP.Value <= 0) return null;
            decimal vp = VPIzMarze(red.TP.Value, marza, red.Valuta, red.Kurs);
            PostaviVP(redovi, red.Code, vp);
            return vp;
        }

        // % povecanja VP za Code: VP = VP aktuelna * (1 + %)
        public static decimal PostaviPovecanje(IEnumerable<StavkaObracuna> redovi, StavkaObracuna red, decimal povecanje)
        {
            decimal vp = VPSaPovecanjem(red.VPAktuelna ?? red.VP, povecanje);
            foreach (var r in redovi.Where(x => x.Code == red.Code))
            {
                r.PovecanjeVP = povecanje;
                r.VP = vp;
            }
            return vp;
        }

        // Code cije varijante imaju razlicite marze (zuta oznaka u gridu)
        public static HashSet<string> CodeSaRazlicitimMarzama(IEnumerable<StavkaObracuna> redovi)
        {
            return new HashSet<string>(redovi
                .Where(r => r.Marza.HasValue)
                .GroupBy(r => r.Code)
                .Where(g => g.Select(r => Math.Round(r.Marza.Value, 4)).Distinct().Count() > 1)
                .Select(g => g.Key));
        }
    }

    // Jedan red cenovnika (maticni podatak). Unete vrednosti su polja,
    // izracunate su svojstva, pa se posle svake izmene odmah vide nove vrednosti.
    // Grid se osvezava pozivom Obavesti() na redovima koji su promenjeni.
    public partial class StavkaObracuna : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        static readonly string[] svaSvojstva = typeof(StavkaObracuna).GetProperties().Select(p => p.Name).ToArray();

        public void Obavesti()
        {
            var h = PropertyChanged;
            if (h == null) return;
            foreach (var n in svaSvojstva)
                h(this, new PropertyChangedEventArgs(n));
        }

        // kljucevi i opis
        public int StavkaID { get; set; }
        public int LargoProizvodID { get; set; }
        public string SifraProizvodaLargo { get; set; }
        public string Code { get; set; }
        public string NazivProizvoda { get; set; }
        public int ProgramID { get; set; }
        public string Program { get; set; }
        public int KolekcijaID { get; set; }
        public string Kolekcija { get; set; }

        // zaglavlje cenovnika
        public string Valuta { get; set; }
        public decimal Kurs { get; set; }
        public decimal PDV { get; set; }

        // troskovi
        public decimal? TrosakStari { get; set; }
        public decimal? TrosakNovi { get; set; }
        public decimal? MarzaStara { get; set; }
        public bool BezTroskaUBazi { get; set; }

        // cena po Code
        public decimal? VPAktuelna { get; set; }
        public decimal PovecanjeVP { get; set; }
        public decimal VP { get; set; }
        public bool VPIzBaze { get; set; }

        // rabat i vazeca akcija (vec razreseni po nivou), akcije za poredjenje
        public decimal Rabat { get; set; }
        public decimal? Akcija { get; set; }
        public List<decimal> AkcijePoredjenje { get; } = new List<decimal>();

        // oznaka za grid: izmenjeno, a nije sacuvano
        public bool Izmenjen { get; set; }

        /* ---------- izracunato ---------- */

        // TP reda = TP-Novi, a ako ga nema, TP-Stari (isto kao vw_IC_CenovnikObracun)
        public decimal? TP
        {
            get { return TrosakNovi ?? TrosakStari; }
        }

        public bool BezTroska
        {
            get { return BezTroskaUBazi || !TP.HasValue || TP.Value <= 0; }
        }

        public decimal? TPuValuti
        {
            get { return ObracunCene.TPuValuti(TP, Valuta, Kurs); }
        }

        public decimal? PromenaTroska
        {
            get { return ObracunCene.PromenaTroska(TrosakNovi, TrosakStari); }
        }

        public decimal? Marza
        {
            get { return ObracunCene.Marza(VP, TPuValuti); }
        }

        public decimal? VPsaPDV
        {
            get { return ObracunCene.VPsaPDV(VP, PDV, Valuta); }
        }

        decimal CenaRabatTacna
        {
            get { return ObracunCene.CenaPosleRabata(VP, Rabat); }
        }

        decimal CenaAkcijaTacna
        {
            get { return ObracunCene.CenaAkcija(CenaRabatTacna, Akcija ?? 0); }
        }

        public decimal CenaPosleRabata
        {
            get { return ObracunCene.Zaokruzi(CenaRabatTacna); }
        }

        public decimal? JelaMarza
        {
            get { return ObracunCene.JelaMarza(CenaRabatTacna, TPuValuti); }
        }

        public decimal? SalonMarza
        {
            get { return ObracunCene.SalonMarza(VP, CenaRabatTacna, Valuta); }
        }

        public decimal? CenaAkcija
        {
            get { return Akcija.HasValue ? ObracunCene.Zaokruzi(CenaAkcijaTacna) : (decimal?)null; }
        }

        public decimal? JelaMarzaAkcija
        {
            get { return Akcija.HasValue ? ObracunCene.JelaMarza(CenaAkcijaTacna, TPuValuti) : null; }
        }

        public decimal? PredlogVPPratiTrosak
        {
            get { return ObracunCene.PredlogVPPratiTrosak(VPAktuelna ?? VP, TrosakNovi, TrosakStari); }
        }

        public decimal? PredlogVPStaraMarza
        {
            get { return ObracunCene.PredlogVPStaraMarza(TrosakNovi, MarzaStara, Valuta, Kurs); }
        }

        // akcija za poredjenje sa indeksom i: cena i Jela marza
        public decimal CenaAkcijaPoredjenje(int i)
        {
            return ObracunCene.Zaokruzi(ObracunCene.CenaAkcija(CenaRabatTacna, AkcijePoredjenje[i]));
        }

        public decimal? JelaMarzaAkcijaPoredjenje(int i)
        {
            return ObracunCene.JelaMarza(ObracunCene.CenaAkcija(CenaRabatTacna, AkcijePoredjenje[i]), TPuValuti);
        }

        // do tri akcije za poredjenje kao kolone grida (zaglavlje nosi procenat)
        decimal? PorCena(int i) { return AkcijePoredjenje.Count > i ? CenaAkcijaPoredjenje(i) : (decimal?)null; }
        decimal? PorJela(int i) { return AkcijePoredjenje.Count > i ? JelaMarzaAkcijaPoredjenje(i) : null; }
        public decimal? Poredjenje1Cena { get { return PorCena(0); } }
        public decimal? Poredjenje1Jela { get { return PorJela(0); } }
        public decimal? Poredjenje2Cena { get { return PorCena(1); } }
        public decimal? Poredjenje2Jela { get { return PorJela(1); } }
        public decimal? Poredjenje3Cena { get { return PorCena(2); } }
        public decimal? Poredjenje3Jela { get { return PorJela(2); } }

        // Code cije varijante imaju razlicite marze (postavlja forma, zuta boja)
        public bool RazliciteMarze { get; set; }

        // iznosi u evrima (dinarski cenovnik)
        public decimal? VPuEUR
        {
            get { return Valuta == ObracunCene.RSD ? ObracunCene.UEvrima(VP, Kurs) : (decimal?)null; }
        }

        public decimal? CenaPosleRabatauEUR
        {
            get { return Valuta == ObracunCene.RSD ? ObracunCene.UEvrima(CenaRabatTacna, Kurs) : (decimal?)null; }
        }

        // zeljena cena posle rabata -> rabat za ovaj red
        public void PostaviCenuPosleRabata(decimal cena)
        {
            Rabat = ObracunCene.RabatIzCene(VP, cena);
        }
    }
}
