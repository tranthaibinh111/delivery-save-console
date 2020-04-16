using Microsoft.EntityFrameworkCore;

namespace DeliverySave.Model
{
  public class DeliverySaveContext : DbContext
  {
    public virtual DbSet<Address> Addresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
      if (!options.IsConfigured)
      {
        options.UseSqlite("Data Source=delivery_save.db");
      }
    }
  }
}