using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.Models
{
    [Table("funcionario_status")]
    public class EmployeeStatus
    {
        [Key]
        [Column("funcionario_status_id")]
        public int id { get; set; }

        [Column("status")]
        public string? status { get; set; }

    }
}

