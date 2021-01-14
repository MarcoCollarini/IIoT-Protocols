using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CollariniVendrame.IIOT.Server.Models;
using CollariniVendrame.IIOT.Server.MQTT.Worker.Model;
using CollariniVendrame.IIOT.Server.ServiceStorageSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace CollariniVendrame.IIOT.Server.MQTT.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IDetectionRepository _detectionRepoitory;
        private readonly IConfiguration _configuration;
        private string _connectionString;
        private string brokerUrl = "localhost";
        private string topicDetection = "scooter/#";
        public Worker(ILogger<Worker> logger, IDetectionRepository rep, IConfiguration conf)
        {
            _logger = logger;
            _configuration = conf;
            _detectionRepoitory = rep;
            _connectionString = conf.GetConnectionString("SQL_CollariniVendrame");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            MqttClient client = new MqttClient(brokerUrl);
            byte code = client.Connect(Guid.NewGuid().ToString());

            client.ProtocolVersion = MqttProtocolVersion.Version_3_1;
            client.MqttMsgSubscribed += Client_MqttMsgSubscribed;
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;

            ushort subcribe = client.Subscribe(new string[] { topicDetection },
            new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

            while (!stoppingToken.IsCancellationRequested)
            {
               // _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic);
            var bodyObj = JsonSerializer.Deserialize<DetectionModel>(Encoding.UTF8.GetString(e.Message));
            var topicStr = e.Topic;
            var splitTopic = topicStr.Split("/");

            var detection = new DetectionConfiguration
            {
                ScooterId = Convert.ToInt32(splitTopic[1]),
                SensorId = Convert.ToInt32(splitTopic[2]),
                SensorType = splitTopic[3],
                SensorValue = bodyObj.SensorValue,
                SensorDetectionDate = bodyObj.SensorDetectionDate
            };
            try
            {
                _detectionRepoitory.insertData(detection, _connectionString);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Detection inserted");
            }
            catch (Exception ex) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
           
        }


        private void Client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            Console.WriteLine("Subscribed for id = " + e.MessageId);
        }
    }
}
