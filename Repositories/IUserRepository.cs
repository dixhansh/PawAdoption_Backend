using PawAdoption_Backend.Models.Domain;

namespace PawAdoption_Backend.Repositories
{
    public interface IUserRepository
    {
        Task<AdopterAddress> AddAddressAsync(AdopterAddress adopterAddress);
    }
}
