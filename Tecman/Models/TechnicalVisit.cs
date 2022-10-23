using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.Models
{
    [Table("aparelho")]
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

        [Column("garantia_id")]
        [ForeignKey("garantia_id")]
        public virtual Warranty Warranty { get; set; }

    }
}
