using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tecman.Models
{
    [Table("ordem_de_servico")]
    public class OrderService
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("cliente_id")]
        [ForeignKey("cliente_id")]
        public virtual Client? client { get; set; }

        [Column("criado_por")]
        [ForeignKey("criado_por")]
        public virtual Employee? createdBy { get; set; }

        [Column("tecnico_id")]
        [ForeignKey("tecnico_id")]
        public virtual Employee? tecnic { get; set; }

        [Column("os_status")]
        [ForeignKey("os_status")]
        public virtual OrderServiceStatus? orderServiceStatus { get; set; }

        [Column("data_de_criacao")]
        public DateTime? dateCreated { get; set; }

        [Column("logradouro")]
        public string? street { get; set; }
        [Column("cep")]
        public string? cep { get; set; }
        [Column("numero")]
        public string? number { get; set; }
        [Column("bairro")]
        public string? district { get; set; }
        [Column("complemento")]
        public string? complement { get; set; }

        [Column("observacao")]
        public string? observacao { get; set; }

        [Column("aparelhos_qtd")]
        public string?  device_qtd{ get; set; }

        [Column("peca_vendida")]
        public bool? pieceSold { get; set; }

        [Column("defeito")]
        public string? defect { get; set; }

        [Column("cliente_peca")]
        public bool? clientPiece { get; set; }

        [Column("orcamento")]
        public decimal? budget { get; set; }

        [Column("valor_recebido")]
        public decimal? amountReceived { get; set; }

        [Column("data_pagamento")]
        public DateTime? datePayment { get; set; }

        [Column("ausencia_1")]
        public DateTime? absence1 { get; set; }

        [Column("ausencia_2")]
        public DateTime? absence2 { get; set; }

        [Column("servico_executado")]
        public string? serviceExecuted { get; set; }

        [Column("periodo_atendimento")]
        public string? periodAttendance { get; set; }

    }
}
