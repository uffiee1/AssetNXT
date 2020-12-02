using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AssetNXT.Models;
using AssetNXT.Settings;

using MongoDB.Bson;
using MongoDB.Driver;

namespace AssetNXT.Repository.Service
{
    public class ConstrainDataRepository<TConstrain> : IConstrainDataRepository<TConstrain> where TConstrain : IConstrain
    {
        private readonly IMongoCollection<TConstrain> _collection;

        public ConstrainDataRepository(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<TConstrain>(GetCollectionName(typeof(TConstrain)));
        }

        private static string GetCollectionName(Type type)
        {
            return type.GetCustomAttribute<BsonCollectionAttribute>().CollectionName;
        }

        // Returns all latest objects from db based on date.
        public List<TConstrain> GetAllLatest()
        {
            return _collection.Find(doc => true).ToList().OrderByDescending(doc => doc.UpdatedAt).GroupBy(doc => new { doc.ConstrainId }, (key, group) => group.First()).ToList();
        }

        // Returns all latest objects from db based on date Async.
        public async Task<List<TConstrain>> GetAllLatestAsync()
        {
            return await Task.FromResult(GetAllLatest());
        }

        // Returns the last saved constrainId.
        public TConstrain GetLastConstrainId()
        {
            return _collection.Find(doc => true).ToList().OrderByDescending(doc => doc.ConstrainId).FirstOrDefault();
        }

        // Returns the last saved constrainId Async.
        public async Task<TConstrain> GetLastConstrainIdAsync()
        {
            return await Task.FromResult(GetLastConstrainId());
        }

        // Returns an object by the constrainId unique for every RuuviStation.
        public TConstrain GetObjectByConstrainId(string id)
        {
            var matches = _collection.Find(doc => doc.ConstrainId == int.Parse(id)).ToList().OrderByDescending(doc => doc.UpdatedAt).ToList();
            return matches.FirstOrDefault();
        }

        // Returns an object by the constrainId unique for every RuuviStation Async.
        public async Task<TConstrain> GetObjectByConstrainIdAsync(string id)
        {
            return await Task.FromResult(GetObjectByConstrainId(id));
        }

        // Returns all records by the unique constrainId.
        public List<TConstrain> GetAllObjectsByConstrainId(string id)
        {
            return _collection.Find(doc => doc.ConstrainId == int.Parse(id)).ToList().OrderByDescending(doc => doc.UpdatedAt).ToList();
        }

        // Returns all records by the unique constraind Async.
        public async Task<List<TConstrain>> GetAllObjectsByConstrainIdAsync(string id)
        {
            return await Task.FromResult(GetAllObjectsByConstrainId(id));
        }

        // Returns an object by the deviceId unique for every RuuviStation.
        public TConstrain GetObjectByDeviceId(string id)
        {
            var matches = _collection.Find(doc => true).ToList().OrderByDescending(doc => doc.UpdatedAt).ToList().Where(doc => doc.Devices.Contains(id));
            return matches.FirstOrDefault();
        }

        // Returns an object by the deviceId unique for every RuuviStation Async.
        public async Task<TConstrain> GetObjectByDeviceIdAsync(string id)
        {
            return await Task.FromResult(GetObjectByDeviceId(id));
        }

        // Creates a record from the model.
        public void CreateObject(TConstrain document)
        {
            document.CreatedAt = DateTime.UtcNow;
            document.UpdatedAt = DateTime.UtcNow;
            _collection.InsertOne(document);
        }

        // Creates a record from the model Async.
        public async Task CreateObjectAsync(TConstrain document)
        {
            document.CreatedAt = DateTime.UtcNow;
            document.UpdatedAt = DateTime.UtcNow;
            await _collection.InsertOneAsync(document);
        }

        // Updates the record from the model by bson _id.
        public void UpdateObject(string id, TConstrain document)
        {
            var objectId = new ObjectId(id);
            document.UpdatedAt = DateTime.UtcNow;
            _collection.ReplaceOne(doc => doc.Id == objectId, document);
        }

        // Updates the record from the model by bson _id Async.
        public async Task UpdateObjectAsync(string id, TConstrain document)
        {
            var objectId = new ObjectId(id);
            document.UpdatedAt = DateTime.UtcNow;
            await _collection.ReplaceOneAsync(doc => doc.Id == objectId, document);
        }

        // Removes the record from the db.
        public void RemoveObject(TConstrain document)
        {
            _collection.DeleteOne(doc => doc.Id == document.Id);
        }

        // Removes the record from the db Async.
        public async Task RemoveObjectAsync(TConstrain document)
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
