using AssetNXT.Dtos;
using AssetNXT.Models.Data;

using AutoMapper;

namespace AssetNXT.Profiles
{
    public class ConstrainProfile : Profile
    {
        public ConstrainProfile()
        {
            CreateMap<Constrain, ConstrainReadDto>();
            CreateMap<ConstrainCreateDto, Constrain>();
            CreateMap<Constrain, ConstrainCreateDto>();
        }
    }
}
