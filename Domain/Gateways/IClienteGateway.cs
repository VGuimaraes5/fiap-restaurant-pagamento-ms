using Domain.Entities;
using Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Domain.Gateways
{
    public interface IClienteGateway : IRepositoryGateway<Cliente, Guid>
    {
        Task<Cliente> GetByCPFAsync(Cpf cpf);
        Task<Cliente> GetByUserIdAsync(string userId);
    }
}
