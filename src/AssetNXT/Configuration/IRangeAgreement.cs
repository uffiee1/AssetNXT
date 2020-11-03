using AssetNXT.Models.Data;

namespace AssetNXT.Configuration
{
    public interface IRangeAgreement<TAgreement> where TAgreement : IConstrain
    {
        public bool Breached(double value);
    }
}
