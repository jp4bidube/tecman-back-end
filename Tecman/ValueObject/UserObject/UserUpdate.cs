using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.ValueObject.UserObject
{
    public class UserUpdate
    {
        public string username { get; set; }
        public string password { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public int employeeId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public int role { get; set; }

    }
}
