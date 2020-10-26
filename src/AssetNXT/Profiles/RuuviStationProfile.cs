using AssetNXT.Data;
using AssetNXT.Models;

using AutoMapper;

namespace AssetNXT.Profiles
{
    public class RuuviStationProfile : Profile
    {
        public RuuviStationProfile()
        {
            CreateMap<RuuviStation, RuuviStationReadDto>();
            CreateMap<RuuviStationCreateDto, RuuviStation>();
            CreateMap<RuuviStation, RuuviStationCreateDto>();
        }
    }
}
