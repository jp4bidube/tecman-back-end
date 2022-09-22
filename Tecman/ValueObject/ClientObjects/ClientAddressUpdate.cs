using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.ValueObject.ClientObjects
{
    public class ClientAddressUpdate
    {
        public bool defaultAddress { get; set; }
        public int id { get; set; }
        public virtual AddressObject address { get; set; }

    }
}
