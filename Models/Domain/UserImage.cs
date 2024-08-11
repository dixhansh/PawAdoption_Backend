using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PawAdoption_Backend.Models.Domain
{
    public class UserImage : BaseEntity
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

        public Guid? UserId { get; set; }

        //Navigational Property
        public User? User { get; set; }
    }
}
