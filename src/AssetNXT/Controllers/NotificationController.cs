using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssetNXT.Dtos;
using AssetNXT.Models.Data;
using AssetNXT.Repository;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AssetNXT.Controllers
{
    [Route("api/notifications")]
    public class NotificationController
    {
        private readonly IMongoDataRepository<RuuviStation> _repository;
        private readonly IMapper _mapper;

        public NotificationController(IMongoDataRepository<RuuviStation> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
    }
}
