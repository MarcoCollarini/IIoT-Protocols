using CollariniVendrame.IIOT.Server.AzureFunction.Queue.Models;
using CollariniVendrame.IIOT.Server.AzureFunction.Queue.ModelsStorage;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollariniVendrame.IIOT.Server.AzureFunction.Queue.ServiceStorage
{
    public class InfluxService
    {
        public static void insertDetection(Detection detectionInflux, InfluxDBClient influxDBClient, string bucket, string org) 
        {
            using (var writeApi = influxDBClient.GetWriteApi()) 
            {
                try {
                    var point = PointData.Measurement("detection")
                        .Field("SensorId", (double)detectionInflux.SensorId)
                        .Field("ScooterId", (double)detectionInflux.ScooterId)
                        .Timestamp(DateTime.UtcNow.AddSeconds(-10), WritePrecision.Ns);

                    writeApi.WriteRecord(bucket, org, WritePrecision.Ns,"detection, sensor_id=1, scooter_id=2");
                }
                catch(Exception e) { Console.WriteLine(e.Message); }
                
            }
        }
    }
}
