using AutoMapper;
using PawAdoption_Backend.Models.Domain;
using PawAdoption_Backend.Models.DTO;

namespace PawAdoption_Backend.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AdopterAddressDto, AdopterAddress>().ReverseMap();
            CreateMap<MedicalRecordDto, PetMedicalRecord>().ReverseMap();
            CreateMap<CreatePetRequestDto, Pet>().ReverseMap();
            CreateMap<Pet, PetResponseDto>().ReverseMap();
            CreateMap<MedicalRecordDto, PetMedicalRecord>().ReverseMap();
        }
    }
}
