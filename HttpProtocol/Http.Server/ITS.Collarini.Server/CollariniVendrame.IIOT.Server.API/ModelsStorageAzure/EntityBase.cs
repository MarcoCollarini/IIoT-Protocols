using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollariniVendrame.IIOT.Server.API.ModelsStorageAzure
{
    public abstract class EntityBase<T> : TableEntity
    {
        public T Entity { get; set; }
    }
    public abstract class EntityBase : EntityBase<int>
    {
    }
}
