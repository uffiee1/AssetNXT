using AssetNXT.Dtos.Core;
using AssetNXT.Models.Core;
using AssetNXT.Models.Core.ServiceAgreement;

using AutoMapper;

namespace AssetNXT.Profiles
{
    public class ConfigurationsProfile : Profile
    {
        public ConfigurationsProfile()
        {
            CreateMap<ServiceAgreement, ServiceAgreementReadDto>();
            CreateMap<ServiceGeometric, ServiceGeometricReadDto>();
        }
    }
}
