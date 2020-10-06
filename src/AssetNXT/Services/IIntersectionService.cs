using System.Threading.Tasks;

using AssetNXT.Logic;
using AssetNXT.Models;

namespace AssetNXT.Services
{
    public interface IIntersectionService
    {
        Task<bool> IntersectsWithAsync(IIntersector intersector, Point point);
    }
}
