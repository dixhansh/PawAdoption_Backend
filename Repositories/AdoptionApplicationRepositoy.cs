using Microsoft.EntityFrameworkCore;
using PawAdoption_Backend.Data;
using PawAdoption_Backend.Models.Domain;

namespace PawAdoption_Backend.Repositories
{
    public class AdoptionApplicationRepositoy : IAdoptionApplicationRepository
    {
        private readonly PawAdoptionDataContext pawAdoptionDataContext;

        public AdoptionApplicationRepositoy(PawAdoptionDataContext pawAdoptionDataContext)
        {
            this.pawAdoptionDataContext = pawAdoptionDataContext;
        }
        public async Task<string> AddAdoptionApplication(AdoptionApplication adoptionApplication)
        {
            await pawAdoptionDataContext.AddAsync(adoptionApplication);
            await pawAdoptionDataContext.SaveChangesAsync();
            return ("application created");
        }

        public async Task<List<AdoptionApplication>> GetAllAdoptionApplicationFromDbAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100)
        {
            /*var adoptionApplications = pawAdoptionDataContext.AdoptionApplications.AsQueryable();*/

            //Filtering


            //Sorting


            //Pagination

            return await pawAdoptionDataContext.AdoptionApplications.ToListAsync();


        }
    }
}
