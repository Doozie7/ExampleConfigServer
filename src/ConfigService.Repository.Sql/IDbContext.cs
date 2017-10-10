using ConfigService.Model;
using Microsoft.EntityFrameworkCore;

namespace ConfigService.Repository.Sql
{
    public interface IDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Setting> Settings { get; set; }
        DbSet<SettingType> SettingTypes { get; set; }
    }
}