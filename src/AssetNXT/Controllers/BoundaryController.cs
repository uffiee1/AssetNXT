using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetNXT.Dtos;
using AssetNXT.Models.Data;
using AssetNXT.Repository;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AssetNXT.Controllers
{
    [Produces("application/json")]
    [Route("api/boundaries")]
    public class BoundaryController : Controller
    {
        private readonly IMongoDataRepository<Boundary> _repository;
        private readonly IMapper _mapper;

        public BoundaryController(IMongoDataRepository<Boundary> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBoundaries()
        {
            var boundaries = await _repository.GetAllAsync();

            if (boundaries != null)
            {
                return Ok(_mapper.Map<IEnumerable<BoundaryCircleReadDto>>(boundaries));
            }

            return NotFound();
        }

        [HttpGet("{id}", Name = "GetBoundaryById")]
        public async Task<IActionResult> GetBoundaryById(string id)
        {
            var boundary = await _repository.GetObjectByIdAsync(id);

            if (boundary != null)
            {
                return Ok(_mapper.Map<BoundaryCircleReadDto>(boundary));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBoundary(BoundaryCircleCreateDto boundaryCircleCreateDto)
        {
            var boundary = _mapper.Map<Boundary>(boundaryCircleCreateDto);

            await _repository.CreateObjectAsync(boundary);

            var boundaryCircleReadDto = _mapper.Map<BoundaryCircleReadDto>(boundary);

            // https://docs.microsoft.com/en-us/dotnet/api/system.web.http.apicontroller.createdatroute?view=aspnetcore-2.2
            return CreatedAtRoute(nameof(GetBoundaryById), new { Id = boundaryCircleReadDto.Id }, boundaryCircleReadDto);
        }
    }
}
