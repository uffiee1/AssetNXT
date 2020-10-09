using System.Linq;
using Ruuvi.Settings;
using MongoDB.Driver;
using System.Collections.Generic;
using Ruuvi.Models;
using System.Threading.Tasks;
using System;
using Ruuvi.Models.Data;
using MongoDB.Bson;

namespace Ruuvi.Repository
{
    public class MongoRepo<TDocument> : IMongoRepo<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepo(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        private protected string GetCollectionName(Type type)
        {
            return ((BsonCollectionAttribute) type.GetCustomAttributes(
                typeof(BsonCollectionAttribute), true).FirstOrDefault())?.CollectionName;
        }

        public virtual IEnumerable<TDocument> GetAll()
        {
            return _collection.Find(doc => true).ToList();
        }

        public virtual Task<IEnumerable<TDocument>> GetAllAsync()
        {
            return Task.Run( () =>
            {
               return (IEnumerable<TDocument>) _collection.Find(doc => true).ToList();
            });
        }

        public virtual TDocument GetObjectById(string id)
        {
            var objectId = new ObjectId(id);
            return _collection.Find<TDocument>(doc => doc.Id == objectId).FirstOrDefault();
        }

        public virtual Task<TDocument> GetObjectByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                 return _collection.Find<TDocument>(doc => doc.Id == objectId).FirstOrDefault();
            });
        }

        public virtual void CreateObject(TDocument document)
        {
            _collection.InsertOne(document);
        }

        public virtual Task CreateObjectAsync(TDocument document)
        {
            return Task.Run(() => _collection.InsertOneAsync(document));
        }

        public virtual void UpdateObject(string id, TDocument document)
        {
            var objectId = new ObjectId(id);
            _collection.ReplaceOne(doc => doc.Id == objectId, document);
        }

        public virtual Task UpdateObjectAsync(string id, TDocument document){
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                _collection.ReplaceOne(doc => doc.Id == objectId, document);
            });
        }

        public void RemoveObject(TDocument document)
        {
            _collection.DeleteOne(doc => doc.Id == document.Id);
        }

        public virtual Task RemoveObjectAsync(TDocument document)
        {
            return Task.Run(() =>
            {
                _collection.DeleteOne(doc => doc.Id == document.Id);
            });
        }

        public virtual void RemoveObjectById(string id)
        {
            var objectId = new ObjectId(id);
            _collection.DeleteOne(doc => doc.Id == objectId);
        }

        public virtual Task RemoveObjectByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                _collection.DeleteOne(doc => doc.Id == objectId);
            });
        }
    }
}