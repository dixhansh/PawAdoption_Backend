using System.ComponentModel.DataAnnotations;

namespace PawAdoption_Backend.Models.DTO
{
    public class AdoptionApplicatonRequestDto
    {
        [Required]
        public Guid AdopterId { get; set; }

        [Required]
        public Guid PetId { get; set; }
    }
}
