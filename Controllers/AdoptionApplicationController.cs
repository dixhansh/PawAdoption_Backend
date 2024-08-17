using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using PawAdoption_Backend.CustomActionFilters;
using PawAdoption_Backend.Models.DTO;
using PawAdoption_Backend.Repositories;
using PawAdoption_Backend.Services;

namespace PawAdoption_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoptionApplicationController : ControllerBase
    {
        private readonly IAdoptionApplicationService adoptionApplicationService;

        public AdoptionApplicationController(IAdoptionApplicationService adoptionApplicationService)
        {
            this.adoptionApplicationService = adoptionApplicationService;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateAdoptionApplicaton([FromBody] AdoptionApplicatonRequestDto adoptionApplicatonRequestDto)
        {
            var result = await adoptionApplicationService.CreateAdoptionApplicatonAsync(adoptionApplicatonRequestDto);
            if(result != null)
            {
                return Ok(result);
            }
            return BadRequest("Adoption application can't be created"); 
        }

        [HttpGet]
        [ValidateModel]
        public async Task<IActionResult> GetAllAdoptionApplication([FromQuery] String? filterOn, [FromQuery] String? filterQuery, [FromQuery] String? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var petDtoList = await adoptionApplicationService.GetAllAdoptionApplicationAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            return Ok(petDtoList);

        }
    }
}
