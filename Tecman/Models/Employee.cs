using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.Models
{
    [Table("funcionario")]
    public class Employee
    {
        [Key]
        [Column("funcionario_id")]
        public int id { get; set; }

        [Column("nome")]
        public string? name { get; set; }

        [Column("telefone")]
        public string? phoneNumber { get; set; }

        [Column("cpf")]
        public string? cpf { get; set; }

        [Column("email")]
        public string? email { get; set; }

        [Column("data_de_nascimento")]
        public DateTime? birthDate { get; set; }

        [Column("data_de_entrada")]
        public DateTime? registrationDate { get; set; }

        [Column("data_de_saida")]
        public DateTime? deactivationDate { get; set; }

        [Column("cargo_id")]
        [ForeignKey("cargo_id")]

        public virtual Role role { get; set; }

        [Column("endereco_id")]
        [ForeignKey("endereco_id")]

        public virtual Address address { get; set; }


    }
}

