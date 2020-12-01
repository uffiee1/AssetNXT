using AssetNXT.Dtos;
using AssetNXT.Models.Core;

using AutoMapper;

namespace AssetNXT.Profiles
{
    public class ConfigurationsProfile : Profile
    {
        public ConfigurationsProfile()
        {
            CreateMap<ServiceAgreement, ServiceConfigurationReadDto>();
        }
    }
}
