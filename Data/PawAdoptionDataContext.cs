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
        public DbSet<UserImage> UserImages { get; set; }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetMedicalRecord> PetMedicalRecords { get; set; }
        public DbSet<PetImage> PetImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seeding custom roles in IdentityRoles
            var adminRoleId = "ae46930c-eeed-4603-9153-d18dae47def7";
            var adopterRoleId = "1dc38c61-1a74-48c8-bdca-e10d18a2cdda";
            var roles = new List<Role>
        {
            new Role
            {
                Id = new Guid(adminRoleId),
                ConcurrencyStamp = adminRoleId,
                Name = Role.Admin,
                NormalizedName = Role.Admin.ToUpper()
            },
            new Role
            {
                Id = new Guid(adopterRoleId),
                ConcurrencyStamp = adopterRoleId,
                Name = Role.Adopter,
                NormalizedName = Role.Adopter.ToUpper()
            }
        };
            modelBuilder.Entity<Role>().HasData(roles);

            // Seed Admin User
            var adminId = new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"); // Static GUID for admin
            var adminSecurityStamp = new Guid("c589c268-0ad8-4d0d-8376-f194bfac675e").ToString(); // Static GUID for SecurityStamp
            var hasher = new PasswordHasher<User>();
            var adminUser = new User
            {
                Id = adminId,
                UserName = "dixhansh@paw.com",
                NormalizedUserName = "DIXHANSH@PAW.COM",
                Email = "dixhansh@paw.com",
                NormalizedEmail = "DIXHANSH@PAW.COM",
                EmailConfirmed = true,
                FirstName = "Dixhansh",
                LastName = "Mamgain",
                DateOfBirth = new DateOnly(1998, 02, 10), // Example date
                Occupation = "FullStack Developer",
                SecurityStamp = adminSecurityStamp
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "admin@123");
            modelBuilder.Entity<User>().HasData(adminUser);

            // Assign Admin Role to Admin User
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = new Guid(adminRoleId),
                UserId = adminId
            });


            // Exclude BaseEntity from being directly mapped
            modelBuilder.Ignore<BaseEntity>();

            // For all entities inheriting from BaseEntity
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(t => typeof(BaseEntity).IsAssignableFrom(t.ClrType)))
            {
                // Configure CreatedAt and UpdatedAt
                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime>("CreatedAt")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("GETUTCDATE()");

                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime>("UpdatedAt")
                    .ValueGeneratedOnAddOrUpdate()
                   .HasDefaultValueSql("GETUTCDATE()");

            }

            // Configure CreatedAt and UpdatedAt for the User entity
            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<User>()
                .Property(u => u.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
               .HasDefaultValueSql("GETUTCDATE()");


            /*Configuring relationships between entities using Fluent API*/

            // Relationship: User (Adopter) -> AdoptionApplication
            modelBuilder.Entity<User>()
                .HasMany(u => u.SubmittedApplications)
                .WithOne(a => a.Adopter)
                .HasForeignKey(a => a.AdopterId)
                .OnDelete(DeleteBehavior.Restrict); // Cascade delete: Deleting Adopter deletes related records in AdoptionApplication

            // Relationship: User (Admin) -> AdoptionApplication
            modelBuilder.Entity<User>()
                .HasMany(u => u.ProcessedApplications)
                .WithOne(a => a.Admin)
                .HasForeignKey(a => a.ProcessedByAdmin)
                .OnDelete(DeleteBehavior.SetNull); // SetNull: Deleting Admin nullifies ProcessedByAdmin in AdoptionApplication

            //Relationship: Pet -> AdoptionApplication
            modelBuilder.Entity<Pet>()
               .HasMany(u => u.SubmittedApplications)
               .WithOne(a => a.Pet)
               .HasForeignKey(a => a.PetId)
               .OnDelete(DeleteBehavior.Restrict); // Restrict delete: Deleting Pet record will be restricted if there is a related record in AdoptionApplication

            // Configure Pet -> PetImages (one-to-many)
            modelBuilder.Entity<Pet>()
                .HasMany(p => p.PetImages)
                .WithOne(i => i.Pet)
                .HasForeignKey(i => i.PetId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure User -> UserImages (one-to-many)
            modelBuilder.Entity<User>()
                .HasMany(p => p.UserImages)
                .WithOne(i => i.User)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            //Relationship: User(Adopter) -> AdoptionBill
            modelBuilder.Entity<User>()
                .HasMany(u => u.AdoptionBills)
                .WithOne(a => a.Adopter)
                .HasForeignKey(a => a.AdopterId)
                .OnDelete(DeleteBehavior.Restrict);

            //Relationship: User(Admin) -> AdoptionBill
            modelBuilder.Entity<User>()
                .HasMany(u => u.ProcessedBills)
                .WithOne(a => a.Admin)
                .HasForeignKey(a => a.ProcessedByAdmin)
                .OnDelete(DeleteBehavior.SetNull);

            //Relationship: AdoptionApplication -> AdoptionBill
            modelBuilder.Entity<AdoptionApplication>()
               .HasOne(u => u.AdoptionBill)
               .WithOne(a => a.AdoptionApplication)
               .HasForeignKey<AdoptionBill>(a => a.AdoptionApplicationId) //In AdoptionBill the FK is present
               .OnDelete(DeleteBehavior.Restrict);

        }

       

    }
}
