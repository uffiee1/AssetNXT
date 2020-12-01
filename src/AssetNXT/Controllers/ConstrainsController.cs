using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetNXT.Dtos;
using AssetNXT.Models.Core;
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
        private readonly IConstrainDataRepository<AgreementConstrain> _repository;
        private readonly IMapper _mapper;

        public ConstrainsController(IConstrainDataRepository<AgreementConstrain> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConstrains()
        {
            var constrains = await _repository.GetAllLatestAsync();

            if (constrains != null)
            {
                return Ok(_mapper.Map<IEnumerable<AgreementConstrainReadDto>>(constrains));
            }

            return NotFound();
        }

        [HttpGet("{id}", Name = "GetConstrainByConstrainId")]
        public async Task<IActionResult> GetConstrainByConstrainId(string id)
        {
            var constrains = await _repository.GetAllObjectsByConstrainIdAsync(id);

            if (constrains != null)
            {
                return Ok(_mapper.Map<IEnumerable<AgreementConstrainReadDto>>(constrains));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateConstrain(AgreementConstrainCreateDto constrainCreateDto)
        {
            var constrain = _mapper.Map<AgreementConstrain>(constrainCreateDto);

            if (constrain != null)
            {
                var lastConstrain = await _repository.GetLastConstrainIdAsync();
                constrain.ConstrainId = lastConstrain != null ? lastConstrain.ConstrainId + 1 : 0;

                await _repository.CreateObjectAsync(constrain);

                return Ok(_mapper.Map<AgreementConstrainReadDto>(constrain));
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConstrain(string id, AgreementConstrainCreateDto constrainCreateDto)
        {
            var constrainModel = _mapper.Map<AgreementConstrain>(constrainCreateDto);
            var constrain = await _repository.GetObjectByConstrainIdAsync(id);

            if (constrain != null)
            {
                constrainModel.UpdatedAt = DateTime.UtcNow;
                constrainModel.Id = new MongoDB.Bson.ObjectId(id);
                _repository.UpdateObject(id, constrainModel);
                return Ok(_mapper.Map<AgreementConstrainReadDto>(constrainModel));
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteConstrain(string id)
        {
            var constrainModel = await _repository.GetObjectByConstrainIdAsync(id);

            if (constrainModel != null)
            {
                await _repository.RemoveObjectAsync(constrainModel);
                return Ok("Successfully deleted from collection!");
            }

            return NotFound();
        }
    }
}
