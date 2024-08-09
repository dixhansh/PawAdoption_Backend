using Microsoft.AspNetCore.Identity;
using PawAdoption_Backend.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace PawAdoption_Backend.Models.Domain
{
    public class User : IdentityUser<Guid>
    {
        public Guid Id { get; set; }

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
      
        //Navigational Properties
        public AdopterAddress? AdopterAddress { get; set; }

    }
}
