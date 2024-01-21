using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Application.Models.PedidoModel;
using Domain.Gateways;
using Domain.Services;

namespace Application.UseCases.PedidoUseCase
{
    public class GetHistoricoClienteUseCaseAsync : IUseCaseIEnumerableAsync<IEnumerable<HistoricoClienteResponse>>
    {
        private readonly IPedidoGateway _gateway;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public GetHistoricoClienteUseCaseAsync(
            IPedidoGateway gateway,
            IIdentityService identityService,
            IMapper mapper)
        {
            _gateway = gateway;
            _mapper = mapper;
            _identityService = identityService;
        }

        public async Task<IEnumerable<HistoricoClienteResponse>> ExecuteAsync()
        {
            var userId = _identityService.GetUserId();
            var result = await _gateway.GetHistoricoAsync(userId);

            return _mapper.Map<IEnumerable<HistoricoClienteResponse>>(result);
        }
    }
}