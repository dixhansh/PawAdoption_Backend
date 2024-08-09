using Microsoft.AspNetCore.Identity.Data;
using PawAdoption_Backend.Models.Domain;
using PawAdoption_Backend.Models.DTO;

namespace PawAdoption_Backend.Services
{
    public interface IUserService
    {
        Task<string?> RegisterAsync(User user, AdopterAddress adopterAddress, string password);
        Task<JwtResponseDto?> ValidateUserAsync(LoginRequest loginRequest);
    }
}
