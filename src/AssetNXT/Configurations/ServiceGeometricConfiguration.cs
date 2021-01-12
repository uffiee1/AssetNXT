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
        private readonly IMongoDataRepository<Route> _geometricRepository;
        private readonly IMongoDataRepository<ServiceGeometric> _serviceGeometricRepository;
        private RuuviStation _station;
        private List<ServiceGeometric> _collection;

        public ServiceGeometricConfiguration(RuuviStation station, IMongoDataRepository<Route> geometricRepository, IMongoDataRepository<ServiceGeometric> serviceGeometricRepository)
        {
            this._station = station;
            this._collection = new List<ServiceGeometric>();
            this._geometricRepository = geometricRepository;
            this._serviceGeometricRepository = serviceGeometricRepository;
        }

        public async Task<List<ServiceGeometric>> IsBreachedCollection()
        {
            var constrains = await this._geometricRepository.GetAllAsync();
            var filterConstrains = constrains.ToList().Where(constrain => constrain.Devices.Any(d => d == this._station.DeviceId)).ToList();

            foreach (var constrain in filterConstrains)
            {
                foreach (var boundary in constrain.Points)
                {
                    ServiceGeometric configuration = new ServiceGeometric();

                    configuration.DeviceId = this._station.DeviceId;
                    configuration.ConstrainName = constrain.Name;
                    configuration.Boundary = IntersectsWith(this._station.Location, boundary);

                    this._collection.Add(configuration);

                    if (configuration.Boundary)
                    {
                        SaveConfiguration(configuration);
                    }
                }
            }

            return this._collection;
        }

        public bool IntersectsWith(Location point, Boundary boundary)
        {
            var ky = 40000 / 360;
            var kx = Math.Cos(Math.PI * point.Latitude / 180.0) * ky;
            var dx = Math.Abs(boundary.Location.Longitude - point.Longitude) * kx;
            var dy = Math.Abs(boundary.Location.Latitude - point.Latitude) * ky;
            return Math.Sqrt((dx * dx) + (dy * dy)) <= boundary.Radius / 1000;
        }

        public void SaveConfiguration(object obj)
        {
            this._serviceGeometricRepository.CreateObject((ServiceGeometric)obj);
        }
    }
}
