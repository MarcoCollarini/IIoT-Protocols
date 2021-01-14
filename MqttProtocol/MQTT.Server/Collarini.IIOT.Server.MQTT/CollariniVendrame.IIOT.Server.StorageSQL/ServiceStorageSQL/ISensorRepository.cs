using CollariniVendrame.IIOT.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollariniVendrame.IIOT.Server.ServiceStorageSQL
{
    public interface ISensorRepository : IRepository<EntityBase<SensorConfiguration>, SensorConfiguration>
    {
    }
}
