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
            Constrain = constrain;
            Collection = new List<Dictionary<string, bool>>();
            Tags = station.Tags.ToList().OrderByDescending(doc => doc.CreateDate)
                .GroupBy(doc => new { doc.Id }, (key, group) => group.First()).ToList();
        }

        public List<Dictionary<string, bool>> Collection { get => _collection; set => _collection = value; }

        public List<Tag> Tags { get => _tags; set => _tags = value; }

        public Constrain Constrain { get => _constrain; set => _constrain = value; }

        public List<Dictionary<string, bool>> Check()
        {
            foreach (var tag in Tags)
            {
                Dictionary<string, bool> record = new Dictionary<string, bool>();

                record.Add(tag.Id, Breached(tag));
                Collection.Add(record);
            }

            return Collection;
        }

        public bool Breached(Tag tag)
        {
            return (tag.Humidity >= Constrain.HumidityMin && tag.Humidity <= Constrain.HumidityMax) &&
                   (tag.Pressure >= Constrain.PressureMin && tag.Pressure <= Constrain.PressureMax) &&
                   (tag.Temperature >= Constrain.TemperatureMin && tag.Humidity <= Constrain.TemperatureMax);
        }
    }
}
