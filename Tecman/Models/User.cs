using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tecman.Models
{
    [Table("usuario")]
    public class User
    {
        [Key]
        [Column("usuario_id")]
        public int id { get; set; }
      
       
        [Column("nome_de_usuario")]
        public string? username { get; set; }
        
        [Column("senha")]
        public string? password { get; set; }
     
        [Column("data_de_ativacao")]
        public DateTime? registrationDate { get; set; }
     
        [Column("data_de_desativacao")]
        public DateTime? deactivationDate { get; set; }
    
        [Column("usuario_status_id")]
        [ForeignKey("usuario_status_id")]
        public virtual UserStatus userStatus { get; set; }

        [Column("usuario_perfil_id")]
        [ForeignKey("usuario_perfil_id")]
        public virtual UserProfile userProfile { get; set; }

        [Column("funcionario_id")]
        [ForeignKey("funcionario_id")]

        public virtual Employee employee { get; set; }

    }
}
