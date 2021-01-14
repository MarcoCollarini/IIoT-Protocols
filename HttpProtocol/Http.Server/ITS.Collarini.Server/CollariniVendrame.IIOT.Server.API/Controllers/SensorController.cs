using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollariniVendrame.IIOT.Server.API.ModelResponse;
using CollariniVendrame.IIOT.Server.API.ModelsStorageAzure;
using CollariniVendrame.IIOT.Server.API.ServiceStorageAzure;
using CollariniVendrame.IIOT.Server.ServiceStorageSQL;
using CollariniVendrame.IIOT.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;

namespace CollariniVendrame.IIOT.Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ServiceStorageAzure.ISensorRepository _sensorRepositoryAzure;
        private readonly ServiceStorageSQL.ISensorRepository _sensorRepositorySQL;
        private readonly string _connectionStringAzure;
        private readonly string _connectionStringSQL;
        private CloudTable _sensorTable;
        public SensorController(IConfiguration conf, ServiceStorageAzure.ISensorRepository repAzure, ServiceStorageSQL.ISensorRepository repSQl)
        {
            _configuration = conf;
            _sensorRepositoryAzure = repAzure;
            _sensorRepositorySQL = repSQl;
            _connectionStringAzure = _configuration.GetConnectionString("ITS_Storage");
            _connectionStringSQL = _configuration.GetConnectionString("SQL_CollariniVendrame");
        }
        // GET: api/Sensor
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Sensor/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Sensor
        [HttpPost]
        public ResponseAPI Post([FromBody] SensorConfiguration configuration)
        {
            /*
             * Insert Sensor in table storage AZURE
             * 
            _sensorTable = _sensorRepository.insertTable(_connectionStringAzure, "CollariniVendrameSensor");
            var sensorEntity = new SensorEntity(Guid.NewGuid(), configuration.SensorId, configuration.SensorType, configuration.SensorMesurementUnit);
            var result = _sensorRepository.Insert(sensorEntity, _sensorTable);
            if (result != null) 
            {
                return Ok("Sensor inserted correctly");
            }
            else 
            {
                return StatusCode(500);
            }
            */
            try
            {
                _sensorRepositorySQL.insertData(configuration, _connectionStringSQL);
                return new ResponseAPI { StatusCode = 200, Message = "Sensor inserted" };
            }
            catch (Exception e)
            {
                return new ResponseAPI { StatusCode = 500, Message = e.Message };
            } 
        }

        // PUT: api/Sensor/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
