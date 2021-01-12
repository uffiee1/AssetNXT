using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetNXT.Dtos;
using AssetNXT.Models.Data;
using AssetNXT.Repository;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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
            this._mapper = mapper;
            this._repository = repository;
        }

        private async Task<List<Notification>> GetAllObjectsAsync()
        {
            var stations = await this._repository.GetAllAsync();
            return stations.GroupBy(doc => new { doc.DeviceId }, (key, group) => group.First()).ToList();  // Groups By DeviceId
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotifications()
        {
            var notifications = await this._repository.GetAllAsync();

            if (notifications != null)
            {
                return Ok(this._mapper.Map<IEnumerable<NotificationReadDto>>(notifications));
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
                return Ok(this._mapper.Map<NotificationReadDto>(notification));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification(NotificationCreateDto notificationCreateDto)
        {
            var notification = this._mapper.Map<Notification>(notificationCreateDto);

            await this._repository.CreateObjectAsync(notification);

            var notificationReadDto = _mapper.Map<NotificationReadDto>(notification);

            // https://docs.microsoft.com/en-us/dotnet/api/system.web.http.apicontroller.createdatroute?view=aspnetcore-2.2
            return CreatedAtRoute(nameof(GetNotificationByDeviceId), new { Id = notificationReadDto.Id }, notificationReadDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotificationByObjectId(string id, NotificationCreateDto notificationCreateDto)
        {
            var notificationModel = this._mapper.Map<Notification>(notificationCreateDto);
            var notification = await this._repository.GetObjectByIdAsync(id);

            if (notification != null)
            {
                notificationModel.UpdatedAt = DateTime.UtcNow;
                notificationModel.Id = new ObjectId(id);
                await this._repository.UpdateObjectAsync(id, notificationModel);
                return Ok(this._mapper.Map<Notification>(notificationModel));
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNotificationByObjectId(string id)
        {
            var notification = await this._repository.GetObjectByIdAsync(id);

            if (notification != null)
            {
                await this._repository.RemoveObjectAsync(notification);
                return Ok("Successfully deleted from collection!");
            }

            return NotFound();
        }
    }
}
