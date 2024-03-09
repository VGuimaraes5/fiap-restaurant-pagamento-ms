using Application.Models.PagamentoModel;
using Application.UseCases.PagamentoUseCase;
using Domain.Entities;
using Domain.Enums;
using Domain.Gateways.External;
using Moq;

namespace Test.Application.UseCases.PagamentoUseCase
{
    public class PutPagamentoUseCaseAsyncTests
    {
        [Fact]
        public async Task ExecuteAsync_ShouldUpdateStatus_WhenStatusIsPending()
        {
            // Arrange
            var mockPagamentoGateway = new Mock<IPagamentoGateway>();
            var pagamento = new Pagamento(TipoPagamento.Pix, "pedido01");
            mockPagamentoGateway.Setup(x => x.GetByPedidoAsync(It.IsAny<String>())).ReturnsAsync(pagamento);
            mockPagamentoGateway.Setup(x => x.UpdateAsync(It.IsAny<Pagamento>())).Returns(Task.CompletedTask);
            var useCase = new PutPagamentoUseCaseAsync(mockPagamentoGateway.Object);

            var request = new PagamentoPutRequest { PedidoId = "pedido01", Status = (short)StatusPagamento.Aprovado };

            // Act
            await useCase.ExecuteAsync(request);

            // Assert
            mockPagamentoGateway.Verify(x => x.UpdateAsync(It.Is<Pagamento>(p => p.Status == StatusPagamento.Aprovado)), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowKeyNotFoundException_WhenPaymentNotFound()
        {
            // Arrange
            var mockPagamentoGateway = new Mock<IPagamentoGateway>();
            mockPagamentoGateway.Setup(x => x.GetByPedidoAsync(It.IsAny<String>())).ReturnsAsync((Pagamento?)null);

            var useCase = new PutPagamentoUseCaseAsync(mockPagamentoGateway.Object);

            var request = new PagamentoPutRequest { PedidoId = "pedido01", Status = (short)StatusPagamento.Aprovado };

            // Act and Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => useCase.ExecuteAsync(request));
        }

    }
}