using AssetNXT.Dtos;
using AssetNXT.Models.Data;

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
