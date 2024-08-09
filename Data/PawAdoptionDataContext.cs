using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PawAdoption_Backend.Models.Domain;
using System.Data;

namespace PawAdoption_Backend.Data
{
    public class PawAdoptionDataContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>   
    {
        public PawAdoptionDataContext(DbContextOptions<PawAdoptionDataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<AdopterAddress> AdopterAddresses { get; set; }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetMedicalRecord> PetMedicalRecords { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seeding custom roles in IdentityRoles
            var adminRoleId = "ae46930c-eeed-4603-9153-d18dae47def7";
            var adopterRoleId = "1dc38c61-1a74-48c8-bdca-e10d18a2cdda";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole
                {
                    Id = adopterRoleId,
                    ConcurrencyStamp = adopterRoleId,
                    Name = "Adopter",
                    NormalizedName = "Adopter".ToUpper()
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

            // Iterate through all the entity types in the model
            // For each entity type that inherits from the BaseEntity class,
            // configure the UpdatedAt property to:
            // 1. Have a default value of the current UTC timestamp (GETUTCDATE())
            // 2. Be automatically updated whenever the entity is added or modified
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property("UpdatedAt")
                        .HasDefaultValueSql("GETUTCDATE()")
                        .ValueGeneratedOnAddOrUpdate();
                }
            }


        }

    }
}
