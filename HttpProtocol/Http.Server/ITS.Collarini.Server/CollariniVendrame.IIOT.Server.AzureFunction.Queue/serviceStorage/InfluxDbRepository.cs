using CollariniVendrame.IIOT.Server.AzureFunction.Queue.Models;
using CollariniVendrame.IIOT.Server.AzureFunction.Queue.ModelsStorage;
using InfluxDB.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollariniVendrame.IIOT.Server.AzureFunction.Queue.ServiceStorage
{
    public class InfluxDbRepository : IInfluxDbRepository
    {
        public void insertDetection(Detection detectionInflux, InfluxDBClient influxDBClient, string bucket, string org)
        {
            InfluxService.insertDetection(detectionInflux, influxDBClient, bucket, org);
        }
    }
}
