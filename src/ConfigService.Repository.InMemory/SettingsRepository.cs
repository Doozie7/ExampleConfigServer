// ReSharper disable ClassNeverInstantiated.Global

using System;
using System.Collections.Generic;
using System.Linq;
using ConfigService.Model;
using Microsoft.Extensions.Logging;

namespace ConfigService.Repository.InMemory
{
    public class SettingsRepository : IRepository<Setting>
    {
        private readonly ILogger<SettingsRepository> _logger;
        private static IList<Setting> _settings;

        public SettingsRepository(ILogger<SettingsRepository> logger)
        {
            _logger = logger;

            if (_settings == null)
            {
                _settings = GetInitialSettingsList();
            }
        }

        /// <inheritdoc />
        public IList<Setting> GetListOf(Func<Setting, bool> filter = null, Func<Setting, object> order = null, int take = 1000, int skip = 0)
        {
            _logger.LogWarning($"Using the InMemory {typeof(SettingsRepository).AssemblyQualifiedName}!");

            if (filter == null)
            {
                filter = c => c != null;
            }

            if (order == null)
            {
                order = c => c.Id;
            }

            return _settings.Where(filter).OrderBy(order).Take(take).Skip(skip).ToList();
        }

        /// <inheritdoc />
        public Setting Add(Setting itemToAdd)
        {
            _settings.Add(itemToAdd);
            return itemToAdd;
        }

        /// <inheritdoc />
        public void Delete(Func<Setting, bool> filter = null)
        {
            filter = filter ?? (c => c != null);
            var toDelete = _settings.Where(filter).ToList();

            foreach (var settingToDelete in toDelete)
            {
                _settings.Remove(settingToDelete);
            }
        }

        private static IList<Setting> GetInitialSettingsList()
        {
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
    }
}
