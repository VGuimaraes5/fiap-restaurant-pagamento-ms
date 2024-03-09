using System;
using Domain.Enums;

namespace Domain.Models
{
    public class PedidoModel
    {
        public string PedidoId { get; set; }
        public TipoPagamento TipoPagamento { get; set; }
    }
}