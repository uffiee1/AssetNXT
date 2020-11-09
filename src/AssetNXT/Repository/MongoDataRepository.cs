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
            var matches = await _collection.FindAsync(doc => true);
            return await matches.ToListAsync();
        }

        public TDocument GetObjectById(string id)
        {
            var matches = _collection.Find(doc => doc.Id == new ObjectId(id));
            return matches.FirstOrDefault();
        }

        public async Task<TDocument> GetObjectByIdAsync(string id)
        {
            var matches = await _collection.FindAsync(doc => doc.Id == new ObjectId(id));
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
            // CreatedAt should be changed to UpdatedAt
            return _collection.Find(doc => true).ToList().OrderByDescending(doc => doc.CreatedAt).GroupBy(doc => new { doc.DeviceId }, (key, group) => group.First()).ToList();
        }

        public async Task<List<TDocument>> GetAllLatestAsync()
        {
            // CreatedAt should be changed to UpdatedAt
            var matches = await _collection.Find(doc => true).ToListAsync();
            return matches.OrderByDescending(doc => doc.CreatedAt).GroupBy(doc => new { doc.DeviceId }, (key, group) => group.First()).ToList();
        }

        public List<TDocument> GetAllToday()
        {
            return _collection.Find<TDocument>(doc => doc.CreatedAt > DateTime.UtcNow.AddDays(-1)).ToList();
        }

        public async Task<List<TDocument>> GetAllTodayAsync()
        {
            return await _collection.Find<TDocument>(doc => doc.CreatedAt > DateTime.UtcNow.AddDays(-1)).ToListAsync();
        }

        public List<TDocument> GetObjectsByDeviceId(string id)
        {
            var matches = _collection.Find(doc => doc.DeviceId == id);
            return matches.ToList();
        }

        public async Task<List<TDocument>> GetObjectsByDeviceIdAsync(string id)
        {
            var matches = await _collection.FindAsync(doc => doc.DeviceId == id);
            return await matches.ToListAsync();
        }
    }
}
