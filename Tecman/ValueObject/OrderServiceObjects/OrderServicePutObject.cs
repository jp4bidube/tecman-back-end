using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.ValueObject.OrderServiceObjects
{
    public class OrderServicePutObject
    {
        public int id { get; set; }
        public int tecnicId { get; set; }
        public string serviceExecuted { get; set; }
        public bool pieceSold { get; set; }
        public bool clientPiece { get; set; }
        public decimal budget { get; set; }
        public decimal amountReceived { get; set; }
        public DateTime? datePayment { get; set; }
        public int clientId { get; set; }
        public string? street { get; set; }
        public string? cep { get; set; }
        public string? number { get; set; }
        public string? district { get; set; }
        public string? complement { get; set; }
        public string? observacao { get; set; }
        public string? defect { get; set; }
        public EquipmentOSPutObject? device { get; set; }


    }
}
