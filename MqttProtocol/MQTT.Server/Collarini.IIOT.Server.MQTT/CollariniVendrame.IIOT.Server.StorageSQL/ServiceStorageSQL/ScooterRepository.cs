using CollariniVendrame.IIOT.Server.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CollariniVendrame.IIOT.Server.ServiceStorageSQL
{
    public class ScooterRepository : IScooterRepository
    {
        public void insertData(EntityBase<ScooterConfiguration> entity, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "INSERT INTO Scooter (ScooterId,Brand,Company) " +
                "VALUES (@ScooterId,@Brand,@Company)";
                connection.Execute(query, entity);
            }
        }
    }
}
