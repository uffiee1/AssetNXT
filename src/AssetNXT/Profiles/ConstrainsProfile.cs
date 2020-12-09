using AssetNXT.Dtos.Core;
using AssetNXT.Models.Core.ServiceAgreement;

using AutoMapper;

namespace AssetNXT.Profiles
{
    public class ConstrainsProfile : Profile
    {
        public ConstrainsProfile()
        {
            CreateMap<Agreement, AgreementReadDto>();
            CreateMap<AgreementCreateDto, Agreement>();
            CreateMap<Agreement, AgreementCreateDto>();
        }
    }
}
