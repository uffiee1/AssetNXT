using System.Threading.Tasks;

using AssetNXT.Logic;
using AssetNXT.Models;

namespace AssetNXT.Services
{
    public class IntersectionService : IIntersectionService
    {
        public Task<bool> IntersectsWithAsync(IIntersector intersector, Point point)
        {
            return Task.FromResult(intersector.IntersectsWith(point));
        }
    }
}
