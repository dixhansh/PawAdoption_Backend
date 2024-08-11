using PawAdoption_Backend.Models.Domain;

namespace PawAdoption_Backend.Repositories
{
    public interface IImageRepository
    {
        Task<PetImage> UploadPetImageAsync(PetImage petImage);
        Task<UserImage> UploadUserImageAsync(UserImage userImage);
    }
}
