using AssetNXT.Models.SLA;

namespace AssetNXT.Models
{
    public class SLAConfigurations
    {
        public TempConfig TempConfig { get; set; }

        public HumidConfig HumidityConfig { get; set; }

        public PressConfig PressureConfig { get; set; }
    }
}
