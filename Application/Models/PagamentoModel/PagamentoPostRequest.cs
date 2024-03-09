using Domain.Enums;

namespace Application.Models.PagamentoModel
{
    public class PagamentoPostRequest
    {
        public TipoPagamento Tipo { get; set; }
        public string PedidoId { get; set; }
    }
}