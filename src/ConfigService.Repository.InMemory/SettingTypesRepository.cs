// ReSharper disable ClassNeverInstantiated.Global

using System;
using System.Collections.Generic;
using ConfigService.Model;
using Microsoft.Extensions.Logging;

namespace ConfigService.Repository.InMemory
{
    public class SettingTypesRepository : IRepository<SettingType>
    {
        private readonly ILogger<SettingTypesRepository> _logger;

        public SettingTypesRepository(ILogger<SettingTypesRepository> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get a list of SettingTypes using a filter, order and by default take 1000 and skip 0
        /// </summary>
        /// <param name="filter">The where clase in the query e.g. c =&gt; c.Enabled == true</param>
        /// <param name="order">The order by clause in the query e.g. c =&gt; c.Id</param>
        /// <param name="take">by default take 1000</param>
        /// <param name="skip">by default skip 0</param>
        /// <returns></returns>
        public IList<SettingType> GetListOf(Func<SettingType, bool> filter = null, Func<SettingType, object> order = null, int take = 1000, int skip = 0)
        {
            _logger.LogWarning("Using the InMemory CustomersRepository!");

            return new List<SettingType>
            {
                new SettingType()
                {
                    Id = 0,
                    ParentId = null,
                    Description = "Root",

                    SequenceNumber = 1,
                    CreatedDate = DateTime.Now
                },
                new SettingType()
                {
                    Id = 1,
                    ParentId = 0,
                    Description = "Email Security",
                    SequenceNumber = 2,
                    CreatedDate = DateTime.Now
                },
                new SettingType()
                {
                    Id = 2,
                    ParentId = 0,
                    Description = "Web Security",
                    SequenceNumber = 3,
                    CreatedDate = DateTime.Now
                },
            };
        }

        /// <inheritdoc />
        public SettingType Add(SettingType itemToAdd)
        {
            throw new NotImplementedException();
        }

        public void Delete(Func<SettingType, bool> filter = null)
        {
            throw new NotImplementedException();
        }
    }
}
