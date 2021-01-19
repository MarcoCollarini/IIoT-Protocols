using System;
using System.Collections.Generic;
using System.Text;

namespace ITS.Vendrame.Scooter.Data.Models.SensorsModel
{
    public class MqttJsonSensorModel : JsonSensorModel
    {
        public string Topic { get; set; }

    }
}
