using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
*    User profiles:
*    1 -> "Master"
*    2 -> "Filho"
*    3 -> "Comercial"
*/

namespace Tecman.Models
{
    [Table("usuario_perfil")]
    public class UserProfile
    {
        [Key]
        [Column("usuario_perfil_id")]
        public int id { get; set; }
        [Column("perfil")]
        public string userProfile { get; set; }
    }
}