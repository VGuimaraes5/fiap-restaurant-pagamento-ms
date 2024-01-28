using API.Controllers;
using Application.Models.PagamentoModel;
using Application.UseCases;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;


namespace Test.API.Controllers
{
    public class PagamentoControllerTests
    {
        [Fact]
        public async Task Status_ShouldReturnOkResult_WhenCalledWithValidIdPedido()
        {
            // Arrange
            var mockGetUseCase = new Mock<IUseCaseAsync<PagamentoGetRequest, Tuple<string, Guid>>>();
            var mockPostUseCase = new Mock<IUseCaseAsync<PagamentoPostRequest>>();
            mockGetUseCase.Setup(x => x.ExecuteAsync(It.IsAny<PagamentoGetRequest>())).ReturnsAsync(new Tuple<string, Guid>("status", Guid.NewGuid()));
            var controller = new PagamentoController(mockGetUseCase.Object, mockPostUseCase.Object);

            // Act
            var result = await controller.Status("idPedido");

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Criar_ShouldReturnOkResult_WhenCalledWithValidRequest()
        {
            // Arrange
            var mockGetUseCase = new Mock<IUseCaseAsync<PagamentoGetRequest, Tuple<string, Guid>>>();
            var mockPostUseCase = new Mock<IUseCaseAsync<PagamentoPostRequest>>();
            mockPostUseCase.Setup(x => x.ExecuteAsync(It.IsAny<PagamentoPostRequest>())).Returns(Task.CompletedTask);
            var controller = new PagamentoController(mockGetUseCase.Object, mockPostUseCase.Object);

            var request = new PagamentoPostRequest { Tipo = TipoPagamento.Pix, PedidoId = "pedido01" };

            // Act
            var result = await controller.Criar(request);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}