using PawAdoption_Backend.Data;
using PawAdoption_Backend.Models.Domain;

namespace PawAdoption_Backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PawAdoptionDataContext pawAdoptionDataContext;

        public UserRepository(PawAdoptionDataContext pawAdoptionDataContext)
        {
            this.pawAdoptionDataContext = pawAdoptionDataContext;
        }

        public async Task<AdopterAddress> AddAddressAsync(AdopterAddress adopterAddress)
        {
            await pawAdoptionDataContext.AdopterAddresses.AddAsync(adopterAddress);
            await pawAdoptionDataContext.SaveChangesAsync();

            return (adopterAddress);
        }
    }
}
