using PawAdoption_Backend.Models.Domain;

namespace PawAdoption_Backend.Repositories
{
    public interface IAuthRepository
    {
        Task<AdopterAddress> AddAddressAsync(AdopterAddress adopterAddress);
    }
}
