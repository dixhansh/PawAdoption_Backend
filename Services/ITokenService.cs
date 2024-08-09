using Microsoft.AspNetCore.Identity;

namespace PawAdoption_Backend.Services
{
    public interface ITokenService
    {
        string CreateJWTToken(IdentityUser user, List<String> roles);
    }
}
