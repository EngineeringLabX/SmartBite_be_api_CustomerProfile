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
        public DbSet<MetabolicProfile> MetabolicProfiles { get; set; }
        public DbSet<CustomerPreference> CustomerPreferences { get; set; }
        public DbSet<DietaryPreference> DietaryPreferences { get; set; }
        public DbSet<AllergenRestriction> AllergenRestrictions { get; set; }
        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<NutritionTarget> NutritionTargets { get; set; }
        public DbSet<MealSchedule> MealSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AddressTypes>().HasQueryFilter(c => c.IsActive);
            builder.Entity<AddressTypes>().HasKey(c => c.AddressTypeId).HasName("PK_AddressTypes_AddressTypeId");

            builder.Entity<Addresses>().HasQueryFilter(c => c.IsActive);
            builder.Entity<Addresses>().HasKey(c => c.AddressId).HasName("PK_Addresses_AddressId");

            builder.Entity<CustomerInfo>().HasQueryFilter(c => c.IsActive);
            builder.Entity<CustomerInfo>().HasKey(c => c.CustomerId).HasName("PK_CustomerInfo_CustomerId");

            builder.Entity<MetabolicProfile>().HasQueryFilter(c => c.IsActive);
            builder.Entity<MetabolicProfile>().HasKey(c => c.MetabolicProfileId).HasName("PK_MetabolicProfiles_MetabolicProfileId");

            builder.Entity<CustomerPreference>().HasQueryFilter(c => c.IsActive);
            builder.Entity<CustomerPreference>().HasKey(c => c.CustomerPreferenceId).HasName("PK_CustomerPreferences_CustomerPreferenceId");

            builder.Entity<DietaryPreference>().HasQueryFilter(c => c.IsActive);
            builder.Entity<DietaryPreference>().HasKey(c => c.DietaryPreferenceId).HasName("PK_DietaryPreferences_DietaryPreferenceId");

            builder.Entity<AllergenRestriction>().HasQueryFilter(c => c.IsActive);
            builder.Entity<AllergenRestriction>().HasKey(c => c.AllergenRestrictionId).HasName("PK_AllergenRestrictions_AllergenRestrictionId");

            builder.Entity<Allergy>().HasQueryFilter(c => c.IsActive);
            builder.Entity<Allergy>().HasKey(c => c.AllergyId).HasName("PK_Allergies_AllergyId");

            builder.Entity<NutritionTarget>().HasQueryFilter(c => c.IsActive);
            builder.Entity<NutritionTarget>().HasKey(c => c.NutritionTargetId).HasName("PK_NutritionTargets_NutritionTargetId");

            builder.Entity<MealSchedule>().HasQueryFilter(c => c.IsActive);
            builder.Entity<MealSchedule>().HasKey(c => c.MealScheduleId).HasName("PK_MealSchedules_MealScheduleId");
        }
    }
}
