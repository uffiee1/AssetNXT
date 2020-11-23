using AssetNXT.Dtos;
using AssetNXT.Models.Data;

using AutoMapper;

namespace AssetNXT.Profiles
{
    public class ConfigurationsProfile : Profile
    {
        public ConfigurationsProfile()
        {
            CreateMap<Configuration, ConfigurationsReadDto>();
        }
    }
}
