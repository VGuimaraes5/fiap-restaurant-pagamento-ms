using Newtonsoft.Json;

namespace Application.Models.PagamentoModel
{
    public class PagamentoPutRequest
    {
        [JsonIgnore]
        public string PedidoId { get; set; }

        [JsonProperty("Status")]
        public short Status { get; set; }
    }
}