using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;

namespace AssetNXT.Repositories
{
    public class MongoDataAccess : IMongoDataAccess
    {
        private readonly IConfiguration _configuration;

        public MongoDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<T> GetByIdAsync<T>(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync<T>()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateAsync<T>(T value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateAsync<T>(string id, T value)
        {
            throw new System.NotImplementedException();
        }
    }
}
