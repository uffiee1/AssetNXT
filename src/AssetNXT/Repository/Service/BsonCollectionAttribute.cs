using System;

namespace AssetNXT.Repository.Service
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal class BsonCollectionAttribute : Attribute
    {
        public string CollectionName { get; }

        public BsonCollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}
