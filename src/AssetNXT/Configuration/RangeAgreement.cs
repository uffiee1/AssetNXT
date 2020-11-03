using AssetNXT.Models.Data;

namespace AssetNXT.Configuration
{
    public class RangeAgreement<TAgreement> : IRangeAgreement<TAgreement> where TAgreement : IConstrain
    {
        private double _minValue;
        private double _maxValue;

        public RangeAgreement(double minValue, double maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public double MinValue { get => _minValue; set => _minValue = value; }

        public double MaxValue { get => _maxValue; set => _maxValue = value; }

        public bool Breached(double value)
        {
            return value >= MinValue && value <= MaxValue;
        }
    }
}
