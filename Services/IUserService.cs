
using PawAdoption_Backend.Models.DTO;

namespace PawAdoption_Backend.Services
{
    public interface IUserService
    {
        Task<UserResponseDto?> GetUserByIdAsync(Guid id);
    }
}
