using Microsoft.AspNetCore.Identity;

namespace PawAdoption_Backend.Models.Domain
{
    public class Role : IdentityRole<Guid>
    {
        public static readonly string Admin = "Admin";
        public static readonly string Adopter = "Adopter";
    }
}


/*You can then use these role constants in your application to assign the appropriate roles to users. For example, when creating a new user, you can assign a role like this:
 
var user = new User
{
    // User properties
};

var adminRole = new Role { Name = Role.Admin };
var adopterRole = new Role { Name = Role.Adopter };

// Assign roles to the user
user.Roles.Add(adminRole);
user.Roles.Add(adopterRole);

 
 Or, when checking a user's role:

 if (user.Roles.Contains(Role.Admin))
{
    // User is an admin
}

if (user.Roles.Contains(Role.Adopter))
{
    // User is an adopter
}*/