using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tecman.Models
{
    [Table("endereco")]
    public class Address
    {
        [Key]
        [Column("endereco_id")]
        public int id { get; set; }
        [Column("logradouro")]
        public string publicPlace { get; set; }
        [Column("cep")]
        public string? cep { get; set; }
        [Column("numero")]
        public string number { get; set; }
        [Column("bairro")]
        public string district { get; set; }
        [Column("complemento")]
        public string? complement { get; set; }
    }
}