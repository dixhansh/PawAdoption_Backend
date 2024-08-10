using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PawAdoption_Backend.Data;
using PawAdoption_Backend.Models.Domain;

namespace PawAdoption_Backend.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly PawAdoptionDataContext pawAdoptionDataContext;

        public PetRepository(PawAdoptionDataContext pawAdoptionDataContext)
        {
            this.pawAdoptionDataContext = pawAdoptionDataContext;
        }

        public async Task<Pet> AddPetAsync(Pet pet)
        {
            await pawAdoptionDataContext.Pets.AddAsync(pet);
            await pawAdoptionDataContext.SaveChangesAsync();

            return pet;
        }

        public async Task<PetMedicalRecord?> RenewMedicalRecordAsync(PetMedicalRecord medicalRecord, Guid id)
        {
            var existingMedicalRecord = await pawAdoptionDataContext.PetMedicalRecords.FindAsync(id);
            
            if(existingMedicalRecord != null)
            {
                existingMedicalRecord.HealthStatus = medicalRecord.HealthStatus;
                existingMedicalRecord.IsVaccinated = medicalRecord.IsVaccinated;
                existingMedicalRecord.IsSpayedOrNeutered = medicalRecord.IsSpayedOrNeutered;
                existingMedicalRecord.PetId = existingMedicalRecord.PetId;

                await pawAdoptionDataContext.SaveChangesAsync();
                var updatedMedicalRecord = await pawAdoptionDataContext.PetMedicalRecords.FindAsync(id);
                return (updatedMedicalRecord);
            }
            return (null);
        }

        public async Task<PetMedicalRecord> CreateMedicalRecordAsync(PetMedicalRecord medicalRecord)
        {
            await pawAdoptionDataContext.PetMedicalRecords.AddAsync(medicalRecord);
            await pawAdoptionDataContext.SaveChangesAsync();
            return (medicalRecord);
        }

        public async Task<Pet?> FindPetById(Guid id)
        {
            var existingPet = await pawAdoptionDataContext.Pets.Include("PetMedicalRecord").FirstOrDefaultAsync(w => w.Id == id);
            if (existingPet == null)
            {
                return null;
            }
            return existingPet;
        }

        public async Task<List<Pet>> GetAllFromDbAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100)
        {
            //Prepairing query
            var pets = pawAdoptionDataContext.Pets.Include("PetMedicalRecord").AsQueryable();

            //Filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    pets = pets.Where(w => w.Name.Contains(filterQuery));
                }
                if (filterOn.Equals("Breed", StringComparison.OrdinalIgnoreCase))
                {
                    pets = pets.Where(w => w.Breed != null && w.Breed.Contains(filterQuery));
                }
                if (filterOn.Equals("Species", StringComparison.OrdinalIgnoreCase))
                {
                    pets = pets.Where(w => w.Species.ToString().Contains(filterQuery));
                }
                if (filterOn.Equals("Gender", StringComparison.OrdinalIgnoreCase))
                {
                    pets = pets.Where(w => w.Gender.ToString().Contains(filterQuery));
                }
                if (filterOn.Equals("Color", StringComparison.OrdinalIgnoreCase))
                {
                    pets = pets.Where(w => w.Color.Contains(filterQuery));
                }
                if (filterOn.Equals("AdoptionStatus", StringComparison.OrdinalIgnoreCase))
                {
                    pets = pets.Where(w => w.AdoptionStatus.ToString().Contains(filterQuery));
                }
                //using navigational properties for filtering
                if (filterOn.Equals("AdoptionStatus", StringComparison.OrdinalIgnoreCase))
                {
                    pets = pets.Where(w => w.PetMedicalRecord != null && w.PetMedicalRecord.HealthStatus.ToString().Contains(filterQuery)); ;
                }
                //add more filtering logic here


            }
            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
                {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    pets = isAscending ? pets.OrderBy(w => w.Name) : pets.OrderByDescending(w => w.Name);
                }
                if (sortBy.Equals("Age", StringComparison.OrdinalIgnoreCase))
                {
                    pets = isAscending ? pets.OrderBy(w => w.Age) : pets.OrderByDescending(w => w.Age);
                }
                if (sortBy.Equals("Weight", StringComparison.OrdinalIgnoreCase))
                {
                    pets = isAscending ? pets.OrderBy(w => w.Weight) : pets.OrderByDescending(w => w.Weight);
                }
                if (sortBy.Equals("ArrivalDate", StringComparison.OrdinalIgnoreCase))
                {
                    pets = isAscending ? pets.OrderBy(w => w.ArrivalDate) : pets.OrderByDescending(w => w.ArrivalDate);
                }
                if (sortBy.Equals("AdoptionDate", StringComparison.OrdinalIgnoreCase))
                {
                    pets = isAscending ? pets.OrderBy(w => w.AdoptionDate) : pets.OrderByDescending(w => w.AdoptionDate);
                }
                //add more sorting logic here

            }
            //Pagination
            var skipResult = (pageNumber - 1) * pageSize;

            //firing the prepaired query and returnig the List<Pet>
            return await pets.Skip(skipResult).Take(pageSize).ToListAsync();
        }
    }
}
