using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;

namespace Tecman.ValueObject.ClientObjects
{
    public class ClientUnique
    {
        public string name { get; set; }
        public string cpf { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public List<ClientAddress> address { get; set; }
    }
}
