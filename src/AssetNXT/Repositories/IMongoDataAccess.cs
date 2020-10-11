using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetNXT.Repositories
{
    public interface IMongoDataAccess
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<T> GetByIdAsync<T>(string id);

        Task<bool> CreateAsync<T>(T value);

        Task<bool> RemoveAsync(string id);

        Task<bool> UpdateAsync<T>(string id, T value);
    }
}
