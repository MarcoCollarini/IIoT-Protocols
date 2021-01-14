using CollariniVendrame.IIOT.Server.API.ModelsStorageAzure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollariniVendrame.IIOT.Server.API.ServiceStorageAzure
{
    public interface IScooterRepository : IRepository<EntityBase<ScooterEntity>, ScooterEntity>
    {
    }
}
