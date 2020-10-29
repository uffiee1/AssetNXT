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
    [Route("api/constrains")]
    public class ConstrainController
    {
        private readonly IMongoDataRepository<Constrain> _repository;
        private readonly IMapper _mapper;

        public ConstrainController(IMongoDataRepository<Constrain> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
    }
}
