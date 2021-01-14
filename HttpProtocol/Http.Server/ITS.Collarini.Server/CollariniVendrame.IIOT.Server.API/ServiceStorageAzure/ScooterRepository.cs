using CollariniVendrame.IIOT.Server.API.ModelsStorageAzure;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollariniVendrame.IIOT.Server.API.ServiceStorageAzure
{
    public class ScooterRepository : IScooterRepository
    {
        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Delete(ScooterEntity id)
        {
            throw new NotImplementedException();
        }

        public EntityBase<ScooterEntity> Get(ScooterEntity id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityBase<ScooterEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public EntityBase<ScooterEntity> Insert(EntityBase<ScooterEntity> entity, CloudTable table)
        {
            return Common.InsertOrMergeEntity(table, entity);
        }

        public CloudTable insertTable(string connectionString, string table)
        {
            return Common.CreateTable(connectionString, table);
        }

        public void Update(EntityBase<ScooterEntity> entity)
        {
            throw new NotImplementedException();
        }
    }
}
