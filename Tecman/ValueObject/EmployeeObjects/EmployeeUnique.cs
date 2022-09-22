using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;

namespace Tecman.ValueObject.EmployeeObjects
{
    public class EmployeeUnique
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? phoneNumber { get; set; }
        public string? cpf { get; set; }
        public string? email { get; set; }
        public DateTime? birthDate { get; set; }
        public DateTime? registrationDate { get; set; }
        public DateTime? deactivationDate { get; set; }
        public virtual Role role { get; set; }
        public virtual Address? address { get; set; }
        public virtual EmployeeStatus employeeStatus { get; set; }
        public string? avatarUrl { get; set; }
        public virtual User user { get; set; }
    }
}
