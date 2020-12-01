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
        public async Task<IActionResult> CreateConstrain(MultipleConstrainsCreateDto multipleConstrainsCreateDto)
        {
            List<Constrain> constrainList = new List<Constrain>();

            foreach (var deviceId in multipleConstrainsCreateDto.Devices)
            {
                ConstrainCreateDto constrainCreateDto = new ConstrainCreateDto();
                constrainCreateDto.DeviceId = deviceId;
                constrainCreateDto.Name = multipleConstrainsCreateDto.Name;
                constrainCreateDto.Description = multipleConstrainsCreateDto.Description;
                constrainCreateDto.HumidityMax = multipleConstrainsCreateDto.HumidityMax;
                constrainCreateDto.HumidityMin = multipleConstrainsCreateDto.HumidityMin;
                constrainCreateDto.PressureMax = multipleConstrainsCreateDto.PressureMax;
                constrainCreateDto.PressureMin = multipleConstrainsCreateDto.PressureMin;
                constrainCreateDto.TemperatureMax = multipleConstrainsCreateDto.TemperatureMax;
                constrainCreateDto.TemperatureMin = multipleConstrainsCreateDto.TemperatureMin;

                var constrain = _mapper.Map<Constrain>(constrainCreateDto);

                await _repository.CreateObjectAsync(constrain);
                constrainList.Add(constrain);
            }

            if (constrainList.Any())
            {
                return Ok(_mapper.Map<IEnumerable<ConstrainReadDto>>(constrainList));
            }

            return NotFound();
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
