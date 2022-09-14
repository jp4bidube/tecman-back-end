using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.ValueObject.UserObject
{
    public class UserCreate
    {
        public string username { get; set; }
        public string password { get; set; }
        public int userProfileId { get; set; }
        public int employeeId { get; set; }

    }
}
