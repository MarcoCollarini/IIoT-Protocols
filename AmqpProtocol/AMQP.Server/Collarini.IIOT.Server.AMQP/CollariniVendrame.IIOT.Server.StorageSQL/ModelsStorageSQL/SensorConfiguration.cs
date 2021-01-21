using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollariniVendrame.IIOT.Server.Models
{
    public class SensorConfiguration : EntityBase<SensorConfiguration>
    {
        public int SensorId { get; set; }
        public string SensorType { get; set; }
        public string SensorMesurementUnit { get; set; }
    }
}
