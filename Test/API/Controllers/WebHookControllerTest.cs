using API.Controllers;
using Application.Models.PagamentoModel;
using Application.UseCases;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Test.Api.Controllers
{
    public class WebHookControllerTests
    {
        [Fact]
        public async Task AtualizaPagamento_ShouldReturnOkResult_WhenCalledWithValidRequest()
        {
            // Arrange
            var mockPutUseCase = new Mock<IUseCaseAsync<PagamentoPutRequest>>();
            mockPutUseCase.Setup(x => x.ExecuteAsync(It.IsAny<PagamentoPutRequest>())).Returns(Task.CompletedTask);
            var controller = new WebHookController(mockPutUseCase.Object);

            var request = new PagamentoPutRequest { Status = (short)StatusPagamento.Aprovado };

            // Act
            var result = await controller.AtualizaPagamento("pedido01", request);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task AtualizaPagamento_ShouldReturnBadRequestResult_WhenExceptionIsThrown()
        {
            // Arrange
            var mockPutUseCase = new Mock<IUseCaseAsync<PagamentoPutRequest>>();
            mockPutUseCase.Setup(x => x.ExecuteAsync(It.IsAny<PagamentoPutRequest>())).Throws(new Exception("Error"));
            var controller = new WebHookController(mockPutUseCase.Object);

            var request = new PagamentoPutRequest { Status = (short)StatusPagamento.Aprovado };

            // Act
            var result = await controller.AtualizaPagamento("pedido01", request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}