using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SmartBite.CustomerProfile.Domain.Entities;

namespace SmartBite.CustomerProfile.Infrastructure.Persistance
{
    public class CustomerProfileDbContext(DbContextOptions<CustomerProfileDbContext> options, IConfiguration configuration, ILogger<CustomerProfileDbContext> logger) : DbContext
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<CustomerProfileDbContext> _logger;

        public DbSet<CustomerInfo> CustomerInfo { get; set; }
        public DbSet<Addresses> Addresses { get; set; }
        public DbSet<AddressTypes> AddressTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AddressTypes>().HasQueryFilter(c => c.IsActive);
            builder.Entity<AddressTypes>().HasKey(c => c.AddressTypeId).HasName("PK_AddressTypes_AddressTypeId");

            builder.Entity<Addresses>().HasQueryFilter(c => c.IsActive);
            builder.Entity<Addresses>().HasKey(c => c.AddressId).HasName("PK_Addresses_AddressId");

            builder.Entity<CustomerInfo>().HasQueryFilter(c => c.IsActive);
            builder.Entity<CustomerInfo>().HasKey(c => c.CustomerId).HasName("PK_CustomerInfo_CustomerId");
        }
    }
}
