using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        /* public async Task<PetImage> UploadPetImageAsync(PetImage image)
         {
             try
             {
                 var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

                 // Uploading image to the localFilePath
                 using var stream = new FileStream(localFilePath, FileMode.Create);
                 await image.File.CopyToAsync(stream);

                 var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

                 image.FilePath = urlFilePath;

                 // Add the image to the Image table
                 await pawAdoptionDataContext.PetImages.AddAsync(image);
                 await pawAdoptionDataContext.SaveChangesAsync();

                 return image;
             }
             catch (DbUpdateException ex)
             {
                 // Log and/or examine the inner exception for more details
                 var sqlException = ex.InnerException as SqlException;
                 if (sqlException != null)
                 {
                     Console.WriteLine($"SQL Error: {sqlException.Message}");
                     Console.WriteLine($"SQL Error Number: {sqlException.Number}");
                     // Additional logging as needed
                 }
                 else
                 {
                     Console.WriteLine($"Error: {ex.Message}");
                     // Handle other types of inner exceptions
                 }

                 throw;  // Rethrow the exception to preserve the stack trace
             }
         }*/


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

 
    }
}
