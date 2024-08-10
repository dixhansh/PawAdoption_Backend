using Microsoft.AspNetCore.Identity;
using PawAdoption_Backend.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawAdoption_Backend.Models.Domain
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

   
        [MaxLength(50)]
        public string Occupation { get; set; }


        public AdopterLivingSituation LivingSituation { get; set; }

     
        public AdopterPetExperience AdopterPetExperience { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        
        //Navigational Properties
      
        public AdopterAddress? AdopterAddress { get; set; }

      
        public virtual ICollection<AdoptionApplication>? SubmittedApplications { get; set; } //virtual keyword enables LazyLoading

      
        public virtual ICollection<AdoptionApplication>? ProcessedApplications { get; set; }

        public virtual ICollection<AdoptionBill>? AdoptionBills { get; set; }

        public virtual ICollection<AdoptionBill>? ProcessedBills { get; set; }


    }
}
