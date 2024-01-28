using Application.Models.PagamentoModel;
using Application.UseCases.PagamentoUseCase;
using Domain.Entities;
using Domain.Entities.Base;
using Domain.Enums;
using Domain.Gateways.External;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Test.Application.UseCases.PagamentoUseCase
{
    public class PostPagamentoUseCaseAsyncTests
    {
        [Fact]
        public async Task ExecuteAsync_ShouldInsertPagamento_WhenCalled()
        {
            // Arrange
            var mockPagamentoGateway = new Mock<IPagamentoGateway>();
            mockPagamentoGateway.Setup(x => x.InsertAsync(It.IsAny<Domain.Entities.Pagamento>())).Returns(Task.CompletedTask);

            var useCase = new PostPagamentoUseCaseAsync(mockPagamentoGateway.Object);

            var request = new PagamentoPostRequest { Tipo = TipoPagamento.Pix, PedidoId = "pedido01" };

            // Act
            await useCase.ExecuteAsync(request);

            // Assert
            mockPagamentoGateway.Verify(x => x.InsertAsync(It.Is<Domain.Entities.Pagamento>(p => p.TipoPagamento == TipoPagamento.Pix && p.PedidoId == "pedido01")), Times.Once);
           
        }
    }
}

