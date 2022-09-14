using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tecman.Models
{
    [Table("cargo")]
    public class Role
    {
        [Key]
        [Column("cargo_id")]
        public int id { get; set; }
        [Column("cargo")]
        public string role { get; set; }
    }
}