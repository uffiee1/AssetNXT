using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetNXT.Dtos.Core;
using AssetNXT.Models.Core;
using AssetNXT.Models.Data;
using AssetNXT.Repository;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Ruuvi.Controllers
{
    [Produces("application/json")]
    [Route("api/routes")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IMongoDataRepository<Route> _repository;
        private readonly IMongoDataRepository<RuuviStation> _repositoryRuuviStation;
        private readonly IMapper _mapper;

        public RoutesController(IMongoDataRepository<Route> repository, IMongoDataRepository<RuuviStation> repositoryRuuviStation, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _repositoryRuuviStation = repositoryRuuviStation;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoutes()
        {
            var routes = await this._repository.GetAllAsync();

            if (routes != null)
            {
                return Ok(this._mapper.Map<IEnumerable<RouteReadDto>>(routes));
            }

            return NotFound();
        }

        [HttpGet("device/{id}")]
        public async Task<IActionResult> GetRoutesByDeviceId(string id)
        {
            var routes = await _repository.GetAllAsync();
            if (routes != null)
            {
                return Ok(_mapper.Map<IEnumerable<RouteReadDto>>(
                    routes.Where(x => x.Devices != null)
                          .Where(x => x.Devices.Contains(id))));
            }

            return NotFound();
        }

        [HttpGet("{id}", Name = "GetRouteById")]
        public async Task<IActionResult> GetRouteById(string id)
        {
            var route = await this._repository.GetObjectByIdAsync(id);

            if (route != null)
            {
                return Ok(this._mapper.Map<RouteReadDto>(route));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoute(RouteCreateDto routeCreateDto)
        {
            var route = this._mapper.Map<Route>(routeCreateDto);

            if (route != null)
            {
                await this._repository.CreateObjectAsync(route);

                var routeReadDto = this._mapper.Map<RouteReadDto>(route);

                // https://docs.microsoft.com/en-us/dotnet/api/system.web.http.apicontroller.createdatroute?view=aspnetcore-2.2
                return CreatedAtRoute(nameof(GetRouteById), new { Id = routeReadDto.Id }, routeReadDto);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRouteByObjectId(string id, RouteCreateDto routeCreateDto)
        {
            var routeModel = this._mapper.Map<Route>(routeCreateDto);
            var route = await this._repository.GetObjectByIdAsync(id);

            if (route != null)
            {
                routeModel.UpdatedAt = DateTime.UtcNow;
                routeModel.Id = new ObjectId(id);
                await this._repository.UpdateObjectAsync(id, routeModel);
                return Ok(this._mapper.Map<RouteReadDto>(routeModel));
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRouteByObjectId(string id)
        {
            var routeModel = await this._repository.GetObjectByIdAsync(id);

            if (routeModel != null)
            {
                await this._repository.RemoveObjectAsync(routeModel);
                return Ok("Successfully deleted from collection!");
            }

            return NotFound();
        }
    }
}
