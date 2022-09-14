using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.ValueObject
{
    public class VerificationTokenObject
    {
        public bool success { get; set; }
        public string user_id { get; set; }
        public string verifyLink { get; set; }
    }
}
