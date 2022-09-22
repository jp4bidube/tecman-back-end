using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.ValueObject.UserObject
{
    public class NewPassword
    {
        public int userId { get; set; }
        public int employeeId { get; set; }
        public string recoveryToken { get; set; }

        public string username { get; set; }
    }
}
