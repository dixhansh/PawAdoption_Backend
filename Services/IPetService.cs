using PawAdoption_Backend.Models.Domain;
using PawAdoption_Backend.Models.DTO;

namespace PawAdoption_Backend.Services
{
    public interface IPetService
    { 
        Task<PetResponseDto> CreatePetAsync(CreatePetRequestDto petDto);
        Task<MedicalRecordDto?> UpdateMedicalRecordAsync(Guid petId,MedicalRecordDto petMedicalRecordDto);
        Task<List<PetResponseDto>> GetAllAsync(String? filterOn = null, String? filterQuery = null, String? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100);
        Task<PetResponseDto?> GetByIdAsync(Guid id);
       
    }
}
