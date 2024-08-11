using Microsoft.AspNetCore.Mvc;
using PawAdoption_Backend.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace PawAdoption_Backend.Models.Domain
{
    public class Pet : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public Species Species { get; set; }

        [MaxLength(100)]
        public string? Breed { get; set; }

        [Range(0, 50)]
        public int? Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [MaxLength(20)]
        public string Color { get; set; }

        [Range(0, 200)]
        public double? Weight { get; set; }

        [Required]
        public DateOnly ArrivalDate { get; set; }

        [Required]
        public AdoptionStatus AdoptionStatus { get; set; }

        public DateOnly? AdoptionDate { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        // Navigational Properties
        
        public PetMedicalRecord? PetMedicalRecord { get; set; }

        public virtual ICollection<AdoptionApplication>? SubmittedApplications { get; set; } //virtual keyword enables LazyLoading

        public virtual ICollection<PetImage>? PetImages { get; set; }

    }
}
