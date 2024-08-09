using PawAdoption_Backend.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawAdoption_Backend.Models.Domain
{
    public class PetMedicalRecord : BaseEntity
    {
        [Required]
        public Guid PetId { get; set; }

        [Required]
        public PetHealthStatus HealthStatus { get; set; }

        [Required]
        public bool IsVaccinated { get; set; }

        [Required]
        public bool IsSpayedOrNeutered { get; set; }

        // Navigation property
        [ForeignKey("PetId")]
        public Pet Pet { get; set; }
    }
}
