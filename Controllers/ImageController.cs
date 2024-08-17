using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawAdoption_Backend.CustomActionFilters;
using PawAdoption_Backend.Models.DTO;
using PawAdoption_Backend.Services;

namespace PawAdoption_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
          
        }

        [HttpPost]
        [Route("Upload")]
        [ValidateModel]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            ValidateFileUpload(imageUploadRequestDto);
            var imageResponseDto = await imageService.UploadImageAsync(imageUploadRequestDto);
            if(imageResponseDto != null)
            {
                return Ok(imageResponseDto);
            }
            return BadRequest("Image not uploaded !");
            
        }

        //delete an image with entity and image id
        [HttpDelete]
        [Route("Delete/{imageId:Guid}/{entityId:Guid}")]
        public async Task<IActionResult> DeleteImage([FromRoute] Guid imageId,[FromRoute] Guid entityId)
        {
            var result = await imageService.DeleteImageAsync(imageId, entityId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Invalid credentials, image not deleted!");
        }




        //Validating image
        private void ValidateFileUpload(ImageUploadRequestDto requestDto)
        {
            var allowedExtensions = new String[] { ".jpg", ".jpeg", ".png", ".gif" };

            if (!allowedExtensions.Contains(Path.GetExtension(requestDto.File.FileName)))
            {
                ModelState.AddModelError("file", "unsupported file extension");
            }

            if (requestDto.File.Length > 10485760) //File size should be less than 10MB
            {
                ModelState.AddModelError("file", "File size is more than 10MB");
            }

        }






    }
}
