using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tecman.Models
{
    [Table("usuario_token")]
    public class UserToken
    {
        [Key]
        [Column("usuario_token_id")]
        public int user_token_id { get; set; }

        [Column("usuario_id")]
        [ForeignKey("usuario_id")]
        public virtual User user { get; set; }

        [Column("token_tipo_id")]
        [ForeignKey("token_tipo_id")]
        public virtual TokenType tokenType { get; set; }

        [Column("token")]
        public string token { get; set; }

        [Column("data_expiracao")]
        public DateTime expirationDate { get; set; }

        [Column("data_de_uso")]
        public DateTime? dateOfUse { get; set; }
    }
}
