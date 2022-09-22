using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;

namespace Tecman.ValueObject.EmployeeObjects
{
    public class EmployeeUnique
    {
        public virtual Employee employee { get; set; }

        public virtual User user { get; set; }
    }
}
