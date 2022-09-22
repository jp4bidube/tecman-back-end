using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.ValueObject.ClientObjects
{
    public class ClientCreate
    {
        public string name { get; set; }
        public string cpf { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public virtual AddressObject address { get; set; }

    }
}
