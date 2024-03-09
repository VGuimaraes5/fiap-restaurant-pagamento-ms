using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.PagamentoModel;
using Domain.Bus;
using Domain.Enums;
using Domain.Gateways.External;

namespace Application.UseCases.PagamentoUseCase
{
    public class PutPagamentoUseCaseAsync : IUseCaseAsync<PagamentoPutRequest>
    {
        private readonly IPagamentoGateway _pagamentoGateway;
        private readonly IPagamentoBus _pagamentoBus;

        public PutPagamentoUseCaseAsync(IPagamentoGateway pagamentoGateway, IPagamentoBus pagamentoBus)
        {
            _pagamentoGateway = pagamentoGateway;
            _pagamentoBus = pagamentoBus;
        }

        public async Task ExecuteAsync(PagamentoPutRequest request)
        {
            if (request.Status == (short)StatusPagamento.Pendente)
                throw new KeyNotFoundException("Status inválido");
            var pagamento = await _pagamentoGateway.GetByPedidoAsync(request.PedidoId);
            if (pagamento == null)
                throw new KeyNotFoundException("Pagamento não encontrado");
            if (pagamento.Status != StatusPagamento.Pendente)
                throw new KeyNotFoundException("Status já informado");

            pagamento.SetStatus(request.Status);
            await _pagamentoGateway.UpdateAsync(pagamento);

            if (pagamento.Status == StatusPagamento.Reprovado)
                await _pagamentoBus.SendPaymentErrorAsync(new Domain.Models.PagamentoStatusModel { PedidoId = pagamento.PedidoId });
            else if (pagamento.Status == StatusPagamento.Aprovado)
                await _pagamentoBus.SendPaymentSuccessAsync(new Domain.Models.PagamentoStatusModel { PedidoId = pagamento.PedidoId });
        }
    }
}