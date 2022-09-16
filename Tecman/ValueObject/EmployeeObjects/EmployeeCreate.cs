using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tecman.ValueObject.EmployeeObjects;

namespace Tecman.ValueObject
{
    public class EmployeeCreate
    {
        public string? name { get; set; }

        public string? phoneNumber { get; set; }

        public string? cpf { get; set; }

        public string? email { get; set; }
        public string? avatar_url { get; set; }

        public DateTime? birthDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public int role { get; set; }

        public virtual AddressObject address { get; set; }
        public virtual EmployeeUser employeeUser { get; set; }

        
    }
}
