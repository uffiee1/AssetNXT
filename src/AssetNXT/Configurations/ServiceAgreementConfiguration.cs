using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetNXT.Models.Core;
using AssetNXT.Models.Data;
using AssetNXT.Repository;

namespace AssetNXT.Configurations
{
    public class ServiceAgreementConfiguration : IServiceAgreementConfiguration
    {
        private readonly IMongoDataRepository<Agreement> _repository;
        private RuuviStation _station;
        private List<Tag> _tags;
        private List<ServiceAgreement> _collection;

        public ServiceAgreementConfiguration(RuuviStation station, IMongoDataRepository<Agreement> repository)
        {
            this._tags = station.Tags;
            this._station = station;
            this._collection = new List<ServiceAgreement>();
            this._repository = repository;
        }

        public async Task<List<ServiceAgreement>> IsBreached()
        {
            foreach (var tag in _tags)
            {
                var constrains = await _repository.GetAllAsync();
                var constrain = constrains.ToList().Where(constrain => constrain.Tags.Any(t => t.Id == tag.Id)).FirstOrDefault();

                if (constrain != null)
                {
                    ServiceAgreement configuration = new ServiceAgreement();

                    configuration.DeviceId = _station.DeviceId;
                    configuration.TagId = tag.Id;
                    configuration.IsActive = tag.IsActive;
                    configuration.UpdateAt = tag.UpdateAt;
                    configuration.CreateDate = tag.CreateDate;
                    configuration.Humidity = (tag.Humidity >= constrain.HumidityMin && tag.Humidity <= constrain.HumidityMax) ? true : false;
                    configuration.Pressure = (tag.Pressure >= constrain.PressureMin && tag.Pressure <= constrain.PressureMax) ? true : false;
                    configuration.Temperature = (tag.Temperature >= constrain.TemperatureMin && tag.Temperature <= constrain.TemperatureMax) ? true : false;

                    this._collection.Add(configuration);
                }
            }

            return this._collection;
        }
    }
}
