using System.ComponentModel.DataAnnotations;

namespace PawAdoption_Backend.Models.DTO
{
    public class AdopterAddressDto
    {
        [Required]
        [MaxLength(100)]
        public string AddressDetails { get; set; }

        [Required]
        [MaxLength(20)]
        public string City { get; set; }

        [Required]
        [MaxLength(20)]
        public string State { get; set; }

        [Required]
        [MaxLength(10)]
        public int ZipCode { get; set; }
    }
}
