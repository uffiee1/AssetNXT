using System.Collections.Generic;
using System.Linq;
using AssetNXT.Models;
using AssetNXT.Models.Data;

namespace AssetNXT.Configuration
{
    public class ServiceAgreement : IServiceAgreement
    {
        private List<Dictionary<string, bool>> _collection;
        private List<Tag> _tags;
        private Constrain _constrain;

        public ServiceAgreement(RuuviStation station, Constrain constrain)
        {
            this.Constrain = constrain;
            this.Collection = new List<Dictionary<string, bool>>();
            this.Tags = station.Tags.ToList().OrderByDescending(doc => doc.CreateDate)
                .GroupBy(doc => new { doc.Id }, (key, group) => group.First()).ToList();
        }

        public List<Dictionary<string, bool>> Collection { get => _collection; set => _collection = value; }

        public List<Tag> Tags { get => _tags; set => _tags = value; }

        public Constrain Constrain { get => _constrain; set => _constrain = value; }

        public List<Dictionary<string, bool>> Check()
        {
            foreach (var tag in this.Tags)
            {
                Dictionary<string, bool> record = new Dictionary<string, bool>();

                record.Add(tag.Id, this.Breached(tag));
                this.Collection.Add(record);
            }

            return this.Collection;
        }

        public bool Breached(Tag tag)
        {
            return (tag.Humidity >= this.Constrain.HumidityMin && tag.Humidity <= Constrain.HumidityMax) &&
                   (tag.Pressure >= this.Constrain.PressureMin && tag.Pressure <= Constrain.PressureMax) &&
                   (tag.Temperature >= this.Constrain.TemperatureMin && tag.Humidity <= Constrain.TemperatureMax);
        }
    }
}
