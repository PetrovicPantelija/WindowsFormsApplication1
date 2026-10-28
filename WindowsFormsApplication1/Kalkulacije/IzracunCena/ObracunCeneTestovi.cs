using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    // ZAH-008: testovi obracuna na brojevima iz Excela (KRT90, sheet Decje sobe).
    // Pokretanje: ObracunCeneTestovi.Pokreni() vraca spisak provera; Greske() samo neuspesne.
    public static class ObracunCeneTestovi
    {
        public class Provera
        {
            public string Naziv;
            public decimal? Ocekivano;
            public decimal? Dobijeno;
            public bool OK { get { return Ocekivano == Dobijeno; } }

            public override string ToString()
            {
                return (OK ? "OK     " : "GRESKA ") + Naziv + ": ocekivano " + Tekst(Ocekivano) + ", dobijeno " + Tekst(Dobijeno);
            }

            static string Tekst(decimal? d)
            {
                return d.HasValue ? d.Value.ToString(CultureInfo.InvariantCulture) : "null";
            }
        }

        static decimal? R(decimal? d, int decimala)
        {
            return d.HasValue ? Math.Round(d.Value, decimala, MidpointRounding.AwayFromZero) : (decimal?)null;
        }

        static StavkaObracuna Red(string valuta, decimal? tpStari, decimal? tpNovi, decimal vp, decimal rabat, decimal? akcija)
        {
            return new StavkaObracuna
            {
                LargoProizvodID = 1, Code = "KRT90", ProgramID = 1, KolekcijaID = 10,
                Valuta = valuta, Kurs = 117.93m, PDV = 0.20m,
                TrosakStari = tpStari, TrosakNovi = tpNovi,
                VPAktuelna = vp, VP = vp, Rabat = rabat, Akcija = akcija
            };
        }

        public static List<Provera> Pokreni()
        {
            var p = new List<Provera>();
            Action<string, decimal?, decimal?> dodaj = (naziv, ocekivano, dobijeno) =>
                p.Add(new Provera { Naziv = naziv, Ocekivano = ocekivano, Dobijeno = dobijeno });

            /* ---------- Devizni: TP 6.449, VP 129, kurs 117,93, rabat 25%, akcije 5% i 13% ---------- */
            var d = Red(ObracunCene.EUR, 6487m, 6449m, 129m, 0.25m, 0.05m);
            d.MarzaStara = 1.3429m;
            d.AkcijePoredjenje.AddRange(new[] { 0.05m, 0.13m });

            dodaj("Devizni marza 135,90%", 1.3590m, R(d.Marza, 4));
            dodaj("Devizni posle rabata 97", 97m, d.CenaPosleRabata);
            dodaj("Devizni Jela marza 77%", 0.77m, R(d.JelaMarza, 2));
            dodaj("Devizni akcija 5%: 92", 92m, d.CenaAkcija);
            dodaj("Devizni akcija 5%: Jela 68%", 0.68m, R(d.JelaMarzaAkcija, 2));
            dodaj("Devizni poredjenje 5%: 92", 92m, d.CenaAkcijaPoredjenje(0));
            dodaj("Devizni poredjenje 13%: 84", 84m, d.CenaAkcijaPoredjenje(1));
            dodaj("Devizni VP sa PDV ne postoji", null, d.VPsaPDV);

            /* ---------- Devizni: TP-Stari 6.487, marza stara 134,29% ---------- */
            dodaj("Devizni promena troska -0,59%", -0.0059m, R(d.PromenaTroska, 4));
            dodaj("Devizni VP prati trosak 128", 128m, d.PredlogVPPratiTrosak);
            dodaj("Devizni VP za staru marzu 128", 128m, d.PredlogVPStaraMarza);

            /* ---------- Dinarski: TP 6.449, VP 16.412, rabat 34%, akcija 0% ---------- */
            var n = Red(ObracunCene.RSD, 6449m, 6449m, 16412m, 0.34m, 0m);
            dodaj("Dinarski marza 154,49%", 1.5449m, R(n.Marza, 4));
            dodaj("Dinarski posle rabata 10.832", 10832m, n.CenaPosleRabata);
            dodaj("Dinarski Jela marza 68%", 0.68m, R(n.JelaMarza, 2));
            dodaj("Dinarski salon marza 52%", 0.52m, R(n.SalonMarza, 2));
            dodaj("Dinarski VP u evrima 139", 139m, n.VPuEUR);
            dodaj("Dinarski VP sa PDV 19.694", 19694m, n.VPsaPDV);

            /* ---------- Pravila preracuna ---------- */

            // marza -> VP: prvo se zaokruzi VP, pa se marza racuna iz zaokruzene VP
            var m = Red(ObracunCene.EUR, 6449m, 6449m, 100m, 0.25m, null);
            var redovi = new List<StavkaObracuna> { m };
            dodaj("Marza 135,90% daje VP 129", 129m, ObracunCene.PostaviMarzu(redovi, m, 1.3590m));
            dodaj("Marza iz zaokruzene VP", 1.3590m, R(m.Marza, 4));

            // varijante istog Code: ista VP, svaka svoja marza; Code se istice
            var v1 = Red(ObracunCene.RSD, 6449m, 6449m, 16000m, 0.34m, null);
            var v2 = Red(ObracunCene.RSD, 7000m, 7000m, 16000m, 0.34m, null);
            v2.LargoProizvodID = 2;
            var varijante = new List<StavkaObracuna> { v1, v2 };
            ObracunCene.PostaviVP(varijante, "KRT90", 16412m);
            dodaj("Varijanta dobija istu VP", 16412m, v2.VP);
            dodaj("Varijanta ima svoju marzu", 1.3446m, R(v2.Marza, 4));
            dodaj("Code sa razlicitim marzama se istice", 1m,
                  ObracunCene.CodeSaRazlicitimMarzama(varijante).Contains("KRT90") ? 1m : 0m);

            // % povecanja VP: VP = VP aktuelna * (1 + %)
            var pv = Red(ObracunCene.RSD, 6449m, 6449m, 16412m, 0.34m, null);
            dodaj("Povecanje 5% od 16.412", 17233m, ObracunCene.PostaviPovecanje(new List<StavkaObracuna> { pv }, pv, 0.05m));

            // zeljena cena posle rabata -> rabat
            var zc = Red(ObracunCene.RSD, 6449m, 6449m, 16412m, 0m, null);
            zc.PostaviCenuPosleRabata(10832m);
            dodaj("Rabat iz cene 10.832", 0.34m, R(zc.Rabat, 2));

            // red bez troska: TP-Stari kad nema TP-Novi; bez oba nema marze
            var bt = Red(ObracunCene.RSD, 6449m, null, 16412m, 0.34m, null);
            dodaj("Bez TP-Novi koristi TP-Stari", 1.5449m, R(bt.Marza, 4));
            var bt2 = Red(ObracunCene.RSD, null, null, 16412m, 0.34m, null);
            dodaj("Bez troska nema marze", null, bt2.Marza);

            /* ---------- Rabat i akcija po nivou: vazi najnizi ---------- */
            var par = new ParametriCenovnika();
            par.Dodaj(new ParametarCenovnika { Nivo = NivoParametra.Cenovnik, Rabat = 0.34m, Akcija = 0m });
            par.Dodaj(new ParametarCenovnika { Nivo = NivoParametra.Program, ProgramID = 1, Rabat = 0.30m });
            var ak = new ParametarCenovnika { Nivo = NivoParametra.Kolekcija, KolekcijaID = 10, Akcija = 0.05m };
            ak.AkcijePoredjenje.AddRange(new[] { 0.05m, 0.13m });
            par.Dodaj(ak);
            par.Dodaj(new ParametarCenovnika { Nivo = NivoParametra.Code, Code = "DRUGI", Rabat = 0.20m });

            var r1 = Red(ObracunCene.RSD, 6449m, 6449m, 16412m, 0m, null);
            var r2 = Red(ObracunCene.RSD, 6449m, 6449m, 16412m, 0m, null);
            r2.ProgramID = 2; r2.KolekcijaID = 20;
            var r3 = Red(ObracunCene.RSD, 6449m, 6449m, 16412m, 0m, null);
            r3.Code = "DRUGI";
            par.Primeni(new[] { r1, r2, r3 });

            dodaj("Rabat programa pregazi cenovnik", 0.30m, r1.Rabat);
            dodaj("Akcija kolekcije pregazi cenovnik", 0.05m, r1.Akcija);
            dodaj("Akcije za poredjenje kolekcije", 2m, r1.AkcijePoredjenje.Count);
            dodaj("Drugi program dobija rabat cenovnika", 0.34m, r2.Rabat);
            dodaj("Drugi program nema akcija za poredjenje", 0m, r2.AkcijePoredjenje.Count);
            dodaj("Rabat Code pregazi program", 0.20m, r3.Rabat);

            return p;
        }

        public static List<Provera> Greske()
        {
            return Pokreni().Where(x => !x.OK).ToList();
        }
    }
}
