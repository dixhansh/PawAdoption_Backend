
using PawAdoption_Backend.Models.Domain;

namespace PawAdoption_Backend.Repositories
{
    public interface IUserRepositoy
    {
        Task<User?> FindUserById(Guid id);
  
    }
}
