using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.ValueObject.OrderServiceObjects
{
    public class OrderServiceComplete
    {
        public string serviceExecuted { get; set; }
        public bool pieceSold { get; set; }
        public bool clientPiece { get; set; }
        public decimal budget { get; set; }
        public decimal amountReceived { get; set; }
        public DateTime datePayment { get; set; }
    }
}
