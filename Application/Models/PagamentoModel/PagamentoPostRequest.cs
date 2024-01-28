using Domain.Enums;
using Newtonsoft.Json;

namespace Application.Models.PagamentoModel
{
    public class PagamentoPostRequest
    {
        [JsonProperty("tipo")]
        public TipoPagamento Tipo { get; set; }

        [JsonProperty("pedidoId")]
        public string PedidoId { get; set; }
    }
}