using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Presistence
{
    public class ECommerceDbContext : IdentityDbContext<ApplicationUser>, IECommerceDbContext
    {

        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options) { }

        DbSet<Product> products { get; set; }
        DbSet<ProductSize> productSizes { get; set; }
        DbSet<Category> categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Stock> Stocks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.ImageUrl).HasMaxLength(500);

                entity.HasOne(e => e.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(e => e.CategoryId);
            });


            modelBuilder.Entity<ProductSize>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Size).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Price).HasPrecision(18, 2);

                entity.HasOne(e => e.Product)
                      .WithMany(p => p.Sizes)
                      .HasForeignKey(e => e.ProductId);
            });


            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ShippingAddress).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsShipped).HasDefaultValue(false);


                entity.HasOne<ApplicationUser>()
                      .WithMany()
                      .HasForeignKey(e => e.ClientId)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.UnitPrice).HasPrecision(18, 2);

                entity.HasOne(e => e.Order)
                      .WithMany(o => o.OrderItems)
                      .HasForeignKey(e => e.OrderId);

                entity.HasOne(e => e.ProductSize)
                      .WithMany()
                      .HasForeignKey(e => e.ProductSizeId);
            });


            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Quantity).IsRequired();

                entity.HasOne(e => e.ProductSize)
                      .WithMany()
                      .HasForeignKey(e => e.ProductSizeId);
            });



            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.OwnerId).IsRequired();
                entity.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(p => p.OwnerId)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.Restrict);
            });








        }



    }
}
