using AssetNXT.Dtos;
using AssetNXT.Models.Data;

using AutoMapper;

namespace AssetNXT.Profiles
{
    public class ConstrainsProfile : Profile
    {
        public ConstrainsProfile()
        {
            CreateMap<Constrain, ConstrainReadDto>();
            CreateMap<ConstrainCreateDto, Constrain>();
            CreateMap<Constrain, ConstrainCreateDto>();
        }
    }
}
