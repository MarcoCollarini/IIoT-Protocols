using CollariniVendrame.IIOT.Server.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CollariniVendrame.IIOT.Server.ServiceStorageSQL
{
    public class SensorRepository : ISensorRepository
    {
        public void insertData(EntityBase<SensorConfiguration> entity, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "INSERT INTO Sensor (SensorId,SensorType,SensorMesurementUnit) " +
                "values (@SensorId,@SensorType,@SensorMesurementUnit)";
                connection.Execute(query, entity);
            }
        }
    }
}
