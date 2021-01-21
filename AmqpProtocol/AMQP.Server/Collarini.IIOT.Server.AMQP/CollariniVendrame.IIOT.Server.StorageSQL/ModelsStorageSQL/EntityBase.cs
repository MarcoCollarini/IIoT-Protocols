using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollariniVendrame.IIOT.Server.Models
{
    public abstract class EntityBase<T>
    {
        public T Entity { get; set; }
    }
    public abstract class EntityBase : EntityBase<int>
    {
    }
}
