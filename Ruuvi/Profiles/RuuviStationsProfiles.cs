using AutoMapper;
using Ruuvi.Dtos;
using Ruuvi.Models;

namespace Ruuvi.Profiles
{
    public class RuuviStationsProfile : Profile
    {
        public RuuviStationsProfile()
        {
            CreateMap<RuuviStation, RuuviStationReadDto>();
            CreateMap<RuuviStationCreateDto, RuuviStation>();
            CreateMap<RuuviStation,RuuviStationCreateDto>();
        }
    }
}