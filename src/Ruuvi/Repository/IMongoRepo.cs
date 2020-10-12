using Ruuvi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ruuvi.Repository
{
    public interface IMongoRepo<TDocument> where TDocument : IDocument
    {
        IEnumerable<TDocument> GetAll();

        Task<IEnumerable<TDocument>> GetAllAsync();
        
        TDocument GetObjectById(string id);

        Task<TDocument> GetObjectByIdAsync(string id);

        void CreateObject(TDocument document);

        Task CreateObjectAsync(TDocument document);

        void UpdateObject(string id, TDocument document);

        Task UpdateObjectAsync(string id, TDocument document);

        void RemoveObject(TDocument document);

        Task RemoveObjectAsync(TDocument document);

        void RemoveObjectById(string id);

        Task RemoveObjectByIdAsync(string id);

    }
}