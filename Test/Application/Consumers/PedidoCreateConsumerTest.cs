using Application.Consumers;
using Application.Models.PagamentoModel;
using Application.UseCases;
using Domain.Enums;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Test.Application.Consumers
{
    public class PedidoCreateConsumerTests
    {
        [Fact]
        public void Consume_ValidMessage_CallsPostUseCase()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfiguration>();
            var mockPostUseCase = new Mock<IUseCaseAsync<PagamentoPostRequest>>();
            var consumer = new PedidoCreateConsumer(mockConfiguration.Object, mockPostUseCase.Object);

            var pedidoModel = new PedidoModel { PedidoId = "id-pedido-01", TipoPagamento = Domain.Enums.TipoPagamento.Cartao };
            var message = JsonSerializer.Serialize(pedidoModel);
            var body = Encoding.UTF8.GetBytes(message);
            var eventArgs = new BasicDeliverEventArgs { Body = new ReadOnlyMemory<byte>(body) };

            // Act
            consumer.Consume(this, eventArgs);

            // Assert
            mockPostUseCase.Verify(useCase => useCase.ExecuteAsync(It.Is<PagamentoPostRequest>(request =>
                request.PedidoId == pedidoModel.PedidoId && request.Tipo == pedidoModel.TipoPagamento)), Times.Once);
        }

    }
}
