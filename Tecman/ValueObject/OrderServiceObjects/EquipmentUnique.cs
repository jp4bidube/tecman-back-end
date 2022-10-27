using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
