using Microsoft.EntityFrameworkCore;

using Service;

namespace DeliverySave.Model
{
  public class DeliverySaveContext : DbContext
  {
    public virtual DbSet<Address> Addresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
      if (!options.IsConfigured)
      {
        var _config = FactoryService.getInstance<ConfigurationService>();

        options.UseSqlite(_config.getConnectionSQLite());
      }
    }
  }
}