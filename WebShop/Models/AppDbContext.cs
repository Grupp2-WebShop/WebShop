using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<ProductModel> Product { get; set; }
        public DbSet<ProductOrderModel> ProductOrder { get; set; }
        public DbSet<OrderModel> Order { get; set; }
        public DbSet<CustomerModel> Customer { get; set; }

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

            modelBuilder.Entity<ProductModel>().HasData(
                new ProductModel { ProductId = 1, ProductName = "Super Mario Galaxy", Price = 150, ImageName = "SuperMarioGalaxy.png", Description = "Mario defies gravity."});
            modelBuilder.Entity<ProductModel>().HasData(
                new ProductModel { ProductId = 2, ProductName = "Dark Souls Remastered", Price = 150, ImageName = "DarkSoulsRemastered.png", Description = "Prepare to die." });
            modelBuilder.Entity<ProductModel>().HasData(
                new ProductModel { ProductId = 3, ProductName = "Pokémon Brilliant Diamond", Price = 300, ImageName = "PokémonBrilliantDiamond.png", Description = "Remake of Pokémon Diamond." });
            modelBuilder.Entity<ProductModel>().HasData(
                new ProductModel { ProductId = 4, ProductName = "The Elder Scrolls V: Skyrim", Price = 300, ImageName = "Skyrim.png", Description = "An open world, Action RPG from Bethesda." });
        }
    }
}
