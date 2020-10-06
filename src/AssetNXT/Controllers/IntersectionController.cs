using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetNXT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntersectionController : Controller
    {
        // GET: api/Intersection
        [HttpGet]
        public IActionResult Get()
        {
            return Json(new List<bool>() { true, false, false, true, false });
        }
    }
}
