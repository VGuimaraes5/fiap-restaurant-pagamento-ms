using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Gateways
{
    public interface ICognitoGateway
    {
        Task<string> CreateUser(Cliente cliente);
        Task DeleteUser(string userId);
    }
}