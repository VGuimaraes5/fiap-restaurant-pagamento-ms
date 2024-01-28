using Application.Models.PagamentoModel;
using Application.UseCases.PagamentoUseCase;
using Domain.Entities;
using Domain.Enums;
using Domain.Gateways.External;
using Moq;

namespace Test.Application.UseCases.PagamentoUseCase
{
    public class GetPagamentoUseCaseAsyncTests
    {
        [Fact]
        public void ExecuteAsync_ReturnsExpectedResult()
        {
            // Arrange
            var mockPagamentoGateway = new Mock<IPagamentoGateway>();
            var pagamento = new Pagamento(TipoPagamento.Pix, "pedido01", StatusPagamento.Aprovado, Guid.NewGuid());
            mockPagamentoGateway.Setup(x => x.GetByPedidoAsync(It.IsAny<String>())).ReturnsAsync(pagamento);

            var useCase = new GetPagamentoUseCaseAsync(mockPagamentoGateway.Object);
            var request = new PagamentoGetRequest { IdPedido = "pedido01" };

            // Act
            var result = useCase.ExecuteAsync(request).Result;

            // Assert
            Assert.Equal(pagamento.Id, result.Item2);
            Assert.Equal("Aprovado", result.Item1);
            mockPagamentoGateway.Verify(x => x.GetByPedidoAsync(request.IdPedido), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ThrowsKeyNotFoundException_WhenPagamentoNotFound()
        {
            // Arrange
            var mockPagamentoGateway = new Mock<IPagamentoGateway>();
            mockPagamentoGateway.Setup(x => x.GetByPedidoAsync(It.IsAny<string>())).ReturnsAsync(value: null);

            var useCase = new GetPagamentoUseCaseAsync(mockPagamentoGateway.Object);
            var request = new PagamentoGetRequest { IdPedido = "pedido01" };

            // Act & Assert
            var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() => useCase.ExecuteAsync(request));
            Assert.Equal("Pagamento não encontrado", ex.Message);
        }

    }
}