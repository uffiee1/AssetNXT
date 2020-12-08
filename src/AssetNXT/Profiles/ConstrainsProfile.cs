using AssetNXT.Dtos.Core;
using AssetNXT.Models.Core.ServiceAgreement;

using AutoMapper;

namespace AssetNXT.Profiles
{
    public class ConstrainsProfile : Profile
    {
        public ConstrainsProfile()
        {
            CreateMap<Agreement, AgreementConstrainReadDto>();
            CreateMap<AgreementConstrainCreateDto, Agreement>();
            CreateMap<Agreement, AgreementConstrainCreateDto>();
        }
    }
}
