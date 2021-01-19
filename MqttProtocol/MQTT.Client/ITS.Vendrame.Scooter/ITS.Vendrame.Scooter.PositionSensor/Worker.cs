using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Client.Sensors;
using ITS.Vendrame.Scooter.Data.Models;
using ITS.Vendrame.Scooter.Data.Models.ProtocolsHelper;
using ITS.Vendrame.Scooter.Data.Models.SensorsModel;
using ITS.Vendrame.Scooter.QueueLibrary.QueueController;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ITS.Vendrame.Scooter.PositionSensor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly QueueController _queueController;
        private VirtualPositionSensor virtualPositionSensor = new VirtualPositionSensor();

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _queueController = new QueueController();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            JsonSensorModel sensorModel = new JsonSensorModel();
            MqttClientModel mqttClientModel = new MqttClientModel();
            PositionSensorModel sensore = new PositionSensorModel();
            sensore.SensorType = "Position_Sensor";
            sensore.ScooterId = 1;
            sensore.SensorId = 3; 
            string topic = "scooter/" + sensore.ScooterId + "/" + sensore.SensorId + "/" + sensore.SensorType;

            while (!stoppingToken.IsCancellationRequested)
            {
                var info = virtualPositionSensor.toJson();
                sensorModel.SensorValue = info.ToString();
                sensorModel.SensorDetectionDate = DateTime.Now;

                var json = JsonSerializer.Serialize(sensorModel);
                Console.WriteLine("Json file sent: " + json);

                MqttJsonSensorModel sensorData = new MqttJsonSensorModel
                {
                    Topic = topic,
                    SensorValue = sensorModel.SensorValue,
                    SensorDetectionDate = sensorModel.SensorDetectionDate
                };

                _queueController.InsertIntoList(sensorData);

                await Task.Delay(30000, stoppingToken);
            }
        }
    }
}
