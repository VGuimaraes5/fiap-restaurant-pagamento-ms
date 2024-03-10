using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.Models.PagamentoModel;
using Application.UseCases;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Application.Consumers
{
    public class PedidoCreateConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IUseCaseAsync<PagamentoPostRequest> _postUseCase;

        public PedidoCreateConsumer(IConfiguration configuration, IUseCaseAsync<PagamentoPostRequest> postUseCase)
        {
            _configuration = configuration;
            _postUseCase = postUseCase;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connectionFactory = new ConnectionFactory { Uri = new Uri(_configuration.GetConnectionString("RabbitMQ") ?? throw new InvalidOperationException("Invalid RabbitMQ connection string!")) };
            var exchange = _configuration["Exchange:PedidoCreate"] ?? throw new InvalidOperationException("Exchange not found!");

            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);
            var queueName = channel.QueueDeclare().QueueName;
            var consumer = new EventingBasicConsumer(channel);

            channel.QueueBind(queueName, exchange, "");
            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            consumer.Received += this.Consume;

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        public async void Consume(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var data = JsonSerializer.Deserialize<PedidoModel>(message);

                await _postUseCase.ExecuteAsync(new PagamentoPostRequest
                {
                    PedidoId = data.PedidoId,
                    Tipo = data.TipoPagamento
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}