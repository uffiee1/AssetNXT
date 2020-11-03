using AssetNXT.Models.Data;

namespace AssetNXT.Configuration
{
    public class ServiceAgreement<TAgreement> : IServiceAgreement<TAgreement> where TAgreement : ITag
    {
        private RangeAgreement<Constrain> _humidityAgreement;

        private RangeAgreement<Constrain> _pressureAgreement;

        private RangeAgreement<Constrain> _temperatureAgreement;

        public ServiceAgreement(Constrain constrain)
        {
            this._humidityAgreement.MaxValue = constrain.HumidityMax;
            this._humidityAgreement.MinValue = constrain.HumidityMin;
            this._pressureAgreement.MaxValue = constrain.PressureMax;
            this._pressureAgreement.MinValue = constrain.PressureMin;
            this._temperatureAgreement.MaxValue = constrain.PressureMax;
            this._temperatureAgreement.MinValue = constrain.PressureMin;
        }

        public RangeAgreement<Constrain> HumidityAgreement { get => _humidityAgreement; set => _humidityAgreement = value; }

        public RangeAgreement<Constrain> PressureAgreement { get => _pressureAgreement; set => _pressureAgreement = value; }

        public RangeAgreement<Constrain> TemperatureAgreement { get => _temperatureAgreement; set => _temperatureAgreement = value; }

        public bool CheckBreach(TAgreement agreement)
        {
            return HumidityAgreement.Breached(agreement.Humidity)
                   && PressureAgreement.Breached(agreement.Pressure)
                   && TemperatureAgreement.Breached(agreement.Temperature);
        }
    }
}
