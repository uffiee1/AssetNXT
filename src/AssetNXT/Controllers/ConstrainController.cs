using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssetNXT.Dtos;
using AssetNXT.Models.Data;
using AssetNXT.Repository;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AssetNXT.Controllers
{
    [Produces("application/json")]
    [Route("api/constrains")]
    public class ConstrainController : Controller
    {
        private readonly IMongoDataRepository<Constrain> _repository;
        private readonly IMapper _mapper;

        public ConstrainController(IMongoDataRepository<Constrain> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConstrains()
        {
            var constrains = await _repository.GetAllLatestAsyc();

            if (constrains != null)
            {
                return Ok(_mapper.Map<IEnumerable<ConstrainReadDto>>(constrains));
            }

            return NotFound();
        }

        [HttpGet("{id}", Name = "GetConstrainById")]
        public async Task<IActionResult> GetConstrainById(string id)
        {
            var constrain = await _repository.GetObjectByIdAsync(id);

            if (constrain != null)
            {
                return Ok(_mapper.Map<ConstrainReadDto>(constrain));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateConstrain(ConstrainCreateDto constrainCreateDto)
        {
            var constrain = _mapper.Map<Constrain>(constrainCreateDto);

            await _repository.CreateObjectAsync(constrain);

            var constrainReadDto = _mapper.Map<ConstrainReadDto>(constrain);

            // https://docs.microsoft.com/en-us/dotnet/api/system.web.http.apicontroller.createdatroute?view=aspnetcore-2.2
            return CreatedAtRoute(nameof(GetConstrainById), new { Id = constrainReadDto.Id }, constrainReadDto);
        }
    }
}
