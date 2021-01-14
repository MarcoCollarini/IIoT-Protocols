using System;
using System.Collections.Generic;
using System.Text;

namespace CollariniVendrame.IIOT.Server.MQTT.Worker.Model
{
    public class DetectionModel
    {
        public string SensorValue { get; set; }
        public DateTime SensorDetectionDate { get; set; }
    }
}
