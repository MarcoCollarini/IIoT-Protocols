using CollariniVendrame.IIOT.Server.AzureFunction.Queue.ModelsStorage;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollariniVendrame.IIOT.Server.AzureFunction.Queue.ServiceStorage
{
    public interface IDetectionRepository
    {
        CloudTable insertTable(string connectionString, string table);
        void insertDetection(CloudTable t, DetectionEntity entity);
    }
}
