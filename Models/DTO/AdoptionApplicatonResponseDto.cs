using PawAdoption_Backend.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace PawAdoption_Backend.Models.DTO
{
    public class AdoptionApplicatonResponseDto
    {
       
        public Guid Id { get; set; }

        public Guid AdopterId { get; set; }

       
        public Guid PetId { get; set; }

   
        public ApplicationStatus ApplicationStatus { get; set; }

      
        public bool ReferenceCheck { get; set; } //completed or pending 

       
        public String? ReasonOfRejection { get; set; }

        public bool IsFeePaid { get; set; }

        public Guid? ProcessedByAdmin { get; set; }
    }
}
