using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.ValueObject.EmployeeObjects
{
    public class TecnicOsDetails
    {
        public int? orderServicesTotal { get; set; }
        public int? orderServicesDone { get; set; }
        public int? orderServicesBudget { get; set; }
        public int? orderServicesCanceled { get; set; }
    }
}
