using PawAdoption_Backend.Models.DTO;

namespace PawAdoption_Backend.Services
{
    public interface IAdoptionApplicationService
    {
        Task<string?> CreateAdoptionApplicatonAsync(AdoptionApplicatonRequestDto adoptionApplicatonRequestDto);
        Task<List<AdoptionApplicatonResponseDto>> GetAllAdoptionApplicationAsync(String? filterOn = null, String? filterQuery = null, String? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100);

    }
}
