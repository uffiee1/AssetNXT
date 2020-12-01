using System.Collections.Generic;
using AssetNXT.Models.Core;

namespace AssetNXT.Configurations
{
    public interface IGeometricConfiguration
    {
        List<GeometricConfiguration> IsBreached(string id);
    }
}
