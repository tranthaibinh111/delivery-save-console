using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DeliverySave {
    public class DeliverySaveContext: DbContext {
        public DbSet<Address> Addresses {get; set;}

        protected override void OnConfiguring (DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=delivery_save.db");
    }
}