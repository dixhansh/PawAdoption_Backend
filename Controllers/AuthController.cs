using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PawAdoption_Backend.CustomActionFilters;
using PawAdoption_Backend.Models.Domain;
using PawAdoption_Backend.Models.DTO;
using PawAdoption_Backend.Services;

namespace PawAdoption_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public AuthController(IMapper mapper, IUserService userService)
        {
            this.mapper = mapper;
            this.userService = userService;
        }

        [HttpPost]
        [Route("Register")]
        [ValidateModel]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            //Mapping Dto to DomainModel  
            var adopterAddress = mapper.Map<AdopterAddress>(registerRequestDto.AdopterAddressDto);
            var user = new User
            {
                UserName = registerRequestDto.Email,
                FirstName = registerRequestDto.FirstName,
                LastName = registerRequestDto.LastName,
                DateOfBirth = registerRequestDto.DateOfBirth,
                Occupation = registerRequestDto.Occupation,
                LivingSituation = registerRequestDto.LivingSituation,
                AdopterPetExperience = registerRequestDto.AdopterPetExperience,
                Email = registerRequestDto.Email
            };

            var result = await userService.RegisterAsync(user,adopterAddress,registerRequestDto.Password);
            if(result == null)
            {
                return BadRequest("Something went wrong!");
            }
            return Ok(result);
            
        }

        [HttpPost]
        [Route("Login")]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var jwtResponseDto = await userService.ValidateUserAsync(loginRequest);
           if(jwtResponseDto != null)
            {
                return Ok(jwtResponseDto);
            }

            return BadRequest("Invalid user credentials!");
        }

    }

    


}
