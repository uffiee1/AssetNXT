using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AssetNXT.Models;

namespace AssetNXT.Repository
{
    public interface IConstrainDataRepository<TConstrain> where TConstrain : IConstrain
    {
        // Returns all latest objects from db based on date.
        List<TConstrain> GetAllLatest();

        // Returns all latest objects from db based on date Async.
        Task<List<TConstrain>> GetAllLatestAsync();

        // Returns an object by the deviceId unique for every RuuviStation.
        TConstrain GetObjectByDeviceId(string id);

        // Returns an object by the constrainId unique for every RuuviStation Async.
        Task<TConstrain> GetObjectByDeviceIdAsync(string id);

        // Returns an object by the constrainId unique for every RuuviStation.
        TConstrain GetObjectByConstrainId(string id);

        // Returns an object by the constrainId unique for every RuuviStation Async.
        Task<TConstrain> GetObjectByConstrainIdAsync(string id);

        // Returns all records by the unique constrainId.
        List<TConstrain> GetAllObjectsByConstrainId(string id);

        // Returns all records by the unique constrainId Async.
        Task<List<TConstrain>> GetAllObjectsByConstrainIdAsync(string id);

        // Creates a record from the model.
        void CreateObject(TConstrain document);

        // Creates a record from the model Async.
        Task CreateObjectAsync(TConstrain document);

        // Updates the record from the model by bson _id.
        void UpdateObject(string id, TConstrain document);

        // Updates the record from the model by bson _id Async.
        Task UpdateObjectAsync(string id, TConstrain document);

        // Removes the record from the db based on the bason _id.
        void RemoveObjectById(string id);

        // Removes the record from the db based on the bason _id Async.
        Task RemoveObjectByIdAsync(string id);

        // Removes the record from the db.
        public void RemoveObject(TConstrain document);

        // Removes the record from the db Async.
        public Task RemoveObjectAsync(TConstrain document);
    }
}
