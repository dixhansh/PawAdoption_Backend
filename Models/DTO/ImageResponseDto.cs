using System.ComponentModel.DataAnnotations;

namespace PawAdoption_Backend.Models.DTO
{
    public class ImageResponseDto
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string? FileDescription { get; set; }
      
        public string FileExtension { get; set; }
     
        public long FileSizeInBytes { get; set; }

        public string FilePath { get; set; }

        public Guid EntityId { get; set; }
    }
}
