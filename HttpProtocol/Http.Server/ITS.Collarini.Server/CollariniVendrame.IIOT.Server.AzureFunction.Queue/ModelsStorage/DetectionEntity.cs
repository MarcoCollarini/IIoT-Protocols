using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollariniVendrame.IIOT.Server.AzureFunction.Queue.ModelsStorage
{
    public class DetectionEntity : TableEntity
    {
        public DetectionEntity() { }

        public DetectionEntity(Guid guid, int scooterId, int sensorId, string sensorValue, string sensorType, DateTime sensorDetectionDate)
        {
            PartitionKey = guid.ToString();
            RowKey = "ScooterId: " + scooterId + " - SensorId: " + sensorId;
            ScooterId = scooterId;
            SensorId = sensorId;
            SensorValue = sensorValue;
            SensorType = sensorType;
            SensorDetectionDate = sensorDetectionDate;
        }

        public int ScooterId { get; set; }
        public int SensorId { get; set; }
        public string SensorValue { get; set; }
        public string SensorType { get; set; }
        public DateTime SensorDetectionDate { get; set; }
    }
}
