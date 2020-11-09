using AssetNXT.Dtos;
using AssetNXT.Models.Data;

using AutoMapper;

namespace AssetNXT.Profiles
{
    public class RuuviStationsProfile : Profile
    {
        public RuuviStationsProfile()
        {
            CreateMap<RuuviStation, RuuviStationReadDto>();
            CreateMap<RuuviStationCreateDto, RuuviStation>();
            CreateMap<RuuviStation, RuuviStationCreateDto>();
        }
    }
}
