using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollariniVendrame.IIOT.Server.API.ModelResponse;
using CollariniVendrame.IIOT.Server.Models;
using CollariniVendrame.IIOT.Server.ServiceStorageSQL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CollariniVendrame.IIOT.Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetectionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDetectionRepository _detectionRepository;
        private readonly string _connectionStringSQL;
        public DetectionController(IConfiguration conf, IDetectionRepository rep)
        {
            _configuration = conf;
            _detectionRepository = rep;
            _connectionStringSQL = _configuration.GetConnectionString("SQL_CollariniVendrame");
        }
        // GET: api/Detection
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Detection/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Detection
        [HttpPost]
        public ResponseAPI Post([FromBody] DetectionConfiguration configuration)
        {
            try
            {
                _detectionRepository.insertData(configuration, _connectionStringSQL);
                return new ResponseAPI { StatusCode = 200, Message = "Detection inserted" };
            }
            catch (Exception e) 
            {
                return new ResponseAPI { StatusCode = 500, Message = e.Message };
            }
        }

        // PUT: api/Detection/5
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
