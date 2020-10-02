using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Ruuvi.Data;
using AutoMapper;
using Ruuvi.Dtos;

namespace Ruuvi.Controllers
{
    
    // api/locations
    [Route("api/[controller]")]
    // decorator gives some default behaviour
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepo _repository;
        private readonly IMapper _mapper;
        public LocationsController(ILocationRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        // GET api/locations
        [HttpGet]
        public ActionResult <IEnumerable<LocationReadDto>> GetAllLocations()
        {
            var locationModelItemsFromRepo = _repository.GetAllLocations();

            if(locationModelItemsFromRepo != null){

               return Ok(_mapper.Map<IEnumerable<LocationReadDto>>(locationModelItemsFromRepo));
               
            }

            return NotFound();
        }

        // GET api/locations/{id}
        [HttpGet("{id}", Name="GetLocationById")]
        public ActionResult <LocationReadDto> GetLocationById(int id)
        {
            var locationModelFromRepo = _repository.GetLocationById(id);

            if(locationModelFromRepo != null)
            {
                return Ok(_mapper.Map<LocationReadDto>(locationModelFromRepo));    
            }

            return NotFound();
        }

    }
}