using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Gateways.External
{
    public interface IPagamentoGateway : IRepositoryGateway<Pagamento, Guid>
    {
        Task<Pagamento> GetByPedidoAsync(Guid pedidoId);
    }
}
