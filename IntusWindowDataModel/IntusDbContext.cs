using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace IntusWindowDataModel
{
    public class IntusDbContext : DbContext
    {
        public IntusDbContext()
        {

        }
        public IntusDbContext(DbContextOptions<IntusDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Orders> orders { get; set; }
        public DbSet<Windows> windows { get; set; }
        public DbSet<SubElements> subElements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                "Server=.;Database = dbIntusWindows; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            }
        }
    }
}