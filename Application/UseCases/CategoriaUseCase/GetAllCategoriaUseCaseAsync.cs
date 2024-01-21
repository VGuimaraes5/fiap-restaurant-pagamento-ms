using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Application.Models.CategoriaModel;
using Domain.Gateways;

namespace Application.UseCases.CategoriaUseCase
{
    public class GetAllCategoriaUseCaseAsync : IUseCaseIEnumerableAsync<IEnumerable<CategoriaResponse>>
    {
        private readonly ICategoriaGateway _categoriaGateway;
        private readonly IMapper _mapper;

        public GetAllCategoriaUseCaseAsync(ICategoriaGateway clienteGateway, IMapper mapper)
        {
            _categoriaGateway = clienteGateway;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoriaResponse>> ExecuteAsync()
        {
            var result = await _categoriaGateway.GetAllAsync();

            return _mapper.Map<IEnumerable<CategoriaResponse>>(result);
        }
    }
}
