using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.ValueObject.ClientObjects
{
    public class AddressJoinClientAddress
    {
        public int id { get; set; }
        public int clientAddressId { get; set; }
        public string? street { get; set; }
        public string? cep { get; set; }
        public string? number { get; set; }
        public string? district { get; set; }
        public string? complement { get; set; }
        public int clientId { get; set; }
        public bool? defaultAddress { get; set; }
    }
}
