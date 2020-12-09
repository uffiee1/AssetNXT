using System.Threading.Tasks;
using AssetNXT.Models.Data;

namespace AssetNXT.Hubs.Clients
{
    public interface IRuuviStationClient
    {
        Task ReceiveRuuviStation(RuuviStation station);
    }
}
