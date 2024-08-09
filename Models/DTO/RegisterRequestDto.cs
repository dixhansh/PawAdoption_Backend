using PawAdoption_Backend.Models.Domain;
using PawAdoption_Backend.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace PawAdoption_Backend.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email address must be a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password, ErrorMessage = "Password must be provided.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Occupation is required.")]
        [MaxLength(50, ErrorMessage = "Occupation cannot exceed 50 characters.")]
        public string Occupation { get; set; }

        [Required(ErrorMessage = "Living situation is required.")]
        public AdopterLivingSituation LivingSituation { get; set; }

        [Required(ErrorMessage = "Adopter pet experience is required.")]
        public AdopterPetExperience AdopterPetExperience { get; set; }

        // Navigational Properties
        [Required(ErrorMessage = "Adopter address is required.")]
        public AdopterAddressDto AdopterAddressDto { get; set; }
    }

}

