using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Tecman.Models
{
    [Table("ordem_servico_status")]
    public class OrderServiceStatus
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("status")]
        public string status { get; set; }
    }
}
