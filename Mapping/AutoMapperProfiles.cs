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
            CreateMap<User, UserResponseDto>().ReverseMap();
            CreateMap<User, UserRequestDto>().ReverseMap();

            CreateMap<MedicalRecordDto, PetMedicalRecord>().ReverseMap();
            CreateMap<CreatePetRequestDto, Pet>().ReverseMap();
            CreateMap<Pet, PetResponseDto>().ReverseMap();
            CreateMap<MedicalRecordDto, PetMedicalRecord>().ReverseMap();

            CreateMap<PetImage, ImageResponseDto>().ForMember(x => x.EntityId, opt => opt.MapFrom(x => x.PetId)).ReverseMap();
            CreateMap<UserImage, ImageResponseDto>().ForMember(x => x.EntityId, opt => opt.MapFrom(x => x.UserId)).ReverseMap();



        }
    }
}
