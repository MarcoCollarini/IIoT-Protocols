using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollariniVendrame.IIOT.Server.API.ModelsStorageAzure
{
    public class ScooterEntity : EntityBase<ScooterEntity>
    {
        public ScooterEntity() { }

        public ScooterEntity(Guid guid, int scooterId, string brand, string company)
        {
            PartitionKey = guid.ToString();
            RowKey = "ScooterId: " + scooterId;
            ScooterId = scooterId;
            Brand = brand;
            Company = company;
        }
        public int ScooterId { get; set; }
        public string Brand { get; set; }
        public string Company { get; set; }
    }
}

