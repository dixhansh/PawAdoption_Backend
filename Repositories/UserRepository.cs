using Microsoft.EntityFrameworkCore;
using PawAdoption_Backend.Data;
using PawAdoption_Backend.Models.Domain;
using PawAdoption_Backend.Models.Enum;

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

        public async Task<User?> RenewUserAsync(Guid id, User userUpdates)
        {
            //Finding user
            var existingUser = await pawAdoptionDataContext.Users.Include("AdopterAddress").FirstOrDefaultAsync(u => u.Id == id);
            if(existingUser != null)
            {
                //apply updates to the existingUser
                existingUser.FirstName = userUpdates.FirstName;
                existingUser.LastName = userUpdates.LastName;
                existingUser.DateOfBirth = userUpdates.DateOfBirth;
                existingUser.Occupation = userUpdates.Occupation;
                existingUser.Email = userUpdates.Email;
                existingUser.NormalizedEmail = userUpdates.Email.ToUpperInvariant();
                existingUser.UserName = userUpdates.Email;
                existingUser.NormalizedUserName = userUpdates.Email.ToUpperInvariant();
                existingUser.LivingSituation = userUpdates.LivingSituation;
                existingUser.AdopterPetExperience = userUpdates.AdopterPetExperience;
                existingUser.AdopterAddress = userUpdates.AdopterAddress;

                await pawAdoptionDataContext.SaveChangesAsync();
                return existingUser;
            }
            return null;
        }


        public async Task<User> RemoveUserAsync(User userToDelete)
        {
            pawAdoptionDataContext.Remove(userToDelete);
            await pawAdoptionDataContext.SaveChangesAsync();
            return userToDelete;
        }


        public async Task<List<User>> GetAllFromDbAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100)
        {
            //Prepairing query
            var users = pawAdoptionDataContext.Users.Include("AdopterAddress").AsQueryable();

            //Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("FirstName", StringComparison.OrdinalIgnoreCase))
                {
                    users = users.Where(w => w.FirstName.Contains(filterQuery));
                }
                if (filterOn.Equals("LastName", StringComparison.OrdinalIgnoreCase))
                {
                    users = users.Where(w => w.LastName.Contains(filterQuery));
                }
                if (filterOn.Equals("Occupation", StringComparison.OrdinalIgnoreCase))
                {
                    users = users.Where(w => w.Occupation.Contains(filterQuery));
                }
                /* parsing filterQuery(string) to Species(enum).
                 * 'true' makes methods caseInsensitive.
                 * out var speciesValues holds the result of the parsing*/
                if (Enum.TryParse<AdopterLivingSituation>(filterQuery, true, out var adopterLivingSituationValues))
                {
                    users = users.Where(w => w.LivingSituation == adopterLivingSituationValues);
                }
                if (Enum.TryParse<AdopterPetExperience>(filterQuery, true, out var adopterPetExperienceValues))
                {
                    users = users.Where(w => w.AdopterPetExperience == adopterPetExperienceValues);
                }
                if (filterOn.Equals("Email", StringComparison.OrdinalIgnoreCase))
                {
                    users = users.Where(w => w.Email != null && w.Email.Contains(filterQuery));
                }

                //using navigational properties for filtering
                if (filterOn.Equals("AddressDetails", StringComparison.OrdinalIgnoreCase))
                {
                    users = users.Where(w => w.AdopterAddress!= null && w.AdopterAddress.AddressDetails.Contains(filterQuery));
                }
                if (filterOn.Equals("City", StringComparison.OrdinalIgnoreCase))
                {
                    users = users.Where(w => w.AdopterAddress != null && w.AdopterAddress.City.Contains(filterQuery));
                }
                if (filterOn.Equals("State", StringComparison.OrdinalIgnoreCase))
                {
                    users = users.Where(w => w.AdopterAddress != null && w.AdopterAddress.State.Contains(filterQuery));
                }
                if (filterOn.Equals("ZipCode", StringComparison.OrdinalIgnoreCase))
                {
                    users = users.Where(w => w.AdopterAddress != null && w.AdopterAddress.ZipCode.Contains(filterQuery));
                }
                //add more filtering logic here


            }
            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("DateOfBirth", StringComparison.OrdinalIgnoreCase))
                {
                    users = isAscending ? users.OrderBy(w => w.DateOfBirth) : users.OrderByDescending(w => w.DateOfBirth);
                }
                //add more sorting logic here

            }
            //Pagination
            var skipResult = (pageNumber - 1) * pageSize;

            //firing the prepaired query and returnig the List<Pet>
            return await users.Skip(skipResult).Take(pageSize).ToListAsync();
        }

        public async Task<List<UserImage>?> FindAllUserImagesByIdAsync(User existingUser)
        {
           var listImagePaths = await pawAdoptionDataContext.Users
                                      .Where(u => u.Id == existingUser.Id)
                                      .SelectMany(u =>  u.UserImages)
                                      .ToListAsync();
            return listImagePaths;
        }
    }
}
