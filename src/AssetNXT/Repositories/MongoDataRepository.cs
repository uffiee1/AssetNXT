namespace AssetNXT.Repositories
{
    public abstract class MongoDataRepository<T> : IMongoDataRepository<T>
    {
        private readonly IMongoDataAccess _dataAccess;

        public MongoDataRepository(IMongoDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
    }
}
