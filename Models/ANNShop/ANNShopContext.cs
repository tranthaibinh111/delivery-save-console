using Microsoft.EntityFrameworkCore;

using Service;

namespace ANNShop.Model
{
  public class ANNShopContext : DbContext
  {
    public virtual DbSet<DeliverySaveAddress> DeliverySaveAddress { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
      if (!options.IsConfigured)
      {
        var _config = FactoryService.getInstance<ConfigurationService>();

        options.UseSqlServer(_config.getConnectionSQLServer());
      }
    }
  }
}