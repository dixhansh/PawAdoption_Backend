using PawAdoption_Backend.Models.DTO;

namespace PawAdoption_Backend.Services
{
    public interface IImageService
    {
        Task<ImageResponseDto?> UploadImageAsync(ImageUploadRequestDto imageUploadRequestDto);
    }
}
