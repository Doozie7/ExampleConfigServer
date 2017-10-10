using Microsoft.Extensions.Logging;

namespace ConfigService.Repository.Sql
{
    public abstract class BaseRepository<T>
    {
        protected IDbContext DbContext { get; }
        private ILogger<BaseRepository<T>> Logger { get; }

        protected BaseRepository(IDbContext dbContext, ILogger<BaseRepository<T>> logger)
        {
            DbContext = dbContext;
            Logger = logger;
        }
    }
}
