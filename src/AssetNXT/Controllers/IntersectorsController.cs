using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AssetNXT.Logic;
using AssetNXT.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetNXT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntersectorsController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var point = new Point { X = 5, Y = 8 };
            var intersectors = new List<Circle>
            {
                CreateSampleCircle01(),
                CreateSampleCircle02()
            };

            return Json(new
            {
                Point = point, Circles = intersectors.Select(circle => new
                {
                    Circle = circle, OutOfBounds = !circle.IntersectsWith(point)
                })
            });
        }

        private Circle CreateSampleCircle01()
        {
            return new Circle { Position = (0, 0), Radius = 25.0 };
        }

        private Circle CreateSampleCircle02()
        {
            return new Circle { Position = (25, 10), Radius = 35.0 };
        }
    }
}
