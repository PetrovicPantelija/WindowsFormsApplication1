using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApplication1.Kalkulacije.IzracunCena
{
    public enum NivoParametra { Cenovnik, Program, Kolekcija, Code }

    // Jedan red IC_CenovnikParametar: rabat, vazeca akcija i akcije za poredjenje za jedan nivo.
    // NULL rabat/akcija = nasledjuje visi nivo; prazna lista akcija = nasledjuje.
    public class ParametarCenovnika
    {
        public NivoParametra Nivo { get; set; }
        public int? ProgramID { get; set; }
        public int? KolekcijaID { get; set; }
        public string Code { get; set; }
        public decimal? Rabat { get; set; }
        public decimal? Akcija { get; set; }
        public List<decimal> AkcijePoredjenje { get; } = new List<decimal>();

        // forma oznacava sta je menjano (za IC_SacuvajParametre)
        public bool PromenjenRabat { get; set; }
        public bool PromenjenaAkcija { get; set; }

        public bool Vazi(StavkaObracuna red)
        {
            switch (Nivo)
            {
                case NivoParametra.Program: return ProgramID == red.ProgramID;
                case NivoParametra.Kolekcija: return KolekcijaID == red.KolekcijaID;
                case NivoParametra.Code: return Code == red.Code;
                default: return true;
            }
        }
    }

    // Svi parametri jednog cenovnika. Za red vazi najnizi nivo koji ima vrednost
    // (Code, pa Kolekcija, pa Program, pa Cenovnik) - isto kao vw_IC_CenovnikObracun.
    public class ParametriCenovnika
    {
        readonly List<ParametarCenovnika> lista = new List<ParametarCenovnika>();

        public IEnumerable<ParametarCenovnika> Svi
        {
            get { return lista; }
        }

        public void Dodaj(ParametarCenovnika p)
        {
            lista.Add(p);
        }

        // nadje ili napravi parametar za nivo
        public ParametarCenovnika Nadji(NivoParametra nivo, int? programID, int? kolekcijaID, string code, bool napravi)
        {
            var p = lista.FirstOrDefault(x => x.Nivo == nivo && x.ProgramID == programID
                                           && x.KolekcijaID == kolekcijaID && x.Code == code);
            if (p == null && napravi)
            {
                p = new ParametarCenovnika { Nivo = nivo, ProgramID = programID, KolekcijaID = kolekcijaID, Code = code };
                lista.Add(p);
            }
            return p;
        }

        IEnumerable<ParametarCenovnika> ZaRed(StavkaObracuna red)
        {
            // Code=3 je najnizi nivo, pa ide prvi
            return lista.Where(p => p.Vazi(red)).OrderByDescending(p => (int)p.Nivo);
        }

        public decimal Rabat(StavkaObracuna red)
        {
            return ZaRed(red).Where(p => p.Rabat.HasValue).Select(p => p.Rabat.Value).DefaultIfEmpty(0).First();
        }

        public decimal? Akcija(StavkaObracuna red)
        {
            return ZaRed(red).Where(p => p.Akcija.HasValue).Select(p => p.Akcija).FirstOrDefault();
        }

        public List<decimal> AkcijePoredjenje(StavkaObracuna red)
        {
            var p = ZaRed(red).FirstOrDefault(x => x.AkcijePoredjenje.Count > 0);
            return p == null ? new List<decimal>() : p.AkcijePoredjenje;
        }

        // upisuje razresene vrednosti u redove (posle ucitavanja ili izmene parametra)
        public void Primeni(IEnumerable<StavkaObracuna> redovi)
        {
            foreach (var r in redovi)
            {
                r.Rabat = Rabat(r);
                r.Akcija = Akcija(r);
                r.AkcijePoredjenje.Clear();
                r.AkcijePoredjenje.AddRange(AkcijePoredjenje(r));
            }
        }
    }
}
