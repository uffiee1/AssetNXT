using System.Collections.Generic;
using AssetNXT.Models.Core;

namespace AssetNXT.Configurations
{
    public interface IServiceAgreementConfiguration
    {
        List<ServiceAgreement> IsBreached(string id);
    }
}
