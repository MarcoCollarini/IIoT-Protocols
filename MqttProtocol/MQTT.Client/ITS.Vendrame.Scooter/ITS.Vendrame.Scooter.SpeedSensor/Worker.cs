using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
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
using uPLibrary.Networking.M2Mqtt;

namespace ITS.Vendrame.Scooter.SpeedSensor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly QueueController _queueController;
        private VirtualSpeedSensor virtualSpeedSensor = new VirtualSpeedSensor();

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
            SpeedSensorModel sensore = new SpeedSensorModel();
            sensore.SensorType = "Speed_Sensor";
            sensore.ScooterId = 1;
            sensore.SensorId = 4;
            string topic = "scooter/" + sensore.ScooterId + "/" + sensore.SensorId + "/" + sensore.SensorType;

            while (!stoppingToken.IsCancellationRequested)
            {
                var info = virtualSpeedSensor.GetSpeed();
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

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
