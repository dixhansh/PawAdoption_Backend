
using Microsoft.AspNetCore.Identity;
using PawAdoption_Backend.Models.DTO;

namespace PawAdoption_Backend.Services
{
    public interface IUserService
    {
        Task<UserResponseDto?> GetUserByIdAsync(Guid id);
        Task<UserResponseDto?> UpdateUserAsync(Guid id, UserRequestDto userDto);
        Task<UserResponseDto?> DeleteUserAsync(Guid id);
        Task<IdentityResult> UpdateOriginalPasswordAsync(Guid id, PasswordRequestDto passwordRequestDto);
        Task<List<UserResponseDto>> GetAllAsync(string? filterOn, string? filterQuery, string? sortBy, bool v, int pageNumber, int pageSize);
        Task<List<ImageResponseDto>?> GetAllUserImagesByIdAsync(Guid id);
    }
}
