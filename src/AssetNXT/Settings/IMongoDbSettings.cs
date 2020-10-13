namespace AssetNXT.Settings
{
    public interface IMongoDbSettings
    {
        public string Name { get; }

        public string ConnectionString { get; }
    }
}
