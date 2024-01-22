using System.Threading.Tasks;
using Application.Models.PagamentoModel;
using Domain.Gateways.External;

namespace Application.UseCases.PagamentoUseCase
{
    public class PostPagamentoUseCaseAsync : IUseCaseAsync<PagamentoPostRequest>
    {
        private readonly IPagamentoGateway _pagamentoGateway;

        public PostPagamentoUseCaseAsync(IPagamentoGateway pagamentoGateway)
        {
            _pagamentoGateway = pagamentoGateway;
        }

        public async Task ExecuteAsync(PagamentoPostRequest request)
        {
            var pagamento = new Domain.Entities.Pagamento(request.Tipo, request.PedidoId);
            await _pagamentoGateway.InsertAsync(pagamento);
        }
    }
}