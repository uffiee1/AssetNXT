using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AssetNXT.Models.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AssetNXT.Configurations
{
    public class ServiceAgreement : IServiceAgreement
    {
        private List<Tag> _tags;
        private Constrain _constrain;
        private List<Configuration> _collection;

        public ServiceAgreement(List<Tag> tags, Constrain constrain)
        {
            this.Tags = tags;
            this.Constrain = constrain;
            this.Collection = new List<Configuration>();
        }

        public List<Tag> Tags { get => _tags; set => _tags = value; }

        public Constrain Constrain { get => _constrain; set => _constrain = value; }

        public List<Configuration> Collection { get => _collection; set => _collection = value; }

        public List<Configuration> IsBreached(string id)
        {
            foreach (var tag in this.Tags)
            {
                Configuration configuration = new Configuration();

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
