using CollariniVendrame.IIOT.Server.API.ModelsStorageAzure;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollariniVendrame.IIOT.Server.API.ServiceStorageAzure
{
    public interface IRepository<TEntity, TKey>
                   where TEntity : EntityBase<TKey>
    {
        IEnumerable<TEntity> GetAll();

        TEntity Get(TKey id);

        TEntity Insert(TEntity entity, CloudTable table);

        void Update(TEntity entity);

        void Delete(TKey id);

        int Count();
        CloudTable insertTable(string connectionString, string table);
    }
}
