using PawAdoption_Backend.Models.Domain;
using PawAdoption_Backend.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PawAdoption_Backend.Models.DTO
{
    public class UserResponseDto
    { 
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public string LastName { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string Occupation { get; set; }

        public AdopterLivingSituation LivingSituation { get; set; }

        public AdopterPetExperience AdopterPetExperience { get; set; }

        //Navigational Properties
        public AdopterAddressDto? AdopterAddress { get; set; }

    }
}
