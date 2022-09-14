using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
*    User status types:
*    1 -> "Incompleto"
*    2 -> "Verificado"
*    3 -> "Completo"
*    4 -> "Financeiro"
*    5 -> "Ativo"
*    6 -> "Inativo"
*    7 -> "Bloqueado"
*    8 -> "Excluido"
*/

namespace Tecman.Models
{
    [Table("usuario_status")]
    public class UserStatus
    {
        [Key]
        [Column("usuario_status_id")]
        public int id { get; set; }
        [Column("status")]
        public string userStatus { get; set; }
    }
}
