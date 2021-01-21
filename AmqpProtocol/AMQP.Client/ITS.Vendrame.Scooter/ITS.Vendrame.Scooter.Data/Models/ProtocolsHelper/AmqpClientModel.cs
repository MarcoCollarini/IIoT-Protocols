using ITS.Vendrame.Scooter.Data.Models.SensorsModel;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ITS.Vendrame.Scooter.Data.Models.ProtocolsHelper
{
    public class AmqpClientModel
    {
        private ConnectionFactory factory;
        private IConnection conn;
        private IModel channel;

        public AmqpClientModel()
        {
            factory = new ConnectionFactory();

            factory.UserName = "guest";
            factory.Password = "guest";
            factory.VirtualHost = "/";
            factory.HostName = "192.168.104.60";

            conn = factory.CreateConnection();
            channel = conn.CreateModel();
            channel.ExchangeDeclare(exchange: "scooter", type: ExchangeType.Direct);

            Console.WriteLine("Connesso al server AMQP.");
        }

        public void PublishMessage(string messageToJson)
        {            
            var body = Encoding.UTF8.GetBytes(messageToJson);
            var sensor = JsonSerializer.Deserialize<SensorSampleModel>(messageToJson);

            channel.BasicPublish(exchange: "scooter",
                                             routingKey: sensor.SensorType,
                                             basicProperties: null,
                                             body: body);
        }
    }
}