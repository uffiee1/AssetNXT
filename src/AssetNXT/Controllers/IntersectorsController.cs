using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AssetNXT.Logic;
using AssetNXT.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetNXT.Controllers
{
    [ApiController]
    public class IntersectorsController : Controller
    {
        [Route("api/[controller]/Triangles")]
        [HttpGet]
        public IActionResult Triangles()
        {
            var point = new Point { X = 5, Y = 8 };
            var intersectors = new List<Triangle>
            {
                CreateSampleTriangle01(),
                CreateSampleTriangle02()
            };

            return Json(new
            {
                Point = point,
                Triangles = intersectors.Select(triangle => new
                {
                    Triangle = triangle,
                    IntersectsWith = triangle.IntersectsWith(point)
                })
            });
        }

        [Route("api/[controller]/Circles")]
        [HttpGet]
        public IActionResult Circles()
        {
            var point = new Point { X = 5, Y = 8 };
            var intersectors = new List<Circle>
            {
                CreateSampleCircle01(),
                CreateSampleCircle02()
            };

            return Json(new
            {
                Point = point,
                Circles = intersectors.Select(circle => new
                {
                    Circle = circle,
                    IntersectsWith = circle.IntersectsWith(point)
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

        private Triangle CreateSampleTriangle01()
        {
            return new Triangle
            {
                A = new Point { X = 0, Y = 0 },
                B = new Point { X = 3, Y = 2 },
                C = new Point { X = 6, Y = 0 },
            };
        }

        private Triangle CreateSampleTriangle02()
        {
            return new Triangle
            {
                A = new Point { X = 6, Y = 0 },
                B = new Point { X = 3, Y = 2 },
                C = new Point { X = 0, Y = 0 },
            };
        }
    }
}
