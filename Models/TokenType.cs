using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
*    Token types:
*    1 -> "Verificação"
*    2 -> "Esqueceu senha"
*    3 -> "Esqueceu email"
*    4 -> "Refresh"
*    5 -> "JWT"
*/

namespace Tecman.Models
{
    [Table("token_tipo")]
    public class TokenType
    {
        [Key]
        [Column("token_tipo_id")]
        public int id { get; set; }

        [Column("tipo")]
        public string tokenType { get; set; }
    }
}
