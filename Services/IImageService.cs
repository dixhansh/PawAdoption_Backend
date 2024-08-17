using PawAdoption_Backend.Models.DTO;

namespace PawAdoption_Backend.Services
{
    public interface IImageService
    {
        Task<string?> DeleteImageAsync(Guid imageId, Guid entityId);
        Task<ImageResponseDto?> UploadImageAsync(ImageUploadRequestDto imageUploadRequestDto);

    }
}
