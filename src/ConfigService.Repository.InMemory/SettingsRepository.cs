// ReSharper disable ClassNeverInstantiated.Global

using System;
using System.Collections.Generic;
using ConfigService.Model;
using Microsoft.Extensions.Logging;

namespace ConfigService.Repository.InMemory
{
    public class SettingsRepository : IRepository<Setting>
    {
        private readonly ILogger<SettingsRepository> _logger;

        public SettingsRepository(ILogger<SettingsRepository> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get a list of Settings using a filter, order and by default take 1000 and skip 0
        /// </summary>
        /// <param name="filter">The where clase in the query e.g. c =&gt; c != null</param>
        /// <param name="order">The order by clause in the query e.g. c =&gt; c.Name</param>
        /// <param name="take">by default take 1000</param>
        /// <param name="skip">by default skip 0</param>
        /// <returns></returns>
        public IList<Setting> GetListOf(Func<Setting, bool> filter = null, Func<Setting, object> order = null, int take = 1000, int skip = 0)
        {
            _logger.LogWarning($"Using the InMemory {typeof(SettingsRepository).AssemblyQualifiedName}!");

            var customer = new Customer
            {
                Id = new Guid("65e51c54-21c5-41e8-8e22-21500379b275"),
                Name = "Globex Corporation",
                Description =
                    "Nothing says, “nonspecific international company serving the needs of consumers through service and synergy” like Globex.",
                CreatedDate = DateTime.Now
            };

            var settingType = new SettingType()
            {
                Id = 1,
                ParentId = 0,
                Description = "Email Security",
                SequenceNumber = 2,
                CreatedDate = DateTime.Now
            };

            var settingType2 = new SettingType()
            {
                Id = 2,
                ParentId = 0,
                Description = "Web Security",
                SequenceNumber = 3,
                CreatedDate = DateTime.Now
            };

            var settings = new List<Setting>
            {
                new Setting
                {
                    Customer = customer,
                    CustomerId = customer.Id,
                    Id = 1,
                    SettingType = settingType,
                    SettingTypeId = settingType.Id,
                    SettingValue = "Service Enabled"
                },
                new Setting
                {
                    Customer = customer,
                    CustomerId = customer.Id,
                    Id = 2,
                    SettingType = settingType2,
                    SettingTypeId = settingType2.Id,
                    SettingValue = "Service Enabled"
                }
            };

            return settings;
        }


        /// <inheritdoc />
        public Setting Add(Setting itemToAdd)
        {
            throw new NotImplementedException();
        }

        public void Delete(Func<Setting, bool> filter = null)
        {
            throw new NotImplementedException();
        }
    }
}
