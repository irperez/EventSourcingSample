using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventSource.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegimenController : ControllerBase
    {
        public RegimenController(ILogger<RegimenController> logger)
        {
            _logger = logger;
        }

        private readonly ILogger<RegimenController> _logger;

        // GET: api/<RegimenController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RegimenController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RegimenController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RegimenController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RegimenController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
