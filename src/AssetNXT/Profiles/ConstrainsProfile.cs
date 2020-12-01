using AssetNXT.Dtos;
using AssetNXT.Models.Core;

using AutoMapper;

namespace AssetNXT.Profiles
{
    public class ConstrainsProfile : Profile
    {
        public ConstrainsProfile()
        {
            CreateMap<AgreementConstrain, AgreementConstrainReadDto>();
            CreateMap<AgreementConstrainCreateDto, AgreementConstrain>();
            CreateMap<AgreementConstrain, AgreementConstrainCreateDto>();
        }
    }
}
