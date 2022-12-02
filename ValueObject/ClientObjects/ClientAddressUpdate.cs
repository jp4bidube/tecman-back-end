using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.ValueObject.ClientObjects
{
    public class ClientAddressUpdate
    {
        public string street { get; set; }
        public string? cep { get; set; }
        public string number { get; set; }
        public string district { get; set; }
        public string? complement { get; set; }
    }

}
