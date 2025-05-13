using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ZepterShop.Models.Basic;
using ZepterShop.Models.Dto;

namespace ZepterShop.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shop>()
                .HasMany(s => s.Orders)
                .WithOne(o => o.Shop)
                .HasForeignKey(o => o.ShopId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(ol => ol.Order)
                .HasForeignKey(ol => ol.OrderId);

            modelBuilder.Entity<OrdersSummarySqlDto>().HasNoKey();
            modelBuilder.Entity<OrdersSummarySqlDto>().HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }
}
