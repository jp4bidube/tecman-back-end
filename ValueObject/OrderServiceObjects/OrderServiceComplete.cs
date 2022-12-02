using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tecman.ValueObject.OrderServiceObjects
{
    public class OrderServiceComplete
    {
        [JsonIgnore]
        public int id { get; set; }
        public int tecnicId { get; set; }
        public string serviceExecuted { get; set; }
        public bool pieceSold { get; set; }
        public bool clientPiece { get; set; }
        public decimal budget { get; set; }
        public decimal amountReceived { get; set; }
        public DateTime? datePayment { get; set; }
        public virtual List<EquipmentUnique> equipments { get; set; }
    }
}
