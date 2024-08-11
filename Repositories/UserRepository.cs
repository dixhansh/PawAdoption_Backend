using Microsoft.EntityFrameworkCore;
using PawAdoption_Backend.Data;
using PawAdoption_Backend.Models.Domain;

namespace PawAdoption_Backend.Repositories
{
    public class UserRepository : IUserRepositoy
    {
        private readonly PawAdoptionDataContext pawAdoptionDataContext;

        public UserRepository(PawAdoptionDataContext pawAdoptionDataContext)
        {
            this.pawAdoptionDataContext = pawAdoptionDataContext;
        }

        public async Task<User?> FindUserById(Guid id)
        {
            var existingUser = await pawAdoptionDataContext.Users.Include("AdopterAddress").FirstOrDefaultAsync(w => w.Id == id);
            if (existingUser != null)
            {
                return existingUser;
            }
            return null;
        }
    }
}
