using Microsoft.AspNetCore.Identity;
using PawAdoption_Backend.Models.Domain;
using PawAdoption_Backend.Models.DTO;

namespace PawAdoption_Backend.Services
{
    public interface ITokenService
    {
        JwtResponseDto CreateJWTToken(User user, List<String> roles);
    }
}
