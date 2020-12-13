using System.Threading.Tasks;
using AssetNXT.Models.Data;

namespace AssetNXT.Hubs
{
    public interface IRuuviStationClient
    {
        Task ReceiveRuuviStation(RuuviStation value);
    }
}
