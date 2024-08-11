using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PawAdoption_Backend.Data;
using PawAdoption_Backend.Models.Domain;
using PawAdoption_Backend.Models.Enum;

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
                    /* parsing filterQuery(string) to Species(enum).
                     * 'true' makes methods caseInsensitive.
                     * out var speciesValues holds the result of the parsing*/
                if (Enum.TryParse<Species>(filterQuery, true, out var speciesValue))
                { 
                    pets = pets.Where(w => w.Species == speciesValue);
                }
                if (Enum.TryParse<Gender>(filterQuery, true, out var genderValue))
                {
                    pets = pets.Where(w => w.Gender == genderValue);
                }
                if (filterOn.Equals("Color", StringComparison.OrdinalIgnoreCase))
                {
                    pets = pets.Where(w => w.Color.Contains(filterQuery));
                }
                if (Enum.TryParse<AdoptionStatus>(filterQuery, true, out var adoptionStatusValue))
                {
                    pets = pets.Where(w => w.AdoptionStatus == adoptionStatusValue);
                }
                //using navigational properties for filtering
                if (Enum.TryParse <PetHealthStatus>(filterQuery, true, out var healthStatusValue))
                {
                    pets = pets.Where(w => w.PetMedicalRecord != null && w.PetMedicalRecord.HealthStatus == healthStatusValue);
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

        public async Task<Pet> RemovePetAsync(Pet pet)
        {
            pawAdoptionDataContext.Pets.Remove(pet);
            await pawAdoptionDataContext.SaveChangesAsync();
            return pet;
        }

        public async Task<Pet?> RenewPetRecordAsync(Guid id, Pet petUpdates)
        {
            //finding pet record that needs to be updated
            var existingPet = await pawAdoptionDataContext.Pets.FindAsync(id);
            if (existingPet != null)
            {
                existingPet.Name = petUpdates.Name;
                existingPet.Species = petUpdates.Species;
                existingPet.Breed = petUpdates.Breed;
                existingPet.Age = petUpdates.Age;
                existingPet.Gender = petUpdates.Gender;
                existingPet.Color = petUpdates.Color;
                existingPet.Weight = petUpdates.Weight;
                existingPet.ArrivalDate = petUpdates.ArrivalDate;
                existingPet.AdoptionStatus = petUpdates.AdoptionStatus;
                existingPet.AdoptionDate = petUpdates.AdoptionDate;
                existingPet.Description = petUpdates.Description;

                await pawAdoptionDataContext.SaveChangesAsync();
                return (existingPet);
            }
            return (null);
        }
    }
}
