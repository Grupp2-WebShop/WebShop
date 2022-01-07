using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<ProductModel> Product { get; set; }
        public DbSet<ProductOrderModel> ProductOrder { get; set; }
        public DbSet<OrderModel> Order { get; set; }
        public DbSet<CustomerModel> Customer { get; set; }
        //public DbSet<OrderHistoryModel> OrderHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductOrderModel>().HasKey(pl => new { pl.ProductId, pl.OrderId });
            modelBuilder.Entity<ProductOrderModel>()
                .HasOne(po => po.Order)
                .WithMany(p => p.Products)
                .HasForeignKey(po => po.OrderId);

            modelBuilder.Entity<ProductOrderModel>()
                .HasOne(po => po.Product)
                .WithMany(p => p.Orders)
                .HasForeignKey(po => po.ProductId);
        }
    }
}
