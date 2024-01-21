using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Application.Models.ClienteModel;
using Domain.Gateways;

namespace Application.UseCases.ClienteUseCase
{
    public class GetAllClienteUseCaseAsync : IUseCaseIEnumerableAsync<IEnumerable<ClienteResponse>>
    {
        private readonly IClienteGateway _clienteGateway;
        private readonly IMapper _mapper;

        public GetAllClienteUseCaseAsync(IClienteGateway clienteGateway, IMapper mapper)
        {
            _clienteGateway = clienteGateway;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteResponse>> ExecuteAsync()
        {
            var result = await _clienteGateway.GetAllAsync();

            return _mapper.Map<IEnumerable<ClienteResponse>>(result);
        }
    }
}
