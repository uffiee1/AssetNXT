using System.Collections.Generic;
using System.Linq;
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

        private async Task<List<Notification>> GetAllObjectsAsync()
        {
            var stations = await _repository.GetAllAsync();
            return stations.GroupBy(doc => new { doc.DeviceId }, (key, group) => group.First()).ToList();  // Groups By DeviceId
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotifications()
        {
            var notifications = await _repository.GetAllAsync();

            if (notifications != null)
            {
                return Ok(_mapper.Map<IEnumerable<NotificationReadDto>>(notifications));
            }

            return NotFound();
        }

        [HttpGet("{id}", Name = "GetNotificationByDeviceId")]
        public async Task<IActionResult> GetNotificationByDeviceId(string id)
        {
            var notifications = await GetAllObjectsAsync();
            var notification = notifications.Find(doc => doc.DeviceId == id);

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
            return CreatedAtRoute(nameof(GetNotificationByDeviceId), new { Id = notificationReadDto.Id }, notificationReadDto);
        }
    }
}
