using System.Collections.Generic;
using AssetNXT.Models.Core;
using AssetNXT.Models.Data;

namespace AssetNXT.Configurations
{
    public class ServiceAgreementConfiguration : IServiceAgreementConfiguration
    {
        private List<Tag> _tags;
        private AgreementConstrain _constrain;
        private List<AgreementConfiguration> _collection;

        public ServiceAgreementConfiguration(List<Tag> tags, AgreementConstrain constrain)
        {
            this.Tags = tags;
            this.Constrain = constrain;
            this.Collection = new List<AgreementConfiguration>();
        }

        public List<Tag> Tags { get => _tags; set => _tags = value; }

        public AgreementConstrain Constrain { get => _constrain; set => _constrain = value; }

        public List<AgreementConfiguration> Collection { get => _collection; set => _collection = value; }

        public List<AgreementConfiguration> IsBreached(string id)
        {
            foreach (var tag in this.Tags)
            {
                AgreementConfiguration configuration = new AgreementConfiguration();

                configuration.DeviceId = id;
                configuration.TagId = tag.Id;
                configuration.IsActive = tag.IsActive;
                configuration.UpdateAt = tag.UpdateAt;
                configuration.CreateDate = tag.CreateDate;
                configuration.Humidity = (tag.Humidity >= this.Constrain.HumidityMin && tag.Humidity <= Constrain.HumidityMax) ? true : false;
                configuration.Pressure = (tag.Pressure >= this.Constrain.PressureMin && tag.Pressure <= Constrain.PressureMax) ? true : false;
                configuration.Temperature = (tag.Temperature >= this.Constrain.TemperatureMin && tag.Temperature <= Constrain.TemperatureMax) ? true : false;

                this.Collection.Add(configuration);
            }

            return this.Collection;
        }
    }
}
