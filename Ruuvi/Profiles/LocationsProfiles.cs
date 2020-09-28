using AutoMapper;
using Ruuvi.Dtos;
using Ruuvi.Models;

namespace Ruuvi.Profiles
{
    public class LocationsProfile : Profile
    {
        public LocationsProfile()
        {
            CreateMap<Location, LocationReadDto>();
            CreateMap<LocationCreateDto, Location>();
            CreateMap<Location,LocationCreateDto>();
        }
    }
}