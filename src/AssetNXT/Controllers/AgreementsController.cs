using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetNXT.Dtos.Core;
using AssetNXT.Models.Core.ServiceAgreement;
using AssetNXT.Models.Data;
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
        private readonly IMongoDataRepository<RuuviStation> _repositoryRuuviStation;
        private readonly IMapper _mapper;

        public AgreementsController(IMongoDataRepository<Agreement> repository, IMongoDataRepository<RuuviStation> repositoryRuuviStation, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _repositoryRuuviStation = repositoryRuuviStation;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConstraints()
        {
            var constraints = await this._repository.GetAllAsync();

            if (constraints != null)
            {
                return Ok(this._mapper.Map<IEnumerable<AgreementReadDto>>(constraints));
            }

            return NotFound();
        }

        [HttpGet("device/{id}")]
        public async Task<IActionResult> GetConstraintsByDeviceId(string id)
        {
            var constrains = await _repository.GetAllAsync();
            if (constrains != null)
            {
                var stations = await _repositoryRuuviStation.GetAllAsync();
                stations = stations.FindAll(doc => doc.DeviceId == id).ToList();

                var tags = stations
                    .SelectMany(x => x.Tags)
                    .Select(x => x.Id)
                    .Distinct();

                var constrainTable = tags.ToDictionary(k => k.ToLower(), v =>
                    constrains.Where(x => x.Tags.Any(y => y.Id == v))
                               .Select(x => _mapper.Map<AgreementReadDto>(x)));

                return Ok(constrainTable);
            }

            return NotFound();
        }

        [HttpGet("{id}", Name = "GetconstraintByObjectId")]
        public async Task<IActionResult> GetConstraintByObjectId(string id)
        {
            var constraint = await this._repository.GetObjectByIdAsync(id);

            if (constraint != null)
            {
                return Ok(this._mapper.Map<AgreementReadDto>(constraint));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateConstraint(AgreementCreateDto constraintCreateDto)
        {
            var constraint = this._mapper.Map<Agreement>(constraintCreateDto);

            if (constraint != null)
            {
                await this._repository.CreateObjectAsync(constraint);

                return Ok(this._mapper.Map<AgreementReadDto>(constraint));
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConstraintByObjectId(string id, AgreementCreateDto constraintCreateDto)
        {
            var constraintModel = this._mapper.Map<Agreement>(constraintCreateDto);
            var constraint = await this._repository.GetObjectByIdAsync(id);

            if (constraint != null)
            {
                constraintModel.UpdatedAt = DateTime.UtcNow;
                constraintModel.Id = new ObjectId(id);
                await this._repository.UpdateObjectAsync(id, constraintModel);
                return Ok(this._mapper.Map<AgreementReadDto>(constraintModel));
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteConstraintByObjectId(string id)
        {
            var constraintModel = await this._repository.GetObjectByIdAsync(id);

            if (constraintModel != null)
            {
                await this._repository.RemoveObjectAsync(constraintModel);
                return Ok("Successfully deleted from collection!");
            }

            return NotFound();
        }
    }
}
