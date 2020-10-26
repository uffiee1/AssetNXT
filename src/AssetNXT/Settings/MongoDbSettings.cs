namespace AssetNXT.Settings
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string Name { get; set; }

        public string ConnectionString { get; set; }
    }
}
