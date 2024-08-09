using AutoMapper;
using PawAdoption_Backend.Models.Domain;
using PawAdoption_Backend.Models.DTO;

namespace PawAdoption_Backend.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterRequestDto, User>().ReverseMap();
            CreateMap<AdopterAddressDto, AdopterAddress>().ReverseMap();
        }
    }
}
