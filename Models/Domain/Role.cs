using Microsoft.AspNetCore.Identity;

namespace PawAdoption_Backend.Models.Domain
{
    public class Role : IdentityRole<Guid>
    {
        public static readonly string Admin = "Admin";
        public static readonly string Adopter = "Adopter";
    }
}
