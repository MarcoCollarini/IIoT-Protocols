using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CollariniVendrame.IIOT.Server.Models;
using CollariniVendrame.IIOT.Server.ServiceStorageSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CollariniVendrame.IIOT.Server.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IDetectionRepository _detectionRepoitory;
        private readonly IConfiguration _configuration;
        private string _connectionString;
        private string brokerUrl = "localhost";
        private string exchange = "scooter";
        private string queue = "Movement_Sensor";
        private IConnection connection;
        private IModel channel;
        public Worker(ILogger<Worker> logger, IDetectionRepository rep, IConfiguration conf)
        {
            _logger = logger;
            _configuration = conf;
            _detectionRepoitory = rep;
            _connectionString = conf.GetConnectionString("SQL_CollariniVendrame");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = brokerUrl };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Direct);
            channel.QueueDeclare(queue: queue,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
            channel.QueueBind(queue, exchange, "Movement_Sensor");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var obj = JsonSerializer.Deserialize<DetectionConfiguration>(message);
                _detectionRepoitory.insertData(obj, _connectionString);
                Console.WriteLine(" [x] Received {0}", message);
            };
            channel.BasicConsume(queue: queue,
                                    autoAck: true,
                                    consumer: consumer);
            
        }
    }
}
