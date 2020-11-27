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

        private static string GetCollectionName(Type type)
        {
            return type.GetCustomAttribute<BsonCollectionAttribute>().CollectionName;
        }

        // Returns all objects from the db.
        public List<TDocument> GetAll()
        {
            var matches = _collection.Find(doc => true);
            return matches.ToList();
        }

        // Returns all objects from the db Async.
        public async Task<List<TDocument>> GetAllAsync()
        {
            return await Task.FromResult(GetAll());
        }

        // Returns all latest objects from db based on date.
        public List<TDocument> GetAllLatest()
        {
            return _collection.Find(doc => true).ToList().OrderByDescending(doc => doc.UpdatedAt).GroupBy(doc => new { doc.DeviceId }, (key, group) => group.First()).ToList();
        }

        // Returns all latest objects from db based on date Async.
        public async Task<List<TDocument>> GetAllLatestAsync()
        {
            return await Task.FromResult(GetAllLatest());
        }

        // Returns an object by the bson _id of the record.
        public TDocument GetObjectById(string id)
        {
            var matches = _collection.Find(doc => doc.Id == new ObjectId(id)).ToList().OrderByDescending(doc => doc.UpdatedAt);
            return matches.FirstOrDefault();
        }

        // Returns an object by the bson _id of the record Async.
        public async Task<TDocument> GetObjectByIdAsync(string id)
        {
            return await Task.FromResult(GetObjectById(id));
        }

        // Returns an object by the deviceId unique for every RuuviStation.
        public TDocument GetObjectByDeviceId(string id)
        {
            var matches = _collection.Find(doc => doc.DeviceId == id).ToList().OrderByDescending(doc => doc.UpdatedAt).ToList();
            return matches.FirstOrDefault();
        }

        // Returns an object by the deviceId unique for every RuuviStation Async.
        public async Task<TDocument> GetObjectByDeviceIdAsync(string id)
        {
            return await Task.FromResult(GetObjectByDeviceId(id));
        }

        // Returns all records by the unique deviceId.
        public List<TDocument> GetAllObjectsByDeviceId(string id)
        {
            return _collection.Find(doc => doc.DeviceId == id).ToList().OrderByDescending(doc => doc.UpdatedAt).ToList();
        }

        // Returns all records by the unique deviceId Async.
        public async Task<List<TDocument>> GetAllObjectsByDeviceIdAsync(string id)
        {
            return await Task.FromResult(GetAllObjectsByDeviceId(id));
        }

        // Creates a record from the model.
        public void CreateObject(TDocument document)
        {
            document.CreatedAt = DateTime.UtcNow;
            document.UpdatedAt = DateTime.UtcNow;
            _collection.InsertOne(document);
        }

        // Creates a record from the model Async.
        public async Task CreateObjectAsync(TDocument document)
        {
            document.CreatedAt = DateTime.UtcNow;
            document.UpdatedAt = DateTime.UtcNow;
            await _collection.InsertOneAsync(document);
        }

        // Updates the record from the model by bson _id.
        public void UpdateObject(string id, TDocument document)
        {
            var objectId = new ObjectId(id);
            document.UpdatedAt = DateTime.UtcNow;
            _collection.ReplaceOne(doc => doc.Id == objectId, document);
        }

        // Updates the record from the model by bson _id Async.
        public async Task UpdateObjectAsync(string id, TDocument document)
        {
            var objectId = new ObjectId(id);
            document.UpdatedAt = DateTime.UtcNow;
            await _collection.ReplaceOneAsync(doc => doc.Id == objectId, document);
        }

        // Removes the record from the db.
        public void RemoveObject(TDocument document)
        {
            _collection.DeleteOne(doc => doc.Id == document.Id);
        }

        // Removes the record from the db Async.
        public async Task RemoveObjectAsync(TDocument document)
        {
            await _collection.DeleteOneAsync(doc => doc.Id == document.Id);
        }

        // Removes the record from the db based on the bason _id.
        public void RemoveObjectById(string id)
        {
            var objectId = new ObjectId(id);
            _collection.DeleteOne(doc => doc.Id == objectId);
        }

        // Removes the record from the db based on the bason _id Async.
        public async Task RemoveObjectByIdAsync(string id)
        {
            var objectId = new ObjectId(id);
            await _collection.DeleteOneAsync(doc => doc.Id == objectId);
        }
    }
}
