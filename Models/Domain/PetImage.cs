using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawAdoption_Backend.Models.Domain
{
    public class PetImage : BaseEntity
    {
        [NotMapped]
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string FileName { get; set; }

        public string? FileDescription { get; set; }

        [Required]
        public string FileExtension { get; set; }

        [Required]
        public long FileSizeInBytes { get; set; }

        [Required]
        public string FilePath { get; set; }

        public Guid? PetId { get; set; }

        //Navigational Property
        public Pet? Pet { get; set; }
    }
}
