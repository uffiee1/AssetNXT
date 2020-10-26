using System;
using System.Threading.Tasks;
using AssetNXT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetNXT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SLAController : ControllerBase
    {
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Post(TestModel test)
        {
            await MockWritingToDatabase(test);
            Console.WriteLine("Succes!");
            return Ok(new { Status = "Succes" });
        }

        private Task<bool> MockWritingToDatabase(TestModel test)
        {
            return Task.FromResult(true);
        }
    }
}
