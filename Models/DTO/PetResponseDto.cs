using PawAdoption_Backend.Models.Domain;
using PawAdoption_Backend.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace PawAdoption_Backend.Models.DTO
{
    public class PetResponseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Species Species { get; set; }

        public string? Breed { get; set; }

        public int? Age { get; set; }

        public Gender Gender { get; set; }

        public string Color { get; set; }

        public double? Weight { get; set; }

        public DateOnly ArrivalDate { get; set; }

        public AdoptionStatus AdoptionStatus { get; set; }

        public DateOnly? AdoptionDate { get; set; }

        public string? Description { get; set; }

                                //keeping the field name same as in Pet.cs
        public MedicalRecordDto? PetMedicalRecord { get; set; }

    }
}
