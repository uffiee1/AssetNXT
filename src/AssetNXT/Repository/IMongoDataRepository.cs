using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AssetNXT.Models;

namespace AssetNXT.Repository
{
    public interface IMongoDataRepository<TDocument>
    where TDocument : IDocument
    {
        // Returns all objects from the db.
        List<TDocument> GetAll();

        // Returns all objects from the db Async.
        Task<List<TDocument>> GetAllAsync();

        // Returns all latest objects from db based on date.
        List<TDocument> GetAllLatest();

        // Returns all latest objects from db based on date Async.
        Task<List<TDocument>> GetAllLatestAsync();

        // Returns an object by the bson _id of the record.
        TDocument GetObjectById(string id);

        // Returns an object by the bson _id of the record Async.
        Task<TDocument> GetObjectByIdAsync(string id);

        // Returns an object by the deviceId unique for every RuuviStation.
        TDocument GetObjectByDeviceId(string id);

        // Returns an object by the deviceId unique for every RuuviStation Async.
        Task<TDocument> GetObjectByDeviceIdAsync(string id);

        // Returns all records by the unique deviceId.
        List<TDocument> GetAllObjectsByDeviceId(string id);

        // Returns all records by the unique deviceId Async.
        Task<List<TDocument>> GetAllObjectsByDeviceIdAsync(string id);

        // Creates a record from the model.
        void CreateObject(TDocument document);

        // Creates a record from the model Async.
        Task CreateObjectAsync(TDocument document);

        // Updates the record from the model by bson _id.
        void UpdateObject(string id, TDocument document);

        // Updates the record from the model by bson _id Async.
        Task UpdateObjectAsync(string id, TDocument document);

        // Removes the record from the db.
        void RemoveObject(TDocument document);

        // Removes the record from the db Async.
        Task RemoveObjectAsync(TDocument document);

        // Removes the record from the db based on the bason _id.
        void RemoveObjectById(string id);

        // Removes the record from the db based on the bason _id Async.
        Task RemoveObjectByIdAsync(string id);
    }
}
