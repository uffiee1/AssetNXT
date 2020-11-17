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
    [ApiController]
    public class ConstrainsController : ControllerBase
    {
        private readonly IMongoDataRepository<Constrain> _repository;
        private readonly IMapper _mapper;

        public ConstrainsController(IMongoDataRepository<Constrain> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConstrains()
        {
            var constrains = await _repository.GetAllAsync();

            if (constrains != null)
            {
                return Ok(_mapper.Map<IEnumerable<ConstrainReadDto>>(constrains));
            }

            return NotFound();
        }

        [HttpGet("{id}", Name = "GetConstrainByDeviceId")]
        public async Task<IActionResult> GetConstrainByDeviceId(string id)
        {
            var constrain = await _repository.GetObjectByDeviceIdAsync(id);

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
            return CreatedAtRoute(nameof(GetConstrainByDeviceId), new { Id = constrainReadDto.Id }, constrainReadDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConstrain(string id, ConstrainCreateDto constrainCreateDto)
        {
            var constrainModel = _mapper.Map<Constrain>(constrainCreateDto);
            var constrain = await _repository.GetObjectByIdAsync(id);

            if (constrain != null)
            {
                constrainModel.UpdatedAt = DateTime.UtcNow;
                constrainModel.Id = new MongoDB.Bson.ObjectId(id);
                _repository.UpdateObject(id, constrainModel);
                return Ok(_mapper.Map<ConstrainReadDto>(constrainModel));
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteConstrain(string id)
        {
            var constrainModel = await _repository.GetObjectByIdAsync(id);

            if (constrainModel != null)
            {
                await _repository.RemoveObjectAsync(constrainModel);
                return Ok("Successfully deleted from collection!");
            }

            return NotFound();
        }
    }
}
