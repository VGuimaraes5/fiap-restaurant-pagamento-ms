using Microsoft.AspNetCore.Mvc;
using Application.Models.PagamentoModel;
using Application.UseCases;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PagamentoController : ControllerBase
    {
        private readonly IUseCaseAsync<PagamentoGetRequest, Tuple<string, Guid>> _getUseCase;
        private readonly IUseCaseAsync<PagamentoPostRequest> _postUseCase;

        public PagamentoController(
            IUseCaseAsync<PagamentoGetRequest, Tuple<string, Guid>> getUseCase,
            IUseCaseAsync<PagamentoPostRequest> postUseCase)
        {
            _getUseCase = getUseCase;
            _postUseCase = postUseCase;
        }

        [HttpGet("StatusPagamento/{IdPedido}")]
        public async Task<IActionResult> Status([FromRoute] Guid IdPedido)
        {
            try
            {
                var result = await _getUseCase.ExecuteAsync(new PagamentoGetRequest { IdPedido = IdPedido });
                return Ok(new
                {
                    Status = result.Item1,
                    PagamentoId = result.Item2
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] PagamentoPostRequest request)
        {
            try
            {
                await _postUseCase.ExecuteAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}