using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.PagamentoModel;
using Domain.Gateways.External;

namespace Application.UseCases.PagamentoUseCase
{
    public class PutPagamentoUseCaseAsync : IUseCaseAsync<PagamentoPutRequest>
    {
        private readonly IPagamentoGateway _pagamentoGateway;

        public PutPagamentoUseCaseAsync(IPagamentoGateway pagamentoGateway)
        {
            _pagamentoGateway = pagamentoGateway;
        }

        public async Task ExecuteAsync(PagamentoPutRequest request)
        {
            var pagamento = await _pagamentoGateway.GetByPedidoAsync(request.PedidoId);
            if (pagamento == null)
                throw new KeyNotFoundException("Pagamento não encontrado");
            if (pagamento.Status != Domain.Enums.StatusPagamento.Pendente)
                throw new KeyNotFoundException("Status já informado");

            pagamento.SetStatus(request.Status);
            await _pagamentoGateway.UpdateAsync(pagamento);
        }
    }
}