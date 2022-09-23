using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.ValueObject
{
    public class AddressObject
    {   
        [Required(AllowEmptyStrings = false)]
        public string street { get; set; }
        public string? cep { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string number { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string district { get; set; }
        public string? complement { get; set; }
        public bool? defaultAddress { get; set; }
    }
}
