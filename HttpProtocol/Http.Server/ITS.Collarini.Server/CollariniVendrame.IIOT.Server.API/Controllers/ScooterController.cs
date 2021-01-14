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
    public class ScooterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ServiceStorageAzure.IScooterRepository _scooterRepositoryAzure;
        private readonly ServiceStorageSQL.IScooterRepository _scooterRepositorySQL;
        private readonly string _connectionStringAzure;
        private readonly string _connectionStringSQL;
        private CloudTable _sensorTable;
        public ScooterController(IConfiguration conf, ServiceStorageAzure.IScooterRepository repAzure, ServiceStorageSQL.IScooterRepository repSQl)
        {
            _configuration = conf;
            _scooterRepositoryAzure = repAzure;
            _scooterRepositorySQL = repSQl;
            _connectionStringAzure = _configuration.GetConnectionString("ITS_Storage");
            _connectionStringSQL = _configuration.GetConnectionString("SQL_CollariniVendrame");
        }
        // GET: api/Scooter
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Scooter/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Scooter
        [HttpPost]
        public ResponseAPI Post([FromBody] ScooterConfiguration configuration)
        {
            /*
             * Insert Scooter in table storage AZURE 
             * 
            _sensorTable = _scooterRepository.insertTable(_connectionStringAzure, "CollariniVendrameScooter");
            var scooterEntity = new ScooterEntity(Guid.NewGuid(), configuration.ScooterId, configuration.Brand, configuration.Company);
            var result = _scooterRepository.Insert(scooterEntity, _sensorTable);
            if (result != null)
            {
                return Ok("Scooter inserted correctly");
            }
            else
            {
                return StatusCode(500);
            }
            */
            try
            {
                _scooterRepositorySQL.insertData(configuration, _connectionStringSQL);
                return new ResponseAPI { StatusCode = 200, Message = "Scooter inserted" };
            }
            catch (Exception e)
            {
                return new ResponseAPI { StatusCode = 500, Message = e.Message };
            }
        }
        // PUT: api/Scooter/5
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
