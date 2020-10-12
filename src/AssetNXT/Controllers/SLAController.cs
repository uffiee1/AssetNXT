using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetNXT.Models;
using AssetNXT.Models.SLA;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetNXT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SLAController : ControllerBase
    {
        // GET: api/SLA
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/SLA
        [HttpPost]
        public string Post([FromBody] dynamic body)
        {
            if (body == null)
            {
                return "fout";
            }
            return "goed";
        }
    }
}
