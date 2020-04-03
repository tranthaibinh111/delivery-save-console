using Microsoft.EntityFrameworkCore;

namespace DeliverySave.Model {
    public class DeliverySaveContext: DbContext {
        protected override void OnConfiguring (DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=delivery_save.db");

        public DbSet<Address> Addresses {get; set;}
    }
}