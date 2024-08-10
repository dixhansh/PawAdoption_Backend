using PawAdoption_Backend.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace PawAdoption_Backend.Models.DTO
{
    public class MedicalRecordDto
    {     
        [Required]
        public PetHealthStatus HealthStatus { get; set; }

        [Required]
        public bool IsVaccinated { get; set; }

        [Required]
        public bool IsSpayedOrNeutered { get; set; }

        [Required]
        public Guid PetId { get; set; }
    }
}
