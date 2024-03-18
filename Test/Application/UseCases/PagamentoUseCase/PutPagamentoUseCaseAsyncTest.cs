using Application.Models.PagamentoModel;
using Application.UseCases.PagamentoUseCase;
using Domain.Entities;
using Domain.Enums;
using Domain.Gateways.External;
using Domain.Bus;
using Moq;

namespace Test.Application.UseCases.PagamentoUseCase
{
    public class PutPagamentoUseCaseAsyncTests
    {
        private readonly Mock<IPagamentoGateway> _pagamentoGatewayMock;
        private readonly Mock<IPagamentoBus> _pagamentoBusMock;
        private readonly PutPagamentoUseCaseAsync _useCase;

        public PutPagamentoUseCaseAsyncTests()
        {
            _pagamentoGatewayMock = new Mock<IPagamentoGateway>();
            _pagamentoBusMock = new Mock<IPagamentoBus>();
            _useCase = new PutPagamentoUseCaseAsync(_pagamentoGatewayMock.Object, _pagamentoBusMock.Object);
        }

    [Fact]
        public async Task ExecuteAsync_ShouldThrowException_WhenStatusIsPendente()
        {
            var request = new PagamentoPutRequest { Status = (short)StatusPagamento.Pendente };

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _useCase.ExecuteAsync(request));
            Assert.Equal("Status inválido", exception.Message);
        }

        [Fact]
        public async Task ExecuteAsync_WhenPagamentoNotFound_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var request = new PagamentoPutRequest { PedidoId = "id-pedido-01", Status = (short)StatusPagamento.Aprovado };
            _pagamentoGatewayMock.Setup(x => x.GetByPedidoAsync(request.PedidoId)).ReturnsAsync((Pagamento?)null);

            // Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _useCase.ExecuteAsync(request));

            // Assert
            Assert.Equal("Pagamento não encontrado", exception.Message);

            // Verify
            _pagamentoGatewayMock.Verify(x => x.GetByPedidoAsync(request.PedidoId), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_WhenStatusAlreadyInformed_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var request = new PagamentoPutRequest { PedidoId = "id-pedido-01", Status = (short)StatusPagamento.Aprovado };
            var pagamento = new Pagamento(TipoPagamento.Cartao, "id-pedido-01", StatusPagamento.Aprovado, Guid.NewGuid());
            _pagamentoGatewayMock.Setup(x => x.GetByPedidoAsync(request.PedidoId)).ReturnsAsync(pagamento);

            // Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _useCase.ExecuteAsync(request));

            // Assert
            Assert.Equal("Status já informado", exception.Message);

            // Verify
            _pagamentoGatewayMock.Verify(x => x.GetByPedidoAsync(request.PedidoId), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_WhenStatusIsReprovado_ShouldCallSendPaymentErrorAsync()
        {
            // Arrange
            var request = new PagamentoPutRequest { PedidoId = "id-pedido-01", Status = (short)StatusPagamento.Reprovado };
            var pagamento = new Pagamento(TipoPagamento.Cartao, "id-pedido-01", StatusPagamento.Pendente, Guid.NewGuid());
            _pagamentoGatewayMock.Setup(x => x.GetByPedidoAsync(request.PedidoId)).ReturnsAsync(pagamento);
            _pagamentoGatewayMock.Setup(x => x.UpdateAsync(It.IsAny<Pagamento>())).Callback<Pagamento>(p => p.SetStatus(request.Status));

            // Act
            await _useCase.ExecuteAsync(request);

            // Assert
            _pagamentoBusMock.Verify(x => x.SendPaymentErrorAsync(It.Is<Domain.Models.PagamentoStatusModel>(m => m.PedidoId == request.PedidoId)), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_WhenStatusIsAprovado_ShouldCallSendPaymentSuccessAsync()
        {
            // Arrange
            var request = new PagamentoPutRequest { PedidoId = "id-pedido-01", Status = (short)StatusPagamento.Aprovado };
            var pagamento = new Pagamento(TipoPagamento.Cartao, "id-pedido-01", StatusPagamento.Pendente, Guid.NewGuid());
            _pagamentoGatewayMock.Setup(x => x.GetByPedidoAsync(request.PedidoId)).ReturnsAsync(pagamento);
            _pagamentoGatewayMock.Setup(x => x.UpdateAsync(It.IsAny<Pagamento>())).Callback<Pagamento>(p => p.SetStatus(request.Status));

            // Act
            await _useCase.ExecuteAsync(request);

            // Assert
            _pagamentoBusMock.Verify(x => x.SendPaymentSuccessAsync(It.Is<Domain.Models.PagamentoStatusModel>(m => m.PedidoId == request.PedidoId)), Times.Once);
        }

    }
}