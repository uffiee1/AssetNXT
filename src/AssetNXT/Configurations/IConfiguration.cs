using System.Collections.Generic;
using System.Threading.Tasks;
using AssetNXT.Models.Core;

namespace AssetNXT.Configurations
{
    public interface IConfiguration<TConfiguration>
    {
        Task<List<TConfiguration>> IsBreached();
    }
}
