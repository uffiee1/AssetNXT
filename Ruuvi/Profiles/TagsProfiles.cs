using AutoMapper;
using Ruuvi.Dtos;
using Ruuvi.Models;

namespace Ruuvi.Profiles
{
    public class TagsProfile : Profile
    {
        public TagsProfile()
        {
            CreateMap<Tag, TagReadDto>();
            CreateMap<TagCreateDto, Tag>();
            CreateMap<Tag,TagCreateDto>();
        }
    }
}