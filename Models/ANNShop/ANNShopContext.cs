using Microsoft.EntityFrameworkCore;

namespace ANNShop.Model
{
  public class ANNShopContext : DbContext
  {
    public virtual DbSet<DeliverySaveAddress> DeliverySaveAddress { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
      if (!options.IsConfigured)
      {
        options.UseSqlServer("data source=DESKTOP-3FHP8JG;initial catalog=inventorymanagement;persist security info=True;user id=sa;password=@ANNserver1357;multipleactiveresultsets=True;");
      }
    }
  }
}