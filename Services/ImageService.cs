using AutoMapper;
using PawAdoption_Backend.Models.Domain;
using PawAdoption_Backend.Models.DTO;
using PawAdoption_Backend.Repositories;

namespace PawAdoption_Backend.Services
{
    public class ImageService : IImageService
    {
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;
        private readonly IPetRepository petRepository;
        private readonly IUserRepositoy userRepositoy;

        public ImageService(IMapper mapper, IImageRepository imageRepository, IPetRepository petRepository, IUserRepositoy userRepositoy)
        {
            this.mapper = mapper;
            this.imageRepository = imageRepository;
            this.petRepository = petRepository;
            this.userRepositoy = userRepositoy;
        }

        public async Task<ImageResponseDto?> UploadImageAsync(ImageUploadRequestDto requestDto)
        {
            //checking entity type
            var entity = await CheckEntityType(requestDto.EntityId);
            
            if (entity != null && entity.Contains("pet"))
            {
                //Mapping dto to Model
                var petImage = new PetImage
                {
                    File = requestDto.File,
                    FileExtension = Path.GetExtension(requestDto.File.FileName),
                    FileSizeInBytes = requestDto.File.Length,
                    FileName = requestDto.FileName,
                    FileDescription = requestDto.FileDescription,
                    PetId = requestDto.EntityId
                };
                petImage = await imageRepository.UploadPetImageAsync(petImage);
                return (mapper.Map<ImageResponseDto>(petImage));
            }
            if (entity != null && entity.Contains("user"))
            {
                //Mapping dto to Model
                var userImage = new UserImage
                {
                    File = requestDto.File,
                    FileExtension = Path.GetExtension(requestDto.File.FileName),
                    FileSizeInBytes = requestDto.File.Length,
                    FileName = requestDto.FileName,
                    FileDescription = requestDto.FileDescription,
                    UserId = requestDto.EntityId
                };
                userImage =  await imageRepository.UploadUserImageAsync(userImage);
                return (mapper.Map<ImageResponseDto>(userImage));
            }
            return (null);
        }

        public async Task<string?> DeleteImageAsync(Guid imageId, Guid entityId)
        {
            var entity = await CheckEntityType(entityId);

            if (entity != null && entity.Contains("pet"))
            {
                var result = await imageRepository.RemovePetImageByIdAsync(imageId);
                if (result != null)
                {
                    return(result);
                }
            }
            if (entity != null && entity.Contains("user"))
            {
               
            }
            return (null);
        }

        //Finding which entityId belongs to which table 
        private async Task<string?> CheckEntityType(Guid entityId)
        {
            var petEntity = await petRepository.FindPetByIdAsync(entityId);
            if (petEntity != null)
            {
                return ("pet");
            }
            var userEntity = await userRepositoy.FindUserById(entityId);
            if (userEntity != null)
            {
                return ("user");
            }
            return (null);

        }

       


    }
}
