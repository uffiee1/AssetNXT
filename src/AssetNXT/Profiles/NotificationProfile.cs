using AssetNXT.Dtos;
using AssetNXT.Models.Data;

using AutoMapper;

namespace AssetNXT.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationReadDto>();
            CreateMap<NotificationCreateDto, Notification>();
            CreateMap<Notification, NotificationCreateDto>();
        }
    }
}
