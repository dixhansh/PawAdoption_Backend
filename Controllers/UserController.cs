using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawAdoption_Backend.CustomActionFilters;
using PawAdoption_Backend.Services;

namespace PawAdoption_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        //get user(adopter) by id 
        [HttpGet]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var userResponseDto = await userService.GetUserByIdAsync(id);
            if(userResponseDto != null)
            {
                return Ok(userResponseDto);
            }
            return BadRequest("User not found !!!");

        }

    }
}
