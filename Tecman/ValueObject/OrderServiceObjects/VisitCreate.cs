using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.ValueObject.OrderServiceObjects
{
    public class VisitCreate
    {
        public bool? clientePiece { get; set; }

        public DateTime? dateVisit { get; set; }

        public string? serviceExecuted { get; set; }

        public int equipmentId { get; set; }

        public int employeeId { get; set; }
    }
}
