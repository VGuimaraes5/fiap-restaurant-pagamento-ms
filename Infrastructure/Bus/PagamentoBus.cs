using System.Text;
using System.Text.Json;
using Domain.Bus;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Infrastructure.Bus
{
    public class PagamentoBus : IPagamentoBus
    {
        private readonly IConfiguration _configuration;
        private readonly ConnectionFactory _connectionFactory;

        public PagamentoBus(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionFactory = new ConnectionFactory { Uri = new Uri(configuration.GetConnectionString("RabbitMQ") ?? throw new InvalidOperationException("Invalid RabbitMQ connection string!")) };
        }

        public async Task SendPaymentSuccessAsync(PagamentoStatusModel model)
        {
            var exchange = _configuration["Exchange:PaymentSuccess"] ?? throw new InvalidOperationException("Exchange not found!");
            using var connection = _connectionFactory.CreateConnection();
            using var channel = _connectionFactory.CreateConnection().CreateModel();

            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);

            await Task.Run(() =>
            {
                channel.BasicPublish(exchange, "", null, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(model)));
            });
        }

        public async Task SendPaymentErrorAsync(PagamentoStatusModel model)
        {
            var exchange = _configuration["Exchange:PaymentError"] ?? throw new InvalidOperationException("Exchange not found!");
            using var connection = _connectionFactory.CreateConnection();
            using var channel = _connectionFactory.CreateConnection().CreateModel();

            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);

            await Task.Run(() =>
            {
                channel.BasicPublish(exchange, "", null, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(model)));
            });
        }
    }
}