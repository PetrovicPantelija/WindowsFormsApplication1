using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Pakovanja.PlanPakovanja
{
    [Table("PlanoviPakovanja")]
    public class PackingPlanModel
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("Naziv")]
        public string Naziv { get; set; }
        [Column("DatumOd")]
        public DateTime DatumOd { get; set; }
        [Column("DatumDo")]
        public DateTime DatumDo { get; set; }
        [Column("Napomena")]
        public string Napomena { get; set; }
        [Column("Konacan")]
        public string Konacan { get; set; }
    }
}
