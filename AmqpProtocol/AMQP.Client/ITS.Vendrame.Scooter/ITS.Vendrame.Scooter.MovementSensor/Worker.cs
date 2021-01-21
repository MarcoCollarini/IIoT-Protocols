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

namespace ITS.Vendrame.Scooter.MovementSensor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly QueueController _queueController;
        private VirtualMovementSensor virtualMovementSensor = new VirtualMovementSensor();

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _queueController = new QueueController();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SensorSampleModel sensore = new SensorSampleModel();
            sensore.SensorType = "Movement_Sensor";
            sensore.ScooterId = 1;
            sensore.SensorId = 2;

            while (!stoppingToken.IsCancellationRequested)
            {
                var info = virtualMovementSensor.GetMovement();
                sensore.SensorValue = info.ToString();
                sensore.SensorDetectionDate = DateTime.Now;

                _queueController.InsertIntoList(sensore);

                Console.WriteLine("Added " + JsonSerializer.Serialize(sensore) + " to queue.");

                await Task.Delay(15000, stoppingToken);
            }
        }
    }
}
