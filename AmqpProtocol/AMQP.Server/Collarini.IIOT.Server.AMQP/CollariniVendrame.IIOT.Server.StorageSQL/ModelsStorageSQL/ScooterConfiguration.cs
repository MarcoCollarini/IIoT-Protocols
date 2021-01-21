using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollariniVendrame.IIOT.Server.Models
{
    public class ScooterConfiguration : EntityBase<ScooterConfiguration>
    {
        public int ScooterId { get; set; }
        public string Brand { get; set; }
        public string Company { get; set; }
    }
}
