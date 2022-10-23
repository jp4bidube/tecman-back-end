using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tecman.Models;

namespace Tecman.ValueObject
{
    public class OrderServiceCreate
    {
        public int clientId { get; set; }
        public int tecnicId { get; set; }
        public string? street { get; set; }
        public string? cep { get; set; }
        public string? number { get; set; }
        public string? district { get; set; }
        public string? complement { get; set; }
        public string? observacao { get; set; }
        public string? defect { get; set; }
        public string[] devices { get; set; }
        [JsonIgnore]
        public Employee userLogged { get; set; }

    }
}
