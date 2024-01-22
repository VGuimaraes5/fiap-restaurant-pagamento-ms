using System;

namespace Application.Models.PagamentoModel
{
    public class PagamentoPutRequest
    {
        public string PedidoId { get; set; }
        public short Status { get; set; }
    }
}