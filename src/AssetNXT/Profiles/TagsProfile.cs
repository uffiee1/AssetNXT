using AssetNXT.Dtos;
using AssetNXT.Models.Core;
using AssetNXT.Models.Data;

using AutoMapper;

namespace AssetNXT.Profiles
{
    public class TagsProfile : Profile
    {
        public TagsProfile()
        {
            CreateMap<RuuviStation, TagReadDto>();
            CreateMap<Constrain, TagReadDto>();
        }
    }
}
