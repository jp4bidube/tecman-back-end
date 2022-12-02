using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.Models
{
    [Table("cliente_endereco")]
    public class ClientAddress
    {
        [Key]
        [Column("cliente_endereco_id")]
        public int id { get; set; }

        [Column("cliente_endereco_cliente_id")]
        public int clientId { get; set; }

        [Column("cliente_endereco_endereco_id")]
        [ForeignKey("cliente_endereco_endereco_id")]

        public virtual Address address { get; set; }

        [Column("default")]
        public bool? defaultAddress { get; set; }

    }
}
