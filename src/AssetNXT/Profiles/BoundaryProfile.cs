using AssetNXT.Dtos;
using AssetNXT.Models.Data;

using AutoMapper;

namespace AssetNXT.Profiles
{
    public class BoundaryProfile : Profile
    {
        public BoundaryProfile()
        {
            CreateMap<Boundary, BoundaryCircleReadDto>();
            CreateMap<BoundaryCircleCreateDto, Boundary>();
            CreateMap<Boundary, BoundaryCircleCreateDto>();
        }
    }
}
