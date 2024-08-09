using PawAdoption_Backend.Models.Domain;
using PawAdoption_Backend.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace PawAdoption_Backend.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }


        [MaxLength(50)]
        public string Occupation { get; set; }


        public AdopterLivingSituation LivingSituation { get; set; }

      
        public AdopterPetExperience AdopterPetExperience { get; set; }

        //Navigational Properties
        public AdopterAddressDto? AdopterAddressDto { get; set; }
    }
}
