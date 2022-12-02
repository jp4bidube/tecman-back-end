using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.Models
{
    [Table("peca")]
    public class Piece
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("nome")]
        public string name { get; set; }
        [Column("codigo")]
        public string code { get; set; }
        [Column("quantidade")]
        public int quantity { get; set; }
        [Column("valor_unitario")]
        public decimal amountQuantity { get; set; }
        [Column("valor_total")]
        public decimal amountTotal { get; set; }
        [Column("obs")]
        public string obs { get; set; }
        [Column("troca")]
        public bool trade { get; set; }
        [Column("ordem_servico_id")]
        [ForeignKey("ordem_servico_id")]
        public virtual OrderService? OrderService { get; set; }

        [Column("equipamento_id")]
        [ForeignKey("equipamento_id")]
        public virtual Equipment? Equipment { get; set; }
    }
}
