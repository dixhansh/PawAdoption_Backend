using PawAdoption_Backend.Models.Domain;

namespace PawAdoption_Backend.Repositories
{
    public interface IPetRepository
    { 
        Task<Pet> AddPetAsync(Pet pet);
        Task<PetMedicalRecord?> RenewMedicalRecordAsync(PetMedicalRecord medicalRecord,Guid medicalRecordId);
        Task<PetMedicalRecord> CreateMedicalRecordAsync(PetMedicalRecord medicalRecord);
        Task<Pet?> FindPetByIdAsync(Guid id);
        Task<List<Pet>> GetAllFromDbAsync(String? filterOn = null, String? filterQuery = null, String? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100);
        Task<Pet> RemovePetAsync(Pet pet);
        Task<Pet?> RenewPetRecordAsync(Guid id, Pet petUpdates);
        Task<List<PetImage>?> FetchAllPetImagesByIdAsync(Guid id);
    }
}
