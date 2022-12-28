using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Pakovanja.PackingPlanItem
{
    [Table("StavkePlanaPakovanja")]
    internal class PackingPlanItemModel
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("IDPlanPakovanja")]
        public int IdPlanaPakovanja { get; set; }
        [Column("RNBroj")]
        public int RnBroj { get; set; }
        [Column("StatusRN")]
        public string RnStatus { get; set; }
        [Column("VrstaRN")]
        public int RnVrsta { get; set; }
        [Column("Artikal")]
        public int Artikal { get; set; }
        [Column("Kod")]
        public string Kod { get; set; }
        [Column("Naziv")]
        public string Naziv { get; set; }
        [Column("KolicinaZaProizvodnju")]
        public decimal KolicinaZaProizvodnju { get; set; }
        [Column("PlaniraniKrajniDatumZavrsetka")]
        public DateTime KrajnjiDatum { get; set; }
        [Column("Termin")]
        public int Termin { get; set; }
    }
}
