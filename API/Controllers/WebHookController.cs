using Microsoft.AspNetCore.Mvc;
using Application.Models.PagamentoModel;
using Application.UseCases;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebHookController : ControllerBase
    {
        private readonly IUseCaseAsync<PagamentoPutRequest> _putUseCase;

        public WebHookController(IUseCaseAsync<PagamentoPutRequest> putUseCase)
        {
            _putUseCase = putUseCase;
        }

        [HttpPut("AtualizaPagamento/{PedidoId}")]
        public async Task<IActionResult> AtualizaPagamento([FromRoute] string PedidoId, [FromBodyAttribute] PagamentoPutRequest request)
        {
            try
            {
                request.PedidoId = PedidoId;
                await _putUseCase.ExecuteAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}