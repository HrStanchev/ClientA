using ClientA.BL.Interfaces;
using ClientA.Models;
using MessagePack;
using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace ClientA.BL.Services
{
    public class RabbitMqService : IRabbitMqService, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;


        public RabbitMqService()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare("Test", ExchangeType.Fanout);

            _channel.QueueDeclare("car_queue", true, false);
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }

        public async Task PublishCarAsync(Car car)
        {
            await Task.Factory.StartNew(() =>
            {
                var body = MessagePackSerializer.Serialize(car);

                _channel.BasicPublish("", "car_queue", body: body);
            }
            );
        }
    }
}
