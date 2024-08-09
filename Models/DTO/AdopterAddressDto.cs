using System.ComponentModel.DataAnnotations;

namespace PawAdoption_Backend.Models.DTO
{
    public class AdopterAddressDto
    {
        [Required(ErrorMessage = "Address details are required.")]
        [MaxLength(100, ErrorMessage = "Address details cannot exceed 100 characters.")]
        public string AddressDetails { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [MaxLength(20, ErrorMessage = "City cannot exceed 20 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [MaxLength(20, ErrorMessage = "State cannot exceed 20 characters.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Zip code is required.")]
        [MaxLength(10, ErrorMessage = "Zip code cannot exceed 10 characters.")]
        public string ZipCode { get; set; }  // Changed to string to handle various formats
    }
}
