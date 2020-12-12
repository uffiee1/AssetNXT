using System.Threading.Tasks;
using AssetNXT.Models.Data;
using Microsoft.AspNetCore.SignalR;

namespace AssetNXT.Hubs
{
    public class RuuviStationHub : Hub
    {
        public Task ReceiveRuuviStation(RuuviStation station)
        {
            // boradcast request
            return Clients.All.SendAsync("GetNewRuuviStations", station);
        }
    }
}
