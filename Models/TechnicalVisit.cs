using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.Models
{
    [Table("visita_garantia")]  
    public class TechnicalVisit
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("cliente_peca")]
        public bool? clientePiece { get; set; }

        [Column("data_visita")]
        public DateTime? dateVisit { get; set; }

        [Column("servico_executado")]
        public string? serviceExecuted { get; set; }

        [Column("equipamento_id")]
        [ForeignKey("equipamento_id")]
        public virtual Equipment Equipment { get; set; }

        [Column("tecnico_id")]
        [ForeignKey("tecnico_id")]
        public virtual Employee Employee { get; set; }

    }
}
