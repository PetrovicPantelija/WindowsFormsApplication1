using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WindowsFormsApplication1.Pakovanja.PregledStavkiPlanaPakovanja
{
    [Table("PregledStavkiPlanaPakovanja")]
    internal class PackingPlanItemOverviewModel
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("IDStavkePlanaPakovanja")]
        public int IdStavkePlanaPakovanja { get; set; }
        [Column("RN")]
        public int RN { get; set; }
        [Column("StatusStavke")]
        public string StatusStavke { get; set; }
        [Column("ArtikalPaket")]
        public int ArtikalPaket { get; set; }
        [Column("KolicinaZaProizvodnju")]
        public decimal KolicinaZaProizvodnju { get; set; }
        [Column("ArtikalIzPrvogNivoaSastavnice")]
        public int ArtikalIzPrvogNivoaSastavnice { get; set; }
        [Column("PotrebnaKolicina")]
        public decimal PotrebnaKolicina { get; set; }
        [Column("TrenutnaKolicinaNaZalihama")]
        public decimal TrenutnaKolicinaNaZalihama { get; set; }
        [Column("RezervisanaKolicinaPoPlanu")]
        public decimal RezervisanaKolicinaPoPlanu { get; set; }
        [Column("UkucatiKolicinuZaRezervaciju")]
        public decimal UkucatiKolicinuZaRezervaciju { get; set; }

    }
}
