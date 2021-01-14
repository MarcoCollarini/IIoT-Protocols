using System;
using System.Text.Json;
using CollariniVendrame.IIOT.Server.AzureFunction.Queue.Models;
using CollariniVendrame.IIOT.Server.AzureFunction.Queue.ModelsStorage;
using CollariniVendrame.IIOT.Server.AzureFunction.Queue.ServiceStorage;
using InfluxDB.Client;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CollariniVendrame.IIOT.Server.AzureFunction.Queue
{
    public class QueueTrigger
    {
        private readonly IConfiguration _configuration;
        private readonly IDetectionRepository _detectionRepository;
        private readonly IInfluxDbRepository _influxRepository;
        private readonly string _connectionString;
        private CloudTable _detectionTable;

        const string token = "gaKjZFSzy5lL3r0ypliflGCVh_kZ4DZjAZoPRV7d3Spy00AdS6cMppW4E3-gd4eouOUJMKi-a_PjptcCbcIXYg==";
        const string bucket = "CollariniVendrame";
        const string org = "marco.collarini@stud.tecnicosuperiorekennedy.it";
        private InfluxDBClient _influxDBClient;

        public QueueTrigger(IConfiguration configuration, IDetectionRepository detRep, IInfluxDbRepository influxDbRepository)
        {
            _configuration = configuration;
            _detectionRepository = detRep;
            _influxRepository = influxDbRepository;

            _connectionString = _configuration.GetConnectionString("ITS_Storage");
            _detectionTable = _detectionRepository.insertTable(_connectionString, "CollariniVendrameDetection");
            _influxDBClient = InfluxDBClientFactory.Create("https://westeurope-1.azure.cloud2.influxdata.com", token.ToCharArray());
        }
        [FunctionName("QueueTrigger")]
        public async void Run([QueueTrigger("collarini-vendrame-queue", Connection = "ITS_Storage")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            if (myQueueItem != null)
            {
                string messageTxt = myQueueItem;
                var detectionDeserialize = JsonSerializer.Deserialize<Detection>(messageTxt);
                
                //azure table storage
                var detectionEntity = new DetectionEntity(Guid.NewGuid(),
                                                            detectionDeserialize.ScooterId,
                                                            detectionDeserialize.SensorId,
                                                            detectionDeserialize.SensorValue,
                                                            detectionDeserialize.SensorType,
                                                            detectionDeserialize.SensorDetectionDate
                                                            );
                _detectionRepository.insertDetection(_detectionTable, detectionEntity);

                //influxdb storage
                var detectionInflux = new Detection
                {
                    ScooterId = detectionDeserialize.ScooterId,
                    SensorId = detectionDeserialize.SensorId,
                    SensorValue = detectionDeserialize.SensorValue,
                    SensorType = detectionDeserialize.SensorType,
                    SensorDetectionDate = detectionDeserialize.SensorDetectionDate
                };                  

                _influxRepository.insertDetection(detectionInflux, _influxDBClient, bucket, org);
              
            }
        }
    }
}