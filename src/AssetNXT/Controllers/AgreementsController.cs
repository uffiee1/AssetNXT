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
    [Route("api/constraints")]
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
        public async Task<IActionResult> GetAllConstraints()
        {
            var constraints = await _repository.GetAllAsync();

            if (constraints != null)
            {
                return Ok(_mapper.Map<IEnumerable<AgreementReadDto>>(constraints));
            }

            return NotFound();
        }

        [HttpGet("{id}", Name = "GetconstraintByObjectId")]
        public async Task<IActionResult> GetConstraintByObjectId(string id)
        {
            var constraint = await _repository.GetObjectByIdAsync(id);

            if (constraint != null)
            {
                return Ok(_mapper.Map<AgreementReadDto>(constraint));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateConstraint(AgreementCreateDto constraintCreateDto)
        {
            var constraint = _mapper.Map<Agreement>(constraintCreateDto);

            if (constraint != null)
            {
                await _repository.CreateObjectAsync(constraint);

                return Ok(_mapper.Map<AgreementReadDto>(constraint));
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConstraintByObjectId(string id, AgreementCreateDto constraintCreateDto)
        {
            var constraintModel = _mapper.Map<Agreement>(constraintCreateDto);
            var constraint = await _repository.GetObjectByIdAsync(id);

            if (constraint != null)
            {
                constraintModel.UpdatedAt = DateTime.UtcNow;
                constraintModel.Id = new ObjectId(id);
                await _repository.UpdateObjectAsync(id, constraintModel);
                return Ok(_mapper.Map<AgreementReadDto>(constraintModel));
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteConstraintByObjectId(string id)
        {
            var constraintModel = await _repository.GetObjectByIdAsync(id);

            if (constraintModel != null)
            {
                await _repository.RemoveObjectAsync(constraintModel);
                return Ok("Successfully deleted from collection!");
            }

            return NotFound();
        }
    }
}
