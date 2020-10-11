using System.Collections.Generic;

namespace AssetNXT.Models
{
    public class RuuviStation
    {
        public int StationId { get; set; }

        public string Location { get; set; }

        public Position Position { get; set; }

        public List<RuuviStationTag> Tags { get; set; }
    }
}
