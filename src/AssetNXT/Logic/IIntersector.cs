using AssetNXT.Models;

namespace AssetNXT.Logic
{
    public interface IIntersector
    {
        bool IntersectsWith(Point point);
    }
}
