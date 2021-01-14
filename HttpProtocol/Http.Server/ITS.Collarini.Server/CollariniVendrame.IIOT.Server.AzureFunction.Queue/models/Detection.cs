using System;
using System.Collections.Generic;
using System.Text;
using InfluxDB.Client.Core;

namespace CollariniVendrame.IIOT.Server.AzureFunction.Queue.Models
{
    public class Detection
    {
        public int SensorId { get; set; }
        public int ScooterId { get; set; }
        public string SensorValue { get; set; }
        public string SensorType { get; set; }
        public DateTime SensorDetectionDate { get; set; }
    }
}
