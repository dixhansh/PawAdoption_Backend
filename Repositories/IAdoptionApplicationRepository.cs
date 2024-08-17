using PawAdoption_Backend.Models.Domain;

namespace PawAdoption_Backend.Repositories
{
    public interface IAdoptionApplicationRepository
    {
        Task<string> AddAdoptionApplication(AdoptionApplication adoptionApplication);
        Task<List<AdoptionApplication>> GetAllAdoptionApplicationFromDbAsync(String? filterOn = null, String? filterQuery = null, String? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100);
    }
}
