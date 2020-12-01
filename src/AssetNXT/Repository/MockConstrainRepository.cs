using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetNXT.Models.Data;

namespace AssetNXT.Repository
{
    public class MockConstrainRepository : IMongoDataRepository<Constrain>
    {
        public List<Constrain> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Constrain>> GetAllAsync()
        {
            return Task.FromResult(GetAll());
        }

        public List<Constrain> GetAllLatest()
        {
            throw new NotImplementedException();
        }

        public Task<List<Constrain>> GetAllLatestAsync()
        {
            throw new NotImplementedException();
        }

        public Constrain GetObjectById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Constrain> GetObjectByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Constrain GetObjectByDeviceId(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Constrain> GetObjectByDeviceIdAsync(string id)
        {
            return Task.FromResult<Constrain>(MockConstrainSLA());
        }

        public List<Constrain> GetAllObjectsByDeviceId(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Constrain>> GetAllObjectsByDeviceIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public void CreateObject(Constrain document)
        {
            throw new NotImplementedException();
        }

        public Task CreateObjectAsync(Constrain document)
        {
            throw new NotImplementedException();
        }

        public void UpdateObject(string id, Constrain document)
        {
            throw new NotImplementedException();
        }

        public Task UpdateObjectAsync(string id, Constrain document)
        {
            throw new NotImplementedException();
        }

        public void RemoveObject(Constrain document)
        {
            throw new NotImplementedException();
        }

        public Task RemoveObjectAsync(Constrain document)
        {
            throw new NotImplementedException();
        }

        public void RemoveObjectById(string id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveObjectByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        private Constrain MockConstrainSLA()
        {
            return new Constrain()
            {
                Name = "sla-config-1",
                Description = "test",
                TemperatureMin = 0,
                TemperatureMax = 5,
                HumidityMin = 0,
                HumidityMax = 99,
                PressureMin = 0,
                PressureMax = 5000
            };
        }
    }
}
