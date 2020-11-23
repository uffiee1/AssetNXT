using System.Collections.Generic;
using AssetNXT.Models.Data;

namespace AssetNXT.Configurations
{
    public interface IServiceAgreement
    {
        List<Configuration> IsBreached(string id);

    }
}
