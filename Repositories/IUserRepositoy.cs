
using PawAdoption_Backend.Models.Domain;

namespace PawAdoption_Backend.Repositories
{
    public interface IUserRepositoy
    {
        Task<User?> FindUserById(Guid id);
        Task<User?> RenewUserAsync(Guid id, User userUpdates);
        Task<User> RemoveUserAsync(User userToDelete);
        Task<List<User>> GetAllFromDbAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending, int pageNumber, int pageSize);
        Task<List<UserImage>?> FindAllUserImagesByIdAsync(User existingUser);
    }
}
