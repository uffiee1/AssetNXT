using System.Collections.Generic;
using AssetNXT.Models.Data;

namespace AssetNXT.Configuration
{
    public interface IServiceAgreement
    {
        List<Dictionary<string, bool>> Check();

        bool Breached(Tag tag);
    }
}
