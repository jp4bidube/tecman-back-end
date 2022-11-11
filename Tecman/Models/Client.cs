using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.Models
{
    [Table("cliente")]
    public class Client
    {
        [Key]
        [Column("cliente_id")]
        public int id { get; set; }
        [Column("nome")]
        public string? name { get; set; }
        [Column("cpf")]
        public string? cpf { get; set; }
        [Column("telefone")]
        public string? phoneNumber { get; set; }
        [Column("email")]
        public string? email { get; set; }

        [Column("endereco")]
        public string? district { get; set; }

    }
}
