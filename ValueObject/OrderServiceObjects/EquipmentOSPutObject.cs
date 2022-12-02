using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.ValueObject.OrderServiceObjects
{
    public class EquipmentOSPutObject
    {
        public int id { get; set; }
        public string type { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public int? mounthsWarranty { get; set; }
        public DateTime? warrantyPeriod { get; set; }

    }
}
