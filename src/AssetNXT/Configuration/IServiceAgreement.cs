using AssetNXT.Models.Data;

namespace AssetNXT.Configuration
{
    public interface IServiceAgreement<TAgreement> where TAgreement : ITag
    {
        bool CheckBreach(TAgreement value);
    }
}
