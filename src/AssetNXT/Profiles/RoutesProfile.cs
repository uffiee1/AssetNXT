using AssetNXT.Dtos.Core;
using AssetNXT.Models.Core;

using AutoMapper;

namespace AssetNXT.Profiles
{
    public class RoutesProfile : Profile
    {
        public RoutesProfile()
        {
            CreateMap<Route, RouteReadDto>();
            CreateMap<RouteCreateDto, Route>();
            CreateMap<Route, RouteCreateDto>();
        }
    }
}
