using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.ValueObject.EmployeeObjects
{
    public class EmployeeUserCreate
    {
        public bool login { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
