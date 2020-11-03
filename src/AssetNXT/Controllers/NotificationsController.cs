using System.Collections.Generic;
using System.Threading.Tasks;
using AssetNXT.Dtos;
using AssetNXT.Models.Data;
using AssetNXT.Repository;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AssetNXT.Controllers
{
    [Produces("application/json")]
    [Route("api/notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IMongoDataRepository<Notification> _repository;
        private readonly IMapper _mapper;

        public NotificationsController(IMongoDataRepository<Notification> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotifications()
        {
            var notifications = await _repository.GetAllLatestAsync();

            if (notifications != null)
            {
                return Ok(_mapper.Map<IEnumerable<NotificationReadDto>>(notifications));
            }

            return NotFound();
        }

        [HttpGet("{id}", Name = "GetNotificationById")]
        public async Task<IActionResult> GetNotificationById(string id)
        {
            var notification = await _repository.GetObjectByIdAsync(id);

            if (notification != null)
            {
                return Ok(_mapper.Map<NotificationReadDto>(notification));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification(NotificationCreateDto notificationCreateDto)
        {
            var notification = _mapper.Map<Notification>(notificationCreateDto);

            await _repository.CreateObjectAsync(notification);

            var notificationReadDto = _mapper.Map<NotificationReadDto>(notification);

            // https://docs.microsoft.com/en-us/dotnet/api/system.web.http.apicontroller.createdatroute?view=aspnetcore-2.2
            return CreatedAtRoute(nameof(GetNotificationById), new { Id = notificationReadDto.Id }, notificationReadDto);
        }
    }
}
