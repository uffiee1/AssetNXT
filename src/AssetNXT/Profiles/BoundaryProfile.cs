using AssetNXT.Dtos;
using AssetNXT.Models.Data;

using AutoMapper;

namespace AssetNXT.Profiles
{
    public class BoundaryProfile : Profile
    {
        public BoundaryProfile()
        {
            CreateMap<Circle, BoundaryCircleReadDto>();
            CreateMap<BoundaryCircleCreateDto, Circle>();
            CreateMap<Circle, BoundaryCircleCreateDto>();
        }
    }
}
