using CollariniVendrame.IIOT.Server.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CollariniVendrame.IIOT.Server.ServiceStorageSQL
{
    public class DetectionRepository : IDetectionRepository
    {
        public void insertData(EntityBase<DetectionConfiguration> entity, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "INSERT INTO Detection (SensorId,ScooterId,SensorValue,SensorType,SensorDetectionDate) " +
                "VALUES (@SensorId,@ScooterId,@SensorValue,@SensorType,@SensorDetectionDate)";
                connection.Execute(query, entity);
            }
        }
    }
}
