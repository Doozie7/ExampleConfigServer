// ReSharper disable ClassNeverInstantiated.Global

using System;
using System.Collections.Generic;
using System.Linq;
using ConfigService.Model;
using Microsoft.Extensions.Logging;

namespace ConfigService.Repository.Sql
{
    public class SettingsRepository : BaseRepository<Setting>, IRepository<Setting>
    {
        public SettingsRepository(IDbContext dbContext, ILogger<SettingsRepository> logger) : base(dbContext, logger)
        { }

        public IList<Setting> GetListOf(Func<Setting, bool> filter = null, Func<Setting, object> order = null, int take = 1000, int skip = 0)
        {
            filter = filter ?? (c => c != null);
            order = order ?? (c => c.Id);

            return DbContext.Settings.Where(filter).OrderBy(order).Take(take).Skip(skip).ToList();
        }

        /// <summary>
        /// Add a new setting to the Settings Repository
        /// </summary>
        /// <param name="itemToAdd"></param>
        /// <returns></returns>
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
