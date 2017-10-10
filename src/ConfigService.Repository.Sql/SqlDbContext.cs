using ConfigService.Model;
using Microsoft.EntityFrameworkCore;

namespace ConfigService.Repository.Sql
{
    public class SqlDbContext : Microsoft.EntityFrameworkCore.DbContext, IDbContext
    {

        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<SettingType> SettingTypes { get; set; }
    }
}
