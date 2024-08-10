using PawAdoption_Backend.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawAdoption_Backend.Models.Domain
{
    public class AdoptionBill : BaseEntity
    {
        [Required]
        public Guid AdoptionApplicationId { get; set; }

        [Required]
        public Guid AdopterId{ get; set; }

        [Required]
        public double AdoptionFee { get; set; }

        [Required]
        public PaymentStatus PaymentStatus { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [MaxLength(50)]
        public string? TransactionId { get; set; }

        [Required]
        public DateOnly DueDate { get; set; }
       
        public Guid? ProcessedByAdmin { get; set; }

        //Navigational Properties
        [ForeignKey("AdoptionApplicationId")]
        public AdoptionApplication AdoptionApplication { get; set; }

        [ForeignKey("AdopterId")]
        public User Adopter { get; set; }

        [ForeignKey("ProcessedByAdmin")]
        public User? Admin { get; set; }
    }
}
