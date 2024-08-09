using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawAdoption_Backend.Models.Domain
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] /*Attribute above created at will automatically populate the field in DB when a record is created*/
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)] /*Here additional configuration needs to be done (done in PawAdoptionDataContext)*/
        public DateTime UpdatedAt { get; set; }
    }
}
