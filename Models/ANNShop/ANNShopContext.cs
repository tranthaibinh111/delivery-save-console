using Microsoft.EntityFrameworkCore;

namespace ANNShop.Model
{
  public class ANNShopContext : DbContext
  {
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(@"Data Source=HOANGANH-MACBOO;Initial Catalog=inventorymanagement;User ID=sa;Password=@ANNserver1357;Connection Timeout=300;;Timeout=360");

    public DbSet<DeliverySaveAddress> DeliverySaveAddresses { get; set; }

  }
}