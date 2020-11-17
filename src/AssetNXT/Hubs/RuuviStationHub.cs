using System.Threading.Tasks;
using AssetNXT.Hubs.Clients;
using AssetNXT.Models.Data;
using Microsoft.AspNetCore.SignalR;

namespace AssetNXT.Hubs
{
    public class RuuviStationHub : Hub<IRuuviStationClient>
    {
        public async Task SendRuuviStation(RuuviStation station)
        {
            await Clients.All.ReceiveRuuviStation(station);
        }
    }
}
