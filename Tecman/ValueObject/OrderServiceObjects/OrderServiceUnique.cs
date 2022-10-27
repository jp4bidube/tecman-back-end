using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.ValueObject.EmployeeObjects;

namespace Tecman.ValueObject.OrderServiceObjects
{
    public class OrderServiceUnique
    {
        public int id { get; set; }

        public virtual Client? client { get; set; }

        public string createdBy { get; set; }

        public virtual TecnicListSelect? Tecnic { get; set; }
        public virtual List<EquipmentUnique>? equipments { get; set; }

        public virtual OrderServiceStatus? orderServiceStatus { get; set; }

        public DateTime? dateCreated { get; set; }

        public string? street { get; set; }
        public string? cep { get; set; }
        public string? number { get; set; }
        public string? district { get; set; }
        public string? complement { get; set; }

        public string? observacao { get; set; }

        public string? device_qtd { get; set; }

        public bool? pieceSold { get; set; }

        public string? defect { get; set; }

        public bool? clientPiece { get; set; }

        public decimal? budget { get; set; }

        public decimal? amountReceived { get; set; }

        public DateTime? datePayment { get; set; }

        public DateTime? absence1 { get; set; }

        public DateTime? absence2 { get; set; }

        public string? serviceExecuted { get; set; }

    }
}
