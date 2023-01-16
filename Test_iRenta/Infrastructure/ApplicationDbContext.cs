using Microsoft.EntityFrameworkCore;
using Test_iRenta.Data.Models.EntityModel;

namespace WorldMafia.Infrastructure
{
    public class ApplicationDbContext : DbContext, IDisposable
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
               : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public override void Dispose()
        {
            base.Dispose();
        }

    }
}
