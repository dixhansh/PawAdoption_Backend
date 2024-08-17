using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using PawAdoption_Backend.Data;
using PawAdoption_Backend.Models.Domain;

namespace PawAdoption_Backend.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly PawAdoptionDataContext pawAdoptionDataContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, PawAdoptionDataContext pawAdoptionDataContext)
        {          
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.pawAdoptionDataContext = pawAdoptionDataContext;
        }

        public async Task<PetImage> UploadPetImageAsync(PetImage image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            //uploading image to the localFilePath
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            //Add the image to the Image table
            await pawAdoptionDataContext.PetImages.AddAsync(image);
            await pawAdoptionDataContext.SaveChangesAsync();
            return (image);

        }

        public async Task<UserImage> UploadUserImageAsync(UserImage image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            //uploading image to the localFilePath
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            //Add the image to the Image table
            await pawAdoptionDataContext.UserImages.AddAsync(image);
            await pawAdoptionDataContext.SaveChangesAsync();
            return image;

        }


        public async Task<string?> RemovePetImageByIdAsync(Guid imageId)
        {
            //checking if image exist
            var image = await pawAdoptionDataContext.PetImages.FindAsync(imageId);
            if (image != null)
            {
                //Removing Image record from the database
                pawAdoptionDataContext.PetImages.Remove(image);
                var url = image.FilePath;
                var fileName = Path.GetFileName(new Uri(url).LocalPath); // Extracts "newimage.jpg"

                // Get the physical path to the project root
                var projectRootPath = webHostEnvironment.ContentRootPath;

                // Combine to get the full file path under the Images folder in the project root
                var filePath = Path.Combine(projectRootPath, "Images", fileName);
                //Removing Image from the local repo
                if (File.Exists(filePath))
                {
                    await Task.Run(() => File.Delete(filePath));
                    if (!File.Exists(filePath))
                    {
                        await pawAdoptionDataContext.SaveChangesAsync();
                        return ("Pet Image deleted successfully");
                    }    
                }
            }
            return null;
        }

       

    }
}
