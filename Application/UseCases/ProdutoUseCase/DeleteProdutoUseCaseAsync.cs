using System.Threading.Tasks;
using Domain.Gateways;
using Application.Models.ProdutoModel;
using System.Collections.Generic;

namespace Application.UseCases.ProdutoUseCase
{
    public class DeleteProdutoUseCaseAsync : IUseCaseAsync<ProdutoDeleteRequest>
    {
        private readonly IProdutoGateway _gateway;

        public DeleteProdutoUseCaseAsync(IProdutoGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task ExecuteAsync(ProdutoDeleteRequest request)
        {
            var existsItem = await _gateway.GetAsync(request.Id);
            if (existsItem == null)
                throw new KeyNotFoundException("Produto não encontrado");

            await _gateway.DeleteAsync(request.Id);
        }
    }
}
