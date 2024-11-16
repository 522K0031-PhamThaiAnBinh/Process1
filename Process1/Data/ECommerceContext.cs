using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Process1.Models;

namespace Process1.Data
{
    // Inherit from IdentityDbContext to handle Identity-related data
    public class ECommerceContext : IdentityDbContext<IdentityUser>
    {
        public ECommerceContext(DbContextOptions<ECommerceContext> options)
            : base(options)
        {
        }

        // DbSets for your business data
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // Override OnModelCreating to configure relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Business data relationships
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerID);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderID);
        }
    }
}
