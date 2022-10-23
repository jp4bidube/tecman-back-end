using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.Models
{
    [Table("garantia")]
    public class Warranty
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("ordem_servico_id")]
        [ForeignKey("ordem_servico_id")]
        public virtual OrderService? OrderService { get; set; }

        [Column("prazo")]
        public DateTime? warrantyPeriod { get; set; }

        [Column("marca")]
        public string? mark { get; set; }

        [Column("modelo")]
        public string? model { get; set; }

        [Column("equipamento")]
        public string? equipment { get; set; }

        [Column("meses_garantia")]
        public string? mounthsWarranty { get; set; }


    }
}
