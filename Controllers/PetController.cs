using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawAdoption_Backend.CustomActionFilters;
using PawAdoption_Backend.Models.Domain;
using PawAdoption_Backend.Models.DTO;
using PawAdoption_Backend.Services;

namespace PawAdoption_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(Roles ="Admin")]
    public class PetController : ControllerBase
    {
        private readonly IPetService petService;

        public PetController(IPetService petService)
        {
            this.petService = petService;
        }

        //Create a Pet Record
        [HttpPost]
        [Route("PetRecord")]
        [ValidateModel]
        public async Task<IActionResult> CreatePet([FromBody] CreatePetRequestDto createPetRequestDto)
        {
            var petResponseDto = await petService.CreatePetAsync(createPetRequestDto);
            if (petResponseDto != null)
            {
                return CreatedAtAction(nameof(GetPetById), new { id = petResponseDto.Id }, petResponseDto); //returning a 201 response
            }
            return BadRequest("New pet record NOT created !");
        }

        //Update if medical record found and create if not exist
        [HttpPut]
        [Route("MedicalRecord/{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateMedicalRecord([FromRoute] Guid id, [FromBody] MedicalRecordDto petMedicalRecordDto)
        {
            var updatedMedicalRecordDto = await petService.UpdateMedicalRecordAsync(id, petMedicalRecordDto);
            if(updatedMedicalRecordDto != null)
            {
                return Ok(updatedMedicalRecordDto);
            }
            return BadRequest("MedicalRecord NOT updated !");

        }


        //Get all pets(emums should be sent by name)
        [HttpGet]
        [ValidateModel]
        public async Task<IActionResult> GetAllPets([FromQuery] String? filterOn, [FromQuery] String? filterQuery, [FromQuery] String? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var petDtoList = await petService.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            return Ok(petDtoList);


        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> GetPetById([FromRoute] Guid id)
        {
            var petResponseDto = await petService.GetByIdAsync(id);
            if(petResponseDto != null)
            {
                return Ok(petResponseDto);
            }
            return BadRequest("Pet not found");
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> DelelePet([FromRoute] Guid id)
        {
            var deletedPetDto = await petService.DeletePetAsync(id);
            if(deletedPetDto != null)
            {
                return Ok(deletedPetDto);
            }
            return BadRequest("Pet record not found/deleted !");
        }

        [HttpPut]
        [Route("PetRecord/{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdatePet([FromRoute] Guid id, [FromBody] CreatePetRequestDto petRequestDto)
        {
            var updatedPetResponseDto = await petService.UpdatePetAsync(id, petRequestDto);
            if(updatedPetResponseDto != null)
            {
                return Ok(updatedPetResponseDto);
            }
            return BadRequest("Updates could not be applied due to invalid Id");
        }

        [HttpGet]
        [Route("PetImages/{id:Guid}")]
        public async Task<IActionResult> GetPetImagesById([FromRoute] Guid id)
        {
            var listOfImages = await petService.GetPetImagesByIdAsync(id);
            if (listOfImages != null)
            {
                return Ok(listOfImages);
            }
            return BadRequest("Images could not be found !!!");
        }
    }
}
