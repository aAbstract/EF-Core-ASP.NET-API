using Microsoft.EntityFrameworkCore;

// entity framework models
using DB_API_TEST.Models.Data;

namespace DB_API_TEST.Database
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options) : base(options) { }

        // database tables
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductOrder> ProductOrder { get; set; }

        public DbSet<User> Users { get; set; }

        // many to many models
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Order>() // first ef model of the relation
            .HasMany(order => order.ProductOrders) // first part of the relation
            .WithOne(productOrder => productOrder.Order) // second part of the relation
            .HasForeignKey(order => order.OrderId); // foreign key name (first ef model PK)
        }
    }
}