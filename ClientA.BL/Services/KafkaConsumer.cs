using ClientA.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ClientA.BL.Services
{
    public class KafkaConsumer : IHostedService
    {
        private IConsumer<byte[], Car> _consumer;

        public KafkaConsumer()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost",
                AutoCommitIntervalMs = 5000,
                FetchWaitMaxMs = 50,
                GroupId = Guid.NewGuid().ToString(),
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true,
                ClientId = "2"
            };

            _consumer = new ConsumerBuilder<byte[], Car>(config)
                            .SetValueDeserializer(new MsgPackDeserializer<Car>())
                            .Build();
        }

        void Print(string s)
        {
            Console.WriteLine(s);
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Subscribe("Cars");
            Task.Factory.StartNew(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                { 
                    try
                    {
                        var result = _consumer.Consume(cancellationToken);
                        CarList.cars.Add(result.Message.Value);
                        Console.WriteLine("Incoming message:");
                        Console.WriteLine($"Car: {result.Message.Value.Name}, Year: {result.Message.Value.Year} \n");
                    }
                    catch (ConsumeException ex)
                    {
                        Console.WriteLine($"Error: {ex.Error.Reason}");
                    }
                }
            }, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
