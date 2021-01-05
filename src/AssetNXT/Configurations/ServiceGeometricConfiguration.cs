using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetNXT.Models.Core;
using AssetNXT.Models.Data;
using AssetNXT.Repository;
using AutoMapper.Configuration;

namespace AssetNXT.Configurations
{
    public class ServiceGeometricConfiguration : IServiceConfiguration<ServiceGeometric>
    {
        private readonly IMongoDataRepository<Route> _repository;
        private RuuviStation _station;
        private List<ServiceGeometric> _collection;

        public ServiceGeometricConfiguration(RuuviStation station, IMongoDataRepository<Route> repository)
        {
            this._station = station;
            this._collection = new List<ServiceGeometric>();
            this._repository = repository;
        }

        public async Task<List<ServiceGeometric>> IsBreachedCollection()
        {
            var constrains = await _repository.GetAllAsync();
            var filterConstrains = constrains.ToList().Where(constrain => constrain.Devices.Any(d => d == _station.DeviceId)).ToList();

            foreach (var constrain in filterConstrains)
            {
                foreach (var boundary in constrain.Points)
                {
                    ServiceGeometric configuration = new ServiceGeometric();

                    configuration.DeviceId = _station.DeviceId;
                    configuration.ConstrainName = constrain.Name;
                    configuration.Boundary = IntersectsWith(_station.Location, boundary);

                    this._collection.Add(configuration);
                }
            }

            return _collection;
        }

        public bool IntersectsWith(Location point, Boundary boundary)
        {
            var latidtude = Math.Pow(point.Latitude - boundary.Location.Latitude, 2);
            var longitude = Math.Pow(point.Longitude - boundary.Location.Longitude, 2);

            return Math.Sqrt(longitude + latidtude) < boundary.Radius;
        }

        public void SaveConfiguration(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
