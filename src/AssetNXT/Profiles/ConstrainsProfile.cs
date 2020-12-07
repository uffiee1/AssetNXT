using AssetNXT.Dtos;
using AssetNXT.Models.Core;

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
