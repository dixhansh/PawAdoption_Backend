using PawAdoption_Backend.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawAdoption_Backend.Models.Domain
{
    public class AdoptionApplication : BaseEntity
    {
        [Required]
        public Guid AdopterId { get; set; }

        [Required]
        public Guid PetId {get; set;}

        [Required]
        public ApplicationStatus ApplicationStatus { get; set; }

        [Required]
        public bool ReferenceCheck { get; set; } //completed or pending 

        [MaxLength(1000)]
        public String? ReasonOfRejection { get; set; }

        public bool IsFeePaid { get; set; }

        public Guid? ProcessedByAdmin { get; set; } 

        //Navigational Properties
        [ForeignKey("PetId")]
        public Pet Pet { get; set; } 

        [ForeignKey("AdopterId")]
        public User Adopter { get; set; } 

        [ForeignKey("ProcessedByAdmin")]
        public User? Admin { get; set; } 

        public AdoptionBill? AdoptionBill { get; set; } 
    }
}
