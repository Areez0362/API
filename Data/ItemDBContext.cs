using API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ItemDBContext : DbContext
    {
        public ItemDBContext(DbContextOptions<ItemDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Items>().HasKey(table => new {
                table.ItemID
            });

            builder.Entity<Receipt>().HasKey(table => new {
                table.ReceiptID
            });

            builder.Entity<SoldItems>().HasKey(table => new {
                table.ItemID, table.ReceiptID
            });

        }

        public DbSet<Items> Items { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        public DbSet<SoldItems> SoldItems { get; set; }

    }
}
