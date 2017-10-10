// ReSharper disable ClassNeverInstantiated.Global

using System;
using System.Collections.Generic;
using System.Linq;
using ConfigService.Model;
using Microsoft.Extensions.Logging;

namespace ConfigService.Repository.Sql
{
    public class SettingTypesRepository : BaseRepository<SettingType>, IRepository<SettingType>
    {
        public SettingTypesRepository(IDbContext dbContext, ILogger<SettingTypesRepository> logger) : base(dbContext, logger)
        { }

        public IList<SettingType> GetListOf(Func<SettingType, bool> filter = null, Func<SettingType, object> order = null, int take = 1000, int skip = 0)
        {
            filter = filter ?? (c => c != null);
            order = order ?? (c => c.Id);

            return DbContext.SettingTypes.Where(filter).OrderBy(order).Take(take).Skip(skip).ToList();
        }

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
