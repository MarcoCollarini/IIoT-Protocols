using CollariniVendrame.IIOT.Server.API.ModelsStorageAzure;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollariniVendrame.IIOT.Server.API.ServiceStorageAzure
{
    public class SensorRepository : ISensorRepository
    {
        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Delete(SensorEntity id)
        {
            throw new NotImplementedException();
        }

        public EntityBase<SensorEntity> Get(SensorEntity id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityBase<SensorEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public EntityBase<SensorEntity> Insert(EntityBase<SensorEntity> entity, CloudTable table)
        {
            return Common.InsertOrMergeEntity(table, entity);
        }

        public CloudTable insertTable(string connectionString, string table)
        {
            return Common.CreateTable(connectionString, table);
        }

        public void Update(EntityBase<SensorEntity> entity)
        {
            throw new NotImplementedException();
        }
    }
}
