using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AssetNXT.Models;
using AssetNXT.Settings;

using MongoDB.Bson;
using MongoDB.Driver;

namespace AssetNXT.Repository
{
    public class MongoDataRepository<TDocument> : IMongoDataRepository<TDocument>
    where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        public MongoDataRepository(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        public List<TDocument> GetAll()
        {
            var matches = _collection.Find(doc => true);
            return matches.ToList();
        }

        public async Task<List<TDocument>> GetAllAsync()
        {
            return await Task.FromResult(GetAll());
        }

        public TDocument GetObjectByDeviceId(string id)
        {
            var matches = _collection.Find(doc => doc.DeviceId == id);
            return matches.FirstOrDefault();
        }

        public async Task<TDocument> GetObjectByDeviceIdAsync(string id)
        {
            var matches = await _collection.FindAsync(doc => doc.DeviceId == id);
            return await matches.FirstOrDefaultAsync();
        }

        public void CreateObject(TDocument document)
        {
            document.CreatedAt = DateTime.UtcNow;
            document.UpdatedAt = DateTime.UtcNow;
            _collection.InsertOne(document);
        }

        public async Task CreateObjectAsync(TDocument document)
        {
            document.CreatedAt = DateTime.UtcNow;
            document.UpdatedAt = DateTime.UtcNow;
            await _collection.InsertOneAsync(document);
        }

        public void UpdateObject(string id, TDocument document)
        {
            var objectId = new ObjectId(id);
            document.UpdatedAt = DateTime.UtcNow;
            _collection.ReplaceOne(doc => doc.Id == objectId, document);
        }

        public async Task UpdateObjectAsync(string id, TDocument document)
        {
            var objectId = new ObjectId(id);
            document.UpdatedAt = DateTime.UtcNow;
            await _collection.ReplaceOneAsync(doc => doc.Id == objectId, document);
        }

        public void RemoveObject(TDocument document)
        {
            _collection.DeleteOne(doc => doc.Id == document.Id);
        }

        public async Task RemoveObjectAsync(TDocument document)
        {
            await _collection.DeleteOneAsync(doc => doc.Id == document.Id);
        }

        public void RemoveObjectById(string id)
        {
            var objectId = new ObjectId(id);
            _collection.DeleteOne(doc => doc.Id == objectId);
        }

        public async Task RemoveObjectByIdAsync(string id)
        {
            var objectId = new ObjectId(id);
            await _collection.DeleteOneAsync(doc => doc.Id == objectId);
        }

        private static string GetCollectionName(Type type)
        {
            return type.GetCustomAttribute<BsonCollectionAttribute>().CollectionName;
        }

        public List<TDocument> GetAllLatest()
        {
            return _collection.Find(doc => true).ToList().OrderByDescending(doc => doc.UpdatedAt).GroupBy(doc => new { doc.DeviceId }, (key, group) => group.First()).ToList();
        }

        public async Task<List<TDocument>> GetAllLatestAsync()
        {
            return await Task.FromResult(GetAllLatest());
        }

        public TDocument GetObjectById(string id)
        {
            var matches = _collection.Find(doc => doc.Id == new ObjectId(id)).ToList();
            return matches.FirstOrDefault();
        }

        public async Task<TDocument> GetObjectByIdAsync(string id)
        {
            return await Task.FromResult(GetObjectById(id));
        }

        public List<TDocument> GetAllByDeviceId(string id)
        {
            var matches = _collection.Find(doc => doc.DeviceId == id);
            return matches.ToList();
        }

        public async Task<List<TDocument>> GetAllByDeviceIdAsync(string id)
        {
            return await Task.FromResult(GetAllByDeviceId(id));
        }
    }
}
