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
        options.UseSqlite(@"Data Source=C:\Users\phanhoanganh9x\Documents\delivery-save-console\delivery_save.db");
      }
    }
  }
}