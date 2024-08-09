using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawAdoption_Backend.Models.Domain
{
    public class AdopterAddress : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string AddressDetails { get; set; }

        [Required]
        [MaxLength(20)]
        public string City { get; set; }

        [Required]
        [MaxLength(20)]
        public string State { get; set; }

        [Required]
        [MaxLength(10)]
        public string ZipCode { get; set; }

        [Required]
        public Guid UserId { get; set; }

        //Navigational Properties
        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
