using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public string? password { get; set; }

        [Column("data_de_ativacao")]
        [JsonIgnore]
        public DateTime? registrationDate { get; set; }
     
        [Column("data_de_desativacao")]
        [JsonIgnore]
        public DateTime? deactivationDate { get; set; }

        [Column("funcionario_id")]
        [ForeignKey("funcionario_id")]
        [JsonIgnore]

        public virtual Employee employee { get; set; }

    }
}
