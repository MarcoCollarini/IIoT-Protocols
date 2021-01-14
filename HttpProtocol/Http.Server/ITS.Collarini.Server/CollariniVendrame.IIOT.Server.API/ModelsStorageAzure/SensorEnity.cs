using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollariniVendrame.IIOT.Server.API.ModelsStorageAzure
{
    public class SensorEntity : EntityBase<SensorEntity> 
    {
        public SensorEntity() { }

        public SensorEntity(Guid guid, int sensorId, string sensorType, string sensorMesurementUnit)
        {
            PartitionKey = guid.ToString();
            RowKey = "SensorId: " + sensorId;
            SensorId = sensorId;
            SensorType = sensorType;
            SensorMesurementUnit = sensorMesurementUnit;
        }
        public int SensorId { get; set; }
        public string SensorType { get; set; }
        public string SensorMesurementUnit { get; set; }
    }
}
