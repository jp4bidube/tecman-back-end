using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tecman.Models;

namespace Tecman.ValueObject.OrderServiceObjects
{
    public class EquipmentUnique
    {
        public int? id { get; set; }
        public string? type { get; set; }
        public string? brand { get; set; }
        public string? model { get; set; }
        public string? mounthsWarranty { get; set; }
        public DateTime? warrantyPeriod { get; set; }
        [JsonIgnore]    
        public virtual List<TechnicalVisit> visits { get; set; }
    }
}
