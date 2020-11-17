using AssetNXT.Dtos;
using AssetNXT.Models.Data;

using AutoMapper;

namespace AssetNXT.Profiles
{
    public class BoundariesProfile : Profile
    {
        public BoundariesProfile()
        {
            CreateMap<Boundary, BoundaryReadDto>();
            CreateMap<BoundaryCreateDto, Boundary>();
            CreateMap<Boundary, BoundaryCreateDto>();
        }
    }
}
