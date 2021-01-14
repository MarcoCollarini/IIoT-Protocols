using CollariniVendrame.IIOT.Server.AzureFunction.Queue.ModelsStorage;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollariniVendrame.IIOT.Server.AzureFunction.Queue.ServiceStorage
{
    public class DetectionRepository : IDetectionRepository
    {
        public void insertDetection(CloudTable t, DetectionEntity entity)
        {
            Common.InsertOrMergeEntity(t, entity);
        }

        public CloudTable insertTable(string connectionString, string table)
        {
            return Common.CreateTable(connectionString, table);
        }
    }
}
