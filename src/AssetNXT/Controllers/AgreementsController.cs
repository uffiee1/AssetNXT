using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssetNXT.Dtos.Core;
using AssetNXT.Models.Core.ServiceAgreement;
using AssetNXT.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;

namespace AssetNXT.Controllers
{
    [Produces("application/json")]
    [Route("api/constrains")]
    [ApiController]
    public class AgreementsController : ControllerBase
    {
        private readonly IMongoDataRepository<Agreement> _repository;
        private readonly IMapper _mapper;

        public AgreementsController(IMongoDataRepository<Agreement> repository, IMapper mapper)
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
                return Ok(_mapper.Map<IEnumerable<AgreementReadDto>>(constrains));
            }

            return NotFound();
        }

        [HttpGet("{id}", Name = "GetConstrainByObjectId")]
        public async Task<IActionResult> GetConstrainByObjectId(string id)
        {
            var constrain = await _repository.GetObjectByIdAsync(id);

            if (constrain != null)
            {
                return Ok(_mapper.Map<AgreementReadDto>(constrain));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateConstrain(AgreementCreateDto constrainCreateDto)
        {
            var constrain = _mapper.Map<Agreement>(constrainCreateDto);

            if (constrain != null)
            {
                await _repository.CreateObjectAsync(constrain);

                return Ok(_mapper.Map<AgreementReadDto>(constrain));
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConstrainByObjectId(string id, AgreementCreateDto constrainCreateDto)
        {
            var constrainModel = _mapper.Map<Agreement>(constrainCreateDto);
            var constrain = await _repository.GetObjectByIdAsync(id);

            if (constrain != null)
            {
                constrainModel.UpdatedAt = DateTime.UtcNow;
                constrainModel.Id = new ObjectId(id);
                await _repository.UpdateObjectAsync(id, constrainModel);
                return Ok(_mapper.Map<AgreementReadDto>(constrainModel));
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteConstrainByObjectId(string id)
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
