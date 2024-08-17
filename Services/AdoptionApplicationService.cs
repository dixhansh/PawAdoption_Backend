using AutoMapper;
using Azure.Core;
using PawAdoption_Backend.Models.Domain;
using PawAdoption_Backend.Models.DTO;
using PawAdoption_Backend.Repositories;

namespace PawAdoption_Backend.Services
{
    public class AdoptionApplicationService : IAdoptionApplicationService
    {
        private readonly IMapper mapper;
        private readonly IAdoptionApplicationRepository adoptionApplicationRepository;

        public AdoptionApplicationService(IMapper mapper,IAdoptionApplicationRepository adoptionApplicationRepository)
        {
            this.mapper = mapper;
            this.adoptionApplicationRepository = adoptionApplicationRepository;
        }

        public async Task<string?> CreateAdoptionApplicatonAsync(AdoptionApplicatonRequestDto adoptionApplicatonRequestDto)
        {
            
                AdoptionApplication adoptionApplication = new AdoptionApplication()
                {
                    AdopterId = adoptionApplicatonRequestDto.AdopterId,
                    PetId = adoptionApplicatonRequestDto.PetId,
                    ApplicationStatus = Models.Enum.ApplicationStatus.Pending,
                    ReferenceCheck = false,
                    ReasonOfRejection = null,
                    IsFeePaid = false,
                    ProcessedByAdmin = null
                };

                var result = await adoptionApplicationRepository.AddAdoptionApplication(adoptionApplication);
                return result;
            
         
           
        }


        public async Task<List<AdoptionApplicatonResponseDto>> GetAllAdoptionApplicationAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100)
        {
            var adoptionApplicationList = await adoptionApplicationRepository.GetAllAdoptionApplicationFromDbAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);

            return (mapper.Map<List<AdoptionApplicatonResponseDto>>(adoptionApplicationList));

        }
    }
}
