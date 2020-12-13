using AssetNXT.Dtos.Core;
using AssetNXT.Models.Core.ServiceAgreement;

using AutoMapper;

namespace AssetNXT.Profiles
{
    public class ConstraintsProfile : Profile
    {
        public ConstraintsProfile()
        {
            CreateMap<Agreement, AgreementReadDto>();
            CreateMap<AgreementCreateDto, Agreement>();
            CreateMap<Agreement, AgreementCreateDto>();
        }
    }
}
