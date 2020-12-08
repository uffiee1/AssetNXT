using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssetNXT.Models.Core;
using AutoMapper.Configuration;

namespace AssetNXT.Configurations
{
    public class GeometricConfiguration : IConfiguration<Route>
    {
        public Task<List<Route>> IsBreached()
        {
            throw new NotImplementedException();
        }
    }
}
